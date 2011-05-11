<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Pages_AssetClass_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h4>
        Edit Asset Classes</h4>
    <br />
    <asp:GridView ID="gridAssetClass" runat="server" class="listing" DataSourceID="sourceAssetClass"
        AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ID" Width="60%"
        OnRowUpdated="gridAssetClass_RowUpdated">
        <RowStyle CssClass="odd" />
        <Columns>
            <asp:TemplateField HeaderText="Name" SortExpression="Name">
                <EditItemTemplate>
                    <asp:TextBox ID="textName" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validRequiredName" runat="server" ErrorMessage="Please enter an Asset Class Name."
                        Display="None" ControlToValidate="textName"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="labelName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                </ItemTemplate>
                <ControlStyle Width="90%" />
                <ItemStyle CssClass="lnowrap" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Group" SortExpression="AssetGroupID">
                <EditItemTemplate>
                    <asp:DropDownList ID="listAssetGroup" runat="server" DataTextField="Name" DataValueField="ID"
                        DataSource="<%# GetModelAssetGroups() %>" SelectedValue='<%# Bind("AssetGroupID") %>'
                        Width="100%">
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="labelAssetGroup" runat="server" Text='<%# Eval("AssetGroup.Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True">
                <ItemStyle CssClass="lnowrap" Width="15%" />
            </asp:CommandField>
        </Columns>
        <AlternatingRowStyle CssClass="even" />
    </asp:GridView>
    <br />
    <asp:Label ID="labelException" class="errortext" runat="server" Text=""></asp:Label>
    <br />
    <asp:ValidationSummary ID="validationSummary1" runat="server" />
    <asp:LinqDataSource ID="sourceAssetClass" runat="server" ContextTypeName="RSMTenon.Data.RepGenDataContext"
        EnableUpdate="True" TableName="AssetClasses" OrderBy="Name">
    </asp:LinqDataSource>
</asp:Content>
