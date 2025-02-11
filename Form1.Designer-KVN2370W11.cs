namespace winform
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
            this.components = new System.ComponentModel.Container();
            this.TabCtrl = new System.Windows.Forms.TabControl();
            this.GTtab = new System.Windows.Forms.TabPage();
            this.ServerUrl_lbl = new System.Windows.Forms.Label();
            this.lineChart1 = new MindFusion.Charting.WinForms.LineChart();
            this.Connect_btn = new System.Windows.Forms.Button();
            this.ReadDta_lbl = new System.Windows.Forms.Label();
            this.ClientUrl_lbl = new System.Windows.Forms.Label();
            this.ReadDta_txt = new System.Windows.Forms.TextBox();
            this.IVtab = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.DataTab = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.database1DataSet = new winform.Database1DataSet();
            this.gTThicknessBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gT_ThicknessTableAdapter = new winform.Database1DataSetTableAdapters.GT_ThicknessTableAdapter();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TabCtrl.SuspendLayout();
            this.GTtab.SuspendLayout();
            this.IVtab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.DataTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.database1DataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gTThicknessBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // TabCtrl
            // 
            this.TabCtrl.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.TabCtrl.Controls.Add(this.GTtab);
            this.TabCtrl.Controls.Add(this.IVtab);
            this.TabCtrl.Controls.Add(this.DataTab);
            this.TabCtrl.Dock = System.Windows.Forms.DockStyle.Left;
            this.TabCtrl.Location = new System.Drawing.Point(0, 0);
            this.TabCtrl.Multiline = true;
            this.TabCtrl.Name = "TabCtrl";
            this.TabCtrl.SelectedIndex = 0;
            this.TabCtrl.Size = new System.Drawing.Size(736, 503);
            this.TabCtrl.TabIndex = 9;
            // 
            // GTtab
            // 
            this.GTtab.BackColor = System.Drawing.Color.Transparent;
            this.GTtab.Controls.Add(this.ServerUrl_lbl);
            this.GTtab.Controls.Add(this.lineChart1);
            this.GTtab.Controls.Add(this.Connect_btn);
            this.GTtab.Controls.Add(this.ReadDta_lbl);
            this.GTtab.Controls.Add(this.ClientUrl_lbl);
            this.GTtab.Controls.Add(this.ReadDta_txt);
            this.GTtab.Location = new System.Drawing.Point(23, 4);
            this.GTtab.Name = "GTtab";
            this.GTtab.Padding = new System.Windows.Forms.Padding(3);
            this.GTtab.Size = new System.Drawing.Size(709, 495);
            this.GTtab.TabIndex = 0;
            this.GTtab.Text = "Contact Sensor GT";
            // 
            // ServerUrl_lbl
            // 
            this.ServerUrl_lbl.AutoSize = true;
            this.ServerUrl_lbl.Location = new System.Drawing.Point(96, 31);
            this.ServerUrl_lbl.Name = "ServerUrl_lbl";
            this.ServerUrl_lbl.Size = new System.Drawing.Size(35, 13);
            this.ServerUrl_lbl.TabIndex = 14;
            this.ServerUrl_lbl.Text = "label1";
            // 
            // lineChart1
            // 
            this.lineChart1.LegendTitle = "Legend";
            this.lineChart1.Location = new System.Drawing.Point(39, 81);
            this.lineChart1.Name = "lineChart1";
            this.lineChart1.Padding = new System.Windows.Forms.Padding(5);
            this.lineChart1.ShowLegend = true;
            this.lineChart1.Size = new System.Drawing.Size(635, 388);
            this.lineChart1.SubtitleFontName = null;
            this.lineChart1.SubtitleFontSize = null;
            this.lineChart1.SubtitleFontStyle = null;
            this.lineChart1.TabIndex = 13;
            this.lineChart1.Text = "lineChart1";
            this.lineChart1.Theme.UniformSeriesFill = new MindFusion.Drawing.SolidBrush("#FF90EE90");
            this.lineChart1.Theme.UniformSeriesStroke = new MindFusion.Drawing.SolidBrush("#FF000000");
            this.lineChart1.Theme.UniformSeriesStrokeThickness = 2D;
            this.lineChart1.TitleFontName = null;
            this.lineChart1.TitleFontSize = null;
            this.lineChart1.TitleFontStyle = null;
            // 
            // Connect_btn
            // 
            this.Connect_btn.Location = new System.Drawing.Point(404, 26);
            this.Connect_btn.Name = "Connect_btn";
            this.Connect_btn.Size = new System.Drawing.Size(75, 23);
            this.Connect_btn.TabIndex = 12;
            this.Connect_btn.Text = "Connect";
            this.Connect_btn.UseVisualStyleBackColor = true;
            this.Connect_btn.Click += new System.EventHandler(this.Connect_btn_Click_1);
            // 
            // ReadDta_lbl
            // 
            this.ReadDta_lbl.AutoSize = true;
            this.ReadDta_lbl.Location = new System.Drawing.Point(34, 58);
            this.ReadDta_lbl.Name = "ReadDta_lbl";
            this.ReadDta_lbl.Size = new System.Drawing.Size(59, 13);
            this.ReadDta_lbl.TabIndex = 11;
            this.ReadDta_lbl.Text = "ReadData:";
            this.ReadDta_lbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ClientUrl_lbl
            // 
            this.ClientUrl_lbl.AutoSize = true;
            this.ClientUrl_lbl.Location = new System.Drawing.Point(36, 31);
            this.ClientUrl_lbl.Name = "ClientUrl_lbl";
            this.ClientUrl_lbl.Size = new System.Drawing.Size(54, 13);
            this.ClientUrl_lbl.TabIndex = 10;
            this.ClientUrl_lbl.Text = "ServerUrl:";
            // 
            // ReadDta_txt
            // 
            this.ReadDta_txt.Location = new System.Drawing.Point(96, 55);
            this.ReadDta_txt.Multiline = true;
            this.ReadDta_txt.Name = "ReadDta_txt";
            this.ReadDta_txt.Size = new System.Drawing.Size(300, 20);
            this.ReadDta_txt.TabIndex = 9;
            // 
            // IVtab
            // 
            this.IVtab.Controls.Add(this.pictureBox1);
            this.IVtab.Location = new System.Drawing.Point(23, 4);
            this.IVtab.Name = "IVtab";
            this.IVtab.Padding = new System.Windows.Forms.Padding(3);
            this.IVtab.Size = new System.Drawing.Size(709, 495);
            this.IVtab.TabIndex = 1;
            this.IVtab.Text = "Vision Sensor IV";
            this.IVtab.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.ErrorImage = global::winform.Properties.Resources.loading1;
            this.pictureBox1.Image = global::winform.Properties.Resources._00000;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(703, 489);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.BackgroundImageChanged += new System.EventHandler(this.pictureBox1_BackgroundImageChanged);
            // 
            // DataTab
            // 
            this.DataTab.Controls.Add(this.dataGridView1);
            this.DataTab.Location = new System.Drawing.Point(23, 4);
            this.DataTab.Name = "DataTab";
            this.DataTab.Padding = new System.Windows.Forms.Padding(3);
            this.DataTab.Size = new System.Drawing.Size(709, 495);
            this.DataTab.TabIndex = 2;
            this.DataTab.Text = "Data";
            this.DataTab.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dateTimeDataGridViewTextBoxColumn,
            this.dataGridViewTextBoxColumn2});
            this.dataGridView1.DataSource = this.gTThicknessBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(703, 489);
            this.dataGridView1.TabIndex = 0;
            // 
            // database1DataSet
            // 
            this.database1DataSet.DataSetName = "Database1DataSet";
            this.database1DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gTThicknessBindingSource
            // 
            this.gTThicknessBindingSource.DataMember = "GT_Thickness";
            this.gTThicknessBindingSource.DataSource = this.database1DataSet;
            // 
            // gT_ThicknessTableAdapter
            // 
            this.gT_ThicknessTableAdapter.ClearBeforeFill = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dateTimeDataGridViewTextBoxColumn
            // 
            this.dateTimeDataGridViewTextBoxColumn.DataPropertyName = "DateTime";
            this.dateTimeDataGridViewTextBoxColumn.HeaderText = "DateTime";
            this.dateTimeDataGridViewTextBoxColumn.Name = "dateTimeDataGridViewTextBoxColumn";
            this.dateTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Thickness";
            this.dataGridViewTextBoxColumn2.HeaderText = "Thickness";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 503);
            this.Controls.Add(this.TabCtrl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.TabCtrl.ResumeLayout(false);
            this.GTtab.ResumeLayout(false);
            this.GTtab.PerformLayout();
            this.IVtab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.DataTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.database1DataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gTThicknessBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabCtrl;
        private System.Windows.Forms.TabPage GTtab;
        private System.Windows.Forms.Label ServerUrl_lbl;
        private MindFusion.Charting.WinForms.LineChart lineChart1;
        private System.Windows.Forms.Button Connect_btn;
        private System.Windows.Forms.Label ReadDta_lbl;
        private System.Windows.Forms.Label ClientUrl_lbl;
        private System.Windows.Forms.TextBox ReadDta_txt;
        private System.Windows.Forms.TabPage IVtab;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabPage DataTab;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn thicknessDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn dateTimeDataGridViewImageColumn;
        private Database1DataSet database1DataSet;
        private System.Windows.Forms.BindingSource gTThicknessBindingSource;
        private Database1DataSetTableAdapters.GT_ThicknessTableAdapter gT_ThicknessTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}

