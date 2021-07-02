<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/membermaster.master" AutoEventWireup="true" CodeFile="ITRformnew.aspx.cs" Inherits="Root_Retailer_ITRformnew" %>
<%@ Register Src="~/Root/UC/ITRFormnew.ascx" TagName="ITRFormnew" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc1:ITRFormnew id="ITRForm111" runat="server" />
</asp:Content>

