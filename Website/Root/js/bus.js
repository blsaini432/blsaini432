$(function () {
    //get data..
    getData();
});
function savecrap()
{

   sessionStorage.setItem(one, two);

    display(one);

}

function display(one)
{

    var rightbox = document.getElementById("rightbox");

    var two = sessionStorage.getItem(one);

    rightbox.innerHTML = "Name of variable:" + one + "<br/> value:" + two;

}

function getData() {
    $.ajax({
        type: "POST",
        //url: "Default2.aspx/GetCustomersJSON",
        //url: "../Default2.aspx/GetCustomersJSON",
        //url: "Default2.aspx/GetCustomersJSON", $('#txtName').val()

        url: "../Handler.ashx",
        data: 'customerId= ' + $("#Hidden1").val() + '',
        dataType: "xml",
        success: function (response) {
            //nodes..
            var xmlData = $(response).find('data');
            //iterate each node..
            xmlData.each(function () {
                //get relevant values..
                var dataObj = new Object();
                dataObj.row = $(this).find('row').text();
                dataObj.column = $(this).find('column').text();
                dataObj.length = $(this).find('length').text();
                dataObj.width = $(this).find('width').text();
                dataObj.name = $(this).find('name').text();
                dataObj.fare = $(this).find('fare').text();
                dataObj.zIndex = $(this).find('zIndex').text();
                dataObj.available = $(this).find('available').text();
                dataObj.ladiesSeat = $(this).find('ladiesSeat').text();
                dataObj.malesSeat = $(this).find('malesSeat').text();
                plotData(dataObj);
            });
            adjustSeats();

        }
    });
}
function adjustSeats() {
    //adjust upper deck container..
    if ($('#upperDeckContainer').children().length > 1) {
        for (var i = 0; i < $('#upperDeckContainer').children().length - 1; i++) {
            if ($('#udRow' + i).children().length > 0) {
                $('#udRow' + i).children().each(function () {
                    if ($(this).prev().attr("id")) {
                        if ($(this).prev().find('img').length > 0) {
                            if ($(this).prev().find('img').data("seatData").length == "2") {
                                $(this).width("0");
                            }
                        }

                    }
                });
            }
        }
    }
    //adjust lower deck container..
    if ($('#lowerDeckContainer').children().length > 1) {
        for (var i = 0; i < $('#lowerDeckContainer').children().length - 1; i++) {
            if ($('#ldRow' + i).children().length > 0) {
                $('#ldRow' + i).children().each(function () {
                    if ($(this).prev().attr("id")) {
                        if ($(this).prev().find('img').length > 0) {
                            if ($(this).prev().find('img').data("seatData").length == "2") {
                                $(this).width("0");
                            }
                        }

                    }
                });
            }
        }
    }


}

