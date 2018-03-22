<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxRawDataSetup.ascx.vb" Inherits="Modules_ucDevxRawDataSetup" %>
<script>
    function view(VIEW_ID) {
        popupwindow.Show();
        callbackPanel_view.PerformCallback(VIEW_ID);
    }
</script>
<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="RawData" runat="server" Width="800">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="SqlDataSource_Data"
                KeyFieldName="VIEW_ID" AutoGenerateColumns="False" Width="100%">
                <Columns>
                    <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowEditButton="true" />

                    <dx:GridViewDataTextColumn FieldName="VIEW_TITLE" CellStyle-Wrap="False">
                        <PropertiesTextEdit>
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="VIEW_QUERY" CellStyle-Wrap="False">
                        <PropertiesTextEdit>
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                        </PropertiesTextEdit>
                       
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataMemoColumn FieldName="VIEW_CONNECTION" Visible="false" CellStyle-Wrap="False">
                        <PropertiesMemoEdit>
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                        </PropertiesMemoEdit>
                         <EditFormSettings Visible="True" />
                    </dx:GridViewDataMemoColumn>

                    <dx:GridViewDataComboBoxColumn FieldName="Department" CellStyle-Wrap="False">
                        <PropertiesComboBox>
                            <Items>
                                <dx:ListEditItem Text="APD" Value="APD" />
                            </Items>
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>


                    <dx:GridViewDataTextColumn FieldName="BIFile" Caption="ContextTable" CellStyle-Wrap="False">
                        <PropertiesTextEdit>
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataHyperLinkColumn Width="100" FieldName="VIEW_ID" Caption="Field">
                        <PropertiesHyperLinkEdit Imageurl="../../../../res/icon/page_white_edit.png" Style-HorizontalAlign="Center" NavigateUrlFormatString="javascript:view({0});">
                        </PropertiesHyperLinkEdit>
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataHyperLinkColumn>



                </Columns>

                <SettingsEditing Mode="EditForm" EditFormColumnCount="2"></SettingsEditing>



            </dx:ASPxGridView>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_Data" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="select * from tblReport_VIEWs order by VIEW_TITLE"
    InsertCommand="insert into tblReport_VIEWs(VIEW_TITLE,VIEW_QUERY,VIEW_CONNECTION,Department,BIFile) values(@VIEW_TITLE,@VIEW_QUERY,@VIEW_CONNECTION,@Department,@BIFile)"
    UpdateCommand="update tblReport_VIEWs set VIEW_TITLE=@VIEW_TITLE,VIEW_QUERY=@VIEW_QUERY,VIEW_CONNECTION=@VIEW_CONNECTION,Department=@Department,BIFile=@BIFile Where VIEW_ID=@VIEW_ID">
    <UpdateParameters>
        <asp:Parameter Name="VIEW_TITLE" Type="String" />
        <asp:Parameter Name="VIEW_QUERY" Type="String" />
        <asp:Parameter Name="VIEW_CONNECTION" Type="String" />
        <asp:Parameter Name="Department" Type="String" />
        <asp:Parameter Name="BIFile" Type="String" />
        <asp:Parameter Name="VIEW_ID" Type="Int32" />
    </UpdateParameters>
    <InsertParameters>
        <asp:Parameter Name="VIEW_TITLE" Type="String" />
        <asp:Parameter Name="VIEW_QUERY" Type="String" />
        <asp:Parameter Name="VIEW_CONNECTION" Type="String" />
        <asp:Parameter Name="Department" Type="String" />
        <asp:Parameter Name="BIFile" Type="String" />
    </InsertParameters>
</asp:SqlDataSource>




