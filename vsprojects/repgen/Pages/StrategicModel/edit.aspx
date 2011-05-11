<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Pages_StrategicModel_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h4>
        Edit Strategic Models</h4>
    <br />
    Strategy:&nbsp;<asp:DropDownList ID="listStrategy" runat="server" DataSource="<%# GetStrategies() %>"
        DataTextField="Name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="listStrategy_SelectedIndexChanged">
    </asp:DropDownList>
    <br />
    <br />
    <asp:GridView ID="gridModel" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        CssClass="listing" DataSourceID="linqSource" Width="60%" ShowFooter="True" OnRowCommand="gridModel_RowCommand"
        OnRowUpdated="gridModel_RowUpdated" DataKeyNames="GUID" OnDataBound="gridModel_DataBound"
        OnRowDataBound="gridModel_RowDataBound">
        <RowStyle CssClass="odd" />
        <Columns>
            <asp:TemplateField HeaderText="Asset Class" SortExpression="AssetClassID">
                <EditItemTemplate>
                    <asp:DropDownList ID="listAssetClass" runat="server" DataSource="<%# GetModelAssetClasses() %>"
                        DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("AssetClassID") %>'
                        Width="90%">
                    </asp:DropDownList>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="listAssetClassAdd" runat="server" DataSource="<%# GetNewModelAssetClasses() %>"
                        DataTextField="Name" DataValueField="ID" Width="90%">
                    </asp:DropDownList>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("AssetClass.Name") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="lnowrap" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Weighting" SortExpression="Weighting">
                <EditItemTemplate>
                    <asp:TextBox ID="textWeightingEdit" runat="server" Text='<%# Bind("Weighting") %>'
                        Width="4em" ValidationGroup="Editing"></asp:TextBox>
                    <asp:RangeValidator ID="validRangeWeightingEdit" runat="server" ErrorMessage="Please enter a Weighting value of between 0 and 1."
                        Type="Double" MaximumValue="1" MinimumValue="0" ControlToValidate="textWeightingEdit"
                        ValidationGroup="Editing" Display="None"></asp:RangeValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Weighting", "{0:0.00%}") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:RangeValidator ID="validRangeWeightingAdd" runat="server" ErrorMessage="Please enter a Weighting value of between 0 and 1."
                        Display="None" MaximumValue="1" MinimumValue="0" Type="Double" ValidationGroup="Adding"
                        ControlToValidate="textWeightingAdd"></asp:RangeValidator>
                    <asp:TextBox ID="textWeightingAdd" runat="server" Width="100%" ValidationGroup="Adding"></asp:TextBox>
                </FooterTemplate>
                <ItemStyle CssClass="right" Width="5em" />
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
        <AlternatingRowStyle CssClass="even" />
    </asp:GridView>
    <br />
    <asp:Label ID="labelException" class="errortext" runat="server" Text=""></asp:Label>
    <asp:ValidationSummary ID="validationSummary1" runat="server" ValidationGroup="Editing" />
    <asp:ValidationSummary ID="validationSummary2" runat="server" ValidationGroup="Adding" />
    <br />
    <asp:Label ID="labelTotalText" runat="server" Text="Total Weightings:&nbsp;" Font-Bold="True"></asp:Label>
    <asp:Label ID="labelTotalValue" runat="server" Text="" Style="text-align: right"></asp:Label>
    <br />
    <asp:LinqDataSource ID="linqSource" runat="server" ContextTypeName="RSMTenon.Data.RepGenDataContext"
        EnableDelete="True" EnableInsert="True" EnableUpdate="True" OrderBy="AssetClassID"
        TableName="StrategicModels" Where="StrategyID == @StrategyID" OnUpdated="linqSource_Updated">
        <WhereParameters>
            <asp:ControlParameter ControlID="listStrategy" Name="StrategyID" PropertyName="SelectedValue"
                Type="String" />
        </WhereParameters>
    </asp:LinqDataSource>
</asp:Content>
