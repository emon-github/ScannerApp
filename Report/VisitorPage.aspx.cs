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
                LoadCombo();
                LoadData();
            }
        }

        private void LoadCombo()
        {
            var list = db.Devices.Select(_ => new { _.deptName}).Distinct().ToList();

            ddlDept.DataSource = list.OrderBy(_ => _.deptName);
            ddlDept.DataBind();

            ddlDept_SelectedIndexChanged(null,null);
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dpt = ddlDept.SelectedItem.Value.ToString();
            var list = db.Devices.Where(_ => _.deptName==dpt).ToList();

            ddlDevice.DataSource = list.OrderBy(_ => _.equipName); 
            ddlDevice.DataBind();

            ClearReport();
        }

        private void ClearReport()
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            var reportDataSource1 = new Microsoft.Reporting.WebForms.ReportDataSource();
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = new List<Staff>();
            ReportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            ReportViewer1.LocalReport.ReportPath = "Report/VisitorReport.rdlc";
            ReportViewer1.LocalReport.Refresh();
        }

        private void LoadData()
        {
            List<Staff> list = null;
            string client = User.Identity.Name;

            string dpt = ddlDept.SelectedItem.Value.ToString();
            string dev = ddlDevice.SelectedItem.Value.ToString();

            if (User.IsInRole("Admin"))
            {
                list = db.Staffs
                    .Where(_ => _.deptName==dpt)
                    .Where(_ => _.emerPer==dev)
                    .OrderByDescending(_ => _.ORDER_BY_DERIVED_0).ToList();
            }
            else
            {
                list = db.Staffs
                    .Where(_ => _.job == client)
                    .Where(_ => _.deptName == dpt)
                    .Where(_ => _.emerPer == dev)
                    .OrderByDescending(_ => _.ORDER_BY_DERIVED_0).ToList();
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

        protected void btnView_Click(object sender, EventArgs e)
        {
            LoadData();
        }

       
    }
}