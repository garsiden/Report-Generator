<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Pages_AssetClass_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView ID="gridAssetClass" runat="server" 
    DataSourceID="sourceAssetClass" AllowSorting="True" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
        CellPadding="3" DataKeyNames="ID" GridLines="Vertical">
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
            </asp:BoundField>
            <asp:CheckBoxField DataField="IsGroup" HeaderText="Group?" ReadOnly="True" 
                SortExpression="IsGroup" />
            <asp:CommandField ShowEditButton="True" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
    </asp:GridView>
    <asp:LinqDataSource ID="sourceAssetClass" runat="server" 
        ContextTypeName="RSMTenon.Data.RepGenDataContext" EnableUpdate="True" 
        TableName="AssetClasses" OrderBy="Name">
    </asp:LinqDataSource>
</asp:Content>

