<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true"
    CodeFile="ListPage.aspx.cs" Inherits="cms_ListPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function askDelete() {
            var agree;
            agree = confirm("Are you sure to delete this Page?");
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
            <h3 class="page-title">List News
            </h3>
        </div>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <div class="container">
                            <div class="row">
                                <asp:DataGrid ID="dgPage" Width="100%" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-striped table-bordered bootstrap-datatable datatable responsive SmallText" 
                        AllowPaging="false" PageSize="10" OnItemDataBound="dgPage_ItemDataBound"
                        OnItemCommand="dgPage_ItemCommand" OnPageIndexChanged="dgPage_PageIndexChanged"
                        OnSortCommand="dgPage_SortCommand" AllowSorting="false" 
                        UseAccessibleHeader="True" onitemcreated="dgPage_ItemCreated">
                        <Columns>
                            <asp:TemplateColumn HeaderText="Page Name" SortExpression="PageName">
                                <ItemTemplate>
                                    <%#Eval("PageName")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Create Date" SortExpression="AddDate">
                                <ItemTemplate>
                                    <%#String.Format("{0:dd-MMM-yyyy hh:mm tt}", Convert.ToDateTime(Eval("AddDate")))%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Last Update" SortExpression="LastUpdate">
                                <ItemTemplate>
                                    <%#String.Format("{0:dd-MMM-yyyy hh:mm tt}", Convert.ToDateTime(Eval("LastUpdate")))%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Action">
                                <ItemTemplate>

                                         <a href='ManagePage.aspx?id=<%#Eval("PageID")%>' class="btn btn-dark btn-icon-text">
                                        <i class="fas fa-pencil-alt btn-icon-append"></i>Edit</a>

                                 
                                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-warning btn-icon-text" Text="Delete"
                                                            AlternateText="Delete" ToolTip="Delete this record" CommandName="Remove" CommandArgument='<%#Eval("PageID") %>'
                                                            OnClientClick='return confirm("Are You Sure To Delete This Record?")' />
                            
                                         <asp:Button ID="btnIsActive" runat="server" CssClass="btn btn-primary btn-icon-text" Text='<%# Convert.ToBoolean(Eval("IsActive")) == true ? "Deactive" : "Active" %>'
                                                            AlternateText="Active/Deactive this record" ToolTip='<%# Convert.ToBoolean(Eval("IsActive")) == true ? "Deactive this record" : "Active this record" %>' CommandName="IsActive"
                                                            CommandArgument='<%#Eval("PageID") %>' OnClientClick='return confirm("Are You Sure To Active/Deactive This Record?")'></asp:Button>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>



                                </div></div></div></div></div></div>

</asp:Content>
