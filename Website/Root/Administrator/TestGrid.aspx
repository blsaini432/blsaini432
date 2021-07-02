<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestGrid.aspx.cs" Inherits="TestGrid" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="Uploaded Files">
                <ItemTemplate>
                    <asp:HyperLink ID="lblImages" Text='<%#Container.DataItem%>' Target="_blank" NavigateUrl='<%#Container.DataItem%>' runat="server"/>
                </ItemTemplate>
            </asp:TemplateField>
                 
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
