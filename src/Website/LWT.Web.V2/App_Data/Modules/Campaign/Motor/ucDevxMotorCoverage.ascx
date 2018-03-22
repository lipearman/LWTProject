﻿<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxMotorCoverage.ascx.vb" Inherits="Modules_ucDevxMotorCoverage" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<script>
    function validatepolicytype() {

        switch (Coverage2.GetValue()) {
            case '1'://ประเภท 1
                Coverage7.SetEnabled(true);
                Coverage8.SetEnabled(true); 
                Coverage9.SetEnabled(true);
                 
                Coverage7.SetText('ตามทุนประกัน');
                Coverage8.SetText('ตามทุนประกัน');
                Coverage9.SetText('-');
                break;
            case '2':	//ประเภท 2
                Coverage7.SetEnabled(false);
                Coverage8.SetEnabled(true);
                Coverage9.SetEnabled(false);
                Coverage7.SetText('-');
                Coverage8.SetText('ตามทุนประกัน');
                Coverage9.SetText('-');
                break;
            case '3':  //ประเภท 3
                Coverage7.SetEnabled(false);
                Coverage8.SetEnabled(false);
                Coverage9.SetEnabled(false);
                Coverage7.SetText('-');
                Coverage8.SetText('-');
                Coverage9.SetText('-');
                break;
            case '5':  //ประเภท 2 พิเศษ
                Coverage7.SetEnabled(true);
                Coverage8.SetEnabled(true);
                Coverage9.SetEnabled(false);
                Coverage7.SetText('ตามทุนประกัน');
                Coverage8.SetText('ตามทุนประกัน');
                Coverage9.SetText('-');
                break;
            case '4':  //ประเภท 3 พิเศษ
                Coverage7.SetEnabled(true);
                Coverage8.SetEnabled(false);
                Coverage9.SetEnabled(false);
                Coverage7.SetText('ตามทุนประกัน');
                Coverage8.SetText('-');
                Coverage9.SetText('-');
                break;
            default:
                Coverage7.SetEnabled(true);
                Coverage8.SetEnabled(true);
                Coverage9.SetEnabled(true);
                Coverage7.SetText('ตามทุนประกัน');
                Coverage8.SetText('ตามทุนประกัน');
                Coverage9.SetText('-');
                break;
        }

    }

</script>

<script type="text/javascript">
    function button1_Click(s, e) {
        if (gridCoverage.IsCustomizationWindowVisible())
            gridCoverage.HideCustomizationWindow();
        else
            gridCoverage.ShowCustomizationWindow();
        UpdateButtonText();
    }
    function grid_CustomizationWindowCloseUp(s, e) {
        UpdateButtonText();
    }
    function UpdateButtonText() {
        var text = gridCoverage.IsCustomizationWindowVisible() ? "Hide" : "Show";
        text += " Customization Window";
        button1.SetText(text);
    }
    </script>
<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Motor Coverage" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>
            <%--            <dx:ASPxButton ID="btnAddCoverage" runat="server" Text="เพิ่มความคุัมครอง" UseSubmitBehavior="false" AutoPostBack="false" Image-IconID="actions_add_16x16">
                <ClientSideEvents Click="function(s){gridCoverage.AddNewRow(); e.processOnServer = false;}" />
            </dx:ASPxButton>--%>
             <dx:ASPxButton ID="btnAddNewCoverage" runat="server" Text="เพิ่ม ความคุัมครอง" Image-IconID="actions_add_16x16">
                <ClientSideEvents Click="function(s, e) {           
                       gridCoverage.AddNewRow();
                       e.processOnServer = false; 
                      }" />
            </dx:ASPxButton>


            <dx:ASPxButton ID="btnCoverageFormat" runat="server" Text="รูปแบบความคุัมครอง" Image-IconID="export_exporttoxlsx_16x16" />
            <dx:ASPxButton ID="btnImportCoverage" runat="server" Text="Import ความคุัมครอง" UseSubmitBehavior="false" AutoPostBack="false" Image-IconID="export_exporttoxlsx_16x16">
                <ClientSideEvents Click="function(s){popUpAddCoverage.Show();tbxdata.SetText(''); e.processOnServer = false;}" />

            </dx:ASPxButton>



