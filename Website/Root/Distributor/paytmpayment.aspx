<%@ Page Language="C#" AutoEventWireup="true" CodeFile="paytmpayment.aspx.cs" Inherits="paytmpayment" %>
<html>

<head>
    <title>Paytm Page</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script>
  function onScriptLoad(){
      var config = {
        "root": "",
        "flow": "DEFAULT",
        "data": {
          "orderId": "<%=ViewState["orderId"] %>", /* update order id */
          "token": "<%=ViewState["txnToken"] %>", /* update token value */
          "tokenType": "TXN_TOKEN",
          "amount": "<%=ViewState["AMOUNT"] %>" /* update amount */
        },
        "handler": {
          "notifyMerchant": function(eventName,data){
            console.log("notifyMerchant handler function called");
            console.log("eventName => ",eventName);
            console.log("data => ",data);
          } 
        }
      };

      if(window.Paytm && window.Paytm.CheckoutJS){
          window.Paytm.CheckoutJS.onLoad(function excecuteAfterCompleteLoad() {
              // initialze configuration using init method 
              window.Paytm.CheckoutJS.init(config).then(function onSuccess() {
                  // after successfully updating configuration, invoke JS Checkout
                  window.Paytm.CheckoutJS.invoke();
              }).catch(function onError(error){
                  console.log("error => ",error);
              });
          });
      } 
  }
</script>
   <%--  <script type="application/javascript" src="https://securegw.paytm.in/merchantpgpui/checkoutjs/merchants/Cybdee15771167977955.js" onload="onScriptLoad()" crossorigin="anonymous"></script>--%>
   <script type="application/javascript" src="https://securegw-stage.paytm.in/merchantpgpui/checkoutjs/merchants/TKINFO51656919140206.js" onload="onScriptLoad()" crossorigin="anonymous"></script>
</head>
<body>
   <br />
    <br />
    <br />
    <br />
    <br />
     <a href="Dashboard.aspx"  style="margin:570px;font-size:16px">&laquo; Back to Dashboard</a>
   </body></html>