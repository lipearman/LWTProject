<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxNoticeMailContactSetup_B0028.ascx.vb" Inherits="Modules_ucDevxNoticeMailContactSetup_B0028" %>
 

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="บริษัทประกันภัย" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>




            <dx:ASPxPageControl ID="TabPage" runat="server" ActiveTabIndex="0" EnableHierarchyRecreation="True">
                <TabPages>
                    <dx:TabPage Text="ผู้ติดต่อ">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl1" runat="server">


                                <dx:ASPxButton ID="btnPopup" runat="server" Text="เพิ่มตัวแทน" Image-IconID="actions_add_16x16">
                                    <ClientSideEvents Click="function(s, e) {  
                                
                       popUpAdd.Show();      
                      
                       e.processOnServer = false; 
                      }" />
                                </dx:ASPxButton>
                                  <dx:ASPxButton ID="btnExportData" runat="server" RenderMode="Button"
                Width="90px" Text="Export" AutoPostBack="false" CausesValidation="false">
                <Image IconID="export_exporttoxlsx_16x16"></Image>
            </dx:ASPxButton>


                                <dx:ASPxGridView ID="gridData" ClientInstanceName="gridData" runat="server" DataSourceID="SqlDataSource_NoticeMailContact"
                               SettingsBehavior-ColumnResizeMode="Control"      
                                    KeyFieldName="ID" AutoGenerateColumns="False" Width="800">
                                    <SettingsPager Mode="ShowPager">
                                        <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                                    </SettingsPager>
                                    <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" AllowEllipsisInText="true" />
                                    <SettingsSearchPanel Visible="true" />
                                    <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />

                                <%--    <SettingsCommandButton>
                                        <UpdateButton ButtonType="Button" Text="บันทึก"></UpdateButton>

                                        <EditButton ButtonType="Button" Text="แก้ไข"></EditButton>
                                        <DeleteButton ButtonType="Button" Text="ลบ"></DeleteButton>

                                        <CancelButton ButtonType="Button" Text="ยกเลิก"></CancelButton>
                                    </SettingsCommandButton>--%>


                                    <SettingsPopup>
                                        <EditForm Modal="true" Width="600" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </SettingsPopup>


                                    <Columns>
                                        <dx:GridViewCommandColumn Width="100" CellStyle-Wrap="False" ShowNewButtonInHeader="false" ShowEditButton="true" ShowDeleteButton="true" />
                                        <%--                        <dx:GridViewDataColumn FieldName="AccountExecutive" ReadOnly="true" Width="100" Settings-AutoFilterCondition="BeginsWith">
                            <Settings AllowHeaderFilter="True" HeaderFilterMode="CheckedList" />
                        </dx:GridViewDataColumn>--%>

                                        <%--                    <dx:GridViewDataComboBoxColumn FieldName="Code" CellStyle-Wrap="False" Caption="รหัส">
                        <PropertiesComboBox TextField="Name"
                            ValueField="Underwriter"
                            EnableSynchronization="False"
                            DataSourceID="SqlDataSource_UW">
                            <ValidationSettings Display="Dynamic">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>--%>



                                        <%--                        <dx:GridViewDataTextColumn FieldName="Code"  Caption="รหัส">
                            <PropertiesTextEdit>
                                <ValidationSettings Display="Dynamic">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>--%>



                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="รหัส" ReadOnly="true" Width="50">
                                        </dx:GridViewDataTextColumn>


                                        <dx:GridViewDataTextColumn FieldName="Name" CellStyle-Wrap="False" Caption="ตัวแทน" Width="200" >
                                            <PropertiesTextEdit>
                                                <ValidationSettings Display="Dynamic">
                                                    <RequiredField IsRequired="true" />
                                                </ValidationSettings>
                                            </PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>





                                        <dx:GridViewDataTextColumn FieldName="ContactName" CellStyle-Wrap="False" Caption="ชื่อผู้ติดต่อ"  Width="120">
                                            <PropertiesTextEdit>
                                                <ValidationSettings Display="Dynamic">
                                                    <RequiredField IsRequired="true" />
                                                </ValidationSettings>
                                            </PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>


                                        <dx:GridViewDataTextColumn FieldName="MailTo" CellStyle-Wrap="False" Caption="To"  Width="300">
                                            <PropertiesTextEdit>
                                                <ValidationSettings Display="Dynamic">
                                                    <RequiredField IsRequired="true" />
                                                </ValidationSettings>
                                            </PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>