<%--            <dx:ASPxButton runat="server" ID="button1" ClientInstanceName="button1" Text="Show Customization Window"
        UseSubmitBehavior="false" AutoPostBack="false">
        <ClientSideEvents Click="button1_Click" />
    </dx:ASPxButton>--%>
            <br />
            <br />
            <dx:ASPxGridView ID="gridCoverage" ClientInstanceName="gridCoverage" runat="server" DataSourceID="SqlDataSource_MotorCoverage"
                KeyFieldName="CoverageID" AutoGenerateColumns="False"
                Width="800">
                <SettingsPager Mode="ShowPager">
                    <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                </SettingsPager>
                <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm"  />
                <SettingsPopup >
                    <EditForm Modal="true" MinWidth="100" Width="400" HorizontalAlign="WindowCenter" VerticalAlign="Middle" />
                    <HeaderFilter Height="200" />
                     <CustomizationWindow VerticalAlign="TopSides" HorizontalAlign="LeftSides" Width="200" Height="400" />
                </SettingsPopup>
                <SettingsDetail ShowDetailRow="false" AllowOnlyOneMasterRowExpanded="false" />
                <SettingsBehavior EnableCustomizationWindow="true" EnableRowHotTrack="true" AutoExpandAllGroups="true" ConfirmDelete="true" />
                <SettingsText Title="Coverage" PopupEditFormCaption="Coverage" />
                <Settings ShowTitlePanel="true" ShowHeaderFilterButton="true" />
               <%-- <SettingsCommandButton>
                    <NewButton ButtonType="Button" Text="เพิ่ม"></NewButton>
                    <UpdateButton ButtonType="Button" Text="บันทึก"></UpdateButton>

                    <EditButton ButtonType="Button" Text="แก้ไข"></EditButton>
                    <DeleteButton ButtonType="Button" Text="ลบ"></DeleteButton>

                    <CancelButton ButtonType="Button" Text="ยกเลิก"></CancelButton>
                </SettingsCommandButton>--%>

                
          
