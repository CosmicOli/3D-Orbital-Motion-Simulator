using System.Diagnostics;
using System.Numerics;
using System.Windows.Forms;

namespace _3D_Orbital_Motion_Simulation
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            tree = new BodyTree(new FixedBody(Color.Gold, "Sol", new Vector3(0, 0, 0), 1.989 * Math.Pow(10, 30), 696340000));
            time = 0;
            resetViewToolStripMenuItem_Click(new object(), new EventArgs());
        }

        // This is the tree structure that the simulation is built upon.
        internal BodyTree tree { get; init; }

        // Time is measured in seconds as the standard SI unit.
        public decimal time { get; set; }
        // This is the amount of time that occurs in the simulation every time a second occurs in real life.
        public decimal secondsPerSecond { get; set; } = 1;

        // This refers to how many metres in the simulation 1 pixel represents.
        private float inverseScaleFactor { get; set; }
        // The x and y translations are done with respect to the simulation's xy plane and not the viewing window. This is because it then stays consistent with scaling.
        private float xTranslation { get; set; }
        private float yTranslation { get; set; }

        private InformationForm informationForm { get; set; }
        private ExportBodysEphemerisForm exportBodysEphemerisForm { get; set; }
        private RemoveBodyForm removeBodyForm { get; set; }
        private AddBodyForm addBodyForm { get; set; }
        private ControllerForm controllerForm { get; set; }

        // A default reduced solar system is created on loading. It contains the sun, all major planets and the moon.
        private void MainForm_Load(object sender, EventArgs e)
        {
            double AU = 1.49597870700 * Math.Pow(10, 11);

            tree.AddToTree("Sol", Color.DarkGray, "Mercury", 3.285 * Math.Pow(10, 23), 0, 0, 0.3871 * AU, 0.20564, 0.122278, 0.843692, 0.50824, 2439700);

            tree.AddToTree("Sol", Color.Salmon, "Venus", 4.867 * Math.Pow(10, 24), 0, 0, 0.7233 * AU, 0.00676, 0.059306, 1.338144, 0.961676, 6051800);

            tree.AddToTree("Sol", Color.Blue, "Earth", 5.972 * Math.Pow(10, 24), 0, 0, AU, 0.0167086, 0, 3.0525809, 5.0282936, 6378100);

            tree.AddToTree("Earth", Color.Gray, "Luna", 7.347 * Math.Pow(10, 22), 0, 0, 0.3844 * Math.Pow(10, 9), 0.0549, 0.08979719, 0, 0, 1737400);

            tree.AddToTree("Sol", Color.OrangeRed, "Mars", 6.39 * Math.Pow(10, 23), 0, 0, 1.5237 * AU, 0.09337, 0.032323, 0.867603, 4.998099, 3389500);

            tree.AddToTree("Sol", Color.SandyBrown, "Jupiter", 1.898 * Math.Pow(10, 27), 0, 0, 5.2025 * AU, 0.04854, 0.022672, 1.750391, -1.50133, 69911000);

            tree.AddToTree("Sol", Color.Beige, "Saturn", 5.683 * Math.Pow(10, 26), 0, 0, 9.5415 * AU, 0.05551, 0.043529, 1.983392, -0.36268, 58232000);

            tree.AddToTree("Sol", Color.Azure, "Uranus", 8.681 * Math.Pow(10, 25), 0, 0, 19.188 * AU, 0.04686, 0.013491, 1.290846, 1.718626, 25362000);

            tree.AddToTree("Sol", Color.Navy, "Neptune", 1.024 * Math.Pow(10, 26), 0, 0, 30.07 * AU, 0.00895, 0.030892, 2.300169, -1.48545, 24622000);
        }

        // Timers in winforms do not run asynchronously to the tick event function.
        // This means that the time between intervals is as predefined, plus the amount of time it actually takes to run the code.
        // To counteract this, the time taken to run the code is measured and used to correct the secondsPerSecond to make it still for the most part accurate.
        Stopwatch watch = Stopwatch.StartNew();
        private void simulationTimer_Tick(object sender, EventArgs e)
        {
            watch.Stop();
            double CorrectionFactor = (double)watch.ElapsedMilliseconds / simulationTimer.Interval;

            if (CorrectionFactor > 0)
            {
                watch.Restart();
            }

            try
            {
                time += secondsPerSecond / Convert.ToDecimal(1000 / (simulationTimer.Interval * CorrectionFactor));
            }
            catch (OverflowException)
            {
                time = 0;
                MessageBox.Show("Time cannot increase as it has reached the decimal limit. Resetting time to 0.");
            }
            mousePositionLabel.Text = "Mouse Position (Metres): " + Convert.ToString(MousePosition.X * inverseScaleFactor + xTranslation) + ", " + Convert.ToString((Height - MousePosition.Y) * inverseScaleFactor + yTranslation);

            if (tree.IsEnabled())
            {
                if (secondsPerSecond > 0)
                {
                    tree.UpdateBodiesCurrentPositions(time);
                }

                if (mouseDown)
                {
                    xTranslation = (mouseStartLocation.X - MousePosition.X) * inverseScaleFactor;
                    yTranslation = (MousePosition.Y - mouseStartLocation.Y) * inverseScaleFactor;
                }

                DrawGUI();
            }
            else
            {
                Graphics gr = CreateGraphics();
                gr.Clear(BackColor);
                return;
            }
        }

        // Winforms graphics require previously drawn graphics to be drawn over back to the being the background colour, so a list of previously drawn locations is required.
        private List<KeyValuePair<Vector3, double>> previousBodyLocations = new List<KeyValuePair<Vector3, double>> { new KeyValuePair<Vector3, double>(new Vector3(0, 0, 0), 0) };
        private void DrawGUI()
        {
            List<KeyValuePair<Body, Vector3>> BodiesAndTheirPositions = tree.GetBodiesAndPositions();
            List<Vector3> positions = new List<Vector3>();
            foreach (KeyValuePair<Body, Vector3> body in BodiesAndTheirPositions)
            {
                positions.Add(body.Value);
            }

            Graphics gr = CreateGraphics();
            Pen pen;

            for (int i = 0; i < previousBodyLocations.Count; i++)
            {
                previousBodyLocations[i] = new KeyValuePair<Vector3, double>(previousBodyLocations[i].Key + new Vector3(xTranslation, yTranslation, 0), previousBodyLocations[i].Value);
            }

            foreach (KeyValuePair<Vector3, double> previousLocation in previousBodyLocations)
            {
                if (scrolled)
                {
                    gr.Clear(BackColor);
                    scrolled = false;
                    break;
                }
                // To stop flashing of non-moved bodies on the screen, they are not drawn over with the background colour unless they specifically have moved.
                if (positions.Contains(previousLocation.Key))
                {
                    continue;
                }

                pen = new Pen(BackColor);

                float CalculateDiameter()
                {
                    float diameter = Convert.ToSingle(previousLocation.Value / inverseScaleFactor) * 2;

                    if (diameter < 2)
                    {
                        diameter = 2;
                    }

                    return diameter;
                }
                float Diameter = CalculateDiameter();

                gr.FillEllipse(pen.Brush, Convert.ToSingle((previousLocation.Key.X - xTranslation - previousLocation.Value) / inverseScaleFactor), Convert.ToSingle(Height - (previousLocation.Key.Y - yTranslation + previousLocation.Value) / inverseScaleFactor), Diameter, Diameter);
            }
            previousBodyLocations.Clear();

            foreach (KeyValuePair<Body, Vector3> Body in BodiesAndTheirPositions)
            {
                pen = new Pen(Body.Key.Colour);

                float CalculateDiameter()
                {
                    float diameter = Convert.ToSingle(Body.Key.Radius / inverseScaleFactor) * 2;

                    // To deal with the fact that planets are comparitively tiny in comparison to the distance between them, a minimum rendered size is defined.
                    if (diameter < 2)
                    {
                        diameter = 2;
                    }

                    return diameter;
                }
                float Diameter = CalculateDiameter();

                try
                {
                    gr.FillEllipse(pen.Brush, Convert.ToSingle((Body.Value.X - xTranslation - Body.Key.Radius) / inverseScaleFactor), Convert.ToSingle(Height - (Body.Value.Y - yTranslation + Body.Key.Radius) / inverseScaleFactor), Diameter, Diameter);
                }
                catch (OverflowException)
                {
                    // If you zoom in too far, the FillEllipse function tries to calculate a massive ellipse which breaks the bounds of the number types used.
                    // The camera is reset and the user is told.
                    resetViewToolStripMenuItem_Click(new object(), new EventArgs());
                    MessageBox.Show("Reached zoom limit. Resetting view.");
                }
                previousBodyLocations.Add(new KeyValuePair<Vector3, double>(Body.Value - new Vector3(xTranslation, yTranslation, 0), Body.Key.Radius));
            }
        }

        // To allow dragging the camera around, the screen translation must be calculated while the user is dragging.
        // This requires asynchronous running, which is done using a semaphore to send a message to the mouse down function to stop doing calculation once the user stops holding left click.
        private SemaphoreSlim mouseUpSignal { get; set; } = new SemaphoreSlim(0, 1);
        private bool mouseDown { get; set; } = false;
        private Point mouseStartLocation { get; set; }
        private async void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left && tree.IsEnabled())
                {
                    mouseDown = true;
                    mouseStartLocation = new Point(Convert.ToInt32(xTranslation / inverseScaleFactor + MousePosition.X), Convert.ToInt32(-yTranslation / inverseScaleFactor + MousePosition.Y));
                    await mouseUpSignal.WaitAsync();
                    mouseDown = false;
                }
            }
            catch (OverflowException)
            {
                // If the user drags or zooms the camera to display space outside of float's range, the camera needs to be reset and the user told.
                resetViewToolStripMenuItem_Click(new object(), new EventArgs());
                mouseDown = false;
                MessageBox.Show("Attempted to click outside of the floating point maximum range. Resetting view.");
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && tree.IsEnabled())
            {
                mouseUpSignal.Release();
            }
            else if (e.Button == MouseButtons.Right && tree.IsEnabled())
            {
                Vector2 MouseLocation = new Vector2(MousePosition.X * inverseScaleFactor + xTranslation, (Height - MousePosition.Y) * inverseScaleFactor + yTranslation);

                string name = "";
                OrbitInformation OrbitInformation = tree.RegisterRightClick(MouseLocation, ref name, inverseScaleFactor);

                if (Application.OpenForms.OfType<InformationForm>().Count() == 0)
                {
                    informationForm = new InformationForm();
                    informationForm.SetLabels(name, OrbitInformation);
                    informationForm.Owner = this;
                    informationForm.Show();
                }
                else
                {
                    informationForm.SetLabels(name, OrbitInformation);
                    informationForm.Focus();
                }
            }
        }

        // As the previous locations and sizes of the bodies on the screen are altered through zooming, the draw function must be told to compensate to still clear the previous locations correctly.
        private bool scrolled { get; set; } = false;
        private void MainForm_MouseScroll(object sender, MouseEventArgs e)
        {
            if (tree.IsEnabled())
            {
                float scrollFactor;
                if (e.Delta < 0)
                {
                    scrollFactor = (float)6 / 5;
                }
                else
                {
                    scrollFactor = (float)5 / 6;
                }

                xTranslation += MousePosition.X * inverseScaleFactor - MousePosition.X * inverseScaleFactor * scrollFactor;
                yTranslation += (Height - MousePosition.Y) * inverseScaleFactor - (Height - MousePosition.Y) * inverseScaleFactor * scrollFactor;

                inverseScaleFactor *= scrollFactor;
                scrolled = true;
            }
        }

        // A custom file type is used here, but in essence it is simply a text file.
        // This is because the binarywriter can only store primitive data types, making saving the custom classes and structures significantly harder through such means.
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Simulation Save File|*.simsave";
            saveFileDialog.Title = "Save the Current Simulation";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                File.WriteAllText(saveFileDialog.FileName, tree.GetBodiesInSaveableFormat());
            }
        }

        // The simulation is saved only when the user tells it too, so they are reminded to save in case they had forgotton.
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Warning: the current simulation will not be saved. Make sure you manually save before loading a different simulation.");

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Simulation Save File|*.simsave";
            openFileDialog.Title = "Load a Saved Simulation";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                try
                {
                    tree.LoadTreeFromSaveableFormat(File.ReadAllText(openFileDialog.FileName));
                    tree.UpdateBodiesCurrentPositionsUsingArraySearch(time);
                    MessageBox.Show("Load successful.");
                }
                catch (Exception)
                {
                    MessageBox.Show("Loading failed. File likely corrupted or not in the correct format.");
                }
            }
        }

        private void exportABodysEphemerisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<ExportBodysEphemerisForm>().Count() == 0)
            {
                exportBodysEphemerisForm = new ExportBodysEphemerisForm(this);

                exportBodysEphemerisForm.Owner = this;
                exportBodysEphemerisForm.Show();
            }
            else
            {
                exportBodysEphemerisForm.Focus();
            }
        }

        private void addBodyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<AddBodyForm>().Count() == 0)
            {
                addBodyForm = new AddBodyForm(this);

                addBodyForm.Owner = this;
                addBodyForm.Show();
            }
            else
            {
                addBodyForm.Focus();
            }
        }

        private void removeBodyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<RemoveBodyForm>().Count() == 0)
            {
                removeBodyForm = new RemoveBodyForm(this);

                removeBodyForm.Owner = this;
                removeBodyForm.Show();
            }
            else
            {
                removeBodyForm.Focus();
            }
        }

        private void simulationControllerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<ControllerForm>().Count() == 0)
            {
                controllerForm = new ControllerForm(this);

                controllerForm.Owner = this;
                controllerForm.Show();
            }
            else
            {
                controllerForm.Focus();
            }
        }

        private void resetViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inverseScaleFactor = 10000000000;
            xTranslation = -9600000000000;
            yTranslation = -5400000000000;
            scrolled = true;
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}