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

namespace TracNghiem_CSDLPT
{
    public partial class LoginSV : DevExpress.XtraEditors.XtraForm
    {
        public LoginSV()
        {
            InitializeComponent();
        }

        private void LoginSV_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'tN_CSDLPTDataSet.V_DS_PHANMANH' table. You can move, or remove it, as needed.
            this.v_DS_PHANMANHTableAdapter.Fill(this.tN_CSDLPTDataSet.V_DS_PHANMANH);
            cmb_Brand_GV.SelectedIndex = 1;
            cmb_Brand_GV.SelectedIndex = 0;
        }


        private void btn_Login_GV_Click(object sender, EventArgs e)
        {
            if (txt_MASV.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên.", "Báo lỗi đăng nhập", MessageBoxButtons.OK);
                txt_MASV.Focus();
                return;
            }

            //Start login
            Program.studentCode = txt_MASV.Text;
            Program.mlogin = "SV"; Program.password = "123";
            if (Program.KetNoi() == 0) return;
            Program.mChinhanh = cmb_Brand_GV.SelectedIndex;

            Program.bds_ListBrand = bds_ListBrand;

            Program.mloginDN = Program.mlogin;
            Program.passwordDN = Program.password;
            
            string strLenh = "EXEC SP_THONGTINDANGNHAPSV '" + Program.studentCode + "'";

            Program.myReader = SqlRequestFunction.ExecSqlDataReader(strLenh);
            if (Program.myReader == null) return;          
            try
            {
                if (Program.myReader.Read())
                {
                    Program.mHoten = Program.myReader.GetString(1);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Sinh viên không tồn tại.", "", MessageBoxButtons.OK);
                return;
            }
            Program.username = Program.studentCode;     // Lay name
            Program.mGroup = Program.myReader.GetString(2);
            Program.myReader.Close();
            Program.conn.Close();
            MessageBox.Show("Sinh Viên " + Program.mHoten
                , "Đăng nhập thành công!"
                , MessageBoxButtons.OK
                , MessageBoxIcon.Information);

            Frm_TN_CSDLPT frm_Tn_csdlpt = new Frm_TN_CSDLPT();
            frm_Tn_csdlpt.Show();
        }

        private void cmb_Brand_GV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Program.servername = cmb_Brand_GV.SelectedValue.ToString();

            }
            catch (Exception) { };
        }

    }
}