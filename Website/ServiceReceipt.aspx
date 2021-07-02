<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ServiceReceipt.aspx.cs" Inherits="ServiceReceipt" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@300;400;500;700;900&display=swap" rel="stylesheet"/> 
	<link rel="stylesheet" href="css/bootstrap.min.css"/>
<style>
body {
    background: #e7e9ed;
    color: #535b61;
    font-family: "roboto", sans-serif;
    font-size: 14px;
    line-height: 22px;
	margin: 0 !important;
	font-weight: 300;
}
.container {
    width: 100%;
}

.invoice-section {
	overflow: hidden;
	padding: 25px 0;
	max-width: 850px;
	background-color: #fff;
	border: 1px solid #ccc;
	margin: 0 auto;
}

.col {
    width: 50%;
}
.col-left {
    float: left;
}
.invoice-id p {
	font-size: 16px;
	color: #000;
	margin-bottom: 4px !important;
}
.mail-id {
	color: #262626;
}		
.declaration-section {
    position: relative;
    display: inline-block;
}
.declaration-title {
	font-weight: 500;
	color: #262626;
	padding-bottom: 5px;
	text-decoration: underline;
}	
.footer-section {
    border-top: 1px solid #000;
    padding-top: 20px;
}
.border-bar {
	border: 1px solid;
	padding: 15px;
	height: 110px;
}
	.border-color th {
	border: 1px solid #000 !important;
}
		.border-color td {
	border: 1px solid #000 !important;
}
/*----====================================
       BBPS Module Page 4 
 ===================================-----*/
.bill_bg_white {
	background: #fff;
	padding:40px;
	display: -webkit-box;
	box-shadow: 0 0 10px #d3ebff;
}
.biil-title {
	color: #231e6f;
	font-weight: 600;
	margin-top: 0;
	padding-bottom: 15px;
	font-size: 26px;
}
.bill-information-coloume {
	padding-bottom: 6px;
}
.bill-static-deta {
	text-align: left;
	font-size: 18px;
	font-weight: 600;
	color: #231e6f;
	width: 55%;
	display: inline-block;
}
.bill-dynamic-deta {
	color: #14132b;
	font-weight: 597;
	font-size: 14px;
	margin-left: -15px;
}
.bill-dynamic-deta::before {
	content: " : ";
	position: absolute;
	color: #231e6f;
	margin-left: -27px;
	font-size: 18px;
}
.bill-found-button .btn-default {
	position: absolute;
	bottom: 0;
	right: 0;
}
.bill_bg_dark {
	background: #fff;
	padding: 10px 0px;
	display: -webkit-box;
}
</style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="invoice-section">
  <div class="container">
 
	<div class="form-section support-form-wrapper ml-mr">
<div class="dark-bg-section">
    <div class="row">
      <div class="col-md-6 col-sm-6 col-xs-12">
        <div class="successfully-button">
          <h4>Transaction Receipt 	 </h4>
			 <p>TR:<asp:Label ID="lblTransId1" runat="server"></asp:Label> </p>
			 <h5>Transaction Details 	 </h5>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-lg-12">
        <div class="bill-info-wrap bill_bg_dark">
          <div class="col-md-6 col-sm-6 col-xs-12">
            <div class="bill-detail">
              <div class="bill-information-coloume"> <span class="bill-static-deta">Service For</span> <span class="bill-dynamic-deta"> <asp:Label ID="lblServiceName" runat="server"></asp:Label></span> </div>
              <div class="bill-information-coloume"> <span class="bill-static-deta">PayDate </span> <span class="bill-dynamic-deta"><asp:Label ID="lblAddDate" runat="server"></asp:Label></span> </div>
              <div class="bill-information-coloume"> <span class="bill-static-deta">Mobile </span> <span class="bill-dynamic-deta"><asp:Label ID="lblPhone" runat="server"></asp:Label></span> </div>
              <div class="bill-information-coloume"> <span class="bill-static-deta">Transaction ID</span> <span class="bill-dynamic-deta"> <asp:Label ID="lbltransid" runat="server"></asp:Label></span> </div>
              <div class="bill-information-coloume"> <span class="bill-static-deta">Status </span> <span class="bill-dynamic-deta">  	<asp:Label ID="lblStatus" runat="server"></asp:Label> </span> </div>
				<div class="bill-information-coloume"> <span class="bill-static-deta">Amount</span> <span class="bill-dynamic-deta">Rs.<asp:Label ID="lblAmount" runat="server"></asp:Label> </span> </div>
              
            </div>
          </div>
        </div>
      </div>
    </div>
    </div>
</div>  
	  
	  
	  
    
  </div>
</div>

    </form>
</body>
</html>
