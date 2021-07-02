<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/membermaster.master" AutoEventWireup="true" CodeFile="Digitalsignature_Report.aspx.cs" Inherits="Root_Retailer_Digitalsignature_Report" %>


<%@ Register Src="~/Root/UC/Digitalsignature_Report.ascx" TagName="Digitalsignature_Report" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <uc1:Digitalsignature_Report id="PanForm111" runat="server" />
</asp:Content>
