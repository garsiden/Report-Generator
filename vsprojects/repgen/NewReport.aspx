<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewReport.aspx.cs" Inherits="NewReport"
    MasterPageFile="~/MasterPages/SiteMasterPage.master" %>

<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="BDP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        New Report</h1>
    <asp:FormView ID="FormView1" runat="server" DataKeyNames="GUID" DataSourceID="sourceClient">
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
            &nbsp;
            <table style="width: 100%;">
                <tr>
                    <td>
                        Name
                    </td>
                    <td>
                        <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' Width="80%" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Meeting Date
                    </td>
                    <td nowrap="nowrap">
                        <BDP:BDPLite ID="bdpMeetingDate" runat="server" SelectedDate='<%# Bind("MeetingDate") %>'
                            Style="display: inline;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Time Horizon
                    </td>
                    <td>
                        <asp:DropDownList ID="listTimeHorizonEdit" runat="server" SelectedValue='<%# Bind("TimeHorizon") %>'
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
                    </td>
                </tr>
                <tr>
                    <td>
                        Existing Assets
                    </td>
                    <td>
                        <asp:CheckBox ID="ExistingAssetsCheckBox" runat="server" Checked='<%# Bind("ExistingAssets") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Strategy
                    </td>
                    <td>
                        <asp:DropDownList ID="listStrategy" runat="server" DataSource="<%# GetStrategies() %>"
                            DataTextField="Name" DataValueField="ID" DataMember="StrategyID" SelectedValue='<%# Bind("StrategyID") %>'
                            AppendDataBoundItems="True">
                            <asp:ListItem Value="XX">-- Select Strategy --</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td nowrap="nowrap">
                        Investment Amount
                    </td>
                    <td>
                        <asp:TextBox ID="InvestmentAmountTextBox" runat="server" Text='<%# Bind("InvestmentAmount") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Initial Fee
                    </td>
                    <td>
                        <asp:TextBox ID="InitialFeeTextBox" runat="server" Text='<%# Bind("InitialFee") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Status
                    </td>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" SelectedIndex='<%# Bind("Status") %>'>
                            <asp:ListItem Selected="True">High Net Worth</asp:ListItem>
                            <asp:ListItem>Affluent</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                            Text="Insert" />
                        <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text="Cancel" />
                    </td>
                </tr>
            </table>
            <asp:ValidationSummary ID="validSummary" runat="server" />
            <asp:CompareValidator ID="validCompareStrategyID" runat="server" ControlToValidate="listStrategy"
                Display="None" ErrorMessage="Please select a Strategy from the list" Operator="NotEqual"
                ValueToCompare="XX"></asp:CompareValidator>
            <br />
            <br />
            &nbsp;
        </InsertItemTemplate>
        <ItemTemplate>
            <table style="width: 100%;">
                <tr>
                    <td>
                        Name
                    </td>
                    <td>
                        <asp:Label ID="NameLabel" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Meeting Date
                    </td>
                    <td>
                        <asp:Label ID="MeetingDateLabel" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Time Horizon
                    </td>
                    <td>
                        <asp:Label ID="TimeHorizonLabel" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Existing Assets
                    </td>
                    <td>
                        <asp:CheckBox ID="ExistingAssetsCheckBox" runat="server" Enabled="False" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Strategy
                    </td>
                    <td>
                        <asp:Label ID="StrategyIDLabel" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Investment Amount
                    </td>
                    <td>
                        <asp:Label ID="InvestmentAmountLabel" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        InitialFee
                    </td>
                    <td>
                        <asp:Label ID="InitialFeeLabel" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Status
                    </td>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                            <asp:ListItem>High Net Worth</asp:ListItem>
                            <asp:ListItem>Affluent</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
            &nbsp;<br />
            <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
                Text="New" />
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
