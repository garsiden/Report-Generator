<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Pages_Strategy_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h4>
        Edit Strategies</h4>
    <br />
    <asp:GridView ID="gridStrategy" runat="server" AutoGenerateColumns="False" CssClass="listing"
        DataKeyNames="ID" DataSourceID="sourceStrategy" OnRowUpdated="gridStrategy_RowUpdated"
        OnRowDeleted="gridStrategy_RowDeleted" Width="100%">
        <RowStyle CssClass="odd" />
        <Columns>
            <asp:TemplateField HeaderText="Strategy Name" SortExpression="Name" ItemStyle-Width="25%">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxStrategyName" runat="server" Text='<%# Bind("Name") %>' Width="100px" Wrap="False"></asp:TextBox><asp:RequiredFieldValidator
                        ID="validRequiredStrategyName" runat="server" ErrorMessage="Please enter a Strategy Name." Display="None" ControlToValidate="TextBoxStrategyName"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Time Horizon" SortExpression="TimeHorizon" ItemStyle-Width="10%">
                <ItemStyle CssClass="cnowrap" />
                <EditItemTemplate>
                    <asp:DropDownList ID="listTimeHorizonEdit" runat="server" SelectedValue='<%# Bind("TimeHorizon") %>'
                        Width="100%">
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
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("TimeHorizon") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Return Over Base" SortExpression="ReturnOverBase" ItemStyle-Width="10%">
                <EditItemTemplate>
                    <asp:RangeValidator ID="validRangeReturnOverBase" runat="server" ControlToValidate="TextBoxReturnOverBase"
                        Display="None" ErrorMessage="Please enter a valid Return Over Base value" MaximumValue="10"
                        MinimumValue="0" Type="Double"></asp:RangeValidator>
                    <asp:TextBox ID="TextBoxReturnOverBase" runat="server" Text='<%# Bind("ReturnOverBase") %>'
                        Width="5em"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("ReturnOverBase", "{0:0\\%}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Rolling Return" SortExpression="RollingReturn" ItemStyle-Width="10%">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxRollingReturn" runat="server" Text='<%# Bind("RollingReturn") %>' Width="5em"></asp:TextBox>
                    <asp:RangeValidator ID="validRangeRollingReturn" runat="server" ControlToValidate="TextBoxRollingReturn"
                        Display="None" ErrorMessage="Please enter a valid Rolling Return value" MaximumValue="10"
                        MinimumValue="-10" Type="Double"></asp:RangeValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("RollingReturn", "{0:0.0\\%}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Aggregate Charge" SortExpression="AggregateCharge" ItemStyle-Width="10%">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxAggregateCharge" runat="server" Text='<%# Bind("AggregateCharge") %>'
                        Width="5em"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validRequiredAggregateCharge" runat="server" ErrorMessage="Please enter an Aggregate Charge vlaue."
                        Display="None" ControlToValidate="TextBoxAggregateCharge"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("AggregateCharge", "{0:0.00\\%}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Benchmark" SortExpression="Benchmark" ItemStyle-Width="25%">
                <EditItemTemplate>
                    <asp:DropDownList ID="listBenchmark" runat="server" DataSource="<%# GetBenchmarks() %>"
                        SelectedValue='<%# Bind("BenchmarkID") %>' DataTextField="Name" DataValueField="ID">
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Benchmark.Name") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="lnowrap" />
            </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="True">
                <ItemStyle Width="5%" />
            </asp:CommandField>
            <asp:CommandField ShowEditButton="True">
                <ItemStyle Width="5%" />
            </asp:CommandField>
        </Columns>
        <AlternatingRowStyle CssClass="even" />
    </asp:GridView>
    <br />
    <asp:ValidationSummary ID="validationSummary" runat="server" />
    <br />
    <asp:Label ID="labelException" class="errortext" runat="server" Text=""></asp:Label>
    <asp:LinqDataSource ID="sourceStrategy" runat="server" ContextTypeName="RSMTenon.Data.RepGenDataContext"
        EnableUpdate="True" TableName="Strategies" EnableDelete="True">
    </asp:LinqDataSource>
</asp:Content>
