<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxDashBoard.ascx.vb" Inherits="Modules_ucDevxDashBoard" %>


<script type="text/javascript">
    var lastView = null;

    function OnViewChanged(cmbView) {

        lastView = cmbView.GetValue().toString();

        if (treeList.GetEditor("DB_ID").InCallback())
            lastView = cmbView.GetValue().toString();
        else
            treeList.GetEditor("DB_ID").PerformCallback(cmbView.GetValue());
    }
    function OnEndCallback(s, e) {
        alert(lastView);
        if (lastView) {
            treeList.GetEditor("DB_ID").PerformCallback(cmbView.GetValue());
            lastView = null;
        }
    }
    function ShowDetailPopup(RID) {
        //alert(RID);
        //LoadingPanel.Show();
        cbPreview.PerformCallback(RID);
    }
</script>
<dx:ASPxCallback ID="cbPreview" runat="server" ClientInstanceName="cbPreview">
    <ClientSideEvents CallbackError="function(s, e) { 
            //LoadingPanel.Hide(); 
        }"
        CallbackComplete="function(s, e) { 
            LoadingPanel.Hide(); 
            window.top.clientView.SetContentUrl('applications/DB/Master/Preview.aspx');
            window.top.clientView.Show();            
        }" />
</dx:ASPxCallback>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Report" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>


            <dx:ASPxTreeList ID="treeList" ClientInstanceName="treeList" runat="server" AutoGenerateColumns="false" Width="900"
                DataSourceID="SqlDataSource_Data" Settings-ShowRoot="true"
                SettingsBehavior-AutoExpandAllNodes="true"
                SettingsEditing-ConfirmDelete="true"
                KeyFieldName="ID"
                ParentFieldName="ParentID">
               <%-- <SettingsEditing Mode="EditFormAndDisplayNode" />--%>
                <SettingsPopupEditForm Width="500" />
                 <SettingsEditing Mode="EditForm" EditFormColumnCount="1"></SettingsEditing>
                <Columns>
                    <dx:TreeListCommandColumn ShowNewButtonInHeader="true">
                        <EditButton Visible="true" />
                        <NewButton Visible="true" />
                        <DeleteButton Visible="false" />
                        <UpdateButton Text="Save"></UpdateButton>
                    </dx:TreeListCommandColumn>


                    <dx:TreeListComboBoxColumn FieldName="DB_ID" Caption="DashBoard">
                        <PropertiesComboBox DataSourceID="SqlDataSource_DB" ValueField="DB_ID" TextField="DB_TITLE">

                        </PropertiesComboBox>


                    </dx:TreeListComboBoxColumn>

                    <dx:TreeListSpinEditColumn FieldName="No" Width="50" CellStyle-HorizontalAlign="Left">
                        <PropertiesSpinEdit>
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                        </PropertiesSpinEdit>
                    </dx:TreeListSpinEditColumn>

                    <dx:TreeListTextColumn FieldName="TITLE" Caption="Report" Width="200" CellStyle-Wrap="False">
                        <PropertiesTextEdit>
                            <ValidationSettings Display="Dynamic">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </PropertiesTextEdit>

                    </dx:TreeListTextColumn>

                    <dx:TreeListMemoColumn FieldName="DESCRIPTION" Width="500" CellStyle-Wrap="True">
                    </dx:TreeListMemoColumn>



                    <dx:TreeListTextColumn Caption="#" Name="View"  >
                        <DataCellTemplate>
                            <%# PreviewDB(Eval("DB_ID").ToString())%>
                        </DataCellTemplate>
                        <EditFormSettings Visible="False" />

                    </dx:TreeListTextColumn>
                      


                </Columns>

            </dx:ASPxTreeList>


        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_Data" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select * from tblDashBoard order by No "
    UpdateCommand="update tblDashBoard set TITLE=@TITLE,DB_ID=@DB_ID, No=@No,ParentID=@ParentID,DESCRIPTION=@DESCRIPTION Where ID=@ID"
    InsertCommand="Insert into tblDashBoard(TITLE,DB_ID,No,ParentID,DESCRIPTION) values(@TITLE,@DB_ID,@No,@ParentID,@DESCRIPTION)"
    DeleteCommand="delete from tblDashBoard where ID=@ID">
    <DeleteParameters>
        <asp:Parameter Name="RID" DbType="Int32" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="TITLE" DbType="String" />
        <asp:Parameter Name="DB_ID" DbType="Int32" />
        <asp:Parameter Name="DESCRIPTION" DbType="String" />
        <asp:Parameter Name="ParentID" DbType="Int32" />
        <asp:Parameter Name="No" DbType="Int32" />
        <asp:Parameter Name="ID" DbType="Int32" />
    </UpdateParameters>

    <InsertParameters>
        <asp:Parameter Name="TITLE" DbType="String" />
        <asp:Parameter Name="DB_ID" DbType="Int32" />
        <asp:Parameter Name="DESCRIPTION" DbType="String" />
        <asp:Parameter Name="ParentID" DbType="Int32" />
        <asp:Parameter Name="No" DbType="Int32" />
    </InsertParameters>
</asp:SqlDataSource>

 
<asp:SqlDataSource ID="SqlDataSource_DB" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select null as DB_ID, null as DB_TITLE 
    union all 
    select DB_ID,DB_TITLE 
    from tblDashBoard_DataSource 
    order by DB_TITLE "></asp:SqlDataSource>


<dx:ASPxPopupControl ID="clientViewDB" runat="server" ClientInstanceName="clientViewDB"
    Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="Business Intelligence"
    AllowDragging="true"
    AllowResize="True"
    EnableAnimation="true"
    CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="true"
    ShowPageScrollbarWhenModal="true"
    ShowMaximizeButton="true"
    Width="900"
    Height="790"
    FooterText=""
    ShowFooter="true">
    <ClientSideEvents Shown="function(s,e){ window.setTimeout(function() {LoadingPanel.Hide();},2000);}" />

    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl5" runat="server">
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
