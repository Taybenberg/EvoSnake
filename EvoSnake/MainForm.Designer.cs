namespace EvoSnake
{
    partial class MainForm
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
            this.DrawingPanel = new System.Windows.Forms.Panel();
            this.GenerationLabel = new System.Windows.Forms.Label();
            this.GenLabel = new System.Windows.Forms.Label();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.FitnessLabel = new System.Windows.Forms.Label();
            this.FitLabel = new System.Windows.Forms.Label();
            this.PlayButton = new System.Windows.Forms.Button();
            this.SpeedControl = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.SpeedControl)).BeginInit();
            this.SuspendLayout();
            // 
            // DrawingPanel
            // 
            this.DrawingPanel.Location = new System.Drawing.Point(0, 0);
            this.DrawingPanel.Name = "DrawingPanel";
            this.DrawingPanel.Size = new System.Drawing.Size(720, 720);
            this.DrawingPanel.TabIndex = 0;
            // 
            // GenerationLabel
            // 
            this.GenerationLabel.AutoSize = true;
            this.GenerationLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GenerationLabel.Location = new System.Drawing.Point(726, 67);
            this.GenerationLabel.Name = "GenerationLabel";
            this.GenerationLabel.Size = new System.Drawing.Size(128, 30);
            this.GenerationLabel.TabIndex = 2;
            this.GenerationLabel.Text = "Generation:";
            // 
            // GenLabel
            // 
            this.GenLabel.AutoSize = true;
            this.GenLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GenLabel.Location = new System.Drawing.Point(860, 67);
            this.GenLabel.Name = "GenLabel";
            this.GenLabel.Size = new System.Drawing.Size(25, 30);
            this.GenLabel.TabIndex = 3;
            this.GenLabel.Text = "0";
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoSize = true;
            this.MessageLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MessageLabel.Location = new System.Drawing.Point(726, 18);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(97, 30);
            this.MessageLabel.TabIndex = 4;
            this.MessageLabel.Text = "Message";
            // 
            // FitnessLabel
            // 
            this.FitnessLabel.AutoSize = true;
            this.FitnessLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FitnessLabel.Location = new System.Drawing.Point(726, 119);
            this.FitnessLabel.Name = "FitnessLabel";
            this.FitnessLabel.Size = new System.Drawing.Size(86, 30);
            this.FitnessLabel.TabIndex = 5;
            this.FitnessLabel.Text = "Fitness:";
            // 
            // FitLabel
            // 
            this.FitLabel.AutoSize = true;
            this.FitLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FitLabel.Location = new System.Drawing.Point(818, 119);
            this.FitLabel.Name = "FitLabel";
            this.FitLabel.Size = new System.Drawing.Size(25, 30);
            this.FitLabel.TabIndex = 6;
            this.FitLabel.Text = "0";
            // 
            // PlayButton
            // 
            this.PlayButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PlayButton.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PlayButton.Location = new System.Drawing.Point(731, 336);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(257, 60);
            this.PlayButton.TabIndex = 7;
            this.PlayButton.Text = "Start";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // SpeedControl
            // 
            this.SpeedControl.Location = new System.Drawing.Point(731, 270);
            this.SpeedControl.Maximum = 100;
            this.SpeedControl.Minimum = 1;
            this.SpeedControl.Name = "SpeedControl";
            this.SpeedControl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SpeedControl.Size = new System.Drawing.Size(257, 45);
            this.SpeedControl.TabIndex = 8;
            this.SpeedControl.TickStyle = System.Windows.Forms.TickStyle.None;
            this.SpeedControl.Value = 100;
            this.SpeedControl.ValueChanged += new System.EventHandler(this.SpeedControl_ValueChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 720);
            this.Controls.Add(this.SpeedControl);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.FitLabel);
            this.Controls.Add(this.FitnessLabel);
            this.Controls.Add(this.MessageLabel);
            this.Controls.Add(this.GenLabel);
            this.Controls.Add(this.GenerationLabel);
            this.Controls.Add(this.DrawingPanel);
            this.Icon = global::EvoSnake.Properties.Resources.favicon;
            this.Name = "MainForm";
            this.Text = "EvoSnake";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.SpeedControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel DrawingPanel;
        private System.Windows.Forms.Label GenerationLabel;
        private System.Windows.Forms.Label GenLabel;
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.Label FitnessLabel;
        private System.Windows.Forms.Label FitLabel;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.TrackBar SpeedControl;
    }
}

