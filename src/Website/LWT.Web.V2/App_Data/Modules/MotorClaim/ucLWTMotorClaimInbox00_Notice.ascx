<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucLWTMotorClaimInbox00_Notice.ascx.vb" Inherits="Modules_ucLWTMotorClaimInbox00_Notice" %>



<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true"  HeaderText=""  runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>
            <dx:ASPxGridView ID="gridData"
                ClientInstanceName="gridData"
                runat="server"
                DataSourceID="SqlDataSource_gridData"
                KeyFieldName="TRID"
                AutoGenerateColumns="False" 
                SettingsBehavior-AllowEllipsisInText="true"
                Settings-HorizontalScrollBarMode="Visible"
                Width="100%">

                <ClientSideEvents RowClick="function(s, e) {
                        var key = s.GetRowKey(e.visibleIndex);  
                        ClientMailPreviewPanel.PerformCallback(key);
                        popupDetails.Show();       
                           
                       
                    }     
                     " />


                <Settings ShowHeaderFilterButton="true" />
                <SettingsPager Mode="ShowPager" />

                <SettingsSearchPanel CustomEditorID="tbToolbarSearch" />

                <SettingsBehavior AllowDragDrop="True"
                    AllowFocusedRow="True"
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
                                    <dx:ASPxButton ID="btnExportData"
                                        ClientInstanceName="btnExportData"
                                        runat="server"
                                        Image-IconID="export_export_16x16office2013"
                                        ToolTip="Export to Excel" Text="Export"
                                        Border-BorderWidth="0"
                                        OnClick="btnExportData_Click">
                                    </dx:ASPxButton>

                                </Template>
                            </dx:GridViewToolbarItem>



                            <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />
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

                    
                   <dx:GridViewDataTextColumn FieldName="SubmitDate" Width="150" SortOrder="Descending" CellStyle-Wrap="False" Settings-AllowEllipsisInText="True" />
 <dx:GridViewDataColumn FieldName="InsurerName" Width="150" Caption="ประกันภัย" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
                    

                    <dx:GridViewDataColumn FieldName="App_TempPolicyNo"  Width="150" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
                    <dx:GridViewDataColumn FieldName="PostDate" Width="100" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
                    <dx:GridViewDataColumn FieldName="IsPost" Width="100" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
                    <%--<dx:GridViewDataColumn FieldName="Status"  Width="100"  CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />--%>

                    <dx:GridViewDataColumn FieldName="CarLicense" Width="150" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
                    <dx:GridViewDataColumn FieldName="DealerName" Caption="ดีลเลอร์" Width="150" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
                    <dx:GridViewDataColumn FieldName="BranchName" Width="150" Caption="สาขา" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
                    <%--<dx:GridViewDataColumn FieldName="ShowRoomName" Width="150" Caption="ShowRoom"  CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />--%>
                    <dx:GridViewDataColumn FieldName="FullCustomerName" Width="150" Caption="ผู้เอาประกันภัย" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
                    <dx:GridViewDataColumn FieldName="ChassisNo" Width="150" Caption="เลขตัวถัง" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
                    <dx:GridViewDataColumn FieldName="PolicyNo" Caption="เลขที่กรมธรรม์" Width="150" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
                    <dx:GridViewDataColumn FieldName="ClaimNo" Width="150" Caption="เลขที่เคลม" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
                    <dx:GridViewDataColumn FieldName="AccidentDate" Width="150" Caption="วันที่เกิดเหตุ" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
                    <dx:GridViewDataColumn FieldName="AccidentPlace" Width="150" Caption="สถานที่เกิดเหตุ" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
                    <dx:GridViewDataColumn FieldName="EffectiveDate" Width="150" Caption="วันคุ้มครอง" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
                    <dx:GridViewDataColumn FieldName="Beneficiary" Width="150" Caption="ผู้รับผลประโยชน์" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
                   <dx:GridViewDataColumn FieldName="NoticeName" Width="150" Caption="ผู้แจ้งเคลม" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
                    <dx:GridViewDataColumn FieldName="NoticeTel" Width="150" Caption="เบอร์โทรผู้แจ้งเคลม" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />


                    <%--<dx:GridViewDataColumn FieldName="TRID" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />--%>
                    <%--<dx:GridViewDataColumn FieldName="IsPost"  Width="50" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />--%>
                    <%--<dx:GridViewDataColumn FieldName="ShowRoomCode" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />--%>
                    <%--<dx:GridViewDataColumn FieldName="ShowRoomName" Width="150" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />--%>
                    <dx:GridViewDataColumn FieldName="DealerCode" Width="150" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
                    <%-- <dx:GridViewDataTextColumn FieldName="ResultMessage" Caption="Result" CellStyle-Wrap="False"
                        Settings-AllowFilterBySearchPanel="False" Width="250"
                        Settings-AllowHeaderFilter="True"
                        SettingsHeaderFilter-Mode="CheckedList" />--%>
                    <dx:GridViewDataTextColumn FieldName="Remark" CellStyle-Wrap="False"
                        Settings-AllowFilterBySearchPanel="False" Width="250"
                        Settings-AllowHeaderFilter="True"
                        SettingsHeaderFilter-Mode="CheckedList" />
                </Columns>
                <GroupSummary>
                    <dx:ASPxSummaryItem FieldName="ShowRoomCode" SummaryType="Count" />
                </GroupSummary>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter runat="server" ID="GridExporter" GridViewID="gridData" />



        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>


