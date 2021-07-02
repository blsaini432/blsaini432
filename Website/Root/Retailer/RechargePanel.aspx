<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Retailer/MemberMaster.master" AutoEventWireup="true" CodeFile="RechargePanel.aspx.cs" Inherits="Root_Retailer_RechargePanel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--   <div id="toPopup">
        <div class="close">
        </div>
        <div id="popup_content" style="height: 250px !important;">
           
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered bootstrap-datatable datatable responsive SmallText"
                    AutoGenerateColumns="false" AllowPaging="false"
                    PageSize="10" Width="100%"
                    AllowSorting="false" ShowHeaderWhenEmpty="true">
                   <Columns>
                     <asp:BoundField HeaderText="RS" DataField="rs" SortExpression="fullName" />
                     <asp:BoundField HeaderText="DESC" DataField="desc" SortExpression="gender" />
                    </Columns>
                </asp:GridView>
          
        </div>

    </div>--%>
    <asp:HiddenField ID="hdnTranType" runat="server" />
    <asp:HiddenField ID="hdnrctype" runat="server" />
    <asp:Literal ID="litRequestConfirm" runat="server" Text="Process My Request"></asp:Literal>
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">
                <asp:Label ID="lblAddEdit" runat="server"></asp:Label>
            </h3>
        </div>


        <%--<div class="static-img">
               <div class="row">
                   <div class="col-md-12">
                       <img src="../../Design/images/carousel/banner_11.jpg"  style="width:100%">
                 </div>
        </div>

       </div>--%>


        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-body">
                        <asp:Repeater runat="server" ID="repeater1">
                            <ItemTemplate>

                                <li data-transition="slideright" data-slotamount="1" data-masterspeed="1000" data-delay="4000" data-saveperformance="off">
                                    <asp:Image ID="myCarousel" ImageUrl='<%#"../../Uploads/Company/BackBanner/actual/"+ Eval("BannerImage")%>' runat="server" Width="100%" Height="300px" />
                                </li>

                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
        <div class="row grid-margin">

            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <h4 class="card-title">Recharge Panel</h4>
                        <div class="row">
                            <div class="col-4">
                                <ul class="nav nav-pills nav-pills-vertical nav-pills-info" id="v-pills-tab" role="tablist" aria-orientation="vertical">
                                    <li class="nav-item">
                                        <a class="nav-link active show" id="v-pills-home-tab" data-toggle="pill" href="#v-pills-home" role="tab" aria-controls="v-pills-home" aria-selected="false">
                                            <i class="fa fa-home"></i>
                                            Prepaid
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="false">
                                            <i class="fa fa-user"></i>
                                            PostPaid
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" id="v-pills-messages-tab" data-toggle="pill" href="#v-pills-messages" role="tab" aria-controls="v-pills-messages" aria-selected="true">
                                            <i class="far fa-envelope-open"></i>
                                            DTH
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" id="v-pills-datacard-tab" data-toggle="pill" href="#v-pills-datacard" role="tab">
                                            <i class="far fa-envelope-open"></i>
                                            DataCard
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" id="v-pills-landline-tab" data-toggle="pill" href="#v-pills-landline" role="tab" aria-controls="v-pills-landline" aria-selected="true">
                                            <i class="far fa-envelope-open"></i>
                                            Landline
                                        </a>
                                    </li>
                                </ul>
                            </div>
                            <div class="col-8">
                                <div class="tab-content tab-content-vertical" id="v-pills-tabContent">
                                    <div class="tab-pane fade active show" id="v-pills-home" role="tabpanel" aria-labelledby="v-pills-home-tab">
                                        <div class="media">
                                            <div class="media-body">
                                                <asp:UpdatePanel ID="UpdatePanel_Home" runat="server">
                                                    <ContentTemplate>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label for="projectinput1">
                                                                        Prepaid Mobile Number</label>
                                                                    <asp:TextBox ID="txtNumberPrepaid" runat="server" class="form-control" ValidationGroup="vgPrepaid"
                                                                        MaxLength="10" onkeyup="if(this.value.length==10)" ClientIDMode="Static"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="txtNumberPrepaid_FilteredTextBoxExtender" runat="server"
                                                                        TargetControlID="txtNumberPrepaid" FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <asp:RequiredFieldValidator ID="rfvNumberPrepaid" runat="server" CssClass="rfv" ControlToValidate="txtNumberPrepaid"
                                                                        ErrorMessage="Please Enter Mobile No !" ValidationGroup="vgPrepaid" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="revNumberPrepaid" runat="server" CssClass="rfv"
                                                                        ControlToValidate="txtNumberPrepaid" ErrorMessage="Invalid Mobile No !" ValidationExpression="\d{10}"
                                                                        ValidationGroup="vgPrepaid"></asp:RegularExpressionValidator>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <label class="label-control" for="projectinput6">
                                                                    Operator</label>
                                                                <asp:DropDownList ID="ddlOperatorPrepaid" runat="server" ClientIDMode="Static" CssClass="form-control">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvOperatorPrepaid" runat="server" CssClass="rfv"
                                                                    ControlToValidate="ddlOperatorPrepaid" ErrorMessage="Please Select Operator !"
                                                                    InitialValue="0" ValidationGroup="vgPrepaid" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            
                                                            </div>
                                                            
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label class="label-control" for="projectinput7">
                                                                    Circle</label>
                                                                <asp:DropDownList ID="ddlCirclePrepaid" runat="server" ClientIDMode="Static" CssClass="form-control">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvCirclePrepaid" runat="server" CssClass="rfv" ControlToValidate="ddlCirclePrepaid"
                                                                    ErrorMessage="Please Select Circle !" InitialValue="0" ValidationGroup="vgPrepaid"
                                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            <asp:Button ID="Button1" OnClick="btn_view_Click"  Text="View Plan" CssClass="btn-success" runat="server"></asp:Button>
                                                                 </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label for="projectinput4">
                                                                        Recharge Amount</label>
                                                                    <asp:TextBox ID="txtAmountPrepaid" autocomplete="off" runat="server" class="form-control"
                                                                        ClientIDMode="Static" MaxLength="4"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="txtAmountPrepaid_FilteredTextBoxExtender" runat="server"
                                                                        TargetControlID="txtAmountPrepaid" FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <asp:RequiredFieldValidator ID="rfvAmountPrepaid" runat="server" CssClass="rfv" ControlToValidate="txtAmountPrepaid"
                                                                        ErrorMessage="Please Enter Amount !" ValidationGroup="vgPrepaid" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                    <%-- <asp:Button ID="btn_offer" runat="server" OnClick="btn_view_Click" Text="view Plan" class="btn btn-info" />--%>
                                                                   <%-- <asp:Button ID="btn" OnClick="btn_view_Click"  Text="view plan" runat="server"></asp:Button>--%>
                                                                </div>
                                                             
                                                            </div>
                                                            <asp:GridView ID="GridView2" runat="server" CssClass="table table-striped table-bordered bootstrap-datatable datatable responsive SmallText"
                                                                AutoGenerateColumns="false" AllowPaging="false"
                                                                PageSize="10" Width="100%"
                                                                AllowSorting="false" ShowHeaderWhenEmpty="true">
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="RS" DataField="rs" SortExpression="fullName" />
                                                                      <asp:BoundField HeaderText="validity" DataField="validity" SortExpression="fullName" />
                                                                    <asp:BoundField HeaderText="benefit" DataField="benefit" SortExpression="gender" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <asp:Button ID="btn_ProceedPrepaid" runat="server" Text="Submit" class="btn btn-raised btn-raised btn-primary"
                                                                        OnClick="btn_ProceedPrepaid_Click" ValidationGroup="vgPrepaid" OnClientClick="if (!Page_ClientValidate('vgPrepaid')){ return false; } this.disabled = true; this.value = 'Submitting...';"
                                                                        UseSubmitBehavior="false" />
                                                                    <asp:Button ID="btn_CancelPrepaid" runat="server" Text="Cancel" class="btn btn-raised btn-raised btn-primary" UseSubmitBehavior="false" />
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="v-pills-profile" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                                        <div class="media">
                                            <div class="media-body">
                                                <asp:UpdatePanel ID="UpdatePanel_Registration" runat="server">
                                                    <ContentTemplate>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label for="projectinput1">
                                                                        Postpaid Mobile Number</label>
                                                                    <asp:TextBox ID="txtNumberPostpaid" runat="server" class="form-control" ValidationGroup="vgPostpaid"
                                                                        MaxLength="10" onkeyup="if(this.value.length<=1) dhlme(this); else if(this.value.length==4)   else if (this.value.length==5) hlme(this);"
                                                                        ClientIDMode="Static"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="txtNumberPostpaid_FilteredTextBoxExtender" runat="server"
                                                                        TargetControlID="txtNumberPostpaid" FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <asp:RequiredFieldValidator ID="rfvNumberPostpaid" runat="server" CssClass="rfv"
                                                                        ControlToValidate="txtNumberPostpaid" ErrorMessage="Please Enter Mobile No !"
                                                                        ValidationGroup="vgPostpaid" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="revNumberPostpaid" runat="server" CssClass="rfv"
                                                                        ControlToValidate="txtNumberPostpaid" ErrorMessage="Invalid Mobile No !" ValidationExpression="\d{10}"
                                                                        ValidationGroup="vgPostpaid"></asp:RegularExpressionValidator>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <label class="label-control" for="projectinput6">
                                                                    Operator</label>
                                                                <asp:DropDownList ID="ddlOperatorPostpaid" runat="server" ClientIDMode="Static" CssClass="form-control">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvOperatorPostpaid" runat="server" CssClass="rfv"
                                                                    ControlToValidate="ddlOperatorPostpaid" ErrorMessage="Please Select Operator !"
                                                                    InitialValue="0" ValidationGroup="vgPostpaid" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label class="label-control" for="projectinput7">
                                                                    Circle</label>
                                                                <asp:DropDownList ID="ddlCirclePostpaid" runat="server" ClientIDMode="Static" CssClass="form-control">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvCirclePostpaid" runat="server" CssClass="rfv"
                                                                    ControlToValidate="ddlCirclePostpaid" ErrorMessage="Please Select Circle !" InitialValue="0"
                                                                    ValidationGroup="vgPostpaid" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label for="projectinput4">
                                                                        Recharge Amount</label>
                                                                    <asp:TextBox ID="txtAmountPostpaid" runat="server" class="form-control" MaxLength="4"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="txtAmountPostpaid_FilteredTextBoxExtender" runat="server"
                                                                        TargetControlID="txtAmountPostpaid" FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <asp:RequiredFieldValidator ID="rfvAmountPostpaid" runat="server" CssClass="rfv"
                                                                        ControlToValidate="txtAmountPostpaid" ErrorMessage="Please Enter Amount !" ValidationGroup="vgPostpaid"
                                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-actions">
                                                            <asp:Button ID="btn_ProceedPostpaid" runat="server" Text="Proceed" class="btn btn-raised btn-raised btn-primary"
                                                                OnClick="btn_ProceedPostpaid_Click" ValidationGroup="vgPostpaid" OnClientClick="if (!Page_ClientValidate('vgPostpaid')){ return false; } this.disabled = true; this.value = 'Submitting...';"
                                                                UseSubmitBehavior="false" />
                                                            <asp:Button ID="btn_CancelPostpaid" runat="server" Text="Cancel" class="btn btn-raised btn-raised btn-primary" UseSubmitBehavior="false" />
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="v-pills-messages" role="tabpanel" aria-labelledby="v-pills-messages-tab">
                                        <div class="media">
                                            <div class="media-body">
                                                <asp:UpdatePanel ID="UpdatePanel_Profile" runat="server">
                                                    <ContentTemplate>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label class="label-control" for="projectinput6">
                                                                        Operator</label>
                                                                    <asp:DropDownList ID="ddlOperatorDTH" runat="server" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvOperatorDTH" runat="server" CssClass="rfv" ControlToValidate="ddlOperatorDTH"
                                                                        ErrorMessage="Please Select DTH Operator !" InitialValue="0" ValidationGroup="vgDTH"
                                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label for="projectinput2">
                                                                        Customer ID</label>
                                                                    <asp:TextBox ID="txtCustomerID" runat="server" class="form-control" ValidationGroup="vgDTH"
                                                                        onkeyup="if(this.value.length<=1) dhlme(this); else if (this.value.length==5) hlme(this);"
                                                                        MaxLength="14"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="txtCustomerID_FilteredTextBoxExtender" runat="server"
                                                                        TargetControlID="txtCustomerID" FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <asp:RequiredFieldValidator ID="rfvCustomerID" runat="server" CssClass="rfv" ControlToValidate="txtCustomerID"
                                                                        ErrorMessage="Please Enter Customer ID !" ValidationGroup="vgDTH" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="revCustomerID" runat="server" CssClass="rfv"
                                                                        ControlToValidate="txtCustomerID" ErrorMessage="Invalid Customer ID !" ValidationExpression="^[0-9]{5,12}$"
                                                                        ValidationGroup="vgDTH"></asp:RegularExpressionValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label for="projectinput3">
                                                                        Recharge Amount</label>
                                                                    <asp:TextBox ID="txtAmountDTH" runat="server" class="form-control" MaxLength="4"
                                                                        ValidationGroup="vgDTH"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="txtAmountDTH_FilteredTextBoxExtender" runat="server"
                                                                        TargetControlID="txtAmountDTH" FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <asp:RequiredFieldValidator ID="rfvAmountDTH" runat="server" CssClass="rfv" ControlToValidate="txtAmountDTH"
                                                                        ErrorMessage="Please Enter Amount !" ValidationGroup="vgDTH" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-actions">
                                                            <asp:Button ID="btn_ProceedDTH" runat="server" Text="Proceed" class="btn btn-raised btn-raised btn-primary"
                                                                OnClick="btn_ProceedDTH_Click" ValidationGroup="vgDTH" OnClientClick="if (!Page_ClientValidate('vgDTH')){ return false; } this.disabled = true; this.value = 'Submitting...';"
                                                                UseSubmitBehavior="false" />
                                                            <asp:Button ID="btn_CancelDTH" runat="server" Text="Cancel" class="btn btn-raised btn-raised btn-primary" UseSubmitBehavior="false" />

                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="v-pills-datacard" role="tabpanel">
                                        <div class="media">
                                            <div class="media-body">
                                                <asp:UpdatePanel ID="UpdatePanel_Datacard" runat="server">
                                                    <ContentTemplate>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label for="projectinput1">
                                                                        Prepaid Mobile Number</label>
                                                                    <asp:TextBox ID="txtDatacardNumber" runat="server" class="form-control" MaxLength="10"
                                                                        onkeyup="if(this.value.length<=1) dhlme(this);  else if (this.value.length==5) hlme(this);"
                                                                        ValidationGroup="vg3"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="txtDatacardNumber_FilteredTextBoxExtender" runat="server"
                                                                        TargetControlID="txtDatacardNumber" FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <asp:RequiredFieldValidator ID="rfvDatacardNumber" runat="server" CssClass="rfv"
                                                                        ControlToValidate="txtDatacardNumber" ErrorMessage="Please Enter Datacard No !"
                                                                        ValidationGroup="vg3" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="revDatacardNumber" runat="server" CssClass="rfv"
                                                                        ControlToValidate="txtDatacardNumber" ErrorMessage="Invalid Datacard No !" ValidationExpression="\d{10}"
                                                                        ValidationGroup="vg3"></asp:RegularExpressionValidator>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <label class="label-control" for="projectinput6">
                                                                    Operator</label>
                                                                <asp:DropDownList ID="ddlDataCard" runat="server" CssClass="form-control">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvDataCard" runat="server" CssClass="rfv" ControlToValidate="ddlDataCard"
                                                                    ErrorMessage="Please Select Operator !" InitialValue="0" ValidationGroup="vg3"
                                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label class="label-control" for="projectinput7">
                                                                        Circle</label>
                                                                    <asp:DropDownList ID="ddlCircleDatacard" runat="server" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvCircleDatacard" runat="server" CssClass="rfv"
                                                                        ControlToValidate="ddlCircleDatacard" ErrorMessage="Please Select Circle !" InitialValue="0"
                                                                        ValidationGroup="vg3" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label for="projectinput4">
                                                                        Recharge Amount</label>
                                                                    <asp:TextBox ID="txtDatacardAmount" runat="server" class="form-control" MaxLength="4"
                                                                        ValidationGroup="vg3"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="txtDatacardAmount_FilteredTextBoxExtender" runat="server"
                                                                        TargetControlID="txtDatacardAmount" FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <asp:RequiredFieldValidator ID="rfvDatacardAmount" runat="server" CssClass="rfv"
                                                                        ControlToValidate="txtDatacardAmount" ErrorMessage="Please Enter Amount !" ValidationGroup="vg3"
                                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-actions">
                                                            <asp:Button ID="btn_ProceedDatacard" runat="server" Text="Proceed" class="btn btn-raised btn-raised btn-primary"
                                                                OnClick="btn_ProceedDatacard_Click" ValidationGroup="vg3" OnClientClick="if (!Page_ClientValidate('vg3')){ return false; } this.disabled = true; this.value = 'Submitting...';"
                                                                UseSubmitBehavior="false" />
                                                            <asp:Button ID="btn_CancelDataCard" runat="server" Text="Cancel" class="btn btn-raised btn-raised btn-primary" UseSubmitBehavior="false" />
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="v-pills-landline" role="tabpanel">
                                        <div class="media">
                                            <div class="media-body">
                                                <asp:UpdatePanel ID="UpdatePanel_Landline" runat="server">
                                                    <ContentTemplate>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label for="projectinput2">
                                                                        STD Code
                                                                    </label>
                                                                    <br />
                                                                    <asp:TextBox ID="txtSTD" class="form-control" runat="server" ValidationGroup="vgLandline" Placeholder="Stdcode"
                                                                        MaxLength="5" onkeyup="if(this.value.length<=1) dhlme(this); else if (this.value.length==5) hlme(this);"
                                                                        onchange="myFunction2()" ClientIDMode="Static"></asp:TextBox>

                                                                    <cc1:FilteredTextBoxExtender ID="txtSTD_FilteredTextBoxExtender" runat="server" TargetControlID="txtSTD"
                                                                        FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <br />
                                                                    <asp:RequiredFieldValidator ID="rfvSTD" runat="server" CssClass="rfv" ControlToValidate="txtSTD"
                                                                        ErrorMessage="Please Enter STD Code !" ValidationGroup="vgLandline" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="revSTD" runat="server" CssClass="rfv" ControlToValidate="txtSTD"
                                                                        ErrorMessage="Invalid STD Code !" ValidationExpression="^[0-9]{3,5}$" ValidationGroup="vgLandline"></asp:RegularExpressionValidator>

                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label for="projectinput2">
                                                                        Landline No
                                                                    </label>
                                                                    <br />
                                                                    <asp:TextBox ID="txtCustomerIDL" class="form-control" runat="server" ValidationGroup="vgLandline"
                                                                        onkeyup="if(this.value.length<=1) dhlme(this); else if (this.value.length==5) hlme(this);"
                                                                        MaxLength="12" placeholder="Telephone No"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCustomerIDL"
                                                                        FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="rfv"
                                                                        ControlToValidate="txtCustomerIDL" ErrorMessage="Please Enter Landline No!"
                                                                        ValidationGroup="vgLandline" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="rfv"
                                                                        ControlToValidate="txtCustomerIDL" ErrorMessage="Invalid Landline Number !" ValidationExpression="^[0-9]{6,9}$"
                                                                        ValidationGroup="vgLandline"></asp:RegularExpressionValidator>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label for="projectinput2">
                                                                        Consumer Account Number</label>
                                                                    <asp:TextBox ID="txtCANumberLandline" runat="server" class="form-control" MaxLength="20"
                                                                        ValidationGroup="vgLandline"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="txtCANumberLandline_FilteredTextBoxExtender" runat="server"
                                                                        TargetControlID="txtCANumberLandline" FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <asp:RequiredFieldValidator ID="rfvCANumberLandline" runat="server" CssClass="rfv"
                                                                        ControlToValidate="txtCANumberLandline" ErrorMessage="Please Enter Consumer Account (CA) Number !"
                                                                        ValidationGroup="vgLandline" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="revCANumberLandline" runat="server" CssClass="rfv"
                                                                        ControlToValidate="txtCANumberLandline" ErrorMessage="Invalid Consumer Account (CA) Number !"
                                                                        ValidationExpression="^[0-9]{8,16}$" ValidationGroup="vgLandline"></asp:RegularExpressionValidator>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label class="label-control" for="projectinput6">
                                                                        Operator</label>
                                                                    <asp:DropDownList ID="ddlLandlineOperator" runat="server" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvLandlineOperator" runat="server" CssClass="rfv"
                                                                        ControlToValidate="ddlLandlineOperator" ErrorMessage="Please Select Landline Operator !"
                                                                        InitialValue="0" ValidationGroup="vgLandline" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label class="label-control" for="projectinput7">
                                                                        Circle</label>
                                                                    <asp:DropDownList ID="ddlCircleLandline" runat="server" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvCircleLandline" runat="server" CssClass="rfv"
                                                                        ControlToValidate="ddlCircleLandline" ErrorMessage="Please Select Circle !" InitialValue="0"
                                                                        ValidationGroup="vgLandline" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label for="projectinput2">
                                                                        Recharge Amount</label>
                                                                    <asp:TextBox ID="txtAmountLandline" runat="server" class="form-control" MaxLength="4"
                                                                        ValidationGroup="vgLandline"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="txtAmountLandline_FilteredTextBoxExtender" runat="server"
                                                                        TargetControlID="txtAmountLandline" FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <asp:RequiredFieldValidator ID="rfvAmountLandline" runat="server" CssClass="rfv"
                                                                        ControlToValidate="txtAmountLandline" ErrorMessage="Please Enter Amount !" ValidationGroup="vgLandline"
                                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-actions">
                                                            <asp:Button ID="btn_ProceedLandline" runat="server" Text="Proceed" class="btn btn-primary"
                                                                OnClick="btn_ProceedLandline_Click" ValidationGroup="vgLandline" OnClientClick="if (!Page_ClientValidate('vgLandline')){ return false; } this.disabled = true; this.value = 'Submitting...';"
                                                                UseSubmitBehavior="false" />&nbsp; &nbsp;
                                    <asp:Button ID="btn_CancelLandline" runat="server" Text="Cancel" class="btn btn-primary" UseSubmitBehavior="false" />

                                                        </div>

                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>

                                    <!-- this is bootstrp modal popup -->
                                    <%--    <div id="myModal" class="modal fade">
                                        <div class="modal-dialog">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                                    <h4 class="modal-title">Welcome in detail page</h4>
                                                </div>
                                                <div class="modal-body" style="overflow-y: scroll; max-height: 85%; margin-top: 50px; margin-bottom: 50px;">
                                                    <asp:Label ID="lblmessage" runat="server" ClientIDMode="Static"></asp:Label>
                                                    <asp:GridView ID="gvBookedBusList" runat="server" CssClass="table table-striped table-bordered bootstrap-datatable datatable responsive SmallText"
                                                        AutoGenerateColumns="false" AllowPaging="false"
                                                        PageSize="10" Width="100%"
                                                        AllowSorting="false" ShowHeaderWhenEmpty="true">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="RS" DataField="rs" SortExpression="fullName" />
                                                            <asp:BoundField HeaderText="DESC" DataField="desc" SortExpression="gender" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <div class="modal-footer">
                                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>

                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
                                    <%-- <div id="myModal" class="modal" tabindex="-1" role="dialog">
                                        <div class="modal-dialog" role="document">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h5 class="modal-title">Modal title</h5>
                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                        <span aria-hidden="true">&times;</span>
                                                    </button>
                                                </div>
                                                <div class="modal-body">
                                                    <asp:Label ID="Label1" runat="server" Text="name:"></asp:Label><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
                                                  <asp:GridView ID="gvBookedBusList" runat="server" CssClass="table table-striped table-bordered bootstrap-datatable datatable responsive SmallText"
                                                        AutoGenerateColumns="false" AllowPaging="false"
                                                        PageSize="10" Width="100%"
                                                        AllowSorting="false" ShowHeaderWhenEmpty="true">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="RS" DataField="rs" SortExpression="fullName" />
                                                            <asp:BoundField HeaderText="DESC" DataField="desc" SortExpression="gender" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <div class="modal-footer">
                                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                                    <button type="button" class="btn btn-primary">Save changes</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>



                                    <div id="myModal" class="modal fade">
                                        <div class="modal-dialog">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                                    <h4 class="modal-title">Article for C# Corner</h4>
                                                </div>
                                                <div class="modal-body" style="overflow-y: scroll; max-height: 85%; margin-top: 50px; margin-bottom: 50px;">
                                                    <asp:Label ID="lblmessage" runat="server" ClientIDMode="Static"></asp:Label>
                                                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered bootstrap-datatable datatable responsive SmallText"
                                                        AutoGenerateColumns="false" AllowPaging="false"
                                                        PageSize="10" Width="100%"
                                                        AllowSorting="false" ShowHeaderWhenEmpty="true">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="RS" DataField="rs" SortExpression="fullName" />
                                                            <asp:BoundField HeaderText="DESC" DataField="desc" SortExpression="gender" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <div class="modal-footer">
                                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>

</asp:Content>

