using System.Numerics;

namespace _3D_Orbital_Motion_Simulation
{
    internal partial class AddBodyForm : Form
    {
        // The main form is passed into this form as it is a class.
        // This means it is reference typed and not copied, allowing access to tree to always be up to date and not a one time copy.
        private MainForm mainForm { get; init; }
        public AddBodyForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        // I use the colour picker dialog to allow a user to input any colour they wish.
        // To display their chosen colour, I use a label the user is unable to click on as it is both quicker and simpler than using the graphics functions.
        private void colourPickButton_Click(object sender, EventArgs e)
        {
            ColorDialog myDialog = new ColorDialog();
            myDialog.AllowFullOpen = true;
            myDialog.ShowHelp = true;
            myDialog.Color = colourBox.BackColor;

            if (myDialog.ShowDialog() == DialogResult.OK)
            {
                colourBox.BackColor = myDialog.Color;
            }
        }

        // This sets the form to be designed for inputting a body defined by state vectors.
        private void stateVectorsSelectionButton_Click(object sender, EventArgs e)
        {
            sharedLabel1.Location = new Point(43, 214);
            sharedLabel2.Location = new Point(43, 243);
            sharedLabel3.Location = new Point(43, 272);
            sharedLabel4.Location = new Point(45, 301);
            sharedLabel5.Location = new Point(45, 330);
            sharedLabel6.Location = new Point(45, 359);
            extraLineLabel.Visible = false;

            sharedLabel1.Text = "Position X:";
            sharedLabel2.Text = "Position Y:";
            sharedLabel3.Text = "Position Z:";
            sharedLabel4.Text = "Velocity X:";
            sharedLabel5.Text = "Velocity Y:";
            sharedLabel6.Text = "Velocity Z:";

            parentBodyInputTextBox.Visible = false;
            parentBodyLabel.Visible = false;

            relativeToParentOrBackgroundVelocityGroupBox.Visible = true;

            Size = new Size(292, 474);
        }

        // This sets the form to be designed for inputting a body defined by keplerian elements.
        private void keplerianElementsSelectionButton_Click(object sender, EventArgs e)
        {
            sharedLabel1.Location = new Point(53, 214);
            sharedLabel2.Location = new Point(39, 243);
            sharedLabel3.Location = new Point(65, 272);
            sharedLabel4.Location = new Point(70, 301);
            sharedLabel5.Location = new Point(9, 330);
            sharedLabel6.Location = new Point(39, 349);
            extraLineLabel.Visible = true;

            sharedLabel1.Text = "True Anomaly:";
            sharedLabel2.Text = "Semi-major Axis:";
            sharedLabel3.Text = "Eccentricity:";
            sharedLabel4.Text = "Inclination:";
            sharedLabel5.Text = "Argument of Periapsis:";
            sharedLabel6.Text = "Longitude of";

            parentBodyInputTextBox.Visible = true;
            parentBodyLabel.Visible = true;

            relativeToParentOrBackgroundVelocityGroupBox.Visible = false;

            Size = new Size(292, 455);
        }

        // As there are many number input boxes, it made the code much easier to read and write by making a standard function for handling their data.
        private double handleNumberInputs(TextBox BaseNumber, TextBox PowerOfTen)
        {
            return Convert.ToDouble(BaseNumber.Text) * Math.Pow(10, Convert.ToDouble(PowerOfTen.Text));
        }

