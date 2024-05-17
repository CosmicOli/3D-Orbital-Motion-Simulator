using System.Numerics;

namespace _3D_Orbital_Motion_Simulation
{
    internal struct BodyTree
    {
        internal abstract class Node
        {
            public List<BodyNode> childNodes { get; private set; } = new List<BodyNode>();

            public void AddChild(BodyNode newNode)
            {
                childNodes.Add(newNode);
            }
        }

        internal class RootNode : Node
        {
            public RootNode()
            {
                enabled = true;
            }

            internal bool enabled { get; set; }
        }

        internal class BodyNode : Node
        {
            public Body assignedBody { get; private set; }

            public BodyNode(Body body)
            {
                assignedBody = body;
            }

            public BodyNode()
            {
                assignedBody = null;
            }

        }

        // The root node is intentionally somewhat disingenuously added to the tree.
        // It allows for storing whether the tree is enabled or disabled, as well as serving as an anchor for other nodes to be added and removed from.
        private RootNode rootNode { get; set; }
        public BodyTree(FixedBody ReferenceBody)
        {
            rootNode = new RootNode();
            rootNode.childNodes.Add(new BodyNode(ReferenceBody));
        }

        public bool IsEnabled()
        {
            return rootNode.enabled;
        }

        // This is implimented as a safety net, not as a prefered method of checking whether a function is runnable.
        // As most areas that rely on the tree being enabled have other surrounding calculations, it is more efficient to cut it off early by checking enabled independently to calling a function and handling the error.
        private void IfNotEnabled()
        {
            if (!IsEnabled())
            {
                throw new NotEnabledException();
            }
        }

        // Once the tree has regained nodes, it can be enabled once again.
        public void Reenable(FixedBody ReferenceBody)
        {
            if (!IsEnabled())
            {
                rootNode.enabled = true;
                rootNode.childNodes[0] = new BodyNode(ReferenceBody);
            }
            else
            {
                throw new Exception("This body is already enabled.");
            }
        }

        // This generic breadth first traversal allows for a function to be ran on the children as defined in the parameters.
        private bool BreadthFirstTraversal(Func<BodyNode, bool> ChildCheckFunction)
        {
            Queue<BodyNode> queue = new Queue<BodyNode>();
            List<BodyNode> visitedNodes = new List<BodyNode>();

            queue.Enqueue(rootNode.childNodes[0]);
            visitedNodes.Add(rootNode.childNodes[0]);
            while (queue.Count > 0)
            {
                BodyNode node = queue.Dequeue();

                foreach (BodyNode child in node.childNodes)
                {
                    if (ChildCheckFunction(child))
                    {
                        return true;
                    }

                    if (!visitedNodes.Contains(child))
                    {
                        queue.Enqueue(child);
                        visitedNodes.Add(child);
                    }
                }
            }
            return false;
        }

        // This is made public as while it is only used internally this as a function could be useful to a coder outside of only a private context.
        public bool AreObjectsWithinRadius(double Radius, Vector3 CurrentRelativeToBackgroundPosition)
        {
            IfNotEnabled();

            Queue<KeyValuePair<BodyNode, Vector3>> queue = new Queue<KeyValuePair<BodyNode, Vector3>>();
            queue.Enqueue(new KeyValuePair<BodyNode, Vector3>(rootNode.childNodes[0], ((FixedBody)rootNode.childNodes[0].assignedBody).Position));

            bool CheckIfWithinRadiusAndCalculateBodyPositions(BodyNode Child)
            {
                Vector3 childRelativeToBackgroundPosition = ((MovingBody)Child.assignedBody).currentPoint.RelativePosition + queue.Peek().Value;
                Vector3 RelativeVector = childRelativeToBackgroundPosition - CurrentRelativeToBackgroundPosition;

                if (RelativeVector.Length() < Radius)
                {
                    return true;
                }

                if (Child.childNodes.Count > 0)
                {
                    queue.Enqueue(new KeyValuePair<BodyNode, Vector3>(Child, childRelativeToBackgroundPosition));
                }

                return false;
            }

            return BreadthFirstTraversal(CheckIfWithinRadiusAndCalculateBodyPositions);
        }