<asp:SqlDataSource ID="SqlDataSource_gridData" runat="server" ConnectionString="<%$ ConnectionStrings:MotorClaimConnectionString %>"
    SelectCommand="select * from V_ClaimDaily_00_Notice_Daily"></asp:SqlDataSource>





<dx:ASPxPopupControl ID="popupDetails" ClientInstanceName="popupDetails"
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
    Width="800"
    Height="680"
    FooterText="" HeaderImage-IconID="programming_tag_32x32"
    ShowFooter="true">

    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">


            <dx:ASPxCallbackPanel ID="MailPreviewPanel" runat="server" RenderMode="Div" CssClass="MailPreviewPanel" ClientInstanceName="ClientMailPreviewPanel">
                <PanelCollection>

                    <dx:PanelContent>
                        <dx:ASPxFormLayout ID="formPreview" Styles-LayoutGroupBox-Caption-ForeColor="#0000ff" SettingsItems-VerticalAlign="Top" DataSourceID="SqlDataSource_Details" runat="server" Width="100%" AlignItemCaptionsInAllGroups="True">
                            <Styles>
                                <LayoutItem Caption-Font-Bold="true"></LayoutItem>
                            </Styles>
                            <Items>

                                <dx:LayoutGroup GroupBoxDecoration="Box" Caption="รายละเอียดการแจ้งงาน" ColCount="3">

                                    <Items>





                                        <dx:LayoutItem Caption="รหัสสาขา" FieldName="ShowRoomCode">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">

                                                    <dx:ASPxLabel ID="ASPxLabel1" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="โชว์รูม" FieldName="ShowRoomName">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel2" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="สาขา" FieldName="BranchName">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel6" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem Caption="ประกันภัย" FieldName="InsurerName">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
                                                    <dx:ASPxLabel AllowEllipsisInText="true" ID="ASPxLabel4" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="เลขรับแจ้ง" FieldName="RefNo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel5" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="เลขเคลม" FieldName="ClaimNo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel3" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="ผู้แจ้งเหตุ" FieldName="NoticeName">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel7" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="เบอร์โทร" FieldName="NoticeTel">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel8" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem Caption="วันที่แจ้งเหตุ" FieldName="ClaimNoticeDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel9" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>





                                        <dx:LayoutItem Caption="สถานะ" FieldName="ClaimStatus">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer40" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel40" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem Caption="แจ้งงาน" FieldName="IsPost">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer41" runat="server">
                                                    <dx:ASPxCheckBox ID="ASPxLabel41" Wrap="False" ReadOnly="true" AllowEllipsisInText="true" runat="server"></dx:ASPxCheckBox>


                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="วันที่แจ้งงาน" FieldName="PostDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer42" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel42" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>





                                    </Items>
                                </dx:LayoutGroup>



                                <dx:LayoutGroup Caption="รายละเอียดความคุ้มครอง" ColCount="3">
                                    <Items>
                                        <dx:LayoutItem Caption="เลขที่ กธ." FieldName="PolicyNo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel10" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="คุ้มครอง" FieldName="EffectiveDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel11" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="สิ้นสุด" FieldName="ExpiryDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel12" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="ผู้เอาประกัน" FieldName="FullCustomerName">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel13" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="ผู้รับผลประโยชน์" FieldName="Beneficiary">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel14" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <%--  <dx:LayoutItem Caption="วันที่แจ้ง" FieldName="ClaimNoticeDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel15" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>--%>
                                    </Items>
                                </dx:LayoutGroup>


                                <dx:LayoutGroup Caption="รายละเอียดรถยนต์" ColCount="3">
                                    <Items>
                                        <dx:LayoutItem Caption="หมายเลขทะเบียน" FieldName="CarLicense">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer155" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel155" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="เลขถัง" FieldName="ChassisNo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel16" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="ทะเบียน" FieldName="CarLicense">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer17" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel17" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="ยี่ห้อ" FieldName="CarBrand">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer18" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel18" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="รุ่น" FieldName="CarModel">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer19" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel19" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="ปี" FieldName="CarYear">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel15" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>



                                        <dx:LayoutItem Caption="ผู้ขับขี่" FieldName="DriverName">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer20" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel20" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="เบอร์โทร" FieldName="DriverTel">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer21" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel21" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                    </Items>
                                </dx:LayoutGroup>




                                <dx:LayoutGroup Caption="รายละเอียดอุบัติเหตุ" ColCount="3">
                                    <Items>
                                        <dx:LayoutItem Caption="วันที่เกิดเหตุ" FieldName="AccidentDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer22" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel22" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="เวลา" FieldName="AccidentTime">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer23" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel23" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="สถานที่เกิดเหตุ" FieldName="AccidentPlace">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer25" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel25" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="ลักษณะการเกิดเหตุ" FieldName="ClaimDetails">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer24" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel24" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="ประเภทเคลม" FieldName="ClaimType">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer26" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel26" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="ผลวิเคราะห์เคลม" FieldName="ClaimResult">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer27" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel27" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem Caption="ความเสียหาย" FieldName="ClaimDamageDetails">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer28" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel28" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                    </Items>
                                </dx:LayoutGroup>






                                <dx:LayoutGroup Caption="ประเภทการซ่อม" ColCount="3">
                                    <Items>

                                        <dx:LayoutItem Caption="ประเภทอู่" FieldName="GarageType">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer32" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel32" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="ชื่ออู่" FieldName="GarageName">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer33" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel33" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="ที่อยู่" FieldName="GaragePlace">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer34" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel34" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="วันที่นำรถเข้าซ่อม" FieldName="CarRepairDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer35" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel35" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="วันที่รับรถ" FieldName="CarReceiveDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer36" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel36" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="ConsentFormNo" FieldName="ConsentFormNo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer37" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel37" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="ชื่อดีลเลอร์ที่สั่งอะไหล่" FieldName="PartsDealerName">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer38" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel38" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="รายการซ่อม" FieldName="PaymentDetails" ColSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer39" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel39" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>




                                        <dx:LayoutItem Caption="ค่าแรง" FieldName="LaborAmt">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer29" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel29" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="ค่าอะไหล่" FieldName="PartsAmt">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer30" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel30" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="ค่ารายการอื่นๆ" FieldName="OtherAmt">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer31" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel31" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem Caption="Remark" FieldName="Remark" ColSpan="3">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer44" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel44" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>



                                    </Items>
                                </dx:LayoutGroup>



                            </Items>




                        </dx:ASPxFormLayout>




                    </dx:PanelContent>
                </PanelCollection>
                <Styles>
                    <Disabled ForeColor="Black"></Disabled>
                </Styles>
            </dx:ASPxCallbackPanel>




        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>

<asp:SqlDataSource ID="SqlDataSource_Details" runat="server" ConnectionString="<%$ ConnectionStrings:MotorClaimConnectionString %>"
    SelectCommand="select * from V_ClaimTransactionData_Details where TRID=@TRID">

    <SelectParameters>
        <asp:SessionParameter Name="TRID" SessionField="TRID" />
    </SelectParameters>

</asp:SqlDataSource>
