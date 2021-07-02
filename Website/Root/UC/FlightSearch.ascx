<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FlightSearch.ascx.cs"
    Inherits="Root_UC_FlightSearch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<link href="../flight/tabs.css" rel="stylesheet" type="text/css" />
<style>
    .text-black {
        color: white !important;
    }
    .fa-caret-up::before {
    margin-right: 25px;
}

.booking-wrap-child .btn-default {
	padding: 24px 20px !important;
}
.booking-wrap-child .fa-sort-asc {
    margin-left: -7px !important;
    right: auto !important;
    position: relative;
    top: 6px !important;
}
.booking-wrap-child .fa-sort-desc {
	right: auto !important;
	margin-left: -8px;
	margin-top: -2px;
}

</style>
<script>
    function txt_DepartDate() {

        var date = new Date();

        var dd = String(date.getDate()).padStart(2, '0');
        var mm = String(date.getMonth() + 1).padStart(2, '0');
        var yyyy = date.getFullYear();

        date = yyyy + '-' + mm + '-' + dd;

        var startDate = $('#<%=txt_DepartDate.ClientID%>').val();

        if ((Date.parse(startDate) < Date.parse(date))) {
            alert("Departure Date can not less than today date");
            $('#<%=txt_DepartDate.ClientID%>').val("");
        }
    }
    function txt_DepartdateReturn() {
        var date = new Date();
        var dd = String(date.getDate()).padStart(2, '0');
        var mm = String(date.getMonth() + 1).padStart(2, '0');
        var yyyy = date.getFullYear();
        date = yyyy + '-' + mm + '-' + dd;

        var startDate = $('#<%=txt_DepartdateReturn.ClientID%>').val();
        if ((Date.parse(startDate) < Date.parse(date))) {
            alert("Departure Date can not less than today date");
            $('#<%=txt_DepartdateReturn.ClientID%>').val("");
        }
    }
    function txt_Returndate() {

        var startDate = $('#<%=txt_DepartdateReturn.ClientID%>').val();
        var endDate = $('#<%=txt_Returndate.ClientID%>').val();

        if ((Date.parse(startDate) >= Date.parse(endDate))) {
            alert("end date should be greater then start date");
            $('#<%=txt_Returndate.ClientID%>').val("");
            $('#<%=txt_DepartdateReturn.ClientID%>').val("");
        }
    }
    <%-- function txt_Returndate_TextChanged() {
        alert("hb");
        var StartDate = document.getElementById('<%=txt_DepartdateReturn.Text %>');;
        var EndDate = document.getElementById('<%=txt_Returndate.Text %>');
        var eDate = Date(EndDate);
        var sDate = Date(StartDate);
        if (StartDate != "" && StartDate != "" && eDate < sDate) {
            alert("Departure date of 2nd segment can't be less than arrival of 1st segment")
        }
    }--%>
    //function txt_Returndate_TextChanged(sender, args)
    //{
    //    alert("ijk");
    //    var StartDate = txt_DepartdateReturn.Text;
    //var EndDate = txt_Returndate.Text;
    //var eDate = Date(EndDate);
    //var sDate = Date(StartDate);
    //if (StartDate != "" && StartDate != "" && eDate > sDate)
    //{
    //    return;
    //}

    //}
