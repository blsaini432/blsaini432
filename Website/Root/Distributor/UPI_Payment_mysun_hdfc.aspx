<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="UPI_Payment_mysun_hdfc.aspx.cs" Inherits="Root_Distributor_UPI_Payment_mysun" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">UPI Payment 
            </h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row grid-margin" id="upi" runat="server">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body">
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Name<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_name" runat="server" autocomplete="off"
                                            MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_Amount" runat="server" ControlToValidate="txt_name"
                                            ErrorMessage="Enter Valid Name" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Pin Code<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_upi" runat="server" autocomplete="off"
                                            MaxLength="6" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_upi"
                                            ErrorMessage="Enter Valid Pin Code" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Button ID="btnSubmit" type="button" runat="server" Text="Submit"
                                            ValidationGroup="vgBeni" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnReset" runat="server" Text="Reset"
                                            CssClass="btn btn-danger" OnClick="btnReset_Click" />
                                    </div>
                                </div> 
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row grid-margin" id="QRCODE" runat="server" visible="false">
                    <div class="card">
                        <div class="card-body">
                             <div class="form-group row">
                                 <img src="<%=ViewState["QrString"] %>" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                <asp:PostBackTrigger ControlID="btnSubmit" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

</asp:Content>

