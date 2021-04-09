using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using TracNghiem_CSDLPT.Share;

namespace TracNghiem_CSDLPT.Common
{
    public static class SqlRequestFunction
    {
        /// <summary>
        /// Execuse query  by SqlCommand
        /// </summary>
        /// <param name="strQuery"></param>
        /// <returns>Results by DataReader</returns>
        public static SqlDataReader ExecSqlDataReader(String strQuery)
        {
            SqlDataReader myreader;
            SqlCommand sqlcmd = new SqlCommand(strQuery, Program.conn);
            sqlcmd.CommandType = CommandType.Text;
            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            try
            {
                myreader = sqlcmd.ExecuteReader(); return myreader;

            }
            catch (SqlException ex)
            {
                Program.conn.Close(); ;
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public static List<object[]> GetListTeacherHadNotAccount()
        {
            string query = "Exec sp_GetListTeacherHadNotAccount";

            SqlDataReader reader = ExecSqlDataReader(query);

            List<object[]> list = new List<object[]>();

            while (reader.Read())
            {
                object[] info = new object[]
                {
                   reader.GetString(0),
                   reader.GetString(1),
                };

                list.Add(info);
            }
            reader.Close(); // <- too easy to forget
            reader.Dispose(); // <- too easy to forget

            return list;
        }

        public static String GetTenKhoa(String maKh)
        {
            String query = "Exec sp_getTenKhoa '" + maKh + "'";
            SqlDataReader reader = ExecSqlDataReader(query);

            while (reader.Read())
            {
                String name = reader.GetString(0);
                reader.Close(); // <- too easy to forget
                reader.Dispose(); // <- too easy to forget

                return name;
            }
            return null;
        }

        public static String GetTenLop(String maLop)
        {
            String query = "Exec sp_getTenLop '" + maLop + "'";
            SqlDataReader reader = ExecSqlDataReader(query);

            while (reader.Read())
            {
                String name = reader.GetString(0);
                reader.Close(); // <- too easy to forget
                reader.Dispose(); // <- too easy to forget

                return name;
            }
            return null;
        }

        public static String GetTenMH(String maMH)
        {
            String query = "Exec sp_getMonHoc '" + maMH + "'";
            SqlDataReader reader = ExecSqlDataReader(query);

            while (reader.Read())
            {
                String name = reader.GetString(0);
                reader.Close(); // <- too easy to forget
                reader.Dispose(); // <- too easy to forget

                return name;
            }
            return null;
        }

        public static StudentInfo GetStudentInfo(String studentCode)
        {
            String query = "Exec sp_GetInfoStudent '" + studentCode + "'";
            SqlDataReader reader = ExecSqlDataReader(query);

            while(reader.Read())
            {
                StudentInfo studentInfo = new StudentInfo
                {
                    FullName = reader.GetString(0),
                    ClassCode = reader.GetString(1),
                    ClassName = reader.GetString(2)
                };

                reader.Close(); // <- too easy to forget
                reader.Dispose(); // <- too easy to forget

                return studentInfo;
            }
            return null;
        }

        public static String GetRegisterExamed(String maMH, String maLop, int lan)
        {
            String query = "Exec sp_RegisterExamed '" + maLop + "', '" + maMH + "', " + lan;
            SqlDataReader reader = ExecSqlDataReader(query);

            while (reader.Read())
            {
                
                String daThi = reader.GetString(0);

                reader.Close(); // <- too easy to forget
                reader.Dispose(); // <- too easy to forget

                return daThi;
            }
            return null;
        }

        public static List<Transcript> GetTranscript(String classCode, String courseCode, int timesStep)
        {
            String query = "Exec sp_GetTranscript '" + classCode + "', '" + courseCode + "', " + timesStep;
            SqlDataReader reader = ExecSqlDataReader(query);

            List<Transcript> listTranscript = new List<Transcript>();

            while (reader.Read())
            {
                String sc = reader.GetString(0);
                String fn = reader.GetString(1);
                double m = 0.0;
                try
                {
                    m = reader.GetDouble(2);
                }
                catch (Exception)
                {

                }

                Transcript transcript = new Transcript
                {
                    StudentCode = sc,
                    FullName = fn,
                    Marks = (float)m
                };

                listTranscript.Add(transcript);
            }

            reader.Close(); // <- too easy to forget
            reader.Dispose(); // <- too easy to forget

            return listTranscript;
        }

        public static int CreateAccount(String loginName, String userName, String password, String role)
        {
            String query = "Exec sp_TaoTaiKhoan '" + loginName + "', '" + password + "', '" + userName + "', '" + role + "'";

            SqlDataReader myreader;
            SqlCommand sqlcmd = new SqlCommand(query, Program.conn);
            sqlcmd.CommandType = CommandType.Text;
            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            try
            {
                myreader = sqlcmd.ExecuteReader();
                return 1;
            }
            catch (SqlException ex)
            {
                Program.conn.Close(); ;
                Debug.WriteLine(ex.Message);
                return ex.Number;
            }
        }

        public static List<ExamTest> GetQuestionForTestExam(String courseCode, String level, int quantity)
        {
            String query = "Exec sp_GetQuestion '" + courseCode + "', '" + level + "', " + quantity;
            SqlDataReader reader = ExecSqlDataReader(query);

            List<ExamTest> listExam = new List<ExamTest>();

            while (reader.Read())
            {
                ExamTest exam = new ExamTest
                {
                    QuestionCode = reader.GetInt32(0),
                    QuestionContent = reader.GetString(1),
                    A = reader.GetString(2),
                    B = reader.GetString(3),
                    C = reader.GetString(4),
                    D = reader.GetString(5),
                    TrueAnswer = reader.GetString(6),
                    YourAnswer = String.Empty
                };
                listExam.Add(exam);
            }

            reader.Close(); // <- too easy to forget
            reader.Dispose(); // <- too easy to forget

            return listExam;
        }

        public static bool IsEnoughQuestion(String courseCode, String level, int quantity)
        {
            String val = String.Empty;
            bool isEnough = true;

            String query = "Exec sp_GetQuestion '" + courseCode + "', '" + level + "', '" + quantity + "'";
            SqlDataReader reader = ExecSqlDataReader(query);

            try
            {
                if (reader.Read())
                {
                    int temp = reader.GetInt32(0);
                }
            }
            catch (Exception)
            {
                isEnough = false;
            }

            reader.Close(); // <- too easy to forget
            reader.Dispose(); // <- too easy to forget

            return isEnough;
        }

        public static void Logout(String loginName)
        {
            string query = "Exec sp_LogOut '" + loginName + "'";

            SqlDataReader reader = ExecSqlDataReader(query);
            if (reader != null)
            {
                reader.Close(); // <- too easy to forget
                reader.Dispose(); // <- too easy to forget
            }
        }

        public static bool DepartmentIsExist(String departmentCode)
        {
            return CodeIsExist("sp_CheckDepartmentExists", departmentCode);
        }

        public static bool HasBeenExamined(String studentCode, String courceCode, int time)
        {
            String query = "Exec sp_HasBeenExamined '" + studentCode + "', '" + courceCode + "', '" + time + "'";
            SqlDataReader reader = ExecSqlDataReader(query);

            while (reader.Read())
            {
                int result = reader.GetInt32(0);

                if (result == 1)
                {
                    reader.Close(); // <- too easy to forget
                    reader.Dispose(); // <- too easy to forget
                    return true;
                }
            }
            reader.Close(); // <- too easy to forget
            reader.Dispose(); // <- too easy to forget
            return false;

        }

        public static bool ClassIsExist(String classCode)
        {
            return CodeIsExist("sp_CheckClassExists", classCode);
        }

        public static bool CauHoiExist(String questionCode)
        {
            return CodeIsExist("sp_CheckQuestionExists", questionCode);
        }

        public static bool StudentIsExist(String studentCode)
        {

            return CodeIsExist("sp_CheckStudentExists", studentCode);
        }

        public static bool TeacherIsExists(String teacherCode)
        {
            return CodeIsExist("sp_CheckTecherExists", teacherCode);
        }

        public static bool CourseIsExists(String courceCode)
        {
            return CodeIsExist("sp_CheckCourseExists", courceCode);
        }

        public static bool RegisterIsExists(String classCode, String courseCode, int time)
        {
            String code = classCode + "', '" + courseCode + "', '" + time;
            return CodeIsExist("sp_CheckRegisterIsExists", code);
        }

        private static bool CodeIsExist(String sp, String code)
        {
            String query = "Exec " + sp + " '" + code + "'";
            SqlDataReader reader = ExecSqlDataReader(query);

            while(reader.Read())
            {
                int result = reader.GetInt32(0);

                if (result == 1)
                {
                    reader.Close(); // <- too easy to forget
                    reader.Dispose(); // <- too easy to forget
                    return true;
                }
            }       
            reader.Close(); // <- too easy to forget
            reader.Dispose(); // <- too easy to forget
            return false;
        }
    }
}
