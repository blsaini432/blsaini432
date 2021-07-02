<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FlightPassengerDetails.ascx.cs" Inherits="Root_UC_FlightPassengerDetails" %>
<link href="../flight/custom.css" rel="stylesheet" />
<!-- Content Wrapper. Contains page content -->
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
 
<div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <div class="content-header sty-one">
        <div class="step-app">
            <ul>
                <li> <img src="../flight/Images/flight/6E.gif" runat="server" id="img_Airline" style="position: unset; padding: 12px; width: 40px;" />
                  </li>
                <li>
                     <asp:Label ID="lbl_Airlie" runat="server" Text="Indigo 6E - 665-L"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lbl_AirlineOrigin" runat="server" Text=""></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lbl_AirlineDesignation" runat="server" Text=""></asp:Label></li>
                <li>
                    <asp:Label ID="lbl_AirlineDuration" runat="server" Text=""></asp:Label></li>
                 <li>
                    <asp:Label ID="lbl_IsLcc" runat="server" Text=""></asp:Label></li>
            </ul>
            
        </div>

        <ol class="breadcrumb">
            <li><a href="FlightSearch.aspx">Flight Search</a></li>
            <li><a href="FlightList.aspx"><i class="fa fa-angle-right"></i>Flight List</li>
            <li><a href="#" style="color: green"><i class="fa fa-angle-right"></i>Passenger Details</a></li>
        </ol>
    </div>
   
    <div class="content" style="margin-top: 49px;">
        <div class="row" style="margin-left: 50px;">
            <div class="col-lg-8">
                <div class="row" id="dv_ReturnFlight" runat="server" visible="false">
                    <div class="col-6">
                        <h6 style="background-color: mediumseagreen; text-align: -webkit-auto; text-align: center"><b style="color: white">Out Booking</b>
                                <asp:Label ID="Lbl_OutBooking" runat="server" Text="" Style="color: white"></asp:Label></h6>
                     
                        <div class="form-horizontal" style="border: 1px solid rgba(0,0,0,.125); padding: 5px; background-color: #5cb85cad" id="Dv_OutBooking" runat="server">
                            <div class="form-group row">
                                <asp:Label ID="lbl_OutFlight" runat="server" class="col-sm-12 control-label"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-6">
                        <h6 style="background-color: mediumseagreen; text-align: -webkit-auto; text-align: center"><b style="color: white">In Booking</b>

                                   <asp:Label ID="Lbl_InBooking" runat="server" Text="" Style="color: white"></asp:Label>
                      
                        </h6>
                        <div class="form-horizontal" style="border: 1px solid rgba(0,0,0,.125); padding: 5px; background-color: aliceblue" id="Dv_InBooking" runat="server">
                            <div class="form-group row">
                                <asp:Label ID="Lbl_InFlight" runat="server" class="col-sm-12 control-label"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card">
                    <div class="card-body">
                        <h4 style="background-color: mediumseagreen; padding: 6px; text-align: -webkit-auto;"><b style="color: white">Enter Passenger Details</b></h4>
                        <p style="color: red;">(Please add correct details of the passenger as mentioned in ID Proof with mobile number so that Airline can inform them in case of any change in the flight timing.)</p>
                        <div class="form-horizontal" style="border: 1px solid rgba(0,0,0,.125); padding: 5px;">
                            <div class="form-group row">
                                <label class="col-sm-3">Passenger Type: <span style="color: red">*</span></label>
                                <div class="col-sm-6">
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="ti-user"></i></div>
                                        <asp:DropDownList ID="ddl_PassengerType" runat="server" class="form-control custom-select" OnSelectedIndexChanged="ddl_PassengerType_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Adults"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Children"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Infant"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <asp:RequiredFieldValidator ID="rfv_PassengerType" runat="server" ControlToValidate="ddl_PassengerType" InitialValue="0" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Passanger"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3 control-label">First Name: <span style="color: red">*</span></label>
                                <div class="col-sm-2">
                                    <asp:DropDownList ID="ddl_Title" runat="server" class="form-control custom-select">
                                        <asp:ListItem Value="0" Text="Title"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Mr"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Ms"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Mrs"></asp:ListItem>
                                        <%--<asp:ListItem Value="4" Text="Miss"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="Mstr"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="ti-user"></i></div>
                                        <asp:TextBox ID="txt_FirstName" runat="server" class="form-control" placeholder="First Name"></asp:TextBox>
                                    </div>
                                </div>
                                <asp:RequiredFieldValidator ID="rfv_Title" runat="server" ControlToValidate="ddl_Title" InitialValue="0" ErrorMessage="Required Title" ForeColor="Red" ValidationGroup="Passanger"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfv_FirstName" runat="server" ControlToValidate="txt_FirstName" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Passanger"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3 control-label">Last Name : <span style="color: red">*</span></label>
                                <div class="col-sm-6">
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="ti-user"></i></div>
                                        <asp:TextBox ID="txt_LastName" runat="server" AutoPostBack="true" class="form-control" OnTextChanged="txt_LastName_TextChanged" placeholder="Last Name"></asp:TextBox>
                                    </div>
                                </div>
                                <asp:RequiredFieldValidator ID="rfv_LastName" runat="server" ControlToValidate="txt_LastName" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Passanger"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3">Gender : <span style="color: red">*</span></label>
                                <div class="col-sm-6">
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="ti-user"></i></div>
                                        <asp:DropDownList ID="ddl_Gender" runat="server" class="form-control custom-select">
                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Male"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Female"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <asp:RequiredFieldValidator ID="rfv_Gender" runat="server" ControlToValidate="ddl_Gender" InitialValue="0" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Passanger"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group row" id="dv_Mobile" runat="server">
                                <label class="col-sm-3 control-label">Mobile : <span style="color: red">*</span></label>
                                <div class="col-sm-6">
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="ti-mobile"></i></div>
                                        <asp:TextBox ID="txt_Mobile" runat="server" class="form-control" MaxLength="10" placeholder="Enter Mobile"></asp:TextBox>
                                    </div>
                                </div>
                                <asp:RequiredFieldValidator ID="rfv_Mobile" runat="server" ControlToValidate="txt_Mobile" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Passanger"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="ref_Mobile" runat="server" ErrorMessage="Enter Correct Number" ForeColor="Red" ValidationGroup="Passanger" ControlToValidate="txt_Mobile" ValidationExpression="^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$" ></asp:RegularExpressionValidator>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3 control-label">D.O.B :</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txt_Dob" runat="server" CssClass="form-control" autocomplete="Off"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="yyyy-MM-dd" PopupButtonID="txt_Dob"
                                        TargetControlID="txt_Dob">
                                    </cc1:CalendarExtender>
                                </div>
                                <asp:RequiredFieldValidator ID="rfv_Dob" runat="server" ControlToValidate="txt_Dob" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Passanger"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3 control-label">Address: <span style="color: red">*</span></label>
                                <div class="col-sm-6">
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="ti-location-pin"></i></div>
                                        <asp:TextBox ID="txt_Address" runat="server" placeholder="Address" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <asp:RequiredFieldValidator ID="rfv_Address" runat="server" ControlToValidate="txt_Address" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Passanger"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group row" id="dv_Email" runat="server">
                                <label class="col-sm-3 control-label">Email: <span style="color: red">*</span></label>
                                <div class="col-sm-6">
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="ti-email"></i></div>
                                        <asp:TextBox ID="txt_Email" runat="server" placeholder="Enter email" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <asp:RequiredFieldValidator ID="rfv_Email" runat="server" ControlToValidate="txt_Email" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Passanger"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rgex_Email" runat="server" ControlToValidate="txt_Email" ErrorMessage="Invalid Email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Passanger"></asp:RegularExpressionValidator>
                            </div>
                            <div class="form-group row">
                                <label for="web" class="col-sm-3 control-label">Country :<span style="color: red">*</span></label>
                                <div class="col-sm-6">
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="ti-location-pin"></i></div>
                                        <asp:DropDownList ID="ddl_Country" runat="server" class="form-control custom-select" OnSelectedIndexChanged="ddl_Country_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                     
                                    </div>
                                </div>
                                   <asp:RequiredFieldValidator ID="rfv_Country" runat="server" ControlToValidate="ddl_Country" InitialValue="0" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Passanger"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group row">
                                <label for="" class="col-sm-3 control-label">City :<span style="color: red">*</span></label>
                                <div class="col-sm-6">
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="ti-location-pin"></i></div>
                                        <asp:DropDownList ID="ddl_City" runat="server" class="form-control custom-select">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-horizontal" id="dv_Passport" runat="server" style="border: 1px solid rgba(0,0,0,.125); padding: 5px;">
                                <div class="form-group row">
                                    <label class="col-sm-3 control-label">Passport No. : </label>
                                    <div class="col-sm-6">
                                        <div class="input-group">
                                            <div class="input-group-addon"><i class="ti-user"></i></div>
                                            <asp:TextBox ID="txt_PassportNo" runat="server" class="form-control" placeholder="Passport No."></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-3 control-label">Passport Exp :</label>
                                    <div class="col-md-6">
                                        <div class="input-group">
                                        <asp:TextBox ID="txt_PassportExp" runat="server" CssClass="form-control" autocomplete="Off"></asp:TextBox>
                                        <cc1:CalendarExtender runat="server" ID="Calendarextender1" Format="yyyy-MM-dd" PopupButtonID="txt_PassportExp"
                                            TargetControlID="txt_PassportExp">
                                        </cc1:CalendarExtender>
                                            </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-sm-9">
                                    <div class="checkbox checkbox-success">
                                        <asp:CheckBox ID="chk_GstDetail" runat="server" OnCheckedChanged="chk_GstDetail_CheckedChanged" AutoPostBack="true" />
                                        <label for="checkbox33" >GST Detail <span style="color: red"><%--(Note : Please fill GST Details only for corporate customer)--%></span></label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-horizontal" id="dv_GstDetail" runat="server" visible="false" style="border: 1px solid rgba(0,0,0,.125); padding: 5px;">
                                <div class="form-group row">
                                    <label class="col-sm-2 control-label">GST Number :</label>
                                    <div class="col-sm-3">
                                        <div class="input-group">
                                            <asp:TextBox ID="txt_GstNumber" runat="server" class="form-control" placeholder="GST Number"></asp:TextBox>
                                        </div>
                                       
                                        <asp:RequiredFieldValidator ID="rfv_GstNumber" runat="server" ControlToValidate="txt_Address" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Passanger"></asp:RequiredFieldValidator>
                                    </div>
                                    <label class="col-sm-3 control-label">GST Company Name :</label>
                                    <div class="col-sm-3">
                                        <div class="input-group">
                                            <asp:TextBox ID="txt_GstCompanyName" runat="server" class="form-control" placeholder="GST Company Name"></asp:TextBox>
                                        </div>
                                        <asp:RequiredFieldValidator ID="rfv_GstCompanyName" runat="server" ControlToValidate="txt_Address" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Passanger" ></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="" class="col-sm-2 control-label">GST Company Contact No:</label>
                                    <div class="col-sm-3">
                                        <div class="input-group">
                                            <asp:TextBox ID="txt_GstCompanyContact" runat="server" class="form-control" placeholder="GST Company Contact No"></asp:TextBox>
                        
                                        </div>
                                                            <asp:RequiredFieldValidator ID="rfv_GstCompanyContact" runat="server" ControlToValidate="txt_GstCompanyContact" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Passanger"></asp:RequiredFieldValidator>
                                             <asp:RegularExpressionValidator ID="rev_GstCompanyContact" runat="server" ErrorMessage="Enter Correct Number" ForeColor="Red" ValidationGroup="Passanger" ControlToValidate="txt_GstCompanyContact" ValidationExpression="^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$" ></asp:RegularExpressionValidator> 
  
                                    </div>
                                    <label for="" class="col-sm-3 control-label">GST Company Address :</label>
                                    <div class="col-sm-3">
                                        <div class="input-group">
                                            <asp:TextBox ID="txt_GstCompanyAddress" runat="server" class="form-control" placeholder="GST Company Address"></asp:TextBox>
                                        </div>
                                        <asp:RequiredFieldValidator ID="rfv_GstCompanyAddress" runat="server" ControlToValidate="txt_Address" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Passanger" ></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="" class="col-sm-3 control-label">GST Company Email :</label>
                                    <div class="col-sm-3">
                                        <div class="input-group">
                                            <asp:TextBox ID="txt_GstCompanyEmail" runat="server" class="form-control" placeholder="GST Company Email"></asp:TextBox>
                                        </div>
                                        <asp:RequiredFieldValidator ID="rfv_GstCompanyEmail" runat="server" ControlToValidate="txt_Address" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Passanger" ></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-sm-9">
                                    <div class="checkbox checkbox-success">
                                        <asp:CheckBox ID="chk_PushBookingRoamer" runat="server" OnCheckedChanged="chk_PushBookingRoamer_CheckedChanged" AutoPostBack="true" />
                                        <label for="">Push Booking to Roamer</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-horizontal" id="dv_PushBookingRoamer" runat="server" visible="false" style="border: 1px solid rgba(0,0,0,.125); padding: 5px;">
                                <div class="form-group row">
                                    <label for="" class="col-sm-3 control-label">Customer Mobile No:</label>
                                    <div class="col-sm-3">
                                        <div class="input-group">
                                            <asp:TextBox ID="txt_CustmerMobile" runat="server" class="form-control" placeholder="Customer Mobile No"></asp:TextBox>
                                        </div>
                                    </div>
                                            <asp:RegularExpressionValidator ID="rev_CustomerNumber" runat="server" ErrorMessage="Enter Correct Number" ForeColor="Red" ValidationGroup="Passanger" ControlToValidate="txt_CustmerMobile" ValidationExpression="^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$" ></asp:RegularExpressionValidator> 
  
                                </div>
                            </div>
                            <h5 style="background-color: #008cd3c7; padding: 6px; text-align: -webkit-auto;"><b style="color: white">SSR Details:</b></h5>
                            <div class="form-horizontal" style="border: 1px solid rgba(0,0,0,.125); padding: 5px;">
                                <b>Baggage Details:</b>
                                <asp:Label ID="lbl_BaggageDetails" runat="server" CssClass="gridview"></asp:Label>
                            </div>
                           <div class="form-group row" style="padding: 5px;" id="dv_ExcessBaggageLcc" runat="server" visible="false">
                                <label for="" class="col-sm-6 control-label">
                                    Select Excess Baggage(Extra charges will be applicable) :</label>
                                <div class="col-sm-6">
                                    <div class="input-group">
                                        <asp:DropDownList ID="ddl_ExcessBaggageLcc" runat="server" class="form-control custom-select">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                             <div class="form-group row" style="padding: 5px;" id="dv_SeatPreferences" runat="server" visible="false">
                                <label for="" class="col-sm-6 control-label">
                                    Seat Preferences :</label>
                                <div class="col-sm-6">
                                    <div class="input-group">
                                        <asp:DropDownList ID="ddl_SeatPreferences" runat="server" class="form-control custom-select">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row" style="padding: 5px;" id="dv_MealPerferencesLcc" runat="server" visible="false">
                                <label for="" class="col-sm-6 control-label">
                                    Meal Preferences :</label>
                                <div class="col-sm-6">
                                    <div class="input-group">
                                        <asp:DropDownList ID="ddl_MealPerferencesLcc" runat="server" class="form-control custom-select">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <b style="color: red">Note : Meal/Seat preferences subject to availability.</b>
                            <br />
                            <b>Fare Rule:</b>
                            <div class="form-horizontal" style="border: 1px solid rgba(0,0,0,.125); padding: 5px;">
                                <div style="overflow-y: scroll; max-height: 100px;">
                                    <asp:Label ID="lbl_FareRules" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-sm-9">
                                    <div class="checkbox checkbox-success">
                                        <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="chk_PushBookingRoamer_CheckedChanged" AutoPostBack="true" />
                                        <label for="">No Assistance and Insurance Required</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-sm-12">
                                    <div class="checkbox checkbox-success">
                                        <asp:CheckBox ID="CheckBox2" runat="server" OnCheckedChanged="chk_PushBookingRoamer_CheckedChanged" AutoPostBack="true" />
                                        <label for="" class="col-sm-12 control-label" style="margin-top: -24px;">
                                            Add Travel Assistance and Insurance for only Rs. 161.00 per Passenger (Terms & Conditions)
