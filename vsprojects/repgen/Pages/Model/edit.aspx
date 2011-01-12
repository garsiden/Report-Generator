<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Pages_Model_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:GridView ID="gridModel" runat="server" AlternatingRowStyle-CssClass="odd" CssClass="listing"
        RowStyle-CssClass="even" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="GUID"
        DataSourceID="sourceModel" ShowFooter="True" OnRowCommand="gridModel_RowCommand"
        OnRowDataBound="gridModel_RowDataBound" Width="100%" 
        Caption="Strategy Name">
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
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("PurchaseCharge") %>' Width="5em"></asp:TextBox>
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
    <asp:Label ID="labelTotalWeighting" runat="server" Text="labelTotalWeighting"></asp:Label>
    <br />
    <asp:Label ID="labelFixedInterestWeighting" runat="server" 
        Text="labelFixedInterestWeighting"></asp:Label>
    <br />
    <asp:Label ID="labelLongEquityWeighting" runat="server" 
        Text="labelLongEquityWeighting"></asp:Label>
    <br />
    <asp:LinqDataSource ID="sourceModel" runat="server" ContextTypeName="RSMTenon.Data.RepGenDataContext"
        EnableDelete="True" EnableInsert="True" EnableUpdate="True" OrderBy="AssetClassID, InvestmentName"
        TableName="Models" Where="StrategyID == @StrategyID">
        <WhereParameters>
            <asp:Parameter DefaultValue="CO" Name="StrategyID" Type="String" />
        </WhereParameters>
    </asp:LinqDataSource>
    <br />
    <asp:GridView ID="gridFixedInterest" runat="server" AutoGenerateColumns="False" Caption="Fixed Interest"
        CssClass="listing" DataSourceID="sourceFixedInterest" DataKeyNames="GUID" OnRowDataBound="gridFixedInterest_RowDataBound"
        ShowFooter="True">
        <RowStyle CssClass="odd" />
        <Columns>
            <asp:TemplateField HeaderText="Asset Class" SortExpression="AssetClassID">
                <EditItemTemplate>
                    <asp:Label ID="labelAssetClassName" runat="server" Text='<%# Eval("AssetGroupClass.AssetClass.Name") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="labelAssetClassName" runat="server" Text='<%# Eval("AssetGroupClass.AssetClass.Name") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="lnowrap" Width="55%" />
            </asp:TemplateField>
            <asp:BoundField DataField="Weighting" DataFormatString="{0:0.00%}" HeaderText="Weighting"
                SortExpression="Weighting">
                <FooterStyle CssClass="right" />
                <ItemStyle CssClass="right" Width="30%" />
            </asp:BoundField>
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                        Text="Update"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="linkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                        Text="Edit"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle CssClass="left" />
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle CssClass="even" />
    </asp:GridView>
    <asp:ObjectDataSource ID="sourceFixedInterest" runat="server" DataObjectTypeName="RSMTenon.Data.ModelBreakdown"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetModelBreakdown"
        TypeName="RSMTenon.Data.ModelBreakdown" UpdateMethod="UpdateModelBreakdown" ConflictDetection="CompareAllValues">
        <UpdateParameters>
            <asp:Parameter Name="modelBreakdown" Type="Object" />
            <asp:Parameter Name="original_modelBreakdown" Type="Object" />
        </UpdateParameters>
        <SelectParameters>
            <asp:Parameter DefaultValue="CO" Name="strategyId" Type="String" />
            <asp:Parameter DefaultValue="FIIN" Name="assetGroupId" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
<br />
    <asp:GridView ID="gridLongEquity" runat="server" AutoGenerateColumns="False" Caption="Long Equity"
        CssClass="listing" DataSourceID="sourceLongEquity" DataKeyNames="GUID"
        ShowFooter="True" onrowdatabound="gridLongEquity_RowDataBound">
        <RowStyle CssClass="odd" />
        <Columns>
            <asp:TemplateField HeaderText="Asset Class" SortExpression="AssetClassID">
                <EditItemTemplate>
                    <asp:Label ID="labelAssetClassName" runat="server" Text='<%# Eval("AssetGroupClass.AssetClass.Name") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="labelAssetClassName" runat="server" Text='<%# Eval("AssetGroupClass.AssetClass.Name") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="lnowrap" Width="55%" />
            </asp:TemplateField>
            <asp:BoundField DataField="Weighting" DataFormatString="{0:0.00%}" HeaderText="Weighting"
                SortExpression="Weighting">
                <FooterStyle CssClass="right" />
                <ItemStyle CssClass="right" Width="30%" />
            </asp:BoundField>
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                        Text="Update"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="linkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                        Text="Edit"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle CssClass="left" />
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle CssClass="even" />
    </asp:GridView>
    <br />
    <br />

    <asp:ObjectDataSource ID="sourceLongEquity" runat="server" DataObjectTypeName="RSMTenon.Data.ModelBreakdown"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetModelBreakdown"
        TypeName="RSMTenon.Data.ModelBreakdown" UpdateMethod="UpdateModelBreakdown" ConflictDetection="CompareAllValues">
        <UpdateParameters>
            <asp:Parameter Name="modelBreakdown" Type="Object" />
            <asp:Parameter Name="original_modelBreakdown" Type="Object" />
        </UpdateParameters>
        <SelectParameters>
            <asp:Parameter DefaultValue="CO" Name="strategyId" Type="String" />
            <asp:Parameter DefaultValue="LOEQ" Name="assetGroupId" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>
