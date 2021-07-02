<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="LoanForm_Report.aspx.cs" Inherits="Root_Distributor_LoanForm_Report" %>
<%@ Register Src="~/Root/UC/LoanForm_Report.ascx" TagName="LoanForm_Report" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <uc1:LoanForm_Report id="PanForm111" runat="server" />
</asp:Content>
