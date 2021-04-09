using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using TracNghiem_CSDLPT.Account;
using TracNghiem_CSDLPT.Common;

namespace TracNghiem_CSDLPT
{
    public partial class Frm_TN_CSDLPT : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Timer _timer;

        public Frm_TN_CSDLPT()
        {
            InitializeComponent();

            DeniceFeatureByRole();

            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += _timer_Tick;
            _timer.Start();

            /*if(Program.mGroup.Equals("SINHVIEN"))
            {
                lblMAGV.Caption = Program.studentCode;
            }
            else
            {
                lblMAGV.Caption = Program.username;
            }*/
            lblMAGV.Caption = Program.username;
            lblHOTEN.Caption = Program.mHoten;
            lblNHOM.Caption = Program.mGroup;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            DateTime current = DateTime.Now;
            lblTIME.Caption = current.ToShortTimeString();
            lblDATE.Caption = current.ToShortDateString();
        }

        private void DeniceFeatureByRole()
        {
            if (Program.mGroup.Equals("SINHVIEN"))
            {
                btn_Khoa.Enabled = btn_Lop.Enabled = false;
                btn_NhapDe.Enabled = btn_RegisterExam.Enabled = btn_MonHoc.Enabled = false;
                btn_Transcript.Enabled = btn_RegisterReport.Enabled = false;

                btn_ChangePassword.Enabled = btn_DeleteAccount.Enabled = btn_Register.Enabled = false;
            }
            else if (Program.mGroup.Equals("GIANGVIEN"))
            {
                btn_GV.Enabled = btn_SV.Enabled = false;
                btn_Khoa.Enabled = btn_Lop.Enabled = false;
                btn_MonHoc.Enabled = false;
                btn_RegisterReport.Enabled = false;
                btn_Register.Enabled = false;
                btn_Transcript.Enabled = false;
            }
            else if (Program.mGroup.Equals("COSO"))
            {
                btn_Thi.Enabled = false;
            }
            else if (Program.mGroup.Equals("TRUONG"))
            {
                btn_Thi.Enabled = false;
            }
        }

        private void btn_Register_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(Frm_CreateAccount));
            if (frm != null) frm.Activate();
            else
            {
                Frm_CreateAccount frm_CreateAccount = new Frm_CreateAccount();
                //frm_CreateAccount.MdiParent = this;
                frm_CreateAccount.Show();
            }
        }

        private Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }

        private void btn_Logout_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có chắc muốn đăng xuất không?"
                , "Confirmation", MessageBoxButtons.YesNo)
                == DialogResult.Yes)
            {
                try
                {
                    SqlRequestFunction.Logout(Program.mlogin);

                    if (Program.conn.State == ConnectionState.Open)
                    {
                        Program.conn.Close();
                    }

                    this.Dispose();
                }
                catch (SqlException)
                {
                    MessageBox.Show("Không thể đăng xuất. Vui lòng thử lại."
                        , "Lỗi đăng xuất."
                        , MessageBoxButtons.OK
                        , MessageBoxIcon.Error);
                    return;
                }

            }
        }

        private void btn_MonHoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(Frm_MonHoc));
            if (frm != null) frm.Activate();
            else
            {
                Frm_MonHoc frm_Monhoc = new Frm_MonHoc();
                frm_Monhoc.MdiParent = this;
                frm_Monhoc.Show();
            }
        }

        private void btn_Khoa_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(Frm_NhapKhoa));
            if (frm != null) frm.Activate();
            else
            {
                Frm_NhapKhoa frm_Khoa = new Frm_NhapKhoa();
                frm_Khoa.MdiParent = this;
                frm_Khoa.Show();
            }
        }

        private void btn_Lop_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(Frm_NhapLop));
            if (frm != null) frm.Activate();
            else
            {
                Frm_NhapLop frm_Lop = new Frm_NhapLop();
                frm_Lop.MdiParent = this;
                frm_Lop.Show();
            }
        }

        private void btn_NhapDe_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(Frm_BoDe));
            if (frm != null) frm.Activate();
            else
            {
                Frm_BoDe frm_BoDe = new Frm_BoDe();
                frm_BoDe.MdiParent = this;
                frm_BoDe.Show();
            }
        }

        private void btn_RegisterExam_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(Frm_DangKyThi));
            if (frm != null) frm.Activate();
            else
            {
                Frm_DangKyThi frm_DangKyThi = new Frm_DangKyThi();
                frm_DangKyThi.MdiParent = this;
                frm_DangKyThi.Show();
            }
        }

        private void btnThi_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(Frm_CBThi));
            if (frm != null) frm.Activate();
            else
            {
                Frm_CBThi frm_CbThi = new Frm_CBThi();
                frm_CbThi.MdiParent = this;
                frm_CbThi.Show();
            }
        }

        private void btn_Transcript_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(Frm_BangDiem));
            if (frm != null) frm.Activate();
            else
            {
                Frm_BangDiem bd = new Frm_BangDiem();
                bd.MdiParent = this;
                bd.Show();
            }
        }

        private void btn_RegisterReport_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(Frm_Report));
            if (frm != null) frm.Activate();
            else
            {
                Frm_Report bd = new Frm_Report();
                bd.MdiParent = this;
                bd.Show();
            }
        }

        private void btn_ChangePassword_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btn_DeleteAccount_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}