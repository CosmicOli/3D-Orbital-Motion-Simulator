using System.Numerics;
using System.Web;
using System.Windows.Forms.Design;

namespace _3D_Orbital_Motion_Simulation
{
    internal class MovingBody : Body
    {
        // A record to store data about points in an orbit.
        // While only relative position and time are used, the rest of the information was added to allow a future coder to implement features based on other orbital position statistics.
        public record PointInformation(decimal Time, double AngleOfVelocityToTangent, double TrueAnomaly, Vector3 RelativePosition, Vector3 RelativeVelocity);

        // The gravitational constant; used for some equations.
        private const double G = 0.0000000000667;

        // The body's name.
        public string Name { get; init; }

        // The body's mass.
        public double Mass { get; init; }

        // The radius of the body (all bodies are assumed to be spherical).
        public double Radius { get; init; }

        // The displayed colour of the body.
        public Color Colour { get; init; }

        // The time the body was created at; used for calculating Position at a given time.
        private decimal StartingTimeFromEpoch { get; init; }

        // The information about a body's orbit.
        public OrbitInformation orbitInformation { get; private set; }
        private double StartingTrueAnomaly { get; set; } 

        // The body's current point information.
        public PointInformation currentPoint { get; private set; }

        // The full orbit's precalculated data points; this is done on instantiation of the class as it is significantly better for performance and the memory tradeoff isn't particularly high.
        // Unfortunately, the equation to calculate time is also non analytically solvable for distance between bodies, which means you can't calculate the current point directly using time, and so predetermine points at given times are used to be approximately equal.
        private PointInformation[] predeterminedPoints { get; set; }

        // This initialiser is specifically for initialising with state vectors.
        public MovingBody(ref bool failed, BodyTree Tree, Color Colour, string Name, double Mass, double ParentMass, double ParentHillSphereRadius, double ParentRadius, double Radius, decimal StartingTimeFromEpoch, Vector3 StartingRelativePosition, Vector3 StartingRelativeVelocity, Vector3 StartingRelativeToBackgroundPosition)
        { 
            this.Name = Name;

            this.Mass = Mass;

            this.StartingTimeFromEpoch = StartingTimeFromEpoch;

            this.Radius = Radius;

            this.Colour = Colour;

            // An arbitrarily chosen parent:child mass ratio of 50:1 has been chosen as the smallest difference in mass before warning the user of innaccuracies.
            // For example, the Earth:Moon ratio is about 81:1 for comparison; despite being accepted under the chosen ratio, the Earth:Moon barycentre is somewhat moved from Earth's center in more accurate simulations.
            // Hence, I believe this to be a generous but reasonable compromise.
            if (ParentMass / 50 < Mass)
            {
                MessageBox.Show("Orbiting body has a significantly high mass ratio to it's parent. The simulation may have increased inaccuracies.");
            }

            failed = InitialiseOrbitConstantsFromStateVectors(Tree, ParentMass, ParentHillSphereRadius, ParentRadius, StartingRelativePosition, StartingRelativeVelocity, StartingRelativeToBackgroundPosition);
        }

        // This initialiser is specifically for initialising with keplerian elements.
        public MovingBody(ref bool failed, BodyTree Tree, Color Colour, string Name, double Mass, double ParentMass, double ParentHillSphereRadius, Vector3 ParentPosition, double ParentRadius, double Radius, decimal StartingTimeFromEpoch, double StartingTrueAnomaly, double SemiMajorAxis, double Eccentricity, double Inclination, double LongitudeOfAscendingNode, double ArgumentOfPeriapsis)
        {
            this.Name = Name;

            this.Mass = Mass;

            this.StartingTimeFromEpoch = StartingTimeFromEpoch;

            this.Radius = Radius;

            this.Colour = Colour;

            this.StartingTrueAnomaly = StartingTrueAnomaly;

            // An arbitrarily chosen parent:child mass ratio of 50:1 has been chosen as the smallest difference in mass before warning the user of innaccuracies.
            // For example, the Earth:Moon ratio is about 81:1 for comparison; despite being accepted under the chosen ratio, the Earth:Moon barycentre is somewhat moved from Earth's center in more accurate simulations.
            // Hence, I believe this to be a generous but reasonable compromise.
            if (ParentMass / 50 < Mass)
            {
                MessageBox.Show("Orbiting body has a significantly high mass ratio to it's parent. The simulation may have increased inaccuracies.");
            }

            failed = InitialiseOrbitConstantsFromKeplerianElements(Tree, ParentMass, ParentHillSphereRadius, ParentPosition, ParentRadius, StartingTrueAnomaly, SemiMajorAxis, Eccentricity, Inclination, LongitudeOfAscendingNode, ArgumentOfPeriapsis);
        }

