<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="card_reports.aspx.cs" Inherits="Root_Distributor_card_report" %>


<%@ Register Src="~/Root/UC/card_report.ascx" TagName="card_report" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <uc1:card_report id="PanForm111" runat="server" />
</asp:Content>
