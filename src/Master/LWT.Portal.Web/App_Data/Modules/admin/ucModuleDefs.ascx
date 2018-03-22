<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucModuleDefs.ascx.vb" Inherits="LWT.Portal.Web.ucModuleDefs" %>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Modules" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>


            <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="SqlDataSource_Modules"
                KeyFieldName="ModuleDefId" AutoGenerateColumns="False" Width="600">
                <Columns>

                    <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowEditButton="true" />

                   <dx:GridViewDataComboBoxColumn FieldName="PortalId" CellStyle-Wrap="False"  GroupIndex="0" >
                        <PropertiesComboBox DataSourceID="SqlDataSource_Site" TextField="PortalName" ValueField="PortalId">
                           
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>

                      <dx:GridViewDataTextColumn FieldName="ModuleDefDesc" CellStyle-Wrap="False">
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="ModuleDefCode" CellStyle-Wrap="False">
                        <PropertiesTextEdit>
                            <ValidationSettings Display="Dynamic">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ModuleDefName" CellStyle-Wrap="False">
                        <PropertiesTextEdit>
                            <ValidationSettings Display="Dynamic">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="ModuleDefSourceFile" CellStyle-Wrap="False">
                        <PropertiesTextEdit>
                            <ValidationSettings Display="Dynamic">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>


                    <dx:GridViewDataTextColumn FieldName="CreateDate" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ModifyDate" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>





                </Columns>

                <SettingsPager Mode="ShowAllRecords" />

                <SettingsEditing Mode="EditFormAndDisplayRow" EditFormColumnCount="1"></SettingsEditing>
                <%--      <SettingsCommandButton>
                    <UpdateButton ButtonType="Button" Text="บันทึก"></UpdateButton>
                    <EditButton ButtonType="Button" Text="แก้ไข"></EditButton>
                    <DeleteButton ButtonType="Button" Text="ลบ"></DeleteButton>
                    <CancelButton ButtonType="Button" Text="ยกเลิก"></CancelButton>
                    <NewButton ButtonType="Button" Text="เพิ่ม"></NewButton>
                </SettingsCommandButton>--%>

                <SettingsPopup>
                    <EditForm Modal="true" Width="500" HorizontalAlign="Center" VerticalAlign="Middle" />
                </SettingsPopup>

            </dx:ASPxGridView>


        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_Modules" runat="server" ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
    SelectCommand="select * from PortalCfg_ModuleDefinitions"

    UpdateCommand="update PortalCfg_ModuleDefinitions 
    set PortalID=@PortalID
    ,ModuleDefName=@ModuleDefName
    ,ModuleDefDesc=@ModuleDefDesc
    ,ModuleDefSourceFile=@ModuleDefSourceFile
    ,ModifyDate=getdate()
    Where ModuleDefId=@ModuleDefId"

    InsertCommand="Insert into PortalCfg_ModuleDefinitions(
                                            PortalID
                                            ,ModuleDefCode
                                            ,ModuleDefName
                                            ,ModuleDefDesc
                                            ,ModuleDefSourceFile
                                            ,CreateDate )
    values(
       @PortalID
     , @ModuleDefCode
     , @ModuleDefName
     , @ModuleDefDesc
    , @ModuleDefSourceFile
    , getdate()
    )



    ">
    <UpdateParameters>
        <asp:Parameter Name="PortalID" />
        <asp:Parameter Name="ModuleDefName" />
        <asp:Parameter Name="ModuleDefDesc" />
        <asp:Parameter Name="ModuleDefSourceFile" />
        <asp:Parameter Name="ModuleDefId" DbType="Int32" />
    </UpdateParameters>




    <InsertParameters>
        <asp:Parameter Name="PortalID" />
        <asp:Parameter Name="ModuleDefCode" />
        <asp:Parameter Name="ModuleDefName" />
        <asp:Parameter Name="ModuleDefDesc" />
        <asp:Parameter Name="ModuleDefSourceFile" />
    </InsertParameters>

</asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_Site" runat="server" ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
    SelectCommand="select * from PortalCfg_Globals order by PortalName "></asp:SqlDataSource>
