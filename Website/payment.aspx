<%@ Page Language="C#" AutoEventWireup="true" CodeFile="payment.aspx.cs" Inherits="payment" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
   
    <div style="text-align: center;color:green">
        <%--<input type='button'value='Cancel' class="btn" onclick="document.location.href='paymentprocess.aspx';"/--%>
<form action="paymentcallback_pe.aspx" method="post">
    <a href="payment.aspx"></a>
<script
    src="https://checkout.razorpay.com/v1/checkout.js"
    data-key="rzp_live_2vjii3tMpb3IJt" 
    data-amount="<%=amount%>"
    data-currency="INR"
    data-order_id="<%=orderId%>"
    data-buttontext="I Agree"
    data-name="Payment"
    data-description="Payment"
    data-image="../../Uploads/Company/Logo/actual/logo.png""
    data-prefill.name="<%=name%>"
    data-prefill.email="<%=email%>"
    data-prefill.contact="<%=mobile%>"
    data-theme.color="#F37254"
></script>
<input type="hidden" value="Hidden Element" name="hidden"/>
</form></div>
</html>
