<%@ Page Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true" CodeFile="Flight_Receipt.aspx.cs" Inherits="Root_Administrator_Flight_Receipt" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/custom.css?ver=13042019" rel="stylesheet" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("pnlContents");
            var printWindow = window.open('', '', 'height=400,width=800,scrollbars=1');
            printWindow.document.write('<html><head><title>Flight Receipt</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="contain">
        <div class="BannerWraper" id="pnlContents">
            <div class="content-wrapper">
                <!-- Content Header (Page header) -->
                <div class="content-header sty-one">
                    <h1>Flight Receipt</h1>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="row" id="dv_ReturnFlight" runat="server" visible="false">
                            <div class="col-6">
                                <h6 style="background-color: mediumseagreen; text-align: -webkit-auto; text-align: center">
                                    <asp:Label ID="Lbl_OutBooking" runat="server" Text="" Style="color: white"></asp:Label></h6>
                                <div class="form-horizontal" style="border: 1px solid rgba(0,0,0,.125); padding: 5px; background-color: #5cb85cad" id="Dv_OutBooking" runat="server">
                                    <div class="form-group row">
                                        <asp:Label ID="lbl_OutFlight" runat="server" class="col-sm-12 control-label"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-6">
                                <h6 style="background-color: mediumseagreen; text-align: -webkit-auto; text-align: center">
                                    <asp:Label ID="Lbl_InBooking" runat="server" Text="" Style="color: white"></asp:Label></h6>
                                <div class="form-horizontal" style="border: 1px solid rgba(0,0,0,.125); padding: 5px; background-color: aliceblue" id="Dv_InBooking" runat="server">
                                    <div class="form-group row">
                                        <asp:Label ID="Lbl_InFlight" runat="server" class="col-sm-12 control-label"></asp:Label><br />
                                        <%--  <asp:LinkButton ID="Lnk_InFlight" runat="server" Text="Click to process in booking" OnClick="Lnk_InFlight_Click" Style="padding-left: 15px"></asp:LinkButton>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card">
                            <div class="card-body">
                                <h4 style="background-color: mediumseagreen; padding: 6px; text-align: -webkit-auto;"><b style="color: white">Booking Details</b></h4>
                                <%--<asp:Label ID="Lbl_InFlight" runat="server" class="col-sm-12 control-label"></asp:Label><br />--%>

                                <div class="form-horizontal" style="border: 1px solid rgba(0,0,0,.125); padding: 5px;">
                                    <div class="form-horizontal" style="border: 1px solid rgba(0,0,0,.125); padding: 5px;">
                                        <table cellspacing="10">

                                            <tr>
                                                <td>
                                                    <b>PNR</b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_PNR" runat="server" ItemStyle-Width="100" Style="width: 200px;" class="col-sm-2 control-label" Text=""></asp:Label>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Booking Id</b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_Bookingid" runat="server" ItemStyle-Width="100" Style="width: 200px;" class="col-sm-2 control-label" Text=""></asp:Label>

                                                </td>
                                            </tr>
                                        </table>

                                    </div>
                                </div>
                                <div class="form-horizontal" style="border: 1px solid rgba(0,0,0,.125); padding: 5px;">
                                    <h5 style="background-color: #008cd3c7; padding: 6px; text-align: -webkit-auto;"><b style="color: white">Flight information:</b></h5>
                                    <div class="form-horizontal" style="border: 1px solid rgba(0,0,0,.125); padding: 5px;">
                                        <table width="100%">
                                            <tr>
                                                <td style="width:15%"><b>Flight No</b></td>
                                                <td>
                                                    <asp:Label ID="lbl_FlightNo" runat="server" class="col-sm-2 control-label" style="width:30%" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width:15%"><b>Origin</b></td>
                                                <td>
                                                    <asp:Label ID="lbl_Origin" runat="server" class="col-sm-2 control-label" style="width:30%" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width:15%"><b>Destination</b></td>
                                                <td>
                                                    <asp:Label ID="lbl_Destination" runat="server" class="col-sm-2 control-label" style="width:30%" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width:15%"><b>Dep. Date time</b></td>
                                                <td>
                                                    <asp:Label ID="lbl_DepDatetime" runat="server" class="col-sm-2 control-label" style="width:30%" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width:15%"><b>Arr. Date time</b></td>
                                                <td>
                                                    <asp:Label ID="lbl_ArrDatetime" runat="server" class="col-sm-2 control-label" style="width:30%" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width:13%"><b>Class</b></td>
                                                <td>
                                                    <asp:Label ID="lbl_Class" runat="server" class="col-sm-1 control-label" style="width:30%" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>

                                    </div>
                                    <h5 style="background-color: #008cd3c7; padding: 6px; text-align: -webkit-auto;"><b style="color: white">Passenger Details:</b></h5>
                                    <div class="form-horizontal" style="border: 1px solid rgba(0,0,0,.125); padding: 5px;">
                                        <asp:GridView ID="gv_PassengerDetails" runat="server" AutoGenerateColumns="false" CssClass="gridview">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="100" HeaderText="Passenger Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Pasengertype" runat="server" class="col-sm-1 control-label" Text='<%#Eval("PaxType").ToString()=="1" ? "Adult":"Child" %>' Visible='<%#Eval("PaxType").ToString()=="3" ? false:true %>'></asp:Label>
                                                        <asp:Label ID="lbl_PasengertypeInfant" runat="server" class="col-sm-1 control-label" Text="Infant" Visible='<%#Eval("PaxType").ToString()=="3" ? true:false %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="100" HeaderText="Name">
                                                    <ItemTemplate><%#Eval("Title")+" "+Eval("FirstName")+" "+Eval("LastName") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="100" HeaderText="Gender">
                                                    <ItemTemplate><%#Eval("Gender").ToString()=="1" ? "Male":"Female" %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="100" HeaderText="D.O.B.">
                                                    <ItemTemplate><%#Eval("DateOfBirth") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <%--                                       <asp:TemplateField HeaderText="Address">
                                            <ItemTemplate><%#Eval("CountryName") %></ItemTemplate>
                                        </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div style="border: 1px solid rgba(0,0,0,.125);">
                            <div class="card-body" style="text-align: center;">
                                <h5 style="background-color: #008cd3c7; padding: 6px; text-align: -webkit-auto;"><b style="color: white">Base Fare:</b></h5>
                                <div class="form-horizontal" style="border: 1px solid rgba(0,0,0,.125);">
                                    <div class="row">
                                        <label class="col-sm-4 control-label" style="color: green">Total Publish Fare:</label>
                                        <asp:Label ID="tbl_PublishFare" runat="server" Text="" class="col-sm-4 control-label" Style="color: green"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <div>
            <center>
                <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="return PrintPanel();" />
            </center>
        </div>
    </div>
</asp:Content>




