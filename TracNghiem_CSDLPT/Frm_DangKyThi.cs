using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;
using TracNghiem_CSDLPT.Common;
using TracNghiem_CSDLPT.Share;

namespace TracNghiem_CSDLPT
{
    public partial class Frm_DangKyThi : DevExpress.XtraEditors.XtraForm
    {


        int ml = 0;
        int mmh = 0;
        int t_do = 0;
        string magv = "";
        string magv1 = "";
        int vitri = 0;
        private string x = "";
        private string y = "";
        private string z = "";
        private int t = 0;

        private string x1 = "";
        private string y1 = "";
        private string z1 = "";

        private string x2 = "";
        private string y2 = "";
        private string z2 = "";

        private CallBackAction _callAction;
        private DataTable table1 = new DataTable();
        private DataColumn dc;
        private DataColumn dc1;
        private DataColumn dc2;
        private DataColumn dc3;
        private DataColumn dc4;
        private DataColumn dc5;
        private DataColumn dc6;
        private DataColumn dc7;
        private int index1 = -1;
        private String[] act = new string[100];
        private int act_index = -1;

        public Frm_DangKyThi()
        {
            InitializeComponent();
            _callAction = new CallBackAction();
            dc = new DataColumn("Mã giáo viên");
            dc1 = new DataColumn("Mã môn học");
            dc2 = new DataColumn("Mã lớp");
            dc3 = new DataColumn("Trình độ");
            dc4 = new DataColumn("Ngày thi");
            dc5 = new DataColumn("Lần");
            dc6 = new DataColumn("Số câu thi");
            dc7 = new DataColumn("Thời gian");
            table1.Columns.Add(dc);
            table1.Columns.Add(dc1);
            table1.Columns.Add(dc2);
            table1.Columns.Add(dc3);
            table1.Columns.Add(dc4);
            table1.Columns.Add(dc5);
            table1.Columns.Add(dc6);
            table1.Columns.Add(dc7);

            DeniceFeatureByRole();
            SetUp();
            JustNumber(txtThoiGian);
            JustNumber(txtSoCauThi);
        }
        private void DeniceFeatureByRole()
        {

            if (Program.mGroup.Equals("TRUONG"))
            {
                btn_Add.Enabled = btn_Delete.Enabled = btn_Edit.Enabled = btn_Thoat.Enabled = btn_Ghi.Enabled = btn_Reset.Enabled = false;
            }
        }

