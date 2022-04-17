namespace AlarmMonitorSystem.SubForm
{
    partial class BaseForm
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
            this.sumaryBtn = new System.Windows.Forms.Button();
            this.HistoryBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // sumaryBtn
            // 
            this.sumaryBtn.Location = new System.Drawing.Point(96, 374);
            this.sumaryBtn.Name = "sumaryBtn";
            this.sumaryBtn.Size = new System.Drawing.Size(120, 38);
            this.sumaryBtn.TabIndex = 0;
            this.sumaryBtn.Text = "Summary";
            this.sumaryBtn.UseVisualStyleBackColor = true;
            // 
            // HistoryBtn
            // 
            this.HistoryBtn.Location = new System.Drawing.Point(240, 374);
            this.HistoryBtn.Name = "HistoryBtn";
            this.HistoryBtn.Size = new System.Drawing.Size(120, 38);
            this.HistoryBtn.TabIndex = 1;
            this.HistoryBtn.Text = "History";
            this.HistoryBtn.UseVisualStyleBackColor = true;
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.HistoryBtn);
            this.Controls.Add(this.sumaryBtn);
            this.Name = "BaseForm";
            this.Text = "BaseForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Button sumaryBtn;
        private Button HistoryBtn;
    }
}