using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TracNghiem_CSDLPT.Common;
using DevExpress.XtraEditors;
using TracNghiem_CSDLPT.Share;
using System.Diagnostics;


namespace TracNghiem_CSDLPT
{
    public partial class Frm_NhapLop : DevExpress.XtraEditors.XtraForm
    {
        DateTimePicker datePicker = new DateTimePicker();
        Rectangle _rectangle;

        private int Db = 0;
        int vitri = 0;
        String macs = "";
        private CallBackAction _callAction;
        private DataTable table1 = new DataTable();
        private DataColumn dc;
        private DataColumn dc1;
        private DataColumn dc2;
        private int index1 = -1;
        private String[] act = new string[100];
        private int act_index = -1;

        private CallBackAction _callBackSubform;
        //private int Db = 0;
        int vitri1 = 0;
        private DataTable table2 = new DataTable();
        private DataColumn dv;
        private DataColumn dv1;
        private DataColumn dv2;
        private DataColumn dv3;
        private DataColumn dv4;
        private DataColumn dv5;
        private int index2 = -1;
        private String[] act1 = new string[100];
        private int act_index1 = -1;

        public Frm_NhapLop()
        {
            InitializeComponent();
            _callAction = new CallBackAction();
            dc = new DataColumn("Mã lớp");
            dc1 = new DataColumn("Tên lớp");
            dc2 = new DataColumn("Mã khoa");
            table1.Columns.Add(dc);
            table1.Columns.Add(dc1);
            table1.Columns.Add(dc2);

            _callBackSubform = new CallBackAction();
            dv = new DataColumn("Mã sinh viên");
            dv1 = new DataColumn("Họ");
            dv2 = new DataColumn("Tên");
            dv3 = new DataColumn("Ngày sinh");
            dv4 = new DataColumn("Địa chỉ");
            dv5 = new DataColumn("Mã lớp");
            table2.Columns.Add(dv);
            table2.Columns.Add(dv1);
            table2.Columns.Add(dv2);
            table2.Columns.Add(dv3);
            table2.Columns.Add(dv4);
            table2.Columns.Add(dv5);

            DeniceFeatureByRole();

            this.dgv_Students.Controls.Add(datePicker);
            datePicker.Visible = false;
            datePicker.Format = DateTimePickerFormat.Custom;

            datePicker.TextChanged += DatePicker_TextChanged;
            dgv_Students.CellClick += Dgv_Students_CellClick;


            //txtMALOP.Enabled = txtTENLOP.Enabled = cmbMAKHOA1.Enabled = cmbMAKHOA.Enabled = false;
        }

        private void DatePicker_TextChanged(object sender, EventArgs e)
        {
            dgv_Students.CurrentCell.Value = datePicker.Text.ToString();
        }

        private void Dgv_Students_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_Students.CurrentCell.ReadOnly)
                return;

            if (e.ColumnIndex <= 0)
                return;

