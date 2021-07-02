<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="Shopact_Report.aspx.cs" Inherits="Root_Retailer_Shopact_Report" %>


<%@ Register Src="~/Root/UC/Shopact_Report.ascx" TagName="Shopact_Report" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <uc1:Shopact_Report id="PanForm111" runat="server" />
</asp:Content>
