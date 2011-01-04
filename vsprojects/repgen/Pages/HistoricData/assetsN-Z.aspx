<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master" AutoEventWireup="true" CodeFile="assetsN-Z.aspx.cs" Inherits="Pages_HistoricData_assetsN_Z" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView ID="gridHistoricData" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" CssClass="listing" DataKeyNames="Date" 
        DataSourceID="sourceHistoricData" ShowFooter="True" 
        onrowcommand="gridHistoricData_RowCommand">
        <RowStyle CssClass="odd" />
        <Columns>
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                        CommandName="Update" Text="Update"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                        CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:LinkButton ID="linkAdd" runat="server" CommandName="Insert">Add</asp:LinkButton>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                        CommandName="Edit" Text="Edit"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                        CommandName="Delete" Text="Delete"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date" SortExpression="Date" ItemStyle-HorizontalAlign="Right">
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
            <asp:TemplateField HeaderText="Cash" SortExpression="CASH">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CASH", "{0:0.00}") %>' Width="5em"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textCASHAdd" runat="server" Width="5em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("CASH", "{0:0.00}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="right" Width="12%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Commercial Property" SortExpression="COPR">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("COPR", "{0:0.00}") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textCOPRAdd" runat="server" Width="5em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("COPR", "{0:0.00}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="right" Width="12%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Commod- ities" SortExpression="COMM">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("COMM", "{0:0.00}") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textCOMMAdd" runat="server" Width="5em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("COMM", "{0:0.00}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="right" Width="12%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Global Equities" SortExpression="GLEQ">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("GLEQ", "{0:0.00}") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textGLEQAdd" runat="server" Width="5em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("GLEQ", "{0:0.00}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="right" Width="12%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Hedge" SortExpression="HEDG">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("HEDG", "{0:0.00}") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textHEDGAdd" runat="server" Width="5em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("HEDG", "{0:0.00}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="right" Width="12%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Long Short" SortExpression="LOSH">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("LOSH", "{0:0.00}") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="textLOSHAdd" runat="server" Width="5em"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("LOSH", "{0:0.00}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="right" Width="12%" />
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle CssClass="even" />
    </asp:GridView>
    <asp:LinqDataSource ID="sourceHistoricData" runat="server" 
        ContextTypeName="RSMTenon.Data.RepGenDataContext" EnableDelete="True" 
        EnableInsert="True" EnableUpdate="True" OrderBy="Date desc" 
        TableName="HistoricDatas">
    </asp:LinqDataSource>

</asp:Content>

