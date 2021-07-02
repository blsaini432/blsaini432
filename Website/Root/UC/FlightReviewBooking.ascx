<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FlightReviewBooking.ascx.cs" Inherits="Root_UC_FlightReviewBooking" %>
<link href="../../flight/custom.css" rel="stylesheet" />

<div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <div class="content-header sty-one">
        <h1>Review Booking</h1>
        <ol class="breadcrumb">
            <li><a href="FlightSearch.aspx">Flight Search</a></li>
            <li><a href="FlightList.aspx"><i class="fa fa-angle-right"></i>Flight List</a></li>
            <li><a href="FlightReviewBooking.aspx"><i class="fa fa-angle-right"></i>Passenger Details</a></li>
            <li><a href="#" style="color: green"><i class="fa fa-angle-right"></i>Review Booking</a></li>
        </ol>
    </div>
    <div class="content">
        <div class="row" style="margin-left: 50px;">
            <div class="col-lg-8">
                <div class="row" id="dv_ReturnFlight" runat="server" visible="false">
                    <div class="col-6">
                        <h6 style="background-color: mediumseagreen; text-align: -webkit-auto; text-align: center"><b style="color: white">Out Booking</b></h6>
                        <div class="form-horizontal" style="border: 1px solid rgba(0,0,0,.125); padding: 5px; background-color: #5cb85cad">
                            <div class="form-group row">
                                <asp:Label ID="lbl_OutFlight" runat="server" class="col-sm-12 control-label"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-6">
                        <h6 style="background-color: mediumseagreen; text-align: -webkit-auto; text-align: center"><b style="color: white">In Booking</b></h6>
                        <div class="form-horizontal" style="border: 1px solid rgba(0,0,0,.125); padding: 5px; background-color: aliceblue" id="Dv_InBooking" runat="server">
                            <div class="form-group row">
                                <asp:Label ID="Lbl_InFlight" runat="server" class="col-sm-12 control-label"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card">
                    <div class="card-body">
                        <h4 style="background-color: mediumseagreen; padding: 6px; text-align: -webkit-auto;"><b style="color: white">Booking Details</b></h4>
                        <div class="form-horizontal" style="border: 1px solid rgba(0,0,0,.125); padding: 5px;">
                            <h5 style="background-color: #008cd3c7; padding: 6px; text-align: -webkit-auto;"><b style="color: white">Flight information:</b></h5>
                            <div class="form-horizontal" style="border: 1px solid rgba(0,0,0,.125); padding: 5px;">
                                <div class="form-group row">
                                    <label class="col-sm-2 control-label">Flight No</label>
                                    <label class="col-sm-2 control-label">Origin</label>
                                    <label class="col-sm-2 control-label">Destination</label>
                                    <label class="col-sm-2 control-label">Dep. Date time</label>
                                    <label class="col-sm-2 control-label">Arr. Date time</label>
                                    <label class="col-sm-2 control-label">Class</label>
                                </div>
                                <div class="form-group row">
                                    <asp:Label ID="lbl_FlightNo" runat="server" class="col-sm-2 control-label" Text=""></asp:Label>
                                    <asp:Label ID="lbl_Origin" runat="server" class="col-sm-2 control-label" Text=""></asp:Label>
                                    <asp:Label ID="lbl_Destination" runat="server" class="col-sm-2 control-label" Text=""></asp:Label>
                                    <asp:Label ID="lbl_DepDatetime" runat="server" class="col-sm-2 control-label" Text=""></asp:Label>
                                    <asp:Label ID="lbl_ArrDatetime" runat="server" class="col-sm-2 control-label" Text=""></asp:Label>
                                    <asp:Label ID="lbl_Class" runat="server" class="col-sm-2 control-label" Text=""></asp:Label>
                                </div>
                            </div>
                            <h5 style="background-color: #008cd3c7; padding: 6px; text-align: -webkit-auto;"><b style="color: white">Passenger Details:</b></h5>
                            <div class="form-horizontal" style="border: 1px solid rgba(0,0,0,.125); padding: 5px;">
                                <asp:GridView ID="gv_PassengerDetails" runat="server" AutoGenerateColumns="false" CssClass="gridview">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Passenger Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Pasengertype" runat="server" Text='<%#Eval("PaxType").ToString()=="1" ? "Adult":"Child" %>' Visible='<%#Eval("PaxType").ToString()=="3" ? false:true %>'></asp:Label>
                                                <asp:Label ID="lbl_PasengertypeInfant" runat="server" Text="Infant" Visible='<%#Eval("PaxType").ToString()=="3" ? true:false %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate><%#Eval("Title")+" "+Eval("FirstName")+" "+Eval("LastName") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gender">
                                            <ItemTemplate><%#Eval("Gender").ToString()=="1" ? "Male":"Female" %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="D.O.B.">
                                            <ItemTemplate><%#Eval("DateOfBirth") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address">
                                            <ItemTemplate><%#Eval("CountryName") %></ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <b>Fare Rule:</b>
                            <div class="form-horizontal" style="border: 1px solid rgba(0,0,0,.125); padding: 5px;">
                                <div style="overflow-y: scroll; max-height: 100px;">
                                    <asp:Label ID="lbl_FareRules" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-sm-12">
                                    <div class="checkbox checkbox-success">
                                        <asp:CheckBox ID="chk_Tearms" runat="server" AutoPostBack="true" />
                                        <label style="color: red">I have reviewed and agreed on the fares and commission offered for this booking.</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" style="padding: 5px;">
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Button ID="btn_Getticket" runat="server" Text=" Get Ticket" class="btn btn-success" UseSubmitBehavior="true" OnClick="btn_Getticket_Click" />
                                </div>
                                <div class="col-md-4">
                                    <asp:Button ID="btn_cancelbook" runat="server" Text="Cancel Booking" class="btn btn-success" UseSubmitBehavior="true" OnClick="btn_cancelbook_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4" style="border: 1px solid rgba(0,0,0,.125);">
                <div class="card-body" style="text-align: center;">
                    <h5 style="background-color: mediumseagreen; padding: 10px;"><b style="color: white;">Sale Summary</b></h5>
                    <asp:DataList ID="dtlist_AirlineSummary" runat="server" Width="100%" RepeatDirection="Vertical">
                        <HeaderTemplate></HeaderTemplate>
                        <ItemTemplate>
                            <div class="form-horizontal" style="border: 1px solid rgba(0,0,0,.125);">
                                <div class="row">
                                    <label for="" class="col-sm-5 control-label">Date: </label>
                                    <div class="col-sm-5">
                                        <asp:Label ID="lbl_JournyDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <label class="col-sm-5 control-label">Flight Number: </label>
                                    <div class="col-sm-5">
                                        <asp:Label ID="lbl_FlightNumber" runat="server" Text='<%#Eval("FlightNumber")%>'></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <label for="" class="col-sm-5 control-label">Dept: </label>
                                    <div class="col-sm-5">
                                        <asp:Label ID="lbl_Dept" runat="server" Text='<%#Eval("Dept")%>'></asp:Label>
                                        <asp:Label ID="lbl_DeptTime" runat="server" Text='<%#"@"+Eval("DeptTime")%>'></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <label class="col-sm-5 control-label">Arr: </label>
                                    <div class="col-sm-5">
                                        <asp:Label ID="lbl_Arr" runat="server" Text='<%#Eval("Arr")%>'></asp:Label>
                                        <asp:Label ID="lbl_ArrTime" runat="server" Text='<%#"@"+Eval("ArrTime")%>'></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                    <h5 style="background-color: #008cd3c7; padding: 6px; text-align: -webkit-auto;"><b style="color: white">Base Fare:</b></h5>
                    <div class="form-horizontal" style="border: 1px solid rgba(0,0,0,.125);">
                        <div class="row">
                            <label class="col-sm-4 control-label" style="color: green">Total Publish Fare:</label>
                            <asp:Label ID="tbl_PublishFare" runat="server" Text="" class="col-sm-4 control-label" Style="color: green"></asp:Label>
                        </div>
                    </div>
                    <asp:DataList ID="dtlist_SaleSummary" runat="server" Width="100%" RepeatDirection="Vertical" >
                        <HeaderTemplate></HeaderTemplate>
                        <ItemTemplate>
                            <div class="form-horizontal" style="border: 1px solid rgba(0,0,0,.125); visibility: hidden;">
                                <div class="row">
                                    <asp:Label ID="lbl_Pasengertype" runat="server" Text='<%#Eval("PaxType").ToString()=="1" ? "Adult":"Child" +"*"+Eval("PxnCount") %>' Visible='<%#Eval("PaxType").ToString()=="3" ? false:true %>' class="col-sm-4 control-label"></asp:Label>
                                    <asp:Label ID="lbl_PasengertypeInfant" runat="server" Text='<%#"Infant*"+Eval("PxnCount") %>' Visible='<%#Eval("PaxType").ToString()=="3" ? true:false %>' class="col-sm-4 control-label"></asp:Label>
                                    <asp:Label ID="lbl_AdultPublishedPrice" runat="server" Text='<%#Eval("BasePublishedPrice") %>' class="col-sm-4 control-label"></asp:Label>
                                </div>
                                <div class="row">
                                    <label class="col-sm-4 control-label">OT Tax :</label>
                                    <asp:Label ID="lbl_PublishedOTTax" runat="server" Text='<%#Eval("PublishedOTTax") %>' class="col-sm-4 control-label"></asp:Label>
                                </div>
                                <div class="row">
                                    <label class="col-sm-4 control-label">YQ Tax :</label>
                                    <asp:Label ID="lbl_PublishedYQTax" runat="server" Text='<%#Eval("PublishedYQTax") %>' class="col-sm-4 control-label"></asp:Label>
                                </div>
                                <div class="row">
                                    <label class="col-sm-5 control-label">T. Fee and S.Charges :</label>
                                    <asp:Label ID="lbl_PublishedTS" runat="server" Text='<%#Eval("PublishTS") %>' class="col-sm-2 control-label"></asp:Label>
                                </div>
                                <div class="row">
                                    <label class="col-sm-4 control-label"><b>Total Fare :</b></label>
                                    <asp:Label ID="lbl_TotalPubPrice" runat="server" Text='<%#Convert.ToDecimal(Eval("BasePublishedPrice"))+Convert.ToDecimal(Eval("PublishedOTTax"))+Convert.ToDecimal(Eval("PublishedYQTax"))+Convert.ToDecimal(Eval("PublishTS")) %>' class="col-sm-4 control-label" Style="font-weight: bold"></asp:Label>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                     
                    <div class="form-horizontal" style="border: 1px solid rgba(0,0,0,.125); visibility: hidden">
                        <div class="row">
                            <label class="col-sm-4 control-label">Discount :</label>
                            <asp:Label ID="lbl_Discount" runat="server" Text="" class="col-sm-4 control-label"></asp:Label>
                        </div>
                        <div class="row">
                            <label class="col-sm-4 control-label" style="color: green">Total Payable:</label>
                            <asp:Label ID="lbl_TotalPayable" runat="server" Text="" class="col-sm-4 control-label" Style="color: green"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
