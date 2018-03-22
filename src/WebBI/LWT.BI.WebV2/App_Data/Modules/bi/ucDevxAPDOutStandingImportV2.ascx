<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxAPDOutStandingImportV2.ascx.vb" Inherits="Modules_ucDevxAPDOutStandingImportV2" %>
<script>

    function OnToolbarItemClick(s, e) {
        if (IsExportToolbarCommand(e.item.name)) {
            e.processOnServer = true;
            e.usePostBack = true;
        }
    }
    function IsExportToolbarCommand(command) {
        return command == "ExportToXLSX" || command == "ExportToXLS";
    }
</script>

<asp:SqlDataSource ID="SqlDataSource_RawData" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIRawDataConnection %>"
    SelectCommand="select * from V_Accounting_Outstanding where [BY]=@UserName">
    <SelectParameters>
        <asp:Parameter Name="UserName" />
    </SelectParameters>

</asp:SqlDataSource>
<dx:ASPxGridViewExporter runat="server" ID="GridExporter" GridViewID="gridRawData" />

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true"
    HeaderText="" Font-Bold="true"
    runat="server" EnableAdaptivity="true" Width="100%" HeaderImage-IconID="businessobjects_bonote_32x32">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxGridView ID="gridRawData" Width="100%"
                ClientInstanceName="gridRawData"
                DataSourceID="SqlDataSource_RawData"
                SettingsBehavior-ColumnResizeMode="Control"
                runat="server" AutoGenerateColumns="false">
                <Styles>
                    <Cell Wrap="False"></Cell>
                </Styles>
                <Settings HorizontalScrollBarMode="Visible" ShowFooter="true" ShowFilterRowMenu="true" UseFixedTableLayout="true"></Settings>
                <SettingsSearchPanel CustomEditorID="tbToolbarSearch" />
                <SettingsBehavior ColumnResizeMode="Control"></SettingsBehavior>
                <SettingsCustomizationDialog Enabled="true" />

                <ClientSideEvents ToolbarItemClick="OnToolbarItemClick" />

                <Toolbars>
                    <dx:GridViewToolbar Position="Top" ItemAlign="Left">
                        <Items>
                            <dx:GridViewToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButtonEdit ID="tbToolbarSearch" runat="server" NullText="Search..." Width="300" Height="100%">
                                        <Buttons>
                                            <dx:SpinButtonExtended Image-IconID="find_find_16x16gray" />
                                        </Buttons>
                                    </dx:ASPxButtonEdit>

                                </Template>
                            </dx:GridViewToolbarItem>


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





                            <dx:GridViewToolbarItem Text="Export to" Image-IconID="actions_download_16x16office2013" BeginGroup="true">
                                <Items>
                                    <dx:GridViewToolbarItem Name="ExportToXLSX" Text="XLSX" Image-IconID="export_exporttoxlsx_16x16office2013" />
                                    <dx:GridViewToolbarItem Name="ExportToXLS" Text="XLS" Image-IconID="export_exporttoxls_16x16office2013" />
                                </Items>

                            </dx:GridViewToolbarItem>

                            <dx:GridViewToolbarItem Command="ShowCustomizationDialog" BeginGroup="true" />
                            <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />

                        </Items>
                    </dx:GridViewToolbar>
                </Toolbars>


                <Columns>
                    <dx:GridViewDataTextColumn FieldName="Inv No." CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="DN Date" CellStyle-Wrap="False" PropertiesDateEdit-DisplayFormatString="{0:dd/MM/yyyy}"></dx:GridViewDataDateColumn>
                    <dx:GridViewDataDateColumn FieldName="Eff. Date" CellStyle-Wrap="False" PropertiesDateEdit-DisplayFormatString="{0:dd/MM/yyyy}"></dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="Client Code" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Client Name" Width="200" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Client Group" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="AA-Group Name" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="AExec" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="UW" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="PolicyNo" Width="200" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Risk" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Brief Description" Width="200" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="BriefII" Width="200" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="BriefIII" Width="200" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Current" CellStyle-Wrap="False" PropertiesTextEdit-DisplayFormatString="{0:N2}"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="31 - 60 Days" CellStyle-Wrap="False" PropertiesTextEdit-DisplayFormatString="{0:N2}"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="61 - 90 Days" CellStyle-Wrap="False" PropertiesTextEdit-DisplayFormatString="{0:N2}"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="91-180 Days" CellStyle-Wrap="False" PropertiesTextEdit-DisplayFormatString="{0:N2}"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="181-365 Days" CellStyle-Wrap="False" PropertiesTextEdit-DisplayFormatString="{0:N2}"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Over 1 Year" CellStyle-Wrap="False" PropertiesTextEdit-DisplayFormatString="{0:N2}"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="AMOUNT BAL" CellStyle-Wrap="False" PropertiesTextEdit-DisplayFormatString="{0:N2}"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Sum >91 days" CellStyle-Wrap="False" PropertiesTextEdit-DisplayFormatString="{0:N2}"></dx:GridViewDataTextColumn>
                   <%-- <dx:GridViewDataDateColumn FieldName="CreateDate" CellStyle-Wrap="False" Visible="false" PropertiesDateEdit-DisplayFormatString="{0:dd/MM/yyyy}"></dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="CreateBy" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataTextColumn>
                  --%>  
                    <dx:GridViewDataTextColumn FieldName="Remark" Width="200"  CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <%--<dx:GridViewDataTextColumn FieldName="BY" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataTextColumn>--%>
                    <dx:GridViewDataDateColumn FieldName="LastDate"  Width="200" CellStyle-Wrap="False" PropertiesDateEdit-DisplayFormatString="{0:dd/MM/yyyy HH:mm:ss}"></dx:GridViewDataDateColumn>


                </Columns>
                <TotalSummary>
                     
                    <dx:ASPxSummaryItem FieldName="Current" SummaryType="Sum" DisplayFormat="{0:N2}" />
                    <dx:ASPxSummaryItem FieldName="31 - 60 Days" SummaryType="Sum" DisplayFormat="{0:N2}" />
                    <dx:ASPxSummaryItem FieldName="61 - 90 Days" SummaryType="Sum" DisplayFormat="{0:N2}" />
                    <dx:ASPxSummaryItem FieldName="91-180 Days" SummaryType="Sum" DisplayFormat="{0:N2}" />
                    <dx:ASPxSummaryItem FieldName="181-365 Days" SummaryType="Sum" DisplayFormat="{0:N2}" />
                    <dx:ASPxSummaryItem FieldName="Over 1 Year" SummaryType="Sum" DisplayFormat="{0:N2}" />
                    <dx:ASPxSummaryItem FieldName="AMOUNT BAL" SummaryType="Sum" DisplayFormat="{0:N2}" />
                    <dx:ASPxSummaryItem FieldName="Sum >91 days" SummaryType="Sum" DisplayFormat="{0:N2}" />
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
