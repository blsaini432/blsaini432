<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GstRegistration_Report.ascx.cs" Inherits="Root_UC_GstRegistration_Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<div class="content-wrapper">
    <div class="page-header">
        <h3 class="page-title">GST Registration Report
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
                                                               <asp:ImageButton ID="ImageButton1" runat="server" text ="Export"  value="Export"
                                                                class="btn btn-dribbble"  OnClick="btnexportExcel_Click" />
                                                        </td>
                                                        
                                                    </tr>
                                                </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="info-box">
                           
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
                                                            <asp:BoundField HeaderText="Name" DataField="NameOnPAN" SortExpression="NameOnPAN" />
                                                           <asp:BoundField HeaderText="GST Type" DataField="GSTType" SortExpression="Amount" />

                                                           <asp:BoundField HeaderText="mobileno" DataField="mobileno" SortExpression="Acknowledgement_No" />
                                                             <asp:BoundField HeaderText="pan Card" DataField="BusinessPanCard" SortExpression="Acknowledgement_No" />
                                                             <asp:BoundField HeaderText="Annual TurnOver" DataField="AnnualTurnOver" SortExpression="Acknowledgement_No" />
                                                            <asp:TemplateField HeaderText="Date" SortExpression="AddDate">
                                                                <ItemTemplate>
                                                                    <%#String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(Eval("AddDate")))%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                         <asp:BoundField HeaderText="Status" DataField="RequestStatus" SortExpression="RequestStatus" />
                                                             <asp:BoundField HeaderText="Admin Remarks" DataField="Remarks" SortExpression="RequestStatus" />
                                                         
                                                         <%--   <asp:TemplateField HeaderStyle-Width="50px" HeaderText="Admin Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl" Visible='<%# Convert.ToBoolean(Eval("StatusSwow")) %>' runat="server"
                                                                        Text='<%#Eval("Staussss") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-Width="30px" HeaderText="Admin Remarks">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl" Visible='<%# Convert.ToBoolean(Eval("StatusSwow")) %>' runat="server"
                                                                        Text='<%#Eval("Remarks") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
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
