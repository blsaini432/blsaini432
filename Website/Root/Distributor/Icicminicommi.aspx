<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="Icicminicommi.aspx.cs" Inherits="Root_Distributor_Icicminicommi" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">
               Mini Statement Commission
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
                               
                            </div>
                           
                        </div>
                        </div></div></div>
            
        </div><div class="col-12 grid-margin stretch-card">
              <div class="card">
                <div class="card-body">
                  <h4 class="card-title"> Mini Statement Commission</h4>
                     <asp:GridView ID="GridView1" runat="server" CssClass="table">
                         <EmptyDataTemplate>
                             No Data Available
                         </EmptyDataTemplate>
                     </asp:GridView>
                    </div></div></div>
        </ContentTemplate></asp:UpdatePanel>
       </div>
</asp:Content>


