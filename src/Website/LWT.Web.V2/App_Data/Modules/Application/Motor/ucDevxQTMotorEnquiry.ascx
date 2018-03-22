<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxQTMotorEnquiry.ascx.vb" Inherits="Modules_ucDevxQTMotorEnquiry" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data.Linq" TagPrefix="dx" %>
<%--start highlighted block--%>
<style>
    .dxeListBox td.dxeLTM,
    .dxeListBox td.dxeTM,
    .dxeListBox td.dxeMIM {
        border-style: none !important;
    }
</style>
<script type="text/javascript">
    function OnNameValidation(s, e) {
        var name = e.value;
        if (name == null)
            return;
    }
    function OnAgeValidation(s, e) {
        var age = e.value;
        if (age == null || age == "")
            return;
        var digits = "0123456789";
        for (var i = 0; i < age.length; i++) {
            if (digits.indexOf(age.charAt(i)) == -1) {
                e.isValid = false;
                break;
            }
        }
        if (e.isValid && age.charAt(0) == '0') {
            age = age.replace(/^0+/, "");
            if (age.length == 0)
                age = "0";
            e.value = age;
        }
        if (age < 18)
            e.isValid = false;
    }
    function OnArrivalDateValidation(s, e) {
        var selectedDate = s.date;
        if (selectedDate == null || selectedDate == false)
            return;
        var currentDate = new Date();
        if (currentDate.getFullYear() != selectedDate.getFullYear() || currentDate.getMonth() != selectedDate.getMonth())
            e.isValid = false;
    }
</script>
<script type="text/javascript">
    var lastcartype = null;
    function OnNewCarTypeChanged(cmbNewCarType) {
        if (newCarType.InCallback())
            lastcartype = cmbNewCarType.GetValue().toString();
        else
            newCarUse.PerformCallback(cmbNewCarType.GetValue().toString());
    }
    function OnEndnewCarUseCallback(s, e) {
        if (lastcartype) {
            newCarType.PerformCallback(lastcartype);
            lastcaruse = null;
        }
    }
</script>


<script type="text/javascript">
    var keyValue;
    function OnMoreInfoClick(element, key) {
        callbackPanel.SetContentHtml("");
        popupMoreDetails.ShowAtElement(element);
        keyValue = key;
    }
    function popup_Shown(s, e) {
        callbackPanel.PerformCallback(keyValue);
    }
</script>



<%--end highlighted block--%>


<%--start Datasource block--%>
<dx:LinqServerModeDataSource ID="LinqServerModeDataSource_tblMotorQuotations" runat="server" ContextTypeName="DataClasses_CPSExt" DefaultSorting="QuotationNo Desc" TableName="V_MotorQuotations" />

 
   <asp:SqlDataSource ID="ObjectDataSource_Location" runat="server" ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
        SelectCommand="select  * from v_location order by LOCATION_NAME "></asp:SqlDataSource>

 

    <asp:SqlDataSource ID="ObjectDataSource_Title" runat="server" ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
        SelectCommand="select  * from Portal_Table where TABLE_NUMBER='T0002'  order by  ITEM_DESC_TH "></asp:SqlDataSource>

 
<asp:SqlDataSource ID="SqlDataSource_SP_CompareQuote" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>" SelectCommand="SP_CompareQuote" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="" Name="QuotationNo" SessionField="QuotationNo" Type="String" />
    </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_Discussion" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    InsertCommand="INSERT INTO [tblDiscussionTrans] ([DiscGroupID], [IDCodeData], [DiscNote], [UserRec], [DateRec]) VALUES (@DiscGroupID, @IDCodeData, @DiscNote, @UserRec, @DateRec)"
    SelectCommand="SELECT * FROM [tblDiscussionTrans] WHERE isnull([DiscGroupID],'') <> '' and ([DiscGroupID] = @DiscGroupID) order by DateRec desc ">
    <InsertParameters>
        <asp:Parameter Name="DiscGroupID" Type="String" />
        <asp:Parameter Name="IDCodeData" Type="String" />
        <asp:Parameter Name="DiscNote" Type="String" />
        <asp:Parameter Name="UserRec" Type="String" />
        <asp:Parameter Name="DateRec" Type="DateTime" />
    </InsertParameters>
    <SelectParameters>
        <asp:SessionParameter DefaultValue=" " Name="DiscGroupID" SessionField="DiscGroupId" Type="String" />
    </SelectParameters>

