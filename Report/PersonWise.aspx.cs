using ScannerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ScannerApp.Report
{
    public partial class PersonWise : System.Web.UI.Page
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
            List<AccessRecord> list = null;
            string client = User.Identity.Name;

            if (User.IsInRole("Admin"))
            {
                list = db.AccessRecords.OrderByDescending(_ => _.recordTime).ToList();
            }
            else
            {
                list = (from d in db.Devices
                        join acc in db.AccessRecords on d.sn equals acc.sn
                        where d.client == User.Identity.Name
                        select acc).ToList();
            }

            DateTime dt = DateTime.Now;

            if (!string.IsNullOrEmpty(txtDate.Text))
            {
                dt = Convert.ToDateTime(txtDate.Text);

                list = list.Where(_ => _.recordTime.Date == dt.Date).ToList();
            }

            ReportViewer1.LocalReport.DataSources.Clear();
            var reportDataSource1 = new Microsoft.Reporting.WebForms.ReportDataSource();
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = list.OrderBy(_ => _.phone).OrderBy(_ => _.recordTime);
            ReportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            ReportViewer1.LocalReport.ReportPath = "Report/PersonWise.rdlc";
            ReportViewer1.LocalReport.Refresh();
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}