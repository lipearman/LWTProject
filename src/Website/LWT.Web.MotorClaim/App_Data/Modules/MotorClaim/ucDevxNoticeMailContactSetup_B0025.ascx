<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxNoticeMailContactSetup_B0025.ascx.vb" Inherits="Modules_ucDevxNoticeMailContactSetup_B0025" %>


<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="บริษัทประกันภัย" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>




            <dx:ASPxPageControl ID="TabPage" runat="server" ActiveTabIndex="0" EnableHierarchyRecreation="True">
                <TabPages>
                    <dx:TabPage Text="ผู้ติดต่อ">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl1" runat="server">

                                <dx:ASPxButton ID="btnExportData" runat="server" RenderMode="Button"
                                    Width="90px" Text="Export" AutoPostBack="false" CausesValidation="false">
                                    <Image IconID="export_exporttoxlsx_16x16"></Image>
                                </dx:ASPxButton>

                                <dx:ASPxGridView ID="gridData" ClientInstanceName="gridData" runat="server" DataSourceID="SqlDataSource_NoticeMailContact"
                                    SettingsBehavior-ColumnResizeMode="Control"
                                    KeyFieldName="ShowRoomCode" AutoGenerateColumns="False" Width="800">
                                    <SettingsPager Mode="ShowPager">
                                        <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                                    </SettingsPager>
                                    <SettingsBehavior EnableRowHotTrack="true" AllowEllipsisInText="true" />
                                    <SettingsSearchPanel Visible="true" />
                                    <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />

                                    <SettingsPopup>
                                        <EditForm Modal="true" Width="600" HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                    </SettingsPopup>


                                    <Columns>

                                        <dx:GridViewCommandColumn Width="50" CellStyle-Wrap="False" ShowNewButtonInHeader="true" ShowEditButton="true" />
                                        <dx:GridViewDataTextColumn FieldName="DealerCode" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ShowRoomCode" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>

                                        <dx:GridViewDataTextColumn FieldName="ShowRoomName" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ShowRoomNameEN" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="BranchCode" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="BranchName" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                                        
                                        <dx:GridViewDataTextColumn FieldName="BranchNameEN" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="PartsOrderCode" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Address" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Province" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Zipcode" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="TelCode" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="TelNo" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="FaxNo" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Email" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="AddressEN" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ProvinceEN" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Region" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="SalesArea" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Type" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="BP" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Longitude" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Latitude" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="SPDeptCode" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataColumn FieldName="IsActive" Width="100"></dx:GridViewDataColumn>




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



                                                            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Test Send Mail" AutoPostBack="false">
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