<%--        <SettingsLoadingPanel Mode="ShowOnStatusBar" />
        <SettingsBehavior EnableCustomizationWindow="true" />
        <ClientSideEvents CustomizationWindowCloseUp="grid_CustomizationWindowCloseUp" />--%>
                

                <Columns>
                    <dx:GridViewCommandColumn Width="100" CellStyle-Wrap="False" ShowEditButton="true" ShowDeleteButton="true" />

                    <dx:GridViewDataTextColumn FieldName="CoverageID" Caption="ID" Visible="false" Settings-AllowGroup="False" Settings-AllowHeaderFilter="false" >
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>


                    <%--            <dx:GridViewDataTextColumn FieldName="Editable" Caption=" " VisibleIndex="0" Settings-AllowHeaderFilter="False">
                        <DataItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxButton runat="server" AutoPostBack="false"
                                            RenderMode="Button"
                                            ID="RowEditable"
                                            CommandArgument='<%# Eval("CoverageID")%>'
                                            Text="แก้ไข">
                                            <ClientSideEvents Click="function(s){ 
                                                                            
                                                                            gridCoverage.StartEditRow(s.cpVisibleIndex);                       
                                                                         }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <dx:ASPxButton runat="server" AutoPostBack="false"
                                            RenderMode="Button"
                                            ID="RowDeleteable"
                                            CommandArgument='<%# Eval("CoverageID")%>'
                                            Text="ลบ">
                                            <ClientSideEvents Click="function(s) 
                                                {
                                                    if(confirm('Do you want to delete?'))
                                                    {  
                                                        gridCoverage.DeleteRow(s.cpVisibleIndex);
                                                    }
                                                    e.processOnServer = false;
                                                }
                                            " />
                                        </dx:ASPxButton>

                                    </td>
                                </tr>

                            </table>


                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>--%>

                    <dx:GridViewDataComboBoxColumn FieldName="Coverage1" Settings-AllowGroup="False" Caption="ลักษณะการใช้" CellStyle-Wrap="False">
                        <PropertiesComboBox DataSourceID="SqlDataSource_CarUse"
                            TextField="txtCarUse"
                            ValueField="CarUse">
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataComboBoxColumn FieldName="Coverage2" Settings-AllowGroup="False" Caption="ประเภท">
                        <PropertiesComboBox DataSourceID="SqlDataSource_PolicyType"
                            TextField="Description"
                            ValueField="InsureType">

                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                        </PropertiesComboBox>

                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataComboBoxColumn FieldName="Coverage3" Settings-AllowGroup="False" Caption="ประเภทอู่" CellStyle-Wrap="False">
                        <PropertiesComboBox DataSourceID="SqlDataSource_Garage"
                            TextField="GarageName"
                            ValueField="GarageID">
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>


                    <dx:GridViewDataTextColumn FieldName="Coverage4" Caption="ความรับผิด/คน"
                        Settings-AllowGroup="False"
                        Settings-AllowHeaderFilter="True"
                        PropertiesTextEdit-DisplayFormatString="{0:n0}">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Coverage5" Settings-AllowGroup="False" Caption="ความรับผิด/ครั้ง" Settings-AllowHeaderFilter="True" PropertiesTextEdit-DisplayFormatString="{0:n0}"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Coverage6" Settings-AllowGroup="False" Caption="ทรัพย์สิน" Settings-AllowHeaderFilter="True" PropertiesTextEdit-DisplayFormatString="{0:n0}"></dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="Coverage7" Settings-AllowGroup="False" Caption="ความเสียหายต่อรถยนต์" Settings-AllowHeaderFilter="True" PropertiesTextEdit-DisplayFormatString="{0:n0}"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Coverage8" Settings-AllowGroup="False" Caption="รถยนต์สูญหาย/ไฟไหม้" Settings-AllowHeaderFilter="False" PropertiesTextEdit-DisplayFormatString="{0:n0}"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Coverage9" Settings-AllowGroup="False" Caption="คุ้มครองน้ำท่วม" Settings-AllowHeaderFilter="True" PropertiesTextEdit-DisplayFormatString="{0:n0}"></dx:GridViewDataTextColumn>



                    <dx:GridViewDataTextColumn FieldName="Coverage10" Settings-AllowGroup="False" Caption="PA ผู้ขับขี่" Settings-AllowHeaderFilter="True" PropertiesTextEdit-DisplayFormatString="{0:n0}"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Coverage11" Settings-AllowGroup="False" Caption="ผู้โดยสาร"  Settings-AllowHeaderFilter="False" PropertiesTextEdit-DisplayFormatString="{0:n0}"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Coverage12" Settings-AllowGroup="False" Caption="PA ผู้โดยสาร" Settings-AllowHeaderFilter="True" PropertiesTextEdit-DisplayFormatString="{0:n0}"></dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="Coverage13" Settings-AllowGroup="False" Caption="ค่ารักษาพยาบาล" Settings-AllowHeaderFilter="False" PropertiesTextEdit-DisplayFormatString="{0:n0}"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Coverage14" Settings-AllowGroup="False" Caption="การประกันตัว" Settings-AllowHeaderFilter="False" PropertiesTextEdit-DisplayFormatString="{0:n0}"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Coverage15" Settings-AllowGroup="False" Caption="Deduct" Settings-AllowHeaderFilter="False" PropertiesTextEdit-DisplayFormatString="{0:n0}"></dx:GridViewDataTextColumn>
                    <%--<dx:GridViewDataTextColumn FieldName="Coverage16" Settings-AllowGroup="False" Caption="ระบุผู้ขับขี่" Settings-AllowHeaderFilter="False"  ></dx:GridViewDataTextColumn>--%>



                    
                    <dx:GridViewDataTextColumn FieldName="CreationBy" Settings-AllowGroup="False" Settings-AllowHeaderFilter="false"  Visible="false"  >
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                      <dx:GridViewDataTextColumn FieldName="CreationDate" Settings-AllowGroup="False" CellStyle-Wrap="False"  Visible="false"  Settings-AllowHeaderFilter="false" >
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                </Columns>


                <Templates>
                    <EditForm >
                        


                        <dx:ASPxFormLayout ID="formCoverage" ClientInstanceName="formCoverage" runat="server"
                            Paddings-Padding="0" RequiredMarkDisplayMode="None" 
                            AlignItemCaptionsInAllGroups="True"
                            ShowItemCaptionColon="true" Width="400"
                            CellStyle-Paddings-Padding="0"
                            GroupBoxDecoration="HeadingLine">
                            <ClientSideEvents Init="function(s, e) {
                                                                       validatepolicytype();
                                                                    }" />
                            <Items>
                                <dx:LayoutGroup ShowCaption="False" ColCount="1">
                                    <Items>

                                        <dx:LayoutItem Caption="ข้อเสนอเบี้ยประกันภัยรถยนต์" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer22" runat="server">
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
                                                    <dx:ASPxComboBox ID="Coverage1"
                                                        CaptionCellStyle-Width="180" Caption=" - ลักษณะการใช้" runat="server" Width="170"
                                                        DropDownStyle="DropDownList" Value='<%# Eval("Coverage1")%>'
                                                        DataSourceID="SqlDataSource_CarUse"
                                                        TextField="txtCarUse" ValidationSettings-ValidationGroup="formCoverage"
                                                        ValueField="CarUse">
                                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                        <CaptionSettings RequiredMarkDisplayMode="Hidden" />

                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                                                    <dx:ASPxComboBox ID="Coverage2" CaptionCellStyle-Width="180" Caption=" - ประเภทกรมธรรม์" runat="server" Width="170"
                                                        DropDownStyle="DropDownList" Value='<%# Eval("Coverage2")%>'
                                                        DataSourceID="SqlDataSource_PolicyType" ClientInstanceName="Coverage2"
                                                        TextField="Description" ValidationSettings-ValidationGroup="formCoverage"
                                                        ValueField="InsureType">
                                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                        <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                                         <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                                                       validatepolicytype()
                                                                    }" />
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage3">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server">
                                                    <dx:ASPxComboBox ID="Coverage3" Caption=" - ลักษณะอู่" CaptionCellStyle-Width="180" DisplayFormatString="#,#0" runat="server" Width="170"
                                                        DropDownStyle="DropDownList" Value='<%# Eval("Coverage3")%>'
                                                        DataSourceID="SqlDataSource_Garage"
                                                        TextField="GarageName" ValidationSettings-ValidationGroup="formCoverage"
                                                        ValueField="GarageID">
                                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                        <CaptionSettings RequiredMarkDisplayMode="Hidden" />

                                                    </dx:ASPxComboBox>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
