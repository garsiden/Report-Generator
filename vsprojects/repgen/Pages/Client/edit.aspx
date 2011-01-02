<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Pages_Client_edit"
    MasterPageFile="~/MasterPages/SiteMasterPage.master" %>

<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="BDP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:FormView ID="formClient" runat="server" DataKeyNames="GUID" DataSourceID="sourceClient"
        CellPadding="4" ForeColor="#333333" Height="242px" Width="308px" OnItemUpdated="formClient_ItemUpdated"
        EnableModelValidation="True">
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <EditItemTemplate>
            <br />
            <table style="width: 100%;">
                <tr>
                    <td class="style1">
                        Name
                    </td>
                    <td>
                        <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' Width="95%" />
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Meeting Date
                    </td>
                    <td align="left" nowrap="nowrap">
                        <BDP:BDPLite ID="bdpMeetingDate" runat="server" SelectedDate='<%# Bind("MeetingDate") %>'
                            Style="display: inline;" DateFormat="d" />
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Initial Fee %
                    </td>
                    <td>
                        <asp:TextBox ID="InitialFeeTextBox" runat="server" Text='<%# Bind("InitialFee", "{0:N}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Time Horizon
                    </td>
                    <td>
                        <asp:DropDownList ID="listTimeHorizonEdit" runat="server" SelectedValue='<%# Bind("TimeHorizon") %>'
                            Width="60px" Height="16px">
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
                    <td class="style1">
                        Existing Assets
                    </td>
                    <td>
                        <asp:CheckBox ID="ExistingAssetsCheckBox" runat="server" Checked='<%# Bind("ExistingAssets") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Strategy
                    </td>
                    <td>
                        <asp:DropDownList ID="listStrategyEdit" runat="server" DataSource="<%# GetStrategies() %>"
                            DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("StrategyID") %>'>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style1" nowrap="nowrap">
                        Investment Amount
                    </td>
                    <td>
                        <asp:TextBox ID="InvestmentAmountTextBox" runat="server" Text='<%# Bind("InvestmentAmount", "{0:0}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp; Status
                    </td>
                    <td>
                        &nbsp;
                        <asp:RadioButtonList ID="radioListStatusUpdate" runat="server" SelectedIndex='<%# Bind("Status") %>'>
                            <asp:ListItem Selected="True">High Net Worth</asp:ListItem>
                            <asp:ListItem>Affluent</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2" rowspan="1">
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
            <table style="width: 100%;">
                <tr>
                    <td>
                        Name
                    </td>
                    <td>
                        <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Meeting Date
                    </td>
                    <td>
                        <BDP:BDPLite ID="dpMeetingInsert" runat="server" SelectedDate='<%# Bind("MeetingDate") %>'
                            Style="display: inline;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Initial Fee
                    </td>
                    <td>
                        <asp:TextBox ID="InitialFeeTextBox" runat="server" Text='<%# Bind("InitialFee", "{0:N}") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Time Horizon
                    </td>
                    <td>
                        <asp:DropDownList ID="listTimeHorizonInsert" runat="server" SelectedValue='<%# Bind("TimeHorizon") %>'
                            Width="60px">
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
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Existing Assets
                    </td>
                    <td nowrap="nowrap">
                        <asp:CheckBox ID="ExistingAssetsCheckBox" runat="server" Checked='<%# Bind("ExistingAssets") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Strategy
                    </td>
                    <td>
                        <asp:DropDownList ID="listStrategyInsert" runat="server" DataSource="<%# GetStrategies() %>"
                            DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("StrategyID") %>'
                            Width="75%">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td nowrap="nowrap">
                        Investment Amount
                    </td>
                    <td>
                        <asp:TextBox ID="InvestmentAmountTextBox" runat="server" Text='<%# Bind("InvestmentAmount", "{0}") %>' />
                    </td>
                </tr>
            </table>
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                Text="Insert" />&nbsp; &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server"
                    CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </InsertItemTemplate>
        <ItemTemplate>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    Name:
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("Name") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Meeting Date:
                    </td>
                    <td>
                        <asp:Label ID="MeetingDateLabel" runat="server" Text='<%# Bind("MeetingDate", "{0:dd/MM/yyyy}") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Time Horizon:
                    </td>
                    <td>
                        <asp:Label ID="TimeHorizonLabel" runat="server" Text='<%# Bind("TimeHorizon") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Strategy
                    </td>
                    <td>
                        <asp:Label ID="StrategyNameLabel" runat="server" Text='<%# Bind("Strategy.Name") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Existing Assets
                    </td>
                    <td>
                        <asp:CheckBox ID="ExistingAssetsCheckBox" runat="server" Checked='<%# Bind("ExistingAssets") %>'
                            Enabled="False" />
                    </td>
                </tr>
                <tr>
                    <td nowrap="nowrap">
                        Investment Amount
                    </td>
                    <td>
                        <asp:Label ID="InvestmentAmountLabel" runat="server" Text='<%# Bind("InvestmentAmount", "{0:£#,##0}") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Initial Fee %
                    </td>
                    <td>
                        <asp:Label ID="InitialFeeLabel" runat="server" Text='<%# Bind("InitialFee", "{0:N}") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Status
                    </td>
                    <td>
                        <asp:RadioButtonList ID="radioListStatus" runat="server" SelectedIndex='<%# Bind("Status") %>'
                            Enabled="False">
                            <asp:ListItem Selected="True">High Net Worth</asp:ListItem>
                            <asp:ListItem>Affluent</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
            <asp:LinkButton ID="linkEdit" runat="server" CommandName="Edit">Edit</asp:LinkButton>
            <br />
        </ItemTemplate>
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <EmptyDataTemplate>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    Name:
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("Name") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Meeting Date:
                    </td>
                    <td>
                        <asp:Label ID="MeetingDateLabel" runat="server" Text='<%# Bind("MeetingDate", "{0:dd/MM/yyyy}") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Time Horizon:
                    </td>
                    <td>
                        <asp:Label ID="TimeHorizonLabel" runat="server" Text='<%# Bind("TimeHorizon") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Strategy
                    </td>
                    <td>
                        <asp:Label ID="StrategyNameLabel" runat="server" Text='<%# Bind("Strategy.Name") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Existing Assets
                    </td>
                    <td>
                        <asp:CheckBox ID="ExistingAssetsCheckBox" runat="server" Checked='<%# Bind("ExistingAssets") %>'
                            Enabled="False" />
                    </td>
                </tr>
                <tr>
                    <td nowrap="nowrap">
                        Investment Amount
                    </td>
                    <td>
                        <asp:Label ID="InvestmentAmountLabel" runat="server" Text='<%# Bind("InvestmentAmount", "{0:£#,##0}") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Initial Fee %
                    </td>
                    <td>
                        <asp:Label ID="InitialFeeLabel" runat="server" Text='<%# Bind("InitialFee", "{0:N}") %>' />
                    </td>
                </tr>
            </table>
            <asp:LinkButton ID="linkInsert" runat="server" CommandName="New">Add New</asp:LinkButton>
        </EmptyDataTemplate>
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
    </asp:FormView>
    </div>
    <asp:Label ID="ExceptionDetails" runat="server"></asp:Label>
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
