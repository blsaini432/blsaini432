<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/membermaster.master" AutoEventWireup="true" CodeFile="Digitalsignature_Reports.aspx.cs" Inherits="Root_Distributor_Digitalsignature_Reports" %>


<%@ Register Src="~/Root/UC/Digitalsignature_Reports.ascx" TagName="Digitalsignature_Reports" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <uc1:Digitalsignature_Reports id="PanForm111" runat="server" />
</asp:Content>
