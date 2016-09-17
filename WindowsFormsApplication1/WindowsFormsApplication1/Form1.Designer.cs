namespace roulette
{
	partial class Form1
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
            this.buttonRun = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonBackup = new System.Windows.Forms.Button();
            this.buttonRestore = new System.Windows.Forms.Button();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(12, 230);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(90, 30);
            this.buttonRun.TabIndex = 0;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.Location = new System.Drawing.Point(108, 230);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(90, 30);
            this.buttonStop.TabIndex = 1;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonBackup
            // 
            this.buttonBackup.Location = new System.Drawing.Point(256, 230);
            this.buttonBackup.Name = "buttonBackup";
            this.buttonBackup.Size = new System.Drawing.Size(90, 30);
            this.buttonBackup.TabIndex = 2;
            this.buttonBackup.Text = "Back up";
            this.buttonBackup.UseVisualStyleBackColor = true;
            this.buttonBackup.Click += new System.EventHandler(this.buttonBackup_Click);
            // 
            // buttonRestore
            // 
            this.buttonRestore.Location = new System.Drawing.Point(352, 230);
            this.buttonRestore.Name = "buttonRestore";
            this.buttonRestore.Size = new System.Drawing.Size(90, 30);
            this.buttonRestore.TabIndex = 3;
            this.buttonRestore.Text = "Restore";
            this.buttonRestore.UseVisualStyleBackColor = true;
            this.buttonRestore.Click += new System.EventHandler(this.buttonRestore_Click);
            // 
            // richTextBox
            // 
            this.richTextBox.Location = new System.Drawing.Point(12, 12);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(302, 212);
            this.richTextBox.TabIndex = 4;
            this.richTextBox.Text = "";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(322, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 212);
            this.listBox1.TabIndex = 5;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(50, 278);
            this.trackBar1.Maximum = 4;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(363, 45);
            this.trackBar1.TabIndex = 6;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 310);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Never";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 310);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "1 per day";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(216, 310);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "1 per hour";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(298, 310);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "1 per 10 min";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(378, 310);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "1 per min";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 335);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.buttonRestore);
            this.Controls.Add(this.buttonBackup);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonRun);
            this.Name = "Form1";
            this.Text = "Roulette";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonRun;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.Button buttonBackup;
		private System.Windows.Forms.Button buttonRestore;
		public System.Windows.Forms.RichTextBox richTextBox;
		private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
	}
}

