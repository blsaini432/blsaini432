<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UPI_Payment_return.aspx.cs" Inherits="Root_Retailer_UPI_Payment_return" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">    
</head>
<body">
    <div>
        <img src="../../Uploads/loader.gif" style="display:block; margin-left: auto;margin-right: auto" />
    </div>
    <%--  <form id="form1" runat="server">--%>
    <form action="https://upigateway.com/gateway/payment" method="post" id="payment_form">
       
        <input type="hidden" name="client_vpa" id="client_vpa" value="<%=ViewState["vpa"]%>" placeholder="Enter Payee UPI">
       
        <input type="hidden" name="amount" id="amount" value="<%=ViewState["Amount"]%>" placeholder="Enter Amount">      
        <!-- client_name field also set hidden if you set value in hidden -->
        <input type="hidden" name="client_name" id="client_name" value="<%=ViewState["name"]%>" placeholder="Enter Payee Name">
        <!-- client_email field also set hidden if you set value in hidden -->
        <input type="hidden" name="client_email" id="client_email" value="<%=ViewState["email"]%>" placeholder="Enter Payee Email">

        
        <!-- client_mobile field also set hidden if you set value in hidden -->
        <input type="hidden" name="client_mobile" id="client_mobile" value="<%=ViewState["mobile"]%>" placeholder="Enter Payee Mobile">
        <input type="hidden" name="key" value="<%=ViewState["key"]%>">

        <!-- p_info is used to Product Related Detail -->
        <input type="hidden" name="p_info" id="p_info" value="RECHARGE">

        <!-- client transactionID is unique string generate by your website or App. Transaction cannot processed again with same transactionID, we prefered to use UUID -->
        <input type="hidden" name="client_txn_id" id="client_txn_id" value="<%=ViewState["txnid"]%>">
        <!-- for recharge B2B Portal, you can use udf1 for user_id from which you will get the Money -->
        <input type="hidden" name="udf1" id="udf1" value="">
        <input type="hidden" name="udf2" id="udf2" value="">
        <input type="hidden" name="udf3" id="udf3" value="">
        <!-- hash is generate from above php code, with all above input field data and updated in hash field using javascript the form has to Submited to UPI Gateway. -->
        <input type="hidden" name="hash" value="" id="hash">
        <!-- Redirect url is not included in hash, put the complete url, it will redirect after the transaction Process is complete. -->
        <input type="hidden" name="redirect_url" value="<%=ViewState["url"]%>">
         <input type="submit" id="pay" value="Pay" style="display:none;">
    </form>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <script>
        var payment_form = document.getElementById("payment_form");
        payment_form.addEventListener("submit", function (e) {
            e.preventDefault();
            var client_vpa = document.getElementById("client_vpa").value;
            var amount = document.getElementById("amount").value;
            var client_name = document.getElementById("client_name").value;
            var client_email = document.getElementById("client_email").value;
            var client_mobile = document.getElementById("client_mobile").value;
            var client_txn_id = document.getElementById("client_txn_id").value;
            var udf1 = document.getElementById("udf1").value;
            var udf2 = document.getElementById("udf2").value;
            var udf3 = document.getElementById("udf3").value;
            var p_info = document.getElementById("p_info").value;
            var hash = document.getElementById("hash");
            $.ajax({
                type: "POST",
                url: "../Retailer/UPI_Payment_return.aspx/getdata",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: '{"client_vpa":"' + client_vpa + '","amount":"' + amount + '","client_name":"' + client_name + '","client_email":"' + client_email + '","client_mobile":"' + client_mobile + '","client_txn_id":"' + client_txn_id + '","p_info":"' + p_info + '"}',
                success: function (data) {
                    console.log(data.d[0].hash);
                    var hash = data.d[0].hash;
                    $("#hash").val(hash);
                    payment_form.submit();
                },
                error: function () {
                    console.log("Hash was unsuccessful");
                }
            });
            return false;
        })
        $("#pay").click();
    </script>
</body>
</html>
