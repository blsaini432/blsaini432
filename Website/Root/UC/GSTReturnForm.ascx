<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GSTReturnForm.ascx.cs" Inherits="Root_UC_GSTReturnForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script type="text/javascript">
       <!--
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    function isAlphabets(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if ((charCode <= 93 && charCode >= 65) || (charCode <= 122 && charCode >= 97))
            return true;
        else
            return false;
    }

    function isNumeric(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode >= 48 && charCode <= 57)
            return true;
        else
            return false;
    }
    function isAlphaNumeric(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if ((charCode > 47 && charCode < 58) || (charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))
            return true;
        else
            return false;
    }
    //-->
</script>
<%--<asp:HiddenField ID="hdn_amount" runat="server" />
<table class="table table-bordered table-hover ">
    <tr>
       
    </tr>
    <tr>
        <td colspan="2">
            <h4 style="color:darkgreen">
                Amount For GST 
                Return (In Rs):-
            </h4>
        </td>
        <td colspan="3">
            <h3>
                <asp:Label ID="lblamt" Font-Bold="true" Font-Size="Medium" ForeColor="Green" runat="server"
                    Text=""></asp:Label>
            </h3>
        </td>
    </tr>
    <tr>
        <td>
            <h5>
                <strong><span class="red"></span>Name as Per PAN Card<code>*</code></strong>
            </h5>
        </td>
        <td>
            <asp:TextBox ID="txt_NamePan" runat="server" MaxLength="50" CssClass="form-control" onkeypress="return isAlphabets(event)"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txt_NamePan"
                Display="Dynamic" ErrorMessage="Please Enter Name on PAN Card !" SetFocusOnError="True"
                ValidationGroup="v"></asp:RequiredFieldValidator>
            
        </td>
        <td colspan="2">
            <h5>
                <strong><span class="red"></span>Father's Name<code>*</code></strong>
            </h5>
        </td>
        <td>
            <asp:TextBox ID="txt_FatherName" runat="server" MaxLength="50" CssClass="form-control" onkeypress="return isAlphabets(event)"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txt_FatherName"
                Display="Dynamic" ErrorMessage="Please Enter Father Name !" SetFocusOnError="True"
                ValidationGroup="v"></asp:RequiredFieldValidator>            
        </td>
    </tr>
    <tr>
        <td>
            <h5>
                <strong><span class="red"></span>Date of Birth <code>*</code></strong>
            </h5>
        </td>
        <td>
            <asp:TextBox ID="txt_dob" runat="server" MaxLength="50" onkeypress="return false;"
                CssClass="form-control"></asp:TextBox>
            <cc1:CalendarExtender runat="server" ID="txt_doasdsab" Format="dd/MM/yyyy" Animated="False"
                PopupButtonID="txt_dob" TargetControlID="txt_dob">
            </cc1:CalendarExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_dob"
                Display="Dynamic" ErrorMessage="Please Enter Date of Birth (dd/MM/yyyy) !" SetFocusOnError="True"
                ValidationGroup="v"></asp:RequiredFieldValidator>
        </td>
        <td colspan="2">
            <h5>
                <strong><span class="red"></span>Mobile No.<code>*</code></strong>
            </h5>
        </td>
        <td>
            <asp:TextBox ID="txt_MobilePartner" runat="server" MaxLength="12" CssClass="form-control" onkeypress="return isNumeric(event)"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txt_MobilePartner"
                Display="Dynamic" ErrorMessage="Please Enter Mobile !" SetFocusOnError="True"
                ValidationGroup="v"></asp:RequiredFieldValidator>            
        </td>
    </tr>
    <tr>
        <td>
            <h5>
                <strong><span class="red"></span>Email ID <code>*</code></strong>
            </h5>
        </td>
        <td>
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
            &nbsp;<h5>
                <strong><span class="red">Permanent </span>Address<code>*</code></strong>
            </h5>
        </td>
        <td>
            <asp:TextBox ID="txt_permanentaddress" runat="server" MaxLength="4000" TextMode="MultiLine"
                Rows="3" CssClass="form-control"></asp:TextBox>
        </td>
        <td colspan="2">
            <h5>
                <strong><span class="red"></span>Address<code>*</code></strong>
            </h5>
        </td>
        <td>
            <asp:TextBox ID="txt_presentaddress" runat="server" MaxLength="4000" TextMode="MultiLine"
                Rows="3" CssClass="form-control"></asp:TextBox>
        </td>
    </tr>
    
    <tr>
        <td>
            </td>
    </tr>
    <tr>
        <td>
            <h5>
                <strong><span class="red">Copy Of GST</span>(Upto 100KB)<code>*</code></strong>
            </h5>
        </td>
        <td>
            <asp:FileUpload ID="fup_Photo" runat="server" />
            <asp:RequiredFieldValidator ID="rfvPanImage" runat="server" ControlToValidate="fup_Photo"
                Display="Dynamic" ErrorMessage="Please Select Photo " ForeColor="Red" SetFocusOnError="True"
                ValidationGroup="v">*</asp:RequiredFieldValidator>
        </td>
        <td colspan="2">
            <h5>
                <strong><span class="red">Sale Purchase Excel</span>(Upto 1MB)<code>*</code></strong>
            </h5>
        </td>
        <td>
            <asp:FileUpload ID="fup_Aadhar" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="fup_Aadhar"
                Display="Dynamic" ErrorMessage="Please Select Aadhar " ForeColor="Red" SetFocusOnError="True"
                ValidationGroup="v">*</asp:RequiredFieldValidator>
        </td>
    </tr>
   
    <tr>
        <td>
            <h5>
                <strong><span class="red">Download Sample Excelsheet </span></strong>&nbsp;</h5>
        </td>
        <td>
       <a href="../../GSTIN%20Format%20For%20DATA.xlsx" class="btn btn-info" > Download
      
           </a>
        </td>
         <td colspan="2">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" class="btn btn-primary"
                OnClick="btnSubmit_Click" />
        </td>
         <td>
            <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-danger" OnClick="btnReset_Click" />
        </td>
    </tr>
 
    
