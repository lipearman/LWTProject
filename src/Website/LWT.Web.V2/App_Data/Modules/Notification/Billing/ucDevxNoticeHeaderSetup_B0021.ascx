<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxNoticeHeaderSetup_B0021.ascx.vb" Inherits="Modules_ucDevxNoticeHeaderSetup_B0021" %>
<script>
    function OnGetRowValues(values) {
        var key = values;
        ShowPopupSendMailProcessing(key);
    }
</script>
<script type="text/javascript">
    var fileNumber = 0;
    var fileName = "";
    var startDate = null;
    function UploadControl_OnFileUploadStart() {
        startDate = new Date();
        ClearProgressInfo();
        pcProgress.Show();
    }
    function UploadControl_OnFilesUploadComplete(e) {
        pcProgress.Hide();
        if (e.errorText)
            ShowMessage(e.errorText);
        else if (e.callbackData == "success") {
            //ShowMessage("File uploading has been successfully completed.");
            LoadingPanel.Show();
            ASPxSpreadsheet1.PerformCallback();

        }
    }
    function ShowMessage(message) {
        window.setTimeout(function () { window.alert(message); }, 0);
    }
    // Progress Dialog
    function UploadControl_OnUploadingProgressChanged(args) {
        if (!pcProgress.IsVisible())
            return;
        if (args.currentFileName != fileName) {
            fileName = args.currentFileName;
            fileNumber++;
        }
        SetCurrentFileUploadingProgress(args.currentFileName, args.currentFileUploadedContentLength, args.currentFileContentLength);
        progress1.SetPosition(args.currentFileProgress);
        SetTotalUploadingProgress(fileNumber, args.fileCount, args.uploadedContentLength, args.totalContentLength);
        progress2.SetPosition(args.progress);
        UpdateProgressStatus(args.uploadedContentLength, args.totalContentLength);
    }
    function SetCurrentFileUploadingProgress(fileName, uploadedLength, fileLength) {
        lblFileName.SetText("Current File Progress: " + fileName);
        lblFileName.GetMainElement().title = fileName;
        lblCurrentUploadedFileLength.SetText(GetContentLengthString(uploadedLength) + " / " + GetContentLengthString(fileLength));
    }
    function SetTotalUploadingProgress(number, count, uploadedLength, totalLength) {
        lblUploadedFiles.SetText("Total Progress: " + number + ' of ' + count + " file(s)");
        lblUploadedFileLength.SetText(GetContentLengthString(uploadedLength) + " / " + GetContentLengthString(totalLength));
    }
    function ClearProgressInfo() {
        SetCurrentFileUploadingProgress("", 0, 0);
        progress1.SetPosition(0);
        SetTotalUploadingProgress(0, 0, 0, 0);
        progress2.SetPosition(0);
        lblProgressStatus.SetText('Elapsed time: 00:00:00 &ensp; Estimated time: 00:00:00 &ensp; Speed: ' + GetContentLengthString(0) + '/s');
        fileNumber = 0;
        fileName = "";
    }
    function UpdateProgressStatus(uploadedLength, totalLength) {
        var currentDate = new Date();
        var elapsedDateMilliseconds = currentDate - startDate;
        var speed = uploadedLength / (elapsedDateMilliseconds / 1000);
        var elapsedDate = new Date(elapsedDateMilliseconds);
        var elapsedTime = GetTimeString(elapsedDate);
        var estimatedMilliseconds = Math.floor((totalLength - uploadedLength) / speed) * 1000;
        var estimatedDate = new Date(estimatedMilliseconds);
        var estimatedTime = GetTimeString(estimatedDate);
        var speed = uploadedLength / (elapsedDateMilliseconds / 1000);
        lblProgressStatus.SetText('Elapsed time: ' + elapsedTime + ' &ensp; Estimated time: ' + estimatedTime + ' &ensp; Speed: ' + GetContentLengthString(speed) + '/s');
    }
    function GetContentLengthString(contentLength) {
        var sizeDimensions = ['bytes', 'KB', 'MB', 'GB', 'TB'];
        var index = 0;
        var length = contentLength;
        var postfix = sizeDimensions[index];
        while (length > 1024) {
            length = length / 1024;
            postfix = sizeDimensions[++index];
        }
        var numberRegExpPattern = /[-+]?[0-9]*(?:\.|\,)[0-9]{0,2}|[0-9]{0,2}/;
        var results = numberRegExpPattern.exec(length);
        length = results ? results[0] : Math.floor(length);
        return length.toString() + ' ' + postfix;
    }
    function GetTimeString(date) {
        var timeRegExpPattern = /\d{1,2}:\d{1,2}:\d{1,2}/;
        var results = timeRegExpPattern.exec(date.toUTCString());
        return results ? results[0] : "00:00:00";
    }
