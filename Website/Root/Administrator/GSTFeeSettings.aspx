<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master"
    AutoEventWireup="true" CodeFile="GSTFeeSettings.aspx.cs" Inherits="Portal_Administrator_GSTFeeSettings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Manage GST/ITR Fees
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
                                        <asp:DropDownList runat="server" CssClass="form-control" AutoPostBack="true" ID="ddlaction">
                                            <asp:ListItem>Select </asp:ListItem>                                          
                                             <asp:ListItem>GST Fee</asp:ListItem>
                                            <asp:ListItem>ITR Fee</asp:ListItem>
                                            <asp:ListItem>Food Registration Fee</asp:ListItem>
                                            <asp:ListItem>Firm Registration Fee</asp:ListItem>
                                            <asp:ListItem>DSC Registration Fee</asp:ListItem>
                                             
                                           
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlaction"
                                            Display="Dynamic" ErrorMessage="Please Select service !" SetFocusOnError="True"
                                            ValidationGroup="v0" InitialValue="0">*</asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Registration of Member Type<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="ddlSourceMember" runat="server" CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSourceMember_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvPackage" runat="server" ControlToValidate="ddlSourceMember"
                                            Display="Dynamic" ErrorMessage="Please Select member type !" SetFocusOnError="True"
                                            ValidationGroup="v" InitialValue="0">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Amount<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_amount" runat="server" CssClass="form-control"></asp:TextBox>
                                          <cc1:FilteredTextBoxExtender ID="txtZIP_FilteredTextBoxExtender" runat="server" Enabled="True"
                                            FilterType="Numbers" TargetControlID="txt_amount">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">

                                        <%-- <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" OnClick="btnSubmit_Click" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
                            <asp:ValidationSummary ID="ValidationSummary" runat="server" ClientIDMode="Static"
                                ValidationGroup="v" />--%>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="v0" class="btn btn-primary" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
  
     <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered bootstrap-datatable datatable responsive SmallText"></asp:GridView>
  </div>
</asp:Content>
