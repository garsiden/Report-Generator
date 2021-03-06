﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="strategy.aspx.cs" Inherits="Pages_Content_strategy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h4>
        Add Content Records For New Strategy</h4>
    <br />
    <asp:DropDownList ID="listStrategy" runat="server" DataTextField="Name" DataSource='<%# GetStrategiesWithoutContent() %>'
        DataValueField="ID" Width="200px">
    </asp:DropDownList>
    <br />
    <br />
    <asp:Button ID="buttonAdd" runat="server" Text="Add Records" OnClick="buttonAdd_Click" />
    <br />
    <br />
    <asp:Label ID="labelStatus" runat="server" Text=""></asp:Label>
</asp:Content>
