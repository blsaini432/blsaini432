<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true"
    CodeFile="Accountipen_Request.aspx.cs" Inherits="Portal_Admin_Accountipen_Request" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/script_pop.js" type="text/javascript"></script>
    <link href="../../css/style2.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="toPopup">
        <div class="close">
        </div>
        <div id="popup_content" style="height: 250px !important;">
            <asp:UpdatePanel ID="upd" runat="server">
                <ContentTemplate>
                    <h4 style="text-align: center; color: green; text-decoration: underline">Update Request</h4>
                    <table width="100%">
                        <tr>
                            <td>Member Info
                            </td>
                            <td>
                                <asp:Literal ID="litMember" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>Details
                            </td>
                            <td>
                                <asp:Literal ID="LitTransaction" runat="server"></asp:Literal>
                                <asp:Label ID="litOpname" runat="server"></asp:Label>
                                <asp:HiddenField ID="hdnid" runat="server" />
                                <asp:HiddenField ID="hdn_memberID" runat="server" />
                                <asp:HiddenField ID="hdn_amount" runat="server" />
                                <asp:HiddenField ID="hdn_txnid" runat="server" />
                            </td>
                        </tr>
                        <tr runat="server" id="ressf" visible="false">
                            <td>Refrance No
                            </td>
                            <td>
                                <asp:TextBox ID="txt_refno" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqrefno" runat="server" Enabled="false" ErrorMessage="Please Enter Refrance No !"
                                    ControlToValidate="txt_refno" Display="Dynamic" SetFocusOnError="True" ValidationGroup="1v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td>Admin Remark
                            </td>
                            <td>
                                <asp:TextBox ID="txtadminRemark" runat="server" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Admin Remarks!"
                                    ControlToValidate="txtadminRemark" Display="Dynamic" SetFocusOnError="True" ValidationGroup="1v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSuccess" runat="server" Visible="false" Text="Success" ValidationGroup="1v"
                                    OnClick="btnSubmit_Click" />

                                <asp:Button ID="btnFail" runat="server" Text="Fail" ValidationGroup="1v" OnClick="btnFail_Click" OnClientClick="this.disabled='true';" UseSubmitBehavior="false" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSuccess" />
                </Triggers>
            </asp:UpdatePanel>
        </div>

    </div>
    <div class="loader">
    </div>
    <div id="backgroundPopup">
    </div>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="content-wrapper">
                <div class="page-header">
                    <h3 class="page-title">Account Open Request
                    </h3>
                </div>
                <div class="row grid-margin">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body">
                                <div class="container">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group row">
                                                <label class="col-sm-3 col-form-label">FromDate<code>*</code></label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="dd-MMM-yyyy" PopupButtonID="txtfromdate"
                                                        TargetControlID="txtfromdate">
                                                    </cc1:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group row">
                                                <label class="col-sm-3 col-form-label">ToDate<code>*</code></label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txttodate" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="dd-MMM-yyyy" Animated="False"
                                                        PopupButtonID="txttodate" TargetControlID="txttodate">
                                                    </cc1:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group row">
                                                <div class="col-sm-6">
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search &gt;&gt;" OnClick="btnSearch_Click"
                                                        class="btn btn-primary" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group row">
                                                <div class="col-sm-6">
                                                    <table class="aleft">
                                                        <tr>
                                                            <td>Record(s) :<asp:Literal ID="litrecordcount" runat="server" Text="0" />
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="btnexportExcel" runat="server" ImageUrl="../images/icon/excel_32X32.png"
                                                                    CssClass="class24" OnClick="btnexportExcel_Click" />
                                                            </td>

                                                        </tr>
                                                    </table>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="info-box">
                                        <%--<div class="form-group">
                                    <label>Search</label>
                                    <input type="text" ng-model="search" class="form-control" placeholder="Search">
                                </div>
                                <div>
                                    <label for="search">items per page:</label>
                                    <input type="number" min="1" max="100" class="form-control" ng-model="pageSize">
                                </div>--%>
                                        <div class="table-responsive">
                                            <table class="table table-bordered table-hover">
                                                <tr>
                                                    <td valign="top" colspan="2">
                                                        <div style="font-size: 12px; margin: 0px; padding: 0;">
                                                        </div>
                                                        <div class="box-inner">
                                                            <div class="box-content">
                                                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Width="100%">
                                                                    <asp:GridView ID="gvBookedBusList" runat="server" CssClass="table table-striped table-bordered bootstrap-datatable datatable responsive SmallText table-responsive"
                                                                        AutoGenerateColumns="false" AllowPaging="false" OnPageIndexChanging="gvBookedBusList_PageIndexChanging"
                                                                        PageSize="10" Width="100%" OnRowCommand="gvBookedBusList_RowCommand" OnSorting="gvBookedBusList_Sorting"
                                                                        AllowSorting="false" ShowHeaderWhenEmpty="true" OnRowCreated="gvDispute_RowCreated">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Bank" DataField="Bank" SortExpression="fullName" />
                                                                            <asp:BoundField HeaderText="Branch name" DataField="branchname" SortExpression="gender" />
                                                                            <asp:BoundField HeaderText="Customee type " DataField="Customeetype" SortExpression="MemberName" />
                                                                            <asp:BoundField HeaderText="Name" DataField="lname" SortExpression="Acknowledgement_No" />
                                                                            <asp:BoundField HeaderText="mobile" DataField="mobile" SortExpression="RequestStatus" />
                                                                            <asp:BoundField HeaderText="Email" DataField="email" SortExpression="Remarks" />
                                                                            <asp:BoundField HeaderText="Pan" DataField="pan" SortExpression="Remarks" />
                                                                            <asp:BoundField HeaderText="Aadhar" DataField="aadhar" SortExpression="Remarks" />
                                                                            <asp:BoundField HeaderText="RequestStatus" DataField="RequestStatus" SortExpression="Remarks" />
                                                                            <asp:TemplateField HeaderText="Approve.">
                                                                                <ItemTemplate>
                                                                                    <asp:Button ID="Approve" runat="server" Text="Approve" CommandArgument='<%#Eval("id") %>'
                                                                                        CommandName="Approve" Visible='<%# Convert.ToBoolean(Eval("RequestStatus")) %>' ToolTip="Approve Request"
                                                                                        Enabled='<%# Convert.ToBoolean(Eval("RequestStatus")) %>' />

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Reject.">
                                                                                <ItemTemplate>
                                                                                    <asp:Button ID="Reject" runat="server" Text="Reject" CommandArgument='<%#Eval("id") %>'
                                                                                        CommandName="Reject" Visible='<%# Convert.ToBoolean(Eval("RequestStatus")) %>' ToolTip="Approve Request"
                                                                                        Enabled='<%# Convert.ToBoolean(Eval("RequestStatus")) %>' />

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div class="dataTables_scrollFoot" style="overflow: hidden; border: 0px; width: 100%;">
                                                <div class="dataTables_scrollFootInner" style="width: 1224px; padding-right: 17px;">
                                                    <table class="display dataTable" role="grid" style="margin-left: 0px; width: 1224px;"></table>
                                                </div>
                                            </div>
                                            <%-- <div class="text-center">
                                        <dir-pagination-controls boundary-links="true" on-page-change="pageChangeHandler(newPageNumber)" template-url="../Angularjsapp/dirPagination.tpl.html"></dir-pagination-controls>
                                    </div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnexportExcel" />
            <%--  <asp:PostBackTrigger ControlID="btnexportWord" />
            <asp:PostBackTrigger ControlID="btnexportpdf" />
            <asp:PostBackTrigger ControlID="gvGST" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
