<%@ Page Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true" CodeFile="Add_linkpdf.aspx.cs" Inherits="Root_Administrator_Add_linkpdf" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Vedios List 
            </h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row grid-margin">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body">
                                <asp:GridView ID="gvImage" runat="server" CssClass="table table-bordered table-responsive"
                                    AutoGenerateColumns="false"
                                    AllowPaging="false" DataKeyNames="id" OnPageIndexChanging="gvImage_PageIndexChanging"
                                    PageSize="10" Width="100%" OnRowCommand="gvImage_RowCommand" OnSorting="gvImage_Sorting"
                                    AllowSorting="false" ShowHeaderWhenEmpty="true"
                                    OnRowCreated="gvImage_RowCreated">
                                    <Columns>
                                        <asp:BoundField HeaderText="id" DataField="ID" SortExpression="id" />
                                        <asp:BoundField HeaderText="Title" DataField="Title" SortExpression="id" />
                                        <asp:BoundField HeaderText="pdf_file" DataField="pdf_file" SortExpression="id" />
                                        <asp:TemplateField HeaderText="Create Date" SortExpression="AddDate">
                                            <ItemTemplate>
                                                <%#String.Format("{0:dd-MMM-yyyy hh:mm tt}", Convert.ToDateTime(Eval("adddate")))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderStyle-Width="16px">
                                            <ItemTemplate>
                                                <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-outline-warning btn-icon-text" Text='<%# Convert.ToBoolean(Eval("IsActive")) == true? "Delete" : "Activate" %>'
                                                    AlternateText="Delete" ToolTip="Delete this record" CommandName="IsDelete" CommandArgument='<%#Eval("ID") %>'
                                                    OnClientClick='return confirm("Are You Sure To Delete This Record?")' />
                                            </ItemTemplate>
                                            <HeaderStyle Width="16px"></HeaderStyle>
                                        </asp:TemplateField>

                                    </Columns>

                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>

                <asp:AsyncPostBackTrigger ControlID="gvImage" EventName="Sorting" />
                <asp:AsyncPostBackTrigger ControlID="gvImage" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="gvImage" EventName="PageIndexChanging" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

</asp:Content>
