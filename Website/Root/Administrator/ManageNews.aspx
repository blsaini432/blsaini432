<%@ Page Title="" Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true"
    CodeFile="ManageNews.aspx.cs" Inherits="cms_ManageNews" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">
                <asp:Label ID="lblAddEdit" runat="server"></asp:Label>
            </h3>
        </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div class="row grid-margin">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">News Title<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                 <asp:TextBox ID="txtNewsName" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNewsName" runat="server" ControlToValidate="txtNewsName"
                        Display="Dynamic" ErrorMessage="Please Enter News Name!" SetFocusOnError="True"
                        ValidationGroup="v"></asp:RequiredFieldValidator>

                            </div>
                        </div>
               
                        
                        <div class="form-group row">
                            <div class="col-lg-3">
                                <label class="col-form-label">News Description<code>*</code></label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txtnewsdescription" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvnewsdesc" runat="server" ControlToValidate="txtnewsdescription"
                                    Display="Dynamic" ErrorMessage="Please Enter News Description !" SetFocusOnError="True"
                                    ValidationGroup="v"></asp:RequiredFieldValidator>

                            </div>
                        </div>

                      <div class="form-group row">
                            <div class="col-lg-3">
                                
                            </div>
                            <div class="col-lg-8">
                             
<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary"
                        ValidationGroup="v" />
           
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" class="btn btn-danger" />
                   
                            </div>
                        </div>
                        </div></div>
                    </div></div></ContentTemplate>
                </asp:UpdatePanel></div>
</asp:Content>
