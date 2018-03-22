<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxNotification.ascx.vb" Inherits="Modules_ucDevxNotification" %>
<%@ Register Src="~/App_Data/UserControls/UploadedFilesContainer.ascx" TagPrefix="uc1" TagName="UploadedFilesContainer" %>


<script type="text/javascript">
    function OnContextMenuItemClick(sender, args) {


        if (args.item.name == "ExportToPDF" || args.item.name == "ExportToXLS") {
            args.processOnServer = true;
            args.usePostBack = true;
        }
        else if (args.item.name == "SumSelected")
            args.processOnServer = true;
        else {

            if (args.item.name == "OpenRow") {
                var index = Grid.GetFocusedRowIndex();
                var key = Grid.GetRowKey(index);
                alert(key);
                popupViewForm.Show();

            }
            else {
                //alert(args.item.name);
            }

            args.processOnServer = false;
            args.usePostBack = false;

            return false;
        }
    }
</script>
<script type="text/javascript">
    function OnToolbarItemClick(s, e) {
        if (IsExportToolbarCommand(e.item.name)) {
            e.processOnServer = true;
            e.usePostBack = true;
        }
    }
    function IsExportToolbarCommand(command) {
        return command == "ExportToPDF" || command == "ExportToXLSX" || command == "ExportToXLS";
    }


    function OnPopupNewForm(s, e) {

        popupNewForm.Show();

        //newTabForm.GetTabByName('newPage1').SetEnabled(true);
        //newTabForm.GetTabByName('newPage2').SetEnabled(true);
        //newTabForm.SetActiveTab(0);
        //ASPxClientEdit.ClearEditorsInContainerById('newFormPage2');
        //ASPxClientEdit.ClearEditorsInContainerById('newFormPage1');

    }
</script>

<style>
    .md-fab-wrapper
    {
        position: fixed;
        bottom: 24px;
        right: 24px;
        z-index: 1004;
        -webkit-transition: margin 280ms cubic-bezier(0.4, 0, 0.2, 1);
        transition: margin 280ms cubic-bezier(0.4, 0, 0.2, 1);
    }

    .md-fab.md-fab-accent
    {
        background: #7cb342;
    }

    .md-fab
    {
        box-sizing: border-box;
        width: 64px;
        height: 64px;
        border-radius: 50%;
        background: #fff;
        color: #727272;
        display: block;
        box-shadow: 0 1px 3px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);
        -webkit-transition: box-shadow 280ms cubic-bezier(0.4, 0, 0.2, 1);
        transition: box-shadow 280ms cubic-bezier(0.4, 0, 0.2, 1);
        border: none;
        position: relative;
        text-align: center;
        cursor: pointer;
    }
</style>


<div class="md-fab-wrapper">
    <a class="md-fab md-fab-accent md-fab-wave" href="javascript:OnPopupNewForm();" data-uk-modal="{center:true}">
        <%-- <i class="material-icons">&#xE150;</i>xxx--%>

        <%--<img src="images/Lockton-Logo_250.png" width="64" />--%>

        <i class="glyphicon glyphicon-pencil" style="font-size: 30px; vertical-align: bottom; color: white; top: 15px"></i>
    </a>
</div>

<%--<style type="text/css">
        #dropZone {
            padding: 20px;
            margin: -20px;
        }
        .ResultFileName {
            text-overflow: ellipsis;
        }
        .contentFooter {
            clear: both;
            padding-top: 20px;
        }
    </style>
    <script type="text/javascript">
        var uploadInProgress = false,
            submitInitiated = false,
            uploadErrorOccurred = false;
        uploadedFiles = [];
        function onFileUploadComplete(s, e) {
            var callbackData = e.callbackData.split("|"),
                uploadedFileName = callbackData[0],
                isSubmissionExpired = callbackData[1] === "True";
            uploadedFiles.push(uploadedFileName);
            if (e.errorText.length > 0 || !e.isValid)
                uploadErrorOccurred = true;
            if (isSubmissionExpired && UploadedFilesTokenBox.GetText().length > 0) {
                var removedAfterTimeoutFiles = UploadedFilesTokenBox.GetTokenCollection().join("\n");
                alert("The following files have been removed from the server due to the defined 5 minute timeout: \n\n" + removedAfterTimeoutFiles);
                UploadedFilesTokenBox.ClearTokenCollection();
            }
        }
        function onFileUploadStart(s, e) {
            uploadInProgress = true;
            uploadErrorOccurred = false;
            UploadedFilesTokenBox.SetIsValid(true);
        }
        function onFilesUploadComplete(s, e) {
            uploadInProgress = false;
            for (var i = 0; i < uploadedFiles.length; i++)
                UploadedFilesTokenBox.AddToken(uploadedFiles[i]);
            updateTokenBoxVisibility();
            uploadedFiles = [];
            if (submitInitiated) {
                SubmitButton.SetEnabled(true);
                SubmitButton.DoClick();
            }
        }
        function onSubmitButtonInit(s, e) {
            s.SetEnabled(true);
        }
        function onSubmitButtonClick(s, e) {
            ASPxClientEdit.ValidateGroup();
            if (!formIsValid())
                e.processOnServer = false;
            else if (uploadInProgress) {
                s.SetEnabled(false);
                submitInitiated = true;
                e.processOnServer = false;
            }
        }
        function onTokenBoxValidation(s, e) {
            var isValid = DocumentsUploadControl.GetText().length > 0 || UploadedFilesTokenBox.GetText().length > 0;
            e.isValid = isValid;
            if (!isValid) {
                e.errorText = "No files have been uploaded. Upload at least one file.";
            }
        }
        function onTokenBoxValueChanged(s, e) {
            updateTokenBoxVisibility();
        }
        function updateTokenBoxVisibility() {
            var isTokenBoxVisible = UploadedFilesTokenBox.GetTokenCollection().length > 0;
            UploadedFilesTokenBox.SetVisible(isTokenBoxVisible);
        }
        function formIsValid() {
            return !ValidationSummary.IsVisible() && DescriptionTextBox.GetIsValid() && UploadedFilesTokenBox.GetIsValid() && !uploadErrorOccurred;
        }
    </script>--%>



<%--start highlighted block--%>
<script type="text/javascript">
    function onFileUploadComplete(s, e) {
        if (e.callbackData) {
            var fileData = e.callbackData.split('|');
            var fileName = fileData[0],
                fileUrl = fileData[1],
                fileSize = fileData[2];
            DXUploadedFilesContainer.AddFile(fileName, fileUrl, fileSize);
        }
    }
</script>
<%--end highlighted block--%>



<dx:ASPxHiddenField runat="server" ID="HiddenField" ClientInstanceName="HiddenField" />


