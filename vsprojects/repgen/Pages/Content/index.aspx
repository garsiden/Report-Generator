<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Pages_Content_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h4>
        Edit Text Content</h4>
    <br />
    <b>Show text for:</b>
    <br />
    <table width="50%">
        <tr>
            <td class="left" width="60%">
                <b>Content Tag:&nbsp;&nbsp;</b>
            </td>
            <td class="left">
                <asp:DropDownList ID="listContentTag" runat="server" DataSource="<%# GetContentTags() %>"
                    AppendDataBoundItems="True" AutoPostBack="True">
                    <asp:ListItem Value="All" Selected="True">All</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="left">
                <b>Strategy:</b>
            </td>
            <td class="left">
                <asp:DropDownList ID="listStrategy" runat="server" AutoPostBack="True" DataSource="<%# GetStrategies() %>"
                    AppendDataBoundItems="True" DataTextField="Name" DataValueField="ID">
                    <asp:ListItem Selected="True" Value="AnyOrNone">Any or None</asp:ListItem>
                    <asp:ListItem Value="Any">Any</asp:ListItem>
                    <asp:ListItem>None</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" class="left">
                <b>Category</b>
            </td>
            <td class="left">
                <asp:RadioButtonList ID="listCategory" runat="server" AutoPostBack="True" BorderStyle="Double">
                    <asp:ListItem Value="0">Assets</asp:ListItem>
                    <asp:ListItem Value="1">Cash</asp:ListItem>
                    <asp:ListItem Selected="True" Value="2">Both</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                &nbsp;
                <asp:Label ID="labelCount" runat="server" Text="count"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <p>
        Use <i>[Strategy]</i> to indicate where a strategy name should be placed.</p>
    <br />
    <asp:GridView ID="gridContent" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        CssClass="listing" DataKeyNames="GUID" DataSourceID="sourceContent" OnRowDataBound="gridContent_RowDataBound">
        <RowStyle CssClass="odd" />
        <Columns>
            <asp:TemplateField SortExpression="ContentID">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text="Content Tag:" Width="10em" Font-Italic="True"></asp:Label>
                    <b>
                        <%# Eval("ContentID") %><br />
                    </b>
                    <asp:Label ID="Label2" runat="server" Text="Strategy:" Width="10em" Font-Italic="True"></asp:Label>
                    <b>
                        <%# Eval("Strategy.Name") %><br />
                    </b>
                    <asp:Label ID="Label3" runat="server" Text="Category:" Width="10em" Font-Italic="True"></asp:Label>
                    <b>
                        <%# Eval("Category") %></b>
                    <br />
                    <%# Eval("Text") %>
                    <br />
                    <br />
                    <div align="right">
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                            Text="Edit"></asp:LinkButton>
                    </div>
                    <br />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text="Content Tag:" Width="10em" Font-Italic="True"></asp:Label>
                    <b>
                        <%# Eval("ContentID") %><br />
                    </b>
                    <asp:Label ID="Label2" runat="server" Text="Strategy:" Width="10em" Font-Italic="True"></asp:Label>
                    <b>
                        <%# Eval("Strategy.Name") %><br />
                    </b>
                    <asp:Label ID="Label3" runat="server" Text="Category:" Width="10em" Font-Italic="True"></asp:Label>
                    <b>
                        <%# Eval("Category") %></b>
                    <br />
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Text") %>' TextMode="MultiLine"
                        Width="100%" Rows="4"></asp:TextBox>
                    <br />
                    <br />
                    <div align="right">
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                            Text="Update"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text="Cancel"></asp:LinkButton>
                    </div>
                    <br />
                </EditItemTemplate>
                <ItemStyle CssClass="left" />
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            No content to display for selected criteria.
        </EmptyDataTemplate>
        <AlternatingRowStyle CssClass="even" />
    </asp:GridView>
    <asp:ObjectDataSource ID="sourceContent" runat="server" ConflictDetection="CompareAllValues"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetContents" TypeName="RSMTenon.Data.Content"
        UpdateMethod="UpdateContent" DataObjectTypeName="RSMTenon.Data.Content">
        <UpdateParameters>
            <asp:Parameter Name="content" Type="Object" />
            <asp:Parameter Name="original_content" Type="Object" />
        </UpdateParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="listStrategy" DefaultValue="AllOrNone" Name="strategyId"
                PropertyName="SelectedValue" Type="String" ConvertEmptyStringToNull="False" />
            <asp:ControlParameter ControlID="listCategory" DefaultValue="2" Name="categoryIdx"
                PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="listContentTag" DefaultValue="String.Empty" Name="contentId"
                PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
