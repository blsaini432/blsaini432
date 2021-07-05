
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Certificate.aspx.cs" Inherits="Certificate" %>

<html>
<head>
    <meta charset="utf-8">
    <title>Certificate</title>

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
            width: 182px;
            padding-top: 52px;
        }

        .logo-section {
            width: 200px;
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
</head>
<body>
   
    <form id="form1" runat="server">
  
        <div class="container">
            <div class="img-banner">
                <div class="row">
                    <div class="col-md-12 col-sm-6 col-xs-12">
                        <div class="logo-section">
                            <asp:Image ID="img1" runat="server" class="top-section" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 col-xs-12">
                        <div class="heading">
                            <h3>CERTIFICATE OF AUTHORIZATION</h3>
                        </div>
                    </div>
                </div>
                <div class="agent-section">
                    <div class="row">
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <div class="top-left">
                                <h2>Agent Code
                                    <asp:Label ID="lblagentcode" runat="server" Text="" Font-Bold="true"></asp:Label></h2>
                            </div>
                        </div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <div class="top-right">
                                <h2>Date of Issuance
                                    <asp:Label ID="lbldateofissue" runat="server" Text="" Font-Bold="true"></asp:Label></h2>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="text-area">
                    <div class="row">
                        <div class="col-md-12">
                            <h3>This is to certify that </h3>
                            <table class="text-area-table table">
                                <thead>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td class="text-left name">
                                            <p>M/s
                                                <asp:Label ID="lblname" runat="server" Text="" Font-Bold="true"></asp:Label></p>
                                        </td>
                                        <%-- <td class="text-right shop-name"><p>Shop Name ------------------</p></td>--%>
                                    </tr>
                                    <tr>
                                        <td class="text-left address">
                                            <p>Address 
                                                <asp:Label ID="lbladdress" runat="server" Text="" Font-Bold="true"></asp:Label></p>
                                        </td>
                                        <td class="text-right authorized">
                                            <p>is an authorized
                                                <asp:Label ID="MemberType" runat="server" Text="" Font-Bold="true"></asp:Label></p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="text-left of-text">
                                            <p>Of
                                                <asp:Label ID="lblcompanyname" runat="server" Text="" Font-Bold="true"></asp:Label></p>
                                        </td>
                                        <td class="text-p">
                                            <p>and authorized to conduct Business on its Behalf with effect from the</p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <%-- <div class="bootom-p">
              <p>date of Issuance. This Certificate is valid for 1 year from the date of Issuance.</p>
            </div>--%>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="footer">
                            <p>
                                Auth. Signatory<br>
                                <asp:Label ID="lblcompanynm" runat="server" Text="" Font-Bold="true"></asp:Label>
                            </p>
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
  
      </form>
</body>
</html>

