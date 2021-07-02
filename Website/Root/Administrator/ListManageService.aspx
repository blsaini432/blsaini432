<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true"
    CodeFile="ListManageService.aspx.cs" Inherits="cms_ListManageService" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function askDelete() {
            var agree;
            agree = confirm("Are you sure to delete this?");
            if (agree) {
                return true;
            }
            return false;
        }		
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">List Service
            </h3>
        </div>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <div class="container">
                            <div class="row">
                                <asp:DataGrid ID="dgNews" Width="100%" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-responsive" 
                        AllowPaging="false" PageSize="10" OnItemDataBound="dgNews_ItemDataBound"
                        OnItemCommand="dgNews_ItemCommand" OnPageIndexChanged="dgNews_PageIndexChanged"  AllowSorting="false" 
                        UseAccessibleHeader="True" onitemcreated="dgNews_ItemCreated">
                        <Columns>
                            <asp:TemplateColumn HeaderText="Name" SortExpression="Name" ItemStyle-Wrap="true" ItemStyle-Width="50">
                                <ItemTemplate>
                                    <%#Eval("Name")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Description" ItemStyle-Wrap="true" ItemStyle-Width="50">
                                <ItemTemplate>
                                    <div style="width:400px">
                                        <%#Eval("Description")%></div>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Create Date" SortExpression="AddDate">
                                <ItemTemplate>
                                    <%#String.Format("{0:dd-MMM-yyyy hh:mm tt}", Convert.ToDateTime(Eval("AddDate")))%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Action">
                                <ItemTemplate>
                                    <a href='ManageService.aspx?id=<%#Eval("Id")%>' class="btn btn-dark btn-icon-text">
                                        <i class="fas fa-pencil-alt btn-icon-append"></i>Edit</a>
                                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-warning btn-icon-text" Text="Delete"
                                                            AlternateText="Delete" ToolTip="Delete this record" CommandName="Remove" CommandArgument='<%#Eval("Id") %>'
                                                            OnClientClick='return confirm("Are You Sure To Delete This Record?")' />
                                 
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                                </div></div></div></div></div></div></div>
</asp:Content>
