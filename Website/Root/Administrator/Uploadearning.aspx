<%@ Page Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true" CodeFile="Uploadearning.aspx.cs" Inherits="Root_Administrator_Uploadearning" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../Design/js/angular.min.js"></script>
    <script src="../Angularjsapp/dirPagination.js"></script>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Amazon Earning
            </h3>
        </div>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <div class="container" ng-app="myApp" ng-controller="myCntrl">
                            <div class="info-box">

                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group row">
                                            <asp:Button ID="btn_export" runat="server" OnClick="btn_export_Click" Text="Download Total Earning Data" CssClass="btn btn-dribbble" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group row">
                                            <a href="../../Uploads/UploadFile/earning.xlsx"  class="btn btn-primary">Download Upload Earning Formate</a>
                                            
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group row">
                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="btn btn-info" />
                                            <label style="color: red; margin-top: 10px;"><b>Note: Must Upload in .xlsx Format</b></label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group row">
                                        <asp:Button Text="Upload" OnClick="Upload" runat="server" CssClass="btn btn-success" />
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

</asp:Content>
