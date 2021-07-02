<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/membermaster.master" AutoEventWireup="true" CodeFile="GstRegistration_Report.aspx.cs" Inherits="Root_Retailer_GstRegistration_Report" %>
<%@ Register Src="~/Root/UC/GstRegistration_Report.ascx" TagName="GstRegistration_Report" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <uc1:GstRegistration_Report id="PanForm111" runat="server" />
</asp:Content>
