<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="payonclick.aspx.cs" Inherits="Root_Distributor_payonclick" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="payonclick.aspx.cs" Inherits="Root_Distributor_payonclick" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <style>
    .copyright-title {
	padding-top: 126px;
}
    .payclick-submit-button {
	position: absolute;
	margin-left: 50%;
	margin-top: 90px;
}
    h3 {
	padding-left: 50%;
}
        </style>
    <form method="post" name="form1" action="https://www.easypolicy.com/epnew/Landing/Landing.aspx">
        <h1>INSURANCE</h1>
        <div class="formcontainer">
            <hr />
            <div class="container">
                <label for="hidden"><strong></strong></label>
                <input type="hidden" id="utm_source" name="utm_source" value="Payonclick">

                <label for="hidden"><strong></strong></label>
                <input type="hidden" name="utm_medium" value="Payonclick">
                <label for="hidden"><strong></strong></label>
                <input type="hidden" name="utm_campaign" value="Payonclick">
                <label for="hidden"><strong></strong></label>
                <input type="hidden" name="utm_term" value="Payonclick">
                <label for="hidden"><strong></strong></label>
                <input type="hidden" name="partnerleadid" value="<%=ViewState["id"] %>">
                <label for="hidden"><strong></strong></label>
                <input type="hidden" name="partneragentid" value="<%=ViewState["MemberId"] %>">
            </div>
            <h3>Insurance</h3>
            <input name="btnGetQuoteWidget" id="btnGetQuoteWidget" value="Submit" type="submit" class="payclick-submit-button">
        </div>
    </form>
   </asp:Content>
