<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="Fire_msmeregistration.aspx.cs" Inherits="Root_Distributor_Fire_msmeregistration" %>

<%@ Register Src="~/Root/UC/FirmmsmeRegistration.ascx" TagName="FirmmsmeRegistration" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:FirmmsmeRegistration id="ITRForm111" runat="server" />
</asp:Content>

