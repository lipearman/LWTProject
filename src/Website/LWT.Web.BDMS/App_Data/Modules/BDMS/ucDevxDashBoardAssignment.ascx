﻿<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxDashBoardAssignment.ascx.vb" Inherits="Modules_ucDevxDashBoardAssignment" %>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="" runat="server" EnableAdaptivity="true" Width="100%" HeaderImage-IconID="businessobjects_bonote_32x32" >
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxSplitter runat="server" ID="ASPxSplitter1" Width="100%" ResizingMode="Live">
                <Panes>
                    <dx:SplitterPane AutoHeight="True" Size="350" MinSize="110px">
                        <ContentCollection>
                            <dx:SplitterContentControl ID="SplitterContentControl1" runat="server"
                                SupportsDisabledAttribute="True">

                                <dx:ASPxGridView ID="gridUser" ClientInstanceName="gridUser" runat="server"
                                    DataSourceID="SqlDataSource_Users"
                                    KeyFieldName="UserName"
                                    AutoGenerateColumns="False" SettingsPager-Mode="EndlessPaging"
                                    EnableRowsCache="false" Width="300">
                                    <Columns>
                                        
                                        <dx:GridViewDataColumn FieldName="UserName" Width="100" CellStyle-Wrap="False" />
                                        <dx:GridViewDataColumn FieldName="Email"   /> 
                                    </Columns>
                                    <Settings ShowColumnHeaders="false" />
                                    <SettingsSearchPanel Visible="true" />
                                    <SettingsBehavior AllowFocusedRow="True" />
                                    <ClientSideEvents  RowDblClick="function(s, e) { 
                                            s.GetRowValues(s.GetFocusedRowIndex(), 'UserName;Email;Comment', function(values){
                                                LoadingPanel.Show();
                                                DetailImage.SetImageUrl('./images/user.png' ); 
                                                displayuser.SetVisible(true);   
                                                displaytitle.SetText(values[1]);
                                                displaydepartment.SetText(values[2]);

                                                
                                               

                                                callbackPanel_view.PerformCallback(values[0]);
                                            });
                                         }" />
                                </dx:ASPxGridView>
                                <br />

                                <dx:ASPxPanel runat="server" ID="displayuser" ClientInstanceName="displayuser" ClientVisible="false">
                                    <PanelCollection>

                                        <dx:PanelContent>
                                            <table style="width: 100%; height: 140px" class="OptionsTable TopMargin">
                                                <tr>
                                                    <td>
                                                        <dx:ASPxImage runat="server" ID="DetailImage" ClientInstanceName="DetailImage" Width="100px" />
                                                        <br />
                                                        Email :<dx:ASPxLabel runat="server" ID="displaytitle" ClientInstanceName="displaytitle"></dx:ASPxLabel>
                                                        <br />
                                                        Note :<dx:ASPxLabel runat="server" ID="displaydepartment" ClientInstanceName="displaydepartment"></dx:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="btnSave"
                                                            ClientInstanceName="btnSave"
                                                            runat="server" Text="Save"
                                                            AutoPostBack="false">
                                                            <ClientSideEvents Click="function(s,e) {
                                                     
                                                    LoadingPanel.Show();
                                                    cbSave.PerformCallback('');                                                                                  

                                            }" />
                                                        </dx:ASPxButton>

                                                        <dx:ASPxCallback ID="cbSave" runat="server" ClientInstanceName="cbSave">
                                                            <ClientSideEvents
                                                                CallbackComplete="function(s, e) { 
                                                                LoadingPanel.Hide();  
                                                                
                                                                }" />
                                                        </dx:ASPxCallback>

                                                    </td>
                                                </tr>
                                            </table>

                                        </dx:PanelContent>
                                    </PanelCollection>


                                </dx:ASPxPanel>



                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                    <dx:SplitterPane AutoHeight="True" Size="70%" MinSize="280px">
                        <ContentCollection>
                            <dx:SplitterContentControl ID="SplitterContentControl2" runat="server"
                                SupportsDisabledAttribute="True">
                                <dx:ASPxCallbackPanel runat="server" ID="callbackPanel_view"
                                    ClientInstanceName="callbackPanel_view" ClientVisible="false"
                                    Height="100%" Width="100%">
                                    <ClientSideEvents
                                        CallbackError="function(s,e){LoadingPanel.Hide();}"
                                        EndCallback="function(s,e){
                                        LoadingPanel.Hide();
                                        callbackPanel_view.SetVisible(true);
                                        treeList.PerformCallback('');
                                        }" />
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent1">




                                            <dx:ASPxTreeList ID="treeList" runat="server" ClientInstanceName="treeList"
                                                AutoGenerateColumns="False" DataSourceID="SqlDataSource_Report"
                                                Width="100%"
                                                KeyFieldName="DB_ID"
                                                ParentFieldName="ParentID">
                                                <Columns>                                                

                                                    <dx:TreeListTextColumn FieldName="TITLE" Caption="Report" Width="200" CellStyle-Wrap="False">
                                                        <PropertiesTextEdit>
                                                            <ValidationSettings Display="Dynamic">
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                        </PropertiesTextEdit>

                                                    </dx:TreeListTextColumn>

                                                    <dx:TreeListMemoColumn FieldName="DESCRIPTION" Width="500" CellStyle-Wrap="True">
                                                    </dx:TreeListMemoColumn>

 
                                                       
                                                </Columns>
                                                <SettingsBehavior AutoExpandAllNodes="true" ExpandCollapseAction="NodeDblClick" ProcessSelectionChangedOnServer="True" />
                                                <SettingsSelection AllowSelectAll="true" Enabled="True" Recursive="True" />
                                                <%--<ClientSideEvents SelectionChanged="treeList_SelectionChanged" CustomDataCallback="treeList_CustomDataCallback" />--%>
                                            </dx:ASPxTreeList>


                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxCallbackPanel>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                </Panes>
            </dx:ASPxSplitter>






        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>


<asp:SqlDataSource ID="SqlDataSource_Users" runat="server" ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
    SelectCommand="select * from Portal_Users order by UserName "></asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_Report" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="

     select [tblDashBoard].[DB_ID]
    ,[tblDashBoard].[No]
    ,[tblDashBoard].[TITLE]
    ,[tblDashBoard].[DESCRIPTION]
    ,[tblDashBoard].[ParentID]
    ,[tblDashBoard].[CREATEDATE]
    ,[tblDashBoard].[CREATEBY]
    ,[tblDashBoard].[MODIFYDATE]
    ,[tblDashBoard].[MODIFYBY] 
    ,[tblDashBoard].[GUID]
    ,tblDashBoard_Assignment.UserName
    from [tblDashBoard] 
    left JOIN dbo.tblDashBoard_Assignment ON [tblDashBoard].[DB_ID] = tblDashBoard_Assignment.[DB_ID]
    and tblDashBoard_Assignment.UserName=@UserName  
    where [tblDashBoard].PortalId=@PortalId 
    Order By [No]

    ">
    <SelectParameters>
        <asp:Parameter Name="PortalId" />
        <asp:SessionParameter Name="UserName" SessionField="assignusername" DefaultValue="" DbType="String" />
    </SelectParameters>
</asp:SqlDataSource>