        // This initialiser is to be exclusively used in creating a tree from a save.
        public MovingBody(Color Colour, string Name, double Mass, double Radius, decimal StartingTimeFromEpoch, double ParentMass, double StartingTrueAnomaly, double LongitudeOfAscendingNode, double ArgumentOfPeriapsis, double LongitudeOfPeriapsis, double Inclination, double Eccentricity, double SemiMajorAxis, double SemiMinorAxis, double Periapsis, double Apoapsis, double AngularMomentum, Vector3 SpecificAngularMomentum, Vector3 EccentricityVector, double TotalEnergy, double HillSphereRadius)
        {
            this.Name = Name;

            this.Mass = Mass;

            this.StartingTimeFromEpoch = StartingTimeFromEpoch;

            this.Radius = Radius;

            this.Colour = Colour;

            ReinitialiseOrbitConstantsFromKeplerianElements(ParentMass, StartingTrueAnomaly, LongitudeOfAscendingNode, ArgumentOfPeriapsis, LongitudeOfPeriapsis, Inclination, Eccentricity, SemiMajorAxis, SemiMinorAxis, Periapsis, Apoapsis, AngularMomentum, SpecificAngularMomentum, EccentricityVector, TotalEnergy, HillSphereRadius);
        }

        private bool InitialiseOrbitConstantsFromStateVectors(BodyTree Tree, double ParentMass, double ParentHillsSphereRadius, double ParentRadius, Vector3 StartingRelativePosition, Vector3 StartingRelativeVelocity, Vector3 StartingRelativeToBackgroundPosition)
        {
            double StartingRelativeAbsPosition = StartingRelativePosition.Length();

            double StartingRelativeAbsVelocity = StartingRelativeVelocity.Length();

            // This is the vector perpendicular from the other 2 vectors which is a measure of angular momentum / mass.
            Vector3 SpecificAngularMomentum = Vector3.Cross(StartingRelativePosition, StartingRelativeVelocity);

            // e is the eccentricity vector, a vector with no unit that points from apoapsis to periapsis with magnitude equal to scalar eccentricity.
            Vector3 EccentricityVector = Vector3.Divide(Vector3.Cross(StartingRelativeVelocity, SpecificAngularMomentum), Convert.ToSingle(G * ParentMass)) - Vector3.Divide(StartingRelativePosition, Convert.ToSingle(StartingRelativeAbsPosition));

            // Orbital eccentricity is a unitless (dimensionless) parameter which gives a measure of how far an orbit is from a perfect circle.
            double Eccentricity = EccentricityVector.Length();

            // This is the distance from the centre of the orbit and the apoapsis or periapsis (the longest diameter).
            double SemiMajorAxis = 1 / (2 / StartingRelativeAbsPosition - Math.Pow(StartingRelativeAbsVelocity, 2) / (G * ParentMass));

            double HillSphereRadius = SemiMajorAxis * (1 - Eccentricity) * Math.Pow(Mass / (3 * ParentMass), (double)1 / 3);

            // Check that no other bodies already exist in that hill sphere.
            if (Tree.AreObjectsWithinRadius(HillSphereRadius, StartingRelativeToBackgroundPosition))
            {
                return true;
            }

            // This is the shortest distance from the parent body.
            // There is an acceptable error in this calculation added on to stop edge case calculation errors caused by rounding errors.
            double Periapsis = SemiMajorAxis * (1 - Eccentricity) + 0.0001;

            if (Periapsis <= ParentRadius)
            {
                throw new Exception("The orbit mustn't collide with the parent body's surface.");
            }

            // This is the furthest distance from the parent body.
            // There is an acceptable error in this calculation added on to stop edge case calculation errors caused by rounding errors.
            double Apoapsis = SemiMajorAxis * (1 + Eccentricity) - 0.0001;

            if (Apoapsis + HillSphereRadius > ParentHillsSphereRadius)
            {
                MessageBox.Show("Any subsequently made bodies must lie in this body's parent's hill sphere before being accepted into this body’s gravitational influence. This is a consequence of the created simulation being potentially innaccurate.");
            }

            if (Apoapsis > ParentHillsSphereRadius)
            {
                throw new Exception("The orbit must lie within the parent's hill sphere.");
            }

            // The semi-minor axis is the shortest diameter of an ellipse. It is perpendicular to the semi-major axis through the centre point if drawn on a graph.
            double SemiMinorAxis = SemiMajorAxis * Math.Sqrt(1 - Math.Pow(Eccentricity, 2));

            // The angle between the equatorial plane (in this case xy) and the plane of the orbit.
            double Inclination = Math.Acos(SpecificAngularMomentum.Z / SpecificAngularMomentum.Length());

            // N is the vector pointing towards the ascending node. If the ascending node doesn't exist (inclination = 0 or pi) it is assigned as a generic Vector3 as it isn't used. It is used for the following equations.
            Vector3 CalculateN()
            {
                if (Inclination != 0 && Inclination != Math.PI)
                {
                    return Vector3.Cross(new Vector3(0, 0, 1), SpecificAngularMomentum);
                }
                else
                {
                    return new Vector3();
                }
            }
            Vector3 N = CalculateN();

            // The angle between the +x direction and the ascending node.
            double CalculateLongitudeOfAscendingNode()
            {
                if (Inclination != 0 && Inclination != Math.PI)
                {
                    if (N.Y >= 0)
                    {
                        return Math.Acos(N.X / N.Length());
                    }
                    else
                    {
                        return 2 * Math.PI - Math.Acos(N.X / N.Length());
                    }
                }
                else
                {
                    return 0;
                }
            }
            double LongitudeOfAscendingNode = CalculateLongitudeOfAscendingNode();


            // The angle between the +x direction and the periapsis.
            double CalculateArgumentOfPeriapsis()
            {
                double argumentOfPeriapsis;
                if (Inclination == 0 || Inclination == Math.PI)
                {
                    argumentOfPeriapsis = Math.Atan2(EccentricityVector.Y, EccentricityVector.X);

                    if (Vector3.Cross(StartingRelativePosition, StartingRelativeVelocity).Z < 0)
                    {
                        argumentOfPeriapsis = 2 * Math.PI - argumentOfPeriapsis;
                    }
                }
                else
                {
                    argumentOfPeriapsis = Math.Acos(Vector3.Dot(N, EccentricityVector) / (N.Length() * EccentricityVector.Length()));

                    if (EccentricityVector.Z < 0)
                    {
                        argumentOfPeriapsis = 2 * Math.PI - argumentOfPeriapsis;
                    }
                }
                return argumentOfPeriapsis;
            }
            double ArgumentOfPeriapsis = CalculateArgumentOfPeriapsis();

            // This is the longitude at which the periapsis would occur is the orbit's inclination was zero.
            double LongitudeOfPeriapsis = LongitudeOfAscendingNode + ArgumentOfPeriapsis;

            // This rotates the orbit such that its periapsis and apoapsis lie on the x axis, specifically such that the periapsis is furthest along the +x direction.
            double AngleFromPeriapsis = 2 * Math.PI - ArgumentOfPeriapsis;
            TransformationMatrix3x3 RotationMatrixForPeriapsis = new TransformationMatrix3x3(new double[,] { { Math.Cos(AngleFromPeriapsis), Math.Sin(AngleFromPeriapsis), 0 }, { -Math.Sin(AngleFromPeriapsis), Math.Cos(AngleFromPeriapsis), 0 }, { 0, 0, 1 } });

            double a;
            double b;
            if (Inclination != 0)
            {
                Vector3 RotateVector3OntoTheXYPlane(Vector3 vector3)
                {
                    // This rotates the line between the ascending and decending node to be along the +x axis.
                    double AngleFromAscendingNode = 2 * Math.PI - LongitudeOfAscendingNode;
                    TransformationMatrix3x3 RotationMatrixForAscendingNode = new TransformationMatrix3x3(new double[,] { { Math.Cos(AngleFromAscendingNode), Math.Sin(AngleFromAscendingNode), 0 }, { -Math.Sin(AngleFromAscendingNode), Math.Cos(AngleFromAscendingNode), 0 }, { 0, 0, 1 } });

                    // This rotates the orbit along it's node line (the x axis), hence giving a flat orbit.
                    double AngleFromInclination = 2 * Math.PI - Inclination;
                    TransformationMatrix3x3 RotationMatrixForInclination = new TransformationMatrix3x3(new double[,] { { 1, 0, 0 }, { 0, Math.Cos(AngleFromInclination), Math.Sin(AngleFromInclination) }, { 0, -Math.Sin(AngleFromInclination), Math.Cos(AngleFromInclination) } });

                    vector3 = RotationMatrixForPeriapsis.Transform(vector3);
                    vector3 = RotationMatrixForInclination.Transform(vector3);
                    vector3 = RotationMatrixForAscendingNode.Transform(vector3);

                    return vector3;
                }

                Vector3 FlattenedRelativeStartPosition = RotateVector3OntoTheXYPlane(StartingRelativePosition);
                Vector3 FlattenedRelativeStartVelocity = RotateVector3OntoTheXYPlane(StartingRelativeVelocity);

                StartingTrueAnomaly = Math.Atan2(FlattenedRelativeStartPosition.X, FlattenedRelativeStartPosition.Y);

                a = FlattenedRelativeStartPosition.Y / FlattenedRelativeStartPosition.X;
                b = FlattenedRelativeStartVelocity.Y / FlattenedRelativeStartVelocity.X;
            }
            else
            {
                Vector3 RotatedPosition = RotationMatrixForPeriapsis.Transform(StartingRelativePosition);
                StartingTrueAnomaly = Math.Atan2(RotatedPosition.X, RotatedPosition.Y);

                a = StartingRelativePosition.Y / StartingRelativePosition.X;
                b = StartingRelativeVelocity.Y / StartingRelativeVelocity.X;
            }
            double CosStartingAngleOfVelocity = (a / Math.Sqrt(Math.Pow(a, 2) + 1)) * (1 / Math.Sqrt(Math.Pow(b, 2) + 1)) - (1 / Math.Sqrt(Math.Pow(a, 2) + 1)) * (b / Math.Sqrt(Math.Pow(b, 2) + 1));
            double StartingPerpendicularAbsVelocity = StartingRelativeAbsVelocity * CosStartingAngleOfVelocity;

            // This is the measure of momentum around an axis.
            double AngularMomentum = Mass * StartingPerpendicularAbsVelocity * StartingRelativeAbsPosition;

            double TotalEnergy = 0.5 * Mass * Math.Pow(StartingRelativeAbsVelocity, 2) + -1 * G * ParentMass * Mass / StartingRelativeAbsPosition;

            currentPoint = new PointInformation(StartingTimeFromEpoch, Math.Acos(CosStartingAngleOfVelocity), double.NaN, StartingRelativePosition, StartingRelativeVelocity);

            // This is the amount of time taken to complete an orbit in seconds.
            decimal orbitalPeriod;

            // 200000 data points was chosen as a good trade between memory and performance.
            predeterminedPoints = CreatePoints(new OrbitInformation(LongitudeOfAscendingNode, ArgumentOfPeriapsis, LongitudeOfPeriapsis, Inclination, Eccentricity, SemiMajorAxis, SemiMinorAxis, Periapsis, Apoapsis, AngularMomentum, SpecificAngularMomentum, EccentricityVector, TotalEnergy, HillSphereRadius, 0), StartingTimeFromEpoch, 200000, ParentMass, StartingRelativeAbsPosition, out orbitalPeriod);

            orbitInformation = new OrbitInformation(LongitudeOfAscendingNode, ArgumentOfPeriapsis, LongitudeOfPeriapsis, Inclination, Eccentricity, SemiMajorAxis, SemiMinorAxis, Periapsis, Apoapsis, AngularMomentum, SpecificAngularMomentum, EccentricityVector, TotalEnergy, HillSphereRadius, orbitalPeriod);

            return false;
        }