</script>

<dx:ASPxGridView runat="server"
    ID="MyGrid"
    SettingsLoadingPanel-Mode="ShowAsPopup"
    ClientInstanceName="MyGrid"
    Width="100%"
    SettingsEditing-EditFormColumnCount="1"
    KeyFieldName="NoticeID"
    SettingsBehavior-ConfirmDelete="true"
    DataSourceID="SqlDataSource_gridData"
    SettingsBehavior-AllowEllipsisInText="true"
    Settings-HorizontalScrollBarMode="Visible">

    <SettingsEditing Mode="EditFormAndDisplayRow" />

    <SettingsPopup>
        <EditForm Width="300" HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
    </SettingsPopup>
    <SettingsCommandButton>
        <UpdateButton Text="Save"></UpdateButton>
    </SettingsCommandButton>
    <SettingsBehavior AllowDragDrop="True" AllowFocusedRow="true"
        ColumnResizeMode="Control" />

    <Styles>
        <Header Font-Bold="true" Font-Underline="true" HorizontalAlign="Center">
        </Header>
    </Styles>


    <Toolbars>
        <dx:GridViewToolbar ItemAlign="Left">
            <Items>


                <dx:GridViewToolbarItem Command="New" />
                <dx:GridViewToolbarItem Command="Edit" />
                <dx:GridViewToolbarItem Command="Delete" />
                <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />

            </Items>
        </dx:GridViewToolbar>
    </Toolbars>

    <ClientSideEvents RowDblClick="function(s, e) {
                        var key = s.GetRowKey(e.visibleIndex);        
                        LoadingPanel.Show();
                        PopupDetails.PerformCallback(key);
                    }     
                     " />

    <Columns>

        <dx:GridViewDataTextColumn FieldName="NoticeID" Width="70" CellStyle-HorizontalAlign="Center" CellStyle-Wrap="False">
            <EditItemTemplate>
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%# Eval("NoticeID")%>'></dx:ASPxLabel>
            </EditItemTemplate>
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataDateColumn FieldName="NoticeDate" Caption="วันที่แจ้ง" CellStyle-Wrap="False">
            <PropertiesDateEdit Width="300" ValidationSettings-RequiredField-IsRequired="true"></PropertiesDateEdit>
        </dx:GridViewDataDateColumn>

        <dx:GridViewDataDateColumn FieldName="DueDate" Caption="วันที่กำหนด" CellStyle-Wrap="False">
            <PropertiesDateEdit Width="300" ValidationSettings-RequiredField-IsRequired="true"></PropertiesDateEdit>
        </dx:GridViewDataDateColumn>


        <dx:GridViewDataTextColumn FieldName="NoticeTitle" CellStyle-Wrap="False" Width="300">
            <PropertiesTextEdit Width="300" ValidationSettings-RequiredField-IsRequired="true"></PropertiesTextEdit>
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="CreationDate" Caption="วันที่สร้าง" SortOrder="Descending" CellStyle-Wrap="False" Width="150" EditFormSettings-Visible="False" />




        <dx:GridViewDataTextColumn FieldName="CreationBy" CellStyle-Wrap="False" EditFormSettings-Visible="False" />
        <dx:GridViewDataTextColumn FieldName="SendDate" Caption="วันที่ส่ง" CellStyle-Wrap="False" EditFormSettings-Visible="False" />
        <dx:GridViewDataTextColumn FieldName="ReSendDate" Caption="ส่งซ้ำ" CellStyle-Wrap="False" EditFormSettings-Visible="False" />






    </Columns>


</dx:ASPxGridView>






<asp:SqlDataSource ID="SqlDataSource_gridData" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * from tblNoticeHeader where NoticeCode=@NoticeCode"
    DeleteCommand="delete from tblNoticeDetail where NoticeID=@NoticeID;
    delete tblNoticeHeader where NoticeID=@NoticeID">
    <SelectParameters>
        <asp:Parameter Name="NoticeCode" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="NoticeID" />
    </DeleteParameters>

</asp:SqlDataSource>





