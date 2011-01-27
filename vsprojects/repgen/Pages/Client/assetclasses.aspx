<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="assetclasses.aspx.cs" Inherits="Pages_Client_assetclasses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h4 id="clientAssetHeader" runat="server">
    </h4>
    <br />
    <asp:DetailsView ID="detailsView" runat="server" AutoGenerateRows="False" DataSourceID="sourceAssets"
        CaptionAlign="Top" CssClass="listing" DataKeyNames="ClientGUID" Width="60%" OnItemInserted="detailsView_ItemInserted"
        OnItemUpdated="detailsView_ItemUpdated">
        <RowStyle CssClass="odd" HorizontalAlign="Left" />
        <EmptyDataTemplate>
            <%# GetAssetClassName("CASH") %>
            &nbsp; </td>
            <td>
                &nbsp;
            </td>
            <tr class="even">
                <td class="lnowrap">
                    <%# GetAssetClassName("COMM") %>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="lnowrap">
                    <%# GetAssetClassName("COPR") %>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr class="even">
                <td class="lnowrap">
                    <%# GetAssetClassName("GLEQ") %>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="lnowrap">
                    <%# GetAssetClassName("HEDG") %>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr class="even">
                <td class="lnowrap">
                    <%# GetAssetClassName("LOSH") %>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="lnowrap">
                    <%# GetAssetClassName("PREQ") %>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr class="even">
                <td class="lnowrap">
                    <%# GetAssetClassName("UKCB") %>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="lnowrap">
                    <%# GetAssetClassName("UKEQ") %>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr class="even">
                <td class="lnowrap">
                    <%# GetAssetClassName("UKGB") %>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="lnowrap">
                    <%# GetAssetClassName("UKHY") %>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr class="even">
                <td class="lnowrap">
                    <%# GetAssetClassName("WOBO") %>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td class="right">
                    <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
                        Text="New" />
                    &nbsp;
                </td>
            </tr>
        </EmptyDataTemplate>
        <Fields>
            <asp:TemplateField HeaderText="Cash" SortExpression="CASH">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CASH") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CASH", "{0:0.0%}") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("CASH", "{0:0.0%}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="left" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Commodities" SortExpression="COMM">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("COMM") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("COMM", "{0:0.0%}") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("COMM", "{0:0.0%}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Commercial Property" SortExpression="COPR">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("COPR") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("COPR", "{0:0.0%}") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("COPR", "{0:0.0%}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Global Equity" SortExpression="GLEQ">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("GLEQ") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("GLEQ", "{0:0.0%}") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("GLEQ", "{0:0.0%}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Hedge" SortExpression="HEDG">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("HEDG") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("HEDG", "{0:0.0%}") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("HEDG", "{0:0.0%}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Long Short" SortExpression="LOSH">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("LOSH") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("LOSH", "{0:0.0%}") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("LOSH", "{0:0.0%}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Private Equity" SortExpression="PREQ">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("PREQ") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("PREQ", "{0:0.0%}") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("PREQ", "{0:0.0%}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UK Corporate Bonds" SortExpression="UKCB">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("UKCB") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("UKCB", "{0:0.0%}") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("UKCB", "{0:0.0%}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UK Equity" SortExpression="UKEQ">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("UKEQ") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("UKEQ", "{0:0.0%}") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("UKEQ", "{0:0.0%}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UK Government Bonds" SortExpression="UKGB">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("UKGB") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("UKGB") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label10" runat="server" Text='<%# Eval("UKGB", "{0:0.0%}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UK High Yield Bonds" SortExpression="UKHY">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("UKHY") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("UKHY", "{0:0.0%}") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label11" runat="server" Text='<%# Eval("UKHY", "{0:0.0%}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="World Bonds" SortExpression="WOBO">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("WOBO") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("WOBO", "{0:0.0%}") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label12" runat="server" Text='<%# Eval("WOBO", "{0:0.0%}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField>
                <EditItemTemplate>
                    <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update">Update</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Cancel">Cancel</asp:LinkButton>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:LinkButton ID="InsertButton" runat="server" CommandName="Insert">Insert</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Cancel">Cancel</asp:LinkButton>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                        Text="Delete" />&nbsp;
                    <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                        Text="Edit" />
                </ItemTemplate>
                <HeaderStyle CssClass="right" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
        </Fields>
        <AlternatingRowStyle CssClass="even" />
    </asp:DetailsView>
    <br />
    <div style="width: 60%; text-align: right;">
        <asp:HyperLink ID="hyperClient" runat="server" NavigateUrl="edit.aspx">Back to Client</asp:HyperLink>
    </div>
    <asp:Label ID="labelException" runat="server"></asp:Label><br />
    <asp:LinqDataSource ID="sourceAssets" runat="server" ContextTypeName="RSMTenon.Data.RepGenDataContext"
        EnableDelete="True" EnableInsert="True" EnableUpdate="True" TableName="ClientAssetClasses"
        Where="ClientGUID == @ClientGUID" OnInserting="sourceAssets_Inserting">
        <WhereParameters>
            <asp:QueryStringParameter DbType="Guid" Name="ClientGUID" QueryStringField="guid" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:ObjectDataSource ID="sourceAssetObject" runat="server" DataObjectTypeName="RSMTenon.Data.ClientAssetClass"
        DeleteMethod="DeleteClientAsset" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetClientAssetClass" TypeName="RSMTenon.Data.ClientAssetClass"
        UpdateMethod="UpdateClientAsset" OnInserting="sourceAssetObject_Inserting" ConflictDetection="CompareAllValues"
        InsertMethod="InsertClientAsset">
        <UpdateParameters>
            <asp:Parameter Name="clientAsset" Type="Object" />
            <asp:Parameter Name="original_clientAsset" Type="Object" />
        </UpdateParameters>
        <SelectParameters>
            <asp:QueryStringParameter DbType="Guid" Name="clientGuid" QueryStringField="guid" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>