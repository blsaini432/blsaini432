<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="EMI.aspx.cs" Inherits="Root_Admin_EMI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../Design/js/angular.min.js"></script>
    <script src="../Angularjsapp/dirPagination.js"></script>
    <script src="../Angularjsapp/distributorapp.js"></script>
</asp:Content>
           <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div class="content-wrapper">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row grid-margin">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body">
                                 <div class="form-group row">
                                   
                                       <a href="https://www.zestmoney.in/partner/amazon/" target="_blank">
                                    <img src="../../Uploads/amazonstore/img/amazonemi.jpg"  height="400px" width="100%"/>
                                   
                                     
                                </div>
                                </div>
                            </div>
                        </div>
                    </div>
              
                
            </ContentTemplate>

        </asp:UpdatePanel>
        </div>
</asp:Content>


