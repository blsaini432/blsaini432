<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="ITRReport.aspx.cs" Inherits="Root_Distributor_ITRReport" %>
<%@ Register Src="~/Root/UC/ITRReport.ascx" TagName="ITRReport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <uc1:ITRReport id="PanForm111" runat="server" />
</asp:Content>
