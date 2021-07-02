<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true"
    CodeFile="AddFund.aspx.cs" Inherits="Root_Admin_AddFundInEWallet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .modalPopup {
            background-color: #FFFFFF;
            border-width: 1px;
            border-style: solid;
            border-color: black;
            padding-left: 10px;
            width: 270px;
            height: 185px;
        }
    </style>
   <%-- <script src="../../Design/js/angular.min.js"></script>
    <script src="../Angularjsapp/angucomplete-alt.js"></script>
    <script src="../Angularjsapp/home.js"></script>--%>
    <link href="../../Design/css/modelpopup.css" rel="stylesheet" />
    <script src="../../Scripts/chosen/jquery.min.js"></script>
    <script src="../../Scripts/chosen/chosen.jquery.min.js"></script>
    <link href="../../Scripts/chosen/chosen.min.css" rel="stylesheet" />
    <script lang="JavaScript" type="text/javascript">
        var message = 'Right Click is disabled';
        function clickIE() { if (event.button == 2) { alert(message); return false; } }
        function clickNS(e) {
            if (document.layers || (document.getElementById && !document.all)) {
                if (e.which == 2 || e.which == 3) { alert(message); return false; }
            }
        }
        if (document.layers) { document.captureEvents(Event.MOUSEDOWN); document.onmousedown = clickNS; }
        else if (document.all && !document.getElementById) { document.onmousedown = clickIE; }
        document.oncontextmenu = new Function('alert(message);return false')
    </script>
   <%-- <script src="../Angularjsapp/angucomplete-alt.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Add Fund In E Wallet
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
                                        <label class="col-form-label">Give Credit<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:CheckBox ID="chkCredit" runat="server" CssClass="form-control" />
                                        (select if you give credit)
                                    </div>
                                </div>
                                <div class="form-group row">


                                    <div class="col-lg-3">
                                        <label class="col-form-label">MemberID<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="ddl_members" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_members_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:Label ID="lblMemberName" runat="server" CssClass="green"></asp:Label>
                                        <asp:Label ID="lblEWalletBalance" runat="server" CssClass="green"></asp:Label>
                                        <asp:HiddenField ID="hidMsrNo" runat="server" />
                                        <asp:HiddenField ID="hidMobile" runat="server" />
                                        <asp:RequiredFieldValidator ID="rfvPackage" runat="server" ControlToValidate="ddl_members"
                                            Display="Dynamic" ErrorMessage="Please Select MemberId !" SetFocusOnError="True"
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
                                        <label class="col-form-label">Narration<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtNarration" runat="server" TextMode="MultiLine" Height="100px"
                                            CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNarration" runat="server" ControlToValidate="txtNarration"
                                            Display="Dynamic" ErrorMessage="Please Enter Narration !" ForeColor="Red" SetFocusOnError="True"
                                            ValidationGroup="v"></asp:RequiredFieldValidator>

                                    </div>
                                </div>


                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" OnClick="btn_dmrotp_Click" 
                                            UseSubmitBehavior="false" class="btn btn-primary" />
                                        &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" class="btn btn-primary" />
                                        <asp:ValidationSummary ID="ValidationSummary" runat="server" ClientIDMode="Static"
                                            ValidationGroup="v" />

                                    </div>
                                </div>
                            </div>
                            <script>
                                $('#<%=ddl_members.ClientID%>').chosen();
                            </script>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <input type="button" value="OpenModalPopup" id="btn_opendmr" runat="server" style="display: none;" />
        <input type="button" value="CloseModalPopup" id="btn_closedmrc" runat="server" style="display: none;" />
        <cc1:ModalPopupExtender ID="mpe_dmrotp" runat="server" PopupControlID="pnltransotp"
            TargetControlID="btn_opendmr" CancelControlID="btn_closedmrc" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnltransotp" runat="server" CssClass="modalPopup" align="center" Style="display: none; width: 50%;">
            <div class="page-header">
                <h3 class="page-title">OTP For Transaction
                </h3>
            </div>
            Enter OTP:<asp:TextBox ID="txt_dmrotp" runat="server" Height="25px" Width="152px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_dmrotp"
                ErrorMessage="*" ValidationGroup="aas"></asp:RequiredFieldValidator>
            <br />
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btn_dmrotp" runat="server" Text="Submit" CssClass="btn btn-primary"
                            ValidationGroup="aas" Width="104px" OnClick="btnSubmit_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Wait...'" />
                        <asp:Button ID="btn_Closedmr" runat="server" ValidationGroup="daas" CssClass="btn btn-danger"
                            Text="Close" OnClick="btn_Closedmr_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
