﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="new.aspx.cs" Inherits="Pages_Client_new"
    MasterPageFile="~/MasterPages/SiteMasterPage.master" %>

<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="BDP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4>
        Add New Client</h4>
    <asp:FormView ID="formView" runat="server" DataKeyNames="GUID" DataSourceID="sourceClient">
        <EditItemTemplate>
            ReportingFrequency:
            <br />
            InitialFeeAmount: GUID:
            <br />
            Name:
            <br />
            MeetingDate:
            <br />
            InitialFee:
            <br />
            TimeHorizon:
            <br />
            ExistingAssets:
            <br />
            StrategyID:
            <br />
            InvestmentAmount:
            <br />
            SSMA_TimeStamp:
            <br />
            ClientAssets:
            <br />
            ClientAssetClasses:
            <br />
            Strategy:
            <br />
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                Text="Update" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False"
                CommandName="Cancel" Text="Cancel" />
        </EditItemTemplate>
        <InsertItemTemplate>
            <table class="listing">
                <tr class="odd">
                    <td class="lnowrap">
                        Name
                    </td>
                    <td class="lnowrap">
                        <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' Width="100%" />
                    </td>
                </tr>
                <tr class="even">
                    <td class="lnowrap">
                        Meeting Date
                    </td>
                    <td class="lnowrap">
                        <BDP:BDPLite ID="bdpMeetingDate" runat="server" SelectedDate='<%# Bind("MeetingDate") %>'
                            Style="display: inline;" />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Time Horizon
                    </td>
                    <td class="lnowrap">
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
                <tr class="even">
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
                        <asp:DropDownList ID="listStrategy" runat="server" DataSource="<%# GetStrategies() %>"
                            DataTextField="Name" DataValueField="ID" DataMember="StrategyID" SelectedValue='<%# Bind("StrategyID") %>'
                            AppendDataBoundItems="True">
                            <asp:ListItem Value="XX">-- Select Strategy --</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="even">
                    <td class="lnowrap">
                        Investment Amount
                    </td>
                    <td class="lnowrap">
                        <asp:TextBox ID="InvestmentAmountTextBox" runat="server" Text='<%# Bind("InvestmentAmount") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Initial Fee
                    </td>
                    <td class="lnowrap">
                        <asp:TextBox ID="InitialFeeTextBox" runat="server" Text='<%# Bind("InitialFee") %>' />
                    </td>
                </tr>
                <tr class="even">
                    <td class="lnowrap">
                        Status
                    </td>
                    <td class="lnowrap">
                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" SelectedIndex='<%# Bind("Status") %>'
                            RepeatLayout="Flow">
                            <asp:ListItem Selected="True">High Net Worth</asp:ListItem>
                            <asp:ListItem>Affluent</asp:ListItem>
                        </asp:RadioButtonList>
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
            <br />
            <br />
            <asp:ValidationSummary ID="validSummary" runat="server" />
            <asp:CompareValidator ID="validCompareStrategyID" runat="server" ControlToValidate="listStrategy"
                Display="None" ErrorMessage="Please select a Strategy from the list" Operator="NotEqual"
                ValueToCompare="XX"></asp:CompareValidator>
        </InsertItemTemplate>
        <ItemTemplate>
            <table class="listing">
                <tr class="odd">
                    <td class="lnowrap">
                        Name
                    </td>
                    <td>
                        <asp:Label ID="NameLabel" runat="server" />
                    </td>
                </tr>
                <tr class="even">
                    <td class="lnowrap">
                        Meeting Date
                    </td>
                    <td>
                        <asp:Label ID="MeetingDateLabel" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Time Horizon
                    </td>
                    <td>
                        <asp:Label ID="TimeHorizonLabel" runat="server" />
                    </td>
                </tr>
                <tr class="even">
                    <td class="lnowrap">
                        Existing Assets
                    </td>
                    <td class="lnowrap">
                        <asp:CheckBox ID="ExistingAssetsCheckBox" runat="server" Enabled="False" />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Strategy
                    </td>
                    <td>
                        <asp:Label ID="StrategyIDLabel" runat="server" />
                    </td>
                </tr>
                <tr class="even">
                    <td class="lnowrap">
                        Investment Amount
                    </td>
                    <td>
                        <asp:Label ID="InvestmentAmountLabel" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        InitialFee
                    </td>
                    <td>
                        <asp:Label ID="InitialFeeLabel" runat="server" />
                    </td>
                </tr>
                <tr class="even">
                    <td class="lnowrap">
                        Status
                    </td>
                    <td class="lnowrap">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatLayout="Flow">
                            <asp:ListItem>High Net Worth</asp:ListItem>
                            <asp:ListItem>Affluent</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
            <br />
            <div align="right">
                <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
                    Text="New" />
            </div>
<br />
<br />
        </ItemTemplate>
    </asp:FormView>
    <asp:ObjectDataSource ID="sourceClient" runat="server" DataObjectTypeName="RSMTenon.Data.Client"
        DeleteMethod="DeleteClient" InsertMethod="InsertClient" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetRecentClients" TypeName="RSMTenon.Data.Client" UpdateMethod="UpdateClient">
        <UpdateParameters>
            <asp:Parameter Name="client" Type="Object" />
            <asp:Parameter Name="original_client" Type="Object" />
        </UpdateParameters>
        <SelectParameters>
            <asp:Parameter Name="number" DefaultValue="1" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
