<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true"
    CodeFile="Ezulix_Recharge_ManageAPI.aspx.cs" Inherits="Recharge_ManageAPI" ValidateRequest="false" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .sss td:first-child
        {
            min-width: 30% !important;
        }
        .sss td:nth-child(2)
        {
            min-width: 2% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
                <h1>
                    <asp:Label ID="lblAddEdit" runat="server"></asp:Label>
                    <small>Admin Panel</small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>API Manage</a></li>
                    <li class="active">API Settings</li>
                </ol>
            </section>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel">
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
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <section class="content mydash">
             <table class="table table-bordered table-hover sss" style="width: 38% !important; float: left">
                    <tr>
                        <td colspan="3" class="aleft">
                            <strong class="star">Note : Fields with <span class="red">*</span> are mandatory fields.</strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1">
                            <span class="red">*</span> API Name
                        </td>
                        <td class="td2">
                            :
                        </td>
                        <td class="td3">
                            <asp:TextBox ID="txtAPIName" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAPIName" runat="server" ControlToValidate="txtAPIName"
                                Display="Dynamic" ErrorMessage="Please Enter API Name !" SetFocusOnError="True"
                                ValidationGroup="v"><img src="../images/warning.png" /></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="red">*</span>Recharge URL
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtURL" runat="server" MaxLength="250" CssClass="form-control"></asp:TextBox>
                            <%--<asp:RegularExpressionValidator ID="revURL" runat="server" ErrorMessage="URL is not valid !"
                                ControlToValidate="txtURL" Display="Dynamic" SetFocusOnError="True" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"
                                ValidationGroup="v"><img src="../images/warning.png" /></asp:RegularExpressionValidator>--%>
                            <asp:RequiredFieldValidator ID="rfvURL" runat="server" ErrorMessage="Please Enter URL !"
                                ControlToValidate="txtURL" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"><img src="../images/warning.png" /></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="red">*</span>Splitter
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtSplitter" runat="server" MaxLength="1" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSplitter" runat="server" ErrorMessage="Please Enter Splitter !"
                                ControlToValidate="txtSplitter" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"><img src="../images/warning.png" /></asp:RequiredFieldValidator>
                            (Ex. : "," or ".")
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <h3>
                                Recharge API Parameters</h3>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--<span class="red">*</span>--%>
                            <asp:TextBox ID="txtprm1" runat="server" MaxLength="50" ValidationGroup="v" Text="uid" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvprm1" runat="server" ErrorMessage="Please Enter Parameter-1 !"
                                ControlToValidate="txtprm1" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"><img src="../images/warning.png" /></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtprm1val" runat="server" MaxLength="50" ValidationGroup="v" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvprm1val" runat="server" ErrorMessage="Please Enter Parameter-1 Value !"
                                ControlToValidate="txtprm1val" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"><img src="../images/warning.png" /></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtprm2" runat="server" MaxLength="50" ValidationGroup="v" Text="pin" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtprm2val" runat="server" MaxLength="50" ValidationGroup="v" CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="red">*</span>Parameter 3
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtprm3" runat="server" MaxLength="50" ValidationGroup="v" Text="number"
                                 CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvprm3" runat="server" ErrorMessage="Please Enter Parameter-3 !"
                                ControlToValidate="txtprm3" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"><img src="../images/warning.png" /></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="red">*</span>Parameter 4
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtprm4" runat="server" MaxLength="50" ValidationGroup="v" Text="operator"
                                CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvprm4" runat="server" ErrorMessage="Please Enter Parameter-4 !"
                                ControlToValidate="txtprm4" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"><img src="../images/warning.png" /></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Parameter 5
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtprm5" runat="server" MaxLength="50" ValidationGroup="v" Text="circle"
                                CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Parameter 6
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtprm6" runat="server" MaxLength="50" ValidationGroup="v" Text="amount"
                                CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Parameter 7
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtprm7" runat="server" MaxLength="50" ValidationGroup="v" Text="account"
                                CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Parameter 8
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtprm8" runat="server" MaxLength="50" ValidationGroup="v" Text="usertx"
                                CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtprm9" runat="server" MaxLength="50" ValidationGroup="v" Text="format" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtprm9val" runat="server" MaxLength="50" ValidationGroup="v" CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtprm10" runat="server" MaxLength="50" ValidationGroup="v" Text="version" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtprm10val" runat="server" MaxLength="50" ValidationGroup="v" CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            TxID Position
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtTxIDPosition" runat="server" MaxLength="2" CssClass="form-control"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtTxIDPosition_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txtTxIDPosition">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Status Position
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtStatusPosition" runat="server" MaxLength="2"  CssClass="form-control"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtStatusPosition_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txtStatusPosition">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="red">*</span>Status msg for Success
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtSuccess" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSuccess" runat="server" ErrorMessage="Please Enter Status msg for Success !"
                                ControlToValidate="txtSuccess" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"><img src="../images/warning.png" /></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="red">*</span>Status msg for Failed
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtFailed" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFailed" runat="server" ErrorMessage="Please Enter Status msg for Failed !"
                                ControlToValidate="txtFailed" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"><img src="../images/warning.png" /></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="red">*</span>Status msg for Pending
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtPending" runat="server" MaxLength="50"  CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPending" runat="server" ErrorMessage="Please Enter Status msg for Pending !"
                                ControlToValidate="txtPending" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"><img src="../images/warning.png" /></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Operator Ref Position
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtOperatorRefPosition" runat="server" MaxLength="2"  CssClass="form-control"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtOperatorRefPosition_FilteredTextBoxExtender"
                                runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtOperatorRefPosition">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ErrorCode Position
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtErrorCodePosition" runat="server" MaxLength="2"  CssClass="form-control"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtErrorCodePosition_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txtErrorCodePosition">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Balance URL
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtBalanceURL" runat="server" MaxLength="250" CssClass="form-control"></asp:TextBox>
                            <%--<asp:RegularExpressionValidator ID="revBalanceURL" runat="server" ErrorMessage="Balance URL is not valid !"
                                ControlToValidate="txtBalanceURL" Display="Dynamic" SetFocusOnError="True" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"
                                ValidationGroup="v"><img src="../images/warning.png" /></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <h3>
                                Balance API Parameters</h3>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtB_prm1" runat="server" MaxLength="50" Text="uid" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtB_prm1val" runat="server" MaxLength="50"  CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtB_prm2" runat="server" MaxLength="50" Text="pin" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtB_prm2val" runat="server" MaxLength="50"  CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtB_prm3" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtB_prm3val" runat="server" MaxLength="50"  CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtB_prm4" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtB_prm4val" runat="server" MaxLength="50"  CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Balance Position
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtB_BalancePosition" runat="server" MaxLength="2"  CssClass="form-control"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtB_BalancePosition_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txtB_BalancePosition">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Status URL
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtStatusURL" runat="server" MaxLength="250"  CssClass="form-control"></asp:TextBox>
                            <%--<asp:RegularExpressionValidator ID="revStatusURL" runat="server" ErrorMessage="Status URL is not valid !"
                                ControlToValidate="txtStatusURL" Display="Dynamic" SetFocusOnError="True" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"
                                ValidationGroup="v"><img src="../images/warning.png" /></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <h3>
                                Status API Parameters</h3>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtS_prm1" runat="server" MaxLength="50" Text="uid" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtS_prm1val" runat="server" MaxLength="50"  CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtS_prm2" runat="server" MaxLength="50" Text="pin" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtS_prm2val" runat="server" MaxLength="50"  CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Parameter 3
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtS_prm3" runat="server" MaxLength="50" Text="txid"  CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Parameter 4
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtS_prm4" runat="server" MaxLength="50"  CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Status Position
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtS_StatusPosition" runat="server" MaxLength="2"  CssClass="form-control"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtS_StatusPosition_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txtS_StatusPosition">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" OnClick="btnSubmit_Click" class="btn btn-primary" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" class="btn btn-primary" />
                            <asp:ValidationSummary ID="ValidationSummary" runat="server" ClientIDMode="Static"
                                ValidationGroup="v" />
                            <br />
                        </td>
                    </tr>
                </table>
                <table class="table table-bordered table-hover " style="width: 40% !important; float: left;">
                    <tr>
                        <td>
                            <div style="width: 99%; height: 1379px; overflow: auto">
                                <asp:GridView ID="gvOperator" runat="server" CssClass="table table-bordered table-hover tablesorter" AutoGenerateColumns="false"
                                    Width="100%" ShowHeaderWhenEmpty="true" Font-Size="10px">
                                    <Columns>
                                        <asp:BoundField DataField="OperatorID" />
                                        <asp:BoundField HeaderText="Service Type" DataField="ServiceTypeName" />
                                        <asp:BoundField HeaderText="Operator Name" DataField="OperatorName" />
                                        <asp:TemplateField HeaderText="Operator Code">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtOperatorCode" runat="server"  CssClass="form-control" Text='<%# Eval("OPCode") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Commission">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCommission" runat="server"  CssClass="form-control" MaxLength="5" Text='<%# String.IsNullOrEmpty(Convert.ToString(Eval("Commission"))) ? "0" : Eval("Commission") %>'></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="txtCommission_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtCommission">
                                                </cc1:FilteredTextBoxExtender>
                                                <asp:CheckBox ID="chkCommissionIsFlat" runat="server" Text="Is Flat" Style="float: left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Surcharge">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSurcharge" runat="server"  CssClass="form-control" MaxLength="5" Text='<%# String.IsNullOrEmpty(Convert.ToString(Eval("Surcharge"))) ? "0" : Eval("Surcharge") %>'></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="txtSurcharge_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtSurcharge">
                                                </cc1:FilteredTextBoxExtender>
                                                <asp:CheckBox ID="chkSurchargeIsFlat" runat="server" Text="Is Flat" Style="float: left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="RowStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                    <PagerSettings Position="Bottom" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
                <table class="table table-bordered table-hover " style="width: 22% !important; float: left; font-family:Arial; font-size:12px;">
                    <tr>
                        <td>
                            <div style="height: 1379px; overflow: auto">
                                <asp:GridView ID="gvCircle" runat="server" CssClass="table table-bordered table-hover tablesorter" AutoGenerateColumns="false"
                                    Width="100%" ShowHeaderWhenEmpty="true" Font-Size="10px">
                                    <Columns>
                                        <asp:BoundField DataField="CircleID" />
                                        <asp:BoundField HeaderText="Circle Name" DataField="CircleName" SortExpression="CircleName" />
                                        <asp:TemplateField HeaderText="Circle Code">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtOperatorCode" runat="server" CssClass="form-control" Text='<%# Eval("CirCode") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="RowStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                    <PagerSettings Position="Bottom" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </section>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
