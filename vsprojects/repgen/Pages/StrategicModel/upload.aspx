<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="upload.aspx.cs" Inherits="Pages_StrategicModel_upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h4>
        Upload Strategic Model Data</h4>
    <br />
    <div>
        Strategic Model:&nbsp;<asp:DropDownList ID="listModel" runat="server" DataSource="<%# GetStrategies() %>"
            DataTextField="Name" DataValueField="ID" AppendDataBoundItems="True">
            <asp:ListItem Selected="True" Value="">-- Select Strategic Model --</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:FileUpload ID="uploader" runat="server" Width="654px" Height="29px" />
        <br />
        <br />
        <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
        <br />
        <br />
        <asp:Label ID="lblStatus" runat="server"></asp:Label>
    </div>
</asp:Content>
