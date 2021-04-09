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
using DevExpress.XtraReports.UI;

namespace TracNghiem_CSDLPT
{
    public partial class Frm_BangDiem : DevExpress.XtraEditors.XtraForm
    {
        String coso = "";
        public Frm_BangDiem()
        {
            InitializeComponent();
            grb_Tool.Enabled = false;
        }

        private void Frm_BangDiem_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dS.MONHOC' table. You can move, or remove it, as needed.
            this.dS.EnforceConstraints = false;

            this.mONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
            this.mONHOCTableAdapter.Fill(this.dS.MONHOC);
            // TODO: This line of code loads data into the 'dS.LOP' table. You can move, or remove it, as needed.
            this.lOPTableAdapter.Connection.ConnectionString = Program.connstr;
            this.lOPTableAdapter.Fill(this.dS.LOP);

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

        private void btn_View_Click(object sender, EventArgs e)
        {
            try
            {
                this.sp_GetTranscriptTableAdapter.Fill(this.dS1.sp_GetTranscript
                    , cmb_Class.SelectedValue.ToString(), cmb_Course.SelectedValue.ToString()
                    , (int)nud_TimesStep.Value);
                if (bs_Transcript.Count == 0)
                {
                    lbl_ClassName.Text = "";
                    lbl_CourseName.Text = "";
                    lbl_TimesStep.Text = "";
                    MessageBox.Show("Không tìm thấy bảng điểm với thông tin tương ứng. Vui lòng kiểm tra lại.",
                    "Không tìm thấy!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                lbl_ClassName.Text = cmb_Class.Text;
                lbl_CourseName.Text = cmb_Course.Text;
                lbl_TimesStep.Text = nud_TimesStep.Value.ToString();
                grb_Tool.Enabled = true;
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private DataTable SetUpDataTable(List<Transcript> transcripts)
        {
            DataTable table = new DataTable();

            DataColumn[] dcs = new DataColumn[]
            {
                new DataColumn("STT"), new DataColumn("Mã Sinh Viên")
                ,new DataColumn("Họ tên"), new DataColumn("Điểm")
                ,new DataColumn("Điểm Chữ")
            };

            table.Columns.AddRange(dcs);

            int index = 1;
            foreach (Transcript item in transcripts)
            {
                DataRow row = table.NewRow();

                row.ItemArray = GetData(index++, item);

                table.Rows.Add(row);
            }

            return table;
        }

        private object[] GetData(int index, Transcript trans)
        {
            return new object[]
            {
                index, trans.StudentCode, trans.FullName, trans.Marks, trans.MarksStr
            };
        }

        private void dgv_Transcript_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btn_PrintTranscript_Click(object sender, EventArgs e)
        {
            Xrp_BangDiem xrp_bd = new Xrp_BangDiem(cmb_Class.SelectedValue.ToString(), cmb_Course.SelectedValue.ToString(), (int)nud_TimesStep.Value);
            xrp_bd.titleBD.Text = "DANH SÁCH ĐIỂM SINH VIÊN LỚP " + cmb_Class.Text + " MÔN HỌC " + cmb_Course.Text + " LẦN " + nud_TimesStep.Value.ToString();

            ReportPrintTool tool = new ReportPrintTool(xrp_bd);

            tool.ShowPreviewDialog();
        }

        private void cmbCoSo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCoSo.SelectedValue.ToString() == "System.Data.DataRowView")
            {
                return;
            }

            Program.servername = cmbCoSo.SelectedValue.ToString();
            MessageBox.Show("aa: " + Program.servername, "", MessageBoxButtons.OK);

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

                this.mONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
                this.mONHOCTableAdapter.Fill(this.dS.MONHOC);
                // TODO: This line of code loads data into the 'dS.LOP' table. You can move, or remove it, as needed.
                this.lOPTableAdapter.Connection.ConnectionString = Program.connstr;
                this.lOPTableAdapter.Fill(this.dS.LOP);
            }
        }
    }
}