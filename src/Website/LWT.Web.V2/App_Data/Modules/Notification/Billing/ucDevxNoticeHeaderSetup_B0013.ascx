<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxNoticeHeaderSetup_B0013.ascx.vb" Inherits="Modules_ucDevxNoticeHeaderSetup_B0013" %>
 
<script type="text/javascript">
    var keyValue;
    function OnMoreInfoClick_TR(element, key) {
        callbackPanel_TR.SetContentHtml("");
        popupMoreDetails_TR.ShowAtElement(element);
        keyValue = key;
    }
    function popup_Shown_TR(s, e) {
        callbackPanel_TR.PerformCallback(keyValue);
    }
    function OnMoreInfoClick_NOTR(element, key) {
        callbackPanel_NOTR.SetContentHtml("");
        popupMoreDetails_NOTR.ShowAtElement(element);
        keyValue = key;
    }
    function popup_Shown_NOTR(s, e) {
        callbackPanel_NOTR.PerformCallback(keyValue);
    }
</script>
<dx:ASPxPopupControl ID="popupMoreDetails_TR" ClientInstanceName="popupMoreDetails_TR" runat="server" 
    AllowDragging="true" 
    PopupHorizontalAlign="OutsideRight">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
            <dx:ASPxCallbackPanel ID="callbackPanel_TR" ClientInstanceName="callbackPanel_TR" runat="server"
                Width="320px" Height="100px" RenderMode="Table">
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server">
 

                        <dx:ASPxGridView ID="detailGrid_TR" runat="server" DataSourceID="SqlDataSource_Details_TR" KeyFieldName="NoticeDataID"
                            Width="800" >
                            <Columns>
  <%--<dx:GridViewDataTextColumn FieldName="No" ></dx:GridViewDataTextColumn>--%>

                            <dx:GridViewDataColumn FieldName="f02" Caption="Client Code" CellStyle-Wrap="False" />
                            <dx:GridViewDataColumn FieldName="f03" Caption="D/N No"  CellStyle-Wrap="False"/>
                            <dx:GridViewDataColumn FieldName="f04" Caption="D/N Date"  CellStyle-Wrap="False"/>
                     <%--       <dx:GridViewDataColumn FieldName="f05" Caption="Insurer"  CellStyle-Wrap="True"/>
                            <dx:GridViewDataColumn FieldName="f06" Caption="ชื่อบริษัทประกันภัย" CellStyle-Wrap="True" />--%>
                                <dx:GridViewDataColumn FieldName="f07" Caption="รายละเอียด" CellStyle-Wrap="True" />
                            <dx:GridViewDataColumn FieldName="f08" Caption="เลขรับแจ้ง" CellStyle-Wrap="True" />
                            <dx:GridViewDataColumn FieldName="f09" Caption="เลขตัวถัง" CellStyle-Wrap="True" />
                            <dx:GridViewDataColumn FieldName="f10" Caption="Policy No."  CellStyle-Wrap="False"/>
                            <dx:GridViewDataColumn FieldName="f11" Caption="Period From"  CellStyle-Wrap="False"/>
                            <dx:GridViewDataColumn FieldName="f12" Caption="Period To"  CellStyle-Wrap="False"/>
                            <dx:GridViewDataColumn FieldName="f13" Caption="NetPremium Premium"  CellStyle-Wrap="False"/>
                            <dx:GridViewDataColumn FieldName="f14" Caption="TotalPremium"  CellStyle-Wrap="False"/>
                                <dx:GridViewDataColumn FieldName="f15" Caption="TRNO"  CellStyle-Wrap="False"/>
                                <dx:GridViewDataColumn FieldName="f16" Caption="TRDATE"  CellStyle-Wrap="False"/>
                                <dx:GridViewDataColumn FieldName="f17" Caption="Balance Amount"  CellStyle-Wrap="False"/>

                                		

                            </Columns>
                            <Settings   VerticalScrollableHeight="450" HorizontalScrollBarMode="Visible" />
                        </dx:ASPxGridView>



                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <ClientSideEvents Shown="popup_Shown_TR" EndCallback="function(s,e){detailGrid_TR.Refresh();}" />
