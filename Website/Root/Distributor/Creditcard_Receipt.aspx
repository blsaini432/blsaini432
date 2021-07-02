<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="Creditcard_Receipt.aspx.cs" Inherits="Root_Distributor_Creditcard_Receipt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="../../Design/css/bbps_module.css">
     <script> 
         function printDiv() {
             var divContents = document.getElementById("print").innerHTML;
            var a = window.open('', '', 'height=500, width=500'); 
            a.document.write('<html>'); 
            a.document.write('<head>');
            a.document.write('<link rel="stylesheet" href="../../css/Receipt_css/bootstrap.min.css">');
            a.document.write('</head>'); 
            a.document.write('<body >'); 
            a.document.write(divContents); 
            a.document.write('</body></html>'); 
            a.document.close(); 
            a.print();
         
        } 
    </script> 
    <style>
        .logo-left img {
            width: 160px;
        }

        .header-section {
            padding: 70px 20px;
            /* box-shadow: 0 0 10px #b9b9b9; */
            margin: 0 auto;
            width: 85%;
        }

        .main-nav {
            padding: 30px 40px;
            border: 1px solid #d7d7d7;
            box-shadow: 1px -1px 8px 1px rgba(0, 0, 0, 0.3);
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


    </style>
    <%-- <link href="../../css/Receipt_css/bootstrap.min.css" rel="stylesheet" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="print" class="header-section">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="main-nav">
                        <div class="row d-flex align-items-center">
                            <div class="col-md-4">
                                <div class="logo-left">
                                    <a href="#">
                                        <img class="ezulix-logo" id="imgcompanymini" runat="server" />
                                    </a>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="Contact-bar text-center">
                                    <h5 class="font-weight-bold">Final Details for Order<asp:Label ID="lbltxnid" runat="server"></asp:Label></h5>
                                    <p>Print this page for your records.</p>

                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="Contact-right">
                                    <h5 class="font-weight-bold">Biller ID: 
                                        <asp:Label ID="lblbillerid" runat="server"></asp:Label></h5>
                                    <p>Issued Date :<asp:Label ID="lbltxndate" runat="server"></asp:Label></p>
                                </div>
                            </div>
                        </div>
                        <hr>

                        <div class="row d-flex align-items-center">
                            <div class="col-md-6">
                                <div class="customer-bar">
                                    <h6>Order Placed: <span class="font-weight-bold">
                                        <asp:Label ID="lbltxndatea" runat="server"></asp:Label></span></h6>
                                    <h6>order number:<span class="font-weight-bold">
                                        <asp:Label ID="lblorder" runat="server"></asp:Label></span></h6>
                                    <h6>Order Total:<span class="font-weight-bold">
                                        <asp:Label ID="lbltotalamount" runat="server"></asp:Label></span></h6>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="customer-bar">
                                    <h6 class="font-weight-bold">Issued by : <span class="font-weight-bold">3dpayment.in</span></h6>
                                    <h6>Customer Name :<span class="font-weight-bold"><asp:Label ID="lblcustomername" runat="server"></asp:Label></span></h6>
                                </div>
                            </div>
                        </div>

                        <hr>
                        <div class="row d-flex align-items-center">
                            <div class="col-md-12">
                                <div class="customer-bar top-m">
                                    <h6 class="text-center"><span class="font-weight-bold ">Credit Card Bill Payment </span></h6>

                                </div>
                            </div>

                        </div>
                        <hr>
                        <div class="row d-flex">
                            <div class="col-md-9">
                                <div class="customer-bar top-m">
                                    <h5><span class="font-weight-bold">Payment Status :
                                        <asp:Label ID="lblstatus" runat="server"></asp:Label>
                                    </span></h5>
                                    <div class="customer-bar">
                                        <h6>mobile number : <span class="font-weight-bold">
                                            <asp:Label ID="lblcustomermobile" runat="server"></asp:Label></span></h6>                                   
                                        <h6>Credit Card Number : <span class="font-weight-bold">
                                            <asp:Label ID="lblservicefee" runat="server"></asp:Label></span></h6>
                                       <%-- <h6>Provide : <span class="font-weight-bold">
                                            <asp:Label ID="lblbillereoperator" runat="server"></asp:Label></span></h6>--%>
                                        <h6>Mode : <span class="font-weight-bold">
                                            <asp:Label ID="lbloprefno" runat="server"></asp:Label></span></h6>
                                        <%--<h6>BBPS Biller Id : <span class="font-weight-bold">
                                            <asp:Label ID="lblbiller_id" runat="server"></asp:Label></span></h6>--%>
                                        <br>
                                        <p>Thank you for your payment. Your payment will be updated at the biller's endwithin 2-3 working days. If you have paid your bill before the due date , no latepayment fees would be charged to your bill. If you have paid your bill partially, youcould be charged a late payment fees by the biller. Any excess payment madewould be adjusted with the next bill due. Partial payments would be liable to latepayment fees.</p>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="customer-bar top-m">
                                    <h5><span class="font-weight-bold">Amount:
                                        <asp:Label ID="lblamount" runat="server"></asp:Label>
                                    </span></h5>


                                </div>
                            </div>


                        </div>
                        <hr>
                        <div class="row d-flex align-items-center">
                            <div class="col-md-12">
                                <div class="customer-bar top-m">
                                    <h6 class="text-center"><span class="font-weight-bold ">Credit Card Bill Payment </span></h6>

                                </div>
                            </div>


                        </div>
                        <hr>
                        <div class="row d-flex align-items-center">

                            <div class="col-md-6">
                                <div class="Contact-bar">
                                    <h5 class="font-weight-bold">Payment Method :</h5>
                                    <p>Online Pay balance</p>
                                    <h5 class="font-weight-bold">Member ID :
                                        <asp:Label ID="lblmemberid" runat="server"></asp:Label></h5>

                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="Contact-right">
                                    <h5 class="font-weight-bold">Item(s) Subtotal  :
                                        <asp:Label ID="lbltotamount" runat="server"></asp:Label>
                                    </h5>
                                      
                                </div>
                            </div>
                        </div>
                    </div>



                    <input type="button" value="Print" onclick="printDiv()">
                </div>
            </div>
        </div>
    </div>
      
</asp:Content>

