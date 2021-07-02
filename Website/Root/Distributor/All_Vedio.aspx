<%@ Page Language="C#" MasterPageFile="~/Root/Distributor/membermaster.master" AutoEventWireup="true" CodeFile="All_Vedio.aspx.cs" Inherits="Root_Distributor_All_Vedio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">All Videos
            </h3>
        </div>

        <div class="raw">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <asp:Repeater runat="server" ID="gvVideo">
                            <ItemTemplate>
                                <div class="col-md-6">
                                    <asp:Label ID="lbfsdfewrewl" runat="server" Text='<%#Eval("title") %>'></asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <a href="../../Uploads/UploadFile/<%# Eval("Video_Name") %>" target="_blank" class="btn btn-info">Click Here Video</a>
                                </div>

                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>


