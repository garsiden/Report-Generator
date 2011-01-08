<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Pages_Model_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:GridView ID="gridModel" runat="server" AlternatingRowStyle-CssClass="odd" CssClass="listing"
        RowStyle-CssClass="even" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="GUID"
        DataSourceID="sourceModel" ShowFooter="True" 
        OnRowCommand="gridModel_RowCommand" 
        onrowdatabound="gridModel_RowDataBound" Width="100%">
        <RowStyle CssClass="even"></RowStyle>
        <Columns>
            <asp:TemplateField HeaderText="Investment Name" SortExpression="InvestmentName">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("InvestmentName") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textInvestmentNameAdd" runat="server"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("InvestmentName") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="left" />
                <ItemStyle CssClass="left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Asset Class" SortExpression="AssetClassID">
                <EditItemTemplate>
                    <asp:DropDownList ID="listAssetClass" runat="server" DataSource="<%# GetModelAssetClasses() %>"
                        DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("AssetClassID") %>'>
                    </asp:DropDownList>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="listAssetClassAdd" runat="server" DataSource="<%# GetModelAssetClasses() %>"
                        DataTextField="Name" DataValueField="ID">
                    </asp:DropDownList>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("AssetClass.Name") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="lnowrap" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Weighting" SortExpression="Weighting">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Weighting") %>' Width="5em"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textWeightingAdd" runat="server" Width="5em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Weighting", "{0:0.00%}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="right" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Expected Yield" SortExpression="ExpectedYield">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("ExpectedYield") %>' Width="5em"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textExpectedYieldAdd" runat="server" Width="5em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("ExpectedYield", "{0:0.00%}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="right" />
                <ItemStyle CssClass="right" Width="5em" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Purchase Charge" SortExpression="PurchaseCharge">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("PurchaseCharge") %>'
                        Width="5em"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textPurchaseChargeAdd" runat="server" Width="5em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("PurchaseCharge", "{0:C}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="right" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                        Text="Update"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:LinkButton ID="linkAdd" runat="server" CommandName="Insert">Add</asp:LinkButton>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                        Text="Edit"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                        Text="Delete"></asp:LinkButton>
                </ItemTemplate>
                <FooterStyle CssClass="left" />
                <ItemStyle CssClass="lnowrap" />
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle CssClass="odd"></AlternatingRowStyle>
    </asp:GridView>
<br />
    <asp:Label ID="labelTotalWeighting" runat="server" Text="Label"></asp:Label>
<br />
    <asp:LinqDataSource ID="sourceModel" runat="server" ContextTypeName="RSMTenon.Data.RepGenDataContext"
        EnableDelete="True" EnableInsert="True" EnableUpdate="True" OrderBy="AssetClassID, InvestmentName"
        TableName="Models" Where="StrategyID == @StrategyID">
        <WhereParameters>
            <asp:Parameter DefaultValue="CO" Name="StrategyID" Type="String" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:GridView ID="gridBreakdown" runat="server" AllowSorting="True" 
        AutoGenerateColumns="False" CssClass="listing" DataKeyNames="GUID" 
        DataSourceID="sourceBreakdown">
        <RowStyle CssClass="odd" />
        <Columns>
            <asp:TemplateField HeaderText="Asset Class" SortExpression="AssetClassID">
                <EditItemTemplate>
                    <asp:DropDownList ID="listBreakdownAssetClass" runat="server" DataSource="<%# GetBreakdownAssetClasses() %>"
                        DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("AssetClassID") %>'>
                   </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("AssetGroupClass.AssetClass.Name") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Asset Group">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" 
                        Text='<%# Bind("AssetGroupClass.AssetGroup.AssetClass.Name") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" 
                        Text='<%# Eval("AssetGroupClass.AssetGroup.AssetClass.Name") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="lnowrap" />
            </asp:TemplateField>
            <asp:BoundField DataField="Weighting" 
                HeaderText="Weighting" SortExpression="Weighting" 
                DataFormatString="{0:0.0000}">
            <ItemStyle CssClass="right" />
            </asp:BoundField>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True">
            <ItemStyle CssClass="left" />
            </asp:CommandField>
        </Columns>
        <AlternatingRowStyle CssClass="even" />
    </asp:GridView>
    <asp:LinqDataSource ID="sourceBreakdown" runat="server" 
        ContextTypeName="RSMTenon.Data.RepGenDataContext" EnableDelete="True" 
        EnableInsert="True" EnableUpdate="True" OrderBy="AssetClassID" 
        TableName="ModelBreakdowns" Where="StrategyID == @StrategyID">
        <WhereParameters>
            <asp:Parameter DefaultValue="CO" Name="StrategyID" Type="String" />
        </WhereParameters>
    </asp:LinqDataSource>
</asp:Content>