<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true"
    HeaderText="" runat="server" HeaderStyle-BackColor="#4796CE" HeaderStyle-ForeColor="White"
    EnableAdaptivity="true" Width="100%"
    HeaderImage-IconID="businessobjects_bonote_32x32">
    <HeaderStyle Height="50" Font-Bold="true" Font-Size="Larger" />
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxGridView runat="server" ID="Grid" ClientInstanceName="Grid" Width="100%" EnableRowsCache="false"
                DataSourceID="SqlDataSource_GridData"
                KeyFieldName="REFNO">

                <Toolbars>
                    <dx:GridViewToolbar Position="Top" ItemAlign="Left">
                        <Items>
                            <dx:GridViewToolbarItem>
                                <Template>
                                    <b>
                                        <dx:ASPxLabel ID="entriesInfo" runat="server" Text="" ClientInstanceName="entriesInfo">
                                        </dx:ASPxLabel>

                                    </b>
                                </Template>
                            </dx:GridViewToolbarItem>
                            <dx:GridViewToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButtonEdit ID="tbToolbarSearch" runat="server" NullText="Search..." Width="300" Height="100%">
                                        <Buttons>
                                            <dx:SpinButtonExtended Image-IconID="find_find_16x16gray" />
                                        </Buttons>
                                    </dx:ASPxButtonEdit>
                                </Template>
                            </dx:GridViewToolbarItem>

                            <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />


                            <dx:GridViewToolbarItem Text="Export to" Image-IconID="actions_download_16x16office2013" BeginGroup="true">
                                <Items>
                                    <dx:GridViewToolbarItem Name="ExportToPDF" Text="PDF" Image-IconID="export_exporttopdf_16x16office2013" />
                                    <dx:GridViewToolbarItem Name="ExportToXLSX" Text="XLSX" Image-IconID="export_exporttoxlsx_16x16office2013" />
                                    <dx:GridViewToolbarItem Name="ExportToXLS" Text="XLS" Image-IconID="export_exporttoxls_16x16office2013" />
                                </Items>

                            </dx:GridViewToolbarItem>




                            <dx:GridViewToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButton ID="ASPxButton1" BeginGroup="true" BackColor="White" Border-BorderWidth="1" Border-BorderColor="#dfdfdf" ForeColor="Black" runat="server" AutoPostBack="false" Image-IconID="actions_add_16x16" Text="New Entry">

                                        <ClientSideEvents Click="OnPopupNewForm" />
                                    </dx:ASPxButton>

                                </Template>
                            </dx:GridViewToolbarItem>

                            <dx:GridViewToolbarItem Command="ShowCustomizationDialog" BeginGroup="true" />

                            <%--<dx:GridViewToolbarItem Command="New"  BeginGroup="true" />--%>
                        </Items>
                    </dx:GridViewToolbar>
                </Toolbars>

                <Columns>

                    <dx:GridViewDataDateColumn FieldName="DateOfIncident"
                        SortOrder="Descending" Visible="true"
                        CellStyle-Wrap="False">
                        <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}"></PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataComboBoxColumn FieldName="CLAIMTYPE" Visible="true">
                        <PropertiesComboBox DataSourceID="SqlDataSource_ClaimType" TextField="Description" ValueField="CLAIMTYPE"></PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>


                    <dx:GridViewDataTextColumn FieldName="LWTREF" Visible="true" CellStyle-Wrap="False" />
                    <dx:GridViewDataTextColumn FieldName="INSURED" Visible="true" CellStyle-Wrap="False" />
                    <dx:GridViewDataTextColumn FieldName="Location" Visible="true" CellStyle-Wrap="False" />
                    <dx:GridViewDataTextColumn FieldName="POLICYNO" Visible="true" CellStyle-Wrap="False" />




                    <dx:GridViewDataTextColumn FieldName="CLAIMMADEBY_TP" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="NOTIFY_DATE_LWT" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="PATIENT_AGE" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="Sex" Visible="false" />

                    <dx:GridViewDataComboBoxColumn FieldName="Nationality" Visible="false">
                        <PropertiesComboBox DataSourceID="SqlDataSource_Nationality" TextField="Description" ValueField="Nationality"></PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataTextColumn FieldName="LOSSDESCRIPTION" Visible="false" />

                    <dx:GridViewDataTextColumn FieldName="CauseOfLoss" Visible="false" />

                    <dx:GridViewDataComboBoxColumn FieldName="CategoryOfMedicalError" Visible="false">
                        <PropertiesComboBox DataSourceID="SqlDataSource_MedicalError" TextField="Description" ValueField="MedicalError"></PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataTextColumn FieldName="Related_Doctors" Visible="false" />

                    <dx:GridViewDataComboBoxColumn FieldName="Medical_Field" Visible="false">
                        <PropertiesComboBox DataSourceID="SqlDataSource_MedicalField" TextField="Description" ValueField="MedicalField"></PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataTextColumn FieldName="FULLORPARTTIME" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="CommentOnPolicyLiability" Visible="false" />

                    <%--  <dx:GridViewDataTextColumn FieldName="RequestClaimByPatient" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="CourtAmount" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="LossReserve" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="TotalClaim" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="Deductible" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="NetClaim" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="Outstanding" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="SettledPaid" Visible="false" />--%>

                    <dx:GridViewDataSpinEditColumn FieldName="RequestClaimByPatient" Visible="true">
                        <PropertiesSpinEdit DisplayFormatString="{0:N0}" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="CourtAmount" Visible="false">
                        <PropertiesSpinEdit DisplayFormatString="{0:N0}" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="LossReserve" Visible="false">
                        <PropertiesSpinEdit DisplayFormatString="{0:N0}" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="TotalClaim" Visible="false">
                        <PropertiesSpinEdit DisplayFormatString="{0:N0}" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="Deductible" Visible="false">
                        <PropertiesSpinEdit DisplayFormatString="{0:N0}" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="NetClaim" Visible="false">
                        <PropertiesSpinEdit DisplayFormatString="{0:N0}" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="Outstanding" Visible="false">
                        <PropertiesSpinEdit DisplayFormatString="{0:N0}" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="SettledPaid" Visible="false">
                        <PropertiesSpinEdit DisplayFormatString="{0:N0}" />
                    </dx:GridViewDataSpinEditColumn>

                    <dx:GridViewDataTextColumn FieldName="Presentstatus" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="UpdatedOn" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="PendingCostingStatus" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="Remark" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="CLAIMYEAR" Visible="false" />

                    <dx:GridViewDataComboBoxColumn FieldName="HOSPITALGROUP" Visible="false">
                        <PropertiesComboBox DataSourceID="SqlDataSource_HospitalGroup" TextField="Description" ValueField="HospitalGroup"></PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataTextColumn FieldName="HospitalRegion" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="HospitalPolicyName" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="CauseOfLossDescription" Visible="false" />








                    <%--
<dx:GridViewDataTextColumn FieldName="ProductName" Visible="false" />
                    <dx:GridViewDataComboBoxColumn FieldName="CategoryID" Caption="Category Name" SortOrder="Descending">
                        <PropertiesComboBox DataSourceID="CategoriesDataSource" ValueType="System.Int32"
                            ValueField="CategoryID" TextField="CategoryName" />
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataTextColumn FieldName="QuantityPerUnit" />

                    <dx:GridViewDataSpinEditColumn FieldName="UnitPrice">
                        <PropertiesSpinEdit DisplayFormatString="c" DisplayFormatInEditMode="true" MinValue="0" MaxValue="60000" />
                    </dx:GridViewDataSpinEditColumn>

                    <dx:GridViewDataSpinEditColumn FieldName="UnitsInStock">
                        <PropertiesSpinEdit MinValue="0" MaxValue="10000" />
                    </dx:GridViewDataSpinEditColumn>

                    <dx:GridViewDataCheckColumn FieldName="Discontinued">
                        <PropertiesCheckEdit AllowGrayed="true" AllowGrayedByClick="false" />
                    </dx:GridViewDataCheckColumn>--%>
                </Columns>

                <%-- <Templates>
            <DetailRow>
                <div style="padding: 3px 3px 2px 3px">
                    <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="true">
                        <TabPages>
                            <dx:TabPage Text="Products" Visible="true">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl1" runat="server">
                                      


                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="Categories" Visible="true">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl2" runat="server">
                                      

                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="Address" Visible="true">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl3" runat="server">
                                       
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                        </TabPages>
                    </dx:ASPxPageControl>
                </div>
            </DetailRow>
        </Templates>
                --%>

                <Settings ShowFilterRowMenu="true" />

                <SettingsDataSecurity AllowDelete="false" AllowEdit="false" AllowInsert="false" />



                <SettingsSearchPanel CustomEditorID="tbToolbarSearch" />

                <SettingsCustomizationDialog Enabled="true" />


                <Settings ShowFooter="false" ShowGroupFooter="VisibleIfExpanded" />

                <SettingsBehavior EnableCustomizationWindow="false" AllowEllipsisInText="True" EnableRowHotTrack="true" ConfirmDelete="true" />

                <SettingsResizing ColumnResizeMode="Control" />

                <%--                <SettingsPopup>
                    <CustomizationWindow Height="400" VerticalAlign="TopSides" />
                </SettingsPopup>--%>

                <SettingsSearchPanel Visible="true" />

                <%--<SettingsDetail ShowDetailRow="true" />--%>

                <SettingsContextMenu Enabled="true">
                    <ColumnMenuItemVisibility ShowFooter="false" ShowFilterRow="false" ShowCustomizationDialog="false" />
                    <RowMenuItemVisibility NewRow="false" Refresh="false"></RowMenuItemVisibility>

                </SettingsContextMenu>

                <Styles>
                    <RowHotTrack BackColor="#4796CE"></RowHotTrack>
                </Styles>
                <ClientSideEvents
                    ToolbarItemClick="OnToolbarItemClick"
                    ContextMenuItemClick="function(s,e) { 
                    OnContextMenuItemClick(s, e); 
                    }"
                    Init="function(s, e) {
	                    var rowCount = s.cpRowCount;
	                    entriesInfo.SetText('All Entries('+rowCount+')');
                    }"
                    RowDblClick="function(s,e){
                        LoadingPanel.Show(); 
                        var key = s.GetRowKey(e.visibleIndex);  
                        popupViewForm.PerformCallback(key);  
                    
                    }"
                    EndCallback="function(s, e) {
	                    var rowCount = s.cpRowCount;
	                    entriesInfo.SetText('All Entries('+rowCount+')');
                    }" />





                <TotalSummary>
                    <dx:ASPxSummaryItem FieldName="TotalClaim" SummaryType="Sum" />
                </TotalSummary>
                <GroupSummary>
                    <dx:ASPxSummaryItem FieldName="TotalClaim" SummaryType="Sum" ShowInGroupFooterColumn="TotalClaim" />
                    <dx:ASPxSummaryItem FieldName="TotalClaim" SummaryType="Min" />
                    <dx:ASPxSummaryItem FieldName="TotalClaim" SummaryType="Max" />
                    <dx:ASPxSummaryItem FieldName="TotalClaim" SummaryType="Count" Visible="false" />
                    <dx:ASPxSummaryItem FieldName="TotalClaim" SummaryType="Average" Visible="false" />
                    <dx:ASPxSummaryItem FieldName="TotalClaim" SummaryType="Sum" Visible="false" />
                </GroupSummary>
            </dx:ASPxGridView>









            <dx:ASPxGridViewExporter runat="server" ID="GridExporter" GridViewID="Grid" />









        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>


