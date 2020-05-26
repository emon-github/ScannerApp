<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccessRecordPage.aspx.cs" Inherits="ScannerApp.Report.AccessRecordPage" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">
</head>
<body>
     <div style="width: auto;">
        <form id="form1" runat="server" style="width: 100%; height: 100%;">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div>
                <div style="padding:5px;">
                   <asp:Label ID="Label1" runat="server" Text="Date :"></asp:Label>
                    <asp:TextBox ID="txtDate" runat="server" TextMode="Date"></asp:TextBox>
                    <asp:Button ID="btnView" runat="server"  OnClick="btnView_Click" Text="View" CssClass="btn btn-info"/> 
                </div>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="false" Width="100%" Height="500">
                </rsweb:ReportViewer>
            </div>
        </form>
    </div>
</body>
</html>
