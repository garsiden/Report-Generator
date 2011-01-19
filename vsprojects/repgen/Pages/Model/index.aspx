<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Pages_Model_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<h4>View Models</h4>
<br />
    Strategy:&nbsp;<asp:DropDownList ID="listStrategy" runat="server" DataSource="<%# GetStrategies() %>"
        DataTextField="Name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="listStrategy_SelectedIndexChanged">
    </asp:DropDownList>
    <br />
    <br />
    <asp:GridView ID="gridModel" runat="server" AutoGenerateColumns="False" OnRowDataBound="gridModel_RowDataBound"
        DataSource="<%# GetAssetClasses() %>" DataKeyNames="ID" CssClass="listing" ShowFooter="True">
        <RowStyle CssClass="odd" />
        <Columns>
            <asp:TemplateField HeaderText="Asset Class" SortExpression="AssetClassID">
                <FooterStyle CssClass="lnowrap" />
                <ItemStyle VerticalAlign="Top" CssClass="left" />
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                    <br />
                    <br />
                    <br />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Investments">
                <FooterStyle CssClass="right" />
                <ItemStyle VerticalAlign="Top" />
                <FooterTemplate>
                    <asp:Table ID="tableFooterTotal" runat="server" Width="100%" Height="100%">
                        <asp:TableRow runat="server">
                            <asp:TableCell runat="server"></asp:TableCell>
                            <asp:TableCell runat="server" Width="15%" CssClass="right"></asp:TableCell>
                            <asp:TableCell runat="server" Width="15%" CssClass="right"></asp:TableCell>
                            <asp:TableCell runat="server" Width="15%" CssClass="right"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:GridView ID="gridChild" runat="server" AutoGenerateColumns="False" CssClass="listing"
                        AlternatingRowStyle-CssClass="even" RowStyle-CssClass="odd" Width="100%" ShowFooter="True"
                        OnRowDataBound="gridChild_RowDataBound">
                        <RowStyle CssClass="odd" />
                        <Columns>
                            <asp:BoundField DataField="InvestmentName" HeaderStyle-Wrap="False">
                                <FooterStyle CssClass="left" />
                                <HeaderStyle Wrap="False" />
                                <ItemStyle CssClass="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="WeightingHNW" HeaderText="Weight HNW" DataFormatString="{0:0.00%}"
                                SortExpression="WeightingHNW" HeaderStyle-Wrap="True">
                                <FooterStyle CssClass="right" />
                                <HeaderStyle Wrap="True" />
                                <ItemStyle CssClass="right" Width="15%"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="WeightingAffluent" DataFormatString="{0:0.00%}" 
                                HeaderText="Weight Affluent" SortExpression="WeightingAffluent" HeaderStyle-Wrap="True">
                                <FooterStyle CssClass="right" />
                                <HeaderStyle Wrap="True" />
                                <ItemStyle CssClass="right" Width="15%"/>
                            <ItemStyle CssClass="right"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="ExpectedYield" HeaderText="Expected Yield" DataFormatString="{0:0.00%}" HeaderStyle-Wrap="True">
                                <FooterStyle CssClass="right" />
                                <HeaderStyle Wrap="True" />
                                <ItemStyle CssClass="right" Width="15%"/>
                            </asp:BoundField>
                        </Columns>
                        <AlternatingRowStyle CssClass="even" />
                    </asp:GridView>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle CssClass="even" />
    </asp:GridView>
    <asp:LinqDataSource ID="sourceAssetClass" runat="server" ContextTypeName="RSMTenon.Data.RepGenDataContext"
        TableName="AssetClasses">
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="sourceDetail" runat="server" ContextTypeName="RSMTenon.Data.RepGenDataContext"
        TableName="Models" Where="AssetClassID == @AssetClassID &amp;&amp; StrategyID == @StrategyID">
        <WhereParameters>
            <asp:Parameter Name="AssetClassID" Type="String" />
            <asp:ControlParameter ControlID="listStrategy" Name="StrategyID" PropertyName="SelectedValue"
                Type="String" />
        </WhereParameters>
    </asp:LinqDataSource>
</asp:Content>
