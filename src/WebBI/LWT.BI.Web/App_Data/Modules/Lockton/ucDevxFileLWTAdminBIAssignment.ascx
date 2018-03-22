<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxFileLWTAdminBIAssignment.ascx.vb" Inherits="Modules_ucDevxFileLWTAdminBIAssignment" %>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Report" runat="server" EnableAdaptivity="true" Width="100%" HeaderImage-IconID="support_feature_16x16office2013">
    <PanelCollection>
        <dx:PanelContent>





              <table>
                <tr>
                    <td>

 <dx:ASPxGridView ID="gridUser" ClientInstanceName="gridUser" runat="server"
                                    DataSourceID="SqlDataSource_Users"   
                                    KeyFieldName="UserName" SettingsBehavior-AllowEllipsisInText="true"
                                    AutoGenerateColumns="False" SettingsPager-Mode="EndlessPaging"
                                    EnableRowsCache="false" Width="300">

       <SettingsCommandButton> 
                                                    <CancelButton RenderMode="Button"  Image-IconID="actions_cancel_16x16office2013" ></CancelButton>
                                                    <UpdateButton Text="Save" RenderMode="Button"  Image-IconID="actions_save_16x16devav" ></UpdateButton>
                                                </SettingsCommandButton>

                                     <SettingsResizing ColumnResizeMode="NextColumn" />

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
                                                        <dx:ASPxButton ID="btnSave" Image-IconID="actions_save_16x16devav"
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


  </td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>

                    <td style="vertical-align: top">

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
                                                KeyFieldName="BID"
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



                                                        <dx:TreeListMemoColumn FieldName="BID" Caption="BID" Visible="false" CellStyle-Wrap="True">
                                                    </dx:TreeListMemoColumn>

                                                        <dx:TreeListMemoColumn FieldName="SourceName" Caption="SourceName" Visible="false" CellStyle-Wrap="True">
                                                    </dx:TreeListMemoColumn>
                                                </Columns>
                                                <SettingsBehavior AutoExpandAllNodes="true" ExpandCollapseAction="NodeDblClick" ProcessSelectionChangedOnServer="True" />
                                                <SettingsSelection AllowSelectAll="true" Enabled="True" Recursive="True" />
                                                <%--<ClientSideEvents SelectionChanged="treeList_SelectionChanged" CustomDataCallback="treeList_CustomDataCallback" />--%>
                                            </dx:ASPxTreeList>


                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxCallbackPanel>
       </td>
                </tr>
            </table>


 



        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>


<asp:SqlDataSource ID="SqlDataSource_Users" runat="server" ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
    SelectCommand="select * from Portal_Users order by UserName "></asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_Report" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="
select tblDataSourceBI.[BID]
,tblDataSourceBI.[No]
,tblDataSourceBI.[TITLE]
,tblDataSourceBI.[DESCRIPTION]
,tblDataSourceBI.[ParentID]
,tblDataSourceBI.[CREATEDATE]
,tblDataSourceBI.[CREATEBY]
,tblDataSourceBI.[MODIFYDATE]
,tblDataSourceBI.[MODIFYBY] 
,tblDataSourceFile.Title as [SourceName]
from tblDataSourceBI 
left join tblDataSourceFile on tblDataSourceBI.DS_ID = dbo.tblDataSourceFile.ID
left JOIN tblDataSourceBI_Assignment 
ON tblDataSourceBI_Assignment.BID = tblDataSourceBI.BID
and tblDataSourceBI_Assignment.UserName=@assignusername  
    

Order By tblDataSourceBI.[No]

    ">
    <SelectParameters>
        <asp:SessionParameter Name="assignusername" SessionField="assignusername" DefaultValue="" DbType="String" /> 
    </SelectParameters>
</asp:SqlDataSource>
