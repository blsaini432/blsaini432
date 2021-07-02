<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="DSCRegistration_report.aspx.cs" Inherits="Root_Retailer_DSCRegistration_report" %>
<%@ Register Src="~/Root/UC/DSCRegistration_report.ascx" TagName="DSCRegistration_report" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <uc1:DSCRegistration_report id="PanForm111" runat="server" />
</asp:Content>
