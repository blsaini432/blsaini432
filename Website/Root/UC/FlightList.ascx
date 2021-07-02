<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FlightList.ascx.cs" Inherits="FlightList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="../../flight/custom.css" rel="stylesheet" />

<div class="container"></div>
<div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <div class="content-header sty-one">
        <h1>
            <label>Your Search Criteria:</label></h1>
        <asp:Label ID="lbl_SearchCriteria" runat="server"></asp:Label>
          <asp:Button ID="btn_Modify" runat="server" Text="Modify Search" OnClick="btn_Modify_Click" CssClass="btn btn-default btn-outline-success" />
                       
        <asp:Button ID="btn_ModifySearch" runat="server" Visible="false" Text="Modify Search" OnClick="btn_ModifySearch_Click" class="btn btn-sm btn-success" />
        <ol class="breadcrumb">
            <li><a href="../FlightSearch.aspx">Flight Search</a></li>
            <li><a href="#" style="color: green"><i class="fa fa-angle-right"></i>Flight List</a></li>
        </ol>
    </div>
    <!-- Main content -->
    <div class="content">
        <!-- Small boxes (Stat box) -->
        <div class="info-box">
            <div class="row" id="dv_ReturnBooking" runat="server" visible="false">
                <div class="col-2"></div>
                <div class="col-4">
                    <asp:GridView runat="server" CssClass="gridview" AutoGenerateColumns="false" ID="gv_OneFlightlist">
                        <Columns>
                            <asp:TemplateField HeaderText="Airline">
                                <ItemTemplate>
                                    <asp:Image ID="img_OneAirImage" runat="server" ImageUrl='<%#"~/Images/flight/"+Eval("AirlineCode")+".gif"%>' style="width: 30px; height: 30px;"/>
                                    <asp:Label ID="lbl_OneAirlineName" runat="server" Text='<%#Eval("AirlineName") %>'></asp:Label>
                                    <br />
                                    <asp:Label ID="lbl_OneAirlineCode" runat="server" Text='<%#Eval("AirlineCode")+"-" %>'></asp:Label>
                                    <asp:Label ID="lbl_OneFlightNumber" runat="server" Text='<%#Eval("FlightNumber") %>'></asp:Label>
                                    <asp:Label ID="lbl_OneFareClass" runat="server" Text='<%#Eval("FareClass") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Departure">
                                <ItemTemplate>
                                    <%#Eval("OriginDetail") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Arrival">
                                <ItemTemplate>
                                    <%#Eval("DestinationDetail") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-4">
                    <asp:GridView runat="server" CssClass="gridview" AutoGenerateColumns="false" ID="gv_TwoFlightlist">
                        <Columns>
                            <asp:TemplateField HeaderText="Airline">
                                <ItemTemplate>
                                    <asp:Image ID="img_TwoAirImage" runat="server" ImageUrl='<%#"~/Images/flight/"+Eval("AirlineCode")+".gif"%>' style="width: 30px; height: 30px;"/>
                                    <asp:Label ID="lbl_TwoAirlineName" runat="server" Text='<%#Eval("AirlineName") %>'></asp:Label>
                                    <br />
                                    <asp:Label ID="lbl_TwoAirlineCode" runat="server" Text='<%#Eval("AirlineCode")+"-" %>'></asp:Label>
                                    <asp:Label ID="lbl_TwoFlightNumber" runat="server" Text='<%#Eval("FlightNumber") %>'></asp:Label>
                                    <asp:Label ID="lbl_TwoFareClass" runat="server" Text='<%#Eval("FareClass") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Departure">
                                <ItemTemplate>
                                    <%#Eval("OriginDetail") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Arrival">
                                <ItemTemplate>
                                    <%#Eval("DestinationDetail") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-2" style="padding-top: 40px">
                    <div class="row">
                        <b>
                            <label class="col-7">Total Price </label>
                        </b>
                        <label class="col-5"></label>
                    </div>
                    <div class="row">
                        <asp:Label ID="lbl_ReturnTotalPrice" runat="server" class="col-7" Text="0"></asp:Label>
                        <asp:Button ID="btn_ReturnBookflight" runat="server" class="btn btn-sm btn-warning" Text="Book Now" OnClick="btn_ReturnBookflight_Click" />
                    </div>
                </div>
            </div>
            <div class="row">
                <%-- StartFilter --%>
                <div <%if (JourneyType == 1)
                    {%>class="col-3"
                    <% }
                    else
                    {%>class="col-2"
                    <% } %>>
                    <div style="background-color: #589fdd; padding: 10px;">
                        <i class="fa fa-search rsearchi"></i>
                        <b>FILTER</b>
                    </div>
                    <cc1:Accordion ID="acc_Filters" runat="server" CssClass="table" HeaderCssClass="repheader" Style="height: auto; overflow: auto; border: 5px solid #f3f3f3;">
                        <Panes>
                            <cc1:AccordionPane ID="accpane_Stop" runat="server">
                                <Header><b>►Stops</b></Header>
                                <Content>
                                    <asp:CheckBoxList ID="chk_Stops" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="chk_Stops_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0" Text="Direct">
                                        </asp:ListItem>
                                        <asp:ListItem Value="1" Text="1">
                                        </asp:ListItem>
                                        <asp:ListItem Value="2" Text="2 & more">
                                        </asp:ListItem>
                                    </asp:CheckBoxList>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="accpane_Airline" runat="server">
                                <Header><b>►Airlines:</b></Header>
                                <Content>
                                    <asp:CheckBoxList ID="chk_Airline" runat="server" AutoPostBack="true" OnSelectedIndexChanged="chk_Airline_SelectedIndexChanged"></asp:CheckBoxList>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="accpane_Faretype" runat="server">
                                <Header><b>►Fare Type:</b></Header>
                                <Content>
                                    <asp:CheckBoxList ID="chk_Faretype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="chk_Faretype_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="Refundable">
                                        </asp:ListItem>
                                        <asp:ListItem Value="1" Text="Non-Refundable">
                                        </asp:ListItem>
                                    </asp:CheckBoxList>
                                </Content>
                            </cc1:AccordionPane>
                        </Panes>
                    </cc1:Accordion>
                    <div style="background-color: #589fdd; padding: 10px;" id="dv_ReturnHeader" runat="server" visible="false">
                        <b>Return Flight</b>
                    </div>
                    <cc1:Accordion ID="acc_ReturnFilters" runat="server" CssClass="table" HeaderCssClass="repheader" Style="height: auto; overflow: auto; border: 5px solid #f3f3f3;" Visible="false">
                        <Panes>
                            <cc1:AccordionPane ID="accpane_ReturnStop" runat="server">
                                <Header><b>►Stops</b></Header>
                                <Content>
                                    <asp:CheckBoxList ID="chk_ReturnStops" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="chk_ReturnStops_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0" Text="Direct">
                                        </asp:ListItem>
                                        <asp:ListItem Value="1" Text="1">
                                        </asp:ListItem>
                                        <asp:ListItem Value="2" Text="2 & more">
                                        </asp:ListItem>
                                    </asp:CheckBoxList>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="accpane_ReturnAirline" runat="server">
                                <Header><b>►Airlines:</b></Header>
                                <Content>
                                    <asp:CheckBoxList ID="chk_ReturnAirline" runat="server" AutoPostBack="true" OnSelectedIndexChanged="chk_ReturnAirline_SelectedIndexChanged"></asp:CheckBoxList>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="acc_ReturnFaretype" runat="server">
                                <Header><b>►Fare Type:</b></Header>
                                <Content>
                                    <asp:CheckBoxList ID="chk_ReturnFaretype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="chk_ReturnFaretype_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="Refundable">
                                        </asp:ListItem>
                                        <asp:ListItem Value="1" Text="Non-Refundable">
                                        </asp:ListItem>
                                    </asp:CheckBoxList>
                                </Content>
                            </cc1:AccordionPane>
                        </Panes>
                    </cc1:Accordion>
                </div>
                <%-- EndFilter --%>
                <div <%if (JourneyType == 1)
                    {%>class="col-9"
                    <% }
                    else
                    {%>class="col-5"
                    <% } %>>
                    <asp:GridView ID="gv_FlightList" runat="server" AutoGenerateColumns="false" CssClass="gridview" AllowSorting="true"
                        OnSorting="gv_FlightList_Sorting" EmptyDataText="No Flight Found" DataKeyNames="FlightNumber" OnRowCommand="gv_FlightList_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="AIRLINE" SortExpression="AirlineName">
                                <ItemTemplate>
                                    <asp:Image ID="img_AirImage" runat="server" ImageUrl='<%#"../flight/Images/flight/"+Eval("AirlineCode")+".gif"%>' style="width: 30px;" />
                                    <asp:Label ID="lbl_AirlineName" runat="server" Text='<%#Eval("AirlineName") %>'></asp:Label><br />
                                    <asp:Label ID="lbl_AirlineCode" runat="server" Text='<%#Eval("AirlineCode")+"-" %>'></asp:Label>
                                    <asp:Label ID="lbl_FlightNumber" runat="server" Text='<%#Eval("FlightNumber") %>'></asp:Label>
                                    <asp:Label ID="lbl_FareClass" runat="server" Text='<%#Eval("FareClass") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DEPARTURE" SortExpression="Origin">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hf_Origin" runat="server" Value='<%#Eval("Origin") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hf_OriginTime" runat="server" Value='<%#"("+Eval("OriginTime")+")" %>'></asp:HiddenField>
                                    <%#Eval("OriginDetail") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ARRIVAL" SortExpression="Destination">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hf_Destination" runat="server" Value='<%#Eval("Destination") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hf_DestinationTime" runat="server" Value='<%#"("+Eval("DestinationTime")+")" %>'></asp:HiddenField>
                                    <%#Eval("DestinationDetail") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DURATION" SortExpression="Duration">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Duration" runat="server" Text='<%#Eval("Duration") %>'></asp:Label><br />
                                    <asp:Label ID="lbl_NoofSeatAvailable" runat="server" Text='<%#Eval("NoofSeatAvailable")+" seat(s) left" %>' ForeColor="Red" Visible='<%# (Eval("NoofSeatAvailable").ToString()) == string.Empty ? false :true %>'></asp:Label><br />
                                    <asp:Label ID="lbl_StopCount" runat="server" Text='<%#Eval("StopCount")+" Stop(s)" %>' ForeColor="Blue" Visible='<%# int.Parse((Eval("StopCount").ToString()))>0 ? true:false %>'></asp:Label>
                                    <asp:Panel ID="pnl_StopDetail" CssClass="modalPopup" runat="server" Visible='<%# int.Parse((Eval("StopCount").ToString()))>0 ? true:false %>'>
                                        <asp:Label ID="lbl_StropDetail" Text='<%#Eval("StopDetail") %>' runat="server"></asp:Label>
                                    </asp:Panel>
                                    <cc1:HoverMenuExtender ID="hme_StopDetail" runat="server" PopupControlID="pnl_StopDetail" TargetControlID="lbl_StopCount"></cc1:HoverMenuExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PUB PRICE" SortExpression="PubPrice">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_PubPrice" runat="server" Text='<%#"Rs. "+Eval("PubPrice") %>'></asp:Label>
                                    <asp:HiddenField ID="Hf_ResultId" runat="server" Value='<%#Eval("Resultid") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="IsLCC" SortExpression="IsLCC">
                                <ItemTemplate>
                                     <asp:Label ID="IsLCC" runat="server" Text='<%# Eval("IsLCC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                         
                            <%--                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnk_LuggageDetail" Text="<i class='fa fa-suitcase'></i>" Style="font-size: 24px" />
                                        <asp:Panel ID="pnl_LuggageDetail" CssClass="modalPopup" runat="server">
                                            <asp:Label ID="lbl_LuggageDetail" Text='<%#Eval("LuggageDetail") %>' runat="server"></asp:Label>
                                        </asp:Panel>
                                        <cc1:HoverMenuExtender ID="hme_LuggageDetail" runat="server" PopupControlID="pnl_LuggageDetail" TargetControlID="lnk_LuggageDetail"></cc1:HoverMenuExtender>
                                        <asp:ImageButton ID="img_Refundable" runat="server" ImageUrl="~/Images/flight/r.png" Visible='<%# Convert.ToBoolean((Eval("IsRefundable").ToString()))==true ? true:false %>' ToolTip="Refundable" />
                                        <asp:ImageButton ID="img_Norefundablek" runat="server" ImageUrl="~/Images/flight/nr.png" Visible='<%# Convert.ToBoolean((Eval("IsRefundable").ToString()))==true ? false:true %>' ToolTip="Non-Refundable" />
                                        <asp:ImageButton ID="img_Farerules" runat="server" ImageUrl="~/Images/flight/rules.png" ToolTip="Fare Rule" CommandArgument='<%#Eval("Resultid")%>' CommandName="rule" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%if (JourneyType == 1)
                                        {%>
                                    <asp:Button ID="btn_Bookflight" runat="server" class="btn btn-sm btn-warning" Text="Book Now" CommandArgument='<%#Eval("Resultid") + ","+Eval("IsLCC") %>' CommandName="book" />
                                    <%
                                        }
                                        else
                                        {
                                     %>
                                    <asp:RadioButton ID="rbtn_Outflight" runat="server" onclick="checkRadioBtn(this);" OnCheckedChanged="rbtn_Outflight_CheckedChanged" AutoPostBack="true" />
                                    <% 
                                     } 
                                        %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div <%if (JourneyType == 1)
                    {%>class="col-9"
                    <% }
                    else
                    {%>class="col-5"
                    <% } %>>
                    <asp:GridView ID="gv_RetrunFlightList" runat="server" AutoGenerateColumns="false" CssClass="gridview" EmptyDataText="No Flight Found" DataKeyNames="FlightNumber">
                        <Columns>
                            <asp:TemplateField HeaderText="AIRLINE" SortExpression="AirlineName">
                                <ItemTemplate>
                                    
                                    <asp:Image ID="img_ReturnAirImage" runat="server" ImageUrl='<%#"../flight/Images/flight/"+Eval("AirlineCode")+".gif"%>' style="width: 30px;"/>
                                    <asp:Label ID="lbl_ReturnAirlineName" runat="server" Text='<%#Eval("AirlineName") %>'></asp:Label><br />
                                    <asp:Label ID="lbl_ReturnAirlineCode" runat="server" Text='<%#Eval("AirlineCode")+"-" %>'></asp:Label>
                                    <asp:Label ID="lbl_ReturnFlightNumber" runat="server" Text='<%#Eval("FlightNumber") %>'></asp:Label>
                                    <asp:Label ID="lbl_ReturnFareClass" runat="server" Text='<%#Eval("FareClass") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DEPARTURE" SortExpression="Origin">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hf_ReturnOrigin" runat="server" Value='<%#Eval("Origin") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hf_ReturnOriginTime" runat="server" Value='<%#"("+Eval("OriginTime")+")" %>'></asp:HiddenField>
                                    <%#Eval("OriginDetail") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ARRIVAL" SortExpression="Destination">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hf_ReturnDestination" runat="server" Value='<%#Eval("Destination") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hf_ReturnDestinationTime" runat="server" Value='<%#"("+Eval("DestinationTime")+")" %>'></asp:HiddenField>
                                    <%#Eval("DestinationDetail") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DURATION" SortExpression="Duration">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_ReturnDuration" runat="server" Text='<%#Eval("Duration") %>'></asp:Label><br />
                                    <asp:Label ID="lbl_ReturnNoofSeatAvailable" runat="server" Text='<%#Eval("NoofSeatAvailable")+" seat(s) left" %>' ForeColor="Red" Visible='<%# (Eval("NoofSeatAvailable").ToString()) == string.Empty ? false :true %>'></asp:Label><br />
                                    <asp:Label ID="lbl_ReturnStopCount" runat="server" Text='<%#Eval("StopCount")+" Stop(s)" %>' ForeColor="Blue" Visible='<%# int.Parse((Eval("StopCount").ToString()))>0 ? true:false %>'></asp:Label>
                                    <asp:Panel ID="pnl_ReturnStopDetail" CssClass="modalPopup" runat="server" Visible='<%# int.Parse((Eval("StopCount").ToString()))>0 ? true:false %>'>
                                        <asp:Label ID="lbl_ReturnStropDetail" Text='<%#Eval("StopDetail") %>' runat="server"></asp:Label>
                                    </asp:Panel>
                                    <cc1:HoverMenuExtender ID="hme_ReturnStopDetail" runat="server" PopupControlID="pnl_ReturnStopDetail" TargetControlID="lbl_ReturnStopCount"></cc1:HoverMenuExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PUB PRICE" SortExpression="PubPrice">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_ReturnPubPrice" runat="server" Text='<%#"Rs. "+Eval("PubPrice") %>'></asp:Label>
                                    <asp:HiddenField ID="Hf_ReturnResultId" runat="server" Value='<%#Eval("Resultid") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IsLCC" SortExpression="IsLCC">
                                <ItemTemplate>
                                    <asp:Label ID="IsLCC" runat="server" Text='<%# Eval("IsLCC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
  
                            <%--                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnk_ReturnLuggageDetail" Text="<i class='fa fa-suitcase'></i>" Style="font-size: 24px" />
                                        <asp:Panel ID="pnl_ReturnLuggageDetail" CssClass="modalPopup" runat="server">
                                            <asp:Label ID="lbl_ReturnLuggageDetail" Text='<%#Eval("LuggageDetail") %>' runat="server"></asp:Label>
                                        </asp:Panel>
                                        <cc1:HoverMenuExtender ID="hme_ReturnLuggageDetail" runat="server" PopupControlID="pnl_ReturnLuggageDetail" TargetControlID="lnk_ReturnLuggageDetail"></cc1:HoverMenuExtender>
                                        <asp:ImageButton ID="img_ReturnRefundable" runat="server" ImageUrl="~/Images/flight/r.png" Visible='<%# Convert.ToBoolean((Eval("IsRefundable").ToString()))==true ? true:false %>' ToolTip="Refundable" />
                                        <asp:ImageButton ID="img_ReturnNorefundablek" runat="server" ImageUrl="~/Images/flight/nr.png" Visible='<%# Convert.ToBoolean((Eval("IsRefundable").ToString()))==true ? false:true %>' ToolTip="Non-Refundable" />
                                        <asp:ImageButton ID="img_ReturnFarerules" runat="server" ImageUrl="~/Images/flight/rules.png" ToolTip="Fare Rule" CommandArgument='<%#Eval("Resultid")%>' CommandName="rule" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:RadioButton ID="rbtn_Inflight" runat="server" onclick="RetruncheckRadioBtn(this);" OnCheckedChanged="rbtn_Inflight_CheckedChanged" AutoPostBack="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>

        <%-- Fare Rules PopUp Start --%>
        <input type="button" value="OpenModalPopup" id="btnPopUpOpen" runat="server" style="display: none;" />
        <cc1:ModalPopupExtender ID="mpe_FareRules" runat="server" BackgroundCssClass="modalDialog"
            TargetControlID="btnPopUpOpen" PopupControlID="dv_FareRules" Enabled="true" CancelControlID="btnPopUpClose">
        </cc1:ModalPopupExtender>
        <div class="ModalInner" id="dv_FareRules" runat="server" style="overflow-x: scroll; max-width: 650px; max-height: 300px; background: #E6E4E4;">
            <input type="button" id="btnPopUpClose" title="Close" class="close" value="X" style="position: unset" />
            <fieldset style="border-radius: 10px;">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_FareRules" runat="server" Text='<%#Eval("FareRuleDetail") %>'></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <%-- Fare Rules PopUp End --%>

        <%-- Modify Search Start --%>
        <input type="button" value="OpenModalPopup" id="btnPopUpOpenOne" runat="server" style="display: none;" />
        <cc1:ModalPopupExtender ID="mpe_ModifySearch" runat="server" BackgroundCssClass="modifymodalBackground"
            TargetControlID="btnPopUpOpenOne" PopupControlID="pnl_ModifySearch" Enabled="true" CancelControlID="btnPopUpCloseOne">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnl_ModifySearch" runat="server" CssClass="modifymodalPopup">
            <div class="header">
                Modify Your Search Criteria
            </div>
            <div class="body">
                <input type="button" id="btnPopUpCloseOne" title="Close" class="close" value="X" />
                <iframe id="iframe_ModifySearch" src="../FlightSearch.aspx" height="100%" width="100%"></iframe>
            </div>
        </asp:Panel>
        <%-- Modify Search End --%>
    </div>
    <!-- /.content -->
</div>
<script type="text/javascript">
    function checkRadioBtn(id)
    {
        var gv = document.getElementById('<%=gv_FlightList.ClientID %>');
        for (var i = 1; i < gv.rows.length; i++)
        {
            var radioBtn = gv.rows[i].cells[5].getElementsByTagName("input");
          
            if (radioBtn[0].id != id.id)
            {
                radioBtn[0].checked = false;
    }
            }
        }
    function RetruncheckRadioBtn(id) {
        var gv = document.getElementById('<%=gv_RetrunFlightList.ClientID %>');
        for (var i = 1; i < gv.rows.length; i++) {
            var radioBtn = gv.rows[i].cells[5].getElementsByTagName("input");
            if (radioBtn[0].id != id.id) {
                radioBtn[0].checked = false;
            }
        }
    }
</script>
