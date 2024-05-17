namespace _3D_Orbital_Motion_Simulation
{
    partial class RemoveBodyForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the Contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            removeBodyButton = new Button();
            removeBodyInputTextBox = new TextBox();
            removeBodyPromptLabel = new Label();
            SuspendLayout();
            // 
            // removeBodyButton
            // 
            removeBodyButton.Location = new Point(9, 44);
            removeBodyButton.Margin = new Padding(2);
            removeBodyButton.Name = "removeBodyButton";
            removeBodyButton.Size = new Size(163, 26);
            removeBodyButton.TabIndex = 0;
            removeBodyButton.Text = "Remove Body And Children";
            removeBodyButton.UseVisualStyleBackColor = true;
            removeBodyButton.Click += removeBodyButton_Click;
            // 
            // removeBodyInputTextBox
            // 
            removeBodyInputTextBox.Location = new Point(9, 22);
            removeBodyInputTextBox.Margin = new Padding(2);
            removeBodyInputTextBox.Name = "removeBodyInputTextBox";
            removeBodyInputTextBox.Size = new Size(163, 23);
            removeBodyInputTextBox.TabIndex = 1;
            removeBodyInputTextBox.KeyPress += removeBodyInputTextBox_KeyPress;
            // 
            // removeBodyPromptLabel
            // 
            removeBodyPromptLabel.AutoSize = true;
            removeBodyPromptLabel.Location = new Point(15, 5);
            removeBodyPromptLabel.Margin = new Padding(2, 0, 2, 0);
            removeBodyPromptLabel.Name = "removeBodyPromptLabel";
            removeBodyPromptLabel.Size = new Size(153, 15);
            removeBodyPromptLabel.TabIndex = 2;
            removeBodyPromptLabel.Text = "Please enter a body's name:";
            // 
            // RemoveBodyForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(179, 81);
            Controls.Add(removeBodyPromptLabel);
            Controls.Add(removeBodyInputTextBox);
            Controls.Add(removeBodyButton);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(2);
            Name = "RemoveBodyForm";
            Text = "Remove a Body";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button removeBodyButton;
        private TextBox removeBodyInputTextBox;
        private Label removeBodyPromptLabel;
    }
}