        private void gIAOVIEN_DANGKYBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bs_GVDK.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dS);
        }

        private void Frm_DangKyThi_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dS.LOP' table. You can move, or remove it, as needed.
            this.dS.EnforceConstraints = false;
            this.lOPTableAdapter.Connection.ConnectionString = Program.connstr;
            this.lOPTableAdapter.Fill(this.dS.LOP);
            // TODO: This line of code loads data into the 'dS.MONHOC' table. You can move, or remove it, as needed.
            this.dS.EnforceConstraints = false;
            this.mONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
            this.mONHOCTableAdapter.Fill(this.dS.MONHOC);
            // TODO: This line of code loads data into the 'dS.GIAOVIEN' table. You can move, or remove it, as needed.
            this.dS.EnforceConstraints = false;
            this.gIAOVIENTableAdapter.Connection.ConnectionString = Program.connstr;
            this.gIAOVIENTableAdapter.Fill(this.dS.GIAOVIEN);
            // TODO: This line of code loads data into the 'dS.GIAOVIEN_DANGKY' table. You can move, or remove it, as needed.
            this.dS.EnforceConstraints = false;
            this.gIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
            this.gIAOVIEN_DANGKYTableAdapter.Fill(this.dS.GIAOVIEN_DANGKY);

            this.cmbCoSo.DataSource = Program.bds_ListBrand;
            this.cmbCoSo.DisplayMember = "TENCOSO";
            this.cmbCoSo.ValueMember = "TENSERVER";
            this.cmbCoSo.SelectedIndex = Program.mChinhanh;

            magv = Program.username;

            if (Program.mGroup == "TRUONG")
            {
                this.cmbCoSo.Enabled = true;
            }
            else
            {
                this.cmbCoSo.Enabled = false;
            }

            if (bs_GVDK.Count == 0)
            {
                btn_Delete.Enabled = false;
            }

            set();
        }

       

        public void set()
        {
            if(bs_GVDK.Count == 0)
            {
                btn_Add.Enabled = true;
                btn_Edit.Enabled = btn_Ghi.Enabled = btn_Delete.Enabled = btn_Reload.Enabled = btn_Thoat.Enabled = btn_Reset.Enabled = false;
                this.groupBox1.Enabled = false;
            }
            else
            {
                btn_Ghi.Enabled = false;
                this.btn_Reset.Enabled = true;
                this.groupBox1.Enabled = false;
                this.txtMH2.Visible = this.txtMaLop1.Visible = this.txtTrinhDo1.Visible = true;
                gcGVDK.Enabled = true;
                /*DataRowView current1 = (DataRowView)bs_GVDK.Current

                /*String maGV = current1["MAGV"].ToString();
                this.bs_GVDK.Position = FindPointionCurrentTeacher(maGV);*/

                DataRowView current = (DataRowView)bs_GVDK.Current;

                String x = current["MAMH"].ToString();
                this.txtMaMH.Text = x;
                String nameMH = SqlRequestFunction.GetTenMH(x);
                this.txtMH2.Text = nameMH;

                String y = current["MALOP"].ToString();
                String nameLOP = SqlRequestFunction.GetTenLop(y);
                this.txtMaLop1.Text = nameLOP;

                String z = current["TRINHDO"].ToString();
                String TrinhDo = GetLevelName(z);
                this.txtTrinhDo1.Text = TrinhDo;


                DateTime currentDateTime = DateTime.Now;
                this.txtNgayThi.MinDate = currentDateTime.AddDays(-1000);
                this.txtNgayThi.MaxDate = currentDateTime.AddDays(60);

                String t = current["NGAYTHI"].ToString();
                this.txtNgayThi.Text = t;
            }        
        }

        private int FindPointionCurrentTeacher(String teacherCode)
        {
            DataView dt = (DataView)bs_GVDK.List;
            dt.Sort = "MAGV";
            int index = dt.Find(teacherCode);
            return index;
        }    

        public String GetLevelName(String level)
        {
            switch (level)
            {
                case "A":
                    return "Đại Học";
                case "B":
                    return "Cao Đẳng";
                case "C":
                    return "Trung Cấp";
                default:
                    return "";
            }
        }

        private void cmbCoSo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCoSo.SelectedValue.ToString() == "System.Data.DataRowView")
            {
                return;
            }

            Program.servername = cmbCoSo.SelectedValue.ToString();

            if (cmbCoSo.SelectedIndex != Program.mChinhanh)
            {
                Program.mlogin = Program.remotelogin;
                Program.password = Program.remotepassword;
            }
            else
            {
                Program.mlogin = Program.mloginDN;
                Program.password = Program.passwordDN;
            }

            if (Program.KetNoi() == 0)
            {
                MessageBox.Show("Lỗi kết nối về chi nhánh mới", "", MessageBoxButtons.OK);
            }
            else
            {
                // TODO: This line of code loads data into the 'dS.LOP' table. You can move, or remove it, as needed.
                this.dS.EnforceConstraints = false;
                this.lOPTableAdapter.Connection.ConnectionString = Program.connstr;
                this.lOPTableAdapter.Fill(this.dS.LOP);
                // TODO: This line of code loads data into the 'dS.MONHOC' table. You can move, or remove it, as needed.
                this.dS.EnforceConstraints = false;
                this.mONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
                this.mONHOCTableAdapter.Fill(this.dS.MONHOC);
                // TODO: This line of code loads data into the 'dS.GIAOVIEN' table. You can move, or remove it, as needed.
                this.dS.EnforceConstraints = false;
                this.gIAOVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                this.gIAOVIENTableAdapter.Fill(this.dS.GIAOVIEN);

                this.dS.EnforceConstraints = false;
                this.gIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
                this.gIAOVIEN_DANGKYTableAdapter.Fill(this.dS.GIAOVIEN_DANGKY);
            }
        }

        public void SetUp()
        {
            DataTable tableLevel = new DataTable("Level");
            tableLevel.Columns.Add("Symbol");
            tableLevel.Columns.Add("Name");

            DataRow dataRow = tableLevel.NewRow();
            dataRow.ItemArray = new object[] { "A", "Đại Học" };
            tableLevel.Rows.Add(dataRow);

            dataRow = tableLevel.NewRow();
            dataRow.ItemArray = new object[] { "B", "Cao Đẳng" };
            tableLevel.Rows.Add(dataRow);

            dataRow = tableLevel.NewRow();
            dataRow.ItemArray = new object[] { "C", "Trung Cấp" };
            tableLevel.Rows.Add(dataRow);

            txtTrinhDo.DataSource = tableLevel;
            txtTrinhDo.ValueMember = "Symbol";
            txtTrinhDo.DisplayMember = "Name";

            magv = Program.username;

            //this.bs_GiaoVien.Position = FindPointionCurrentTeacher(Program.username);

            /*this.txtNgayThi.MinDate = DateTime.Now;
            DateTime currentDateTime = DateTime.Now;
            this.txtNgayThi.MaxDate = currentDateTime.AddDays(60);*/
        }

        public void ShowAll(BindingSource src)
        {
            DataView dt = (DataView)src.List;
            DataTable table = dt.Table;

            Debug.WriteLine("Start show table content.");
            foreach (DataRow row in table.Rows)
            {
                string ID = row["MAGV"].ToString();
                string Name = row["TEN"].ToString();
                string FamilyName = row["MAKH"].ToString();

                Debug.WriteLine("{MAGV=" + ID + ", TEN=" + Name + ", MAKH=" + FamilyName + "}");
            }
        }

        public void JustNumber(TextBox textBox)
        {
            textBox.KeyPress += (sender, e) =>
            {
                if (e.KeyChar < '0' || e.KeyChar > '9')
                    e.Handled = true;
            };
        }

        private bool ValidateFrm()
        {
            if (!ValidateEmpty())
            {
                return false;
            }
            else
            {
                if (ValidateValue())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool ValidateEmpty()
        {
            bool validate = true;
            if (mAGVTextBox.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Vui lòng nhập mã giáo viên!!!", "", MessageBoxButtons.OK);
                this.ActiveControl = this.mAGVTextBox;
                validate = false;
            }
            if (txtMaMH.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Vui lòng chọn môn thi!!!", "", MessageBoxButtons.OK);
                this.ActiveControl = this.txtMaMH;
                validate = false;
            }
            if (txtMaLop.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Vui lòng nhập lớp thi!!!", "", MessageBoxButtons.OK);
                this.ActiveControl = this.txtMaLop;
                validate = false;
            }
            if (txtTrinhDo.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Vui lòng chọn trình độ!!!", "", MessageBoxButtons.OK);
                this.ActiveControl = this.txtTrinhDo;
                validate = false;
            }
            if (txtNgayThi.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Vui lòng chọn ngày thi!!!", "", MessageBoxButtons.OK);
                validate = false;
            }
            if (txtLan.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Vui lòng chọn lần thi!!!", "", MessageBoxButtons.OK);
                validate = false;
            }
            if (txtSoCauThi.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Vui lòng nhập số câu thi!!!", "", MessageBoxButtons.OK);
                this.ActiveControl = this.txtSoCauThi;
                validate = false;
            }         
            if (txtThoiGian.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Vui lòng nhập thời gian thi!!!", "", MessageBoxButtons.OK);
                this.ActiveControl = this.txtThoiGian;
                validate = false;
            }
            

            return validate;
        }

        private bool ValidateValue()
        {
            String ngayThi = txtNgayThi.Value.ToString("dd-MM-yyyy");
            this.txtNgayThi.Text = ngayThi;

            bool validate = true;

            if (Convert.ToInt32(txtLan.Text) < 1 || Convert.ToInt32(txtLan.Text) > 2)
            {
                MessageBox.Show("Lần thi không thuộc[1 - 2]", "", MessageBoxButtons.OK);               
                this.ActiveControl = this.txtLan;
                validate = false;
            }

            validate = ValidateTimeExam() ? validate : false;

            validate = ValidateQuantityQues() ? validate : false;

            validate = ValidateCodeRegister(txtMaLop.SelectedValue.ToString(), txtMaMH.SelectedValue.ToString(),
                                            Convert.ToInt32(txtLan.Text)) ? validate : false;

            if (validate)
            {
                validate = ValidateGetQuestion(txtMaMH.SelectedValue.ToString(), txtTrinhDo.SelectedValue.ToString(),
                                               Convert.ToInt32(txtSoCauThi.Text)) ? validate : false;
            }

            return validate;
        }


        private bool ValidateValueEdit()
        {
            String ngayThi = txtNgayThi.Value.ToString("dd-MM-yyyy");
            this.txtNgayThi.Text = ngayThi;

            bool validate = true;

            if (Convert.ToInt32(txtLan.Text) < 1 || Convert.ToInt32(txtLan.Text) > 2)
            {
                MessageBox.Show("Lần thi không thuộc[1 - 2]", "", MessageBoxButtons.OK);
                this.ActiveControl = this.txtLan;
                validate = false;
            }

            validate = ValidateTimeExam() ? validate : false;

            validate = ValidateQuantityQues() ? validate : false;
            validate = ValidateCodeRegister(x1, y1,
                                            Convert.ToInt32(txtLan.Text)) ? validate : false;

            if (validate)
            {
                validate = ValidateGetQuestion(y1, z1,
                                               Convert.ToInt32(txtSoCauThi.Text)) ? validate : false;
            }

            return validate;
        }

        private bool ValidateValueEdit1()
        {
            
            bool validate = true;

            validate = ValidateTimeExam() ? validate : false;

            validate = ValidateQuantityQues() ? validate : false;

            if (validate)
            {
                validate = ValidateGetQuestion(txtMaMH.Text, x1,
                                               Convert.ToInt32(txtSoCauThi.Text)) ? validate : false;
            }
            

            return validate;
        }

        private bool ValidateTimeExam()
        {
            bool validate = true;
            try
            {
                int value = Convert.ToInt32(txtThoiGian.Text);

                if (value < 15 || value > 60)
                {
                    MessageBox.Show("Thời gian thi không thuộc[15 - 60] phút.", "", MessageBoxButtons.OK);
                    this.ActiveControl = this.txtThoiGian;
                    validate = false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Trường dữ liệu không hợp lệ", "", MessageBoxButtons.OK);
                this.ActiveControl = this.txtThoiGian;
                validate = false;
            }
            return validate;
        }

        private bool ValidateQuantityQues()
        {
            bool validate = true;
            try
            {
                int value = Convert.ToInt32(txtSoCauThi.Text);
                MessageBox.Show("a: " + value, "", MessageBoxButtons.OK);
                if (value < 10 || value > 100)
                {
                    MessageBox.Show("Số câu thi không thuộc [10-100] câu", "", MessageBoxButtons.OK);
                    this.ActiveControl = this.txtSoCauThi;
                    validate = false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Trường dữ liệu không hợp lệ", "", MessageBoxButtons.OK);
                this.ActiveControl = this.txtSoCauThi;
                validate = false;
            }
            return validate;
        }

        private bool ValidateCodeRegister(String classCode, String courseCode, int time)
        {
            if (SqlRequestFunction.RegisterIsExists(classCode, courseCode, time))
            {
                MessageBox.Show("Lượt thi của Môn thi này cho lớp đó đã được tổ chức thi hai lần. Và không thể tổ chức thêm.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool ValidateGetQuestion(String courseCode, String level, int quantity)
        {
            bool isEnough = SqlRequestFunction.IsEnoughQuestion(courseCode, level, quantity);

            if (!isEnough)
            {
                MessageBox.Show("Không đủ câu hỏi để tổ chức thi. Vui lòng kiểm tra lại",
                                "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return isEnough;
        }

        private void btnDangKi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.btn_Thoat.Tag = "ADD";
            this.btn_Ghi.Tag = "ADD";
            this.txtMH2.Visible = false;
            this.txtMaLop1.Visible = false;
            this.txtTrinhDo1.Visible = false;
            gcGVDK.Enabled = false;

            SetUpButtonForAction();        
        }

        public void SetUpButtonForAction()
        {
            //this.cmbMAKHOA.Enabled = true;
            this.btn_Add.Enabled = false;
            this.btn_Edit.Enabled = false;
            this.btn_Delete.Enabled = false;
            this.btn_Ghi.Enabled = true;
            this.btn_Reload.Enabled = false;
            this.btn_Reset.Enabled = false;
            this.groupBox1.Enabled = true;
            
            this.txtNgayThi.MinDate = DateTime.Now;
            DateTime currentDateTime = DateTime.Now;
            this.txtNgayThi.MaxDate = currentDateTime.AddDays(60);
            if (this.btn_Ghi.Tag.Equals("ADD"))
            {
                bs_GVDK.AddNew();
            }
            if (Program.mGroup.Equals("GIANGVIEN"))
            {
                mAGVTextBox.Text = magv;
                mAGVTextBox.Enabled = false;
            }
            this.txtNgayThi.CustomFormat = " ";
            this.txtNgayThi.Format = DateTimePickerFormat.Custom;

            this.txtMaLop.Enabled = true;
            this.txtMaMH.Enabled = true;
            this.txtLan.Enabled = true;
            this.txtMaLop1.Enabled = true;
            this.txtMH2.Enabled = true;
            this.txtMH2.Text = "";
            this.txtMaLop1.Text = "";
            this.txtTrinhDo1.Text = "";
            
            
        }

        private void btn_Edit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.btn_Ghi.Tag = "EDIT";
            this.btn_Thoat.Tag = "EDIT";



            this.txtNgayThi.MinDate = DateTime.Now;
            DateTime currentDateTime = DateTime.Now;
            this.txtNgayThi.MaxDate = currentDateTime.AddDays(60);
            gcGVDK.Enabled = false;

            vitri = bs_GVDK.Position;
            

            DataRowView current = (DataRowView)bs_GVDK.Current;

            String maMH = current["MAMH"].ToString();
            String maLOP = current["MALOP"].ToString();
            int lan = Int16.Parse(current["LAN"].ToString());

            String daThi = SqlRequestFunction.GetRegisterExamed(maMH, maLOP, lan);        
            if(daThi.Equals("X"))
            {
                MessageBox.Show("Phần đăng kí này đã thi rồi!!!", "", MessageBoxButtons.OK);
                set();
                return;
            }

            if (Program.mGroup.Equals("GIANGVIEN"))
            {
                magv1 = current["MAGV"].ToString();
                string m = magv1.Trim();
                if (m == magv)
                {
                    this.btn_Add.Enabled = false;
                    this.btn_Edit.Enabled = false;
                    this.btn_Delete.Enabled = false;
                    this.btn_Ghi.Enabled = true;
                    this.btn_Reload.Enabled = false;
                    this.btn_Reset.Enabled = false;
                    this.groupBox1.Enabled = true;

                    this.txtMaMH.Enabled = false;
                    this.txtMaLop.Enabled = false;
                    this.txtLan.Enabled = false;
                    this.txtMaLop1.Enabled = false;
                    this.txtMH2.Enabled = false;

                    y = current["SOCAUTHI"].ToString();
                    z = current["TRINHDO"].ToString();
                    t = Int16.Parse(current["LAN"].ToString());
                    t_do = 0;
                }
                else
                {
                    MessageBox.Show("Bạn không sửa phần đăng kí này!!!", "", MessageBoxButtons.OK);
                }
            }
            else
            {
                this.btn_Add.Enabled = false;
                this.btn_Edit.Enabled = false;
                this.btn_Delete.Enabled = false;
                this.btn_Ghi.Enabled = true;
                this.btn_Reload.Enabled = false;
                this.btn_Reset.Enabled = false;
                this.groupBox1.Enabled = true;

                this.txtMaMH.Enabled = false;
                this.txtMaLop.Enabled = false;
                this.txtLan.Enabled = false;
                this.txtMaLop1.Enabled = false;
                this.txtMH2.Enabled = false;

                y = current["SOCAUTHI"].ToString();
                z = current["TRINHDO"].ToString();
                t = Int16.Parse(current["LAN"].ToString());
                t_do = 0;
            }

            
        }

        public bool Add()
        {
            bool isEmpty = ValidateEmpty();

            if (!isEmpty)
                return false;

            bool isValue = ValidateValue();
            if (!isValue)
                return false;
            else
            {
                object[] data = new object[] { mAGVTextBox.Text, txtMaMH.SelectedValue.ToString(), txtMaLop.SelectedValue.ToString(), txtTrinhDo.SelectedValue.ToString(), txtNgayThi.Text, txtLan.Value, txtSoCauThi.Text, txtThoiGian.Text };
                this._callAction.FillData(Share.Action.RecoveryAdd, SetUpCurrentData(data));
                return true;
            }
        }

        public DataTable SetUpCurrentData(object[] datas)
        {
            DataRow dr = table1.NewRow();
            dr.ItemArray = datas;
            table1.Rows.Add(dr);

            return table1;
        }

        public void WriteToDB()
        {
            bs_GVDK.EndEdit();
            DataRowView current = (DataRowView)bs_GVDK.Current;
            current["MAMH"] = this.txtMaMH.SelectedValue;
            current["MALOP"] = this.txtMaLop.SelectedValue;
            current["TRINHDO"] = this.txtTrinhDo.SelectedValue;
            current["LAN"] = this.txtLan.Value;
            bs_GVDK.ResetCurrentItem();
            this.gIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
            this.gIAOVIEN_DANGKYTableAdapter.Update(this.dS.GIAOVIEN_DANGKY);
        }

        public void WriteToDBEdit()
        {
            bs_GVDK.EndEdit();
            bs_GVDK.ResetCurrentItem();
            this.gIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
            this.gIAOVIEN_DANGKYTableAdapter.Update(this.dS.GIAOVIEN_DANGKY);
        }

        public bool Edit()
        {
            bool isEmpty = ValidateEmpty();

            if (!isEmpty)
                return false;

            if (t_do == 0)
            {
                x1 = txtTrinhDo.Text.ToString();
            }
            else
            {
                x1 = txtTrinhDo.SelectedValue.ToString();
            }
            bool isValue = ValidateValueEdit1();

            if (!isValue)
                return false;

            String ngayThi = this.txtNgayThi.Text;


            DataRowView currentRow = (DataRowView)bs_GVDK.Current;

            if (currentRow != null)
            {
                currentRow["TRINHDO"] = z;
                currentRow["SOCAUTHI"] = y;
                
                this._callAction.FillData(Share.Action.RecoveryEdit, SetUpCurrentData(currentRow.Row.ItemArray));

                this.txtNgayThi.Text = ngayThi;
                object[] data = new object[] { mAGVTextBox.Text, txtMaMH.Text, txtMaLop.Text, x1, txtNgayThi.Text, txtLan.Value, txtSoCauThi.Text, txtThoiGian.Text };
                currentRow.Row.ItemArray = data;
                return true;
            }
            else
            {
                XtraMessageBox.Show(StringLibrary.E_EditEmpty, StringLibrary.E_EditNotify, MessageBoxButtons.OK);
                return false;
            }
        }

        private void btn_Ghi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (btn_Ghi.Tag == null)
                    return;
                if (this.btn_Ghi.Tag.Equals("ADD"))
                {
                    bool addSucess = Add();

                    if (!addSucess)
                    {
                        MessageBox.Show("Lỗi", "", MessageBoxButtons.OK);
                    }

                    if (addSucess)
                    {
                        WriteToDB();
                        index1++;
                        act_index++;
                        act[act_index] = "AddSuccess";
                        gcGVDK.Enabled = true;
                        btn_Add.Enabled = btn_Edit.Enabled = btn_Delete.Enabled = btn_Reload.Enabled = btn_Thoat.Enabled = btn_Reset.Enabled = true;
                        btn_Ghi.Enabled = false;
                        set();
                    }
                }
                else if (this.btn_Ghi.Tag.Equals("EDIT"))
                {
                    bool editSuccess = Edit();
                    if (editSuccess)
                    {
                        WriteToDBEdit();
                        index1++;
                        act_index++;
                        act[act_index] = "EditSuccess";
                        gcGVDK.Enabled = true;
                        btn_Add.Enabled = btn_Edit.Enabled = btn_Delete.Enabled = btn_Reload.Enabled = btn_Thoat.Enabled = btn_Reset.Enabled = true;
                        btn_Ghi.Enabled = false;
                        set();
                    }
                }
                else return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi đăng kí. " + ex.Message, "", MessageBoxButtons.OK);
            }
        }

        private void btn_Reset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this._callAction.BackAction == Share.Action.None)
            {
                MessageBox.Show("Không có dữ liệu để phục hồi. Vui lòng kiểm tra lại!", "Phục hồi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (act[act_index].Equals("AddSuccess"))
                {
                    this._callAction.BackAction = Share.Action.RecoveryAdd;
                }
                if (act[act_index].Equals("EditSuccess"))
                {
                    this._callAction.BackAction = Share.Action.RecoveryEdit;
                }
                if (act[act_index].Equals("DeleteSuccess"))
                {
                    this._callAction.BackAction = Share.Action.RecoveryDelete;
                }
                RecoveryDataByAction(this._callAction);
            }
        }

        public void RecoveryDataByAction(CallBackAction cAction)
        {
            if (cAction.BackAction == Share.Action.RecoveryAdd)
            {
                DataRow dr = cAction.Table.Rows[index1];

                DataView dt = (DataView)bs_GVDK.List;

                String key1 = dr.ItemArray[1].ToString();
                String key2 = dr.ItemArray[2].ToString();
                String key3 = dr.ItemArray[5].ToString();
                dt.Sort = "MAMH, MALOP ,LAN";
                int index = dt.Find(new object[] { key1, key2, key3 });
                bs_GVDK.RemoveAt(index);
                WriteToDBEdit();
                table1.Rows[index1].Delete();
                this._callAction.FillData(Share.Action.RecoveryAdd, table1);
                act_index--;
                index1--;
                /*String key1 = (((DataRowView)bs_Lop[bs_Lop.Position])["MAKH"].ToString());
                String name = SqlRequestFunction.GetTenKhoa(key1);
                cmbMAKHOA1.Text = name;*/
            }
            else if (cAction.BackAction == Share.Action.RecoveryDelete)
            {
                DataRow dr = cAction.Table.Rows[index1];
                DataRowView drv = (DataRowView)bs_GVDK.AddNew();
                drv.Row.ItemArray = dr.ItemArray;
                WriteToDBEdit();
                table1.Rows[index1].Delete();
                this._callAction.FillData(Share.Action.RecoveryDelete, table1);
                act_index--;
                index1--;
            }
            else if (cAction.BackAction == Share.Action.RecoveryEdit)
            {
                DataRow dr = cAction.Table.Rows[index1];

                DataView dt = (DataView)bs_GVDK.List;

                String key1 = dr.ItemArray[1].ToString();
                String key2 = dr.ItemArray[2].ToString();
                String key3 = dr.ItemArray[5].ToString();

                dt.Sort = "MAMH, MALOP ,LAN";
                bs_GVDK.Position = dt.Find(new object[] { key1, key2, key3 });

                DataRowView currentRow = (DataRowView)bs_GVDK.Current;

                currentRow.Row.ItemArray = dr.ItemArray;
                WriteToDBEdit();
                table1.Rows[index1].Delete();
                this._callAction.FillData(Share.Action.RecoveryEdit, table1);
                act_index--;
                index1--;
            }

            if (index1 == -1)
            {
                this._callAction.Reset();
                this.Refresh();
            }

        }

        private void btn_Reload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.gIAOVIEN_DANGKYTableAdapter.Fill(this.dS.GIAOVIEN_DANGKY);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi reload: " + ex.Message, "", MessageBoxButtons.OK);
            }
        }

        private void btn_Thoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (btn_Thoat.Tag == null)
            {
                return;
            }
            if (btn_Thoat.Tag.Equals("ADD"))
            {
                if (XtraMessageBox.Show(
                    "Bạn có chắc muốn thoát quá trình nhập không?",
                    "Thoát",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    FreeAllControl();
                }
            }
            else if (btn_Thoat.Tag.Equals("EDIT"))
            {
                if (XtraMessageBox.Show(
                    "Bạn có chắc muốn thoát quá trình sửa không?",
                    "Thoát",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    FreeAllControl();
                }
            }
        }

        public void FreeAllControl()
        {
            this.groupBox1.Enabled = true;
            this.btn_Add.Enabled = true;
            this.btn_Edit.Enabled = true;
            this.btn_Delete.Enabled = true;
            btn_Write.Enabled = false;

            this.btn_Thoat.Tag = this.btn_Write.Tag = "";
            this.gIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
            this.gIAOVIEN_DANGKYTableAdapter.Fill(this.dS.GIAOVIEN_DANGKY);
            set();
        }

        private void btn_Delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bs_GVDK.Position;

            if (Program.mGroup == "COSO")
            {
                if (MessageBox.Show("Bạn có thật sự muốn xóa câu hỏi không??", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        DataRowView currentRow = (DataRowView)bs_GVDK.Current;
                        this._callAction.FillData(Share.Action.RecoveryDelete, SetUpCurrentData(currentRow.Row.ItemArray));
                        magv1 = (((DataRowView)bs_GVDK[bs_GVDK.Position])["MAGV"].ToString());
                        bs_GVDK.RemoveCurrent();
                        this.gIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.gIAOVIEN_DANGKYTableAdapter.Update(this.dS.GIAOVIEN_DANGKY);
                        index1++;
                        act_index++;
                        act[act_index] = "DeleteSuccess";
                        if(bs_GVDK.Count == 0) btn_Delete.Enabled = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xóa câu hỏi. bạn hãy xóa lại \n" + ex.Message, "", MessageBoxButtons.OK);
                        this.gIAOVIEN_DANGKYTableAdapter.Fill(this.dS.GIAOVIEN_DANGKY);
                        bs_GVDK.Position = bs_GVDK.Find("MAGV", magv1);
                        return;
                    }
                }
                if (bs_GVDK.Count == 0) btn_Delete.Enabled = false;
            }
            if (Program.mGroup == "GIANGVIEN")
            {
                magv1 = ((DataRowView)bs_GVDK[vitri])["MAGV"].ToString();
                string m = magv1.Trim();
                if (m == magv)
                {
                    String mach = "";
                    //Int32 makh = 0;
                    if (MessageBox.Show("Bạn có thật sự muốn xóa câu hỏi không??", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        try
                        {
                            DataRowView currentRow = (DataRowView)bs_GVDK.Current;
                            this._callAction.FillData(Share.Action.RecoveryDelete, SetUpCurrentData(currentRow.Row.ItemArray));
                            magv1 = (((DataRowView)bs_GVDK[bs_GVDK.Position])["MAGV"].ToString());
                            bs_GVDK.RemoveCurrent();
                            this.gIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
                            this.gIAOVIEN_DANGKYTableAdapter.Update(this.dS.GIAOVIEN_DANGKY);
                            index1++;
                            act_index++;
                            act[act_index] = "DeleteSuccess";
                            if (bs_GVDK.Count == 0) btn_Delete.Enabled = false;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi xóa câu hỏi. bạn hãy xóa lại \n" + ex.Message, "", MessageBoxButtons.OK);
                            this.gIAOVIEN_DANGKYTableAdapter.Fill(this.dS.GIAOVIEN_DANGKY);
                            bs_GVDK.Position = bs_GVDK.Find("CAUHOI", mach);
                            return;
                        }
                    }
                    if (bs_GVDK.Count == 0) btn_Delete.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Bạn không có quyền xóa câu hỏi này!!!", "", MessageBoxButtons.OK);
                }
            }
        }

        private void txtNgayThi_ValueChanged(object sender, EventArgs e)
        {
            this.txtNgayThi.CustomFormat = "dd-MM-yy";
        }

        private void gcGVDK_Click(object sender, EventArgs e)
        {
            DateTime currentDateTime1 = DateTime.Now;
            this.txtNgayThi.MinDate = currentDateTime1.AddDays(-1000);
            DateTime currentDateTime = DateTime.Now;
            this.txtNgayThi.MaxDate = currentDateTime.AddDays(60);

            DataRowView current = (DataRowView)bs_GVDK.Current;
            String x = current["MAMH"].ToString();
            String nameMH = SqlRequestFunction.GetTenMH(x);
            this.txtMH2.Text = nameMH;

            String y = current["MALOP"].ToString();
            String nameLOP = SqlRequestFunction.GetTenLop(y);
            this.txtMaLop1.Text = nameLOP;

            String z = current["TRINHDO"].ToString();
            String TrinhDo = GetLevelName(z);
            this.txtTrinhDo1.Text = TrinhDo;

            String t = current["NGAYTHI"].ToString();
            this.txtNgayThi.Text = t;
        }

        private void txtTrinhDo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtTrinhDo1.Visible = false;
            t_do = 1;
        }

        private void mAGVTextBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtMaMH_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}