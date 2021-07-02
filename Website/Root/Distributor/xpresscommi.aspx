<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="xpresscommi.aspx.cs" Inherits="Root_Distributor_xpresscommi" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Manage DMR Commission
            </h3>
        </div>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row grid-margin">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body">
                              
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





