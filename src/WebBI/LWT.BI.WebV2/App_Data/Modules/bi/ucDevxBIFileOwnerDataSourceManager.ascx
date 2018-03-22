<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxBIFileOwnerDataSourceManager.ascx.vb" Inherits="Modules_ucDevxBIFileOwnerDataSourceManager" %>
<script>

    function OnGetSelectedFieldValues(result) {
        cbAddColumns.PerformCallback(result);
        //for (var i = 0; i < result.length; i++)
        //alert(result[i]);
        //for (var j = 0; j < result[i].length; j++) {
        //alert(result[i][j]);
        //}
    }
    function OnDeleteFieldValues(result) {
        //alert(result);
        cbDeleteFields.PerformCallback(result);
    }

    function OnDeleteCalculationFieldValues(result) {
        //alert(result);
        cbDeleteCalculationFields.PerformCallback(result);
    }
</script>

<asp:SqlDataSource ID="SqlDataSource_gridData" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select * from tblDataSourceFile where Owner=@UserName and PortalId=@PortalId " >
    <SelectParameters>
        <asp:Parameter Name="UserName" />
        <asp:Parameter Name="PortalId" />
    </SelectParameters>

</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_PivotSummaryType" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select Value SummaryType,* from tblPivotGridViewOption where GroupName = 'PivotSummaryType' order by Name "></asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_PivotSummaryDisplayType" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select Value SummaryType,* from tblPivotGridViewOption where GroupName = 'PivotSummaryDisplayType' order by Name "></asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_FilterArea" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select * from tblPivotGridViewOption where GroupName = 'FilterArea' order by Name "></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_FormatType" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select * from tblPivotGridViewOption where GroupName = 'FormatType' order by Name "></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_PivotGroupInterval" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select * from tblPivotGridViewOption where GroupName = 'PivotGroupInterval' order by Name "></asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_UnboundExpressionMode" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select * from tblPivotGridViewOption where GroupName = 'UnboundExpressionMode' order by Name "></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_UnboundColumnType" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select * from tblPivotGridViewOption where GroupName = 'UnboundColumnType' order by Name "></asp:SqlDataSource>




<asp:SqlDataSource ID="SqlDataSource_COLUMN_NAME" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIRawDataConnection %>"
    SelectCommand="
        select COLUMN_NAME
        from INFORMATION_SCHEMA.COLUMNS
        where TABLE_NAME=@DSGUID
        and COLUMN_NAME not in(
          SELECT tblDataSourceFile_Field.FIELD_NAME
          FROM [Portal.BI].[dbo].[tblDataSourceFile]
          inner join [Portal.BI].[dbo].tblDataSourceFile_Field on [tblDataSourceFile].ID = tblDataSourceFile_Field.DS_ID
          where GUID=@DSGUID
    )
    ">
    <SelectParameters>
        <asp:SessionParameter Name="DSGUID" SessionField="DSGUID" />
    </SelectParameters>

</asp:SqlDataSource>








<dx:ASPxCallback ID="cbUpdateData" runat="server" ClientInstanceName="cbUpdateData">
    <ClientSideEvents EndCallback="function(s, e) { 
            LoadingPanel.Hide(); 
        }"
        CallbackError="function(s, e) { 
            LoadingPanel.Hide(); 
        }"
        CallbackComplete="function(s, e) {
            LoadingPanel.Hide();
            alert(e.result);
            
          
            
                  
        }" />
