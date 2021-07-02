﻿<%@ Page Title="" Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true" CodeFile="BBPSSettings.aspx.cs" Inherits="Portals_Admin_BBPSSettings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">
                Manage BBPS Commission Setting
            </h3>
        </div>
     
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                             <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Service<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                            <asp:DropDownList ID="ddl_service" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddl_service"
                                Display="Dynamic" ErrorMessage="Please select service !" SetFocusOnError="True"
                                ValidationGroup="v" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Package Name<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                    <asp:DropDownList ID="ddlPackage" runat="server" CssClass="form-control" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlPackage_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvPackage" runat="server" ControlToValidate="ddlPackage"
                                Display="Dynamic" ErrorMessage="Please select Package !" SetFocusOnError="True"
                                ValidationGroup="v" InitialValue="0"></asp:RequiredFieldValidator>

                            </div>
                        </div>
                            <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Commission Amount<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                            <asp:TextBox ID="txtcomission" runat="server" CssClass="form-control" Text="0.00"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcomission"
                                Display="Dynamic" ErrorMessage="Please Enter Amount!" ForeColor="Red" InitialValue="0"
                                SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                            <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">Flat/Percentage<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                          <asp:CheckBox ID="chkflat" runat="server" Text="Flat/Percentage" AutoPostBack="true" />
                          
                            </div>
                        </div>
                                      <div class="form-group row">
                            <div class="col-lg-3">
                               
                            </div>
                            <div class="col-lg-8">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-danger" />
                            </div>
                        </div>
                        </div></div></div>
            
        </div><div class="col-12 grid-margin stretch-card">
              <div class="card">
                <div class="card-body">
                  <h4 class="card-title"> BBPS Fees</h4>
                     <asp:GridView ID="GridView1" runat="server" CssClass="table">
                         <EmptyDataTemplate>
                             No Data Available
                         </EmptyDataTemplate>
                     </asp:GridView>
                    </div></div></div>
       
       </div>
</asp:Content>