function plotData(dataObj) {
    //upper deck container..
    var udContainer = $('#upperDeckContainer');

    //lower deck container
    var ldContainer = $('#lowerDeckContainer');

    //check the iteration is for upper deck or lower deck..
    //if the zIndex value is 1 then it is upper deck else it is lower deck..
    var zIndex = parseInt(dataObj.zIndex);
    if (zIndex == 1) {
        //get row number and create all the below rows..
        var row = parseInt(dataObj.row);
        for (var i = 0; i <= row; i++) {
            var rowSelector = $('#udRow' + i);
            if (rowSelector.length == 0) {
                var rowObj = $('<div/>').attr("id", "udRow" + i).css({ "width": "100%", "height": "5%", "margin-top": "2px", "margin-bottom": "2px", "float": "left" });
                udContainer.append(rowObj);
            }
        }
        //plot columns..
        var column = parseInt(dataObj.column);
        for (var i = 0; i <= column; i++) {
            var columnSelector = $('#udRow' + row + 'Column' + i);
            if (columnSelector.length == 0) {
                var columnObj = $('<div/>').attr("id", "udRow" + row + "Column" + i).css({"float": "left", "height": "102%", "width": "3%"});
                $('#udRow' + row).append(columnObj);
                columnObj.append()

            }
        }

        //plot seats..
        if (dataObj.available == "false") {
            //plot unavailable seats..
            //check which seat will be plotted by checking width.. if the width is 2 then plot sleeper else plot single seat..
            var seatWidth = parseInt(dataObj.width);
            var seatLength = parseInt(dataObj.length);
            if (seatLength == 1) {
                plotSeat("../images/single-seat-booked.png", seatLength, $('#udRow' + row + "Column" + column), dataObj);
            }
            else if (seatLength == 2) {
                plotSeat("../images/slpeer-seat-booked.png", seatLength, $('#udRow' + row + "Column" + column), dataObj);
            }
        }
        else if (dataObj.available == "true") {
            if (dataObj.ladiesSeat == "true") {
                //if ladies seats..
                var seatWidth = parseInt(dataObj.width);
                var seatLength = parseInt(dataObj.length);
                if (seatLength == 1) {
                    //plot single seat..
                    plotSeat("../images/single-seat-ladies.png", seatLength, $('#udRow' + row + "Column" + column), dataObj);
                }
                else if (seatLength == 2) {
                    plotSeat("../images/slpeer-seat-ladies.png", seatLength, $('#udRow' + row + "Column" + column), dataObj);
                }
            }
            else {
                var seatWidth = parseInt(dataObj.width);
                var seatLength = parseInt(dataObj.length);
                if (seatLength == 1) {
                    //plot single seat..
                    plotSeat("../images/single-seat.png", seatLength, $('#udRow' + row + "Column" + column), dataObj);
                }
                else if (seatLength == 2) {
                    plotSeat("../images/slpeer-seat.png", seatLength, $('#udRow' + row + "Column" + column), dataObj);
                }
            }
        }
    }
    else {
        //plot for lower deck..
        var row = parseInt(dataObj.row);
        for (var i = 0; i <= row; i++) {
            var rowSelector = $('#ldRow' + i);
            if (rowSelector.length == 0) {
                var rowObj = $('<div/>').attr("id", "ldRow" + i).css({ "width": "100%", "height": "5%", "margin-top": "2px", "margin-bottom": "2px", "float": "left" });
                ldContainer.append(rowObj);
            }
        }
        //plot columns..
        var column = parseInt(dataObj.column);
        for (var i = 0; i <= column; i++) {
            var columnSelector = $('#ldRow' + row + 'Column' + i);
            if (columnSelector.length == 0) {
                var columnObj = $('<div/>').attr("id", "ldRow" + row + "Column" + i).css({"float": "left", "height": "102%", "width": "3%"});
                $('#ldRow' + row).append(columnObj);
            }
        }

        //plot seats..
        if (dataObj.available == "false") {
            //plot unavailable seats..
            //check which seat will be plotted by checking width.. if the width is 2 then plot sleeper else plot single seat..
            var seatWidth = parseInt(dataObj.width);
            var seatLength = parseInt(dataObj.length);
            if (seatLength == 1) {
                plotSeat("../images/single-seat-booked.png", seatLength, $('#ldRow' + row + "Column" + column), dataObj);
            }
            else if (seatLength == 2) {
                plotSeat("../images/slpeer-seat-booked.png", seatLength, $('#ldRow' + row + "Column" + column), dataObj);
            }
        }
        else if (dataObj.available == "true") {
            if (dataObj.ladiesSeat == "true") {
                //if ladies seats..
                var seatWidth = parseInt(dataObj.width);
                var seatLength = parseInt(dataObj.length);
                if (seatLength == 1) {
                    //plot single seat..
                    plotSeat("../images/single-seat-ladies.png", seatLength, $('#ldRow' + row + "Column" + column), dataObj);
                }
                else if (seatLength == 2) {
                    plotSeat("../images/slpeer-seat-ladies.png", seatLength, $('#ldRow' + row + "Column" + column), dataObj);
                }
            }
            else {
                var seatWidth = parseInt(dataObj.width);
                var seatLength = parseInt(dataObj.length);
                if (seatLength == 1) {
                    //plot single seat..
                    plotSeat("../images/single-seat.png", seatLength, $('#ldRow' + row + "Column" + column), dataObj);
                }
                else if (seatLength == 2) {
                    plotSeat("../images/slpeer-seat.png", seatLength, $('#ldRow' + row + "Column" + column), dataObj);
                }
            }
        }
    }
}

var selectedSeatsArray = new Array();