</dx:ASPxPopupControl>


<dx:ASPxPopupControl ID="popupMoreDetails_NOTR" ClientInstanceName="popupMoreDetails_NOTR" runat="server" 
    AllowDragging="true" 
    PopupHorizontalAlign="OutsideRight">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
            <dx:ASPxCallbackPanel ID="callbackPanel_NOTR" ClientInstanceName="callbackPanel_NOTR" runat="server"
                Width="320px" Height="100px" RenderMode="Table">
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent2" runat="server">
 

                        <dx:ASPxGridView ID="detailGrid_NOTR" runat="server" 
                            DataSourceID="SqlDataSource_Details_NOTR" 
                            KeyFieldName="NoticeDataID"
                            Width="800" >
                            <Columns>
  <%--<dx:GridViewDataTextColumn FieldName="No" ></dx:GridViewDataTextColumn>--%>

                            <dx:GridViewDataColumn FieldName="f02" Caption="Client Code" CellStyle-Wrap="False" HeaderStyle-Wrap="False" />
                            <dx:GridViewDataColumn FieldName="f03" Caption="D/N No"  CellStyle-Wrap="False" HeaderStyle-Wrap="False"/>
                            <dx:GridViewDataColumn FieldName="f04" Caption="D/N Date"  CellStyle-Wrap="False" HeaderStyle-Wrap="False"/>
                     <%--       <dx:GridViewDataColumn FieldName="f05" Caption="Insurer"  CellStyle-Wrap="True"/>
                            <dx:GridViewDataColumn FieldName="f06" Caption="ชื่อบริษัทประกันภัย" CellStyle-Wrap="True" />--%>
                            <dx:GridViewDataColumn FieldName="f07" Caption="เลขรับแจ้ง" CellStyle-Wrap="True" HeaderStyle-Wrap="False" />
                            <dx:GridViewDataColumn FieldName="f08" Caption="เลขตัวถัง" CellStyle-Wrap="True"  HeaderStyle-Wrap="False"/>
                            <dx:GridViewDataColumn FieldName="f09" Caption="Policy No."  CellStyle-Wrap="False" HeaderStyle-Wrap="False"/>
                            <dx:GridViewDataColumn FieldName="f10" Caption="Period From"  CellStyle-Wrap="False" HeaderStyle-Wrap="False"/>
                            <dx:GridViewDataColumn FieldName="f11" Caption="Period To"  CellStyle-Wrap="False" HeaderStyle-Wrap="False"/>
                            <dx:GridViewDataColumn FieldName="f12" Caption="NetPremium Premium"  CellStyle-Wrap="False" HeaderStyle-Wrap="False"/>
                            <dx:GridViewDataColumn FieldName="f13" Caption="TotalPremium"  CellStyle-Wrap="False" HeaderStyle-Wrap="False"/>

   		

                            </Columns>
                            <Settings   VerticalScrollableHeight="450" HorizontalScrollBarMode="Visible" />
                        </dx:ASPxGridView>



                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <ClientSideEvents Shown="popup_Shown_NOTR" EndCallback="function(s,e){detailGrid_NOTR.Refresh();}" />
</dx:ASPxPopupControl>


<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="บริษัทประกันภัย" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>
            <dx:ASPxButton ID="btnPopup" runat="server" Text="เพิ่มงานแจ้งส่งกรมธรรม์" Image-IconID="actions_add_16x16">
                <ClientSideEvents Click="function(s, e) {  
                                
                       popUpAdd.Show();      
                      
                       e.processOnServer = false; 
                      }" />
            </dx:ASPxButton>