<%--                                        <dx:GridViewDataTextColumn FieldName="MailCC" Caption="CC"  Width="200">
                                        </dx:GridViewDataTextColumn>



                                        <dx:GridViewDataTextColumn FieldName="CreationDate" Caption="วันที่สร้าง"  Width="100">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataTextColumn>

                                        <dx:GridViewDataTextColumn FieldName="CreationBy" Caption="ผู้สร้าง"  Width="100">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataTextColumn>

                                        <dx:GridViewDataTextColumn FieldName="ModifyDate" Caption="วันที่แก้ไข"  Width="100">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataTextColumn>

                                        <dx:GridViewDataTextColumn FieldName="ModifyBy" Caption="ผู้แก้ไข"  Width="100">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataTextColumn>--%>




                                    </Columns>



                                </dx:ASPxGridView>
                                 <dx:ASPxGridViewExporter runat="server" ID="GridExporter" GridViewID="gridData">
                <Styles>
                    <Default Font-Names="Tahoma" Font-Size="Medium" Wrap="False"></Default>
                </Styles>
            </dx:ASPxGridViewExporter>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Email">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl2" runat="server">



                                <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server">
                                    <Items>
                                        <dx:LayoutGroup Caption="Email">
                                            <Items>
                                                <dx:LayoutItem Caption="Subject">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox ID="MailSubject" Width="600" ValidationSettings-RequiredField-IsRequired="true"
                                                                runat="server" />
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="From">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox ID="MailFrom" Width="600"
                                                                runat="server" />
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
<%--                                                <dx:LayoutItem Caption="To">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox ID="MailTo" Width="600"
                                                                runat="server" />
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>--%>
                                                <dx:LayoutItem Caption="CC">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox ID="MailCC" Width="600"
                                                                runat="server" />
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
<%--                                                <dx:LayoutItem Caption="BCC">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox ID="MailBcc" Width="600"
                                                                runat="server" />
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>--%>
                                                <dx:LayoutItem Caption="Body">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxHtmlEditor ID="MailBody" runat="server" ValidationSettings-RequiredField-IsRequired="true">
                                                            </dx:ASPxHtmlEditor>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption=" ">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxButton runat="server" Text="Save" AutoPostBack="false"
                                                                ValidationContainerID="ASPxFormLayout1">
                                                                <ClientSideEvents Click="function(s, e) {
                                                                    if(ASPxClientEdit.AreEditorsValid()) 
                                                                    {                                              
                                                                        LoadingPanel.Show();
                                                                        cbSaveEmail.PerformCallback('');
                                                                    }
                                                                    else
                                                                    {
                                                                        alert('กรุณากรอกข้อมูลให้ครบ');
                                                                    }
                                                                    e.processOnServer = false;
                                                                }" />


                                                            </dx:ASPxButton>
                                                            <dx:ASPxCallback runat="server" ID="cbSaveEmail" ClientInstanceName="cbSaveEmail">
                                                                <ClientSideEvents 
                                                                    CallbackError="function(s,e){LoadingPanel.Hide(); }" 
                                                                    CallbackComplete="function(s,e){ 
                                                                     LoadingPanel.Hide();   
                                                                     if (e.result != 'success') {
                                                                        alert(e.result);                                                
                                                                     }                                                                     
                                                                     e.processOnServer = false;
                                                                }" />
                                                            </dx:ASPxCallback>



                                                            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Test Send Mail" AutoPostBack="false" >
                                                                <ClientSideEvents Click="function(s, e) {
                                                                    LoadingPanel.Show();
                                                                    cbTestSendEmail.PerformCallback('');
                                                                    e.processOnServer = false;
                                                                }" />


                                                            </dx:ASPxButton>
                                                             <dx:ASPxCallback runat="server" ID="cbTestSendEmail" ClientInstanceName="cbTestSendEmail">
                                                                <ClientSideEvents 
                                                                    CallbackError="function(s,e){LoadingPanel.Hide(); }" 
                                                                    CallbackComplete="function(s,e){ 
                                                                     LoadingPanel.Hide();   
                                                                     if (e.result == 'success') {
                                                                        alert('Send');                                                
                                                                     }                                                                     
                                                                     e.processOnServer = false;
                                                                }" />
                                                            </dx:ASPxCallback>


                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>



                                            </Items>
                                        </dx:LayoutGroup>
                                    </Items>
                                </dx:ASPxFormLayout>





                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>


                </TabPages>
            </dx:ASPxPageControl>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>



