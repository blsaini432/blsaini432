<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master"
    AutoEventWireup="true" CodeFile="psa_reg.aspx.cs" Inherits="Root_Distributor_psa_reg"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">
              PSA -UTI Panel
            </h3>
        </div>

                <div class="row grid-margin">
                    <div class="col-12">
                         <div class="row">
                    <div class="col-lg-12">
                            <div class="card">
                    <div class="card-body">
                    <div class="faq-section">
                    <div class="container-fluid bg-success py-2">
                      <p class="mb-0 text-white">Important Points</p>
                    </div>
                    <div id="accordion-1" class="accordion">
                      <div class="card">
                        <div id="collapseOne" class="collapse show" aria-labelledby="headingOne" data-parent="#accordion-1" style="">
                          <div class="card-body">
                            1.Aadhar Should be clear in size<br />
                            2.PAN Should be clear in size<br />
                            3.Document Upload Size should not be greater than 1 MB.<br />
                            4. All * Sign are mandatory fields.<br />
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                        </div></div></div>
                </div><br />
                        <div class="row">
                               <div class="col-lg-12">
                        <div class="card">
                            <div class="card-body">
                                <asp:Label ID="lbl_Status" runat="server" Visible="false" Style="color: Red"></asp:Label>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Name<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtFirstName" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                                            Display="Dynamic" ErrorMessage="Please Enter FirstName !" SetFocusOnError="True"
                                            ValidationGroup="reg"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Email<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_Email" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_Email" runat="server" ControlToValidate="txt_Email" ValidationGroup="reg" ErrorMessage="Please Enter Email"></asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter Valid Email" ControlToValidate="txt_Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Mobile<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_Mno" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_Mno" runat="server" ControlToValidate="txt_Mno" ValidationGroup="reg" ErrorMessage="Please Enter Mobile"></asp:RequiredFieldValidator></td>
                                  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_loginID" runat="server"
                                      TargetControlID="txt_Mno" ValidChars="0123456789">
                                  </cc1:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="regExp_loginID" runat="server" CssClass="rfv"
                                            ControlToValidate="txt_Mno" ErrorMessage="Input correct mobile number !"
                                            ValidationGroup="reg" SetFocusOnError="true" ValidationExpression="^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[6789]\d{9}$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <label class="col-form-label">Upload Aadhar<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:FileUpload ID="fu_Identity" runat="server" />&nbsp;
                        <asp:Button ID="btn_Identity" runat="server" Text="Upload Aadhar" CssClass="btn btn-warning btn-rounded btn-fw" 
                            OnClick="btn_Identity_Click" />
                                        

                                    </div>
                                </div>

                                <div class="form-group row">

                                    <div class="col-lg-3">
                                        <label class="col-form-label">Upload PAN<code>*</code></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:FileUpload ID="fu_Address" runat="server" />&nbsp;
                        <asp:Button ID="btn_Address" runat="server" Text="Upload PAN" CssClass="btn btn-warning btn-rounded btn-fw" 
                            OnClick="btn_Address_Click" Width="155px" />
                                        
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-8">

                                        <asp:Button ID="btn_Submit" runat="server" Text="Submit" OnClick="btn_Submit_Click" class="btn btn-primary"
                                            ValidationGroup="reg" />

                                        <asp:Button ID="btn_Reset" runat="server" Text="Cancel" OnClick="btn_Reset_Click" class="btn btn-danger" />

                                    </div>
                                </div>
                            </div>
                        </div></div></div>
                    </div>
                </div>
              
        </div>
</asp:Content>
