<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="Purchageservice.aspx.cs" Inherits="Root_Distributor_Purchageservice " %>

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
            <h3 class="page-title">Purchage Services
            </h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row grid-margin">
                    <div class="col-12 card">                
                            <div class="card-body row">                            
                                    <div class="col-lg-2">
                                        <div class="form-group">
                                            <label class="col-form-label">Select Service<code>*</code></label>
                                        </div>
                                         </div>
                                        <div class="col-lg-3">
                                            <asp:DropDownList ID="ddlservice" runat="server" Height="35px" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_Eboard_SelectedIndexChanged">
                                                <asp:ListItem>Select Service Type</asp:ListItem>
                                                <asp:ListItem>Recharge</asp:ListItem>
                                                <asp:ListItem>DMR</asp:ListItem>
                                                <asp:ListItem>AEPS</asp:ListItem>
                                                <asp:ListItem>BBPS NEW</asp:ListItem>
                                                <asp:ListItem>X-PRESS DMR</asp:ListItem>
                                                <asp:ListItem>UTI</asp:ListItem>                                            
                                            </asp:DropDownList>
                                        </div>                                                                       
                                        <div class="col-md-6">
                                    <div id="tr_service" runat="server" visible="false">
                                        <div class="col-md-9">
                                            <div class="form-group" style="color:green">
                                                This Service Purchage Amount
                                              <asp:Label ID="lbl_servicetag" runat="server"></asp:Label><code></code
                                            </div>
                                          </div>
                                        <div class="col-lg-3">
                                            <asp:Button ID="Button1" runat="server" Text="Pay Now" OnClick="btnSubmit_Click" ValidationGroup="v0" class="btn btn-primary" />                                               
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