</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource_CarBrand" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="SELECT ID, Name FROM tblCarBrandModel where ParentID is null  order by Name "></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource_CarModel" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="SELECT ID, Name FROM tblCarBrandModel where ParentID is not null and ParentID=?  order by Name ">
    <SelectParameters>
        <asp:Parameter Name="?" />
    </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_CarType" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>" SelectCommand="SELECT * FROM [tblCarType]"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource_CarUse" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>" SelectCommand="SELECT CarUse, txtCarUse FROM tblCarUse WHERE (CarUse LIKE @Param1 + '%')">
    <SelectParameters>
        <asp:Parameter Name="Param1" DefaultValue="-1" Type="String" />
    </SelectParameters>
</asp:SqlDataSource>


<%--end Datasource block--%>

<%--Start MotorQuotation  --%>

<dx:ASPxRoundPanel ID="pnMotorQuotation" ClientInstanceName="pnMotorQuotation" EnableAnimation="true" ShowCollapseButton="true" AllowCollapsingByHeaderClick="true" HeaderText="ข้อมูลเสนอราคา" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxButton ID="btnImageAndText" runat="server" RenderMode="Button"
                Width="90px" Text="สร้างใบเสนอราคา" AutoPostBack="false" CausesValidation="false">
                <Image IconID="actions_new_32x32"></Image>
                <ClientSideEvents Click="function(s, e) {    
                       popUpNewQuotation.Show(); 
                       ASPxClientEdit.ClearEditorsInContainerById('frmNewQuotation');                      
                      }" />
            </dx:ASPxButton>
            <dx:ASPxButton ID="ASPxButton1" runat="server" RenderMode="Button"
                Width="90px" Text="Refresh" AutoPostBack="false" CausesValidation="false">
                <Image IconID="actions_refresh_32x32"></Image>
                <ClientSideEvents Click="function(s, e) {           
                       dgQuotations.ApplySearchPanelFilter('');        
                      }" />
            </dx:ASPxButton>

            <br />
            <br />
            <dx:ASPxGridView ID="dgQuotations"
                ClientInstanceName="dgQuotations"
                runat="server"
                KeyFieldName="QuotationNo"
                Width="100%"
                AutoGenerateColumns="False"
                DataSourceID="LinqServerModeDataSource_tblMotorQuotations"
                EnableCallBacks="true"
                EnableCallbackAnimation="true"
                EnableCallbackCompression="true"
                EnableRowsCache="true">

                <%--start Grid Setting block--%>
                <SettingsLoadingPanel Mode="ShowAsPopup" />
                <SettingsBehavior EnableCustomizationWindow="true" EnableRowHotTrack="true" ProcessSelectionChangedOnServer="True" />

                <SettingsDataSecurity AllowDelete="false" AllowEdit="false" AllowInsert="false" />
                <SettingsSearchPanel Visible="true" />

                <GroupSummary>
                    <dx:ASPxSummaryItem SummaryType="Count" />
                </GroupSummary>
                <SettingsPager>
                    <PageSizeItemSettings Visible="true" Items="10, 20, 50" />
                </SettingsPager>
                <Columns>
                    <dx:GridViewDataButtonEditColumn FieldName="QuotationNo" Width="100" Caption="เลขที่เสนอราคา" VisibleIndex="1" CellStyle-Wrap="False" SortOrder="Descending">
                        <CellStyle Wrap="False"></CellStyle>
                        <DataItemTemplate>
                            <a href='applications/editmotorquotation.aspx?quotationno=<%# Eval("QuotationNo")%>' onclick="LoadingPanel.Show();"><%# Eval("QuotationNo")%></a>
                        </DataItemTemplate>
                    </dx:GridViewDataButtonEditColumn>

                    <dx:GridViewDataButtonEditColumn FieldName="ConfirmCode" Width="100" Caption="เลขที่ App" VisibleIndex="2" CellStyle-Wrap="False">
                        <CellStyle Wrap="False"></CellStyle>
                        <DataItemTemplate><%# Eval("ConfirmCode")%></DataItemTemplate>
                    </dx:GridViewDataButtonEditColumn>
                    <dx:GridViewDataTextColumn FieldName="CustName" Caption="ผู้ติดต่อ" VisibleIndex="3" CellStyle-Wrap="False">
                        <CellStyle Wrap="False"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ClientCode" Caption="ClientCode" VisibleIndex="4" CellStyle-Wrap="False">
                        <CellStyle Wrap="False"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="InsuredName" Caption="ผู้เอาประกัน" VisibleIndex="5" CellStyle-Wrap="False">
                        <CellStyle Wrap="False"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="CarId" Caption="ทะเบียน" VisibleIndex="8" CellStyle-Wrap="False">
                        <CellStyle Wrap="False"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="DateRec" Caption="วันที่บันทึก" VisibleIndex="9" CellStyle-Wrap="False">
                        <CellStyle Wrap="False"></CellStyle>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="UserRec" Caption="ผู้บันทึก" VisibleIndex="10" CellStyle-Wrap="False">
                        <CellStyle Wrap="False"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="QuotationStatus" Caption="สถานะ" VisibleIndex="11" CellStyle-Wrap="False">
                        <CellStyle Wrap="False"></CellStyle>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataColumn Caption="More Info" VisibleIndex="13" Width="15%">
                        <DataItemTemplate>
                            <a href="javascript:void(0);" onclick="OnMoreInfoClick(this, '<%# Container.KeyValue %>')">More Info...</a>
                        </DataItemTemplate>
                        <CellStyle Wrap="False"></CellStyle>
                    </dx:GridViewDataColumn>
                </Columns>

            </dx:ASPxGridView>

            <dx:ASPxGridViewExporter runat="server" ID="GridExporter" GridViewID="dgQuotations" />
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<%--End MotorQuotation  --%>



