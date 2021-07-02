<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="Manageservice.aspx.cs" Inherits="Root_Retailer_Manageservice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-4 grid-margin stretch-card">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">
                        <i class="fas fa-servicestack-alt"></i>
                        Manage Services
                    </h4>
                    <div class="table-responsive">
                        <asp:Repeater ID="rep" runat="server">
                            <ItemTemplate>
                                <table class="table table-bordered table-hover">
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="Large" Text="Recharge"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="Large" ForeColor='<%# Eval("Recharge").ToString() == "ON" ? System.Drawing.Color.Green : System.Drawing.Color.Red %>' Text='<%# Eval("Recharge")%>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="Large" Text="UTI"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="Large" ForeColor='<%# Eval("UTI").ToString() == "ON" ? System.Drawing.Color.Green : System.Drawing.Color.Red %>' Text='<%# Eval("UTI")%>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" Text="AEPS"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblaeps" runat="server" Font-Bold="True" Font-Size="Large" ForeColor='<%# Eval("AEPS").ToString() == "ON" ? System.Drawing.Color.Green : System.Drawing.Color.Red %>' Text='<%# Eval("AEPS")%>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Large" Text="BBPS"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblbbps" runat="server" Font-Bold="True" Font-Size="Large" Text='<%# Eval("BBPS") %>' ForeColor='<%# Eval("BBPS").ToString() == "ON" ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Large" Text="PrepaidCard"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblprepaidcard" runat="server" Font-Bold="True" Font-Size="Large" Text='<%# Eval("prepaidcard") %>' ForeColor='<%# Eval("prepaidcard").ToString() == "ON" ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Large" Text="DMR"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbldmr" runat="server" Font-Bold="True" Font-Size="Large" Text='<%# Eval("DMR") %>' ForeColor='<%# Eval("DMR").ToString() == "ON" ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="Large" Text="XPress DMR"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblxpressdmr" runat="server" Font-Bold="True" Font-Size="Large" Text='<%# Eval("XpressDMR") %>' ForeColor='<%# Eval("XpressDMR").ToString() == "ON" ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="Large" Text="PAYOUT"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblpayout" runat="server" Font-Bold="True" Font-Size="Large" Text='<%# Eval("Payout") %>' ForeColor='<%# Eval("Payout").ToString() == "ON" ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label></td>
                                    </tr>

                                </table>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>

