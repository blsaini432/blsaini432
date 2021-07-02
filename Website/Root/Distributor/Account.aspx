<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="Accountopen.aspx.cs" Inherits="Root_Distributor_Accountopen " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .modalPopup {
            background-color: #FFFFFF;
            border-width: 1px;
            border-style: solid;
            border-color: black;
            padding-left: 10px;
            width: 270px;
            height: 185px;
        }
    </style>
    <script type="text/javascript">
        function confirmation() {
            if (confirm('are you sure you want to Submit Data?')) {
                return true;
            } else {
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function convertTimestamptoTime() {
            var timestamp = Math.round((new Date()).getTime() / 1000);
            alert(timestamp);
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Account Opening
            </h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row grid-margin">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body">
                               <div class="form-group row">
                                    <div class="col-lg-3">
                                         <td colspan="2">SBI Bank - : </td>
                                    </div>
                                    <div class="col-lg-8">
                                       <a href="https://www.sbiyono.sbi/wps/portal/accountopening/digital-account#!/aoCustomerOpenOVD" target="_blank">Click Here</a>
                                    </div>
                                </div>
                                 <div class="form-group row">
                                    <div class="col-lg-3">
                                         <td colspan="2">YES Bank - : </td>
                                    </div>
                                    <div class="col-lg-8">
                                       <a href="https://yesaim.yesbank.in/yesaimweb/#!/mobileNo" target="_blank">Click Here</a>
                                    </div>
                                </div>
                                 <div class="form-group row">
                                    <div class="col-lg-3">
                                         <td colspan="2">Bank of Baroda - : </td>
                                    </div>
                                    <div class="col-lg-8">
                                       <a href="https://tabit.bankofbaroda.com/BarodaInstaClick/#/savings/registration" target="_blank">Click Here</a>
                                    </div>
                                </div>
                                 <div class="form-group row">
                                    <div class="col-lg-3">
                                         <td colspan="2">Axis Bank - : </td>
                                    </div>
                                    <div class="col-lg-8">
                                       <a href="https://leap.axisbank.com/?cta=savings-listingpage-banner&_ga=2.229919772.108282377.1595418911-2056579457.158" target="_blank">Click Here</a>
                                    </div>
                                </div>
                                 <div class="form-group row">
                                    <div class="col-lg-3">
                                         <td colspan="2">Rbl bank - : </td>
                                    </div>
                                    <div class="col-lg-8">
                                       <a href="https://abacus.rblbank.com/DigiAqui/#/welcome?LeadSource=Website-hp-abacus-spot1&website-banner=hp-abacus-spot1&itm_campaign=Digital-Sav-Acc&itm_medium=Website&itm_source=hp-abacus-spot1" target="_blank">Click Here</a>
                                    </div>
                                </div>
                                 
                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </ContentTemplate>

        </asp:UpdatePanel>
        </div>
</asp:Content>
