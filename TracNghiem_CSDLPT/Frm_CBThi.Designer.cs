namespace TracNghiem_CSDLPT
{
    partial class Frm_CBThi
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_StudentCode = new System.Windows.Forms.Label();
            this.lbl_StudentName = new System.Windows.Forms.Label();
            this.lbl_ClassName = new System.Windows.Forms.Label();
            this.lbl_ClassCode = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.grb_InfoFind = new System.Windows.Forms.GroupBox();
            this.cmb_Course = new System.Windows.Forms.ComboBox();
            this.bs_MonHoc = new System.Windows.Forms.BindingSource(this.components);
            this.dS = new TracNghiem_CSDLPT.DS();
            this.btn_Find = new System.Windows.Forms.Button();
            this.dtp_DateExam = new System.Windows.Forms.DateTimePicker();
            this.nud_TimesStep = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mONHOCTableAdapter = new TracNghiem_CSDLPT.DSTableAdapters.MONHOCTableAdapter();
            this.grb_StartExam = new System.Windows.Forms.GroupBox();
            this.btn_Start = new System.Windows.Forms.Button();
            this.dgv_Results = new System.Windows.Forms.DataGridView();
            this.bs_GVDK = new System.Windows.Forms.BindingSource(this.components);
            this.gIAOVIEN_DANGKYTableAdapter = new TracNghiem_CSDLPT.DSTableAdapters.GIAOVIEN_DANGKYTableAdapter();
            this.txtMH = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.grb_InfoFind.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bs_MonHoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_TimesStep)).BeginInit();
            this.grb_StartExam.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Results)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_GVDK)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_StudentCode);
            this.groupBox1.Controls.Add(this.lbl_StudentName);
            this.groupBox1.Controls.Add(this.lbl_ClassName);
            this.groupBox1.Controls.Add(this.lbl_ClassCode);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(944, 116);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin sinh viên";
            // 
            // lbl_StudentCode
            // 
            this.lbl_StudentCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_StudentCode.AutoSize = true;
            this.lbl_StudentCode.Location = new System.Drawing.Point(557, 76);
            this.lbl_StudentCode.Name = "lbl_StudentCode";
            this.lbl_StudentCode.Size = new System.Drawing.Size(72, 17);
            this.lbl_StudentCode.TabIndex = 10;
            this.lbl_StudentCode.Text = "................";
            // 
            // lbl_StudentName
            // 
            this.lbl_StudentName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_StudentName.AutoSize = true;
            this.lbl_StudentName.Location = new System.Drawing.Point(557, 31);
            this.lbl_StudentName.Name = "lbl_StudentName";
            this.lbl_StudentName.Size = new System.Drawing.Size(68, 17);
            this.lbl_StudentName.TabIndex = 9;
            this.lbl_StudentName.Text = "...............";
            // 
            // lbl_ClassName
            // 
            this.lbl_ClassName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_ClassName.AutoSize = true;
            this.lbl_ClassName.Location = new System.Drawing.Point(167, 76);
            this.lbl_ClassName.Name = "lbl_ClassName";
            this.lbl_ClassName.Size = new System.Drawing.Size(56, 17);
            this.lbl_ClassName.TabIndex = 8;
            this.lbl_ClassName.Text = "............";
            // 
            // lbl_ClassCode
            // 
            this.lbl_ClassCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_ClassCode.AutoSize = true;
            this.lbl_ClassCode.Location = new System.Drawing.Point(167, 31);
            this.lbl_ClassCode.Name = "lbl_ClassCode";
            this.lbl_ClassCode.Size = new System.Drawing.Size(48, 17);
            this.lbl_ClassCode.TabIndex = 7;
            this.lbl_ClassCode.Text = "..........";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(485, 76);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 17);
            this.label10.TabIndex = 6;
            this.label10.Text = "Ma SV:";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(485, 31);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 17);
            this.label11.TabIndex = 5;
            this.label11.Text = "Tên SV:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(81, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Tên lớp:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(84, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Mã lớp:";
            // 
            // grb_InfoFind
            // 
            this.grb_InfoFind.Controls.Add(this.txtMH);
            this.grb_InfoFind.Controls.Add(this.cmb_Course);
            this.grb_InfoFind.Controls.Add(this.btn_Find);
            this.grb_InfoFind.Controls.Add(this.dtp_DateExam);
            this.grb_InfoFind.Controls.Add(this.nud_TimesStep);
            this.grb_InfoFind.Controls.Add(this.label3);
            this.grb_InfoFind.Controls.Add(this.label2);
            this.grb_InfoFind.Controls.Add(this.label1);
            this.grb_InfoFind.Dock = System.Windows.Forms.DockStyle.Top;
            this.grb_InfoFind.Location = new System.Drawing.Point(0, 116);
            this.grb_InfoFind.Name = "grb_InfoFind";
            this.grb_InfoFind.Size = new System.Drawing.Size(944, 122);
            this.grb_InfoFind.TabIndex = 2;
            this.grb_InfoFind.TabStop = false;
            this.grb_InfoFind.Text = "Thông tin thi";
            // 
            // cmb_Course
            // 
            this.cmb_Course.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_Course.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_Course.DataSource = this.bs_MonHoc;
            this.cmb_Course.DisplayMember = "TENMH";
            this.cmb_Course.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Course.FormattingEnabled = true;
            this.cmb_Course.Location = new System.Drawing.Point(139, 24);
            this.cmb_Course.Name = "cmb_Course";
            this.cmb_Course.Size = new System.Drawing.Size(238, 25);
            this.cmb_Course.TabIndex = 7;
            this.cmb_Course.ValueMember = "MAMH";
            this.cmb_Course.SelectedIndexChanged += new System.EventHandler(this.cmb_Course_SelectedIndexChanged);
            // 
            // bs_MonHoc
            // 
            this.bs_MonHoc.DataMember = "MONHOC";
            this.bs_MonHoc.DataSource = this.dS;
            // 
            // dS
            // 
            this.dS.DataSetName = "DS";
            this.dS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btn_Find
            // 
            this.btn_Find.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Find.Location = new System.Drawing.Point(368, 82);
            this.btn_Find.Name = "btn_Find";
            this.btn_Find.Size = new System.Drawing.Size(208, 34);
            this.btn_Find.TabIndex = 2;
            this.btn_Find.Text = "Tìm";
            this.btn_Find.UseVisualStyleBackColor = true;
            this.btn_Find.Click += new System.EventHandler(this.btn_Find_Click);
            // 
            // dtp_DateExam
            // 
            this.dtp_DateExam.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_DateExam.Location = new System.Drawing.Point(506, 24);
            this.dtp_DateExam.Name = "dtp_DateExam";
            this.dtp_DateExam.Size = new System.Drawing.Size(142, 25);
            this.dtp_DateExam.TabIndex = 6;
            this.dtp_DateExam.Value = new System.DateTime(2020, 11, 22, 12, 15, 37, 0);
            // 
            // nud_TimesStep
            // 
            this.nud_TimesStep.InterceptArrowKeys = false;
            this.nud_TimesStep.Location = new System.Drawing.Point(807, 24);
            this.nud_TimesStep.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nud_TimesStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_TimesStep.Name = "nud_TimesStep";
            this.nud_TimesStep.Size = new System.Drawing.Size(45, 25);
            this.nud_TimesStep.TabIndex = 5;
            this.nud_TimesStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(706, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Lần thi:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(407, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ngày thi:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Môn thi:";
            // 
            // mONHOCTableAdapter
            // 
            this.mONHOCTableAdapter.ClearBeforeFill = true;
            // 
            // grb_StartExam
            // 
            this.grb_StartExam.Controls.Add(this.btn_Start);
            this.grb_StartExam.Controls.Add(this.dgv_Results);
            this.grb_StartExam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grb_StartExam.Location = new System.Drawing.Point(0, 238);
            this.grb_StartExam.Name = "grb_StartExam";
            this.grb_StartExam.Size = new System.Drawing.Size(944, 210);
            this.grb_StartExam.TabIndex = 3;
            this.grb_StartExam.TabStop = false;
            // 
            // btn_Start
            // 
            this.btn_Start.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Start.Location = new System.Drawing.Point(352, 142);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(257, 62);
            this.btn_Start.TabIndex = 1;
            this.btn_Start.Text = "Bắt Đầu Thi";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // dgv_Results
            // 
            this.dgv_Results.AllowUserToAddRows = false;
            this.dgv_Results.AllowUserToDeleteRows = false;
            this.dgv_Results.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_Results.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Results.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgv_Results.Location = new System.Drawing.Point(3, 21);
            this.dgv_Results.Name = "dgv_Results";
            this.dgv_Results.ReadOnly = true;
            this.dgv_Results.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Results.Size = new System.Drawing.Size(938, 106);
            this.dgv_Results.TabIndex = 0;
            // 
            // bs_GVDK
            // 
            this.bs_GVDK.DataMember = "GIAOVIEN_DANGKY";
            this.bs_GVDK.DataSource = this.dS;
            // 
            // gIAOVIEN_DANGKYTableAdapter
            // 
            this.gIAOVIEN_DANGKYTableAdapter.ClearBeforeFill = true;
            // 
            // txtMH
            // 
            this.txtMH.Location = new System.Drawing.Point(139, 24);
            this.txtMH.Name = "txtMH";
            this.txtMH.Size = new System.Drawing.Size(209, 25);
            this.txtMH.TabIndex = 8;
            // 
            // Frm_CBThi
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 448);
            this.Controls.Add(this.grb_StartExam);
            this.Controls.Add(this.grb_InfoFind);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Frm_CBThi";
            this.Text = "Frm_CBThi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_CBThi_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grb_InfoFind.ResumeLayout(false);
            this.grb_InfoFind.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bs_MonHoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_TimesStep)).EndInit();
            this.grb_StartExam.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Results)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_GVDK)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_StudentCode;
        private System.Windows.Forms.Label lbl_StudentName;
        private System.Windows.Forms.Label lbl_ClassName;
        private System.Windows.Forms.Label lbl_ClassCode;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox grb_InfoFind;
        private System.Windows.Forms.ComboBox cmb_Course;
        private System.Windows.Forms.Button btn_Find;
        private System.Windows.Forms.DateTimePicker dtp_DateExam;
        private System.Windows.Forms.NumericUpDown nud_TimesStep;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DS dS;
        private System.Windows.Forms.BindingSource bs_MonHoc;
        private DSTableAdapters.MONHOCTableAdapter mONHOCTableAdapter;
        private System.Windows.Forms.GroupBox grb_StartExam;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.DataGridView dgv_Results;
        private System.Windows.Forms.BindingSource bs_GVDK;
        private DSTableAdapters.GIAOVIEN_DANGKYTableAdapter gIAOVIEN_DANGKYTableAdapter;
        private System.Windows.Forms.TextBox txtMH;
    }
}