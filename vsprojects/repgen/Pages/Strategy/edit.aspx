<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Pages_Strategy_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:FormView ID="formStrategy" runat="server" DataKeyNames="ID" DataSourceID="sourceStrategy"
        Height="212px" Width="266px">
        <EditItemTemplate>
            <table style="width: 100%;">
                <tr>
                    <td>
                        Name:
                    </td>
                    <td>
                        <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Time Horizon:
                    </td>
                    <td>
                        <asp:DropDownList ID="listTimeHorizonEdit" runat="server" SelectedValue='<%# Bind("TimeHorizon") %>'>
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
                    </td>
                </tr>
                <tr>
                    <td>
                        Return Over Base:
                    </td>
                    <td>
                        <asp:TextBox ID="ReturnOverBaseTextBox" runat="server" Text='<%# Bind("ReturnOverBase", "{0:N}") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Cost:
                    </td>
                    <td>
                        <asp:TextBox ID="CostTextBox" runat="server" Text='<%# Bind("Cost", "{0:N}") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Rolling Return:
                    </td>
                    <td>
                        <asp:TextBox ID="RollingReturnTextBox" runat="server" Text='<%# Bind("RollingReturn", "{0:N}") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Benchmark:
                    </td>
                    <td>
                        <asp:DropDownList ID="listBenchmark" runat="server" DataSource='<%# GetBenchmarks() %>'
                            SelectedValue='<%# Bind("BenchmarkID") %>' DataTextField="Name" DataValueField="ID">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                Text="Update" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False"
                CommandName="Cancel" Text="Cancel" />
        </EditItemTemplate>
        <InsertItemTemplate>
            ID:
            <asp:TextBox ID="IDTextBox" runat="server" Text='<%# Bind("ID") %>' />
            <br />
            Name:
            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
            <br />
            TimeHorizon:
            <asp:TextBox ID="TimeHorizonTextBox" runat="server" Text='<%# Bind("TimeHorizon") %>' />
            <br />
            ReturnOverBase:
            <asp:TextBox ID="ReturnOverBaseTextBox" runat="server" Text='<%# Bind("ReturnOverBase") %>' />
            <br />
            Cost:
            <asp:TextBox ID="CostTextBox" runat="server" Text='<%# Bind("Cost") %>' />
            <br />
            BenchmarkID:
            <asp:TextBox ID="BenchmarkIDTextBox" runat="server" Text='<%# Bind("BenchmarkID") %>' />
            <br />
            RollingReturn:
            <asp:TextBox ID="RollingReturnTextBox" runat="server" Text='<%# Bind("RollingReturn") %>' />
            <br />
            Clients:
            <asp:TextBox ID="ClientsTextBox" runat="server" Text='<%# Bind("Clients") %>' />
            <br />
            ModelBreakdowns:
            <asp:TextBox ID="ModelBreakdownsTextBox" runat="server" Text='<%# Bind("ModelBreakdowns") %>' />
            <br />
            Contents:
            <asp:TextBox ID="ContentsTextBox" runat="server" Text='<%# Bind("Contents") %>' />
            <br />
            Benchmark:
            <asp:TextBox ID="BenchmarkTextBox" runat="server" Text='<%# Bind("Benchmark") %>' />
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                Text="Insert" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False"
                CommandName="Cancel" Text="Cancel" />
        </InsertItemTemplate>
        <ItemTemplate>
            <table style="width: 100%;">
                <tr>
                    <td>
                        Name:
                    </td>
                    <td>
                        <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("Name") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Time Horizon:
                    </td>
                    <td>
                        <asp:Label ID="TimeHorizonLabel" runat="server" Text='<%# Bind("TimeHorizon") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Return Over Base:
                    </td>
                    <td>
                        <asp:Label ID="ReturnOverBaseLabel" runat="server" Text='<%# Bind("ReturnOverBase") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Cost:
                    </td>
                    <td>
                        <asp:Label ID="CostLabel" runat="server" Text='<%# Bind("Cost", "{0:£#,##0}") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Rolling Return:
                    </td>
                    <td>
                        <asp:Label ID="RollingReturnLabel" runat="server" Text='<%# Bind("RollingReturn") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Benchmark:
                    </td>
                    <td>
                        <asp:Label ID="BenchmarkLabel" runat="server" Text='<%# Bind("Benchmark.Name") %>' />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                            Text="Edit" />
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:FormView>
    <asp:LinqDataSource ID="sourceStrategy" runat="server" ContextTypeName="RSMTenon.Data.RepGenDataContext"
        EnableUpdate="True" TableName="Strategies" Where="ID == @ID">
        <WhereParameters>
            <asp:QueryStringParameter DefaultValue="CO" Name="ID" QueryStringField="id" Type="String" />
        </WhereParameters>
    </asp:LinqDataSource>
</asp:Content>
