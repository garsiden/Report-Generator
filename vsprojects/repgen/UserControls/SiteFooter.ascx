<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SiteFooter.ascx.cs" Inherits="UserControls_SiteFooter" %>
<table style="width: 100%;">
    <tr>
        <td width="5%">
            &nbsp;
        </td>
        <td align="center">
            <em>Copyright © 2010 RSM Tenon</em><br />
            <asp:HyperLink ID="homeLink" runat="server" NavigateUrl="~/Default.aspx">Home</asp:HyperLink>|
            Support | FAQ | Administration
<%--            <asp:LinkButton ID="logoutLinkBtn" runat="server" CommandName="Logout" CausesValidation="False">Log out</asp:LinkButton>--%>
        </td>
        <td width="5%">
            &nbsp;
        </td>
    </tr>
</table>