</dx:ASPxCallback>
<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true"
    HeaderText="" Font-Bold="true"
    runat="server" EnableAdaptivity="true" Width="100%" HeaderImage-IconID="businessobjects_bonote_32x32">
     <HeaderStyle BackColor="#4796CE" ForeColor="White" />
    <PanelCollection>
        <dx:PanelContent>
            <dx:ASPxGridView runat="server"
                ID="gridData" ClientInstanceName="gridData"
                Settings-ShowPreview="true"
                SettingsBehavior-EnableRowHotTrack="true"
                Styles-PreviewRow-ForeColor="Red"
                SettingsBehavior-AllowEllipsisInText="true"
                SettingsLoadingPanel-Mode="ShowAsPopup"
                Width="98%"
                KeyFieldName="GUID" EnableAdaptivity="true"
                DataSourceID="SqlDataSource_gridData"
                Settings-HorizontalScrollBarMode="Visible">
                <ClientSideEvents
                    RowDblClick="function(s, e) {
                        LoadingPanel.Show(); 
                        var key = s.GetRowKey(e.visibleIndex);  
                        popupDetails.PerformCallback(key);     
                    }     
                     " />
                <SettingsPopup
                    CustomizationWindow-VerticalAlign="TopSides"
                    CustomizationWindow-HorizontalAlign="LeftSides">
                </SettingsPopup>
                <Settings ShowHeaderFilterButton="true" />

                <SettingsSearchPanel CustomEditorID="tbToolbarSearch" />

                <SettingsBehavior AllowDragDrop="True"
                    AllowFocusedRow="True" EnableCustomizationWindow="true"
                    EnableRowHotTrack="True"
                    ColumnResizeMode="Control" />

                <SettingsPager Mode="ShowPager" PageSize="10">
                    <PageSizeItemSettings Visible="true" Items="10, 15, 30, 45" ShowAllItem="false" />
                </SettingsPager>
                <Styles>
                    <Header Font-Bold="true" Font-Underline="true" HorizontalAlign="Center">
                    </Header>
                </Styles>
                <Toolbars>
                    <dx:GridViewToolbar ItemAlign="Left">
                        <Items>
                            <dx:GridViewToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButton ID="ASPxButton1"
                                        ClientInstanceName="bnUploadXLS"
                                        Border-BorderWidth="0"
                                        AutoPostBack="false"
                                        runat="server"
                                        Image-IconID="navigation_up_16x16office2013"
                                        ToolTip="Import" Text="Import">
                                        <ClientSideEvents Click="function(s,e){ 
                                  
                                  popupImportDS.Show();
                                
                                      
                                }" />
                                    </dx:ASPxButton>
                                </Template>
                            </dx:GridViewToolbarItem>


                            <%--<dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />--%>

                            <dx:GridViewToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="false" Image-IconID="actions_refresh_16x16office2013" Text="Refresh">

                                        <ClientSideEvents Click="function(s,e){
                                                                            gridData.Refresh();
                                                                        }" />
                                    </dx:ASPxButton>
                                </Template>
                            </dx:GridViewToolbarItem>


                            <dx:GridViewToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButtonEdit ID="tbToolbarSearch" runat="server" NullText="Search..." Height="100%">
                                        <Buttons>
                                            <dx:SpinButtonExtended Image-IconID="find_find_16x16gray" />
                                        </Buttons>
                                    </dx:ASPxButtonEdit>
                                </Template>
                            </dx:GridViewToolbarItem>
                        </Items>
                    </dx:GridViewToolbar>
                </Toolbars>
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="ID" CellStyle-Wrap="False" Settings-AllowEllipsisInText="True" />

                    <dx:GridViewDataTextColumn FieldName="Title" Width="150" CellStyle-Wrap="False" Settings-AllowEllipsisInText="True" />
                    <dx:GridViewDataTextColumn FieldName="DESCRIPTION" Width="300" CellStyle-Wrap="False" Settings-AllowEllipsisInText="True" />
                    <dx:GridViewDataTextColumn FieldName="ASATDATE" Width="150" CellStyle-Wrap="False" Settings-AllowEllipsisInText="True" />

                    <dx:GridViewDataTextColumn FieldName="GUID" Width="200" CellStyle-Wrap="False" Settings-AllowEllipsisInText="True" />
                    <dx:GridViewDataTextColumn FieldName="Owner" CellStyle-Wrap="False" Settings-AllowEllipsisInText="True" />

                </Columns>



            </dx:ASPxGridView>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<dx:ASPxPopupControl ID="popupImportDS" ClientInstanceName="popupImportDS"
    runat="server"
    Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="Import"
    AllowDragging="true"
    AllowResize="True"
    DragElement="Window"
    EnableAnimation="true"
    CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="true"
    ShowPageScrollbarWhenModal="true"
    ScrollBars="Auto"
    EnableAdaptivity="true"
    Width="700">
     <HeaderStyle BackColor="#4796CE" ForeColor="White" />
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">

            <dx:ASPxCallbackPanel ID="MailFormPanel" runat="server" RenderMode="Div" Height="100%" ClientInstanceName="ClientMailFormPanel">
                <PanelCollection>
                    <dx:PanelContent>


                        <dx:ASPxFormLayout ID="frmImportConsentForm" ClientInstanceName="frmImportConsentForm" runat="server">

                            <Items>
                                <dx:LayoutGroup Caption="Upload" AlignItemCaptions="true" ColCount="1">
                                    <Items>

                                        <dx:LayoutItem Caption="Title">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox runat="server" ID="newTitle" ValidationSettings-RequiredField-IsRequired="true"></dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Description">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox runat="server" ID="newDescription" ValidationSettings-RequiredField-IsRequired="true"></dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="ASAT">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxDateEdit runat="server" ID="newASAT" ValidationSettings-RequiredField-IsRequired="true"></dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="กรุณาเลือกไฟล์ข้อมูล">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxUploadControl ID="frmImport_UploadFile"
                                                        ClientInstanceName="frmImport_UploadFile"
                                                        runat="server"
                                                        ShowUploadButton="false"
                                                        ShowProgressPanel="true"
                                                        Width="400">
                                                        <ValidationSettings AllowedFileExtensions=".xlsx,.xls"></ValidationSettings>
                                                        <ClientSideEvents ValidationErrorOccurred=" function(s, e) {   LoadingPanel.Hide();  }"
                                                            FileUploadComplete=" function(s, e) {    
                                                                                

                                                                                if (e.callbackData.indexOf('success') == -1 ){    
                                                                                    LoadingPanel.Hide();
                                                                                    alert(e.callbackData);
                                                                                }
                                                                                else
                                                                                {
                                                                                     

                                                                                    
                                                                                    cbSaveNewData.PerformCallback();
                                                                                }
                                                                        }" />
                                                    </dx:ASPxUploadControl>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxButton runat="server" ID="frmImport_btnUPload" Image-IconID="navigation_up_16x16"
                                                        Text="Upload" AutoPostBack="false">
                                                        <ClientSideEvents Click="function(s,e)
                                                                                  {
                                                 
                                                                                  if(ASPxClientEdit.AreEditorsValid()) 
                                                                                    {                                              
                                                                                        var txt = frmImport_UploadFile.GetText(); 
                                                           
                                                                                        if (txt == '') 
                                                                                        {                                                               
                                                                                           alert('Invalid File!!!');
                                                                                        }
                                                                                        else
                                                                                        {                                            
                                                                                           LoadingPanel.Show();
                                                                                           frmImport_UploadFile.Upload();
                                                                                        }                                                           
                                                                                    }
                                                                                    e.processOnServer = false;
                                                                           }
                                                                           " />
                                                    </dx:ASPxButton>

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




        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>

<dx:ASPxCallback ID="cbSaveNewData" runat="server" ClientInstanceName="cbSaveNewData">
    <ClientSideEvents EndCallback="function(s, e) { 
            LoadingPanel.Hide(); 
        }"
        CallbackError="function(s, e) { 
            LoadingPanel.Hide(); 
        }"
        CallbackComplete="function(s, e) {
            LoadingPanel.Hide();
            alert(e.result);
            popupImportDS.Hide();
            gridData.Refresh();
            
                  
        }" />
</dx:ASPxCallback>

