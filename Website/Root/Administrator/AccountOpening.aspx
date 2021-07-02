<%@ Page Title="" Language="C#" MasterPageFile="AdminMAster.master" AutoEventWireup="true"
    CodeFile="AccountOpening.aspx.cs" Inherits="Root_Admin_AccountOpening" %>

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
                                        <label class="col-form-label">Bank Name<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_bankname" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvBannerName" runat="server" ControlToValidate="txt_bankname"
                                            Display="Dynamic" ErrorMessage="Please Enter bank name !" ForeColor="Red" SetFocusOnError="True"
                                            ValidationGroup="v"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">BannerImage<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:FileUpload ID="FileUploadBannerImage" runat="server" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="rfvBannerImage" runat="server" ControlToValidate="FileUploadBannerImage"
                                            Display="Dynamic" ErrorMessage="Please Select BannerImage !" SetFocusOnError="True"
                                            ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Link<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_link" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_link"
                                            Display="Dynamic" ErrorMessage="Please Enter link !" ForeColor="Red" SetFocusOnError="True"
                                            ValidationGroup="v"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                              
                            <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">instructions<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_instr" runat="server"  CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_instr"
                                            Display="Dynamic" ErrorMessage="Please Enter instructions !" ForeColor="Red" SetFocusOnError="True"
                                            ValidationGroup="v"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                
                                </div>

                                </div>

                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                                        &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-danger" />


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
