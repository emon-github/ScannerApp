using ScannerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ScannerApp.Report
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                var data = db.AccessRecords.ToList();
                var reportDataSource1 = new Microsoft.Reporting.WebForms.ReportDataSource();
                reportDataSource1.Name = "DataSet1";
                reportDataSource1.Value = data;
                ReportViewer1.LocalReport.DataSources.Add(reportDataSource1);
                ReportViewer1.LocalReport.ReportPath = "Report/Report2.rdlc";

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "Button clicked";
        }
    }
}