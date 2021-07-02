<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/membermaster.master" AutoEventWireup="true" CodeFile="Pansionyojanareport.aspx.cs" Inherits="Root_Retailer_Pansionyojanareport" %>

<%@ Register Src="~/Root/UC/Pansionyojanareport.ascx" TagName="Pansionyojanareport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:Pansionyojanareport id="PanForm111" runat="server" />
</asp:Content>