<dx:ASPxPopupControl ID="PopupDetails" runat="server"
    ClientInstanceName="PopupDetails"
    Modal="True"
    Maximized="true"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText=""
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
    FooterText=""
    ShowFooter="false">

    <ClientSideEvents EndCallback="function(s,e){

        

        PopupDetails.SetHeaderText(PopupDetails.cpHeaderDetailsText);
        PopupDetails.Show();
        LoadingPanel.Hide();
        
        }" />

    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl5" runat="server">
            <dx:ASPxFormLayout ID="formPreview" Styles-LayoutGroupBox-Caption-ForeColor="#0000ff"
                SettingsItems-VerticalAlign="Top"
                runat="server"
                Width="100%"
                AlignItemCaptionsInAllGroups="True">
                <Styles>
                    <LayoutItem Caption-Font-Bold="true"></LayoutItem>
                </Styles>
                <Items>
                    <dx:LayoutGroup GroupBoxDecoration="Box" ShowCaption="False" ColCount="2">
                        <Items>
                            <dx:LayoutItem Caption="Title" FieldName="NoticeTitle">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">

                                        <dx:ASPxLabel ID="ASPxLabel1" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="CreationDate" FieldName="CreationDate">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">

                                        <dx:ASPxLabel ID="ASPxLabel2" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="วันที่แจ้ง" FieldName="NoticeDate">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">

                                        <dx:ASPxLabel ID="ASPxLabel3" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="วันที่กำหนดชำระ" FieldName="DueDate">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">

                                        <dx:ASPxLabel ID="ASPxLabel4" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="SendDate" FieldName="SendDate">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">

                                        <dx:ASPxLabel ID="ASPxLabel5" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="ReSendDate" FieldName="ReSendDate">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">

                                        <dx:ASPxLabel ID="ASPxLabel6" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>


                            <dx:LayoutItem ShowCaption="False" ColSpan="2">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
                                        <dx:ASPxGridView runat="server"
                                            ID="MyGridDetails"
                                            SettingsLoadingPanel-Mode="ShowAsPopup"
                                            ClientInstanceName="MyGridDetails"
                                            Width="100%"
                                            KeyFieldName="AgentCode"
                                            SettingsBehavior-ConfirmDelete="true"
                                            DataSourceID="SqlDataSource_gridDetails"
                                            SettingsBehavior-AllowEllipsisInText="true"
                                            Settings-HorizontalScrollBarMode="Visible">
                                            <Settings ShowFooter="True" />
                                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                            <Styles>
                                                <Header Font-Bold="true" Font-Underline="true" HorizontalAlign="Center"></Header>
                                                <Footer Font-Bold="true" Font-Underline="true" HorizontalAlign="Center"></Footer>
                                            </Styles>



                                            <SettingsBehavior AllowDragDrop="True" AllowFocusedRow="true" ColumnResizeMode="Control" />
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
                                                                    Image-IconID="mail_newmail_16x16"
                                                                    ToolTip="นำเข้า" Text="นำเข้า">
                                                                    <ClientSideEvents Click="function(s,e){ 
                                                                                PopupImportData.PerformCallback();
                                                                            }" />
                                                                </dx:ASPxButton>
                                                            </Template>
                                                        </dx:GridViewToolbarItem>

                                                      


                                                        <dx:GridViewToolbarItem BeginGroup="true">
                                                            <Template>
                                                                <dx:ASPxButton ID="ASPxButton5" AutoPostBack="false"
                                                                    runat="server" Border-BorderWidth="0"
                                                                    Image-IconID="edit_delete_16x16"
                                                                    Text="Delete">
                                                                    <ClientSideEvents Click="function(s,e){
                                                                        var key= MyGridDetails.GetSelectedKeysOnPage();
                                                                       
                                                                        if(key=='')
                                                                        {
                                                                            alert('Please Select Agent.');
                                                                        }
                                                                        else
                                                                        {
                                                                           if(confirm('Confirm to delete?'))
                                                                            {
                                                                        LoadingPanel.Show();
                                                                                cbDeleteData.PerformCallback(key);
                                                                            }   
                                                                        }
                                                                        
                                                                        
                                                                        }" />

                                                                </dx:ASPxButton>
                                                            </Template>
                                                        </dx:GridViewToolbarItem>





                                                          <dx:GridViewToolbarItem BeginGroup="true">
                                                            <Template>
                                                                <dx:ASPxButton ID="ASPxButton6" OnClick="GridExportSummary_Click"
                                                                    runat="server" Border-BorderWidth="0"
                                                                    Image-IconID="actions_download_16x16office2013"
                                                                    Text="Export">
                                                                </dx:ASPxButton>
                                                            </Template>
                                                        </dx:GridViewToolbarItem>





                                                        <%--<dx:GridViewToolbarItem Command="Delete" />--%>
                                                       
                                                        <dx:GridViewToolbarItem BeginGroup="true">
                                                            <Template>
                                                                <dx:ASPxButton AutoPostBack="false"
                                                                    runat="server" Border-BorderWidth="0"
                                                                    Image-IconID="mail_mail_16x16office2013"
                                                                    Text="Send Mail">
                                                                    <ClientSideEvents Click="function(s,e){
                                                                        var key= MyGridDetails.GetSelectedKeysOnPage();
                                                                       
                                                                        if(key=='')
                                                                        {
                                                                         alert('Please Select Agent.');
                                                                        }
                                                                        else
                                                                        {
                                                                           if(confirm('Confirm to Send Mail?'))
                                                                            {
                                                                                //LoadingPanel.Show();
                                                                                ShowPopupSendMailProcessing(key);
                                                                            }   
                                                                        }
                                                                        
                                                                        
                                                                        }" />

                                                                </dx:ASPxButton>
                                                            </Template>
                                                        </dx:GridViewToolbarItem>





                                                         <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />

                                                    </Items>
                                                </dx:GridViewToolbar>
                                            </Toolbars>

                                            <ClientSideEvents RowDblClick="function(s, e) {
                                                    var key = s.GetRowKey(e.visibleIndex);        
                                                    LoadingPanel.Show();
                                                    PopupDataDetails.PerformCallback(key);
                                                }     
                                                 " />


                                            <Columns>
                                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" ShowClearFilterButton="true" VisibleIndex="0" SelectAllCheckboxMode="AllPages" />

                                                <dx:GridViewDataTextColumn FieldName="AgentCode" SortOrder="Ascending" Caption="AgentCode" CellStyle-Wrap="False" />
                                                <dx:GridViewDataTextColumn FieldName="AgentName" Width="200" Caption="AgentName" CellStyle-Wrap="False" />
                                                <dx:GridViewDataTextColumn FieldName="f02" Width="200" Caption="Sub Name" CellStyle-Wrap="False" />
                                                <dx:GridViewDataTextColumn FieldName="f10" Width="150" Caption="Net Paid" CellStyle-HorizontalAlign="Right" CellStyle-Wrap="False" />
                                                <dx:GridViewDataTextColumn FieldName="New" Caption="New" CellStyle-HorizontalAlign="Center" CellStyle-Wrap="False" />
                                                <dx:GridViewDataTextColumn FieldName="Renew" Caption="Renew" CellStyle-HorizontalAlign="Center" CellStyle-Wrap="False" />

                                                <%--                                      <dx:GridViewDataColumn Width="50">
                                                    <DataItemTemplate>
                                                        <dx:ASPxButton ID="btnRowSendMail" runat="server"
                                                            Border-BorderWidth="0" AutoPostBack="false"
                                                            Image-IconID="mail_mail_16x16office2013"
                                                            Text="">
                                                            <ClientSideEvents Click="function(s){ 
                                                                            var key = s.cpAgentCode;
                                                                                      
                                                                            if(confirm('Confirm to Send Mail?'))
                                                                            {
                                                                                //LoadingPanel.Show();
                                                                                ShowPopupSendMailProcessing(key);
                                                                            }            
                                                                         }" />
                                                        </dx:ASPxButton>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>--%>
                                            </Columns>
                                            <TotalSummary>
                                                <dx:ASPxSummaryItem FieldName="f10" SummaryType="Sum" DisplayFormat="{0:N2}" />
                                                <dx:ASPxSummaryItem FieldName="New" SummaryType="Sum" DisplayFormat="{0:N0}" />
                                                <dx:ASPxSummaryItem FieldName="Renew" SummaryType="Sum" DisplayFormat="{0:N0}" />
                                            </TotalSummary>
                                        </dx:ASPxGridView>

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