function plotSeat(imgSrc, seatLength, container, dataObj) {
    var seatImage = $('<img/>').attr({"src": imgSrc, "title": dataObj.name + " - Rs." + dataObj.fare}).css({"width": "100%"});

    if (seatLength == 1) {
        seatImage.css({"padding": "2px"});
    }
    else if (seatLength == 2) {
        container.css({"width": "6%"});
    }
    seatImage.data("seatData", dataObj);


    seatImage.click(function () {
            //select seats..
            var seatData = $(this).data("seatData");
            if (seatData.available == "true") {
                if (seatData.length == "1") {
                    if (seatData.ladiesSeat == "false") {
                        if (seatData.seatSelected) {
                            $(this).attr("src", "../images/single-seat.png");
                            seatData.seatSelected = false;
                           
                            $("#hfselectedseats").val(null);
                            $("#hfselectedseats1").val(null);
                            $("#hfselectedseats2").val(null);
                            $("#hfselectedseats3").val(null);
                            $("#hfselectedseats4").val(null);
                            $("#hfselectedseats5").val(null);
                            popArray(seatData);
                        }
                        else {
                            if (selectedSeatsArray.length < 6) {
                                $(this).attr("src", "../images/single-selected.png");
                                seatData.seatSelected = true;
                              
                            }
                            pushArray(seatData);
                        }

                    }
                    else {
                        if (seatData.seatSelected) {
                            $(this).attr("src", "../images/single-seat-ladies.png");
                            seatData.seatSelected = false;
                            $("#hfselectedseats").val(null);
                            $("#hfselectedseats1").val(null);
                            $("#hfselectedseats2").val(null);
                            $("#hfselectedseats3").val(null);
                            $("#hfselectedseats4").val(null);
                            $("#hfselectedseats5").val(null);
                            popArray(seatData);

                        }
                        else {
                            if (selectedSeatsArray.length < 6) {
                                $(this).attr("src", "../images/single-seat-ladies-selected.png");
                                seatData.seatSelected = true;
                            }
                            pushArray(seatData);
                        }

                    }
                }
                else {
                    if (seatData.ladiesSeat == "false") {
                        if (seatData.seatSelected) {
                            $(this).attr("src", "../images/slpeer-seat.png");
                            seatData.seatSelected = false;
                            $("#hfselectedseats").val(null);
                            $("#hfselectedseats1").val(null);
                            $("#hfselectedseats2").val(null);
                            $("#hfselectedseats3").val(null);
                            $("#hfselectedseats4").val(null);
                            $("#hfselectedseats5").val(null);
                            popArray(seatData);
                        }
                        else {
                            if (selectedSeatsArray.length < 6) {
                                $(this).attr("src", "../images/slpeer-seelcted.png");
                                seatData.seatSelected = true;
                            }
                            pushArray(seatData);
                        }

                    }
                    else {
                        if (seatData.seatSelected) {
                            $(this).attr("src", "../images/slpeer-seat-ladies.png");
                            seatData.seatSelected = false;
                            $("#hfselectedseats").val(null);
                            $("#hfselectedseats1").val(null);
                            $("#hfselectedseats2").val(null);
                            $("#hfselectedseats3").val(null);
                            $("#hfselectedseats4").val(null);
                            $("#hfselectedseats5").val(null);
                            popArray(seatData);
                        }
                        else {
                            if (selectedSeatsArray.length < 6) {
                                $(this).attr("src", "../images/slpeer-seat-ladies-selected.png");
                                seatData.seatSelected = true;
                            }
                            pushArray(seatData);
                        }
                    }
                }
            }
        }
    );
    container.html(seatImage);
}

function pushArray(dataObj) {
    if (selectedSeatsArray.length < 6) {
        selectedSeatsArray.push(dataObj);
        plotSelectedSeats();
    }
    else {
        alert('You cannot add more than 6 seats');
    }
}

function popArray(dataObj) {
    for (var i = 0; i < selectedSeatsArray.length; i++) {
        if (selectedSeatsArray[i].name == dataObj.name) {
            selectedSeatsArray.splice(i, 1);
        }

    }
    sessionStorage.setItem("key", dataObj.name);
    totalFare = sessionStorage.getItem("key");
    plotSelectedSeats();
}

function plotSelectedSeats() {

   
    var totalFare = 0;
    var selectedSeatsArraynew = new Array();
    $('#seatsContainer').empty();
    for (var i = 0; i < selectedSeatsArray.length; i++) {

        $('#seatsContainer').append("<span style='background-color: green;padding-left: 5px;padding-right: 5px; float:left;margin-left: 2px; margin-right: 2px;border-radius: 5px;color:#fff'>" + selectedSeatsArray[i].name + "</span>");
        totalFare += parseFloat(selectedSeatsArray[i].fare);
       



    } $('#fareContainer').html(totalFare);
    $("#hftotalFare").val(totalFare);    
    for (var i = 0; i < selectedSeatsArray.length; i++) {
        $("#hfselectedseats").val(selectedSeatsArray[0].name);
        $("#hfselectedseats1").val(selectedSeatsArray[1].name);
        $("#hfselectedseats2").val(selectedSeatsArray[2].name);
        $("#hfselectedseats3").val(selectedSeatsArray[3].name);
        $("#hfselectedseats4").val(selectedSeatsArray[4].name);
        $("#hfselectedseats5").val(selectedSeatsArray[5].name);
    }
       
  

}