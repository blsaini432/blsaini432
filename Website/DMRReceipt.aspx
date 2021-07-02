<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DMRReceipt.aspx.cs" Inherits="Temp_Receipt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .inv_1 td
        {
            color: #000 !important;
        }
    </style>
</head>
<body style="background:#fff; font-family:Arial; font-size:12px;">
    <form id="form1" runat="server">
       <div class="contain">
        <div class="BannerWraper" id="div" runat="server">
            <asp:Repeater ID="repPage" runat="server" OnItemDataBound="repPage_ItemDataBound">
                <ItemTemplate>
                    <table style="border: 1px solid Black; margin: 10px auto;" width="700px" cellspacing="10"
                        class="inv_1">
                        <tr>
                            <td colspan="2" align="center" style="font-size: 18px; font-weight: bold">
                               DMR RECEIPT
                            </td>
                        </tr>
                        <tr>
                        <td colspan="2" align="center" style="font-size: 10px; font-weight: bold">
                        (Note : This is only a Temporary Receipt.)
                        </td>
                        </tr>
                        <tr>
                            <td>
                                <b>RECEIPT # :</b> TR -
                                <%#Eval("EzulixTranid")%>
                            </td>
                            <td align="right">
                                <b>Date :</b>
                                <%#String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(Eval("created")))%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>                           
                        </tr>
                        <tr>
                            <td colspan="2" align="left">
                                <hr />
                                <b>TRANSACTION DETAILS</b>
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b> Benificiary Name</b>
                            </td>
                            <td style="text-transform:capitalize">
                                <%--<asp:Literal ID="litName" runat="server"></asp:Literal>--%>
                                <%# Eval("beni_name")%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Service For</b>
                            </td>
                            <td>
                                DMR
                            </td>
                        </tr>                        
                        <tr>
                            <td>
                                <b>Acknowledgement ID</b>
                            </td>
                            <td>
                                <%#Eval("EzulixTranid")%>
                            </td>
                        </tr>                        
                        <tr>
                            <td>
                                <b>Time</b>
                            </td>
                            <td>
                                <%#String.Format("{0:hh:mm tt}", Convert.ToDateTime(Eval("created")))%>
                            </td>
                        </tr>

                             <tr>
                            <td>
                                <b>Bank Name</b>
                            </td>
                            <td>
                                 <%#Eval("bank")%>
                            </td>
                        </tr>
                           <tr>
                            <td>
                                <b>AccountNumber</b>
                            </td>
                            <td>
                                 <%#Eval("beni_account")%>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <b>Mobile</b>
                            </td>
                            <td>
                                
                                <%#Eval("remit_mobile")%>
                            </td>
                        </tr>       
                         <tr>
                            <td>
                                <b>Mode</b>
                            </td>
                            <td>
                                
                                <%#Eval("mode")%>
                            </td>
                        </tr>           
                       <tr>
                            <td>
                                <b>Amount Transferd</b>
                            </td>
                            <td>
                                Rs.
                                <%#Eval("Amount")%>
                            </td>
                        </tr>                              
                        <tr>
                            <td colspan="2">
                                <hr />
                            </td>
                        </tr>
                       
                </ItemTemplate>
            </asp:Repeater>
           <tr>
                <td colspan="2" align="center" style="font-size: 10px;">
                    <hr />
                    <asp:Repeater ID="rptmycompany" runat="server">
                        <ItemTemplate>
                    <%# Eval("companyname") %><br />
                    <%# Eval("Address") %>
                    </ItemTemplate>
                    </asp:Repeater>
                    <br />
                    This is a system generated Receipt. Hence no seal or signature required.
                </td>
            </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
