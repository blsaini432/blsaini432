<%@ Page Title="" Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true"
    CodeFile="ManagePackage.aspx.cs" Inherits="Root_Admin_ManagePackage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">
                <asp:Label ID="lblAddEdit" runat="server"></asp:Label>
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
                                        <label class="col-form-label">Package Name<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtPackageName" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPackageName" runat="server" ControlToValidate="txtPackageName"
                                            Display="Dynamic" ErrorMessage="Please Enter Package Name !" ForeColor="Red"
                                            SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Member Type<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="ddlMemberType" runat="server" CssClass="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlMemberType"
                                            Display="Dynamic" ErrorMessage="Please Enter Member type!" ForeColor="Red" InitialValue="0"
                                            SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>

                                    </div>
                                </div>

                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">

                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" OnClick="btnSubmit_Click" class="btn btn-primary" />
                                        &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" class="btn btn-danger" />

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
