using ScannerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ScannerApp.Report
{
    public partial class VisitorPage : System.Web.UI.Page
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            List<Staff> list = null;
            string client = User.Identity.Name;

            if (User.IsInRole("Admin"))
            {
                list = db.Staffs.Take(100).OrderByDescending(_ => _.ORDER_BY_DERIVED_0).ToList();
            }
            else
            {
                list = db.Staffs.Where(_ => _.job == client).OrderByDescending(_ => _.ORDER_BY_DERIVED_0).ToList();
              //  list = db.Staffs.OrderByDescending(_ => _.ORDER_BY_DERIVED_0).ToList();
            }            

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            var reportDataSource1 = new Microsoft.Reporting.WebForms.ReportDataSource();
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = list;
            ReportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            ReportViewer1.LocalReport.ReportPath = "Report/VisitorReport.rdlc";
            ReportViewer1.LocalReport.Refresh();
        }
    }
}