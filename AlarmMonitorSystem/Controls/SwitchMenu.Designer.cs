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
            this.SuspendLayout();
            // 
            // HistoryBtn
            // 
            this.HistoryBtn.Location = new System.Drawing.Point(150, 28);
            this.HistoryBtn.Name = "HistoryBtn";
            this.HistoryBtn.Size = new System.Drawing.Size(120, 38);
            this.HistoryBtn.TabIndex = 3;
            this.HistoryBtn.Text = "History";
            this.HistoryBtn.UseVisualStyleBackColor = true;
            // 
            // sumaryBtn
            // 
            this.sumaryBtn.Location = new System.Drawing.Point(6, 28);
            this.sumaryBtn.Name = "sumaryBtn";
            this.sumaryBtn.Size = new System.Drawing.Size(120, 38);
            this.sumaryBtn.TabIndex = 2;
            this.sumaryBtn.Text = "Summary";
            this.sumaryBtn.UseVisualStyleBackColor = true;
            // 
            // SwitchMenu
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(508, 86);
            this.ControlBox = false;
            this.Controls.Add(this.HistoryBtn);
            this.Controls.Add(this.sumaryBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SwitchMenu";
            this.ResumeLayout(false);

        }

        #endregion

        private Button HistoryBtn;
        private Button sumaryBtn;
    }
}
