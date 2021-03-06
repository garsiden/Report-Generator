<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Pages_Client_edit"
    MasterPageFile="~/MasterPages/SiteMasterPage.master" %>

<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="BDP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4 id="clientHeader" runat="server">
    </h4>
    <br />
    <asp:FormView ID="formClient" runat="server" DataKeyNames="GUID" DataSourceID="sourceClient"
        OnItemUpdated="formClient_ItemUpdated" EnableModelValidation="True"
        Width="60%" OnModeChanging="formClient_ModeChanging">
        <EditItemTemplate>
            <table class="listing">
                <tr class="odd">
                    <td class="left" width="30%">
                        Name
                    </td>
                    <td class="lnowrap">
                        <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' Width="95%" />
                    </td>
                </tr>
                <tr class="even">
                    <td class="lnowrap">
                        Date Issued
                    </td>
                    <td class="lnowrap">
                        <BDP:BDPLite ID="bdpDateIssued" runat="server" SelectedDate='<%# Bind("DateIssued") %>'
                            Style="display: inline;" DateFormat="d" />
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
                        </asp:DropDownList> year(s)
                    </td>
                </tr>
                <tr class="even">
                    <td class="lnowrap">
                        Use Existing Assets
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
                        <asp:DropDownList ID="listStrategy" runat="server" DataSource="<%# GetStrategies() %>"
                            DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("StrategyID") %>'
                            Width="70%" onchange='setClientStatus();'>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="even">
                    <td class="lnowrap">
                        Investment Amount (�)
                    </td>
                    <td class="left">
                        <asp:TextBox ID="InvestmentAmountTextBox" runat="server" Text='<%# Bind("InvestmentAmount", "{0:0}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Status<span style="color:Red;"><b>*</b></span>
                    </td>
                    <td class="left">
                        <asp:RadioButtonList ID="radioListStatus" runat="server" SelectedIndex='<%# Bind("Status") %>'
                            BorderStyle="None" RepeatLayout="Flow">
                            <asp:ListItem Selected="True">High Net Worth</asp:ListItem>
                            <asp:ListItem>Affluent</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
            <br />
            <div align="right">
                <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                    Text="Update" />&nbsp;
                <asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                    Text="Cancel" />
            </div>
            <asp:ValidationSummary ID="validSummary" runat="server" />
            <asp:RequiredFieldValidator ID="validRequiredName" runat="server" ErrorMessage="Please enter a client Name."
                Display="None" ControlToValidate="NameTextBox"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="validRequiredMeetingDate" runat="server" ErrorMessage="Please enter a Date Issued."
                ControlToValidate="bdpDateIssued" Display="None"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="validRequiredInvestmentAmount" runat="server" ErrorMessage="Please enter an Investment Amount."
                ControlToValidate="InvestmentAmountTextBox" Display="None"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="validCompareInvestment" runat="server" ErrorMessage="Please enter a valid Investment Amount."
                ValueToCompare="1" Type="Currency" Operator="GreaterThanEqual" ControlToValidate="InvestmentAmountTextBox"
                Display="None"></asp:CompareValidator>
            <asp:CustomValidator ID="validCustomClientTimeHorizon" runat="server" OnServerValidate="TimeHorizonServerValidate"
                ErrorMessage="Client's Time Horizon is less than Strategy's." Display="None"></asp:CustomValidator>
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
                <tr class="even">
                    <td class="lnowrap">
                        Date Issued
                    </td>
                    <td class="lnowrap">
                        <BDP:BDPLite ID="bdpDateIssuedInsert" runat="server" SelectedDate='<%# Bind("DateIssued") %>'
                            Style="display: inline;" />
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
                        <asp:DropDownList ID="listStrategyInsert" runat="server" DataSource="<%# GetStrategies() %>"
                            DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("StrategyID") %>'
                            Width="75%">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="even">
                    <td class="lnowrap">
                        Investment Amount
                    </td>
                    <td class="lnowrap">
                        <asp:TextBox ID="InvestmentAmountTextBox" runat="server" Text='<%# Bind("InvestmentAmount", "{0}") %>' />
                    </td>
                </tr>
            </table>
            <br />
            <div align="right">
                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                    Text="Insert" />&nbsp; &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server"
                        CausesValidation="False" CommandName="Cancel" Text="Cancel" />
            </div>
        </InsertItemTemplate>
        <ItemTemplate>
            <table class="listing">
                <tr>
                    <td class="lnowrap">
                        Name
                    </td>
                    <td class="lnowrap">
                        <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("Name") %>' />
                    </td>
                </tr>
                <tr class="even">
                    <td class="lnowrap">
                        Date Issued
                    </td>
                    <td class="lnowrap">
                        <asp:Label ID="DateIssuedLabel" runat="server" Text='<%# Bind("DateIssued", "{0:dd/MM/yyyy}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Time Horizon
                    </td>
                    <td class="lnowrap">
                        <asp:Label ID="TimeHorizonLabel" runat="server" Text='<%# Eval("TimeHorizon") + " year(s)" %>' />
                    </td>
                </tr>
                <tr class="even">
                    <td class="lnowrap">
                        Use Existing Assets
                    </td>
                    <td class="lnowrap">
                        <asp:CheckBox ID="ExistingAssetsCheckBox" runat="server" Checked='<%# Bind("ExistingAssets") %>'
                            Enabled="False" />
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
                <tr class="even">
                    <td class="lnowrap">
                        Investment Amount
                    </td>
                    <td class="lnowrap">
                        <asp:Label ID="InvestmentAmountLabel" runat="server" Text='<%# Bind("InvestmentAmount", "{0:�#,##0}") %>' />
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
            </table>
            <br />
            <div align="right">
                <asp:LinkButton ID="linkEdit" runat="server" CommandName="Edit">Edit</asp:LinkButton>
            </div>
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
                        Date Issued
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
                        Existing Assets
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
                        Investment Amount &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="lnowrap">
                        Status
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
    <asp:HyperLink ID="hyperClass" runat="server" NavigateUrl="~/Pages/Client/assetclasses.aspx">Add client assets by class</asp:HyperLink>
    <br />
    <asp:HyperLink ID="hyperAsset" runat="server" NavigateUrl="~/Pages/Client/assets.aspx">Add client assets by investment</asp:HyperLink>
    <br />
    <br />
    <asp:HyperLink ID="hyperClients" runat="server" NavigateUrl="~/Pages/Client/index.aspx">All Clients</asp:HyperLink>
    <div style="width: 60%" align="right">
        <asp:Button ID="btnCreateReport" runat="server" OnClick="btnCreateReport_Click" Text="Report"
            CssClass="button" Width="90px" OnClientClick="hideException('labelException');" />
    </div>
    <br />
    <asp:Label ID="labelException" class="errortext" runat="server"></asp:Label>
    <div id="textnote">
        <ul runat="server" id="noteList" style="width: 60%;">
            <li style="color:red"><b>* Note</b></li>
            <li>Clients with portfolios under �250,000 <u>OR</u> that require ISA-eligible funds
                only should be categorised as Affluent. </li>
            <li>Clients with no requirement for ISA-eligibility <u>AND</u> have a portfolio of over
                �250,000 should be categorised as High Net Worth. </li>
        </ul>
    </div>
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

    <script type="text/javascript">
        function setClientStatus() {
            var list = document.getElementById("ctl00_ContentPlaceHolder1_formClient_listStrategy");
            var opts = document.getElementsByName("ctl00$ContentPlaceHolder1$formClient$radioListStatus");
            var disable = true;
            if (list.options[list.selectedIndex].value == "TC") {
                opts[0].checked = true;
            } else {
                disable = false;
            }
            for (var i = 0; i < opts.length; i++)
                opts[i].disabled = disable;
        }
    </script>

</asp:Content>
