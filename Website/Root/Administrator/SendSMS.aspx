<%@ Page Title="" Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true"
    CodeFile="SendSMS.aspx.cs" Inherits="SendSMS" ValidateRequest="false" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="js/CheckAll.js" type="text/javascript"></script>
    <style type="text/css">
        .selected {
            background-color: #A1DCF2;
        }
    </style>
    <script src="../js/MaxLength.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">




    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">Send SMS
            </h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row grid-margin">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body">

                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                    <ProgressTemplate>
                                        <div class="loading-overlay">
                                            <div class="wrapper">
                                                <div class="ajax-loader-outer">
                                                    Loading...
                                                </div>
                                            </div>
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                                <label class="col-form-label">To<code>*</code></label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:RadioButton ID="rdbAll" runat="server" Text="All Members" OnCheckedChanged="rdbAll_CheckedChanged"
                                                    AutoPostBack="true" GroupName="ToMobile" />&nbsp;&nbsp;
                            <asp:RadioButton ID="rdbSelected" runat="server" Text="Selected Members" OnCheckedChanged="rdbSelected_CheckedChanged"
                                AutoPostBack="true" GroupName="ToMobile" /><br />
                                                <asp:TextBox ID="txtToMobile" runat="server" TextMode="MultiLine" Style="width: 100% !important; height: 200px !important"
                                                    ValidationGroup="v"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ControlToValidate="txtToMobile"
                                                    ValidationGroup="v" ErrorMessage="Please Enter Mobile !">*</asp:RequiredFieldValidator>
                                            </div>

                                        </div>
                                       

                 <div class="form-group row">
                     <div class="col-lg-3">
                         <label class="col-form-label">SMS<code>*</code></label>
                     </div>
                     <div class="col-lg-8">
                         <asp:TextBox ID="txtSMS" runat="server" Width="100%" ValidationGroup="v" ClientIDMode="Static"
                             TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="rfvSMS" runat="server" ControlToValidate="txtSMS"
                             ValidationGroup="v" ErrorMessage="Please Enter SMS !">*</asp:RequiredFieldValidator>

                     </div>
                 </div>

                                        <div class="form-group row">
                                            <div class="col-lg-3">
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Send" ValidationGroup="v" OnClick="btnSubmit_Click" class="btn btn-primary" />
                                                &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" class="btn btn-primary" />
                                                <asp:ValidationSummary ID="ValidationSummary" runat="server"
                                                    ClientIDMode="Static" ValidationGroup="v" />

                                            </div>
                                        </div>


                                        <section class="content mydash">


                                            <table class="table table-bordered table-hover table-responsive">
                                                <tr style="display: none;">
                                                    <td colspan="1" class="aleft">
                                                        <strong class="star">Note : Fields with <span class="red">*</span> are mandatory fields.</strong>
                                                    </td>
                                                </tr>
                                                <tr valign="top">
                                                    
                                                    <td rowspan="4" valign="top" id="tdGrid" runat="server" visible="false">
                                                        <table class="table table-bordered table-hover table-responsive ">
                                                            <tr style="display: none;">
                                                                <td>Member ID
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtMemberID" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>Member Name
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtuse" runat="server" MaxLength="250" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="display: none;">
                                                                <td>Mobile
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtmobile" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>Email
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtemail" runat="server" MaxLength="210" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="display: none;">
                                                                <td>State
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlStateName" runat="server" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>City
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtcity" runat="server" MaxLength="210" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="display: none;">
                                                                <td>MemberType
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlMemberType" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Value="0">Select Member Type</asp:ListItem>
                                                                        <asp:ListItem Value="4">Distributor</asp:ListItem>
                                                                        <asp:ListItem Value="5">Retailer</asp:ListItem>
                                                                        <asp:ListItem Value="3">Master Distributor</asp:ListItem>
                                                                        <asp:ListItem Value="7">Reseller</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td></td>
                                                                <td></td>
                                                            </tr>
                                                            <tr style="display: none;">
                                                                <td colspan="4">
                                                                    <b>Members those has not logged in this period</b>
                                                                </td>
                                                            </tr>
                                                            <tr style="display: none;">
                                                                <td>From Date :
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="dd-MMM-yyyy" PopupButtonID="txtfromdate"
                                                                        TargetControlID="txtfromdate">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                                <td>To Date :
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txttodate" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="dd-MMM-yyyy" Animated="False"
                                                                        PopupButtonID="txttodate" TargetControlID="txttodate">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            <tr style="display: none;">
                                                                <td colspan="4" align="right">
                                                                    <asp:Button ID="btnSearch" runat="server" Text="Search &gt;&gt;" OnClick="btnSearch_Click" class="btn btn-primary" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <div class="box-inner">
                                                                        <div class="box-content">
                                                                            <asp:GridView ID="gvMemberMaster" runat="server" AutoGenerateColumns="false"
                                                                                AllowPaging="false" OnPageIndexChanging="gvMemberMaster_PageIndexChanging" PageSize="5" ClientIDMode="Static"
                                                                                ShowHeaderWhenEmpty="true" Width="350px"
                                                                                CssClass="table table-striped table-bordered bootstrap-datatable datatable responsive SmallText"
                                                                                OnRowCreated="gvMemberMaster_RowCreated">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkHeader" runat="server" ClientIDMode="Static" onclick="checkAll(this);" />
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkRow" runat="server" onclick="Check_Click(this)" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField HeaderText="MemberID" DataField="MemberID" />
                                                                                    <asp:TemplateField HeaderText="Name">
                                                                                        <ItemTemplate>
                                                                                            <%#Eval("MemberName")%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField HeaderText="MemberType" DataField="MemberType" />
                                                                                    <asp:BoundField HeaderText="Mobile" DataField="Mobile" />
                                                                                    <%--<asp:TemplateField HeaderText="Last Login">
                                                    <ItemTemplate>
                                                        <%#String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(Eval("LastLoginDate")))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                                                    <asp:TemplateField HeaderText="Status">
                                                                                        <ItemTemplate>
                                                                                            <img src='<%# Convert.ToBoolean(Eval("IsActive")) == true ? "../images/icon/IsActive.png" : "../images/icon/IsDeactive.png" %>'
                                                                                                alt="Active/Deactive this record" title='<%# Convert.ToBoolean(Eval("IsActive")) == true ? "Deactive this record" : "Active this record" %>' />
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle Width="16px"></HeaderStyle>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <EmptyDataTemplate>
                                                                                    <div class="EmptyDataTemplate">
                                                                                        No Record Found !
                                                                                    </div>
                                                                                </EmptyDataTemplate>
                                                                                <%--<PagerStyle CssClass="PagerStyle" />--%>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <asp:Button ID="btnLeft" runat="server" Text="<<" OnClick="btnLeft_Click" class="btn btn-primary" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </section>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="gvMemberMaster" EventName="PageIndexChanging" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>




</asp:Content>
