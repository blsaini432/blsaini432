<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master"
    AutoEventWireup="true" CodeFile="RegRequests.aspx.cs" Inherits="Root_Admin_RegRequests" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/script_pop.js"></script>
    <link href="../../css/style2.css" rel="stylesheet" />
    <script src="../../Scripts/chosen/jquery.min.js"></script>
    <script src="../../Scripts/chosen/chosen.jquery.min.js"></script>
    <link href="../../Scripts/chosen/chosen.min.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">
        function calc1(a, b, c) {
            var x = document.getElementById(a);
            var y = document.getElementById(b);
            var z = document.getElementById(c);
            if (x.value == "") { x.value = '0'; }
            if (y.value == "") { y.value = '0'; }
            z.value = (1 * x.value) + (1 * y.value);
        }
    </script>
    <style>
#ctl00_ContentPlaceHolder1_ddlparentmember {
	width: 100% !important;
	display: block !important;
}
#ctl00_ContentPlaceHolder1_ddlparentmember_chosen {
	width: 100% !important;
}
#ctl00_ContentPlaceHolder1_ddlparentmember_chosen {
	width: 94% !important;
	position: relative;
	top: -37px;
}
#ctl00_ContentPlaceHolder1_ddlparentmember_chosen .chosen-single {
	border-radius: 0;
	height: 37px;
}
    </style>
    <script>
         function getdata()
        {
            debugger;
            var fromdate = document.getElementById('<%=txtfromdate.ClientID %>').value;
            var todate = document.getElementById('<%=txttodate.ClientID %>').value;
            document.getElementById('<%=hdnfromdate.ClientID %>').value = fromdate;
            document.getElementById('<%=hdntodate.ClientID %>').value = todate
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="toPopup">
        <div class="close">
        </div>
        <div id="popup_content" style="height: 580px !important;">
            <h1>Update Registration Request
                    <small>Admin Panel</small>
            </h1>
            <div class="table table-responsive">
                <table class="table table-responsive">
                    <tr>
                        <td>Member Info
                        </td>
                        <td>
                            <asp:Literal ID="litMember" runat="server"></asp:Literal><asp:HiddenField ID="hdnid"
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Member Type
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlmymembertype" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlMemberType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlmymembertype"
                                Display="Dynamic" ErrorMessage="Please Select member type !" SetFocusOnError="True"
                                ValidationGroup="v0" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>Parent Member ID
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlparentmember" CssClass="form-control parent-detail" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2"  runat="server" ControlToValidate="ddlparentmember"
                               Display="Dynamic" ErrorMessage="Please Select Parent member id !" SetFocusOnError="True"
                                ValidationGroup="v0" InitialValue="0">*</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                  <script>
                        $('#<%=ddlparentmember.ClientID%>').chosen();
                    </script>
                    <tr>
                        <td>Commission Package
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlmypackage" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlmypackage"
                                Display="Dynamic" ErrorMessage="Please Select commission package !" SetFocusOnError="True"
                                ValidationGroup="v0" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:MultiView ID="mvw1" runat="server" ActiveViewIndex="0">
                                <asp:View ID="vv1" runat="server">
                                </asp:View>
                                <asp:View ID="vv2" runat="server">
                                    <table>
                                        <thead>
                                            <td>Registration Type
                                            </td>
                                            <td>Admin Profit
                                            </td>
                                            <td>My Profit
                                            </td>
                                            <td>Net Joining Fees
                                            </td>
                                        </thead>
                                        <tr>
                                            <td>Advisor Registration
                                            </td>
                                            <td>
                                                <asp:TextBox ID="DSO1_Admin" CssClass="form-control" runat="server" Text="0" ClientIDMode="Static" onkeyup="calc1('DSO1_Admin','DSO1_self','DSO1_tot');"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="txtZIP_FilteredTextBoxExtender" runat="server" Enabled="True"
                                                    FilterType="Numbers" TargetControlID="DSO1_Admin">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="DSO1_self" runat="server" CssClass="form-control" Text="0" ClientIDMode="Static" onkeyup="calc1('DSO1_Admin','DSO1_self','DSO1_tot');"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                    FilterType="Numbers" TargetControlID="DSO1_self">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="DSO1_tot" runat="server" CssClass="form-control" Text="0" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                    FilterType="Numbers" TargetControlID="DSO1_tot">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <asp:View ID="vv3" runat="server">
                                    <table>
                                        <thead>
                                            <td>Registration Type
                                            </td>
                                            <td>Admin Profit
                                            </td>
                                            <td>My Profit
                                            </td>
                                            <td>Net Joining Fees
                                            </td>
                                        </thead>
                                        <tr>
                                            <td>Advisor Registration
                                            </td>
                                            <td>
                                                <asp:TextBox ID="DLC1_admin" runat="server" CssClass="form-control" Text="0" ClientIDMode="Static" onkeyup="calc1('DLC1_admin','DLC1_self','DLC1_tot');"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                    FilterType="Numbers" TargetControlID="DLC1_admin">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="DLC1_self" CssClass="form-control" runat="server" Text="0" ClientIDMode="Static" onkeyup="calc1('DLC1_admin','DLC1_self','DLC1_tot');"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                                    FilterType="Numbers" TargetControlID="DLC1_self">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="DLC1_tot" CssClass="form-control" runat="server" Text="0" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                    FilterType="Numbers" TargetControlID="DLC1_tot">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>DSO Registration
                                            </td>
                                            <td>
                                                <asp:TextBox ID="DLC2_admin" CssClass="form-control" runat="server" Text="0" ClientIDMode="Static" onkeyup="calc1('DLC2_admin','DLC2_self','DLC2_tot');"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                                    FilterType="Numbers" TargetControlID="DLC2_admin">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="DLC2_self" CssClass="form-control" runat="server" Text="0" ClientIDMode="Static" onkeyup="calc1('DLC2_admin','DLC2_self','DLC2_tot');"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                                    FilterType="Numbers" TargetControlID="DLC2_self">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="DLC2_tot" CssClass="form-control" runat="server" Text="0" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True"
                                                    FilterType="Numbers" TargetControlID="DLC2_tot">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <asp:View ID="vv4" runat="server">
                                    <table>
                                        <thead>
                                            <td>Registration Type
                                            </td>
                                            <td>Admin Profit
                                            </td>
                                            <td>My Profit
                                            </td>
                                            <td>Net Joining Fees
                                            </td>
                                        </thead>
                                        <tr>
                                            <td>Advisor Registration
                                            </td>
                                            <td>
                                                <asp:TextBox ID="sh1_admin" CssClass="form-control" runat="server" Text="0" ClientIDMode="Static" onkeyup="calc1('sh1_admin','sh1_self','sh1_tot');"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                                    FilterType="Numbers" TargetControlID="sh1_admin">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="sh1_self" CssClass="form-control" runat="server" Text="0" ClientIDMode="Static" onkeyup="calc1('sh1_admin','sh1_self','sh1_tot');"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True"
                                                    FilterType="Numbers" TargetControlID="sh1_self">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="sh1_tot" CssClass="form-control" runat="server" Text="0" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" Enabled="True"
                                                    FilterType="Numbers" TargetControlID="sh1_tot">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>DSO Registration
                                            </td>
                                            <td>
                                                <asp:TextBox ID="sh2_admin" CssClass="form-control" runat="server" Text="0" ClientIDMode="Static" onkeyup="calc1('sh2_admin','sh2_self','sh2_tot');"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" Enabled="True"
                                                    FilterType="Numbers" TargetControlID="sh2_admin">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="sh2_self" CssClass="form-control" runat="server" Text="0" ClientIDMode="Static" onkeyup="calc1('sh2_admin','sh2_self','sh2_tot');"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" Enabled="True"
                                                    FilterType="Numbers" TargetControlID="sh2_self">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="sh2_tot" CssClass="form-control" runat="server" Text="0" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" Enabled="True"
                                                    FilterType="Numbers" TargetControlID="sh2_tot">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>DLC Registration
                                            </td>
                                            <td>
                                                <asp:TextBox ID="sh3_admin" CssClass="form-control" runat="server" Text="0" ClientIDMode="Static" onkeyup="calc1('sh3_admin','sh3_self','sh3_tot');"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" Enabled="True"
                                                    FilterType="Numbers" TargetControlID="sh3_admin">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="sh3_self" CssClass="form-control" runat="server" Text="0" ClientIDMode="Static" onkeyup="calc1('sh3_admin','sh3_self','sh3_tot');"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" Enabled="True"
                                                    FilterType="Numbers" TargetControlID="sh3_self">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="sh3_tot" CssClass="form-control" runat="server" Text="0" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" Enabled="True"
                                                    FilterType="Numbers" TargetControlID="sh3_tot">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                            </asp:MultiView>
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnSuccess" runat="server" Text="Approve Request" OnClick="btnSuccess_Click" CssClass="btn btn-primary"
                                ValidationGroup="v0" />
                            <asp:Button ID="btnFail" runat="server" Text="Reject Request" OnClick="btnFail_Click" CssClass="btn btn-danger" />
                            <asp:Button ID="Button1" runat="server" Text="Close Window" OnClick="Button1_Click" CssClass="btn btn-dark" />

                        </td>
                    </tr>
                </table>
            </div>

        </div>
        <!--your content end-->
    </div>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <div class="loader">
    </div>
    <div id="backgroundPopup">
    </div>


    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Membership request (Website)
            </h3>
        </div>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <div class="container">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">FromDate<code>*</code></label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:HiddenField ID="hdnfromdate" runat="server"  ClientIDMode="Static" />
                                              <asp:Image ID="Image1" runat="server" ImageUrl="../Upload/calender.png" Height="20px" Width="20px" />
                                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtfromdate"
                                                Display="Dynamic" ErrorMessage="Please Enter From Date !" SetFocusOnError="True"
                                                ValidationGroup="v"></asp:RequiredFieldValidator>
                                            <cc1:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd-MM-yyyy" PopupButtonID="Image1"
                                                TargetControlID="txtfromdate">
                                            </cc1:CalendarExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">ToDate<code>*</code></label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control"></asp:TextBox>
                                              <asp:HiddenField ID="hdntodate" runat="server"  ClientIDMode="Static" />
                                            <asp:Image ID="imgbt" runat="server" ImageUrl="../Upload/calender.png" Height="20px" Width="20px" />
                                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txttodate"
                                                Display="Dynamic" ErrorMessage="Please Enter To Date !" SetFocusOnError="True"
                                                ValidationGroup="v"></asp:RequiredFieldValidator>
                                            <cc1:CalendarExtender runat="server" ID="CalendarExtender2" Format="dd-MM-yyyy" Animated="False"
                                                PopupButtonID="imgbt" TargetControlID="txttodate">
                                            </cc1:CalendarExtender>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group row">
                                        <div class="col-sm-6">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search &gt;&gt;" OnClick="btnSearch_Click" class="btn btn-primary" />
                                        </div>
                                    </div>
                                </div>
                                 <div class="col-md-2">
                                    <div class="form-group row">
                                        <div class="col-sm-6">
                                            <asp:Button ID="btn_export" runat="server" OnClick="btn_export_Click" Text="Export" CssClass="btn btn-dribbble" OnClientClick="getdata()" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="table table-responsive">
                                <asp:GridView ID="gvHistory" runat="server" CssClass="table table-striped table-bordered bootstrap-datatable datatable responsive SmallText"
                                    AutoGenerateColumns="false"
                                    Width="100%" OnRowCommand="gvHistory_RowCommand"  ShowHeaderWhenEmpty="true"
                                    OnRowCreated="gvHistory_RowCreated">
                                    <Columns>
                                        <asp:BoundField HeaderText="S No" DataField="sno" SortExpression="sno" />
                                        <asp:BoundField HeaderText="MemberID" DataField="memberid" SortExpression="memberid" />
                                        <asp:BoundField HeaderText="MemberName" DataField="membername" SortExpression="membername" />
                                        <asp:BoundField HeaderText="Email" DataField="email" SortExpression="email" />

                                        <asp:BoundField HeaderText="Mobile" DataField="mobile" SortExpression="mobile" />
                                        <asp:BoundField HeaderText="State Name" DataField="statename" SortExpression="statename" />
                                        <asp:BoundField HeaderText="City Name" DataField="cityname" SortExpression="cityname" />
                                        <asp:BoundField HeaderText="Member Type" DataField="membertype" SortExpression="membertype" />
                                        <asp:BoundField HeaderText="Owner Id" DataField="ownerid" SortExpression="ownerid" />


                                        <asp:TemplateField HeaderText="Date" SortExpression="AddDate">
                                            <ItemTemplate>
                                                <%#String.Format("{0:dd-MMM-yyyy hh:mm tt}", Convert.ToDateTime(Eval("AddDate")))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Update">
                                            <ItemTemplate>
                                                <asp:Button ID="btnprocess" runat="server" Text="Update" CssClass="btn btn-info" CommandName="process" CommandArgument='<%# Eval("msrno") %>' Visible='<%# Convert.ToBoolean(Eval("valt")) == true ? true : false %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:Button ID="btnReject" runat="server" Text="Delete" CssClass="btn btn-danger" CommandName="Reject" CommandArgument='<%# Eval("msrno") %>' Visible='<%# Convert.ToBoolean(Eval("valt")) == true ? true : false %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>

                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>