<asp:SqlDataSource ID="SqlDataSource_GridData" runat="server" ConnectionString="<%$ ConnectionStrings:BDMSConnectionString %>"
    SelectCommand="
    select
       REFNO
      ,CLAIMTYPE
      ,LWTREF
      ,INSURED
      ,Location
      ,POLICYNO
      ,DateOfIncident
      ,CLAIMMADEBY_TP
      ,NOTIFY_DATE_LWT
      ,PATIENT_AGE
      ,Sex
      ,Nationality
      ,LOSSDESCRIPTION
      ,CauseOfLoss
      ,CategoryOfMedicalError
      ,Related_Doctors
      ,Medical_Field
      ,FULLORPARTTIME
      ,CommentOnPolicyLiability
      ,RequestClaimByPatient
      ,CourtAmount
      ,LossReserve
      ,TotalClaim
      ,Deductible
      ,NetClaim
      ,Outstanding
      ,SettledPaid
      ,Presentstatus
      ,UpdatedOn
      ,PendingCostingStatus
      ,Remark
      ,CLAIMYEAR
      ,HOSPITALGROUP
      ,HospitalRegion
      ,HospitalPolicyName
      ,CauseOfLossDescription
  FROM tblClaim

  order by DateOfIncident desc
    "></asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_ClaimType" runat="server"
    ConnectionString="<%$ ConnectionStrings:BDMSConnectionString %>"
    SelectCommand="select CLAIMTYPE,Description from tblClaimType "></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_Nationality" runat="server"
    ConnectionString="<%$ ConnectionStrings:BDMSConnectionString %>"
    SelectCommand="select Nationality,Description from tblNationality "></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_MedicalError" runat="server"
    ConnectionString="<%$ ConnectionStrings:BDMSConnectionString %>"
    SelectCommand="select MedicalError,Description from tblMedicalError "></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_MedicalField" runat="server"
    ConnectionString="<%$ ConnectionStrings:BDMSConnectionString %>"
    SelectCommand="select MedicalField,Description from tblMedicalField "></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_HospitalGroup" runat="server"
    ConnectionString="<%$ ConnectionStrings:BDMSConnectionString %>"
    SelectCommand="select HospitalGroup,Description from tblHospitalGroup "></asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_Hospital" runat="server"
    ConnectionString="<%$ ConnectionStrings:BDMSConnectionString %>"
    SelectCommand="select HospitalName,Hospital from Hospital "></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_Location" runat="server"
    ConnectionString="<%$ ConnectionStrings:BDMSConnectionString %>"
    SelectCommand="select Location,Province from Location "></asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_Doctor" runat="server"
    ConnectionString="<%$ ConnectionStrings:BDMSConnectionString %>"
    SelectCommand="SELECT [Related_Doctors] as [Doctor]
  FROM [BDMS].[dbo].V_ClaimDetails
  where [Related_Doctors] is not null
  Group by [Related_Doctors]
  order by [Related_Doctors] "></asp:SqlDataSource>


