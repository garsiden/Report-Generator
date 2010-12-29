<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewReport.aspx.cs" Inherits="NewReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<h1>New Report</h1>
    <form id="newReprtForm" runat="server">
    <div>
        <table style="width: 100%;" id="newReportTable">
            <tr>
                <td>
                    <asp:Label ID="clientNameLabel" runat="server" Text="Client Name" AssociatedControlID="clientNameText"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="clientNameText" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="meetingDateLabel" runat="server" Text="Meeting Date" AssociatedControlID="meetingDateText"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="meetingDateText" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="reportingFrequencyLabel" runat="server" Text="Reporting Frequency"
                        AssociatedControlID="reportingFrequencyList"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="reportingFrequencyList" runat="server">
                        <asp:ListItem Selected="True" Value="H">Half yearly</asp:ListItem>
                        <asp:ListItem Value="Q">Quarterly</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="initialFeeLabel" runat="server" Text="Initial Fee" AssociatedControlID="initialFeeText"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="initialFeeText" runat="server"></asp:TextBox>
                </td>
            </tr>
<tr>
<td>
    <asp:Label ID="timeHorizonLabel" runat="server" Text="Time Horizon" 
        AssociatedControlID="timeHorizonText"></asp:Label></td>
<td>
    <asp:TextBox ID="timeHorizonText" runat="server"></asp:TextBox></td>
</tr>
<tr>
<td>
    <asp:Label ID="suitableLabel" runat="server" 
        AssociatedControlID="suitableCheck" Text="Suitable"></asp:Label>
    </td>
<td>
    <asp:CheckBox ID="suitableCheck" runat="server" />
    </td>
</tr>
<tr>
<td>
    <asp:Label ID="existingAssetsLabel" runat="server" 
        AssociatedControlID="existingAssetsCheck" Text="Existing Assets"></asp:Label>
    </td>
<td>
    <asp:CheckBox ID="existingAssetsCheck" runat="server" />
    </td>
</tr>
<tr>
<td>
    <asp:Label ID="investmetTypeLabel" runat="server" 
        AssociatedControlID="investmentTypeRadio" Text="Investment Type"></asp:Label>
    </td>
<td>
    <asp:RadioButtonList ID="investmentTypeRadio" runat="server" 
        RepeatDirection="Horizontal">
        <asp:ListItem Value="I">Income</asp:ListItem>
        <asp:ListItem Value="G">Growth</asp:ListItem>
    </asp:RadioButtonList>
    </td>
</tr>
<tr>
<td>
    <asp:Label ID="strategyLabel" runat="server" 
        AssociatedControlID="strategyRadio" Text="Strategy"></asp:Label>
    </td>
<td>
    <asp:RadioButtonList ID="strategyRadio" runat="server">
        <asp:ListItem Value="D">Defensive</asp:ListItem>
        <asp:ListItem Value="C">Cautious</asp:ListItem>
        <asp:ListItem Value="B">Balanced</asp:ListItem>
        <asp:ListItem Value="G">Growth</asp:ListItem>
        <asp:ListItem Value="O">Opportunistic</asp:ListItem>
    </asp:RadioButtonList>
    </td>
</tr>
        </table>
    </div>
    <asp:Button ID="createReportButton" runat="server" Text="Create Report" 
        onclick="createReportButton_Click" />
    </form>
</body>
</html>
