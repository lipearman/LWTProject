<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxAESetup.ascx.vb" Inherits="Modules_ucDevxAESetup" %>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Account Executive" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>
            




                <dx:ASPxButton ID="btnImportAE" runat="server" Text="นำเข้าจาก SIBIS" Image-IconID="actions_add_16x16">
                    <ClientSideEvents Click="function(s, e) {           
                       popUpAddAE.Show();        
                       e.processOnServer = false; 
                      }" />
                </dx:ASPxButton>
           
            
                <dx:ASPxGridView ID="gridAE" ClientInstanceName="gridAE" runat="server" DataSourceID="SqlDataSource_AE"
                    KeyFieldName="AccountExecutive" AutoGenerateColumns="False" Width="800">
                    <SettingsPager Mode="ShowPager">
                        <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                    </SettingsPager>
                    <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" />
                    <SettingsSearchPanel Visible="true" />
                    <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />

                 <%--   <SettingsCommandButton>
                        <UpdateButton ButtonType="Button" Text="บันทึก"></UpdateButton>

                        <EditButton ButtonType="Button" Text="แก้ไข"></EditButton>
                        <DeleteButton ButtonType="Button" Text="ลบ"></DeleteButton>

                        <CancelButton ButtonType="Button" Text="ยกเลิก"></CancelButton>
                    </SettingsCommandButton>--%>


                    <SettingsPopup>
                        <EditForm Modal="true" Width="300" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </SettingsPopup>


                    <Columns>
                        <dx:GridViewCommandColumn Width="100" CellStyle-Wrap="False" ShowNewButtonInHeader="false" ShowEditButton="true" ShowDeleteButton="true" />
                        <dx:GridViewDataColumn FieldName="AccountExecutive" ReadOnly="true" Width="100" Settings-AutoFilterCondition="BeginsWith">
                            <Settings AllowHeaderFilter="True" HeaderFilterMode="CheckedList" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataTextColumn FieldName="Name" Width="400">
                            <PropertiesTextEdit>
                                <ValidationSettings Display="Dynamic">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </PropertiesTextEdit>

                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="Address1" Width="100">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataColumn>
                    </Columns>
                </dx:ASPxGridView>




          
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_AE" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * from [dbo].[Register.AccountExecutive] order by AccountExecutive"
    DeleteCommand="delete from [Register.AccountExecutive] where AccountExecutive=@AccountExecutive"
    UpdateCommand="update [Register.AccountExecutive] set name=@Name where AccountExecutive=@AccountExecutive ">
    <DeleteParameters>
        <asp:Parameter Name="AccountExecutive" Type="String" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="AccountExecutive" Type="String" />
        <asp:Parameter Name="Name" Type="String" />
    </UpdateParameters>

</asp:SqlDataSource>

<dx:ASPxPopupControl ID="popUpAddAE"
    ClientInstanceName="popUpAddAE"
    runat="server" Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="Account Executive"
    AllowDragging="true"
    EnableAnimation="true" CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="true" Width="100px">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">

            <dx:ASPxFormLayout ID="frmNewAE" Width="100px" runat="server" AlignItemCaptionsInAllGroups="True" ColCount="1" RequiredMarkDisplayMode="None" ShowItemCaptionColon="False">
                <Items>

                    <dx:LayoutItem Caption="Account Executive">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer
                                ID="LayoutItemNestedControlContainer9"
                                runat="server"
                                SupportsDisabledAttribute="True">
                                <dx:ASPxGridLookup ID="GridAELookup" runat="server"
                                    SelectionMode="Single"
                                    DataSourceID="SqlDataSource_AELookup"
                                    ClientInstanceName="GridAELookup"
                                    KeyFieldName="Code" Width="100px"
                                    TextFormatString="{0}">
                                    <GridViewProperties>
                                        <SettingsBehavior EnableRowHotTrack="true" />
                                        <SettingsSearchPanel Visible="true" />
                                    </GridViewProperties>
                                    <ClearButton Visibility="True"></ClearButton>
                                    <ValidationSettings>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="Code" Width="50" Settings-AutoFilterCondition="BeginsWith" />
                                        <dx:GridViewDataColumn FieldName="Name" Width="500" CellStyle-Wrap="False" Settings-AutoFilterCondition="Contains" />
                                    </Columns>
                                </dx:ASPxGridLookup>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>


                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxButton ID="btnSaveAE" runat="server" ValidationContainerID="frmNewAE"
                                    Text="Save" Width="100">
                                    <ClientSideEvents Click="function(s, e) {
                                        if(ASPxClientEdit.AreEditorsValid()) 
                                        {                                              
                                            LoadingPanel.Show();
                                            cbSaveAddAE.PerformCallback('');
                                        }
                                        else
                                        {
                                            alert('กรุณากรอกข้อมูลให้ครบ');
                                        }
                                        e.processOnServer = false;
                                    }" />
                                </dx:ASPxButton>
                                <dx:ASPxCallback runat="server" ID="cbSaveAddAE" ClientInstanceName="cbSaveAddAE">
                                    <ClientSideEvents CallbackComplete="function(s,e){ 
                                             LoadingPanel.Hide();   
                                             if (e.result != 'success') {
                                                alert(e.result);                                                
                                             } 
                                             else
                                             {
                                                gridAE.Refresh();
                                                popUpAddAE.Hide();
                                             }
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

<asp:SqlDataSource ID="SqlDataSource_AELookup" runat="server" ConnectionString="<%$ ConnectionStrings:SIBISDBConnectionString %>"
    SelectCommand="SELECT rtrim(AccountExecutive) as Code, rtrim(AccountExecutive) + ' - ' + rtrim(Name) as Name 
    FROM AccountExecutive  order by AccountExecutive"></asp:SqlDataSource>

<%--<script>

    $(function () {

        guidely.add({
            attachTo: '#target-1'
             , anchor: 'top-left'
             , title: 'Guide Title'
             , text: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut.'
        });

        guidely.add({
            attachTo: '#target-2'
            , anchor: 'top-right'
            , title: 'Guide Title'
            , text: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut.'
        });

        guidely.add({
            attachTo: '#target-3'
            , anchor: 'middle-middle'
            , title: 'Guide Title'
            , text: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut.'
        });

        guidely.add({
            attachTo: '#target-4'
            , anchor: 'top-right'
            , title: 'Guide Title'
            , text: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut.'
        });




    });

</script>
                <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Help" >
                    <ClientSideEvents Click="function(s, e) {           
                               guidely.init({ welcome: false, startTrigger: false });
                       e.processOnServer = false; 
                      }" />
                </dx:ASPxButton>


            <div id="target-1"> </div>
            <br /> <br /> <br /> <br />
            <hr />



  <div id="target-2">

      yyyy


  </div>
<hr /><br /> <br /> <br /> <br />
  <div id="target-3">

      yyyy


  </div>
<hr /><br /> <br /> <br /> <br />
  <div id="target-4">
      yyyy



  </div>--%>