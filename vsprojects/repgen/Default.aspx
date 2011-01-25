<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default"
    MasterPageFile="~/MasterPages/SiteMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="gridClient" runat="server" AutoGenerateColumns="False" DataKeyNames="GUID"
        DataSourceID="sourceClient" CssClass="listing" Caption="Recent Clients" 
        AllowSorting="True"  Width="60%" 
        onselectedindexchanged="gridClient_SelectedIndexChanged">
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="GUID" DataNavigateUrlFormatString="~/Pages/Client/edit.aspx?guid={0}"
                DataTextField="Name" HeaderText="Client Name" SortExpression="Name" >
            <ItemStyle CssClass="lnowrap" />
            </asp:HyperLinkField>
            <asp:BoundField DataField="MeetingDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Meeting Date"
                SortExpression="MeetingDate" >
            <HeaderStyle Wrap="False" />
            <ItemStyle Width="10%" />
            </asp:BoundField>
            <asp:CommandField ShowDeleteButton="True" >
            <ItemStyle CssClass="left" Width="5%" />
            </asp:CommandField>
            <asp:CommandField SelectText="Report" ShowSelectButton="True">
            <ItemStyle CssClass="left" Width="5%" />
            </asp:CommandField>
        </Columns>
        <EmptyDataTemplate>
            No clients to display.
        </EmptyDataTemplate>
    </asp:GridView>
        <asp:LinqDataSource ID="sourceClient" runat="server" 
            ContextTypeName="RSMTenon.Data.RepGenDataContext" 
            OrderBy="MeetingDate desc, Name" TableName="Clients" 
        EnableDelete="True" onselecting="sourceClient_Selecting">
        </asp:LinqDataSource>

    <br />
    <div align="right" style="width:60%">    
        <asp:HyperLink ID="hyperNewClient" runat="server" NavigateUrl="Pages/Client/new.aspx">Add New Client</asp:HyperLink></div>

</asp:Content>
