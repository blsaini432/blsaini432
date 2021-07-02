<%@ Page Language="C#" AutoEventWireup="true" CodeFile="payonclick.aspx.cs" Inherits="Root_Retailer_payonclick" %>

<!DOCTYPE html>

<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   
</head>
    <form method="post" name="form1" action="https://www.easypolicy.com/epnew/Landing/Landing.aspx"> // UAT
        utm_source:
    <input type="text" id="utm_source" name="utm_source" value="CCSC"><br> 
        utm_medium:
        <input type="text" name="utm_medium" value="CCSC"><br>
        utm_campaign:
        <input type="text" name="utm_campaign" value="CCSC"><br>
        utm_term:
        <input type="text" name="utm_term" value="CCSC"><br>

        partnerleadid :
        <input type="text" name="partnerleadid" value="54775675756756"><br>

        partneragentid:
      <input type="text" name="partneragentid" value="757575754676gdgfg"><br> 
       Udf1:
        <input type="text" name="Udf1" value=""><br>
        Udf2:
        <input type="text" name="Udf2" value=""> <br>
        Udf3:
        <input type="text" name="Udf3" value="DESKTOP"><br> 

        <div class="col4-box2" style="text-align: center; clear: both;">
            <input name="btnGetQuoteWidget" id="btnGetQuoteWidget" value="Submit" type="submit">
        </div>
    </form>


</html>--%>



<!DOCTYPE html>
<html>
<head>


    <style>
        html, body {
            display: flex;
            justify-content: center;
            font-family: Roboto, Arial, sans-serif;
            font-size: 15px;
        }

        form {
            border: 5px solid #f1f1f1;
            position: absolute;
        }

        input[type=text], input[type=password] {
            width: 100%;
            padding: 16px 8px;
            margin: 8px 0;
            display: inline-block;
            border: 1px solid #ccc;
            box-sizing: border-box;
        }

        button {
            background-color: #8ebf42;
            color: white;
            padding: 14px 0;
            margin: 10px 0;
            border: none;
            cursor: grabbing;
            width: 100%;
        }

        #btnGetQuoteWidget {
            background: blue;
            font-size: 24px;
        }

        h1 {
            text-align: center;
            fone-size: 18;
        }

        button:hover {
            opacity: 0.8;
        }

        .formcontainer {
            text-align: left;
            margin: 24px 50px 12px;
        }

        .container {
            padding: 16px 0;
            text-align: left;
        }

        span.psw {
            float: right;
            padding-top: 0;
            padding-right: 15px;
        }
        /* Change styles for span on extra small screens */
        @media screen and (max-width: 300px) {
            span.psw {
                display: block;
                float: none;
            }
    </style>
</head>
<body>
    <form method="post" name="form1" action="https://www.easypolicy.com/epnew/Landing/Landing.aspx">
        <h1>INSURANCE</h1>
        <div class="formcontainer">
            <hr />
            <div class="container">
                 <label for="hidden"><strong></strong></label>
                <input type="hidden" id="utm_source" name="utm_source" value="Payonclick">

                <label for="hidden"><strong></strong></label>
                <input type="hidden" name="utm_medium" value="Payonclick">
                <label for="hidden"><strong></strong></label>
                <input type="hidden" name="utm_campaign" value="Payonclick">
                <label for="hidden"><strong></strong></label>
                <input type="hidden" name="utm_term" value="Payonclick">
                <label for="hidden"><strong></strong></label>
                <input type="hidden" name="partnerleadid" value="<%=ViewState["id"] %>">
                <label for="hidden"><strong></strong></label>
                <input type="hidden" name="partneragentid" value="<%=ViewState["MemberId"] %>">
            </div>
            <input name="btnGetQuoteWidget" id="btnGetQuoteWidget" value="Submit" type="submit">
    </form>
</body>
</html>