<dx:ASPxPopupControl ID="popupNewForm" runat="server" ClientInstanceName="popupNewForm"
    Modal="True" Maximized="true"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="Medical Malpractice Notification (New)"
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
    HeaderImage-IconID="businessobjects_boreport_32x32"
    HeaderStyle-BackColor="WindowFrame"
    Width="840"
    Height="660"
    FooterText=""
    ShowFooter="false">
    <HeaderStyle BackColor="#4796CE" ForeColor="White" />
    <ClientSideEvents
        PopUp="function(s,e){
          newTabForm.GetTabByName('newPage2').SetEnabled(true);
          ASPxClientEdit.ClearEditorsInContainerById('newFormPage2');
          UploadControl.ClearText();
          newTabForm.GetTabByName('newPage2').SetEnabled(false);
        }"
        Shown="function(s,e){
            newTabForm.GetTabByName('newPage1').SetEnabled(true);
            ASPxClientEdit.ClearEditorsInContainerById('newFormPage1');
            newTabForm.SetActiveTab(0);
        }" />

    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl5" runat="server">



            <dx:ASPxPageControl ID="newTabForm" ClientInstanceName="newTabForm" runat="server" ActiveTabIndex="0" EnableHierarchyRecreation="True">
                <TabPages>
                    <dx:TabPage Text="Page 1" Name="newPage1" ClientEnabled="true" TabImage-IconID="businessobjects_boreport_32x32">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl1" runat="server">



                                <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server"
                                    RequiredMarkDisplayMode="None" ClientInstanceName="newFormPage1"
                                    SettingsItemCaptions-Location="Left"
                                    AlignItemCaptionsInAllGroups="true" Width="100%">
                                    <Items>
                                        <dx:LayoutGroup Caption="Insured" GroupBoxStyle-Caption-Font-Bold="true" GroupBoxStyle-Caption-Font-Size="Medium" GroupBoxDecoration="HeadingLine">
                                            <Items>
                                                <dx:LayoutItem ShowCaption="False" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <table>
                                                                <tr>
                                                                    <td style="padding-right: 5px;">
                                                                        <dx:ASPxComboBox runat="server"
                                                                            DataSourceID="SqlDataSource_ClaimType" TextField="Description" ValueField="CLAIMTYPE"
                                                                            ID="ASPxComboBox2" Caption="Type Of Notifocation" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1"
                                                                            DropDownStyle="DropDownList"
                                                                            EnableClientSideAPI="True"
                                                                            EncodeHtml="false">
                                                                            <ValidationSettings ValidationGroup="newformpage1" Display="None" ErrorDisplayMode="None">
                                                                                <RequiredField IsRequired="true" ErrorText="" />
                                                                            </ValidationSettings>
                                                                            <InvalidStyle BackColor="LightPink" />
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td style="padding-right: 5px;">
                                                                        <dx:ASPxComboBox runat="server"
                                                                            DataSourceID="SqlDataSource_Hospital"
                                                                            TextField="HospitalName"
                                                                            ValueField="Hospital"
                                                                            ID="ASPxComboBox3"
                                                                            Caption="Insured"
                                                                            CaptionSettings-Position="Top"
                                                                            CaptionStyle-Font-Italic="true"
                                                                            CaptionStyle-Font-Size="Smaller"
                                                                            CaptionStyle-Font-Bold="true"
                                                                            CaptionCellStyle-Paddings-Padding="1"
                                                                            DropDownStyle="DropDownList"
                                                                            EnableClientSideAPI="True"
                                                                            EncodeHtml="false">
                                                                            <ValidationSettings ValidationGroup="newformpage1" Display="None" ErrorDisplayMode="None">
                                                                                <RequiredField IsRequired="true" ErrorText="" />
                                                                            </ValidationSettings>
                                                                            <InvalidStyle BackColor="LightPink" />
                                                                        </dx:ASPxComboBox>



                                                                    </td>

                                                                    <td style="padding-right: 5px;">
                                                                        <%-- <dx:ASPxComboBox runat="server"
                                                                            ID="ASPxComboBox4" Caption="Location" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1"
                                                                            DropDownStyle="DropDownList" />--%>


                                                                        <dx:ASPxComboBox runat="server"
                                                                            DataSourceID="SqlDataSource_Location"
                                                                            TextField="Province"
                                                                            ValueField="Location"
                                                                            ID="ASPxComboBox4"
                                                                            Caption="Location"
                                                                            CaptionSettings-Position="Top"
                                                                            CaptionStyle-Font-Italic="true"
                                                                            CaptionStyle-Font-Size="Smaller"
                                                                            CaptionStyle-Font-Bold="true"
                                                                            CaptionCellStyle-Paddings-Padding="1"
                                                                            DropDownStyle="DropDownList"
                                                                            EnableClientSideAPI="True"
                                                                            EncodeHtml="false">
                                                                            <ValidationSettings ValidationGroup="newformpage1" Display="None" ErrorDisplayMode="None">
                                                                                <RequiredField IsRequired="true" ErrorText="" />
                                                                            </ValidationSettings>
                                                                            <InvalidStyle BackColor="LightPink" />
                                                                        </dx:ASPxComboBox>

                                                                    </td>
                                                                    <td style="padding-right: 5px;">
                                                                        <dx:ASPxTextBox runat="server" ID="ASPxTextBox6" Caption="Policy No." CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">

                                                                            <ValidationSettings ValidationGroup="newformpage1" Display="None" ErrorDisplayMode="None">
                                                                                <RequiredField IsRequired="true" ErrorText="" />
                                                                            </ValidationSettings>
                                                                            <InvalidStyle BackColor="LightPink" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem ShowCaption="False" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <table>
                                                                <tr>
                                                                    <td style="padding-right: 5px;">
                                                                        <dx:ASPxTextBox runat="server" ID="ASPxTextBox10" Caption="Title" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">

                                                                            <%--<ValidationSettings ValidationGroup="newformpage1" Display="None" ErrorDisplayMode="None">
                                                                                <RequiredField IsRequired="true" ErrorText="" />
                                                                            </ValidationSettings>
                                                                            <InvalidStyle BackColor="LightPink" />--%>
                                                                        </dx:ASPxTextBox>

                                                                    </td>
                                                                    <td style="padding-right: 5px;">
                                                                        <dx:ASPxTextBox runat="server" ID="ASPxTextBox9" NullText="First" Caption="&nbsp;" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                                            <%--<ValidationSettings ValidationGroup="newformpage1" Display="None" ErrorDisplayMode="None">
                                                                                <RequiredField IsRequired="true" ErrorText="" />
                                                                            </ValidationSettings>
                                                                            <InvalidStyle BackColor="LightPink" />--%>
                                                                        </dx:ASPxTextBox>
                                                                    </td>

                                                                    <td style="padding-right: 5px;">
                                                                        <dx:ASPxTextBox runat="server" ID="ASPxTextBox8" NullText="Middle" Caption="&nbsp;" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                                            <%--<ValidationSettings ValidationGroup="newformpage1" Display="None" ErrorDisplayMode="None">
                                                                                <RequiredField IsRequired="true" ErrorText="" />
                                                                            </ValidationSettings>
                                                                            <InvalidStyle BackColor="LightPink" />--%>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td style="padding-right: 5px;">
                                                                        <dx:ASPxTextBox runat="server" ID="ASPxTextBox7" NullText="Last" Caption="&nbsp;" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                                            <%-- <ValidationSettings ValidationGroup="newformpage1" Display="None" ErrorDisplayMode="None">
                                                                                <RequiredField IsRequired="true" ErrorText="" />
                                                                            </ValidationSettings>
                                                                            <InvalidStyle BackColor="LightPink" />--%>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem ShowCaption="False" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <table>
                                                                <tr>
                                                                    <td style="padding-right: 5px;">
                                                                        <dx:ASPxTextBox runat="server" ID="ASPxTextBox11" NullText="Phone(International)" Caption="Phone" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                                            <%-- <MaskSettings Mask="+(999) 000-0000" IncludeLiterals="None" />
                                                                          <InvalidStyle BackColor="LightPink" />
                                                                            <ValidationSettings Display="None" ErrorDisplayMode="None">
                                                                                  <RegularExpression ValidationExpression="^(\(?\d{3}\)?\-?\d{3}\-?\d{4})$|[0-9]*$|(\(\s*\)\s*-)" />
                                                                            </ValidationSettings>--%>
                                                                        </dx:ASPxTextBox>

                                                                    </td>
                                                                    <td style="padding-right: 5px;">
                                                                        <dx:ASPxTextBox runat="server" ID="ASPxTextBox12" NullText="Email" Caption="Email" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                                            <%--    <ValidationSettings Display="None" ErrorDisplayMode="None">
                                                                           <RegularExpression ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                                                       </ValidationSettings>
                                                                         <InvalidStyle BackColor="LightPink" />--%>
                                                                        </dx:ASPxTextBox>
                                                                    </td>

                                                                    <td style="padding-right: 5px;"></td>
                                                                    <td style="padding-right: 5px;"></td>
                                                                </tr>
                                                            </table>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>


                                            </Items>
                                        </dx:LayoutGroup>
                                        <dx:LayoutGroup Caption="Details Of Claimant" GroupBoxStyle-Caption-Font-Bold="true" GroupBoxStyle-Caption-Font-Size="Medium" GroupBoxDecoration="HeadingLine">
                                            <Items>

                                                <dx:LayoutItem ShowCaption="False" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>


                                                            <table>
                                                                <tr>
                                                                    <td style="padding-right: 5px;">
                                                                        <dx:ASPxTextBox runat="server" ID="ASPxTextBox13" NullText="Title" Caption="Patient Name" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                                        </dx:ASPxTextBox>

                                                                    </td>
                                                                    <td style="padding-right: 5px;">
                                                                        <dx:ASPxTextBox runat="server" ID="ASPxTextBox14" NullText="First" Caption="&nbsp;" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                                        </dx:ASPxTextBox>
                                                                    </td>

                                                                    <td style="padding-right: 5px;">
                                                                        <dx:ASPxTextBox runat="server" ID="ASPxTextBox15" NullText="Middle" Caption="&nbsp;" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td style="padding-right: 5px;">
                                                                        <dx:ASPxTextBox runat="server" ID="ASPxTextBox16" NullText="Last" Caption="&nbsp;" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem ShowCaption="False" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>


                                                            <table>
                                                                <tr>
                                                                    <td style="padding-right: 5px;">
                                                                        <dx:ASPxDateEdit runat="server" ID="ASPxTextBox20" NullText="" Caption="Date Of Birth" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                                        </dx:ASPxDateEdit>

                                                                    </td>
                                                                    <td style="padding-right: 5px;">
                                                                        <dx:ASPxSpinEdit runat="server" ID="ASPxTextBox21" NullText="" Caption="Age" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                                        </dx:ASPxSpinEdit>
                                                                    </td>

                                                                    <td style="padding-right: 5px;">
                                                                        <%-- <dx:ASPxComboBox runat="server" ID="ASPxTextBox22" DropDownStyle="DropDownList" NullText="" Caption="Nationality" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                                        
                                                                        </dx:ASPxComboBox>--%>

                                                                        <dx:ASPxComboBox runat="server"
                                                                            DataSourceID="SqlDataSource_Nationality"
                                                                            TextField="Description"
                                                                            ValueField="Nationality"
                                                                            ID="ASPxComboBox14"
                                                                            Caption="Nationality"
                                                                            CaptionSettings-Position="Top"
                                                                            CaptionStyle-Font-Italic="true"
                                                                            CaptionStyle-Font-Size="Smaller"
                                                                            CaptionStyle-Font-Bold="true"
                                                                            CaptionCellStyle-Paddings-Padding="1"
                                                                            DropDownStyle="DropDownList"
                                                                            EnableClientSideAPI="True"
                                                                            EncodeHtml="false">
                                                                        </dx:ASPxComboBox>


                                                                    </td>
                                                                    <td style="padding-right: 5px;">
                                                                        <dx:ASPxComboBox runat="server" ID="ASPxTextBox23" DropDownStyle="DropDownList" NullText="" Caption="Sex" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="" />
                                                                                <dx:ListEditItem Text="Male" />
                                                                                <dx:ListEditItem Text="Female" />
                                                                            </Items>

                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem ShowCaption="False" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <table>
                                                                <tr>
                                                                    <td style="padding-right: 5px;">
                                                                        <dx:ASPxTextBox runat="server" ID="ASPxTextBox17" NullText="Phone(International)" Caption="Phone" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                                        </dx:ASPxTextBox>

                                                                    </td>
                                                                    <td style="padding-right: 5px;">
                                                                        <dx:ASPxTextBox runat="server" ID="ASPxTextBox18" NullText="Email" Caption="Email" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                                        </dx:ASPxTextBox>
                                                                    </td>

                                                                    <td style="padding-right: 5px;"></td>
                                                                    <td style="padding-right: 5px;"></td>
                                                                </tr>
                                                            </table>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>


                                                <dx:LayoutItem ShowCaption="False" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                            </Items>
                                        </dx:LayoutGroup>
                                    </Items>
                                </dx:ASPxFormLayout>


                                <dx:ASPxButton ID="newNextPage" BeginGroup="true" ValidationGroup="newformpage1" runat="server" AutoPostBack="false" Image-IconID="arrows_next_16x16" Text="Next">

                                    <ClientSideEvents Click="function(s,e){ 
                                       // alert(ASPxClientEdit.ValidateGroup('newformpage1'));
                                                                            if(ASPxClientEdit.ValidateGroup('newformpage1')) 
                                                                            {
                                                                                newTabForm.GetTabByName('newPage1').SetEnabled(false);
                                                                                newTabForm.GetTabByName('newPage2').SetEnabled(true);

                                                                                newTabForm.SetActiveTab(1);
                                                                            }

                                                                        }" />
                                </dx:ASPxButton>

                                <dx:ASPxButton ID="ASPxButton12" BeginGroup="true" runat="server" AutoPostBack="false" Image-IconID="actions_close_16x16devav" Text="Close">

                                    <ClientSideEvents Click="function(s,e){ popupNewForm.Hide(); }" />

                                </dx:ASPxButton>

                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Page 2" Name="newPage2" ClientEnabled="false" TabImage-IconID="businessobjects_boreport_32x32">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl2" runat="server">


                                <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server"
                                    RequiredMarkDisplayMode="Auto" ClientInstanceName="newFormPage2"
                                    SettingsItemCaptions-Location="Left"
                                    AlignItemCaptionsInAllGroups="true" Width="800">
                                    <Items>
                                        <dx:LayoutGroup Caption="Details Of Claims or Circumstances" GroupBoxStyle-Caption-Font-Bold="true" GroupBoxStyle-Caption-Font-Size="Medium" GroupBoxDecoration="HeadingLine">
                                            <Items>
                                                <dx:LayoutItem ShowCaption="False" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <table>
                                                                <tr>
                                                                    <td style="padding-right: 25px;">
                                                                        <dx:ASPxDateEdit runat="server" ID="ASPxDateEdit2" NullText="" Caption="Date Of Incident" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">

                                                                            <ValidationSettings ValidationGroup="newformpage2" SetFocusOnError="true" Display="None" ErrorDisplayMode="None">
                                                                                <RequiredField IsRequired="true" ErrorText="" />
                                                                            </ValidationSettings>
                                                                            <InvalidStyle BackColor="LightPink" />
                                                                        </dx:ASPxDateEdit>

                                                                    </td>
                                                                    <td style="padding-right: 15px;">
                                                                        <dx:ASPxDateEdit runat="server" ID="ASPxDateEdit3" NullText="" Caption="Date Of Claim Made by Patient" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                                            <ValidationSettings ValidationGroup="newformpage2" SetFocusOnError="true" Display="None" ErrorDisplayMode="None">
                                                                                <RequiredField IsRequired="true" ErrorText="" />
                                                                            </ValidationSettings>
                                                                            <InvalidStyle BackColor="LightPink" />

                                                                        </dx:ASPxDateEdit>

                                                                    </td>

                                                                    <td style="padding-right: 15px;">
                                                                        <dx:ASPxDateEdit runat="server" ID="ASPxDateEdit4" NullText="" Caption="OPD/IPD of 1st Admission Date" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">

                                                                            <ValidationSettings ValidationGroup="newformpage2" SetFocusOnError="true" Display="None" ErrorDisplayMode="None">
                                                                                <RequiredField IsRequired="true" ErrorText="" />
                                                                            </ValidationSettings>
                                                                            <InvalidStyle BackColor="LightPink" />
                                                                        </dx:ASPxDateEdit>

                                                                    </td>

                                                                </tr>
                                                            </table>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem ShowCaption="False" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <table>
                                                                <tr>
                                                                    <td style="padding-right: 25px;">

                                                                        <dx:ASPxComboBox runat="server"
                                                                            DataSourceID="SqlDataSource_MedicalError" TextField="Description" ValueField="MedicalError"
                                                                            ID="ASPxComboBox15" Caption="Category of Medical Error" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1"
                                                                            DropDownStyle="DropDownList"
                                                                            EnableClientSideAPI="True"
                                                                            EncodeHtml="false">
                                                                        </dx:ASPxComboBox>

                                                                    </td>
                                                                    <td style="padding-right: 5px;">
                                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" ForeColor="Black" Font-Bold="true" Font-Italic="true" Font-Size="Smaller" Wrap="False" Text="Level of Severlity (How do you rate the complaint in regard of serviousness?)"></dx:ASPxLabel>

                                                                        <table>
                                                                            <tr>
                                                                                <td>Low</td>
                                                                                <td>&nbsp;</td>
                                                                                <td>
                                                                                    <dx:ASPxRatingControl ID="ASPxRatingControl1" Value="0" runat="server" />
                                                                                </td>
                                                                                <td>&nbsp;</td>
                                                                                <td>High</td>

                                                                            </tr>
                                                                        </table>




                                                                    </td>


                                                                </tr>
                                                            </table>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem ShowCaption="False" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxMemo ID="ASPxMemo1" runat="server" Caption="Brief Summary of Incident" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1" Width="100%" Height="50"></dx:ASPxMemo>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>


                                                <dx:LayoutItem ShowCaption="False" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" ForeColor="Black" Font-Bold="true" Font-Italic="true" Font-Size="Smaller" Wrap="False" Text="Related Doctor's Name"></dx:ASPxLabel>
                                                            <dx:ASPxTokenBox ID="ASPxTokenBox1" runat="server" Width="100%" DataSourceID="SqlDataSource_Doctor" TextField="Doctor" ValueField="Doctor">
                                                            </dx:ASPxTokenBox>
                                                            <%-- <dx:ASPxGridView ID="ASPxGridView1" runat="server" SettingsText-EmptyDataRow="No Related Doctor's Name" SettingsBehavior-ConfirmDelete="true" SettingsEditing-Mode="Inline" Paddings-Padding="0" Border-BorderWidth="0"
                                                                AutoGenerateColumns="False" Width="100%">
                                                              
                                                                <Columns>
                                                                    <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowEditButton="true" ShowDeleteButton="true" VisibleIndex="0" />
                                                                    <dx:GridViewDataColumn Caption="Title">
                                                                    </dx:GridViewDataColumn>

                                                                    <dx:GridViewDataTextColumn Caption="FirstName">
                                                                    </dx:GridViewDataTextColumn>


                                                                    <dx:GridViewDataColumn Caption="LastName">
                                                                    </dx:GridViewDataColumn>

                                                                    <dx:GridViewDataComboBoxColumn Caption="Medical Field"></dx:GridViewDataComboBoxColumn>
                                                                </Columns>
                                                                <SettingsEditing EditFormColumnCount="4" />
                                                                <SettingsPager Mode="ShowAllRecords" />
                                                                <Settings ShowTitlePanel="true" /> 
                                                            </dx:ASPxGridView>--%>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem ShowCaption="False" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxMemo ID="ASPxMemo2" runat="server" Caption="Peer Review" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1" Width="100%" Height="50"></dx:ASPxMemo>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>




                                                <dx:LayoutItem ShowCaption="False" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <table>
                                                                <tr>
                                                                    <td style="padding-right: 25px;">
                                                                        <dx:ASPxSpinEdit runat="server" ID="ASPxComboBox5" DecimalPlaces="2" DisplayFormatString="{0:N2}"
                                                                            AllowMouseWheel="false"
                                                                            SpinButtons-ShowIncrementButtons="false" SpinButtons-ShowLargeIncrementButtons="false"
                                                                            NullText="0.00" Caption="Amount of Claim/Demand" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                                        </dx:ASPxSpinEdit>
                                                                    </td>
                                                                    <td style="padding-right: 5px;">



                                                                        <dx:ASPxComboBox runat="server"
                                                                            ID="ASPxComboBox6" Caption="Currency" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1"
                                                                            DropDownStyle="DropDownList">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="" />
                                                                                <dx:ListEditItem Text="THB" />
                                                                                <dx:ListEditItem Text="USD" />
                                                                            </Items>
                                                                        </dx:ASPxComboBox>

                                                                    </td>


                                                                </tr>
                                                            </table>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>


                                            </Items>
                                        </dx:LayoutGroup>
                                        <dx:LayoutGroup Caption="Other Information" GroupBoxStyle-Caption-Font-Bold="true" GroupBoxStyle-Caption-Font-Size="Medium" GroupBoxDecoration="HeadingLine">
                                            <Items>


                                                <dx:LayoutItem ShowCaption="False" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxMemo ID="ASPxMemo6" runat="server" Caption="Please give all other information relevant to this claim" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1" Width="100%" Height="50"></dx:ASPxMemo>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>



                                                <dx:LayoutItem ShowCaption="False">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxLabel ID="ASPxLabel10" runat="server" ForeColor="Black" Font-Bold="true" Font-Italic="true" Font-Size="Smaller" Wrap="False" Text="Please attach copies of supporting correspondeces and/or documents"></dx:ASPxLabel>

                                                            <div class="uploadContainer">
                                                                <dx:ASPxUploadControl ID="UploadControl" runat="server" ClientInstanceName="UploadControl" Width="320"
                                                                    NullText="Browse or Drag files here " UploadMode="Advanced" ShowTextBox="true" ShowAddRemoveButtons="false" ShowUploadButton="false" ShowProgressPanel="True">
                                                                    <AdvancedModeSettings EnableMultiSelect="True" EnableFileList="True" EnableDragAndDrop="True" />
                                                                    <ValidationSettings MaxFileSize="4194304" AllowedFileExtensions=".pdf, .doc, .docx, .xls, .xlsx">
                                                                    </ValidationSettings>
                                                                    <ClientSideEvents FilesUploadStart="function(s, e) { DXUploadedFilesContainer.Clear(); }"
                                                                        FileUploadComplete="onFileUploadComplete" />
                                                                </dx:ASPxUploadControl>


                                                                <p class="note">
                                                                    <dx:ASPxLabel ID="AllowedFileExtensionsLabel" runat="server" Text="Allowed file extensions: .pdf, .doc, .docx, .xls, .xlsx." Font-Size="8pt">
                                                                    </dx:ASPxLabel>
                                                                    <br />
                                                                    <dx:ASPxLabel ID="MaxFileSizeLabel" runat="server" Text="Maximum file size: 4 MB." Font-Size="8pt">
                                                                    </dx:ASPxLabel>
                                                                </p>
                                                            </div>


















                                                            <%--  <div id="dropZone">
                                                                <dx:ASPxUploadControl runat="server" ID="DocumentsUploadControl" ClientInstanceName="DocumentsUploadControl" Width="100%"
                                                                    AutoStartUpload="true" ShowProgressPanel="True" ShowTextBox="false" BrowseButton-Text="Add documents" FileUploadMode="OnPageLoad"
                                                                    >
                                                                    <AdvancedModeSettings EnableMultiSelect="true" EnableDragAndDrop="true" ExternalDropZoneID="dropZone" />
                                                                    <ValidationSettings
                                                                        AllowedFileExtensions=".pdf, .doc, .docx, .xls, .xlsx "
                                                                        MaxFileSize="4194304">
                                                                    </ValidationSettings>
                                                                    <ClientSideEvents
                                                                        FileUploadComplete="onFileUploadComplete"
                                                                        FilesUploadComplete="onFilesUploadComplete"
                                                                        FilesUploadStart="onFileUploadStart" />
                                                                </dx:ASPxUploadControl>
                                                              
                                                                <dx:ASPxTokenBox runat="server" Width="100%" ID="UploadedFilesTokenBox" Border-BorderWidth="0" ClientInstanceName="UploadedFilesTokenBox"
                                                                    NullText="Select the documents to submit" AllowCustomTokens="false" ClientVisible="false">
                                                                    <ClientSideEvents Init="updateTokenBoxVisibility" ValueChanged="onTokenBoxValueChanged" Validation="onTokenBoxValidation" />
                                                                    <ValidationSettings EnableCustomValidation="true" />
                                                                </dx:ASPxTokenBox>
                                                                <br />
                                                                <p class="Note">
                                                                    <dx:ASPxLabel ID="AllowedFileExtensionsLabel" runat="server" Text="Allowed file extensions: .pdf, .doc, .docx, .xls, .xlsx ." Font-Size="8pt" />
                                                                    <br />
                                                                    <dx:ASPxLabel ID="MaxFileSizeLabel" runat="server" Text="Maximum file size: 4 MB." Font-Size="8pt" />
                                                                </p>
                                                                <dx:ASPxValidationSummary runat="server" ID="ValidationSummary" ClientInstanceName="ValidationSummary"
                                                                    RenderMode="Table" Width="250px" ShowErrorAsLink="false" />
                                                            </div>--%>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>




                                            </Items>
                                        </dx:LayoutGroup>
                                    </Items>
                                </dx:ASPxFormLayout>




                                <dx:ASPxButton ID="newBackPage" BeginGroup="true" runat="server" AutoPostBack="false" Image-IconID="arrows_prev_16x16" Text="Back">

                                    <ClientSideEvents Click="function(s,e){

                                            newTabForm.GetTabByName('newPage1').SetEnabled(true);
                                            newTabForm.GetTabByName('newPage2').SetEnabled(false);

                                            newTabForm.SetActiveTab(0);

                                            }" />
                                </dx:ASPxButton>

                                <dx:ASPxButton ID="ASPxButton2" BeginGroup="true" runat="server" AutoPostBack="false" Image-IconID="save_save_16x16" Text="Save as Draft">

                                    <ClientSideEvents Click="function(s,e){ 
                                     
                                                if(ASPxClientEdit.ValidateGroup('newformpage2')) 
                                                {
                                                    if(confirm('Confirm to save draft?'))
                                                     {
                                                         popupNewForm.Hide();
                                                         Grid.Refresh();
                                                     }
                                                }

                                            }" />


                                </dx:ASPxButton>
                                <dx:ASPxButton ID="ASPxButton3" BeginGroup="true" runat="server" AutoPostBack="false" Image-IconID="content_checkbox_16x16" Text="Submit">

                                    <ClientSideEvents Click="function(s,e){ 
                                     
                                                if(ASPxClientEdit.ValidateGroup('newformpage2')) 
                                                {
                                                    if(confirm('Confirm to Submit?'))
                                                     {
                                                        popupNewForm.Hide();
                                                        Grid.Refresh();
                                                     }
                                                }

                                            }" />

                                </dx:ASPxButton>

                                <dx:ASPxButton ID="ASPxButton13" BeginGroup="true" runat="server" AutoPostBack="false" Image-IconID="actions_close_16x16devav" Text="Close">

                                    <ClientSideEvents Click="function(s,e){ 
                                       if(confirm('Confirm to Close Window?'))
                                        { 
                                            popupNewForm.Hide(); 
                                        }
                                        
                                        }" />

                                </dx:ASPxButton>

                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>










        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>