</script>



    
<div class="content-wrapper">
    <div class="content" style="background-image: linear-gradient(180deg,#051322,#15457c)">
        <div class="row" style="margin-left: 230px;">
            <div class="col-12">
                <div class="card" style="border: none; background-color: unset">
                    <div class="card-body p-b-0">
                        <h4 class="card-title text-black" color: white;>Book Flights</h4>
                   
                        <ul class="nav nav-tabs customtab2" role="tablist">
                            <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#home7" role="tab">
                                <span class="hidden-sm-up"><i class="ti-home"></i></span><span class="hidden-xs-down">One way</span></a> </li>
                            <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#profile7" role="tab">
                                <span class="hidden-sm-up"><i class="ti-user"></i></span><span class="hidden-xs-down">Return</span></a> </li>
                        </ul>
                       
                        <div class="tab-content">
                            <div class="tab-pane active" id="home7" role="tabpanel">
                                <div class="card-body">
                                    <div class="form-horizontal form-bordered">
                                        <div class="form-body">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3" style="color: white">
                                                    Depart From<span style="color: red">*</span></label>
                                                <div class="col-md-6">
                                                    <cc1:AutoCompleteExtender ServiceMethod="GetAirportList" MinimumPrefixLength="1"
                                                        CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" TargetControlID="txt_DepartFrom"
                                                        ID="AutoCompleteExtender_DepartFrom" runat="server" FirstRowSelected="false">
                                                    </cc1:AutoCompleteExtender>
                                                    <asp:TextBox ID="txt_DepartFrom" runat="server" class="form-control" placeholder="Depart From"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:RequiredFieldValidator ID="rfv_DepartFrom" runat="server" ControlToValidate="txt_DepartFrom"
                                                        ValidationGroup="OneWaySearch" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3" style="color: white">
                                                    Going To<span style="color: red">*</span></label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txt_GoingTo" runat="server" class="form-control" placeholder="Going To"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ServiceMethod="GetAirportList" MinimumPrefixLength="1"
                                                        CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" TargetControlID="txt_GoingTo"
                                                        ID="AutoCompleteExtender_GoingTo" runat="server" FirstRowSelected="false">
                                                    </cc1:AutoCompleteExtender>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:RequiredFieldValidator ID="rfv_Goingto" runat="server" ControlToValidate="txt_GoingTo"
                                                        ValidationGroup="OneWaySearch" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3" style="color: white">
                                                    Depart<span style="color: red">*</span></label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txt_DepartDate" runat="server" CssClass="form-control" autocomplete="Off"  OnChange="javascript:txt_DepartDate();" ></asp:TextBox>
                                                    <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="yyyy-MM-dd" PopupButtonID="txt_DepartDate"
                                                        TargetControlID="txt_DepartDate">
                                                    </cc1:CalendarExtender>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:RequiredFieldValidator ID="rfv_DepartDate" runat="server" ControlToValidate="txt_DepartDate"
                                                        ValidationGroup="OneWaySearch" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                               
                                                <div class="col-lg-3">
                                                     <label  style="color: white">
                                                    Adults(12+ yrs)<span style="color: red">*</span></label><br />
                                                    <div class="input-group spinner booking-wrap-child">
                                                        <input type="text" id="txt_Adults" class="form-control" value="0" runat="server"
                                                            min="0" max="9">
                                                        <div class="input-group-btn-vertical">
                                                            <button class="btn" type="button" style="border-radius: 0px 0px 0px 0px !important;">
                                                                <i class="fa fa-sort-asc"></i>
                                                            </button>
                                                            <button class="btn" style="border-radius: 0px 5px 5px 0px !important; margin: -4px; border-left: 1px solid #d2d6de;" type="button">
                                                                <i class="fa fa-sort-desc"></i>
                                                            </button>
                                                        </div>
                                                    </div>
                                                </div>
                                               
                                                <div class="col-lg-3">
                                                     <label  style="color: white">
                                                    Children (2-11 Yrs)<span style="color: red">*</span></label><br />
                                                    <div class="input-group spinner booking-wrap-child">
                                                        <input type="text" id="txt_Child" class="form-control" value="0" min="0" max="5"
                                                            runat="server">
                                                        <div class="input-group-btn-vertical">
                                                            <button class="btn" type="button" style="border-radius: 0px 0px 0px 0px !important;">
                                                                <i class="fa fa-sort-asc"></i>
                                                            </button>
                                                            <button class="btn" type="button" style="border-radius: 0px 5px 5px 0px !important; margin: -4px; border-left: 1px solid #d2d6de;">
                                                                <i class="fa fa-sort-desc"></i>
                                                            </button>
                                                        </div>
                                                    </div>
                                                </div>
                                             
                                                <div class="col-lg-3">
                                                       <label  style="color: white">
                                                    Infant (Under 2 Yrs)</label><br />
                                                    <div class="input-group spinner booking-wrap-child">
                                                        <input type="text" id="txt_Infant" class="form-control" value="0" min="0" max="4"
                                                            runat="server">
                                                        <div class="input-group-btn-vertical">
                                                            <button class="btn" type="button" style="border-radius: 0px 0px 0px 0px !important;">
                                                                <i class="fa fa-sort-asc"></i>
                                                            </button>
                                                            <button class="btn" type="button" style="border-radius: 0px 5px 5px 0px !important; margin: -4px; border-left: 1px solid #d2d6de;">
                                                                <i class="fa fa-sort-desc"></i>
                                                            </button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3" style="color: white">
                                                    Class</label>
                                                <div class="col-md-6">
                                                    <select class="form-control custom-select" id="ddl_Class" runat="server">
                                                        <option value="0" selected="selected">Any</option>
                                                        <option value="1">Economy</option>
                                                        <option value="2">PremiumEconomy</option>
                                                        <option value="3">Business</option>
                                                        <option value="5">First</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="offset-sm-3 col-sm-9">
                                                    <div class="checkbox checkbox-success">
                                                        <input id="chk_OnewayDirect" type="checkbox" runat="server">
                                                        <label for="checkbox33" style="color: white">
                                                            Show Direct flights</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3" style="color: white">
                                                    Preferred Carrier</label>
                                                <div class="col-md-6">
                                                    <input class="form-control" type="text" placeholder="Please enter only GDS Airline(s).">
                                                </div>
                                            </div>
                                         
                                        </div>
                                        <div class="form-actions">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="row">
                                                        <div class="offset-sm-3 col-md-9">
                                                            <asp:Button ID="btn_flightonewaysearch" runat="server" class="btn btn-success" Text="Flight Search"
                                                                ValidationGroup="OneWaySearch" OnClick="btn_flightonewaysearch_Click" />
                                                            <button type="button" class="btn btn-inverse">
                                                                Cancel</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                             
                                </div>
                            </div>
                            <div class="tab-pane  p-20" id="profile7" role="tabpanel">
                                <div class="card-body">
                                    <div class="form-horizontal form-bordered">
                                        <div class="form-body">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3" style="color: white">
                                                    Depart From<span style="color: red">*</span></label>
                                                <div class="col-md-6">
                                                    <cc1:AutoCompleteExtender ServiceMethod="GetAirportList" MinimumPrefixLength="1"
                                                        CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" TargetControlID="txt_DepartFromReturn"
                                                        ID="AutoCompleteExtender_DepartFromReturn" runat="server" FirstRowSelected="false">
                                                    </cc1:AutoCompleteExtender>
                                                    <asp:TextBox ID="txt_DepartFromReturn" runat="server" class="form-control" placeholder="Depart From"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:RequiredFieldValidator ID="rfv_DepartFromReturn" runat="server" ControlToValidate="txt_DepartFromReturn"
                                                        ValidationGroup="ReturnSearch" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3" style="color: white">
                                                    Going To<span style="color: red">*</span></label>
                                                <div class="col-md-6">
                                                    <cc1:AutoCompleteExtender ServiceMethod="GetAirportList" MinimumPrefixLength="1"
                                                        CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" TargetControlID="txt_GoingtoReturn"
                                                        ID="AutoCompleteExtender_GoingtoReturn" runat="server" FirstRowSelected="false">
                                                    </cc1:AutoCompleteExtender>
                                                    <asp:TextBox ID="txt_GoingtoReturn" runat="server" class="form-control" placeholder="Going To"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:RequiredFieldValidator ID="rfv_GoingtoReturn" runat="server" ControlToValidate="txt_GoingtoReturn"
                                                        ValidationGroup="ReturnSearch" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3" style="color: white">
                                                    Depart<span style="color: red">*</span></label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txt_DepartdateReturn" runat="server" CssClass="form-control" autocomplete="Off"  OnChange="javascript:txt_DepartdateReturn();" ></asp:TextBox>
                                                    <cc1:CalendarExtender runat="server" ID="Ce_DepartdateReturn" Format="yyyy-MM-dd" PopupButtonID="txt_DepartdateReturn"
                                                        TargetControlID="txt_DepartdateReturn">
                                                    </cc1:CalendarExtender>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:RequiredFieldValidator ID="rfv_DepartdateReturn" runat="server" ControlToValidate="txt_DepartdateReturn"
                                                        ValidationGroup="ReturnSearch" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3" style="color: white">
                                                    Return<span style="color: red">*</span></label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txt_Returndate" runat="server"   CssClass ="form-control" autocomplete="Off"  OnChange="javascript:txt_Returndate();"></asp:TextBox>
                                                    <cc1:CalendarExtender runat="server" ID="Ce_Returndate" Format="yyyy-MM-dd" PopupButtonID="txt_Returndate"
                                                        TargetControlID="txt_Returndate">
                                                    </cc1:CalendarExtender>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:RequiredFieldValidator ID="rfv_Returndate" runat="server" ControlToValidate="txt_Returndate"
                                                        ValidationGroup="ReturnSearch" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                               
                                                <div class="col-lg-3">
                                                     <label style="color: white">
                                                    Adults(12+ yrs)<span style="color: red">*</span></label>
                                                    <div class="input-group spinner booking-wrap-child">
                                                        <input type="text" id="txt_AdultsReturn" class="form-control" value="0" runat="server"
                                                            min="0" max="9">
                                                        <div class="input-group-btn-vertical">
                                                            <button class="btn" type="button" style="border-radius: 0px 0px 0px 0px !important;">
                                                                      <i class="fa fa-sort-asc"></i>
                                                            </button>
                                                            <button class="btn" type="button" style="border-radius: 0px 5px 5px 0px !important; margin: -4px; border-left: 1px solid #d2d6de;">
                                                          <i class="fa fa-sort-desc"></i>
                                                            </button>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-3">
                                                     <label  style="color: white">
                                                    Children (2-11 Yrs)<span style="color: red">*</span></label>
                                                    <div class="input-group spinner booking-wrap-child">
                                                        <input type="text" id="txt_ChildReturn" class="form-control" value="0" min="0" max="5"
                                                            runat="server">
                                                        <div class="input-group-btn-vertical">
                                                            <button class="btn" type="button" style="border-radius: 0px 0px 0px 0px !important;">
                                                                      <i class="fa fa-sort-asc"></i>
                                                            </button>
                                                            <button class="btn" type="button" style="border-radius: 0px 5px 5px 0px !important; margin: -4px; border-left: 1px solid #d2d6de;">
                                                                <i class="fa fa-sort-desc"></i>
                                                            </button>
                                                        </div>
                                                    </div>
                                                </div>
                                           
                                                <div class="col-lg-3">
                                                         <label  style="color: white">
                                                    Infant (Under 2 Yrs)</label>
                                                    <div class="input-group spinner booking-wrap-child">
                                                        <input type="text" id="txt_InfantReturn" class="form-control" value="0" min="0" max="4"
                                                            runat="server">
                                                        <div class="input-group-btn-vertical">
                                                            <button class="btn" type="button" style="border-radius: 0px 0px 0px 0px !important;">
                                                                        <i class="fa fa-sort-asc"></i>
                                                            </button>
                                                            <button class="btn" type="button" style="border-radius: 0px 5px 5px 0px !important; margin: -4px; border-left: 1px solid #d2d6de;">
                                                               <i class="fa fa-sort-desc"></i>
                                                            </button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3" style="color: white">
                                                    Class</label>
                                                <div class="col-md-6">
                                                    <select class="form-control custom-select" id="ddl_ClassReturn" runat="server">
                                                        <option value="0" selected="selected">Any</option>
                                                        <option value="1">Economy</option>
                                                        <option value="2">PremiumEconomy</option>
                                                        <option value="3">Business</option>
                                                        <option value="5">First</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="offset-sm-3 col-sm-9">
                                                    <div class="checkbox checkbox-success">
                                                        <input id="chk_DirectReturn" type="checkbox">
                                                        <label for="checkbox33" style="color: white">
                                                            Show Direct flights</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3" style="color: white">
                                                    Preferred Carrier</label>
                                                <div class="col-md-6">
                                                    <input class="form-control" type="text" placeholder="Please enter only GDS Airline(s).">
                                                </div>
                                            </div>
                                    
                                        </div>
                                        <div class="form-actions">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="row">
                                                        <div class="offset-sm-3 col-md-9">
                                                            <asp:Button ID="btn_flightreturnsearch" runat="server" class="btn btn-success" Text="Flight Search"
                                                                ValidationGroup="ReturnSearch" OnClick="btn_flightreturnsearch_Click" />
                                                            <button type="button" class="btn btn-inverse">
                                                                Cancel</button>
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
                </div>
            </div>
        </div>
    </div>