        // This adds specifically a moving body defined using state vectors.
        public void AddToTree(Color Colour, string NewBodyName, double NewBodyMass, decimal Time, Vector3 CurrentRelativeVelocity, Vector3 CurrentRelativeToBackgroundPosition, double Radius, bool VelocityRelativeToBackground)
        {
            IfNotEnabled();

            BodyTree thisTree = this;
            Vector3 CurrentCalculatedPosition = CurrentRelativeToBackgroundPosition - ((FixedBody)rootNode.childNodes[0].assignedBody).Position;

            bool IsObjectInTree(BodyNode Child)
            {
                if (Child.assignedBody.Name == NewBodyName)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            if (!BreadthFirstTraversal(IsObjectInTree))
            {
                FindAndPopulatePosition(rootNode.childNodes[0], CurrentRelativeVelocity, CurrentCalculatedPosition);
            }
            else
            {
                throw new Exception("The name chosen is already taken by a body in the simulation.");
            }


            void FindAndPopulatePosition(BodyNode parentNode, Vector3 currentCalculatedVelocity, Vector3 currentCalculatedPosition)
            {
                List<BodyNode> ValidParents = parentNode.childNodes.FindAll(x => (currentCalculatedPosition - ((MovingBody)x.assignedBody).currentPoint.RelativePosition).Length() < ((MovingBody)x.assignedBody).orbitInformation.HillSphereRadius);

                if (ValidParents.Count > 0)
                {
                    BodyNode CalculateValidParent()
                    {
                        if (ValidParents.Count > 1)
                        {
                            // In case of a conflict, the default winner is that with the highest mass / r^2.
                            // This is because gravity is proportional to mass / r^2.
                            return ValidParents.MaxBy(new Func<BodyNode, double>(x => x.assignedBody.Mass / Math.Pow((currentCalculatedPosition - ((MovingBody)x.assignedBody).currentPoint.RelativePosition).Length(), 2)));
                        }
                        else
                        {
                            return ValidParents[0];
                        }
                    }
                    BodyNode ValidParent = CalculateValidParent();

                    if (VelocityRelativeToBackground)
                    {
                        currentCalculatedVelocity -= ((MovingBody)ValidParent.assignedBody).currentPoint.RelativeVelocity;
                    }

                    currentCalculatedPosition -= ((MovingBody)ValidParent.assignedBody).currentPoint.RelativePosition;

                    FindAndPopulatePosition(ValidParent, currentCalculatedVelocity, currentCalculatedPosition);
                }
                else
                {
                    bool failed = false;

                    double CalculateHillSphereRadius()
                    {
                        if (parentNode.assignedBody.GetType() == typeof(MovingBody))
                        {
                            return ((MovingBody)parentNode.assignedBody).orbitInformation.HillSphereRadius;
                        }
                        else
                        {
                            return double.PositiveInfinity;
                        }
                    }
                    double parentHillSphereRadius = CalculateHillSphereRadius();

                    MovingBody NewBody = new MovingBody(ref failed, thisTree, Colour, NewBodyName, NewBodyMass, parentNode.assignedBody.Mass, parentHillSphereRadius, parentNode.assignedBody.Radius, Radius, Time, currentCalculatedPosition, CurrentRelativeVelocity, CurrentRelativeToBackgroundPosition);

                    if (!failed)
                    {
                        parentNode.AddChild(new BodyNode(NewBody));
                    }
                }
            }
        }

        // This adds specifically a moving body defined using keplerian elements.
        public void AddToTree(string ParentBodyName, Color Colour, string NewBodyName, double NewBodyMass, decimal StartingTimeFromEpoch, double StartingTrueAnomaly, double SemiMajorAxis, double Eccentricity, double Inclination, double LongitudeOfAscendingNode, double ArgumentOfPeriapsis, double Radius)
        {
            IfNotEnabled();

            // Instead of checking whether the name is unique followed by finding the parent's location, you can combine the tree traversal to save processing time.
            BodyNode parentNode = new BodyNode();
            bool parentNodeFound = false;

            if (ParentBodyName == rootNode.childNodes[0].assignedBody.Name)
            {
                parentNode = rootNode.childNodes[0];
                parentNodeFound = true;
            }

            bool CheckIfChildIsEitherParentOrSharingChildName(BodyNode Child)
            {
                if (Child.assignedBody.Name == NewBodyName)
                {
                    throw new Exception("The name chosen is already taken by a body in the simulation."); 
                }

                if (Child.assignedBody.Name == ParentBodyName)
                {
                    parentNode = Child;
                    parentNodeFound = true;
                }

                return false;
            }
            BreadthFirstTraversal(CheckIfChildIsEitherParentOrSharingChildName);

            if (!parentNodeFound)
            {
                throw new Exception("Chosen parent doesn't exist in the simulation.");
            }

            bool failed = false;

            double parentHillSphereRadius;
            Vector3 parentPosition;
            if (parentNode.assignedBody.GetType() == typeof(MovingBody))
            {
                parentHillSphereRadius = ((MovingBody)parentNode.assignedBody).orbitInformation.HillSphereRadius;
                parentPosition = ((MovingBody)parentNode.assignedBody).currentPoint.RelativePosition;
            }
            else
            {
                parentHillSphereRadius = double.PositiveInfinity;
                parentPosition = ((FixedBody)parentNode.assignedBody).Position;
            }

            MovingBody NewBody = new MovingBody(ref failed, this, Colour, NewBodyName, NewBodyMass, parentNode.assignedBody.Mass, parentHillSphereRadius, parentPosition, parentNode.assignedBody.Radius, Radius, StartingTimeFromEpoch, StartingTrueAnomaly, SemiMajorAxis, Eccentricity, Inclination, LongitudeOfAscendingNode, ArgumentOfPeriapsis);

            if (!failed)
            {
                parentNode.AddChild(new BodyNode(NewBody));
            }
        }

        public void RemoveFromTree(string RemovedBodyName)
        {
            IfNotEnabled();

            // The reference body isn't covered in the search function, so it is checked independently first.
            // This is useful as a different procedure has to be ran if the reference body is removed.
            if (RemovedBodyName == rootNode.childNodes[0].assignedBody.Name.ToLower())
            {
                rootNode.childNodes[0] = new BodyNode();
                rootNode.enabled = false;
                MessageBox.Show("Body removed succesfully.");
                return;
            }

            List<BodyNode> visitedNodes = new List<BodyNode>();
            if (DepthFirstSearch(rootNode.childNodes[0]))
            {
                MessageBox.Show("Body removed succesfully.");
            }
            else
            {
                MessageBox.Show("Body not found.");
            }

            bool DepthFirstSearch(BodyNode parent)
            {
                visitedNodes.Add(parent);

                foreach (BodyNode child in parent.childNodes)
                {
                    if (child.assignedBody.Name.ToLower() == RemovedBodyName)
                    {
                        parent.childNodes.Remove(child);
                        return true;
                    }

                    if (!visitedNodes.Contains(child))
                    {
                        if (DepthFirstSearch(child))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public void ExportBodysEphemeris(string ExportedBodyName)
        {
            IfNotEnabled();

            BodyNode childNode = new BodyNode();

            List<BodyNode> visitedNodes = new List<BodyNode>();

            if (DepthFirstSearch(rootNode.childNodes[0]))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();

                saveFileDialog.Filter = "csv|*.csv";
                saveFileDialog.Title = "Export Body's Ephemeris";
                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName != "")
                {
                    File.WriteAllText(saveFileDialog.FileName, ((MovingBody)childNode.assignedBody).ReturnEphemeris());
                }
            }
            else
            {
                MessageBox.Show("Body not found.");
            }

            bool DepthFirstSearch(BodyNode parent)
            {
                visitedNodes.Add(parent);

                foreach (BodyNode child in parent.childNodes)
                {
                    if (child.assignedBody.Name.ToLower() == ExportedBodyName)
                    {
                        childNode = child;
                        return true;
                    }

                    if (!visitedNodes.Contains(child))
                    {
                        if (DepthFirstSearch(child))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public void LoadTreeFromSaveableFormat(string InputString)
        {
            Vector3 CreateVector3FromString(string InputString)
            {
                string[] processedInputStringAsArray = InputString.Substring(1, InputString.Length - 2).Split(", ");

                return new Vector3(Convert.ToSingle(processedInputStringAsArray[0]), Convert.ToSingle(processedInputStringAsArray[1]), Convert.ToSingle(processedInputStringAsArray[2]));
            }

            MovingBody ConvertStringToMovingBody(string[] InputStringArray, double ParentMass)
            {
                string Name = InputStringArray[1];
                double Mass = Convert.ToDouble(InputStringArray[2]);
                double Radius = Convert.ToDouble(InputStringArray[3]);
                Color Colour = Color.FromArgb(Convert.ToInt32(InputStringArray[4]));
                decimal StartingTimeFromEpoch = Convert.ToDecimal(InputStringArray[5]);
                double StartingTrueAnomaly = Convert.ToDouble(InputStringArray[6]);

                string OrbitInformationAsString = InputStringArray[7].Substring(18, InputStringArray[7].Length - 20);
                string[] orbitInformationAsStringArray = OrbitInformationAsString.Split(", ");

                for (int i = 0; i < orbitInformationAsStringArray.Length; i++)
                {
                    if (orbitInformationAsStringArray[i].Contains("="))
                    {
                        orbitInformationAsStringArray[i] = orbitInformationAsStringArray[i].Substring(orbitInformationAsStringArray[i].IndexOf('=') + 2);
                    }
                }

                double LongitudeOfAscendingNode = Convert.ToDouble(orbitInformationAsStringArray[0]);
                double ArgumentOfPeriapsis = Convert.ToDouble(orbitInformationAsStringArray[1]);
                double LongitudeOfPeriapsis = Convert.ToDouble(orbitInformationAsStringArray[2]);
                double Inclination = Convert.ToDouble(orbitInformationAsStringArray[3]);
                double Eccentricity = Convert.ToDouble(orbitInformationAsStringArray[4]);
                double SemiMajorAxis = Convert.ToDouble(orbitInformationAsStringArray[5]);
                double SemiMinorAxis = Convert.ToDouble(orbitInformationAsStringArray[6]);
                double Periapsis = Convert.ToDouble(orbitInformationAsStringArray[7]);
                double Apoapsis = Convert.ToDouble(orbitInformationAsStringArray[8]);
                double AngularMomentum = Convert.ToDouble(orbitInformationAsStringArray[9]);
                Vector3 SpecificAngularMomentum = new Vector3(Convert.ToSingle(orbitInformationAsStringArray[10]), Convert.ToSingle(orbitInformationAsStringArray[11]), Convert.ToSingle(orbitInformationAsStringArray[12]));
                Vector3 EccentricityVector = new Vector3(Convert.ToSingle(orbitInformationAsStringArray[13]), Convert.ToSingle(orbitInformationAsStringArray[14]), Convert.ToSingle(orbitInformationAsStringArray[15]));
                double TotalEnergy = Convert.ToDouble(orbitInformationAsStringArray[16]);
                double HillSphereRadius = Convert.ToDouble(orbitInformationAsStringArray[17]);
                decimal OrbitalPeriod = Convert.ToDecimal(orbitInformationAsStringArray[18]);

                return new MovingBody(Colour, Name, Mass, Radius, StartingTimeFromEpoch, ParentMass, StartingTrueAnomaly, LongitudeOfAscendingNode, ArgumentOfPeriapsis, LongitudeOfPeriapsis, Inclination, Eccentricity, SemiMajorAxis, SemiMinorAxis, Periapsis, Apoapsis, AngularMomentum, SpecificAngularMomentum, EccentricityVector, TotalEnergy, HillSphereRadius);
            }

            void FindAndPopulatePosition(BodyNode parentNode, List<string> path, string[] referenceBodyAsAStringArray)
            {
                if (path.Count > 1)
                {
                    int childNodePosition = parentNode.childNodes.FindIndex(child => child.assignedBody.Name == path[1]);
                    FindAndPopulatePosition(parentNode.childNodes[childNodePosition], path.Skip(1).ToList(), referenceBodyAsAStringArray);
                }
                else if (path.Count == 1)
                {
                    parentNode.childNodes.Add(new BodyNode(ConvertStringToMovingBody(referenceBodyAsAStringArray, parentNode.assignedBody.Mass)));
                }
            }

            rootNode.enabled = false;
            rootNode.childNodes.Clear();

            List<string> bodies = InputString.Split('\n').ToList();

            string[] referenceBodyAsAStringArray = bodies[0].Split(",|");
            FixedBody referenceBody = new FixedBody(Color.FromArgb(Convert.ToInt32(referenceBodyAsAStringArray[4])), referenceBodyAsAStringArray[1], CreateVector3FromString(referenceBodyAsAStringArray[5]), Convert.ToDouble(referenceBodyAsAStringArray[2]), Convert.ToDouble(referenceBodyAsAStringArray[3]));

            rootNode.AddChild(new BodyNode(referenceBody));
            bodies.Remove(bodies[0]);

            while (bodies.Count > 0)
            {
                referenceBodyAsAStringArray = bodies[0].Split(",|");

                if (referenceBodyAsAStringArray[0] != "")
                {
                    List<string> heritage = referenceBodyAsAStringArray[0].Substring(1, referenceBodyAsAStringArray[0].Length - 2).Split(", ").ToList();

                    FindAndPopulatePosition(rootNode.childNodes[0], heritage.ToList(), referenceBodyAsAStringArray);
                }

                bodies.Remove(bodies[0]);
            }

            rootNode.enabled = true;
        }

        public string GetBodiesInSaveableFormat()
        {
            IfNotEnabled();

            string output = "";

            List<string> Heritage = new List<string>();
            List<KeyValuePair<string[], BodyNode>> visitedNodes = new List<KeyValuePair<string[], BodyNode>>();
            void DepthFirstTraversal(BodyNode parent)
            {
                visitedNodes.Add(new KeyValuePair<string[], BodyNode>(Heritage.ToArray(), parent));

                Heritage.Add(parent.assignedBody.Name);
                foreach (BodyNode child in parent.childNodes)
                {
                    if (visitedNodes.FindAll(x => x.Value.assignedBody.Name == child.assignedBody.Name).Count() == 0)
                    {
                        if (child.childNodes.Count != 0)
                        {
                            DepthFirstTraversal(child);
                            if (parent.childNodes.Last() == child)
                            {
                                Heritage.Remove(Heritage.Last());
                            }
                        }
                        else
                        {
                            visitedNodes.Add(new KeyValuePair<string[], BodyNode>(Heritage.ToArray(), child));
                        }
                    }
                }
                Heritage.Remove(Heritage.Last());
            }

            DepthFirstTraversal(rootNode.childNodes[0]);

            foreach (KeyValuePair<string[], BodyNode> HeritageAndNode in visitedNodes)
            {
                output += "[";
                if (HeritageAndNode.Key.Length > 0)
                {
                    string subOutput = "";
                    foreach (string bodyName in HeritageAndNode.Key)
                    {
                        subOutput += ", " + bodyName;
                    }
                    output += subOutput.Remove(0, 2);
                }
                output += "],|";

                output += HeritageAndNode.Value.assignedBody.ToString();
            }

            return output;
        }

        // A breadth first search is done on the tree to gather all of their current locations for display.
        public List<KeyValuePair<Body, Vector3>> GetBodiesAndPositions()
        {
            IfNotEnabled();

            Queue<KeyValuePair<BodyNode, Vector3>> queue = new Queue<KeyValuePair<BodyNode, Vector3>>();
            List<KeyValuePair<BodyNode, Vector3>> visitedNodes = new List<KeyValuePair<BodyNode, Vector3>>();

            queue.Enqueue(new KeyValuePair<BodyNode, Vector3>(rootNode.childNodes[0], ((FixedBody)rootNode.childNodes[0].assignedBody).Position));
            visitedNodes.Add(new KeyValuePair<BodyNode, Vector3>(rootNode.childNodes[0], ((FixedBody)rootNode.childNodes[0].assignedBody).Position));

            bool UpdatePositions(BodyNode Child)
            {
                Vector3 ChildRelativeToBackgroundPosition = ((MovingBody)Child.assignedBody).currentPoint.RelativePosition + queue.Peek().Value;
                if (!visitedNodes.Contains(new KeyValuePair<BodyNode, Vector3>(Child, ChildRelativeToBackgroundPosition)))
                {
                    if (Child.childNodes.Count > 0)
                    {
                        queue.Enqueue(new KeyValuePair<BodyNode, Vector3>(Child, ChildRelativeToBackgroundPosition));
                    }
                    visitedNodes.Add(new KeyValuePair<BodyNode, Vector3>(Child, ChildRelativeToBackgroundPosition));
                }

                if (queue.Peek().Key.childNodes.Last().assignedBody.Name == Child.assignedBody.Name)
                {
                    queue.Dequeue();
                }

                return false;
            }

            BreadthFirstTraversal(UpdatePositions);

            List<KeyValuePair<Body, Vector3>> bodies = new List<KeyValuePair<Body, Vector3>>();

            foreach (KeyValuePair<BodyNode, Vector3> nodeAndRelativeToBackgroundPosition in visitedNodes)
            {
                bodies.Add(new KeyValuePair<Body, Vector3>(nodeAndRelativeToBackgroundPosition.Key.assignedBody, nodeAndRelativeToBackgroundPosition.Value));
            }

            return bodies;
        }

        public void UpdateBodiesCurrentPositions(decimal Time)
        {
            IfNotEnabled();

            bool UpdateChild(BodyNode child)
            {
                ((MovingBody)child.assignedBody).UpdateCurrentPoint(Time);
                return false;
            }
            BreadthFirstTraversal(UpdateChild);
        }

        public void UpdateBodiesCurrentPositionsUsingArraySearch(decimal Time)
        {
            IfNotEnabled();

            bool UpdateChild(BodyNode child)
            {
                ((MovingBody)child.assignedBody).UpdateCurrentPointUsingArraySearch(Time);
                return false;
            }
            BreadthFirstTraversal(UpdateChild);
        }

        // To calculate what is being clicked on by the mouse, the tree has to be queried.
        public OrbitInformation RegisterRightClick(Vector2 rightClickPosition, ref string name, float inverseScaleFactor)
        {
            IfNotEnabled();

            BodyNode referenceBody = rootNode.childNodes[0];
            return FindClickedObject(referenceBody, rightClickPosition, ref name);

            OrbitInformation FindClickedObject(BodyNode parentNode, Vector2 currentCalculatedPosition, ref string name)
            {
                List<BodyNode> validParents = parentNode.childNodes.FindAll(x => (currentCalculatedPosition - new Vector2(((MovingBody)x.assignedBody).currentPoint.RelativePosition.X, ((MovingBody)x.assignedBody).currentPoint.RelativePosition.Y)).Length() < 20 * inverseScaleFactor);
                List<BodyNode> validParentsByRadius = parentNode.childNodes.FindAll(x => (currentCalculatedPosition - new Vector2(((MovingBody)x.assignedBody).currentPoint.RelativePosition.X, ((MovingBody)x.assignedBody).currentPoint.RelativePosition.Y)).Length() < x.assignedBody.Radius);

                foreach (BodyNode validParentByRadius in validParentsByRadius) 
                {
                    if (!validParents.Contains(validParentByRadius))
                    {
                        validParents.Add(validParentByRadius);
                    }
                }

                if (validParents.Count > 0)
                {
                    BodyNode CalculateValidParent()
                    {
                        if (validParents.Count > 1)
                        {
                            // Field strength is proportional to M / r^2, so I have made in the case of a conflict the decided body be that which provides the largest "proportionalised" field strength at the mouse Position.
                            return validParents.MaxBy(new Func<BodyNode, double>(x => x.assignedBody.Mass / Math.Pow((currentCalculatedPosition - new Vector2(((MovingBody)x.assignedBody).currentPoint.RelativePosition.X, ((MovingBody)x.assignedBody).currentPoint.RelativePosition.Y)).Length(), 2)));
                        }
                        else
                        {
                            return validParents[0];
                        }
                    }

                    BodyNode ValidParent = CalculateValidParent();

                    currentCalculatedPosition -= new Vector2(((MovingBody)ValidParent.assignedBody).currentPoint.RelativePosition.X, ((MovingBody)ValidParent.assignedBody).currentPoint.RelativePosition.Y);

                    return FindClickedObject(ValidParent, currentCalculatedPosition, ref name);
                }
                else
                {
                    name = parentNode.assignedBody.Name;
                    if (parentNode.assignedBody.Name == referenceBody.assignedBody.Name)
                    {
                        return new OrbitInformation(double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, new Vector3(0, 0, 0), new Vector3(0, 0, 0), double.NaN, double.NaN, -1);
                    }
                }

                return ((MovingBody)parentNode.assignedBody).orbitInformation;
            }
        }
    }
}
