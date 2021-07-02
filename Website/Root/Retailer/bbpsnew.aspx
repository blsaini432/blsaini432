<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="bbpsnew.aspx.cs" Inherits="Root_Retailer_bbpsnew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Electricty-BBPS Panel
            </h3>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-6 grid-margin stretch-card">
                                <div class="card">
                                    <div class="card-body">
                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">Select Board<code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:DropDownList ID="ddl_Eboard" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_Eboard_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfv_Eboard" runat="server" InitialValue="0" ValidationGroup="getbill" ControlToValidate="ddl_Eboard"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div id="tr_service" runat="server" visible="false">
                                            <div class="form-group row">
                                                <div class="col-lg-3">
                                                    <asp:Label ID="lbl_servicetag" runat="server"></asp:Label><code>*</code>
                                                </div>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txt_servicenum" runat="server" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfv_servicenum" runat="server" ValidationGroup="getbill" ErrorMessage="Enter Service Number"
                                                        ControlToValidate="txt_servicenum"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-lg-3">
                                                    <asp:Label ID="Label1" runat="server" Text="Mobile"></asp:Label><code>*</code>
                                                </div>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txt_customermobile" runat="server" CssClass="form-control" Style="text-transform: uppercase"
                                                        placeholder="Customer Mobile"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="dv_jha" runat="server" visible="false">

                                            <div class="form-group row">
                                                <div class="col-lg-3">
                                                    <asp:Label ID="Label2" runat="server" Style="text-transform: uppercase" Text="CUSTMER MOBILE*"></asp:Label><code>*</code>
                                                </div>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txt_custmermobilejha" runat="server" CssClass="form-control" Style="text-transform: uppercase"
                                                        placeholder="Customer Mobile"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-lg-3">
                                                    <asp:Label ID="lbl_servicetagjha" runat="server" Style="text-transform: uppercase"></asp:Label><span>*</span>
                                                </div>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txt_servicejha" runat="server" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="getbill"
                                                        ControlToValidate="txt_servicejha"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-lg-3">
                                                    <label class="col-form-label">SUB DIVISION<code>*</code></label>
                                                </div>
                                                <div class="col-lg-8">
                                                    <asp:DropDownList ID="ddl_jhasub" runat="server" CssClass="selectbox">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfv_jhasub" runat="server" ValidationGroup="getbill"
                                                        ControlToValidate="ddl_jhasub" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                        </div>

                                        <div id="dvGujrat" runat="server" visible="false">

                                            <div class="form-group row">
                                                <div class="col-lg-3">
                                                    <asp:Label ID="lblservicetaggujrat" runat="server" Style="text-transform: uppercase"></asp:Label><span>*</span>
                                                </div>
                                                <div class="col-lg-8">

                                                    <asp:TextBox ID="txtconsumernumbergujrat" runat="server" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvamtGujrat" runat="server" ValidationGroup="paybill"
                                                        ControlToValidate="txtconsumernumbergujrat"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-lg-3">
                                                    <asp:Label ID="lbl_amtgujrat" runat="server" Style="text-transform: uppercase" Text="Amount"></asp:Label><span>*</span>
                                                </div>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txt_amountgujrat" runat="server" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfv_amountgujrat" runat="server" ValidationGroup="paybill"
                                                        ControlToValidate="txt_amountgujrat"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                        </div>

                                        <div id="dvMaharashtra" runat="server" visible="false">

                                            <div class="form-group row">
                                                <div class="col-lg-3">
                                                    <label class="col-form-label">CUSTMER MOBILE<code>*</code></label>

                                                </div>
                                                <div class="col-lg-8">

                                                    <asp:TextBox ID="txt_custmermobilemaharshtra" runat="server" CssClass="form-control" Style="text-transform: uppercase"
                                                        placeholder="Customer Mobile"></asp:TextBox>
                                                </div>
                                            </div>


                                            <div class="form-group row">
                                                <div class="col-lg-3">
                                                    <asp:Label ID="lblserviceMaharashtra" runat="server" Style="text-transform: uppercase"></asp:Label><span>*</span>

                                                </div>
                                                <div class="col-lg-8">

                                                    <asp:TextBox ID="txtconsumernumbermaharshtra" runat="server" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfv_txtconsumernumbermaharshtra" runat="server" ValidationGroup="paybill"
                                                        ControlToValidate="txtconsumernumbermaharshtra"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>



                                            <div class="form-group row">
                                                <div class="col-lg-3">
                                                    <asp:Label ID="lblbillingunitMahashtra" runat="server" Style="text-transform: uppercase"
                                                        Text="BILLING UNIT"></asp:Label><span>*</span>

                                                </div>
                                                <div class="col-lg-8">

                                                    <asp:TextBox ID="txtbillingunitmahashtra" runat="server" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfv_txtbillingunitmahashtra" runat="server" ValidationGroup="paybill"
                                                        ControlToValidate="txtbillingunitmahashtra"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:Button ID="btn_Getbill" runat="server" Text="Get Bill" ValidationGroup="getbill" OnClientClick="if (!Page_ClientValidate('getbill')){ return false; } this.disabled = true; this.value = 'Processing...';"
                                                    UseSubmitBehavior="false" CssClass="btn btn-primary btn-block"
                                                    OnClick="btn_Getbill_Click" Visible="false" />
                                                &nbsp;&nbsp;
                                                <asp:Button ID="btn_PaybillGujrat" runat="server" Text="Pay Bill" ValidationGroup="paybill" OnClientClick="if (!Page_ClientValidate('paybill')){ return false; } this.disabled = true; this.value = 'Processing...';"
                                                    UseSubmitBehavior="false" CssClass="btn btn-primary btn-block"
                                                    Visible="false" OnClick="btn_PaybillGujrat_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6 grid-margin stretch-card">
                                <div class="card">
                                    <div class="card-body">
                                        <img src="../images/BBPS-LOGO.png" />
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                                            CssClass="table table-bordered" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="BILL DETAILS" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <div class="col-lg-6">
                                                            <p class="clearfix">
                                                                <span class="float-left">
                                                                    <label class="col-form-label">BILL NUMBER:</label>
                                                                </span>
                                                                <span class="float-right text-muted">
                                                                    <label class="col-form-label"><%#Eval("billnumber") %></label>
                                                                </span>
                                                            </p>
                                                            <p class="clearfix">
                                                                <span class="float-left">
                                                                    <label class="col-form-label">BILL DATE</label>
                                                                </span>
                                                                <span class="float-right text-muted">
                                                                    <label class="col-form-label"><%#Eval("billdate") %></label>

                                                                </span>
                                                            </p>
                                                            <p class="clearfix">
                                                                <span class="float-left">
                                                                    <label class="col-form-label">PERIOD</label>
                                                                </span>
                                                                <span class="float-right text-muted">
                                                                    <label class="col-form-label"><%#Eval("billperiod") %></label>
                                                                </span>
                                                            </p>
                                                            <p class="clearfix">
                                                                <span class="float-left">
                                                                    <label class="col-form-label">NAME</label>
                                                                </span>
                                                                <span class="float-right text-muted">
                                                                    <label class="col-form-label"><%#Eval("customername") %></label>
                                                                </span>
                                                            </p>
                                                            <p class="clearfix">
                                                                <span class="float-left">
                                                                    <label class="col-form-label">DUE AMOUNT</label>
                                                                </span>
                                                                <span class="float-right text-muted">
                                                                    <label class="col-form-label"><%#Eval("dueamount") %></label>
                                                                </span>
                                                            </p>
                                                            <p class="clearfix">
                                                                <span class="float-left">
                                                                    <label class="col-form-label">DUE DATE</label>
                                                                </span>
                                                                <span class="float-right text-muted">
                                                                    <label class="col-form-label"><%#Eval("duedate") %></label>

                                                                </span>
                                                            </p>

                                                        </div>
                                                        <div class="col-lg-8">
                                                            <asp:Button ID="btn_paynow" runat="server" CssClass="btn btn-primary btn-block" Text="PayNow" ValidationGroup="getbill"
                                                                CommandName="pay" CommandArgument='<%# Eval("reference_id") %>' OnClientClick="if (!Page_ClientValidate('getbill')){ return false; } this.disabled = true; this.value = 'Processing...';" UseSubmitBehavior="false" />
                                                        </div>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
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

