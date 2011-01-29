<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="new.aspx.cs" Inherits="Pages_Strategy_new" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h4>
        Add New Strategy</h4>
    <br />
    <asp:DetailsView ID="detailsView" runat="server" Height="50px" Width="50%" AlternatingRowStyle-CssClass="even"
        CssClass="listing" AutoGenerateRows="False" DataKeyNames="ID" DataSourceID="linqStrategy"
        RowStyle-CssClass="odd" OnItemInserted="detailsView_ItemInserted">
        <RowStyle CssClass="odd"></RowStyle>
        <EmptyDataTemplate>
            ID </td>
            <td style="width: 70%">
                &nbsp;
            </td>
            </tr>
            <tr class="even">
                <td>
                    Name
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr class="odd">
                <td>
                    Time Horizon
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr class="even">
                <td class="lnowrap">
                    Return Over Base &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr class="odd">
                <td>
                    Benchmark
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr class="even">
                <td>
                    Rolling Return
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr class="odd">
                <td colspan="2" class="right">
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="New"
                        Text="New"></asp:LinkButton>
                </td>
            </tr>
        </EmptyDataTemplate>
        <Fields>
            <asp:TemplateField HeaderText="ID" SortExpression="ID" ItemStyle-Width="70%">
                <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="textStrategyID" runat="server" Text='<%# Bind("ID") %>' MaxLength="2"
                        Width="2em"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validRequiredID" runat="server" ErrorMessage="Please enter an upper case two letter Strategy ID."
                        Display="None" ControlToValidate="textStrategyID"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="validRegexStrategyID" runat="server" ErrorMessage="Please enter an upper case two letter Strategy ID."
                        ControlToValidate="textStrategyID" Display="None" ValidationExpression="[A-Z][A-Z]"></asp:RegularExpressionValidator>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name" SortExpression="Name">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="textName" runat="server" Text='<%# Bind("Name") %>' Width="100%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validRequiredName" runat="server" ErrorMessage="Please enter a Strategy Name."
                        Display="None" ControlToValidate="textName"></asp:RequiredFieldValidator>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Time Horizon" SortExpression="TimeHorizon">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("TimeHorizon") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:DropDownList ID="listTimeHorizonEdit" runat="server" SelectedValue='<%# Bind("TimeHorizon") %>'
                        Width="20%">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem>13</asp:ListItem>
                        <asp:ListItem>14</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>16</asp:ListItem>
                        <asp:ListItem>17</asp:ListItem>
                        <asp:ListItem>19</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                    </asp:DropDownList>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("TimeHorizon") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Return Over Base &nbsp;" SortExpression="ReturnOverBase">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("ReturnOverBase") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="textROB" runat="server" Text='<%# Bind("ReturnOverBase") %>' Width="5em"></asp:TextBox>
                    <asp:RangeValidator ID="validRangeROB" runat="server" ErrorMessage="Please enter a Return Over Base of between 0 and 10."
                        ControlToValidate="textROB" Display="None" Type="Double" MinimumValue="0" MaximumValue="10"></asp:RangeValidator>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("ReturnOverBase") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Benchmark" SortExpression="BenchmarkID">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("BenchmarkID") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:DropDownList ID="listBenchmark" runat="server" DataSource='<%# GetBenchmarks() %>'
                        SelectedValue='<%# Bind("BenchmarkID") %>' DataTextField="Name" DataValueField="ID"
                        AppendDataBoundItems="True" Width="100%">
                        <asp:ListItem Selected="True" Value="0">-- Select Benchmark --</asp:ListItem>
                    </asp:DropDownList>
                    <asp:CompareValidator ID="validCompareBenchmark" runat="server" ErrorMessage="Please select a Benchmark from the list."
                        Display="None" ControlToValidate="listBenchmark" ValueToCompare="0" Operator="NotEqual"></asp:CompareValidator>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("BenchmarkID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Rolling Return" SortExpression="RollingReturn">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("RollingReturn") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="textRR" runat="server" Text='<%# Bind("RollingReturn") %>' Width="5em"></asp:TextBox>
                    <asp:RangeValidator ID="validRangeRR" runat="server" ErrorMessage="Please enter a Rolling Return value of beween -5 and 10"
                        Display="None" Type="Double" MaximumValue="10" ControlToValidate="textRR" MinimumValue="-5"></asp:RangeValidator>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("RollingReturn") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" ItemStyle-CssClass="right">
                <InsertItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Insert"
                        Text="Insert"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="Cancel"></asp:LinkButton>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="New"
                        Text="New"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Fields>
        <AlternatingRowStyle CssClass="even"></AlternatingRowStyle>
    </asp:DetailsView>
    <br />

    <asp:ValidationSummary ID="validationSummary1" runat="server" />
    <asp:Label ID="labelException" runat="server" Text=""></asp:Label>
<br />
    <asp:HyperLink ID="hyperNewTextContent" runat="server" NavigateUrl="~/Pages/Content/strategy.aspx">Add strategy text content</asp:HyperLink>
    <asp:LinqDataSource ID="linqStrategy" runat="server" ContextTypeName="RSMTenon.Data.RepGenDataContext"
        EnableInsert="True" TableName="Strategies" Where="ID == @ID">
        <WhereParameters>
            <asp:Parameter DefaultValue="false" Name="ID" Type="String" />
        </WhereParameters>
    </asp:LinqDataSource>
</asp:Content>
