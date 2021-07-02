<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="udyogaadhar.aspx.cs" Inherits="Root_Distributor_udyogaadhar" %>

<%@ Register Src="~/Root/UC/udyogaadhar.ascx" TagName="udyogaadhar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:udyogaadhar id="ITRForm111" runat="server" />
</asp:Content>

