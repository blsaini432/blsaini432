<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="Creditcard.aspx.cs" Inherits="Root_Distributor_Creditcard" %>

<%@ Register Src="~/Root/UC/Creditcard.ascx" TagName="Creditcard" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc1:Creditcard id="ITRForm111" runat="server" />
</asp:Content>