</div>

                   
<script type="text/javascript" src="../js/jquery.min.js"></script>
<script>
    $('.spinner .btn:first-of-type').on('click', function () {
        var btn = $(this);
        var input = btn.closest('.spinner').find('input');
        if (input.attr('max') == undefined || parseInt(input.val()) < parseInt(input.attr('max'))) {
            input.val(parseInt(input.val(), 10) + 1);
            var txtvalue = (parseInt(input.val(), 10) + 1) - 1;
            if (input.attr('id') == "txt_Adults") {
                $('#txt_Adults').val(txtvalue);
            } else if (input.attr('id') == "txtChild") { $('#Child').val(txtvalue); } else if (input.attr('id') == "txtInfant") { $('#Infant').val(txtvalue); }
        }
    });
    $('.spinner .btn:last-of-type').on('click', function () {
        var btn = $(this);
        var input = btn.closest('.spinner').find('input');
        if (input.attr('min') == undefined || parseInt(input.val()) > parseInt(input.attr('min'))) {
            input.val(parseInt(input.val(), 10) - 1);
            var txtvalue = (parseInt(input.val(), 10) - 1) + 1;
            if (input.attr('id') == "txt_Adults") {
                $('#txt_Adults').val(txtvalue);
            } else if (input.attr('id') == "txtChild") { $('#Child').val(txtvalue); } else if (input.attr('id') == "txtInfant") { $('#Infant').val(txtvalue); }
        }
    });

    $('#btn_flightonewaysearch').click(function () {
        if ($('#txt_Adults').val == 0 && $('#txt_Child').val == 0) {
            alert("Please select one adults or one child")
            return false;
        }
        else
            return true;
    })

    //$("#txt_Returndate").change(function () {
    //    alert("juh");
    //    var startDate = document.getElementById("txt_DepartdateReturn").value;
    //    var endDate = document.getElementById("txt_Returndate").value;
    //    if ((Date.parse(startDate) >= Date.parse(endDate))) {
    //        alert("end date should be greater then start date");
    //        document.getElementById("txt_Returndate").value = "";
    //    }
    //});
</script>