<asp:SqlDataSource ID="SqlDataSource_gridDetails" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * from V_BillingD500Notification where NotificationID=@NoticeID and f01='SUM' "
    DeleteCommand="delete from tblNoticeDetail where NoticeID=@NoticeID and Code=@AgentCode;
    delete tblNoticeHeader where NoticeID=@NoticeID and Code=@AgentCode ">
    <SelectParameters>
        <asp:SessionParameter SessionField="NoticeID" Name="NoticeID" />
    </SelectParameters>
    <DeleteParameters>
        <asp:SessionParameter SessionField="NoticeID" Name="NoticeID" />
        <asp:Parameter Name="AgentCode" />
    </DeleteParameters>

</asp:SqlDataSource>

<dx:ASPxGridViewExporter runat="server" ID="GridExportSummary" GridViewID="MyGridDetails">
    <Styles>
        <Default Font-Names="Tahoma" Font-Size="Medium" Wrap="False"></Default>
    </Styles>
</dx:ASPxGridViewExporter>

<dx:ASPxGridViewExporter runat="server" ID="GridExportData" GridViewID="MyGridDataDetails">
    <Styles>
        <Default Font-Names="Tahoma" Font-Size="Medium" Wrap="False"></Default>
    </Styles>
</dx:ASPxGridViewExporter>


