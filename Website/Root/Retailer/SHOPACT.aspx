<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="SHOPACT.aspx.cs" Inherits="Root_Retailer_SHOPACT" %>

<%@ Register Src="~/Root/UC/SHOPACT.ascx" TagName="SHOPACT" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:SHOPACT id="ITRForm111" runat="server" />
</asp:Content>

