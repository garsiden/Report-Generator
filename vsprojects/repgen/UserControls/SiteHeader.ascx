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
        <td>
        </td>
        <td align="right" width="50%">
            <asp:Label ID="greetingLabel" runat="server"></asp:Label>
        </td>
    </tr>
</table>
