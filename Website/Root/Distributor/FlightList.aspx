<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="FlightList.aspx.cs" Inherits="FlightList" %>

<%@ Register Src="~/Root/UC/FlightList.ascx" TagName="FlightList" TagPrefix="Uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <style type="text/css">
        .divWaiting {
            position: fixed;
            background-color: #FAFAFA;
            z-index: 2147483647 !important;
            opacity: 0.8;
            overflow: hidden;
            text-align: center;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
            padding-top: 20%;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
         
     <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                ClientIDMode="Predictable" ViewStateMode="Inherit" style="height: 100%; width: 100%;">
                <ProgressTemplate>
                    <div class="divWaiting">
                        <div id="progressBackgroundFilter">
                        </div>
                        <div id="processMessage">
                            <div class="loading_page" align="center" style="background-color: White; vertical-align: middle">
                                <img src="../../flight/Images/flight/progressbar.gif" alt="Loading..." style="float: none" />&#160;Please
                            Wait...
                            </div>
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                            
    <Uc:FlightList ID="Uc_FlightList" runat="server" />
                     </ContentTemplate>
            </asp:UpdatePanel>
    
</asp:Content>

