<%@ Page Language="C#" AutoEventWireup="true" CodeFile="paytmpayment_js.aspx.cs" Inherits="paytmpayment" %>

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
    <script type="application/javascript" src="https://securegw-stage.paytm.in/merchantpgpui/checkoutjs/merchants/Runfin01656809110447.js" onload="onScriptLoad()" crossorigin="anonymous"></script>
</head>
<body>
<%--<script type="application/javascript" src="{HOST}/merchantpgpui/checkoutjs/merchants/{MID}.js" onload="onScriptLoad();" crossorigin="anonymous"></script>--%>
   <br />
    <br />
    <br />
    <br />
    <br />
     <a href="Dashboard.aspx"  style="margin:600px;font-size:22px">&laquo; Back to Dashboard</a>
   </body></html>