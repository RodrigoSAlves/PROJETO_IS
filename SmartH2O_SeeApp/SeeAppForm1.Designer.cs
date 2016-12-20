namespace SmartH2O_SeeApp
{
    partial class SeeAppForm1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxPH = new System.Windows.Forms.CheckBox();
            this.checkBoxNH3 = new System.Windows.Forms.CheckBox();
            this.checkBoxCI2 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.buttonAlarmsBetweenDates = new System.Windows.Forms.Button();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnDailyStats = new System.Windows.Forms.Button();
            this.btnWeeklyStats = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(378, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "SmartH2O";
            // 
            // checkBoxPH
            // 
            this.checkBoxPH.AutoSize = true;
            this.checkBoxPH.Location = new System.Drawing.Point(33, 96);
            this.checkBoxPH.Name = "checkBoxPH";
            this.checkBoxPH.Size = new System.Drawing.Size(41, 17);
            this.checkBoxPH.TabIndex = 1;
            this.checkBoxPH.Text = "PH";
            this.checkBoxPH.UseVisualStyleBackColor = true;
            this.checkBoxPH.CheckedChanged += new System.EventHandler(this.checkBoxPH_CheckedChanged);
            // 
            // checkBoxNH3
            // 
            this.checkBoxNH3.AutoSize = true;
            this.checkBoxNH3.Location = new System.Drawing.Point(33, 120);
            this.checkBoxNH3.Name = "checkBoxNH3";
            this.checkBoxNH3.Size = new System.Drawing.Size(48, 17);
            this.checkBoxNH3.TabIndex = 2;
            this.checkBoxNH3.Text = "NH3";
            this.checkBoxNH3.UseVisualStyleBackColor = true;
            this.checkBoxNH3.CheckedChanged += new System.EventHandler(this.checkBoxNH3_CheckedChanged);
            // 
            // checkBoxCI2
            // 
            this.checkBoxCI2.AutoSize = true;
            this.checkBoxCI2.Location = new System.Drawing.Point(33, 144);
            this.checkBoxCI2.Name = "checkBoxCI2";
            this.checkBoxCI2.Size = new System.Drawing.Size(42, 17);
            this.checkBoxCI2.TabIndex = 3;
            this.checkBoxCI2.Text = "CI2";
            this.checkBoxCI2.UseVisualStyleBackColor = true;
            this.checkBoxCI2.CheckedChanged += new System.EventHandler(this.checkBoxCI2_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(110, 89);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(292, 104);
            this.textBox1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Parameters:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(107, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Information:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(408, 89);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 7;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(409, 116);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 8;
            // 
            // buttonAlarmsBetweenDates
            // 
            this.buttonAlarmsBetweenDates.Location = new System.Drawing.Point(439, 154);
            this.buttonAlarmsBetweenDates.Name = "buttonAlarmsBetweenDates";
            this.buttonAlarmsBetweenDates.Size = new System.Drawing.Size(135, 23);
            this.buttonAlarmsBetweenDates.TabIndex = 9;
            this.buttonAlarmsBetweenDates.Text = "See Alarms between";
            this.buttonAlarmsBetweenDates.UseVisualStyleBackColor = true;
            this.buttonAlarmsBetweenDates.Click += new System.EventHandler(this.buttonAlarmsBetweenDates_Click);
            // 
            // chart
            // 
            chartArea5.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.chart.Legends.Add(legend5);
            this.chart.Location = new System.Drawing.Point(12, 216);
            this.chart.Name = "chart";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.chart.Series.Add(series5);
            this.chart.Size = new System.Drawing.Size(300, 147);
            this.chart.TabIndex = 10;
            this.chart.Text = "chart1";
            // 
            // chart1
            // 
            chartArea6.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chart1.Legends.Add(legend6);
            this.chart1.Location = new System.Drawing.Point(458, 216);
            this.chart1.Name = "chart1";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.chart1.Series.Add(series6);
            this.chart1.Size = new System.Drawing.Size(300, 157);
            this.chart1.TabIndex = 11;
            this.chart1.Text = "chart1";
            // 
            // btnDailyStats
            // 
            this.btnDailyStats.Location = new System.Drawing.Point(66, 385);
            this.btnDailyStats.Name = "btnDailyStats";
            this.btnDailyStats.Size = new System.Drawing.Size(142, 23);
            this.btnDailyStats.TabIndex = 12;
            this.btnDailyStats.Text = "Daily Statistics";
            this.btnDailyStats.UseVisualStyleBackColor = true;
            this.btnDailyStats.Click += new System.EventHandler(this.btnDailyStats_Click);
            // 
            // btnWeeklyStats
            // 
            this.btnWeeklyStats.Location = new System.Drawing.Point(538, 385);
            this.btnWeeklyStats.Name = "btnWeeklyStats";
            this.btnWeeklyStats.Size = new System.Drawing.Size(123, 23);
            this.btnWeeklyStats.TabIndex = 13;
            this.btnWeeklyStats.Text = "Weekly Statistics\t";
            this.btnWeeklyStats.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 420);
            this.Controls.Add(this.btnWeeklyStats);
            this.Controls.Add(this.btnDailyStats);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.buttonAlarmsBetweenDates);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.checkBoxCI2);
            this.Controls.Add(this.checkBoxNH3);
            this.Controls.Add(this.checkBoxPH);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxPH;
        private System.Windows.Forms.CheckBox checkBoxNH3;
        private System.Windows.Forms.CheckBox checkBoxCI2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Button buttonAlarmsBetweenDates;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button btnDailyStats;
        private System.Windows.Forms.Button btnWeeklyStats;
    }
}

