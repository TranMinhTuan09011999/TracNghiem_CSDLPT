namespace TracNghiem_CSDLPT
{
    partial class LoginSV
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
            System.Windows.Forms.Label tENCOSOLabel;
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_Brand_GV = new System.Windows.Forms.ComboBox();
            this.bds_ListBrand = new System.Windows.Forms.BindingSource(this.components);
            this.tN_CSDLPTDataSet = new TracNghiem_CSDLPT.TN_CSDLPTDataSet();
            this.txt_MASV = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_Login_GV = new System.Windows.Forms.Button();
            this.v_DS_PHANMANHTableAdapter = new TracNghiem_CSDLPT.TN_CSDLPTDataSetTableAdapters.V_DS_PHANMANHTableAdapter();
            tENCOSOLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bds_ListBrand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tN_CSDLPTDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // tENCOSOLabel
            // 
            tENCOSOLabel.AutoSize = true;
            tENCOSOLabel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tENCOSOLabel.Location = new System.Drawing.Point(3, 0);
            tENCOSOLabel.Name = "tENCOSOLabel";
            tENCOSOLabel.Size = new System.Drawing.Size(109, 22);
            tENCOSOLabel.TabIndex = 0;
            tENCOSOLabel.Text = "Tên Cơ Sở: ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btn_Login_GV);
            this.groupBox1.Location = new System.Drawing.Point(482, 102);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(563, 253);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.36281F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.63719F));
            this.tableLayoutPanel1.Controls.Add(tENCOSOLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmb_Brand_GV, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txt_MASV, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(22, 35);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(517, 100);
            this.tableLayoutPanel1.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 22);
            this.label3.TabIndex = 14;
            this.label3.Text = "Mã Sinh Viên:";
            // 
            // cmb_Brand_GV
            // 
            this.cmb_Brand_GV.DataSource = this.bds_ListBrand;
            this.cmb_Brand_GV.DisplayMember = "TENCOSO";
            this.cmb_Brand_GV.FormattingEnabled = true;
            this.cmb_Brand_GV.Location = new System.Drawing.Point(211, 3);
            this.cmb_Brand_GV.Name = "cmb_Brand_GV";
            this.cmb_Brand_GV.Size = new System.Drawing.Size(294, 27);
            this.cmb_Brand_GV.TabIndex = 1;
            this.cmb_Brand_GV.ValueMember = "TENSERVER";
            this.cmb_Brand_GV.SelectedIndexChanged += new System.EventHandler(this.cmb_Brand_GV_SelectedIndexChanged);
            // 
            // bds_ListBrand
            // 
            this.bds_ListBrand.DataMember = "V_DS_PHANMANH";
            this.bds_ListBrand.DataSource = this.tN_CSDLPTDataSet;
            // 
            // tN_CSDLPTDataSet
            // 
            this.tN_CSDLPTDataSet.DataSetName = "TN_CSDLPTDataSet";
            this.tN_CSDLPTDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // txt_MASV
            // 
            this.txt_MASV.BackColor = System.Drawing.Color.White;
            this.txt_MASV.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_MASV.Location = new System.Drawing.Point(211, 53);
            this.txt_MASV.Name = "txt_MASV";
            this.txt_MASV.Size = new System.Drawing.Size(202, 26);
            this.txt_MASV.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(330, 175);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 36);
            this.button1.TabIndex = 16;
            this.button1.Text = "THOÁT";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btn_Login_GV
            // 
            this.btn_Login_GV.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Login_GV.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Login_GV.Location = new System.Drawing.Point(81, 175);
            this.btn_Login_GV.Name = "btn_Login_GV";
            this.btn_Login_GV.Size = new System.Drawing.Size(139, 36);
            this.btn_Login_GV.TabIndex = 15;
            this.btn_Login_GV.Text = "Đăng Nhập";
            this.btn_Login_GV.UseVisualStyleBackColor = true;
            this.btn_Login_GV.Click += new System.EventHandler(this.btn_Login_GV_Click);
            // 
            // v_DS_PHANMANHTableAdapter
            // 
            this.v_DS_PHANMANHTableAdapter.ClearBeforeFill = true;
            // 
            // LoginSV
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 463);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LoginSV";
            this.Text = "Đăng Nhập Sinh Viên";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LoginSV_Load);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bds_ListBrand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tN_CSDLPTDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_Login_GV;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_MASV;
        private System.Windows.Forms.ComboBox cmb_Brand_GV;
        private TN_CSDLPTDataSet tN_CSDLPTDataSet;
        private System.Windows.Forms.BindingSource bds_ListBrand;
        private TN_CSDLPTDataSetTableAdapters.V_DS_PHANMANHTableAdapter v_DS_PHANMANHTableAdapter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}