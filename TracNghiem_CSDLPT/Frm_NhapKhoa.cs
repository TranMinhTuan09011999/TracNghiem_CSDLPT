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
using TracNghiem_CSDLPT.Share;
using System.Data.SqlClient;
using TracNghiem_CSDLPT.Common;


namespace TracNghiem_CSDLPT
{
    public partial class Frm_NhapKhoa : DevExpress.XtraEditors.XtraForm
    {
        string macn = "";
        int vitri = 0;
        private CallBackAction _callAction;
        private DataTable table1 = new DataTable();
        private DataColumn dc;
        private DataColumn dc1;
        private DataColumn dc2;
        private int index1 = -1;
        private String[] act = new string[100];
        private int act_index = -1;


        private CallBackAction _callBackSubform;
        private int Db = 0;
        String macs = "";
        private DataTable table2 = new DataTable();
        private DataColumn dv;
        private DataColumn dv1;
        private DataColumn dv2;
        private DataColumn dv3;
        private DataColumn dv4;
        private int index2 = -1;
        private String[] act1 = new string[100];
        private int act_index1 = -1;

        public Frm_NhapKhoa()
        {
            InitializeComponent();
            _callAction = new CallBackAction();
            _callBackSubform = new CallBackAction();
            dc = new DataColumn("Mã khoa");
            dc1 = new DataColumn("Tên khoa");
            dc2 = new DataColumn("Cơ sở");
            table1.Columns.Add(dc);
            table1.Columns.Add(dc1);
            table1.Columns.Add(dc2);


            _callAction = new CallBackAction();
            dv = new DataColumn("Mã giáo viên");
            dv1 = new DataColumn("Họ");
            dv2 = new DataColumn("Tên");
            dv3 = new DataColumn("Địa chỉ");
            dv4 = new DataColumn("Mã khoa");
            table2.Columns.Add(dv);
            table2.Columns.Add(dv1);
            table2.Columns.Add(dv2);
            table2.Columns.Add(dv3);
            table2.Columns.Add(dv4);
            


            DeniceFeatureByRole();

            //this._callAction = new CallBackAction();
            //this._callBackSubform = new CallBackAction();

            //bs_Khoa.CurrentChanged += Bs_Khoa_CurrentChanged;
        }

