<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/membermaster.master"
    AutoEventWireup="true" CodeFile="PanCardApplication.aspx.cs" Inherits="Root_Retailer_PanCardApplication" %>

<%@ Register Src="../UC/PanForm.ascx" TagName="PanForm" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PanForm id="PanForm111" runat="server" />
</asp:Content>
