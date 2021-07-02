<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="rechargecommi.aspx.cs" Inherits="Root_Retailer_rechargecommi" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">
                 Recharge Commission
            </h3>
        </div>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row grid-margin">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body">
                            
                                <div class="table-responsive">
                                <asp:GridView ID="gvOperator" runat="server" CssClass="table" AutoGenerateColumns="false"
                                    DataKeyNames="OperatorID" Width="100%" ShowHeaderWhenEmpty="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S No.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Operator Name" DataField="OperatorName" SortExpression="OperatorName" />
                                        <asp:BoundField HeaderText="Service Type" DataField="ServiceType" SortExpression="ServiceType" />
                                        <asp:BoundField HeaderText="Commission" DataField="Commission" SortExpression="ServiceType" />
                                        
                                       
                                    </Columns>
                                    <EmptyDataTemplate>
                                      No Record Found!!
                                    </EmptyDataTemplate>
                                </asp:GridView></div>
                                <div class="form-group row">
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












