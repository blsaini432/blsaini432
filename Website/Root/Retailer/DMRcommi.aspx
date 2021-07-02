<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="DMRcommi.aspx.cs" Inherits="Root_Retailer_DMRcommi" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .sss td:first-child {
            min-width: 30% !important;
        }


        .sss td:nth-child(2) {
            min-width: 2% !important;
        }

        .ttt td:first-child {
            min-width: 30% !important;
        }

        .ttt td:nth-child(2) {
            min-width: 30% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title"> DMR Commission
            </h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row grid-margin">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body">
                                
                                
                                <div class="table-responsive">
                                    <asp:GridView ID="gvOperator" runat="server" 
                                CssClass="table table-bordered table-hover tablesorter" AutoGenerateColumns="false"
                                DataKeyNames="id" Width="100%" ShowHeaderWhenEmpty="true" 
                                onrowcommand="gvOperator_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="StartRange">
                                        <ItemTemplate>
                                            <%# Eval("startval") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EndRange">
                                        <ItemTemplate>
                                            <%# Eval("endval") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Surcharge">
                                        <ItemTemplate>
                                            <%# Eval("surcharge") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Is Flat">
                                        <ItemTemplate>
                                            <%# Convert.ToString(Eval("IsFlat")) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="EmptyDataTemplate">
                                        No Record Found !</div>
                                </EmptyDataTemplate>
                                <RowStyle CssClass="RowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                                <HeaderStyle CssClass="HeaderStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <PagerSettings Position="Bottom" />
                            </asp:GridView>
                                </div>
                                <div class="form-group row" id="dvbutton" runat="server">
                                    <div class="col-lg-3">
                                    </div>
                                   
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
           <Triggers>
           
        </Triggers>
        </asp:UpdatePanel>
    </div>

</asp:Content>