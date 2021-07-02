<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PanCorrectionForm.ascx.cs"
    Inherits="Root_UC_PanCorrectionForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<

<div class="content-wrapper">
    <div class="page-header">
        <h3 class="page-title">Pan Correction Form
        </h3>
    </div>
    <div class="row grid-margin">

        <div class="col-12">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body">
                            <table class="table table-responsive table-bordered table-hover" style="margin-left: 10px; width: 98%; background-color: #fff;">
                                <tr>
                                    <td colspan="6" class="aleft">
                                        <h3>
                                            <strong class="star">Note : Fields with <span>*</span> are mandatory fields.</strong></h3>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <h3>Amount For PAN Card(In Rs):-
                                        </h3>
                                    </td>
                                    <td colspan="2">
                                        <h3>
                                            <asp:Label ID="lblamt" Font-Bold="true" Font-Size="Medium" ForeColor="Green" runat="server"
                                                Text=""></asp:Label>
                                        </h3>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5>
                                            <strong><span>*</span>PAN Card NO.</strong>
                                        </h5>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txt_panNo" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="panval1" runat="server" ErrorMessage="Please Enter PAN Card Number !"
                                            ControlToValidate="txt_panNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5>
                                            <strong><span>*</span>Select Application Type</strong>
                                        </h5>
                                    </td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="ddl_applicationtype" AutoPostBack="true" runat="server" CssClass="form-control chosenselect"
                                            Style="width: 100%" OnSelectedIndexChanged="ddl_applicationtype_SelectedIndexChanged">
                                            <asp:ListItem Value="New" Selected="True">Apply For New PAN</asp:ListItem>
                                            <asp:ListItem Value="update">Update Existing PAN</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="trTxnNo" runat="server" visible="false">
                                    <td>
                                        <h5>
                                            <strong><span>*</span>Acknowledgement No.</strong>
                                        </h5>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTxnNo" runat="server" MaxLength="20" CssClass="form-control"
                                            OnTextChanged="txtTxnNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5>
                                            <strong><span>*</span>Category of Applicant</strong>
                                        </h5>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_pancat" runat="server" CssClass="form-control chosenselect"
                                            Style="width: 100%" OnSelectedIndexChanged="ddl_pancat_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Value="Individual" Selected="True">Individual</asp:ListItem>
                                            <asp:ListItem Value="Firm">Firm</asp:ListItem>
                                            <asp:ListItem Value="Body of Individuals">Body of Individuals</asp:ListItem>
                                            <asp:ListItem Value="Association of Persons">Association of Persons</asp:ListItem>
                                            <asp:ListItem Value="Local Authority">Local Authority</asp:ListItem>
                                            <asp:ListItem Value="Company">Company</asp:ListItem>
                                            <asp:ListItem Value="Trust">Trust</asp:ListItem>
                                            <asp:ListItem Value="Artificial Juridical Person">Artificial Juridical Person</asp:ListItem>
                                            <asp:ListItem Value="Goverment">Goverment</asp:ListItem>
                                            <asp:ListItem Value="Limited Liability Partnership">Limited Liability Partnership</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddl_pancat"
                                            InitialValue="-1" Display="Dynamic" ErrorMessage="Please Select  Category of Applicant !"
                                            SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <h5>
                                            <strong><span>*</span>Date of application</strong>
                                        </h5>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_Rdate" runat="server" Enabled="false" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5>
                                            <strong><span>*</span>Gender</strong>
                                        </h5>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rdoBtnLstGender" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="0" Text="Male"></asp:ListItem>
                                            <asp:ListItem Text="Female" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5>
                                            <strong><span>*</span>Applicant Name</strong>
                                        </h5>
                                    </td>
                                    <td>First Name
            <asp:TextBox ID="txt_fristname" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="rfvEmployeeName" runat="server" ControlToValidate="txt_fristname"
                Display="Dynamic" ErrorMessage="Please Enter Frist name !" SetFocusOnError="True"
                ValidationGroup="v"></asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td>Middle Name
            <asp:TextBox ID="txt_Middlename" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <span>*</span>Last Name
            <asp:TextBox ID="txt_lastname" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_lastname"
                                            Display="Dynamic" ErrorMessage="Please Enter Last name !" SetFocusOnError="True"
                                            ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr runat="server" id="pan2" visible="false">
                                    <td>
                                        <h5>
                                            <strong><span>*</span>Old Info OF Applicant </strong>
                                        </h5>
                                    </td>
                                    <td>
                                        <span>*</span>Last Name
            <asp:TextBox ID="txt_oldlastname" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="panval2" runat="server" ControlToValidate="txt_oldlastname"
                                            Display="Dynamic" ErrorMessage="Please Enter  Applicant Old Last Name !" SetFocusOnError="True"
                                            ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <span>*</span>First Name
            <asp:TextBox ID="txt_oldfristname" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="panval3" runat="server" ControlToValidate="txt_oldfristname"
                                            Display="Dynamic" ErrorMessage="Please Enter Applicant Old Frist Name  !" SetFocusOnError="True"
                                            ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>Middle Name
            <asp:TextBox ID="txt_oldmiddlename" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5>
                                            <strong><span>*</span>Name Required on PAN Card</strong>
                                        </h5>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txt_nameonpan" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txt_nameonpan"
                                            Display="Dynamic" ErrorMessage="Please Enter Name Required on PAN Card !" SetFocusOnError="True"
                                            ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr runat="server" id="trFathName">
                                    <td>
                                        <h5>
                                            <strong>Father Name</strong>
                                        </h5>
                                    </td>
                                    <td>First Name
            <asp:TextBox ID="txt_ffristname" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txt_ffristname"
                Display="Dynamic" ErrorMessage="Please Enter Father Frist name !" SetFocusOnError="True"
                ValidationGroup="v"></asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td>Middle Name
            <asp:TextBox ID="txt_Fmiddlename" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>Last Name/SurName
            <asp:TextBox ID="txt_flastname" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txt_flastname"
                Display="Dynamic" ErrorMessage="Please Enter Father Last name !" SetFocusOnError="True"
                ValidationGroup="v"></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr runat="server" id="pan3" visible="false">
                                    <td>
                                        <h5>
                                            <strong><span>*</span>Old Info Of Father</strong>
                                        </h5>
                                    </td>
                                    <td>
                                        <span>*</span>Last Name/SurName
            <asp:TextBox ID="txt_flastname1" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="panval4" runat="server" ControlToValidate="txt_flastname1"
                                            Display="Dynamic" ErrorMessage="Please Enter Father Last old name !" SetFocusOnError="True"
                                            ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <span>*</span>First Name
            <asp:TextBox ID="txt_ffristname1" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="panval5" runat="server" ControlToValidate="txt_ffristname1"
                                            Display="Dynamic" ErrorMessage="Please Enter Father old Frist name !" SetFocusOnError="True"
                                            ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>Middle Name
            <asp:TextBox ID="txt_Fmiddlename1" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5>
                                            <strong><span>*</span>Date of Birth (dd/MM/yyyy)</strong>
                                        </h5>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txt_dob" runat="server" MaxLength="50" onkeypress="return false;"
                                            CssClass="form-control"></asp:TextBox>
                                        <cc1:CalendarExtender runat="server" ID="txt_doasdsab" Format="dd/MM/yyyy" Animated="False"
                                            PopupButtonID="txt_dob" TargetControlID="txt_dob">
                                        </cc1:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_dob"
                                            Display="Dynamic" ErrorMessage="Please Enter Date of Birth (dd/MM/yyyy) !" SetFocusOnError="True"
                                            ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5>
                                            <strong>Contact Details</strong>
                                        </h5>
                                    </td>
                                    <td>ISD Code
            <asp:TextBox ID="txt_isdcode" runat="server" MaxLength="5" Text="91" Enabled="false"
                CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txt_isdcode"
                                            Display="Dynamic" ErrorMessage="Please Enter ISD Code !" SetFocusOnError="True"
                                            ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>STD Code
            <asp:TextBox ID="txt_std" runat="server" MaxLength="5" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <span>*</span>Mobile Number
            <asp:TextBox ID="txt_telno" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtMobile_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" FilterType="Numbers" TargetControlID="txt_telno">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ErrorMessage="Please Enter Mobile !"
                                            ControlToValidate="txt_telno" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5>
                                            <strong><span></span>Email</strong>
                                        </h5>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Email is not valid !"
                                            ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ValidationGroup="v"></asp:RegularExpressionValidator>

                                    </td>
                                </tr>
                                <tr id="trAadhar" runat="server">
                                    <td>
                                        <h5>
                                            <strong><span>*</span> Aadhar Number</strong>
                                        </h5>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txt_adhar" runat="server" MaxLength="12" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAadhar" runat="server" ErrorMessage="Please Enter Aadhar Number !"
                                            ControlToValidate="txt_adhar" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                            Enabled="True" FilterType="Numbers" TargetControlID="txt_adhar">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5>
                                            <strong><span>*</span> Residence Address</strong>
                                        </h5>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txt_resiadd" runat="server" MaxLength="4000" TextMode="MultiLine"
                                            Rows="3" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Please Enter Residence Address !"
                                            ControlToValidate="txt_resiadd" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <tr style="display: none;">
                                            <td>
                                                <asp:CheckBox ID="chk_same" AutoPostBack="true" OnCheckedChanged="chk_check" runat="server" />
                                            </td>
                                            <td colspan="3">
                                                <strong>Same AS Residence Address</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <h5>
                                                    <strong><span></span>Office Address</strong>
                                                </h5>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txt_cadd" runat="server" MaxLength="4000" TextMode="MultiLine" Rows="3"
                                                    CssClass="form-control"></asp:TextBox>

                                            </td>
                                        </tr>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <tr style="display: none;">
                                    <td>
                                        <h5>
                                            <strong><span>*</span>Identity Proof</strong>
                                        </h5>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Identitytype" runat="server" CssClass="form-control chosenselect"
                                            Style="width: 100%">
                                            <asp:ListItem Value="-1" Selected="True">Select Identity Proof Type</asp:ListItem>
                                            <asp:ListItem Value="AdharCard">AdharCard</asp:ListItem>
                                            <asp:ListItem Value="passport">passport</asp:ListItem>
                                            <asp:ListItem Value="driving licence">driving licence</asp:ListItem>
                                            <asp:ListItem Value="Voter ID">Voter ID</asp:ListItem>
                                            <asp:ListItem Value="Ration card">Ration card</asp:ListItem>
                                            <asp:ListItem Value="Arm's License">Arm's License</asp:ListItem>
                                            <asp:ListItem Value="Photo identity card issued by the Central or State Government or Public Sector Undertaking">Photo identity card issued by the Central or State Government or Public Sector Undertaking</asp:ListItem>
                                            <asp:ListItem Value="Pensioner card having photograph">Pensioner card having photograph</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddl_Identitytype"
                InitialValue="-1" Display="Dynamic" ErrorMessage="Please Select Identity Proof Type!"
                SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="FileUploadIdentityImage" runat="server" />
                                        <%--            <asp:RequiredFieldValidator ID="rfvPanImage" runat="server" ControlToValidate="FileUploadIdentityImage"
                Display="Dynamic" ErrorMessage="Please Select Identity Proof " ForeColor="Red"
                SetFocusOnError="True" ValidationGroup="v">*</asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td>Note:PDF File only
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>
                                        <h5>
                                            <strong>Address Proof</strong>
                                        </h5>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_addresproff" runat="server" CssClass="form-control chosenselect"
                                            Style="width: 100%" ClientIDMode="Static">
                                            <asp:ListItem Value="-1" Selected="True">Select Identity Proof Type</asp:ListItem>
                                            <asp:ListItem Value="AdharCard">AdharCard</asp:ListItem>
                                            <asp:ListItem Value="passport">passport</asp:ListItem>
                                            <asp:ListItem Value="driving licence">driving licence</asp:ListItem>
                                            <asp:ListItem Value="Voter ID">Voter ID</asp:ListItem>
                                            <asp:ListItem Value=" Passport of the spouse"> Passport of the spouse</asp:ListItem>
                                            <asp:ListItem Value="Post office passbook having address">Post office passbook having address</asp:ListItem>
                                            <asp:ListItem Value="Latest property tax assessment order">Latest property tax assessment order</asp:ListItem>
                                            <asp:ListItem Value="Domicile certificate issued by the Government">Domicile certificate issued by the Government</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddl_addresproff"
                InitialValue="-1" Display="Dynamic" ErrorMessage="Please Select Address Proof Type!"
                SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="FileUploadadressImage" runat="server" />
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="FileUploadadressImage"
                Display="Dynamic" ErrorMessage="Please Select Address Proof " ForeColor="Red"
                SetFocusOnError="True" ValidationGroup="v">*</asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td>Note:PDF File only
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>
                                        <h5>
                                            <strong>DOB Proof</strong>
                                        </h5>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_dobproff" runat="server" CssClass="form-control chosenselect"
                                            Style="width: 100%">
                                            <asp:ListItem Value="-1" Selected="True">Select Identity Proof Type</asp:ListItem>
                                            <asp:ListItem Value="AdharCard">AdharCard</asp:ListItem>
                                            <asp:ListItem Value="passport">passport</asp:ListItem>
                                            <asp:ListItem Value="driving licence">driving licence</asp:ListItem>
                                            <asp:ListItem Value="Voter ID">Voter ID</asp:ListItem>
                                            <asp:ListItem Value=" Matriculation certificate or Mark sheet of recognized board"> Matriculation certificate or Mark sheet of recognized board</asp:ListItem>
                                            <asp:ListItem Value="Birth certificate issued by the municipal authority or any office authorised">Birth certificate issued by the municipal authority or any office authorised</asp:ListItem>
                                            <asp:ListItem Value="Photo identity card issued by the Central or State Government or Public Sector Undertaking">Photo identity card issued by the Central or State Government or Public Sector Undertaking</asp:ListItem>
                                            <asp:ListItem Value="Domicile certificate issued by the Government">Domicile certificate issued by the Government</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddl_dobproff"
                InitialValue="-1" Display="Dynamic" ErrorMessage="Please Select DOB Proof Type!"
                SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="FiledobressImage" runat="server" />
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="FiledobressImage"
                Display="Dynamic" ErrorMessage="Please Select DOB Proof " ForeColor="Red" SetFocusOnError="True"
                ValidationGroup="v">*</asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td>Note:PDF File only
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td>
                                        <h5>
                                            <strong><span>*</span>Upload Correction Form</strong>
                                        </h5>
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="fupForm49" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="fupForm49"
                                            Display="Dynamic" ErrorMessage="Please Select Form " ForeColor="Red" SetFocusOnError="True"
                                            ValidationGroup="v">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td>Note:PDF File only
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>
                                        <h5>
                                            <strong><span>*</span>Upload Form No. 49A(II)</strong>
                                        </h5>
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="fupForm49A" runat="server" />
                                        <asp:RequiredFieldValidator ID="rfvForm49A" runat="server" ControlToValidate="fupForm49A"
                                            Display="Dynamic" ErrorMessage="Please Select Form " ForeColor="Red" SetFocusOnError="True"
                                            ValidationGroup="v">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td>Note:PDF File only
                                    </td>s
                                </tr>
                                <tr runat="server">
                                    <td>
                                        <h5>
                                            <strong><span>*</span>Download Correction Form </strong>
                                        </h5>
                                    </td>
                                    <td>
                                       
                                        <a href="../../Uploads/Servicesimage/Actual/PAN%20Correction%20Application.pdf" target="_blank" class="btn btn-primary">Click here</a>
                                  
                                    </td>

                                </tr>
                                <tr runat="server" id="pan5" visible="false">
                                    <td>
                                        <h5>
                                            <strong><span>*</span>Surrender PANs</strong>
                                        </h5>
                                    </td>
                                    <td>PAN 1
            <asp:TextBox ID="txt_surendrapan1" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>PAN 2
            <asp:TextBox ID="txt_surendrapan2" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>PAN 3
            <asp:TextBox ID="txt_surendrapan3" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td>&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" class="btn btn-primary"
                                            OnClick="btnSubmit_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary" OnClick="btnReset_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:ValidationSummary ID="ValidationSummary" runat="server" ClientIDMode="Static"
                                            ValidationGroup="v" />
                                    </td>
                                </tr>
                            </table>


                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
