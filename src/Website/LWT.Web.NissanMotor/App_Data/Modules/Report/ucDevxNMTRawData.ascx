<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxNMTRawData.ascx.vb" Inherits="Modules_ucDevxNLTHGetTempData" %>
<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="ข้อมูล RAWData" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxFormLayout ID="frmSearch" ClientInstanceName="frmSearch" runat="server" AlignItemCaptionsInAllGroups="True" ColCount="1" RequiredMarkDisplayMode="None" ShowItemCaptionColon="False">
                <Items>
                    <dx:LayoutGroup Caption="Search" ShowCaption="False" GroupBoxDecoration="None" ColCount="4" Width="330px">
                        <Items>

                            <dx:LayoutItem Caption="วันที่ ">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer
                                        ID="LayoutItemNestedControlContainer23"
                                        runat="server"
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxDateEdit ID="DateFrom" ClientInstanceName="DateFrom" DisplayFormatString="{0:dd/MM/yyyy}" runat="server">
                                            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>

                                        </dx:ASPxDateEdit>

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption=" &nbsp; &nbsp;ถึงวันที่">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxDateEdit ID="DateTo" ClientInstanceName="DateTo" DisplayFormatString="{0:dd/MM/yyyy}" runat="server">
                                            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>

                                        </dx:ASPxDateEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption=" &nbsp; &nbsp;">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">


                                        <table>
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="btnSearch" runat="server" RenderMode="Button"
                                                        Width="90px" Text="Search" AutoPostBack="false" CausesValidation="true" ValidationContainerID="frmSearch">
                                                        <Image IconID="filter_filter_16x16"></Image>
                                                        <ClientSideEvents Click="function(s, e) { 
                                                            if(ASPxClientEdit.AreEditorsValid()) 
                                                            { 
                                                               LoadingPanel.Show();
                                                               e.processOnServer = true;
                                                            }
                                                            else
                                                            {
                                                                alert('กรุณากรอกข้อมูลให้ครบ');
                                                            }
                                                        }" />
                                                    </dx:ASPxButton>
                                                </td>
                                                <td>&nbsp; &nbsp;</td>
                                                <td></td>
                                            </tr>

                                        </table>

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                </Items>
            </dx:ASPxFormLayout>
            <%-- ============ todo0 =======================--%>
            <%--KeyFieldName="Client" --%>
            <dx:ASPxGridView ID="gridData" ClientInstanceName="gridData" runat="server" DataSourceID="SqlDataSource_gridData"
                KeyFieldName="TempID" AutoGenerateColumns="False" Width="100%"
                SettingsBehavior-AutoExpandAllGroups="true">
                <SettingsPager Mode="ShowPager">
                    <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                </SettingsPager>
                <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" />
                <SettingsSearchPanel Visible="true" />
                <Settings ShowGroupPanel="true" />
                <Columns>

                    <dx:GridViewDataColumn FieldName="RowNo" CellStyle-Wrap="False" Caption="No." ExportCellStyle-Wrap="False" />

    <%--                <dx:GridViewDataColumn Caption="No."
                        FilterCellStyle-BackgroundImage-HorizontalPosition="center" VisibleIndex="0"
                        Width="4%" HeaderStyle-HorizontalAlign="Center">
                        <DataItemTemplate>
                            <div style="text-align: center">
                                <%# Container.ItemIndex + 1%>
                            </div>
                        </DataItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </dx:GridViewDataColumn>--%>
<dx:GridViewDataColumn FieldName="ShowroomID" CellStyle-Wrap="False" Caption="DealerCode" ExportCellStyle-Wrap="False" />

<dx:GridViewDataColumn FieldName="CarChassisNo" CellStyle-Wrap="False" Caption="CHASSIS NO." ExportCellStyle-Wrap="False" />

<dx:GridViewDataColumn FieldName="ShorwoomNameEn" CellStyle-Wrap="False" Caption="SHOWROOM NAME" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="ShowroomName" CellStyle-Wrap="False" Caption="ชื่อโชว์รูม" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="CarTypeNameFull" CellStyle-Wrap="False" Caption="MODEL" ExportCellStyle-Wrap="False" />
<dx:GridViewDataDateColumn FieldName="DateEffective" CellStyle-Wrap="False" Caption="EFFECTIVE DATE" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="Campaign" CellStyle-Wrap="False" Caption="CAMPAIGN " ExportCellStyle-Wrap="False" />


 </Columns>
 
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter runat="server" ID="GridExporter" GridViewID="gridData" />

            <br />
            <dx:ASPxButton ID="btnExportData" runat="server" RenderMode="Button"
                Width="90px" Text="Export" AutoPostBack="false" CausesValidation="false">
                <Image IconID="export_exporttoxlsx_16x16"></Image>
            </dx:ASPxButton>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>


<asp:SqlDataSource ID="SqlDataSource_gridData" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"></asp:SqlDataSource>
