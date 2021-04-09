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
using TracNghiem_CSDLPT.Common;
using TracNghiem_CSDLPT.Share;
using System.Diagnostics;


namespace TracNghiem_CSDLPT
{
    public partial class Frm_MonHoc : DevExpress.XtraEditors.XtraForm
    {
        int vitri = 0;
        private CallBackAction _callAction;
        private DataTable table1 = new DataTable();
        private DataColumn dc;
        private DataColumn dc1;
        private int index1 = -1;
        private String[] act = new string[100];
        private int act_index = -1;
        public Frm_MonHoc()
        {
            InitializeComponent();
            _callAction = new CallBackAction();
            dc = new DataColumn("Mã môn học");
            dc1 = new DataColumn("Tên môn học");
            table1.Columns.Add(dc);
            table1.Columns.Add(dc1);
            DeniceFeatureByRole();
            //bs_MonHoc.CurrentChanged += Bs_MonHoc_CurrentChanged;
            //DeniceFeatureByRole();
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
                
                // TODO: This line of code loads data into the 'dS.GIAOVIEN_DANGKY' table. You can move, or remove it, as needed.
                this.dS.EnforceConstraints = false;

                // TODO: This line of code loads data into the 'dS.BANGDIEM' table. You can move, or remove it, as needed.
                this.bANGDIEMTableAdapter.Connection.ConnectionString = Program.connstr;
                this.bANGDIEMTableAdapter.Fill(this.dS.BANGDIEM);

                this.bODETableAdapter.Connection.ConnectionString = Program.connstr;
                this.bODETableAdapter.Fill(this.dS.BODE);

                this.gIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
                this.gIAOVIEN_DANGKYTableAdapter.Fill(this.dS.GIAOVIEN_DANGKY);
                // TODO: This line of code loads data into the 'dS.MONHOC' table. You can move, or remove it, as needed.
                this.mONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
                this.mONHOCTableAdapter.Fill(this.dS.MONHOC);

                //macn = ((DataRowView)bs_Khoa[0])["MACS"].ToString();*/
            }
        }

