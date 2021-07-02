<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master"
    AutoEventWireup="true" CodeFile="PanCorrection_Report.aspx.cs" Inherits="Root_Distributor_PanCorrection_Report" %>
<%@ Register Src="~/Root/UC/PanCorrection_Report.ascx" TagName="PanCorrection_Report" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PanCorrection_Report ID="PanCorrection111" runat="server" />
</asp:Content>