<dx:ASPxPopupControl ID="PopupDataDetails" runat="server"
    ClientInstanceName="PopupDataDetails"
    Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText=""
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
    Height="480"
    FooterText=""
    ShowFooter="false">
    <ClientSideEvents EndCallback="function(s,e){
        PopupDataDetails.SetHeaderText(PopupDataDetails.cpHeaderDataDetailsText);
        PopupDataDetails.Show();
        LoadingPanel.Hide();
        }" />
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">

            <dx:ASPxFormLayout ID="formPreviewData"
                Styles-LayoutGroupBox-Caption-ForeColor="#0000ff"
                SettingsItems-VerticalAlign="Top"
                runat="server"
                Width="100%"
                AlignItemCaptionsInAllGroups="True">
                <Styles>
                    <LayoutItem Caption-Font-Bold="true"></LayoutItem>
                </Styles>
                <Items>

                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">
                                <dx:ASPxGridView runat="server"
                                    ID="MyGridDataDetails" DataSourceID="SqlDataSource_GridDataDetails"
                                    SettingsLoadingPanel-Mode="ShowAsPopup"
                                    ClientInstanceName="MyGridDataDetails"
                                    Width="100%"
                                    KeyFieldName="NoticeDataID"
                                    SettingsBehavior-ConfirmDelete="true"
                                    SettingsBehavior-AllowEllipsisInText="true"
                                    Settings-HorizontalScrollBarMode="Visible">

                                    <Settings ShowGroupPanel="True" ShowFooter="True" ShowGroupFooter="VisibleIfExpanded" />

                                    <SettingsPager Mode="ShowPager" PageSize="10">
                                        <PageSizeItemSettings Visible="false" Items="10, 15, 30, 45" ShowAllItem="false" />
                                    </SettingsPager>
                                    <Styles>
                                        <Header Font-Bold="true" Font-Underline="true" HorizontalAlign="Center"></Header>
                                        <Footer Font-Bold="true" Font-Underline="true" HorizontalAlign="Center"></Footer>
                                    </Styles>
                                    <SettingsBehavior AllowDragDrop="True" AllowFocusedRow="true" ColumnResizeMode="Control" />
                                    <Toolbars>
                                        <dx:GridViewToolbar ItemAlign="Left">
                                            <Items>
                                                <dx:GridViewToolbarItem BeginGroup="true">
                                                    <Template>
                                                        <dx:ASPxButton ID="ASPxButton1" OnClick="GridExportData_Click"
                                                            runat="server" Border-BorderWidth="0"
                                                            Image-IconID="actions_download_16x16office2013"
                                                            Text="Export">
                                                        </dx:ASPxButton>
                                                    </Template>
                                                </dx:GridViewToolbarItem>


                                                <dx:GridViewToolbarItem BeginGroup="true">
                                                    <Template>
                                                        <dx:ASPxButton ID="ASPxButton2" AutoPostBack="false"
                                                            runat="server" Border-BorderWidth="0"
                                                            Image-IconID="mail_mail_16x16office2013"
                                                            Text="Send Mail">
                                                            <ClientSideEvents Click="function(s,e){
 
                                                                           if(confirm('Confirm to Send Mail?'))
                                                                            {
                                                                                MyGridDetails.GetRowValues(MyGridDetails.GetFocusedRowIndex(), 'AgentCode', OnGetRowValues);
                                                                            }   

                                                                        }" />

                                                        </dx:ASPxButton>
                                                    </Template>
                                                </dx:GridViewToolbarItem>

                                                <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />
                                            </Items>
                                        </dx:GridViewToolbar>
                                    </Toolbars>


                                    <Columns>
                                        <%--Dealer Code
    ,วันแจ้งงาน
    ,Dealer
    ,INS
    ,Client Code
    ,DN No
    ,Customer Name
    ,Detail
    , ค่าเบี้ยสุทธิ 
    , ค่าส่งเสริมการขาย
     , หักส่วนต่าง(ให้ส่วนลดลูกค้า) 
    , หักค่าธรรมเนียมตัดบัตรวิริยะ 
    , บวก Vat 7% (Dealer) 
    , หัก WH 3% (LWT) 
    , ค่าส่งเสริมการขายสุทธิ "--%>
                                        <dx:GridViewDataTextColumn FieldName="f01" Caption="New/Renew" />
                                        <dx:GridViewDataTextColumn FieldName="f02" Caption="Dealer Code" />
                                        <dx:GridViewDataTextColumn FieldName="f03" Caption="Dealer" />
                                        <dx:GridViewDataTextColumn FieldName="f04" Caption="วันแจ้งงาน" />
                                        <dx:GridViewDataTextColumn FieldName="f05" Caption="INS" />
                                        <dx:GridViewDataTextColumn FieldName="f06" Caption="Client Code" />
                                        <dx:GridViewDataTextColumn FieldName="f07" Caption="DN No" />
                                        <dx:GridViewDataTextColumn FieldName="f08" Caption="Customer Name" />
                                        <dx:GridViewDataTextColumn FieldName="f09" Caption="Detail" />

                                        <dx:GridViewDataTextColumn FieldName="f10" Caption="ค่าเบี้ยสุทธิ" CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                        <dx:GridViewDataTextColumn FieldName="f11" Caption="ค่าส่งเสริมการขาย"  CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                        <dx:GridViewDataTextColumn FieldName="f12" Caption="หักส่วนต่าง(ให้ส่วนลดลูกค้า)"  CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                        <dx:GridViewDataTextColumn FieldName="f13" Caption="หักค่าธรรมเนียมตัดบัตรวิริยะ"  CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                        <dx:GridViewDataTextColumn FieldName="f14" Caption="บวก Vat 7% (Dealer)"  CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                        <dx:GridViewDataTextColumn FieldName="f15" Caption="หัก WH 3% (LWT)"  CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                        <dx:GridViewDataTextColumn FieldName="f16" Caption="ค่าส่งเสริมการขายสุทธิ"  CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    </Columns>
                                    <TotalSummary>
                                        <%--<dx:ASPxSummaryItem FieldName="f01" SummaryType="Count" DisplayFormat="{0:N0}" />--%>

                                        <dx:ASPxSummaryItem FieldName="f10" SummaryType="Sum" DisplayFormat="{0:N2}" />
                                        <dx:ASPxSummaryItem FieldName="f11" SummaryType="Sum" DisplayFormat="{0:N2}" />
                                        <dx:ASPxSummaryItem FieldName="f12" SummaryType="Sum" DisplayFormat="{0:N2}" />
                                        <dx:ASPxSummaryItem FieldName="f13" SummaryType="Sum" DisplayFormat="{0:N2}" />
                                        <dx:ASPxSummaryItem FieldName="f14" SummaryType="Sum" DisplayFormat="{0:N2}" />
                                        <dx:ASPxSummaryItem FieldName="f15" SummaryType="Sum" DisplayFormat="{0:N2}" />
                                        <dx:ASPxSummaryItem FieldName="f16" SummaryType="Sum" DisplayFormat="{0:N2}" />

                                    </TotalSummary>

                                    <GroupSummary>
                                        <%--<dx:ASPxSummaryItem FieldName="f01" SummaryType="Count" DisplayFormat="{0:N0}" />--%>

                                        <dx:ASPxSummaryItem FieldName="f10" SummaryType="Sum" DisplayFormat="{0:N2}" />
                                        <dx:ASPxSummaryItem FieldName="f11" SummaryType="Sum" DisplayFormat="{0:N2}" />
                                        <dx:ASPxSummaryItem FieldName="f12" SummaryType="Sum" DisplayFormat="{0:N2}" />
                                        <dx:ASPxSummaryItem FieldName="f13" SummaryType="Sum" DisplayFormat="{0:N2}" />
                                        <dx:ASPxSummaryItem FieldName="f14" SummaryType="Sum" DisplayFormat="{0:N2}" />
                                        <dx:ASPxSummaryItem FieldName="f15" SummaryType="Sum" DisplayFormat="{0:N2}" />
                                        <dx:ASPxSummaryItem FieldName="f16" SummaryType="Sum" DisplayFormat="{0:N2}" />
                                    </GroupSummary>
                                </dx:ASPxGridView>

                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                </Items>
            </dx:ASPxFormLayout>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>


