<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/membermaster.master" AutoEventWireup="true" CodeFile="NPansionyojanareport.aspx.cs" Inherits="Root_Distributor_NPansionyojanareport" %>

<%@ Register Src="~/Root/UC/NPansionyojanareport.ascx" TagName="NPansionyojanareport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:NPansionyojanareport id="PanForm111" runat="server" />
</asp:Content>

