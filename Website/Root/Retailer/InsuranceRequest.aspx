<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="InsuranceRequest.aspx.cs" Inherits="Root_Retailer_InsuranceRequest" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="../../js/script_pop.js"></script>
  <link href="../../css/style2.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Insurance Request
            </h3>
        </div>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                         <div id="toPopup" style="position:relative;">
                           <div id="popup_content" style="height: 420px !important;">
            <asp:UpdatePanel ID="upd" runat="server">
                <ContentTemplate>
                      <div class="table-responsive">
                           <h3 class="page-title">Quotation For Application</h3>
                                   
                    <table class="table table-bordered">
                        <tr>
                            <td> <h5><strong><span class="red"></span>Member Info</strong></h5>
                      
                            </td>
                            <td>
                                <asp:Literal ID="litMember" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                               <h5><strong><span class="red"></span>Status</strong></h5>
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
                            <td>
                            <h5><strong><span class="red"></span> Refrance No</strong></h5>
                  
                            </td>
                            <td>
                                <asp:TextBox ID="txt_refno" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqrefno" runat="server" Enabled="false" ErrorMessage="Please Enter Refrance No !"
                                    ControlToValidate="txt_refno" Display="Dynamic" SetFocusOnError="True" ValidationGroup="1v"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                   
                          <tr>
        <td>
            <h5>
                <strong>OD</strong></h5>
        </td>
        <td>
            <asp:TextBox ID="txt_od" runat="server" MaxLength="50" CssClass="form-control" ></asp:TextBox>
                
            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" 
                ControlToValidate="txt_od" Display="Dynamic" ErrorMessage="Please Enter Registration No."
                SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr runat="server" id="pan3">
        <td>
            <h5>
                <strong>TP</strong>
            </h5>
        </td>
        <td>
            <asp:TextBox ID="txt_tp" runat="server" MaxLength="50" CssClass="form-control" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="panval2" runat="server" ErrorMessage="Make Required"
                ControlToValidate="txt_tp" Display="Dynamic" SetFocusOnError="True" 
                ValidationGroup="v"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <h5>
                <strong>Discount</strong></h5>
        </td>
        <td>
            &nbsp;<asp:TextBox ID="txt_discount" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txt_discount"
                Display="Dynamic" ErrorMessage="Please Enter model name !" SetFocusOnError="True"
                ValidationGroup="v"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr runat="server" id="pan4">
        <td>
            <h5>
                <strong>Tax</strong></h5>
        </td>
        <td>
            <asp:TextBox ID="txt_tax" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txt_tax"
                Display="Dynamic" ErrorMessage="Please Enter year" SetFocusOnError="True"
                ValidationGroup="v"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <h5>
                <strong>Primium</strong></h5>
        </td>
        <td>
            <asp:TextBox ID="txt_prminum" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox> 
               
            <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ErrorMessage="Please Enter Aadhar Number !"
                ControlToValidate="txt_prminum" Display="Dynamic" SetFocusOnError="True" 
                ValidationGroup="v"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <h5>
                <strong>Comission</strong>
            </h5>
        </td>
        <td>
            <asp:TextBox ID="txt_comission" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="Please Enter Aadhar Number !"
                ControlToValidate="txt_comission" Display="Dynamic" SetFocusOnError="True" 
                ValidationGroup="v"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <h5>
                <strong>NetPayable</strong></h5>
        </td>
        <td>
            <asp:TextBox ID="txt_netpayable" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ErrorMessage="Please Enter Aadhar Number !"
                ControlToValidate="txt_netpayable" Display="Dynamic" SetFocusOnError="True" 
                ValidationGroup="v"></asp:RequiredFieldValidator>
        </td>
    </tr>
     <tr> <td>
            <h5>
                <strong>Remarks</strong></h5>
        </td>
        <td>
            <asp:TextBox ID="txtrtremaraks" runat="server" MaxLength="5000"  CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Remarks !"
                ControlToValidate="txtrtremaraks" Display="Dynamic" SetFocusOnError="True" 
                ValidationGroup="v"></asp:RequiredFieldValidator>
        </td>
                        </tr>
                        
       

                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnSuccess" runat="server" Visible="false" Text="ReSubmit For Quotation" ValidationGroup="v" CssClass="btn btn-primary"
                                    OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnFail" runat="server" Text="Success" CssClass="btn btn-primary" ValidationGroup="v" OnClick="btnFail_Click" />
                                           <asp:Button ID="btnclose" runat="server" Visible="true" Text="Close" OnClientClick="javascript:disablePopup()" 
                                    CssClass="btn btn-danger" />
                            </td>
                        </tr>


                         <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnpayment" runat="server" Visible="false" Text="Click Here To do Payment"
                                    OnClick="btnpayment_Click" CssClass="btn btn-danger" />
                                
                            </td>
                        </tr>

                    </table>
                          </div>
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
        </div>
                        </div>
    <div class="loader">
    </div>
    <div id="backgroundPopup">
    </div>
    <div class="content-header">
 
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Retailer</a></li>
            <li class="active">List Insurance Request</li>
        </ol>
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
                      <asp:ImageButton ID="btnexportExcel" runat="server" ImageUrl="../images/icon/excel_32X32.png"
                                                    CssClass="class24" OnClick="btnexportExcel_Click" />
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
                            <td>
                                By Request Status
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_status" CssClass="form-control" runat="server">
                                
                                   <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                    <asp:ListItem Value="Success">Success</asp:ListItem>
                                    <asp:ListItem Value="Awaitingforpayment">Awaitingforpayment</asp:ListItem>
                                    <asp:ListItem Value="ReSubmitQuotation">ReSubmitQuotation</asp:ListItem>
                                     <asp:ListItem Value="PendingforQutoation">PendingforQutoation</asp:ListItem>
                                      <asp:ListItem Value="Payment Done">Payment Done</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                Txn ID
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
                             <div class="table-responsive">
                             <div class="table-responsive" style="overflow-y:auto;">
                                        <asp:GridView ID="gvBookedBusList" runat="server" CssClass="table table-bordered table-hover"
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
                                                <asp:BoundField HeaderText="Type" DataField="Type" SortExpression="Type" />
                                                <asp:BoundField HeaderText="Other" DataField="Other" SortExpression="Other" />
                                                <asp:BoundField HeaderText="Registrationno" DataField="Registrationno" SortExpression="Registrationno" />
                                                <asp:BoundField HeaderText="Make" DataField="Make" SortExpression="Make" />
                                                 <asp:BoundField HeaderText="Model" DataField="Model" SortExpression="Model" />
                                                <asp:BoundField HeaderText="Year" DataField="Year" SortExpression="Year" />
                                                <asp:BoundField HeaderText="TType" DataField="TType" SortExpression="TType" />
                                                    <asp:BoundField HeaderText="Mobile" DataField="Mobile" SortExpression="Mobile" />
                                                 <asp:BoundField HeaderText="Email" DataField="Email" SortExpression="Email" />
                                                    <asp:BoundField HeaderText="IDV" DataField="IDV" SortExpression="IDV" />
                                                    <asp:BoundField HeaderText="LastNCV" DataField="LastNCP" SortExpression="LastNCP" />
                                                <asp:TemplateField HeaderText="Request On" SortExpression="RequestDate">
                                                    <ItemTemplate>
                                                        <%#String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(Eval("RequestDate")))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="30px">
                                                    <ItemTemplate>
                                                        <asp:Button ID="Approve" runat="server" Text="View Details" CssClass="btn btn-primary" CommandArgument='<%#Eval("kid") %>'  CommandName="Show"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                    <%--<asp:TemplateField HeaderText="Download Policy">
                                                        <ItemTemplate>
                                                            <a href="../../Uploads/InsuranceRequest/Actual/<%# Eval("Policyimage") %>" target="_blank">Download</a>
                   
                                                        </ItemTemplate>
                                                    </asp:TemplateField> --%>   
                                                
                                            </Columns>
                                        </asp:GridView>
                                </div></div>
                         
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnexportExcel" />



        </Triggers>
    </asp:UpdatePanel>
    </div></div>
                        </div></div></div>
</asp:Content>

