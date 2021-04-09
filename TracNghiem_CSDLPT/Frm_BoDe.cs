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
using TracNghiem_CSDLPT.Share;
using TracNghiem_CSDLPT.Common;

namespace TracNghiem_CSDLPT
{
    public partial class Frm_BoDe : DevExpress.XtraEditors.XtraForm
    {
        int vitri = 0;
        string magv = "";
        string magv1 = "";
        int mmh = 0;
        int t_do = 0;
        int dapAn = 0;

        private string x = "";
        private string y = "";

        private string x1 = "";
        private string y1 = "";


        private CallBackAction _callAction;
        private DataTable table1 = new DataTable();
        private int index1 = -1;
        private String[] act = new string[100];
        private int act_index = -1;

        public Frm_BoDe()
        {
            InitializeComponent();
            _callAction = new CallBackAction();

            DataColumn[] dc = new DataColumn[]
            {
                new DataColumn("Câu hỏi"), new DataColumn("Mã môn học")
                ,new DataColumn("Trình độ"), new DataColumn("Nội dung")
                ,new DataColumn("A"), new DataColumn("B")
                ,new DataColumn("C"), new DataColumn("D")
                ,new DataColumn("Đáp Án"), new DataColumn("Mã giáo viên")
            };
            

            table1.Columns.AddRange(dc);
            table1.Columns[0].DataType = typeof(Int32);

            DeniceFeatureByRole();
            SetUpTrinhDo();
            JustNumber(txtCH);
        }

        public void JustNumber(TextBox textBox)
        {
            textBox.KeyPress += (sender, e) =>
            {
                if (e.KeyChar < '0' || e.KeyChar > '9')
                    e.Handled = true;
            };
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
                MessageBox.Show("Lỗi kết nối về chi nhánh mới", "", MessageBoxButtons.OK);
            }
            else
            {
                this.dS.EnforceConstraints = false;
                this.bODETableAdapter.Connection.ConnectionString = Program.connstr;
                this.bODETableAdapter.Fill(this.dS.BODE);

                this.dS.EnforceConstraints = false;
                this.gIAOVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                this.gIAOVIENTableAdapter.Fill(this.dS.GIAOVIEN);
                // TODO: This line of code loads data into the 'dS.MONHOC' table. You can move, or remove it, as needed.
                this.dS.EnforceConstraints = false;
                this.mONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
                this.mONHOCTableAdapter.Fill(this.dS.MONHOC);
            }
        }

