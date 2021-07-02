<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/membermaster.master" AutoEventWireup="true" CodeFile="GstReturnreport.aspx.cs" Inherits="Root_Retailer_GstReturnreport" %>

<%@ Register Src="~/Root/UC/GSTReturnReport.ascx" TagName="GSTReturnReport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <uc1:GSTReturnReport id="PanForm111" runat="server" />
</asp:Content>
