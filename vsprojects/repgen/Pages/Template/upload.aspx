<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master" AutoEventWireup="true" CodeFile="upload.aspx.cs" Inherits="Pages_Template_upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h4>Upload Template</h4>
<br />
<div>
    <asp:FileUpload ID="uploader" runat="server" Width="476px" />
<br />
<br />
    <asp:Button ID="btnUpload" runat="server" Text="Upload" 
        onclick="btnUpload_Click" />
<br />
<br />
   <asp:Label ID="lblStatus" runat="server"></asp:Label>
</div>
</asp:Content>

