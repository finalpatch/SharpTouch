namespace SharpTouch
{
    partial class ControlPanel
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.generalPage = new System.Windows.Forms.TabPage();
            this.m_autoStart = new System.Windows.Forms.CheckBox();
            this.settingsPage = new System.Windows.Forms.TabPage();
            this.aboutPage = new System.Windows.Forms.TabPage();
            this.m_devName = new System.Windows.Forms.Label();
            this.m_apiVer = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_exit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_scrollSpeed = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.m_speedLabel = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.generalPage.SuspendLayout();
            this.settingsPage.SuspendLayout();
            this.aboutPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_scrollSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.generalPage);
            this.tabControl1.Controls.Add(this.settingsPage);
            this.tabControl1.Controls.Add(this.aboutPage);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(410, 209);
            this.tabControl1.TabIndex = 0;
            // 
            // generalPage
            // 
            this.generalPage.Controls.Add(this.m_autoStart);
            this.generalPage.Location = new System.Drawing.Point(4, 22);
            this.generalPage.Name = "generalPage";
            this.generalPage.Padding = new System.Windows.Forms.Padding(3);
            this.generalPage.Size = new System.Drawing.Size(402, 183);
            this.generalPage.TabIndex = 0;
            this.generalPage.Text = "General";
            this.generalPage.UseVisualStyleBackColor = true;
            // 
            // m_autoStart
            // 
            this.m_autoStart.AutoSize = true;
            this.m_autoStart.Location = new System.Drawing.Point(7, 7);
            this.m_autoStart.Name = "m_autoStart";
            this.m_autoStart.Size = new System.Drawing.Size(140, 17);
            this.m_autoStart.TabIndex = 0;
            this.m_autoStart.Text = "Auto start with Windows";
            this.m_autoStart.UseVisualStyleBackColor = true;
            this.m_autoStart.CheckedChanged += new System.EventHandler(this.m_autoStart_CheckedChanged);
            // 
            // settingsPage
            // 
            this.settingsPage.Controls.Add(this.groupBox2);
            this.settingsPage.Controls.Add(this.groupBox1);
            this.settingsPage.Location = new System.Drawing.Point(4, 22);
            this.settingsPage.Name = "settingsPage";
            this.settingsPage.Size = new System.Drawing.Size(402, 183);
            this.settingsPage.TabIndex = 2;
            this.settingsPage.Text = "Settings";
            this.settingsPage.UseVisualStyleBackColor = true;
            // 
            // aboutPage
            // 
            this.aboutPage.Controls.Add(this.m_devName);
            this.aboutPage.Controls.Add(this.m_apiVer);
            this.aboutPage.Controls.Add(this.label3);
            this.aboutPage.Controls.Add(this.label2);
            this.aboutPage.Controls.Add(this.label1);
            this.aboutPage.Location = new System.Drawing.Point(4, 22);
            this.aboutPage.Name = "aboutPage";
            this.aboutPage.Size = new System.Drawing.Size(402, 183);
            this.aboutPage.TabIndex = 1;
            this.aboutPage.Text = "About";
            this.aboutPage.UseVisualStyleBackColor = true;
            // 
            // m_devName
            // 
            this.m_devName.AutoSize = true;
            this.m_devName.Location = new System.Drawing.Point(123, 41);
            this.m_devName.Name = "m_devName";
            this.m_devName.Size = new System.Drawing.Size(68, 13);
            this.m_devName.TabIndex = 4;
            this.m_devName.Text = "device name";
            // 
            // m_apiVer
            // 
            this.m_apiVer.AutoSize = true;
            this.m_apiVer.Location = new System.Drawing.Point(123, 28);
            this.m_apiVer.Name = "m_apiVer";
            this.m_apiVer.Size = new System.Drawing.Size(58, 13);
            this.m_apiVer.TabIndex = 3;
            this.m_apiVer.Text = "api version";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Synaptics Device:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Synaptics API version:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "SharpTouch v0.1";
            // 
            // m_exit
            // 
            this.m_exit.Location = new System.Drawing.Point(12, 227);
            this.m_exit.Name = "m_exit";
            this.m_exit.Size = new System.Drawing.Size(75, 23);
            this.m_exit.TabIndex = 1;
            this.m_exit.Text = "Exit";
            this.m_exit.UseVisualStyleBackColor = true;
            this.m_exit.Click += new System.EventHandler(this.m_exit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_speedLabel);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.m_scrollSpeed);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(188, 183);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scroll Settings";
            // 
            // groupBox2
            // 
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Location = new System.Drawing.Point(202, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 183);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Gesture Settings";
            // 
            // m_scrollSpeed
            // 
            this.m_scrollSpeed.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.m_scrollSpeed.LargeChange = 100;
            this.m_scrollSpeed.Location = new System.Drawing.Point(6, 36);
            this.m_scrollSpeed.Maximum = 1500;
            this.m_scrollSpeed.Name = "m_scrollSpeed";
            this.m_scrollSpeed.Size = new System.Drawing.Size(176, 45);
            this.m_scrollSpeed.SmallChange = 20;
            this.m_scrollSpeed.TabIndex = 0;
            this.m_scrollSpeed.TickFrequency = 100;
            this.m_scrollSpeed.Value = 1000;
            this.m_scrollSpeed.Scroll += new System.EventHandler(this.m_scrollSpeed_Scroll);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Speed";
            // 
            // m_speedLabel
            // 
            this.m_speedLabel.AutoSize = true;
            this.m_speedLabel.Location = new System.Drawing.Point(51, 20);
            this.m_speedLabel.Name = "m_speedLabel";
            this.m_speedLabel.Size = new System.Drawing.Size(33, 13);
            this.m_speedLabel.TabIndex = 2;
            this.m_speedLabel.Text = "100%";
            // 
            // ControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 262);
            this.Controls.Add(this.m_exit);
            this.Controls.Add(this.tabControl1);
            this.Name = "ControlPanel";
            this.Text = "ControlPanel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ControlPanel_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.generalPage.ResumeLayout(false);
            this.generalPage.PerformLayout();
            this.settingsPage.ResumeLayout(false);
            this.aboutPage.ResumeLayout(false);
            this.aboutPage.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_scrollSpeed)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage generalPage;
        private System.Windows.Forms.CheckBox m_autoStart;
        private System.Windows.Forms.TabPage aboutPage;
        private System.Windows.Forms.TabPage settingsPage;
        private System.Windows.Forms.Label m_devName;
        private System.Windows.Forms.Label m_apiVer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button m_exit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar m_scrollSpeed;
        private System.Windows.Forms.Label m_speedLabel;
    }
}