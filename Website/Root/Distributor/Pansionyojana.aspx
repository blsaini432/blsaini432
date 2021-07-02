<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/membermaster.master"
    AutoEventWireup="true" CodeFile="Pansionyojana.aspx.cs" Inherits="Root_Distributor_Pansionyojana" %>

<%@ Register Src="../UC/Pansionyojana.ascx" TagName="Pansionyojana" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:Pansionyojana id="PanForm111" runat="server" />
</asp:Content>
