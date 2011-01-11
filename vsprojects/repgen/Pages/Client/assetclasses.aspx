<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="assetclasses.aspx.cs" Inherits="Pages_Client_assetclasses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:FormView ID="formView" runat="server" DataKeyNames="ClientGUID" Height="351px"
        Width="375px" OnItemCommand="formView_ItemCommand" 
        DataSourceID="sourceAssetObject">
        <EditItemTemplate>
            <table class="listing">
                <tr>
                    <td class="lnowrap">
                        <%# GetAssetClassName("CASH") %>
                        </td>
                    <td class="right">
                        <asp:TextBox ID="CASHTextBox" runat="server" Text='<%# Bind("CASH", "{0:0.000}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        <%# GetAssetClassName("COMM") %>
                    </td>
                    <td class="right">
                        <asp:TextBox ID="COMMTextBox" runat="server" Text='<%# Bind("COMM", "{0:0.000}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        <%# GetAssetClassName("COPR")%>
                    </td>
                    <td class="right">
                        <asp:TextBox ID="COPRTextBox" runat="server" Text='<%# Bind("COPR", "{0:0.000}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        <%# GetAssetClassName("GLEQ")%></td>
                    <td class="right">
                        <asp:TextBox ID="GLEQTextBox" runat="server" Text='<%# Bind("GLEQ", "{0:0.000}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        <%# GetAssetClassName("HEDG")%> 
                    </td>
                    <td class="right">
                        <asp:TextBox ID="HEDGTextBox" runat="server" Text='<%# Bind("HEDG", "{0:0.000}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        <%# GetAssetClassName("LOSH")%>
                    </td>
                    <td class="right">
                        <asp:TextBox ID="LOSHTextBox" runat="server" Text='<%# Bind("LOSH", "{0:0.000}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        <%# GetAssetClassName("PREQ")%> 
                    </td>
                    <td class="right">
                        <asp:TextBox ID="PREQTextBox" runat="server" Text='<%# Bind("PREQ", "{0:0.000}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        <%# GetAssetClassName("UKCB")%> 
                    </td>
                    <td class="right">
                        <asp:TextBox ID="UKCBTextBox" runat="server" Text='<%# Bind("UKCB", "{0:0.000}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        <%# GetAssetClassName("UKEQ")%> 
                    </td>
                    <td class="right">
                        <asp:TextBox ID="UKEQTextBox" runat="server" Text='<%# Bind("UKEQ", "{0:0.000}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        <%# GetAssetClassName("UKGB")%> 
                    </td>
                    <td class="right">
                        <asp:TextBox ID="UKGBTextBox" runat="server" Text='<%# Bind("UKGB", "{0:0.000}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        <%# GetAssetClassName("UKHY")%> 
                    </td>
                    <td class="right">
                        <asp:TextBox ID="UKHYTextBox" runat="server" Text='<%# Bind("UKHY", "{0:0.000}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        <%# GetAssetClassName("WOBO")%> 
                    </td>
                    <td class="right">
                        <asp:TextBox ID="WOBOTextBox" runat="server" Text='<%# Bind("WOBO", "{0:0.000}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        
                        <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                            CommandName="Update" Text="Update" />
                        <asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" 
                            CommandName="Cancel" Text="Cancel" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </EditItemTemplate>
        <InsertItemTemplate>
            <table class="listing"
                <tr>
                    <td><%# GetAssetClassName("CASH") %>
                        </td>
                    <td>
                        <asp:TextBox ID="CASHTextBox" runat="server" Text='<%# Bind("CASH") %>' />
                    </td>
                </tr>
                <tr>
                    <td><%# GetAssetClassName("COMM") %>
                        </td>
                    <td>
                        <asp:TextBox ID="COMMTextBox" runat="server" Text='<%# Bind("COMM") %>' />
                    </td>
                </tr>
                <tr>
                    <td><%# GetAssetClassName("COPR") %>
                        </td>
                    <td>
                        <asp:TextBox ID="COPRTextBox" runat="server" Text='<%# Bind("COPR") %>' />
                    </td>
                </tr>
                <tr>
                    <td><%# GetAssetClassName("GLEQ") %>
                        </td>
                    <td>
                        <asp:TextBox ID="GLEQTextBox" runat="server" Text='<%# Bind("GLEQ") %>' />
                    </td>
                </tr>
                <tr><%# GetAssetClassName("HEDG") %>
                    <td>
                        </td>
                    <td>
                        <asp:TextBox ID="HEDGTextBox" runat="server" Text='<%# Bind("HEDG") %>' />
                    </td>
                </tr>
                <tr>
                    <td><%# GetAssetClassName("LOSH") %>
                        </td>
                    <td>
                        <asp:TextBox ID="LOSHTextBox" runat="server" Text='<%# Bind("LOSH") %>' />
                    </td>
                </tr>
                <tr>
                    <td><%# GetAssetClassName("PREQ") %>
                        </td>
                    <td>
                        <asp:TextBox ID="PREQTextBox" runat="server" Text='<%# Bind("PREQ") %>' />
                    </td>
                </tr>
                <tr>
                    <td><%# GetAssetClassName("UKCB") %>
                        </td>
                    <td>
                        <asp:TextBox ID="UKCBTextBox" runat="server" Text='<%# Bind("UKCB") %>' />
                    </td>
                </tr>
                <tr>
                    <td><%# GetAssetClassName("UKEQ") %>
                        </td>
                    <td>
                        <asp:TextBox ID="UKEQTextBox" runat="server" Text='<%# Bind("UKEQ") %>' />
                    </td>
                </tr>
                <tr>
                    <td><%# GetAssetClassName("UKGB") %>
                        </td>
                    <td>
                        <asp:TextBox ID="UKGBTextBox" runat="server" Text='<%# Bind("UKGB") %>' />
                    </td>
                </tr>
                <tr>
                    <td><%# GetAssetClassName("UKHY") %>
                        </td>
                    <td>
                        <asp:TextBox ID="UKHYTextBox" runat="server" Text='<%# Bind("UKHY") %>' />
                    </td>
                </tr>
                <tr>
                    <td><%# GetAssetClassName("WOBO") %>
                        </td>
                    <td>
                        <asp:TextBox ID="WOBOTextBox" runat="server" Text='<%# Bind("WOBO") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                            CommandName="Insert" Text="Insert" />
                        <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" 
                            CommandName="Cancel" Text="Cancel" />
                    </td>
                    <td>
                        </td>
                </tr>
            </table>
        </InsertItemTemplate>
        <ItemTemplate>
            <table style="width: 100%;" class="listing">
                <tr>
                    <td class="lnowrap">
                        <%# GetAssetClassName("CASH") %>
                    </td>
                    <td class="right">
                        <asp:Label ID="CASHLabel" runat="server" Text='<%# Eval("CASH", "{0:0.0%}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        <%# GetAssetClassName("COMM") %>
                    </td>
                    <td class="right">
                        <asp:Label ID="COMMLabel" runat="server" Text='<%# Eval("COMM", "{0:0.0%}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        <%# GetAssetClassName("COPR") %>
                    </td>
                    <td class="right">
                        <asp:Label ID="COPRLabel" runat="server" Text='<%# Eval("COPR", "{0:0.0%}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        <%# GetAssetClassName("GLEQ") %>
                    </td>
                    <td class="right">
                        <asp:Label ID="GLEQLabel" runat="server" Text='<%# Eval("GLEQ", "{0:0.0%}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        <%# GetAssetClassName("HEDG") %>
                    </td>
                    <td class="right">
                        <asp:Label ID="HEDGLabel" runat="server" Text='<%# Eval("HEDG", "{0:0.0%}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        <%# GetAssetClassName("LOSH") %>
                    </td>
                    <td class="right">
                        <asp:Label ID="LOSHLabel" runat="server" Text='<%# Eval("LOSH", "{0:0.0%}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        <%# GetAssetClassName("PREQ") %>
                    </td>
                    <td class="right">
                        <asp:Label ID="PREQLabel" runat="server" Text='<%# Eval("PREQ", "{0:0.0%}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        <%# GetAssetClassName("UKCB") %>
                    </td>
                    <td class="right">
                        <asp:Label ID="UKCBLabel" runat="server" Text='<%# Eval("UKCB", "{0:0.0%}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        <%# GetAssetClassName("UKEQ") %>
                    </td>
                    <td class="right">
                        <asp:Label ID="UKEQLabel" runat="server" Text='<%# Eval("UKEQ", "{0:0.0%}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        <%# GetAssetClassName("UKGB") %>
                    </td>
                    <td class="right">
                        <asp:Label ID="UKGBLabel" runat="server" Text='<%#Eval("UKGB", "{0:0.0%}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        <%# GetAssetClassName("UKHY") %>
                    </td>
                    <td class="right">
                        <asp:Label ID="UKHYLabel" runat="server" Text='<%# Eval("UKHY", "{0:0.0%}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        <%# GetAssetClassName("WOBO") %>
                    </td>
                    <td class="right">
                        <asp:Label ID="WOBOLabel" runat="server" Text='<%# Eval("WOBO", "{0:0.0%}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap" style="width: 156px">
                        <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                            Text="Edit" />
                        <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                            Text="Delete" />
                    </td>
                    <td class="lnowrap">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <br />
            <br />
            
        </ItemTemplate>
        <EmptyDataTemplate>
            <table style="width: 100%;">
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
                            Text="New" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </asp:FormView>
    <asp:LinqDataSource ID="sourceAssets" runat="server" ContextTypeName="RSMTenon.Data.RepGenDataContext"
        EnableDelete="True" EnableInsert="True" EnableUpdate="True" TableName="ClientAssetClasses"
        Where="ClientGUID == @ClientGUID" OnInserting="sourceAssets_Inserting">
        <WhereParameters>
            <asp:QueryStringParameter DbType="Guid" DefaultValue="636C8103-E06D-4575-AAFC-574474C2D7F8"
                Name="ClientGUID" QueryStringField="guid" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:ObjectDataSource ID="sourceAssetObject" runat="server" DataObjectTypeName="RSMTenon.Data.ClientAssetClass"
        DeleteMethod="DeleteClientAsset" InsertMethod="InsertClient" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetClientAssetClass" TypeName="RSMTenon.Data.ClientAssetClass"
        UpdateMethod="UpdateClientAsset" OnInserting="sourceAssetObject_Inserting"
        OnObjectCreated="sourceAssetObject_ObjectCreated" 
        >
        <UpdateParameters>
            <asp:Parameter Name="clientAsset" Type="Object" />
            <asp:Parameter Name="original_clientAsset" Type="Object" />
        </UpdateParameters>
        <SelectParameters>
            <asp:QueryStringParameter DbType="Guid" Name="clientGuid" QueryStringField="guid" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
