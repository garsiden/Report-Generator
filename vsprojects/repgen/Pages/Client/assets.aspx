<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="assets.aspx.cs" Inherits="Pages_Client_assets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h4 id="clientAssetHeader" runat="server">
    </h4>
    <br />
    <asp:GridView ID="gridAsset" runat="server" AutoGenerateColumns="False" CssClass="listing"
        DataKeyNames="GUID" DataSourceID="sourceAssetsObject" AllowSorting="True" OnRowCommand="gridAsset_RowCommand"
        ShowFooter="True" OnRowDataBound="gridAsset_RowDataBound" OnRowUpdated="gridAsset_RowUpdated">
        <RowStyle CssClass="odd" />
        <Columns>
            <asp:TemplateField HeaderText="Asset Name">
                <EditItemTemplate>
                    <asp:TextBox ID="textBoxAssetName" runat="server" Text='<%# Bind("AssetName") %>'
                        Width="100%" CausesValidation="True" ValidationGroup="InvestmentEdit"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validRequiredAssetNameEdit" runat="server" ErrorMessage="Please enter an Investment Name"
                        ControlToValidate="textBoxAssetName" Display="None" ValidationGroup="InvestmentEdit"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textAssetNameAdd" runat="server" Width="100%" ValidationGroup="Investment"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validRequiredAssetName" runat="server" ErrorMessage="Please enter an Investment Name."
                        ControlToValidate="textAssetNameAdd" Display="None" ValidationGroup="Investment"></asp:RequiredFieldValidator>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("AssetName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="lnowrap" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Amount">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Amount", "{0:0}") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textAmountAdd" runat="server" ValidationGroup="Investment"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validRequiredAmount" runat="server" ErrorMessage="Please enter an Investment Amount."
                        ControlToValidate="textAmountAdd" Display="None" ValidationGroup="Investment"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="validCompareAmountAdd" runat="server" ErrorMessage="Please enter a valid Investment Amount."
                        ControlToValidate="textAmountAdd" Display="None" ValidationGroup="Investment"
                        ValueToCompare="0" Operator="GreaterThan" Type="Currency"></asp:CompareValidator>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Amount", "{0:C0}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="right" Width="20%" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <FooterTemplate>
                    <asp:LinkButton ID="linkAdd" runat="server" CommandName="Insert" ValidationGroup="Investment">Add</asp:LinkButton>
                </FooterTemplate>
                <FooterStyle CssClass="left" />
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select"
                        Text="Select"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle CssClass="left" Width="5%" />
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True" ValidationGroup="InvestmentEdit">
                <ItemStyle CssClass="lnowrap" Width="5%" />
            </asp:CommandField>
            <asp:CommandField ShowDeleteButton="True">
                <ItemStyle CssClass="left" Width="5%" />
            </asp:CommandField>
        </Columns>
        <EmptyDataTemplate>
            <span style="width: 70%;">Asset Name</span> <span style="width: 30%; float: right;">
                Amount</span> </td> </tr>
            <tr>
                <td class="lnowrap" width="70%" colspan="3">
                    <asp:TextBox ID="textAssetNameAddAllNew" runat="server" Width="100%" ValidationGroup="Investment"></asp:TextBox>
                </td>
                <td class="right" width="20%">
                    <asp:TextBox ID="textAmountAddAllNew" runat="server" ValidationGroup="Investment"></asp:TextBox>
                </td>
                <td class="right">
                    <asp:LinkButton ID="linkAdd" runat="server" CommandName="Insert" CommandArgument="AllNew"
                        ValidationGroup="Investment">Add</asp:LinkButton>
                </td>
                <asp:RequiredFieldValidator ID="validRequiredAssetName" runat="server" ControlToValidate="textAssetNameAddAllNew"
                    Display="None" ErrorMessage="Please enter an Investment Name." ValidationGroup="Investment"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="validRequiredAmount" runat="server" ControlToValidate="textAmountAddAllNew"
                    Display="None" ErrorMessage="Please enter an Investment Amount." ValidationGroup="Investment"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="validCompareAmountAdd" runat="server" ControlToValidate="textAmountAddAllNew"
                    Display="None" ErrorMessage="Please enter a valid Investment Amount." Operator="GreaterThan"
                    Type="Currency" ValidationGroup="Investment" ValueToCompare="0"></asp:CompareValidator>
            </tr>
        </EmptyDataTemplate>
        <AlternatingRowStyle CssClass="even" />
    </asp:GridView>
    <br />
    <div style="text-align: right">
        <asp:HyperLink ID="hyperClient" runat="server" NavigateUrl="~/Pages/Client/edit.aspx">Back to Client</asp:HyperLink>
    </div>
    <table id="tableInvestmentSummary" runat="server" class="listing" style="width: 40%">
        <tr>
            <td class="lnowrap">
                Total Assets:&nbsp;
            </td>
            <td class="right">
                <asp:Label ID="labelTotalAssets" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="lnowrap">
                Total Investment:&nbsp;
            </td>
            <td class="right">
                <asp:Label ID="labelTotalInvestment" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Label ID="labelException" runat="server"></asp:Label>
    <br />
    <asp:ValidationSummary ID="validationSummary1" runat="server" ValidationGroup="Investment" />
    <asp:ValidationSummary ID="validationSummary2" runat="server" ValidationGroup="InvestmentEdit" />
    <br />
    <asp:LinqDataSource ID="sourceAssets" runat="server" ContextTypeName="RSMTenon.Data.RepGenDataContext"
        EnableDelete="True" EnableInsert="True" EnableUpdate="True" TableName="ClientAssets"
        Where="ClientGUID == @ClientGUID" OrderBy="AssetName">
        <WhereParameters>
            <asp:QueryStringParameter DbType="Guid" Name="ClientGUID" QueryStringField="guid" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:ObjectDataSource ID="sourceAssetsObject" runat="server" ConflictDetection="CompareAllValues"
        DataObjectTypeName="RSMTenon.Data.ClientAsset" DeleteMethod="DeleteClientAsset"
        InsertMethod="InsertClientAsset" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetAllClientsAssets" TypeName="RSMTenon.Data.ClientAsset" UpdateMethod="UpdateClientAsset">
        <UpdateParameters>
            <asp:Parameter Name="clientAsset" Type="Object" />
            <asp:Parameter Name="original_clientAsset" Type="Object" />
        </UpdateParameters>
        <SelectParameters>
            <asp:QueryStringParameter DbType="Guid" DefaultValue="" Name="clientGuid" QueryStringField="guid" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:DetailsView ID="detailsView" runat="server" AutoGenerateRows="False" CssClass="listing"
        DataSourceID="sourceDetails" Height="50px" Width="302px" OnDataBound="detailsView_DataBound"
        DataKeyNames="GUID" OnItemUpdated="detailsView_ItemUpdated">
        <Fields>
            <asp:BoundField DataField="CASH" HeaderText="Cash" SortExpression="CASH" HeaderStyle-CssClass="left"
                DataFormatString="{0:0.0%}">
                <HeaderStyle CssClass="left"></HeaderStyle>
                <ItemStyle CssClass="right" />
            </asp:BoundField>
            <asp:BoundField DataField="COMM" HeaderText="Commodities" SortExpression="COMM" DataFormatString="{0:0.0%}">
                <HeaderStyle CssClass="left" />
                <ItemStyle CssClass="right" Width="5em" />
            </asp:BoundField>
            <asp:BoundField DataField="COPR" HeaderText="Commercial Property" SortExpression="COPR"
                DataFormatString="{0:0.0%}">
                <HeaderStyle CssClass="left" />
                <ItemStyle CssClass="right" />
            </asp:BoundField>
            <asp:BoundField DataField="GLEQ" HeaderText="Global Equities" SortExpression="GLEQ"
                DataFormatString="{0:0.0%}">
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:BoundField>
            <asp:BoundField DataField="HEDG" HeaderText="Hedge" SortExpression="HEDG" DataFormatString="{0:0.0%}">
                <HeaderStyle CssClass="left" />
                <ItemStyle CssClass="right" />
            </asp:BoundField>
            <asp:BoundField DataField="LOSH" HeaderText="Long Short Equities" SortExpression="LOSH"
                DataFormatString="{0:0.0%}">
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:BoundField>
            <asp:BoundField DataField="PREQ" HeaderText="Private Equity" SortExpression="PREQ"
                DataFormatString="{0:0.0%}">
                <HeaderStyle CssClass="left" />
                <ItemStyle CssClass="right" />
            </asp:BoundField>
            <asp:BoundField DataField="UKCB" HeaderText="UK Corporate Bonds" SortExpression="UKCB"
                DataFormatString="{0:0.0%}">
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:BoundField>
            <asp:BoundField DataField="UKEQ" HeaderText="UK Equities" SortExpression="UKEQ" DataFormatString="{0:0.0%}">
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:BoundField>
            <asp:BoundField DataField="UKGB" HeaderText="UK Government Bonds" SortExpression="UKGB"
                DataFormatString="{0:0.0%}">
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:BoundField>
            <asp:BoundField DataField="UKHY" HeaderText="UK High Yield Bonds" SortExpression="UKHY"
                DataFormatString="{0:0.0%}">
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:BoundField>
            <asp:BoundField DataField="WOBO" HeaderText="World Bonds" SortExpression="WOBO" DataFormatString="{0:0.0%}">
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:BoundField>
            <asp:CommandField ShowEditButton="True">
                <HeaderStyle CssClass="right" />
                <ItemStyle CssClass="right" />
            </asp:CommandField>
        </Fields>
        <AlternatingRowStyle CssClass="even" />
    </asp:DetailsView>
    <br />
    <asp:Label ID="labelExceptionDetails" runat="server"></asp:Label>
    <asp:ObjectDataSource ID="sourceDetails" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetClientAsset" TypeName="RSMTenon.Data.ClientAsset" ConflictDetection="CompareAllValues"
        DataObjectTypeName="RSMTenon.Data.ClientAsset" UpdateMethod="UpdateClientAsset"
        DeleteMethod="DeleteClientAsset" InsertMethod="InsertClientAsset" OnSelecting="sourceDetails_Selecting"
        OnUpdating="sourceDetails_Updating">
        <UpdateParameters>
            <asp:Parameter Name="clientAsset" Type="Object" />
            <asp:Parameter Name="original_clientAsset" Type="Object" />
        </UpdateParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="gridAsset" Name="guid" PropertyName="SelectedDataKey.Values[&quot;GUID&quot;]"
                DbType="Guid" DefaultValue="00000000-0000-0000-0000-000000000000" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <!-- DefaultValue="00000000-0000-0000-0000-000000000000" -->
</asp:Content>
