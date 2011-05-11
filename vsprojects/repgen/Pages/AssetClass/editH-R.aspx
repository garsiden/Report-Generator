<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="editH-R.aspx.cs" Inherits="Pages_AssetClass_editH_R" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h4>
        Edit Asset Class Historic Data (H - R)</h4>
    <br />
    <asp:GridView ID="gridHistoricData" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        CssClass="listing" DataKeyNames="Date" DataSourceID="sourceHistoricData" ShowFooter="True"
        OnRowCommand="gridHistoricData_RowCommand" UseAccessibleHeader="False" Width="100%"
        AllowSorting="True" OnRowDeleted="gridHistoricData_RowDeleted" OnRowUpdated="gridHistoricData_RowUpdated">
        <RowStyle CssClass="odd" />
        <Columns>
            <asp:TemplateField HeaderText="Date" SortExpression="Date">
                <EditItemTemplate>
                    <asp:Label ID="labelDateEdit" runat="server" Text='<%# Eval("Date", "{0:d}") %>'></asp:Label>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textDateAdd" runat="server" Width="6em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="labelDate" runat="server" Text='<%# Bind("Date", "{0:d}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="center" />
                <ItemStyle CssClass="center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Hedge">
                <EditItemTemplate>
                    <asp:TextBox ID="textHEDGEdit" runat="server" Text='<%# Bind("HEDG", "{0:0.00}") %>'
                        Width="5em"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textHEDGAdd" runat="server" Width="5em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="labelHEDG" runat="server" Text='<%# Bind("HEDG", "{0:0.00}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="right" />
                <HeaderStyle CssClass="center" />
                <ItemStyle CssClass="right" Width="15%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Long Short">
                <EditItemTemplate>
                    <asp:TextBox ID="textLOSHEdit" runat="server" Text='<%# Bind("LOSH", "{0:0.00}") %>'
                        Width="5em"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textLOSHAdd" runat="server" Width="5em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="labelLOSH" runat="server" Text='<%# Bind("LOSH", "{0:0.00}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="right" HorizontalAlign="Right" />
                <HeaderStyle CssClass="center" />
                <ItemStyle CssClass="right" Width="15%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Managed Futures">
                <EditItemTemplate>
                    <asp:TextBox ID="textMAFUEdit" runat="server" Text='<%# Bind("MAFU", "{0:0.00}") %>'
                        Width="5em"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textMAFUAdd" runat="server" Width="5em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="labelMAFU" runat="server" Text='<%# Bind("MAFU", "{0:0.00}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="right" HorizontalAlign="Right" />
                <HeaderStyle CssClass="center" />
                <ItemStyle CssClass="right" Width="15%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Private Equity">
                <EditItemTemplate>
                    <asp:TextBox ID="textPREQEdit" runat="server" Text='<%# Bind("PREQ", "{0:0.00}") %>'
                        Width="5em"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textPREQAdd" runat="server" Width="5em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="textPREQ" runat="server" Text='<%# Bind("PREQ", "{0:0.00}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="right" />
                <HeaderStyle CssClass="center" />
                <ItemStyle CssClass="right" Width="15%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Real Estate">
                <EditItemTemplate>
                    <asp:TextBox ID="textCOPREdit" runat="server" Text='<%# Bind("COPR", "{0:0.00}") %>'
                        Width="5em"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textCOPRAdd" runat="server" Width="5em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="labelCOPR" runat="server" Text='<%# Bind("COPR", "{0:0.00}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle CssClass="right" />
                <HeaderStyle CssClass="center" />
                <ItemStyle CssClass="right" Width="15%" />
            </asp:TemplateField>
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
        </Columns>
        <AlternatingRowStyle CssClass="even" />
    </asp:GridView>
    <br />
    <p align="right">
        <asp:HyperLink ID="linkAG" runat="server" NavigateUrl="~/Pages/AssetClass/editA-G.aspx">A-G</asp:HyperLink>&nbsp;
        <asp:HyperLink ID="linkSZ" runat="server" NavigateUrl="~/Pages/AssetClass/editS-Z.aspx">S-Z</asp:HyperLink>
    </p>
    <br />
    <br />
    <asp:Label ID="labelException" class="errortext" runat="server" Text=""></asp:Label>
    <asp:LinqDataSource ID="sourceHistoricData" runat="server" ContextTypeName="RSMTenon.Data.RepGenDataContext"
        EnableDelete="True" EnableInsert="True" EnableUpdate="True" OrderBy="Date desc"
        TableName="HistoricDatas" OnInserted="sourceHistoricData_Inserted">
    </asp:LinqDataSource>
</asp:Content>
