<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true"
    CodeFile="DeductFund.aspx.cs" Inherits="Root_Admin_DeductFund" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       <%--<script src="../../Design/js/angular.min.js"></script>
        <script src="../Angularjsapp/angucomplete-alt.js"></script>
    <script src="../Angularjsapp/home.js"></script>--%>
    <link href="../../Design/css/modelpopup.css" rel="stylesheet" />
     <script src="../../Scripts/chosen/jquery.min.js"></script>
      <script src="../../Scripts/chosen/chosen.jquery.min.js"></script>
   <link href="../../Scripts/chosen/chosen.min.css" rel="stylesheet" />
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Deduct Fund In E Wallet
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
                                        <label class="col-form-label">MemberID<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8" >
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
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" OnClick="btnSubmit_Click" OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true; this.value = 'Submitting...';"
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
      
    </div>
</asp:Content>
