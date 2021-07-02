<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="pasfeecommi.aspx.cs" Inherits="Root_Retailer_pasfeecommi" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">
              
            </h3>
        </div>

            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
       <div class="col-12 grid-margin stretch-card">
              <div class="card">
                <div class="card-body">
                  <h4 class="card-title"> UTI Coupon Fees</h4>
                     <asp:GridView ID="GridView1" runat="server" CssClass="table">
                         <EmptyDataTemplate>
                             No Data Available
                         </EmptyDataTemplate>
                     </asp:GridView>
                    </div></div></div>
        </ContentTemplate></asp:UpdatePanel>
       </div>
</asp:Content>