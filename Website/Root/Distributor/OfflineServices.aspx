<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="OfflineServices.aspx.cs" Inherits="Root_Distributor_OfflineServices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
    <div class="page-header">
        <h3 class="page-title">Registration Form
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
                                    <label class="col-form-label" style="color: green">Service</label>

                                </div>
                                <div class="col-lg-8">
                                    <asp:DropDownList ID="ddlservice" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlservice_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlservice"
                                        Display="Dynamic" ErrorMessage="Please Select Service !" InitialValue="0"  SetFocusOnError="True"
                                        ValidationGroup="v"></asp:RequiredFieldValidator>
                                </div>
                            </div>


                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label" style="color: green">Amount (In Rs):--</label>

                                </div>
                                <div class="col-lg-8">
                                    <asp:Label ID="lblamt" Font-Bold="true" CssClass="form-control" Font-Size="Medium" ForeColor="Green" runat="server"
                                        Text=""></asp:Label>
                                </div>
                            </div>
                   
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label" style="color: green">form Instruction :--</label>

                                </div>
                                <div class="col-lg-8">
                                    
                                    <asp:TextBox ID="lblinstructions" Enabled="false" Font-Bold="true" CssClass="form-control" Font-Size="Medium" ForeColor="Green" runat="server"
                                        Text="" TextMode="MultiLine" Height="500px"></asp:TextBox>
                                </div>
                            </div>
                      
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Name<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_Name" runat="server" MaxLength="50" CssClass="form-control" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_Name"
                                        Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                        ValidationGroup="v"></asp:RequiredFieldValidator>

                                </div>

                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Mobile<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_Mobile" runat="server" MaxLength="12" CssClass="form-control" onkeypress="return isNumeric(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txt_Mobile"
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
                                    <label class="col-form-label">Address<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_address" runat="server" MaxLength="4000" TextMode="MultiLine"
                                        Rows="3" CssClass="form-control"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Address !"
                                        ControlToValidate="txt_address" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                             <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Company Name<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_companyname" runat="server" MaxLength="4000" TextMode="MultiLine"
                                        Rows="3" CssClass="form-control"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter CompanyName !"
                                        ControlToValidate="txt_companyname" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <label class="col-form-label">Address<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txt_comnpanyaddress" runat="server" MaxLength="4000" TextMode="MultiLine"
                                        Rows="3" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter CompanyAddress !"
                                        ControlToValidate="txt_comnpanyaddress" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group row" id="aadharcard" runat="server" >
                                <div class="col-lg-3">
                                    <label class="col-form-label">Upload Documents<code>*</code></label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" AllowMultiple="true" />
                                  
                                     <asp:Button ID="Button1" runat="server" Text="Uplaod" OnClick="Button1_Click" />  
                                        (You Can Upload Multiple Documents as per the instructions max Document 15 Allowed)
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-3">
                                </div>
                                <div class="col-lg-8">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary"
                                        ValidationGroup="v" />
                                
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