        private bool InitialiseOrbitConstantsFromKeplerianElements(BodyTree Tree, double ParentMass, double ParentHillsSphereRadius, Vector3 CurrentParentPosition, double ParentRadius, double StartingTrueAnomaly, double SemiMajorAxis, double Eccentricity, double Inclination, double LongitudeOfAscendingNode, double ArgumentOfPeriapsis)
        {
            double startingRelativeAbsPosition = (SemiMajorAxis * (1 - Math.Pow(Eccentricity, 2))) / (1 + Eccentricity * Math.Cos(StartingTrueAnomaly));
            Vector3 startingRelativePosition = new Vector3(Convert.ToSingle(startingRelativeAbsPosition * Math.Cos(StartingTrueAnomaly)), Convert.ToSingle(startingRelativeAbsPosition * Math.Sin(StartingTrueAnomaly)), 0);

            // This is the furthest distance from the parent body.
            double Apoapsis = SemiMajorAxis * (1 + Eccentricity);

            // This is the shortest distance from the parent body.
            double Periapsis = SemiMajorAxis * (1 - Eccentricity);

            if (Periapsis <= ParentRadius)
            {
                throw new Exception("The orbit mustn't collide with the parent body's surface.");
            }

            double TotalVelocitySquaredAtPeriapsis = G * ParentMass * (2 / Periapsis - 1 / SemiMajorAxis);

            Vector3 RotateVector3OntoTheOrbitalPath(Vector3 vector3)
            {
                TransformationMatrix3x3 RotationMatrixForPeriapsis = new TransformationMatrix3x3(new double[,] { { Math.Cos(ArgumentOfPeriapsis), Math.Sin(ArgumentOfPeriapsis), 0 }, { -Math.Sin(ArgumentOfPeriapsis), Math.Cos(ArgumentOfPeriapsis), 0 }, { 0, 0, 1 } });
                TransformationMatrix3x3 RotationMatrixForInclination = new TransformationMatrix3x3(new double[,] { { 1, 0, 0 }, { 0, Math.Cos(Inclination), Math.Sin(Inclination) }, { 0, -Math.Sin(Inclination), Math.Cos(Inclination) } });
                TransformationMatrix3x3 RotationMatrixForAscendingNode = new TransformationMatrix3x3(new double[,] { { Math.Cos(LongitudeOfAscendingNode), Math.Sin(LongitudeOfAscendingNode), 0 }, { -Math.Sin(LongitudeOfAscendingNode), Math.Cos(LongitudeOfAscendingNode), 0 }, { 0, 0, 1 } });

                vector3 = RotationMatrixForPeriapsis.Transform(vector3);
                vector3 = RotationMatrixForInclination.Transform(vector3);
                vector3 = RotationMatrixForAscendingNode.Transform(vector3);

                return vector3;
            }

            Vector3 PeriapsisRelativePosition = RotateVector3OntoTheOrbitalPath(new Vector3(Convert.ToSingle(Periapsis), 0, 0));
            Vector3 PeriapsisRelativeVelocity = RotateVector3OntoTheOrbitalPath(new Vector3(0, Convert.ToSingle(Math.Sqrt(TotalVelocitySquaredAtPeriapsis)), 0));

            double TotalEnergy = (-G * ParentMass * Mass) / (Apoapsis + Periapsis);

            double HillSphereRadius = SemiMajorAxis * (1 - Eccentricity) * Math.Pow(Mass / (3 * ParentMass), (double)1 / 3);

            Vector3 StartingRelativeToBackgroundPosition = startingRelativePosition + CurrentParentPosition;

            // Check that no other bodies already exist in that hill sphere.
            if (Tree.AreObjectsWithinRadius(HillSphereRadius, StartingRelativeToBackgroundPosition))
            {
                return true;
            }

            if (Apoapsis + HillSphereRadius > ParentHillsSphereRadius)
            {
                MessageBox.Show("Any subsequently made bodies must lie in this body's parent's hill sphere before being accepted into this body’s gravitational influence. This is a consequence of the created simulation being potentially innaccurate.");
            }

            if (Apoapsis > ParentHillsSphereRadius)
            {
                throw new Exception("The orbit must lie within the parent's hill sphere.");
            }

            // The semi-minor axis is the shortest diameter of an ellipse. It is perpendicular to the semi-major axis through the centre point if drawn on a graph.
            double SemiMinorAxis = SemiMajorAxis * Math.Sqrt(1 - Math.Pow(Eccentricity, 2));

            // This is the vector perpendicular from the other 2 vectors which is a measure of angular momentum / mass.
            Vector3 SpecificAngularMomentum = Vector3.Cross(PeriapsisRelativePosition, PeriapsisRelativeVelocity);

            // e is the eccentricity vector, a vector with no unit that points from apoapsis to periapsis with magnitude equal to scalar eccentricity.
            Vector3 EccentricityVector = Vector3.Divide(Vector3.Cross(PeriapsisRelativeVelocity, SpecificAngularMomentum), Convert.ToSingle(G * ParentMass)) - Vector3.Divide(PeriapsisRelativePosition, PeriapsisRelativePosition.Length());

            // This is the longitude at which the periapsis would occur is the orbit's inclination was zero.
            double LongitudeOfPeriapsis = LongitudeOfAscendingNode + ArgumentOfPeriapsis;

            double StartingPeriapsisAbsVelocity = PeriapsisRelativeVelocity.Length();

            // This is the measure of momentum around an axis.
            double AngularMomentum = Mass * StartingPeriapsisAbsVelocity * PeriapsisRelativePosition.Length();

            double StartingRelativeAbsVelocitySquared = G * ParentMass * (2 / Periapsis - 1 / SemiMajorAxis);

            double CalculateStartingPerpendicularVelocity()
            {
                double startingPerpendicularVelocity = AngularMomentum / (Mass * startingRelativeAbsPosition);

                if (StartingRelativeAbsVelocitySquared <= Periapsis)
                {
                    startingPerpendicularVelocity += startingPerpendicularVelocity / 400000;
                }
                else if (StartingRelativeAbsVelocitySquared >= Apoapsis)
                {
                    startingPerpendicularVelocity -= startingPerpendicularVelocity / 400000;
                }

                return startingPerpendicularVelocity;
            }

            double StartingPerpendicularVelocity = CalculateStartingPerpendicularVelocity();

            Vector3 CalculateStartingRelativeVelocity()
            {
                Vector3 startingRelativeVelocity = new Vector3(Convert.ToSingle(StartingPerpendicularVelocity), Convert.ToSingle(Math.Sqrt(Math.Pow(StartingPerpendicularVelocity, 2) - StartingRelativeAbsVelocitySquared)), 0);

                if (StartingTrueAnomaly > Math.PI || StartingTrueAnomaly < 0)
                {
                    startingRelativeVelocity *= -1;
                }

                return startingRelativeVelocity;
            }

            Vector3 StartingRelativeVelocity = RotateVector3OntoTheOrbitalPath(CalculateStartingRelativeVelocity());

            currentPoint = new PointInformation(StartingTimeFromEpoch, double.NaN, double.NaN, startingRelativePosition, StartingRelativeVelocity);

            // This is the amount of time taken to complete an orbit in seconds.
            decimal orbitalPeriod;

            // 200000 data points was chosen as a good trade between memory and performance.
            predeterminedPoints = CreatePoints(new OrbitInformation(LongitudeOfAscendingNode, ArgumentOfPeriapsis, LongitudeOfPeriapsis, Inclination, Eccentricity, SemiMajorAxis, SemiMinorAxis, Periapsis, Apoapsis, AngularMomentum, SpecificAngularMomentum , EccentricityVector, TotalEnergy, HillSphereRadius, 0), StartingTimeFromEpoch, 200000, ParentMass, startingRelativeAbsPosition, out orbitalPeriod);

            orbitInformation = new OrbitInformation(LongitudeOfAscendingNode, ArgumentOfPeriapsis, LongitudeOfPeriapsis, Inclination, Eccentricity, SemiMajorAxis, SemiMinorAxis, Periapsis, Apoapsis, AngularMomentum, SpecificAngularMomentum, EccentricityVector, TotalEnergy, HillSphereRadius, orbitalPeriod);

            return false;
        }