        private void createBodyButton_Click(object sender, EventArgs e)
        {
            try
            {
                double Mass = handleNumberInputs(massInputTextBox, powerOfTenInputTextBoxMass);
                double Radius = handleNumberInputs(radiusInputTextBox, powerOfTenInputTextBoxRadius);
                double SharedInput1 = handleNumberInputs(sharedInputTextBox1, powerOfTenInputTextBox1);
                double SharedInput2 = handleNumberInputs(sharedInputTextBox2, powerOfTenInputTextBox2);
                double SharedInput3 = handleNumberInputs(sharedInputTextBox3, powerOfTenInputTextBox3);

                if (mainForm.tree.IsEnabled())
                {
                    double SharedInput4 = handleNumberInputs(sharedInputTextBox4, powerOfTenInputTextBox4);
                    double SharedInput5 = handleNumberInputs(sharedInputTextBox5, powerOfTenInputTextBox5);
                    double SharedInput6 = handleNumberInputs(sharedInputTextBox6, powerOfTenInputTextBox6);

                    if (keplerianElementsSelectionButton.Checked)
                    {
                        mainForm.tree.AddToTree(parentBodyInputTextBox.Text, colourBox.BackColor, nameInputTextBox.Text, Mass, mainForm.time, SharedInput1, SharedInput2, SharedInput3, SharedInput4, SharedInput5, SharedInput6, Radius);
                    }
                    else if (stateVectorsSelectionButton.Checked)
                    {
                        mainForm.tree.AddToTree(colourBox.BackColor, nameInputTextBox.Text, Mass, mainForm.time, new Vector3(Convert.ToSingle(SharedInput4), Convert.ToSingle(SharedInput5), Convert.ToSingle(SharedInput6)), new Vector3(Convert.ToSingle(SharedInput1), Convert.ToSingle(SharedInput2), Convert.ToSingle(SharedInput3)), Radius, relativeToBackgroundVelocityButton.Checked);
                    }
                }
                else
                {
                    mainForm.tree.Reenable(new FixedBody(colourBox.BackColor, nameInputTextBox.Text, new Vector3(Convert.ToSingle(SharedInput1), Convert.ToSingle(SharedInput2), Convert.ToSingle(SharedInput3)), Mass, Radius));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to create body with the given parameters.");
            }
        }

        // The form has to continuously check whether the tree is enabled so that as soon as a user deletes the reference body they aren't able to break the tree structure by attempting to add a moving body as the reference body.
        private void checkIfTreeEnabledTimer_Tick(object sender, EventArgs e)
        {
            if (!mainForm.tree.IsEnabled())
            {
                sharedLabel1.Location = new Point(43, 214);
                sharedLabel2.Location = new Point(43, 243);
                sharedLabel3.Location = new Point(43, 272);

                sharedLabel1.Text = "Position X:";
                sharedLabel2.Text = "Position Y:";
                sharedLabel3.Text = "Position Z:";

                if (keplerianElementsSelectionButton.Checked)
                {
                    keplerianElementsSelectionButton.Checked = false;

                    createBodyButton.Text = "Create Reference Body";

                    sharedInputTextBox1.Text = "0";
                    sharedInputTextBox2.Text = "0";
                    sharedInputTextBox3.Text = "0";

                    powerOfTenInputTextBox1.Text = "0";
                    powerOfTenInputTextBox2.Text = "0";
                    powerOfTenInputTextBox3.Text = "0";
                }

                movingBodyDefinitionTypeGroupBox.Visible = false;

                Size = new Size(292, 338);
            }
            else
            {
                if (!movingBodyDefinitionTypeGroupBox.Visible)
                {
                    movingBodyDefinitionTypeGroupBox.Visible = true;
                    keplerianElementsSelectionButton.Checked = true;
                    keplerianElementsSelectionButton_Click(sender, e);

                    createBodyButton.Text = "Create Body";

                    sharedInputTextBox1.Text = "";
                    sharedInputTextBox2.Text = "";
                    sharedInputTextBox3.Text = "";
                    sharedInputTextBox4.Text = "";
                    sharedInputTextBox5.Text = "";
                    sharedInputTextBox6.Text = "";

                    powerOfTenInputTextBox1.Text = "";
                    powerOfTenInputTextBox2.Text = "";
                    powerOfTenInputTextBox3.Text = "";
                    powerOfTenInputTextBox4.Text = "";
                    powerOfTenInputTextBox5.Text = "";
                    powerOfTenInputTextBox6.Text = "";
                }
            }
        }

        // Input validation for all standard form number's decimal part.
        private void standardFormNumberInputTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Imposes an arbitrary limit of 100 characters maximum allowed in the text box to stop string character limit overflows.
            if (((TextBox)sender).Text.Length >= 100 && e.KeyChar != 8 && e.KeyChar != 127)
            {
                e.Handled = true;
            }
            // Only allows inputs of numbers, the '.' and '-' character as well as backspace and the delete key.
            else if ((e.KeyChar > 47 && e.KeyChar < 58) || e.KeyChar == 45 || e.KeyChar == 8 || e.KeyChar == 127)
            {
                e.Handled = false;
            }
            else if (e.KeyChar == 46)
            {
                // If there is already a decimal point, don't allow another.
                if (((TextBox)sender).Text.Contains('.'))
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
            else if (e.KeyChar == 45)
            {
                // If there is already a minus, don't allow another.
                if (((TextBox)sender).Text.Contains('-'))
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        // Input validation for all standard form number's power of ten part.
        private void powerOfTenInputTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Imposes a maximum length of 2 characters given the largest possible float is of the order of magnitude of 10^38 and 38 is 2 characters long.
            if (((TextBox)sender).Text.Length >= 2 && e.KeyChar != 8 && e.KeyChar != 127)
            {
                e.Handled = true;
            }
            // Only allows inputs of numbers as well as backspace and the delete key.
            else if ((e.KeyChar > 47 && e.KeyChar < 58) || e.KeyChar == 45 || e.KeyChar == 8 || e.KeyChar == 127)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        // Input validation for the name of the added or parent body.
        private void nameInputTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Imposes a basically arbitrary maximum length derived from the maximum number of characters for a name to still be displayed in full on the informationForm. 
            if (((TextBox)sender).Text.Length >= 17 && e.KeyChar != 8 && e.KeyChar != 127)
            {
                e.Handled = true;
            }
            // Allows all regular characters and punctuation, space, and backspace.
            else if ((e.KeyChar > 31 && e.KeyChar < 128) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
