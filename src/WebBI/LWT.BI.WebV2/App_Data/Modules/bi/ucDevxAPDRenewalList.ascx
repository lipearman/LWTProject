<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxAPDRenewalList.ascx.vb" Inherits="Modules_ucDevxAPDRenewalList" %>


<asp:SqlDataSource ID="SqlDataSource_RawData" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIRawDataConnection %>"
    SelectCommand="select * from V_APDRenewalList  order by LotNo"
    DeleteCommand="delete tblAPDRenewalList where LotNo=@LotNo"
    >
    <DeleteParameters>
        <asp:Parameter Name="LotNo" />
    </DeleteParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_Details" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIRawDataConnection %>"
    SelectCommand="select * from tblAPDRenewalList where LotNo=@LotNo  order by LotNo">
    <SelectParameters>
        <asp:SessionParameter Name="LotNo" SessionField="LotNo" />
    </SelectParameters>
</asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_LotNo" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="select * from V_LotNo order by LotNo"></asp:SqlDataSource>



<dx:ASPxGridViewExporter runat="server" ID="GridExporter" GridViewID="gridDetails" />


<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true"
    HeaderText="" Font-Bold="true"
    runat="server" EnableAdaptivity="true" Width="100%" HeaderImage-IconID="businessobjects_bonote_32x32">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxGridView ID="gridRawData" Width="100%"
                ClientInstanceName="gridRawData"  
                DataSourceID="SqlDataSource_RawData" KeyFieldName="LotNo"
                SettingsBehavior-ColumnResizeMode="Control"
                runat="server" AutoGenerateColumns="false">
                <Styles>
                    <Cell Wrap="False"></Cell>
                </Styles>
                <Settings ShowFooter="true" ShowFilterRowMenu="true" UseFixedTableLayout="true"  ></Settings>
               
                <SettingsBehavior AllowDragDrop="True"
                    AllowFocusedRow="True"
                    EnableRowHotTrack="True"
                    ConfirmDelete="true"
                    ColumnResizeMode="Control" />


                <SettingsPager Mode="ShowPager" Visible="true" PageSize="12" > 
                </SettingsPager>


                <ClientSideEvents
                    RowDblClick="function(s, e) {
                        LoadingPanel.Show(); 
                        var key = s.GetRowKey(e.visibleIndex);  
                        popupDetails.PerformCallback(key);     
                    }     
                     " />

                <Toolbars>
                    <dx:GridViewToolbar Position="Top" ItemAlign="Left">
                        <Items>



                            <dx:GridViewToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButton ID="ASPxButton1"
                                        ClientInstanceName="bnUploadXLS"
                                        Border-BorderStyle="Groove" Border-BorderWidth="1" ForeColor="Black" BackColor="White"
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

                            <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />
                             <dx:GridViewToolbarItem Command="Delete" BeginGroup="true" />
                        </Items>
                    </dx:GridViewToolbar>
                </Toolbars>


                <Columns>
                    <dx:GridViewDataTextColumn FieldName="FiscalYear" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="LotNo" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="ImportDate" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center" CellStyle-Wrap="False" PropertiesDateEdit-DisplayFormatString="{0:dd/MM/yyyy}"></dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="TotalNo" HeaderStyle-HorizontalAlign="Center" Width="200" PropertiesTextEdit-DisplayFormatString="{0:N0}" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                </Columns>
                <TotalSummary>

                    <dx:ASPxSummaryItem FieldName="TotalNo" SummaryType="Sum" DisplayFormat="Total  {0:N0} Policy(s)" />
                </TotalSummary>
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
    <ClientSideEvents Shown="function(s,e){ASPxClientEdit.ClearEditorsInContainerById('frmImportConsentForm');}" />


    <HeaderStyle BackColor="#4796CE" ForeColor="White" />
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">

            <dx:ASPxCallbackPanel ID="MailFormPanel" runat="server" RenderMode="Div" Height="100%" ClientInstanceName="ClientMailFormPanel">
                <PanelCollection>
                    <dx:PanelContent>


                        <dx:ASPxFormLayout ID="frmImportConsentForm" ClientInstanceName="frmImportConsentForm" runat="server">

                            <Items>
                                <dx:LayoutGroup Caption="Import" AlignItemCaptions="true" ColCount="1">
                                    <Items>
                                       


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
                                                        <ClientSideEvents ValidationErrorOccurred=" function(s, e) {   
                                                                LoadingPanel.Hide();  
                                                            }"
                                                            FileUploadComplete=" function(s, e) {    
                                                                                if (e.callbackData.indexOf('success') == -1 ){    
                                                                                    LoadingPanel.Hide();
                                                                                    alert(e.callbackData);
                                                                                }
                                                                                else
                                                                                {
                                                                                    LoadingPanel.Hide(); 
                                                                                    popupImportDS.Hide();
                                                                                    gridRawData.Refresh();
                                                                                }
                                                                        }" />
                                                    </dx:ASPxUploadControl>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                         <dx:LayoutItem Caption="LotNo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox ID="cbLotNo" runat="server"
                                                        DataSourceID="SqlDataSource_LotNo" ValidationSettings-RequiredField-IsRequired="true"
                                                        TextField="LotNo"
                                                        ValueField="LotNo">
                                                    </dx:ASPxComboBox>

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
                                                                                           //frmImport_UploadFile.Upload();
                                                                                           cbUpload.PerformCallback();
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
    Width="800"
    HeaderImage-IconID="programming_tag_16x16">
    <HeaderImage IconID="programming_tag_16x16"></HeaderImage>
    <HeaderStyle BackColor="#4796CE" ForeColor="White" />
    <ClientSideEvents
        PopUp="function(s,e){
         
        }"
        BeginCallback="function(s,e){ 
            popupDetails.Show();
        }"
        Shown="function(s,e){
         LoadingPanel.Hide(); 
         gridDetails.Refresh(); 
        }" />

    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">

            <dx:ASPxGridView ID="gridDetails" Width="100%"
                ClientInstanceName="gridDetails"
                DataSourceID="SqlDataSource_Details"
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
                            <dx:GridViewToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="false" Image-IconID="actions_refresh_16x16office2013" Text="Refresh">

                                        <ClientSideEvents Click="function(s,e){
                                                                            gridDetails.Refresh();
                                                                        }" />
                                    </dx:ASPxButton>
                                </Template>
                            </dx:GridViewToolbarItem>
                            <dx:GridViewToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButton ID="btnExportData" runat="server" RenderMode="Button" OnClick="btnExportData_Click"
                                        Width="90px" Text="Export" AutoPostBack="false" CausesValidation="false">
                                        <Image IconID="export_exporttoxlsx_16x16"></Image>
                                    </dx:ASPxButton>

                                    

                                </Template>
                            </dx:GridViewToolbarItem>

                            <dx:GridViewToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButton ID="btnClose" runat="server" RenderMode="Button"
                                        Width="90px" Text="Close" AutoPostBack="false" CausesValidation="false">
                                        <Image IconID="actions_close_16x16office2013"></Image>
                                        <ClientSideEvents Click="function(s, e) { popupDetails.Hide();    }" />
                                    </dx:ASPxButton>

                                </Template>
                            </dx:GridViewToolbarItem>


                        </Items>
                    </dx:GridViewToolbar>
                </Toolbars>

                <ClientSideEvents EndCallback="function(s,e){LoadingPanel.Hide();}" />




            </dx:ASPxGridView>

        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>



<dx:ASPxCallback ID="cbUpload" runat="server" ClientInstanceName="cbUpload">
    <ClientSideEvents
        CallbackError="function(s, e) { 
            LoadingPanel.Hide(); 
        }"
        CallbackComplete="function(s, e) {
            frmImport_UploadFile.Upload();  
        }" />
</dx:ASPxCallback>
