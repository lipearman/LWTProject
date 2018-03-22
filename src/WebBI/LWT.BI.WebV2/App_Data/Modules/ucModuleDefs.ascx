<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucModuleDefs.ascx.vb" Inherits="Modules_ucModuleDefs" %>
 
<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="" runat="server" EnableAdaptivity="true" Width="100%" HeaderImage-IconID="businessobjects_bonote_32x32"  >
    <PanelCollection>
        <dx:PanelContent>
 


            <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="SqlDataSource_Modules"
                KeyFieldName="ModuleDefId"     SettingsBehavior-AllowEllipsisInText="true" SettingsResizing-ColumnResizeMode="Control"
                AutoGenerateColumns="False" EnableAdaptivity="true" Width="100%"
                 SettingsSearchPanel-Visible="true"
                >
                <SettingsCommandButton>
                    <UpdateButton Text="Save" ></UpdateButton>
                </SettingsCommandButton>

                <SettingsBehavior  AllowEllipsisInText="True" />
               
                <Columns>

                    <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowEditButton="true" Width="120" />

<%--                    <dx:GridViewDataTextColumn FieldName="ModuleDefCode" CellStyle-Wrap="False" Width="150">
                        <PropertiesTextEdit>
                            <ValidationSettings Display="Dynamic">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>--%>
                    <dx:GridViewDataTextColumn FieldName="ModuleDefName" CellStyle-Wrap="False">
                        <PropertiesTextEdit>
                            <ValidationSettings Display="Dynamic">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>


<%--                    <dx:GridViewDataTextColumn FieldName="ModuleDefDesc" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>--%>




 <%--                   <dx:GridViewDataTextColumn FieldName="ModuleDefSourceFile" CellStyle-Wrap="False">
                        <PropertiesTextEdit>
                            <ValidationSettings Display="Dynamic">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>--%>

                   <dx:GridViewDataComboBoxColumn
                        FieldName="ModuleDefSourceFile"
                        CellStyle-Wrap="False">
                        <PropertiesComboBox DataSourceID="ObjectDataSource_ModuleFiles"  >
                            <ValidationSettings>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>


                    <dx:GridViewDataTextColumn FieldName="CreateDate" Visible="False" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ModifyDate" Visible="False" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>





                </Columns>

                <SettingsPager Mode="ShowAllRecords" />

                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"></SettingsEditing>
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



<asp:ObjectDataSource ID="ObjectDataSource_ModuleFiles" runat="server" SelectMethod="GetModulePath" TypeName="MyObjectDataSource">
    <SelectParameters>
        <asp:Parameter Name="rootpath" />
        <asp:Parameter Name="pathname" />
    </SelectParameters>
</asp:ObjectDataSource>


<asp:SqlDataSource ID="SqlDataSource_Modules" runat="server" ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
    SelectCommand="select * from PortalCfg_ModuleDefinitions Where PortalId=@PortalId Order By ModuleDefName"
    UpdateCommand="update PortalCfg_ModuleDefinitions 
    set  ModuleDefName=@ModuleDefName
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
     , newid()
     , @ModuleDefName
     , @ModuleDefDesc
    , @ModuleDefSourceFile
    , getdate()
    )



    ">
    <UpdateParameters>

        <asp:Parameter Name="ModuleDefName" />
        <asp:Parameter Name="ModuleDefSourceFile" />
        <asp:Parameter Name="ModuleDefId" DbType="Int32" />
    </UpdateParameters>




    <InsertParameters>
        <asp:SessionParameter Name="PortalId" SessionField="PortalId" />
        <asp:Parameter Name="ModuleDefName" />
        <asp:Parameter Name="ModuleDefDesc" />
        <asp:Parameter Name="ModuleDefSourceFile" />



    </InsertParameters>


    <SelectParameters>
        <asp:SessionParameter Name="PortalId" SessionField="PortalId" />
    </SelectParameters>

</asp:SqlDataSource>

