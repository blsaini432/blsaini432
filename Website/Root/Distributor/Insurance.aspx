<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master"
    AutoEventWireup="true" CodeFile="Insurance.aspx.cs" Inherits="Root_Distributor_Insurance"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">
       Insurance Reqquest Form        </h3>
        </div>        
        <div class="row grid-margin">
            <div class="col-12">
                <div class="row">
                    <div class="col-lg-12">
                            <div class="card">
                    <div class="card-body">
                    <table class="table table-bordered table-responsive" width="100%">
    <tr>
        <td colspan="4" class="aleft" align="center">
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" 
                Text="Apply For Insurance Request"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="4" class="aleft">
    
                <label class="col-form-label">Note : Fields with are mandatory fields.<code>*</code></label>
              
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td colspan="3">
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" Font-Bold="True" 
                RepeatDirection="Horizontal" ValidationGroup="v" Width="644px" 
                onselectedindexchanged="RadioButtonList1_SelectedIndexChanged" 
                AutoPostBack="True">
                <asp:ListItem Selected="True">Bike</asp:ListItem>
                <asp:ListItem>Private Car</asp:ListItem>
                <asp:ListItem>Taxi Pass(Public)</asp:ListItem>
                <asp:ListItem>Good Pass(LCV)</asp:ListItem>
                <asp:ListItem>School Bus</asp:ListItem>
                <asp:ListItem>Other</asp:ListItem>
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="RadioButtonList1"
                InitialValue="-1" Display="Dynamic" ErrorMessage="Please Select At least One option"
                SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="other" runat="server" visible="false">
        <td>
            <h5>
                <strong><span class="red">*</span>Other</strong></h5>
        </td>
        <td>
            <asp:TextBox ID="txt_other" runat="server" MaxLength="50" 
                CssClass="form-control"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            <h5>
                <strong><span class="red">*</span>Registration No.</strong></h5>
        </td>
        <td>
            <asp:TextBox ID="txt_registrationno" runat="server" MaxLength="50" 
                CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                ControlToValidate="txt_registrationno" Display="Dynamic" ErrorMessage="Please Enter Registration No."
                SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
        </td>
        <td>
            <h5>
                <strong><span class="red">*</span>Date of application</strong>
            </h5>
        </td>
        <td>
            <asp:TextBox ID="txt_Rdate" runat="server" Enabled="false" MaxLength="10" CssClass="form-control"></asp:TextBox>
        </td>
    </tr>
    <tr runat="server" id="pan1">
        <td>
            <h5>
                <strong><span class="red">*</span>Make</strong>
            </h5>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txt_make" runat="server" MaxLength="50" 
                CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="panval1" runat="server" ErrorMessage="Make Required"
                ControlToValidate="txt_make" Display="Dynamic" SetFocusOnError="True" 
                ValidationGroup="v"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <h5>
                <strong><span class="red">*</span>Model</strong></h5>
        </td>
        <td>
            &nbsp;<asp:TextBox ID="txt_model" runat="server" MaxLength="50" 
                CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_model"
                Display="Dynamic" ErrorMessage="Please Enter model name !" SetFocusOnError="True"
                ValidationGroup="v"></asp:RequiredFieldValidator>
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr runat="server" id="pan2">
        <td>
            <h5>
                <strong><span class="red">*</span>Year</strong></h5>
        </td>
        <td>
            <asp:TextBox ID="txt_year" runat="server" MaxLength="50" 
                CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txt_year"
                Display="Dynamic" ErrorMessage="Please Enter year" SetFocusOnError="True"
                ValidationGroup="v"></asp:RequiredFieldValidator>
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            <h5>
                <strong><span class="red">*</span>Type</strong></h5>
        </td>
        <td colspan="3">
            <asp:RadioButtonList ID="RadioButtonList2" runat="server" Font-Bold="True" 
                RepeatDirection="Horizontal" ValidationGroup="v" Width="177px" 
                AppendDataBoundItems="True">
                <asp:ListItem>Third Party</asp:ListItem>
                <asp:ListItem>Full</asp:ListItem>
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="RadioButtonList2"
                InitialValue="-1" Display="Dynamic" ErrorMessage="Please Select At least One option"
                SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <h5>
                <strong><span class="red">*</span>Mobile</strong></h5>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txt_mobile" runat="server" MaxLength="50" 
                CssClass="form-control"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <h5>
                <strong><span class="red">*</span>Email</strong>
            </h5>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Email is not valid !"
                ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                ValidationGroup="v"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Please Enter Email !"
                ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <h5>
                <strong><span class="red">*</span>IDV(Optional)</strong></h5>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txt_idv" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Please Enter Aadhar Number !"
                ControlToValidate="txt_idv" Display="Dynamic" SetFocusOnError="True" 
                ValidationGroup="v"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <h5>
                <strong><span class="red">*</span> Last NCP(Optional)</strong></h5>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txt_ncp" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Please Enter Residence Address !"
                ControlToValidate="txt_ncp" Display="Dynamic" SetFocusOnError="True" 
                ValidationGroup="v"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <h5>
                <strong><span class="red">*</span>Upload RC -Front</strong>
            </h5>
        </td>
        <td>
            <asp:FileUpload ID="FileUploadrcfront" runat="server" />
        </td>
        <td>
            <asp:RequiredFieldValidator ID="rfvPanImage" runat="server" ControlToValidate="FileUploadrcfront"
                Display="Dynamic" ErrorMessage="Please Upload RC -Front Copy " ForeColor="Red"
                SetFocusOnError="True" ValidationGroup="v">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <h5>
                <strong><span class="red"></span>Upload RC -Back</strong></h5>
        </td>
        <td>
            <asp:FileUpload ID="FileUploadrcback" runat="server" />
        </td>
        <td>
<%--            <asp:RequiredFieldValidator ID="rfvPanImage0" runat="server" ControlToValidate="FileUploadrcback"
                Display="Dynamic" ErrorMessage="Please Upload RC -Back Copy" ForeColor="Red"
                SetFocusOnError="True" ValidationGroup="v">*</asp:RequiredFieldValidator>--%>
        </td>
    </tr>
    <tr>
        <td>
            <h5>
                <strong><span class="red"></span>Upload Last Insurance</strong></h5>
        </td>
        <td>
            <asp:FileUpload ID="filluploadlastinsurance" runat="server" />
        </td>
        <td>
          <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="filluploadlastinsurance"
                Display="Dynamic" ErrorMessage="Please Upload Last Insurance" ForeColor="Red"
                SetFocusOnError="True" ValidationGroup="v">*</asp:RequiredFieldValidator>
        </td>--%>
    </tr>
    <tr>
        <td>
            &nbsp;&nbsp;&nbsp;
        </td>
        <td>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" class="btn btn-primary"
                OnClick="btnSubmit_Click" />
        &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary" OnClick="btnReset_Click" />
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>

</table>
                        </div></div></div>
                </div><br />
                  
                  </div></div>
     </div>



</asp:Content>
