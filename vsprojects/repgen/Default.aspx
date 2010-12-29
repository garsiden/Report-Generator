<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="reportGrid" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" DataSourceID="clientsObjectDataSource" ForeColor="#333333" 
            GridLines="None" AllowSorting="True">
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="GUID" 
                    DataNavigateUrlFormatString="~/NewReport.aspx?guid={0}" DataTextField="Name" 
                    HeaderText="Client Name" SortExpression="Name" />
                <asp:BoundField DataField="MeetingDate" HeaderText="Meeting Date" 
                    SortExpression="MeetingDate" DataFormatString="{0:d}" />
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
    </div>
    <asp:ObjectDataSource ID="clientsObjectDataSource" runat="server" SelectMethod="GetRecentClients"
        TypeName="RSMTenon.Data.Client">
        <SelectParameters>
            <asp:Parameter DefaultValue="10" Name="number" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </form>
</body>
</html>
