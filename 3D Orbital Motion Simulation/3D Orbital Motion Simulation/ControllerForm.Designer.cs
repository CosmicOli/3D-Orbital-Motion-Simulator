namespace _3D_Orbital_Motion_Simulation
{
    partial class ControllerForm
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
            pausePlayButton = new Button();
            clockSpeedTitleLabel = new Label();
            secondsPerSecondTrackbar = new TrackBar();
            secondsPerSecondLabel = new Label();
            currentTimeLabel = new Label();
            currentTimeTextBox = new TextBox();
            setCurrentTimeButton = new Button();
            updateCurrentTimeTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)secondsPerSecondTrackbar).BeginInit();
            SuspendLayout();
            // 
            // pausePlayButton
            // 
            pausePlayButton.Location = new Point(8, 72);
            pausePlayButton.Margin = new Padding(2);
            pausePlayButton.Name = "pausePlayButton";
            pausePlayButton.Size = new Size(146, 25);
            pausePlayButton.TabIndex = 0;
            pausePlayButton.Text = "Pause";
            pausePlayButton.UseVisualStyleBackColor = true;
            pausePlayButton.Click += pausePlayButton_Click;
            // 
            // clockSpeedTitleLabel
            // 
            clockSpeedTitleLabel.AutoSize = true;
            clockSpeedTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point);
            clockSpeedTitleLabel.Location = new Point(45, 8);
            clockSpeedTitleLabel.Margin = new Padding(2, 0, 2, 0);
            clockSpeedTitleLabel.Name = "clockSpeedTitleLabel";
            clockSpeedTitleLabel.Size = new Size(72, 15);
            clockSpeedTitleLabel.TabIndex = 1;
            clockSpeedTitleLabel.Text = "Clock Speed";
            // 
            // secondsPerSecondTrackbar
            // 
            secondsPerSecondTrackbar.LargeChange = 1;
            secondsPerSecondTrackbar.Location = new Point(8, 25);
            secondsPerSecondTrackbar.Margin = new Padding(2);
            secondsPerSecondTrackbar.Minimum = 1;
            secondsPerSecondTrackbar.Name = "secondsPerSecondTrackbar";
            secondsPerSecondTrackbar.Size = new Size(146, 45);
            secondsPerSecondTrackbar.TabIndex = 2;
            secondsPerSecondTrackbar.Value = 1;
            secondsPerSecondTrackbar.Scroll += secondsPerSecondTrackbar_Scroll;
            // 
            // secondsPerSecondLabel
            // 
            secondsPerSecondLabel.AutoSize = true;
            secondsPerSecondLabel.Location = new Point(47, 55);
            secondsPerSecondLabel.Name = "secondsPerSecondLabel";
            secondsPerSecondLabel.Size = new Size(67, 15);
            secondsPerSecondLabel.TabIndex = 4;
            secondsPerSecondLabel.Text = "1 second /s";
            // 
            // currentTimeLabel
            // 
            currentTimeLabel.AutoSize = true;
            currentTimeLabel.Font = new Font("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point);
            currentTimeLabel.Location = new Point(43, 110);
            currentTimeLabel.Name = "currentTimeLabel";
            currentTimeLabel.Size = new Size(76, 15);
            currentTimeLabel.TabIndex = 5;
            currentTimeLabel.Text = "Current Time";
            // 
            // currentTimeTextBox
            // 
            currentTimeTextBox.Location = new Point(8, 128);
            currentTimeTextBox.Name = "currentTimeTextBox";
            currentTimeTextBox.Size = new Size(146, 23);
            currentTimeTextBox.TabIndex = 6;
            currentTimeTextBox.KeyPress += currentTimeTextBox_KeyPress;
            // 
            // setCurrentTimeButton
            // 
            setCurrentTimeButton.Location = new Point(8, 157);
            setCurrentTimeButton.Name = "setCurrentTimeButton";
            setCurrentTimeButton.Size = new Size(146, 23);
            setCurrentTimeButton.TabIndex = 7;
            setCurrentTimeButton.Text = "Set Time";
            setCurrentTimeButton.UseVisualStyleBackColor = true;
            setCurrentTimeButton.Click += setCurrentTimeButton_Click;
            // 
            // updateCurrentTimeTimer
            // 
            updateCurrentTimeTimer.Enabled = true;
            updateCurrentTimeTimer.Tick += updateCurrentTimeTimer_Tick;
            // 
            // ControllerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(163, 190);
            Controls.Add(setCurrentTimeButton);
            Controls.Add(currentTimeTextBox);
            Controls.Add(currentTimeLabel);
            Controls.Add(secondsPerSecondLabel);
            Controls.Add(secondsPerSecondTrackbar);
            Controls.Add(clockSpeedTitleLabel);
            Controls.Add(pausePlayButton);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(2);
            Name = "ControllerForm";
            Text = "Simulation Settings";
            ((System.ComponentModel.ISupportInitialize)secondsPerSecondTrackbar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button pausePlayButton;
        private Label clockSpeedTitleLabel;
        private TrackBar secondsPerSecondTrackbar;
        private Label secondsPerSecondLabel;
        private Label currentTimeLabel;
        private TextBox currentTimeTextBox;
        private Button setCurrentTimeButton;
        private System.Windows.Forms.Timer updateCurrentTimeTimer;
    }
}