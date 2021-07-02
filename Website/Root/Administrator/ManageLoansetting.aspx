<%@ Page Title="" Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true" CodeFile="ManageLoansetting.aspx.cs" Inherits="Portals_Admin_ManageLoansetting" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Manage Loan Fees
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
                                        <label class="col-form-label">Service Type<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                         <asp:DropDownList ID="ddlservice" runat="server" Height="35px" CssClass="form-control" AutoPostBack="true">
                                        <asp:ListItem>Select Loan Type</asp:ListItem>
                                        <asp:ListItem>Loan</asp:ListItem>
                                    </asp:DropDownList>
                                       
                                    </div>
                                </div>
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
