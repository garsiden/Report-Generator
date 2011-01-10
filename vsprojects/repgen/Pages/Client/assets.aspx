<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="assets.aspx.cs" Inherits="Pages_Client_assets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:GridView ID="gridAsset" runat="server" AutoGenerateColumns="False" CssClass="listing"
        DataKeyNames="GUID" DataSourceID="sourceAssetsObject" AllowSorting="True" 
        onrowcommand="gridAsset_RowCommand" ShowFooter="True">
        <RowStyle CssClass="odd" />
        <Columns>
            <asp:TemplateField HeaderText="Asset Name" SortExpression="AssetName">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("AssetName") %>' Width="100%"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textAssetNameAdd" runat="server" Width="100%"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("AssetName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="lnowrap" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Amount" SortExpression="Amount">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Amount", "{0:0}") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textAmountAdd" runat="server"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Amount", "{0:c}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="right" Width="20%" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <FooterTemplate>
                    <asp:LinkButton ID="linkAdd" runat="server" CommandName="Insert">Add</asp:LinkButton>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                        CommandName="Select" Text="Select"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle CssClass="left" Width="5%" />
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True">
                <ItemStyle CssClass="lnowrap" Width="5%" />
            </asp:CommandField>
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
        <AlternatingRowStyle CssClass="even" />
    </asp:GridView>
    <asp:LinqDataSource ID="sourceAssets" runat="server" AutoGenerateWhereClause="True"
        ContextTypeName="RSMTenon.Data.RepGenDataContext" EnableDelete="True" EnableInsert="True"
        EnableUpdate="True" TableName="ClientAssets">
        <WhereParameters>
            <asp:QueryStringParameter DbType="Guid" DefaultValue="979DE312-8E99-49D3-9D41-54ECAE0CAD5C"
                Name="ClientGUID" QueryStringField="guid" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:ObjectDataSource ID="sourceAssetsObject" runat="server" 
        ConflictDetection="CompareAllValues" 
        DataObjectTypeName="RSMTenon.Data.ClientAsset" DeleteMethod="DeleteClientAsset" 
        InsertMethod="InsertClient" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetAllClientsAssets" TypeName="RSMTenon.Data.ClientAsset" 
        UpdateMethod="UpdateClientAsset">
        <UpdateParameters>
            <asp:Parameter Name="clientAsset" Type="Object" />
            <asp:Parameter Name="original_clientAsset" Type="Object" />
        </UpdateParameters>
        <SelectParameters>
            <asp:QueryStringParameter DbType="Guid" 
                DefaultValue="979DE312-8E99-49D3-9D41-54ECAE0CAD5C" Name="clientGuid" 
                QueryStringField="guid" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <br />
    <asp:DetailsView ID="detailsView" runat="server" AutoGenerateRows="False" Caption="Investment Name"
        CssClass="listing" DataSourceID="sourceDetails" Height="50px" 
        Width="302px" 
        ondatabound="detailsView_DataBound" DataKeyNames="GUID" >
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
                <HeaderStyle CssClass="left" />
                <ItemStyle CssClass="left" />
            </asp:CommandField>
        </Fields>
        <AlternatingRowStyle CssClass="even" />
    </asp:DetailsView>
    <br />

    <asp:Label ID="labelAssetAllocation" runat="server"></asp:Label>
    <br />
    <asp:ObjectDataSource ID="sourceDetails" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetClientAsset" TypeName="RSMTenon.Data.ClientAsset" 
        ConflictDetection="CompareAllValues" 
        DataObjectTypeName="RSMTenon.Data.ClientAsset" 
         UpdateMethod="UpdateClientAsset">
        <UpdateParameters>
            <asp:Parameter Name="clientAsset" Type="Object" />
            <asp:Parameter Name="original_clientAsset" Type="Object" />
        </UpdateParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="gridAsset" DbType="Guid" Name="guid" PropertyName="SelectedDataKey.Values[&quot;GUID&quot;]" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
</asp:Content>