<asp:SqlDataSource ID="SqlDataSource_NoticeMailContact" runat="server" ConnectionString="<%$ ConnectionStrings:MotorClaimConnectionString %>"
    SelectCommand="select * from tblShowRoom "
    InsertCommand="insert into tblShowRoom(
     ShowRoomCode
    ,DealerCode
    ,BranchCode
    ,PartsOrderCode
    ,ShowRoomName
    ,BranchName
    ,Address
    ,Province
    ,Zipcode
    ,TelCode
    ,TelNo
    ,FaxNo
    ,Email
    ,ShowRoomNameEN
    ,BranchNameEN
    ,AddressEN
    ,ProvinceEN
    ,Region
    ,SalesArea
    ,Type
    ,BP
    ,Longitude
    ,Latitude
    ,SPDeptCode
    ,IsActive
    ,CreationDate
    ,CreationBy
    ) 

    values(
        @ShowRoomCode
        ,@DealerCode
        ,@BranchCode
        ,@PartsOrderCode
        ,@ShowRoomName
        ,@BranchName
        ,@Address
        ,@Province
        ,@Zipcode
        ,@TelCode
        ,@TelNo
        ,@FaxNo
        ,@Email
        ,@ShowRoomNameEN
        ,@BranchNameEN
        ,@AddressEN
        ,@ProvinceEN
        ,@Region
        ,@SalesArea
        ,@Type
        ,@BP
        ,@Longitude
        ,@Latitude
        ,@SPDeptCode
        ,1
        ,getdate()
        ,@UserName
    )
    "
    UpdateCommand="UpDate tblShowRoom set
     DealerCode = @DealerCode
    ,BranchCode = @BranchCode
    ,PartsOrderCode = @PartsOrderCode
    ,ShowRoomName = @ShowRoomName
    ,BranchName = @BranchName
    ,Address = @Address
    ,Province = @Province
    ,Zipcode = @Zipcode
    ,TelCode = @TelCode
    ,TelNo = @TelNo
    ,FaxNo = @FaxNo
    ,Email = @Email
    ,ShowRoomNameEN = @ShowRoomNameEN
    ,BranchNameEN = @BranchNameEN
    ,AddressEN = @AddressEN
    ,ProvinceEN = @ProvinceEN
    ,Region = @Region
    ,SalesArea = @SalesArea
    ,Type = @Type
    ,BP = @BP
    ,Longitude = @Longitude
    ,Latitude = @Latitude
    ,SPDeptCode = @SPDeptCode
    ,IsActive = @IsActive
    ,ModifyBy=@UserName
    ,ModifyDate=getdate()
    where ShowRoomCode = @ShowRoomCode ">


    <InsertParameters>
        <asp:Parameter Name="ShowRoomCode" />
        <asp:Parameter Name="DealerCode" />
        <asp:Parameter Name="BranchCode" />
        <asp:Parameter Name="PartsOrderCode" />
        <asp:Parameter Name="ShowRoomName" />
        <asp:Parameter Name="BranchName" />
        <asp:Parameter Name="Address" />
        <asp:Parameter Name="Province" />
        <asp:Parameter Name="Zipcode" />
        <asp:Parameter Name="TelCode" />
        <asp:Parameter Name="TelNo" />
        <asp:Parameter Name="FaxNo" />
        <asp:Parameter Name="Email" />
        <asp:Parameter Name="ShowRoomNameEN" />
        <asp:Parameter Name="BranchNameEN" />
        <asp:Parameter Name="AddressEN" />
        <asp:Parameter Name="ProvinceEN" />
        <asp:Parameter Name="Region" />
        <asp:Parameter Name="SalesArea" />
        <asp:Parameter Name="Type" />
        <asp:Parameter Name="BP" />
        <asp:Parameter Name="Longitude" />
        <asp:Parameter Name="Latitude" />
        <asp:Parameter Name="SPDeptCode" />
        <asp:Parameter Name="IsActive" />
        <asp:Parameter Name="UserName" />
    </InsertParameters>

    <UpdateParameters>
        <asp:Parameter Name="ShowRoomCode" />
        <asp:Parameter Name="DealerCode" />
        <asp:Parameter Name="BranchCode" />
        <asp:Parameter Name="PartsOrderCode" />
        <asp:Parameter Name="ShowRoomName" />
        <asp:Parameter Name="BranchName" />
        <asp:Parameter Name="Address" />
        <asp:Parameter Name="Province" />
        <asp:Parameter Name="Zipcode" />
        <asp:Parameter Name="TelCode" />
        <asp:Parameter Name="TelNo" />
        <asp:Parameter Name="FaxNo" />
        <asp:Parameter Name="Email" />
        <asp:Parameter Name="ShowRoomNameEN" />
        <asp:Parameter Name="BranchNameEN" />
        <asp:Parameter Name="AddressEN" />
        <asp:Parameter Name="ProvinceEN" />
        <asp:Parameter Name="Region" />
        <asp:Parameter Name="SalesArea" />
        <asp:Parameter Name="Type" />
        <asp:Parameter Name="BP" />
        <asp:Parameter Name="Longitude" />
        <asp:Parameter Name="Latitude" />
        <asp:Parameter Name="SPDeptCode" />
        <asp:Parameter Name="IsActive" />
        <asp:Parameter Name="UserName" />
    </UpdateParameters>




</asp:SqlDataSource>
