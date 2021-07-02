<%@ Page Language="C#" AutoEventWireup="true" CodeFile="paymentAeps.aspx.cs" Inherits="paymentAeps" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
    <h4>I agree that after deducting the charge of PG from the amount of this transaction done by me, the remaining amount will be credited to my wallet and if the transaction is spread, it will take 72 hours to get any refund</h4>
    <div style="text-align: center;color:green">
     <%--  <input type='button'value='Cancel' class="btn" onclick="document.location.href='paymentpgaeps.aspx';"--%>
<form action="paymentcallbackAeps.aspx" method="post">
    
<script

    src="https://checkout.razorpay.com/v1/checkout.js"
    data-key="rzp_live_NY32WiX9K7Kzyy" 
    data-amount="<%=amount%>"
    data-currency="INR"
    data-order_id="<%=orderId%>"
    data-buttontext="I Agree"
    data-name="Payment"
    data-description="Payment"
    data-image="../../Uploads/Company/Logo/actual/logo.png""
    data-prefill.name="<%=name%>"
    data-prefill.email="<%=emailsend%>"
    data-prefill.contact="<%=number%>"
    data-theme.color="#F37254"
></script>
<input type="hidden" value="Hidden Element" name="hidden"/>
</form></div>
</html>
