<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Pages_Content_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 149px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td class="style1" valign="top">
                &nbsp; Show text for:
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1" valign="top">
                <asp:Label ID="labelCount" runat="server" Text="count"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1" valign="top">
                Content</td>
            <td>
                <asp:RadioButtonList ID="listContent" runat="server" AutoPostBack="True" 
                    BorderStyle="Double">
                    <asp:ListItem Value="0">Strategy</asp:ListItem>
                    <asp:ListItem Value="1">Charts</asp:ListItem>
                    <asp:ListItem Selected="True" Value="2">Both</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1" valign="top">
                Category</td>
            <td>
                <asp:RadioButtonList ID="listCategory" runat="server" AutoPostBack="True" 
                    BorderStyle="Double">
                    <asp:ListItem Value="0">Assets</asp:ListItem>
                    <asp:ListItem Value="1">Cash</asp:ListItem>
                    <asp:ListItem Selected="True" Value="2">Both</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;Strategy:</td>
            <td>
                &nbsp;
                <asp:DropDownList ID="listStrategy" runat="server" AutoPostBack="True" DataSource="<%# GetStrategies() %>"
                    AppendDataBoundItems="True" DataTextField="Name" DataValueField="ID">
                    <asp:ListItem Selected="True" Value="%">All</asp:ListItem>
                    <asp:ListItem Value="">None</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:GridView ID="gridContent" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        CssClass="listing" DataKeyNames="GUID" DataSourceID="sourceContent" 
        onrowdatabound="gridContent_RowDataBound">
        <RowStyle CssClass="odd" />
        <Columns>
            <asp:TemplateField SortExpression="ContentID">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text="Content Tag:" Width="10em" Font-Italic="True"></asp:Label>
                    <b><%# Eval("ContentID") %><br /></b>
                    <asp:Label ID="Label2" runat="server" Text="Strategy:" Width="10em" Font-Italic="True"></asp:Label>
                    <b><%# Eval("Strategy.Name") %><br /></b>
                    <asp:Label ID="Label3" runat="server" Text="Category:" Width="10em" Font-Italic="True"></asp:Label>
                    <b><%# Eval("Category") %></b>
                    <br />

                    <%# Eval("Text") %>
                    <br />
                    <br />
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                        Text="Edit"></asp:LinkButton>
                    <br />
                </ItemTemplate>
                <EditItemTemplate>
                    Content Tag:
                    <%# Eval("ContentID") %>
                    Strategy:
                    <%# Eval("Strategy.Name") %>
                    Category:
                    <%# Eval("Category") %>
                    <br />

                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Text") %>' TextMode="MultiLine"
                        Width="100%" Rows="4"></asp:TextBox>
                    <br />
                    <br />
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                        Text="Update"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="Cancel"></asp:LinkButton>
                    <br />

                </EditItemTemplate>
                <ItemStyle CssClass="left" />
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle CssClass="even" />
    </asp:GridView>
    <asp:ObjectDataSource ID="sourceContent" runat="server" 
        ConflictDetection="CompareAllValues" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetContent" TypeName="RSMTenon.Data.Content" 
        UpdateMethod="UpdateClient">
        <UpdateParameters>
            <asp:Parameter Name="content" Type="Object" />
            <asp:Parameter Name="original_content" Type="Object" />
        </UpdateParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="listStrategy" DefaultValue="%" Name="strategyId"
                PropertyName="SelectedValue" Type="String" ConvertEmptyStringToNull="False" />
            <asp:ControlParameter ControlID="listContent" DefaultValue="2" 
                Name="contentIdx" PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="listCategory" DefaultValue="2" 
                Name="categoryIdx" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