        private void kHOABindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bs_Khoa.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dS);

        }

        private void Frm_NhapKhoa_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dS.GIAOVIEN_DANGKY' table. You can move, or remove it, as needed.
            this.dS.EnforceConstraints = false;
            this.gIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
            this.gIAOVIEN_DANGKYTableAdapter.Fill(this.dS.GIAOVIEN_DANGKY);
            // TODO: This line of code loads data into the 'dS.BODE' table. You can move, or remove it, as needed.
            this.dS.EnforceConstraints = false;
            this.bODETableAdapter.Connection.ConnectionString = Program.connstr;
            this.bODETableAdapter.Fill(this.dS.BODE);
            //theo nguyên tắc đầu 1 có trước, đầu nhiều có sau

            // TODO: This line of code loads data into the 'dS.KHOA' table. You can move, or remove it, as needed.
            this.dS.EnforceConstraints = false;
            this.kHOATableAdapter.Connection.ConnectionString = Program.connstr;
            this.kHOATableAdapter.Fill(this.dS.KHOA);

            // TODO: This line of code loads data into the 'dS.GIAOVIEN' table. You can move, or remove it, as needed.
            this.dS.EnforceConstraints = false;
            this.gIAOVIENTableAdapter.Connection.ConnectionString = Program.connstr;
            this.gIAOVIENTableAdapter.Fill(this.dS.GIAOVIEN);
            // TODO: This line of code loads data into the 'dS.LOP' table. You can move, or remove it, as needed.
            this.dS.EnforceConstraints = false;
            this.lOPTableAdapter.Connection.ConnectionString = Program.connstr;
            this.lOPTableAdapter.Fill(this.dS.LOP);

            this.cmbCoSo.DataSource = Program.bds_ListBrand;
            this.cmbCoSo.DisplayMember = "TENCOSO";
            this.cmbCoSo.ValueMember = "TENSERVER";
            this.cmbCoSo.SelectedIndex = Program.mChinhanh;
            macn = ((DataRowView)bs_Khoa[0])["MACS"].ToString();

            if(Program.mGroup == "TRUONG")
            {
                this.cmbCoSo.Enabled = true;
            }else
            {
                this.cmbCoSo.Enabled = false;
            }

            if(bs_Khoa.Count == 0)
            {
                btn_Delete.Enabled = false;
            }

            set();
        }

        public void set()
        {
            btn_Write.Enabled = false;
            this.btn_Reset.Enabled = true;
            this.btn_Reload.Enabled = true;
            this.groupBox1.Enabled = false;
        }

        private void btn_Add_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.btn_Thoat.Tag = "ADD";
            this.btn_Write.Tag = "ADD";

            SetUpButtonForAction();
           
        }

        public void SetUpButtonForAction()
        {
            this.btn_Add.Enabled = false;
            this.btn_Edit.Enabled = false;
            this.btn_Delete.Enabled = false;
            this.btn_Write.Enabled = true;
            this.btn_Reload.Enabled = false;
            this.btn_Reset.Enabled = false;
            this.groupBox1.Enabled = true;

            if (this.btn_Write.Tag.Equals("ADD"))
            {
                bs_Khoa.AddNew();
                cmbMACS.Text = macn;
                cmbMACS.Enabled = false;
            }
        }

        private void DeniceFeatureByRole()
        {

            if (Program.mGroup.Equals("TRUONG"))
            {
                btn_Add.Enabled = btn_Delete.Enabled = btn_Edit.Enabled = btn_Thoat.Enabled = btn_Write.Enabled = btn_Reset.Enabled = false;
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
                        gcKhoa.Enabled = true;
                        btn_Add.Enabled = btn_Edit.Enabled = btn_Delete.Enabled = btn_Reload.Enabled = btn_Thoat.Enabled = true;
                        btn_Write.Enabled = false;
                        set();
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
                        gcKhoa.Enabled = true;
                        btn_Add.Enabled = btn_Edit.Enabled = btn_Delete.Enabled = btn_Reload.Enabled = btn_Thoat.Enabled = true;
                        btn_Write.Enabled = false;
                        set();
                        //DataTable dt = SetUpCurrentData(((DataRowView)bs_MonHoc.Current).Row.ItemArray);
                    }
                }
                else return;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("MAKH"))
                    MessageBox.Show("Mã khoa không được trùng", "", MessageBoxButtons.OK);
                else
                {
                    MessageBox.Show("Lỗi ghi khoa. " + ex.Message, "", MessageBoxButtons.OK);
                }
            }
        }

        public bool Add()
        {
            bool isEmpty = ValidateEmpty();

            if (!isEmpty)
                return false;
            if (SqlRequestFunction.DepartmentIsExist(txtMAKH.Text.Trim()))
            {
                MessageBox.Show("Mã khoa đã tồn tại.", "", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                object[] data = new object[] { txtMAKH.Text, txtTENKH.Text, cmbMACS.Text};
                this._callAction.FillData(Share.Action.RecoveryAdd, SetUpCurrentData(data));
                return true;
            }
        }

        public bool Edit()
        {
            bool isEmpty = ValidateEmpty();

            if (!isEmpty)
                return false;

            DataRowView currentRow = (DataRowView)bs_Khoa.Current;

            if (currentRow != null)
            {
                this._callAction.FillData(Share.Action.RecoveryEdit, SetUpCurrentData(currentRow.Row.ItemArray));
                object[] data = new object[] { txtMAKH.Text, txtTENKH.Text, cmbMACS.Text };
                currentRow.Row.ItemArray = data;
                return true;
            }
            else
            {
                XtraMessageBox.Show(StringLibrary.E_EditEmpty, StringLibrary.E_EditNotify, MessageBoxButtons.OK);
                return false;
            }
        }

        public void WriteToDB()
        {
            bs_Khoa.EndEdit();
            bs_Khoa.ResetCurrentItem();
            this.kHOATableAdapter.Connection.ConnectionString = Program.connstr;
            this.kHOATableAdapter.Update(this.dS.KHOA);
        }

        public bool ValidateEmpty()
        {
            if (txtMAKH.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Vui lòng nhập mã khoa.", "", MessageBoxButtons.OK);
                txtMAKH.Focus();
                return false;
            }

            if (txtTENKH.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Vui lòng nhập tên khoa.", "", MessageBoxButtons.OK);
                txtTENKH.Focus();
                return false;
            }

            return true;
        }

        public DataTable SetUpCurrentData(object[] datas)
        {
            DataRow dr = table1.NewRow();
            dr.ItemArray = datas;
            table1.Rows.Add(dr);

            return table1;
        }

        private void cmbMACS_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                int index = bs_Khoa.Find("MAKH", dr.ItemArray[0]);
                bs_Khoa.RemoveAt(index);
                WriteToDB();
                table1.Rows[index1].Delete();
                this._callAction.FillData(Share.Action.RecoveryAdd, table1);
                act_index--;
                index1--;
                set();
            }
            else if (cAction.BackAction == Share.Action.RecoveryDelete)
            {
                DataRow dr = cAction.Table.Rows[index1];
                DataRowView drv = (DataRowView)bs_Khoa.AddNew();
                drv.Row.ItemArray = dr.ItemArray;
                WriteToDB();
                table1.Rows[index1].Delete();
                this._callAction.FillData(Share.Action.RecoveryDelete, table1);
                act_index--;
                index1--;
                set();
            }
            else if (cAction.BackAction == Share.Action.RecoveryEdit)
            {
                DataRow dr = cAction.Table.Rows[index1];
                bs_Khoa.Position = bs_Khoa.Find("MAKH", dr.ItemArray[0]);
                DataRowView currentRow = (DataRowView)bs_Khoa.Current;
                currentRow.Row.ItemArray = dr.ItemArray;
                WriteToDB();
                table1.Rows[index1].Delete();
                this._callAction.FillData(Share.Action.RecoveryEdit, table1);
                act_index--;
                index1--;
                set();
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
                this.kHOATableAdapter.Fill(this.dS.KHOA);
            }catch(Exception ex)
            {
                MessageBox.Show("Lỗi reload: " + ex.Message, "", MessageBoxButtons.OK);
            }
        }

        private void btn_Delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String makh = "";

            DataView dt = (DataView)bs_GiaoVien.List;
            dt.Sort = "MAKH";
            String key1 = (((DataRowView)bs_Khoa[bs_Khoa.Position])["MAKH"].ToString());
            DataRowView[] rowView = dt.FindRows(new object[] { key1 });
            if (rowView.Length != 0)
            {
                MessageBox.Show("Không thể xóa khoa này vì có giáo viên", "", MessageBoxButtons.OK);
                return;
            }

            DataView dt1 = (DataView)bs_Lop.List;
            dt1.Sort = "MAKH";
            String key2 = (((DataRowView)bs_Khoa[bs_Khoa.Position])["MAKH"].ToString());
            DataRowView[] rowView1 = dt1.FindRows(new object[] { key1 });
            if (rowView1.Length != 0)
            {
                MessageBox.Show("Không thể xóa khoa này vì có Lớp", "", MessageBoxButtons.OK);
                return;
            }
            if (MessageBox.Show("Bạn có thật sự muốn xóa khoa không??","Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                DataRowView currentRow = (DataRowView)bs_Khoa.Current;
                this._callAction.FillData(Share.Action.RecoveryDelete, SetUpCurrentData(currentRow.Row.ItemArray));
                bs_Khoa.RemoveCurrent();
                WriteToDB();
                index1++;
                act_index++;
                act[act_index] = "DeleteSuccess";
                if (bs_Khoa.Count == 0) btn_Delete.Enabled = false;
            }
        }

        private void cmbCoSo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbCoSo.SelectedValue.ToString() == "System.Data.DataRowView")
            {
                return;
            }

            Program.servername = cmbCoSo.SelectedValue.ToString();

            if(cmbCoSo.SelectedIndex != Program.mChinhanh)
            {
                Program.mlogin = Program.remotelogin;
                Program.password = Program.remotepassword;
            }
            else
            {
                Program.mlogin = Program.mloginDN;
                Program.password = Program.passwordDN;
            }

            if(Program.KetNoi() == 0)
            {
                MessageBox.Show("Lỗi kết nối về chi nhánh mới", "", MessageBoxButtons.OK);
            }
            else
            {
                // TODO: This line of code loads data into the 'dS.KHOA' table. You can move, or remove it, as needed.
                this.dS.EnforceConstraints = false;
                this.kHOATableAdapter.Connection.ConnectionString = Program.connstr;
                this.kHOATableAdapter.Fill(this.dS.KHOA);

                // TODO: This line of code loads data into the 'dS.GIAOVIEN' table. You can move, or remove it, as needed.
                this.dS.EnforceConstraints = false;
                this.gIAOVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                this.gIAOVIENTableAdapter.Fill(this.dS.GIAOVIEN);
                // TODO: This line of code loads data into the 'dS.LOP' table. You can move, or remove it, as needed.
                this.dS.EnforceConstraints = false;
                this.lOPTableAdapter.Connection.ConnectionString = Program.connstr;
                this.lOPTableAdapter.Fill(this.dS.LOP);

                macn = ((DataRowView)bs_Khoa[0])["MACS"].ToString();
            }
        }

        private void btn_Edit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.btn_Write.Tag = "EDIT";
            this.btn_Thoat.Tag = "EDIT";

            this.btn_Add.Enabled = false;
            this.btn_Edit.Enabled = false;
            this.btn_Delete.Enabled = false;
            this.btn_Write.Enabled = true;
            this.btn_Reload.Enabled = false;
            this.btn_Reset.Enabled = false;
            this.groupBox1.Enabled = true;

            this.txtMAKH.Enabled = false;
            this.cmbMACS.Enabled = false;

            vitri = bs_Khoa.Position;
        }

        private void btn_Thoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(btn_Thoat.Tag == null)
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
            this.kHOATableAdapter.Connection.ConnectionString = Program.connstr;
            this.kHOATableAdapter.Fill(this.dS.KHOA);
            set();
        }

        private void tsmi_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGVHadError(dgv_Teachers))
                    return;
                _callBackSubform.BackAction = Share.Action.RecoveryAdd;
                DataRowView drv = (DataRowView)bs_GiaoVien.AddNew();
                dgv_Teachers.CurrentRow.Cells[0].ReadOnly = false;
                dgv_Teachers.CurrentRow.Cells[1].ReadOnly = false;
                dgv_Teachers.CurrentRow.Cells[2].ReadOnly = false;
                dgv_Teachers.CurrentRow.Cells[3].ReadOnly = false;
                dgv_Teachers.RowValidating += Dgv_Teachers_RowValidating;
                dgv_Teachers.RowValidated += Dgv_Teachers_RowValidated;

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

        

        private void Dgv_Teachers_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            dgv_Teachers.Rows[e.RowIndex].ErrorText = null;
            foreach (DataGridViewCell cell in dgv_Teachers.Rows[e.RowIndex].Cells)
            {
                cell.ErrorText = null;
            }

            try
            {
                dgv_Teachers.RowValidating -= Dgv_Teachers_RowValidating;
                dgv_Teachers.RowValidated -= Dgv_Teachers_RowValidated;

                SaveTeacherToDb();
                if (this._callBackSubform.BackAction == Share.Action.RecoveryAdd)
                {
                    object[] data = new object[] { dgv_Teachers.Rows[e.RowIndex].Cells[0].Value,
                                               dgv_Teachers.Rows[e.RowIndex].Cells[1].Value,
                                               dgv_Teachers.Rows[e.RowIndex].Cells[2].Value,
                                               dgv_Teachers.Rows[e.RowIndex].Cells[3].Value,
                                               dgv_Teachers.Rows[e.RowIndex].Cells[4].Value};
                    this._callBackSubform.Table = SetUpCurrentDataSub(data);
                }
                else if (this._callBackSubform.BackAction == Share.Action.RecoveryEdit)
                {
                    dgv_Teachers.CurrentRow.Cells[1].ReadOnly = true;
                    dgv_Teachers.CurrentRow.Cells[2].ReadOnly = true;
                    dgv_Teachers.CurrentRow.Cells[3].ReadOnly = true;
                }

                Debug.WriteLine("Rowvalidated");
            }
            catch (Exception ex)
            {

            }
        }

        private void SaveTeacherToDb()
        {
            bs_GiaoVien.EndEdit();
            bs_GiaoVien.ResetCurrentItem();
            this.gIAOVIENTableAdapter.Update(dS.GIAOVIEN);
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

            dgv_Teachers.CurrentRow.Cells[1].ReadOnly = false;
            dgv_Teachers.CurrentRow.Cells[2].ReadOnly = false;
            dgv_Teachers.CurrentRow.Cells[3].ReadOnly = false;

            object[] data = ((DataRowView)bs_GiaoVien.Current).Row.ItemArray;
            this._callBackSubform.Table = SetUpCurrentDataSub(data);

            dgv_Teachers.RowValidating += Dgv_Teachers_RowValidating;
            dgv_Teachers.RowValidated += Dgv_Teachers_RowValidated;

            index2++;
            act_index1++;
            act1[act_index1] = "EditSuccess";
        }

        private void Dgv_Teachers_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                bool isFirst = true;
                int index = 0;
                foreach (DataGridViewCell drv in dgv_Teachers.Rows[e.RowIndex].Cells)
                {
                    if (String.IsNullOrEmpty(drv.Value.ToString()))
                    {
                        e.Cancel = true;

                        dgv_Teachers.Rows[e.RowIndex].ErrorText = "Had error";
                        dgv_Teachers.Rows[e.RowIndex].Cells[index].ErrorText = "Dose not empty.";

                        dgv_Teachers.CurrentCell = drv;
                    }
                    else
                    {
                        dgv_Teachers.Rows[e.RowIndex].Cells[index].ErrorText = null;
                    }

                    if (isFirst)
                    {
                        string valCurrent = drv.FormattedValue.ToString();

                        if (String.IsNullOrEmpty(drv.Value.ToString()))
                        {
                            e.Cancel = true;

                            dgv_Teachers.Rows[e.RowIndex].ErrorText = "Had error";
                            dgv_Teachers.Rows[e.RowIndex].Cells[index].ErrorText = "Dose not empty.";

                            dgv_Teachers.CurrentCell = drv;
                        }
                        else
                        {
                            if (SqlRequestFunction.StudentIsExist(valCurrent))
                            {
                                dgv_Teachers.Rows[e.RowIndex].ErrorText = "Had error";
                                dgv_Teachers.Rows[e.RowIndex].Cells[index].ErrorText = "Teacher Code Is Exists.";
                                dgv_Teachers.CurrentCell = drv;
                            }
                            else
                            {
                                dgv_Teachers.Rows[e.RowIndex].Cells[index].ErrorText = null;
                            }
                            
                        }

                        isFirst = false;
                    }
                    index++;
                }
            }
            catch { }
        }
        private void tsmi_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView currentRow = (DataRowView)bs_GiaoVien.Current;

                DataView dt = (DataView)bs_BoDe.List;
                dt.Sort = "MAGV";
                String key1 = (((DataRowView)bs_GiaoVien[bs_GiaoVien.Position])["MAGV"].ToString());
                DataRowView[] rowView = dt.FindRows(new object[] { key1 });

                DataView dt1 = (DataView)bs_GVDK.List;
                dt1.Sort = "MAGV";
                String key2 = (((DataRowView)bs_GiaoVien[bs_GiaoVien.Position])["MAGV"].ToString());
                DataRowView[] rowView1 = dt1.FindRows(new object[] { key2 });

                if (rowView.Length != 0)
                {
                    MessageBox.Show("Không thể xóa giáo viên này. Vì giáo viên này đã lập đề thi.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (rowView1.Length != 0)
                {
                    MessageBox.Show("Không thể xóa giáo viên này. Vì giáo viên đăng ký thi.", "Error",
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
                            bs_GiaoVien.RemoveCurrent();
                            SaveTeacherToDb();

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
                DataView dt = (DataView)bs_GiaoVien.List;
                dt.Sort = "MAGV";
                DataRow dr = cAction.Table.Rows[index2];
                String key = dr.ItemArray[0].ToString();
                MessageBox.Show("a: "+ dr.ItemArray[0], "", MessageBoxButtons.OK);
                int index = dt.Find(key);

                bs_GiaoVien.RemoveAt(index);
                SaveTeacherToDb();

                table2.Rows[index2].Delete();
                this._callBackSubform.FillData(Share.Action.RecoveryAdd, table2);
                act_index1--;
                index2--;
            }
            else if (cAction.BackAction == Share.Action.RecoveryDelete)
            {
                DataView dt = (DataView)bs_GiaoVien.List;
                dt.Sort = "MAGV";
                if (dt.FindRows(cAction.Table.Rows[index2]).Length != 0)
                {
                    MessageBox.Show("Mã giáo viên đã tồn tại.Vui lòng nhập lại!");
                }
                else
                {
                    DataRow dr = cAction.Table.Rows[index2];
                    DataRowView drv = (DataRowView)bs_GiaoVien.AddNew();
                    drv.Row.ItemArray = dr.ItemArray;

                    SaveTeacherToDb();
                    table2.Rows[index2].Delete();
                    this._callBackSubform.FillData(Share.Action.RecoveryDelete, table2);
                    act_index1--;
                    index2--;
                }              
            }
            else if (cAction.BackAction == Share.Action.RecoveryEdit)
            {
                DataView dt = (DataView)bs_GiaoVien.List;
                dt.Sort = "MAGV";
                DataRow dr = cAction.Table.Rows[index2];
                String key = dr.ItemArray[0].ToString();
                int index = dt.Find(key);

                bs_GiaoVien.Position = index;

                DataRowView currentRow = (DataRowView)bs_GiaoVien.Current;

                currentRow.Row.ItemArray = dr.ItemArray;
                SaveTeacherToDb();
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

        private void dgv_Teachers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}