<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Pages_Strategy_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h4>Edit Strategies</h4>
<br />
    <asp:GridView ID="gridStrategy" runat="server" AutoGenerateColumns="False" 
        CssClass="listing" DataKeyNames="ID" DataSourceID="sourceStrategy">
        <RowStyle CssClass="odd" />
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Strategy Name" 
                SortExpression="Name">
            <ItemStyle CssClass="lnowrap" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Time Horizon" SortExpression="TimeHorizon">
            <ItemStyle CssClass="cnowrap" />
                <EditItemTemplate>
                        <asp:DropDownList ID="listTimeHorizonEdit" runat="server" 
                            SelectedValue='<%# Bind("TimeHorizon") %>' Width="100%">
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
            <asp:BoundField DataField="ReturnOverBase" HeaderText="Return Over Base" 
                SortExpression="ReturnOverBase">
            <ItemStyle CssClass="right" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Cost" SortExpression="Cost">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Cost", "{0:0}") %>' 
                        Width="5em"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("Cost", "{0:C}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:BoundField DataField="RollingReturn" HeaderText="Rolling Return" 
                SortExpression="RollingReturn">
            <ControlStyle Width="5em" />
            <ItemStyle CssClass="right" />
            </asp:BoundField>
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
    <asp:LinqDataSource ID="sourceStrategy" runat="server" 
        ContextTypeName="RSMTenon.Data.RepGenDataContext" EnableUpdate="True" 
        TableName="Strategies">
    </asp:LinqDataSource>
    </asp:Content>

