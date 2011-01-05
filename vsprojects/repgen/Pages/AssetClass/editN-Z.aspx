<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="assetsN-Z.aspx.cs" Inherits="Pages_HistoricData_assetsN_Z" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:GridView ID="gridHistoricData" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        CssClass="listing" DataKeyNames="Date" DataSourceID="sourceHistoricData" ShowFooter="True"
        OnRowCommand="gridHistoricData_RowCommand" UseAccessibleHeader="False" Width="5em">
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
            <asp:TemplateField HeaderText="Private Equity" SortExpression="PREQ">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("PREQ", "{0:0.00}") %>'
                        Width="5em"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textCASHAdd" runat="server" Width="5em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("PREQ", "{0:0.00}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="right" />
                <ItemStyle CssClass="right" Width="12%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UK Corp Bonds" SortExpression="UKCB">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("UKCB", "{0:0.00}") %>'
                        Width="5em"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textCOPRAdd" runat="server" Width="5em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("UKCB", "{0:0.00}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="right" />
                <ItemStyle CssClass="right" Width="12%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UK Govt Bonds" SortExpression="UKGB">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("UKGB", "{0:0.00}") %>'
                        Width="5em"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textCOMMAdd" runat="server" Width="5em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("UKGB", "{0:0.00}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="right" />
                <ItemStyle CssClass="right" Width="12%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UK High Yield" SortExpression="UKHY">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("UKHY", "{0:0.00}") %>'
                        Width="5em"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textGLEQAdd" runat="server" Width="5em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("UKHY", "{0:0.00}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="right" />
                <ItemStyle CssClass="right" Width="12%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UK Equities" SortExpression="UKEQ">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("UKEQ", "{0:0.00}") %>'
                        Width="5em"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textHEDGAdd" runat="server" Width="5em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("UKEQ", "{0:0.00}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="right" />
                <ItemStyle CssClass="right" Width="12%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="World Bonds" SortExpression="WOBO">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("WOBO", "{0:0.00}") %>'
                        Width="5em"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textLOSHAdd" runat="server" Width="5em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("WOBO", "{0:0.00}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="right" HorizontalAlign="Right" />
                <ItemStyle CssClass="right" Width="12%" />
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle CssClass="even" />
    </asp:GridView>
    <br />
<p align="right">
    <asp:HyperLink ID="linkAM" runat="server" NavigateUrl="~/Pages/HistoricData/assetsA-M.aspx">A - M</asp:HyperLink>
</p>
    <asp:LinqDataSource ID="sourceHistoricData" runat="server" ContextTypeName="RSMTenon.Data.RepGenDataContext"
        EnableDelete="True" EnableInsert="True" EnableUpdate="True" OrderBy="Date desc"
        TableName="HistoricDatas">
    </asp:LinqDataSource>
</asp:Content>
