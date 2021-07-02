<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master"
    AutoEventWireup="true" CodeFile="Setting_RegistrationFee.aspx.cs" Inherits="Root_Admin_Setting_RegistrationFee" %>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">
                  Manage Registration Fees
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
                                      <asp:DropDownList ID="ddlSourceMember" runat="server" CssClass="form-control class" AutoPostBack="true" onselectedindexchanged="ddlSourceMember_SelectedIndexChanged">
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
                                         <asp:DropDownList ID="ddlmymembertype" runat="server" CssClass="form-control class" AutoPostBack="true" onselectedindexchanged="ddlMemberType_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlmymembertype"
                                    Display="Dynamic" ErrorMessage="Please Select member type !" SetFocusOnError="True"
                                    ValidationGroup="v0" InitialValue="0">*</asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                           <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Admin Profit</label>
                                    </div>
                                    <div class="col-lg-8">
                                     <asp:TextBox ID="DSO1_Admin" runat="server" Text="0" ClientIDMode="Static" onkeyup="calc1('DSO1_Admin','DSO1_self','DSO1_tot');" CssClass="form-control"></asp:TextBox> 
                                        <cc1:FilteredTextBoxExtender ID="txtZIP_FilteredTextBoxExtender" runat="server" Enabled="True"
                                                FilterType="Numbers" TargetControlID="DSO1_Admin">
                                            </cc1:FilteredTextBoxExtender>

                                    </div>
                                </div>

                                               <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">My Profit</label>
                                    </div>
                                    <div class="col-lg-8">
                                   <asp:TextBox ID="DSO1_self" runat="server" Text="0" ClientIDMode="Static" CssClass="form-control" onkeyup="calc1('DSO1_Admin','DSO1_self','DSO1_tot');"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                FilterType="Numbers" TargetControlID="DSO1_self">
                                            </cc1:FilteredTextBoxExtender>

                                    </div>
                                </div>


                                                        <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Net Joining Fees</label>
                                    </div>
                                    <div class="col-lg-8">
                                 <asp:TextBox ID="DSO1_tot" runat="server" CssClass="form-control" Text="0" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                FilterType="Numbers" TargetControlID="DSO1_tot">
                                            </cc1:FilteredTextBoxExtender>

                                    </div>
                                </div>

                                                          <div class="form-group row">
                                                                             <div class="col-lg-3">
                                        
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Button ID="btnSuccess" runat="server" Text="Submit Setting" OnClick="btnSubmit_Click" ValidationGroup="v0" class="btn btn-primary" />
                                        </div>
                                                              </div>
                                </div>
                            </div></div></div></ContentTemplate></asp:UpdatePanel></div>

</asp:Content>
