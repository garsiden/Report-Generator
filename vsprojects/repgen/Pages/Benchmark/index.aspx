<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Pages_Benchmark_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView ID="gridBenchmark" runat="server" 
    DataSourceID="sourceBenchmark" AllowSorting="True" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
        CellPadding="3" DataKeyNames="ID" GridLines="Vertical">
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
            </asp:BoundField>
            <asp:CheckBoxField DataField="IsGroup" HeaderText="IsGroup" ReadOnly="True" 
                SortExpression="IsGroup" />
            <asp:CommandField ShowEditButton="True" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
    </asp:GridView>
    <asp:LinqDataSource ID="sourceBenchmark" runat="server" 
        ContextTypeName="RSMTenon.Data.Benchmark" EnableUpdate="True" 
        TableName="AssetClasses">
    </asp:LinqDataSource>
</asp:Content>

