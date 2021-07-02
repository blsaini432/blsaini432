<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/membermaster.master"
    AutoEventWireup="true" CodeFile="NPansionyojana.aspx.cs" Inherits="Root_Retailer_NPansionyojana" %>

<%@ Register Src="../UC/NPansionyojana.ascx" TagName="NPansionyojana" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:NPansionyojana id="PanForm111" runat="server" />
</asp:Content>
