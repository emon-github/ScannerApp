﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisitorPage.aspx.cs" Inherits="ScannerApp.Report.VisitorPage" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous" />
</head>
<body>
    <div style="width: auto; padding:5px;">
        <form id="form1" runat="server" style="width: 100%; height: 100%;">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div style="padding:5px;">
                 <asp:Label ID="Label1" runat="server" Text="Department:"></asp:Label>
                <asp:DropDownList ID="ddlDept" runat="server" DataTextField="deptName" DataValueField="deptName" AutoPostBack="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged"></asp:DropDownList>
                 <asp:Label ID="Label2" runat="server" Text="Device :"></asp:Label>
                <asp:DropDownList ID="ddlDevice" runat="server"  DataTextField="equipName" DataValueField="sn"></asp:DropDownList>
                <asp:Button ID="btnView" runat="server" Text="View"  OnClick="btnView_Click"/>
            </div>
            <div>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="false" Width="100%" Height="600">
                </rsweb:ReportViewer>
            </div>
        </form>
    </div>
</body>
</html>
