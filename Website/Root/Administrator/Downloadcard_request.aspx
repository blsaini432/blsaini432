<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true"
    CodeFile="Downloadcard_request.aspx.cs" Inherits="Root_Administrator_Downloadcard_request" %>

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
                    <h4 style="text-align: center; color: green; text-decoration: underline">Update </h4>
                    <table width="100%">
                        <asp:HiddenField ID="hdnid" runat="server" Visible="false" />
                        <%--<tr runat="server" id="ressf" visible="false">
                            <td>Refrance No
                            </td>
                            <td>
                                <asp:TextBox ID="txt_refno" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqrefno" runat="server" Enabled="false" ErrorMessage="Please Enter Refrance No !"
                                    ControlToValidate="txt_refno" Display="Dynamic" SetFocusOnError="True" ValidationGroup="1v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                            </td>
                        </tr>--%>

                        <tr>
                            <td>Upload Download Card 
                            </td>
                            <td>
                                <asp:FileUpload ID="fup_Photo" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvPanImage" runat="server" ControlToValidate="fup_Photo"
                                    Display="Dynamic" ErrorMessage="Please Select Photo " ForeColor="Red" SetFocusOnError="True"
                                    ValidationGroup="v">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnFail" runat="server" Text ="Upload" Visible="true" OnClick="btnFail_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
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
                    <h3 class="page-title">Download Card Request
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
                                                                <asp:BoundField HeaderText="Msrno" DataField="Msrno" SortExpression="ITRType" />
                                                                <asp:BoundField HeaderText="Name" DataField="NAME" SortExpression="NameOnPan" />
                                                                <asp:BoundField HeaderText="HHID_NO" DataField="HHID_NO" SortExpression="HHID_NO" />
                                                                <asp:TemplateField HeaderText="Add Date" SortExpression="DATE">
                                                                    <ItemTemplate>
                                                                        <%#String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(Eval("DATE")))%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-Width="30px">
                                                                    <ItemTemplate>


                                                                        <asp:Button ID="Approve" runat="server" Text="Upload Cards" CommandArgument='<%#Eval("id") %>'
                                                                            CommandName="Approve" ToolTip="Approve Request" />

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

