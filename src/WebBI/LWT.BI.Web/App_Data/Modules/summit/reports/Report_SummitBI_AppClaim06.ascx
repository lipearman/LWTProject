<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Report_SummitBI_AppClaim06.ascx.vb" Inherits="Reports_Report_SummitBI_AppClaim06" %>
<script type="text/javascript">
    function button1_Click(s, e) {
        if (gridData.IsCustomizationWindowVisible())
            gridData.HideCustomizationWindow();
        else
            gridData.ShowCustomizationWindow();
        UpdateButtonText();
    }
    function grid_CustomizationWindowCloseUp(s, e) {
        UpdateButtonText();
    }
    function UpdateButtonText() {
        var text = gridData.IsCustomizationWindowVisible() ? "Hide" : "Show";
        text += " Column";
        button1.SetText(text);
    }
</script>



<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" ShowHeader="false" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxFormLayout ID="frmSearch" ClientInstanceName="frmSearch" runat="server" AlignItemCaptionsInAllGroups="True" ColCount="1" RequiredMarkDisplayMode="None" ShowItemCaptionColon="False">
                <Items>
                    <dx:LayoutGroup Caption="Search" ShowCaption="False" GroupBoxDecoration="None" ColCount="4" Width="330px">
                        <Items>
                            <dx:LayoutItem Caption="เลือก">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer
                                        ID="LayoutItemNestedControlContainer4"
                                        runat="server"
                                        SupportsDisabledAttribute="True">

                                        <dx:ASPxComboBox ID="FilterDateType"
                                            runat="server"
                                            ValueType="System.String">
                                            <Items>
                                                <dx:ListEditItem Text="วันที่แจ้งเปิดเคลม" Value="วันที่แจ้งเปิดเคลม" Selected="true" />
                                                <dx:ListEditItem Text="วันที่เกิดเหตุ" Value="วันที่เกิดเหตุ" />
                                                <dx:ListEditItem Text="วันที่ทำสัญญา" Value="DateMakeContract" />
                                                <%-- <dx:ListEditItem Text="วันที่แจ้งงาน" Value="DateMakePolicy" />--%>
                                                <dx:ListEditItem Text="วันเริ่มคุ้มครอง" Value="DateBegin" />
                                                <dx:ListEditItem Text="วันหมดอายุ" Value="DateEnd" />
                                            </Items>

                                        </dx:ASPxComboBox>


                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="จากวันที่">
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
            <dx:ASPxGridView ID="gridData"
                ClientInstanceName="gridData" DataSourceID="SqlDataSource_gridData"
                runat="server" Styles-Cell-Wrap="False"
                Width="100%" Settings-HorizontalScrollBarMode="Visible"
                SettingsBehavior-AutoExpandAllGroups="true">
                <SettingsPager Mode="ShowPager">
                    <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                </SettingsPager>
                <SettingsSearchPanel Visible="true" />
                <Settings ShowHeaderFilterButton="true" />



                <SettingsSearchPanel CustomEditorID="tbToolbarSearch" />
                <SettingsPopup
                    CustomizationWindow-VerticalAlign="TopSides"
                    CustomizationWindow-HorizontalAlign="LeftSides">
                </SettingsPopup>
                <SettingsBehavior EnableRowHotTrack="true" AllowEllipsisInText="true" EnableCustomizationWindow="true" ColumnResizeMode="Control" />




                <Toolbars>
                    <dx:GridViewToolbar ItemAlign="Left">
                        <Items>

                            <dx:GridViewToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButton runat="server" ID="button1"
                                        ClientInstanceName="button1"
                                        Text="Show Column"
                                        UseSubmitBehavior="false"
                                        Image-IconID="richedit_showallfieldresults_16x16" Border-BorderWidth="0"
                                        AutoPostBack="false">
                                        <ClientSideEvents Click="button1_Click" />
                                    </dx:ASPxButton>
                                </Template>
                            </dx:GridViewToolbarItem>




                            <dx:GridViewToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButton ID="btnExport" OnClick="btnExportData_Click"
                                        runat="server" Border-BorderWidth="0"
                                        Image-IconID="export_exporttoxlsx_16x16"
                                        ToolTip="Export"
                                        Text="Export">
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
                    <dx:GridViewDataTextColumn FieldName="Number" Caption="ลำดับที่" VisibleIndex="0" UnboundType="Integer"></dx:GridViewDataTextColumn>




                    <dx:GridViewDataColumn Caption="Note (เลขเคลม)" FieldName="ClaimID"></dx:GridViewDataColumn>





                    <dx:GridViewDataColumn Caption="Claim Status" FieldName="ClaimStatus"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="Contract No." FieldName="ContractNo"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="CustomerName" FieldName="CustomerName"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="Insurance Company" FieldName="UWName"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="CertificateNo" FieldName="TempPolicyNo"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="ทุนประกัน" FieldName="ClaimSuminsure"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="ยี่ห้อ" FieldName="BrandCode"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="รุ่น" FieldName="ModelCode"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="ChassisNo" FieldName="MChassisNo"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="MLicense" FieldName="MLicense"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="MProvince" FieldName="MProvince"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="ClaimCause" FieldName="ClaimTypeName"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="เพิ่มเติม" FieldName="ClaimTypeOther"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="วันที่เกิดเหตุ" FieldName="AccidentDate"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="เวลาเกิดเหตุ" FieldName="AccidentTime"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="สถานที่เกิดเหตุ" FieldName="AccidentPlaceGroup"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="PoliceStation" FieldName="PoliceStation"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="Province" FieldName="Province"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="วันที่แจ้งเปิดเคลม" FieldName="ClaimDate2"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="วันที่รับ file scan / เอกสารเบื้องต้น" FieldName="DateReceivedDocFromClient"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="วันที่อนุมัติ" FieldName="DateSendDocToInsurer"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="ระยะเวลาพิจารณา" FieldName="PeriodOfConsider"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="ผลการพิจารณา" FieldName="ResultClaim"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="สาเหตุ" FieldName="Remark"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="วันที่รับเอกสารครบถ้วน" FieldName="DocCompleteDate"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="วันที่จ่ายค่าสินไหม (วันที่โอนเงินเข้าบริษัท )" FieldName="ChequeDate"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="ระยะเวลาการจ่ายสินไหม" FieldName="PeriodOfClaim"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="วันที่ยกเลิกเคลม" FieldName="CancelDate"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="เหตุผลที่ยกเลิกเคลม" FieldName="CancelReason"></dx:GridViewDataColumn>



