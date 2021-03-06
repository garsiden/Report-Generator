﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default"
    MasterPageFile="~/MasterPages/SiteMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<br />
<br />
    <asp:GridView ID="gridClient" runat="server" AutoGenerateColumns="False" DataKeyNames="GUID"
        DataSourceID="sourceClient" CssClass="listing" Caption="Recent Clients" AllowSorting="True"
        Width="60%" OnSelectedIndexChanged="gridClient_SelectedIndexChanged">
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="GUID" DataNavigateUrlFormatString="~/Pages/Client/edit.aspx?guid={0}"
                DataTextField="Name" HeaderText="Client Name" SortExpression="Name">
                <ItemStyle CssClass="lnowrap" />
            </asp:HyperLinkField>
            <asp:BoundField DataField="DateIssued" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date Issued"
                SortExpression="DateIssued" ItemStyle-CssClass="center">
                <HeaderStyle Wrap="False" />
                <ItemStyle Width="10%" />
            </asp:BoundField>
            <asp:CommandField ShowDeleteButton="True">
                <ItemStyle CssClass="left" Width="5%" />
            </asp:CommandField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="linkReport" runat="server" CausesValidation="False" CommandName="Select"
                        Text="Report" OnClientClick="hideException('labelException');"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle CssClass="left" Width="5%" />
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            No clients to display.
        </EmptyDataTemplate>
    </asp:GridView>
    <br />
    <br />
    <div align="right" style="width: 60%">
        <asp:HyperLink ID="hyperNewClient" runat="server" NavigateUrl="Pages/Client/new.aspx">New Client</asp:HyperLink></div>
    <asp:Label ID="labelException" class="errortext" runat="server" Visible="False"></asp:Label>
    <asp:LinqDataSource ID="sourceClient" runat="server" ContextTypeName="RSMTenon.Data.RepGenDataContext"
        OrderBy="DateIssued desc, Name" TableName="Clients" EnableDelete="True" OnSelecting="sourceClient_Selecting">
    </asp:LinqDataSource>
</asp:Content>
