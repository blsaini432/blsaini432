<%@ Page Title="" Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true"
    CodeFile="ManagePage.aspx.cs" Inherits="cms_ManagePage" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
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
                                .
                         <div class="form-group row">
                             <div class="col-lg-3">
                                 <label class="col-form-label">Page Name<code>*</code></label>
                             </div>
                             <div class="col-lg-8">
                                 <asp:TextBox ID="txtPageName" runat="server" MaxLength="50" CssClass="form-control" ValidationGroup="v"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="rfvPageName" runat="server" ControlToValidate="txtPageName"
                                     Display="Dynamic" ErrorMessage="Please Enter Page Name !" SetFocusOnError="True"
                                     ValidationGroup="v"></asp:RequiredFieldValidator>

                             </div>
                         </div>

                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Parent Page<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="ddlPage" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Page Heading<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtPageHeading" runat="server" MaxLength="100" CssClass="form-control"
                                            ValidationGroup="v"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPageHeading"
                                            Display="Dynamic" ErrorMessage="Please Enter Page Heading !" SetFocusOnError="True"
                                            ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Meta Title :<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtMetaTitle" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Meta Keywords :<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtMetaKeywords" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Meta Description :<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtMetaDesc" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Page Description:<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <CKEditor:CKEditorControl ID="ckPageDesc" runat="server" BasePath="~/Root/CKEditor/"
                                            Height="200px" Width="700px">
                                        </CKEditor:CKEditorControl>
                                        <asp:RequiredFieldValidator ID="rfvPageDesc" runat="server" ControlToValidate="ckPageDesc"
                                            Display="Dynamic" ErrorMessage="Please Enter Page Desc !" SetFocusOnError="True"
                                            ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary"
                                            ValidationGroup="v" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Reset" OnClick="btnCancel_Click" class="btn btn-danger" />

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

</asp:Content>