        private bool ReinitialiseOrbitConstantsFromKeplerianElements(double ParentMass, double StartingTrueAnomaly, double LongitudeOfAscendingNode, double ArgumentOfPeriapsis, double LongitudeOfPeriapsis, double Inclination, double Eccentricity, double SemiMajorAxis, double SemiMinorAxis, double Periapsis, double Apoapsis, double AngularMomentum, Vector3 SpecificAngularMomentum, Vector3 EccentricityVector, double TotalEnergy, double HillSphereRadius)
        {
            double startingRelativeAbsPosition = (SemiMajorAxis * (1 - Math.Pow(Eccentricity, 2))) / (1 + Eccentricity * Math.Cos(StartingTrueAnomaly));

            // This is the amount of time taken to complete an orbit in seconds.
            decimal orbitalPeriod;

            predeterminedPoints = CreatePoints(new OrbitInformation(LongitudeOfAscendingNode, ArgumentOfPeriapsis, LongitudeOfPeriapsis, Inclination, Eccentricity, SemiMajorAxis, SemiMinorAxis, Periapsis, Apoapsis, AngularMomentum, SpecificAngularMomentum, EccentricityVector, TotalEnergy, HillSphereRadius, 0), StartingTimeFromEpoch, 200000, ParentMass, startingRelativeAbsPosition, out orbitalPeriod);

            orbitInformation = new OrbitInformation(LongitudeOfAscendingNode, ArgumentOfPeriapsis, LongitudeOfPeriapsis, Inclination, Eccentricity, SemiMajorAxis, SemiMinorAxis, Periapsis, Apoapsis, AngularMomentum, SpecificAngularMomentum, EccentricityVector, TotalEnergy, HillSphereRadius, orbitalPeriod);

            return false;
        }

