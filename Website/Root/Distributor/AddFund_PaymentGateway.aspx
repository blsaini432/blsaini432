<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true"
    CodeFile="AddFund_PaymentGateway.aspx.cs" Inherits="Root_Admin_AddFund_PaymentGateway" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<section class="content-header">
                <h1>
                 <asp:Label ID="lblAddEdit" runat="server" Text="Add Fund Via Payment Gateway (Online Payment)"></asp:Label>
                    <small>Distributor Panel</small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Wallet System</a></li>
                    <li class="active">Add Fund Via Payment Gateway (Online Payment)</li>
                </ol>
            </section>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="loading-overlay">
                <div class="wrapper">
                    <div class="ajax-loader-outer">
                        Loading...
                    </div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="middiWarapperinner">
                <div id="Warapperhead">
                   
                   
                </div>
                <table class="table table-bordered table-hover ">
                    <tr>
                        <td colspan="3" class="aleft">
                            <strong class="star">Note : Fields with <span class="red">*</span> are mandatory fields.</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="red">*</span>Fund Amount
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotalAmount" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTotalAmount" runat="server" ControlToValidate="txtTotalAmount"
                                Display="Dynamic" ErrorMessage="Please Enter Amount" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="v">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-primary" />
                            <asp:ValidationSummary ID="ValidationSummary" runat="server"
                                ClientIDMode="Static" ValidationGroup="v" />
                        </td>

                    </tr>
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