<dx:ASPxPopupControl ID="popupEditForm" runat="server" ClientInstanceName="popupEditForm"
    Modal="True" Maximized="true"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="Medical Malpractice Notification (Edit)"
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
    HeaderImage-IconID="businessobjects_boreport_32x32"
    HeaderStyle-BackColor="WindowFrame"
    Width="840"
    Height="660"
    FooterText=""
    ShowFooter="false">
    <HeaderStyle BackColor="#4796CE" ForeColor="White" />
    <ClientSideEvents
        CloseUp="function(s,e){  
 
        }"
        BeginCallback="function(s,e){ 
            popupEditForm.Show();
        }"
        Shown="function(s,e){
 
            LoadingPanel.Hide(); 

            editTabForm.GetTabByName('editPage1').SetEnabled(true);
            editTabForm.GetTabByName('editPage2').SetEnabled(false);
            editTabForm.SetActiveTab(0);
        }" />




    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">



            <dx:ASPxPageControl ID="ASPxPageControl1" ClientInstanceName="editTabForm" runat="server" ActiveTabIndex="0" EnableHierarchyRecreation="True">
                <TabPages>
                    <dx:TabPage Text="Page 1" Name="editPage1" ClientEnabled="true" TabImage-IconID="businessobjects_boreport_32x32">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl3" runat="server">



                                <dx:ASPxFormLayout ID="editFormPage1" runat="server"
                                    RequiredMarkDisplayMode="None" ClientInstanceName="editFormPage1"
                                    SettingsItemCaptions-Location="Left"
                                    AlignItemCaptionsInAllGroups="true" Width="100%">
                                    <Items>

                                        <dx:LayoutGroup Caption="Insured" ColCount="4" GroupBoxStyle-Caption-Font-Bold="true" GroupBoxStyle-Caption-Font-Size="Medium" GroupBoxDecoration="HeadingLine">
                                            <Items>
                                                <dx:LayoutItem FieldName="CLAIMTYPE" Caption="Type Of Notifocation" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxComboBox runat="server"
                                                                DataSourceID="SqlDataSource_ClaimType" TextField="Description" ValueField="CLAIMTYPE"
                                                                ID="ASPxComboBox1" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1"
                                                                DropDownStyle="DropDownList"
                                                                EnableClientSideAPI="True"
                                                                EncodeHtml="false">
                                                                <ValidationSettings ValidationGroup="editformpage1" Display="None" ErrorDisplayMode="None">
                                                                    <RequiredField IsRequired="true" ErrorText="" />
                                                                </ValidationSettings>
                                                                <InvalidStyle BackColor="LightPink" />
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem Caption="Insured" FieldName="INSURED" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxComboBox runat="server"
                                                                DataSourceID="SqlDataSource_Hospital"
                                                                TextField="HospitalName"
                                                                ValueField="Hospital"
                                                                ID="ASPxComboBox7"
                                                                CaptionSettings-Position="Top"
                                                                CaptionStyle-Font-Italic="true"
                                                                CaptionStyle-Font-Size="Smaller"
                                                                CaptionStyle-Font-Bold="true"
                                                                CaptionCellStyle-Paddings-Padding="1"
                                                                DropDownStyle="DropDownList"
                                                                EnableClientSideAPI="True"
                                                                EncodeHtml="false">
                                                                <ValidationSettings ValidationGroup="editformpage1" Display="None" ErrorDisplayMode="None">
                                                                    <RequiredField IsRequired="true" ErrorText="" />
                                                                </ValidationSettings>
                                                                <InvalidStyle BackColor="LightPink" />
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem Caption="Location" FieldName="Location" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxComboBox runat="server"
                                                                DataSourceID="SqlDataSource_Location"
                                                                TextField="Province"
                                                                ValueField="Location"
                                                                ID="ASPxComboBox8"
                                                                CaptionSettings-Position="Top"
                                                                CaptionStyle-Font-Italic="true"
                                                                CaptionStyle-Font-Size="Smaller"
                                                                CaptionStyle-Font-Bold="true"
                                                                CaptionCellStyle-Paddings-Padding="1"
                                                                DropDownStyle="DropDownList"
                                                                EnableClientSideAPI="True"
                                                                EncodeHtml="false">
                                                                <ValidationSettings ValidationGroup="editformpage1" Display="None" ErrorDisplayMode="None">
                                                                    <RequiredField IsRequired="true" ErrorText="" />
                                                                </ValidationSettings>
                                                                <InvalidStyle BackColor="LightPink" />
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem Caption="Policy No." FieldName="POLICYNO" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox runat="server" ID="ASPxTextBox1" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">

                                                                <ValidationSettings ValidationGroup="editformpage1" Display="None" ErrorDisplayMode="None">
                                                                    <RequiredField IsRequired="true" ErrorText="" />
                                                                </ValidationSettings>
                                                                <InvalidStyle BackColor="LightPink" />
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem Caption="Contact Person" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox runat="server" ID="ASPxTextBox2" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">

                                                                <%--<ValidationSettings ValidationGroup="newformpage1" Display="None" ErrorDisplayMode="None">
                                                                                <RequiredField IsRequired="true" ErrorText="" />
                                                                            </ValidationSettings>
                                                                            <InvalidStyle BackColor="LightPink" />--%>
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem Caption="&nbsp;" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox runat="server" ID="ASPxTextBox3" NullText="First" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                                <%--<ValidationSettings ValidationGroup="newformpage1" Display="None" ErrorDisplayMode="None">
                                                                                <RequiredField IsRequired="true" ErrorText="" />
                                                                            </ValidationSettings>
                                                                            <InvalidStyle BackColor="LightPink" />--%>
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem Caption="&nbsp;" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox runat="server" ID="ASPxTextBox4" NullText="Middle" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                                <%--<ValidationSettings ValidationGroup="newformpage1" Display="None" ErrorDisplayMode="None">
                                                                                <RequiredField IsRequired="true" ErrorText="" />
                                                                            </ValidationSettings>
                                                                            <InvalidStyle BackColor="LightPink" />--%>
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem Caption="&nbsp;" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox runat="server" ID="ASPxTextBox5" NullText="Last" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                                <%-- <ValidationSettings ValidationGroup="newformpage1" Display="None" ErrorDisplayMode="None">
                                                                                <RequiredField IsRequired="true" ErrorText="" />
                                                                            </ValidationSettings>
                                                                            <InvalidStyle BackColor="LightPink" />--%>
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem Caption="Phone" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox runat="server" ID="ASPxTextBox19" NullText="Phone(International)" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                                <%-- <MaskSettings Mask="+(999) 000-0000" IncludeLiterals="None" />
                                                                          <InvalidStyle BackColor="LightPink" />
                                                                            <ValidationSettings Display="None" ErrorDisplayMode="None">
                                                                                  <RegularExpression ValidationExpression="^(\(?\d{3}\)?\-?\d{3}\-?\d{4})$|[0-9]*$|(\(\s*\)\s*-)" />
                                                                            </ValidationSettings>--%>
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem Caption="Email" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox runat="server" ID="ASPxTextBox22" NullText="Email" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                                <%--    <ValidationSettings Display="None" ErrorDisplayMode="None">
                                                                           <RegularExpression ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                                                       </ValidationSettings>
                                                                         <InvalidStyle BackColor="LightPink" />--%>
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:EmptyLayoutItem ColSpan="2"></dx:EmptyLayoutItem>
                                            </Items>
                                        </dx:LayoutGroup>

                                        <dx:LayoutGroup Caption="Details Of Claimant" ColCount="4" GroupBoxStyle-Caption-Font-Bold="true" GroupBoxStyle-Caption-Font-Size="Medium" GroupBoxDecoration="HeadingLine">
                                            <Items>
                                                <dx:LayoutItem Caption="Patient Name" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox runat="server" ID="ASPxTextBox24" NullText="Title" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem Caption="&nbsp;" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox runat="server" ID="ASPxTextBox25" NullText="First" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem Caption="&nbsp;" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox runat="server" ID="ASPxTextBox26" NullText="Middle" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem Caption="&nbsp;" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox runat="server" ID="ASPxTextBox27" NullText="Last" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>




                                                <dx:LayoutItem Caption="Date Of Birth" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxDateEdit runat="server" ID="ASPxDateEdit1" NullText="" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                            </dx:ASPxDateEdit>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem Caption="Age" FieldName="PATIENT_AGE" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxSpinEdit runat="server" ID="ASPxSpinEdit1" NullText="" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                            </dx:ASPxSpinEdit>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem Caption="Nationality" FieldName="Nationality" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxComboBox runat="server"
                                                                DataSourceID="SqlDataSource_Nationality"
                                                                TextField="Description"
                                                                ValueField="Nationality"
                                                                ID="ASPxComboBox9"
                                                                CaptionSettings-Position="Top"
                                                                CaptionStyle-Font-Italic="true"
                                                                CaptionStyle-Font-Size="Smaller"
                                                                CaptionStyle-Font-Bold="true"
                                                                CaptionCellStyle-Paddings-Padding="1"
                                                                DropDownStyle="DropDownList"
                                                                EnableClientSideAPI="True"
                                                                EncodeHtml="false">
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem Caption="Sex" FieldName="Sex" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxComboBox runat="server" ID="ASPxComboBox10" DropDownStyle="DropDownList" NullText="" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                                <Items>
                                                                    <dx:ListEditItem Text="" />
                                                                    <dx:ListEditItem Text="Male" />
                                                                    <dx:ListEditItem Text="Female" />
                                                                </Items>

                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem Caption="Phone" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox runat="server" ID="ASPxTextBox28" NullText="Phone(International)" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                            </dx:ASPxTextBox>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem Caption="Email" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox runat="server" ID="ASPxTextBox29" NullText="Email" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:EmptyLayoutItem ColSpan="2"></dx:EmptyLayoutItem>
                                            </Items>
                                        </dx:LayoutGroup>

                                    </Items>
                                </dx:ASPxFormLayout>


                                <dx:ASPxButton ID="ASPxButton4" BeginGroup="true" ValidationGroup="editformpage1" runat="server" AutoPostBack="false" Image-IconID="arrows_next_16x16" Text="Next">

                                    <ClientSideEvents Click="function(s,e){ 

                                                                            if(ASPxClientEdit.ValidateGroup('editformpage1')) 
                                                                            {
                                                                                editTabForm.GetTabByName('editPage1').SetEnabled(false);
                                                                                editTabForm.GetTabByName('editPage2').SetEnabled(true);

                                                                                editTabForm.SetActiveTab(1);
                                                                            }

                                                                        }" />
                                </dx:ASPxButton>

                                  <dx:ASPxButton ID="ASPxButton15" BeginGroup="true" runat="server" AutoPostBack="false" Image-IconID="actions_close_16x16devav" Text="Close">

                                    <ClientSideEvents Click="function(s,e){ popupEditForm.Hide(); }" />

                                </dx:ASPxButton>

                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Page 2" Name="editPage2" ClientEnabled="false" TabImage-IconID="businessobjects_boreport_32x32">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl4" runat="server">


                                <dx:ASPxFormLayout ID="editFormPage2" runat="server"
                                    RequiredMarkDisplayMode="Auto" ClientInstanceName="editFormPage2"
                                    SettingsItemCaptions-Location="Left"
                                    AlignItemCaptionsInAllGroups="true" Width="800">
                                    <Items>

                                        <dx:LayoutGroup Caption="Details Of Claims or Circumstances" ColCount="3" GroupBoxStyle-Caption-Font-Bold="true" GroupBoxStyle-Caption-Font-Size="Medium" GroupBoxDecoration="HeadingLine">
                                            <Items>


                                                <dx:LayoutItem Caption="Date Of Incident" FieldName="DateOfIncident" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxDateEdit runat="server" ID="ASPxDateEdit5" NullText="" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">

                                                                <ValidationSettings ValidationGroup="editformpage2" SetFocusOnError="true" Display="None" ErrorDisplayMode="None">
                                                                    <RequiredField IsRequired="true" ErrorText="" />
                                                                </ValidationSettings>
                                                                <InvalidStyle BackColor="LightPink" />
                                                            </dx:ASPxDateEdit>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem Caption="Date Of Claim Made by Patient" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxDateEdit runat="server" ID="ASPxDateEdit6" NullText="" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                                <ValidationSettings ValidationGroup="editformpage2" SetFocusOnError="true" Display="None" ErrorDisplayMode="None">
                                                                    <RequiredField IsRequired="true" ErrorText="" />
                                                                </ValidationSettings>
                                                                <InvalidStyle BackColor="LightPink" />

                                                            </dx:ASPxDateEdit>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem Caption="OPD/IPD of 1st Admission Date" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxDateEdit runat="server" ID="ASPxDateEdit7" NullText="" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">

                                                                <ValidationSettings ValidationGroup="editformpage2" SetFocusOnError="true" Display="None" ErrorDisplayMode="None">
                                                                    <RequiredField IsRequired="true" ErrorText="" />
                                                                </ValidationSettings>
                                                                <InvalidStyle BackColor="LightPink" />
                                                            </dx:ASPxDateEdit>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>




                                                <dx:LayoutItem Caption="Category of Medical Error" FieldName="CategoryOfMedicalError" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>

                                                            <dx:ASPxComboBox runat="server"
                                                                DataSourceID="SqlDataSource_MedicalError" TextField="Description" ValueField="MedicalError"
                                                                ID="ASPxComboBox11" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1"
                                                                DropDownStyle="DropDownList"
                                                                EnableClientSideAPI="True"
                                                                EncodeHtml="false">
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem ColSpan="2" Caption="Level Of Claim Severlity (How do you rate the complaint in regard of Serviousness?)" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>


                                                            <table>
                                                                <tr>
                                                                    <td>Low</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxRatingControl ID="ASPxRatingControl2" Value="0" runat="server" />
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>High</td>

                                                                </tr>
                                                            </table>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem ColSpan="3" FieldName="LOSSDESCRIPTION" Caption="Brief Summary of Incident" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxMemo ID="ASPxMemo3" runat="server" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1" Width="100%" Height="50"></dx:ASPxMemo>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>



                                                <dx:LayoutItem ColSpan="3" FieldName="Related_Doctors" Caption="Related Doctor's Name" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTokenBox ID="ASPxTokenBox2" runat="server" Width="100%" DataSourceID="SqlDataSource_Doctor" TextField="Doctor" ValueField="Doctor">
                                                            </dx:ASPxTokenBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem ColSpan="3" Caption="Peer Review" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxMemo ID="ASPxMemo4" runat="server" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1" Width="100%" Height="50"></dx:ASPxMemo>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem Caption="Amount of Claim/Demand" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxSpinEdit runat="server" ID="ASPxSpinEdit2" DecimalPlaces="2" DisplayFormatString="{0:N2}"
                                                                AllowMouseWheel="false"
                                                                SpinButtons-ShowIncrementButtons="false" SpinButtons-ShowLargeIncrementButtons="false"
                                                                NullText="0.00" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                            </dx:ASPxSpinEdit>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Currency" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxComboBox runat="server"
                                                                ID="ASPxComboBox12" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1"
                                                                DropDownStyle="DropDownList">
                                                                <Items>
                                                                    <dx:ListEditItem Text="" />
                                                                    <dx:ListEditItem Text="THB" />
                                                                    <dx:ListEditItem Text="USD" />
                                                                </Items>
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:EmptyLayoutItem></dx:EmptyLayoutItem>



                                            </Items>
                                        </dx:LayoutGroup>


                                        <dx:LayoutGroup Caption="Other Information" GroupBoxStyle-Caption-Font-Bold="true" GroupBoxStyle-Caption-Font-Size="Medium" GroupBoxDecoration="HeadingLine">
                                            <Items>
                                                <dx:LayoutItem Caption="Please give all other information relevant to this claim" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxMemo ID="ASPxMemo5" runat="server" CaptionSettings-Position="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1" Width="100%" Height="50"></dx:ASPxMemo>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Please attach copies of supporting correspondeces and/or documents" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <div class="uploadContainer">
                                                                <dx:ASPxUploadControl ID="ASPxUploadControl1" runat="server" ClientInstanceName="UploadControl" Width="320"
                                                                    NullText="Browse or Drag files here " UploadMode="Advanced" ShowTextBox="true" ShowAddRemoveButtons="false" ShowUploadButton="false" ShowProgressPanel="True">
                                                                    <AdvancedModeSettings EnableMultiSelect="True" EnableFileList="True" EnableDragAndDrop="True" />
                                                                    <ValidationSettings MaxFileSize="4194304" AllowedFileExtensions=".pdf, .doc, .docx, .xls, .xlsx">
                                                                    </ValidationSettings>
                                                                    <%--  <ClientSideEvents FilesUploadStart="function(s, e) { DXUploadedFilesContainer.Clear(); }"
                                                                        FileUploadComplete="onFileUploadComplete" />--%>
                                                                </dx:ASPxUploadControl>


                                                                <p class="note">
                                                                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Allowed file extensions: .pdf, .doc, .docx, .xls, .xlsx." Font-Size="8pt">
                                                                    </dx:ASPxLabel>
                                                                    <br />
                                                                    <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Maximum file size: 4 MB." Font-Size="8pt">
                                                                    </dx:ASPxLabel>
                                                                </p>
                                                            </div>

                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>



                                            </Items>
                                        </dx:LayoutGroup>


                                    </Items>
                                </dx:ASPxFormLayout>




                                <dx:ASPxButton ID="ASPxButton5" BeginGroup="true" runat="server" AutoPostBack="false" Image-IconID="arrows_prev_16x16" Text="Back">

                                    <ClientSideEvents Click="function(s,e){

                                            editTabForm.GetTabByName('editPage1').SetEnabled(true);
                                            editTabForm.GetTabByName('editPage2').SetEnabled(false);

                                            editTabForm.SetActiveTab(0);

                                            }" />
                                </dx:ASPxButton>


                                  <dx:ASPxButton ID="ASPxButton16" BeginGroup="true" runat="server" AutoPostBack="false" Image-IconID="actions_close_16x16devav" Text="Close">

                                    <ClientSideEvents Click="function(s,e){ 
                                        
                                      if(confirm('Confirm to Close Window?'))
                                        {  popupEditForm.Hide(); }
                                        
                                        }" />

                                </dx:ASPxButton>

                                <dx:ASPxButton ID="ASPxButton6" BeginGroup="true" runat="server" AutoPostBack="false" Image-IconID="save_save_16x16" Text="Save as Draft">

                                    <ClientSideEvents Click="function(s,e){ 
                                     
                                                if(ASPxClientEdit.ValidateGroup('editformpage2')) 
                                                {
                                                    if(confirm('Confirm to save draft?'))
                                                     {
                                                         popupEditForm.Hide();
                                                         Grid.Refresh();
                                                     }
                                                }

                                            }" />


                                </dx:ASPxButton>
                                <dx:ASPxButton ID="ASPxButton7" BeginGroup="true" runat="server" AutoPostBack="false" Image-IconID="content_checkbox_16x16" Text="Submit">

                                    <ClientSideEvents Click="function(s,e){ 
                                     
                                                if(ASPxClientEdit.ValidateGroup('editformpage2')) 
                                                {
                                                    if(confirm('Confirm to Submit?'))
                                                     {
                                                        popupEditForm.Hide();
                                                        Grid.Refresh();
                                                     }
                                                }

                                            }" />

                                </dx:ASPxButton>

                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>










        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>




