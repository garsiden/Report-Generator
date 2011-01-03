<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Pages_Model_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    Strategy:&nbsp;<asp:DropDownList ID="listStrategy" runat="server" DataSource="<%# GetStrategies() %>"
        DataTextField="Name" DataValueField="ID" AutoPostBack="True" 
        onselectedindexchanged="listStrategy_SelectedIndexChanged">
    </asp:DropDownList>
    <br />
    <br />
    <asp:GridView ID="gridModel" runat="server" AutoGenerateColumns="False" onrowdatabound="gridModel_RowDataBound" 
        DataSource="<%# GetAssetClasses() %>" DataKeyNames="ID" CssClass="listing">
        <RowStyle CssClass="odd" />
        <Columns>
            <asp:TemplateField HeaderText="Asset Class" SortExpression="AssetClassID">
<ItemStyle VerticalAlign="Top" CssClass="left"/>
                <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                    <br />
                    <br />
                    <br />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Investments">
                <ItemStyle VerticalAlign="Top" />
                <ItemTemplate>
                    <asp:GridView ID="gridChild" runat="server" AutoGenerateColumns="False" CssClass="listing" AlternatingRowStyle-CssClass="even" RowStyle-CssClass="odd" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="InvestmentName" ItemStyle-CssClass="left" HeaderStyle-Wrap="False" />
                            <asp:BoundField DataField="Weighting" HeaderText="Weighting" DataFormatString="{0:0.00%}"  ItemStyle-Width="20%" ItemStyle-CssClass="right" HeaderStyle-Wrap="False" />
                            <asp:BoundField DataField="ExpectedYield" HeaderText="Expected Yield" DataFormatString="{0:0.00%}"  ItemStyle-Width="20%" ItemStyle-CssClass="right" HeaderStyle-Wrap="False" />
                        </Columns>
                    </asp:GridView>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle CssClass="odd" />
    </asp:GridView>
    <asp:LinqDataSource ID="sourceAssetClass" runat="server" 
        ContextTypeName="RSMTenon.Data.RepGenDataContext" TableName="AssetClasses">
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="sourceDetail" runat="server" 
        ContextTypeName="RSMTenon.Data.RepGenDataContext" TableName="Models" 
        Where="AssetClassID == @AssetClassID &amp;&amp; StrategyID == @StrategyID" 
        Select="new (InvestmentName, Weighting, ExpectedYield)">
        <WhereParameters>
            <asp:Parameter Name="AssetClassID" Type="String" />
            <asp:ControlParameter ControlID="listStrategy" Name="StrategyID" 
                PropertyName="SelectedValue" Type="String" />
        </WhereParameters>
    </asp:LinqDataSource>
</asp:Content>
