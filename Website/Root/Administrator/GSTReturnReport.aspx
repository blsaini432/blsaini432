<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/adminmaster.master" AutoEventWireup="true"
    CodeFile="GSTReturnReport.aspx.cs" Inherits="Root_Administrator_GSTReturnReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/script_pop.js" type="text/javascript"></script>
    <link href="../../css/style2.css" rel="stylesheet" type="text/css" />
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="toPopup">
        <div class="close">
        </div>
        <div id="popup_content" style="height: 250px !important;">
            <asp:UpdatePanel ID="upd" runat="server">
                <ContentTemplate>
                    <h4 style="text-align: center;color:green; text-decoration: underline">Update GST Request</h4>
                    <table width="100%">
                        <asp:HiddenField ID="hdnid" runat="server" Visible="false" />
                        <tr runat="server" id="ressf" visible="false">
                            <td>Refrance No
                            </td>
                            <td>
                                <asp:TextBox ID="txt_refno" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqrefno" runat="server" Enabled="false" ErrorMessage="Please Enter Refrance No !"
                                    ControlToValidate="txt_refno" Display="Dynamic" SetFocusOnError="True" ValidationGroup="1v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat="server" id="recp" visible="false">
                            <td>Add Receipt File
                            </td>
                            <td>
                                <asp:FileUpload ID="FileUploadadressImage" runat="server" />
                                <asp:RequiredFieldValidator ID="reqrecipt" runat="server" ControlToValidate="FileUploadadressImage"
                                    Display="Dynamic" ErrorMessage="Please Select Address Proof " ForeColor="Red"
                                    Enabled="false" SetFocusOnError="True" ValidationGroup="1v">*</asp:RequiredFieldValidator>
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
                                <asp:Button ID="btnFail" runat="server" Text="Fail" ValidationGroup="1v" OnClick="btnFail_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSuccess" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <!--your content end-->
    </div>
    <div class="loader">
    </div>
    <div id="backgroundPopup">
    </div>
    <div class="content-header">
        <h1>
            <small>List GST Return Request</small>
        </h1>
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
            <div class="box-content">
                <div class="content mydash">
                    <table class="table table-bordered table-hover ">
                        <tr>
                            <td>From Date
                            </td>
                            <td>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="dd-MMM-yyyy" PopupButtonID="txtfromdate"
                                    TargetControlID="txtfromdate">
                                </cc1:CalendarExtender>
                            </td>
                            <td>To Date
                            </td>
                            <td>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="dd-MMM-yyyy" Animated="False"
                                    PopupButtonID="txttodate" TargetControlID="txttodate">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="position: relative; left: 25%">
                                <asp:Button ID="btnSearch" runat="server" Text="Search &gt;&gt;" OnClick="btnSearch_Click"
                                    class="btn btn-primary" />
                            </td>
                        </tr>
                    </table>
                    <table class="aleft" style="left: 76%; margin-top: -52px; position: absolute">
                        <tr>
                            <td>Record(s) :<asp:Literal ID="litrecordcount" runat="server" Text="0" />
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
                    <table class="table table-bordered table-hover" style="width: 100%; display: block; overflow-x: scroll">

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
                                     
                                        <asp:BoundField HeaderText="Name" DataField="NameOnPan" SortExpression="NameOnPan" />
                                        <asp:BoundField HeaderText="Father Name" DataField="FatherName" SortExpression="FatherName" />
                                        <asp:BoundField HeaderText="DOB" DataField="DOB" SortExpression="DOB" />

                                        <asp:BoundField HeaderText="Contact No" DataField="Mobieno" SortExpression="Mobieno" />
                                        <asp:BoundField HeaderText="Email" DataField="Emailid" SortExpression="Emailid" />

                                        <asp:TemplateField HeaderText="GSTCopy">
                                            <ItemTemplate>
                                                <a href="../Upload/PanCardRequest/Medium/<%# Eval(" copygst") %>" target="_blank">Download
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SalePurchase">
                                            <ItemTemplate>
                                                <a href="../Upload/PanCardRequest/Medium/<%# Eval("salepurchase") %>" target="_blank">Download
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Client Data">
                                            <ItemTemplate>
                                                <asp:Button ID="btnWord" runat="server" Text="Download" CommandArgument='<%#Eval("GSTreturnkid") %>'
                                                    CommandName="WordDownload" ToolTip="Download Word File" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Add Date" SortExpression="AddDate">
                                            <ItemTemplate>
                                                <%#String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(Eval("AddDate")))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="30px">
                                            <ItemTemplate>
                                                <asp:Button ID="Approve" runat="server" Text="Approve" CommandArgument='<%#Eval("GSTreturnkid") %>'
                                                    CommandName="Approve" Visible='<%# Convert.ToBoolean(Eval("IsEnable")) %>' ToolTip="Approve Request"
                                                    Enabled='<%# Convert.ToBoolean(Eval("IsEnable")) %>' />
                                                <asp:Label ID="lblssdsd" Visible='<%# Convert.ToBoolean(Eval("StatusSwow")) %>' runat="server"
                                                    Text='<%#Eval("Staussss") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="30px">
                                            <ItemTemplate>
                                                <asp:Button ID="Reject" runat="server" Text="Reject" CommandArgument='<%#Eval("GSTreturnkid") %>'
                                                    CommandName="Reject" Visible='<%# Convert.ToBoolean(Eval("IsEnable")) %>' ToolTip="Approve Request"
                                                    Enabled='<%# Convert.ToBoolean(Eval("IsEnable")) %>' />
                                                <asp:Label ID="lbfsdfewrewl" Visible='<%# Convert.ToBoolean(Eval("StatusSwow")) %>'
                                                    runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>


                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnexportExcel" />
            <asp:PostBackTrigger ControlID="btnexportWord" />
            <asp:PostBackTrigger ControlID="btnexportpdf" />
            <asp:PostBackTrigger ControlID="gvGST" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>--%>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="toPopup">
        <div class="close">
        </div>
        <div id="popup_content" style="height: 250px !important;">
            <asp:UpdatePanel ID="upd" runat="server">
                <ContentTemplate>
                    <h4 style="text-align: center; color: green; text-decoration: underline">Update GST Request</h4>
                    <table width="100%">
                        <asp:HiddenField ID="hdnid" runat="server" Visible="false" />
                        <tr runat="server" id="ressf" visible="false">
                            <td>Refrance No
                            </td>
                            <td>
                                <asp:TextBox ID="txt_refno" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqrefno" runat="server" Enabled="false" ErrorMessage="Please Enter Refrance No !"
                                    ControlToValidate="txt_refno" Display="Dynamic" SetFocusOnError="True" ValidationGroup="1v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <%--<tr runat="server" id="recp" visible="false">
                            <td>Add Receipt File -1
                            </td>
                            <td>
                                <asp:FileUpload ID="FileUploadadressImage" runat="server" />
                                <asp:RequiredFieldValidator ID="reqrecipt" runat="server" ControlToValidate="FileUploadadressImage"
                                    Display="Dynamic" ErrorMessage="Please Select receipt1 " ForeColor="Red"
                                    Enabled="false" SetFocusOnError="True" ValidationGroup="1v">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>--%>
                       <%-- <tr runat="server" id="Tr1" visible="false">
                            <td>Add Receipt File -2
                            </td>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="FileUploadadressImage"
                                    Display="Dynamic" ErrorMessage="Please Select Address receipt2 " ForeColor="Red"
                                    Enabled="false" SetFocusOnError="True" ValidationGroup="1v">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>--%>
                        <%--<tr runat="server" id="Tr2" visible="false">
                            <td>Add Receipt File -3
                            </td>
                            <td>
                                <asp:FileUpload ID="FileUpload2" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="FileUpload2"
                                    Display="Dynamic" ErrorMessage="Please Select Address receipt3 " ForeColor="Red"
                                    Enabled="false" SetFocusOnError="True" ValidationGroup="1v">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>--%>
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
                    <h3 class="page-title">List GST Return Request
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
                                                                   <asp:ImageButton ID="btnexportExcel" runat="server" text ="Export"  value="Export"
                                                                class="btn btn-dribbble"  OnClick="btnexportExcel_Click" />
                                                               
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

                                                                <asp:BoundField HeaderText="Name" DataField="NameOnPan" SortExpression="NameOnPan" />
                                                                <asp:BoundField HeaderText="Father Name" DataField="FatherName" SortExpression="FatherName" />
                                                                <asp:BoundField HeaderText="DOB" DataField="DOB" SortExpression="DOB" />

                                                                <asp:BoundField HeaderText="Contact No" DataField="Mobieno" SortExpression="Mobieno" />
                                                                <asp:BoundField HeaderText="Email" DataField="Emailid" SortExpression="Emailid" />

                                                                <asp:TemplateField HeaderText="GSTCopy">
                                                                    <ItemTemplate>
                                                                        <a href="../../Uploads/Servicesimage/Actual/<%# Eval(" copygst") %>" target="_blank">Download
                                                                        </a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SalePurchase">
                                                                    <ItemTemplate>
                                                                        <a href="../../Uploads/Servicesimage/Actual/<%# Eval("salepurchase") %>" target="_blank">Download
                                                                        </a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                

                                                              <%--  <asp:TemplateField HeaderText="Client Data">
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="btnWord" runat="server" Text="Download" CommandArgument='<%#Eval("GSTreturnkid") %>'
                                                                            CommandName="WordDownload" ToolTip="Download Word File" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>

                                                                <asp:TemplateField HeaderText="Add Date" SortExpression="AddDate">
                                                                    <ItemTemplate>
                                                                        <%#String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(Eval("AddDate")))%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-Width="30px">
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="Approve" runat="server" Text="Approve" CommandArgument='<%#Eval("GSTreturnkid") %>'
                                                                            CommandName="Approve" Visible='<%# Convert.ToBoolean(Eval("IsEnable")) %>' ToolTip="Approve Request"
                                                                            Enabled='<%# Convert.ToBoolean(Eval("IsEnable")) %>' />
                                                                        <asp:Label ID="lblssdsd" Visible='<%# Convert.ToBoolean(Eval("StatusSwow")) %>' runat="server"
                                                                            Text='<%#Eval("Staussss") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-Width="30px">
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="Reject" runat="server" Text="Reject" CommandArgument='<%#Eval("GSTreturnkid") %>'
                                                                            CommandName="Reject" Visible='<%# Convert.ToBoolean(Eval("IsEnable")) %>' ToolTip="Approve Request"
                                                                            Enabled='<%# Convert.ToBoolean(Eval("IsEnable")) %>' />
                                                                        <asp:Label ID="lbfsdfewrewl" Visible='<%# Convert.ToBoolean(Eval("StatusSwow")) %>'
                                                                            runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                                                                    </ItemTemplate>
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
           <%-- <asp:PostBackTrigger ControlID="btnexportWord" />
            <asp:PostBackTrigger ControlID="btnexportpdf" />
            <asp:PostBackTrigger ControlID="gvGST" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