<%--                                         <dx:LayoutItem ShowCaption="False" FieldName="Coverage16">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer26" runat="server">
                                                    <dx:ASPxComboBox ID="Coverage16" Caption=" - ผู้ขับขี่" CaptionCellStyle-Width="180" DisplayFormatString="#,#0" runat="server" Width="170"
                                                        DropDownStyle="DropDownList" Value='<%# Eval("Coverage16")%>'                                                         
                                                        ValidationSettings-ValidationGroup="formCoverage"
                                                        >
                                                        <Items>
                                                            <dx:ListEditItem Text="ไม่ระบุ" Value="0"  />
                                                            <dx:ListEditItem Text="1" Value="1 คน"  />
                                                            <dx:ListEditItem Text="2" Value="2 คน"  />
                                                        </Items>
                                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                        <CaptionSettings RequiredMarkDisplayMode="Hidden" />

                                                    </dx:ASPxComboBox>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>--%>

                                        <dx:LayoutItem Caption="ความรับผิดต่อบุคคลภายนอก" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer21" runat="server">
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>



                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage4">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">
                                                    <dx:ASPxComboBox ID="Coverage4" Caption="- บาดเจ็บหรือเสียชีวิต/คน(บาท)"
                                                        CaptionCellStyle-Width="180"
                                                        runat="server" Width="170"
                                                        DropDownStyle="DropDownList" Value='<%# Eval("Coverage4")%>'
                                                        DataSourceID="SqlDataSource_TPBIP"
                                                        TextField="Coverage" ValidationSettings-ValidationGroup="formCoverage"
                                                        ValueField="Coverage">
                                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                        <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage5">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
                                                    <dx:ASPxComboBox ID="Coverage5" Caption="- บาดเจ็บหรือเสียชัวิต/ครั้ง(บาท)" CaptionCellStyle-Width="180" runat="server" Width="170"
                                                        DropDownStyle="DropDownList" Value='<%# Eval("Coverage5")%>'
                                                        DataSourceID="SqlDataSource_TPBIT"
                                                        TextField="Coverage" ValidationSettings-ValidationGroup="formCoverage"
                                                        ValueField="Coverage">
                                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                        <CaptionSettings RequiredMarkDisplayMode="Hidden" />

                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage6">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">

                                                    <dx:ASPxComboBox ID="Coverage6" Caption="- ทรัพย์สิน(บาท)" CaptionCellStyle-Width="180" runat="server" Width="170"
                                                        DropDownStyle="DropDownList" Value='<%# Eval("Coverage6")%>'
                                                        DataSourceID="SqlDataSource_TPPD"
                                                        TextField="Coverage" ValidationSettings-ValidationGroup="formCoverage"
                                                        ValueField="Coverage">
                                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                        <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                                    </dx:ASPxComboBox>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem Caption="ความเสียหายต่อรถยนต์" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer20" runat="server">
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage7" HorizontalAlign="Left">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">


                                                    <dx:ASPxComboBox ID="Coverage7" Caption=" - ความเสียหายต่อรถยนต์"  ClientInstanceName="Coverage7"
                                                        CaptionCellStyle-Width="180" 
                                                        runat="server" Width="170"
                                                        DropDownStyle="DropDownList" Value='<%# Eval("Coverage7")%>'>
                                                        <Items>
                                                            <dx:ListEditItem Text="-" Value="-" />
                                                            <dx:ListEditItem Text="ตามทุนประกัน" Value="ตามทุนประกัน" />
                                                        </Items>
                                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                        <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                                    </dx:ASPxComboBox>


                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage8" HorizontalAlign="Left">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer24" runat="server">

                                                     <dx:ASPxComboBox ID="Coverage8" Caption=" - รถยนต์สูญหาย/ไฟไหม้" ClientInstanceName="Coverage8"
                                                        CaptionCellStyle-Width="180" 
                                                        runat="server" Width="170"
                                                        DropDownStyle="DropDownList" Value='<%# Eval("Coverage8")%>'>
                                                        <Items>
                                                            <dx:ListEditItem Text="-" Value="-" />
                                                            <dx:ListEditItem Text="ตามทุนประกัน" Value="ตามทุนประกัน" />
                                                        </Items>
                                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                        <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                                    </dx:ASPxComboBox>

                                                     
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage9" HorizontalAlign="Left">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer25" runat="server">


                                                     <dx:ASPxComboBox ID="Coverage9" Caption=" - คุ้มครองน้ำท่วม" ClientInstanceName="Coverage9"
                                                        CaptionCellStyle-Width="180" 
                                                        runat="server" Width="170"
                                                        DropDownStyle="DropDownList" Value='<%# Eval("Coverage9")%>'>
                                                        <Items>
                                                            <dx:ListEditItem Text="-" Value="-" />
                                                            <dx:ListEditItem Text="ตามทุนประกัน" Value="ตามทุนประกัน" />
                                                        </Items>
                                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                        <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                                    </dx:ASPxComboBox>

                                                     
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="ความคุ้มครองตาม ร.ย. 01 การประกันภัยอุบัติเหตุส่วนบุคคล" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer19" runat="server">
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage10">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">
                                                    <dx:ASPxSpinEdit ID="Coverage10" Caption=" - ประกันภัยอุบัติเหตุผู้ขับขี่(บาท)"
                                                        CaptionCellStyle-Width="180" Value='<%# Eval("Coverage10")%>'
                                                        DisplayFormatString="#,#0" ValidationSettings-ValidationGroup="formCoverage"
                                                        NumberType="Integer" MinValue="0"
                                                        AllowMouseWheel="false"
                                                        SpinButtons-ShowIncrementButtons="false"
                                                        runat="server">
                                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                        <CaptionSettings RequiredMarkDisplayMode="Hidden" />

                                                    </dx:ASPxSpinEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage11">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server">
                                                    <dx:ASPxSpinEdit ID="Coverage11" ValidationSettings-ValidationGroup="formCoverage"
                                                        Caption=" - จำนวนผู้โดยสาร(คน)" Value='<%# Eval("Coverage11")%>'
                                                        CaptionCellStyle-Width="180"
                                                        DisplayFormatString="#,#0"
                                                        NumberType="Integer" MinValue="0"
                                                        AllowMouseWheel="false"
                                                        SpinButtons-ShowIncrementButtons="false" runat="server">
                                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                        <CaptionSettings RequiredMarkDisplayMode="Hidden" />

                                                    </dx:ASPxSpinEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage12">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server">
                                                    <dx:ASPxSpinEdit ID="Coverage12" ValidationSettings-ValidationGroup="formCoverage"
                                                        Caption=" - ประกันภัยอุบัติเหตุผู้โดยสาร(บาท)" Value='<%# Eval("Coverage12")%>'
                                                        CaptionCellStyle-Width="180" DisplayFormatString="#,#0" NumberType="Integer" MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" runat="server">
                                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                        <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                                    </dx:ASPxSpinEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>



                                        <dx:LayoutItem Caption="ความคุ้มครองตาม ร.ย. 02 การประกันภัยค่ารักษาพยาบาล" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer18" runat="server">
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>



                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage13">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server">
                                                     
                                                     <dx:ASPxComboBox ID="Coverage13" Caption=" - ค่ารักษาพยาบาล(บาท)" CaptionCellStyle-Width="180" runat="server" Width="170"
                                                        DropDownStyle="DropDownList" Value='<%# Eval("Coverage13")%>'
                                                        DataSourceID="SqlDataSource_RY02"
                                                        TextField="Coverage" ValidationSettings-ValidationGroup="formCoverage"
                                                        ValueField="Coverage">
                                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                        <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                                    </dx:ASPxComboBox>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem Caption="ความคุ้มครองตาม ร.ย. 03 การประกันตัวผู้ขับขี่" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer17" runat="server">
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage14">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server">
                                                    <dx:ASPxSpinEdit ID="Coverage14" ValidationSettings-ValidationGroup="formCoverage"
                                                        Caption=" - การประกันตัวผู้ขับขี่(บาท)" Value='<%# Eval("Coverage14")%>'
                                                        CaptionCellStyle-Width="180"
                                                        DisplayFormatString="#,#0" NumberType="Integer" MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" runat="server">
                                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                        <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                                    </dx:ASPxSpinEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="ความเสียหายส่วนแรก (Deductible)" CaptionStyle-Font-Bold="true">

                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server">
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>

                                        </dx:LayoutItem>

                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage15">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server">
                                                    <dx:ASPxSpinEdit ID="Coverage15" ValidationSettings-ValidationGroup="formCoverage"
                                                        Caption=" - ความเสียหายส่วนแรก(บาท) " Value='<%# Eval("Coverage15")%>'
                                                        CaptionCellStyle-Width="180" DisplayFormatString="#,#0"
                                                        NumberType="Integer" MinValue="0" AllowMouseWheel="false"
                                                        SpinButtons-ShowIncrementButtons="false" runat="server">
                                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                        <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                                    </dx:ASPxSpinEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>



                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer23" runat="server">

                                                    <%--                                        <dx:ASPxButton ID="btnSaveCoverage" runat="server" Text="Save" ValidationContainerID="formCoverage" CausesValidation="true">
                                            <Image IconID="save_save_16x16"></Image>
                                        </dx:ASPxButton>--%>

                                                    <table style="float: left">
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxButton ID="b1" runat="server" UseSubmitBehavior="false" CausesValidation="true" AutoPostBack="false"
                                                                    Text="Save">
                                                                    <ClientSideEvents Click="function(s){
                                                                                if(ASPxClientEdit.ValidateGroup('formCoverage')) gridCoverage.UpdateEdit();
                                                                               //if(ASPxClientEdit.AreEditorsValid()) gridCoverage.UpdateEdit();                         
                                                                            }
                                                                    
                                                                        " />
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                            <td>
                                                                <dx:ASPxButton ID="b2" runat="server" UseSubmitBehavior="false" AutoPostBack="false"
                                                                    Text="Cancel">
                                                                    <ClientSideEvents Click="function(s){
                                                                        gridCoverage.CancelEdit();
                                                                     }
                                                                        " />
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                        </dx:ASPxFormLayout>




                    </EditForm>
                </Templates>


            </dx:ASPxGridView>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>






