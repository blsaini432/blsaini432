<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="Newpaper.aspx.cs" Inherits="Root_Retailer_Newpaper " %>

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
            <h3 class="page-title">News Papers
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
                                          <td colspan="2">Times of india  :</td>
                                    </div>
                                    <div class="col-lg-8">
                                     <a href="https://timesofindia.indiatimes.com/" target="_blank">Times of india paper</a>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                         <td colspan="2">Aaj Tak News :</td>
                                    </div>
                                    <div class="col-lg-8">
                                       <a href="https://aajtak.intoday.in/" target="_blank">Aaj Tak news paper</a>
                                    </div>
                                </div>
                               <div class="form-group row">
                                    <div class="col-lg-3">
                                         <td colspan="2">Jagran  : </td>
                                    </div>
                                    <div class="col-lg-8">
                                       <a href="https://www.jagran.com/state/west-bengal" target="_blank">Jagran News Paper</a>
                                    </div>
                                </div>
                                 <div class="form-group row">
                                    <div class="col-lg-3">
                                         <td colspan="2">Ananda bajar : </td>
                                    </div>
                                    <div class="col-lg-8">
                                       <a href="https://www.anandabazar.com/" target="_blank">Ananda bajar </a>
                                    </div>
                                </div>
                                 <div class="form-group row">
                                    <div class="col-lg-3">
                                         <td colspan="2">Samaja paper: </td>
                                    </div>
                                    <div class="col-lg-8">
                                       <a href="https://samajaepaper.in/" target="_blank">Samaja paper</a>
                                    </div>
                                </div>
                               
                               

                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </ContentTemplate>

        </asp:UpdatePanel>
</asp:Content>