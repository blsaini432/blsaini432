<%@ Page Title="" Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true"
    CodeFile="ListBankerMaster.aspx.cs" Inherits="Root_Admin_ListBankerMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../Design/Files/jquery-1.10.2.min.js" type="text/javascript"></script> 
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=gvBankerMaster.ClientID%>').DataTable({ "paging": true, "ordering": false, "searching": true });
    });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Sortable table
            </h3>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="#">Tables</a></li>
                    <li class="breadcrumb-item active" aria-current="page">Sortable table</li>
                </ol>
            </nav>
        </div>
        <div class="row">
            <div class="col-12 grid-margin">
                <div class="card">
                    <div class="card-body">
                        <h4 class="card-title">Basic Sortable Table</h4>
                        <p class="page-description">Add class <code>.sortable-table</code></p>
                        <div class="row">
                            <div class="table-sorter-wrapper col-lg-12 table-responsive">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvBankerMaster" runat="server" CssClass="table"
                                            AutoGenerateColumns="false"
                                            AllowPaging="true" DataKeyNames="BankerMasterID" OnPageIndexChanging="gvBankerMaster_PageIndexChanging"
                                            PageSize="10" Width="100%" OnRowCommand="gvBankerMaster_RowCommand" OnSorting="gvBankerMaster_Sorting"
                                            AllowSorting="false" ShowHeaderWhenEmpty="true"
                                            OnRowCreated="gvBankerMaster_RowCreated">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="BankerMasterName" DataField="BankerMasterName" SortExpression="BankerMasterName" ItemStyle-CssClass="sortStyle ascStyle" />
                                                <asp:BoundField HeaderText="Create Date" DataField="AddDate" SortExpression="AddDate" ItemStyle-CssClass="sortStyle ascStyle" />
                                                <asp:BoundField HeaderText="Last Update" DataField="LastUpdate" SortExpression="LastUpdate" ItemStyle-CssClass="sortStyle ascStyle" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <a href="ManageBankerMaster.aspx?id=<%#Eval("BankerMasterID") %>" title="Edit this record" class="btn btn-dark btn-icon-text">
                                                            <i class="fas fa-pencil-alt btn-icon-append"></i>Edit
                                                        </a>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="16px"></HeaderStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>

                                                        <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-warning btn-icon-text" Text="Delete"
                                                            AlternateText="Delete" ToolTip="Delete this record" CommandName="IsDelete" CommandArgument='<%#Eval("BankerMasterID") %>'
                                                            OnClientClick='return confirm("Are You Sure To Delete This Record?")' />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="16px"></HeaderStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnIsActive" runat="server" CssClass="btn btn-primary btn-icon-text" Text='<%# Convert.ToBoolean(Eval("IsActive")) == true ? "Deactive" : "Active" %>'
                                                            AlternateText="Active/Deactive this record" ToolTip='<%# Convert.ToBoolean(Eval("IsActive")) == true ? "Deactive this record" : "Active this record" %>' CommandName="IsActive"
                                                            CommandArgument='<%#Eval("BankerMasterID") %>' OnClientClick='return confirm("Are You Sure To Active/Deactive This Record?")'></asp:Button>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="16px"></HeaderStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pagination" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="gvBankerMaster" EventName="Sorting" />
                                        <asp:AsyncPostBackTrigger ControlID="gvBankerMaster" EventName="RowCommand" />
                                        <asp:AsyncPostBackTrigger ControlID="gvBankerMaster" EventName="PageIndexChanging" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
