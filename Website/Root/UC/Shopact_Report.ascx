<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Shopact_Report.ascx.cs" Inherits="Root_UC_Shopact_Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div class="content mydash" style="padding-top: 53px;">
    <table class="table table-bordered table-hover ">
        <tr>
            <td>
                From Date
            </td>
            <td>
                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control"></asp:TextBox>
                <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="dd-MMM-yyyy" PopupButtonID="txtfromdate"
                    TargetControlID="txtfromdate">
                </cc1:CalendarExtender>
            </td>
            <td>
                To Date
            </td>
            <td>
                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control"></asp:TextBox>
                <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="dd-MMM-yyyy" Animated="False"
                    PopupButtonID="txttodate" TargetControlID="txttodate">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td style="display:none;">
                By Request Status
            </td>
            <td style="display:none;">
                <asp:DropDownList ID="ddl_status" runat="server" Visible="false">
                    <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                    <asp:ListItem Value="Pending">Pending</asp:ListItem>
                    <asp:ListItem Value="success">success</asp:ListItem>
                    <asp:ListItem Value="failed">failed</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                Acknowledgement No
            </td>
            <td>
                <asp:TextBox ID="txt_orderID" runat="server" CssClass="form-control"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="3">
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Search &gt;&gt;" OnClick="btnSearch_Click"
                    class="btn btn-primary" />
            </td>
        </tr>
    </table>
    <div class="user-section" style="display:none;">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12">
                <div class="card card-hight">
                    <div class="card-action">
                        <h3>
                            Total Applied Pan Card<asp:Label ID="lblTotPan" runat="server" Style="float: right;"></asp:Label></h3>
                    </div>
                    <div class="card-content">
                        <div class="table-responsive">
                            <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                <thead>
                                    <tr>
                                        <th>
                                            Total Successfull
                                        </th>
                                        <th>
                                            Total Fail
                                        </th>
                                        <th>
                                            Total Temp Rejected
                                        </th>
                                        <th>
                                            Total Pending
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr class="odd gradeX">
                                        <td>
                                            <asp:Label ID="lblSuccPan" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFailPan" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTempPan" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPendPan" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <table class="table table-bordered table-hover">
        <tr>
            <td valign="top" colspan="2">
                <div style="font-size: 0px; left: 88%; margin-top: -32px; padding: 0; position: absolute;">
                    <table class="aleft">
                        <tr>
                            <td>
                                Record(s) :<asp:Literal ID="litrecordcount" runat="server" Text="0" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnexportExcel" runat="server" ImageUrl="../images/icon/excel_32X32.png"
                                    CssClass="class24" OnClick="btnexportExcel_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnexportWord" runat="server" ImageUrl="../images/icon/word_32X32.png"
                                    CssClass="class24" OnClick="btnexportWord_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnexportPdf" runat="server" ImageUrl="../images/icon/pdf_32X32.png"
                                    CssClass="class24" OnClick="btnexportPdf_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
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
                                <asp:BoundField HeaderText="Status" DataField="RequestStatus" SortExpression="RequestStatus" />
                                <asp:BoundField HeaderText="Request By" DataField="MemberName" SortExpression="MemberName" />
                                 <asp:BoundField HeaderText="Trans_No" DataField="Acknowledgement_No" SortExpression="Acknowledgement_No" />
                                
                                <asp:BoundField HeaderText="Name" DataField="Name_of_ect" SortExpression="Name_of_ect" />
                                <asp:BoundField HeaderText="Aadhar number" DataField="Aadharnumber" SortExpression="Aadharnumber" />
                                <asp:BoundField HeaderText="Admin Remarks" DataField="Remarks" SortExpression="Remarks" />
                               <asp:TemplateField HeaderText="Receipt">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl" Visible='<%# Convert.ToBoolean(Eval("StatusDTSwow")) %>' runat="server">
                                                            <a href="../Upload/PanCardRequest/Actual/<%# Eval("ReciptImg") %>" target="_blank">
                                                                Download                                                                
                                                            </a>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                
                                 <%-- <asp:TemplateField HeaderText="Request On" SortExpression="AddDate">
                                    <ItemTemplate>
                                        <%#String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(Eval("AddDate")))%>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                              <%--  <asp:TemplateField HeaderText="Temp. Receipt" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl" Visible='<%# Convert.ToBoolean(Eval("IsTempEnable")) %>' runat="server">
                                                            <a href='<%#"../../Temp_Receipt.aspx?ITRkid=" + Eval("ITRkid")%>' target="_blank">
                                                                Download                                                                
                                                            </a>
                                        </asp:Label>
                                    </ItemTemplate>--%>
                              
                               <%-- <asp:TemplateField HeaderText="Receipt1">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl" Visible='<%# Convert.ToBoolean(Eval("StatusDTSwow")) %>' runat="server">
                                                            <a href="../../Portal/Upload/PanCardrecipt/Actual/<%# Eval("ReciptImg1") %>" target="_blank">
                                                                Download                                                                
                                                            </a>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                 <%--<asp:TemplateField HeaderText="Receipt2">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl" Visible='<%# Convert.ToBoolean(Eval("StatusDTSwow")) %>' runat="server">
                                                            <a href="../Upload/PanCardrecipt/Actual/<%# Eval("ReciptImg2") %>" target="_blank">
                                                                Download                                                                
                                                            </a>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                 <%-- <asp:TemplateField HeaderText="Receipt3">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl" Visible='<%# Convert.ToBoolean(Eval("StatusDTSwow")) %>' runat="server">
                                                            <a href="../Upload/PanCardrecipt/Actual/<%# Eval("ReciptImg3") %>" target="_blank">
                                                                Download                                                                
                                                            </a>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="50px" HeaderText="Admin Status">
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
</div>
<div class="loader">
</div>
<div id="backgroundPopup" onclick="disablePopup();">
</div>
