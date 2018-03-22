<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucSiteSettings.ascx.vb" Inherits="Modules_ucSiteSettings" %>
<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Projects" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>


            <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="SqlDataSource_Project"
                KeyFieldName="PortalId" AutoGenerateColumns="False" Width="600">
                <Columns>
 




                    <dx:GridViewCommandColumn  ShowEditButton="true" />
                      <dx:GridViewDataTextColumn FieldName="PortalCode" CellStyle-Wrap="False">
                         
                        <EditFormSettings  Visible="False" />
                    </dx:GridViewDataTextColumn>


                    
                     <dx:GridViewDataColumn FieldName="PortalId" CellStyle-Wrap="False">
                         <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataTextColumn FieldName="PortalName" CellStyle-Wrap="False">
                        <PropertiesTextEdit>
                            <ValidationSettings Display="Dynamic">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataColumn FieldName="UnderConstruction" CellStyle-Wrap="False">
                    </dx:GridViewDataColumn>

                  


                    <dx:GridViewDataTextColumn FieldName="cfg_conn" CellStyle-Wrap="True" Visible="false">
                        <EditFormSettings Visible="True" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="cfg_query_user" CellStyle-Wrap="True"  Visible="false">
                    <EditFormSettings Visible="True" />
                         </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="cfg_url_passport" CellStyle-Wrap="False">
                    </dx:GridViewDataTextColumn>


                     <dx:GridViewDataTextColumn FieldName="CreateDate" CellStyle-Wrap="False" >
                         <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                     <dx:GridViewDataTextColumn FieldName="ModifyDate" CellStyle-Wrap="False" >
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

<asp:SqlDataSource ID="SqlDataSource_Project" runat="server" ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
    SelectCommand="select * from PortalCfg_Globals Where PortalId=@PortalId"
    UpdateCommand="update PortalCfg_Globals 
    set PortalName=@PortalName
        ,UnderConstruction=@UnderConstruction
        ,PortalCode=@PortalCode
        ,PortalPage=@PortalPage
        ,ModifyDate=getdate()
        ,cfg_conn=@cfg_conn
        ,cfg_query_user=@cfg_query_user
        ,cfg_url_passport=@cfg_url_passport 
    Where PortalId=@PortalId"
    InsertCommand="Insert into PortalCfg_Globals(PortalName
                                                ,UnderConstruction
                                                ,PortalCode
                                                ,PortalPage
                                                ,CreateDate
                                                ,cfg_conn
                                                ,cfg_query_user
                                                ,cfg_url_passport)
    values(
     @PortalName
     , @UnderConstruction
     , @PortalCode
     , @PortalPage
     , getdate()
     , @cfg_conn
     , @cfg_query_user
     , @cfg_url_passport
    )



    ">
    <UpdateParameters>
        <asp:Parameter Name="PortalName" />
        <asp:Parameter Name="UnderConstruction" />
        <asp:Parameter Name="PortalCode" />
        <asp:Parameter Name="PortalPage" />
        <asp:Parameter Name="cfg_conn" />
        <asp:Parameter Name="cfg_query_user" />
        <asp:Parameter Name="cfg_url_passport" />
        <asp:Parameter Name="PortalId" DbType="Int32" />       
    </UpdateParameters>




    <InsertParameters>
        <asp:Parameter Name="PortalName" />
        <asp:Parameter Name="UnderConstruction" />
        <asp:Parameter Name="PortalCode" />
        <asp:Parameter Name="PortalPage" />
        <asp:Parameter Name="cfg_conn" />
        <asp:Parameter Name="cfg_query_user" />
        <asp:Parameter Name="cfg_url_passport" />
    </InsertParameters>


    <SelectParameters>
        <asp:SessionParameter Name="PortalId" SessionField="PortalId" />
    </SelectParameters>


</asp:SqlDataSource>