<asp:SqlDataSource ID="SqlDataSource_MotorCoverage" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * from V_MotorCoverage where RiskGroupID=@RiskGroupID "
    UpdateCommand="update tblCoverage
    set Coverage1 = @Coverage1
        ,Coverage2= @Coverage2
        ,Coverage3= @Coverage3
        ,Coverage4= @Coverage4
        ,Coverage5= @Coverage5
        ,Coverage6= @Coverage6
        ,Coverage7= @Coverage7
        ,Coverage8= @Coverage8
        ,Coverage9= @Coverage9
        ,Coverage10= replace(convert(varchar,cast(@Coverage10 as money),1), '.00','')
        ,Coverage11= @Coverage11
        ,Coverage12 = replace(convert(varchar,cast(@Coverage12 as money),1), '.00','')
        ,Coverage13= replace(convert(varchar,cast(@Coverage13 as money),1), '.00','')
        ,Coverage14= replace(convert(varchar,cast(@Coverage14 as money),1), '.00','')
        ,Coverage15 = replace(convert(varchar,cast(@Coverage15 as money),1), '.00','') 
        ,ModifyDate = getdate()
        ,ModifyBy=@UserName 
    where CoverageID=@CoverageID"
    DeleteCommand="delete tblCoverage where CoverageID=@CoverageID "
    InsertCommand="
        INSERT INTO tblCoverage
                   (RiskGroupID
                   ,Coverage1
                   ,Coverage2
                   ,Coverage3
                   ,Coverage4
                   ,Coverage5
                   ,Coverage6
                   ,Coverage7
                   ,Coverage8
                   ,Coverage9
                   ,Coverage10
                   ,Coverage11
                    ,Coverage12
                   ,Coverage13
                   ,Coverage14
                    ,Coverage15
                   ,CreationDate
                   ,CreationBy         
                   )
             VALUES
             (@RiskGroupID
               ,@Coverage1
               ,@Coverage2
               ,@Coverage3
               ,@Coverage4
               ,@Coverage5
               ,@Coverage6
               ,@Coverage7
               ,@Coverage8
               ,@Coverage9
                ,replace(convert(varchar,cast(@Coverage10 as money),1), '.00','')
                ,@Coverage11
                ,replace(convert(varchar,cast(@Coverage12 as money),1), '.00','') 
                ,replace(convert(varchar,cast(@Coverage13 as money),1), '.00','')
                ,replace(convert(varchar,cast(@Coverage14 as money),1), '.00','')
                ,replace(convert(varchar,cast(@Coverage15 as money),1), '.00','')  
               ,GETDATE()
               ,@UserName         
             )    
    ">

    <InsertParameters>
        <asp:Parameter Name="Coverage1" />
        <asp:Parameter Name="Coverage2" />
        <asp:Parameter Name="Coverage3" />
        <asp:Parameter Name="Coverage4" />
        <asp:Parameter Name="Coverage5" />
        <asp:Parameter Name="Coverage6" />
        <asp:Parameter Name="Coverage7" />
        <asp:Parameter Name="Coverage8" />
        <asp:Parameter Name="Coverage9" />
        <asp:Parameter Name="Coverage10" />
        <asp:Parameter Name="Coverage11" />
        <asp:Parameter Name="Coverage12" />
        <asp:Parameter Name="Coverage13" />
        <asp:Parameter Name="Coverage14" />
        <asp:Parameter Name="Coverage15" />
        <asp:Parameter Name="RiskGroupID" />
        <asp:Parameter Name="UserName" />
    </InsertParameters>

    <UpdateParameters>
        <asp:Parameter Name="CoverageID" />
        <asp:Parameter Name="Coverage1" />
        <asp:Parameter Name="Coverage2" />
        <asp:Parameter Name="Coverage3" />
        <asp:Parameter Name="Coverage4" />
        <asp:Parameter Name="Coverage5" />
        <asp:Parameter Name="Coverage6" />
        <asp:Parameter Name="Coverage7" />
        <asp:Parameter Name="Coverage8" />
        <asp:Parameter Name="Coverage9" />
        <asp:Parameter Name="Coverage10" />
        <asp:Parameter Name="Coverage11" />
        <asp:Parameter Name="Coverage12" />
        <asp:Parameter Name="Coverage13" />
        <asp:Parameter Name="Coverage14" />
        <asp:Parameter Name="Coverage15" />
        <asp:Parameter Name="UserName" />
    </UpdateParameters>
    <DeleteParameters>
        <asp:Parameter Name="CoverageID" />
    </DeleteParameters>

    <SelectParameters>
        <asp:Parameter Name="RiskGroupID" />
    </SelectParameters>
