<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="Digitalsignatures.aspx.cs" Inherits="Root_Retailer_Digitalsignatures" %>

<%@ Register Src="~/Root/UC/Digitalsignatures.ascx" TagName="Digitalsignatures" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Digitalsignatures id="ITRForm111" runat="server" />
</asp:Content>

