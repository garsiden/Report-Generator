<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master" AutoEventWireup="true" CodeFile="new.aspx.cs" Inherits="Pages_Content_new" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h4>Add Content Records For New Strategy</h4>
    <asp:DropDownList ID="listStrategy" runat="server" DataTextField="Name" DataSource='<%# GetStrategiesWithoutContent() %>'
        DataValueField="ID" Width="200px">
    </asp:DropDownList>
<br />
<br />
    <asp:Button ID="buttonAdd" runat="server" Text="Add Records" 
        onclick="buttonAdd_Click" />
<br />
<br />
    <asp:Label ID="labelStatus" runat="server" Text=""></asp:Label>

</asp:Content>

