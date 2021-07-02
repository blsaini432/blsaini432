<%@ Page Language="C#" MasterPageFile="~/Root/Retailer/membermaster.master" AutoEventWireup="true" CodeFile="All_PDF.aspx.cs" Inherits="Root_Retailer_All_PDF" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">All PDF
            </h3>
        </div>

        <div class="raw">
            <div class="card">
                <div class="card-body">
                     <div class="row">
                        <asp:Repeater runat="server" ID="gvVideo">
                            <ItemTemplate>
                                <div class="col-md-6">
                                   <%-- <video style="width: 100%;" controls>--%>
                                  
                                
                                     <%-- <img src='<%# "../../Uploads/UploadFile/"+Eval("Video_Name")%>' >--%>
                                 <%--   </video>--%>
                                 <asp:Label ID="lbfsdfewrewl" runat="server" Text='<%#Eval("title") %>'></asp:Label>
                                   
                                    <%--  <textbox ID='<%#Eval("title") %>'></textbox>--%>
                                </div>
                                <div class="col-md-6">
                                  
                                     <a href="../../Uploads/UploadFile/<%# Eval("Pdf_file") %>" target="_blank" class="btn btn-primary">Download PDF</a>
                                </div>
                               
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                 
                </div>
            </div>
        </div>
    </div>
</asp:Content>


