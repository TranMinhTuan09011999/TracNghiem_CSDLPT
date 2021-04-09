using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.DataAccess.Sql;
using System.Collections.Generic;

namespace TracNghiem_CSDLPT
{
    public partial class Xrp_BangDiem : DevExpress.XtraReports.UI.XtraReport
    {
        public Xrp_BangDiem()
        {
            InitializeComponent();
        }

        public Xrp_BangDiem(String MASV, String MALOP, int LAN)
        {
            InitializeComponent();
            List<QueryParameter> lparam = this.sqlDataSource1.Queries[0].Parameters;

            lparam[0].Value = MASV;
            lparam[1].Value = MALOP;
            lparam[2].Value = LAN;
        }

    }
}