<dx:ASPxPopupControl ID="popupwindow" ClientInstanceName="popupwindow" runat="server" CloseOnEscape="true" Modal="True"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    ScrollBars="Auto"
    AllowDragging="true"
    AllowResize="True"
    EnableAnimation="true"
    CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="true"
    ShowPageScrollbarWhenModal="true"
    ShowMaximizeButton="true"
    HeaderText="Fields" PopupAnimationType="None">

    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            <dx:ASPxPanel ID="Panel1" runat="server" DefaultButton="btOK">
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent2" runat="server">

                        <dx:ASPxCallbackPanel runat="server" ID="callbackPanel_view"
                            ClientInstanceName="callbackPanel_view"
                            Height="100%" Width="100%">
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1">
                                    <dx:ASPxFormLayout ID="formLayout" runat="server" Width="100%" RequiredMarkDisplayMode="RequiredOnly" EnableViewState="false" EncodeHtml="false">
                                        <Items>
                                            <dx:LayoutGroup Caption="Fields" ColCount="1">
                                                <Items>
                                                    <dx:LayoutItem ShowCaption="False">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                                                <table>
                                                                    <tr>
                                                                        <td>


                                                                            <dx:ASPxTokenBox Caption="Fields" ID="ASPxTokenFields" AllowCustomTokens="false" ClientInstanceName="ASPxTokenFields" NullText="Select Fields" runat="server" Width="100%">
                                                                                <ValidationSettings>
                                                                                    <RequiredField IsRequired="true" />
                                                                                </ValidationSettings>
                                                                            </dx:ASPxTokenBox>

                                                                        </td>
                                                                        <td>


                                                                            <dx:ASPxButton ID="btnAddFields"
                                                                                ClientInstanceName="btnAddFields"
                                                                                runat="server" Text="Add Field"
                                                                                AutoPostBack="false">
                                                                                <ClientSideEvents Click="function(s,e) {
                                                                                 if(ASPxClientEdit.AreEditorsValid()) {
                                                                                   LoadingPanel.Show();
                                                                                   cbAddFields.PerformCallback(ASPxTokenFields.GetValue());                                                                                  
                                                                                 }
                                                                        }" />
                                                                            </dx:ASPxButton>

                                                                            <dx:ASPxCallback ID="cbAddFields" runat="server" ClientInstanceName="cbAddFields">
                                                                                <ClientSideEvents
                                                                                    CallbackComplete="function(s, e) {                                                                                    
                                                                                             LoadingPanel.Hide(); 
                                                                                             gridFields.Refresh();  
                                                                                            ASPxTokenFields.SetText('');                                                                                                                                                                                                                                  
                                                                                }" />
                                                                            </dx:ASPxCallback>


                                                                        </td>
                                                                    </tr>
                                                                </table>



                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
 
                                                    <dx:LayoutItem ShowCaption="False">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">






                                                                <dx:ASPxGridView ID="gridFields" ClientInstanceName="gridFields" runat="server"
                                                                    DataSourceID="SqlDataSource_Fields"
                                                                    KeyFieldName="FIELD_ID"
                                                                    SettingsPager-PageSizeItemSettings-ShowAllItem="true"
                                                                    AutoGenerateColumns="False"
                                                                    Width="100%">
                                                                    <SettingsPager Mode="ShowAllRecords" />
                                                                    <SettingsEditing Mode="Batch" EditFormColumnCount="2" />


                                                                    <SettingsPopup>
                                                                        <EditForm Modal="true" Width="300" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </SettingsPopup>


                                                                    <Columns>

                                                                        <dx:GridViewCommandColumn Width="100" CellStyle-Wrap="False" ShowDeleteButton="true" ShowEditButton="true" />

                                                                        <dx:GridViewDataTextColumn FieldName="FIELD_NAME" Caption="Name">
                                                                            <PropertiesTextEdit>
                                                                                <ValidationSettings Display="Dynamic">
                                                                                    <RequiredField IsRequired="true" />
                                                                                </ValidationSettings>

                                                                            </PropertiesTextEdit>
                                                                            <EditFormSettings Visible="False" />
                                                                        </dx:GridViewDataTextColumn>

                                                                        <dx:GridViewDataTextColumn FieldName="FIELD_CAPTION" Caption="Caption">
                                                                            <PropertiesTextEdit>
                                                                                <ValidationSettings Display="Dynamic">
                                                                                    <RequiredField IsRequired="true" />
                                                                                </ValidationSettings>
                                                                            </PropertiesTextEdit>
                                                                        </dx:GridViewDataTextColumn>





                                                                        <dx:GridViewDataComboBoxColumn FieldName="SummaryType" Caption="SumType">
                                                                            <PropertiesComboBox ValueField="Value" TextField="Name" Width="100" DataSourceID="SqlDataSource_PivotSummaryType">

                                                                                <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                                                                            </PropertiesComboBox>
                                                                        </dx:GridViewDataComboBoxColumn>


                                                                        <dx:GridViewDataComboBoxColumn FieldName="CellFormat_FormatType" Caption="CellType">
                                                                            <PropertiesComboBox ValueField="Value" TextField="Name" Width="100" DataSourceID="SqlDataSource_FormatType">

                                                                                <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                                                                            </PropertiesComboBox>
                                                                        </dx:GridViewDataComboBoxColumn>

                                                                        <dx:GridViewDataComboBoxColumn FieldName="CellFormat_FormatString" Caption="CellFormat">
                                                                            <PropertiesComboBox Width="100">
                                                                                <Items>
                                                                                    <dx:ListEditItem Text="{0:N0}" Value="{0:N0}" />
                                                                                    <dx:ListEditItem Text="{0:N1}" Value="{0:N1}" />
                                                                                    <dx:ListEditItem Text="{0:N2}" Value="{0:N2}" />

                                                                                    <dx:ListEditItem Text="{0:P0}" Value="{0:P0}" />
                                                                                    <dx:ListEditItem Text="{0:P1}" Value="{0:P1}" />
                                                                                    <dx:ListEditItem Text="{0:P2}" Value="{0:P2}" />

                                                                                </Items>
                                                                                <ValidationSettings Display="Dynamic">
                                                                                    <RequiredField IsRequired="true" />
                                                                                </ValidationSettings>
                                                                            </PropertiesComboBox>

                                                                        </dx:GridViewDataComboBoxColumn>


                                                                        <dx:GridViewDataComboBoxColumn FieldName="UnboundExpressionMode" Caption="UnboundExpressionMode">
                                                                            <PropertiesComboBox ValueField="Value" TextField="Name" Width="100" DataSourceID="SqlDataSource_UnboundExpressionMode">
                                                                                <Items>
                                                                                    <dx:ListEditItem Text="Name" Value="Value" />
                                                                                </Items>
                                                                                <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                                                                            </PropertiesComboBox>
                                                                        </dx:GridViewDataComboBoxColumn>

                                                                        <dx:GridViewDataTextColumn FieldName="UnboundExpression" Caption="UnboundExpression" CellStyle-Wrap="False">
                                                                            <PropertiesTextEdit>
                                                                            </PropertiesTextEdit>

                                                                        </dx:GridViewDataTextColumn>

                                                                        <dx:GridViewDataComboBoxColumn FieldName="UnboundColumnType" Caption="UnboundColumnType">
                                                                            <PropertiesComboBox ValueField="Value" TextField="Name" Width="100" DataSourceID="SqlDataSource_UnboundColumnType">
                                                                                <Items>
                                                                                    <dx:ListEditItem Text="Name" Value="Value" />
                                                                                </Items>
                                                                                <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                                                                            </PropertiesComboBox>
                                                                        </dx:GridViewDataComboBoxColumn>


                                                                        <dx:GridViewDataComboBoxColumn FieldName="GroupInterval" Caption="GroupInterval">
                                                                            <PropertiesComboBox ValueField="Value" TextField="Name" Width="100" DataSourceID="SqlDataSource_PivotGroupInterval">
                                                                                <Items>
                                                                                    <dx:ListEditItem Text="Name" Value="Value" />
                                                                                </Items>
                                                                                <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                                                                            </PropertiesComboBox>
                                                                        </dx:GridViewDataComboBoxColumn>

                                                                    </Columns>
                                                                </dx:ASPxGridView>




                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                </Items>
                                            </dx:LayoutGroup>
                                        </Items>
                                    </dx:ASPxFormLayout>

                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>

                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>




