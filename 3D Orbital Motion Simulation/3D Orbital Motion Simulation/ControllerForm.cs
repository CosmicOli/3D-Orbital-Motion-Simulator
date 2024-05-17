namespace _3D_Orbital_Motion_Simulation
{
    public partial class ControllerForm : Form
    {
        // The main form is passed into this form as it is a class.
        // This means it is reference typed and not copied, allowing access to tree to always be up to date and not a one time copy.
        // It also gives access to time and secondsPerSecond; these two variables are both displayed and changed by this form.
        private MainForm mainForm { get; init; }
        public ControllerForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void secondsPerSecondTrackbar_Scroll(object sender, EventArgs e)
        {
            void SetSecondsPerSecondFromTrackbarValue(ulong secondsPerSecond, string labelText, int labelXPosition)
            {
                mainForm.secondsPerSecond = secondsPerSecond;

                secondsPerSecondLabel.Text = labelText;
                secondsPerSecondLabel.Location = new Point(labelXPosition, 55);
            }

            // It made a lot more sense to have fixed and human time intervals than allowing a psuedo continuous trackbar, so predefined lengths of time were chosed to correspond to the trackbar's discrete values.
            switch (secondsPerSecondTrackbar.Value)
            {
                case 1:
                    SetSecondsPerSecondFromTrackbarValue(1, "1 second /s", 47);
                    break;
                case 2:
                    SetSecondsPerSecondFromTrackbarValue(60, "1 minute /s", 47);
                    break;
                case 3:
                    SetSecondsPerSecondFromTrackbarValue(3600, "1 hour /s", 54);
                    break;
                case 4:
                    SetSecondsPerSecondFromTrackbarValue(86400, "1 day /s", 56);
                    break;
                case 5:
                    SetSecondsPerSecondFromTrackbarValue(604800, "1 week /s", 52);
                    break;
                case 6:
                    SetSecondsPerSecondFromTrackbarValue(2678400, "31 days /s", 50);
                    break;
                case 7:
                    SetSecondsPerSecondFromTrackbarValue(15778800, "0.5 years /s", 49);
                    break;
                case 8:
                    SetSecondsPerSecondFromTrackbarValue(31557600, "1 year /s", 55);
                    break;
                case 9:
                    SetSecondsPerSecondFromTrackbarValue(315576000, "10 years /s", 49);
                    break;
                case 10:
                    SetSecondsPerSecondFromTrackbarValue(3155760000, "100 years /s", 46);
                    break;
            }
        }

        private void pausePlayButton_Click(object sender, EventArgs e)
        {
            if (pausePlayButton.Text == "Pause")
            {
                pausePlayButton.Text = "Play";
                secondsPerSecondTrackbar.Enabled = false;
                mainForm.secondsPerSecond = 0;
            }
            else
            {
                secondsPerSecondTrackbar.Enabled = true;
                pausePlayButton.Text = "Pause";
                secondsPerSecondTrackbar_Scroll(sender, e);
            }
        }

        private void updateCurrentTimeTimer_Tick(object sender, EventArgs e)
        {
            if (pausePlayButton.Text == "Pause")
            {
                // This is rounded as it makes it more readable to a human without lots of unnecesary decimal places.
                currentTimeTextBox.Text = Convert.ToString(Math.Round(mainForm.time, 2));
            }
        }

        private void setCurrentTimeButton_Click(object sender, EventArgs e)
        {
            if (pausePlayButton.Text == "Play")
            {
                mainForm.time = Convert.ToDecimal(currentTimeTextBox.Text);
                mainForm.tree.UpdateBodiesCurrentPositionsUsingArraySearch(Convert.ToDecimal(currentTimeTextBox.Text));
            }
        }

        // Input validation for changing the current time.
        private void currentTimeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Imposes an arbitrary limit of 100 characters maximum allowed in the text box to stop string character limit overflows.
            if (((TextBox)sender).Text.Length >= 100 && e.KeyChar != 8 && e.KeyChar != 127)
            {
                e.Handled = true;
            }
            // Only allows inputs of numbers, the '.' character as well as backspace and the delete key.
            else if ((e.KeyChar > 47 && e.KeyChar < 58) || e.KeyChar == 8 || e.KeyChar == 127)
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
            else
            {
                e.Handled = true;
            }
        }
    }
}
