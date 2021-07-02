<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="Certificate.aspx.cs" Inherits="Root_Distributor_Certificate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:content id="Content1" contentplaceholderid="head" runat="Server">
<style>
body {max-width:100%;padding:0;margin:0;height:auto;width:auto;text-align:center !important;}
.container {width:1026px !important;padding:0 !important;}
.img-banner {background: url(../../certificateback.png);no-repeat;height: 670px;padding: 0 50px;margin-top: 30px;}
.text-area-table {width: 100%;max-width: 100%;margin-bottom: 20px;margin-right: 50px !important;padding: 0 50px;display: block;margin-bottom: 0 !important;}
tr td {border:none !important;}
.top-section {width: 100px;padding-top: 52px;}
.logo-section {width: 200px;}
.logo {width: 170px;margin-top: 50px;}
.heading h1 {margin-right: 40px;margin-top: 20px;font-size: 24px;font-weight: 600;}
.agent-section {width: 100%;display: flow-root;}
.top-left {float: left;/* width: 44%; */margin-left: 50px;}
.top-left h2 {font-size: 20px;font-weight: 600;}
.top-right h2 {font-size: 20px;font-weight: 600;}
.text-area h3 {font-size: 18px;font-weight: 600;text-align: center;margin-right: 29px;}
.text-left {width: 50%;text-align: left;}
.text-right {width: 50%;text-align: left;}
.text-area-table p {margin: 4px 0;font-weight: 600;font-size: 18px;color: #000;}
.footer p {float: right;padding-right: 50px;font-size: 18px;}
.bootom-p {font-weight: 600;font-size: 18px;text-align: left;padding-left: 50px;color: #000;}
 @media (min-width: 768px) {
 .container {max-width: 1026px !important;width:100%;}}
</style>
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="Server">
   
  <div class="container">
    <div class="img-banner">
      <div class="row">
        <div class="col-md-12 col-sm-6 col-xs-12">
          <div class="logo-section"> <asp:Image id="img1" runat="server" class="top-section"/> </div>
        </div>
      </div>
      <div class="row">
        <div class="col-md-12 col-xs-12">
          <div class="heading">
            <h1>Certificate of Authorization</h1>
          </div>
        </div>
      </div>
      <div class="agent-section">
        <div class="row">
          <div class="col-md-6 col-sm-6 col-xs-12">
            <div class="top-left">
              <h2>Agent Code : <asp:Label ID="lblagentcode" runat="server" Text="" Font-Bold="true"></asp:Label></h2>

            </div>
          </div>
          <div class="col-md-6 col-sm-6 col-xs-12">
            <div class="top-right">
              <h2>Date of Issuance :<asp:Label ID="lbldateofissue" runat="server" Text="" Font-Bold="true"></asp:Label></h2>
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
                  <td class="text-left name"><p>M/s <asp:Label ID="lblname" runat="server" Text="" Font-Bold="true"></asp:Label></p></td>
                     <td class="text-left address"><p>Address <asp:Label ID="lbladdress" runat="server" Text="" Font-Bold="true"></asp:Label></p></td>
                 
                </tr>
                <tr>
                 
                  <td class="text-right authorized"><p>is an authorized <asp:Label ID="MemberType" runat="server" Text="" Font-Bold="true"></asp:Label></p></td>
                </tr>
        
              </tbody>
            </table>
              <div class="bootom-p">
              <p>Of <asp:Label ID="lblcompanyname" runat="server" Text="" Font-Bold="true"></asp:Label></p>
            </div>
            <div class="bootom-p">
              <p>and authorized to conduct Business on its Behalf with effect from thedate of Issuance.</p>
            </div>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-md-12">
          <div class="footer">
            <p>Auth. Signatory<br>
              <asp:Label ID="lblcompanynm" runat="server" Text="" Font-Bold="true"></asp:Label></p>
          </div>
        </div>
      </div>
    </div>
  </div>

    <p>
&nbsp; <asp:Button ID="btnExport" runat="server" Text="Print" Visible="false"
            onclick="btnExport_Click" /></p>
  
    </asp:content>
