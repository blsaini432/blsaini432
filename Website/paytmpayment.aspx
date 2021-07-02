<%@ Page Language="C#" AutoEventWireup="true" CodeFile="paytmpayment.aspx.cs" Inherits="paytmpayment" %>

<!DOCTYPE html>
<html>
<head>
    <title>Paytm Page</title>
</head>
<body>
    <center>
         <h1>Please do not refresh this page...</h1>
      </center>
    <div id="paytm-checkoutjs"></div>
    <script type="application/javascript" src="https://securegw.paytm.in/merchantpgpui/checkoutjs/merchants/Myymke27224961376376.js" onload="onScriptLoad();" crossorigin="anonymous"></script>
    <script>
        function onScriptLoad() {
            alert("test")
            var config = {
                "root": "",
                "flow": "DEFAULT",
                "data": {
                    "orderId":<%=ViewState["orderId"] %>, /* update order id */
                    "token":<%=ViewState["txnToken"] %>, /* update token value */
                    "tokenType": "TXN_TOKEN",
                    "amount":<%=ViewState["AMOUNT"] %>, /* update amount */
                },
                "handler": {
                    "notifyMerchant": function (eventName, data) {
                        console.log("notifyMerchant handler function called");
                        console.log("eventName => ", eventName);
                        console.log("data => ", data);
                    }
                }
            };
    if (window.Paytm && window.Paytm.CheckoutJS) {
        window.Paytm.CheckoutJS.onLoad(function excecuteAfterCompleteLoad() {
            // initialze configuration using init method 
            window.Paytm.CheckoutJS.init(config).then(function onSuccess() {
                // after successfully updating configuration, invoke Blink Checkout
                window.Paytm.CheckoutJS.invoke();
            }).catch(function onError(error) {
                console.log("error => ", error);
            });
        });
    }
}
    </script>
 <%--<form method="post" action="https://securegw-stage.paytm.in/theia/api/v1/showPaymentPage?mid=ShreeS77807381065934&orderId=<%=ViewState["orderId"] %>" name="paytm">--%>
  <form method="post" action="https://securegw.paytm.in/theia/api/v1/showPaymentPage?mid=Myymke27224961376376&orderId=<%=ViewState["orderId"] %>" name="paytm">
        <table border="1">
            <tbody>
                <input type="hidden" name="mid" value="Myymke27224961376376">
                <input type="hidden" name="orderId" value="<%=ViewState["orderId"] %>">
                <input type="hidden" name="txnToken" value="<%=ViewState["txnToken"] %>">
            </tbody>
        </table>
        <script type="text/javascript"> document.paytm.submit(); </script>
    </form>
   
</body>
</html>
