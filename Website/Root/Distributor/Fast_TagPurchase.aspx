<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/membermaster.master"
    AutoEventWireup="true" CodeFile="Fast_TagPurchase.aspx.cs" Inherits="Root_Distributor_Fast_TagPurchase"
    MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
                <ol class="breadcrumb">
                   
                </ol>
            </section>
    <section>
             <table class="table table-bordered table-hover ">
                    <tr>
                        <td align="center">
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Underline="True" 
                                Text="Fast Tag Purchase Form"></asp:Label>
                                </td>
                                </tr>
                    <tr>
                        <td>
                            <div id="wizard" class="swMain">
                                <br />
                                <strong class="star">Note : Fields with <span class="red">*</span> are mandatory fields.</strong>&nbsp;&nbsp;
                                <br />
                                  <div>
                                        <table class="table table-bordered table-hover ">
                                            <tr>
                                                <td style="text-align:center">
                                                    <div id="ddiv" runat="server" >
                                                        <table class="table">
                                                            <tr>
                                                                <td class="aleft" colspan="3">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <h3>Amount For Per Fast Tag(In Rs):- </h3>
                                                                </td>
                                                                <td>
                                                                    <h3>
                                                                        <asp:Label ID="lblamt" runat="server" Font-Bold="true" Font-Size="Medium" 
                                                        ForeColor="Green" Text=""></asp:Label>
                                                                    </h3>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                            </tr>
                                                            <tr id="pan2" runat="server">
                                                                <td>
                                                                    <h5><strong><span class="red">*</span>How Many Fast Tag Do you want to buy? </strong></h5>
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:TextBox ID="txt_couponbuy" runat="server" AutoPostBack="True" 
                                                    CssClass="form-control" MaxLength="50" 
                                                    ontextchanged="txt_couponbuy_TextChanged"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                    ControlToValidate="txt_couponbuy" ErrorMessage="*" ValidationGroup="v"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><strong>Total Amount Deducted From Your Wallet</strong></td>
                                                                <td colspan="2">
                                                                      <asp:TextBox ID="Label2" runat="server" AutoPostBack="True" CssClass="form-control" MaxLength="50"  Enabled="false" Text="0"></asp:TextBox>
                                                   
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><strong>Delievery Address</strong></td>
                                                                <td colspan="2">
                                                                    <asp:TextBox ID="txt_delieveryaddress" runat="server" CssClass="form-control" MaxLength="50" Height="95px" TextMode="MultiLine" Width="178px"></asp:TextBox>
                                                                   <asp:RequiredFieldValidator ID="ref" runat="server" ControlToValidate="txt_delieveryaddress" ValidationGroup="v" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;&nbsp;&nbsp;
                                            </td>
                                                                <td>&nbsp;&nbsp;&nbsp;
                                            </td>
                                                                <td>
                                                                    <asp:Button ID="btnpurchase" runat="server" class="btn btn-primary" 
                                                    OnClick="btnpurchase_Click" Text="Submit" ValidationGroup="v" OnClientClick="return confirm('Are you sure you want to purchase ?')"  />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">
                                                                    <asp:ValidationSummary ID="ValidationSummary" runat="server" 
                                                    ClientIDMode="Static" ValidationGroup="v" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>

                                        </table>
                                        </div>
                                </td>
                                </tr>
                                </table>
            </section>
</asp:Content>--%>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Fast Tag Purchase
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
                                        <label class="col-form-label">Amount For Per Fast Tag(In Rs):-<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Label ID="lblamt" runat="server" Font-Bold="true" Font-Size="Medium"
                                            ForeColor="Green" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">How Many Fast Tag Do you want to buy?<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                         <asp:TextBox ID="txt_couponbuy" runat="server" AutoPostBack="True" 
                                                    CssClass="form-control" MaxLength="50" 
                                                    ontextchanged="txt_couponbuy_TextChanged"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter fast tag buy" ControlToValidate="txt_couponbuy"></asp:RequiredFieldValidator>
                                                      

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Total Amount Deducted From Your Wallet<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="Label2" runat="server" AutoPostBack="True" CssClass="form-control" MaxLength="50" Enabled="false" Text="0"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Delievery Address<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_delieveryaddress" runat="server" CssClass="form-control" MaxLength="50" TextMode="MultiLine"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_delieveryaddress"  ErrorMessage="Please Enter Address" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Button ID="btnpurchase" runat="server" class="btn btn-primary"
                                            OnClick="btnpurchase_Click" Text="Submit" />
                                        <asp:Button ID="btnReset" runat="server" Text="Reset"
                                            CssClass="btn btn-danger" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                <asp:PostBackTrigger ControlID="btnpurchase" />
            </Triggers>
        </asp:UpdatePanel>

    </div>
</asp:Content>
