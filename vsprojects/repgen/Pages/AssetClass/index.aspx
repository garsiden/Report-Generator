<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Pages_AssetClass_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h4>Edit Asset Classes</h4>
<br />
    <asp:GridView ID="gridAssetClass" runat="server" class="listing"
    DataSourceID="sourceAssetClass" AllowSorting="True" 
    AutoGenerateColumns="False" DataKeyNames="ID" Width="60%">
        <RowStyle CssClass="odd" />
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ItemStyle-CssClass="lnowrap">

            <ControlStyle Width="90%" />
            <ItemStyle CssClass="lnowrap" />

            </asp:BoundField>
            <asp:CommandField ShowEditButton="True">
            <ItemStyle CssClass="lnowrap" Width="15%" />
            </asp:CommandField>
        </Columns>
        <AlternatingRowStyle CssClass="even" />
    </asp:GridView>
    <asp:LinqDataSource ID="sourceAssetClass" runat="server" 
        ContextTypeName="RSMTenon.Data.RepGenDataContext" EnableUpdate="True" 
        TableName="AssetClasses" OrderBy="Name">
    </asp:LinqDataSource>
</asp:Content>