<dx:GridViewDataColumn Caption="แบบฟอร์มเรียกร้องค่าสินไหม" FieldName="Doc7"></dx:GridViewDataColumn>
<dx:GridViewDataColumn Caption="บันทึกประจำวันเบื้องต้น" FieldName="Doc5"></dx:GridViewDataColumn>
<dx:GridViewDataColumn Caption="บันทึกประจำวันระบุเลขคดีอาญา" FieldName="Doc52"></dx:GridViewDataColumn>
<dx:GridViewDataColumn Caption="บันทึกประจำวันสรุปผลคดี" FieldName="Doc6"></dx:GridViewDataColumn>
<dx:GridViewDataColumn Caption="สัญญาเช่าซื้อ" FieldName="Doc13"></dx:GridViewDataColumn>
<dx:GridViewDataColumn Caption="สำเนาบัตรประชาชนผู้เอาประกันภัย" FieldName="Doc20"></dx:GridViewDataColumn>
<dx:GridViewDataColumn Caption="สำเนาทะเบียนบ้านผู้เอาประกันภัย" FieldName="Doc14"></dx:GridViewDataColumn>
<dx:GridViewDataColumn Caption="สำเนาทะเบียนรถ" FieldName="Doc15"></dx:GridViewDataColumn>
<dx:GridViewDataColumn Caption="ชุดโอนกรรมสิทธิ์" FieldName="Doc4"></dx:GridViewDataColumn>
<dx:GridViewDataColumn Caption="หนังสือมอบอำนาจชุดโอน" FieldName="Doc27"></dx:GridViewDataColumn>
<dx:GridViewDataColumn Caption="ใบคู่มือทะเบียน" FieldName="Doc3"></dx:GridViewDataColumn>
<dx:GridViewDataColumn Caption="ชุดจด" FieldName="Doc51"></dx:GridViewDataColumn>
<dx:GridViewDataColumn Caption="กุญแจรถจักรยานยนต์" FieldName="Doc2"></dx:GridViewDataColumn>
<dx:GridViewDataColumn Caption="หนังสือโอนกรรมสิทธิ์" FieldName="Doc43"></dx:GridViewDataColumn>
<dx:GridViewDataColumn Caption="หนังสือขอรับค่าสินไหมทดแทน" FieldName="Doc44"></dx:GridViewDataColumn>
<dx:GridViewDataColumn Caption="หนังสือรับรองกรรมสิทธิ์" FieldName="Doc45"></dx:GridViewDataColumn>
<dx:GridViewDataColumn Caption="หนังสือสละสิทธิ์รับค่าสินไหมฯ" FieldName="Doc31"></dx:GridViewDataColumn>
<dx:GridViewDataColumn Caption="ใบเสนอราคาค่าอะไหล่และค่าซ่อม" FieldName="Doc9"></dx:GridViewDataColumn>
<dx:GridViewDataColumn Caption="รูปถ่ายความเสียหาย" FieldName="Doc11"></dx:GridViewDataColumn>
<dx:GridViewDataColumn Caption="สัญญาประนีประนอมยอมความ" FieldName="Doc46"></dx:GridViewDataColumn>
<dx:GridViewDataColumn Caption="สัญญาค้ำประกัน" FieldName="Doc12"></dx:GridViewDataColumn>
<dx:GridViewDataColumn Caption="สำเนาบัตรประชาชนผู้ค้ำประกัน" FieldName="Doc18"></dx:GridViewDataColumn>
<dx:GridViewDataColumn Caption="สำเนาเอกสารติดตามผู้เอาประกัน" FieldName="Doc24"></dx:GridViewDataColumn>
<dx:GridViewDataColumn Caption="สำเนาหนังสือแจ้งบอกเลิกสัญญา" FieldName="Doc22"></dx:GridViewDataColumn>
<dx:GridViewDataColumn Caption="สำเนาเอกสารการชำระค่าเช่าซื้อ" FieldName="Doc23"></dx:GridViewDataColumn>








