<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="Providentfund.aspx.cs" Inherits="Root_Retailer_Providentfund" %>

<%@ Register Src="~/Root/UC/Providentfund.ascx" TagName="Providentfund" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Providentfund id="ITRForm111" runat="server" />
</asp:Content>

