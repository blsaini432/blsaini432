<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Id.aspx.cs" Inherits="Id" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 64%;
        }

        .auto-style2 {
            width: 163px;
            height: 68px;
        }



        .auto-style3 {
            height: 23px;
        }

        .auto-style4 {
            height: 43px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table align="center" class="auto-style1" style="border-color: #181F8B; border-style: solid;">
                <tr>
                    <td>
                        <asp:Image ID="imgcompany" CssClass="auto-style2" ImageUrl="~/logo.png" runat="server" />
                    </td>
                    <td colspan="3">
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="X-Large" Text="Essential Services Provider to the country-" ForeColor="#181F8B"></asp:Label>
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Banking  and ATM Services"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                    <td align="center" colspan="2" style="background-color: #E85519; color: #FFFFFF;">
                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="IDENTITY CARD"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" rowspan="6">
                        <asp:Image ID="Image1" runat="server" Height="148px" Width="154px" />
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Name:" Font-Size="Large"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblname" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Text="Merchant ID:" Font-Size="Large"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblmerchantid" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="State:" Font-Size="Large"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblstate" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Text="Department:" Font-Size="Large"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbldepartment" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label8" runat="server" Font-Bold="True" Text="DOJ:" Font-Size="Large"></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:Label ID="lbldoj" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label9" runat="server" Font-Bold="True" Text="Valid Till:" Font-Size="Large"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblvalidtil" runat="server" Font-Bold="True" Font-Size="Large" Text=" December 2021"></asp:Label>
                    </td>
                </tr>
                <tr>

                    <td colspan="2">&nbsp;</td>
                    <td colspan="2" align="right">
                       <img src="../../Uploads/signature.jpg" height="80px" width="200px" />
                        <br />
                        <asp:Label ID="lblcname" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                    <td colspan="2" align="right">&nbsp;</td>
                </tr>
                <tr style="background-color: #181F8B; color: #FFFFFF;">
                    <td colspan="2" align="center" style="background-color: #181F8B; color: #FFFFFF; font-size: large; font-weight: bold;">
                        <asp:PlaceHolder ID="plBarCode" runat="server" />
                        &nbsp;</td>
                    <td colspan="2">
                        <p style="background-color: #181F8B; color: #FFFFFF; font-size: large; font-weight: bold;">
                            The Holder of this card has documentary evidence to prove
                     that they are involved in providing essential services
to the citizen of this country.
                        </p>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <hr style="height: 5px; background-color: #000000;" />
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Image ID="Image2" CssClass="auto-style2" ImageUrl="~/logo.png" runat="server" /></td>
                    <td colspan="2" align="center" style="background-color: #E85519; color: #FFFFFF;">
                        <asp:Label ID="Label10" runat="server" Font-Bold="True" Text="INSTRUCTIONS"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <ul>
                            <li>This card must be produced as per demand</li>
                            <li>Holders of this card will be accountable for any misuse ,loss or damage caused</li>
                            <li>Loss must be reported to the Registered Office on an immedidate basis</li>
                        </ul>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Label ID="Label11" runat="server" Font-Bold="True" Text="If found please return to Registerd Office" Font-Size="Large" ForeColor="#181F8B"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="auto-style3" align="center">

                        <asp:Label ID="lblcompanyname" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>

                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="lbladdress" runat="server" Font-Bold="True"></asp:Label>
                     <%--   <img src="../../Uploads/ammm 001.jpg" height="150px" width="250px" />--%>
                    </td>
                    <%-- <td>
                    <img src="../../Uploads/head.jpeg" height="150px" width="150px" />
                </td>--%>
                </tr>
                <tr>
                    <td colspan="4" align="center" class="auto-style4" style="background-color: #181F8B; color: #FFFFFF; font-size: large; font-weight: bold">
                        <asp:Label ID="lblcompanyname0" runat="server" Font-Bold="True" Font-Size="Large">Website : </asp:Label>
                        <asp:Label ID="lblwebsite" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
