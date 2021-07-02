<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="Providentfund_report.aspx.cs" Inherits="Root_Retailer_Providentfund_report" %>


<%@ Register Src="~/Root/UC/Providentfund_report.ascx" TagName="Providentfund_report" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <uc1:Providentfund_report id="PanForm111" runat="server" />
</asp:Content>