<dx:ASPxPopupControl ID="popupDetails"
    ClientInstanceName="popupDetails"
    runat="server"
    Modal="True" Maximized="true"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="Details"
    AllowDragging="true"
    AllowResize="True"
    DragElement="Window"
    EnableAnimation="true"
    CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="true"
    ShowPageScrollbarWhenModal="true"
    ScrollBars="Auto"
    ShowMaximizeButton="true"
    EnableAdaptivity="true"
    Width="800" >
    <HeaderImage IconID="programming_tag_16x16" ></HeaderImage> 
      <HeaderStyle BackColor="#4796CE" ForeColor="White" />
    <ClientSideEvents
        PopUp="function(s,e){
         tbTabs.SetActiveTab(tbTabs.GetTabByName('RawData'));
        }"
        CloseUp="function(s,e){  
            gridData.Refresh();
        }"
        BeginCallback="function(s,e){ 
            popupDetails.Show();
        }"
        Shown="function(s,e){
         
         //gridRawData.Refresh();

         LoadingPanel.Hide();  

        }" />

    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">

            <dx:ASPxCallbackPanel ID="DataPreview" runat="server" RenderMode="Div" CssClass="MailPreviewPanel" ClientInstanceName="DataPreview">

                <DisabledStyle ForeColor="Black"></DisabledStyle>
                <PanelCollection>

                    <dx:PanelContent>

                        <dx:ASPxFormLayout ID="formPreview" Styles-LayoutGroupBox-Caption-ForeColor="#0000ff" SettingsItems-VerticalAlign="Top" runat="server" Width="100%" AlignItemCaptionsInAllGroups="True">

                            <SettingsItems VerticalAlign="Top"></SettingsItems>

                            <Styles>
                                <LayoutGroupBox>
                                    <Caption ForeColor="Blue"></Caption>
                                </LayoutGroupBox>

                                <LayoutItem Caption-Font-Bold="true">
                                    <Caption Font-Bold="True"></Caption>
                                </LayoutItem>
                            </Styles>


                            <Items>
                                <dx:LayoutGroup GroupBoxDecoration="None" Caption="Details" ColCount="2">
                                    <Items>
                                        <dx:LayoutItem Caption="Title">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">

                                                    <dx:ASPxTextBox runat="server" Width="100%" ID="editTitle" ValidationSettings-RequiredField-IsRequired="true">
                                                        <ValidationSettings>
                                                            <RequiredField IsRequired="True"></RequiredField>
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>


                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Description">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">

                                                    <dx:ASPxTextBox runat="server" Width="100%" ID="editDescription" ValidationSettings-RequiredField-IsRequired="true">
                                                        <ValidationSettings>
                                                            <RequiredField IsRequired="True"></RequiredField>
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>


                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <%--              <dx:LayoutItem ShowCaption="False" Caption="ASAT">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">

                                                  <dx:ASPxDateEdit runat="server" ID="editASAP" ValidationSettings-RequiredField-IsRequired="true">
                                                        <ValidationSettings>
                                                            <RequiredField IsRequired="True"></RequiredField>
                                                        </ValidationSettings>
                                                    </dx:ASPxDateEdit>


                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        --%>


                                        <dx:LayoutItem Caption="" ColSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">

                                                    <dx:ASPxButton runat="server" ID="btnSaveData" Image-IconID="actions_save_16x16devav"
                                                        Text="Save" AutoPostBack="false">
                                                        <ClientSideEvents Click="function(s,e)
                                                                                  {
                                                 
                                                                                  if(ASPxClientEdit.AreEditorsValid()) 
                                                                                    {                 
                                                                                          LoadingPanel.Show();                              
                                                                                          cbUpdateData.PerformCallback();                                                    
                                                                                    }
                                                                                    e.processOnServer = false;
                                                                           }
                                                                           " />

                                                        <Image IconID="actions_save_16x16devav"></Image>
                                                    </dx:ASPxButton>


                                                    <dx:ASPxButton runat="server" ID="btnUpdateData" CausesValidation="false" Image-IconID="navigation_up_16x16"
                                                        Text="Replace Data" AutoPostBack="false">
                                                        <ClientSideEvents Click="function(s,e)
                                                                                  {
                                                                                  popupUpdateDS.Show();
                                                                                  e.processOnServer = false;
                                                                           }
                                                                           " />

                                                        <Image IconID="navigation_up_16x16"></Image>
                                                    </dx:ASPxButton>


                                                    <dx:ASPxButton runat="server" ID="btnAppendData" CausesValidation="false" Image-IconID="actions_insert_16x16office2013"
                                                        Text="Append Data" AutoPostBack="false">

                                                        <ClientSideEvents Click="function(s,e)
                                                                                  {
                                                                                   popupInsertDS.Show();
                                                                                  e.processOnServer = false;
                                                                           }
                                                                           " />

                                                        <Image IconID="actions_insert_16x16office2013"></Image>
                                                    </dx:ASPxButton>



                                                    <dx:ASPxButton ID="btnClose" runat="server" RenderMode="Button"
                                                        Width="90px" Text="Close" AutoPostBack="false" CausesValidation="false">
                                                        <Image IconID="actions_close_16x16office2013"></Image>
                                                        <ClientSideEvents Click="function(s, e) {   popupDetails.Hide();    }" />
                                                    </dx:ASPxButton>



                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                        </dx:ASPxFormLayout>

 
 <dx:ASPxPageControl ID="tbTabs" ClientInstanceName="tbTabs" runat="server" 
                            
                            Width="100%" Height="100%" ActiveTabIndex="0" EnableHierarchyRecreation="True">
                           <ClientSideEvents   TabClick="function(s,e){
                              
                                   if(e.tab.name=='RawData')
                                   {
                                    gridRawData.Refresh();
                                   }
                                   else  if(e.tab.name=='Fields')
                                   {
                                    gridFields.Refresh();
                                   }
                                   else  if(e.tab.name=='CalculationFields')
                                   {
                                    gridCalculationFields.Refresh();
                                   }

                                
                               }" />
                            
                             <TabPages>
                                <dx:TabPage Text="RawData" Name="RawData" >
                                    <TabImage IconID="programming_tag_16x16"></TabImage>
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl1" runat="server">


                                            <dx:ASPxGridView ID="gridRawData" Width="100%"
                                                ClientInstanceName="gridRawData"
                                                DataSourceID="SqlDataSource_RawData"
                                                SettingsBehavior-ColumnResizeMode="Control"
                                                runat="server" AutoGenerateColumns="true">
                                                <Styles>
                                                    <Cell Wrap="False"></Cell>
                                                </Styles>

                                                <Settings HorizontalScrollBarMode="Visible"></Settings>
                                                <SettingsSearchPanel Visible="true" />
                                                <SettingsBehavior ColumnResizeMode="Control"></SettingsBehavior>
                                                <Toolbars>
                                                    <dx:GridViewToolbar ItemAlign="Left">
                                                        <Items>

                                                            <%--<dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />--%>
                                                            <dx:GridViewToolbarItem BeginGroup="true">
                                                                <Template>
                                                                    <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="false" Image-IconID="actions_refresh_16x16office2013" Text="Refresh">

                                                                        <ClientSideEvents Click="function(s,e){
                                                                            gridRawData.Refresh();
                                                                        }" />
                                                                    </dx:ASPxButton>
                                                                </Template>
                                                            </dx:GridViewToolbarItem>


                                                            <%-- <dx:GridViewToolbarItem BeginGroup="true">
                                                                <Template>
                                                                    <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="false" Image-IconID="actions_refresh_16x16office2013" Text="Clear Cache">

                                                                        <ClientSideEvents Click="function(s,e){
                                                                            LoadingPanel.Show();
                                                                            cbClearCache.PerformCallback('');
                                                                        }" />
                                                                    </dx:ASPxButton>
                                                                </Template>
                                                            </dx:GridViewToolbarItem>--%>
                                                        </Items>
                                                    </dx:GridViewToolbar>
                                                </Toolbars>

                                                <ClientSideEvents EndCallback="function(s,e){LoadingPanel.Hide();}" />




                                            </dx:ASPxGridView>


                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="Fields" Name="Fields">
                                    <TabImage IconID="programming_tag_16x16"></TabImage>
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl2" runat="server">





                                            <dx:ASPxGridView ID="gridFields" ClientInstanceName="gridFields" runat="server"
                                                DataSourceID="SqlDataSource_Fields"
                                                KeyFieldName="FIELD_ID"
                                                SettingsBehavior-ConfirmDelete="true"
                                                SettingsPager-PageSizeItemSettings-ShowAllItem="true"
                                                AutoGenerateColumns="False"
                                                Width="100%">

                                                <ClientSideEvents
                                                    RowDblClick="function(s, e) {
                                                   s.StartEditRow(e.visibleIndex);
                      
                                                }     
                                                 " />



                                                <SettingsCommandButton>
                                                    <DeleteButton RenderMode="Button" Image-IconID="actions_cancel_16x16office2013"></DeleteButton>
                                                    <CancelButton RenderMode="Button" Image-IconID="actions_cancel_16x16office2013"></CancelButton>
                                                    <UpdateButton Text="Save" RenderMode="Button" Image-IconID="actions_save_16x16devav"></UpdateButton>
                                                </SettingsCommandButton>

                                                <SettingsPager Mode="ShowAllRecords">
                                                    <PageSizeItemSettings ShowAllItem="True"></PageSizeItemSettings>
                                                </SettingsPager>
                                                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
                                                <SettingsPopup>
                                                    <EditForm Modal="true" Width="400" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </SettingsPopup>
                                                <SettingsBehavior AllowFocusedRow="true" AllowEllipsisInText="true" ColumnResizeMode="Control" />

                                                <Toolbars>
                                                    <dx:GridViewToolbar Position="Top" ItemAlign="Left">
                                                        <Items>
                                                            <dx:GridViewToolbarItem BeginGroup="true">
                                                                <Template>
                                                                    <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="false" Image-IconID="actions_download_16x16office2013" Text="Retrieve Fields">
                                                                        <ClientSideEvents Click="function(s,e){
                                                                            
                                                                              LoadingPanel.Show();
                                                                              popupFields.PerformCallback('');  
                                                                         
                                                                         }" />
                                                                    </dx:ASPxButton>
                                                                </Template>
                                                            </dx:GridViewToolbarItem>

                                                            <dx:GridViewToolbarItem BeginGroup="true">
                                                                <Template>


                                                                    <dx:ASPxButton ID="ASPxButton5" runat="server" CausesValidation="false" Image-IconID="actions_cancel_16x16office2013"
                                                                        Text="Delete Field" AutoPostBack="false">
                                                                        <ClientSideEvents Click="function(s,e) {


                                                                                    if(gridFields.GetSelectedRowCount()==0)
                                                                                    {
                                                                                        alert('No select data.');
                                                                                    }  
                                                                                    else
                                                                                    {   

                                                                                        if(confirm('confirm to delete?'))
                                                                                        {
                                                                                            LoadingPanel.Show();   
                                                                                            gridFields.GetSelectedFieldValues('FIELD_ID', OnDeleteFieldValues);     
                                                                                        }
                                                                                    }  
                                                                                  e.processOnServer = false;
                                                                           }
                                                                           " />

                                                                        <Image IconID="actions_cancel_16x16office2013"></Image>
                                                                    </dx:ASPxButton>

                                                                </Template>
                                                            </dx:GridViewToolbarItem>



                                                            <dx:GridViewToolbarItem BeginGroup="true">
                                                                <Template>
                                                                    <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="false" Image-IconID="actions_refresh_16x16office2013" Text="Refresh">

                                                                        <ClientSideEvents Click="function(s,e){
                                                                            gridFields.Refresh();
                                                                        }" />
                                                                    </dx:ASPxButton>
                                                                </Template>
                                                            </dx:GridViewToolbarItem>







                                                        </Items>
                                                    </dx:GridViewToolbar>
                                                </Toolbars>




                                                <Columns>
                                            
                                                    <dx:GridViewCommandColumn Width="80" ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" VisibleIndex="0"></dx:GridViewCommandColumn>

                                                    <dx:GridViewDataTextColumn FieldName="FIELD_NAME" Caption="Name" Width="200" CellStyle-Wrap="False">
                                                        <PropertiesTextEdit Width="350">
                                                            <ValidationSettings Display="Dynamic">
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>

                                                        </PropertiesTextEdit>
                                                        <EditFormSettings Visible="False" />
                                                    </dx:GridViewDataTextColumn>

                                                    <dx:GridViewDataTextColumn FieldName="FIELD_CAPTION" Caption="Caption" Width="200" CellStyle-Wrap="False">
                                                        <PropertiesTextEdit Width="350">
                                                            <ValidationSettings Display="Dynamic">
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>

                                                        </PropertiesTextEdit>
                                                        <EditFormSettings Visible="True" VisibleIndex="0" />
                                                        <CellStyle Wrap="False"></CellStyle>
                                                    </dx:GridViewDataTextColumn>

                                                    <dx:GridViewDataComboBoxColumn FieldName="SummaryType" Caption="SummaryType" Width="150" CellStyle-Wrap="False">
                                                        <PropertiesComboBox Width="350" ValueField="Value" TextField="Name" DataSourceID="SqlDataSource_PivotSummaryType">

                                                            <ValidationSettings RequiredField-IsRequired="true">
                                                                <RequiredField IsRequired="True"></RequiredField>
                                                            </ValidationSettings>
                                                        </PropertiesComboBox>
                                                        <EditFormSettings Visible="True" VisibleIndex="1" />
                                                        <CellStyle Wrap="False"></CellStyle>
                                                    </dx:GridViewDataComboBoxColumn>




                                                     

                                                    <dx:GridViewDataComboBoxColumn FieldName="PivotSummaryDisplayType" Visible="false" Width="200" Caption="SummaryDisplay">
                                                        <PropertiesComboBox Width="350" ValueField="Value" TextField="Name" DataSourceID="SqlDataSource_PivotSummaryDisplayType">

                                                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                                                        </PropertiesComboBox>
                                                        <EditFormSettings Visible="True" VisibleIndex="2" />
                                                    </dx:GridViewDataComboBoxColumn>

                                                    <dx:GridViewDataComboBoxColumn FieldName="CellFormat_FormatType" Visible="false" Caption="CellType">
                                                        <PropertiesComboBox Width="350" ValueField="Value" TextField="Name" DataSourceID="SqlDataSource_FormatType">

                                                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                                                        </PropertiesComboBox>
                                                        <EditFormSettings Visible="True" VisibleIndex="3" />
                                                    </dx:GridViewDataComboBoxColumn>
 

                                                    <dx:GridViewDataComboBoxColumn FieldName="CellFormat_FormatString" Caption="CellFormat" Width="200" CellStyle-Wrap="False">
                                                        <PropertiesComboBox Width="350">
                                                            <Items>
                                                                <dx:ListEditItem Text="Default" Value="" />
                                                                <dx:ListEditItem Text="#0 eg. 2017" Value="#0" />
                                                                <dx:ListEditItem Text="{0:N0} eg. 2,017" Value="{0:N0}" />
                                                                <dx:ListEditItem Text="{0:N1} eg. 2,017.0" Value="{0:N1}" />
                                                                <dx:ListEditItem Text="{0:N2} eg. 2,017.00" Value="{0:N2}" />

                                                                <dx:ListEditItem Text="{0:P0} eg. 1%" Value="{0:P0}" />
                                                                <dx:ListEditItem Text="{0:P1} eg. 1.0%" Value="{0:P1}" />
                                                                <dx:ListEditItem Text="{0:P2} eg. 1.00%" Value="{0:P2}" />

                                                                <dx:ListEditItem Text="{0:yyyy-MM-dd} eg. 2017-01-31" Value="{0:yyyy-MM-dd}" />
                                                                <dx:ListEditItem Text="{0:dd/MM/yyyy} eg. 31/01/2017" Value="{0:dd/MM/yyyy}" />
                                                                <dx:ListEditItem Text="{0:HH:mm} eg. 00:00" Value="{0:HH:mm}" />

                                                            </Items>

                                                        </PropertiesComboBox>
                                                        <EditFormSettings Visible="True" VisibleIndex="4" />
                                                        <CellStyle Wrap="False"></CellStyle>

                                                    </dx:GridViewDataComboBoxColumn>

 
                                                    <dx:GridViewDataComboBoxColumn FieldName="GroupInterval" Visible="false" Caption="GroupInterval">
                                                        <PropertiesComboBox Width="350" ValueField="Value" TextField="Name" DataSourceID="SqlDataSource_PivotGroupInterval">

                                                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                                                        </PropertiesComboBox>
                                                        <EditFormSettings Visible="True" VisibleIndex="8" />
                                                    </dx:GridViewDataComboBoxColumn>
 

                                                    <dx:GridViewDataComboBoxColumn FieldName="Dimension" Caption="Dimension" Width="100" CellStyle-Wrap="False">
                                                        <PropertiesComboBox Width="350">
                                                            <Items>
                                                                <dx:ListEditItem Text="" Value="" />
                                                                <dx:ListEditItem Text="Fac" Value="FA" />
                                                                <dx:ListEditItem Text="Measure" Value="ME" />

                                                            </Items>

                                                        </PropertiesComboBox>
                                                        <EditFormSettings Visible="True" VisibleIndex="9" />
                                                        <CellStyle Wrap="False"></CellStyle>

                                                    </dx:GridViewDataComboBoxColumn>

                                                </Columns>
                                            </dx:ASPxGridView>

 

                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>


                                 <dx:TabPage Text="Calculation Fields" Name="CalculationFields">
                                     <TabImage IconID="programming_tag_16x16"></TabImage>
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl3" runat="server">

                                            
                                            <dx:ASPxGridView ID="gridCalculationFields" ClientInstanceName="gridCalculationFields" runat="server"
                                                DataSourceID="SqlDataSource_Calculation_Fields"
                                                KeyFieldName="FIELD_ID"
                                                SettingsBehavior-ConfirmDelete="true"
                                                SettingsPager-PageSizeItemSettings-ShowAllItem="true"
                                                AutoGenerateColumns="False"
                                                Width="100%">

                                                <ClientSideEvents
                                                    RowDblClick="function(s, e) {
                                                   s.StartEditRow(e.visibleIndex);
                      
                                                }     
                                                 " />



                                                <SettingsCommandButton>
                                                    <DeleteButton RenderMode="Button" Image-IconID="actions_cancel_16x16office2013"></DeleteButton>
                                                    <CancelButton RenderMode="Button" Image-IconID="actions_cancel_16x16office2013"></CancelButton>
                                                    <UpdateButton Text="Save" RenderMode="Button" Image-IconID="actions_save_16x16devav"></UpdateButton>
                                                </SettingsCommandButton>

                                                <SettingsPager Mode="ShowAllRecords">
                                                    <PageSizeItemSettings ShowAllItem="True"></PageSizeItemSettings>
                                                </SettingsPager>
                                                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
                                                <SettingsPopup>
                                                    <EditForm Modal="true" Width="400" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </SettingsPopup>
                                                <SettingsBehavior AllowFocusedRow="true" AllowEllipsisInText="true" ColumnResizeMode="Control" />

                                                <Toolbars>
                                                    <dx:GridViewToolbar Position="Top" ItemAlign="Left">
                                                        <Items>
  

                                                            <dx:GridViewToolbarItem BeginGroup="true">
                                                                <Template>
                                                                    <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="false" Image-IconID="actions_download_16x16office2013" Text="Add Calculation Field">
                                                                        <ClientSideEvents Click="function(s,e){
                                                                             gridCalculationFields.AddNewRow();
                                                                         }" />
                                                                    </dx:ASPxButton>
                                                                </Template>
                                                            </dx:GridViewToolbarItem>



                                                            <dx:GridViewToolbarItem BeginGroup="true">
                                                                <Template>


                                                                    <dx:ASPxButton ID="ASPxButton5" runat="server" CausesValidation="false" Image-IconID="actions_cancel_16x16office2013"
                                                                        Text="Delete Field" AutoPostBack="false">
                                                                        <ClientSideEvents Click="function(s,e) {


                                                                                    if(gridCalculationFields.GetSelectedRowCount()==0)
                                                                                    {
                                                                                        alert('No select data.');
                                                                                    }  
                                                                                    else
                                                                                    {   

                                                                                        if(confirm('confirm to delete?'))
                                                                                        {
                                                                                            LoadingPanel.Show();   
                                                                                            gridCalculationFields.GetSelectedFieldValues('FIELD_ID', OnDeleteCalculationFieldValues);     
                                                                                        }
                                                                                    }  
                                                                                  e.processOnServer = false;
                                                                           }
                                                                           " />

                                                                        <Image IconID="actions_cancel_16x16office2013"></Image>
                                                                    </dx:ASPxButton>

                                                                </Template>
                                                            </dx:GridViewToolbarItem>



                                                            <dx:GridViewToolbarItem BeginGroup="true">
                                                                <Template>
                                                                    <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="false" Image-IconID="actions_refresh_16x16office2013" Text="Refresh">

                                                                        <ClientSideEvents Click="function(s,e){
                                                                            gridCalculationFields.Refresh();
                                                                        }" />
                                                                    </dx:ASPxButton>
                                                                </Template>
                                                            </dx:GridViewToolbarItem>







                                                        </Items>
                                                    </dx:GridViewToolbar>
                                                </Toolbars>




                                                <Columns>
                                                
                                                    <dx:GridViewCommandColumn Width="80" ShowSelectCheckbox="True"  SelectAllCheckboxMode="Page" VisibleIndex="0"></dx:GridViewCommandColumn>

                                                    <dx:GridViewDataTextColumn FieldName="FIELD_NAME" Caption="Name" Width="200" CellStyle-Wrap="False">
                                                        <PropertiesTextEdit Width="350">
                                                            <ValidationSettings Display="Dynamic">
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>

                                                        </PropertiesTextEdit>
                                                        <EditFormSettings Visible="False" />
                                                    </dx:GridViewDataTextColumn>

                                                    <dx:GridViewDataTextColumn FieldName="FIELD_CAPTION" Caption="Caption" Width="200" CellStyle-Wrap="False">
                                                        <PropertiesTextEdit Width="350">
                                                            <ValidationSettings Display="Dynamic">
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>

                                                        </PropertiesTextEdit>
                                                        <EditFormSettings Visible="True" VisibleIndex="0" />
                                                        <CellStyle Wrap="False"></CellStyle>
                                                    </dx:GridViewDataTextColumn>

                                                    <dx:GridViewDataComboBoxColumn FieldName="SummaryType" Caption="SummaryType" Width="200" CellStyle-Wrap="False">
                                                        <PropertiesComboBox Width="350" ValueField="Value" TextField="Name" DataSourceID="SqlDataSource_PivotSummaryType">

                                                            <ValidationSettings RequiredField-IsRequired="true">
                                                                <RequiredField IsRequired="True"></RequiredField>
                                                            </ValidationSettings>
                                                        </PropertiesComboBox>
                                                        <EditFormSettings Visible="True" VisibleIndex="1" />
                                                        <CellStyle Wrap="False"></CellStyle>
                                                    </dx:GridViewDataComboBoxColumn>

                                                     
                                                    <dx:GridViewDataComboBoxColumn FieldName="PivotSummaryDisplayType" Visible="false" Caption="SummaryDisplay">
                                                        <PropertiesComboBox Width="350" ValueField="Value" TextField="Name" DataSourceID="SqlDataSource_PivotSummaryDisplayType">

                                                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                                                        </PropertiesComboBox>
                                                        <EditFormSettings Visible="True" VisibleIndex="2" />
                                                    </dx:GridViewDataComboBoxColumn>

                                                    <dx:GridViewDataComboBoxColumn FieldName="CellFormat_FormatType" Visible="false"  Width="200" Caption="CellType">
                                                        <PropertiesComboBox Width="350" ValueField="Value" TextField="Name" DataSourceID="SqlDataSource_FormatType">

                                                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                                                        </PropertiesComboBox>
                                                        <EditFormSettings Visible="True" VisibleIndex="3" />
                                                    </dx:GridViewDataComboBoxColumn>

 

                                                    <dx:GridViewDataComboBoxColumn FieldName="CellFormat_FormatString" Caption="CellFormat"  Width="200" CellStyle-Wrap="False">
                                                        <PropertiesComboBox Width="350">
                                                            <Items>
                                                                <dx:ListEditItem Text="Default" Value="" />
                                                                <dx:ListEditItem Text="#0 eg. 2017" Value="#0" />
                                                                <dx:ListEditItem Text="{0:N0} eg. 2,017" Value="{0:N0}" />
                                                                <dx:ListEditItem Text="{0:N1} eg. 2,017.0" Value="{0:N1}" />
                                                                <dx:ListEditItem Text="{0:N2} eg. 2,017.00" Value="{0:N2}" />

                                                                <dx:ListEditItem Text="{0:P0} eg. 1%" Value="{0:P0}" />
                                                                <dx:ListEditItem Text="{0:P1} eg. 1.0%" Value="{0:P1}" />
                                                                <dx:ListEditItem Text="{0:P2} eg. 1.00%" Value="{0:P2}" />

                                                                <dx:ListEditItem Text="{0:yyyy-MM-dd} eg. 2017-01-31" Value="{0:yyyy-MM-dd}" />
                                                                <dx:ListEditItem Text="{0:dd/MM/yyyy} eg. 31/01/2017" Value="{0:dd/MM/yyyy}" />
                                                                <dx:ListEditItem Text="{0:HH:mm} eg. 00:00" Value="{0:HH:mm}" />

                                                            </Items>

                                                        </PropertiesComboBox>
                                                        <EditFormSettings Visible="True" VisibleIndex="4" />
                                                        <CellStyle Wrap="False"></CellStyle>

                                                    </dx:GridViewDataComboBoxColumn>

                                                     
                                                    <dx:GridViewDataTextColumn FieldName="UnboundExpression" Visible="false" Caption="UnboundExpression" CellStyle-Wrap="False">
                                                        <PropertiesTextEdit Width="350">
                                                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                                                        </PropertiesTextEdit>
                                                        <EditFormSettings Visible="True" VisibleIndex="6" />
                                                    </dx:GridViewDataTextColumn>

                                                    <dx:GridViewDataComboBoxColumn FieldName="UnboundColumnType" Visible="false" Caption="UnboundColumnType">
                                                        <PropertiesComboBox Width="350" ValueField="Value" TextField="Name" DataSourceID="SqlDataSource_UnboundColumnType">

                                                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                                                        </PropertiesComboBox>
                                                        <EditFormSettings Visible="True" VisibleIndex="7" />

                                                    </dx:GridViewDataComboBoxColumn>


                                                    <dx:GridViewDataComboBoxColumn FieldName="GroupInterval" Visible="false" Caption="GroupInterval">
                                                        <PropertiesComboBox Width="350" ValueField="Value" TextField="Name" DataSourceID="SqlDataSource_PivotGroupInterval">

                                                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                                                        </PropertiesComboBox>
                                                        <EditFormSettings Visible="True" VisibleIndex="8" />
                                                    </dx:GridViewDataComboBoxColumn>





                                                    <dx:GridViewDataComboBoxColumn FieldName="Dimension" Caption="Dimension" Width="100" CellStyle-Wrap="False">
                                                        <PropertiesComboBox Width="350">
                                                            <Items>
                                                                <dx:ListEditItem Text="" Value="" />
                                                                <dx:ListEditItem Text="Fac" Value="FA" />
                                                                <dx:ListEditItem Text="Measure" Value="ME" />

                                                            </Items>

                                                        </PropertiesComboBox>
                                                        <EditFormSettings Visible="True" VisibleIndex="9" />
                                                        <CellStyle Wrap="False"></CellStyle>

                                                    </dx:GridViewDataComboBoxColumn>

                                                </Columns>
                                            </dx:ASPxGridView>

 
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>

                            </TabPages>
                        </dx:ASPxPageControl>

                    </dx:PanelContent>
                </PanelCollection>
                <Styles>
                    <Disabled ForeColor="Black"></Disabled>
                </Styles>
            </dx:ASPxCallbackPanel>

        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>



