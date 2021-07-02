<%@ Page Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="FlightRefundCncelAmt.aspx.cs" Inherits="FlightRefundCncelAmt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <style type="text/css">
        .divWaiting {
            position: fixed;
            background-color: #FAFAFA;
            z-index: 2147483647 !important;
            opacity: 0.8;
            overflow: hidden;
            text-align: center;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
            padding-top: 20%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                ClientIDMode="Predictable" ViewStateMode="Inherit" style="height: auto; width: 100%;">
                <ProgressTemplate>
                    <div class="divWaiting">
                        <div id="progressBackgroundFilter">
                        </div>
                        <div id="processMessage">
                            <div class="loading_page" align="center" style="background-color: White; vertical-align: middle">
                                <img src="../../flight/Images/flight/progressbar.gif" alt="Loading..." style="float: none" />&#160;Please
                            Wait...
                            </div>
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
    <section class="banner py-0">
        <div class=" owl-carousel hero-slider">
            <div style="background-color: #164390; min-width: 1300px; height: 155px !important;">
            </div>
        </div>
    </section>
    <div class="content-header">
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Refund Cancelled Flight Ticket</a></li>
            <li class="active">Flight Ticket List</li>
        </ol>
    </div>
   
            <div>
                <table class="table table-bordered table-hover ">
                    <tr>
                        <td>From Date
                        </td>
                        <td>
                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control" autocomplete="Off"></asp:TextBox>
                            <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="dd-MM-yyyy" PopupButtonID="txtfromdate"
                                TargetControlID="txtfromdate">
                            </cc1:CalendarExtender>
                        </td>
                        <td>To Date
                        </td>
                        <td>
                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control" autocomplete="Off"></asp:TextBox>
                            <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="dd-MM-yyyy" Animated="False"
                                PopupButtonID="txttodate" TargetControlID="txttodate">
                            </cc1:CalendarExtender>
                        </td>
                        <asp:TextBox ID="txtMemberid" runat="server" CssClass="form-control" MaxLength="8"
                            Visible="false"></asp:TextBox>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search &gt;&gt;" OnClick="btnSearch_Click"
                                class="btn btn-primary" />
                        </td>
                    </tr>
                </table>
                <asp:MultiView ID="MV" runat="server" ActiveViewIndex="0">
                    <asp:View ID="v1" runat="server">
                        <div style="font-size: 12px; margin: 0px; padding: 0; visibility: hidden;">
                            <table class="aleft">
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
                        </div>
                        <table class="aleft" style="width: 100%;">
                            <tr>
                                <td>
                                    <div class="box-inner">
                                        <div class="box-content">
                                            <asp:GridView ID="gv_Transaction" runat="server" CssClass="table table-bordered table-striped dtable"
                                                AutoGenerateColumns="false" AllowPaging="true" DataKeyNames="id" Width="100%" EmptyDataText=" No Record Found"
                                                AllowSorting="false" ShowHeaderWhenEmpty="true" OnRowCommand="gvTicketTransaction_RowCommand" OnPageIndexChanging="gv_Transaction_PageIndexChanging" PageSize="10" ShowFooter="true">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="id" DataField="id" SortExpression="id" />
                                                    <asp:BoundField HeaderText="TicketId" DataField="TicketId" SortExpression="TicketId" />
                                                    <asp:BoundField HeaderText="PNR" DataField="PNR" SortExpression="PNR" />
                                                    <asp:BoundField HeaderText="BookingId" DataField="BookingId" SortExpression="BookingId" />
                                                    <asp:BoundField HeaderText="AgentId" DataField="Air_agentid" SortExpression="Air_agentid" />
                                                    <asp:BoundField HeaderText="PublishFee" DataField="Air_PublishFee" SortExpression="Air_PublishFee" />
                                                     <asp:BoundField HeaderText="Amount" DataField="amountstatus" SortExpression="amountstatus" />
                                                       <asp:BoundField HeaderText="Flight" DataField="FlightStatus" SortExpression="FlightStatus" />
                                                    <asp:TemplateField HeaderText="Date" SortExpression="addedon">
                                                        <ItemTemplate>
                                                            <%#String.Format("{0:dd-MMM-yyyy hh:mm tt}", Convert.ToDateTime(Eval("addedon")))%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Refund">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btn_Cancel" runat="server" CommandName="Refund" Text="Refund"
                                                                CommandArgument='<%#Eval("id")%>' />
                                                        </ItemTemplate>
                                                        
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Receipt">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btn_Receipt" runat="server" CommandName="Receipt" Text="Receipt"
                                                                CommandArgument='<%#Eval("id")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

