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
using DevExpress.XtraReports.UI;

namespace TracNghiem_CSDLPT
{
    public partial class Frm_Report : DevExpress.XtraEditors.XtraForm
    {
        String coso = "";
        public Frm_Report()
        {
            InitializeComponent();
            this.cmbCoSo.DataSource = Program.bds_ListBrand;
            this.cmbCoSo.DisplayMember = "TENCOSO";
            this.cmbCoSo.ValueMember = "TENSERVER";
            this.cmbCoSo.SelectedIndex = Program.mChinhanh;
            //macn = ((DataRowView)bs_Khoa[0])["MACS"].ToString();

            if (Program.mGroup == "TRUONG")
            {
                this.cmbCoSo.Enabled = true;
            }
            else
            {
                this.cmbCoSo.Enabled = false;
            }
        }

        private void Frm_Report_Load(object sender, EventArgs e)
        {
            this.cmbCoSo.DataSource = Program.bds_ListBrand;
            this.cmbCoSo.DisplayMember = "TENCOSO";
            this.cmbCoSo.ValueMember = "TENSERVER";
            this.cmbCoSo.SelectedIndex = Program.mChinhanh;
            //macn = ((DataRowView)bs_Khoa[0])["MACS"].ToString();

            if (Program.mGroup == "TRUONG")
            {
                this.cmbCoSo.Enabled = true;
            }
            else
            {
                this.cmbCoSo.Enabled = false;
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
                
            }
        }

        private void btn_Preview_Click(object sender, EventArgs e)
        {
            if (cmbCoSo.Text.ToString().Equals("CO SO 1"))
            {
                coso = "CS1";
            }
            else
            {
                coso = "CS2";
            }
            if (DateFrom.Value > DateTo.Value)
            {
                MessageBox.Show("Ngày trước phải nhỏ hơn hoặc bằng ngày sau.", "", MessageBoxButtons.OK);
                return;
            }

            try
            {
                this.sp_GetReportTableAdapter.Fill(this.dS.sp_GetReport
                    , coso, DateFrom.Value
                    , DateTo.Value);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            if (cmbCoSo.Text.ToString().Equals("CO SO 1"))
            {
                coso = "CS1";
            }
            else
            {
                coso = "CS2";
            }
            if (DateFrom.Value > DateTo.Value)
            {
                MessageBox.Show("Ngày trước phải nhỏ hơn hoặc bằng ngày sau.", "", MessageBoxButtons.OK);
                return;
            }

            Xrp_DanhSachDangKy xrp_dsDK = new Xrp_DanhSachDangKy(coso
                    , DateFrom.Value.ToShortDateString()
                    , DateTo.Value.ToShortDateString());
            xrp_dsDK.Xrptitle.Text = "DANH SÁCH ĐĂNG KÝ THI TRẮC NGHIỆM CƠ SỞ " + coso + " TỪ NGÀY " + DateFrom.Value.ToString("dd-MM-yyyy") + " ĐẾN NGÀY " + DateTo.Value.ToString("dd-MM-yyyy");
            xrp_dsDK.GVLap.Text = Program.mHoten;
            ReportPrintTool tool = new ReportPrintTool(xrp_dsDK);

            tool.ShowPreviewDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}