<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true" CodeFile="PanRequest_ol.aspx.cs" Inherits="Root_Admin_panreport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .input {
            float: left;
            width: 87%;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">PAN Request
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
                                            <asp:TextBox ID="txt_fdate" runat="server" MaxLength="50" CssClass="form-control" autcomplete="off" ClientIDMode="Static"></asp:TextBox>
                                            <asp:Image ID="Image1" runat="server" ImageUrl="../Upload/calender.png" Height="20px" Width="20px" />
                                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txt_fdate"
                                                Display="Dynamic" ErrorMessage="Please Enter From Date !" SetFocusOnError="True"
                                                ValidationGroup="v"></asp:RequiredFieldValidator>
                                            <cc1:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd-MM-yyyy" PopupButtonID="Image1" TargetControlID="txt_fdate">
                                            </cc1:CalendarExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">ToDate<code>*</code></label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txttdate" runat="server" MaxLength="50" CssClass="form-control" autcomplete="off" ClientIDMode="Static"></asp:TextBox>
                                            <asp:Image ID="imgbt" runat="server" ImageUrl="../Upload/calender.png" Height="20px" Width="20px" />
                                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txttdate"
                                                Display="Dynamic" ErrorMessage="Please Enter To Date !" SetFocusOnError="True"
                                                ValidationGroup="v"></asp:RequiredFieldValidator>
                                            <cc1:CalendarExtender runat="server" ID="CalendarExtender2" Format="dd-MM-yyyy" Animated="False"
                                                PopupButtonID="imgbt" TargetControlID="txttdate">
                                            </cc1:CalendarExtender>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-md-4">
                                    <div class="form-group row">
                                        <label class="col-form-label">TxnID/name/memberid<code>*</code></label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="TxnID" runat="server" MaxLength="30" CssClass="form-control"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group row">
                                        <div class="col-sm-6">
                                            <asp:Button ID="btnsearch" runat="server" OnClick="btnsearch_Click1" Text="Search" CssClass="btn btn-primary" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group row">
                                        <div class="col-sm-6">
                                            <asp:Button ID="btn_export" runat="server" OnClick="btn_export_Click" Text="Export" CssClass="btn btn-dribbble" OnClientClick="getdata()" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                         
                            <div class="info-box">


                                <div class="table-responsive">
                                    <asp:GridView ID="gvBookedBusList" runat="server" CssClass="table table-responsive"
                                        AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="gvBookedBusList_PageIndexChanging"
                                        PageSize="10" Width="100%" OnRowCommand="gvBookedBusList_RowCommand"
                                        AllowSorting="false" ShowHeaderWhenEmpty="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr. No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="MemberID" DataField="MemberID" SortExpression="MemberID" />
                                            <asp:BoundField HeaderText="Status" DataField="Status" SortExpression="Status" />
                                            <asp:BoundField HeaderText="Amount" DataField="Amount" SortExpression="Amount" />
                                            <asp:BoundField HeaderText="TxnID" DataField="Acknowledgement_No" SortExpression="Acknowledgement_No" />
                                            <asp:BoundField HeaderText="Name" DataField="NameOnPAN" SortExpression="NameOnPAN" />
                                            <asp:BoundField HeaderText="PanCardType" DataField="RequestType" SortExpression="RequestType" />
                                            <asp:BoundField HeaderText="FatherName" DataField="FatherName" SortExpression="FatherName" />
                                            <asp:BoundField HeaderText="ContactNo" DataField="ContactNo" SortExpression="ContactNo" />
                                            <asp:BoundField HeaderText="Email" DataField="Email" SortExpression="Email" />
                                            <asp:BoundField HeaderText="AdharNo" DataField="AdharNo" SortExpression="AdharNo" />



                                            <asp:TemplateField HeaderText="Request On" SortExpression="RequestDate">
                                                <ItemTemplate>
                                                    <%#String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(Eval("RequestDate")))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PDF">
                                                <ItemTemplate>
                                                    <a href="../../Uploads/Servicesimage/Actual/<%# Eval("AddProof") %>" target="_blank" class="btn btn-info">Download
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" Final PDF">
                                                <ItemTemplate>
                                                    <asp:Button ID="btndownloadmerge" runat="server" Text='Download' Visible='<%# Convert.ToString(Eval("ReciptImg"))!=""?true:false %>' CommandName="downloadmerge" CommandArgument='<%# Eval("ReciptImg")+ "|" + Eval("AddProof") + "|" + Eval("RefNo") %>' class="btn btn-success" />
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Client Data" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnWord" runat="server" Text="Download" CommandArgument='<%#Eval("Pankid") %>'
                                                        CommandName="WordDownload" ToolTip="Download Word File" CssClass="btn btn-dark" />
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Receipt">
                                                <ItemTemplate>
                                                    <asp:FileUpload ID="fupRcpt" runat="server" Enabled='<%#Eval("Status").ToString() == "Pending" ? true :false %>'></asp:FileUpload>
                                                    <a href="../../Uploads/Servicesimage/Actual/<%# Eval("ReciptImg") %>" target="_blank" class="btn btn-info">Download
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_remarks" runat="server" Text='<%#Eval("Remarks") %>' Enabled='<%#Eval("Status").ToString() == "Pending" ? true :false %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_refno" runat="server" Text='<%#Eval("RefNo") %>' Enabled='<%#Eval("Status").ToString() == "Pending" ? true :false %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="30px">
                                                <ItemTemplate>
                                                    <asp:Button ID="Approve" runat="server" Text="Approve" CommandArgument='<%#Eval("Pankid") %>' CssClass="btn btn-success"
                                                        CommandName="Approve" Visible='<%#Eval("Status").ToString() == "Pending" ? true :false %>' ToolTip="Approve Request" />


                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="30px">
                                                <ItemTemplate>
                                                    <asp:Button ID="Reject" runat="server" Text="Reject" CommandArgument='<%#Eval("Pankid") %>' CssClass="btn btn-danger"
                                                        CommandName="Reject" Visible='<%#Eval("Status").ToString() == "Pending" ? true :false %>' ToolTip="Reject Request" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="30px">
                                                <ItemTemplate>
                                                    <asp:Button ID="TempReject" runat="server" Text="Temp Reject" CommandArgument='<%#Eval("Pankid") %>' CssClass="btn btn-info"
                                                        CommandName="TempReject" Visible='<%#Eval("Status").ToString() == "Pending" ? true :false %>' ToolTip="Temp Reject Request" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
