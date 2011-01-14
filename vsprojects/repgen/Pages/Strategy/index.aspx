<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Pages_Strategy_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h4>
        Edit Strategies</h4>
    <br />
    <asp:GridView ID="gridStrategy" runat="server" AutoGenerateColumns="False" CssClass="listing"
        DataKeyNames="ID" DataSourceID="sourceStrategy">
        <RowStyle CssClass="odd" />
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Strategy Name" SortExpression="Name">
                <ItemStyle CssClass="lnowrap" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Time Horizon" SortExpression="TimeHorizon">
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
            <asp:TemplateField HeaderText="Return Over Base" SortExpression="ReturnOverBase">
                <EditItemTemplate>
                    <asp:RangeValidator ID="validRangeReturnOverBase" runat="server" 
                        ControlToValidate="TextBoxReturnOverBase" Display="None" 
                        ErrorMessage="Please enter a valid Return Over Base value" MaximumValue="10" 
                        MinimumValue="0" Type="Double"></asp:RangeValidator>
                    <asp:TextBox ID="TextBoxReturnOverBase" runat="server" 
                        Text='<%# Bind("ReturnOverBase") %>' Width="5em"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("ReturnOverBase") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Cost" SortExpression="Cost">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxCost" runat="server" 
                        Text='<%# Bind("Cost", "{0:0}") %>' Width="5em"></asp:TextBox>
                    <asp:RangeValidator ID="validRangeCost" runat="server" 
                        ControlToValidate="TextBoxCost" Display="None" 
                        ErrorMessage="Please enter a valid Cost" MaximumValue="5000" MinimumValue="0" 
                        Type="Currency"></asp:RangeValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("Cost", "{0:C0}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Rolling Return" SortExpression="RollingReturn">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxRollingReturn" runat="server" 
                        Text='<%# Bind("RollingReturn") %>'></asp:TextBox>
                    <asp:RangeValidator ID="validRangeRollingReturn" runat="server" 
                        ControlToValidate="TextBoxRollingReturn" Display="None" 
                        ErrorMessage="Please enter a valid Rolling Return value" MaximumValue="10" 
                        MinimumValue="-10" Type="Double"></asp:RangeValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("RollingReturn") %>'></asp:Label>
                </ItemTemplate>
                <ControlStyle Width="5em" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Benchmark" SortExpression="Benchmark">
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
            <asp:CommandField ShowEditButton="True" />
        </Columns>
        <AlternatingRowStyle CssClass="even" />
    </asp:GridView>
<br />
    <asp:ValidationSummary ID="validationSummary" runat="server" />
    <asp:LinqDataSource ID="sourceStrategy" runat="server" ContextTypeName="RSMTenon.Data.RepGenDataContext"
        EnableUpdate="True" TableName="Strategies">
    </asp:LinqDataSource>
</asp:Content>
