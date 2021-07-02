<%@ Page Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true" CodeFile="Add_Vedio.aspx.cs" Inherits="Root_Administrator_Add_Vedio" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress AssociatedUpdatePanelID="UpdatePanel1" ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #fbfbfb; opacity: 0.7;">
                <span>
                    <img alt="Loading" src="../../Design/images/pageloader.gif" />
                </span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">
                <asp:Label ID="lblAddEdit" runat="server"></asp:Label>
            </h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row grid-margin">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body">
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Title<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_title" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Vedio Upload<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                        <asp:Label ID="Label1" runat="server" Text="mp4 video"></asp:Label>

                                    </div>
                                </div>
                                <%--<div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Upload PDF<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:FileUpload ID="pdf_file" runat="server" />
                                        <asp:Label ID="Label2" runat="server" Text="Upload PDF"></asp:Label>

                                    </div>
                                </div>--%>
                               <%-- <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Upload Link<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_link" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>--%>
                                <disv class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" OnClick="BtnUpload_Click" CssClass="btn btn-primary" />
                                        &nbsp;&nbsp;&nbsp;
                                    </div>
                                </disv>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSubmit" />

            </Triggers>
        </asp:UpdatePanel>

    </div>
</asp:Content>