<asp:SqlDataSource ID="SqlDataSource_NoticeMailContact" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * from tblNoticeMailContact where NoticeCode = @NoticeCode"
    InsertCommand="insert into tblNoticeMailContact(NoticeCode,Code,Name,MailTo,MailCC,ContactName,IsActive,CreationDate,CreationBy) 
    values(@NoticeCode,@Code,@Name,@MailTo,@MailCC,@ContactName,1,getdate(),@UserName)
    "
    DeleteCommand="delete from tblNoticeMailContact where ID = @ID"
    UpdateCommand="update tblNoticeMailContact set Name=@Name
    ,ContactName=@ContactName
    ,MailTo=@MailTo
    ,MailCC=@MailCC 
    ,ModifyBy=@UserName
    ,ModifyDate=getdate()
    where ID = @ID ">

    <UpdateParameters>
        <asp:Parameter Name="Code" />
        <asp:Parameter Name="Name" />
        <asp:Parameter Name="MailTo" />
        <asp:Parameter Name="MailCC" />
        <asp:Parameter Name="ContactName" />
        <asp:Parameter Name="UserName" />
    </UpdateParameters>
    <InsertParameters>
        <asp:Parameter Name="NoticeCode" />
        <asp:Parameter Name="Code" />
        <asp:Parameter Name="Name" />
        <asp:Parameter Name="MailTo" />
        <asp:Parameter Name="MailCC" />
        <asp:Parameter Name="ContactName" />
        <asp:Parameter Name="UserName" />
    </InsertParameters>


    <SelectParameters>
        <asp:Parameter Name="NoticeCode" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="ID" />
    </DeleteParameters>

</asp:SqlDataSource>

<dx:ASPxPopupControl ID="popUpAdd"
    ClientInstanceName="popUpAdd"
    runat="server" Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="Account Executive"
    AllowDragging="true"
    EnableAnimation="true" CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="true" Width="100px">
    <ClientSideEvents PopUp="function(s,e){ ASPxClientEdit.ClearEditorsInContainerById('frmNew') ;}" />
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">

            <dx:ASPxFormLayout ID="frmNew" ClientInstanceName="frmNew" Width="100px" runat="server" AlignItemCaptionsInAllGroups="True" ColCount="1" RequiredMarkDisplayMode="None" ShowItemCaptionColon="False">

                <Items>

                   <dx:LayoutItem Caption="รหัส">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxTextBox runat="server" Width="300" ID="newCode" ValidationSettings-RequiredField-IsRequired="true">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="ชื่อ">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxTextBox runat="server" Width="300" ID="newName" ValidationSettings-RequiredField-IsRequired="true">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>


                    <dx:LayoutItem Caption="ชื่อผู้ติดต่อ">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxTextBox runat="server" Width="300" ID="newContactName" ValidationSettings-RequiredField-IsRequired="true">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="MailTo">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxTextBox runat="server" Width="300" ID="newMailTo" ValidationSettings-RequiredField-IsRequired="true"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="MailCC">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxTextBox runat="server" Width="300" ID="newMailCC"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxButton ID="btnSaveNew" runat="server" ValidationContainerID="frmNew"
                                    Text="Save" Width="100">
                                    <ClientSideEvents Click="function(s, e) {
                                        if(ASPxClientEdit.AreEditorsValid()) 
                                        {                                              
                                            LoadingPanel.Show();
                                            cbSaveAddNew.PerformCallback('');
                                        }
                                        else
                                        {
                                            alert('กรุณากรอกข้อมูลให้ครบ');
                                        }
                                        e.processOnServer = false;
                                    }" />
                                </dx:ASPxButton>
                                <dx:ASPxCallback runat="server" ID="cbSaveAddNew" ClientInstanceName="cbSaveAddNew">
                                    <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide(); }" CallbackComplete="function(s,e){ 
                                             LoadingPanel.Hide();   
                                             if (e.result != 'success') {
                                                alert(e.result);                                                
                                             } 
                                             else
                                             {
                                                popUpAdd.Hide();
                                                gridData.Refresh();
                                                
                                             }
                                             e.processOnServer = false;

                                        }" />
                                </dx:ASPxCallback>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                </Items>
            </dx:ASPxFormLayout>



        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>


 


