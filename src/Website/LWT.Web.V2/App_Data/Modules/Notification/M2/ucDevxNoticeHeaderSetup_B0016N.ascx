<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxNoticeHeaderSetup_B0016N.ascx.vb" Inherits="Modules_ucDevxNoticeHeaderSetup_B0016N" %>
<%@ Register Assembly="PdfViewer" Namespace="PdfViewer" TagPrefix="cc1" %>
<style>
    .x-grid-row-summary .x-grid-cell-inner
    {
        font-weight: bold;
        color: red;
    }
</style>

<dx:ASPxGridView runat="server"
    ID="MyGrid"
    SettingsLoadingPanel-Mode="ShowAsPopup"
    ClientInstanceName="MyGrid"
    Width="100%" SettingsEditing-EditFormColumnCount="1"
    KeyFieldName="NoticeID" SettingsBehavior-ConfirmDelete="true"
    DataSourceID="SqlDataSource_gridData"
    Settings-HorizontalScrollBarMode="Visible">

    <SettingsEditing Mode="PopupEditForm" />

    <SettingsPopup>
        <EditForm Modal="true" Width="400" HorizontalAlign="Center" VerticalAlign="Middle" />
    </SettingsPopup>
    <SettingsCommandButton>
        <UpdateButton Text="Save"></UpdateButton>
    </SettingsCommandButton>
    <SettingsBehavior AllowDragDrop="True" AllowFocusedRow="true"
        ColumnResizeMode="Control" />
    <Toolbars>
        <dx:GridViewToolbar ItemAlign="Left">
            <Items>

                <dx:GridViewToolbarItem BeginGroup="true">
                    <Template>
                        <dx:ASPxButton ID="btnDownloadFormat" OnClick="btnDownloadFormat_click"
                            runat="server" Border-BorderWidth="0"
                            Image-IconID="actions_download_16x16office2013"
                            ToolTip="Excel for Import"
                            Text="Export">
                        </dx:ASPxButton>
                    </Template>
                </dx:GridViewToolbarItem>


                <dx:GridViewToolbarItem Command="New" />
                <dx:GridViewToolbarItem Command="Edit" />
                <dx:GridViewToolbarItem Command="Delete" />
                <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />

            </Items>
        </dx:GridViewToolbar>
    </Toolbars>




    <Columns>
        <dx:GridViewDataTextColumn Caption="Upload Data" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center" Width="80">
            <EditFormSettings Visible="False" />
            <DataItemTemplate>
                <dx:ASPxButton ID="cmdUpload" runat="server" Border-BorderWidth="0" AutoPostBack="false" Image-IconID="save_saveto_16x16office2013" ToolTip="Upload Data">
                    <ClientSideEvents Click="function(s,e){
                         LoadingPanel.Show();
                        cbShowUpload.PerformCallback(s.cpNoticeID);
                         
                    }" />
                </dx:ASPxButton>

            </DataItemTemplate>
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="NoticeID" Width="70" CellStyle-HorizontalAlign="Center" CellStyle-Wrap="False">
            <EditItemTemplate>
                <dx:ASPxLabel runat="server" Text='<%# Eval("NoticeID")%>'></dx:ASPxLabel>
            </EditItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="NoticeTitle" CellStyle-Wrap="False" Width="300">
            <PropertiesTextEdit Width="300" ValidationSettings-RequiredField-IsRequired="false"></PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataDateColumn FieldName="NoticeDate" CellStyle-Wrap="False">
            <PropertiesDateEdit ValidationSettings-RequiredField-IsRequired="true"></PropertiesDateEdit>
        </dx:GridViewDataDateColumn>
        <dx:GridViewDataTextColumn FieldName="CreationDate" CellStyle-Wrap="False" Width="120" EditFormSettings-Visible="False" />
        <dx:GridViewDataTextColumn FieldName="CreationBy" CellStyle-Wrap="False" EditFormSettings-Visible="False" />
        <dx:GridViewDataTextColumn FieldName="SendDate" CellStyle-Wrap="False" EditFormSettings-Visible="False" />
        <dx:GridViewDataTextColumn FieldName="ReSendDate" CellStyle-Wrap="False" EditFormSettings-Visible="False" />


        <dx:GridViewDataTextColumn CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center" Caption="Endorsement" Width="100">
            <EditFormSettings Visible="False" />
            <DataItemTemplate>

                <dx:ASPxButton ID="cmdExportEnds" AutoPostBack="false"  CommandArgument='<%# Eval("NoticeID")%>'
                    OnClick="btnDownloadEndorse_click" runat="server" 
                    ToolTip="Export Endorsement" Image-IconID="export_exporttoxlsx_16x16office2013">

                    <ClientSideEvents Click="function(s,e){
                        //cbExportEnds.PerformCallback(s.cpNoticeID);
                         e.processOnServer = true;
                    }" />
                </dx:ASPxButton>
                <dx:ASPxButton ID="cmdSendEnds" AutoPostBack="false" runat="server" ToolTip="Send Endorsement" Image-IconID="mail_mail_16x16">

                    <ClientSideEvents Click="function(s,e){
                        cbSendEnds.PerformCallback(s.cpNoticeID);
                    }" />
                </dx:ASPxButton>

            </DataItemTemplate>
        </dx:GridViewDataTextColumn>


<%--        <dx:GridViewCommandColumn  Width="50" ButtonRenderMode="Image">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="cmdExportAmend">
                        <Image ToolTip="Export Amendment" IconID="export_exporttoxlsx_16x16office2013"/>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