</asp:SqlDataSource>


<dx:ASPxPopupControl ID="popUpAddCoverage"
    ClientInstanceName="popUpAddCoverage"
    runat="server" Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="Coverage" Border-BorderWidth="0"
    AllowDragging="true" Width="300"
    EnableAnimation="true" CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="false"
    ShowPageScrollbarWhenModal="true">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">

            <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Border-BorderWidth="0"
                Paddings-Padding="0" AlignItemCaptionsInAllGroups="True"
                RequiredMarkDisplayMode="None" ShowItemCaptionColon="true" Width="400"
                CellStyle-Paddings-Padding="0" GroupBoxDecoration="HeadingLine">
                <Items>
                    <dx:LayoutGroup ShowCaption="False">
                        <Items>

                            <dx:LayoutItem ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">

                                        <dx:ASPxMemo runat="server" ID="tbxdata" Width="400" Height="300" ClientInstanceName="tbxdata">
                                            <ValidationSettings>
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxMemo>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">

                                        <dx:ASPxButton ID="btnImportCoverageData" runat="server" ValidationContainerID="popUpAddCoverage" Image-IconID="DiskUpload"
                                            Text="Import" Width="100">
                                            <Image IconID="export_exporttoxlsx_16x16"></Image>
                                        </dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                </Items>
            </dx:ASPxFormLayout>





        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>


