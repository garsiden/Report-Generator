<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Pages_Strategy_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView ID="gridStrategy" runat="server" AutoGenerateColumns="False" 
        CssClass="listing" DataKeyNames="ID" DataSourceID="sourceStrategy">
        <RowStyle CssClass="odd" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="Name" HeaderText="Strategy Name" 
                SortExpression="Name">
            <ItemStyle CssClass="lnowrap" />
            </asp:BoundField>
            <asp:BoundField DataField="TimeHorizon" HeaderText="Time Horizon" 
                SortExpression="TimeHorizon">
            <ItemStyle CssClass="right" />
            </asp:BoundField>
            <asp:BoundField DataField="ReturnOverBase" HeaderText="Return Over Base" 
                SortExpression="ReturnOverBase">
            <ItemStyle CssClass="right" />
            </asp:BoundField>
            <asp:BoundField DataField="Cost" DataFormatString="{0:£#,##0}" 
                HeaderText="Cost" SortExpression="Cost">
            <ItemStyle CssClass="right" />
            </asp:BoundField>
            <asp:BoundField DataField="RollingReturn" HeaderText="Rolling Return" 
                SortExpression="RollingReturn">
            <ItemStyle CssClass="right" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Benchmark" SortExpression="Benchmark">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Benchmark") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Benchmark.Name") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="lnowrap" />
            </asp:TemplateField>
            <asp:HyperLinkField DataNavigateUrlFields="ID" 
                DataNavigateUrlFormatString="edit.aspx?id={0}" Text="Edit" />
            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
                SortExpression="ID" Visible="False" />
        </Columns>
        <AlternatingRowStyle CssClass="even" />
    </asp:GridView>
    <asp:LinqDataSource ID="sourceStrategy" runat="server" 
        ContextTypeName="RSMTenon.Data.RepGenDataContext" EnableUpdate="True" 
        TableName="Strategies">
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="sourceContent" runat="server" 
        ContextTypeName="RSMTenon.Data.RepGenDataContext" EnableUpdate="True" 
        TableName="Contents" Where="StrategyID == @StrategyID" 
        OrderBy="ContentID, Category">
        <WhereParameters>
            <asp:ControlParameter ControlID="gridStrategy" Name="StrategyID" 
                PropertyName="SelectedValue" Type="String" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:ListView ID="ListView1" runat="server" DataKeyNames="GUID" 
        DataSourceID="sourceContent">
        <ItemTemplate>
            <span style="background-color: #DCDCDC;color: #000000;">GUID:
            <asp:Label ID="GUIDLabel" runat="server" Text='<%# Eval("GUID") %>' />
            <br />
            ContentID:
            <asp:Label ID="ContentIDLabel" runat="server" Text='<%# Eval("ContentID") %>' />
            <br />
            Category:
            <asp:Label ID="CategoryLabel" runat="server" Text='<%# Eval("Category") %>' />
            <br />
            StrategyID:
            <asp:Label ID="StrategyIDLabel" runat="server" 
                Text='<%# Eval("StrategyID") %>' />
            <br />
            Text:
            <asp:Label ID="TextLabel" runat="server" Text='<%# Eval("Text") %>' />
            <br />
            SSMA_TimeStamp:
            <asp:Label ID="SSMA_TimeStampLabel" runat="server" 
                Text='<%# Eval("SSMA_TimeStamp") %>' />
            <br />
            Strategy:
            <asp:Label ID="StrategyLabel" runat="server" Text='<%# Eval("Strategy") %>' />
            <br />
            <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
            <br />
            <br />
            </span>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <span style="background-color: #FFF8DC;">GUID:
            <asp:Label ID="GUIDLabel" runat="server" Text='<%# Eval("GUID") %>' />
            <br />
            ContentID:
            <asp:Label ID="ContentIDLabel" runat="server" Text='<%# Eval("ContentID") %>' />
            <br />
            Category:
            <asp:Label ID="CategoryLabel" runat="server" Text='<%# Eval("Category") %>' />
            <br />
            StrategyID:
            <asp:Label ID="StrategyIDLabel" runat="server" 
                Text='<%# Eval("StrategyID") %>' />
            <br />
            Text:
            <asp:Label ID="TextLabel" runat="server" Text='<%# Eval("Text") %>' />
            <br />
            SSMA_TimeStamp:
            <asp:Label ID="SSMA_TimeStampLabel" runat="server" 
                Text='<%# Eval("SSMA_TimeStamp") %>' />
            <br />
            Strategy:
            <asp:Label ID="StrategyLabel" runat="server" Text='<%# Eval("Strategy") %>' />
            <br />
            <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
            <br />
            <br />
            </span>
        </AlternatingItemTemplate>
        <EmptyDataTemplate>
            <span>No data was returned.</span>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <span style="">GUID:
            <asp:TextBox ID="GUIDTextBox" runat="server" Text='<%# Bind("GUID") %>' />
            <br />
            ContentID:
            <asp:TextBox ID="ContentIDTextBox" runat="server" 
                Text='<%# Bind("ContentID") %>' />
            <br />
            Category:
            <asp:TextBox ID="CategoryTextBox" runat="server" 
                Text='<%# Bind("Category") %>' />
            <br />
            StrategyID:
            <asp:TextBox ID="StrategyIDTextBox" runat="server" 
                Text='<%# Bind("StrategyID") %>' />
            <br />
            Text:
            <asp:TextBox ID="TextTextBox" runat="server" Text='<%# Bind("Text") %>' />
            <br />
            Strategy:
            <asp:TextBox ID="StrategyTextBox" runat="server" 
                Text='<%# Bind("Strategy") %>' />
            <br />
            <asp:Button ID="InsertButton" runat="server" CommandName="Insert" 
                Text="Insert" />
            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                Text="Clear" />
            <br />
            <br />
            </span>
        </InsertItemTemplate>
        <LayoutTemplate>
            <div ID="itemPlaceholderContainer" runat="server" 
                style="font-family: Verdana, Arial, Helvetica, sans-serif;">
                <span ID="itemPlaceholder" runat="server" />
            </div>
            <div style="text-align: center;background-color: #CCCCCC;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
            </div>
        </LayoutTemplate>
        <EditItemTemplate>
            <span style="background-color: #008A8C;color: #FFFFFF;">GUID:
            <asp:Label ID="GUIDLabel1" runat="server" Text='<%# Eval("GUID") %>' />
            <br />
            ContentID:
            <asp:TextBox ID="ContentIDTextBox" runat="server" 
                Text='<%# Bind("ContentID") %>' />
            <br />
            Category:
            <asp:TextBox ID="CategoryTextBox" runat="server" 
                Text='<%# Bind("Category") %>' />
            <br />
            StrategyID:
            <asp:TextBox ID="StrategyIDTextBox" runat="server" 
                Text='<%# Bind("StrategyID") %>' />
            <br />
            Text:
            <asp:TextBox ID="TextTextBox" runat="server" Text='<%# Bind("Text") %>' />
            <br />
            SSMA_TimeStamp:
            <asp:Label ID="SSMA_TimeStampLabel1" runat="server" 
                Text='<%# Eval("SSMA_TimeStamp") %>' />
            <br />
            Strategy:
            <asp:TextBox ID="StrategyTextBox" runat="server" 
                Text='<%# Bind("Strategy") %>' />
            <br />
            <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                Text="Update" />
            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                Text="Cancel" />
            <br />
            <br />
            </span>
        </EditItemTemplate>
        <SelectedItemTemplate>
            <span style="background-color: #008A8C;font-weight: bold;color: #FFFFFF;">GUID:
            <asp:Label ID="GUIDLabel" runat="server" Text='<%# Eval("GUID") %>' />
            <br />
            ContentID:
            <asp:Label ID="ContentIDLabel" runat="server" Text='<%# Eval("ContentID") %>' />
            <br />
            Category:
            <asp:Label ID="CategoryLabel" runat="server" Text='<%# Eval("Category") %>' />
            <br />
            StrategyID:
            <asp:Label ID="StrategyIDLabel" runat="server" 
                Text='<%# Eval("StrategyID") %>' />
            <br />
            Text:
            <asp:Label ID="TextLabel" runat="server" Text='<%# Eval("Text") %>' />
            <br />
            SSMA_TimeStamp:
            <asp:Label ID="SSMA_TimeStampLabel" runat="server" 
                Text='<%# Eval("SSMA_TimeStamp") %>' />
            <br />
            Strategy:
            <asp:Label ID="StrategyLabel" runat="server" Text='<%# Eval("Strategy") %>' />
            <br />
            <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
            <br />
            <br />
            </span>
        </SelectedItemTemplate>
    </asp:ListView>
</asp:Content>

