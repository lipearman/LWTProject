<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxAttribute.ascx.vb" Inherits="Modules_ucDevxAttribute" %>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="" runat="server" EnableAdaptivity="true" Width="100%" HeaderImage-IconID="support_feature_16x16office2013">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxGridView ID="grid"
                ClientInstanceName="grid"
                runat="server" Width="100%" KeyFieldName="ID"
                DataSourceID="SqlDataSource_Data"
                AutoGenerateColumns="False" SettingsBehavior-ConfirmDelete="true"
                SettingsPager-Mode="ShowAllRecords">
                <ClientSideEvents
                    RowDblClick="function(s, e) {
                                                   s.StartEditRow(e.visibleIndex);
                      
                                                }     
                                                 " />

                <%--<SettingsCustomizationDialog Enabled="true" ShowSortingPage="false" ShowGroupingPage="false" />--%>
                <SettingsBehavior AllowEllipsisInText="True" AllowFocusedRow="true" />
                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"></SettingsEditing>
                <SettingsResizing ColumnResizeMode="Control" />
                <SettingsPopup>
                    <EditForm Modal="true" HorizontalAlign="Center" VerticalAlign="Above"  />
                </SettingsPopup>

                <SettingsCommandButton>
                    <EditButton Image-IconID="edit_edit_16x16office2013" />
                    <UpdateButton Text="Save" Image-IconID="actions_save_16x16devav"></UpdateButton>
                    <CancelButton Image-IconID="actions_cancel_16x16office2013"></CancelButton>
                </SettingsCommandButton>


                <Toolbars>
                    <dx:GridViewToolbar Position="Top" ItemAlign="Left">
                        <Items>
                            <dx:GridViewToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxComboBox Width="300" Caption="Cube" NullText="Select Cube" ID="ASPxComboBox1" runat="server" ValueField="CUBE_ID" TextField="CUBE" DataSourceID="SqlDataSource_Cube">
                                        <ClientSideEvents SelectedIndexChanged="function(s,e)
                                            {
                                                 cbCube.PerformCallback(s.GetValue());
                                        }" />
                                    </dx:ASPxComboBox>
                                </Template>
                            </dx:GridViewToolbarItem>
                           
                            <%--<dx:GridViewToolbarItem Command="Edit" />--%>






                            <dx:GridViewToolbarItem Command="Delete" BeginGroup="true" />
                            <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />

                        <dx:GridViewToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButton runat="server" AutoPostBack="false" Image-IconID="actions_download_16x16office2013" Text="Retrieve Fields">

                                        <ClientSideEvents Click="function(s,e){
                                                 if(confirm('Confirm to Retrieve Fields?'))
                                                 {
                                                    cbRetrieve.PerformCallback('');
                                                 }
                                             }" />
                                    </dx:ASPxButton>
                                </Template>
                            </dx:GridViewToolbarItem>


                        </Items>
                    </dx:GridViewToolbar>
                </Toolbars>

                <Columns>

                    <%--<dx:GridViewCommandColumn ShowEditButton="true" Width="50" />--%>
                    <%--   
                        <dx:GridViewDataTextColumn FieldName="CUBE_ID" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="DATABASE" Settings-AllowHeaderFilter="True" SettingsHeaderFilter-Mode="CheckedList" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="CUBE" Settings-AllowHeaderFilter="True" SettingsHeaderFilter-Mode="CheckedList" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="BASE_CUBE_NAME" Settings-AllowHeaderFilter="True" SettingsHeaderFilter-Mode="CheckedList" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    --%>
                    <dx:GridViewDataTextColumn Width="400" FieldName="FIELD" Settings-AllowGroup="False" CellStyle-Wrap="False">
                        <EditItemTemplate>
                            <dx:ASPxLabel runat="server" Text='<%# Eval("FIELD")%>'></dx:ASPxLabel>
                        </EditItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="TYPE" Settings-AllowGroup="False" CellStyle-Wrap="False">
                        <EditItemTemplate>
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%# Eval("TYPE")%>'></dx:ASPxLabel>
                        </EditItemTemplate>
                    </dx:GridViewDataTextColumn>


                    <dx:GridViewDataTextColumn FieldName="ATTRIBUTE" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <%--   <dx:GridViewDataTextColumn FieldName="VISIBLE" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    --%>


                    <dx:GridViewDataComboBoxColumn
                        FieldName="FormatType"
                        CellStyle-Wrap="False">
                        <PropertiesComboBox DataSourceID="SqlDataSource_FormatType" ValueField="FormatType" TextField="FormatType">

                            <ValidationSettings>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>


                    <dx:GridViewDataTextColumn FieldName="FOLDER" Settings-AllowHeaderFilter="True" SettingsHeaderFilter-Mode="CheckedList" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <%--<dx:GridViewDataTextColumn FieldName="TYPE" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>--%>

                    <dx:GridViewDataTextColumn FieldName="Description" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>



                </Columns>
            </dx:ASPxGridView>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_FormatType" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="
    select convert(varchar(50),'#0') as FormatType 
    union all 
    select convert(varchar(50),'{0:N0}') as FormatType 
    union all 
    select convert(varchar(50),'{0:N1}') as FormatType
    union all
    select convert(varchar(50),'{0:N2}') as FormatType 

   union all
    select convert(varchar(50),'{0:yyyy-MM-dd}') as FormatType 

    "></asp:SqlDataSource>






