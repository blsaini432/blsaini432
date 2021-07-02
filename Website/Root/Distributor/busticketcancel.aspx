<%@ Page Title="" Language="C#" MasterPageFile="~/Root/Distributor/MemberMaster.master" AutoEventWireup="true" CodeFile="busticketcancel.aspx.cs" Inherits="Root_Distributor_busticketcancel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1 {
            width: 75%;
            border-collapse: collapse;
            border-style: solid;
            border-width: 1px;
        }

        .style9 {
            width: 227px;
            height: 43px;
        }

        .style2 {
            height: 22px;
        }

        .style3 {
            height: 21px;
        }

        .style4 {
            width: 100%;
            border-collapse: collapse;
            border-style: solid;
            border-width: 1px;
        }

        .style7 {
            width: 268px;
            height: 22px;
        }

        .style6 {
            width: 268px;
            height: 21px;
        }

        .style5 {
            width: 268px;
        }

        .style8 {
            height: 97px;
        }
    </style>
    <script type="text/javascript">
        function Validate(sender, args) {
            var gridView = document.getElementById("<%=GridView1.ClientID %>");
            var checkBoxes = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="content-wrapper">
                <div class="page-header">
                    <h3 class="page-title">Cancel Booking
                    </h3>
                </div>
                <div class="row grid-margin">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body">
                                <div class="container">
                                    <div class="row">
                                        <div class="col-md-3">

                                            <img alt="" class="style9" src="../../Uploads/Company/Logo/actual/637136759356612236logo.png" /></td>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Overline="False"
                                                Font-Size="X-Large" ForeColor="#666666" Text="BUS TICKET"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="lblfrom" runat="server" Font-Bold="True" Font-Overline="False"
                                                Font-Size="X-Large" ForeColor="Black"></asp:Label>
                                            &nbsp;→<asp:Label ID="lblto" runat="server" Font-Bold="True" Font-Overline="False"
                                                Font-Size="X-Large" ForeColor="Black"></asp:Label>
                                            &nbsp;&nbsp;
                    <asp:Label ID="lbldoj" runat="server" Font-Bold="True" Font-Overline="False"
                        Font-Size="X-Large" ForeColor="Black"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group row">
                                                <asp:Label ID="lblbustype" runat="server" Font-Bold="False"
                                                    Font-Overline="False" Font-Size="Large" ForeColor="Black"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group row">
                                                <asp:Label ID="lbltravelername" runat="server" Font-Bold="True"
                                                    Font-Overline="False" Font-Size="Large" ForeColor="Black"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <div class="form-group row">
                                                <asp:Label ID="lblfrom1" runat="server" Font-Bold="True" Font-Overline="False"
                                                    Font-Size="Large" ForeColor="Black">Location</asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lbllocation" runat="server" Font-Bold="False"
                                                        Font-Overline="False" Font-Size="Large" ForeColor="Black"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group row">

                                                <asp:Label ID="lblfrom2" runat="server" Font-Bold="True" Font-Overline="False"
                                                    Font-Size="Large" ForeColor="Black">Landmark</asp:Label>

                                                <div class="col-sm-6">
                                                    <asp:Label ID="lbllandmark" runat="server" Font-Bold="False"
                                                        Font-Overline="False" Font-Size="Large" ForeColor="Black"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group row">

                                                <asp:Label ID="lblfrom3" runat="server" Font-Bold="True" Font-Overline="False"
                                                    Font-Size="Large" ForeColor="Black">Boarding Point</asp:Label>

                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblboardingpoint" runat="server" Font-Bold="False"
                                                        Font-Overline="False" Font-Size="Large" ForeColor="Black"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group row">

                                                <asp:Label ID="lblfrom4" runat="server" Font-Bold="True" Font-Overline="False"
                                                    Font-Size="Large" ForeColor="Black">Contact</asp:Label>

                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblcontact" runat="server" Font-Bold="False"
                                                        Font-Overline="False" Font-Size="Large" ForeColor="Black"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group row">

                                                <asp:Label ID="lblfrom5" runat="server" Font-Bold="True" Font-Overline="False"
                                                    Font-Size="Large" ForeColor="Black">Total Fare</asp:Label>

                                                <div class="col-sm-6">
                                                    <asp:Label ID="lbltotalfare" runat="server" Font-Bold="False"
                                                        Font-Overline="False" Font-Size="Large" ForeColor="Black"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <div class="form-group row">
                                                <asp:Label ID="lblfrom6" runat="server" Font-Bold="True" Font-Overline="False"
                                                    Font-Size="Large" ForeColor="Black">Booking Date</asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblbookingdate" runat="server" Font-Bold="False"
                                                        Font-Overline="False" Font-Size="Large" ForeColor="Black"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group row">

                                                <asp:Label ID="lblfrom7" runat="server" Font-Bold="True" Font-Overline="False"
                                                    Font-Size="Large" ForeColor="Black">Order ID</asp:Label>

                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblorderid" runat="server" Font-Bold="False"
                                                        Font-Overline="False" Font-Size="Large" ForeColor="Black"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group row">
                                                <asp:Label ID="lblfrom8" runat="server" Font-Bold="True" Font-Overline="False"
                                                    Font-Size="Large" ForeColor="Black">PNR</asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblpnr" runat="server" Font-Bold="False" Font-Overline="False"
                                                        Font-Size="Large" ForeColor="Black"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group row">
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Overline="False"
                                                    Font-Size="Large" ForeColor="Black">PNR</asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Overline="False"
                                                        Font-Size="Large" ForeColor="Black"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group row">
                                                <asp:Label ID="lblfrom9" runat="server" Font-Bold="True" Font-Overline="False"
                                                    Font-Size="Large" ForeColor="Black">Ticket #</asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblticket" runat="server" Font-Bold="False"
                                                        Font-Overline="False" Font-Size="Large" ForeColor="Black"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group row">
                                                <asp:Label ID="lblfrom10" runat="server" Font-Bold="True" Font-Overline="False"
                                                    Font-Size="Large" ForeColor="Black">Reporting Time</asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lbldeparturetime" runat="server" Font-Bold="False"
                                                        Font-Overline="False" Font-Size="Large" ForeColor="Black"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group row">
                                                <asp:Label ID="lblfrom11" runat="server" Font-Bold="True" Font-Overline="False"
                                                    Font-Size="Large" ForeColor="Black">Departure Time</asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblreportingtime" runat="server" Font-Bold="False"
                                                        Font-Overline="False" Font-Size="Large" ForeColor="Black"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group row">
                                                <div class="col-sm-6">
                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                                        Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkRow" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="name" HeaderText="PassengerName" />
                                                            <asp:BoundField DataField="age" HeaderText="Age" />
                                                            <asp:BoundField DataField="mobile" HeaderText="Mobile" />
                                                            <asp:BoundField DataField="seats" HeaderText="SeatName" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group row">
                                                <div class="col-sm-6">
                                                    <asp:GridView ID="GridView2" runat="server" Width="100%" BorderWidth="0px">
                                                        <AlternatingRowStyle BorderWidth="0px" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group row">
                                                <div class="col-sm-6">
                                                    <asp:Button ID="btn_cancel" runat="server" BackColor="#cb3904"
                                                        CommandArgument='<%#Eval("booking_id")%>' CommandName="cancelticket"
                                                        CssClass="btn btn-default btn btn-primary" Font-Bold="True" ForeColor="White"
                                                        Text="Cancel Ticket" Width="154px" OnClick="btn_cancel_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="info-box">
                                        <div class="table-responsive">
                                            <div class="dataTables_scrollFoot" style="overflow: hidden; border: 0px; width: 100%;">
                                                <div class="dataTables_scrollFootInner" style="width: 1224px; padding-right: 17px;">
                                                    <table class="display dataTable" role="grid" style="margin-left: 0px; width: 1224px;"></table>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
            </div>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>


