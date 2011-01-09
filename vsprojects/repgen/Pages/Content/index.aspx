<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Pages_Content_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td>
                &nbsp; Strategy:
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
                <asp:DropDownList ID="listStrategy" runat="server" AutoPostBack="True" DataSource="<%# GetStrategies() %>"
                    AppendDataBoundItems="True" DataTextField="Name" DataValueField="ID">
                    <asp:ListItem Selected="True" Value="%">All</asp:ListItem>
                    <asp:ListItem Value="">None</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" AutoPostBack="True">
                    <asp:ListItem Selected="True" Value="0">Strategy</asp:ListItem>
                    <asp:ListItem Selected="True" Value="1">Charts</asp:ListItem>
                    <asp:ListItem Selected="True" Value="2">General</asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <td>
                <asp:RadioButtonList ID="radioStrategyAssets" runat="server" AutoPostBack="True"
                    Height="59px">
                    <asp:ListItem Value="0">Existing Assets</asp:ListItem>
                    <asp:ListItem Value="1">Cash</asp:ListItem>
                    <asp:ListItem Selected="True" Value="2">All</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp; Charts
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
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
        CssClass="listing" DataKeyNames="GUID" DataSourceID="sourceContent">
        <RowStyle CssClass="odd" />
        <Columns>
            <asp:TemplateField SortExpression="ContentID">
                <ItemTemplate>
                    <b>
                        <asp:Label ID="Label1" runat="server" Text="Content Tag:" Width="10em"></asp:Label>
                        <%# Eval("ContentID") %><br />
                    </b><small><i>
                        <asp:Label ID="Label2" runat="server" Text="Strategy:" Width="10em"></asp:Label>
                        <%# Eval("Strategy.Name") %><br />
                        <asp:Label ID="Label3" runat="server" Text="Category:" Width="10em"></asp:Label>
                        <%# Eval("Category") %>
                    </i></small>
                    <br />
                    <hr />
                    <%# Eval("Text") %>
                    <br />
                    <br />
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                        Text="Edit"></asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    Content Tag:
                    <%# Eval("ContentID") %>
                    Strategy:
                    <%# Eval("Strategy.Name") %>
                    Category:
                    <%# Eval("Category") %>
                    <br />
                    <hr />
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Text") %>' TextMode="MultiLine"
                        Width="100%"></asp:TextBox>
                    <br />
                    <br />
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                        Text="Update"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <ItemStyle CssClass="left" />
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle CssClass="even" />
    </asp:GridView>
    <asp:ObjectDataSource ID="sourceContent" runat="server" ConflictDetection="CompareAllValues"
        DataObjectTypeName="RSMTenon.Data.Content" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetContent" TypeName="RSMTenon.Data.Content" UpdateMethod="UpdateClient">
        <UpdateParameters>
            <asp:Parameter Name="content" Type="Object" />
            <asp:Parameter Name="original_content" Type="Object" />
        </UpdateParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="listStrategy" DefaultValue="%" Name="strategyId"
                PropertyName="SelectedValue" Type="String" ConvertEmptyStringToNull="False" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
