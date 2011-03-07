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
            <asp:TemplateField HeaderText="Fund Name">
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
            <asp:TemplateField HeaderText="Amount £">
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
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Amount", "{0:#,##0}") %>'></asp:Label>
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
            <span style="width: 70%;">Fund Name</span> <span style="width: 30%; float: right;">Amount
                £</span> </td> </tr>
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
                    <asp:RequiredFieldValidator ID="validRequiredAssetName" runat="server" ControlToValidate="textAssetNameAddAllNew"
                        Display="None" ErrorMessage="Please enter an Investment Name." ValidationGroup="Investment"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="validRequiredAmount" runat="server" ControlToValidate="textAmountAddAllNew"
                        Display="None" ErrorMessage="Please enter an Investment Amount." ValidationGroup="Investment"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="validCompareAmountAdd" runat="server" ControlToValidate="textAmountAddAllNew"
                        Display="None" ErrorMessage="Please enter a valid Investment Amount." Operator="GreaterThan"
                        Type="Currency" ValidationGroup="Investment" ValueToCompare="0"></asp:CompareValidator>
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
    <asp:ValidationSummary ID="validationSummary3" runat="server" ValidationGroup="AssetClassEdit" />
    <br />
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
        DataKeyNames="GUID" OnItemUpdated="detailsView_ItemUpdated" 
        onmodechanged="detailsView_ModeChanged">
        <Fields>
            <asp:TemplateField HeaderText="Cash" SortExpression="CASH">
                <EditItemTemplate>
                    <asp:TextBox ID="textCASH" runat="server" Text='<%# Bind("CASH") %>' ValidationGroup="AssetClassEdit"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexCASH" runat="server" ErrorMessage="Please enter a percentage Cash value."
                        Display="None" ControlToValidate="textCASH" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="AssetClassEdit"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CASH", "{0:0.0}") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("CASH", "{0:0.0}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="left" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Commodities" SortExpression="COMM">
                <EditItemTemplate>
                    <asp:TextBox ID="textCOMM" runat="server" Text='<%# Bind("COMM") %>'></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexCOMM" runat="server" ErrorMessage="Please enter a percentage Commodities value."
                        Display="None" ControlToValidate="textCOMM" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="AssetClassEdit"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("COMM", "{0:0.0}") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("COMM", "{0:0.0}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="left" />
                <ItemStyle CssClass="right" Width="5em" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Real Estate" SortExpression="COPR">
                <EditItemTemplate>
                    <asp:TextBox ID="textCOPR" runat="server" Text='<%# Bind("COPR") %>'></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexCOPR" runat="server" ErrorMessage="Please enter a percentage Real Estate value."
                        Display="None" ControlToValidate="textCOPR" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="AssetClassEdit"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("COPR", "{0:0.0}") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("COPR", "{0:0.0}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="left" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Global Equities" SortExpression="GLEQ">
                <EditItemTemplate>
                    <asp:TextBox ID="textGLEQ" runat="server" Text='<%# Bind("GLEQ") %>'></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexGLEQ" runat="server" ErrorMessage="Please enter a percentage Global Equities value."
                        Display="None" ControlToValidate="textGLEQ" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="AssetClassEdit"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("GLEQ", "{0:0.0}") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("GLEQ", "{0:0.0}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Hedge" SortExpression="HEDG">
                <EditItemTemplate>
                    <asp:TextBox ID="textHEDG" runat="server" Text='<%# Bind("HEDG") %>'></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexHEDG" runat="server" ErrorMessage="Please enter a percentage Hedge value."
                        Display="None" ControlToValidate="textHEDG" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="AssetClassEdit"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("HEDG", "{0:0.0}") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("HEDG", "{0:0.0}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="left" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Long Short Equities" SortExpression="LOSH">
                <EditItemTemplate>
                    <asp:TextBox ID="textLOSH" runat="server" Text='<%# Bind("LOSH") %>'></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexLOSH" runat="server" ErrorMessage="Please enter a percentage Long Short value."
                        Display="None" ControlToValidate="textLOSH" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="AssetClassEdit"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("LOSH", "{0:0.0}") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("LOSH", "{0:0.0}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Private Equity" SortExpression="PREQ">
                <EditItemTemplate>
                    <asp:TextBox ID="textPREQ" runat="server" Text='<%# Bind("PREQ") %>'></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexPREQ" runat="server" ErrorMessage="Please enter a percentage Private Equity value."
                        Display="None" ControlToValidate="textPREQ" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="AssetClassEdit"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("PREQ", "{0:0.0}") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("PREQ", "{0:0.0}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="left" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UK Corporate Bonds" SortExpression="UKCB">
                <EditItemTemplate>
                    <asp:TextBox ID="textUKCB" runat="server" Text='<%# Bind("UKCB") %>'></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexUKCB" runat="server" ErrorMessage="Please enter a percentage UK Corporate Bonds value."
                        Display="None" ControlToValidate="textUKCB" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="AssetClassEdit"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("UKCB", "{0:0.0}") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("UKCB", "{0:0.0}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UK Equities" SortExpression="UKEQ">
                <EditItemTemplate>
                    <asp:TextBox ID="textUKEQ" runat="server" Text='<%# Bind("UKEQ") %>'></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexUKEQ" runat="server" ErrorMessage="Please enter a percentage UK Equities value."
                        Display="None" ControlToValidate="textUKEQ" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="AssetClassEdit"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("UKEQ", "{0:0.0}") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("UKEQ", "{0:0.0}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UK Government Bonds" SortExpression="UKGB">
                <EditItemTemplate>
                    <asp:TextBox ID="textUKGB" runat="server" Text='<%# Bind("UKGB") %>'></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexUKGB" runat="server" ErrorMessage="Please enter a percentage UK Government Bonds value."
                        Display="None" ControlToValidate="textUKGB" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="AssetClassEdit"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("UKGB", "{0:0.0}") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("UKGB", "{0:0.0}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UK High Yield Bonds" SortExpression="UKHY">
                <EditItemTemplate>
                    <asp:TextBox ID="textUKHY" runat="server" Text='<%# Bind("UKHY") %>'></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexUKHY" runat="server" ErrorMessage="Please enter a percentage UK High Yield Bonds value."
                        Display="None" ControlToValidate="textUKHY" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="AssetClassEdit"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("UKHY", "{0:0.0}") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("UKHY", "{0:0.0}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="World Bonds" SortExpression="WOBO">
                <EditItemTemplate>
                    <asp:TextBox ID="textWOBO" runat="server" Text='<%# Bind("WOBO") %>'></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexWOBO" runat="server" ErrorMessage="Please enter a percentage World Bonds value."
                        Display="None" ControlToValidate="textWOBO" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="AssetClassEdit"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("WOBO", "{0:0.0}") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label12" runat="server" Text='<%# Bind("WOBO", "{0:0.0}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True" ValidationGroup="AssetClassEdit">
                <HeaderStyle CssClass="right" />
                <ItemStyle CssClass="right" />
            </asp:CommandField>
        </Fields>
        <AlternatingRowStyle CssClass="even" />
    </asp:DetailsView>
    <br />
    <asp:Label ID="labelInstruction" runat="server" Text="Enter values as percentages to one decimal place e.g., 10.5 for 10.5%<br />" Visible="false"></asp:Label>
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
