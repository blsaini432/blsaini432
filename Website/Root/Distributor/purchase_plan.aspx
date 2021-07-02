<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="purchase_plan.aspx.cs" Inherits="root_Distributor_purchase_plan" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">
              Purchase Plan
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
                                <label class="col-form-label"> Request For Member Type<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                        <asp:DropDownList ID="ddlmymembertype" runat="server" AutoPostBack="true" CssClass="form-control" 
                                onselectedindexchanged="ddlMemberType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlmymembertype"
                                    Display="Dynamic" ErrorMessage="Please Select member type !" SetFocusOnError="True"
                                    ValidationGroup="v0" InitialValue="0">*</asp:RequiredFieldValidator>

                            </div>
                        </div>
                              <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Per Id Fees<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                       <asp:TextBox ID="DSO1_totadmin" runat="server" ClientIDMode="Static" CssClass="form-control" 
                                Enabled="False" Text="0"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="DSO1_totadmin_FilteredTextBoxExtender" 
                                runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="DSO1_totadmin">
                            </cc1:FilteredTextBoxExtender>

                            </div>
                        </div>
                              <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label"> No. Of ID to Purchase<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                        <asp:TextBox ID="DSO1_totid" runat="server" ClientIDMode="Static" Text="0" CssClass="form-control" 
                                AutoPostBack="True" ontextchanged="DSO1_totid_TextChanged"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="DSO1_totid_FilteredTextBoxExtender" 
                                runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="DSO1_totid">
                            </cc1:FilteredTextBoxExtender>

                            </div>
                        </div>
                            <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label"> Total Amount Deduct From Wallet<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
 <asp:TextBox ID="DSO1_totalamount" runat="server" ClientIDMode="Static" CssClass="form-control" 
                                Text="0" Enabled="False"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="DSO1_totalamount_FilteredTextBoxExtender" 
                                runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="DSO1_totalamount">
                            </cc1:FilteredTextBoxExtender>

                            </div>
                        </div>
                               <div class="form-group row">
                            <div class="col-lg-3">
                            
                            </div>
                            <div class="col-lg-8">
 
  <asp:Button ID="btnSuccess" runat="server" class="btn btn-primary" 
                                OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="v0" 
                                CssClass="btn btn-primary" />
                            </div>
                        </div>

                        </div></div></div></div></ContentTemplate></asp:UpdatePanel></div>


                    <%--        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                ClientIDMode="Static" Style="display: none" ValidationGroup="v0" />
                            <asp:GridView ID="gvState" runat="server" AllowPaging="false" 
                                AllowSorting="false" AutoGenerateColumns="false" 
                                CssClass="table table-bordered" DataKeyNames="ReqId" 
                                 PageSize="10" ShowHeaderWhenEmpty="true" 
                                Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr. No.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="MemberId" HeaderText="MemberId" 
                                        SortExpression="MemberId" />
                                    <asp:BoundField DataField="MemberName" HeaderText="MemberName" 
                                        SortExpression="MemberName" />
                                    <asp:BoundField DataField="membertype" HeaderText="ForMember" 
                                        SortExpression="membertype" />
                                    <asp:BoundField DataField="idpurchase" HeaderText="Noofidpurchase" 
                                        SortExpression="idpurchase" />
                                    <asp:BoundField DataField="amount" HeaderText="Price" SortExpression="amount" />
                                    <asp:BoundField DataField="TranId" HeaderText="TransactionId" 
                                        SortExpression="TranId" />
                                    <asp:TemplateField HeaderStyle-Width="16px" HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lbladminstatus" runat="server" 
                                                Text='<%# Convert.ToString(Eval("ActiveStatus")) == "Pending" ? "Pending" : "Active" %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="16px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="RequestDate" HeaderText="RequestDate" 
                                        SortExpression="RequestDate" />
                                    <asp:BoundField DataField="ActiveDate" HeaderText="ActiveDate" 
                                        SortExpression="ActiveDate" />
                                  
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="EmptyDataTemplate">
                                        No Record Found !</div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                            </td>
                    </tr>
                </table>
            </section>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>


