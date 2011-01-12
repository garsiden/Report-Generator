<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Pages_Benchmark_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h4>Edit Benchmarks</h4>
<br />
    <asp:GridView ID="gridBenchmark" runat="server" AutoGenerateColumns="False" CssClass="listing"
        DataKeyNames="ID" DataSourceID="sourceBenchmark" Width="70%">
        <RowStyle CssClass="odd" />
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
            <ControlStyle Width="90%" />
            <ItemStyle CssClass="lnowrap"/>
            </asp:BoundField>
            <asp:CommandField ShowEditButton="True" >
            <ItemStyle CssClass="left" />
            </asp:CommandField>
        </Columns>
        <AlternatingRowStyle CssClass="even" />
    </asp:GridView>
    <asp:LinqDataSource ID="sourceBenchmark" runat="server" 
        ContextTypeName="RSMTenon.Data.RepGenDataContext" EnableUpdate="True" 
        OrderBy="Name" TableName="Benchmarks">
    </asp:LinqDataSource>
</asp:Content>

