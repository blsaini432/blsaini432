<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="DSCRegistrationform.aspx.cs" Inherits="Root_Retailer_DSCRegistrationform" %>

<%@ Register Src="~/Root/UC/DSCRegistration.ascx" TagName="DSCRegistration" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:DSCRegistration id="ITRForm111" runat="server" />
</asp:Content>

