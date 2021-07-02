<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Recharge_Receipt.aspx.cs" Inherits="Recharge_Receipt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .inv_1 td
        {
            color: #000 !important;
        }
        .d *{color:#000;}
        .rf{color:#DD4B39;}
    </style>
</head>
<body style="background:#fff; font-family:Arial; font-size:12px;">
    <form id="form1" runat="server">
       <div class="contain">
        <div class="BannerWraper" id="div" runat="server">

        <table style="border: 1px solid #00A65A; margin: 10px auto;max-width: 700px;width: 100%;" cellspacing="10" class="inv_1" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2" align="center" style="font-size: 10px;" class="d">
                <table border="0" width="100%">
                    <tr>
                        <td>
                            <asp:Image ID="imgCompanyLogo" runat="server" style="max-width:150px;" AlternateText="Company Logo" />
                        </td>
                        <td style="text-align:right">
                        <asp:Repeater ID="rptmycompany" runat="server">
                        <ItemTemplate>
                    <b><%# Eval("FirstName")%>&nbsp;<%# Eval("lastname") %><br />
                    <%# Eval("Address") %><br />
                    <%# Eval("Email") %>, <%# Eval("mobile") %></b>
                    </ItemTemplate>
                    </asp:Repeater>
                        </td>
                    </tr>
                </table>
                </td>
            </tr>
            <asp:Repeater ID="repPage" runat="server" OnItemDataBound="repPage_ItemDataBound">
                <ItemTemplate>
                    
                        <tr>
                            <td colspan="2" align="center" style="font-size: 18px; font-weight: bold;" class="d">
                                Recharge Invoice
                            </td>
                        </tr>
                        <tr>
                            <td  class="d">
                                <b>Invoice # :</b> 
                                <%#Eval("receiptno")%>
                            </td>
                            <td align="right" class="d">
                                <b>Date :</b>
                                <%#String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(Eval("AddDate")))%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <hr class="rf" />
                                <b>TRANSACTION DETAILS</b>
                                <hr class="rf" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Service Provider</b>
                            </td>
                            <td>
                                <%#Eval("OperatorName")%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Service Number</b>
                            </td>
                            <td>
                                <%#Eval("MobileNo")%>
                                [Account Number:<%#Eval("caNumber")%>]
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Transaction ID</b>
                            </td>
                            <td>
                                <%#Eval("TransID")%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Operator Reference Number</b>
                            </td>
                            <td>
                                <%#Eval("APIMessage")%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Time</b>
                            </td>
                            <td>
                                <%#String.Format("{0:hh:mm tt}", Convert.ToDateTime(Eval("AddDate")))%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Recharge Amount</b>
                            </td>
                            <td>
                                Rs.
                                <%#Eval("RechargeAmount")%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr class="rf" />
                            </td>
                        </tr>
                        <tr style="font-size: 16px">
                            <td>
                                <b>Net Amount</b>
                            </td>
                            <td>
                                Rs.
                                <%#Eval("RechargeAmount")%>
                            </td>
                        </tr>
                       <tr>
                            <td colspan="2">
                                <hr class="rf" />
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <span style="font-size: 10px">Merchant Name :</span><br />
                                <b><i style="text-transform:capitalize; font-size:12px"><%# Eval("MemberName") %></i></b>
                            </td>
                            <td style="text-transform:capitalize; text-align:right">
                                <%# Eval("address") %><br />
                                <%# Eval("mymobile") %><br />
                                <%# Eval("Email") %>
                                
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-size:10px; text-align:center">
                            This is a system generated Receipt. Hence no seal or signature required.
                            </td>
                        </tr>
                </ItemTemplate>
            </asp:Repeater>
           
            </table>
        </div>
    </div>
    </form>
</body>
</html>
