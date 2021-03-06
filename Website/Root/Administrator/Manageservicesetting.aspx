<%@ Page Title="" Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true" CodeFile="Manageservicesetting.aspx.cs" Inherits="Portals_Admin_Manageservicesetting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-header">
        <h1>
           Manage Service FeeSettings
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Setting</a></li>
            <li class="active"> Manage Service FeeSettings</li>
        </ol>
    </div>
<%--    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="loading-overlay">
                <div class="wrapper">
                    <div class="ajax-loader-outer">
                        Loading...
                    </div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>

            <div class="content mydash">
                <h2 style="text-align: center; text-decoration: underline">
                    Manage Service FeeSettings</h2>
                <table width="100%" class="table table-bordered table-hover">
                     <tr>
                        <td style="width: 20%;">
                           Service
                        </td>
                        <td>
                                    <asp:DropDownList ID="ddlservice" runat="server" CssClass="form-control">
                                
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlservice"
                                Display="Dynamic" ErrorMessage="Please select service !" SetFocusOnError="True"
                                ValidationGroup="v" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%;">
                           Package
                        </td>
                        <td>
                                    <asp:DropDownList ID="ddlPackage" runat="server" CssClass="form-control" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlPackage_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvPackage" runat="server" ControlToValidate="ddlPackage"
                                Display="Dynamic" ErrorMessage="Please select Package !" SetFocusOnError="True"
                                ValidationGroup="v" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                          <td style="width: 20%;">
                            
                                  
                                       Amount(In Rs)
                                    </td>
                                    <td>
                                         <asp:TextBox ID="txtcomission" runat="server" CssClass="form-control"></asp:TextBox>
                                       
                                 
                                    </td>
                         
                      
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" OnClick="btnSubmit_Click" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
                            <asp:ValidationSummary ID="ValidationSummary" runat="server" ClientIDMode="Static"
                                ValidationGroup="v" />
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered bootstrap-datatable datatable responsive SmallText table-responsive"></asp:GridView>
            </div>
      
</asp:Content>