<asp:SqlDataSource ID="SqlDataSource_Data" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select tblAttribute.ID
     ,tblCube.CUBE_ID
    ,tblCube.[DATABASE]
    ,tblCube.[CUBE]
    ,tblCube.[BASE_CUBE_NAME]

    ,tblAttribute.[FOLDER]
    ,tblAttribute.[ATTRIBUTE]
    ,tblAttribute.[FIELD]
    ,tblAttribute.[VISIBLE]
    ,tblAttribute.[FormatType] 
    ,tblAttribute.[Description] 
    ,tblAttribute.[TYPE]

    from tblAttribute

    inner join tblCube on tblAttribute.CUBE_ID = tblCube.CUBE_ID

    where tblCube.CUBE_ID=@CUBE_ID
    "
    DeleteCommand="delete from tblAttribute where ID=@ID"
    UpdateCommand="update tblAttribute 
     set FOLDER=@FOLDER
    ,ATTRIBUTE=@ATTRIBUTE
    ,FormatType=@FormatType 
    ,Description=@Description 
    where ID=@ID">
    <SelectParameters>
        <asp:SessionParameter Name="CUBE_ID" SessionField="CUBE_ID" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="ID" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="FOLDER" />
        <asp:Parameter Name="ATTRIBUTE" />
        <asp:Parameter Name="Description" />
        <asp:Parameter Name="FormatType" Type="String" />
        <asp:Parameter Name="ID" />
    </UpdateParameters>
</asp:SqlDataSource>




<asp:SqlDataSource ID="SqlDataSource_Cube" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select tblCube.CUBE_ID
    , tblCube.[DATABASE] + '/' + tblCube.[CUBE] as CUBE
    , tblCube.[BASE_CUBE_NAME] 
    from tblCube  
    order by   tblCube.[DATABASE] + '/' + tblCube.[CUBE] 
    "></asp:SqlDataSource>


<dx:ASPxCallback ID="cbCube" runat="server" ClientInstanceName="cbCube">
    <ClientSideEvents CallbackComplete="function(s,e){grid.Refresh();}" />
</dx:ASPxCallback>

<dx:ASPxCallback ID="cbRetrieve" runat="server" ClientInstanceName="cbRetrieve">
    <ClientSideEvents CallbackComplete="function(s,e){
        
        if(e.result == 'success'){
            grid.Refresh();
        }
        else
        {
            alert(e.result);
        }
        
        
        }" />
</dx:ASPxCallback>



<dx:ASPxPivotGrid ID="pivotGrid"
    runat="server" Visible="false"
    CustomizationFieldsLeft="600"
    CustomizationFieldsTop="400"
    EnableRowsCache="false"
    OptionsView-DataHeadersDisplayMode="Popup"
    ClientInstanceName="pivotGrid"
    ClientIDMode="AutoID">
</dx:ASPxPivotGrid>
