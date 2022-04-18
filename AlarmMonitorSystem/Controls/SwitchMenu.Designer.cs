namespace AlarmMonitorSystem.Controls
{
    partial class SwitchMenu
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.HistoryBtn = new System.Windows.Forms.Button();
            this.sumaryBtn = new System.Windows.Forms.Button();
            this.settingsBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // HistoryBtn
            // 
            this.HistoryBtn.Location = new System.Drawing.Point(180, 9);
            this.HistoryBtn.Name = "HistoryBtn";
            this.HistoryBtn.Size = new System.Drawing.Size(120, 38);
            this.HistoryBtn.TabIndex = 3;
            this.HistoryBtn.Text = "History";
            this.HistoryBtn.UseVisualStyleBackColor = true;
            this.HistoryBtn.Click += new System.EventHandler(this.HistoryBtn_Click);
            // 
            // sumaryBtn
            // 
            this.sumaryBtn.Location = new System.Drawing.Point(24, 9);
            this.sumaryBtn.Name = "sumaryBtn";
            this.sumaryBtn.Size = new System.Drawing.Size(120, 38);
            this.sumaryBtn.TabIndex = 2;
            this.sumaryBtn.Text = "Summary";
            this.sumaryBtn.UseVisualStyleBackColor = true;
            this.sumaryBtn.Click += new System.EventHandler(this.sumaryBtn_Click);
            // 
            // settingsBtn
            // 
            this.settingsBtn.Location = new System.Drawing.Point(341, 9);
            this.settingsBtn.Name = "settingsBtn";
            this.settingsBtn.Size = new System.Drawing.Size(120, 38);
            this.settingsBtn.TabIndex = 4;
            this.settingsBtn.Text = "Settings";
            this.settingsBtn.UseVisualStyleBackColor = true;
            this.settingsBtn.Click += new System.EventHandler(this.settingsBtn_Click);
            // 
            // SwitchMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.settingsBtn);
            this.Controls.Add(this.HistoryBtn);
            this.Controls.Add(this.sumaryBtn);
            this.Name = "SwitchMenu";
            this.Size = new System.Drawing.Size(489, 54);
            this.ResumeLayout(false);

        }

        #endregion

        private Button HistoryBtn;
        private Button sumaryBtn;
        private Button settingsBtn;
    }
}
