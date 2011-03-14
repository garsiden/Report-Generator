﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SiteHeader.ascx.cs" Inherits="UserControls_SiteHeader" %>
<table style="width: 100%;">
    <tr>
        <td width="50%" rowspan="2">
            <a id="anchorHome" href="~/Default.aspx" target="_parent" runat="server">
                <img id="imgLogo" alt="IntroPro" src="~/Images/RSMTenon.png" border="0" 
                runat="server" /></a>
        </td>
    </tr>
    <tr>
        <td align="right">
            &nbsp;</td>
    </tr>
    <tr>
        <td nowrap="nowrap">
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" 
                Font-Size="Large" Text="The Investment Management Service Report"></asp:Label>
        </td>
        <td align="right" width="50%">
            <asp:Label ID="userLabel" runat="server" style="margin-right:10px"></asp:Label><br />
            <asp:Label ID="roleLabel" runat="server" style="margin-right:10px" Visible="false"></asp:Label>
        </td>
    </tr>
</table>
