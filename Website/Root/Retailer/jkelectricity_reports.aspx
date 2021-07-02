<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="jkelectricity_reports.aspx.cs" Inherits="Root_Retailer_jk" %>


<%@ Register Src="~/Root/UC/jkelectricity.ascx" TagName="jkelectricity" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <uc1:jkelectricity id="PanForm111" runat="server" />
</asp:Content>
