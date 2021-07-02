<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="LoanForm.aspx.cs" Inherits="Root_Distributor_LoanForm" %>
<%@ Register Src="~/Root/UC/LoanForm.ascx" TagName="LoanForm" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:LoanForm id="ITRForm111" runat="server" />
</asp:Content>

