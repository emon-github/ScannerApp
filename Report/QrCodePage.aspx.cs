using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ScannerApp.Report
{
    public partial class QrCodePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.LocalReport.EnableExternalImages = true;
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            QRCoder.QRCodeGenerator generator = new QRCoder.QRCodeGenerator();
            QRCoder.QRCodeData data = generator.CreateQrCode(TextBox1.Text, QRCoder.QRCodeGenerator.ECCLevel.Q);
            QRCoder.QRCode qR = new QRCoder.QRCode(data);
            Bitmap bmp = qR.GetGraphic(7);
            
        }
    }
}