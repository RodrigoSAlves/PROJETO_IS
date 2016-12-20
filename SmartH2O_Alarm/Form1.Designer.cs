namespace Alarms
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
            this.btnStartAlarm = new System.Windows.Forms.Button();
            this.btnStopAlarm = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnStartAlarm
            // 
            this.btnStartAlarm.Location = new System.Drawing.Point(40, 87);
            this.btnStartAlarm.Name = "btnStartAlarm";
            this.btnStartAlarm.Size = new System.Drawing.Size(75, 23);
            this.btnStartAlarm.TabIndex = 0;
            this.btnStartAlarm.Text = "Start Alarm";
            this.btnStartAlarm.UseVisualStyleBackColor = true;
            this.btnStartAlarm.Click += new System.EventHandler(this.btnStartAlarm_Click);
            // 
            // btnStopAlarm
            // 
            this.btnStopAlarm.Location = new System.Drawing.Point(161, 87);
            this.btnStopAlarm.Name = "btnStopAlarm";
            this.btnStopAlarm.Size = new System.Drawing.Size(75, 23);
            this.btnStopAlarm.TabIndex = 1;
            this.btnStopAlarm.Text = "Stop Alarm";
            this.btnStopAlarm.UseVisualStyleBackColor = true;
            this.btnStopAlarm.Click += new System.EventHandler(this.btnStopAlarm_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Init and Stop Alarms";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(14, 116);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(266, 121);
            this.textBox1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStopAlarm);
            this.Controls.Add(this.btnStartAlarm);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartAlarm;
        private System.Windows.Forms.Button btnStopAlarm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
    }
}

