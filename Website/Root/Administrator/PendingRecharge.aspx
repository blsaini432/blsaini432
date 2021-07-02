<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true"
    CodeFile="PendingRecharge.aspx.cs" Inherits="Root_Admin_PendingRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $("[id*=chkHeader]").live("click", function () {
            var chkHeader = $(this);
            var grid = $(this).closest("table");
            $("input[type=checkbox]", grid).each(function () {
                if (chkHeader.is(":checked")) {
                    $(this).attr("checked", "checked");
                    $("td", $(this).closest("tr")).addClass("selected");
                } else {
                    $(this).removeAttr("checked");
                    $("td", $(this).closest("tr")).removeClass("selected");
                }
            });
        });
        $("[id*=chkRow]").live("click", function () {
            var grid = $(this).closest("table");
            var chkHeader = $("[id*=chkHeader]", grid);
            if (!$(this).is(":checked")) {
                $("td", $(this).closest("tr")).removeClass("selected");
                chkHeader.removeAttr("checked");
            } else {
                $("td", $(this).closest("tr")).addClass("selected");
                if ($("[id*=chkRow]", grid).length == $("[id*=chkRow]:checked", grid).length) {
                    chkHeader.attr("checked", "checked");
                }
            }
        });
    </script>
    <style type="text/css">
        .selected
        {
            background-color: #A1DCF2;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Pending Recharge History
            </h3>
        </div>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="loading-overlay">
                <div class="wrapper">
                    <div class="ajax-loader-outer">
                   <img src="../../Design/images/pageloader.gif" />
                    </div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>    
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            
        <ContentTemplate>
            <asp:Button ID="btnCheckStatus" runat="server" Text="Check Status" OnClick="btnCheckStatus_Click" class="btn btn-primary" />
         <div class="table-responsive">

                               <asp:GridView ID="gvHistory" runat="server" CssClass="table  table-bordered bootstrap-datatable datatable responsive " 
                            AutoGenerateColumns="false"
                                AllowPaging="false" DataKeyNames="HistoryID" OnPageIndexChanging="gvHistory_PageIndexChanging"
                                PageSize="10" Width="100%" OnSorting="gvHistory_Sorting" AllowSorting="false"
                                ShowHeaderWhenEmpty="true" onrowcommand="gvHistory_RowCommand" 
                                    onrowcreated="gvHistory_RowCreated">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkHeader" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkRow" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="S No" DataField="SerialNo" SortExpression="SerialNo" />
                                    <asp:BoundField HeaderText="MemberID" DataField="MemberID" SortExpression="MemberID" />
                                    <asp:BoundField HeaderText="MobileNo" DataField="MobileNo" SortExpression="MobileNo" />
                                    <asp:BoundField HeaderText="Amount" DataField="RechargeAmount" SortExpression="RechargeAmount" />
                                    <asp:BoundField HeaderText="OperatorName" DataField="OperatorName" SortExpression="OperatorName" />
                                    <asp:BoundField HeaderText="ServiceType" DataField="ServiceType" SortExpression="ServiceType" />
                                    <asp:BoundField HeaderText="Tx ID" DataField="TransID" SortExpression="TransID" />
                                    <asp:BoundField HeaderText="API Tx ID" DataField="APITransID" SortExpression="APITransID" />
                                    <asp:BoundField HeaderText="Status" DataField="Status" SortExpression="Status" />
                                    <asp:BoundField HeaderText="APIID" DataField="APIID" SortExpression="APIID" />
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                        <%# String.Format("{0:dd/MM/yyyy hh:mm:ss tt}", Eval("Adddate")) %>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Force Update">
                                        <ItemTemplate>
                                            <asp:Button id="btnfsuccess" runat="server" Text="Success" CommandArgument='<%# Eval("HistoryID") %>' CommandName="Success" OnClientClick="return confirm('Are you sure to mark this as success ?')" CssClass="btn btn-success" />
                                            <asp:Button id="btnFfailed" runat="server" Text="Fail" CommandArgument='<%# Eval("HistoryID") %>' CommandName="Fail" OnClientClick="return confirm('Are you sure to mark this as fail ?')" CssClass="btn btn-danger" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                
                            </asp:GridView>
                               </div>
          </ContentTemplate>  
                                <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gvHistory" EventName="Sorting" />
            <asp:AsyncPostBackTrigger ControlID="gvHistory" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="gvHistory" EventName="PageIndexChanging" />
        </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div></div></div></div>
</asp:Content>