Domestic Travel Assistance and Insurance is valid from 14 Apr 2019 to 14 Apr 2019 from your date of journey or till your date of return, which ever is earlier. To know more on the coverage, please click here.</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" style="padding: 5px;">
                            <div class="row">
                                <div class="col-sm-8">
                                    <asp:Button ID="btn_AddPaasengerDetail" runat="server" OnClick="btn_AddPaasengerDetail_Click" Text="Add" class="btn btn-success waves-effect waves-light" ValidationGroup="Passanger" />
                                </div>
                                <div class="col-sm-4" style="visibility: hidden;">
                                    <asp:Button ID="btn_ProceedBooking" runat="server" Text=" Proceed to Booking Review" class="btn btn-success" OnClick="btn_ProceedBooking_Click" />

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
                    <asp:DataList ID="dtlist_SaleSummary" runat="server" Width="100%" RepeatDirection="Vertical">
                        <HeaderTemplate></HeaderTemplate>
                        <ItemTemplate>
                            <div class="form-horizontal" style="border: 1px solid rgba(0,0,0,.125);">
                                <div class="row">
                                    <asp:Label ID="lbl_Pasengertype" runat="server" Text='<%#Eval("PaxType").ToString()=="1" ? "Adult"+"*"+Eval("PxnCount"):"Child" +"*"+Eval("PxnCount") %>' Visible='<%#Eval("PaxType").ToString()=="3" ? false:true %>' class="col-sm-4 control-label"></asp:Label>
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
                    <div class="form-horizontal" style="border: 1px solid rgba(0,0,0,.125); visibility: hidden;">
                        <div class="row" >
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
