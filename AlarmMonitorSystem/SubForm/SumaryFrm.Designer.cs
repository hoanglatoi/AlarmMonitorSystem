namespace AlarmMonitorSystem.SubForm
{
    partial class SumaryFrm
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
            this.switchMenu1 = new AlarmMonitorSystem.Controls.SwitchMenu();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // switchMenu1
            // 
            this.switchMenu1.Location = new System.Drawing.Point(129, 377);
            this.switchMenu1.Name = "switchMenu1";
            this.switchMenu1.Size = new System.Drawing.Size(478, 61);
            this.switchMenu1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(170, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 67);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // SumaryFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.switchMenu1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SumaryFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SumaryFrm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SumaryFrm_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.SwitchMenu switchMenu1;
        private Label label1;
    }
}