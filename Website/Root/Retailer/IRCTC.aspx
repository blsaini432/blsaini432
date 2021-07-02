<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="IRCTC.aspx.cs" Inherits="Root_Retailer_CarInsurance " %>

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
            <h3 class="page-title">
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
                                          <h3>Coming Soon</h3>
                                    </div>
                                   <div class="col-lg-3">
                                       <img src="../../Uploads/User/Profile/downyload.png" />
                                </div>
                            </div>
                        </div>
                       
                    </div>
                </div>
                </div>
            </ContentTemplate>

        </asp:UpdatePanel>
</asp:Content>
