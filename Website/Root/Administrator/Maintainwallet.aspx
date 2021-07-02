<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true"
    CodeFile="Maintainwallet.aspx.cs" Inherits="Root_Admin_Maintainwallet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

       <script src="../../Design/js/angular.min.js"></script>
        <script src="../Angularjsapp/angucomplete-alt.js"></script>
    <script src="../Angularjsapp/home.js"></script>

    <link href="../../Design/css/modelpopup.css" rel="stylesheet" />
    
    <script lang="JavaScript" type="text/javascript">
        var message = 'Right Click is disabled';
        function clickIE() { if (event.button == 2) { alert(message); return false; } }
        function clickNS(e)
        {
            if (document.layers || (document.getElementById && !document.all))
            {
                if (e.which == 2 || e.which == 3) { alert(message); return false; }
            }
        }
        if (document.layers) { document.captureEvents(Event.MOUSEDOWN); document.onmousedown = clickNS; }
        else if (document.all && !document.getElementById) { document.onmousedown = clickIE; }
        document.oncontextmenu = new Function('alert(message);return false')
    </script>
    <script src="../Angularjsapp/angucomplete-alt.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="content-wrapper">

        <div class="page-header">
            <h3 class="page-title">Maintain Minimum Wallet Balance
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
                                        <label class="col-form-label">MemberType<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8" >
                                        <asp:DropDownList ID="ddl_membertype" runat="server" CssClass="form-control" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddl_membertype"
                                            Display="Dynamic" ErrorMessage="Please Select MemberType!" SetFocusOnError="True"
                                            ValidationGroup="v" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Wallet<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8" >
                                        <asp:DropDownList ID="ddl_wallet" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_wallet_SelectedIndexChanged">
                                            <asp:ListItem Text="Select Wallet" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Ewallet" Value="E"></asp:ListItem>
                                            <asp:ListItem Text="Aeps Wallet" Value="R"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvPackage" runat="server" ControlToValidate="ddl_wallet"
                                            Display="Dynamic" ErrorMessage="Please Select Wallet !" SetFocusOnError="True"
                                            ValidationGroup="v" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Amount<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtAmount" runat="server" MaxLength="10" Text="0.0" CssClass="form-control"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtAmount_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" FilterType="Numbers,Custom" TargetControlID="txtAmount" ValidChars=".">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ControlToValidate="txtAmount"
                                            Display="Dynamic" ErrorMessage="Please Enter Amount !" ForeColor="Red" SetFocusOnError="True"
                                            ValidationGroup="v"></asp:RequiredFieldValidator>

                                    </div>
                                </div>

                             
                              
                                
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" OnClick="btnSubmit_Click" OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true; this.value = 'Submitting...';"
                                            UseSubmitBehavior="false" class="mdc-button mdc-button--raised mdc-ripple-upgraded" />
                                        &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" class="mdc-button mdc-button--raised filled-button--secondary mdc-ripple-upgraded" />
                                      

                                    </div>
                                </div>



                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
       
    </div>
</asp:Content>
