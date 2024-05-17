namespace _3D_Orbital_Motion_Simulation
{
    internal partial class RemoveBodyForm : Form
    {
        // The main form is passed into this form as it is a class.
        // This means it is reference typed and not copied, allowing access to tree to always be up to date and not a one time copy.
        MainForm mainForm { get; init; }
        public RemoveBodyForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        // Input validation for the name of the removed body.
        private void removeBodyInputTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Imposes a maximum length of the removed body name given the maximum length name an added body can have. 
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

        private void removeBodyButton_Click(object sender, EventArgs e)
        {
            if (mainForm.tree.IsEnabled() == true)
            {
                mainForm.tree.RemoveFromTree(removeBodyInputTextBox.Text.ToLower());
            }
            else
            {
                MessageBox.Show("There are currently no bodies in the simulation.");
            }
        }
    }
}
