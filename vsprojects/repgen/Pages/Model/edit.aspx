<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Pages_Model_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h4>
        Edit Models</h4>
    <br />
    Strategy:&nbsp;<asp:DropDownList ID="listStrategy" runat="server" DataSource="<%# GetStrategies() %>"
        DataTextField="Name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="listStrategy_SelectedIndexChanged">
    </asp:DropDownList>
    <br />
    <asp:Label ID="labelException" class="errortext" runat="server" Text=""></asp:Label>
    <asp:ValidationSummary ID="validationSummary1" runat="server" ValidationGroup="Editing" />
    <asp:ValidationSummary ID="validationSummary2" runat="server" ValidationGroup="Adding" />
    <br />
    <asp:GridView ID="gridModel" runat="server" AlternatingRowStyle-CssClass="odd" CssClass="listing"
        RowStyle-CssClass="even" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="GUID"
        DataSourceID="sourceModel" ShowFooter="True" OnRowCommand="gridModel_RowCommand"
        OnRowDataBound="gridModel_RowDataBound" Width="100%" Caption="Strategy Name">
        <RowStyle CssClass="even"></RowStyle>
        <Columns>
            <asp:TemplateField HeaderText="Investment Name" SortExpression="InvestmentName">
                <EditItemTemplate>
                    <asp:TextBox ID="textInvestmentNameEdit" runat="server" Text='<%# Bind("InvestmentName") %>'
                        ValidationGroup="Editing"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validRequiredInvestmentNameEdit" runat="server" ErrorMessage="Please enter an Investment Name."
                        Display="None" ControlToValidate="textInvestmentNameEdit" ValidationGroup="Editing"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textInvestmentNameAdd" runat="server" ValidationGroup="Adding"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validRequiredInvestmentNameAdd" runat="server" ErrorMessage="Please enter an Investment Name."
                        Display="None" ControlToValidate="textInvestmentNameAdd" ValidationGroup="Adding"></asp:RequiredFieldValidator>
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
            <asp:TemplateField HeaderText="HNW" SortExpression="WeightingHNW" ItemStyle-Width="4em">
                <EditItemTemplate>
                    <asp:TextBox ID="textWeightingHNWEdit" runat="server" Text='<%# Bind("WeightingHNW") %>'
                        Width="4em" ValidationGroup="Editing"></asp:TextBox>
                    <asp:RangeValidator ID="validRangeWeightingHNWEdit" runat="server" ErrorMessage="Please enter a HNW Weighting value of between 0 and 1."
                        Type="Double" MaximumValue="1" MinimumValue="0" ControlToValidate="textWeightingHNWEdit"
                        ValidationGroup="Editing" Display="None"></asp:RangeValidator>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:RangeValidator ID="validRangeWeightingHNWAdd" runat="server" ErrorMessage="Please enter a HNW Weighting value of between 0 and 1."
                        Display="None" MaximumValue="1" MinimumValue="0" Type="Double" ValidationGroup="Adding"
                        ControlToValidate="textWeightingHNWAdd"></asp:RangeValidator>
                    <asp:TextBox ID="textWeightingHNWAdd" runat="server" Width="4em" ValidationGroup="Adding"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("WeightingHNW", "{0:0.00%}") %>'
                        Width="4em"></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="right" />
                <ItemStyle CssClass="right" Width="4em" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Affluent" SortExpression="WeightingAffluent" ItemStyle-Width="4em">
                <EditItemTemplate>
                    <asp:TextBox ID="textWeightingAffluentEdit" runat="server" Text='<%# Bind("WeightingAffluent") %>'
                        Width="4em" ValidationGroup="Editing"></asp:TextBox>
                    <asp:RangeValidator ID="validRangeWeightingAffluentEdit" runat="server" ErrorMessage="Please enter an Affluent Weighting of between 0 and 1."
                        ValidationGroup="Editing" Display="None" ControlToValidate="textWeightingAffluentEdit"
                        Type="Double" MaximumValue="1" MinimumValue="0"></asp:RangeValidator>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textWeightingAffluentAdd" runat="server" Width="4em" ValidationGroup="Adding"></asp:TextBox>
                    <asp:RangeValidator ID="validRangeWeightingAffluentAdd" runat="server" ErrorMessage="Please enter an Affluent Weighting of between 0 and 1."
                        ValidationGroup="Adding" MaximumValue="1" MinimumValue="0" Type="Double" ControlToValidate="textWeightingAffluentAdd"
                        Display="None" Font-Bold="False"></asp:RangeValidator>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("WeightingAffluent", "{0:0.00%}") %>'
                        Width="4em"></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="right" Width="4em" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Expected Yield" SortExpression="ExpectedYield" ItemStyle-Width="4em">
                <EditItemTemplate>
                    <asp:TextBox ID="textExpectedYieldEdit" runat="server" Text='<%# Bind("ExpectedYield") %>'
                        Width="4em" ValidationGroup="Editing"></asp:TextBox>
                    <asp:RangeValidator ID="validRangeExpectedYield" runat="server" ErrorMessage="Please enter an Expected Yield of between 0 and 0.2" ValidationGroup="Editing" ControlToValidate="textExpectedYieldEdit" Type="Double" MinimumValue="0" MaximumValue="0.2" Display="None"></asp:RangeValidator>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textExpectedYieldAdd" runat="server" Width="4em" ValidationGroup="Adding"></asp:TextBox>
                    <asp:RangeValidator ID="validRangeExpectedYieldAdd" runat="server" ErrorMessage="Please enter an Expected Yield of between 0 and 0.2" Display="None" ControlToValidate="textExpectedYieldAdd" ValidationGroup="Adding" MaximumValue="0.2" MinimumValue="0" Type="Double"></asp:RangeValidator>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("ExpectedYield", "{0:0.00%}") %>'
                        Width="4em"></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="right" />
                <ItemStyle CssClass="right" Width="4em" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Purchase Charge" SortExpression="PurchaseCharge" ItemStyle-Width="4em">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("PurchaseCharge") %>' Width="4em"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textPurchaseChargeAdd" runat="server" Width="4em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("PurchaseCharge", "{0:C}") %>'
                        Width="4em"></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="right" />
                <ItemStyle CssClass="right" Width="4em" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" ItemStyle-Width="4em">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                        Text="Update" ValidationGroup="Editing"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:LinkButton ID="linkAdd" runat="server" CommandName="Insert" ValidationGroup="Adding">Add</asp:LinkButton>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                        Text="Edit"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                        Text="Delete"></asp:LinkButton>
                </ItemTemplate>
                <FooterStyle CssClass="left" />
                <ItemStyle CssClass="lnowrap" Width="4em" Wrap="False" />
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle CssClass="odd"></AlternatingRowStyle>
    </asp:GridView>
    <br />
    <table class="listing" style="width: 40%">
        <caption>
            Weighting Totals</caption>
        <tr>
            <td class="lnowrap" width="20%">
                Total Weighting:&nbsp;
            </td>
            <td class="right">
                <asp:Label ID="labelTotalHNW" runat="server" Text="Total HNW"></asp:Label>
            </td>
            <td class="right">
                <asp:Label ID="labelTotalAFF" runat="server" Text="Total AFF"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="lnowrap" width="20%">
                Total Fixed Interest Weighting:&nbsp;
            </td>
            <td class="right">
                <asp:Label ID="labelFIIN_HNW" runat="server" Text="FIIN HNW"></asp:Label>
            </td>
            <td class="right">
                <asp:Label ID="labelFIIN_AFF" runat="server" Text="FIIN AFF"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="lnowrap" width="20%">
                Total Long Equity Weighting:
            </td>
            <td class="right">
                <asp:Label ID="labelLOEQ_HNW" runat="server" Text=" LOEQ HNW"></asp:Label>
            </td>
            <td class="right">
                <asp:Label ID="labelLOEQ_AFF" runat="server" Text="LOEQ AFF"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <asp:LinqDataSource ID="sourceModel" runat="server" ContextTypeName="RSMTenon.Data.RepGenDataContext"
        EnableDelete="True" EnableInsert="True" EnableUpdate="True" OrderBy="AssetClassID, InvestmentName"
        TableName="Models" Where="StrategyID == @StrategyID">
        <WhereParameters>
            <asp:ControlParameter ControlID="listStrategy" Name="StrategyID" PropertyName="SelectedValue"
                Type="String" />
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
            <asp:TemplateField HeaderText="Weighting HNW" SortExpression="WeightingHNW">
                <EditItemTemplate>
                    <asp:TextBox ID="textFIINWeightingHNW" runat="server" Text='<%# Bind("WeightingHNW") %>' ValidationGroup="Editing"></asp:TextBox>
                    <asp:RangeValidator ID="validRangeFIINWeightingHNW" runat="server" ErrorMessage="Please enter a Fixed Interest HNW Weighting of between 0 and 1." Display="None" ControlToValidate="textFIINWeightingHNW" Type="Double" MaximumValue="1" MinimumValue="0" ValidationGroup="Editing"></asp:RangeValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" 
                        Text='<%# Eval("WeightingHNW", "{0:0.00%}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="right" />
                <ItemStyle CssClass="right" Width="20%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Weighting Affluent" 
                SortExpression="WeightingAffluent">
                <EditItemTemplate>
                    <asp:TextBox ID="textFIINWeightingAffluent" runat="server" 
                        Text='<%# Bind("WeightingAffluent") %>' ValidationGroup="Editing"></asp:TextBox>
                    <asp:RangeValidator ID="validRangeFIINWeightingAffluent" runat="server" ErrorMessage="Please enter a Fixed Interest Affluent Weighting of between 0 and 1." Display="None" ControlToValidate="textFIINWeightingAffluent" Type="Double" MaximumValue="1" MinimumValue="0" ValidationGroup="Editing"></asp:RangeValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" 
                        Text='<%# Eval("WeightingAffluent", "{0:0.00%}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="right" />
                <ItemStyle CssClass="right" Width="20%" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                        Text="Update" ValidationGroup="Editing"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="linkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                        Text="Edit"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle CssClass="lnowrap" />
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle CssClass="even" />
    </asp:GridView>
    <asp:ObjectDataSource ID="sourceFixedInterest" runat="server" DataObjectTypeName="RSMTenon.Data.ModelBreakdown"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetModelBreakdown"
        TypeName="RSMTenon.Data.ModelBreakdown" UpdateMethod="UpdateModelBreakdown" ConflictDetection="CompareAllValues">
        <SelectParameters>
            <asp:ControlParameter ControlID="listStrategy" DefaultValue="" Name="strategyId"
                PropertyName="SelectedValue" Type="String" />
            <asp:Parameter DefaultValue="FIIN" Name="assetGroupId" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <asp:GridView ID="gridLongEquity" runat="server" AutoGenerateColumns="False" Caption="Long Equity"
        CssClass="listing" DataSourceID="sourceLongEquity" DataKeyNames="GUID" ShowFooter="True"
        OnRowDataBound="gridLongEquity_RowDataBound">
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
            <asp:TemplateField HeaderText="Weighting HNW" SortExpression="WeightingHNW">
                <EditItemTemplate>
                    <asp:TextBox ID="textLOEQWeightingHNW" runat="server" Text='<%# Bind("WeightingHNW") %>' ValidationGroup="Editing"></asp:TextBox>
                    <asp:RangeValidator ID="validRangeLOEQWeightingHNW" runat="server" ErrorMessage="Please enter a Long Equity HNW Weighting value of between 0 and 1." Type="Double" MaximumValue="1" MinimumValue="0" ValidationGroup="Editing" ControlToValidate="textLOEQWeightingHNW" Display="None"></asp:RangeValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" 
                        Text='<%# Eval("WeightingHNW", "{0:0.00%}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="right" />
                <ItemStyle CssClass="right" Width="20%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Weighting Affluent" 
                SortExpression="WeightingAffluent">
                <EditItemTemplate>
                    <asp:TextBox ID="textLOEQWeightingAffluent" runat="server" 
                        Text='<%# Bind("WeightingAffluent") %>' ValidationGroup="Editing"></asp:TextBox>
                    <asp:RangeValidator ID="validRangeLOEQWeightingAffluent" runat="server" ErrorMessage="Please enter a Long Equity Affluent Weighting value of between 0 and 1." Type="Double" MaximumValue="1" MinimumValue="0" ValidationGroup="Editing" Display="None" ControlToValidate="textLOEQWeightingAffluent"></asp:RangeValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" 
                        Text='<%# Eval("WeightingAffluent", "{0:0.00%}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="right" />
                <ItemStyle CssClass="right" Width="20%" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                        Text="Update" ValidationGroup="Editing"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="linkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                        Text="Edit"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle CssClass="lnowrap" />
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle CssClass="even" />
    </asp:GridView>
    <br />
    <br />
    <asp:ObjectDataSource ID="sourceLongEquity" runat="server" DataObjectTypeName="RSMTenon.Data.ModelBreakdown"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetModelBreakdown"
        TypeName="RSMTenon.Data.ModelBreakdown" UpdateMethod="UpdateModelBreakdown" ConflictDetection="CompareAllValues">
        <SelectParameters>
            <asp:ControlParameter ControlID="listStrategy" DefaultValue="" Name="strategyId"
                PropertyName="SelectedValue" Type="String" />
            <asp:Parameter DefaultValue="LOEQ" Name="assetGroupId" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
