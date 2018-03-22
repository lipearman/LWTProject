<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxCarBrandModelSetup.ascx.vb" Inherits="Modules_ucDevxCarBrandModelSetup" %>


<script>
    function OnFocusedCardChanged() {
        LoadingPanel.Show();
        cardView.GetCardValues(cardView.GetFocusedCardIndex(), 'ID;Name', OnGetCardValues);
    }
    function OnGetCardValues(values) {
        //alert(values);
        //LoadingPanel.Show();
        //lbUserName.SetText(values)
        //cbOpenPopup.PerformCallback(values);
        //lbModelImage.SetImageUrl('images/carlogo/' + values[1] + '.gif');
        lbBrandName.SetText(values[1]) 
        cbModel.PerformCallback(values[0]);
    }

</script>
<dx:ASPxCallback runat="server" ID="cbModel" ClientInstanceName="cbModel">
    <ClientSideEvents CallbackComplete="function(s,e){                                                                                     
                LoadingPanel.Hide(); 
                detailGrid.Refresh();
            }" />
</dx:ASPxCallback>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Car Brand" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent> 
            <dx:ASPxCardView ID="cardView" ClientInstanceName="cardView" runat="server" DataSourceID="SqlDataSource_CarBrand"
                Width="1000" KeyFieldName="ID"     >
                <ClientSideEvents FocusedCardChanged="OnFocusedCardChanged" />
                <SettingsBehavior AllowFocusedCard="true" ConfirmDelete="true"  />
                <SettingsPager>
                    <SettingsTableLayout ColumnCount="4" RowsPerPage="1" />
                </SettingsPager>
                <SettingsSearchPanel Visible="true" />
           <%--     <Settings ShowCustomizationPanel="false"  />--%>

                <Columns>
                    <dx:CardViewColumn FieldName="ID" />
                    <dx:CardViewColumn FieldName="Name" />
                </Columns>

                <CardLayoutProperties ColCount="1">
                    <Items>
                        <dx:CardViewColumnLayoutItem Caption="" ColSpan="1" HorizontalAlign="Center">
                            <Template>
                                <img src="images/carlogo/<%# Eval("Name")%>.gif" id="logo" width="100" />
                            </Template>
                        </dx:CardViewColumnLayoutItem>
                        <dx:CardViewColumnLayoutItem ShowCaption="False" HorizontalAlign="Center">
                            <Template>
                                <center><%# Eval("Name")%></center>
                            </Template>
                        </dx:CardViewColumnLayoutItem>
                    </Items>
                </CardLayoutProperties>
            </dx:ASPxCardView>


            <dx:ASPxFormLayout ID="frmdata" Width="100px" runat="server" ColCount="1">
                <Items>
                    <dx:LayoutGroup Caption="Model">
                        <Items>
                            <dx:LayoutItem Caption="" ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer
                                        ID="LayoutItemNestedControlContainer9"
                                        runat="server"
                                        SupportsDisabledAttribute="True">



 
<dx:ASPxLabel ID="lbBrandName" ClientInstanceName="lbBrandName" runat="server" Text=""></dx:ASPxLabel>


     <dx:ASPxGridView ID="detailGrid" ClientInstanceName="detailGrid" runat="server" DataSourceID="SqlDataSource_CarModel" KeyFieldName="ID"
                            Width="300" >
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" />
                            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />

                            <SettingsPopup>
                                <EditForm Modal="true" Width="300" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </SettingsPopup>

<%--                            <SettingsCommandButton>
                                <UpdateButton ButtonType="Button" Text="บันทึก"></UpdateButton>

                                <EditButton ButtonType="Button" Text="แก้ไข"></EditButton>
                                <DeleteButton ButtonType="Button" Text="ลบ"></DeleteButton>

                                <CancelButton ButtonType="Button" Text="ยกเลิก"></CancelButton>

                                <NewButton ButtonType="Button" Text="เพิ่ม"></NewButton>
                            </SettingsCommandButton>--%>




                            <Columns>
                                <dx:GridViewCommandColumn Width="100" CellStyle-Wrap="False" ShowNewButtonInHeader="true" ShowEditButton="true" ShowDeleteButton="true" />

                                <dx:GridViewDataColumn FieldName="Name">
                                </dx:GridViewDataColumn>


                            </Columns>

                        </dx:ASPxGridView>



                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                        </Items>

                    </dx:LayoutGroup>

                </Items>
                <Styles LayoutItem-Caption-Font-Bold="true" />
            </dx:ASPxFormLayout>




























            

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>


<asp:SqlDataSource ID="SqlDataSource_CarBrand" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select ID,Name,(select count(*) from tblCarBrandModel b where b.parentid = tblCarBrandModel.id ) as Model from tblCarBrandModel where ParentID is null and name <> 'ALL'  order by name ">
    <%--    
     InsertCommand="insert into tblCarBrandModel(ParentID,Name,IsActive) values(@ParentID,@Name,@IsActive)"
    UpdateCommand="update tblCarBrandModel set ParentID=@ParentID,Name=@Name,IsActive=@IsActive Where ID=@ID"
       
    
    <InsertParameters>
        <asp:Parameter Name="ID" />
        <asp:Parameter Name="ParentID" />
        <asp:Parameter Name="Name" />
        <asp:Parameter Name="IsActive" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="ID" />
        <asp:Parameter Name="ParentID" />
        <asp:Parameter Name="Name" />
        <asp:Parameter Name="IsActive" />
    </UpdateParameters>--%>
</asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_CarModel" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * from tblCarBrandModel where ParentID is not null and ParentID = @ParentID order by name "
    UpdateCommand="update tblCarBrandModel set Name = @Name where ID=@ID"
    InsertCommand="insert into tblCarBrandModel(Name,ParentID) values(@Name,@ParentID)"
    DeleteCommand="delete tblCarBrandModel where ID=@ID">
    <SelectParameters>
        <asp:SessionParameter Name="ParentID" SessionField="ParentID" />
         
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="ID" />
        <asp:Parameter Name="Name" />
    </UpdateParameters>
    <InsertParameters>
        <asp:SessionParameter Name="ParentID" SessionField="ParentID" />
        <asp:Parameter Name="Name" />
        
    </InsertParameters>
    <DeleteParameters>
        <asp:Parameter Name="ID" />
    </DeleteParameters>
</asp:SqlDataSource>