<%--<asp:SqlDataSource ID="SqlDataSource_RawData" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIRawDataConnection %>"
    SelectCommand="select * from [_Blank]">
</asp:SqlDataSource> --%>

<asp:ObjectDataSource ID="SqlDataSource_RawData" runat="server"
    SelectMethod="GetBITable"
    TypeName="WebCacheWithSqlDependency.DbManager">
    <SelectParameters>
        <asp:SessionParameter Name="tableName" SessionField="DSGUID" Type="String" DefaultValue="_Blank" />
    </SelectParameters>
</asp:ObjectDataSource>


<dx:ASPxCallback ID="cbImportUpdateData" runat="server" ClientInstanceName="cbImportUpdateData">
    <ClientSideEvents EndCallback="function(s, e) { 
            LoadingPanel.Hide(); 
        }"
        CallbackError="function(s, e) { 
            LoadingPanel.Hide(); 
        }"
        CallbackComplete="function(s, e) {
            LoadingPanel.Hide();
            alert(e.result);
            
          
            
                  
        }" />
</dx:ASPxCallback>




<asp:SqlDataSource ID="SqlDataSource_Fields" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select * from tblDataSourceFile_Field Where DS_ID=@DS_ID and UnboundExpressionMode<>3 order by FIELD_ID desc"
    DeleteCommand="delete from tblDataSourceFile_Field  where FIELD_ID=@FIELD_ID"
    UpdateCommand="update tblDataSourceFile_Field
    set      FIELD_CAPTION=@FIELD_CAPTION
            ,SummaryType=@SummaryType
            ,CellFormat_FormatType=@CellFormat_FormatType
            ,CellFormat_FormatString=@CellFormat_FormatString
            ,GroupInterval=@GroupInterval 
            ,PivotSummaryDisplayType=@PivotSummaryDisplayType  
    where FIELD_ID=@FIELD_ID

    ">


    <SelectParameters>
        <asp:SessionParameter Name="DS_ID" SessionField="DS_ID" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="FIELD_ID" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="FIELD_CAPTION" Type="String" />
        <asp:Parameter Name="SummaryType" Type="Int32" />
        <asp:Parameter Name="CellFormat_FormatType" Type="String" />
        <asp:Parameter Name="CellFormat_FormatString" Type="String" />
        <asp:Parameter Name="PivotSummaryDisplayType" Type="Int32" />
        <asp:Parameter Name="GroupInterval" Type="Int32" />
        <asp:Parameter Name="FIELD_ID" />
    </UpdateParameters>