<dx:ASPxButton ID="btnExcelFormat" runat="server" Text="Format Excel" Image-IconID="export_exporttoxlsx_16x16" />



            <%--  <dx:ASPxButton ID="btnAddNewHeader" runat="server" Text="เพิ่มงานแจ้งส่งกรมธรรม์" Image-IconID="actions_add_16x16">
                <ClientSideEvents Click="function(s, e) {                                  
                                       cbAddNewHeader.PerformCallback('')                        
                                       e.processOnServer = false; 
                                      }" />
            </dx:ASPxButton>


            <dx:ASPxCallback runat="server" ID="cbAddNewHeader" ClientInstanceName="cbAddNewHeader">
                <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide(); }"
                    CallbackComplete="function(s,e){ 
                                             LoadingPanel.Hide();   
                                             if (e.result != 'success') {
                                                alert(e.result);                                                
                                             } 
                                             else
                                             { 
                                                gridData.Refresh();                                                
                                             }
                                             e.processOnServer = false;
                                        }" />
            </dx:ASPxCallback>--%>



            <dx:ASPxGridView ID="gridData" ClientInstanceName="gridData" runat="server" DataSourceID="SqlDataSource_NoticeHeader"
                KeyFieldName="NoticeID" AutoGenerateColumns="False" Width="800">
                <SettingsPager Mode="ShowPager">
                    <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                </SettingsPager>
                <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" />
                <SettingsSearchPanel Visible="true" />
              
            <%--    <SettingsCommandButton>
                    <UpdateButton ButtonType="Button" Text="บันทึก"></UpdateButton>

                    <EditButton ButtonType="Button" Text="แก้ไข"></EditButton>
                    <DeleteButton ButtonType="Button" Text="ลบ"></DeleteButton>

                    <CancelButton ButtonType="Button" Text="ยกเลิก"></CancelButton>
                </SettingsCommandButton>--%>


                <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />

                <Columns>
                    <dx:GridViewCommandColumn Width="50" CellStyle-Wrap="False"  ShowDeleteButton="true" />


                    <dx:GridViewDataTextColumn FieldName="NoticeID" Caption="NoticeID"></dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="NoticeTitle" Caption="Title"></dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="CreationDate" Caption="วันที่สร้าง">
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="CreationBy" Caption="ผู้สร้าง">
                    </dx:GridViewDataTextColumn>


                </Columns>
                <Templates>
                    <DetailRow>


 <dx:ASPxButton ID="ASPxButton1" runat="server" Text="ส่งเมล์" Image-IconID="mail_send_16x16" AutoPostBack="false">
     <ClientSideEvents Click="function(s,e){
                        detailGrid.PerformCallback('sendmail');
                        }" />
 </dx:ASPxButton>



                        <dx:ASPxGridView ID="detailGrid" ClientInstanceName="detailGrid" runat="server" DataSourceID="SqlDataSource_Details" KeyFieldName="Code"
                            Width="100%" OnCustomCallback="detailGrid_CustomCallback"
                            OnBeforePerformDataSelect="detailGrid_DataSelect">
                            <Columns>

                                <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectAllCheckboxMode="AllPages"></dx:GridViewCommandColumn>

                                <dx:GridViewDataColumn FieldName="Code" />
                                <dx:GridViewDataColumn FieldName="Name" />
                               <%-- <dx:GridViewDataColumn FieldName="TR" />
                                <dx:GridViewDataColumn FieldName="NOTR" />--%>

<dx:GridViewDataColumn Caption="NOTR" VisibleIndex="8" Width="15%" CellStyle-HorizontalAlign="Center">
    <DataItemTemplate>
        <a href="javascript:void(0);" onclick="OnMoreInfoClick_NOTR(this, '<%# Container.KeyValue %>')">
            <%# Eval("NOTR")%></a>
    </DataItemTemplate>
</dx:GridViewDataColumn>
<dx:GridViewDataColumn Caption="TR" VisibleIndex="8" Width="15%"  CellStyle-HorizontalAlign="Center">
    <DataItemTemplate>
        <a href="javascript:void(0);" onclick="OnMoreInfoClick_TR(this, '<%# Container.KeyValue %>')">
            <%# Eval("TR")%></a>
    </DataItemTemplate>
</dx:GridViewDataColumn>
                            </Columns>

                        </dx:ASPxGridView>

                    </DetailRow>
                </Templates>


            </dx:ASPxGridView>


        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>


