using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace TracNghiem_CSDLPT
{
    public partial class Frm_DangNhap : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Frm_DangNhap()
        {
            InitializeComponent();
        }

        private Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }

        private void barButtonItem1_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(Login));
            if (frm != null) frm.Activate();
            else
            {
                Login login = new Login();
                login.MdiParent = this;
                login.Show();
            }
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(LoginSV));
            if (frm != null) frm.Activate();
            else
            {
                LoginSV loginSV = new LoginSV();
                loginSV.MdiParent = this;
                loginSV.Show();
            }
        }
    }
}