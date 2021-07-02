<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="CreditcardReport.aspx.cs" Inherits="Root_Retailer_CreditCardReport" %>


<%@ Register Src="~/Root/UC/CreditCardReport.ascx" TagName="CreditCardReport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <uc1:CreditCardReport id="PanForm111" runat="server" />
</asp:Content>