<asp:SqlDataSource ID="SqlDataSource_GridDataDetails" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * 
    from tblNoticeDetail where NoticeID=@NoticeID and Code=@AgentCode and [f01] in('NEW','RENEW')  ">
    <SelectParameters>
        <asp:SessionParameter SessionField="NoticeID" Name="NoticeID" />
        <asp:SessionParameter SessionField="AgentCode" Name="AgentCode" />
    </SelectParameters>


</asp:SqlDataSource>

<script type="text/javascript">
    function ShowPopupSendMailProcessing(ID) {
        cbSendMailProcessing.PerformCallback(ID);

    }
</script>

<dx:ASPxCallback ID="cbSendMailProcessing" runat="server" ClientInstanceName="cbSendMailProcessing">
    <ClientSideEvents
        CallbackComplete="function(s, e) { 
            PopupSendMailProcessing.SetContentUrl('applications/Task/B0021.aspx');
            PopupSendMailProcessing.Show();            
        }" />
</dx:ASPxCallback>

<dx:ASPxPopupControl ID="PopupSendMailProcessing" runat="server"
    ClientInstanceName="PopupSendMailProcessing"
    Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="Send Mail Processing..."
    AllowDragging="true"
    AllowResize="false"
    DragElement="Window"
    EnableAnimation="true"
    CloseAction="CloseButton"
    ShowMaximizeButton="true"
    ShowCloseButton="true"
    ShowCollapseButton="false"
    EnableCallbackAnimation="true"
    EnableViewState="true"
    ShowPageScrollbarWhenModal="true"
    ScrollBars="Auto"
    Width="800"
    Height="400"
    FooterText=""
    ShowFooter="false">
    <ClientSideEvents Closing="function(s, e) {
	        e.cancel = !confirm('Do you want to close this popup?');
        }" />

    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>

