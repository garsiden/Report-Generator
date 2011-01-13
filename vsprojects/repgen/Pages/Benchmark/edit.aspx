<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Pages_Benchmark_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h4>Edit Benchmark Historic Data</h4>
<br />
    <asp:GridView ID="gridBenchmarkData" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        CssClass="listing" DataKeyNames="Date" DataSourceID="sourceBenchmarkData" ShowFooter="True"
        OnRowCommand="gridBenchmarkData_RowCommand" UseAccessibleHeader="False" 
        Width="5em" AllowSorting="True">
        <RowStyle CssClass="odd" />
        <Columns>
            <asp:TemplateField ShowHeader="False" ItemStyle-CssClass="lnowrap">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                        Text="Update"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:LinkButton ID="linkAdd" runat="server" CommandName="Insert">Add</asp:LinkButton>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                        Text="Edit"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                        Text="Delete"></asp:LinkButton>
                </ItemTemplate>
                <FooterStyle CssClass="left" />
                <ItemStyle CssClass="lnowrap"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date" SortExpression="Date">
                <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Date", "{0:d}") %>'></asp:Label>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textDateAdd" runat="server" Width="6em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Date", "{0:d}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Active Managed">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ACMA", "{0:0.00}") %>'
                        Width="5em"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textACMAAdd" runat="server" Width="5em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("ACMA", "{0:0.00}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="right" />
                <ItemStyle CssClass="right" Width="12%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Balanced Managed">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("BAMA", "{0:0.00}") %>'
                        Width="5em"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textBAMAAdd" runat="server" Width="5em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("BAMA", "{0:0.00}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="right" />
                <ItemStyle CssClass="right" Width="12%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Cautious Managed">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("CAMA", "{0:0.00}") %>'
                        Width="5em"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textCAMAAdd" runat="server" Width="5em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("CAMA", "{0:0.00}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="right" />
                <ItemStyle CssClass="right" Width="12%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Global Growth">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("GLGR", "{0:0.00}") %>'
                        Width="5em"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textGLGRAdd" runat="server" Width="5em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("GLGR", "{0:0.00}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="right" />
                <ItemStyle CssClass="right" Width="12%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Strategic Bonds £">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("STBO", "{0:0.00}") %>'
                        Width="5em"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textSTBOAdd" runat="server" Width="5em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("STBO", "{0:0.00}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="right" />
                <ItemStyle CssClass="right" Width="12%" />
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle CssClass="even" />
    </asp:GridView>
    <asp:LinqDataSource ID="sourceBenchmarkData" runat="server" ContextTypeName="RSMTenon.Data.RepGenDataContext"
        EnableDelete="True" EnableInsert="True" EnableUpdate="True" OrderBy="Date desc"
        TableName="BenchmarkDatas">
    </asp:LinqDataSource>



</asp:Content>

