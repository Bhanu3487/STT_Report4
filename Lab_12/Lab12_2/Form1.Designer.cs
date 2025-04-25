using System.Windows.Forms;
using System.Drawing;

namespace Lab12_2 
{
    partial class Form1 
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.Button btnStart;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            // Dispose the timer if it exists when the form is disposed
            if (disposing && (_updateTimer != null))
            {
                _updateTimer.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.txtTime = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // txtTime
            //
            this.txtTime.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtTime.Location = new System.Drawing.Point(40, 30);
            this.txtTime.Name = "txtTime";
            this.txtTime.PlaceholderText = "HH:MM:SS";
            this.txtTime.Size = new System.Drawing.Size(180, 34);
            this.txtTime.TabIndex = 0;
            //
            // btnStart
            //
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnStart.Location = new System.Drawing.Point(75, 75);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(110, 40);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start Alarm";
            this.btnStart.UseVisualStyleBackColor = true;
            // This line connects the button click to our C# method
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            //
            // Form1
            //
            this.AcceptButton = this.btnStart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 150); 
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtTime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alarm App";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}