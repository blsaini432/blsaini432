<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/membermaster.master"
    AutoEventWireup="true" CodeFile="Accountopeng.aspx.cs" Inherits="Root_Distributor_Accountopeng" %>
<%@ Register Src="../UC/Accountopen.ascx" TagName="Accountopen" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:Accountopen id="PanForm111" runat="server" />
</asp:Content>