<dx:ASPxPopupControl ID="popUpAdd"
    ClientInstanceName="popUpAdd"
    runat="server" Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="Policy TBA"
    AllowDragging="true"
    EnableAnimation="true" CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="true" Width="100px">
    <ClientSideEvents PopUp="function(s,e){ ASPxClientEdit.ClearEditorsInContainerById('frmNew') ;}" />
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">

            <dx:ASPxFormLayout ID="frmNew" ClientInstanceName="frmNew" Width="100px" runat="server" AlignItemCaptionsInAllGroups="True" ColCount="1" RequiredMarkDisplayMode="None" ShowItemCaptionColon="False">

                <Items>


                    <dx:LayoutItem Caption="File">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxUploadControl ID="uploaddata"
                                    ClientInstanceName="uploaddata" UploadMode="Auto"
                                    runat="server" ShowProgressPanel="true" ShowUploadButton="true">
                                    <ValidationSettings MaxFileSize="4194304" AllowedFileExtensions=".xls" />

                                    <ClientSideEvents FileUploadComplete="function(s, e) {                                   
                                            popUpAdd.Hide();
                                            gridData.Refresh();
                                        }" />  
                                    <UploadButton Text="Upload">
                                        <Image IconID="actions_importimage_16x16">
                                        </Image>
                                    </UploadButton> 
                                </dx:ASPxUploadControl>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>



                </Items>
            </dx:ASPxFormLayout>



        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>






<asp:SqlDataSource ID="SqlDataSource_NoticeHeader" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * from tblNoticeHeader where NoticeCode = @NoticeCode order by NoticeID desc"
    DeleteCommand="delete from tblNoticeDetail where NoticeID = @NoticeID;
    delete from tblNoticeHeader where NoticeID = @NoticeID">


    <SelectParameters>
        <asp:Parameter Name="NoticeCode" />
    </SelectParameters>

    <DeleteParameters>
        <asp:Parameter Name="NoticeID" />
    </DeleteParameters>

</asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_Details" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="with data_TR as 
                    ( SELECT tblNoticeMailContact.Code,  COUNT(*) as TR 
                      FROM tblNoticeDetail inner join tblNoticeMailContact
                      on tblNoticeDetail.Code = tblNoticeMailContact.Code
                      where NoticeID = @NoticeID and tblNoticeMailContact.NoticeCode = @NoticeCode and tblNoticeDetail.f01='TR'   
                      group  by  tblNoticeMailContact.Name,tblNoticeMailContact.code 
                     ) 
                    , data_NOTR as 
                    ( SELECT tblNoticeMailContact.Code,  COUNT(*) as NOTR 
                      FROM tblNoticeDetail inner join tblNoticeMailContact
                      on tblNoticeDetail.Code = tblNoticeMailContact.Code
                      where NoticeID = @NoticeID and tblNoticeMailContact.NoticeCode = @NoticeCode and tblNoticeDetail.f01='NOTR'   
                      group  by  tblNoticeMailContact.Name,tblNoticeMailContact.code  
                      )
                      select tblNoticeMailContact.Code
                      , tblNoticeMailContact.Name
                      ,isnull(data_TR.TR,0) as TR  
                      ,isnull(data_NOTR.NOTR,0) as NOTR 
                      from tblNoticeMailContact 
                      left join data_TR on tblNoticeMailContact.Code = data_TR.code
                      left join data_NOTR on tblNoticeMailContact.Code = data_NOTR.code
                      where NoticeCode = @NoticeCode and isnull(data_TR.TR,0) + isnull(data_NOTR.NOTR,0) > 0" >


    <SelectParameters>
        <asp:Parameter Name="NoticeCode" />
        <asp:SessionParameter Name="NoticeID" SessionField="NoticeID" />
    </SelectParameters>



</asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_Details_TR" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * from tblNoticeDetail 
                where NoticeID = @NoticeID
                and Code =@Code
                and f01='TR'
    " >
    <SelectParameters>
        <asp:SessionParameter Name="NoticeID" SessionField="NoticeID" />
        <asp:Parameter Name="Code" />
    </SelectParameters>
</asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_Details_NOTR" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * from tblNoticeDetail 
                where NoticeID = @NoticeID
                and Code =@Code
                and f01='NOTR'
    " >
    <SelectParameters>
        <asp:Parameter Name="Code" />
        <asp:SessionParameter Name="NoticeID" SessionField="NoticeID" />
    </SelectParameters>
</asp:SqlDataSource>
 

 









