<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/membermaster.master"
    AutoEventWireup="true" CodeFile="Fast_TagRecharge.aspx.cs" Inherits="Root_DistributorFast_TagRecharge"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                                Text="Fast Tag Recharge"></asp:Label>
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
                                                                <td class="aleft" colspan="2">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>Mobile Number Linked with Fasttag</td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_moible" runat="server" CssClass="form-control" MaxLength="11"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txt_vehicle" ForeColor="Red"></asp:RequiredFieldValidator>
                                                       <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Mobile" runat="server" TargetControlID="txt_moible"
                                                ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:RegularExpressionValidator ID="regExp_Mobile" runat="server" CssClass="rfv"
                                                ControlToValidate="txt_moible" ErrorMessage="Input correct mobile number !"
                                                SetFocusOnError="true" ValidationExpression="^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$"><img src="../images/warning.png"/></asp:RegularExpressionValidator>
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Vehicle Number</td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_vehicle" runat="server" CssClass="form-control"></asp:TextBox>
                                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txt_vehicle" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Bank Associated with Fastag</td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_bankname" runat="server" CssClass="form-control"></asp:TextBox>
                                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txt_bankname" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Recharge Amount</td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_rechargeamunt" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="txt_rechargeamunt" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                       <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" ValidChars="0123456789" TargetControlID="txt_rechargeamunt">
                                                                        </cc1:FilteredTextBoxExtender>
                                                
                                            </td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:Button ID="btn_Submit" runat="server" Text="Recharge" CssClass="btn btn-primary" OnClick="btn_Submit_Click" />
                                            &nbsp;</td>
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
            <h3 class="page-title">Fast Tag Recharge
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
                                        <label class="col-form-label">Mobile Number Linked with Fasttag<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_moible" runat="server" CssClass="form-control" MaxLength="11"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Fasttag Mobile Number" ControlToValidate="txt_vehicle" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Mobile" runat="server" TargetControlID="txt_moible"
                                            ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="regExp_Mobile" runat="server" CssClass="rfv"
                                            ControlToValidate="txt_moible" ErrorMessage="Input correct mobile number !"
                                            SetFocusOnError="true" ValidationExpression="^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$"><img src="../images/warning.png"/></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Vehicle Number<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_vehicle" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Vehicle Number " ControlToValidate="txt_vehicle" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Bank Associated with Fastag<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_bankname" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter Fastag Name" ControlToValidate="txt_bankname" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Recharge Amount<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_rechargeamunt" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter Recharge Amount" ControlToValidate="txt_rechargeamunt" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" ValidChars="0123456789" TargetControlID="txt_rechargeamunt">
                                        </cc1:FilteredTextBoxExtender>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Recharge" CssClass="btn btn-primary" OnClick="btn_Submit_Click" />
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
                <asp:PostBackTrigger ControlID="btn_Submit" />
            </Triggers>
        </asp:UpdatePanel>

    </div>
</asp:Content>
