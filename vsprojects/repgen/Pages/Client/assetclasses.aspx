<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="assetclasses.aspx.cs" Inherits="Pages_Client_assetclasses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:DetailsView ID="detailsView" runat="server" AutoGenerateRows="False" DataSourceID="sourceAssetObject"
        CaptionAlign="Top" CssClass="listing" DataKeyNames="ClientGUID" 
        Width="60%">
        <RowStyle CssClass="odd" />
        <EmptyDataTemplate>
            <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
                Text="New" />
        </EmptyDataTemplate>
        <Fields>
            <asp:BoundField DataField="CASH" HeaderText="Cash" SortExpression="CASH" DataFormatString="{0:0.0%}">
                <ItemStyle CssClass="right" />
                <HeaderStyle CssClass="left"></HeaderStyle>
            </asp:BoundField>
            <asp:BoundField DataField="COMM" HeaderText="Commodities" SortExpression="COMM" DataFormatString="{0:0.0%}">
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:BoundField>
            <asp:BoundField DataField="COPR" HeaderText="Commercial Property" SortExpression="COPR"
                DataFormatString="{0:0.0%}">
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:BoundField>
            <asp:BoundField DataField="GLEQ" HeaderText="Global Equity" SortExpression="GLEQ"
                DataFormatString="{0:0.0%}">
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:BoundField>
            <asp:BoundField DataField="HEDG" HeaderText="Hedge" SortExpression="HEDG" DataFormatString="{0:0.0%}">
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:BoundField>
            <asp:BoundField DataField="LOSH" HeaderText="Long Short" SortExpression="LOSH" DataFormatString="{0:0.0%}">
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:BoundField>
            <asp:BoundField DataField="PREQ" HeaderText="Private Equity" SortExpression="PREQ"
                DataFormatString="{0:0.0%}">
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:BoundField>
            <asp:BoundField DataField="UKCB" HeaderText="UK Corporate Bonds" SortExpression="UKCB"
                DataFormatString="{0:0.0%}">
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:BoundField>
            <asp:BoundField DataField="UKEQ" HeaderText="UK Equity" SortExpression="UKEQ" DataFormatString="{0:0.0%}">
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
            <asp:TemplateField>
                <EditItemTemplate>
                    <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update">Update</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton4" runat="server"  CommandName="Cancel">Cancel</asp:LinkButton>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:LinkButton ID="InsertButton" runat="server" CommandName="Insert">Insert</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Cancel">Cancel</asp:LinkButton>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                        Text="Edit" />&nbsp;
                    <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                        Text="Delete" />
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="lnowrap" />
            </asp:TemplateField>

        </Fields>
        <AlternatingRowStyle CssClass="even" />
    </asp:DetailsView>
    <asp:LinqDataSource ID="sourceAssets" runat="server" ContextTypeName="RSMTenon.Data.RepGenDataContext"
        EnableDelete="True" EnableInsert="True" EnableUpdate="True" TableName="ClientAssetClasses"
        Where="ClientGUID == @ClientGUID" OnInserting="sourceAssets_Inserting">
        <WhereParameters>
            <asp:QueryStringParameter DbType="Guid" DefaultValue="636C8103-E06D-4575-AAFC-574474C2D7F8"
                Name="ClientGUID" QueryStringField="guid" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:ObjectDataSource ID="sourceAssetObject" runat="server" DataObjectTypeName="RSMTenon.Data.ClientAssetClass"
        DeleteMethod="DeleteClientAsset" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetClientAssetClass" TypeName="RSMTenon.Data.ClientAssetClass"
        UpdateMethod="UpdateClientAsset" OnInserting="sourceAssetObject_Inserting" 
        OnObjectCreating="sourceAssetObject_ObjectCreating" 
        ConflictDetection="CompareAllValues" InsertMethod="InsertClientAsset">
        <UpdateParameters>
            <asp:Parameter Name="clientAsset" Type="Object" />
            <asp:Parameter Name="original_clientAsset" Type="Object" />
        </UpdateParameters>
        <SelectParameters>
            <asp:QueryStringParameter DbType="Guid" Name="clientGuid" QueryStringField="guid" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
