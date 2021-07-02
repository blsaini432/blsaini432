<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master"
    AutoEventWireup="true" CodeFile="psa_coupon.aspx.cs" Inherits="Root_Retailer_psa_coupon"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">PSA -UTI Coupon Purchase Panel
            </h3>
        </div>
                <div class="row grid-margin">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body">
                                <asp:Label ID="Label1" runat="server" Visible="false" Style="color: Red"></asp:Label>
                                <div class="form-group row">
                                    <div class="col-lg-4">
                                        <label class="col-form-label">Amount For Per Coupon (In Rs):-<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Label ID="lblamt" runat="server" Font-Bold="true" Font-Size="Medium"
                                            ForeColor="Green" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-4">
                                        <label class="col-form-label">PSA ID<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_psaid" runat="server" Enabled="false" CssClass="form-control"
                                            MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txt_psaid" ErrorMessage="*" ValidationGroup="v"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-4">
                                        <label class="col-form-label">PSA Password<code>*</code></label>
                                    </div>
                                      <div class="col-lg-8">
                                    <asp:TextBox ID="txt_psapassword" runat="server" Enabled="false" CssClass="form-control"
                                        MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="txt_psapassword" ErrorMessage="*" ValidationGroup="v"></asp:RequiredFieldValidator>
                                          </div>
                                </div>




                                <div class="form-group row">
                                    <div class="col-lg-4">
                                        <label class="col-form-label">PSA Link<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <a href="https://www.psaonline.utiitsl.com/psaonline/" target="_blank" class="form-control"><b>Click Here</b> </a>
                                    </div>
                                </div>


                                <div class="form-group row">
                                    <div class="col-lg-4">
                                        <label class="col-form-label">How Many Coupons Do you want to buy?<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_couponbuy" runat="server" AutoPostBack="True"
                                            CssClass="form-control" MaxLength="50"
                                            ></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Amount" runat="server" TargetControlID="txt_couponbuy"
                                    ValidChars="0123456789"/>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="txt_couponbuy" ErrorMessage="*" ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </div>
                                </div>



                                <div class="form-group row">
                                    <div class="col-lg-4">
                                        
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Button ID="btnpurchase" runat="server" class="btn btn-primary"
                                            OnClick="btnpurchase_Click" Text="Submit"/>
                                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Large"
                                            Text=""></asp:Label>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
       
    </div>
</asp:Content>