<asp:SqlDataSource ID="SqlDataSource_Fields" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="select * from tblReport_VIEWs_Field Where VIEW_ID=@VIEW_ID order by FIELD_NAME"

    UpdateCommand="update tblReport_VIEWs_Field 
    set      FIELD_CAPTION=@FIELD_CAPTION
            ,SummaryType=@SummaryType
            ,CellFormat_FormatType=@CellFormat_FormatType
            ,CellFormat_FormatString=@CellFormat_FormatString
            ,UnboundExpressionMode=@UnboundExpressionMode
            ,UnboundExpression=@UnboundExpression
            ,UnboundColumnType=@UnboundColumnType
            ,GroupInterval=@GroupInterval    

    Where FIELD_ID=@FIELD_ID"

    DeleteCommand="delete from tblReport_VIEWs_Field where FIELD_ID=@FIELD_ID">

    <UpdateParameters>
        <asp:Parameter Name="FIELD_CAPTION" Type="String" />
        <asp:Parameter Name="SummaryType" Type="Int32" />
        <asp:Parameter Name="CellFormat_FormatType" Type="String" />
        <asp:Parameter Name="CellFormat_FormatString" Type="String" />

        <asp:Parameter Name="UnboundExpressionMode" Type="Int32" />
        <asp:Parameter Name="UnboundExpression" Type="String" />
        <asp:Parameter Name="UnboundColumnType" Type="Int32" />

        <asp:Parameter Name="GroupInterval" Type="Int32" />
        <asp:Parameter Name="FIELD_ID" Type="Int32" />

    </UpdateParameters>
    <SelectParameters>
        <asp:SessionParameter Name="VIEW_ID" SessionField="VIEW_ID" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="FIELD_ID" Type="Int32" />
    </DeleteParameters>

</asp:SqlDataSource>



<asp:SqlDataSource ID="SqlDataSource_PivotSummaryType" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="select Value SummaryType,* from tblReport_MasterData where GroupName = 'PivotSummaryType' order by Name "></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_FilterArea" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="select * from tblReport_MasterData where GroupName = 'FilterArea' order by Name "></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_FormatType" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="select * from tblReport_MasterData where GroupName = 'FormatType' order by Name "></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_PivotGroupInterval" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="select * from tblReport_MasterData where GroupName = 'PivotGroupInterval' order by Name "></asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_UnboundExpressionMode" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="select * from tblReport_MasterData where GroupName = 'UnboundExpressionMode' order by Name "></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_UnboundColumnType" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="select * from tblReport_MasterData where GroupName = 'UnboundColumnType' order by Name "></asp:SqlDataSource>
