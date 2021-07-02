<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true"
    CodeFile="ManageFundRequest_old1.aspx.cs" Inherits="Root_Admin_ManageFundRequest" %>
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
            <h3 class="page-title">Paytm Payment Gateway
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
                                        <label class="col-form-label">Amount<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_Amounts" runat="server" autocomplete="off"
                                            MaxLength="6" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_Amount" runat="server" ControlToValidate="txt_Amounts"
                                            ErrorMessage="Enter Valid Amount" ValidationGroup="vgBeni"></asp:RequiredFieldValidator>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Amount" runat="server" TargetControlID="txt_Amounts"
                                            ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Button ID="btnSubmit" type="button" runat="server" Text="Add Fund By Payment Gateway"
                                            ValidationGroup="vgBeni" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />                                     
                                    </div>
                                </div>
                                <div class="form-group row">
                                   <h4 style="color:red"> Payment Gateway Charge details </h4>
                                </div>
                                <div class="form-group row" style="color:red">
                                   UPI = 0
                                    <br />
                                    Net banking =1.95% + GST
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
                     
                    </div>

                </div>

            </ContentTemplate>
            <Triggers>
               
                <asp:PostBackTrigger ControlID="btnSubmit" />
             
            </Triggers>
        </asp:UpdatePanel>
    </div>



</asp:Content>