<%--Start popUpNewQuotation  --%>
<dx:ASPxPopupControl ID="popUpNewQuotation"
    ClientInstanceName="popUpNewQuotation"
    runat="server" Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="สร้างใบเสนอราคา"
    AllowDragging="true"
    EnableAnimation="true" CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="true" Width="138px">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">

            <dx:ASPxFormLayout ID="frmNewQuotation" runat="server" AlignItemCaptionsInAllGroups="True" ColCount="2" RequiredMarkDisplayMode="None" ShowItemCaptionColon="False">
                <Items>
                    <dx:LayoutGroup Caption="โครงการ" GroupBoxDecoration="HeadingLine" Width="330px" ColSpan="2">
                        <Items>
                            <dx:LayoutItem ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server" SupportsDisabledAttribute="True">



                                        <dx:ASPxComboBox ID="UserProject" runat="server" Caption="โครงการ"
                                            DropDownStyle="DropDownList" DataSourceID="SqlDataSource_UserProject" 
                                            ValueField="ProjectID"
                                            ValueType="System.String"
                                            TextFormatString="{0}"
                                            NullText="เลือกโครงการ"
                                            IncrementalFilteringMode="StartsWith">
                                            <ItemStyle Border-BorderStyle="None">
                                                <Border BorderStyle="None"></Border>
                                            </ItemStyle>
                                            <ClearButton Visibility="True"></ClearButton>
                                            <Columns>
                                                <dx:ListBoxColumn FieldName="ProjectName" Caption="" />
                   
                                            </Columns>

                                            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <InvalidStyle BackColor="LightPink" />
                                        </dx:ASPxComboBox>

                                        <asp:SqlDataSource ID="SqlDataSource_UserProject" runat="server"
                                            ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
                                            SelectCommand="select ProjectID, ProjectName from V_UserProject where UserName=@UserName order by ProjectName ">
                                            <SelectParameters>
                                              <asp:Parameter Name="UserName" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>



                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                        </Items>
                    </dx:LayoutGroup>



                    <dx:LayoutGroup Caption="รายละเอียดลูกค้า" GroupBoxDecoration="HeadingLine" Width="330px">
                        <Items>
                            <dx:LayoutItem Caption="คำนำหน้า">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer18" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxComboBox ID="newCustTitleName" runat="server" Width="80"
                                            DropDownStyle="DropDownList" DataSourceID="ObjectDataSource_Title" ValueField="ITEM_DESC_TH"
                                            ValueType="System.String"
                                            TextFormatString="{0}"
                                            NullText="คำนำหน้า"
                                            IncrementalFilteringMode="StartsWith">
                                            <ItemStyle Border-BorderStyle="None">
                                                <Border BorderStyle="None"></Border>
                                            </ItemStyle>
                                            <ClearButton Visibility="True"></ClearButton>
                                            <Columns>
                                                <dx:ListBoxColumn FieldName="ITEM_DESC_TH" Caption="ค้นหาคำนำหน้า" />

                                            </Columns>
                                        </dx:ASPxComboBox>

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="ชื่อลูกค้า">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer
                                        ID="LayoutItemNestedControlContainer23"
                                        runat="server"
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxTextBox ID="newCustName" runat="server"
                                            Width="250" MaxLength="255">
                                            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <InvalidStyle BackColor="LightPink" />
                                        </dx:ASPxTextBox>

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="ประเภทบัตร">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxRadioButtonList TextWrap="false" ID="newCustIDCardsType" runat="server" TextAlign="Right" RepeatDirection="Horizontal">

                                            <Items>
                                                <dx:ListEditItem Text="บัตรประชาชน" Value="1" />
                                                <dx:ListEditItem Text="หนังสือเดินทาง" Value="2" />
                                                <dx:ListEditItem Text="อื่นๆ" Value="3" />
                                            </Items>
                                            <Border BorderStyle="None" />

                                        </dx:ASPxRadioButtonList>

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="เลขที่บัตร" Height="16">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxTextBox ID="newCustIDCards" Width="250" runat="server" MaxLength="50">
                                        </dx:ASPxTextBox>

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="ที่อยู่">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxTextBox Width="250" ID="newCustAddress" runat="server" MaxLength="255" />
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" SupportsDisabledAttribute="True">

                                        <dx:ASPxComboBox ID="newLOCATION_CODE" runat="server" Width="250" DropDownWidth="300"
                                            DropDownStyle="DropDown"
                                            DataSourceID="ObjectDataSource_Location"
                                            ValueField="LOCATION_CODE"
                                            ValueType="System.String"
                                            TextFormatString="{0}"
                                            EnableCallbackMode="true"
                                            IncrementalFilteringMode="Contains"
                                            CallbackPageSize="20">
                                            <ClearButton Visibility="True"></ClearButton>
                                            <ItemStyle Border-BorderStyle="None">
                                                <Border BorderStyle="None"></Border>
                                            </ItemStyle>
                                            <Columns>
                                                <dx:ListBoxColumn FieldName="LOCATION_NAME" Caption="ค้นหาที่อยู่" />
                                            </Columns>
                                        </dx:ASPxComboBox>

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="โทรศัพท์">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxTextBox ID="newCustTelNo" Width="250" runat="server" MaxLength="255">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="อีเมล์">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxTextBox ID="newCustEmail" Width="250" runat="server" MaxLength="255">
                                            <ValidationSettings SetFocusOnError="True" Display="Dynamic" ErrorTextPosition="Right">
                                                <RegularExpression ErrorText="" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="แฟกซ์">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxTextBox ID="newCustFaxNo" Width="250" runat="server" MaxLength="50">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                    <dx:LayoutGroup Caption="รายละเอียดรถ" GroupBoxDecoration="HeadingLine" Width="330px">
                        <Items>
                            <%--                            <dx:LayoutItem  Caption="ประเภทรถ" >
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server" SupportsDisabledAttribute="True">
                                         <dx:ASPxComboBox   ID="newCarType" ClientInstanceName="newCarType"
                                             runat="server" 
                                             DropDownStyle="DropDownList" 
                                             DataSourceID="SqlDataSource_CarType"
                                             TextField="CarTypeName" 
                                             ValueField="CarType"
                                             EnableSynchronization="False"
                                             >
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) { OnNewCarTypeChanged(s); }" />
                                             </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                             <dx:LayoutItem Caption="ลักษณะการใช้" >
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer18" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxComboBox  ID="newCarUse"  ClientInstanceName="newCarUse"
                                            runat="server"   
                                            DropDownStyle="DropDownList"
                                            DataSourceID="SqlDataSource_CarUse" 
                                            TextField="txtCarUse"
                                            ValueField="CarUse" 
                                            IncrementalFilteringMode="StartsWith" 
                                            EnableSynchronization="False" 
                                            >
                                            <ClientSideEvents EndCallback=" OnEndnewCarUseCallback"/>
                                         </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>--%>

                            <dx:LayoutItem Caption="ยี่ห้อ">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server" SupportsDisabledAttribute="True">

                                        <dx:ASPxComboBox runat="server"
                                            ID="newCarBrand" ClientInstanceName="newCarBrand"
                                            DropDownStyle="DropDownList"
                                            IncrementalFilteringMode="StartsWith"
                                            DataSourceID="SqlDataSource_CarBrand"
                                            EnableSynchronization="true"
                                            TextField="Name"
                                            ValueField="ID"
                                            Enablecallback="true">

                                            <ClientSideEvents SelectedIndexChanged="function(s, e) { newCarModel.PerformCallback(s.GetValue().toString()); }" />
                                             <ValidationSettings ErrorDisplayMode="None" Display="Dynamic">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <InvalidStyle BackColor="LightPink" />

                                        </dx:ASPxComboBox>

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>




                            <dx:LayoutItem Caption="รุ่น">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server" SupportsDisabledAttribute="True">

                                        <dx:ASPxComboBox runat="server"
                                            ID="newCarModel" ClientInstanceName="newCarModel"
                                            TextField="Name"
                                            ValueField="ID"
                                            IncrementalFilteringMode="StartsWith"
                                            EnableViewState="true"
                                            EnableSynchronization="true"
                                            DropDownStyle="DropDownList">

                                             <ValidationSettings ErrorDisplayMode="None" Display="Dynamic">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <InvalidStyle BackColor="LightPink" />
                                        </dx:ASPxComboBox>

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>




                            <dx:LayoutItem Caption="แบบ">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer20" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxTextBox ID="newCarSubModelName" runat="server" MaxLength="255">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>





                            <dx:LayoutItem Caption="ทุน">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer21" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxSpinEdit ID="newCarSuminsured" runat="server" MaxLength="10" DisplayFormatString="#,#"
                                            Increment="10000" Number="100000" NumberType="Integer"
                                            MinValue="0">

                                            <%-- <ValidationSettings ErrorDisplayMode="None" Display="Dynamic">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                            <InvalidStyle BackColor="LightPink" />--%>
                                        </dx:ASPxSpinEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="ปีรถ">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer19" runat="server" SupportsDisabledAttribute="True">
                                        <%--                                        <dx:ASPxTextBox ID="newCarReg" runat="server">
                                            <MaskSettings Mask="0000" />
                                            <ValidationSettings ValidateOnLeave="false"></ValidationSettings>
                                        </dx:ASPxTextBox>--%>

                                        <dx:ASPxSpinEdit ID="newCarReg" runat="server" MaxLength="4"
                                            Increment="1" NumberType="Integer"
                                            MinValue="0">
                                        </dx:ASPxSpinEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="ทะเบียน">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxTextBox ID="newCarId" runat="server" MaxLength="50" />
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>



                            <dx:LayoutItem Caption="เลขเครื่อง">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer17" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxTextBox ID="newCarEngineNo" runat="server" MaxLength="255">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="เลขถัง">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxTextBox ID="newCarChassisNo" runat="server" MaxLength="50">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="ขนาด">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer22" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxTextBox ID="NewCarSize" runat="server" MaxLength="50">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>


                    <%--                    <dx:LayoutGroup Caption="ติดต่อ"  >
                        <Items>
                            <dx:LayoutItem Caption="ติดต่อคุณ"  >
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxTextBox ID="newContact" runat="server">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="เบอร์ต่อ"  >
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxTextBox ID="newTelExt" runat="server">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                        </Items>
                    </dx:LayoutGroup>
                    <dx:LayoutGroup Caption="หมายเหตุ" GroupBoxDecoration="HeadingLine">
                        <Items>
                            <dx:LayoutItem Caption=""  >
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxMemo ShowCaption="False" Width="300" ID="newNoteOther" runat="server"></dx:ASPxMemo>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                        </Items>
                    </dx:LayoutGroup>--%>

                    <dx:LayoutItem ShowCaption="False" HorizontalAlign="Center" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server"
                                SupportsDisabledAttribute="True"
                                AutoPostBack="False">
                                <dx:ASPxButton ID="btnSaveNewQuotation" runat="server" ValidationContainerID="frmNewQuotation"
                                    Text="บันทึกใบเสนอราคา" Width="100">
                                    <ClientSideEvents Click="function(s, e) {
                                        if(ASPxClientEdit.AreEditorsValid()) {
                                            LoadingPanel.Show();
                                            cbSaveNewQuotation.PerformCallback('');
                                        }
                                    }" />
                                </dx:ASPxButton>
                                <dx:ASPxCallback runat="server" ID="cbSaveNewQuotation" ClientInstanceName="cbSaveNewQuotation">
                                    <ClientSideEvents CallbackComplete="function(s,e){
                                        e.processOnServer = false;
                                        LoadingPanel.Hide();
                                    }" />
                                </dx:ASPxCallback>


                                &nbsp;
                                <dx:ASPxButton ID="btnReset" runat="server" AutoPostBack="False" Text="Reset ใบเสนอราคา"
                                    CausesValidation="False">
                                    <ClientSideEvents Click="function(s, e) { ASPxClientEdit.ClearEditorsInContainerById('frmNewQuotation');   }" />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>



                </Items>
            </dx:ASPxFormLayout>



        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
