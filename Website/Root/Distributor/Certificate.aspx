<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Certificate.aspx.cs" Inherits="Root_Distributor_Certificate" %>

<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <title>Azamreceipt</title>
    <link rel="stylesheet" href="../../css/Certificate_boostrap/bootstrap.min.css">
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com">
    <link href="https://fonts.googleapis.com/css2?family=Playfair+Display:wght@500&display=swap" rel="stylesheet">
    <style>
        .logo-left img {
            width: 160px;
        }

        .logo-left {
            background: #220605;
            z-index: 2;
            position: relative;
        }

        .header-bar {
            border-top: 2px solid #220605;
            padding-top: 18px;
        }

        .box-wrapper {
            background: #220605;
            width: 110px;
            height: 110px;
            transform: rotate(45deg);
            text-align: center;
            margin: 0 auto;
            position: absolute;
            top: -2px;
            left: 0;
            right: 0;
            z-index: 0;
        }

        .header-section {
            padding: 70px 20px;
            box-shadow: 0 0 10px #b9b9b9;
            background: #ffc536;
        }

        body {
            width: 70%;
            margin: 0 auto;
        }

        .customer-bar img {
            width: 25px;
        }

        .left-m {
            margin-left: 5px;
        }

        .border-bar {
            padding: 11px;
            border: 4px solid #e5e5e5;
            border-radius: 10px;
        }

        .bill-static-deta {
            text-align: left;
            font-size: 18px;
            font-weight: 600;
            color: #212529;
            width: 55%;
            display: inline-block;
        }

        .bill-dynamic-deta::before {
            content: " : ";
            position: absolute;
            color: #231e6f;
            margin-left: -27px;
            font-size: 18px;
        }

        .bill-dynamic-deta {
            color: #14132b;
            font-weight: 600;
            font-size: 16px;
            margin-left: 15px;
        }

        .top-m h6 {
            margin-top: 10px;
        }

        h5 {
            font-size: 14px;
        }

        span {
            font-size: 13px !important;
        }

        th {
            font-size: 13px;
        }

        p {
            font-size: 14px;
        }
        /*--- box-section ---*/
        .box-bar li {
            display: initial;
            margin-left: 15px;
        }

        .box-border {
            background: #d8d8d8;
            padding: 2px 12px;
            border-radius: 3px;
            border: 1px solid #979797;
        }

        .box-sction {
            padding: 20px 0;
        }

        .box-bar li:nth-child(3) {
            background: #fff;
        }

        .box-bar li:nth-child(5) {
            background: #fa3988;
        }

        .box-bar li:nth-child(7) {
            background: #2276e3;
        }

        .logo-left img {
            width: 60px;
            margin: 0 auto;
            display: block;
        }

        .bg-chgn {
            padding: 25px;
            background: #fff;
            margin-top: 20px;
        }

        .logo-center img {
            width: 290px;
            margin: 0 auto;
            display: block;
        }

        .certificate-heading span {
            color: #220605;
            font-size: 38px !important;
        }

        .certificate-heading {
            margin-top: 30px !important;
        }

        .this-bar {
            margin-top: 20px;
            font-size: 18px;
            letter-spacing: 3px;
            color: #220605;
        }

        .Contact-bar {
            text-align: center;
        }

        .Contact-right {
            text-align: center;
        }

        .certificate-heading .font-bar {
            font-family: 'Playfair Display', serif;
            font-size: 48px !important;
        }

        .certificate-heading .font-size-bar {
            font-size: 32px !important;
        }

        .main-nav {
            border-bottom: 2px solid #220605;
            padding-bottom: 18px;
        }

        .position-bar {
            width: 100px !important;
            position: absolute;
            right: 50px;
            top: 0px;
        }
    </style>
</head>

<body>
    <form id="form" runat="server">
        <div class="header-section">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="main-nav">
                            <div class="row d-flex align-items-center">
                                <div class="col-md-12">
                                    <div class="header-bar">
                                        <div class="box-wrapper"></div>
                                        <div class="logo-left">
                                            <img src="../../css/Certificate_boostrap/img/icon.png">
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="bg-chgn">
                                <div class="row d-flex align-items-center">
                                    <div class="col-md-12">
                                        <div class="logo-center">
                                            <img src="../../css/Certificate_boostrap/img/logo.png">
                                            <img class="position-bar" src="../../css/Certificate_boostrap/img/flipcart-logo.png">
                                        </div>
                                    </div>


                                </div>
                                <div class="row d-flex align-items-center">
                                    <div class="col-md-12">
                                        <div class="customer-bar top-m">
                                            <h6 class="text-center certificate-heading"><span class="font-weight-bold ">certificate for authorization </span></h6>
                                            <p class="this-bar text-center">THIS IS TO CERTIFY THAT</p>
                                            <h6 class="text-center certificate-heading"><span class="font-weight-bold font-bar "><span class="font-size-bar">M/S <asp:Label ID="lblname" runat="server"></asp:Label></h6>
                                        </div>
                                    </div>


                                </div>

                                <div class="row d-flex">
                                    <div class="col-md-12">
                                        <div class="customer-bar top-m">

                                            <div class="customer-bar">


                                                Address <asp:Label ID="lbladdress" runat="server"></asp:Label><p>is an Authorized Retailer of VivaStore India PVT LTD and authorized to conduct Business on its Behalf with the effect from the date of issuance.</p>
                                            </div>

                                        </div>
                                    </div>



                                </div>
                                <hr>


                                <div class="row d-flex align-items-center">

                                    <div class="col-md-6">
                                        <div class="Contact-bar">
                                            <h5 class="font-weight-bold">Date Of Issuance-
                                                <asp:Label ID="lbldate" runat="server"></asp:Label></h5>
                                            <h5 class="font-weight-bold">Agent Code-
                                                <asp:Label ID="lblagentcode" runat="server" Font-Bold="true"></asp:Label></h5>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="Contact-right">
                                            <h5 class="font-weight-bold">Issuing Authority</h5>
                                            <h5 class="font-weight-bold">Vivastore India PVT LTD</h5>

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


