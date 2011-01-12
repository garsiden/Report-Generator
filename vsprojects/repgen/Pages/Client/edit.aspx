<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Pages_Client_edit"
    MasterPageFile="~/MasterPages/SiteMasterPage.master" %>

<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="BDP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h4 id="clientHeader" runat="server"></h4>
<br />
    <asp:FormView ID="formClient" runat="server" DataKeyNames="GUID" DataSourceID="sourceClient"
        OnItemUpdated="formClient_ItemUpdated" EnableModelValidation="True" OnDataBound="formClient_DataBound">
        <EditItemTemplate>
            <table class="listing">
                <tr>
                    <td class="left">
                        Name
                    </td>
                    <td class="lnowrap">
                        <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' Width="95%" />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Meeting Date
                    </td>
                    <td class="lnowrap">
                        <BDP:BDPLite ID="bdpMeetingDate" runat="server" SelectedDate='<%# Bind("MeetingDate") %>'
                            Style="display: inline;" DateFormat="d" />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Initial Fee %
                    </td>
                    <td class="left">
                        <asp:TextBox ID="InitialFeeTextBox" runat="server" Text='<%# Bind("InitialFee", "{0:N}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Time Horizon
                    </td>
                    <td class="left">
                        <asp:DropDownList ID="listTimeHorizonEdit" runat="server" SelectedValue='<%# Bind("TimeHorizon") %>'>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                            <asp:ListItem>13</asp:ListItem>
                            <asp:ListItem>14</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                            <asp:ListItem>16</asp:ListItem>
                            <asp:ListItem>17</asp:ListItem>
                            <asp:ListItem>19</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Existing Assets
                    </td>
                    <td class="left">
                        <asp:CheckBox ID="ExistingAssetsCheckBox" runat="server" Checked='<%# Bind("ExistingAssets") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Strategy
                    </td>
                    <td class="left">
                        <asp:DropDownList ID="listStrategyEdit" runat="server" DataSource="<%# GetStrategies() %>"
                            DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("StrategyID") %>'>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Investment Amount
                    </td>
                    <td class="left">
                        <asp:TextBox ID="InvestmentAmountTextBox" runat="server" Text='<%# Bind("InvestmentAmount", "{0:0}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Status
                    </td>
                    <td class="left">
                        <asp:RadioButtonList ID="radioListStatusUpdate" runat="server" SelectedIndex='<%# Bind("Status") %>'
                            BorderStyle="None" RepeatLayout="Flow">
                            <asp:ListItem Selected="True">High Net Worth</asp:ListItem>
                            <asp:ListItem>Affluent</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="right" colspan="2" rowspan="1">
                        <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                            Text="Update" />&nbsp;
                        <asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text="Cancel" />
                    </td>
                </tr>
            </table>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                HeaderText="Input errors:" />
            <asp:RequiredFieldValidator ID="vaildNameRequired" runat="server" ControlToValidate="NameTextBox"
                ErrorMessage="Name required" Display="None"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="vaildRangeInitialFee" runat="server" ControlToValidate="InitialFeeTextBox"
                ErrorMessage="Initial Fee must be between 0 and 5" MaximumValue="5" MinimumValue="0"
                Type="Double" Display="None"></asp:RangeValidator>
            <asp:CompareValidator ID="validCompareInvestmentAmount" runat="server" ErrorMessage="Investment Amount must be greater than 0"
                ValueToCompare="0" ControlToValidate="InvestmentAmountTextBox" Display="None"
                Operator="GreaterThan" Type="Integer"></asp:CompareValidator>
        </EditItemTemplate>
        <InsertItemTemplate>
            <table class="listing">
                <tr>
                    <td class="lnowrap">
                        Name
                    </td>
                    <td class="lnowrap">
                        <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Meeting Date
                    </td>
                    <td class="lnowrap">
                        <BDP:BDPLite ID="dpMeetingInsert" runat="server" SelectedDate='<%# Bind("MeetingDate") %>'
                            Style="display: inline;" />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Initial Fee
                    </td>
                    <td class="lnowrap">
                        <asp:TextBox ID="InitialFeeTextBox" runat="server" Text='<%# Bind("InitialFee", "{0:N}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Time Horizon
                    </td>
                    <td class="lnowrap">
                        <asp:DropDownList ID="listTimeHorizonInsert" runat="server" SelectedValue='<%# Bind("TimeHorizon") %>'>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                            <asp:ListItem>13</asp:ListItem>
                            <asp:ListItem>14</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                            <asp:ListItem>16</asp:ListItem>
                            <asp:ListItem>17</asp:ListItem>
                            <asp:ListItem>19</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Existing Assets
                    </td>
                    <td class="lnowrap">
                        <asp:CheckBox ID="ExistingAssetsCheckBox" runat="server" Checked='<%# Bind("ExistingAssets") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Strategy
                    </td>
                    <td class="lnowrap">
                        <asp:DropDownList ID="listStrategyInsert" runat="server" DataSource="<%# GetStrategies() %>"
                            DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("StrategyID") %>'
                            Width="75%">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Investment Amount
                    </td>
                    <td class="lnowrap">
                        <asp:TextBox ID="InvestmentAmountTextBox" runat="server" Text='<%# Bind("InvestmentAmount", "{0}") %>' />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="right">
                        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                            Text="Insert" />&nbsp; &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server"
                                CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                    </td>
                </tr>
            </table>
        </InsertItemTemplate>
        <ItemTemplate>
            <table class="listing">
                <tr>
                    <td class="lnowrap">
                        Name
                    </td>
                    </td>
                    <td class="lnowrap">
                        <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("Name") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Meeting Date
                    </td>
                    <td class="lnowrap">
                        <asp:Label ID="MeetingDateLabel" runat="server" Text='<%# Bind("MeetingDate", "{0:dd/MM/yyyy}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Time Horizon
                    </td>
                    <td class="lnowrap">
                        <asp:Label ID="TimeHorizonLabel" runat="server" Text='<%# Eval("TimeHorizon") + " years(s)" %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Strategy
                    </td>
                    <td class="lnowrap">
                        <asp:Label ID="StrategyNameLabel" runat="server" Text='<%# Bind("Strategy.Name") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Existing Assets
                    </td>
                    <td class="lnowrap">
                        <asp:CheckBox ID="ExistingAssetsCheckBox" runat="server" Checked='<%# Bind("ExistingAssets") %>'
                            Enabled="False" />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Investment Amount
                    </td>
                    <td class="lnowrap">
                        <asp:Label ID="InvestmentAmountLabel" runat="server" Text='<%# Bind("InvestmentAmount", "{0:�#,##0}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Initial Fee %
                    </td>
                    <td class="lnowrap">
                        <asp:Label ID="InitialFeeLabel" runat="server" Text='<%# Bind("InitialFee", "{0:N}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Status
                    </td>
                    <td class="lnowrap">
                        <asp:RadioButtonList ID="radioListStatus" runat="server" SelectedIndex='<%# Bind("Status") %>'
                            Enabled="False" RepeatLayout="Flow">
                            <asp:ListItem Selected="True">High Net Worth</asp:ListItem>
                            <asp:ListItem>Affluent</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="right" colspan="2">
                        <asp:LinkButton ID="linkEdit" runat="server" CommandName="Edit">Edit</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
        <EmptyDataTemplate>
            <table class="listing">
                <tr>
                    <td class="lnowrap">
                        Name
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Meeting Date
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Time Horizon
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Strategy
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Existing Assets
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Investment Amount
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Initial Fee %
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="right">
                        <asp:LinkButton ID="linkInsert" runat="server" CommandName="New">Add New</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </asp:FormView>
    <br />
    <asp:HyperLink ID="hyperAsset" runat="server" NavigateUrl="~/Pages/Client/assets.aspx">Add/amend client assets by investment</asp:HyperLink>
    <br />
    <asp:HyperLink ID="hyperClass" runat="server" NavigateUrl="~/Pages/Client/assetclasses.aspx">Add/amend client assets by class</asp:HyperLink>
    <br />
    <br />
    <asp:Button ID="btnCreateReport" runat="server" OnClick="btnCreateReport_Click" Text="Create Report" />
    <br />
    <br />
    <asp:Label ID="ExceptionDetails" runat="server"></asp:Label>
    <br />
    <br />
    <asp:ObjectDataSource ID="sourceClient" runat="server" ConflictDetection="CompareAllValues"
        DataObjectTypeName="RSMTenon.Data.Client" DeleteMethod="DeleteClient" InsertMethod="InsertClient"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetClientByGUID"
        TypeName="RSMTenon.Data.Client" UpdateMethod="UpdateClient">
        <UpdateParameters>
            <asp:Parameter Name="client" Type="Object" />
            <asp:Parameter Name="original_client" Type="Object" />
        </UpdateParameters>
        <SelectParameters>
            <asp:QueryStringParameter DbType="Guid" DefaultValue="null" Name="clientGuid" QueryStringField="guid" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