</table>--%>


<div class="content-wrapper">
    <div class="page-header">
        <h3 class="page-title">GST Return Form
        </h3>
    </div>

    <div class="row grid-margin">
       
        <div class="col-12">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body">
                            <asp:Label ID="lbl_Status" runat="server" Visible="false" Style="color: Red"></asp:Label>
                             <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label" style="color: green">Amount For GST Return (In Rs):--</label>

                                </div>
                                <div class="col-lg-8">

                                    <asp:Label ID="lblamt" Font-Bold="true" CssClass="form-control" Font-Size="Medium" ForeColor="Green" runat="server"
                                        Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Name<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_NamePan" runat="server" MaxLength="50" CssClass="form-control" onkeypress="return isAlphabets(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txt_NamePan"
                                        Display="Dynamic" ErrorMessage="Please Enter Name on PAN Card !" SetFocusOnError="True"
                                        ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Father Name<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_FatherName" runat="server" MaxLength="50" CssClass="form-control" onkeypress="return isAlphabets(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txt_FatherName"
                                        Display="Dynamic" ErrorMessage="Please Enter Father Name !" SetFocusOnError="True"
                                        ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">DOB<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_dob" runat="server" MaxLength="50" onkeypress="return false;"
                                        CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="txt_doasdsab" Format="dd/MM/yyyy" Animated="False"
                                        PopupButtonID="txt_dob" TargetControlID="txt_dob">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_dob"
                                        Display="Dynamic" ErrorMessage="Please Enter Date of Birth (dd/MM/yyyy) !" SetFocusOnError="True"
                                        ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Mobile<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_MobilePartner" runat="server" MaxLength="12" CssClass="form-control" onkeypress="return isNumeric(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txt_MobilePartner"
                                        Display="Dynamic" ErrorMessage="Please Enter Mobile !" SetFocusOnError="True"
                                        ValidationGroup="v"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Email<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Email is not valid !"
                                        ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="v"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Please Enter Email !"
                                        ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Permanent Address<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_permanentaddress" runat="server" MaxLength="4000" TextMode="MultiLine"
                                        Rows="3" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Address<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_presentaddress" runat="server" MaxLength="4000" TextMode="MultiLine"
                                        Rows="3" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Sale Purchase<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="fup_Aadhar" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="fup_Aadhar" ErrorMessage="*" ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>

                            <div class="form-group row">

                                <div class="col-lg-3">
                                    <label class="col-form-label">Copy Of Gst <code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="fup_Photo" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="fup_Photo" ErrorMessage="*" ValidationGroup="reg"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                         
                            <div class="form-group row">
                                <div class="col-lg-3">
                                </div>
                                <div class="col-lg-8">
                                    <%-- <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="s" class="btn btn-primary" OnClick="btnSubmit_Click" />
                                      <asp:Button ID="btnReset" runat="server" Text="Reset" ValidationGroup="s" class="btn btn-primary" OnClick="btnReset_Click" />--%>
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary"
                                        ValidationGroup="v" />

                                    <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" class="btn btn-danger" />

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</div>
