﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="Firemems_report.aspx.cs" Inherits="Root_Distributor_Firemems_report" %>
<%@ Register Src="~/Root/UC/Firmmsme_report.ascx" TagName="Firmmsme_report" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <uc1:Firmmsme_report id="PanForm111" runat="server" />
</asp:Content>