<%--End popUpNewQuotation  --%>




<dx:ASPxPopupControl ID="popupMoreDetails" ClientInstanceName="popupMoreDetails" runat="server" AllowDragging="true" PopupHorizontalAlign="OutsideRight" ShowHeader="false">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
            <dx:ASPxCallbackPanel ID="callbackPanel" ClientInstanceName="callbackPanel" runat="server"
                Width="320px" Height="100px" RenderMode="Table">
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server">

                        <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" AlignItemCaptionsInAllGroups="True" ColCount="1" RequiredMarkDisplayMode="None" ShowItemCaptionColon="true">
                            <Items>
                                <dx:LayoutGroup Caption="รายละเอียดอื่นๆ" GroupBoxDecoration="HeadingLine" Width="330px">
                                    <Items>
                                        <dx:LayoutItem Caption="เลขอ้างอิง">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">

                                                    <dx:ASPxLabel ID="moreReferenceNo" runat="server" Text=""></dx:ASPxLabel>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="ยี่ห้อ">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server" SupportsDisabledAttribute="True">

                                                    <dx:ASPxLabel ID="moreCarBrandName" runat="server" Text=""></dx:ASPxLabel>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="รุ่น">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server" SupportsDisabledAttribute="True">

                                                    <dx:ASPxLabel ID="moreCarModelName" runat="server" Text=""></dx:ASPxLabel>

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
    <ClientSideEvents Shown="popup_Shown" />
</dx:ASPxPopupControl>




