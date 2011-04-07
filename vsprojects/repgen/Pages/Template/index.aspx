<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Pages_Template_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h4>
        Set Template</h4>
    <br />
    <asp:Label ID="Label1" runat="server" Text="Label">Select template:&nbsp;</asp:Label>
    <asp:DropDownList ID="dropList" runat="server" DataSource="<%# GetTemplates() %>"
        DataTextField="Name" DataValueField="Name" Width="165px">
    </asp:DropDownList>
    <br />
    <br />
    <asp:Button ID="btnSetTemplate" runat="server" Text="Set Template" OnClick="btnSetTemplate_Click" />
    <br />
    <br />
    <asp:Label ID="lblStatus" runat="server"></asp:Label>
    </div>
</asp:Content>
