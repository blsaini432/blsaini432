<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master"
    AutoEventWireup="true" CodeFile="Setting_MemberPlan.aspx.cs" Inherits="Root_Admin_Setting_MemberPlan" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<script language="javascript" type="text/javascript">
    function calc1(a, b, c) {
        var x = document.getElementById(a);
        var y = document.getElementById(b);
        var z = document.getElementById(c);
        if (x.value == "") { x.value = '0'; }
        if (y.value == "") { y.value = '0'; }
        z.value = (1 * x.value) + (1 * y.value);
    }
    </script>
    <style type="text/css">
        .style1
        {
            width: 172px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Setting Member Plan
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
                                        <label class="col-form-label">  Registration Made by Member Type<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                    <asp:DropDownList ID="ddlSourceMember" runat="server" AutoPostBack="true" onselectedindexchanged="ddlSourceMember_SelectedIndexChanged" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSourceMember"
                                    Display="Dynamic" ErrorMessage="Please Select member type !" SetFocusOnError="True" 
                                    ValidationGroup="v0" InitialValue="0">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                            <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Registration of Member Type<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                    <asp:DropDownList ID="ddlmymembertype" runat="server" AutoPostBack="true" CssClass="form-control" onselectedindexchanged="ddlMemberType_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlmymembertype"
                                    Display="Dynamic" ErrorMessage="Please Select member type !" SetFocusOnError="True" 
                                    ValidationGroup="v0" InitialValue="0">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                       <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label"> Per Id Fees<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                <asp:TextBox ID="DSO1_tot" runat="server" ClientIDMode="Static" Text="0" CssClass="form-control"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="DSO1_tot_FilteredTextBoxExtender" 
                                    runat="server" Enabled="True" FilterType="Numbers" TargetControlID="DSO1_tot">
                                </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                
                                       <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                      <asp:Button ID="btnSuccess" runat="server" CssClass="btn btn-primary" 
                                    OnClick="btnSubmit_Click" Text="Submit Setting" ValidationGroup="v0" />
                                    </div>
                                </div>

                                </div></div></div></div></ContentTemplate></asp:UpdatePanel></div>




</asp:Content>
