<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="BBPS_Electricity_New.aspx.cs" Inherits="Root_Distributor_BBPS_Electricity_New" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="../../Design/css/bbps_module.css">
    <script type="text/javascript">
        //        function pageLoad() {
        //            var manager = window.Sys.WebForms.PageRequestManager.getInstance();
        //            manager.add_endRequest(endRequest);
        //            manager.add_beginRequest(OnBeginRequest);
        //        }

        function OnBeginRequest() {
            window.$get('UpProgress').style.display = "block";
        }

        function endRequest() {
            window.$get('UpProgress').style.display = "none";
        }
    </script>
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper bg_gray">
        <!-- BBPS Logo section---->
        <div class="bbps-logo-section">
            <div class="row">
                <div class="bbps-logo"><a href="#">
                    <img src="../../Design/images/bbps_module/bbps_logo.png"></a></div>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel_Home" runat="server">
            <ContentTemplate>
                <!-- BBPS Module-3---->
                <div class="bill-form-section">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="service-title">
                                <h3>Home/Recharge & Bill Payments</h3>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="single-service-wrap">
                                <div class="single-service-form-detail">
                                    <div class="form-group text-input-fild">
                                        <label for="Electricity" class="control-label col-md-4 col-sm-12 col-xs-12">Category</label>
                                        <div class="col-md-8 col-sm-12 col-xs-12">
                                            <asp:DropDownList ID="ddl_Catgory" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_Catgory_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" ValidationGroup="getbill" ControlToValidate="ddl_Catgory"></asp:RequiredFieldValidator>
                                            <samp class="bar"></samp>
                                        </div>
                                    </div>
                                    <div class="form-group text-input-fild">
                                        <label for="Electricity" class="control-label col-md-4 col-sm-12 col-xs-12">Board</label>
                                        <div class="col-md-8 col-sm-12 col-xs-12">
                                            <asp:DropDownList ID="ddl_Eboard" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_Eboard_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfv_Eboard" runat="server" InitialValue="0" ValidationGroup="getbill" ControlToValidate="ddl_Eboard"></asp:RequiredFieldValidator>
                                            <samp class="bar"></samp>
                                        </div>
                                    </div>
                                    <div class="form-group text-input-fild pd-no" id="divdivsion" runat="server" style="display: none;">
                                        <label for="Subdivison" class="control-label col-md-4 col-sm-12 col-xs-12">Sub Division Board</label>
                                        <div class="col-md-8 col-sm-12 col-xs-12">
                                            <asp:DropDownList ID="ddl_subdivison" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                            <samp class="bar"></samp>
                                        </div>
                                    </div>
                                    <div class="form-group text-input-fild pd-no">
                                        <asp:Label ID="lblconsumerno" runat="server" CssClass="control-label col-md-4 col-sm-12 col-xs-12" Text="Consumer Number"></asp:Label>

                                        <div class="col-md-6 col-sm-12 col-xs-12">
                                            <asp:TextBox ID="txt_servicenum" runat="server" CssClass="form-control" Style="text-transform: uppercase" placeholder="Enter Here"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv_servicenum" runat="server" ValidationGroup="getbill" ErrorMessage="Enter Service Number"
                                                ControlToValidate="txt_servicenum"></asp:RequiredFieldValidator>


                                            <samp class="bar"></samp>
                                        </div>
                                    </div>
                                    <div class="form-group text-input-fild pd-no" id="displaymobile" runat="server">
                                        <label for="Mobile Number" class="control-label col-md-4 col-sm-12 col-xs-12">Mobile No. </label>
                                        <div class="col-md-8 col-sm-12 col-xs-12">
                                            <asp:TextBox ID="txt_customermobile" runat="server" CssClass="form-control" Style="text-transform: uppercase" placeholder="Enter Mobile Number"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="rfv_Mobile" runat="server" CssClass="rfv" ControlToValidate="txt_customermobile"
                                                ErrorMessage="Please Enter Mobile No. !" ValidationGroup="getbill" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Mobile" runat="server" TargetControlID="txt_customermobile"
                                                ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:RegularExpressionValidator ID="regExp_Mobile" runat="server" CssClass="rfv"
                                                ControlToValidate="txt_customermobile" ErrorMessage="Input correct mobile number !" ValidationGroup="getbill"
                                                SetFocusOnError="true" ValidationExpression="^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[6789]\d{9}$"></asp:RegularExpressionValidator>
                                            <samp class="bar"></samp>
                                        </div>
                                    </div>

                                    <div class="form-group text-input-fild pd-no" id="divbillamount" runat="server" style="display: none;">
                                        <label for="Mobile Number" class="control-label col-md-4 col-sm-12 col-xs-12">Bill Amount</label>
                                        <div class="col-md-8 col-sm-12 col-xs-12">
                                            <asp:TextBox ID="txt_amount" runat="server" CssClass="form-control" Style="text-transform: uppercase" placeholder="Enter Bill Amount"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_amount"
                                                ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>

                                            <samp class="bar"></samp>
                                        </div>
                                    </div>
                                    <div class="form-group text-input-fild pd-no">
                                        <div class=" col-md-4 col-sm-4 col-xs-12"></div>
                                        <div class="submit-button single-service-submit fetch-button">
                                            <asp:Button ID="btn_fetchfill" runat="server" Text="Fetch Details" OnClick="btn_fetchfill_Click" CssClass="btn btn-default" ValidationGroup="getbill" Visible="false" />

                                            <asp:Button ID="Button1" runat="server" Text="Pay Bill " OnClick="Button1_Click" CssClass="btn btn-default" ValidationGroup="getbill" Visible="false" />
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_fetchfill" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpProgress" runat="server" style="height: 100%; width: 100%;">
            <ProgressTemplate>
                <div class="divWaiting">
                    <div id="progressBackgroundFilter">
                    </div>
                    <div id="processMessage">
                        <div class="loading_page" align="center" style="background-color: White; vertical-align: middle">
                            <img src="../../Design/images/pageloader.gif" alt="Loading..." style="float: none" />&#160;Please
                        Wait...
                        </div>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <!-- content-wrapper ends -->
    <!-- partial:partials/_footer.html -->

    <!-- partial -->

</asp:Content>

