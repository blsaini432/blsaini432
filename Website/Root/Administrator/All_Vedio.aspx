<%@ Page Language="C#" MasterPageFile="~/Root/Administrator/AdminMaster.master" AutoEventWireup="true" CodeFile="All_Vedio.aspx.cs" Inherits="Root_Administrator_All_Vedio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="page-header">
            <h3 class="page-title">All Related Vedios
            </h3>
        </div>
        <div class="raw">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <asp:Repeater runat="server" ID="gvVideo">
                            <ItemTemplate>
                                <div class="col-md-4">
                                    <video style="width: 100%;" controls>
                                        <source src='<%# "../../Uploads/UploadFile/"+Eval("Video_Name")%>' type='video/mp4'>
                                    </video>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


