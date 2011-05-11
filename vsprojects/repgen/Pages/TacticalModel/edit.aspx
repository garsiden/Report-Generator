<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Pages_TacticalModel_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h4>
        Edit Tactical Models</h4>
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
        Width="100%" Caption="Strategy Name">
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
                    <asp:TextBox ID="textInvestmentNameAdd" runat="server" ValidationGroup="Adding" Width="100%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validRequiredInvestmentNameAdd" runat="server" ErrorMessage="Please enter an Investment Name."
                        Display="None" ControlToValidate="textInvestmentNameAdd" ValidationGroup="Adding"></asp:RequiredFieldValidator>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("InvestmentName") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="left" />
                <ItemStyle CssClass="left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Asset Group" SortExpression="AssetGroupID">
                <EditItemTemplate>
                    <asp:DropDownList ID="listAssetGroup" runat="server" DataSource="<%# GetModelAssetGroups() %>"
                        DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("AssetGroupID") %>'>
                    </asp:DropDownList>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="listAssetGroupAdd" runat="server" DataSource="<%# GetModelAssetGroups() %>"
                        DataTextField="Name" DataValueField="ID">
                    </asp:DropDownList>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("AssetGroup.Name") %>'></asp:Label>
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
                    <asp:RangeValidator ID="validRangeExpectedYield" runat="server" ErrorMessage="Please enter an Expected Yield of between 0 and 0.2"
                        ValidationGroup="Editing" ControlToValidate="textExpectedYieldEdit" Type="Double"
                        MinimumValue="0" MaximumValue="0.2" Display="None"></asp:RangeValidator>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textExpectedYieldAdd" runat="server" Width="4em" ValidationGroup="Adding"></asp:TextBox>
                    <asp:RangeValidator ID="validRangeExpectedYieldAdd" runat="server" ErrorMessage="Please enter an Expected Yield of between 0 and 0.2"
                        Display="None" ControlToValidate="textExpectedYieldAdd" ValidationGroup="Adding"
                        MaximumValue="0.2" MinimumValue="0" Type="Double"></asp:RangeValidator>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("ExpectedYield", "{0:0.00%}") %>'
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
    <asp:LinqDataSource ID="sourceModel" runat="server" ContextTypeName="RSMTenon.Data.RepGenDataContext"
        EnableDelete="True" EnableInsert="True" EnableUpdate="True" OrderBy="AssetGroupID, InvestmentName"
        TableName="TacticalModels" Where="StrategyID == @StrategyID">
        <WhereParameters>
            <asp:ControlParameter ControlID="listStrategy" Name="StrategyID" PropertyName="SelectedValue"
                Type="String" />
        </WhereParameters>
    </asp:LinqDataSource>
</asp:Content>