</asp:SqlDataSource>



<asp:SqlDataSource ID="SqlDataSource_Calculation_Fields" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select * from tblDataSourceFile_Field Where DS_ID=@DS_ID and UnboundExpressionMode=3 order by FIELD_ID desc"
    DeleteCommand="delete from tblDataSourceFile_Field  where FIELD_ID=@FIELD_ID"
    UpdateCommand="update tblDataSourceFile_Field
    set      FIELD_CAPTION=@FIELD_CAPTION
            ,SummaryType=@SummaryType
            ,CellFormat_FormatType=@CellFormat_FormatType
            ,CellFormat_FormatString=@CellFormat_FormatString
            ,UnboundExpression=@UnboundExpression
            ,UnboundColumnType=@UnboundColumnType
            ,GroupInterval=@GroupInterval 
            ,PivotSummaryDisplayType=@PivotSummaryDisplayType  
            ,Dimension=@Dimension  
    where FIELD_ID=@FIELD_ID"
    
    
    InsertCommand="
INSERT INTO tblDataSourceFile_Field
           (DS_ID
           ,FIELD_CAPTION
           ,FIELD_NAME
           ,SummaryType
           ,CellFormat_FormatType
           ,CellFormat_FormatString
           ,GroupInterval
           ,UnboundExpressionMode
           ,UnboundExpression
           ,UnboundColumnType
           ,PivotSummaryDisplayType
           ,AREA
           ,ORDERBY
           ,AREAINDEX
           ,Dimension)
     VALUES
           (@DS_ID
           ,@FIELD_CAPTION
           ,@FIELD_CAPTION
           ,@SummaryType
           ,@CellFormat_FormatType
           ,@CellFormat_FormatString
           ,@GroupInterval
           ,3
           ,@UnboundExpression
           ,@UnboundColumnType
           ,@PivotSummaryDisplayType
           ,-1
           ,0
           ,-1
           ,@Dimension)
    
    "
    
    >


    <SelectParameters>
        <asp:SessionParameter Name="DS_ID" SessionField="DS_ID" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="FIELD_ID" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="FIELD_CAPTION" Type="String" />
        <asp:Parameter Name="SummaryType" Type="Int32" />
        <asp:Parameter Name="CellFormat_FormatType" Type="String" />
        <asp:Parameter Name="CellFormat_FormatString" Type="String" />
        <asp:Parameter Name="UnboundExpression" Type="String" />
        <asp:Parameter Name="UnboundColumnType" Type="Int32" />
        <asp:Parameter Name="PivotSummaryDisplayType" Type="Int32" />
        <asp:Parameter Name="GroupInterval" Type="Int32" />
        <asp:Parameter Name="FIELD_ID" />
    </UpdateParameters>
    <InsertParameters>
        <asp:SessionParameter Name="DS_ID" SessionField="DS_ID" />
        <asp:Parameter Name="FIELD_CAPTION" Type="String" />
        <asp:Parameter Name="SummaryType" Type="Int32" />
        <asp:Parameter Name="CellFormat_FormatType" Type="String" />
        <asp:Parameter Name="CellFormat_FormatString" Type="String" />
        <asp:Parameter Name="UnboundExpression" Type="String" />
        <asp:Parameter Name="UnboundColumnType" Type="Int32" />
        <asp:Parameter Name="PivotSummaryDisplayType" Type="Int32" />
        <asp:Parameter Name="GroupInterval" Type="Int32" />
        <asp:Parameter Name="Dimension" Type="String" />

    </InsertParameters>
