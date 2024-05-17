namespace _3D_Orbital_Motion_Simulation
{
    partial class ExportBodysEphemerisForm
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
            exportBodysEphemerisButton = new Button();
            pickBodyInputTextBox = new TextBox();
            removeBodyPromptLabel = new Label();
            warningLabel1 = new Label();
            warningLabel2 = new Label();
            warningLabel3 = new Label();
            SuspendLayout();
            // 
            // exportBodysEphemerisButton
            // 
            exportBodysEphemerisButton.Location = new Point(9, 95);
            exportBodysEphemerisButton.Margin = new Padding(2);
            exportBodysEphemerisButton.Name = "exportBodysEphemerisButton";
            exportBodysEphemerisButton.Size = new Size(163, 26);
            exportBodysEphemerisButton.TabIndex = 0;
            exportBodysEphemerisButton.Text = "Export Body's Ephemeris";
            exportBodysEphemerisButton.UseVisualStyleBackColor = true;
            exportBodysEphemerisButton.Click += exportBodysEphemerisButton_Click;
            // 
            // pickBodyInputTextBox
            // 
            pickBodyInputTextBox.Location = new Point(9, 22);
            pickBodyInputTextBox.Margin = new Padding(2);
            pickBodyInputTextBox.Name = "pickBodyInputTextBox";
            pickBodyInputTextBox.Size = new Size(163, 23);
            pickBodyInputTextBox.TabIndex = 1;
            pickBodyInputTextBox.KeyPress += pickBodyInputTextBox_KeyPress;
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
            // warningLabel1
            // 
            warningLabel1.AutoSize = true;
            warningLabel1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            warningLabel1.ForeColor = Color.Red;
            warningLabel1.Location = new Point(16, 47);
            warningLabel1.Margin = new Padding(2, 0, 2, 0);
            warningLabel1.Name = "warningLabel1";
            warningLabel1.Size = new Size(149, 15);
            warningLabel1.TabIndex = 3;
            warningLabel1.Text = "PLEASE SAVE BEFORE USE";
            // 
            // warningLabel2
            // 
            warningLabel2.AutoSize = true;
            warningLabel2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            warningLabel2.ForeColor = Color.Red;
            warningLabel2.Location = new Point(23, 62);
            warningLabel2.Margin = new Padding(2, 0, 2, 0);
            warningLabel2.Name = "warningLabel2";
            warningLabel2.Size = new Size(131, 15);
            warningLabel2.TabIndex = 4;
            warningLabel2.Text = "THIS MAY TAKE A FEW";
            // 
            // warningLabel3
            // 
            warningLabel3.AutoSize = true;
            warningLabel3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            warningLabel3.ForeColor = Color.Red;
            warningLabel3.Location = new Point(24, 77);
            warningLabel3.Margin = new Padding(2, 0, 2, 0);
            warningLabel3.Name = "warningLabel3";
            warningLabel3.Size = new Size(130, 15);
            warningLabel3.TabIndex = 5;
            warningLabel3.Text = "HOURS TO COMPLETE";
            // 
            // ExportBodysEphemerisForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(179, 132);
            Controls.Add(warningLabel3);
            Controls.Add(warningLabel2);
            Controls.Add(warningLabel1);
            Controls.Add(removeBodyPromptLabel);
            Controls.Add(pickBodyInputTextBox);
            Controls.Add(exportBodysEphemerisButton);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(2);
            Name = "ExportBodysEphemeris";
            Text = "Export a Body's Ephemeris";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button exportBodysEphemerisButton;
        private TextBox pickBodyInputTextBox;
        private Label removeBodyPromptLabel;
        private Label warningLabel1;
        private Label warningLabel2;
        private Label warningLabel3;
    }
}