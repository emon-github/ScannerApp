using ScannerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ScannerApp.Report
{
    public partial class AccessRecordPage : System.Web.UI.Page
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.dtFrom.Text = DateTime.Now.ToString("yyyy-MM-dd");               
                this.dtTo.Text = DateTime.Now.ToString("yyyy-MM-dd");

                LoadData();
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            List<AccessRecord> list = null;
            string client = User.Identity.Name;

            DateTime dtF = DateTime.Now;
            DateTime dtT = DateTime.Now.AddDays(1);

            if (!string.IsNullOrEmpty(dtFrom.Text))
            {
                dtF = Convert.ToDateTime(dtFrom.Text);                
            }
            if (!string.IsNullOrEmpty(dtTo.Text))
            {
                dtT = Convert.ToDateTime(dtTo.Text).AddDays(1);
            }

            if (User.IsInRole("Admin"))
            {
                list = db.AccessRecords
                    .Where( _ => _.recordTime >= dtF.Date)
                    .Where( _ => _.recordTime <= dtT.Date).OrderByDescending(_ => _.recordTime).ToList();
            }
            else
            {
                list = (from d in db.Devices
                        join acc in db.AccessRecords on d.sn equals acc.sn
                        where d.client == User.Identity.Name
                        select acc).Where(_ => (_.recordTime >= dtF.Date && _.recordTime <= dtT.Date)).ToList();
            }           

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            var reportDataSource1 = new Microsoft.Reporting.WebForms.ReportDataSource();
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = list;
            ReportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            ReportViewer1.LocalReport.ReportPath = "Report/AccessRecord.rdlc";
            ReportViewer1.LocalReport.Refresh();
        }
    }
}