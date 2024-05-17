namespace _3D_Orbital_Motion_Simulation
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the Contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            simulationTimer = new System.Windows.Forms.Timer(components);
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            loadToolStripMenuItem = new ToolStripMenuItem();
            exportABodysEphemerisToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            addBodyToolStripMenuItem = new ToolStripMenuItem();
            removeBodyToolStripMenuItem = new ToolStripMenuItem();
            simulationControllerToolStripMenuItem = new ToolStripMenuItem();
            resetViewToolStripMenuItem = new ToolStripMenuItem();
            quitToolStripMenuItem = new ToolStripMenuItem();
            mousePositionLabel = new Label();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // simulationTimer
            // 
            simulationTimer.Enabled = true;
            simulationTimer.Interval = 50;
            simulationTimer.Tick += simulationTimer_Tick;
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(24, 24);
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, simulationControllerToolStripMenuItem, resetViewToolStripMenuItem, quitToolStripMenuItem });
            menuStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(4, 1, 0, 1);
            menuStrip.Size = new Size(685, 24);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveAsToolStripMenuItem, loadToolStripMenuItem, exportABodysEphemerisToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 22);
            fileToolStripMenuItem.Text = "File";
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(215, 22);
            saveAsToolStripMenuItem.Text = "Save As";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(215, 22);
            loadToolStripMenuItem.Text = "Load";
            loadToolStripMenuItem.Click += loadToolStripMenuItem_Click;
            // 
            // exportABodysEphemerisToolStripMenuItem
            // 
            exportABodysEphemerisToolStripMenuItem.Name = "exportABodysEphemerisToolStripMenuItem";
            exportABodysEphemerisToolStripMenuItem.Size = new Size(215, 22);
            exportABodysEphemerisToolStripMenuItem.Text = "Export A Body's Ephemeris";
            exportABodysEphemerisToolStripMenuItem.Click += exportABodysEphemerisToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addBodyToolStripMenuItem, removeBodyToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 22);
            editToolStripMenuItem.Text = "Edit";
            // 
            // addBodyToolStripMenuItem
            // 
            addBodyToolStripMenuItem.Name = "addBodyToolStripMenuItem";
            addBodyToolStripMenuItem.Size = new Size(147, 22);
            addBodyToolStripMenuItem.Text = "Add Body";
            addBodyToolStripMenuItem.Click += addBodyToolStripMenuItem_Click;
            // 
            // removeBodyToolStripMenuItem
            // 
            removeBodyToolStripMenuItem.Name = "removeBodyToolStripMenuItem";
            removeBodyToolStripMenuItem.Size = new Size(147, 22);
            removeBodyToolStripMenuItem.Text = "Remove Body";
            removeBodyToolStripMenuItem.Click += removeBodyToolStripMenuItem_Click;
            // 
            // simulationControllerToolStripMenuItem
            // 
            simulationControllerToolStripMenuItem.Name = "simulationControllerToolStripMenuItem";
            simulationControllerToolStripMenuItem.Size = new Size(132, 22);
            simulationControllerToolStripMenuItem.Text = "Simulation Controller";
            simulationControllerToolStripMenuItem.Click += simulationControllerToolStripMenuItem_Click;
            // 
            // resetViewToolStripMenuItem
            // 
            resetViewToolStripMenuItem.Name = "resetViewToolStripMenuItem";
            resetViewToolStripMenuItem.Size = new Size(75, 22);
            resetViewToolStripMenuItem.Text = "Reset View";
            resetViewToolStripMenuItem.Click += resetViewToolStripMenuItem_Click;
            // 
            // quitToolStripMenuItem
            // 
            quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            quitToolStripMenuItem.Size = new Size(42, 22);
            quitToolStripMenuItem.Text = "Quit";
            quitToolStripMenuItem.Click += quitToolStripMenuItem_Click;
            // 
            // mousePositionLabel
            // 
            mousePositionLabel.AutoSize = true;
            mousePositionLabel.ForeColor = Color.White;
            mousePositionLabel.Location = new Point(0, 24);
            mousePositionLabel.Name = "mousePositionLabel";
            mousePositionLabel.Size = new Size(92, 15);
            mousePositionLabel.TabIndex = 1;
            mousePositionLabel.Text = "Mouse Position:";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(685, 566);
            Controls.Add(mousePositionLabel);
            Controls.Add(menuStrip);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            MainMenuStrip = menuStrip;
            Name = "MainForm";
            Text = "Orbital Motion";
            WindowState = FormWindowState.Maximized;
            Load += MainForm_Load;
            MouseDown += MainForm_MouseDown;
            MouseUp += MainForm_MouseUp;
            MouseWheel += MainForm_MouseScroll;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer simulationTimer;
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem simulationControllerToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem addBodyToolStripMenuItem;
        private ToolStripMenuItem removeBodyToolStripMenuItem;
        private Label mousePositionLabel;
        private ToolStripMenuItem resetViewToolStripMenuItem;
        private ToolStripMenuItem quitToolStripMenuItem;
        private ToolStripMenuItem exportABodysEphemerisToolStripMenuItem;
    }
}