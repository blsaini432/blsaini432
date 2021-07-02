<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/membermaster.master" AutoEventWireup="true" CodeFile="Digitalsignature.aspx.cs" Inherits="Root_Distributor_Digitalsignature" %>

<%@ Register Src="~/Root/UC/Digitalsignature.ascx" TagName="Digitalsignature" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Digitalsignature id="ITRForm111" runat="server" />
</asp:Content>

