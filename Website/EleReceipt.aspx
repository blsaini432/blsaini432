<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EleReceipt.aspx.cs" Inherits="Temp_Receipt" %>

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
            <td align="left">
                      <img src="root/images/BBPS-LOGO.png" height="70px" width="150px" />
            </td>
                            <td align="right" style="font-size: 18px; font-weight: bold; text-align:right; width:60%;" >
                  
                             Electricity RECEIPT
                            </td>         <td align="right" style="width:50%;">
                                    <img src="edelogo.jpg" height="70px" width="150px" />
                            </td>
                        </tr>
                        <tr>
                        <td colspan="3" align="center" style="font-size: 10px; font-weight: bold">
                        (Note : This is only a Temporary Receipt.)
                        </td>
                        </tr>
                        <tr>
                            <td>
                                <b>RECEIPT# :</b>TR-
                                <%#Eval("agent_id")%>
                            </td>
                            <td align="right">
                                <b>Date :</b>
                                <%#String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(Eval("paydate")))%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>                           
                        </tr>
                        <tr>
                            <td colspan="3" align="left">
                                <hr />
                                <b>TRANSACTION DETAILS</b>
                                <hr />
                            </td>
                        </tr>
  
                        <tr>
                            <td>
                                <b>Service For</b>
                            </td>
                            <td>
                                Electricity
                            </td>
                        </tr>                        
                        <tr>
                            <td>
                                <b>Acknowledgement ID</b>
                            </td>
                            <td>
                                <%#Eval("agent_id")%>
                            </td>
                        </tr>                        
                        <tr>
                            <td>
                                <b>PayDate</b>
                            </td>
                            <td>
                                <%#Convert.ToDateTime(Eval("paydate"))%>
                            </td>
                        </tr>

                             <tr>
                            <td>
                                <b>Service Name</b>
                            </td>
                            <td>
                                 <%#Eval("servicename")%>
                            </td>
                        </tr>
                           <tr>
                            <td>
                                <b>AccountNumber</b>
                            </td>
                            <td>
                                 <%#Eval("account_no")%>
                            </td>
                        </tr>
        
                       <tr>
                            <td>
                                <b>Amount</b>
                            </td>
                            <td>
                                Rs.
                                <%#Eval("trans_amt")%>
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
                <td colspan="3" align="center" style="font-size: 10px;">
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
