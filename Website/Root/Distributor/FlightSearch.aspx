<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="FlightSearch.aspx.cs" Inherits="FlightSearch" %>

<%@ Register Src="~/Root/UC/FlightSearch.ascx" TagName="FlightSearch" TagPrefix="Uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link href="../../flight/js/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Google Font -->
    <link href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700"
        rel="stylesheet" />
    <!-- Theme style -->
    <link href="../../flight/style.css" rel="stylesheet" type="text/css" />
    <link href="../../flight/font-awesome/css/font-awesome.min.css" rel="stylesheet"
        type="text/css" />
    <link href="../../flight/et-line-font/et-line-font.css" rel="stylesheet" type="text/css" />
    <link href="../../flight/themify-icons/themify-icons.css" rel="stylesheet" type="text/css" />
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
    <!-- jQuery 3 -->
    <script src="../../flight/js/jquery.min.js" type="text/javascript"></script>

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
    <Uc:FlightSearch ID="Uc_FlightSearch" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
     <script src="../../flight/js/tether.min.js"></script>
        <script src="../../flight/js/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(document).bind('contextmenu', function (e) {
                e.preventDefault();
            })
            document.onkeydown = function (event) {
                event = (event || window.event);
                if (event.keyCode == 112 ||
                    event.keyCode == 113 ||
                    event.keyCode == 114 ||
                    event.keyCode == 115 ||
                    event.keyCode == 116 ||
                    event.keyCode == 117 ||
                    event.keyCode == 118 ||
                    event.keyCode == 119 ||
                    event.keyCode == 120 ||
                    event.keyCode == 121 ||
                    event.keyCode == 122 ||
                    event.keyCode == 123 ||
                    event.keyCode == 124 ||
                    event.keyCode == 125 ||
                    event.keyCode == 126) {
                    return false;
                }
            }
        </script>
</asp:Content>

