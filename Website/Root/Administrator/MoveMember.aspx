<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true" CodeFile="MoveMember.aspx.cs" Inherits="Root_Admin_MoveMember" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../Scripts/chosen/jquery.min.js"></script>
    <script src="../../Scripts/chosen/chosen.jquery.min.js"></script>
    <link href="../../Scripts/chosen/chosen.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Move Member
            </h3>
        </div>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <div class="loading-overlay">
                                    <div class="wrapper">
                                        <div class="ajax-loader-outer">
                                            Loading...
                                        </div>
                                    </div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div id="middiWarapperinner">
                                    <div id="Warapperhead">
                                    </div>
                                    <div class="content mydash">
                                        <asp:MultiView ID="mvw" runat="server" ActiveViewIndex="0">
                                            <asp:View ID="v1" runat="server">
                                                <table class="table table-bordered table-hover xtbl">
                                                    <tr>
                                                        <td colspan="3" class="aleft">
                                                            <strong class="star">STEP 1 of 4 : Fields with <span class="red">*</span> are mandatory
                                            fields.</strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td1">
                                                            <span class="red">*</span> Enter Member ID
                                                        </td>
                                                        <td class="td2">:
                                                        </td>
                                                        <td class="td3">
                                                            <asp:TextBox ID="txtMobile" runat="server" MaxLength="10" CssClass="form-control"
                                                                AutoPostBack="true" OnTextChanged="txtMobile_TextChanged"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvCurrentPassword" runat="server" ErrorMessage="Please Enter Member ID !"
                                                                ControlToValidate="txtMobile" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Literal ID="litMemberInfo" runat="server" Text="Member Details will appear here .."></asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" OnClick="btnSubmit_Click"
                                                                Visible="false" CssClass="btn btn-primary" />
                                                            &nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-primary"
                                            Visible="false" />
                                                            <asp:ValidationSummary ID="ValidationSummary" runat="server" ClientIDMode="Static"
                                                                ValidationGroup="v" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:View>
                                            <asp:View ID="vw2" runat="server">
                                                <table class="table table-bordered table-hover xtbl">
                                                    <tr>
                                                        <td colspan="3" class="aleft">
                                                            <strong class="star">STEP 2 of 4 : We have sent an OTP at your Registered Mobile Number..</strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Member Details
                                                        </td>
                                                        <td>:
                                                        </td>
                                                        <td>
                                                            <asp:Literal ID="litMemberDetails1" runat="server" Text="Member Details will appear here .."></asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Parent Member Details
                                                        </td>
                                                        <td>:
                                                        </td>
                                                        <td>
                                                            <asp:Literal ID="litParentMemberDetails" runat="server" Text="Upline Member Details will appear here .."></asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Current Designation
                                                        </td>
                                                        <td>:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblDesignation" runat="server" CssClass="form-control"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td1">
                                                            <span class="red">*</span> New Owner ID
                                                        </td>
                                                        <td class="td2">:
                                                        </td>
                                                        <td class="">
                                                            <asp:DropDownList ID="ddlNewOwnerID" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlMembertype_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select New Owner !"
                                                                ControlToValidate="ddlNewOwnerID" Display="Dynamic" InitialValue="0" SetFocusOnError="True"
                                                                ValidationGroup="va"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                                                        </td>


                                                    </tr>


                                                    <tr>
                                                        <td>
                                                            <span class="red">*</span> Owner Details
                                                        </td>
                                                        <td>:
                                                        </td>
                                                        <td>
                                                            <asp:Literal ID="LitNewOwner" runat="server"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnOTPsubmit" runat="server" Text="Submit" ValidationGroup="va" CssClass="btn btn-primary"
                                                                OnClick="btnOTPsubmit_Click" />
                                                            &nbsp;&nbsp;&nbsp;
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ClientIDMode="Static"
                                            ValidationGroup="va" />
                                                        </td>
                                                    </tr>

                                                </table>



                                            </asp:View>


                                            <asp:View ID="vv3" runat="server">
                                                <table class="table table-bordered table-hover xtbl">
                                                    <tr>
                                                        <td colspan="3" class="aleft">
                                                            <strong class="star">Success !! Member account has been upgraded successfully.</strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td1">
                                                            <span class="red">*</span> Member New Details
                                                        </td>
                                                        <td class="td2">:
                                                        </td>
                                                        <td class="td3">
                                                            <asp:Literal ID="lblNewMemberDetails" runat="server"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:View>
                                        </asp:MultiView>
                                    </div>


                                </div>

                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
                <%--<script>
                    $('#<%=ddlNewOwnerID.ClientID%>').chosen();
                </script>--%>
            </div>
        </div>
    </div>



</asp:Content>

