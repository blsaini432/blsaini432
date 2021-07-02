<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="Fooddruglicense_Report.aspx.cs" Inherits="Root_Distributor_Fooddruglicense_Report" %>
<%@ Register Src="~/Root/UC/Fooddruglicense_Report.ascx" TagName="Fooddruglicense_Report" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <uc1:Fooddruglicense_Report id="PanForm111" runat="server" />
</asp:Content>
