<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bus.aspx.cs" Inherits="bus" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true"
    CodeFile="bus.aspx.cs" Inherits="Root_Distributor_bus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../js/jquery.dataTables.min.js" type="text/javascript"></script>
    <link href="../chosen/chosen.css" rel="stylesheet" type="text/css" />
    <script src="../chosen/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../chosen/chosen.jquery.js" type="text/javascript"></script>
    <script src="../chosen/chosen.proto.js" type="text/javascript"></script>

    <script src="../jss/bus.js" type="text/javascript"></script>
  
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript">
        $(window).load(function () {
            $("#pageloaddiv").fadeOut(2000);
        });
    </script>
    <style type="text/css">
        #container {
            margin: 10px auto;
            width: 80%;
        }

        .auto-style1 {
            width: 60%;
        }

        .auto-style2 {
            height: 23px;
        }
        

        .auto-style3 {
        }

        .auto-style4 {
            width: 798px;
        }

        .auto-style5 {
            height: 59px;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            width: 80%;
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 80%;
            border: 3px solid #0DA9D0;
            padding: 0;
        }

            .modalPopup .header {
                background-color: #2FBDF1;
                height: 500px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .body {
                min-height: 100px;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

        .modalPop {
            background-color: #FFFFFF;
            width: 50%;
            border: 3px solid #0DA9D0;
            padding: 0;
        }

            .modalPop .header {
                background-color: #2FBDF1;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPop .body {
                min-height: 100px;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

        .style5 {
            height: 23px;
            width: 102%;
        }

        .style7 {
            width: 176px;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
             // alert("document");
            $(".chzn-select").chosen();
            $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
             window.location.reload();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="Hidden1" runat="server" />
    <asp:Panel ID="pnlsearchbus" runat="server" CssClass="modal-content">
        <div class="content-wrapper">
            <div class="page-header">
                <h3 class="page-title">Bus Booking
                </h3>
            </div>
            <div class="row grid-margin">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">From<code>*</code></label>
                                    <div class="col-sm-9">
                                        <asp:DropDownList ID="ddl_leavingfrom" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">To<code>*</code></label>
                                    <div class="col-sm-9">
                                        <asp:DropDownList ID="ddl_goingto" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">Date<code>*</code></label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox><ajaxToolkit:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" BehaviorID="TextBox1_CalendarExtender" CssClass="cal_Theme1" TargetControlID="TextBox1" Format="yyyy-MM-dd" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group row">

                                    <div class="col-sm-9">
                                        <asp:Button ID="Button1" runat="server" BackColor="#392C70" Font-Bold="True"
                                            ForeColor="White" Height="37px" Text="Search" Width="100px"
                                            OnClick="Button1_Click" />
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <section class="after_search">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="_panel">
                        <div class="clearfix"></div>
                        <div class="panel-body_" style="min-height: 230px;">
                            <div class="table-responsive">
                                <asp:UpdatePanel ID="upbusserch" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="bussearches" runat="server">
                                            <table width="100%" cellspacing="0">
                                                <tr id="t1" runat="server" visible="false" style="padding-top: 30px;">
                                                    <td class="auto-style4">
                                                        <asp:Label ID="lblsource" runat="server" Font-Bold="True" Font-Italic="True"
                                                            Font-Size="X-Large"></asp:Label>
                                                        &nbsp; ➠&nbsp;&nbsp;
                                   
                                    <asp:Label ID="lbldestination" runat="server" Font-Bold="True"
                                        Font-Italic="True" Font-Size="X-Large"></asp:Label>
                                                        &nbsp;
                                    <asp:Label ID="lbldoj" runat="server" Font-Bold="True"></asp:Label>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td class="auto-style4">&nbsp;</td>
                                                    <td align="right">&nbsp;</td>
                                                </tr>
                                            </table>
                                            <asp:Repeater ID="rptrCustomer" runat="server"
                                                OnItemDataBound="rptrCustomer_ItemDataBound">

                                                <ItemTemplate>

                                                    <table width="100%" border="0" style="border-color: #e9ecef; background-color: #eff3f7;" cssclass="modal-content">
                                                        <tr>
                                                            <td tabindex="0" style="width: 300px; padding-top: 18px; padding-bottom: 10px; padding-left: 10px; padding-right: 10px;">

                                                                <strong>
                                                                    <asp:Label ID="lbltravelsname" runat="server"
                                                                        ForeColor="#121313" Text='<%# DataBinder.Eval(Container.DataItem, "travels")%>'></asp:Label></strong><br>
                                                                <br />
                                                                <strong>
                                                                    <asp:Label ID="lblbustype" runat="server"
                                                                        Text='<%# Eval("bustype") %>' ForeColor="#E10AE8"></asp:Label></strong>
                                                                <br />
                                                                <%--  <td><a href="#openModal"   CommandArgument='<%# Eval("data_Id") %>'on_click="linkbtncancelpolicy_Click" class="badge badge-success badge-pill" title="click to view member detail">Cancellaion Policy</a></td>--%>
                                                                <asp:LinkButton ID="linkbtncancelpolicy" runat="server"
                                                                    CommandArgument='<%# Eval("data_Id") %>' ForeColor="white" class="badge badge-success badge-pill" title="click to view Cancellaion Policy" OnCommand="linkbtncancelpolicy_Click">Cancellaion Policy</asp:LinkButton>

                                                            </td>
                                                            <td>

                                                                <span>
                                                                    <asp:Label ID="lbldeparturedate" runat="server"
                                                                        ForeColor="#1A1B1C" Text='<%# Eval("doj")%>'></asp:Label><br>
                                                                    <asp:Label ID="lbldeparturetime" runat="server"
                                                                        ForeColor="#1A1B1C" Text='<%# Eval("departureTime") %>'></asp:Label><br>
                                                                    <br>
                                                                    <asp:LinkButton ID="linkbtnboardingpoints" runat="server"
                                                                        BackgroundCssClass="modalBackground" CommandArgument='<%# Eval("data_Id") %>'
                                                                        ForeColor="white" class="badge badge-success badge-pill" title="click to view Boarding Points" OnCommand="linkbtnboardingpoints_Click">Boarding Points</asp:LinkButton>
                                                                </span></td>
                                                            <td>

                                                                <strong>
                                                                    <center>
                          <i class="fa fa-clock-o"></i><br>
                         <asp:Label ID="lblduration" runat="server"  ForeColor="#424F63" 
                                                       ></asp:Label>
                        </center>
                                                                </strong></td>
                                                            <td>
                                                                <span>
                                                                    <asp:Label ID="lblarivaldate" runat="server"
                                                                        ForeColor="#1A1B1C" Text='<%# Eval("doj")%>'></asp:Label><br>
                                                                    <asp:Label ID="lblarivaltime" runat="server"
                                                                        ForeColor="#1A1B1C" Text='<%# Eval("arrivalTime") %>'></asp:Label><br>
                                                                    <br>
                                                                    <asp:LinkButton ID="linkbtndroppingpoints" runat="server"
                                                                        CommandArgument='<%# Eval("data_Id") %>' ForeColor="white" class="badge badge-success badge-pill" title="click to view Dropping Points"
                                                                        OnCommand="linkbtndroppingpoints_Click">Dropping Points</asp:LinkButton></span></td>
                                                            <td>
                                                                <strong>
                                                                    <asp:Label ID="lblavaibleseats" runat="server"
                                                                        ForeColor="#F47809" Text='<%#Eval("availableSeats") + " " + "Seats"  %>'></asp:Label>
                                                                </strong></td>
                                                            <td style="width: 80px; color: #0b94f7;"><small><i>Starting from </i></small>
                                                                <br>
                                                                <strong>
                                                                    <asp:Label ID="lblprice" runat="server" Font-Bold="True" ForeColor="#0b94f7" ToolTip='<%# Eval("data_Id") %>'>&gt;</asp:Label>
                                                                </strong>
                                                                <br>
                                                                <br>
                                                                <asp:Button ID="btnviewseats" runat="server" BackColor="#392C70" CommandArgument='<%#Eval("id") + "-" + Eval("data_Id") + "-" + Eval("travels") %>' ForeColor="White"
                                                                    Text="View Seats" Width="80%" OnClick="btnviewseats_Click" class="btn btn-default btn btn-primary" ValidationGroup="X" />
                                                            </td>
                                                        </tr>
                                                        <br />
                                                    </table>

                                                </ItemTemplate>



                                            </asp:Repeater>

                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>


    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
        PopupControlID="pnlseatlayout" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="pnlseatlayout" runat="server" Visible="true" Width="70%" Height="600px" CssClass="modal-content" Style="display: none;">
        <div class="row grid-margin">
            <div class="col-12">
                <div class="table-responsive">
                    <table width="100%">
                        <tr>
                            <td>

                                <asp:DropDownList ID="ddboardingpoint" runat="server" CssClass="form-control"
                                    TabIndex="2" Width="300px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                    runat="server" ErrorMessage="*" ForeColor="Red"
                                    ControlToValidate="ddboardingpoint" ValidationGroup="b"
                                    InitialValue="Boarding Point"></asp:RequiredFieldValidator>

                            </td>
                            <td>
                                <asp:DropDownList ID="dddroppingpoint" runat="server" CssClass="form-control"
                                    TabIndex="2" Width="300px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ErrorMessage="*" ForeColor="Red" ControlToValidate="dddroppingpoint"
                                    ValidationGroup="b" InitialValue="Dropping Point"></asp:RequiredFieldValidator></td>
                            <td style="align-content: flex-end; align: right;">
                                <asp:Button ID="btnHide" runat="server" Text="X" CssClass="close" OnClick="btnHide_Click" data-dismiss="modal" />
                            </td>

                        </tr>
                    </table>

                    <div id="container">
                        <div id="upperDeckContainer" style="border: 1px solid #000; padding: 2%; border-radius: 5px; width: 80%;">
                            <p>
                                Upper
                            </p>
                        </div>
                        <div id="lowerDeckContainer" style="border: 1px solid #000; padding: 2%; margin-top: 1%; border-radius: 5px; width: 80%;">
                            <p>
                                Lower
                            </p>
                        </div>
                        <%--<img src="images/Seatlayouts.jpg" />--%>
                        <div id="selectedSeatsContainer" style="margin-top: 2%; margin-bottom: 2%">
                            <div style="float: left;">
                                <div style="float: left;">
                                    Seat(s)
                                </div>

                                <div id="seatsContainer" style="float: left;" runat="server">
                                </div>
                            </div>
                            <div style="float: right;">
                                <div style="float: left;">
                                    Fare:
                                </div>
                                <span id="fareContainer" runat="server" style="float: left; color: red; font-weight: bold; border-width: 0;" enabled="true" width="20px"></span>
                                <%-- <asp:Label ID="fareContainer" runat="server" Text="0" style="float:left;color: red; font-weight: bold; border-width:0;" Enabled="true" Width="20px"></asp:Label>--%>
                                <asp:HiddenField ID="hftotalFare" runat="server" />
                                <asp:HiddenField ID="hfselectedseats" runat="server" />
                                <asp:HiddenField ID="hfselectedseats1" runat="server" />
                                <asp:HiddenField ID="hfselectedseats2" runat="server" />
                                <asp:HiddenField ID="hfselectedseats3" runat="server" />
                                <asp:HiddenField ID="hfselectedseats4" runat="server" />
                                <asp:HiddenField ID="hfselectedseats5" runat="server" />
                            </div>

                        </div>

                        <div style="float: right;"></div>
                        <div style="float: right;">
                            <asp:Button ID="btnnextseat" runat="server" BackColor="#1161ea" Font-Bold="True" ForeColor="White" Height="41px" Text="Next" Width="90px" ValidationGroup="b" OnClick="btnnextseat_Click" />

                        </div>
                    </div>

                </div>
            </div>
        </div>

    </asp:Panel>

    <triggers>
            <asp:PostBackTrigger ControlID="btnnextseat" />
        </triggers>

    <asp:LinkButton ID="LinkButton2" runat="server"></asp:LinkButton>
    <ajaxToolkit:ModalPopupExtender ID="mpboardingpoints" runat="server"
        PopupControlID="pnlboardinglayout" TargetControlID="LinkButton2" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="pnlboardinglayout" runat="server" Visible="true" Width="40%" CssClass="modal-content" Style="display: none;">
        <table width="100%">
            <tr>
                <td align="left">
                    <asp:Label ID="lbldoj0" runat="server" Font-Bold="True">Boarding Points</asp:Label>
                </td>
                <td align="right">
                    <asp:Button ID="btnhideboarding" runat="server" Text="X" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                        <HeaderTemplate>
                            <table class="table table-bordered table-hover" align="center" width="100%">

                                <thead>
                                    <tr>
                                        <th>Time
                                        </th>
                                        <th>Location
                                        </th>

                                    </tr>
                                </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>

                                <td>
                                    <asp:Label ID="lbltime" runat="server" Font-Bold="True" ForeColor="#424F63"
                                        Text='<%# Eval("time") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbllocation" runat="server" Font-Bold="True" ForeColor="#424F63"
                                        Text='<%# Eval("bpName") %>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>

        </table>
    </asp:Panel>

    <br />
    <br />
    <br />

    <asp:LinkButton ID="linkcancelid" runat="server"></asp:LinkButton>
    <ajaxToolkit:ModalPopupExtender ID="mpcancelpopup" runat="server"
        PopupControlID="pnlcancelayout" TargetControlID="linkcancelid" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="pnlcancelayout" runat="server" Visible="true" Width="50%" CssClass="modal-content" Style="display: none;">
        <div id="openModal" class="modalDialog">
            <div class="row">
                <div class="col-md-12">
                    <div class="info-box">
                        <table width="100%">
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label4" runat="server" Font-Bold="True">Cancellation Policy</asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Button ID="Button2" runat="server" Text="X" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Repeater ID="rptcancelpolicy" runat="server">
                                        <HeaderTemplate>
                                            <table class="table table-bordered table-hover">
                                                <thead>
                                                    <tr>
                                                        <th>Description
                                                        </th>
                                                        <th>Charges
                                                        </th>

                                                    </tr>
                                                </thead>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>

                                                <td>
                                                    <asp:Label ID="lblcanceldesc" runat="server" Font-Bold="True" ForeColor="#424F63"
                                                        Text='<%# Eval("Description") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblcancelcharge" runat="server" Font-Bold="True" ForeColor="#424F63"
                                                        Text='<%# Eval("Charges") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>

                        </table>
                    </div>
                </div>
            </div>
        </div>

    </asp:Panel>
    <br />
    <br />



    <asp:LinkButton ID="LinkButton3" runat="server"></asp:LinkButton>
    <ajaxToolkit:ModalPopupExtender ID="mpdroppingpoints" runat="server"
        PopupControlID="pnldroppinglayout" TargetControlID="LinkButton3" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnldroppinglayout" runat="server" Visible="true" Width="40%"
        CssClass="modal-content" Style="display: none;">
        <table width="100%">
            <tr>
                <td align="left">
                    <asp:Label ID="lbldoj1" runat="server" Font-Bold="True">Dropping Points</asp:Label>
                </td>
                <td align="right">
                    <asp:Button ID="btnhidedropping" runat="server" Text="X" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Repeater ID="Repeater3" runat="server" OnItemDataBound="Repeater3_ItemDataBound">
                        <HeaderTemplate>
                            <table class="table table-bordered table-hover"
                                align="center" width="100%">

                                <thead>
                                    <tr>
                                        <th>Time
                                        </th>
                                        <th>Location
                                        </th>

                                    </tr>
                                </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>

                                <td>
                                    <asp:Label ID="lbltime0" runat="server" Font-Bold="True" ForeColor="#424F63"
                                        Text='<%# Eval("time") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbllocation0" runat="server" Font-Bold="True" ForeColor="#424F63"
                                        Text='<%# Eval("bpName") %>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>

        </table>
    </asp:Panel>
    <br />
    <br />

    <asp:LinkButton ID="LinkButton4" runat="server"></asp:LinkButton>
    <ajaxToolkit:ModalPopupExtender ID="mppassanger" runat="server"
        PopupControlID="pnlpassangerlist" TargetControlID="LinkButton4" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="pnlpassangerlist" runat="server" Visible="true"
        CssClass="modal-content" Style="display: none; padding: 10px 20px 10px 20px;">
        <div style="max-height: 650px; overflow-y: scroll;">
            <asp:Panel ID="pnlmain" runat="server" Visible="false">

                <table class="border-dottet">
                    <tr>
                        <td colspan="2" align="right">
                            <asp:Button ID="btnhideboarding0" CssClass="top-button" runat="server"
                                OnClick="btnhideboarding0_Click" Text="X" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="background-color: #666666; padding: 2px 0 2px 10px;">
                            <asp:Label ID="Label29" runat="server" Font-Bold="True" ForeColor="White"
                                Text="Passanger Details"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3">
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" Font-Bold="True" Font-Size="Medium" RepeatDirection="Horizontal">
                                <asp:ListItem>MR</asp:ListItem>
                                <asp:ListItem>MRS</asp:ListItem>
                                <asp:ListItem>MS</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="vvv" ControlToValidate="RadioButtonList1"></asp:RequiredFieldValidator>
                        </td>

                        <td class="auto-style3">
                            <asp:Label ID="Label2" runat="server" BackColor="Gray" Font-Bold="True"
                                ForeColor="White" Text="Seat Name:"></asp:Label>
                            <asp:Label ID="lblseat1" runat="server" BackColor="#33CC33" Font-Bold="True"
                                ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                    <tr>

                        <td class="auto-style2">
                            <asp:TextBox ID="txtprimarypassanger" runat="server" Placeholder="Primary Passanger" Style="border: 0px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="vvv" ControlToValidate="txtprimarypassanger"></asp:RequiredFieldValidator>

                        </td>

                        <td class="auto-style2">
                            <asp:TextBox ID="txtage" runat="server" Placeholder="Age" MaxLength="3" Style="border: 0px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="vvv" ControlToValidate="txtage"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revNumberPrepaid" runat="server"
                                ControlToValidate="txtage" ErrorMessage="Invalid age !" ValidationExpression="^(0|[1-9]\d*)$"
                                ValidationGroup="vvv"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Panel ID="pnlcopsg1" runat="server" Visible="false">
                <table class="border-dottet">

                    <tr>
                        <td>
                            <asp:RadioButtonList ID="rbtcopsg1" runat="server" Font-Bold="True"
                                Font-Size="Medium" RepeatDirection="Horizontal">
                                <asp:ListItem>MR</asp:ListItem>
                                <asp:ListItem>MRS</asp:ListItem>
                                <asp:ListItem>MS</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="vvv" ControlToValidate="rbtcopsg1"></asp:RequiredFieldValidator>
                        </td>

                        <td>
                            <asp:Label ID="Label6" runat="server" BackColor="Gray" Font-Bold="True"
                                ForeColor="White" Text="Seat Name:"></asp:Label>
                            <asp:Label ID="lblcoseat1" runat="server" BackColor="#33CC33" Font-Bold="True"
                                ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                    <tr>

                        <td class="auto-style2">
                            <asp:TextBox ID="txtcopaasanger1" runat="server" placeholder="Co Passanger-1" Style="border: 0px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="vvv" ControlToValidate="txtcopaasanger1"></asp:RequiredFieldValidator>
                        </td>

                        <td class="auto-style2">
                            <asp:TextBox ID="txtage1" runat="server" placeholder="Age" Style="border: 0px;" MaxLength="3"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="vvv" ControlToValidate="txtage1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                ControlToValidate="txtage1" ErrorMessage="Invalid age !" ValidationExpression="^(0|[1-9]\d*)$"
                                ValidationGroup="vvv"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Panel ID="pnlcopsg2" runat="server" Visible="false">
                <table class="border-dottet">

                    <tr>
                        <td>
                            <asp:RadioButtonList ID="rbtcopsg2" runat="server" Font-Bold="True"
                                Font-Size="Medium" RepeatDirection="Horizontal">
                                <asp:ListItem>MR</asp:ListItem>
                                <asp:ListItem>MRS</asp:ListItem>
                                <asp:ListItem>MS</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="vvv" ControlToValidate="rbtcopsg2"></asp:RequiredFieldValidator>
                        </td>

                        <td>
                            <asp:Label ID="Label10" runat="server" BackColor="Gray" Font-Bold="True"
                                ForeColor="White" Text="Seat Name:"></asp:Label>
                            <asp:Label ID="lblcoseat2" runat="server" BackColor="#33CC33" Font-Bold="True"
                                ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                    <tr>

                        <td class="auto-style2">
                            <asp:TextBox ID="txtcopassanger2" runat="server" placeholder="Co Passanger-2" Style="border: 0px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="vvv" ControlToValidate="txtcopassanger2"></asp:RequiredFieldValidator>
                        </td>

                        <td class="auto-style2">
                            <asp:TextBox ID="txtage2" runat="server" placeholder="Age" MaxLength="3" Style="border: 0px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="vvv" ControlToValidate="txtage2"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                ControlToValidate="txtage2" ErrorMessage="Invalid age !" ValidationExpression="^(0|[1-9]\d*)$"
                                ValidationGroup="vvv"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Panel ID="pnlcopsg3" runat="server" Visible="false">
                <table class="border-dottet">

                    <tr>
                        <td>
                            <asp:RadioButtonList ID="rbtcopsg3" runat="server" Font-Bold="True"
                                Font-Size="Medium" RepeatDirection="Horizontal">
                                <asp:ListItem>MR</asp:ListItem>
                                <asp:ListItem>MRS</asp:ListItem>
                                <asp:ListItem>MS</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="vvv" ControlToValidate="rbtcopsg3"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="Label14" runat="server" BackColor="Gray" Font-Bold="True"
                                ForeColor="White" Text="Seat Name:"></asp:Label>
                            <asp:Label ID="lblcoseat3" runat="server" BackColor="#33CC33" Font-Bold="True"
                                ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                    <tr>

                        <td>
                            <asp:TextBox ID="txtcopassanger3" runat="server" placeholder="Co Passanger-3" Style="border: 0px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="vvv" ControlToValidate="txtcopassanger3"></asp:RequiredFieldValidator>
                        </td>

                        <td>
                            <asp:TextBox ID="txtage3" runat="server" placeholder="Age" MaxLength="3" Style="border: 0px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="vvv" ControlToValidate="txtage3"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                ControlToValidate="txtage3" ErrorMessage="Invalid age !" ValidationExpression="^(0|[1-9]\d*)$"
                                ValidationGroup="vvv"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlcopsg4" runat="server" Visible="false">
                <table class="border-dottet">
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="rbtcopsg4" runat="server" Font-Bold="True"
                                Font-Size="Medium" RepeatDirection="Horizontal">
                                <asp:ListItem>MR</asp:ListItem>
                                <asp:ListItem>MRS</asp:ListItem>
                                <asp:ListItem>MS</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="vvv" ControlToValidate="rbtcopsg4"></asp:RequiredFieldValidator>
                        </td>

                        <td>
                            <asp:Label ID="Label18" runat="server" BackColor="Gray" Font-Bold="True"
                                ForeColor="White" Text="Seat Name:"></asp:Label>
                            <asp:Label ID="lblcoseat4" runat="server" BackColor="#33CC33" Font-Bold="True"
                                ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                    <tr>

                        <td>
                            <asp:TextBox ID="txtcopassanger4" runat="server" MaxLength="3" placeholder="Co Passanger-4" Style="border: 0px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="vvv" ControlToValidate="txtcopassanger4"></asp:RequiredFieldValidator>
                        </td>

                        <td>
                            <asp:TextBox ID="txtage4" runat="server" placeholder="Age" MaxLength="3" Style="border: 0px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="vvv" ControlToValidate="txtage4"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                ControlToValidate="txtage4" ErrorMessage="Invalid age !" ValidationExpression="^(0|[1-9]\d*)$"
                                ValidationGroup="vvv"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Panel ID="pnlcopsg5" runat="server" Visible="false">
                <table class="border-dottet">


                    <tr>
                        <td>
                            <asp:RadioButtonList ID="rbtcopsg5" runat="server" Font-Bold="True"
                                Font-Size="Medium" RepeatDirection="Horizontal">
                                <asp:ListItem>MR</asp:ListItem>
                                <asp:ListItem>MRS</asp:ListItem>
                                <asp:ListItem>MS</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="vvv" ControlToValidate="rbtcopsg5"></asp:RequiredFieldValidator>

                        </td>

                        <td>
                            <asp:Label ID="Label22" runat="server" BackColor="Gray" Font-Bold="True"
                                ForeColor="White" Text="Seat Name:"></asp:Label>
                            <asp:Label ID="lblcoseat5" runat="server" BackColor="#33CC33" Font-Bold="True"
                                ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtcopassanger5" runat="server" placeholder="Co Passanger-5" Style="border: 0px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="vvv" ControlToValidate="txtcopassanger5"></asp:RequiredFieldValidator>
                        </td>

                        <td>
                            <asp:TextBox ID="txtage5" runat="server" placeholder="Age" MaxLength="3" Style="border: 0px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="vvv" ControlToValidate="txtage5"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server"
                                ControlToValidate="txtage5" ErrorMessage="Invalid age !" ValidationExpression="^(0|[1-9]\d*)$"
                                ValidationGroup="vvv"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="Panel1" runat="server">
                <table class="border-dottet">
                    <tr>
                        <td colspan="2" style="background-color: #374152">
                            <asp:Label ID="Label26" runat="server" Font-Bold="True" ForeColor="White"
                                Text="Contact Details"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtmobile" runat="server" placeholder="Mobile" Style="border: 0px;" MaxLength="10"></asp:TextBox>

                            <ajaxToolkit:FilteredTextBoxExtender ID="txtMobile_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txtmobile">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="vvv" ControlToValidate="txtmobile"></asp:RequiredFieldValidator>
                        </td>

                        <td>
                            <asp:TextBox ID="txtemail" runat="server" placeholder="Email" Style="border: 0px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="vvv" ControlToValidate="txtemail"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator
                                ID="RegularExpressionValidator1" runat="server" ErrorMessage="Incorrect Email" ForeColor="Red" ValidationGroup="vvv" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtemail"></asp:RegularExpressionValidator>
                        </td>
                    </tr>

                    <tr>
                        <td>&nbsp;</td>
                        <td align="right">
                            <asp:Button ID="btnnextprocess" runat="server" ValidationGroup="vvv"
                                BackColor="#374152" Font-Bold="True" ForeColor="White" Height="37px"
                                Text="Next" Width="90px"
                                OnClick="btnnextprocess_Click" />&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
            <br />
        </div>
    </asp:Panel>

    &nbsp;  
    <p>
        &nbsp;
    </p>

    <asp:LinkButton ID="LinkButton5" runat="server"></asp:LinkButton>
    <ajaxToolkit:ModalPopupExtender ID="mpbookedsummary" runat="server"
        PopupControlID="pnlbookedsummary" TargetControlID="LinkButton5" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlbookedsummary" runat="server" Visible="true" Style="display: none; padding: 10px 20px 10px 20px;" Width="60%"
        CssClass="modal-content">
        <table border="2">
            <tr>
                <td colspan="3" align="right">
                    <asp:Button ID="btnhideboarding1" runat="server"
                        OnClick="btnhideboarding0_Click" Text="X" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="background-color: #374152">
                    <asp:Label ID="Label30" runat="server" Font-Bold="True" ForeColor="White"
                        Text="Summary"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style3" colspan="3" align="center">
                    <asp:Label ID="lblsource0" runat="server" Font-Bold="True" Font-Italic="True"
                        Font-Size="Medium"></asp:Label>
                    &nbsp;&nbsp; &nbsp; ➠&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblsource1" runat="server" Font-Bold="True" Font-Italic="True"
                                        Font-Size="Medium"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style7">
                    <asp:Label ID="lbltravellernames" runat="server" Font-Bold="True"
                        ForeColor="Black"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label33" runat="server" Font-Bold="True" ForeColor="Black"
                        Text="Blocked Seats(S)"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:Label ID="lblblockseats1" runat="server" Font-Bold="True"
                        ForeColor="White" BackColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style7">&nbsp;</td>
                <td class="style5">&nbsp;</td>
                <td class="auto-style2">
                    <asp:Button ID="btnpaynow" runat="server"
                        BackColor="Red" Font-Bold="True" ForeColor="White" Height="37px"
                        Width="120px" ValidationGroup="b" OnClick="btnpaynow_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
