<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:GridView ID="gridClient" runat="server" AutoGenerateColumns="False" DataKeyNames="GUID"
        DataSourceID="sourceClient">
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="GUID" DataNavigateUrlFormatString="~/Pages/Report.aspx?guid={0}"
                DataTextField="Name" />
            <asp:BoundField DataField="MeetingDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="MeetingDate"
                SortExpression="MeetingDate" />
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="sourceClient" runat="server" DataObjectTypeName="RSMTenon.Data.Client"
        DeleteMethod="DeleteClient" SelectMethod="GetRecentClients" TypeName="RSMTenon.Data.Client"
        InsertMethod="InsertClient" OldValuesParameterFormatString="original_{0}" UpdateMethod="UpdateStockExchange">
        <SelectParameters>
            <asp:Parameter DefaultValue="10" Name="number" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</body>
</html>