</asp:SqlDataSource>





<dx:ASPxCallback ID="cbRetrieveFields" runat="server" ClientInstanceName="cbRetrieveFields">
    <ClientSideEvents
        EndCallback="function(s,e){LoadingPanel.Hide()}"
        CallbackComplete="function(s,e){
        
        if(e.result == 'success'){
            LoadingPanel.Hide();
            gridFields.Refresh();
        }
        else
        {
            alert(e.result);
        }
        
        
        }" />
</dx:ASPxCallback>


<dx:ASPxCallback ID="cbDeleteData" runat="server" ClientInstanceName="cbDeleteData">
    <ClientSideEvents
        EndCallback="function(s,e){LoadingPanel.Hide()}"
        CallbackComplete="function(s,e){
            LoadingPanel.Hide();
            if(e.result == 'success'){
                popupDetails.Hide();
            }
            else
            {
                alert(e.result);
            }
        }" />
</dx:ASPxCallback>





<dx:ASPxPopupControl ID="popupUpdateDS" ClientInstanceName="popupUpdateDS"
    runat="server"
    Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="Replace Data"
    AllowDragging="true"
    AllowResize="True"
    DragElement="Window"
    EnableAnimation="true"
    CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="true"
    ShowPageScrollbarWhenModal="true"
    ScrollBars="Auto"
    EnableAdaptivity="true"
    Width="700">
     <HeaderStyle BackColor="#4796CE" ForeColor="White" />
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" RenderMode="Div" Height="100%">
                <PanelCollection>
                    <dx:PanelContent>
                        <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server">
                            <Items>
                                <dx:LayoutGroup Caption="Upload" AlignItemCaptions="true" ColCount="1">
                                    <Items>
                                        <dx:LayoutItem Caption="กรุณาเลือกไฟล์ข้อมูล">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxUploadControl ID="frmImport_UpdateFile" FileUploadMode="OnPageLoad"
                                                        ClientInstanceName="frmImport_UpdateFile" EnableViewState="false"
                                                        runat="server" ValidationSettings-MaxFileSize="30000000"
                                                        ShowUploadButton="false"
                                                        ShowProgressPanel="true"
                                                        Width="400">
                                                        <ValidationSettings AllowedFileExtensions=".xlsx,.xls"></ValidationSettings>
                                                        <ClientSideEvents
                                                            ValidationErrorOccurred=" function(s, e) {   LoadingPanel.Hide();  }"
                                                            FileUploadComplete=" function(s, e) {    
                                                                               
                                                                                if (e.callbackData.indexOf('success') == -1 ){  
                                                                                     LoadingPanel.Hide();  
                                                                                     alert(e.callbackData);
                                                                                }
                                                                                else
                                                                                {
                                                                                    popupUpdateDS.Hide();
                                                                                    gridRawData.PerformCallback();
                                                                                    
                                                                                }
                                                                        }" />
                                                    </dx:ASPxUploadControl>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxButton runat="server" ID="ASPxButton2" Image-IconID="navigation_up_16x16"
                                                        Text="Upload" AutoPostBack="false">
                                                        <ClientSideEvents
                                                            Click="function(s,e)
                                                                                  {
                                                 
                                                                                  if(ASPxClientEdit.AreEditorsValid()) 
                                                                                    {                                              
                                                                                        var txt = frmImport_UpdateFile.GetText(); 
                                                           
                                                                                        if (txt == '') 
                                                                                        {                                                               
                                                                                           alert('Invalid File!!!');
                                                                                        }
                                                                                        else
                                                                                        {                                            
                                                                                           LoadingPanel.Show();
                                                                                           frmImport_UpdateFile.Upload();
                                                                                        }                                                           
                                                                                    }
                                                                                    e.processOnServer = false;
                                                                           }
                                                                           " />

                                                        <Image IconID="navigation_up_16x16"></Image>
                                                    </dx:ASPxButton>

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




        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>



