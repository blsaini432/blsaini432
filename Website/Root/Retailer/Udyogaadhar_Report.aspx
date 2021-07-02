<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="Udyogaadhar_Report.aspx.cs" Inherits="Root_Retailer_Udyogaadhar_Report" %>


<%@ Register Src="~/Root/UC/Udyogaadhar_Report.ascx" TagName="Udyogaadhar_Report" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <uc1:Udyogaadhar_Report id="PanForm111" runat="server" />
</asp:Content>
