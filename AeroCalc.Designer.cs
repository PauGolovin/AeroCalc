namespace AeroCalc
{
    partial class AeroCalc
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
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.fileButton = new System.Windows.Forms.Button();
            this.nameLabel = new System.Windows.Forms.Label();
            this.fileTextBox = new System.Windows.Forms.TextBox();
            this.enterWingLabel = new System.Windows.Forms.Label();
            this.ratioLabel = new System.Windows.Forms.Label();
            this.ratioTextBox = new System.Windows.Forms.TextBox();
            this.taperTextBox = new System.Windows.Forms.TextBox();
            this.taperLabel = new System.Windows.Forms.Label();
            this.sweepTextBox = new System.Windows.Forms.TextBox();
            this.sweepLabel = new System.Windows.Forms.Label();
            this.AoALabel = new System.Windows.Forms.Label();
            this.firstAoATextBox = new System.Windows.Forms.TextBox();
            this.lastAoATextBox = new System.Windows.Forms.TextBox();
            this.stepTextBox = new System.Windows.Forms.TextBox();
            this.meshSizeLabel = new System.Windows.Forms.Label();
            this.XMeshSizeTextBox = new System.Windows.Forms.TextBox();
            this.XMeshSizeLabel = new System.Windows.Forms.Label();
            this.ZMeshSizeTextFile = new System.Windows.Forms.TextBox();
            this.ZMeshSizeLabel = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.resultTextBox = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.importButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fileButton
            // 
            this.fileButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.fileButton.Location = new System.Drawing.Point(275, 50);
            this.fileButton.Name = "fileButton";
            this.fileButton.Size = new System.Drawing.Size(90, 23);
            this.fileButton.TabIndex = 0;
            this.fileButton.Text = "Search File";
            this.fileButton.UseVisualStyleBackColor = true;
            this.fileButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.nameLabel.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.nameLabel.Location = new System.Drawing.Point(10, 10);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(360, 22);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Aerodynamic Calculation by Panel Method";
            // 
            // fileTextBox
            // 
            this.fileTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fileTextBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.fileTextBox.Location = new System.Drawing.Point(10, 50);
            this.fileTextBox.Name = "fileTextBox";
            this.fileTextBox.Size = new System.Drawing.Size(250, 23);
            this.fileTextBox.TabIndex = 2;
            this.fileTextBox.Text = "Search File with Wing Nodes Coordinates";
            this.fileTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.fileTextBox_MouseClick);
            // 
            // enterWingLabel
            // 
            this.enterWingLabel.AutoSize = true;
            this.enterWingLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.enterWingLabel.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.enterWingLabel.Location = new System.Drawing.Point(10, 85);
            this.enterWingLabel.Name = "enterWingLabel";
            this.enterWingLabel.Size = new System.Drawing.Size(212, 19);
            this.enterWingLabel.TabIndex = 3;
            this.enterWingLabel.Text = "Enter next wing characteristic:";
            // 
            // ratioLabel
            // 
            this.ratioLabel.AutoSize = true;
            this.ratioLabel.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ratioLabel.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.ratioLabel.Location = new System.Drawing.Point(10, 120);
            this.ratioLabel.Name = "ratioLabel";
            this.ratioLabel.Size = new System.Drawing.Size(46, 17);
            this.ratioLabel.TabIndex = 4;
            this.ratioLabel.Text = "Ratio:";
            // 
            // ratioTextBox
            // 
            this.ratioTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ratioTextBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.ratioTextBox.Location = new System.Drawing.Point(60, 120);
            this.ratioTextBox.Name = "ratioTextBox";
            this.ratioTextBox.Size = new System.Drawing.Size(110, 23);
            this.ratioTextBox.TabIndex = 5;
            this.ratioTextBox.Text = "Enter Wing Ratio";
            this.ratioTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ratioTextBox_MouseClick);
            // 
            // taperTextBox
            // 
            this.taperTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.taperTextBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.taperTextBox.Location = new System.Drawing.Point(255, 120);
            this.taperTextBox.Name = "taperTextBox";
            this.taperTextBox.Size = new System.Drawing.Size(110, 23);
            this.taperTextBox.TabIndex = 7;
            this.taperTextBox.Text = "Enter Wing Taper";
            this.taperTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.taperTextBox_MouseClick);
            // 
            // taperLabel
            // 
            this.taperLabel.AutoSize = true;
            this.taperLabel.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.taperLabel.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.taperLabel.Location = new System.Drawing.Point(205, 120);
            this.taperLabel.Name = "taperLabel";
            this.taperLabel.Size = new System.Drawing.Size(47, 17);
            this.taperLabel.TabIndex = 6;
            this.taperLabel.Text = "Taper:";
            // 
            // sweepTextBox
            // 
            this.sweepTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.sweepTextBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.sweepTextBox.Location = new System.Drawing.Point(60, 160);
            this.sweepTextBox.Name = "sweepTextBox";
            this.sweepTextBox.Size = new System.Drawing.Size(110, 23);
            this.sweepTextBox.TabIndex = 9;
            this.sweepTextBox.Text = "Enter Wing Sweep";
            this.sweepTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.sweepTextBox_MouseClick);
            // 
            // sweepLabel
            // 
            this.sweepLabel.AutoSize = true;
            this.sweepLabel.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.sweepLabel.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.sweepLabel.Location = new System.Drawing.Point(10, 160);
            this.sweepLabel.Name = "sweepLabel";
            this.sweepLabel.Size = new System.Drawing.Size(51, 17);
            this.sweepLabel.TabIndex = 8;
            this.sweepLabel.Text = "Sweep:";
            // 
            // AoALabel
            // 
            this.AoALabel.AutoSize = true;
            this.AoALabel.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.AoALabel.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.AoALabel.Location = new System.Drawing.Point(10, 200);
            this.AoALabel.Name = "AoALabel";
            this.AoALabel.Size = new System.Drawing.Size(314, 17);
            this.AoALabel.TabIndex = 10;
            this.AoALabel.Text = "Enter first and last angles and step for calculation";
            // 
            // firstAoATextBox
            // 
            this.firstAoATextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.firstAoATextBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.firstAoATextBox.Location = new System.Drawing.Point(10, 230);
            this.firstAoATextBox.Name = "firstAoATextBox";
            this.firstAoATextBox.Size = new System.Drawing.Size(110, 23);
            this.firstAoATextBox.TabIndex = 11;
            this.firstAoATextBox.Text = "Enter First AoA";
            this.firstAoATextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.firstAoATextBox_MouseClick);
            // 
            // lastAoATextBox
            // 
            this.lastAoATextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.lastAoATextBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lastAoATextBox.Location = new System.Drawing.Point(132, 230);
            this.lastAoATextBox.Name = "lastAoATextBox";
            this.lastAoATextBox.Size = new System.Drawing.Size(110, 23);
            this.lastAoATextBox.TabIndex = 12;
            this.lastAoATextBox.Text = "Enter Last AoA";
            this.lastAoATextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lastAoATextBox_MouseClick);
            // 
            // stepTextBox
            // 
            this.stepTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.stepTextBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.stepTextBox.Location = new System.Drawing.Point(254, 230);
            this.stepTextBox.Name = "stepTextBox";
            this.stepTextBox.Size = new System.Drawing.Size(110, 23);
            this.stepTextBox.TabIndex = 13;
            this.stepTextBox.Text = "Enter Step";
            this.stepTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.stepTextBox_MouseClick);
            // 
            // meshSizeLabel
            // 
            this.meshSizeLabel.AutoSize = true;
            this.meshSizeLabel.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.meshSizeLabel.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.meshSizeLabel.Location = new System.Drawing.Point(10, 270);
            this.meshSizeLabel.Name = "meshSizeLabel";
            this.meshSizeLabel.Size = new System.Drawing.Size(298, 17);
            this.meshSizeLabel.TabIndex = 14;
            this.meshSizeLabel.Text = "Enter the discretization of the wing to calculate";
            // 
            // XMeshSizeTextBox
            // 
            this.XMeshSizeTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.XMeshSizeTextBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.XMeshSizeTextBox.Location = new System.Drawing.Point(105, 300);
            this.XMeshSizeTextBox.Name = "XMeshSizeTextBox";
            this.XMeshSizeTextBox.Size = new System.Drawing.Size(70, 23);
            this.XMeshSizeTextBox.TabIndex = 16;
            this.XMeshSizeTextBox.Text = "X Mesh Size";
            this.XMeshSizeTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.XMeshSizeTextBox_MouseClick);
            // 
            // XMeshSizeLabel
            // 
            this.XMeshSizeLabel.AutoSize = true;
            this.XMeshSizeLabel.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.XMeshSizeLabel.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.XMeshSizeLabel.Location = new System.Drawing.Point(10, 300);
            this.XMeshSizeLabel.Name = "XMeshSizeLabel";
            this.XMeshSizeLabel.Size = new System.Drawing.Size(93, 17);
            this.XMeshSizeLabel.TabIndex = 15;
            this.XMeshSizeLabel.Text = "Along X Axis:";
            // 
            // ZMeshSizeTextFile
            // 
            this.ZMeshSizeTextFile.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ZMeshSizeTextFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.ZMeshSizeTextFile.Location = new System.Drawing.Point(295, 300);
            this.ZMeshSizeTextFile.Name = "ZMeshSizeTextFile";
            this.ZMeshSizeTextFile.Size = new System.Drawing.Size(70, 23);
            this.ZMeshSizeTextFile.TabIndex = 18;
            this.ZMeshSizeTextFile.Text = "Z Mesh Size";
            this.ZMeshSizeTextFile.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ZMeshSizeTextFile_MouseClick);
            // 
            // ZMeshSizeLabel
            // 
            this.ZMeshSizeLabel.AutoSize = true;
            this.ZMeshSizeLabel.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ZMeshSizeLabel.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.ZMeshSizeLabel.Location = new System.Drawing.Point(200, 300);
            this.ZMeshSizeLabel.Name = "ZMeshSizeLabel";
            this.ZMeshSizeLabel.Size = new System.Drawing.Size(91, 17);
            this.ZMeshSizeLabel.TabIndex = 17;
            this.ZMeshSizeLabel.Text = "Along Z Axis:";
            // 
            // startButton
            // 
            this.startButton.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.startButton.ForeColor = System.Drawing.Color.Green;
            this.startButton.Location = new System.Drawing.Point(195, 350);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(170, 50);
            this.startButton.TabIndex = 19;
            this.startButton.Text = "Start Calculating";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // resultTextBox
            // 
            this.resultTextBox.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.resultTextBox.Location = new System.Drawing.Point(400, 10);
            this.resultTextBox.Multiline = true;
            this.resultTextBox.Name = "resultTextBox";
            this.resultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resultTextBox.Size = new System.Drawing.Size(390, 313);
            this.resultTextBox.TabIndex = 20;
            this.resultTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // importButton
            // 
            this.importButton.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.importButton.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.importButton.Location = new System.Drawing.Point(400, 350);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(190, 50);
            this.importButton.TabIndex = 21;
            this.importButton.Text = "Export to Excel";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.exitButton.ForeColor = System.Drawing.Color.Red;
            this.exitButton.Location = new System.Drawing.Point(600, 350);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(190, 50);
            this.exitButton.TabIndex = 22;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.clearButton.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.clearButton.Location = new System.Drawing.Point(12, 350);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(170, 50);
            this.clearButton.TabIndex = 23;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // AeroCalc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.resultTextBox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.ZMeshSizeTextFile);
            this.Controls.Add(this.ZMeshSizeLabel);
            this.Controls.Add(this.XMeshSizeTextBox);
            this.Controls.Add(this.XMeshSizeLabel);
            this.Controls.Add(this.meshSizeLabel);
            this.Controls.Add(this.stepTextBox);
            this.Controls.Add(this.lastAoATextBox);
            this.Controls.Add(this.firstAoATextBox);
            this.Controls.Add(this.AoALabel);
            this.Controls.Add(this.sweepTextBox);
            this.Controls.Add(this.sweepLabel);
            this.Controls.Add(this.taperTextBox);
            this.Controls.Add(this.taperLabel);
            this.Controls.Add(this.ratioTextBox);
            this.Controls.Add(this.ratioLabel);
            this.Controls.Add(this.enterWingLabel);
            this.Controls.Add(this.fileTextBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.fileButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AeroCalc";
            this.Text = "AeroCalc";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button fileButton;
        private Label nameLabel;
        private TextBox fileTextBox;
        private Label enterWingLabel;
        private Label ratioLabel;
        private TextBox ratioTextBox;
        private TextBox taperTextBox;
        private Label taperLabel;
        private TextBox sweepTextBox;
        private Label sweepLabel;
        private Label AoALabel;
        private TextBox firstAoATextBox;
        private TextBox lastAoATextBox;
        private TextBox stepTextBox;
        private Label meshSizeLabel;
        private TextBox XMeshSizeTextBox;
        private Label XMeshSizeLabel;
        private TextBox ZMeshSizeTextFile;
        private Label ZMeshSizeLabel;
        private Button startButton;
        private TextBox resultTextBox;
        private OpenFileDialog openFileDialog1;
        private Button importButton;
        private Button exitButton;
        private Button clearButton;
    }
}