<dx:ASPxPopupControl ID="popupInsertDS" ClientInstanceName="popupInsertDS"
    runat="server"
    Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="Insert Data"
    AllowDragging="true"
    AllowResize="True"
    DragElement="Window"
    EnableAnimation="true"
    CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="true"
    ShowPageScrollbarWhenModal="true"
    ScrollBars="Auto"
    EnableAdaptivity="true"
    Width="700">
     <HeaderStyle BackColor="#4796CE" ForeColor="White" />
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
            <dx:ASPxCallbackPanel ID="ASPxCallbackPanel2" runat="server" RenderMode="Div" Height="100%">
                <PanelCollection>
                    <dx:PanelContent>
                        <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server">
                            <Items>
                                <dx:LayoutGroup Caption="Upload" AlignItemCaptions="true" ColCount="1">
                                    <Items>
                                        <dx:LayoutItem Caption="กรุณาเลือกไฟล์ข้อมูล">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxUploadControl ID="frmImport_InsertFile"
                                                        ClientInstanceName="frmImport_InsertFile"
                                                        runat="server"
                                                        ShowUploadButton="false"
                                                        ShowProgressPanel="true"
                                                        Width="400">
                                                        <ValidationSettings AllowedFileExtensions=".xlsx,.xls"></ValidationSettings>
                                                        <ClientSideEvents
                                                            ValidationErrorOccurred="function(s, e) {   LoadingPanel.Hide();  }"
                                                            FileUploadComplete="function(s, e) {    
                                                                                 
                                                                                if (e.callbackData.indexOf('success') == -1 ){  
                                                                                     LoadingPanel.Hide();   
                                                                                     alert(e.callbackData);
                                                                                }
                                                                                else
                                                                                {
                                                                                    popupInsertDS.Hide();
                                                                                    gridRawData.PerformCallback();
                                                                                    
                                                                                }
                                                                        }" />
                                                    </dx:ASPxUploadControl>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxButton runat="server" ID="ASPxButton3" Image-IconID="navigation_up_16x16"
                                                        Text="Upload" AutoPostBack="false">
                                                        <ClientSideEvents Click="function(s,e)
                                                                                  {
                                                 
                                                                                  if(ASPxClientEdit.AreEditorsValid()) 
                                                                                    {                                              
                                                                                        var txt = frmImport_InsertFile.GetText(); 
                                                           
                                                                                        if (txt == '') 
                                                                                        {                                                               
                                                                                           alert('Invalid File!!!');
                                                                                        }
                                                                                        else
                                                                                        {                                            
                                                                                           LoadingPanel.Show();
                                                                                           frmImport_InsertFile.Upload();
                                                                                        }                                                           
                                                                                    }
                                                                                    e.processOnServer = false;
                                                                           }
                                                                           " />

                                                        <Image IconID="navigation_up_16x16"></Image>
                                                    </dx:ASPxButton>

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




        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>


