<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true"
    CodeFile="ManageFundRequest.aspx.cs" Inherits="Root_Admin_ManageFundRequest" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script lang="javascript" type="text/javascript">
        function myvldt(a, b) {
            //            GetVerification();
            if (Page_ClientValidate(b) == true) {
                x = confirm("Are you sure to process ?");
                if (x == true) {
                    a.style.display = 'none';
                    return x;
                }
                else {

                    if (window.event) { //will be true with IE, false with other browsers
                        window.event.returnValue = false;
                    } //IE specific, seems to work
                    else {
                        return false;
                    }
                }
            }


        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Send Fund Add Request 
            </h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row">

                    <div class="col-lg-5">
                        <div class="card">
                            <div class="card-body">
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">From BankName<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="ddlBankName" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBankName" runat="server" ControlToValidate="ddlBankName"
                                            Display="Dynamic" ErrorMessage="Please Select From BankName !" ForeColor="Red"
                                            SetFocusOnError="True" ValidationGroup="v" InitialValue="0"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">To BankName<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="ddlBankTo" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBankTo" runat="server" ControlToValidate="ddlBankTo"
                                            Display="Dynamic" ErrorMessage="Please Select To BankName !" ForeColor="Red"
                                            SetFocusOnError="True" ValidationGroup="v" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>


                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Payment Mode<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="ddlPaymentMode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged"
                                            CssClass="form-control">
                                            <asp:ListItem Value="0">Select Payment Mode</asp:ListItem>
                                            <asp:ListItem Value="Cash">Cash</asp:ListItem>
                                            <asp:ListItem Value="Cheque">Cheque</asp:ListItem>
                                            <asp:ListItem Value="Demand Draft(DD)">Demand Draft(DD)</asp:ListItem>
                                            <asp:ListItem Value="IMPS">IMPS</asp:ListItem>
                                            <asp:ListItem Value="NEFT">NEFT</asp:ListItem>
                                             <asp:ListItem Value="UPI">UPI</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvPaymentMode" runat="server" ControlToValidate="ddlPaymentMode"
                                            Display="Dynamic" ErrorMessage="Please Enter PaymentMode !" ForeColor="Red" SetFocusOnError="True"
                                            ValidationGroup="v" InitialValue="0"></asp:RequiredFieldValidator>

                                    </div>
                                </div>



                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Payment Proof<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:FileUpload ID="FileUpload1" runat="server" Width="210" /><asp:HiddenField ID="hidPaymentProof"
                                            runat="server" />

                                    </div>
                                </div>



                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">
                                            <asp:Label ID="lblchqtitle" runat="server" Text="Cheque Or DDNumber"></asp:Label><code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtChequeOrDDNumber" runat="server" autocomplete="off" MaxLength="20" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvChequeOrDDNumber" runat="server" ControlToValidate="txtChequeOrDDNumber"
                                            Display="Dynamic" ErrorMessage="Please Enter Cheque Or DDNumber !" ForeColor="Red"
                                            SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>

                                    </div>
                                </div>

                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">
                                            <asp:Label ID="lbldttitle" runat="server" Text="ChequeDate"></asp:Label><code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtChequeDate" autocomplete="off" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                                        <cc1:CalendarExtender runat="server" ID="txtChequeDate_ce" Format="MM/dd/yyyy" Animated="False"
                                            PopupButtonID="txtChequeDate" TargetControlID="txtChequeDate">
                                        </cc1:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="rfvChequeDate" runat="server" ControlToValidate="txtChequeDate"
                                            Display="Dynamic" ErrorMessage="Please Enter ChequeDate !" ForeColor="Red" SetFocusOnError="True"
                                            ValidationGroup="v"></asp:RequiredFieldValidator>

                                    </div>
                                </div>

                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Fund Amount<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtTotalAmount"  autocomplete="off" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Amount" runat="server" TargetControlID="txtTotalAmount"
                                                ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="rfvTotalAmount" runat="server" ControlToValidate="txtTotalAmount"
                                            Display="Dynamic" ErrorMessage="Please Enter Amount !" ForeColor="Red" SetFocusOnError="True"
                                            ValidationGroup="v"></asp:RequiredFieldValidator>

                                        <asp:Label ID="lblredmsg" runat="server" Text="(Mininum Amount Should be 500)" ForeColor="Red" Font-Bold="false" Visible="false"></asp:Label>
                                        <asp:Label ID="lblTransactionCharge" runat="server" Text="" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label>

                                    </div>
                                </div>

                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Remarks<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtRemark" autocomplete="off" runat="server" MaxLength="500" TextMode="MultiLine" Height="100"
                                            CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>

                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" OnClick="btnSubmit_Click" CssClass="btn btn-primary" OnClientClick="return myvldt(this, 'v');" />

                                        <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-primary" />

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-7">
                        <div class="card">
                            <div class="card-body">
                                <asp:GridView ID="gvMemberBanker" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false"
                                    CellPadding="5" CssClass="table-responsive">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Member Name" SortExpression="MemberName">
                                            <ItemTemplate>
                                                <%#Eval("MemberName") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Bank Name" DataField="BankerMasterName" SortExpression="BankerMasterName" />
                                        <asp:BoundField HeaderText="Branch Name" DataField="BankBranch" SortExpression="BankBranch" />
                                        <asp:BoundField HeaderText="Account Type" DataField="AccountType" SortExpression="AccountType" />
                                        <asp:BoundField HeaderText="Account Number" DataField="AccountNumber" SortExpression="AccountNumber" />
                                        <asp:BoundField HeaderText="IFSC Code" DataField="IFSCCode" SortExpression="IFSCCode" />
                                    </Columns>
                                </asp:GridView>
                              
                            </div>
                        </div>
                       <div class="card">
                            <div class="card-body">
                        <img src="../../Uploads/FundRequest/Actual/qucodea.png" style="height:35%" />
                                <img src="../../Uploads/FundRequest/Actual/qucodea.png" />
                                 </div>
                           <img src="../../Uploads/FundRequest/Actual/qucodea.png" />
                            </div>
                                 <%-- <div class="card">
                            <div class="card-body">
                                <h4>UPI: apexmart@icici</h4>
                            </div>
                            </div>
                    <div class="card">
                            <div class="card-body">
                                <a href="Dmr.aspx">
                                    <span class="btn btn-success">Withdraw <asp:Label ID="lblebalance" runat="server"></asp:Label></span>
                            </div>
                            </div>--%>
                    </div>
                    
                </div>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                <asp:PostBackTrigger ControlID="btnSubmit" />
                <asp:AsyncPostBackTrigger ControlID="ddlPaymentMode" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>



</asp:Content>
