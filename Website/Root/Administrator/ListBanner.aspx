<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true"
    CodeFile="ListBanner.aspx.cs" Inherits="Root_Admin_ListBanner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">
                List Banner
            </h3>
        </div>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row grid-margin">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body">
                                <asp:GridView ID="gvBanner" runat="server" CssClass="table table-bordered table-responsive" 
                            AutoGenerateColumns="false"
                                AllowPaging="false" DataKeyNames="BannerID" OnPageIndexChanging="gvBanner_PageIndexChanging"
                                PageSize="10" Width="100%" OnRowCommand="gvBanner_RowCommand" OnSorting="gvBanner_Sorting"
                                AllowSorting="false" ShowHeaderWhenEmpty="true" 
                                    onrowcreated="gvBanner_RowCreated">
                                <Columns>
                                    <asp:BoundField HeaderText="BannerID" DataField="BannerID" SortExpression="BannerID" />
                                    <asp:BoundField HeaderText="BannerName" DataField="BannerName" SortExpression="BannerName" />
                                    <asp:TemplateField HeaderText="BannerImage">
                                        <ItemTemplate>
                                            <img src=' <%# "../../Uploads/Company/BackBanner/actual/"+ Eval("BannerImage")%>' height="50" alt="" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-----------------------------------------------------------------------------------------------------------------%>
                                    <asp:TemplateField HeaderText="Create Date" SortExpression="AddDate">
                                        <ItemTemplate>
                                            <%#String.Format("{0:dd-MMM-yyyy hh:mm tt}", Convert.ToDateTime(Eval("AddDate")))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <%-- <asp:TemplateField HeaderText="Last Update" SortExpression="LastUpdate">
                                        <ItemTemplate>
                                            <%#String.Format("{0:dd-MMM-yyyy hh:mm tt}", Convert.ToDateTime(Eval("LastUpdate")))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderStyle-Width="16px">
                                        <ItemTemplate>
                                            <a href="ManageBanner.aspx?id=<%#Eval("BannerID") %>" title="Edit this record" class="btn btn-dark btn-icon-text">
                                                Edit
                                            </a>

                                        </ItemTemplate>
                                        <HeaderStyle Width="16px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="16px">
                                        <ItemTemplate>
                                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-outline-warning btn-icon-text" Text='<%# Convert.ToBoolean(Eval("IsActive")) == true? "Activate" : "Deactivate" %>'
                                                AlternateText="Delete" ToolTip="Delete this record" CommandName="IsDelete" CommandArgument='<%#Eval("BannerID") %>'
                                                OnClientClick='return confirm("Are You Sure To Delete This Record?")' />
                                        </ItemTemplate>
                                        <HeaderStyle Width="16px"></HeaderStyle>
                                    </asp:TemplateField>
                           
                                </Columns>
                                
                            </asp:GridView>
                            </div></div></div></div></ContentTemplate>  <Triggers>
          
            <asp:AsyncPostBackTrigger ControlID="gvBanner" EventName="Sorting" />
            <asp:AsyncPostBackTrigger ControlID="gvBanner" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="gvBanner" EventName="PageIndexChanging" />
        </Triggers></asp:UpdatePanel>
            </div>

</asp:Content>