--%>


        <dx:GridViewDataTextColumn CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center" Caption="Amendment" Width="100">
            <EditFormSettings Visible="False" />
            <DataItemTemplate>

                <dx:ASPxButton ID="cmdExportAmend" AutoPostBack="false" CommandArgument='<%# Eval("NoticeID")%>' OnClick="btnDownloadAmend_click"  runat="server" ToolTip="Export Amendment" Image-IconID="export_exporttoxlsx_16x16office2013">

                    <ClientSideEvents Click="function(s,e){
                         //cbExportAmend.PerformCallback(s.cpNoticeID);
                        e.processOnServer = true;
                    }" />
                </dx:ASPxButton>
                <dx:ASPxButton ID="cmdSendAmend" AutoPostBack="false" runat="server" ToolTip="Send Amendment" Image-IconID="mail_mail_16x16">

                    <ClientSideEvents Click="function(s,e){
                        cbSendAmend.PerformCallback(s.cpNoticeID);
                    }" />
                </dx:ASPxButton>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
    </Columns>

</dx:ASPxGridView>

<dx:ASPxGridViewExporter runat="server" ID="GridExporter" GridViewID="MyGrid">
    <Styles>
        <Default Font-Names="Tahoma" Font-Size="Medium" Wrap="False"></Default>
    </Styles>
</dx:ASPxGridViewExporter>

<asp:SqlDataSource ID="SqlDataSource_gridData" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select top 1000 * from tblNoticeHeader where NoticeCode=@NoticeCode"
    DeleteCommand="delete from tblNoticeDetail where NoticeID=@NoticeID;
    delete tblNoticeHeader where NoticeID=@NoticeID">
    <SelectParameters>
        <asp:Parameter Name="NoticeCode" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="NoticeID" />
    </DeleteParameters>

</asp:SqlDataSource>

<dx:ASPxCallback runat="server" ID="cbShowUpload" ClientInstanceName="cbShowUpload">
    <ClientSideEvents CallbackComplete="function(s,e){
           LoadingPanel.Hide();  
           pcUpload.Show();
        }" />
</dx:ASPxCallback>

<dx:ASPxCallback runat="server" ID="cbUpload" ClientInstanceName="cbUpload">
    <ClientSideEvents CallbackComplete="function(s,e){  
        alert(e.result); 
        pcUpload.Hide();

        }" />
</dx:ASPxCallback>
<dx:ASPxCallback runat="server" ID="cbExportEnds" ClientInstanceName="cbExportEnds">
     <ClientSideEvents CallbackComplete="function(s,e){  
         if(e.result!='success'){
            alert(e.result); 
         }
        }" />
</dx:ASPxCallback>
<dx:ASPxCallback runat="server" ID="cbSendEnds" ClientInstanceName="cbSendEnds">
    <ClientSideEvents CallbackComplete="function(s,e){  
        alert(e.result); 
        }" />
</dx:ASPxCallback>
<dx:ASPxCallback runat="server" ID="cbExportAmend" ClientInstanceName="cbExportAmend">
     <ClientSideEvents CallbackComplete="function(s,e){  
         if(e.result!='success'){
            alert(e.result); 
         }
        }" />
</dx:ASPxCallback>
<dx:ASPxCallback runat="server" ID="cbSendAmend" ClientInstanceName="cbSendAmend">
    <ClientSideEvents CallbackComplete="function(s,e){  
        alert(e.result); 
        }" />
</dx:ASPxCallback>


















 <dx:ASPxPopupControl ID="pcUpload" ClientInstanceName="pcUpload"
        runat="server" 
        CloseAction="CloseButton" 
        CloseOnEscape="true" 
        Modal="True"
        PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" 
        
        HeaderText="Upload Data"
        AllowDragging="True" 
        PopupAnimationType="None" 
        EnableViewState="False">
        <ClientSideEvents PopUp="function(s, e) { 
            ASPxClientEdit.ClearGroup('entryGroup'); 
            tbdata.Focus(); 
            }" />
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <dx:ASPxPanel ID="Panel1" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <table>
                                <tr>
                                    
                                    <td class="pcmCellText">
                                       
                                        <dx:ASPxMemo ID="tbdata"  
                                            runat="server" 
                                            Width="480px"  Height="340"
                                            ClientInstanceName="tbdata">
                                              <ValidationSettings 
                                                  EnableCustomValidation="True" 
                                                  ValidationGroup="entryGroup" 
                                                  SetFocusOnError="True"
                                                ErrorDisplayMode="Text" 
                                                  ErrorTextPosition="Bottom" 
                                                  CausesValidation="True">
                                                <RequiredField IsRequired="True" />
                                             
                                                <ErrorFrameStyle Font-Size="10px">
                                                    <ErrorTextPaddings PaddingLeft="0px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>



                                        </dx:ASPxMemo>
                                    </td>
                                    <td rowspan="4">
                                        <div class="pcmSideSpacer">
                                        </div>
                                    </td>
                                </tr>
                                 
                                <tr>
                                    <td colspan="2">
                                        <div class="pcmButton">
                                            <dx:ASPxButton ID="btOK" runat="server" Text="OK" Width="80px" AutoPostBack="False" style="float: left; margin-right: 8px">
                                                <ClientSideEvents Click="function(s, e) { 
                                                    if(ASPxClientEdit.ValidateGroup('entryGroup')) 
                                                         cbUpload.PerformCallback(tbdata.GetText());
                                                    }" />
                                            </dx:ASPxButton>
                                            <dx:ASPxButton ID="btCancel" runat="server" Text="Cancel" Width="80px" AutoPostBack="False" style="float: left; margin-right: 8px">
                                                <ClientSideEvents Click="function(s, e) { 
                                                        pcUpload.Hide(); 
                                                    }" />
                                            </dx:ASPxButton>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
 
            </dx:PopupControlContentControl>
        </ContentCollection>
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>




 