<%--<asp:SqlDataSource ID="SqlDataSource_ProjectRisk" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand=" SELECT  RTRIM([RiskID]) AS RiskID ,rtrim([ProjectName]) + ' - ' + RTRIM([Risk]) as Name 
    FROM [CPS].[dbo].[V_ProjectRisk] where Risk like 'MV' and AECode in(select AECode from tblProjectUserAssignment where UserName=@UserName)
    order by [ProjectName] , [Risk] ">
    <SelectParameters>
        <asp:Parameter DefaultValue="" Name="UserName" />
    </SelectParameters>

</asp:SqlDataSource>--%>

<asp:SqlDataSource ID="SqlDataSource_Garage" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand=" SELECT  * from tblGarageType order by ShowID "></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource_PolicyType" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand=" SELECT  * from tblPolicyType "></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource_CarUse" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>" SelectCommand="select CarUse, CarUse + ' - ' + txtCarUse as  txtCarUse from tblCarUse order by CarUse"></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_TPBIP" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>" SelectCommand="select '0' as Coverage, 0 as Orderby union  SELECT replace(convert(varchar,cast(Coverage as money),1), '.00','') Coverage, Coverage as Orderby FROM tblCoverListMaster where CoverType='TPBIP' order by Orderby "></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource_TPBIT" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>" SelectCommand="select '0' as Coverage, 0 as Orderby union  SELECT replace(convert(varchar,cast(Coverage as money),1), '.00','') Coverage, Coverage as Orderby FROM tblCoverListMaster where CoverType='TPBIT' order by Orderby "></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource_TPPD" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>" SelectCommand="select '0' as Coverage, 0 as Orderby union  SELECT replace(convert(varchar,cast(Coverage as money),1), '.00','') Coverage, Coverage as Orderby FROM tblCoverListMaster where CoverType='TPPD' order by Orderby "></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource_RY02" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>" SelectCommand="select '0' as Coverage, 0 as Orderby union  SELECT replace(convert(varchar,cast(Coverage as money),1), '.00','') Coverage, Coverage as Orderby FROM tblCoverListMaster where CoverType='RY02' order by Orderby "></asp:SqlDataSource>
