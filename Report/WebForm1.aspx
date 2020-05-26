<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ScannerApp.Report.WebForm1" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div style="width: auto;">
        <form id="form1" runat="server" style="width: 100%; height: 100%;">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div>
                <div>
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </div>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="false" Width="100%" Height="500">
                </rsweb:ReportViewer>
            </div>
        </form>
    </div>
</body>
</html>
