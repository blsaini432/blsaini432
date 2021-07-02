
function getTabName(s) {
    document.getElementById('ctl00_ContentPlaceHolder1_hf_tab').value = s;
}

function ravi(song) {

    //Find the audio control on the page
    var audio = document.getElementById('rrvv');
    //songNames holds the comma separated name of songs
    var songNames = song;
    var lstsongNames = songNames.split(',');
    var curPlaying = 0;
    var urls = audio.getElementsByTagName('source');
    urls[0].src = lstsongNames[0];
    audio.load();
    //Plays the audio song
    audio.play(); audio.play();
}
function hlme(a) {
    a.setAttribute("style", "font-size:20px; background:#000; color:#fff");
}
function dhlme(a) {
    a.setAttribute("style", "");
}

function myFunction2() {
    if ($("#ddlCirclePostpaid").val() == 0) {
        GetPostpaid();
    }
}
function GetPostpaid() {
    $.ajax({
        type: "POST",
        url: "DashBoard.aspx/GetPostpaid",
        data: '{searchTerm: "' + $("#txtNumberPostpaid").val() + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var str = response.d.split(',');
            $("#ddlOperatorPostpaid").val(str[0]);
            $("#ddlCirclePostpaid").val(str[1]);
            $('#ddlOperatorPostpaid').trigger("chosen:updated");
            $('#ddlCirclePostpaid').trigger("chosen:updated");
        },
        failure: function (response) {
            //alert(response.d);
        },
        error: function (response) {
            //alert(response.d);
        }
    });
}
/*function GetTariffPlans() {
    var operId = $("#ddlOperatorPrepaid option:selected").val();
    var cirId = $("#ddlCirclePrepaid option:selected").val();

    if ((operId != '0') && (cirId != '')) {
        var str = "http://Vybhavonlineservices.co.in/popup.aspx?OperatorID=" + operId + "&CircleID=" + cirId + "";
        $('#pptarrif').css('display', 'block');
        $("#pptarrif").attr("href", str);
    }
    else {
        $('#pptarrif').css('display', 'none');
        $("#pptarrif").attr("href", "");
    }
    $("#tabs").show();
}
function disableTariffPlans() {
    $("#popup").fadeOut("normal");
    $("#background").fadeOut("normal");
}
$(document).ready(function () {
    $("#background").click(function () {
        disableTariffPlans();
    });
});*/
function GetTariffPlans() {
    
    var operId = $("#ddlOperatorPrepaid option:selected").val();
    var cirId = $("#ddlCirclePrepaid option:selected").val();
    //if (operId != '0') {
    //    Getsurcharge(operId, "lblschrg");
    //}
    if ((operId != '0') && (cirId != '')) {
        GetTariffPlansr(operId, cirId);
    }
    //    else {
    //        $('#pptarrif').css('display', 'none');
    //        $("#pptarrif").attr("href", "");
    //    }
}
function GetTariffPlansr(o, c) {
    $.ajax({
        type: "POST",
        url: "dashboard.aspx/GetTRF",
        data: '{searchTerm: "' + o + ',' + c + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var str = response.d.split('^');
            //alert(str[0] + '===' + str[1]);
            //$("#trfPlan").val(str);
            document.getElementById('trfPlan').innerHTML = str[0];
            $('#myModal').modal('show');
        },
        failure: function (response) {
            //alert(response.d);
        },
        error: function (response) {
            //alert(response.d);
        }
    });

    //    var operId = $("#ddlOperator option:selected").val();
    //    var cirId = $("#ddlCirclePrepaid option:selected").val();
    //    if (operId != '0') {
    //        Getsurcharge(operId, "lblschrg");
    //    }
    //    if ((operId != '0') && (cirId != '')) {
    //        var str = "popup.aspx?OperatorID=" + operId + "&CircleID=" + cirId + "";
    //        $('#pptarrif').css('display', 'block');
    //        $("#pptarrif").attr("href", str);
    //    }
    //    else {
    //        $('#pptarrif').css('display', 'none');
    //        $("#pptarrif").attr("href", "");
    //    }
}
function GetAmountPlan(amount, outptid) {
    var operId = '0';
    var cirId = '0';
    if (outptid = 'lbltxt1') {
        var operId = $("#ddlOperatorPrepaid option:selected").val();
        var cirId = $("#ddlCirclePrepaid option:selected").val();
    }

    if ((operId != '0') && (cirId != '0') && (amount != '')) {
        amount = amount + '$' + operId + '$' + cirId;
        $.ajax({
            type: "POST",
            url: "DashBoard.aspx/GetAmountPlan",
            data: '{searchTerm: "' + amount + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                //alert(response);
                var str = response.d.split(',');
                document.getElementById(outptid).innerHTML = str[0];
                //$("#" + outptid).val(str[0]);
            },
            failure: function (response) {
                //alert(response.d);
            },
            error: function (response) {
                //alert(response.d);
            }
        });
    }
    else {
        document.getElementById(outptid).innerHTML = 'No offer / plan available !!';
    }
}
function myFunction() {
    if ($("#ddlCirclePrepaid").val() == 0) {
        GetCustomers();
    }
}
function GetCustomers() {
    $.ajax({
        type: "POST",
        url: "DashBoard.aspx/GetCustomers",
        data: '{searchTerm: "' + $("#txtNumberPrepaid").val() + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var str = response.d.split(',');
            $("#ddlOperatorPrepaid").val(str[0]);
            $("#ddlCirclePrepaid").val(str[1]);
            $('#ddlOperatorPrepaid').trigger("chosen:updated");
            $('#ddlCirclePrepaid').trigger("chosen:updated");
            GetTariffPlans();
        },
        failure: function (response) {
            //alert(response.d);
        },
        error: function (response) {
            //alert(response.d);
        }
    });
}

function getElec() {
    if ($("#ddlElectricity").val() == "" && $("#txtCANumber").val() == "")
        return false;
    $.ajax({
        type: "POST",
        url: "DashBoard.aspx/GetElec",
        data: '{searchTerm: "' + $("#ddlElectricity").val() + "^" + $("#txtCANumber").val() + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var str = response.d.split('^');
            document.getElementById('trfPlan').innerHTML = str[0];
            document.getElementById('txtAmountElectricity').value = str[1];
            if (str[1] * 1 > 0)
                document.getElementById('txtAmountElectricity').disabled = true;
            else
                document.getElementById('txtAmountElectricity').disabled = false;
        },
        failure: function (response) {
        },
        error: function (response) {
        }
    });
}
function getIns() {
    if ($("#txtpolicynumber").val() == "" && $("#ddlInsurance").val() == "")
        return false;
    $.ajax({
        type: "POST",
        url: "DashBoard.aspx/GetIns",
        data: '{searchTerm: "' + $("#ddlInsurance").val() + "^" + $("#txtpolicynumber").val() + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var str = response.d.split('^');
            document.getElementById('trfPlan').innerHTML = str[0];
            document.getElementById('txtInsuranceAmount').value = str[1];
            if (str[1] * 1 > 0)
                document.getElementById('txtInsuranceAmount').disabled = true;
            else
                document.getElementById('txtInsuranceAmount').disabled = false;
        },
        failure: function (response) {
        },
        error: function (response) {
        }
    });
}
function pikAmt(a) {
    document.getElementById('txtAmountPrepaid').value = a;
    return false;
}
function clrsr(a) {
    document.getElementById('trfPlan').innerHTML = "";
    document.getElementById('hdnSelectedTab').value = a;
    return false;
}