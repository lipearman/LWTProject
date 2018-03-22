<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxReports.ascx.vb" Inherits="Modules_ucDevxReports" %>
xxxx

<script type="text/javascript">

    function ShowMasterBIPopup(RID) {
        window.top.LoadingPanel.Show();
        cbPreview.PerformCallback(RID);
    }
</script>



<dx:ASPxCallback ID="cbPreview" runat="server" ClientInstanceName="cbPreview">
    <ClientSideEvents CallbackError="function(s, e) { 
            LoadingPanel.Hide(); 
        }"
        CallbackComplete="function(s, e) { 
            LoadingPanel.Hide(); 
            window.top.clientView.SetContentUrl('applications/BI/Master/Preview.aspx');
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
                KeyFieldName="RID"
                ParentFieldName="ParentID">
                <SettingsEditing Mode="EditFormAndDisplayNode" />
                <SettingsPopupEditForm Width="500" />
                <Columns>
                    <dx:TreeListCommandColumn ShowNewButtonInHeader="true">
                        <EditButton Visible="true" />
                        <NewButton Visible="true" />
                        <DeleteButton Visible="false" />
                        <UpdateButton Text="Save"></UpdateButton>
                    </dx:TreeListCommandColumn>

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

                    <%--    <dx:TreeListComboBoxColumn FieldName="DEPARTMENT" Caption="DEP" CellStyle-Wrap="False">
                         
                        <PropertiesComboBox>
                            <Items>
                                <dx:ListEditItem Text="APD" Value="APD" />
                            </Items>
                            <ValidationSettings Display="Dynamic">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </PropertiesComboBox>

                    </dx:TreeListComboBoxColumn>--%>


                    <dx:TreeListComboBoxColumn FieldName="VIEW_ID" Caption="Data Source" CellStyle-Wrap="False">

                        <PropertiesComboBox DataSourceID="SqlDataSource_VIEWs" TextField="VIEW_TITLE" ValueField="VIEW_ID" ValueType="System.String">

                            <%-- <ValidationSettings Display="Dynamic">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>--%>
                        </PropertiesComboBox>

                    </dx:TreeListComboBoxColumn>




                    <%--                    <dx:TreeListComboBoxColumn FieldName="VIEW_ID" Caption="DATA">
                        <PropertiesComboBox TextField="VIEW_TITLE" ValueField="VIEW_ID" DataSourceID="SqlDataSource_VIEWs">
                           
                        </PropertiesComboBox>
                    </dx:TreeListComboBoxColumn>--%>


                    <%--      <dx:TreeListComboBoxColumn FieldName="DB_ID" Caption="DashBoard">
                        <PropertiesComboBox  DataSourceID="SqlDataSource_DB" ValueField="DB_ID" TextField="DB_TITLE">
                        </PropertiesComboBox>


                    </dx:TreeListComboBoxColumn>--%>





                    <dx:TreeListHyperLinkColumn  FieldName="RID" Caption="Template" HeaderStyle-HorizontalAlign="Center">
                        <PropertiesHyperLink  ImageUrl="../../../../res/icon/report.png"  NavigateUrlFormatString="javascript:ShowMasterBIPopup({0});">
                        </PropertiesHyperLink>
                        <EditFormSettings Visible="False" />
                    </dx:TreeListHyperLinkColumn>

 
                </Columns>

            </dx:ASPxTreeList>


        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_Data" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="select * from tblReport where REPORT_TYPE='BI' and  RID not in(27,33,34) order by No "
    UpdateCommand="update tblReport set TITLE=@TITLE,DB_ID=@DB_ID, VIEW_ID=@VIEW_ID,No=@No,ParentID=@ParentID,DESCRIPTION=@DESCRIPTION Where RID=@RID"
    InsertCommand="Insert into tblReport(TITLE,DB_ID,VIEW_ID,No,ParentID,DESCRIPTION,REPORT_TYPE) values(@TITLE,@DB_ID,@VIEW_ID,@No,@ParentID,@DESCRIPTION,'BI')"
    DeleteCommand="delete from tblReport where RID=@RID">
    <DeleteParameters>
        <asp:Parameter Name="RID" DbType="Int32" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="TITLE" DbType="String" />
        <asp:Parameter Name="DB_ID" DbType="Int32" />
        <asp:Parameter Name="VIEW_ID" DbType="Int32" />
        <asp:Parameter Name="DESCRIPTION" DbType="String" />
        <asp:Parameter Name="ParentID" DbType="Int32" />
        <asp:Parameter Name="No" DbType="Int32" />
        <asp:Parameter Name="RID" DbType="Int32" />
    </UpdateParameters>

    <InsertParameters>
        <asp:Parameter Name="TITLE" DbType="String" />
        <asp:Parameter Name="DB_ID" DbType="Int32" />
        <asp:Parameter Name="VIEW_ID" DbType="Int32" />
        <asp:Parameter Name="DESCRIPTION" DbType="String" />
        <asp:Parameter Name="ParentID" DbType="Int32" />
        <asp:Parameter Name="No" DbType="Int32" />
    </InsertParameters>
</asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_VIEWs" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="select null VIEW_ID,null VIEW_TITLE union all select VIEW_ID,VIEW_TITLE from tblReport_VIEWs order by VIEW_TITLE "></asp:SqlDataSource>

<dx:ASPxPopupControl ID="clientViewBI" runat="server" ClientInstanceName="clientViewBI"
    Modal="True" Maximized="true"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="Business Intelligent"
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
    ShowFooter="false">
    <ClientSideEvents Shown="function(s,e){ window.setTimeout(function() {LoadingPanel.Hide();},2000);}" />

    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl5" runat="server">
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
