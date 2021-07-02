<%@ Page Language="C#" MasterPageFile="~/Front.master" AutoEventWireup="true" CodeFile="StaticData.aspx.cs"
    Inherits="StaticData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<style>
p{line-height:1.4em;}
.about_us{ margin:0% 0px;}
.section-title{ margin-bottom:2%;margin-top:15%; text-align:left;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:Repeater ID="repPage" runat="server">
        <ItemTemplate>
    <div class="intro-header1"></div>
    <section class="about_us page-section" id="about"> 
        <div class="container">
            <div class="section-title text-center"> 
                <h2 class="section-heading"><asp:Literal ID="Literal2" runat="server" Text='<%#Eval("pageheading")%>'></asp:Literal></h2>
            </div> 
            <div class="row">
                <div class="col-sm-12 text-left" style="margin-bottom:20px;">
                    <asp:Literal ID="Literal1" runat="server" Text='<%#Eval("PageDesc")%>'></asp:Literal>
                </div>
            </div>
        </div>
    </section>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
