﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master" AutoEventWireup="true" CodeFile="upload.aspx.cs" Inherits="Pages_AssetClass_upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h4>Upload Asset Class Prices</h4>
<br />
<div>
    <asp:FileUpload ID="uploader" runat="server" Width="100%" Height="29px" />
<br />
<br />
    <asp:Button ID="btnUpload" runat="server" Text="Upload" 
        onclick="btnUpload_Click" />
<br />
<br />
   <asp:Label ID="lblStatus" runat="server"></asp:Label>
</div>
 
</asp:Content>

