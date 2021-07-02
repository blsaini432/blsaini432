<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="Offlinebill_payder.aspx.cs" Inherits="Root_Retailer_Offlinebill_payder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Electricity bill Payment
            </h3>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12 grid-margin stretch-card">
                                <div class="card">
                                    <div class="card-body">
                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">State Name<code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:DropDownList ID="ddlStateName" runat="server"
                                                    CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvStateNamesignup" runat="server"
                                                    ControlToValidate="ddlStateName" Display="Dynamic"
                                                    ErrorMessage="Please Select State !" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="v0">*</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">Select Board<code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:DropDownList ID="ddl_Eboard" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfv_Eboard" runat="server" InitialValue="0" ValidationGroup="getbill" ControlToValidate="ddl_Eboard"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">K. Number/Account No.<code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txt_knumber" runat="server" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="getbill" ErrorMessage="Enter K Number /Account Number"
                                                    ControlToValidate="txt_knumber"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">Customer Name<code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txt_name" runat="server" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="getbill" ErrorMessage="Enter Customer Name"
                                                    ControlToValidate="txt_name"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">Customer Mobile No.<code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txt_mobile" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="getbill" ErrorMessage="Enter Customer Mobile No."
                                                    ControlToValidate="txt_mobile"></asp:RequiredFieldValidator>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_mobile"
                                                    ValidChars="0123456789">
                                                </cc1:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">Bill Unit<code></code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txt_billunite" runat="server" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">Due Date <code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txt_date" runat="server" MaxLength="50" onkeypress="return false;"
                                                    CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd/MM/yyyy" Animated="False"
                                                    PopupButtonID="txt_date" TargetControlID="txt_date">
                                                </cc1:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_date"
                                                    Display="Dynamic" ErrorMessage="Please Enter Due  Date  (dd/MM/yyyy) !" SetFocusOnError="True"
                                                    ValidationGroup="getbill"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">Amount<code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txt_amount" runat="server" CssClass="form-control" ></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="getbill" ErrorMessage="Enter Amount "
                                                    ControlToValidate="txt_amount"></asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txt_amount"
                                            ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                                 </div>
                                        </div>



                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:Button ID="btn_Getbill" runat="server" Text="Pay Bill" ValidationGroup="getbill" OnClientClick="if (!Page_ClientValidate('getbill')){ return false; } this.disabled = true; this.value = 'Processing...';"
                                                    UseSubmitBehavior="false" CssClass="btn btn-primary"
                                                    OnClick="btn_Getbill_Click" />

                                            </div>
                                        </div>
                                    </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>


    </div>
</asp:Content>

