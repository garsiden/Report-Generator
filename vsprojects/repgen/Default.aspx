<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default"
    MasterPageFile="~/MasterPages/SiteMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="gridClient" runat="server" AutoGenerateColumns="False" DataKeyNames="GUID"
        DataSourceID="sourceClient" CssClass="listing">
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="GUID" DataNavigateUrlFormatString="~/Pages/Client/edit.aspx?guid={0}"
                DataTextField="Name" HeaderText="Client Name" SortExpression="Name" >
            <ItemStyle CssClass="lnowrap" />
            </asp:HyperLinkField>
            <asp:BoundField DataField="MeetingDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Meeting Date"
                SortExpression="MeetingDate" />
            <asp:CommandField ShowDeleteButton="True" >
            <ItemStyle CssClass="left" />
            </asp:CommandField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="sourceClient" runat="server" DataObjectTypeName="RSMTenon.Data.Client"
        DeleteMethod="DeleteClient" SelectMethod="GetRecentClients" TypeName="RSMTenon.Data.Client"
        InsertMethod="InsertClient" OldValuesParameterFormatString="original_{0}" UpdateMethod="UpdateClient">
        <UpdateParameters>
            <asp:Parameter Name="client" Type="Object" />
            <asp:Parameter Name="original_client" Type="Object" />
        </UpdateParameters>
        <SelectParameters>
            <asp:Parameter DefaultValue="10" Name="number" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
