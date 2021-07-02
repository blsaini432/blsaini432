<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="Certificate_new1.aspx.cs" Inherits="Root_Distributor_Certificate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <script type="text/javascript">
    $(function () {
        $("#btnPrint").click(function () {
            var contents = $("#printarea").html();
            var frame1 = $('<iframe />');
            frame1[0].name = "frame1";
            frame1.css({ "position": "absolute", "top": "-1000000px" });
            $("body").append(frame1);
            var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
            frameDoc.document.open();
            //Create a new HTML document.
            frameDoc.document.write('<html><head><title></title>');
            frameDoc.document.write('</head><body>');
            //Append the external CSS file.
            frameDoc.document.write('<link href="/Content/bootstrap/css/bootstrap.min.css" rel="stylesheet" />');
            //Append the DIV contents.
            frameDoc.document.write(contents);
            frameDoc.document.write('</body></html>');
            frameDoc.document.close();
            setTimeout(function () {
                window.frames["frame1"].focus();
                window.frames["frame1"].print();
                frame1.remove();
            }, 500);
        });
    });
</script>

    <style>
        body {
            max-width: 100%;
            padding: 0;
            margin: 0;
            height: auto;
            width: auto;
            text-align: center !important;
        }

        .container {
            width: 1026px !important;
            padding: 0 !important;
        }

        .img-banner {
            background: url(../../certificateback.png);
            height: 670px;
            /*padding: 0 50px;*/
            margin-top: 30px;
            width: 100%;
        }

        .text-area-table {
            width: 100%;
            max-width: 100%;
            margin-bottom: 20px;
            margin-right: 50px !important;
            padding: 0 50px;
            display: block;
            margin-bottom: 0 !important;
        }

        tr td {
            border: none !important;
        }

        .top-section {
            width: 80px;
            padding-top: 20px;
        }

        .logo-icici img, .logo-yes img {
            width: 134px;
        }

        .logo-brand img {
            width: 200px;
            margin-top: -18px;
        }

        .certificate-title {
            font-size: 22px;
        }

        .logo-section {
            padding: 47px 0;
        }

        .logo {
            width: 170px;
            margin-top: 50px;
        }
        .heading h1 {
            margin-right: 40px;
            margin-top: 20px;
            font-size: 24px;
            font-weight: 600;
        }

        .agent-section {
            width: 100%;
            display: flow-root;
        }

        .top-left {
            float: left; /* width: 44%; */
            margin-left: 50px;
        }

            .top-left h2 {
                font-size: 20px;
                font-weight: 600;
            }

        .top-right {
        }

            .top-right h2 {
                font-size: 20px;
                font-weight: 600;
            }

        .text-area h3 {
            font-size: 18px;
            font-weight: 600;
            text-align: center;
            margin-right: 29px;
        }

        .footer-brand-logo img {
            width: 132px;
        }

        .footer-date-text {
            font-size: 19px;
            margin-top: 10px;
            padding-bottom: 15px;
        }

        .row.footer-certificate {
            align-items: center;
        }

        .auth-Signatory-title p {
            font-size: 18px;
        }

        .text-left {
            width: 50%;
            text-align: left;
        }

        .text-right {
            width: 50%;
            text-align: left;
        }

        .text-area-table p {
            margin: 4px 0;
            font-weight: 600;
            font-size: 18px;
            color: #000;
        }

        /*.member_img img {
            width: 100px;
        }*/


        .footer p {
            float: right;
            padding-right: 50px;
            font-size: 18px;
        }

        .bootom-p {
            font-weight: 600;
            font-size: 18px;
            text-align: left;
            padding-left: 50px;
            color: #000;
        }

        @media (min-width: 768px) {
            .container {
                max-width: 1026px !important;
                width: 100%;
            }
        }

    </style>
    
    <link href="../../css/Certificate_boostrap/bootstrap.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="container">
        <div class="img-banner" id="printarea">
            <div class="row">
                <div class="col-md-4 col-sm-4 col-xs-12">
                    <div class="logo-icici">
                        <div class="logo-section">
                            <img src="../../Uploads/Company/actual/icici_logo.png" />
                            <%-- <asp:Image ID="img1" runat="server" class="top-section" />--%>
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-4 col-xs-12">
                    <div class="logo-brand">
                        <div class="logo-section">
                            <img src="../../Uploads/Company/actual/Akvira.png" />
                            <h1 class="certificate-title">Certificate of Authorization</h1>
                            <%-- <asp:Image ID="img1" runat="server" class="top-section" />--%>
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-4 col-xs-12">
                    <div class="logo-yes">
                        <div class="logo-section">
                            <img src="../../Uploads/Company/actual/yesbank.png" />
                            <%-- <asp:Image ID="img1" runat="server" class="top-section" />--%>
                        </div>
                    </div>
                </div>

            </div>
            <div class="row">
                <div class="col-md-12">
                    <h3>This is to certify that </h3>
                </div>
            </div>
            <div class="agent-section">
                <div class="row">
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <div class="top-left">
                            <%--  <h2>babulal
                                    <asp:Label ID="lbldateofissue" runat="server" Text="" Font-Bold="true"></asp:Label></h2>--%>
                            <h3>M/s
                                                <asp:Label ID="lblname" runat="server" Text="" Font-Bold="true"></asp:Label>
                            </h3>

                            <h3>Address 
                                                <asp:Label ID="lbladdress" runat="server" Text="" Font-Bold="true"></asp:Label>
                            </h3>
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <div class="top-right">
                            <h2>Agent Code
                                    <asp:Label ID="lblagentcode" runat="server" Text="" Font-Bold="true"></asp:Label></h2>
                            <div class="member_img">

                                <asp:Image ID="img1" runat="server" class="top-section" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="text-area">
                <div class="row">
                    <div class="col-md-12">
                        <p class="authorized-text" style="font-size: 20px; padding: 0px 49px">
                            is an authorized Member of AkVIRA TECHNOLOGIES
                            and authorized Customer Service point to conduct Business on its Behalf with effect from the date of Issuance. This Certificate is valid for 1 year from the date of Issuance.
                        </p>

                        <%--<td class="text-left of-text">
                                        <p>
                                            Of
                                                <asp:Label ID="lblcompanyname" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </p>
                                    </td>--%>
                    </div>
                </div>
            </div>
            <div class="row footer-certificate">
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <div class="footer-brand-logo">
                        <img src="../../Uploads/Company/actual/certifite_logo.png" />
                        <h2 class="footer-date-text">Date Of Issues
                                    <asp:Label ID="lbldateofissue" runat="server" Text="" Font-Bold="true"></asp:Label></h2>

                    </div>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <div class="auth-Signatory-title">
                        <p>
                            <%--<img src="../../Uploads/signature.jpg" height="80px" width="200px" /><br>--%>
                                Auth. Signatory<br>
                            <asp:Label ID="lblcompanynm" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>


        </div>
    </div>
    <div class="row">
        <div class="col-md-4 col-sm-4 col-xs-12">
         <button id="btnPrint" class="btn btn-primary btn-sm"><i class="fa fa-print"></i> Print</button>
        </div>
    </div>

</asp:Content>
