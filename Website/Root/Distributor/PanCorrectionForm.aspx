<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master"
    AutoEventWireup="true" CodeFile="PanCorrectionForm.aspx.cs" Inherits="Root_Distributor_PanCorrectionForm" %>
<%@ Register Src="~/Root/UC/PanCorrectionForm.ascx" TagName="PanCorrectionForm" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PanCorrectionForm ID="PanCorrection111" runat="server" />
</asp:Content>