        private void bODEBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bs_BoDe.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dS);

        }

        private void Frm_BoDe_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dS.GIAOVIEN' table. You can move, or remove it, as needed.
            this.dS.EnforceConstraints = false;
            this.gIAOVIENTableAdapter.Connection.ConnectionString = Program.connstr;
            this.gIAOVIENTableAdapter.Fill(this.dS.GIAOVIEN);
            // TODO: This line of code loads data into the 'dS.MONHOC' table. You can move, or remove it, as needed.
            this.dS.EnforceConstraints = false;
            this.mONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
            this.mONHOCTableAdapter.Fill(this.dS.MONHOC);
            // TODO: This line of code loads data into the 'dS.BODE' table. You can move, or remove it, as needed.
            this.dS.EnforceConstraints = false;
            this.bODETableAdapter.Connection.ConnectionString = Program.connstr;
            this.bODETableAdapter.Fill(this.dS.BODE);

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

            if (bs_BoDe.Count == 0)
            {
                btn_Delete.Enabled = false;
            }
            set();
        }

        public void set()
        {
            btn_Write.Enabled = false;
            this.btn_Reset.Enabled = true;
            this.groupBox1.Enabled = false;
            this.txtMH1.Visible = this.txtTrinhDo1.Visible = true;
            DataRowView current = (DataRowView)bs_BoDe.Current;

            String x = current["MAMH"].ToString();
            String nameMH = SqlRequestFunction.GetTenMH(x);
            this.txtMH1.Text = nameMH;

            String z = current["TRINHDO"].ToString();
            String TrinhDo = GetLevelName(z);
            this.txtTrinhDo1.Text = TrinhDo;
        }

        public String GetCourseName(String courseCode)
        {
            DataView dt = (DataView)bs_MonHoc.List;
            dt.Sort = "MAMH";

            DataRowView rowView = dt.FindRows(courseCode)[0];

            return rowView.Row.ItemArray[1].ToString();
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


        public void SetUpTrinhDo()
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

            cmbTrinhDo.DataSource = tableLevel;
            cmbTrinhDo.ValueMember = "Symbol";
            cmbTrinhDo.DisplayMember = "Name";

        }

        private void btn_Add_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.btn_Thoat.Tag = "ADD";
            this.btn_Write.Tag = "ADD";
            this.txtMH1.Visible = false;
            this.txtTrinhDo1.Visible = false;
            SetUpButtonForAction();
        }

        public void SetUpButtonForAction()
        {
            //this.cmbMAKHOA.Enabled = true;
            this.btn_Add.Enabled = false;
            this.btn_Edit.Enabled = false;
            this.btn_Delete.Enabled = false;
            this.btn_Write.Enabled = true;
            this.btn_Reload.Enabled = false;
            this.btn_Reset.Enabled = false;
            this.groupBox1.Enabled = true;
            
            if (this.btn_Write.Tag.Equals("ADD"))
            {
                bs_BoDe.AddNew();
            }
            if (Program.mGroup.Equals("GIANGVIEN"))
            {
                cmbMaGV.Text = magv;
                cmbMaGV.Enabled = false;
            }

            this.txtMH1.Text = "";
            this.txtTrinhDo1.Text = "";
        }

        private void btn_Edit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.btn_Write.Tag = "EDIT";
            this.btn_Thoat.Tag = "EDIT";

            vitri = bs_BoDe.Position;


            DataRowView current = (DataRowView)bs_BoDe.Current;


            if (Program.mGroup.Equals("GIANGVIEN"))
            {
                magv1 = ((DataRowView)bs_BoDe[vitri])["MAGV"].ToString();
                string m = magv1.Trim();
                if (m == magv)
                {
                    this.gcBoDe.Enabled = false;
                    this.btn_Add.Enabled = false;
                    this.btn_Edit.Enabled = false;
                    this.btn_Delete.Enabled = false;
                    this.btn_Write.Enabled = true;
                    this.btn_Reload.Enabled = false;
                    this.btn_Reset.Enabled = false;
                    this.groupBox1.Enabled = true;
                    this.txtCH.Enabled = false;

                    x = current["MAMH"].ToString();
                    y = current["TRINHDO"].ToString();

                    mmh = 0;
                    t_do = 0;
                    dapAn = 0;
                }                     
                else
                {
                    MessageBox.Show("Bạn không thể sửa câu hỏi này!!!", "", MessageBoxButtons.OK);
                }
            }else
            {
                this.gcBoDe.Enabled = false;
                this.btn_Add.Enabled = false;
                this.btn_Edit.Enabled = false;
                this.btn_Delete.Enabled = false;
                this.btn_Write.Enabled = true;
                this.btn_Reload.Enabled = false;
                this.btn_Reset.Enabled = false;
                this.groupBox1.Enabled = true;
                this.txtCH.Enabled = false;

                x = current["MAMH"].ToString();
                y = current["TRINHDO"].ToString();

                mmh = 0;
                t_do = 0;
                dapAn = 0;
            }

        }

        private bool ValidateEmpty()
        {
            bool validate = true;

            if (txtCH.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Vui lòng nhập mã câu hỏi!!!", "", MessageBoxButtons.OK);
                txtCH.Focus();
                validate = false;
                return validate;
            }
            if (cmbMonHoc.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Vui lòng chọn mã môn học!!!", "", MessageBoxButtons.OK);
                this.ActiveControl = this.cmbMonHoc;
                validate = false;
                return validate;
            }
            if (cmbMaGV.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Vui lòng chọn mã giáo viên!!!", "", MessageBoxButtons.OK);
                this.ActiveControl = this.cmbMaGV;
                validate = false;
                return validate;
            }
            if (cmbTrinhDo.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Vui lòng chọn trình độ!!!", "", MessageBoxButtons.OK);
                this.ActiveControl = this.cmbTrinhDo;
                validate = false;
                return validate;
            }
            if (cmbDapAn.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Vui lòng chọn đáp án!!!", "", MessageBoxButtons.OK);
                this.ActiveControl = this.cmbDapAn;
                validate = false;
                return validate;
            }
            if (txtNoiDung.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Vui lòng nhập nội dung câu hỏi!!!", "", MessageBoxButtons.OK);
                txtCH.Focus();
                validate = false;
                return validate;
            }
            if (txtA.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Vui lòng nhập đáp án A!!!", "", MessageBoxButtons.OK);
                txtA.Focus();
                validate = false;
                return validate;
            }
            if (txtB.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Vui lòng nhập đáp án B!!!", "", MessageBoxButtons.OK);
                txtB.Focus();
                validate = false;
                return validate;
            }
            if (txtC.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Vui lòng nhập đáp án C!!!", "", MessageBoxButtons.OK);
                txtC.Focus();
                validate = false;
                return validate;
            }
            if (txtD.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Vui lòng nhập đáp án D!!!", "", MessageBoxButtons.OK);
                txtD.Focus();
                validate = false;
                return validate;
            }
            return validate;
        }

        public bool Add()
        {
            bool isEmpty = ValidateEmpty();

            if (!isEmpty)
                return false;
            if (SqlRequestFunction.CauHoiExist(txtCH.Text.Trim()))
            {
                MessageBox.Show("Mã môn học đã tồn tại.", "", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                object[] data = new object[] { Int32.Parse(txtCH.Text.ToString()), cmbMonHoc.SelectedValue.ToString(), cmbTrinhDo.SelectedValue.ToString(), txtNoiDung.Text, txtA.Text, txtB.Text, txtC.Text, txtD.Text, cmbDapAn.Text, cmbMaGV.Text };
                this._callAction.FillData(Share.Action.RecoveryAdd, SetUpCurrentData(data));
                return true;
            }      
        }

        public bool Edit()
        {
            bool isEmpty = ValidateEmpty();

            if (!isEmpty)
                return false;           

            DataRowView currentRow = (DataRowView)bs_BoDe.Current;
            if (currentRow != null)
            {
                currentRow["MAMH"] = x;
                currentRow["TRINHDO"] = y;
                this._callAction.FillData(Share.Action.RecoveryEdit, SetUpCurrentData(currentRow.Row.ItemArray));
                if (mmh == 0)
                {
                    x1 = this.cmbMonHoc.Text.ToString();
                }
                else
                {
                    x1 = this.cmbMonHoc.SelectedValue.ToString();
                    currentRow["MAMH"] = x1;
                }

                if (t_do == 0)
                {
                    y1 = this.cmbTrinhDo.Text.ToString();
                }
                else
                {
                    y1 = this.cmbTrinhDo.SelectedValue.ToString();
                    currentRow["TRINHDO"] = y1;
                }
                object[] data = new object[] { Int32.Parse(txtCH.Text.ToString()), x1, y1, txtNoiDung.Text, txtA.Text, txtB.Text, txtC.Text, txtD.Text, cmbDapAn.Text, cmbMaGV.Text };
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

        public void WriteToDB()
        {
            bs_BoDe.EndEdit();
            DataRowView current = (DataRowView)bs_BoDe.Current;
            current["MAMH"] = this.cmbMonHoc.SelectedValue;
            current["TRINHDO"] = this.cmbTrinhDo.SelectedValue;
            bs_BoDe.ResetCurrentItem();
            this.bODETableAdapter.Connection.ConnectionString = Program.connstr;
            this.bODETableAdapter.Update(this.dS.BODE);
        }

        public void WriteToDBEdit()
        {
            bs_BoDe.EndEdit();
            bs_BoDe.ResetCurrentItem();
            this.bODETableAdapter.Connection.ConnectionString = Program.connstr;
            this.bODETableAdapter.Update(this.dS.BODE);
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
                        gcBoDe.Enabled = true;
                        btn_Add.Enabled = btn_Edit.Enabled = btn_Delete.Enabled = btn_Reload.Enabled = btn_Thoat.Enabled = btn_Reset.Enabled = true;
                        btn_Write.Enabled = false;
                        set();
                    }
                }
                else if (this.btn_Write.Tag.Equals("EDIT"))
                {
                    bool editSuccess = Edit();
                    if (editSuccess)
                    {
                        WriteToDBEdit();
                        index1++;
                        act_index++;
                        act[act_index] = "EditSuccess";
                        gcBoDe.Enabled = true;
                        btn_Add.Enabled = btn_Edit.Enabled = btn_Delete.Enabled = btn_Reload.Enabled = btn_Thoat.Enabled = btn_Reset.Enabled = true;
                        btn_Write.Enabled = false;
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
                int index = bs_BoDe.Find("CAUHOI", dr.ItemArray[0]);
                bs_BoDe.RemoveAt(index);
                WriteToDBEdit();
                table1.Rows[index1].Delete();
                this._callAction.FillData(Share.Action.RecoveryAdd, table1);
                act_index--;
                index1--;
                set();
            }
            else if (cAction.BackAction == Share.Action.RecoveryDelete)
            {
                DataRow dr = cAction.Table.Rows[index1];
                DataRowView drv = (DataRowView)bs_BoDe.AddNew();
                drv.Row.ItemArray = dr.ItemArray;
                WriteToDBEdit();
                table1.Rows[index1].Delete();
                this._callAction.FillData(Share.Action.RecoveryDelete, table1);
                act_index--;
                index1--;
                set();
            }
            else if (cAction.BackAction == Share.Action.RecoveryEdit)
            {
                DataRow dr = cAction.Table.Rows[index1];
                bs_BoDe.Position = bs_BoDe.Find("CAUHOI", dr.ItemArray[0]);

                DataRowView currentRow = (DataRowView)bs_BoDe.Current;
                currentRow.Row.ItemArray = dr.ItemArray;
                WriteToDBEdit();
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

        private void btn_Delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bs_BoDe.Position;

            if(Program.mGroup == "COSO")
            {
                String mach = "";
                if (MessageBox.Show("Bạn có thật sự muốn xóa câu hỏi không??", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        DataRowView currentRow = (DataRowView)bs_BoDe.Current;
                        mach = (((DataRowView)bs_BoDe[bs_BoDe.Position])["CAUHOI"].ToString());
                        this._callAction.FillData(Share.Action.RecoveryDelete, SetUpCurrentData(currentRow.Row.ItemArray));
                        bs_BoDe.RemoveCurrent();
                        this.bODETableAdapter.Connection.ConnectionString = Program.connstr;
                        this.bODETableAdapter.Update(this.dS.BODE);
                        index1++;
                        act_index++;
                        act[act_index] = "DeleteSuccess";
                        if (bs_BoDe.Count == 0) btn_Delete.Enabled = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xóa câu hỏi. bạn hãy xóa lại \n" + ex.Message, "", MessageBoxButtons.OK);
                        this.bODETableAdapter.Fill(this.dS.BODE);
                        bs_BoDe.Position = bs_BoDe.Find("CAUHOI", mach);
                        return;
                    }
                }
                if (bs_BoDe.Count == 0) btn_Delete.Enabled = false;
            }
            if (Program.mGroup == "GIANGVIEN")
            {
                magv1 = ((DataRowView)bs_BoDe[vitri])["MAGV"].ToString();
                string m = magv1.Trim();
                if (m == magv)
                {
                    String mach = "";
                    //Int32 makh = 0;
                    if (MessageBox.Show("Bạn có thật sự muốn xóa câu hỏi không??", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        try
                        {
                            DataRowView currentRow = (DataRowView)bs_BoDe.Current;
                            mach = (((DataRowView)bs_BoDe[bs_BoDe.Position])["CAUHOI"].ToString());
                            this._callAction.FillData(Share.Action.RecoveryDelete, SetUpCurrentData(currentRow.Row.ItemArray));
                            bs_BoDe.RemoveCurrent();
                            this.bODETableAdapter.Connection.ConnectionString = Program.connstr;
                            this.bODETableAdapter.Update(this.dS.BODE);
                            index1++;
                            act_index++;
                            act[act_index] = "DeleteSuccess";
                            if (bs_BoDe.Count == 0) btn_Delete.Enabled = false;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi xóa câu hỏi. bạn hãy xóa lại \n" + ex.Message, "", MessageBoxButtons.OK);
                            this.bODETableAdapter.Fill(this.dS.BODE);
                            bs_BoDe.Position = bs_BoDe.Find("CAUHOI", mach);
                            return;
                        }
                    }
                    if (bs_BoDe.Count == 0) btn_Delete.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Bạn không có quyền xóa câu hỏi này!!!", "", MessageBoxButtons.OK);
                }
                }
            }

        private void btn_Reload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.bODETableAdapter.Fill(this.dS.BODE);
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

        private void gcBoDe_Click(object sender, EventArgs e)
        {
            set();
        }

        private void cmbMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMH1.Visible = false;
            mmh = 1;
        }

        private void cmbTrinhDo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTrinhDo1.Visible = false;
            t_do = 1;
        }

        private void cmbMonHoc_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            txtMH1.Visible = false;
            mmh = 1;
        }

    }
}