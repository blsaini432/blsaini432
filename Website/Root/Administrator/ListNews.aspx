<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true"
    CodeFile="ListNews.aspx.cs" Inherits="cms_ListNews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function askDelete() {
            var agree;
            agree = confirm("Are you sure to delete this News?");
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
                                <asp:DataGrid ID="dgNews" Width="100%" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-responsive" 
                        AllowPaging="false" PageSize="10" OnItemDataBound="dgNews_ItemDataBound"
                        OnItemCommand="dgNews_ItemCommand" OnPageIndexChanged="dgNews_PageIndexChanged"
                        OnSortCommand="dgNews_SortCommand" AllowSorting="false" 
                        UseAccessibleHeader="True" onitemcreated="dgNews_ItemCreated">
                        <Columns>
                            <asp:TemplateColumn HeaderText="News Name" SortExpression="NewsName">
                                <ItemTemplate>
                                    <%#Eval("NewsName")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="News Desc">
                                <ItemTemplate>
                                    <div style="width:400px">
                                        <%#Eval("NewsDesc")%></div>
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
                                    <a href='ManageNews.aspx?id=<%#Eval("NewsID")%>' class="btn btn-dark btn-icon-text">
                                        <i class="fas fa-pencil-alt btn-icon-append"></i>Edit</a>

                                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-warning btn-icon-text" Text="Delete"
                                                            AlternateText="Delete" ToolTip="Delete this record" CommandName="Remove" CommandArgument='<%#Eval("NewsID") %>'
                                                            OnClientClick='return confirm("Are You Sure To Delete This Record?")' />

                                    <asp:Button ID="btnIsActive" runat="server" CssClass="btn btn-primary btn-icon-text" Text='<%# Convert.ToBoolean(Eval("IsActive")) == true ? "Deactive" : "Active" %>'
                                                            AlternateText="Active/Deactive this record" ToolTip='<%# Convert.ToBoolean(Eval("IsActive")) == true ? "Deactive this record" : "Active this record" %>' CommandName="IsActive"
                                                            CommandArgument='<%#Eval("NewsID") %>' OnClientClick='return confirm("Are You Sure To Active/Deactive This Record?")'></asp:Button>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                                </div></div></div></div></div></div></div>
</asp:Content>
