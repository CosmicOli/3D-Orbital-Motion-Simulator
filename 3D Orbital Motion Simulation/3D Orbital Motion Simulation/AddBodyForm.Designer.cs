namespace _3D_Orbital_Motion_Simulation
{
    partial class AddBodyForm
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
            components = new System.ComponentModel.Container();
            colourPickButton = new Button();
            colourBox = new Label();
            nameLabel = new Label();
            nameInputTextBox = new TextBox();
            massInputTextBox = new TextBox();
            massLabel = new Label();
            radiusInputTextBox = new TextBox();
            radiusLabel = new Label();
            sharedInputTextBox2 = new TextBox();
            sharedLabel2 = new Label();
            sharedInputTextBox1 = new TextBox();
            sharedLabel1 = new Label();
            sharedInputTextBox5 = new TextBox();
            sharedLabel5 = new Label();
            sharedInputTextBox4 = new TextBox();
            sharedLabel4 = new Label();
            sharedInputTextBox3 = new TextBox();
            sharedLabel3 = new Label();
            sharedInputTextBox6 = new TextBox();
            sharedLabel6 = new Label();
            movingBodyDefinitionTypeGroupBox = new GroupBox();
            stateVectorsSelectionButton = new RadioButton();
            keplerianElementsSelectionButton = new RadioButton();
            createBodyButton = new Button();
            extraLineLabel = new Label();
            parentBodyInputTextBox = new TextBox();
            parentBodyLabel = new Label();
            checkIfTreeEnabledTimer = new System.Windows.Forms.Timer(components);
            powerOfTenInputTextBox1 = new TextBox();
            powerOfTenInputTextBox2 = new TextBox();
            powerOfTenInputTextBox3 = new TextBox();
            powerOfTenInputTextBox4 = new TextBox();
            powerOfTenInputTextBox5 = new TextBox();
            powerOfTenInputTextBox6 = new TextBox();
            powerOfTenLabel1 = new Label();
            powerOfTenLabel2 = new Label();
            powerOfTenLabel3 = new Label();
            powerOfTenLabel4 = new Label();
            powerOfTenLabel5 = new Label();
            powerOfTenLabel6 = new Label();
            powerOfTenLabelMass = new Label();
            powerOfTenLabelRadius = new Label();
            powerOfTenInputTextBoxMass = new TextBox();
            powerOfTenInputTextBoxRadius = new TextBox();
            relativeToParentOrBackgroundVelocityGroupBox = new GroupBox();
            relativeToBackgroundVelocityButton = new RadioButton();
            relativeToParentButton = new RadioButton();
            movingBodyDefinitionTypeGroupBox.SuspendLayout();
            relativeToParentOrBackgroundVelocityGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // colourPickButton
            // 
            colourPickButton.Location = new Point(12, 42);
            colourPickButton.Name = "colourPickButton";
            colourPickButton.Size = new Size(52, 23);
            colourPickButton.TabIndex = 0;
            colourPickButton.Text = "Colour";
            colourPickButton.UseVisualStyleBackColor = true;
            colourPickButton.Click += colourPickButton_Click;
            // 
            // colourBox
            // 
            colourBox.AutoSize = true;
            colourBox.BackColor = Color.White;
            colourBox.BorderStyle = BorderStyle.Fixed3D;
            colourBox.Location = new Point(70, 46);
            colourBox.Name = "colourBox";
            colourBox.Size = new Size(18, 17);
            colourBox.TabIndex = 1;
            colourBox.Text = "   ";
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.BackColor = Color.Transparent;
            nameLabel.Location = new Point(15, 79);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(42, 15);
            nameLabel.TabIndex = 2;
            nameLabel.Text = "Name:";
            // 
            // nameInputTextBox
            // 
            nameInputTextBox.Location = new Point(63, 74);
            nameInputTextBox.Name = "nameInputTextBox";
            nameInputTextBox.Size = new Size(136, 23);
            nameInputTextBox.TabIndex = 3;
            nameInputTextBox.KeyPress += nameInputTextBox_KeyPress;
            // 
            // massInputTextBox
            // 
            massInputTextBox.Location = new Point(63, 103);
            massInputTextBox.Name = "massInputTextBox";
            massInputTextBox.Size = new Size(136, 23);
            massInputTextBox.TabIndex = 5;
            massInputTextBox.KeyPress += standardFormNumberInputTextBox_KeyPress;
            // 
            // massLabel
            // 
            massLabel.AutoSize = true;
            massLabel.BackColor = Color.Transparent;
            massLabel.Location = new Point(20, 106);
            massLabel.Name = "massLabel";
            massLabel.Size = new Size(37, 15);
            massLabel.TabIndex = 4;
            massLabel.Text = "Mass:";
            // 
            // radiusInputTextBox
            // 
            radiusInputTextBox.Location = new Point(63, 132);
            radiusInputTextBox.Name = "radiusInputTextBox";
            radiusInputTextBox.Size = new Size(136, 23);
            radiusInputTextBox.TabIndex = 7;
            radiusInputTextBox.KeyPress += standardFormNumberInputTextBox_KeyPress;
            // 
            // radiusLabel
            // 
            radiusLabel.AutoSize = true;
            radiusLabel.BackColor = Color.Transparent;
            radiusLabel.Location = new Point(12, 135);
            radiusLabel.Name = "radiusLabel";
            radiusLabel.Size = new Size(45, 15);
            radiusLabel.TabIndex = 6;
            radiusLabel.Text = "Radius:";
            // 
            // sharedInputTextBox2
            // 
            sharedInputTextBox2.Location = new Point(134, 240);
            sharedInputTextBox2.Name = "sharedInputTextBox2";
            sharedInputTextBox2.Size = new Size(65, 23);
            sharedInputTextBox2.TabIndex = 13;
            sharedInputTextBox2.KeyPress += standardFormNumberInputTextBox_KeyPress;
            // 
            // sharedLabel2
            // 
            sharedLabel2.AutoSize = true;
            sharedLabel2.BackColor = Color.Transparent;
            sharedLabel2.Location = new Point(39, 243);
            sharedLabel2.Name = "sharedLabel2";
            sharedLabel2.Size = new Size(97, 15);
            sharedLabel2.TabIndex = 12;
            sharedLabel2.Text = "Semi-major Axis:";
            // 
            // sharedInputTextBox1
            // 
            sharedInputTextBox1.Location = new Point(134, 211);
            sharedInputTextBox1.Name = "sharedInputTextBox1";
            sharedInputTextBox1.Size = new Size(65, 23);
            sharedInputTextBox1.TabIndex = 11;
            sharedInputTextBox1.KeyPress += standardFormNumberInputTextBox_KeyPress;
            // 
            // sharedLabel1
            // 
            sharedLabel1.AutoSize = true;
            sharedLabel1.BackColor = Color.Transparent;
            sharedLabel1.Location = new Point(53, 214);
            sharedLabel1.Name = "sharedLabel1";
            sharedLabel1.Size = new Size(83, 15);
            sharedLabel1.TabIndex = 10;
            sharedLabel1.Text = "True Anomaly:";
            // 
            // sharedInputTextBox5
            // 
            sharedInputTextBox5.Location = new Point(134, 327);
            sharedInputTextBox5.Name = "sharedInputTextBox5";
            sharedInputTextBox5.Size = new Size(65, 23);
            sharedInputTextBox5.TabIndex = 19;
            sharedInputTextBox5.KeyPress += standardFormNumberInputTextBox_KeyPress;
            // 
            // sharedLabel5
            // 
            sharedLabel5.AutoSize = true;
            sharedLabel5.BackColor = Color.Transparent;
            sharedLabel5.Location = new Point(9, 330);
            sharedLabel5.Name = "sharedLabel5";
            sharedLabel5.Size = new Size(127, 15);
            sharedLabel5.TabIndex = 18;
            sharedLabel5.Text = "Argument of Periapsis:";
            // 
            // sharedInputTextBox4
            // 
            sharedInputTextBox4.Location = new Point(134, 298);
            sharedInputTextBox4.Name = "sharedInputTextBox4";
            sharedInputTextBox4.Size = new Size(65, 23);
            sharedInputTextBox4.TabIndex = 17;
            sharedInputTextBox4.KeyPress += standardFormNumberInputTextBox_KeyPress;
            // 
            // sharedLabel4
            // 
            sharedLabel4.AutoSize = true;
            sharedLabel4.BackColor = Color.Transparent;
            sharedLabel4.Location = new Point(70, 301);
            sharedLabel4.Name = "sharedLabel4";
            sharedLabel4.Size = new Size(66, 15);
            sharedLabel4.TabIndex = 16;
            sharedLabel4.Text = "Inclination:";
            // 
            // sharedInputTextBox3
            // 
            sharedInputTextBox3.Location = new Point(134, 269);
            sharedInputTextBox3.Name = "sharedInputTextBox3";
            sharedInputTextBox3.Size = new Size(65, 23);
            sharedInputTextBox3.TabIndex = 15;
            sharedInputTextBox3.KeyPress += standardFormNumberInputTextBox_KeyPress;
            // 
            // sharedLabel3
            // 
            sharedLabel3.AutoSize = true;
            sharedLabel3.BackColor = Color.Transparent;
            sharedLabel3.Location = new Point(65, 272);
            sharedLabel3.Name = "sharedLabel3";
            sharedLabel3.Size = new Size(71, 15);
            sharedLabel3.TabIndex = 14;
            sharedLabel3.Text = "Eccentricity:";
            // 
            // sharedInputTextBox6
            // 
            sharedInputTextBox6.Location = new Point(134, 356);
            sharedInputTextBox6.Name = "sharedInputTextBox6";
            sharedInputTextBox6.Size = new Size(65, 23);
            sharedInputTextBox6.TabIndex = 21;
            sharedInputTextBox6.KeyPress += standardFormNumberInputTextBox_KeyPress;
            // 
            // sharedLabel6
            // 
            sharedLabel6.AutoSize = true;
            sharedLabel6.BackColor = Color.Transparent;
            sharedLabel6.Location = new Point(39, 349);
            sharedLabel6.Name = "sharedLabel6";
            sharedLabel6.Size = new Size(75, 15);
            sharedLabel6.TabIndex = 20;
            sharedLabel6.Text = "Longitude of";
            // 
            // movingBodyDefinitionTypeGroupBox
            // 
            movingBodyDefinitionTypeGroupBox.Controls.Add(stateVectorsSelectionButton);
            movingBodyDefinitionTypeGroupBox.Controls.Add(keplerianElementsSelectionButton);
            movingBodyDefinitionTypeGroupBox.Location = new Point(15, 161);
            movingBodyDefinitionTypeGroupBox.Name = "movingBodyDefinitionTypeGroupBox";
            movingBodyDefinitionTypeGroupBox.Size = new Size(250, 42);
            movingBodyDefinitionTypeGroupBox.TabIndex = 22;
            movingBodyDefinitionTypeGroupBox.TabStop = false;
            movingBodyDefinitionTypeGroupBox.Text = "Definition Type";
            // 
            // stateVectorsSelectionButton
            // 
            stateVectorsSelectionButton.AutoSize = true;
            stateVectorsSelectionButton.Location = new Point(137, 17);
            stateVectorsSelectionButton.Name = "stateVectorsSelectionButton";
            stateVectorsSelectionButton.Size = new Size(92, 19);
            stateVectorsSelectionButton.TabIndex = 1;
            stateVectorsSelectionButton.Text = "State Vectors";
            stateVectorsSelectionButton.UseVisualStyleBackColor = true;
            stateVectorsSelectionButton.Click += stateVectorsSelectionButton_Click;
            // 
            // keplerianElementsSelectionButton
            // 
            keplerianElementsSelectionButton.AutoSize = true;
            keplerianElementsSelectionButton.Checked = true;
            keplerianElementsSelectionButton.Location = new Point(6, 17);
            keplerianElementsSelectionButton.Name = "keplerianElementsSelectionButton";
            keplerianElementsSelectionButton.Size = new Size(125, 19);
            keplerianElementsSelectionButton.TabIndex = 0;
            keplerianElementsSelectionButton.TabStop = true;
            keplerianElementsSelectionButton.Text = "Keplerian Elements";
            keplerianElementsSelectionButton.UseVisualStyleBackColor = true;
            keplerianElementsSelectionButton.Click += keplerianElementsSelectionButton_Click;
            // 
            // createBodyButton
            // 
            createBodyButton.Location = new Point(12, 12);
            createBodyButton.Name = "createBodyButton";
            createBodyButton.Size = new Size(253, 24);
            createBodyButton.TabIndex = 23;
            createBodyButton.Text = "Create Body";
            createBodyButton.UseVisualStyleBackColor = true;
            createBodyButton.Click += createBodyButton_Click;
            // 
            // extraLineLabel
            // 
            extraLineLabel.AutoSize = true;
            extraLineLabel.Location = new Point(18, 364);
            extraLineLabel.Name = "extraLineLabel";
            extraLineLabel.Size = new Size(118, 15);
            extraLineLabel.TabIndex = 24;
            extraLineLabel.Text = "the Ascending Node:";
            // 
            // parentBodyInputTextBox
            // 
            parentBodyInputTextBox.Location = new Point(133, 385);
            parentBodyInputTextBox.Name = "parentBodyInputTextBox";
            parentBodyInputTextBox.Size = new Size(132, 23);
            parentBodyInputTextBox.TabIndex = 26;
            parentBodyInputTextBox.KeyPress += nameInputTextBox_KeyPress;
            // 
            // parentBodyLabel
            // 
            parentBodyLabel.AutoSize = true;
            parentBodyLabel.BackColor = Color.Transparent;
            parentBodyLabel.Location = new Point(27, 388);
            parentBodyLabel.Name = "parentBodyLabel";
            parentBodyLabel.Size = new Size(109, 15);
            parentBodyLabel.TabIndex = 25;
            parentBodyLabel.Text = "Parent Body Name:";
            // 
            // checkIfTreeEnabledTimer
            // 
            checkIfTreeEnabledTimer.Enabled = true;
            checkIfTreeEnabledTimer.Tick += checkIfTreeEnabledTimer_Tick;
            // 
            // powerOfTenInputTextBox1
            // 
            powerOfTenInputTextBox1.Location = new Point(241, 211);
            powerOfTenInputTextBox1.Name = "powerOfTenInputTextBox1";
            powerOfTenInputTextBox1.Size = new Size(24, 23);
            powerOfTenInputTextBox1.TabIndex = 27;
            powerOfTenInputTextBox1.Text = "0";
            powerOfTenInputTextBox1.TextAlign = HorizontalAlignment.Center;
            powerOfTenInputTextBox1.KeyPress += powerOfTenInputTextBox_KeyPress;
            // 
            // powerOfTenInputTextBox2
            // 
            powerOfTenInputTextBox2.Location = new Point(241, 240);
            powerOfTenInputTextBox2.Name = "powerOfTenInputTextBox2";
            powerOfTenInputTextBox2.Size = new Size(24, 23);
            powerOfTenInputTextBox2.TabIndex = 28;
            powerOfTenInputTextBox2.Text = "0";
            powerOfTenInputTextBox2.TextAlign = HorizontalAlignment.Center;
            powerOfTenInputTextBox2.KeyPress += powerOfTenInputTextBox_KeyPress;
            // 
            // powerOfTenInputTextBox3
            // 
            powerOfTenInputTextBox3.Location = new Point(241, 269);
            powerOfTenInputTextBox3.Name = "powerOfTenInputTextBox3";
            powerOfTenInputTextBox3.Size = new Size(24, 23);
            powerOfTenInputTextBox3.TabIndex = 29;
            powerOfTenInputTextBox3.Text = "0";
            powerOfTenInputTextBox3.TextAlign = HorizontalAlignment.Center;
            powerOfTenInputTextBox3.KeyPress += powerOfTenInputTextBox_KeyPress;
            // 
            // powerOfTenInputTextBox4
            // 
            powerOfTenInputTextBox4.Location = new Point(241, 298);
            powerOfTenInputTextBox4.Name = "powerOfTenInputTextBox4";
            powerOfTenInputTextBox4.Size = new Size(24, 23);
            powerOfTenInputTextBox4.TabIndex = 30;
            powerOfTenInputTextBox4.Text = "0";
            powerOfTenInputTextBox4.TextAlign = HorizontalAlignment.Center;
            powerOfTenInputTextBox4.KeyPress += powerOfTenInputTextBox_KeyPress;
            // 
            // powerOfTenInputTextBox5
            // 
            powerOfTenInputTextBox5.Location = new Point(241, 327);
            powerOfTenInputTextBox5.Name = "powerOfTenInputTextBox5";
            powerOfTenInputTextBox5.Size = new Size(24, 23);
            powerOfTenInputTextBox5.TabIndex = 31;
            powerOfTenInputTextBox5.Text = "0";
            powerOfTenInputTextBox5.TextAlign = HorizontalAlignment.Center;
            powerOfTenInputTextBox5.KeyPress += powerOfTenInputTextBox_KeyPress;
            // 
            // powerOfTenInputTextBox6
            // 
            powerOfTenInputTextBox6.Location = new Point(241, 356);
            powerOfTenInputTextBox6.Name = "powerOfTenInputTextBox6";
            powerOfTenInputTextBox6.Size = new Size(24, 23);
            powerOfTenInputTextBox6.TabIndex = 32;
            powerOfTenInputTextBox6.Text = "0";
            powerOfTenInputTextBox6.TextAlign = HorizontalAlignment.Center;
            powerOfTenInputTextBox6.KeyPress += powerOfTenInputTextBox_KeyPress;
            // 
            // powerOfTenLabel1
            // 
            powerOfTenLabel1.AutoSize = true;
            powerOfTenLabel1.BackColor = Color.Transparent;
            powerOfTenLabel1.Location = new Point(205, 214);
            powerOfTenLabel1.Name = "powerOfTenLabel1";
            powerOfTenLabel1.Size = new Size(33, 15);
            powerOfTenLabel1.TabIndex = 33;
            powerOfTenLabel1.Text = "x10^";
            // 
            // powerOfTenLabel2
            // 
            powerOfTenLabel2.AutoSize = true;
            powerOfTenLabel2.BackColor = Color.Transparent;
            powerOfTenLabel2.Location = new Point(205, 243);
            powerOfTenLabel2.Name = "powerOfTenLabel2";
            powerOfTenLabel2.Size = new Size(33, 15);
            powerOfTenLabel2.TabIndex = 34;
            powerOfTenLabel2.Text = "x10^";
            // 
            // powerOfTenLabel3
            // 
            powerOfTenLabel3.AutoSize = true;
            powerOfTenLabel3.BackColor = Color.Transparent;
            powerOfTenLabel3.Location = new Point(205, 272);
            powerOfTenLabel3.Name = "powerOfTenLabel3";
            powerOfTenLabel3.Size = new Size(33, 15);
            powerOfTenLabel3.TabIndex = 35;
            powerOfTenLabel3.Text = "x10^";
            // 
            // powerOfTenLabel4
            // 
            powerOfTenLabel4.AutoSize = true;
            powerOfTenLabel4.BackColor = Color.Transparent;
            powerOfTenLabel4.Location = new Point(205, 301);
            powerOfTenLabel4.Name = "powerOfTenLabel4";
            powerOfTenLabel4.Size = new Size(33, 15);
            powerOfTenLabel4.TabIndex = 36;
            powerOfTenLabel4.Text = "x10^";
            // 
            // powerOfTenLabel5
            // 
            powerOfTenLabel5.AutoSize = true;
            powerOfTenLabel5.BackColor = Color.Transparent;
            powerOfTenLabel5.Location = new Point(205, 330);
            powerOfTenLabel5.Name = "powerOfTenLabel5";
            powerOfTenLabel5.Size = new Size(33, 15);
            powerOfTenLabel5.TabIndex = 37;
            powerOfTenLabel5.Text = "x10^";
            // 
            // powerOfTenLabel6
            // 
            powerOfTenLabel6.AutoSize = true;
            powerOfTenLabel6.BackColor = Color.Transparent;
            powerOfTenLabel6.Location = new Point(205, 359);
            powerOfTenLabel6.Name = "powerOfTenLabel6";
            powerOfTenLabel6.Size = new Size(33, 15);
            powerOfTenLabel6.TabIndex = 38;
            powerOfTenLabel6.Text = "x10^";
            // 
            // powerOfTenLabelMass
            // 
            powerOfTenLabelMass.AutoSize = true;
            powerOfTenLabelMass.BackColor = Color.Transparent;
            powerOfTenLabelMass.Location = new Point(205, 106);
            powerOfTenLabelMass.Name = "powerOfTenLabelMass";
            powerOfTenLabelMass.Size = new Size(33, 15);
            powerOfTenLabelMass.TabIndex = 39;
            powerOfTenLabelMass.Text = "x10^";
            // 
            // powerOfTenLabelRadius
            // 
            powerOfTenLabelRadius.AutoSize = true;
            powerOfTenLabelRadius.BackColor = Color.Transparent;
            powerOfTenLabelRadius.Location = new Point(205, 135);
            powerOfTenLabelRadius.Name = "powerOfTenLabelRadius";
            powerOfTenLabelRadius.Size = new Size(33, 15);
            powerOfTenLabelRadius.TabIndex = 40;
            powerOfTenLabelRadius.Text = "x10^";
            // 
            // powerOfTenInputTextBoxMass
            // 
            powerOfTenInputTextBoxMass.Location = new Point(241, 103);
            powerOfTenInputTextBoxMass.Name = "powerOfTenInputTextBoxMass";
            powerOfTenInputTextBoxMass.Size = new Size(24, 23);
            powerOfTenInputTextBoxMass.TabIndex = 41;
            powerOfTenInputTextBoxMass.Text = "0";
            powerOfTenInputTextBoxMass.TextAlign = HorizontalAlignment.Center;
            powerOfTenInputTextBoxMass.KeyPress += powerOfTenInputTextBox_KeyPress;
            // 
            // powerOfTenInputTextBoxRadius
            // 
            powerOfTenInputTextBoxRadius.Location = new Point(241, 132);
            powerOfTenInputTextBoxRadius.Name = "powerOfTenInputTextBoxRadius";
            powerOfTenInputTextBoxRadius.Size = new Size(24, 23);
            powerOfTenInputTextBoxRadius.TabIndex = 42;
            powerOfTenInputTextBoxRadius.Text = "0";
            powerOfTenInputTextBoxRadius.TextAlign = HorizontalAlignment.Center;
            powerOfTenInputTextBoxRadius.KeyPress += powerOfTenInputTextBox_KeyPress;
            // 
            // relativeToParentOrBackgroundVelocityGroupBox
            // 
            relativeToParentOrBackgroundVelocityGroupBox.Controls.Add(relativeToBackgroundVelocityButton);
            relativeToParentOrBackgroundVelocityGroupBox.Controls.Add(relativeToParentButton);
            relativeToParentOrBackgroundVelocityGroupBox.Location = new Point(15, 385);
            relativeToParentOrBackgroundVelocityGroupBox.Name = "relativeToParentOrBackgroundVelocityGroupBox";
            relativeToParentOrBackgroundVelocityGroupBox.Size = new Size(250, 42);
            relativeToParentOrBackgroundVelocityGroupBox.TabIndex = 23;
            relativeToParentOrBackgroundVelocityGroupBox.TabStop = false;
            relativeToParentOrBackgroundVelocityGroupBox.Text = "Velocity Relative to Parent or Background";
            relativeToParentOrBackgroundVelocityGroupBox.Visible = false;
            // 
            // relativeToBackgroundVelocityButton
            // 
            relativeToBackgroundVelocityButton.AutoSize = true;
            relativeToBackgroundVelocityButton.Location = new Point(134, 17);
            relativeToBackgroundVelocityButton.Name = "relativeToBackgroundVelocityButton";
            relativeToBackgroundVelocityButton.Size = new Size(89, 19);
            relativeToBackgroundVelocityButton.TabIndex = 1;
            relativeToBackgroundVelocityButton.Text = "Background";
            relativeToBackgroundVelocityButton.UseVisualStyleBackColor = true;
            // 
            // relativeToParentButton
            // 
            relativeToParentButton.AutoSize = true;
            relativeToParentButton.Checked = true;
            relativeToParentButton.Location = new Point(34, 17);
            relativeToParentButton.Name = "relativeToParentButton";
            relativeToParentButton.Size = new Size(62, 19);
            relativeToParentButton.TabIndex = 0;
            relativeToParentButton.TabStop = true;
            relativeToParentButton.Text = "Parent ";
            relativeToParentButton.UseVisualStyleBackColor = true;
            // 
            // AddBodyForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(276, 416);
            Controls.Add(relativeToParentOrBackgroundVelocityGroupBox);
            Controls.Add(powerOfTenInputTextBoxRadius);
            Controls.Add(powerOfTenInputTextBoxMass);
            Controls.Add(powerOfTenLabelRadius);
            Controls.Add(powerOfTenLabelMass);
            Controls.Add(powerOfTenLabel6);
            Controls.Add(powerOfTenLabel5);
            Controls.Add(powerOfTenLabel4);
            Controls.Add(powerOfTenLabel3);
            Controls.Add(powerOfTenLabel2);
            Controls.Add(powerOfTenLabel1);
            Controls.Add(powerOfTenInputTextBox6);
            Controls.Add(powerOfTenInputTextBox5);
            Controls.Add(powerOfTenInputTextBox4);
            Controls.Add(powerOfTenInputTextBox3);
            Controls.Add(powerOfTenInputTextBox2);
            Controls.Add(powerOfTenInputTextBox1);
            Controls.Add(parentBodyInputTextBox);
            Controls.Add(parentBodyLabel);
            Controls.Add(extraLineLabel);
            Controls.Add(createBodyButton);
            Controls.Add(movingBodyDefinitionTypeGroupBox);
            Controls.Add(sharedInputTextBox6);
            Controls.Add(sharedLabel6);
            Controls.Add(sharedInputTextBox5);
            Controls.Add(sharedLabel5);
            Controls.Add(sharedInputTextBox4);
            Controls.Add(sharedLabel4);
            Controls.Add(sharedInputTextBox3);
            Controls.Add(sharedLabel3);
            Controls.Add(sharedInputTextBox2);
            Controls.Add(sharedLabel2);
            Controls.Add(sharedInputTextBox1);
            Controls.Add(sharedLabel1);
            Controls.Add(radiusInputTextBox);
            Controls.Add(radiusLabel);
            Controls.Add(massInputTextBox);
            Controls.Add(massLabel);
            Controls.Add(nameInputTextBox);
            Controls.Add(nameLabel);
            Controls.Add(colourBox);
            Controls.Add(colourPickButton);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(2);
            Name = "AddBodyForm";
            Text = "Add a Body";
            movingBodyDefinitionTypeGroupBox.ResumeLayout(false);
            movingBodyDefinitionTypeGroupBox.PerformLayout();
            relativeToParentOrBackgroundVelocityGroupBox.ResumeLayout(false);
            relativeToParentOrBackgroundVelocityGroupBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button colourPickButton;
        private Label colourBox;
        private Label nameLabel;
        private TextBox nameInputTextBox;
        private TextBox massInputTextBox;
        private Label massLabel;
        private TextBox radiusInputTextBox;
        private Label radiusLabel;
        private TextBox sharedInputTextBox2;
        private Label sharedLabel2;
        private TextBox sharedInputTextBox1;
        private Label sharedLabel1;
        private TextBox sharedInputTextBox5;
        private Label sharedLabel5;
        private TextBox sharedInputTextBox4;
        private Label sharedLabel4;
        private TextBox sharedInputTextBox3;
        private Label sharedLabel3;
        private TextBox sharedInputTextBox6;
        private Label sharedLabel6;
        private GroupBox movingBodyDefinitionTypeGroupBox;
        private RadioButton stateVectorsSelectionButton;
        private RadioButton keplerianElementsSelectionButton;
        private Button createBodyButton;
        private Label extraLineLabel;
        private TextBox parentBodyInputTextBox;
        private Label parentBodyLabel;
        private System.Windows.Forms.Timer checkIfTreeEnabledTimer;
        private TextBox powerOfTenInputTextBox1;
        private TextBox powerOfTenInputTextBox2;
        private TextBox powerOfTenInputTextBox3;
        private TextBox powerOfTenInputTextBox4;
        private TextBox powerOfTenInputTextBox5;
        private TextBox powerOfTenInputTextBox6;
        private Label powerOfTenLabel1;
        private Label powerOfTenLabel2;
        private Label powerOfTenLabel3;
        private Label powerOfTenLabel4;
        private Label powerOfTenLabel5;
        private Label powerOfTenLabel6;
        private Label powerOfTenLabelMass;
        private Label powerOfTenLabelRadius;
        private TextBox powerOfTenInputTextBoxMass;
        private TextBox powerOfTenInputTextBoxRadius;
        private GroupBox relativeToParentOrBackgroundVelocityGroupBox;
        private RadioButton relativeToBackgroundVelocityButton;
        private RadioButton relativeToParentButton;
    }
}