<dx:GridViewDataColumn FieldName="DocComplete" Caption="ผลตรวจสอบเอกสารสแกน" Visible="false"></dx:GridViewDataColumn>
<dx:GridViewDataColumn FieldName="DateReceivedDocFromClient" Caption="วันรับไฟล์เอกสาร" Visible="false"></dx:GridViewDataColumn>
<dx:GridViewDataColumn FieldName="DateSendDocToInsurer" Caption="วันส่งไฟล์ให้ประกัน" Visible="false"></dx:GridViewDataColumn>
<dx:GridViewDataColumn FieldName="DueDatePayment" Caption="กำหนดการโอนค่าสินไหม" Visible="false"></dx:GridViewDataColumn>
<dx:GridViewDataColumn FieldName="WithdrawalSnt_D" Caption="วันที่ส่งเอกสารให้ประกัน" Visible="false"></dx:GridViewDataColumn>
<dx:GridViewDataColumn FieldName="BookRegistrationNo" Caption="เลขที่ใบเสร็จ" Visible="false"></dx:GridViewDataColumn>
<dx:GridViewDataColumn FieldName="ReceiptRec_D" Caption="วันที่รับใบเสร็จ" Visible="false"></dx:GridViewDataColumn>
<dx:GridViewDataColumn FieldName="ReceiptSnt_D" Caption="วันที่ส่งใบเสร็จให้ประกัน" Visible="false"></dx:GridViewDataColumn>
<dx:GridViewDataColumn FieldName="WithdrawalRec_D" Caption="วันที่รับเอกสารตั้งเบิก" Visible="false"></dx:GridViewDataColumn>
<dx:GridViewDataColumn FieldName="Status" Caption="Status" Visible="false"></dx:GridViewDataColumn>
<dx:GridViewDataColumn FieldName="PolStatus1" Caption="PolStatus1" Visible="false"></dx:GridViewDataColumn>
<dx:GridViewDataColumn FieldName="NotWithdrawal" Caption="ขาดเอกสารตั้งเบิก" Visible="false"></dx:GridViewDataColumn>
<dx:GridViewDataColumn FieldName="NotReceipt" Caption="ขาดใบเสร็จ" Visible="false"></dx:GridViewDataColumn>
<dx:GridViewDataColumn FieldName="cl_Cancel" Caption="ยกเลิก/ปฏิเสธ การเคลม" Visible="false"></dx:GridViewDataColumn>
<dx:GridViewDataColumn FieldName="cl_CancelType" Caption="เนื่องจาก" Visible="false"></dx:GridViewDataColumn>
<dx:GridViewDataColumn FieldName="cl_CancelReason" Caption="เหตุยกเลิกเคลม" Visible="false"></dx:GridViewDataColumn>

                    <%--
                        
                        <dx:GridViewDataColumn Caption="Status" FieldName="Status"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="PolStatus1" FieldName="Status"></dx:GridViewDataColumn>
                        
                         --================ ข้อมูลการเคลม =================
                        --ขาดเอกสารตั้งเบิก NotWithdrawal
                        --ขาดใบเสร็จ NotReceipt


                        --================ ข้อมูลการเคลม =================
                        --ยกเลิก / ปฏิเสธ การเคลม cl_Cancel
                        --เนื่องจาก cl_CancelType
                        --เหตุยกเลิกเคลม cl_CancelReason

                        --%>


                </Columns>

            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter runat="server" ID="GridExporter" GridViewID="gridData" />
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>


<asp:SqlDataSource ID="SqlDataSource_gridData" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="select * from RawData_SummitBI_AppClaim06 where DateMakeContract is null "></asp:SqlDataSource>
