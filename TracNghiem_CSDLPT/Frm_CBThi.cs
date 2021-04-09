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
    public partial class Frm_CBThi : DevExpress.XtraEditors.XtraForm
    {
        public Frm_CBThi()
        {
            InitializeComponent();
        }

        private void SetUp()
        {
            if (Program.mGroup.Equals("SINHVIEN"))
            {
                StudentInfo studentInfo = SqlRequestFunction.GetStudentInfo(Program.username);

                this.lbl_StudentCode.Text = Program.username;
                this.lbl_StudentName.Text = studentInfo.FullName;
                this.lbl_ClassCode.Text = studentInfo.ClassCode;
                this.lbl_ClassName.Text = studentInfo.ClassName;
            }

            this.dtp_DateExam.MinDate = DateTime.Now;
            DateTime currentDateTime = DateTime.Now;
            this.dtp_DateExam.MaxDate = currentDateTime.AddDays(60);

            this.grb_StartExam.Visible = false;

            string x = this.cmb_Course.SelectedValue.ToString();
            String nameMH = SqlRequestFunction.GetTenMH(x);
            this.txtMH.Text = nameMH;
        }

        private void Frm_CBThi_Load(object sender, EventArgs e)
        {          
            this.dS.EnforceConstraints = false;
            // TODO: This line of code loads data into the 'dS.GIAOVIEN_DANGKY' table. You can move, or remove it, as needed.
            this.gIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
            this.gIAOVIEN_DANGKYTableAdapter.Fill(this.dS.GIAOVIEN_DANGKY);
            this.mONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
            this.mONHOCTableAdapter.Fill(this.dS.MONHOC);

            /*this.tbla_GVDK.Connection.ConnectionString = Program.connstr;
            this.tbla_GVDK.Fill(this.ds_TN_CSDLPT.GIAOVIEN_DANGKY);*/

            SetUp();
        }

        private void btn_Find_Click(object sender, EventArgs e)
        {

            if (this.cmb_Course.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("Vui lòng chọn môn thi!!!", "", MessageBoxButtons.OK);
                return;
            }
            DataView dt = (DataView)bs_GVDK.List;

            String key1 = cmb_Course.SelectedValue.ToString();
            String key2 = dtp_DateExam.Value.ToString("dd-MM-yyyy");
            String key3 = nud_TimesStep.Value.ToString();

            dt.Sort = "MAMH, NGAYTHI ,LAN";
            DataRowView[] rowView = dt.FindRows(new object[] { key1, key2, key3 });

            if (rowView.Length != 0)
            {
                dgv_Results.DataSource = SetUpCurrentData(rowView);

                grb_StartExam.Visible = true;
            }
            else
            {
                MessageBox.Show("Môn học này không được đăng ký thi. Vui lòng thông báo đến giáo viên để biết thêm chi tiết"
                    , "Không tìm thấy bài thi."
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Error);

                this.dgv_Results.DataSource = null;
                return;
            }
        }

        private DataTable SetUpCurrentData(DataRowView[] dataRow)
        {
            DataTable table = new DataTable();

            DataColumn[] dcs = new DataColumn[] { new DataColumn("Mã GV"), new DataColumn("Mã môn học")
                                                ,new DataColumn("Mã lớp"), new DataColumn("Trình độ")
                                                ,new DataColumn("Ngày thi"), new DataColumn("Lần thi")
                                                ,new DataColumn("Số câu"), new DataColumn("Thời gian")};

            table.Columns.AddRange(dcs);

            foreach (DataRowView item in dataRow)
            {
                DataRow dr = table.NewRow();
                dr.ItemArray = item.Row.ItemArray;
                table.Rows.Add(dr);
            }

            return table;
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            if (dgv_Results.SelectedRows == null && dgv_Results.Rows.Count > 0)
            {
                dgv_Results.Rows[0].Selected = true;
            }

            DataGridViewRow gridView = dgv_Results.SelectedRows[0];

            TestInfo testInfo = new TestInfo()
            {
                CourseCode = cmb_Course.SelectedValue.ToString(),
                CourseName = cmb_Course.Text,
                StudentCode = lbl_StudentCode.Text,
                StudentName = lbl_StudentName.Text,
                TimesStep = int.Parse(gridView.Cells[5].Value.ToString()),
                TotalTime = int.Parse(gridView.Cells[7].Value.ToString()),
                DateExam = Convert.ToDateTime(gridView.Cells[4].Value),
                ClassName = lbl_ClassName.Text,
                listQuestion = GetListQuestion()
            };

            if (Program.mGroup.Equals("SINHVIEN"))
            {
                if (SqlRequestFunction.HasBeenExamined(testInfo.StudentCode, testInfo.CourseCode, testInfo.TimesStep))
                {
                    MessageBox.Show("Bạn đã thi môn này rồi. Vui lòng kiểm tra lại thông tin."
                        , "Đã kiểm tra."
                        , MessageBoxButtons.OK
                        , MessageBoxIcon.Information);
                    return;
                }
            }

            TestExam testExam = new TestExam(testInfo);

            testExam.ShowDialog();
        }

        private List<ExamTest> GetListQuestion()
        {
            DataGridViewRow gridView = dgv_Results.SelectedRows[0];

      

            return SqlRequestFunction.GetQuestionForTestExam(gridView.Cells[1].Value.ToString()
                                                            , gridView.Cells[3].Value.ToString()
                                                            , int.Parse(gridView.Cells[6].Value.ToString()));
        }

        private void cmb_Course_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMH.Visible = false;
        }
    }
}