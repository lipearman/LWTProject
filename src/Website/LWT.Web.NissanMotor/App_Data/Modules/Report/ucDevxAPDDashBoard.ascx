<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxAPDDashBoard.ascx.vb" Inherits="Modules_ucDevxAPDDashBoard" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

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
        LoadingPanel.Show();
        //clientViewDB.SetContentUrl('applications/DB/Preview.aspx?RID=' + RID);
        //clientViewDB.Show();
        cbPreview.PerformCallback(RID);
    }
</script>
<dx:ASPxCallback ID="cbPreview" runat="server" ClientInstanceName="cbPreview">
    <ClientSideEvents CallbackError="function(s, e) { 
            LoadingPanel.Hide(); 
        }"
        CallbackComplete="function(s, e) { 
            LoadingPanel.Hide(); 
            clientViewDB.SetContentUrl('applications/DB/Master/Preview.aspx');
            clientViewDB.Show();            
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
                    <dx:TreeListComboBoxColumn FieldName="DB_ID" Caption="DashBoard">
                        <PropertiesComboBox DataSourceID="SqlDataSource_DB" ValueField="DB_ID" TextField="DB_TITLE">

                            <%--  <ValidationSettings Display="Dynamic">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>--%>
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


                    <%--  <dx:TreeListComboBoxColumn FieldName="VIEW_ID" Caption="DATA" CellStyle-Wrap="False">

                        <PropertiesComboBox DataSourceID="SqlDataSource_VIEWs" TextField="VIEW_TITLE" ValueField="VIEW_ID" ValueType="System.String">

                            <ValidationSettings Display="Dynamic">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </PropertiesComboBox>

                    </dx:TreeListComboBoxColumn>--%>




                    <%--                    <dx:TreeListComboBoxColumn FieldName="VIEW_ID" Caption="DATA">
                        <PropertiesComboBox TextField="VIEW_TITLE" ValueField="VIEW_ID" DataSourceID="SqlDataSource_VIEWs">
                           
                        </PropertiesComboBox>
                    </dx:TreeListComboBoxColumn>--%>








                    <dx:TreeListTextColumn Caption="#" Name="View"  >
                        <DataCellTemplate>
                            <%# PreviewDB(Eval("RID").ToString(), Eval("DB_ID").ToString())%>
                        </DataCellTemplate>
                        <EditFormSettings Visible="False" />

                    </dx:TreeListTextColumn>
                      



<%--                    <dx:TreeListHyperLinkColumn FieldName="RID" Caption="Template" HeaderStyle-HorizontalAlign="Center">
                        <PropertiesHyperLink ImageUrl="../../../../res/icon/report.png" NavigateUrlFormatString='javascript:<%# PreviewDB(Eval("RID").ToString(), Eval("DB_ID").ToString())%>;'>
                        </PropertiesHyperLink>
                        <EditFormSettings Visible="False" />
                    </dx:TreeListHyperLinkColumn>--%>


                </Columns>

            </dx:ASPxTreeList>


        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_Data" runat="server" ConnectionString="<%$ ConnectionStrings:NissanMotorConnectionString %>"
    SelectCommand="select * from tblReport where REPORT_TYPE='DB' order by No "
    UpdateCommand="update tblReport set TITLE=@TITLE,DB_ID=@DB_ID, No=@No,ParentID=@ParentID,DESCRIPTION=@DESCRIPTION Where RID=@RID"
    InsertCommand="Insert into tblReport(TITLE,DB_ID,No,ParentID,DESCRIPTION,REPORT_TYPE) values(@TITLE,@DB_ID,@No,@ParentID,@DESCRIPTION,'DB')"
    DeleteCommand="delete from tblReport where RID=@RID">
    <DeleteParameters>
        <asp:Parameter Name="RID" DbType="Int32" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="TITLE" DbType="String" />
        <asp:Parameter Name="DB_ID" DbType="Int32" />
        <asp:Parameter Name="DESCRIPTION" DbType="String" />
        <asp:Parameter Name="ParentID" DbType="Int32" />
        <asp:Parameter Name="No" DbType="Int32" />
        <asp:Parameter Name="RID" DbType="Int32" />
    </UpdateParameters>

    <InsertParameters>
        <asp:Parameter Name="TITLE" DbType="String" />
        <asp:Parameter Name="DB_ID" DbType="Int32" />
        <asp:Parameter Name="DESCRIPTION" DbType="String" />
        <asp:Parameter Name="ParentID" DbType="Int32" />
        <asp:Parameter Name="No" DbType="Int32" />
    </InsertParameters>
</asp:SqlDataSource>


<%--<asp:SqlDataSource ID="SqlDataSource_VIEWs" runat="server" ConnectionString="<%$ ConnectionStrings:NissanMotorConnectionString %>"
    SelectCommand="select * from tblReport_VIEWs order by VIEW_TITLE "></asp:SqlDataSource>--%>

<asp:SqlDataSource ID="SqlDataSource_DB" runat="server" ConnectionString="<%$ ConnectionStrings:NissanMotorConnectionString %>"
    SelectCommand="select null as DB_ID, null as DB_TITLE union all select DB_ID,DB_TITLE from tblReport_DashBoard order by DB_TITLE "></asp:SqlDataSource>


<dx:ASPxPopupControl ID="clientViewDB" runat="server" ClientInstanceName="clientViewDB"
    Modal="True"
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
    ShowFooter="true">
    <ClientSideEvents Shown="function(s,e){ window.setTimeout(function() {LoadingPanel.Hide();},2000);}" />

    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl5" runat="server">
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
