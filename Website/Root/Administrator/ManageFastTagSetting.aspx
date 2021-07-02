<%@ Page Title="" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="ManageFastTagSetting.aspx.cs" Inherits="Portals_Admin_ManageFastTagSetting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>


<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-header">
        <h1>
           Manage Fast Tag FeeSettings
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Setting</a></li>
            <li class="active"> Manage FastTag FeeSettings</li>
        </ol>
    </div>


            <div class="content mydash">
                <h2 style="text-align: center; text-decoration: underline">
                    Manage Fast Tag FeeSettings</h2>
                <table width="100%" class="table table-bordered table-hover">
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
      
</asp:Content>--%>






<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Manage Fast Tag FeeSettings
            </h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row grid-margin">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body">
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Registration of Member Type<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="ddlPackage" runat="server" CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlPackage_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvPackage" runat="server" ControlToValidate="ddlPackage"
                                            Display="Dynamic" ErrorMessage="Please select Package !" SetFocusOnError="True"
                                            ValidationGroup="v" InitialValue="0">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Amount<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtcomission" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="v0" class="btn btn-primary" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered bootstrap-datatable datatable responsive SmallText"></asp:GridView>
</asp:Content>


