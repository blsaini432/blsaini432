<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="Fooddruglicense.aspx.cs" Inherits="Root_Retailer_Fooddruglicense" %>

<%@ Register Src="~/Root/UC/Fooddruglicense.ascx" TagName="Fooddruglicense" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Fooddruglicense id="ITRForm111" runat="server" />
</asp:Content>