<dx:ASPxPopupControl ID="PopupImportData"
    runat="server"
    ClientInstanceName="PopupImportData"
    Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    AllowDragging="true"
    AllowResize="false"
    DragElement="Window"
    EnableAnimation="true"
    CloseAction="CloseButton"
    ShowMaximizeButton="true"
    ShowCloseButton="true"
    ShowCollapseButton="false"
    EnableCallbackAnimation="true"
    EnableViewState="true"
    Width="1050"
    Height="500"
    ShowPageScrollbarWhenModal="true"
    ScrollBars="Auto"
    ShowFooter="false">
    <ClientSideEvents EndCallback="function(s,e){
        PopupImportData.Show();
        }" />

    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">




            <table>
                <tr>
                    <td style="vertical-align: top; width: 100px">
                        <dx:ASPxButton ID="ASPxButton3" OnClick="btnDownloadFormat_click"
                            runat="server"
                            Image-IconID="actions_download_16x16office2013"
                            Text="Download Format">
                        </dx:ASPxButton>
                    </td>
                    <td>&nbsp; 
                    </td>
                    <td style="vertical-align: top">
                        <dx:ASPxUploadControl ID="UploadControl" runat="server" ClientInstanceName="UploadControl" Width="330" Height="50"
                            NullText="Click here to browse files..." UploadMode="Advanced" AutoStartUpload="True">
                            <AdvancedModeSettings EnableMultiSelect="false" EnableDragAndDrop="false" />
                            <ValidationSettings MaxFileSize="10000000" ShowErrors="false" AllowedFileExtensions=".xlsx"></ValidationSettings>
                            <ClientSideEvents FilesUploadStart="function(s, e) { UploadControl_OnFileUploadStart(); }"
                                FileUploadComplete="function(s, e) { UploadControl_OnFilesUploadComplete(e); }"
                                UploadingProgressChanged="function(s, e) { UploadControl_OnUploadingProgressChanged(e); }" />
                        </dx:ASPxUploadControl>
                    </td>
                    <td>&nbsp; 
                    </td>
                    <td style="vertical-align: top"></td>



                </tr>
                <tr>
                    <td colspan="5">

                        <b>Note</b>: The size of file selected for upload is limited to 10 MB.
                    </td>


                </tr>
            </table>

            <dx:ASPxSpreadsheet ID="ASPxSpreadsheet1" ClientInstanceName="ASPxSpreadsheet1" runat="server"
                WorkDirectory="~/UploadFiles"
                ActiveTabIndex="0"
                ReadOnly="true" Height="300"
                Border-BorderWidth="1"
                ShowFormulaBar="false"
                RibbonMode="None"
                ShowConfirmOnLosingChanges="false"
                ShowSheetTabs="true">
                <ClientSideEvents
                    EndCallback="function(s,e){
                   LoadingPanel.Hide();
                   }"
                    CallbackError="function(s,e){
                    LoadingPanel.Hide();
                   }" />
            </dx:ASPxSpreadsheet>
            <br />
            <dx:ASPxButton ID="ASPxButton4"
                runat="server" AutoPostBack="false"
                Image-IconID="actions_download_16x16office2013"
                Text="Import Data">
                <ClientSideEvents Click="function(s,e){
                        LoadingPanel.Show();
                        cbUploadData.PerformCallback();
                    }" />
            </dx:ASPxButton>



        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>



