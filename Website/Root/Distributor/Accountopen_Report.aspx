<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/membermaster.master"
    AutoEventWireup="true" CodeFile="Accountopen_Report.aspx.cs" Inherits="Root_Distributor_Accountopen_Report" %>
<%@ Register Src="../UC/Accountopen_report.ascx" TagName="Accountopen_report" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:Accountopen_report id="PanForm111" runat="server" />
</asp:Content>
