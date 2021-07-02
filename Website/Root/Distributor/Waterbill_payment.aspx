<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="Waterbill_payment.aspx.cs" Inherits="Root_Distributor_Waterbill_payment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Water bill Payment
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
                                                <label class="col-form-label">Water Bill Type  <code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:DropDownList ID="DROPE" runat="server" Height="35px" CssClass="form-control">
                                                    <asp:ListItem>Select Type</asp:ListItem>
                                                    <asp:ListItem Value="AMCW">Ahmedabad Municipal Corporation</asp:ListItem>
                                                    <asp:ListItem Value="BMCW">Bhopal Municipal Corporation - Water  </asp:ListItem>
                                                    <asp:ListItem Value="DDAW"> Delhi Development Authority (DDA) - Water  </asp:ListItem>
                                                    <asp:ListItem Value="DJBW">Delhi Jal Board  </asp:ListItem>
                                                    <asp:ListItem Value="DPHE">Department of Public Health Engineering-Water, Mizoram </asp:ListItem>
                                                    <asp:ListItem Value="GWMC">Greater Warangal Municipal Corporation - Water </asp:ListItem>
                                                    <asp:ListItem Value="FGIL">Gwalior Municipal Corporation - Water </asp:ListItem>
                                                    <asp:ListItem Value="HMWS">Hyderabad Metropolitan Water Supply and Sewerage Board</asp:ListItem>
                                                    <asp:ListItem Value="IMCW">Indore Municipal Corporation - Water </asp:ListItem>
                                                    <asp:ListItem Value="JMCW">Jabalpur Municipal Corporation - Water</asp:ListItem>
                                                    <asp:ListItem Value="MCJW"> Municipal Corporation Jalandhar</asp:ListItem>
                                                    <asp:ListItem Value="MCLW">Municipal Corporation Ludhiana - Water</asp:ListItem>
                                                    <asp:ListItem Value="MCAW">Municipal Corporation of Amritsar</asp:ListItem>
                                                    <asp:ListItem Value="MCCW">Municipal Corporation of Gurugram</asp:ListItem>
                                                    <asp:ListItem Value="AVLI">Mysuru City Corporation</asp:ListItem>
                                                    <asp:ListItem Value="NDMC">New Delhi Municipal Council (NDMC) - Water</asp:ListItem>
                                                    <asp:ListItem Value="PMCW"> Pune Municipal Corporation - Water</asp:ListItem>
                                                    <asp:ListItem Value="SGMC">Surat Municipal Corporation - Water</asp:ListItem>
                                                    <asp:ListItem Value="UNNW"> Ujjain Nagar Nigam - PHED</asp:ListItem>
                                                    <asp:ListItem Value="UJSW">Uttarakhand Jal Sansthan</asp:ListItem>
                                                    <asp:ListItem Value="PHED"> Rajasthan - Water </asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="DROPE" ErrorMessage="*" ValidationGroup="ev"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">K. Number/Account No.<code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txt_knumber" runat="server" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="ev" ErrorMessage="Enter K Number /Account Number"
                                                    ControlToValidate="txt_knumber"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:button id="Button1" runat="server" text="Search "
                                            cssclass="btn btn-primary"
                                            xmlns:asp="#unknown"
                                            validationgroup="ev" onclick="btnsearch_click"></asp:button>
                                    </div>
                                </div>
                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">Customer Name<code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txt_name" runat="server" Enabled="false" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="" ErrorMessage="Enter Customer Name"
                                                    ControlToValidate="txt_name"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                       <%-- <div class="form-group row">
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
                                        </div>--%>
                                     <%--   <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">Bill Unit<code></code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txt_billunite" runat="server" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>

                                            </div>
                                        </div>--%>
                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">Due Date <code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txt_date"  runat="server" MaxLength="50" Enabled="false" onkeypress="return false;"
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
                                                <asp:TextBox ID="txt_amount" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="getbill" ErrorMessage="Enter Amount "
                                                    ControlToValidate="txt_amount"></asp:RequiredFieldValidator>
                                              
                                            </div>
                                        </div>



                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:Button ID="btn_Getbill" runat="server" Text="Pay Bill" ValidationGroup="getbill" OnClientClick="if (!Page_ClientValidate('getbill')){ return false; } this.disabled = true; this.value = 'Processing...';"
                                                    UseSubmitBehavior="false" CssClass="btn btn-primary"
                                                    OnClick="btn_paybill" />

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