<dx:ASPxPopupControl ID="pcProgress" runat="server" ClientInstanceName="pcProgress" Modal="True" HeaderText="Uploading"
    PopupAnimationType="None" CloseAction="None" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="460px"
    AllowDragging="true" ShowPageScrollbarWhenModal="True" ShowCloseButton="False" ShowFooter="True">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server" SupportsDisabledAttribute="True">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 100%;">
                        <div style="overflow: hidden; width: 280px;">
                            <dx:ASPxLabel ID="lblFileName" runat="server" ClientInstanceName="lblFileName" Text=""
                                Wrap="False">
                            </dx:ASPxLabel>
                        </div>
                    </td>
                    <td class="NoWrap" style="text-align: right">
                        <dx:ASPxLabel ID="lblCurrentUploadedFileLength" runat="server" ClientInstanceName="lblCurrentUploadedFileLength"
                            Text="" Wrap="False">
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="TopPadding">
                        <dx:ASPxProgressBar ID="ASPxProgressBar1" runat="server" Height="21px" Width="100%"
                            ClientInstanceName="progress1">
                        </dx:ASPxProgressBar>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="Spacer" style="height: 12px;"></div>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%;">
                        <dx:ASPxLabel ID="lblUploadedFiles" runat="server" ClientInstanceName="lblUploadedFiles" Text=""
                            Wrap="False">
                        </dx:ASPxLabel>
                    </td>
                    <td class="NoWrap" style="text-align: right">
                        <dx:ASPxLabel ID="lblUploadedFileLength" runat="server" ClientInstanceName="lblUploadedFileLength"
                            Text="" Wrap="False">
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="TopPadding">
                        <dx:ASPxProgressBar ID="ASPxProgressBar2" runat="server" CssClass="BottomMargin" Height="21px" Width="100%"
                            ClientInstanceName="progress2">
                        </dx:ASPxProgressBar>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="Spacer" style="height: 12px;"></div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <dx:ASPxLabel ID="lblProgressStatus" runat="server" ClientInstanceName="lblProgressStatus" Text=""
                            Wrap="False">
                        </dx:ASPxLabel>
                    </td>
                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <FooterTemplate>
        <div style="overflow: hidden;">
            <dx:ASPxButton ID="btnCancel" runat="server" AutoPostBack="False" Text="Cancel" ClientInstanceName="btnCancel" Width="100px" Style="float: right">
                <ClientSideEvents Click="function(s, e) { UploadControl.Cancel(); }" />
            </dx:ASPxButton>
        </div>
    </FooterTemplate>
    <FooterStyle>
        <Paddings Padding="5px" PaddingRight="10px" />
    </FooterStyle>
</dx:ASPxPopupControl>





<dx:ASPxCallback ID="cbUploadData" runat="server" ClientInstanceName="cbUploadData">
    <ClientSideEvents
        CallbackComplete="function(s, e) { 
            LoadingPanel.Hide();
            if(e.result !='success'){
                alert(e.result);
            }
            else{
                
                MyGridDetails.Refresh();
                PopupImportData.Hide();
            }
        }" />
</dx:ASPxCallback>




<dx:ASPxCallback ID="cbDeleteData" runat="server" ClientInstanceName="cbDeleteData">
    <ClientSideEvents
        CallbackComplete="function(s, e) { 
            LoadingPanel.Hide();
            if(e.result !='success'){
                alert(e.result);
            }
            else{
                
                MyGridDetails.Refresh();
            }
        }" />
</dx:ASPxCallback>
