<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true"
    CodeFile="testdrop.aspx.cs" Inherits="Root_Distributor_testdrop" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
  <%--  <script src="/js/jquery-1.11.2.js" type="text/javascript"></script> --%>

  <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

      <script src="../../Design/js/jquery.min.js"></script>
      <script src="../../Scripts/chosen/chosen.jquery.min.js"></script>
      <link href="../../chosen/chosen.min.css" rel="stylesheet" />
     
</asp:Content>    
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">                   
<asp:UpdatePanel ID="upMain" runat="server">
    <ContentTemplate>
        <asp:DropDownList ID="DropDownList1" 
                          CssClass="form-control chosen" 
                        runat="server">
            <asp:ListItem Text="Select Course" Value="0" 
                          CssClass="form-control" runat="server"/>
            <asp:ListItem Text="core java" Value="1" 
                          CssClass="form-control" runat="server" />                  
            <asp:ListItem Text="C" Value="2" 
                          CssClass="form-control" runat="server" /> 
            <asp:ListItem Text="C++" Value="3" 
                          CssClass="form-control" runat="server" />  
            <asp:ListItem Text="C#" Value="4" 
                          CssClass="form-control" runat="server" />
        </asp:DropDownList>
         <script>
          $('#<%=DropDownList1.ClientID%>').chosen();
      </script>
   </ContentTemplate>
</asp:UpdatePanel>
</asp:Content> 
