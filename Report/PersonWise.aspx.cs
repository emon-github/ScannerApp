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
                this.txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                LoadData();
            }
        }

        private void LoadData()
        {
            lblMsg.Text = "";
            List<AccessRecord> list = null;
            string client = User.Identity.Name;

            DateTime dt = DateTime.Now;
            DateTime dtEnd = dt.AddDays(1);

            if (txtDate.Text == "" && txtPhone.Text == "")
            {
                lblMsg.Text = "Please insert either date or phone number and try again.";

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.EnableExternalImages = true;
                var reportDataSource2 = new Microsoft.Reporting.WebForms.ReportDataSource();
                reportDataSource2.Name = "DataSet1";
                reportDataSource2.Value = new List<AccessRecord>();
                ReportViewer1.LocalReport.DataSources.Add(reportDataSource2);
                ReportViewer1.LocalReport.ReportPath = "Report/PersonWise.rdlc";
                ReportViewer1.LocalReport.Refresh();

                return;
            }
            if (txtDate.Text == "" && txtPhone.Text != "")
            {

                if (User.IsInRole("Admin"))
                {
                    list = db.AccessRecords
                        .Where(_ => _.phone.Contains(txtPhone.Text.Trim()))                        
                        .OrderByDescending(_ => _.recordTime).ToList();
                }
                else
                {
                    list = (from d in db.Devices
                            join acc in db.AccessRecords on d.sn equals acc.sn
                            where d.client == User.Identity.Name
                            select acc)
                            .Where(_ => _.phone.Contains(txtPhone.Text.Trim()))
                            .ToList();
                }
            }
            if (!string.IsNullOrEmpty(txtDate.Text))
            {
                dt = Convert.ToDateTime(txtDate.Text);
                dtEnd = dt.AddDays(1);

                if (User.IsInRole("Admin"))
                {
                    list = db.AccessRecords
                        .Where(_ => _.recordTime >= dt.Date)
                        .Where(_ => _.recordTime <= dtEnd.Date)
                        .OrderByDescending(_ => _.recordTime).ToList();
                }
                else
                {
                    list = (from d in db.Devices
                            join acc in db.AccessRecords on d.sn equals acc.sn
                            where d.client == User.Identity.Name
                            select acc)
                            .Where(_ => _.recordTime >= dt.Date)
                            .Where(_ => _.recordTime <= dtEnd.Date)
                            .ToList();
                }

                if (!string.IsNullOrEmpty(txtPhone.Text))
                {
                    list = list.Where(_ => _.MyPhone.Contains(txtPhone.Text.Trim())).ToList();
                }
            }

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            var reportDataSource1 = new Microsoft.Reporting.WebForms.ReportDataSource();
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = list.OrderBy(_ => _.phone).OrderByDescending(_ => _.recordTime);
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