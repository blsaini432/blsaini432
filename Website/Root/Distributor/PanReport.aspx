<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/membermaster.master" AutoEventWireup="true" CodeFile="PanReport.aspx.cs" Inherits="Root_Distributor_PanReport" %>


<%@ Register Src="~/Root/UC/PanReport.ascx" TagName="PanReport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PanReport id="PanForm111" runat="server" />
</asp:Content>