<dx:ASPxPopupControl ID="popupFields"
    ClientInstanceName="popupFields"
    runat="server"
    Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="Fields"
    AllowDragging="true"
    AllowResize="True"
    DragElement="Window"
    EnableAnimation="true"
    CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="true"
    ShowPageScrollbarWhenModal="true"
    ScrollBars="Auto"
    ShowMaximizeButton="true"
    EnableAdaptivity="true"
    Width="600" Height="400"
    HeaderImage-IconID="programming_tag_16x16">
    <HeaderImage IconID="programming_tag_16x16"></HeaderImage>
     <HeaderStyle BackColor="#4796CE" ForeColor="White" />
    <ClientSideEvents
        CloseUp="function(s,e){  
            gridFields.Refresh();
        }"
        BeginCallback="function(s,e){ 
            popupFields.Show();
        }"
        Shown="function(s,e){
         gridColumns.Refresh();
         LoadingPanel.Hide(); 
        }" />

    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl6" runat="server">




            <dx:ASPxGridView ID="gridColumns" ClientInstanceName="gridColumns" runat="server"
                DataSourceID="SqlDataSource_COLUMN_NAME"
                KeyFieldName="COLUMN_NAME" Width="100%">
                <ClientSideEvents SelectionChanged="" />
                <Columns>
                    <dx:GridViewCommandColumn Width="80" ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" VisibleIndex="0"></dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="COLUMN_NAME" VisibleIndex="1" />
                </Columns>
            </dx:ASPxGridView>
            <br />
            <dx:ASPxButton runat="server" ID="ASPxButton4" Image-IconID="actions_save_16x16devav"
                Text="Add Fields" AutoPostBack="false">
                <ClientSideEvents Click="function(s,e)
                                {
                                        if(gridColumns.GetSelectedRowCount()==0)
                                        {
                                            alert('No select data.');
                                        }  
                                        else
                                        {   
                                          LoadingPanel.Show();   
                                          gridColumns.GetSelectedFieldValues('COLUMN_NAME', OnGetSelectedFieldValues);     
                                        }                        
                                }
                                " />

            </dx:ASPxButton>

        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>


<dx:ASPxCallback ID="cbAddColumns" runat="server" ClientInstanceName="cbAddColumns">
    <ClientSideEvents EndCallback="function(s, e) { 
            LoadingPanel.Hide(); 
        }"
        CallbackError="function(s, e) { 
            LoadingPanel.Hide(); 
        }"
        CallbackComplete="function(s, e) {
            LoadingPanel.Hide();
            popupFields.Hide();
            gridFields.Refresh();
            //alert(e.result);     
        }" />
</dx:ASPxCallback>

<dx:ASPxCallback ID="cbDeleteFields" runat="server" ClientInstanceName="cbDeleteFields">
    <ClientSideEvents EndCallback="function(s, e) { 
            LoadingPanel.Hide(); 
        }"
        CallbackError="function(s, e) { 
            LoadingPanel.Hide(); 
        }"
        CallbackComplete="function(s, e) {
            LoadingPanel.Hide();
            gridFields.Refresh();     
        }" />
</dx:ASPxCallback>

<dx:ASPxCallback ID="cbDeleteCalculationFields" runat="server" ClientInstanceName="cbDeleteCalculationFields">
    <ClientSideEvents EndCallback="function(s, e) { 
            LoadingPanel.Hide(); 
        }"
        CallbackError="function(s, e) { 
            LoadingPanel.Hide(); 
        }"
        CallbackComplete="function(s, e) {
            LoadingPanel.Hide();
            gridCalculationFields.Refresh();     
        }" />
</dx:ASPxCallback>