using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TracNghiem_CSDLPT.Common;

namespace TracNghiem_CSDLPT
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'tN_CSDLPTDataSet.V_DS_PHANMANH' table. You can move, or remove it, as needed.
            this.v_DS_PHANMANHTableAdapter.Fill(this.tN_CSDLPTDataSet.V_DS_PHANMANH);
            cmb_Brand_GV.SelectedIndex = 1;
            cmb_Brand_GV.SelectedIndex = 0;

        }

        private void tENCOSOComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Program.servername = cmb_Brand_GV.SelectedValue.ToString();

            }
            catch (Exception) { };
        }

        private void btn_Login_GV_Click(object sender, EventArgs e)
        {
            if (txt_UserName.Text.Trim() == "")
            {
                MessageBox.Show("Tài khoản đăng nhập không được rỗng.", "Báo lỗi đăng nhập", MessageBoxButtons.OK);
                txt_UserName.Focus();
                return;
            }
            Program.mlogin = txt_UserName.Text; Program.password = txt_Password.Text;
            if (Program.KetNoi() == 0) return;
            Program.mChinhanh = cmb_Brand_GV.SelectedIndex; 

            Program.bds_ListBrand = bds_ListBrand;

            Program.mloginDN = Program.mlogin;
            Program.passwordDN = Program.password;
            string strLenh = "EXEC SP_THONGTINDANGNHAPGV '" + Program.mloginDN + "'";

            Program.myReader = SqlRequestFunction.ExecSqlDataReader(strLenh);
            if (Program.myReader == null) return;
            Program.myReader.Read();
            Program.username = Program.myReader.GetString(0);     // Lay user name
            if (Convert.IsDBNull(Program.username))
            {
                MessageBox.Show("Giáo viên không tồn tại.", "", MessageBoxButtons.OK);
                return;
            }
            Program.mHoten = Program.myReader.GetString(1);
            Program.mGroup = Program.myReader.GetString(2);
            Program.myReader.Close();
            Program.conn.Close();
            MessageBox.Show("Nhân viên - Nhóm : " + Program.mHoten + " - " + Program.mGroup
                , "Đăng nhập thành công!"
                , MessageBoxButtons.OK, MessageBoxIcon.Information);

            Frm_TN_CSDLPT frm_Tn_csdlpt = new Frm_TN_CSDLPT();
            frm_Tn_csdlpt.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
