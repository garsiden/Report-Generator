<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Pages_Client_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:GridView ID="gridClient" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" CaptionAlign="Top" 
        CssClass="listing" DataKeyNames="GUID" DataSourceID="sourceClient" 
        onrowcommand="gridClient_RowCommand">
        <RowStyle CssClass="even" />
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="GUID" 
                DataNavigateUrlFormatString="~/Pages/Client/edit.aspx?guid={0}" 
                DataTextField="Name" HeaderText="Name" SortExpression="Name" 
                Text="Edit client details">
            <ItemStyle CssClass="lnowrap" />
            </asp:HyperLinkField>
            <asp:TemplateField HeaderText="Strategy" SortExpression="StrategyID">
                <EditItemTemplate>
                    <asp:TextBox ID="textStrategyName" runat="server" Text='<%# Bind("Strategy.Name") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Strategy.Name") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="left" />
            </asp:TemplateField>
            <asp:BoundField DataField="MeetingDate" DataFormatString="{0:d}" 
                HeaderText="Meeting Date" SortExpression="MeetingDate" />
            <asp:BoundField DataField="InitialFee" DataFormatString="{0:0.00}" 
                HeaderText="Initial Fee" SortExpression="InitialFee">
            <ItemStyle CssClass="right" />
            </asp:BoundField>
            <asp:BoundField DataField="TimeHorizon" HeaderText="Time Horizon" 
                SortExpression="TimeHorizon">
            <ItemStyle CssClass="right" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Investment Amount" 
                SortExpression="InvestmentAmount">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" 
                        Text='<%# Bind("InvestmentAmount") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" 
                        Text='<%# Eval("InvestmentAmount", "{0:c}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:BoundField DataField="StatusName" HeaderText="Status" 
                SortExpression="HighNetWorth" />
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
        <AlternatingRowStyle CssClass="odd" />
    </asp:GridView>
    <asp:LinqDataSource ID="sourceClient" runat="server" 
        ContextTypeName="RSMTenon.Data.RepGenDataContext" EnableDelete="True" 
        EnableInsert="True" EnableUpdate="True" OrderBy="Name" TableName="Clients">
    </asp:LinqDataSource>
</asp:Content>