<dx:ASPxPopupControl ID="popupViewForm" runat="server" ClientInstanceName="popupViewForm"
    Modal="True" Maximized="true"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="Medical Malpractice Notification (View)"
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
    HeaderImage-IconID="businessobjects_boreport_32x32"
    HeaderStyle-BackColor="WindowFrame"
    Width="840"
    Height="660"
    FooterText=""
    ShowFooter="false">
    <HeaderStyle BackColor="#4796CE" ForeColor="White" />

    <ClientSideEvents
        CloseUp="function(s,e){  
            //gridData.Refresh();
        }"
        BeginCallback="function(s,e){ 
            popupViewForm.Show();
        }"
        Shown="function(s,e){
        //gridRawData.PerformCallback();

         //gridRawData.Refresh();

        //PopupDataDetails.SetHeaderText(PopupDataDetails.cpHeaderDataDetailsText);
        //gridFields.Refresh();

         LoadingPanel.Hide(); 
      
        }" />


    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">

            <dx:ASPxFormLayout ID="viewForm" runat="server"
                RequiredMarkDisplayMode="Auto" ClientInstanceName="viewForm"
                SettingsItemCaptions-Location="Left"
                AlignItemCaptionsInAllGroups="true" Width="800">
                <Items>
                    <dx:LayoutGroup Caption="Insured" ColCount="4" GroupBoxStyle-Caption-Font-Bold="true" GroupBoxStyle-Caption-Font-Size="Medium" GroupBoxDecoration="HeadingLine">
                        <Items>
                            <dx:LayoutItem FieldName="CLAIMTYPE" Caption="Type Of Notifocation" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Insured" FieldName="INSURED" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Location" FieldName="Location" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Policy No." FieldName="POLICYNO" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Contact Person" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="-" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="&nbsp;" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel18" runat="server" Text="-" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="&nbsp;" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel19" runat="server" Text="-" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="&nbsp;" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel20" runat="server" Text="-" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Phone" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel21" runat="server" Text="-" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Email" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel22" runat="server" Text="-" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:EmptyLayoutItem ColSpan="2"></dx:EmptyLayoutItem>
                        </Items>
                    </dx:LayoutGroup>

                    <dx:LayoutGroup Caption="Details Of Claimant" ColCount="4" GroupBoxStyle-Caption-Font-Bold="true" GroupBoxStyle-Caption-Font-Size="Medium" GroupBoxDecoration="HeadingLine">
                        <Items>
                            <dx:LayoutItem Caption="Patient Name" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel23" runat="server" Text="-" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="&nbsp;" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel24" runat="server" Text="-" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="&nbsp;" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel25" runat="server" Text="-" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="&nbsp;" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel26" runat="server" Text="-" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>




                            <dx:LayoutItem Caption="Date Of Birth" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel27" runat="server" Text="-" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Age" FieldName="PATIENT_AGE" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel28" runat="server" Text="-" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Nationality" FieldName="Nationality" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel29" runat="server" Text="-" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Sex" FieldName="Sex" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel30" runat="server" Text="-" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Phone" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel31" runat="server" Text="-" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Email" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel32" runat="server" Text="-" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:EmptyLayoutItem ColSpan="2"></dx:EmptyLayoutItem>
                        </Items>
                    </dx:LayoutGroup>


                    <dx:LayoutGroup Caption="Details Of Claims or Circumstances" ColCount="3" GroupBoxStyle-Caption-Font-Bold="true" GroupBoxStyle-Caption-Font-Size="Medium" GroupBoxDecoration="HeadingLine">
                        <Items>


                            <dx:LayoutItem Caption="Date Of Incident" FieldName="DateOfIncident" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel34" runat="server" Text="-" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Date Of Claim Made by Patient" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel35" runat="server" Text="-" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="OPD/IPD of 1st Admission Date" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel36" runat="server" Text="-" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>




                            <dx:LayoutItem Caption="Category of Medical Error" FieldName="CategoryOfMedicalError" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel37" runat="server" Text="-" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem ColSpan="2" Caption="Level Of Claim Severlity (How do you rate the complaint in regard of Serviousness?)" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>

                                        <table>
                                            <tr>
                                                <td>Low</td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <dx:ASPxRatingControl ID="ASPxRatingControl4" Value="4" runat="server" ReadOnly="true" />
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>High</td>

                                            </tr>
                                        </table>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem ColSpan="3" FieldName="LOSSDESCRIPTION" Caption="Brief Summary of Incident" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel39" runat="server" Text="-" Wrap="True" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>



                            <dx:LayoutItem ColSpan="3" FieldName="Related_Doctors" Caption="Related Doctor's Name" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel40" runat="server" Text="-" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem ColSpan="3" Caption="Peer Review" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel41" runat="server" Text="-" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Amount of Claim/Demand" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel42" runat="server" Text="-" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Currency" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel38" runat="server" Text="-" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>



                        </Items>
                    </dx:LayoutGroup>


                    <dx:LayoutGroup Caption="Other Information" GroupBoxStyle-Caption-Font-Bold="true" GroupBoxStyle-Caption-Font-Size="Medium" GroupBoxDecoration="HeadingLine">
                        <Items>
                            <dx:LayoutItem Caption="Please give all other information relevant to this claim" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="-" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Please attach copies of supporting correspondeces and/or documents" CaptionSettings-Location="Top" CaptionStyle-Font-Italic="true" CaptionStyle-Font-Size="Smaller" CaptionStyle-Font-Bold="true" CaptionCellStyle-Paddings-Padding="1">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="-" Wrap="False" />

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>



                        </Items>
                    </dx:LayoutGroup>



                </Items>
            </dx:ASPxFormLayout>

            <dx:ASPxButton ID="ASPxButton8" BeginGroup="true" runat="server" Image-IconID="edit_edit_16x16" AutoPostBack="false" Text="Edit">
                <ClientSideEvents Click="function(s,e){
                         LoadingPanel.Show(); 
                         popupViewForm.Hide();
                         popupEditForm.PerformCallback('');  
                        
                    }" />


            </dx:ASPxButton>

            <dx:ASPxButton ID="ASPxButton10" BeginGroup="true" runat="server" Image-IconID="save_save_16x16" AutoPostBack="false" Text="Save as Draft">
                <ClientSideEvents Click="function(s,e){
                        if(confirm('Confirm to Save Draft?'))
                        {


                            popupViewForm.Hide();

                            Grid.Refresh();
                        }
                    }" />

            </dx:ASPxButton>
            <dx:ASPxButton ID="ASPxButton11" BeginGroup="true" runat="server" Image-IconID="content_checkbox_16x16" AutoPostBack="false" Text="Summit">

                <ClientSideEvents Click="function(s,e){
                        if(confirm('Confirm to Submit?'))
                        {
                            popupViewForm.Hide();

                           Grid.Refresh();
                        }
                    }" />
            </dx:ASPxButton>


            <dx:ASPxButton ID="ASPxButton9" BeginGroup="true" runat="server" Image-IconID="edit_delete_16x16" AutoPostBack="false" Text="Delete">

                <ClientSideEvents Click="function(s,e){
                        if(confirm('Are you sure you want to delete this item?'))
                        {
                            popupViewForm.Hide();

                            Grid.Refresh();
                        }
                    }" />

            </dx:ASPxButton>

            <dx:ASPxButton ID="ASPxButton14" BeginGroup="true" runat="server" AutoPostBack="false" Image-IconID="actions_close_16x16devav" Text="Close">

                <ClientSideEvents Click="function(s,e){ popupViewForm.Hide(); }" />

            </dx:ASPxButton>

        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
