<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master" AutoEventWireup="true" CodeFile="upload.aspx.cs" Inherits="Pages_Model_upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h4>Upload Model Data</h4>
<br />

<div>
    Model:&nbsp;<asp:DropDownList ID="listModel" runat="server" 
        DataSource="<%# GetStrategies() %>" DataTextField="Name" DataValueField="ID" 
        AppendDataBoundItems="True">
        <asp:ListItem Selected="True" Value="">-- Select Model --</asp:ListItem>
    </asp:DropDownList>
    <br />
<br />
    <asp:FileUpload ID="uploader" runat="server" Width="654px" Height="29px" />
<br />
<br />
    <asp:Button ID="btnUpload" runat="server" Text="Upload" 
        onclick="btnUpload_Click" />
<br />
<br />
   <asp:Label ID="lblStatus" runat="server"></asp:Label>
</div>
 
</asp:Content>

