<%@ Page Language="C#" AutoEventWireup="true" CodeFile="new.aspx.cs" Inherits="Pages_Client_new"
    MasterPageFile="~/MasterPages/SiteMasterPage.master" %>

<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="BDP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4>
        Add New Client</h4>
    <asp:FormView ID="formView" runat="server" DataKeyNames="GUID" DataSourceID="sourceClient"
        OnItemInserted="formView_ItemInserted" OnItemInserting="formView_ItemInserting"
        Width="60%">
        <EditItemTemplate>
            ReportingFrequency:
            <br />
            Name:
            <br />
            DateIssued:
            <br />
            TimeHorizon:
            <br />
            ExistingAssets:
            <br />
            StrategyID:
            <br />
            InvestmentAmount:
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
                    <td class="lnowrap" width="36%">
                        Name
                    </td>
                    <td class="lnowrap">
                        <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' Width="100%" />
                    </td>
                </tr>
                <tr class="even">
                    <td class="lnowrap">
                        Date Issued
                    </td>
                    <td class="lnowrap">
                        <BDP:BDPLite ID="bdpDateIssued" runat="server" SelectedDate='<%# Bind("DateIssued") %>'
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
                        </asp:DropDownList> year(s)
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
                            AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="listStrategy_SelectedIndexChanged">
                            <asp:ListItem Value="XX">-- Select Strategy --</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="even">
                    <td class="lnowrap">
                        Investment Amount (£)
                    </td>
                    <td class="lnowrap">
                        <asp:TextBox ID="InvestmentAmountTextBox" runat="server" Text='<%# Bind("InvestmentAmount") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Status <span style="color:Red;"><b>*</b></span>
                    </td>
                    <td class="lnowrap">
                        <asp:RadioButtonList ID="radioListStatus" runat="server" RepeatLayout="Flow" SelectedValue='<%# Bind("Status") %>'>
                            <asp:ListItem Value="0">High Net Worth</asp:ListItem>
                            <asp:ListItem Value="1">Affluent</asp:ListItem>
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
            <asp:ValidationSummary ID="validSummary" runat="server" />
            <asp:RequiredFieldValidator ID="validRequiredName" runat="server" ErrorMessage="Please enter a client Name."
                Display="None" ControlToValidate="NameTextBox"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="validRequiredMeetingDate" runat="server" ErrorMessage="Please enter a Date Issued."
                ControlToValidate="bdpDateIssued" Display="None"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="validCompareStrategyID" runat="server" ControlToValidate="listStrategy"
                Display="None" ErrorMessage="Please select a Strategy from the list." Operator="NotEqual"
                ValueToCompare="XX"></asp:CompareValidator>
            <asp:RequiredFieldValidator ID="validRequiredInvestmentAmount" runat="server" ErrorMessage="Please enter an Investment Amount."
                ControlToValidate="InvestmentAmountTextBox" Display="None"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="validCompareInvestment" runat="server" ErrorMessage="Please enter a valid Investment Amount."
                ValueToCompare="1" Type="Currency" Operator="GreaterThanEqual" ControlToValidate="InvestmentAmountTextBox"
                Display="None"></asp:CompareValidator>
            <asp:RequiredFieldValidator ID="validRequiredStatus" runat="server" ErrorMessage="Please select a client Status."
                Display="None" ControlToValidate="radioListStatus"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="validCustomTimeHorizon" runat="server" ErrorMessage="Client's Time Horizon is less than Strategy's."
                OnServerValidate="TimeHorizonServerValidate" Display="None"></asp:CustomValidator>
        </InsertItemTemplate>
        <ItemTemplate>
            <table class="listing">
                <tr class="odd">
                    <td class="lnowrap" width="36%">
                        Name
                    </td>
                    <td>
                        <asp:Label ID="NameLabel" runat="server" />
                    </td>
                </tr>
                <tr class="even">
                    <td class="lnowrap">
                        Date Issued
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
                        Status
                    </td>
                    <td class="lnowrap">
                        <asp:RadioButtonList ID="radioListStatus" runat="server" RepeatLayout="Flow">
                            <asp:ListItem Value="0">High Net Worth</asp:ListItem>
                            <asp:ListItem Value="1">Affluent</asp:ListItem>
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
        </ItemTemplate>
        <EmptyDataTemplate>
            <itemtemplate>
            <table class="listing">
                <tr class="odd">
                    <td class="lnowrap" width="36%">
                        Name
                    </td>
                    <td>
                        <asp:Label ID="NameLabel" runat="server"/>
                    </td>
                </tr>
                <tr class="even">
                    <td class="lnowrap">
                        Date Issued
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
                        Status
                    </td>
                    <td class="lnowrap">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatLayout="Flow">
                            <asp:ListItem>High Net Worth</asp:ListItem><asp:ListItem>Affluent</asp:ListItem></asp:RadioButtonList></td></tr></table><br />
            <div align="right">
                <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
                    Text="New" />
            </div>
        </itemtemplate>
        </EmptyDataTemplate>
    </asp:FormView>
    <br />
    <asp:Label ID="labelException" class="errortext" runat="server"></asp:Label><br />
    <div id="textnote">
        <ul runat="server" id="noteList" style="width: 60%;">
            <li style="color:Red;"><b>* Note</b></li>
            <li>Clients with portfolios under £250,000 <u>OR</u> that require ISA-eligible funds
                only should be categorised as Affluent. </li>
            <li>Clients with no requirement for ISA-eligibility <u>AND</u> have a portfolio of over
                £250,000 should be categorised as High Net Worth. </li>
        </ul>
    </div>
    <br />
    <asp:ObjectDataSource ID="sourceClient" runat="server" DataObjectTypeName="RSMTenon.Data.Client"
        DeleteMethod="DeleteClient" InsertMethod="InsertClient" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetRecentClients" TypeName="RSMTenon.Data.Client" UpdateMethod="UpdateClient"
        OnInserted="sourceClient_Inserted">
        <UpdateParameters>
            <asp:Parameter Name="client" Type="Object" />
            <asp:Parameter Name="original_client" Type="Object" />
        </UpdateParameters>
        <SelectParameters>
            <asp:Parameter Name="number" DefaultValue="0" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
