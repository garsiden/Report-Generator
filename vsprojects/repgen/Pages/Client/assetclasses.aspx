<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="assetclasses.aspx.cs" Inherits="Pages_Client_assetclasses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h4 id="clientAssetHeader" runat="server">
    </h4>
    <br />
    <asp:DetailsView ID="detailsView" runat="server" AutoGenerateRows="False" DataSourceID="sourceAssets"
        CaptionAlign="Top" CssClass="listing" DataKeyNames="ClientGUID" 
        Width="60%" OnItemInserted="detailsView_ItemInserted"
        OnItemUpdated="detailsView_ItemUpdated" 
        onmodechanged="detailsView_ModeChanged">
        <RowStyle CssClass="odd" HorizontalAlign="Left" />
        <EmptyDataTemplate>
            <%# GetAssetClassName("CASH") %>
            &nbsp; </td>
            <td>
                &nbsp;
            </td>
            <tr class="even">
                <td class="lnowrap">
                    <%# GetAssetClassName("COMM") %>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="lnowrap">
                    <%# GetAssetClassName("COPR") %>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr class="even">
                <td class="lnowrap">
                    <%# GetAssetClassName("GLEQ") %>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="lnowrap">
                    <%# GetAssetClassName("HEDG") %>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr class="even">
                <td class="lnowrap">
                    <%# GetAssetClassName("LOSH") %>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="lnowrap">
                    <%# GetAssetClassName("PREQ") %>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr class="even">
                <td class="lnowrap">
                    <%# GetAssetClassName("UKCB") %>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="lnowrap">
                    <%# GetAssetClassName("UKEQ") %>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr class="even">
                <td class="lnowrap">
                    <%# GetAssetClassName("UKGB") %>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="lnowrap">
                    <%# GetAssetClassName("UKHY") %>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr class="even">
                <td class="lnowrap">
                    <%# GetAssetClassName("WOBO") %>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td class="right">
                    <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
                        Text="New" />
                    &nbsp;
                </td>
            </tr>
        </EmptyDataTemplate>
        <Fields>
            <asp:TemplateField HeaderText="Cash" SortExpression="CASH">
                <EditItemTemplate>
                    <asp:TextBox ID="textCASHEdit" runat="server" Text='<%# Bind("CASH", "{0:0.0}") %>'></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexCASHEdit" runat="server" ErrorMessage="Please enter a percentage Cash value."
                        Display="None" ControlToValidate="textCASHEdit" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$" ValidationGroup="Edit"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="textCASHInsert" runat="server" Text='<%# Bind("CASH", "{0:0.0}") %>'
                        ValidationGroup="Insert"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexCASHInsert" runat="server" ErrorMessage="Please enter a percentage Cash value."
                        Display="None" ControlToValidate="textCASHInsert" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="Insert"></asp:RegularExpressionValidator>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("CASH", "{0:0.0}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="left" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Commodities" SortExpression="COMM">
                <EditItemTemplate>
                    <asp:TextBox ID="textCOMMEdit" runat="server" Text='<%# Bind("COMM") %>' ValidationGroup="Edit"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexCOMMEdit" runat="server" ErrorMessage="Please enter a percentage Commodities value."
                        Display="None" ControlToValidate="textCOMMEdit" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="textCOMMInsert" runat="server" Text='<%# Bind("COMM", "{0:0.0}") %>'
                        ValidationGroup="Insert"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexCOMMInsert" runat="server" ErrorMessage="Please enter a percentage Commodities value."
                        Display="None" ControlToValidate="textCOMMInsert" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="Insert"></asp:RegularExpressionValidator>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("COMM", "{0:0.0}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Real Estate" SortExpression="COPR">
                <EditItemTemplate>
                    <asp:TextBox ID="textCOPREdit" runat="server" Text='<%# Bind("COPR") %>' ValidationGroup="Edit"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexCOPREdit" runat="server" ErrorMessage="Please enter a percentage Real Estate value."
                        Display="None" ControlToValidate="textCOPREdit" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="textCOPRInsert" runat="server" Text='<%# Bind("COPR", "{0:0.0}") %>'
                        ValidationGroup="Insert"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexCOPRInsert" runat="server" ErrorMessage="Please enter a percentage Real Estate value."
                        Display="None" ControlToValidate="textCOPRInsert" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="Insert"></asp:RegularExpressionValidator>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("COPR", "{0:0.0}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Global Equity" SortExpression="GLEQ">
                <EditItemTemplate>
                    <asp:TextBox ID="textGLEQEdit" runat="server" Text='<%# Bind("GLEQ") %>' ValidationGroup="Edit"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexGLEQEdit" runat="server" ErrorMessage="Please enter a percentage Global Equities value."
                        Display="None" ControlToValidate="textGLEQEdit" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="textGLEQInsert" runat="server" Text='<%# Bind("GLEQ", "{0:0.0}") %>'
                        ValidationGroup="Insert"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexGLEQInsert" runat="server" ErrorMessage="Please enter a percentage Global Equities value."
                        Display="None" ControlToValidate="textGLEQInsert" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="Insert"></asp:RegularExpressionValidator>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("GLEQ", "{0:0.0}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Hedge" SortExpression="HEDG">
                <EditItemTemplate>
                    <asp:TextBox ID="textHEDGEdit" runat="server" Text='<%# Bind("HEDG") %>' ValidationGroup="Edit"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexHEDGEdit" runat="server" ErrorMessage="Please enter a percentage Hedge value."
                        Display="None" ControlToValidate="textHEDGEdit" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="textHEDGInsert" runat="server" Text='<%# Bind("HEDG", "{0:0.0}") %>'
                        ValidationGroup="Insert" TextMode="Password"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexHEDGInsert" runat="server" ErrorMessage="Please enter a percentage Hedge value."
                        Display="None" ControlToValidate="textHEDGInsert" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="Insert"></asp:RegularExpressionValidator>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("HEDG", "{0:0.0}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Long Short" SortExpression="LOSH">
                <EditItemTemplate>
                    <asp:TextBox ID="textLOSHEdit" runat="server" Text='<%# Bind("LOSH") %>' ValidationGroup="Edit"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexLOSHEdit" runat="server" ErrorMessage="Please enter a percentage Long Short value."
                        Display="None" ControlToValidate="textLOSHEdit" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="textLOSHInsert" runat="server" Text='<%# Bind("LOSH", "{0:0.0}") %>'
                        ValidationGroup="Insert"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexLOSHInsert" runat="server" ErrorMessage="Please enter a percentage Long Short value."
                        Display="None" ControlToValidate="textLOSHInsert" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="Insert"></asp:RegularExpressionValidator>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("LOSH", "{0:0.0}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Private Equity" SortExpression="PREQ">
                <EditItemTemplate>
                    <asp:TextBox ID="textPREQEdit" runat="server" Text='<%# Bind("PREQ") %>' ValidationGroup="Edit"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexPREQEdit" runat="server" ErrorMessage="Please enter a percentage Private Equity value."
                        Display="None" ControlToValidate="textPREQEdit" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="textPREQInsert" runat="server" Text='<%# Bind("PREQ", "{0:0.0}") %>'
                        ValidationGroup="Insert"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexPREQInsert" runat="server" ErrorMessage="Please enter a percentage Private Equity value."
                        Display="None" ControlToValidate="textPREQInsert" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="Insert"></asp:RegularExpressionValidator>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("PREQ", "{0:0.0}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UK Corporate Bonds" SortExpression="UKCB">
                <EditItemTemplate>
                    <asp:TextBox ID="textUKCBEdit" runat="server" Text='<%# Bind("UKCB") %>' ValidationGroup="Edit"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexUKCBEdit" runat="server" ErrorMessage="Please enter a percentage UK Corporate Bonds value."
                        Display="None" ControlToValidate="textUKCBEdit" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="textUKCBInsert" runat="server" Text='<%# Bind("UKCB", "{0:0.0}") %>'
                        ValidationGroup="Insert"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexUKCBInsert" runat="server" ErrorMessage="Please enter a percentage UK Corporate Bonds value."
                        Display="None" ControlToValidate="textUKCBInsert" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="Insert"></asp:RegularExpressionValidator>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("UKCB", "{0:0.0}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UK Equities" SortExpression="UKEQ">
                <EditItemTemplate>
                    <asp:TextBox ID="textUKEQEdit" runat="server" Text='<%# Bind("UKEQ") %>' ValidationGroup="Edit"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexUKEQEdit" runat="server" ErrorMessage="Please enter a percentage UK Equities value."
                        Display="None" ControlToValidate="textUKEQEdit" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="textUKEQInsert" runat="server" Text='<%# Bind("UKEQ", "{0:0.0}") %>'
                        ValidationGroup="Insert"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexUKEQInsert" runat="server" ErrorMessage="Please enter a percentage UK Equities value."
                        Display="None" ControlToValidate="textUKEQInsert" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="Insert"></asp:RegularExpressionValidator>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("UKEQ", "{0:0.0}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UK Government Bonds" SortExpression="UKGB">
                <EditItemTemplate>
                    <asp:TextBox ID="textUKGBEdit" runat="server" Text='<%# Bind("UKGB") %>' ValidationGroup="Edit"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexUKGBEdit" runat="server" ErrorMessage="Please enter a percentage UK Government Bonds value."
                        Display="None" ControlToValidate="textUKGBEdit" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="textUKGBInsert" runat="server" Text='<%# Bind("UKGB") %>' ValidationGroup="Insert"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexUKGBInsert" runat="server" ErrorMessage="Please enter a percentage UK Government Bonds value."
                        Display="None" ControlToValidate="textUKGBInsert" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="Insert"></asp:RegularExpressionValidator>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label10" runat="server" Text='<%# Eval("UKGB", "{0:0.0}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UK High Yield Bonds" SortExpression="UKHY">
                <EditItemTemplate>
                    <asp:TextBox ID="textUKHYEdit" runat="server" Text='<%# Bind("UKHY") %>' ValidationGroup="Edit"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexUKHYEdit" runat="server" ErrorMessage="Please enter a percentage UK High Yield Bonds value."
                        Display="None" ControlToValidate="textUKHYEdit" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="textUKHYInsert" runat="server" Text='<%# Bind("UKHY", "{0:0.0}") %>'
                        ValidationGroup="Insert"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexUKHYInsert" runat="server" ErrorMessage="Please enter a percentage UK High Yield Bonds value."
                        Display="None" ControlToValidate="textUKHYInsert" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="Insert"></asp:RegularExpressionValidator>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label11" runat="server" Text='<%# Eval("UKHY", "{0:0.0}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="World Bonds" SortExpression="WOBO">
                <EditItemTemplate>
                    <asp:TextBox ID="textWOBOEdit" runat="server" Text='<%# Bind("WOBO") %>' ValidationGroup="Edit"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexWOBOEdit" runat="server" ErrorMessage="Please enter a percentage World Bonds value."
                        Display="None" ControlToValidate="textWOBOEdit" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="textWOBOInsert" runat="server" Text='<%# Bind("WOBO", "{0:0.0}") %>'
                        ValidationGroup="Insert"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validRegexWOBOInsert" runat="server" ErrorMessage="Please enter a percentage World Bonds value."
                        Display="None" ControlToValidate="textWOBOInsert" ValidationExpression="^100(?:\.0)?$|^\d{1,2}(?:\.\d)?$"
                        ValidationGroup="Insert"></asp:RegularExpressionValidator>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label12" runat="server" Text='<%# Eval("WOBO", "{0:0.0}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="lnowrap" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
            <asp:TemplateField>
                <EditItemTemplate>
                    <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" ValidationGroup="Edit">Update</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Cancel">Cancel</asp:LinkButton>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:LinkButton ID="InsertButton" runat="server" CommandName="Insert" ValidationGroup="Insert">Insert</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Cancel">Cancel</asp:LinkButton>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />&nbsp;
                    <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                </ItemTemplate>
                <HeaderStyle CssClass="right" />
                <ItemStyle CssClass="right" />
            </asp:TemplateField>
        </Fields>
        <AlternatingRowStyle CssClass="even" />
    </asp:DetailsView>
    <br />
    <div style="width: 60%; text-align: right;">
        <asp:HyperLink ID="hyperClient" runat="server" NavigateUrl="edit.aspx">Back to Client</asp:HyperLink>
    </div>
    <asp:Label ID="labelException" runat="server"></asp:Label><br />
    <asp:ValidationSummary ID="validationSummaryEdit" runat="server" ValidationGroup="Edit" />
    <asp:ValidationSummary ID="validationSummaryInsert" runat="server" ValidationGroup="Insert" />
    <asp:Label ID="labelInstruction" runat="server" Text="Enter values as percentages to one decimal place e.g., 10.5 for 10.5%" Visible="false"></asp:Label>
    <asp:LinqDataSource ID="sourceAssets" runat="server" ContextTypeName="RSMTenon.Data.RepGenDataContext"
        EnableDelete="True" EnableInsert="True" EnableUpdate="True" TableName="ClientAssetClasses"
        Where="ClientGUID == @ClientGUID" OnInserting="sourceAssets_Inserting">
        <WhereParameters>
            <asp:QueryStringParameter DbType="Guid" Name="ClientGUID" QueryStringField="guid" />
        </WhereParameters>
    </asp:LinqDataSource>
</asp:Content>