            if (dgv_Students.Columns[e.ColumnIndex].Name == "NGAYSINH")
            {
                try
                {
                    try
                    {
                        datePicker.Value = (DateTime)dgv_Students.CurrentCell.Value;
                    }
                    catch
                    {
                        datePicker.Value = DateTime.Now;
                    }

                    _rectangle = dgv_Students.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    datePicker.Size = new Size(_rectangle.Width, _rectangle.Height);
                    datePicker.Location = new Point(_rectangle.X, _rectangle.Y);
                    datePicker.Visible = true;
                }
                catch (Exception ex)
                {
                    return;
                }
            }
            else
            {
                if (datePicker.Visible)
                    datePicker.Visible = false;
            }
        }

        private void DeniceFeatureByRole()
        {

            if (Program.mGroup.Equals("TRUONG"))
            {
                btn_Add.Enabled = btn_Delete.Enabled = btn_Edit.Enabled = btn_Thoat.Enabled = btn_Write.Enabled = btn_Reset.Enabled = false;
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
                MessageBox.Show("Lỗi kết nối về cơ sở mới", "", MessageBoxButtons.OK);
            }
            else
            {
                this.dS.EnforceConstraints = false;
                this.kHOATableAdapter.Connection.ConnectionString = Program.connstr;
                // TODO: This line of code loads data into the 'dS.KHOA' table. You can move, or remove it, as needed.
                this.kHOATableAdapter.Fill(this.dS.KHOA);

                this.dS.EnforceConstraints = false;
                this.lOPTableAdapter.Connection.ConnectionString = Program.connstr;
                // TODO: This line of code loads data into the 'dS.LOP' table. You can move, or remove it, as needed.
                this.lOPTableAdapter.Fill(this.dS.LOP);

                this.dS.EnforceConstraints = false;
                // TODO: This line of code loads data into the 'dS.SINHVIEN' table. You can move, or remove it, as needed.
                this.sINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                this.sINHVIENTableAdapter.Fill(this.dS.SINHVIEN);
                // TODO: This line of code loads data into the 'dS.GIAOVIEN_DANGKY' table. You can move, or remove it, as needed.
                this.gIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
                this.gIAOVIEN_DANGKYTableAdapter.Fill(this.dS.GIAOVIEN_DANGKY);

                //macn = ((DataRowView)bs_Khoa[0])["MACS"].ToString();*/
            }
        }

        private void lOPBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bs_Lop.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dS);

        }

        private void Frm_NhapLop_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dS.SINHVIEN' table. You can move, or remove it, as needed.
            //this.sINHVIENTableAdapter.Fill(this.dS.SINHVIEN);
            // TODO: This line of code loads data into the 'dS.BANGDIEM' table. You can move, or remove it, as needed.
            this.dS.EnforceConstraints = false;
            this.bANGDIEMTableAdapter.Connection.ConnectionString = Program.connstr;
            this.bANGDIEMTableAdapter.Fill(this.dS.BANGDIEM);
            this.dS.EnforceConstraints = false;
            this.kHOATableAdapter.Connection.ConnectionString = Program.connstr;
            // TODO: This line of code loads data into the 'dS.KHOA' table. You can move, or remove it, as needed.
            this.kHOATableAdapter.Fill(this.dS.KHOA);

            this.dS.EnforceConstraints = false;
            // TODO: This line of code loads data into the 'dS.SINHVIEN' table. You can move, or remove it, as needed.
            this.sINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
            this.sINHVIENTableAdapter.Fill(this.dS.SINHVIEN);

            this.dS.EnforceConstraints = false;
            this.lOPTableAdapter.Connection.ConnectionString = Program.connstr;
            // TODO: This line of code loads data into the 'dS.LOP' table. You can move, or remove it, as needed.
            this.lOPTableAdapter.Fill(this.dS.LOP);

            
            // TODO: This line of code loads data into the 'dS.GIAOVIEN_DANGKY' table. You can move, or remove it, as needed.
            this.gIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
            this.gIAOVIEN_DANGKYTableAdapter.Fill(this.dS.GIAOVIEN_DANGKY);




            //macs =((DataRowView)bs)
            this.cmbCoSo.DataSource = Program.bds_ListBrand;
            this.cmbCoSo.DisplayMember = "TENCOSO";
            this.cmbCoSo.ValueMember = "TENSERVER";
            this.cmbCoSo.SelectedIndex = Program.mChinhanh;

            if (Program.mGroup == "TRUONG")
            {
                this.cmbCoSo.Enabled = true;
            }
            else
            {
                this.cmbCoSo.Enabled = false;
            }

            if (bs_Lop.Count == 0)
            {
                btn_Delete.Enabled = false;
            }

            //this.groupBox1.Enabled = false;
            this.btn_Reset.Enabled = false;
            this.btn_Write.Enabled = false;
            DataView dt = (DataView)bs_Lop.List;
            String key1 = (((DataRowView)bs_Lop[bs_Lop.Position])["MAKH"].ToString());
            String name = SqlRequestFunction.GetTenKhoa(key1);
            cmbMAKHOA1.Text = name;
            txtMALOP.Enabled = txtTENLOP.Enabled = cmbMAKHOA.Enabled = cmbMAKHOA1.Enabled = false;
        }

        private void btn_Add_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.btn_Thoat.Tag = "ADD";
            this.btn_Write.Tag = "ADD";
            cmbMAKHOA1.Visible = false;
            txtMALOP.Enabled = true;
            txtTENLOP.Enabled = true;
            cmbMAKHOA.Enabled = true;
            Db = 1;
            SetUpButtonForAction();
        }

        public void SetUpButtonForAction()
        {
            this.cmbMAKHOA.Enabled = true;
            this.btn_Add.Enabled = false;
            this.btn_Edit.Enabled = false;
            this.btn_Delete.Enabled = false;
            this.btn_Write.Enabled = true;
            this.btn_Reset.Enabled = false;

            if (this.btn_Write.Tag.Equals("ADD"))
            {
                bs_Lop.AddNew();
            }
        }

        private void btn_Write_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (btn_Write.Tag == null)
                    return;
                if (this.btn_Write.Tag.Equals("ADD"))
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
                        gcLop.Enabled = true;
                        btn_Add.Enabled = btn_Edit.Enabled = btn_Delete.Enabled = btn_Reload.Enabled = btn_Thoat.Enabled = btn_Reset.Enabled = true;
                        btn_Write.Enabled = false;
                        String key1 = cmbMAKHOA.Text.ToString();
                        String name = SqlRequestFunction.GetTenKhoa(key1);
                        cmbMAKHOA1.Text = name;
                        cmbMAKHOA1.Visible = true;
                        txtMALOP.Enabled = txtTENLOP.Enabled = cmbMAKHOA.Enabled = cmbMAKHOA1.Enabled = false;
                    }
                }
                else if (this.btn_Write.Tag.Equals("EDIT"))
                {
                    bool editSuccess = Edit();

                    if (editSuccess)
                    {
                        WriteToDB();
                        index1++;
                        act_index++;
                        act[act_index] = "EditSuccess";
                        gcLop.Enabled = true;
                        btn_Add.Enabled = btn_Edit.Enabled = btn_Delete.Enabled = btn_Reload.Enabled = btn_Thoat.Enabled = btn_Reset.Enabled = true;
                        btn_Write.Enabled = false;
                        String key1 = cmbMAKHOA.Text.ToString();
                        String name = SqlRequestFunction.GetTenKhoa(key1);
                        cmbMAKHOA1.Text = name;
                        cmbMAKHOA1.Visible = true;
                        txtMALOP.Enabled = txtTENLOP.Enabled = cmbMAKHOA.Enabled = cmbMAKHOA1.Enabled = false;
                    }
                }
                else return;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("MAMH"))
                    MessageBox.Show("Mã môn học không được trùng", "", MessageBoxButtons.OK);
                else
                {
                    MessageBox.Show("Lỗi ghi Môn học. " + ex.Message, "", MessageBoxButtons.OK);
                }
            }
        }

        public void WriteToDB()
        {
            bs_Lop.EndEdit();
            if (Db == 1)
            {
                DataRowView current = (DataRowView)bs_Lop.Current;
                current["MAKH"] = this.cmbMAKHOA.SelectedValue;
            }
            bs_Lop.ResetCurrentItem();
            this.lOPTableAdapter.Connection.ConnectionString = Program.connstr;
            this.lOPTableAdapter.Update(this.dS.LOP);
        }
        public void WriteToDB1()
        {
            bs_Lop.EndEdit();
            bs_Lop.ResetCurrentItem();
            this.lOPTableAdapter.Connection.ConnectionString = Program.connstr;
            this.lOPTableAdapter.Update(this.dS.LOP);
        }

        public bool Add()
        {
            bool isEmpty = ValidateEmpty();

            if (!isEmpty)
                return false;
            if (SqlRequestFunction.ClassIsExist(txtMALOP.Text.Trim()))
            {
                MessageBox.Show("Mã lớp học đã tồn tại.", "", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                object[] data = new object[] { txtMALOP.Text, txtTENLOP.Text, cmbMAKHOA.SelectedValue.ToString() };
                this._callAction.FillData(Share.Action.RecoveryAdd, SetUpCurrentData(data));
                return true;
            }
        }

        public bool Edit()
        {
            bool isEmpty = ValidateEmpty();

            if (!isEmpty)
                return false;

            DataRowView currentRow = (DataRowView)bs_Lop.Current;

            if (currentRow != null)
            {
                this._callAction.FillData(Share.Action.RecoveryEdit, SetUpCurrentData(currentRow.Row.ItemArray));
                object[] data = new object[] { txtMALOP.Text, txtTENLOP.Text, cmbMAKHOA.Text.ToString() };
                currentRow.Row.ItemArray = data;
                return true;
            }
            else
            {
                XtraMessageBox.Show(StringLibrary.E_EditEmpty, StringLibrary.E_EditNotify, MessageBoxButtons.OK);
                return false;
            }
        }

        public DataTable SetUpCurrentData(object[] datas)
        {
            DataRow dr = table1.NewRow();
            dr.ItemArray = datas;
            table1.Rows.Add(dr);

            return table1;
        }

        public bool ValidateEmpty()
        {
            if (txtMALOP.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Vui lòng nhập mã lớp.", "", MessageBoxButtons.OK);
                txtMALOP.Focus();
                return false;
            }

            if (txtTENLOP.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Vui lòng nhập tên môn học.", "", MessageBoxButtons.OK);
                txtTENLOP.Focus();
                return false;
            }
            if (cmbMAKHOA.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Vui lòng chọn khoa.", "", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private void btn_Edit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.btn_Write.Tag = "EDIT";
            this.btn_Thoat.Tag = "EDIT";
            vitri = bs_Lop.Position;
            groupBox1.Enabled = true;
            cmbMAKHOA.Visible = true;
            cmbMAKHOA1.Visible = true;
            cmbMAKHOA1.Enabled = true;
            cmbMAKHOA.Enabled = true;
            txtMALOP.Enabled = false;
            txtTENLOP.Enabled = true;
            Db = 0;
            SetUpButtonForAction();
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
                int index = bs_Lop.Find("MALOP", dr.ItemArray[0]);
                bs_Lop.RemoveAt(index);
                WriteToDB1();
                table1.Rows[index1].Delete();
                this._callAction.FillData(Share.Action.RecoveryAdd, table1);
                act_index--;
                index1--;
                String key1 = (((DataRowView)bs_Lop[bs_Lop.Position])["MAKH"].ToString());
                String name = SqlRequestFunction.GetTenKhoa(key1);
                cmbMAKHOA1.Text = name;
            }
            else if (cAction.BackAction == Share.Action.RecoveryDelete)
            {
                DataRow dr = cAction.Table.Rows[index1];
                DataRowView drv = (DataRowView)bs_Lop.AddNew();
                drv.Row.ItemArray = dr.ItemArray;
                WriteToDB1();
                table1.Rows[index1].Delete();
                this._callAction.FillData(Share.Action.RecoveryDelete, table1);
                act_index--;
                index1--;
                String key1 = (((DataRowView)bs_Lop[bs_Lop.Position])["MAKH"].ToString());
                String name = SqlRequestFunction.GetTenKhoa(key1);
                cmbMAKHOA1.Text = name;
            }
            else if (cAction.BackAction == Share.Action.RecoveryEdit)
            {
                DataRow dr = cAction.Table.Rows[index1];
                bs_Lop.Position = bs_Lop.Find("MALOP", dr.ItemArray[0]);

                DataRowView currentRow = (DataRowView)bs_Lop.Current;

                currentRow.Row.ItemArray = dr.ItemArray;
                WriteToDB1();
                table1.Rows[index1].Delete();
                this._callAction.FillData(Share.Action.RecoveryEdit, table1);
                act_index--;
                index1--;
                String key1 = (((DataRowView)bs_Lop[bs_Lop.Position])["MAKH"].ToString());
                String name = SqlRequestFunction.GetTenKhoa(key1);
                cmbMAKHOA1.Text = name;
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
                this.lOPTableAdapter.Fill(this.dS.LOP);
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
            this.btn_Add.Enabled = true;
            this.btn_Edit.Enabled = true;
            this.btn_Delete.Enabled = true;
            btn_Write.Enabled = false;
            txtMALOP.Enabled = txtTENLOP.Enabled = cmbMAKHOA.Enabled = cmbMAKHOA1.Enabled = false;

            this.btn_Thoat.Tag = this.btn_Write.Tag = "";
            this.lOPTableAdapter.Connection.ConnectionString = Program.connstr;
            this.lOPTableAdapter.Fill(this.dS.LOP);
        }

        private void btn_Delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String malop = "";
            Db = 1;
            DataView dt = (DataView)bs_GVDK.List;
            dt.Sort = "MALOP";
            String key1 = (((DataRowView)bs_Lop[bs_Lop.Position])["MALOP"].ToString());
            DataRowView[] rowView = dt.FindRows(new object[] { key1 });
            if (rowView.Length != 0)
            {
                MessageBox.Show("Không thể xóa lớp này vì có trong giáo viên đăng ký", "", MessageBoxButtons.OK);
                return;
            }

            DataView dt1 = (DataView)bs_SinhVien.List;
            dt1.Sort = "MALOP";
            String key2 = (((DataRowView)bs_Lop[bs_Lop.Position])["MALOP"].ToString());
            DataRowView[] rowView1 = dt1.FindRows(new object[] { key1 });
            if (rowView1.Length != 0)
            {
                MessageBox.Show("Không thể xóa lớp này vì có trong sinh viên", "", MessageBoxButtons.OK);
                return;
            }
            //String makhoa = (((DataRowView)bs_Lop[bs_Lop.Position])["MAKH"].ToString());

            if (MessageBox.Show("Bạn có thật sự muốn xóa lớp không??", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                MessageBox.Show("aa", "", MessageBoxButtons.OK);
                DataRowView currentRow = (DataRowView)bs_Lop.Current;
                this._callAction.FillData(Share.Action.RecoveryDelete, SetUpCurrentData(currentRow.Row.ItemArray));
                bs_Lop.RemoveCurrent();
                WriteToDB1();
                index1++;
                act_index++;
                act[act_index] = "DeleteSuccess";
                if (bs_Khoa.Count == 0) btn_Delete.Enabled = false;
                btn_Reset.Enabled = true;
                String key = (((DataRowView)bs_Lop[bs_Lop.Position])["MAKH"].ToString());
                String name = SqlRequestFunction.GetTenKhoa(key);
                cmbMAKHOA1.Text = name;
                cmbMAKHOA.Text = key;
                txtMALOP.Enabled = txtTENLOP.Enabled = cmbMAKHOA.Enabled = cmbMAKHOA1.Enabled = false;
            }
        }

        private void cmbMAKHOA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.btn_Write.Tag == null)
            {
                return;
            }
            if (this.btn_Write.Tag.Equals("EDIT"))
            {
                //   cmbMAKHOA1.Visible = false;
            }
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.lOPTableAdapter.FillBy(this.dS.LOP);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
        private void gcLop_Click_1(object sender, EventArgs e)
        {
            String key1 = (((DataRowView)bs_Lop[bs_Lop.Position])["MAKH"].ToString());
            String name = SqlRequestFunction.GetTenKhoa(key1);
            cmbMAKHOA1.Text = name;
            cmbMAKHOA.Text = key1;
        }

        private void cmbMAKHOA_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (this.btn_Write.Tag == null)
            {
                return;
            }
            if (this.btn_Write.Tag.Equals("EDIT"))
            {
                Db = 1;
                cmbMAKHOA1.Text = cmbMAKHOA.Text;
            }
        }

        private void tsmi_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGVHadError(dgv_Students))
                    return;
                _callBackSubform.BackAction = Share.Action.RecoveryAdd;
                DataRowView drv = (DataRowView)bs_SinhVien.AddNew();
                dgv_Students.CurrentRow.Cells[0].ReadOnly = false;
                dgv_Students.CurrentRow.Cells[1].ReadOnly = false;
                dgv_Students.CurrentRow.Cells[2].ReadOnly = false;
                dgv_Students.CurrentRow.Cells[3].ReadOnly = false;
                dgv_Students.CurrentRow.Cells[4].ReadOnly = false;
                dgv_Students.RowValidating += Dgv_Students_RowValidating;
                dgv_Students.RowValidated += Dgv_Students_RowValidated;

                index2++;
                act_index1++;
                act1[act_index1] = "AddSuccess";
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
        }

        private bool DGVHadError(DataGridView dgv)
        {
            foreach (DataGridViewRow rowView in dgv.Rows)
            {
                bool a = rowView.IsNewRow;
                if (rowView.ErrorText != "")
                {
                    return true;
                }
            }
            return false;
        }

        private void Dgv_Students_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            
            try
            {
                bool isFirst = true;
                int index = 0;
                foreach (DataGridViewCell drv in dgv_Students.Rows[e.RowIndex].Cells)
                {
                    if (String.IsNullOrEmpty(drv.Value.ToString()))
                    {
                        
                        e.Cancel = true;

                        dgv_Students.Rows[e.RowIndex].ErrorText = "Had error";
                        dgv_Students.Rows[e.RowIndex].Cells[index].ErrorText = "Dose not empty.";

                        dgv_Students.CurrentCell = drv;
                    }
                    else
                    {                      
                        dgv_Students.Rows[e.RowIndex].Cells[index].ErrorText = null;
                    }

                    if (isFirst)
                    {
                        string valCurrent = drv.FormattedValue.ToString();

                        if (String.IsNullOrEmpty(drv.Value.ToString()))
                        {

                            e.Cancel = true;

                            dgv_Students.Rows[e.RowIndex].ErrorText = "Had error";
                            dgv_Students.Rows[e.RowIndex].Cells[index].ErrorText = "Dose not empty.";

                            dgv_Students.CurrentCell = drv;
                        }else
                        {
                            if (SqlRequestFunction.StudentIsExist(valCurrent))
                            {
                                dgv_Students.Rows[e.RowIndex].ErrorText = "Had error";
                                dgv_Students.Rows[e.RowIndex].Cells[index].ErrorText = "Student Code Is Exists.";
                                dgv_Students.CurrentCell = drv;
                            }
                            else
                            {
                                dgv_Students.Rows[e.RowIndex].Cells[index].ErrorText = null;
                            }
                        }
                     
                        isFirst = false;
                    }
                    index++;
                }
            }
            catch { }
        }

        private void Dgv_Students_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            dgv_Students.Rows[e.RowIndex].ErrorText = null;
            foreach (DataGridViewCell cell in dgv_Students.Rows[e.RowIndex].Cells)
            {
                cell.ErrorText = null;
            }

            try
            {
                dgv_Students.RowValidating -= Dgv_Students_RowValidating;
                dgv_Students.RowValidated -= Dgv_Students_RowValidated;

                SaveStudentToDb();
                if (this._callBackSubform.BackAction == Share.Action.RecoveryAdd)
                {
                    object[] data = new object[] { dgv_Students.Rows[e.RowIndex].Cells[0].Value,
                                               dgv_Students.Rows[e.RowIndex].Cells[1].Value,
                                               dgv_Students.Rows[e.RowIndex].Cells[2].Value,
                                               dgv_Students.Rows[e.RowIndex].Cells[3].Value,
                                               dgv_Students.Rows[e.RowIndex].Cells[4].Value,
                                               dgv_Students.Rows[e.RowIndex].Cells[5].Value};
                    this._callBackSubform.Table = SetUpCurrentDataSub(data);
                }
                else if (this._callBackSubform.BackAction == Share.Action.RecoveryEdit)
                {
                    dgv_Students.CurrentRow.Cells[1].ReadOnly = true;
                    dgv_Students.CurrentRow.Cells[2].ReadOnly = true;
                    dgv_Students.CurrentRow.Cells[3].ReadOnly = true;
                    dgv_Students.CurrentRow.Cells[4].ReadOnly = true;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void SaveStudentToDb()
        {
            this.bs_SinhVien.EndEdit();
            this.bs_SinhVien.ResetCurrentItem();
            this.sINHVIENTableAdapter.Update(this.dS.SINHVIEN);
        }

        public DataTable SetUpCurrentDataSub(object[] datas)
        {
            DataRow dr = table2.NewRow();
            dr.ItemArray = datas;
            table2.Rows.Add(dr);

            return table2;
        }

        private void tsmi_Edit_Click(object sender, EventArgs e)
        {
            _callBackSubform.BackAction = Share.Action.RecoveryEdit;

            dgv_Students.CurrentRow.Cells[1].ReadOnly = false;
            dgv_Students.CurrentRow.Cells[2].ReadOnly = false;
            dgv_Students.CurrentRow.Cells[3].ReadOnly = false;
            dgv_Students.CurrentRow.Cells[4].ReadOnly = false;

            object[] data = ((DataRowView)bs_SinhVien.Current).Row.ItemArray;
            this._callBackSubform.Table = SetUpCurrentDataSub(data);

            dgv_Students.RowValidating += Dgv_Students_RowValidating;
            dgv_Students.RowValidated += Dgv_Students_RowValidated;

            index2++;
            act_index1++;
            act1[act_index1] = "EditSuccess";
        }

        private void tsmi_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView currentRow = (DataRowView)bs_SinhVien.Current;

                DataView dt = (DataView)bs_BangDiem.List;
                dt.Sort = "MASV";
                String key1 = (((DataRowView)bs_SinhVien[bs_SinhVien.Position])["MASV"].ToString());
                DataRowView[] rowView = dt.FindRows(new object[] { key1 });


                if (rowView.Length != 0)
                {
                    MessageBox.Show("Không thể xóa sinh viên này vì có trong bảng điểm.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    object[] data = currentRow.Row.ItemArray;
                    DialogResult dialogResult = MessageBox.Show(
                    "Có chắc muốn xóa giáo viên: " + data[1] + " " + data[2],
                    "Xoá",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            bs_SinhVien.RemoveCurrent();
                            SaveStudentToDb();

                            this._callBackSubform.Table = SetUpCurrentDataSub(data);
                            _callBackSubform.BackAction = Share.Action.RecoveryDelete;

                            MessageBox.Show("Xóa thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            index2++;
                            act_index1++;
                            act1[act_index1] = "DeleteSuccess";
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Can not delete row because: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
        }

        private void tsmi_Reset_Click(object sender, EventArgs e)
        {
            if (_callBackSubform.BackAction == Share.Action.None)
            {
                MessageBox.Show("Không có dữ liệu để phục hồi. Vui lòng kiểm tra lại!", "Phục hồi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (act1[act_index1].Equals("AddSuccess"))
                {
                    this._callBackSubform.BackAction = Share.Action.RecoveryAdd;
                }
                if (act1[act_index1].Equals("EditSuccess"))
                {
                    this._callBackSubform.BackAction = Share.Action.RecoveryEdit;
                }
                if (act1[act_index1].Equals("DeleteSuccess"))
                {
                    this._callBackSubform.BackAction = Share.Action.RecoveryDelete;
                }
                RecoveryDataByActionSub(this._callBackSubform);
            }
        }

        public void RecoveryDataByActionSub(CallBackAction cAction)
        {
            if (cAction.BackAction == Share.Action.RecoveryAdd)
            {
                DataView dt = (DataView)bs_SinhVien.List;
                dt.Sort = "MASV";
                DataRow dr = cAction.Table.Rows[index2];
                String key = dr.ItemArray[0].ToString();
                int index = dt.Find(key);

                bs_SinhVien.RemoveAt(index);
                SaveStudentToDb();

                table2.Rows[index2].Delete();
                this._callBackSubform.FillData(Share.Action.RecoveryAdd, table2);
                act_index1--;
                index2--;
            }
            else if (cAction.BackAction == Share.Action.RecoveryDelete)
            {
                DataView dt = (DataView)bs_SinhVien.List;
                dt.Sort = "MASV";
                if (dt.FindRows(cAction.Table.Rows[index2]).Length != 0)
                {
                    MessageBox.Show("Mã sinh viên đã tồn tại.Vui lòng nhập lại!");
                }
                else
                {
                    DataRow dr = cAction.Table.Rows[index2];
                    DataRowView drv = (DataRowView)bs_SinhVien.AddNew();
                    drv.Row.ItemArray = dr.ItemArray;

                    SaveStudentToDb();
                    table2.Rows[index2].Delete();
                    this._callBackSubform.FillData(Share.Action.RecoveryDelete, table2);
                    act_index1--;
                    index2--;
                }
            }
            else if (cAction.BackAction == Share.Action.RecoveryEdit)
            {
                DataView dt = (DataView)bs_SinhVien.List;
                dt.Sort = "MASV";
                DataRow dr = cAction.Table.Rows[index2];
                String key = dr.ItemArray[0].ToString();
                int index = dt.Find(key);

                bs_SinhVien.Position = index;

                DataRowView currentRow = (DataRowView)bs_SinhVien.Current;

                currentRow.Row.ItemArray = dr.ItemArray;
                SaveStudentToDb();
                table2.Rows[index2].Delete();
                this._callBackSubform.FillData(Share.Action.RecoveryEdit, table2);
                act_index1--;
                index2--;
            }

            if (index2 == -1)
            {
                this._callBackSubform.Reset();
                this.Refresh();
            }
        }
    }
}