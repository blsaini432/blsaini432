<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/membermaster.master" AutoEventWireup="true" CodeFile="GSTReturnApplication.aspx.cs" Inherits="Root_Distributor_GSTReturnApplication" %>

<%@ Register Src="../UC/GSTReturnForm.ascx" TagName="GSTReturnForm" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc1:GSTReturnForm id="GSTForm111" runat="server" />
</asp:Content>