        private PointInformation[] CreatePoints(OrbitInformation OrbitInformation, decimal TimeOffset, int NumberOfIntervals, double ParentMass, double StartingDistance, out decimal OrbitalPeriod)
        {
            double SpecificTotalEnergy = OrbitInformation.TotalEnergy / Mass;

            // This is derived using -2 * V * U / K = 0 (bare in mind U is the only important part as it contains AbsSpecificAngularMomentum).
            // This is because of the "time" equation used later, which requires virtually 0 error in A, breaks when using the derivation from A = Angular Momentum / Mass due to the error being increased.
            // The area that breaks is Math.Sqrt(-2 * V * U / K)  hence needs to equal basically 0 as to stretch the time equation's bounds to be as close as possible to correct.
            double AbsSpecificAngularMomentum = Math.Sqrt(2 * SpecificTotalEnergy * Math.Pow(OrbitInformation.Periapsis, 2) + 2 * G * ParentMass * OrbitInformation.Periapsis);

            if (AbsSpecificAngularMomentum > 0)
            {
                AbsSpecificAngularMomentum -= AbsSpecificAngularMomentum * Math.Pow(10, -10);
            }
            else
            {
                AbsSpecificAngularMomentum += AbsSpecificAngularMomentum * Math.Pow(10, -10);
            }

            // K is a constant used in the following equations.
            double K = Math.Pow(G * ParentMass, 2) + 2 * SpecificTotalEnergy * Math.Pow(AbsSpecificAngularMomentum, 2);

            // U is a variable determined by r; MidpointAdjustedU is hence a variable determined by midpointDistance.
            double MidpointAdjustedU = 2 * SpecificTotalEnergy * Math.Pow(OrbitInformation.SemiMajorAxis, 2) + 2 * G * ParentMass * OrbitInformation.SemiMajorAxis - Math.Pow(AbsSpecificAngularMomentum, 2);

            // MidpointTimeAdjustment is the time added to the second equation for time too adjust for the discontinuity in the graphs.
            decimal MidpointTimeAdjustment = Convert.ToDecimal(-2 * G * ParentMass / (4 * SpecificTotalEnergy) * (Math.Sqrt(-2 / SpecificTotalEnergy) * Math.Asin(Math.Round(Math.Sqrt(-2 * SpecificTotalEnergy * MidpointAdjustedU / K), 5))));

            // OrbitalPeriod is the total time taken to complete one orbit in seconds.
            OrbitalPeriod = Convert.ToDecimal(2 * Math.PI * Math.Sqrt(Math.Pow(OrbitInformation.SemiMajorAxis, 3) / (G * ParentMass)));

            // Given that the position the body starts at is not the apoapsis, there is an offset in the time from the natural time given from starting at apoapsis.
            decimal startPositionTimeOffset = 0;

            // If at the extremes errors occur right on those edges. This means a small offset is given to keep the edgecases from throwing errors.
            if (StartingDistance <= OrbitInformation.Periapsis)
            {
                CalculatePoint(StartingDistance + StartingDistance / 400000, startPositionTimeOffset, OrbitalPeriod, true, out startPositionTimeOffset);
            }
            else if (StartingDistance >= OrbitInformation.Apoapsis)
            {
                CalculatePoint(StartingDistance - StartingDistance / 400000, startPositionTimeOffset, OrbitalPeriod, true, out startPositionTimeOffset);
            }
            else
            {
                CalculatePoint(StartingDistance, startPositionTimeOffset, OrbitalPeriod, true, out startPositionTimeOffset);
            }

            PointInformation[] predeterminedPoints = new PointInformation[NumberOfIntervals];
            
            for (int i = 0; i < NumberOfIntervals; i++)
            {
                // Points lie both above and below the semi-major axis line, hence a positive and negative case need to be created.
                double TrueAnomaly = i * 2 * Math.PI / NumberOfIntervals;

                double RelativeAbsPosition = (OrbitInformation.SemiMajorAxis * (1 - Math.Pow(OrbitInformation.Eccentricity, 2))) / (1 + OrbitInformation.Eccentricity * Math.Cos(TrueAnomaly));
                Vector3 relativePosition = new Vector3(Convert.ToSingle(RelativeAbsPosition * Math.Cos(TrueAnomaly)), Convert.ToSingle(RelativeAbsPosition * Math.Sin(TrueAnomaly)), 0);

                decimal time;

                bool CalculateIfPositive()
                {
                    if (TrueAnomaly > Math.PI)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                bool positive = CalculateIfPositive();

                CalculatePoint(RelativeAbsPosition, startPositionTimeOffset, OrbitalPeriod, positive, out time);

                double AngleOfVelocityToTangent = Math.Acos(OrbitInformation.AngularMomentum / Math.Sqrt(2 * OrbitInformation.TotalEnergy * Mass * Math.Pow(RelativeAbsPosition, 2) + 2 * G * ParentMass * Math.Pow(Mass, 2) * RelativeAbsPosition));

                double PerpendicularVelocity = OrbitInformation.AngularMomentum / (Mass * RelativeAbsPosition);
                double TotalVelocitySquared = 2 * OrbitInformation.TotalEnergy / Mass + (2 * G * ParentMass) / RelativeAbsPosition;
                Vector3 relativeVelocity = new Vector3(Convert.ToSingle(PerpendicularVelocity), Convert.ToSingle(Math.Sqrt(TotalVelocitySquared - Math.Pow(PerpendicularVelocity, 2))), 0);

                RotatePoint(ref relativePosition, ref relativeVelocity);

                predeterminedPoints[i] = new PointInformation(time, AngleOfVelocityToTangent, TrueAnomaly, relativePosition, relativeVelocity);
            }

            // This allows the efficient way of calculating which point the body is currently at to work.
            predeterminedPoints = predeterminedPoints.OrderBy(new Func<PointInformation, decimal>(x => x.Time)).ToArray();

            return predeterminedPoints;

            void CalculatePoint(double DistanceToParent, decimal StartPositionTimeOffset, decimal OrbitalPeriod, bool Positive, out decimal time)
            {
                // U is a constant used in the following equations.
                double U = 2 * SpecificTotalEnergy * DistanceToParent * DistanceToParent + 2 * G * ParentMass * DistanceToParent - AbsSpecificAngularMomentum * AbsSpecificAngularMomentum;

                if (DistanceToParent < OrbitInformation.SemiMajorAxis && Positive)
                {
                    time = (Convert.ToDecimal(-G * ParentMass / (4 * SpecificTotalEnergy) * (Math.Sqrt(-2 / SpecificTotalEnergy) * Math.Asin(Math.Round(Math.Sqrt(-2 * SpecificTotalEnergy * U / K), 5))) + 1 / (2 * SpecificTotalEnergy) * Math.Sqrt(U)) + TimeOffset + OrbitalPeriod - StartPositionTimeOffset) % OrbitalPeriod;
                }
                else if (DistanceToParent > OrbitInformation.SemiMajorAxis && Positive)
                {
                    time = (Convert.ToDecimal(G * ParentMass / (4 * SpecificTotalEnergy) * (Math.Sqrt(-2 / SpecificTotalEnergy) * Math.Asin(Math.Round(Math.Sqrt(-2 * SpecificTotalEnergy * U / K), 5))) + 1 / (2 * SpecificTotalEnergy) * Math.Sqrt(U)) + MidpointTimeAdjustment + TimeOffset + OrbitalPeriod - StartPositionTimeOffset) % OrbitalPeriod;
                }
                else if (DistanceToParent < OrbitInformation.SemiMajorAxis && !Positive)
                {
                    time = OrbitalPeriod - (Convert.ToDecimal(-G * ParentMass / (4 * SpecificTotalEnergy) * (Math.Sqrt(-2 / SpecificTotalEnergy) * Math.Asin(Math.Round(Math.Sqrt(-2 * SpecificTotalEnergy * U / K), 5))) + 1 / (2 * SpecificTotalEnergy) * Math.Sqrt(U)) + TimeOffset + OrbitalPeriod + StartPositionTimeOffset) % OrbitalPeriod;
                }
                else if (DistanceToParent > OrbitInformation.SemiMajorAxis && !Positive)
                {
                    time = OrbitalPeriod - (Convert.ToDecimal(G * ParentMass / (4 * SpecificTotalEnergy) * (Math.Sqrt(-2 / SpecificTotalEnergy) * Math.Asin(Math.Round(Math.Sqrt(-2 * SpecificTotalEnergy * U / K), 5))) + 1 / (2 * SpecificTotalEnergy) * Math.Sqrt(U)) + MidpointTimeAdjustment + TimeOffset + OrbitalPeriod + StartPositionTimeOffset) % OrbitalPeriod;
                }
                else
                {
                    throw new Exception("Variable 'time' has failed to be defined.");
                }
            }

            void RotatePoint(ref Vector3 relativePosition, ref Vector3 relativeVelocity)
            {
                // This rotates the orbit such that its periapsis is at the correct angle to the +x direction.
                TransformationMatrix3x3 rotationMatrixForPeriapsis = new TransformationMatrix3x3(new double[,] { { Math.Cos(OrbitInformation.ArgumentOfPeriapsis), Math.Sin(OrbitInformation.ArgumentOfPeriapsis), 0 }, { -Math.Sin(OrbitInformation.ArgumentOfPeriapsis), Math.Cos(OrbitInformation.ArgumentOfPeriapsis), 0 }, { 0, 0, 1 } });

                relativePosition = rotationMatrixForPeriapsis.Transform(relativePosition);
                relativeVelocity = rotationMatrixForPeriapsis.Transform(relativeVelocity);

                // This rotates the orbit to be the correct inclination.
                TransformationMatrix3x3 rotationMatrixForInclination = new TransformationMatrix3x3(new double[,] { { 1, 0, 0 }, { 0, Math.Cos(OrbitInformation.Inclination), Math.Sin(OrbitInformation.Inclination) }, { 0, -Math.Sin(OrbitInformation.Inclination), Math.Cos(OrbitInformation.Inclination) } });

                relativePosition = rotationMatrixForInclination.Transform(relativePosition);
                relativeVelocity = rotationMatrixForInclination.Transform(relativeVelocity);

                // This rotates the orbit such that the ascending node is at the correct angle to the +x direction.
                TransformationMatrix3x3 rotationMatrixForAscendingNode = new TransformationMatrix3x3(new double[,] { { Math.Cos(OrbitInformation.ArgumentOfPeriapsis), Math.Sin(OrbitInformation.ArgumentOfPeriapsis), 0 }, { -Math.Sin(OrbitInformation.ArgumentOfPeriapsis), Math.Cos(OrbitInformation.ArgumentOfPeriapsis), 0 }, { 0, 0, 1 } });

                relativePosition = rotationMatrixForAscendingNode.Transform(relativePosition);
                relativeVelocity = rotationMatrixForAscendingNode.Transform(relativeVelocity);
            }
        }

        private int currentIndex { get; set; } = 0;
        private ulong timeWraps = 0;
        public void UpdateCurrentPoint(decimal time)
        {
            if ((ulong)time / (ulong)orbitInformation.OrbitalPeriod != timeWraps)
            {
                timeWraps = (ulong)time / (ulong)orbitInformation.OrbitalPeriod;
                currentIndex = 0;
                return;
            }

            time = (time - StartingTimeFromEpoch) % orbitInformation.OrbitalPeriod;

            while (time > predeterminedPoints[(currentIndex + 1) % (predeterminedPoints.Length - 1)].Time)
            {
                currentIndex++;
            }

            currentPoint = predeterminedPoints[currentIndex];
        }

        public void UpdateCurrentPointUsingArraySearch(decimal time)
        {
            decimal[] predeterminedAdjustedTimes = new decimal[predeterminedPoints.Count()];

            if ((ulong)time / (ulong)orbitInformation.OrbitalPeriod != timeWraps)
            {
                timeWraps = (ulong)time / (ulong)orbitInformation.OrbitalPeriod;
            }

            time = (time - StartingTimeFromEpoch) % orbitInformation.OrbitalPeriod;

            int count = 0;
            foreach (PointInformation point in predeterminedPoints)
            {
                predeterminedAdjustedTimes[count] = Math.Abs(point.Time - time);
                count++;
            }

            currentIndex = Array.IndexOf(predeterminedAdjustedTimes, predeterminedAdjustedTimes.Min());

            currentPoint = predeterminedPoints[currentIndex];
        }

        public string ReturnEphemeris()
        {
            MessageBox.Show("This may take up to a few hours as 200000 data points are having to be converted into a human readable form.");

            string output = "Time, Angle of Velocity to Tangent, True Anomaly, Relative Position X, Relative Position Y, Relative Position Z, Relative Velocity X, Relative Velocity Y, Relative Velocity Z\n";

            foreach (PointInformation point in predeterminedPoints)
            {
                output += point.Time + ",";
                output += point.AngleOfVelocityToTangent + ",";
                output += point.TrueAnomaly + ",";
                output += point.RelativePosition.ToString().Substring(1, output.Length - 2) + ",";
                output += point.RelativeVelocity.ToString().Substring(1, output.Length - 2) + "\n";
            }

            return output;
        }

        // Instead of using commas, a comma and pipe symbol together are used to stop ambiguity in places such as converting a vector into a string.
        public override string ToString()
        {
            string output = "";
            output += Name              + ",|";
            output += Mass.ToString()   + ",|";
            output += Radius.ToString() + ",|";
            output += Colour.ToArgb().ToString() + ",|";

            output += StartingTimeFromEpoch.ToString() + ",|";
            output += StartingTrueAnomaly.ToString()   + ",|";
            output += orbitInformation.ToString()      + "\n";

            output = output.Replace("<", "");
            output = output.Replace(">", "");

            return output;
        }
    }
}
