<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="Vistore.aspx.cs" Inherits="Root_Retailer_Vistore" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Vi Store
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
                                    <div class="col-lg-8">

                                          <label class="col-form-label" style="color: green"></label>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Vi store <code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="ddlservice" runat="server" Height="35px" CssClass="form-control" AutoPostBack="true">
                                            <asp:ListItem>Select Product</asp:ListItem>
                                            <asp:ListItem>MORPHO DEVICE</asp:ListItem>
                                            <asp:ListItem>MANTRA  DEVICE</asp:ListItem>
                                            <asp:ListItem>KEYBORD</asp:ListItem>
                                            <asp:ListItem>MOUSE</asp:ListItem>
                                            <asp:ListItem>VGA CABLE</asp:ListItem>
                                            <asp:ListItem>POWER CABLE</asp:ListItem>
                                            <asp:ListItem>PRINTER</asp:ListItem>
                                            <asp:ListItem>ANTI VIRUS</asp:ListItem>
                                            <asp:ListItem>RAM</asp:ListItem>
                                            <asp:ListItem>USB HUB</asp:ListItem>
                                            <asp:ListItem>WIFI ADAPTOR</asp:ListItem>
                                            <asp:ListItem>PRINTER CABLE</asp:ListItem>
                                            <asp:ListItem>MOTHER BOARD</asp:ListItem>
                                            <asp:ListItem>HDD</asp:ListItem>
                                            <asp:ListItem>CASH COUNTING MACHINE</asp:ListItem>
                                            <asp:ListItem>DVD WRITER</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                </div>

                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                                            ValidationGroup="vgBeni" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>

                <asp:PostBackTrigger ControlID="btnSubmit" />
            </Triggers>
        </asp:UpdatePanel>



    </div>
</asp:Content>

