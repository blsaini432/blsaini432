<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoanForm_Report.ascx.cs" Inherits="Root_UC_LoanForm_Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div class="content-wrapper">
    <div class="page-header">
        <h3 class="page-title">Loan Form Report
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
                                    <label class="col-sm-3 col-form-label">FromDate</label>
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
                                    <label class="col-sm-3 col-form-label">ToDate</label>
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
                                        <asp:Button ID="btnSearch" runat="server" Text="Search &gt;&gt;" OnClick="btnSearch_Click" class="btn btn-primary" />
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
                                                       <asp:ImageButton ID="btnexportExcel" runat="server" text ="Export"  value="Export"
                                                                class="btn btn-dribbble"  OnClick="btnexportExcel_Click" />
                                                </td>
                                                <%--<td>
                                                            <asp:ImageButton ID="btnexportWord" runat="server" ImageUrl="../images/icon/word_32X32.png"
                                                                CssClass="class24" OnClick="btnexportWord_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="btnexportPdf" runat="server" ImageUrl="../images/icon/pdf_32X32.png"
                                                                CssClass="class24" OnClick="btnexportPdf_Click" />
                                                        </td>--%>
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
                                <div id="loader" style='display: none;'>
                                    <img src='../../Design/images/pageloader.gif' width='32px' height='32px'>
                                </div>
                                <table class="table table-bordered table-hover">
                                    <tr>
                                        <td valign="top" colspan="2">
                                            <div class="box-inner">
                                                <div class="box-content">
                                                    <asp:GridView ID="gvBookedBusList" runat="server" CssClass="table table-striped table-bordered bootstrap-datatable datatable responsive SmallText"
                                                        AutoGenerateColumns="false" AllowPaging="false" OnPageIndexChanging="gvBookedBusList_PageIndexChanging"
                                                        PageSize="10" Width="100%" OnRowCommand="gvBookedBusList_RowCommand" OnSorting="gvBookedBusList_Sorting"
                                                        AllowSorting="false" ShowHeaderWhenEmpty="true" OnRowCreated="gvDispute_RowCreated">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Name" DataField="fullName" SortExpression="fullName" />
                                                            <asp:BoundField HeaderText="loan type" DataField="gender" SortExpression="gender" />
                                                            <asp:BoundField HeaderText="Request By" DataField="MemberName" SortExpression="MemberName" />
                                                            <asp:BoundField HeaderText="Trans_No" DataField="Acknowledgement_No" SortExpression="Acknowledgement_No" />
                                                            <asp:BoundField HeaderText="Status" DataField="RequestStatus" SortExpression="RequestStatus" />

                                                            <asp:BoundField HeaderText="Admin Remarks" DataField="Remarks" SortExpression="Remarks" />

                                                        </Columns>
                                                    </asp:GridView>
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

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
