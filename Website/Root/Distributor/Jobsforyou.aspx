<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="Jobsforyou.aspx.cs" Inherits="Root_Distributor_Jobsforyou" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Jobs for you
            </h3>
        </div>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Name<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_cardname" CssClass="form-control" runat="server"
                                    autocomplete="off" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_cardname"
                                    SetFocusOnError="true" ErrorMessage="Enter name" ValidationGroup="v"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Mobile Number   <code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_mobile" CssClass="form-control" runat="server"
                                    autocomplete="off" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_mobile"
                                    SetFocusOnError="true" ErrorMessage="Enter mobile number" ValidationGroup="v"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="regExp_Mobile" runat="server" CssClass="rfv"
                                    ControlToValidate="txt_mobile" ErrorMessage="Input correct mobile number !" ValidationGroup="v"
                                    SetFocusOnError="true" ValidationExpression="^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[6789]\d{9}$"></asp:RegularExpressionValidator>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txt_mobile"
                                    ValidChars="0123456789">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">DOB   <code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_date" runat="server" MaxLength="50" onkeypress="return false;"
                                    CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender runat="server" ID="txt_doasdsab" Format="dd/MM/yyyy" Animated="False"
                                    PopupButtonID="txt_date" TargetControlID="txt_date">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_date"
                                    Display="Dynamic" ErrorMessage="Please Enter Date  (dd/MM/yyyy) !" SetFocusOnError="True"
                                    ValidationGroup="v"></asp:RequiredFieldValidator>

                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Full Address<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_address" CssClass="form-control" runat="server"
                                    autocomplete="off" MaxLength="200"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_address"
                                    SetFocusOnError="true" ErrorMessage="Enter FullAddress" ValidationGroup="v"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Experience<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_experance" CssClass="form-control" runat="server" placeholder="1 year"
                                    autocomplete="off" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_experance"
                                    SetFocusOnError="true" ErrorMessage="Enter Experience  " ValidationGroup="v"></asp:RequiredFieldValidator>

                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_experance"
                                    ValidChars="0123456789">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">In Which Field Have you Worked<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_working" CssClass="form-control" runat="server"
                                    autocomplete="off" MaxLength="150"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_working"
                                    SetFocusOnError="true" ErrorMessage="Enter worked" ValidationGroup="v"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Your Last Salary <code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_salary" CssClass="form-control" runat="server"
                                    autocomplete="off" MaxLength="12"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_salary"
                                    SetFocusOnError="true" ErrorMessage="Enter Salary" ValidationGroup="v"></asp:RequiredFieldValidator>

                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txt_salary"
                                    ValidChars="0123456789">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Want Salary Now <code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_wantsalary" CssClass="form-control" runat="server"
                                    autocomplete="off" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_wantsalary"
                                    SetFocusOnError="true" ErrorMessage="Enter Want Salary Now " ValidationGroup="v"></asp:RequiredFieldValidator>

                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txt_wantsalary"
                                    ValidChars="0123456789">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">What are you doing Currently <code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:RadioButtonList ID="rdoBtnsLstGender" runat="server" CssClass="form-control" Width="193px" RepeatDirection="Horizontal">
                                    <asp:ListItem> Job </asp:ListItem>
                                    <asp:ListItem> Free</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="rdoBtnsLstGender" ErrorMessage="check mark" ValidationGroup="v"></asp:RequiredFieldValidator>

                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Upload Photo<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:FileUpload ID="fu_Identity" runat="server" />&nbsp;
                        <asp:Button ID="btn_Identity" runat="server" Text="Upload Aadhar" CssClass="btn btn-warning btn-rounded btn-fw"
                            OnClick="btn_Identity_Click" />


                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-3">
                            </div>
                            <div class="col-lg-8">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit "
                                    CssClass="btn btn-primary"
                                    ValidationGroup="v" OnClick="btnSubmit_Click"></asp:Button>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

