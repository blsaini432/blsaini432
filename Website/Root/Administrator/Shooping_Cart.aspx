<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/adminmaster.master" AutoEventWireup="true"
    CodeFile="Shooping_Cart.aspx.cs" Inherits="Root_Administrator_Shooping_Cart" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        td:first-child {
            min-width: 30% !important;
        }

        td:nth-child(2) {
            min-width: 2% !important;
        }

        .auto-style1 {
            height: 75px;
        }
    </style>
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<section class="content-header">
                <h1>
                    <asp:Label ID="lblAddEdit" runat="server"></asp:Label>
                    <small>Admin Panel</small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard">Shooping</i></a></li>
                    <li class="active">Add Product</li>
                </ol>
            </section>


            <section class="content mydash">
             <table class="table table-bordered table-hover ">
                    <tr>
                        <td colspan="3" class="aleft">
                            <strong class="star">Note : Fields with <span class="red">*</span> are mandatory fields.</strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1">
                            <span class="red">*</span> Product Name
                        </td>
                        <td class="td2">
                            :
                        </td>
                        <td class="td3">
                            <asp:TextBox ID="txtproductname" runat="server" 
                                CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCurrentPassword" runat="server" ErrorMessage="Please Enter Product Name !"
                                ControlToValidate="txtproductname" Display="Dynamic" SetFocusOnError="True"
                                ValidationGroup="v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="red">*</span> Description
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtdescription" runat="server"
                                CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ErrorMessage="Please Enter Product Description !"
                                ControlToValidate="txtdescription" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="red">*</span>Quantity
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txt_quanitity" runat="server" 
                               CssClass="form-control" Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ErrorMessage="Please Enter Quantity !"
                                ControlToValidate="txt_quanitity" Display="Dynamic" SetFocusOnError="True"
                                ValidationGroup="v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                           
                        </td>
                    </tr>
                          <tr>
                        <td class="auto-style1">
                             <span class="red">*</span>Price Per Unit<strong>(In INR only)</strong></td>
                        <td class="auto-style1">
                            :</td>
                        <td class="auto-style1">
                            <asp:TextBox ID="txt_priceperunit" runat="server" 
                               CssClass="form-control" Width="150px"></asp:TextBox>
                           
                            <asp:RequiredFieldValidator ID="rfvConfirmPassword0" runat="server" ControlToValidate="txt_priceperunit" Display="Dynamic" ErrorMessage="Please Enter Product Price!" SetFocusOnError="True" ValidationGroup="v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                           
                        </td>
                    </tr>
                    <tr>
                        <td><span class="red">*</span>Minimum Order Per Unit </td>
                        <td>: </td>
                        <td>
                            <asp:TextBox ID="txtminiumorderperunit" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtminiumorderperunit" Display="Dynamic" ErrorMessage="Please Enter Minium Order Per Unit  !" SetFocusOnError="True" ValidationGroup="v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td><span class="red">*</span>Product Image&nbsp;</td>
                        <td>:</td>
                        <td>
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" OnClick="btnSubmit_Click" class="btn btn-primary" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" class="btn btn-primary" />
                            <asp:ValidationSummary ID="ValidationSummary" runat="server" ClientIDMode="Static"
                                ValidationGroup="v" />
                        </td>
                    </tr>
                </table>
            </section>
      
</asp:Content>--%>



<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">
                <asp:Label ID="Label1" runat="server"></asp:Label>
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
                                        <asp:Label ID="lblAddEdit" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-8">
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Product Name <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtproductname" runat="server"
                                            CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCurrentPassword" runat="server" ErrorMessage="Please Enter Product Name !"
                                            ControlToValidate="txtproductname" Display="Dynamic" SetFocusOnError="True"
                                            ValidationGroup="v"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Description  <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtdescription" runat="server"
                                            CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ErrorMessage="Please Enter Product Description !"
                                            ControlToValidate="txtdescription" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Quantity  <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_quanitity" runat="server"
                                            CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ErrorMessage="Please Enter Quantity !"
                                            ControlToValidate="txt_quanitity" Display="Dynamic" SetFocusOnError="True"
                                            ValidationGroup="v"></asp:RequiredFieldValidator>

                                    </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-lg-3">
                                            <label class="col-form-label">Price Per Unit(In INR only) <code>*</code></label>
                                        </div>
                                        <div class="col-lg-8">
                                            <asp:TextBox ID="txt_priceperunit" runat="server"
                                                CssClass="form-control"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="rfvConfirmPassword0" runat="server" ControlToValidate="txt_priceperunit" Display="Dynamic" ErrorMessage="Please Enter Product Price!" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-lg-3">
                                            <label class="col-form-label">Minimum Order Per Unit  <code>*</code></label>
                                        </div>
                                        <div class="col-lg-8">
                                            <asp:TextBox ID="txtminiumorderperunit" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtminiumorderperunit" Display="Dynamic" ErrorMessage="Please Enter Minium Order Per Unit  !" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-lg-3">
                                            <label class="col-form-label">Product Image  <code>*</code></label>
                                        </div>
                                        <div class="col-lg-8">
                                            
                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator ID="rfvBannerImage" runat="server" ControlToValidate="FileUpload1"
                                ErrorMessage="Please Select  !" 
                               ></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" OnClick="btnSubmit_Click" class="btn btn-primary" />
                                        &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" class="btn btn-primary" />
                                        <%--<asp:ValidationSummary ID="ValidationSummary" runat="server" ClientIDMode="Static"
                                ValidationGroup="v" />--%>

                                        <%-- <asp:Button ID="Button1" runat="server" Text="Submit" ValidationGroup="v" OnClick="btnSubmit_Click" class="btn btn-primary" />
                                        &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="Button2" runat="server" Text="Reset" OnClick="btnReset_Click" class="btn btn-danger" />--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
