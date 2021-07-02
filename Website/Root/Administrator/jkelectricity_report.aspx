<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true"
    CodeFile="jkelectricity_report.aspx.cs" Inherits="Root_Administrator_jkelectricity_report" %>

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
                    <h4 style="text-align: center; color: green; text-decoration: underline">Update JKelectricity Bill</h4>
                    <table width="100%">
                        <asp:HiddenField ID="hdnid" runat="server" Visible="false" />


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
                                <asp:Button ID="btnFail" runat="server" Text="Fail" ValidationGroup="1v" OnClick="btnFail_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSuccess" />
                    <asp:PostBackTrigger ControlID="btnFail" />
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
                    <h3 class="page-title">J&KPDD Electricity Request
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
                                                                <asp:ImageButton ID="btnexportExcel" runat="server" text="Export" value="Export"
                                                                    class="btn btn-dribbble" OnClick="btnexportExcel_Click" />
                                                            </td>

                                                        </tr>
                                                    </table>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="info-box">

                                        <div class="table-responsive">
                                            <table class="table table-bordered table-hover">

                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="gvGST" runat="server" CssClass="table table-striped table-bordered bootstrap-datatable datatable responsive SmallText"
                                                            AutoGenerateColumns="false" AllowPaging="false" OnPageIndexChanging="gvGST_PageIndexChanging"
                                                            PageSize="10" Width="100%" OnRowCommand="gvGST_RowCommand" OnSorting="gvGST_Sorting"
                                                            AllowSorting="false" ShowHeaderWhenEmpty="true" OnRowCreated="gvGST_RowCreated">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sr. No.">
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:BoundField HeaderText="memberID" DataField="memberID" SortExpression="memberID" />
                                                                <asp:BoundField HeaderText="MsrNo" DataField="RequestByMsrNo" SortExpression="MsrNo" />
                                                                <asp:BoundField HeaderText="Name" DataField="Name" SortExpression="Name" />
                                                                <asp:BoundField HeaderText="ConsumerNo" DataField="Consumer" SortExpression="Consumer" />
                                                                <asp:TemplateField HeaderText="Add Date" SortExpression="AddDate">
                                                                      </asp:TemplateField>

                                                                <asp:BoundField HeaderText="Amount " DataField="Amount" SortExpression="Amount" />
                                                                <asp:BoundField HeaderText="txnno" DataField="txnno" SortExpression="txnno" />
                                                                <asp:BoundField HeaderText="RequestStatus" DataField="RequestStatus" SortExpression="txnno" />
                                                                <asp:TemplateField HeaderStyle-Width="30px">
                                                                    <ItemTemplate>           <ItemTemplate>
                                                                        <%#String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(Eval("AddDate")))%>
                                                                    </ItemTemplate>
                                                   
                                                                       
                                                                        <%--<asp:Button ID="Approve" runat="server" Text="Approve" CommandArgument='<%#Eval("id") %>'
                                                                            CommandName="Approve" ToolTip="Approve Request" />--%>
                                                                        <asp:Button ID="Approve" runat="server" Text="Approve" CommandArgument='<%#Eval("id") %>'
                                                                            CommandName="Approve" Visible='<%# Convert.ToBoolean(Eval("IsEnable")) %>' ToolTip="Approve Request"
                                                                            Enabled='<%# Convert.ToBoolean(Eval("IsEnable")) %>' />
                                                                      <%--  <asp:Label ID="lbfsdfewrewl"
                                                                            runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                              
                                                                <asp:TemplateField HeaderStyle-Width="30px">
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="Reject" runat="server" Text="Reject" CommandArgument='<%#Eval("id") %>'
                                                                            CommandName="Reject" Visible='<%# Convert.ToBoolean(Eval("IsEnable")) %>' ToolTip="Approve Request"
                                                                            Enabled='<%# Convert.ToBoolean(Eval("IsEnable")) %>' />
                                                                        <asp:Label ID="lbfsdfewrewl"
                                                                            runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                   <%-- <asp:Button ID="Reject" runat="server" Text="Reject" CommandArgument='<%#Eval("id") %>'
                                                                            CommandName="Reject" ToolTip="Approve Request" />--%>
                                                                      
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>


                                                    </td>
                                                </tr>
                                            </table>
                                            <div class="dataTables_scrollFoot" style="overflow: hidden; border: 0px; width: 100%;">
                                                <div class="dataTables_scrollFootInner" style="width: 1224px; padding-right: 17px;">
                                                    <table class="display dataTable" role="grid" style="margin-left: 0px; width: 1224px;"></table>
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnexportExcel" />

        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