        private void mONHOCBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bs_MonHoc.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dS);

        }

        private void DeniceFeatureByRole()
        {

            if (Program.mGroup.Equals("TRUONG"))
            {
                btn_Add.Enabled = btn_Delete.Enabled = btn_Edit.Enabled = btn_Thoat.Enabled = btn_Write.Enabled = btn_Reset.Enabled = false;
            }
        }

        private void Frm_MonHoc_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dS.BODE' table. You can move, or remove it, as needed.
            // TODO: This line of code loads data into the 'dS.GIAOVIEN_DANGKY' table. You can move, or remove it, as needed.
            this.dS.EnforceConstraints = false;

            // TODO: This line of code loads data into the 'dS.BANGDIEM' table. You can move, or remove it, as needed.
            this.bANGDIEMTableAdapter.Connection.ConnectionString = Program.connstr;
            this.bANGDIEMTableAdapter.Fill(this.dS.BANGDIEM);

            this.bODETableAdapter.Connection.ConnectionString = Program.connstr;
            this.bODETableAdapter.Fill(this.dS.BODE);

            this.gIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
            this.gIAOVIEN_DANGKYTableAdapter.Fill(this.dS.GIAOVIEN_DANGKY);
            // TODO: This line of code loads data into the 'dS.MONHOC' table. You can move, or remove it, as needed.
            this.mONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
            this.mONHOCTableAdapter.Fill(this.dS.MONHOC);

            this.dS.EnforceConstraints = false;

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

            if (bs_MonHoc.Count == 0)
            {
                btn_Delete.Enabled = false;
            }
            btn_Write.Enabled = false;
        }

        private void btn_Add_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.btn_Thoat.Tag = "ADD";
            this.btn_Write.Tag = "ADD";

            SetUpButtonForAction();
        }

        private void btn_Edit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.btn_Write.Tag = "EDIT";
            this.btn_Thoat.Tag = "EDIT";
            vitri = bs_MonHoc.Position;
            SetUpButtonForAction();
        }

        public void SetUpButtonForAction()
        {
            this.groupBox1.Enabled = true;
            this.btn_Add.Enabled = false;
            this.btn_Edit.Enabled = false;
            this.btn_Delete.Enabled = false;
            this.btn_Write.Enabled = true;

            if (this.btn_Write.Tag.Equals("ADD"))
            {
                bs_MonHoc.AddNew();
            }
            if (this.btn_Write.Tag.Equals("EDIT"))
            {
                txtMAMH.Enabled = false;
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

                    if(!addSucess)
                    {
                        MessageBox.Show("Lỗi", "", MessageBoxButtons.OK);
                    }

                    if (addSucess)
                    {
                        WriteToDB();
                        index1++;
                        act_index++;
                        act[act_index] = "AddSuccess";
                        gcMonHoc.Enabled = true;
                        btn_Add.Enabled = btn_Edit.Enabled = btn_Delete.Enabled = btn_Reload.Enabled = btn_Thoat.Enabled = true;
                        btn_Write.Enabled = false;                                       
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
                        gcMonHoc.Enabled = true;
                        btn_Add.Enabled = btn_Edit.Enabled = btn_Delete.Enabled = btn_Reload.Enabled = btn_Thoat.Enabled = true;
                        btn_Write.Enabled = false;
                        //DataTable dt = SetUpCurrentData(((DataRowView)bs_MonHoc.Current).Row.ItemArray);
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
            bs_MonHoc.EndEdit();
            bs_MonHoc.ResetCurrentItem();
            this.mONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
            this.mONHOCTableAdapter.Update(this.dS.MONHOC);
        }

        public bool ValidateEmpty()
        {
            if (txtMAMH.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Vui lòng nhập mã môn học.", "", MessageBoxButtons.OK);
                txtMAMH.Focus();
                return false;
            }

            if (txtTENMH.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Vui lòng nhập tên môn học.", "", MessageBoxButtons.OK);
                txtTENMH.Focus();
                return false;
            }

            return true;
        }

        public bool Add()
        {
            bool isEmpty = ValidateEmpty();

            if (!isEmpty)
                return false;
            if (SqlRequestFunction.CourseIsExists(txtMAMH.Text.Trim()))
            {
                MessageBox.Show("Mã môn học đã tồn tại.", "", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                object[] data = new object[] { txtMAMH.Text, txtTENMH.Text };
                this._callAction.FillData(Share.Action.RecoveryAdd, SetUpCurrentData(data));
                return true;
            }
        }

        public bool Edit()
        {
            bool isEmpty = ValidateEmpty();

            if (!isEmpty)
                return false;

            DataRowView currentRow = (DataRowView)bs_MonHoc.Current;

            if (currentRow != null)
            {            
                    this._callAction.FillData(Share.Action.RecoveryEdit, SetUpCurrentData(currentRow.Row.ItemArray));
                    object[] data = new object[] { txtMAMH.Text, txtTENMH.Text };
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
                int index = bs_MonHoc.Find("MAMH", dr.ItemArray[0]);
                bs_MonHoc.RemoveAt(index);
                WriteToDB();
                table1.Rows[index1].Delete();
                this._callAction.FillData(Share.Action.RecoveryAdd, table1);
                act_index--;
                index1--;
            }
            else if (cAction.BackAction == Share.Action.RecoveryDelete)
            {
                DataRow dr = cAction.Table.Rows[index1];
                DataRowView drv = (DataRowView)bs_MonHoc.AddNew();
                drv.Row.ItemArray = dr.ItemArray;
                WriteToDB();
                table1.Rows[index1].Delete();
                this._callAction.FillData(Share.Action.RecoveryDelete, table1);
                act_index--;
                index1--;
            }
            else if (cAction.BackAction == Share.Action.RecoveryEdit)
            {
                DataRow dr = cAction.Table.Rows[index1];
                bs_MonHoc.Position = bs_MonHoc.Find("MAMH", dr.ItemArray[0]);

                DataRowView currentRow = (DataRowView)bs_MonHoc.Current;

                currentRow.Row.ItemArray = dr.ItemArray;
                WriteToDB();
                table1.Rows[index1].Delete();
                this._callAction.FillData(Share.Action.RecoveryEdit, table1);
                act_index--;
                index1--;
            }

            if(index1 == -1)
            {
                this._callAction.Reset();
                this.Refresh();
            }
            
        }

        private void btn_Delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String mamh = "";
            DataView dt = (DataView)bs_GVDK.List;
            dt.Sort = "MAMH";
            String key1 = (((DataRowView)bs_MonHoc[bs_MonHoc.Position])["MAMH"].ToString());
            DataRowView[] rowView = dt.FindRows(new object[] { key1 });
            if (rowView.Length != 0)
            {
                MessageBox.Show("Không thể xóa môn học này vì có trong giáo viên đăng ký", "", MessageBoxButtons.OK);
                return;
            }

            DataView dt1 = (DataView)bs_BoDe.List;
            dt1.Sort = "MAMH";
            String key2 = (((DataRowView)bs_MonHoc[bs_MonHoc.Position])["MAMH"].ToString());
            DataRowView[] rowView1 = dt1.FindRows(new object[] { key1 });
            if (rowView.Length != 0)
            {
                MessageBox.Show("Không thể xóa môn học này vì có trong bộ đề", "", MessageBoxButtons.OK);
                return;
            }

            DataView dt2 = (DataView)bs_BangDiem.List;
            dt2.Sort = "MAMH";
            String key3 = (((DataRowView)bs_MonHoc[bs_MonHoc.Position])["MAMH"].ToString());
            DataRowView[] rowView2 = dt2.FindRows(new object[] { key1 });
            if (rowView.Length != 0)
            {
                MessageBox.Show("Không thể xóa môn học này vì có trong bảng điểm", "", MessageBoxButtons.OK);
                return;
            }

            if (MessageBox.Show("Bạn có thật sự muốn xóa môn học này không??", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                DataRowView currentRow = (DataRowView)bs_MonHoc.Current;
                this._callAction.FillData(Share.Action.RecoveryDelete, SetUpCurrentData(currentRow.Row.ItemArray));
                bs_MonHoc.RemoveCurrent();
                WriteToDB();
                index1++;
                act_index++;
                act[act_index] = "DeleteSuccess";
                if (bs_MonHoc.Count == 0) btn_Delete.Enabled = false;
            }           
        }

        private void btn_Reload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.mONHOCTableAdapter.Fill(this.dS.MONHOC);
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
            this.mONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
            this.mONHOCTableAdapter.Fill(this.dS.MONHOC);
        }
    }
}