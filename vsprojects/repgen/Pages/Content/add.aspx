<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="new.aspx.cs" Inherits="Pages_Content_new" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h4>
        Add New Text Content</h4>
    <br />
    <asp:FormView ID="formView" runat="server" DataKeyNames="GUID" DataSourceID="linqContent"
        OnItemInserting="formView_ItemInserting" Width="75%" 
        oniteminserted="formView_ItemInserted">
        <EditItemTemplate >
            ContentID:
            <asp:TextBox ID="ContentIDTextBox" runat="server" Text='<%# Bind("ContentID") %>' />
            <br />
            Category:
            <asp:TextBox ID="CategoryTextBox" runat="server" Text='<%# Bind("Category") %>' />
            <br />
            StrategyID:
            <asp:TextBox ID="StrategyIDTextBox" runat="server" Text='<%# Bind("StrategyID") %>' />
            <br />
            Text:
            <asp:TextBox ID="TextTextBox" runat="server" Text='<%# Bind("Text") %>' />
            <br />
            Strategy:
            <asp:TextBox ID="StrategyTextBox" runat="server" Text='<%# Bind("Strategy") %>' />
            <br />
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                Text="Update" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False"
                CommandName="Cancel" Text="Cancel" />
        </EditItemTemplate>
        <InsertItemTemplate>
            <table class="listing">
                <tr class="odd">
                    <td class="lnowrap" style="width: 20%">
                        ContentID
                    </td>
                    <td class="lnowrap">
                        <asp:TextBox ID="ContentIDTextBox" runat="server" Text='<%# Bind("ContentID") %>'
                            Width="65%" />
                        <asp:RequiredFieldValidator ID="validRequiredContentId" runat="server" ErrorMessage="Please enter a ContentID"
                            ControlToValidate="ContentIDTextBox" Display="None"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="even">
                    <td class="lnowrap">
                        Category
                    </td>
                    <td class="lnowrap">
                        <asp:DropDownList ID="listCategory" runat="server" SelectedValue='<%# Bind("Category") %>' Width="50%" >
                            <asp:ListItem Value="0"> -- Select Category -- </asp:ListItem>
                            <asp:ListItem Value="None">None</asp:ListItem>
                            <asp:ListItem Value="existing-assets">existing-assets</asp:ListItem>
                            <asp:ListItem Value="no-existing-assets">no-existing-assets</asp:ListItem>
                        </asp:DropDownList>
                        <asp:CompareValidator ID="validCompareCategory" runat="server" ErrorMessage="Please select a Category or None from the list."
                            ValueToCompare="0" Operator="NotEqual" ControlToValidate="listCategory" Display="None"></asp:CompareValidator>

                    </td>
                </tr>
                <tr class="odd">
                    <td class="lnowrap">
                        Strategy
                    </td>
                    <td class="lnowrap">
                        <asp:DropDownList ID="listStrategy" runat="server" DataSource='<%# GetStrategies() %>'
                            DataTextField="Name" AppendDataBoundItems="True" DataValueField="ID" SelectedValue='<%# Bind("StrategyID") %>'
                            Width="40%">
                            <asp:ListItem Value="0"> -- Select Strategy -- </asp:ListItem>
                            <asp:ListItem Value="None">None</asp:ListItem>
                        </asp:DropDownList>
                        <asp:CompareValidator ID="validCompareStrategy" runat="server" ErrorMessage="Please select a Strategy Name or None from the list."
                            ValueToCompare="0" Operator="NotEqual" ControlToValidate="listStrategy" Display="None"></asp:CompareValidator>
                    </td>
                </tr>
                <tr class="even">
                    <td class="lnowrap">
                        Text
                    </td>
                    <td class="lnowrap">
                        <asp:TextBox ID="TextTextBox" runat="server" Text='<%# Bind("Text") %>' Rows="8"
                            TextMode="MultiLine" Width="95%" />
                        <asp:RequiredFieldValidator ID="validRequiredText" runat="server" ErrorMessage="Please enter Text Content."
                            Display="None" ControlToValidate="TextTextBox"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
            <br />
            <div align="right">
                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Insert"
                    Text="Insert" />&nbsp;
                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                    Text="Cancel" />
            </div>
        </InsertItemTemplate>
        <ItemTemplate>
            ContentID:
            <asp:Label ID="ContentIDLabel" runat="server" Text='<%# Bind("ContentID") %>' />
            <br />
            Category:
            <asp:Label ID="CategoryLabel" runat="server" Text='<%# Bind("Category") %>' />
            <br />
            StrategyID:
            <asp:Label ID="StrategyIDLabel" runat="server" Text='<%# Bind("StrategyID") %>' />
            <br />
            Text:
            <asp:Label ID="TextLabel" runat="server" Text='<%# Bind("Text") %>' />
            <br />
            <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                Text="Delete" />
            &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
                Text="New" />
        </ItemTemplate>
        <EmptyDataTemplate>
            <table class="listing">
                <tr class="odd">
                    <td class="lnowrap" style="width: 20%">
                        ContentID&nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr class="even">
                    <td class="lnowrap">
                        Category&nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr class="odd">
                    <td class="lnowrap">
                        Strategy&nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr class="even">
                    <td class="lnowrap" style="height:150px">
                        Text&nbsp;
                    </td>
                    <td height="150px">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <div align="right">
                <asp:LinkButton ID="NewButton0" runat="server" CausesValidation="False" CommandName="New"
                    Text="New" />
            </div>
        </EmptyDataTemplate>
    </asp:FormView>
            <br />
            <br />
            <asp:ValidationSummary ID="validSummary" runat="server" />
<br/>
    <asp:Label ID="labelException" runat="server" Text=""></asp:Label>


    <asp:LinqDataSource ID="linqContent" runat="server" 
        ContextTypeName="RSMTenon.Data.RepGenDataContext" EnableInsert="True" 
        TableName="Contents" Where="ContentID == @ContentID">
        <WhereParameters>
            <asp:Parameter DefaultValue="false" Name="ContentID" Type="String" />
        </WhereParameters>
    </asp:LinqDataSource>
</asp:Content>
