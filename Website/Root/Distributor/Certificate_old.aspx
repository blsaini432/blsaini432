

<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="Certificate_old.aspx.cs" Inherits="Root_Retailer_Certificate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:content id="Content1" contentplaceholderid="head" runat="Server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="Server">
<div class="certificate-section">
  <div class="container">
    <div class="row">
      <div class="caertficate-header">
        <div class="col-md-6"></div>
        <div class="col-lg-3 col-md-3 col-sm-6">
          <div class="logo-left">  <asp:Image id="img1" runat="server"/> </div>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6">
          <div class="logo-right">
            <p class="logo-title">Supported By</p>
            <div class="logo-img"> <img src="../../new/img/icici_logo.png"> <img src="../../new/img/paytm_logo.png"> <img src="../../new/img/yesbank_logo.png"> </div>
            <p class="logo-title">Partnership With By</p>
            <p class="logo-title">Ezulix Software Pvt. Ltd.</p>
          </div>
        </div>
      </div>
      <div class="certificate-detail-bar">
        <div class="col-md-12">
          <div class="heading-title">
            <h1>CERTIFICATE</h1>
            <p>OF REGISTRATION</p>
          </div>
          <div class="presented-title">
            <p>THIS CERTIFICATE IS PRESENTED TO</p>
          </div>
          <div class="holder-name">
            <asp:Label ID="lblname" runat="server" ></asp:Label>
          </div>
        </div>
        <div class="member-detail">
          <div class="col-md-3">
            <div class="member-wrap member-id"> <span class="detail-static">MEMBER ID - </span> <span class="detail-dainemic"><asp:Label ID="lblmemberid" runat="server" ></asp:Label></span> </div>
          </div>
          <div class="col-md-9">
            <div class="member-wrap  member-mobile"> <span class="detail-static">MOBILE - </span> <span class="detail-dainemic"><asp:Label ID="lblmobile" runat="server" ></asp:Label></span> </div>
          </div>
        </div>
        <div class="text-blog">
          <div class="col-md-12">
            <p>is a registered retail partner of <strong>PAYDEER - </strong> a unit of <strong>CYBDEER NETWORK PVT LTD</strong> and also authorized for doing business of Digital Financial servie vehalf of itself at</p>
            <p><strong>Resistered Location :-</strong> <asp:Label ID="lbladdress" runat="server"></asp:Label></p>
            <p><strong>Vallidity : - </strong> Till Termination Date </p>
          </div>
        </div>
        <div class="certificate-footer">
          <div class="col-md-4">
            <div class="date-opstion">
              <asp:Label ID="lbldateofissue" runat="server"></asp:Label>
              <span class="date-title">DATE</span> </div>
          </div>
          <div class="col-md-4">
            <div class="date-opstion">
              <p>20-07-2020</p>
              <span class="date-title">SIGNATURE</span> </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>
    </asp:content>

