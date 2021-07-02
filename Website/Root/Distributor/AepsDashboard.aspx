<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="AepsDashboard.aspx.cs" Inherits="Root_Distributor_AepsDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .divWaiting {
            position: fixed;
            background-color: #FAFAFA;
            z-index: 2147483647 !important;
            opacity: 0.8;
            overflow: hidden;
            text-align: center;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
            padding-top: 20%;
        }

        #ctl00_ContentPlaceHolder1_Rb_Device td, #ctl00_ContentPlaceHolder1_Rb_Bank td {
            width: 130px;
        }
    </style>
    <script src="../../Design/js/jquery.min.js" type="text/javascript"></script>
    <script>
        $(document).on('click', '._txnhistory', function () {
            window.location = "AepsNewTranscation.aspx";
        });
        $(document).on('click', '._downloaddriver', function () {
            var Uri = location.origin + "/EKYC_DRIVERS.zip";
            window.location = Uri;
        });
        $(document).on('click', '._payout', function () {
            window.location = "AepsPayOut.aspx";
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">AEPS Panel
            </h3>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-body">
                        <asp:Repeater runat="server" ID="repeater1">
                            <ItemTemplate>

                                <li data-transition="slideright" data-slotamount="1" data-masterspeed="1000" data-delay="4000" data-saveperformance="off">
                                    <asp:Image ID="myCarousel" ImageUrl='<%#"../../Uploads/Company/BackBanner/actual/"+ Eval("BannerImage")%>' runat="server" Width="100%" Height="300px" />
                                </li>

                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <div class="deshboard-form-section">
                            <div class="row">
                                <div class="col-3">
                                </div>
                                <div class="col-9">
                                    <img src="../../Uploads/Yeslogo.jpg" class="img-responsive" style="padding-left: 525px;" />
                                </div>
                            </div>
                            <div class="deshboard-form-menu">
                                <div class="container">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <ul class="nav nav-pills nav-pills-success" id="pills-tab" role="tablist">
                                                <li class="nav-item">
                                                    <a class="nav-link active" id="pills-home-tab" data-toggle="pill" href="#pills-home" role="tab" aria-controls="pills-home" aria-selected="true">Aeps Dashboard</a>
                                                </li>
                                                <li class="nav-item">
                                                    <a class="_downloaddriver nav-link" id="pills-profile-tab" data-toggle="pill" href="#pills-profile" role="tab" aria-controls="pills-profile" aria-selected="false">Biomatric Drivers</a>
                                                </li>
                                                <li class="nav-item">
                                                    <a class=" _payout nav-link" id="pills-contact-tab" data-toggle="pill" href="#pills-contact" role="tab" aria-controls="pills-contact" aria-selected="false">Aeps Pay Out</a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-panel-section">
                                <div class="container" style="margin-top: 50px; width: 100%">
                                    <div class="row grid-margin table-responsive">
                                        <div class="col-md-12 grid-margin stretch-card">
                                            <div class="card">
                                                <div class="card-body table-responsive">
                                                    <ul class="nav nav-tabs customtab2" role="tablist">
                                                        <asp:RadioButtonList ID="Rb_ServiceType" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="Rb_ServiceType_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="11" Text="YBL Aeps Withdrawal"></asp:ListItem>
                                                            <asp:ListItem Value="10" Text="YBL Aeps Balance Enquiry"></asp:ListItem>
                                                            <asp:ListItem Value="003" Text="Card Transcation "></asp:ListItem>
                                                            <asp:ListItem Value="004" Text="Card Balance Enquiry"></asp:ListItem>
                                                            <asp:ListItem Value="001" Text="Fino Aeps Withdrawal"></asp:ListItem>
                                                            <asp:ListItem Value="002" Text="Fino Aeps Balance Enquiry"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </ul>
                                                    <div class="tab-content tabcontent-border">
                                                        <div class="tab-pane active" id="home" role="tabpanel" aria-expanded="true">
                                                            <div class="pad-20">
                                                                <div class="form-group row">
                                                                    <div class="col-lg-3">
                                                                        <label class="col-form-label">Mobile:<code>*</code></label>
                                                                    </div>
                                                                    <div class="col-lg-8">
                                                                        <asp:TextBox ID="Txt_Mobile" runat="server" placeholder="Mobile Number" MaxLength="10" class="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="Ftbe_Mobile" runat="server" TargetControlID="Txt_Mobile" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                                                                        <asp:RequiredFieldValidator ID="Rfv_Mobile" runat="server" ControlToValidate="Txt_Mobile" ForeColor="Red" ErrorMessage="Mobile number required" ValidationGroup="Aeps"></asp:RequiredFieldValidator>

                                                                    </div>
                                                                </div>

                                                                <%--                                             <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Name:<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                 <asp:TextBox ID="Txt_Name" runat="server" placeholder="Name" class="form-control" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="Ftbe_Name" runat="server"
                                                            TargetControlID="Txt_Name" ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                                        </cc1:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="Rfv_Name" runat="server" ControlToValidate="Txt_Name" ForeColor="Red" ErrorMessage="Name required" ValidationGroup="Aeps"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="Txt_Name" ID="Rev_Name" ValidationExpression="^[\s\S]{5,35}$" runat="server" ErrorMessage="Minimum 5 and Maximum 35 characters."></asp:RegularExpressionValidator>
                                 <asp:Button ID="btn_Register" runat="server" Text="Register Custmer" Class="btn btn-success" ValidationGroup="Aeps" Style="height: 36px" Visible="false" OnClick="btn_Register_Click" />

                            </div>
                        </div>--%>
                                                                <div class="form-group row">
                                                                    <div class="col-lg-3">
                                                                        <label class="col-form-label">Amount:<code>*</code></label>
                                                                    </div>
                                                                    <div class="col-lg-8">
                                                                        <asp:TextBox ID="Txt_Amount" runat="server" class="form-control" placeholder="Amount" MaxLength="5"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="Ftbe_Amount" runat="server" TargetControlID="Txt_Amount" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                                                                        <asp:RangeValidator
                                                                            ID="RangeValidator_Amount"
                                                                            runat="server"
                                                                            ControlToValidate="Txt_Amount"
                                                                            Type="Integer"
                                                                            MinimumValue="101"
                                                                            MaximumValue="10000"
                                                                            ErrorMessage="Amount between or equal 101 to 10000" ValidationGroup="Aeps">
                                                                        </asp:RangeValidator>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <div class="col-lg-3">
                                                                    </div>
                                                                    <div class="col-lg-8">
                                                                        <asp:Button ID="Btn_Scan" runat="server" Text="Submit" Class="btn btn-primary" ValidationGroup="Aeps" OnClick="Btn_Scan_Click" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%-- Loader --%>
                            <div class="divWaiting" id="Dv_Loader" style="display: none;">
                                <img src="../../flight/Images/flight/progressbar.gif" />
                            </div>
                            <%-- Loader --%>
                        </div>


                    </div>
                </div>
            </div>
        </div>

    </div>













</asp:Content>
