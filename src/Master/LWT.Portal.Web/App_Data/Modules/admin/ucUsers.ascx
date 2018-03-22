<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucUsers.ascx.vb" Inherits="LWT.Portal.Web.ucUsers" %>

<script>


    function SetUser(result) {

        //cbOpenPopup.PerformCallback(values);

        //AECodeList.UnselectAll();

        //var AElist = result.split(';');
        //var index;
        //var indices = [];

        //for (i = 0; i < AECodeList.items.length; i++) {
        //    for (index = 0; index < AElist.length; index++) {
        //        if (AECodeList.items[i].value == AElist[index]) {
        //            indices.push(i);
        //        }
        //    }
        //}

        //AECodeList.SelectIndices(indices);
    }

    function OnFocusedCardChanged() {
        LoadingPanel.Show();
        cardView.GetCardValues(cardView.GetFocusedCardIndex(), 'sAMAccountName', OnGetCardValues);
    }
    function OnGetCardValues(values) {

        //lbUserName.SetText(values)
        //lbUserImage.SetImageUrl('http://lockthbnkedc01/sites/HRDweb/LWT%20Member%20Photos/' + values + '.jpg');
        cbOpenPopup.PerformCallback(values);
    }

</script>
<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="User" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxCallback runat="server" ID="cbOpenPopup" ClientInstanceName="cbOpenPopup">
                <ClientSideEvents CallbackComplete="function(s,e){                                                                                     
                            //LoadingPanel.Hide(); 
                            //SetUser(e.result);  
                            //formLayout.Refresh();
                            //alert(e.result);
                            callbackPanel_formLayout.PerformCallback();
                        }" />
            </dx:ASPxCallback>

            <dx:ASPxCardView ID="cardView" ClientInstanceName="cardView" runat="server" DataSourceID="SqlDataSource_User"
                Width="1000" KeyFieldName="sAMAccountName">
                <ClientSideEvents FocusedCardChanged="OnFocusedCardChanged" />
                <SettingsBehavior AllowFocusedCard="true" />
                <SettingsPager>
                    <SettingsTableLayout ColumnCount="4" RowsPerPage="1" />
                </SettingsPager>
                <SettingsSearchPanel Visible="true" />

                <Columns>
                    <dx:CardViewColumn FieldName="sAMAccountName" />
                    <dx:CardViewColumn FieldName="displayName" />
                    <dx:CardViewColumn FieldName="department" />
                </Columns>

                <CardLayoutProperties ColCount="1">
                    <Items>
                        <dx:CardViewColumnLayoutItem Caption="" ColSpan="1" HorizontalAlign="Center">
                            <Template>
                                <img src='http://lockthbnkedc01/sites/HRDweb/LWT%20Member%20Photos/<%# Eval("sAMAccountName")%>.jpg' />
                            </Template>
                        </dx:CardViewColumnLayoutItem>
                        <dx:CardViewColumnLayoutItem ShowCaption="False" HorizontalAlign="Center">
                            <Template>
                                <center><%# Eval("displayName")%> <br /> (<%# Eval("department")%>)</center>
                            </Template>
                        </dx:CardViewColumnLayoutItem>
                    </Items>
                </CardLayoutProperties>
            </dx:ASPxCardView>
            <br />


            <dx:ASPxCallbackPanel runat="server" ID="callbackPanel_formLayout"  SettingsLoadingPanel-Enabled="false"
                ClientInstanceName="callbackPanel_formLayout" Width="100%">
                <ClientSideEvents EndCallback="function(s,e){LoadingPanel.Hide(); }" CallbackError="function(s,e){LoadingPanel.Hide(); }" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1">

                        <dx:ASPxSplitter ID="ASPxSplitter1" ClientInstanceName="ASPxSplitter1" runat="server" Height="400px" Width="1000" ResizingMode="Live">
                            <Panes>
                                <dx:SplitterPane AutoHeight="True" Size="54%" MinSize="110px">
                                    <ContentCollection>
                                        <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">

                                            <%--<dx:ASPxCallback ID="cbformLayout" runat="server" ClientInstanceName="cbformLayout"></dx:ASPxCallback>--%>




                                            <dx:ASPxFormLayout ID="formLayout" ClientInstanceName="formLayout" runat="server" DataSourceID="SqlDataSource_UserData" AlignItemCaptionsInAllGroups="True">
                                                <Items>
                                                    <dx:LayoutGroup Caption="Personal Information" GroupBoxDecoration="HeadingLine">
                                                        <Items>
                                                            <dx:LayoutItem Caption="User Name" FieldName="UserName">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel1"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Email" FieldName="Email">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel11"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>

                                                            <dx:LayoutItem Caption="PasswordQuestion" FieldName="PasswordQuestion">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer17" runat="server" SupportsDisabledAttribute="True">
                                                                         
                                                                        <dx:ASPxTextBox runat="server" ID="txtPasswordQuestion" ></dx:ASPxTextBox>

                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="PasswordAnswer" FieldName="PasswordAnswer">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer18" runat="server" SupportsDisabledAttribute="True">
                                                                         <dx:ASPxTextBox runat="server" ID="txtPasswordAnswer" ></dx:ASPxTextBox>

                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>

                                                            <dx:LayoutItem Caption="Comment" FieldName="Comment">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server" SupportsDisabledAttribute="True">

                                                                        <dx:ASPxMemo runat="server" ID="txtComment" Height="100"></dx:ASPxMemo>

                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>



                                                            <dx:LayoutItem Caption="IsApproved" FieldName="IsApproved">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxCheckBox runat="server" ID="cbIsApproved"></dx:ASPxCheckBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="IsLocked" FieldName="IsLocked">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxCheckBox runat="server" ID="cbIsLocked"></dx:ASPxCheckBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="ExpiredDate" FieldName="ExpiredDate">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxDateEdit ID="deExpiredDate" runat="server" Width="200px" />
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>



                                                            <dx:LayoutItem ShowCaption="False" HorizontalAlign="Right" Width="100">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server" SupportsDisabledAttribute="True">


                                                                        <dx:ASPxButton ID="btnSaveUserRoles" Image-IconID="actions_save_16x16devav"
                                                                            ClientInstanceName="btnSaveUserRoles" Width="100"
                                                                            runat="server" Text="Save"
                                                                            AutoPostBack="false">
                                                                            <ClientSideEvents Click="function(s,e) {
                                                     
                                                                                    LoadingPanel.Show();
                                                                                    cbSaveUserRoles.PerformCallback('');                                                                                  

                                                                            }" />
                                                                        </dx:ASPxButton>

                                                                        <dx:ASPxCallback ID="cbSaveUserRoles" runat="server" ClientInstanceName="cbSaveUserRoles">
                                                                            <ClientSideEvents 
                                                                                CallbackError="function(s, e) {LoadingPanel.Hide(); }"
                                                                                CallbackComplete="function(s, e) {LoadingPanel.Hide(); }" />
                                                                        </dx:ASPxCallback>

                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:LayoutGroup>




                                                    <dx:LayoutGroup Caption="Activity Statics" GroupBoxDecoration="HeadingLine">
                                                        <Items>
                                                            <dx:LayoutItem Caption="CreationDate" FieldName="CreationDate">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel2"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>

                                                            <dx:LayoutItem Caption="LastLoginDate" FieldName="LastLoginDate">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel3"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>

                                                            <dx:LayoutItem Caption="LastActivityDate" FieldName="LastActivityDate">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel4"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>

                                                            <dx:LayoutItem Caption="LastPasswordChangedDate" FieldName="LastPasswordChangedDate">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel5"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:LayoutGroup>




                                                    <dx:LayoutGroup Caption="Login Statistics" GroupBoxDecoration="HeadingLine">
                                                        <Items>
                                                            <dx:LayoutItem Caption="LastLockOutDate" FieldName="LastLockOutDate">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel6"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>

                                                            <dx:LayoutItem Caption="FailedPasswordAttemptCount" FieldName="FailedPasswordAttemptCount">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel7"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>

                                                            <dx:LayoutItem Caption="FailedPasswordAttemptWindowStart" FieldName="FailedPasswordAttemptWindowStart">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel8"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>

                                                            <dx:LayoutItem Caption="FailedPasswordAnswerAttemptCount" FieldName="FailedPasswordAnswerAttemptCount">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel9"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>

                                                            <dx:LayoutItem Caption="FailedPasswordAnswerAttemptWindowStart" FieldName="FailedPasswordAnswerAttemptWindowStart">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel10"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:LayoutGroup>

                                                </Items>
                                            </dx:ASPxFormLayout>





                                        </dx:SplitterContentControl>
                                    </ContentCollection>
                                </dx:SplitterPane>
                                <dx:SplitterPane>
                                    <ContentCollection>
                                        <dx:SplitterContentControl ID="SplitterContentControl2" runat="server">

                                            <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" AlignItemCaptionsInAllGroups="True">
                                                <Items>
                                                    <dx:LayoutGroup Caption="Roles" GroupBoxDecoration="HeadingLine">
                                                        <Items>
                                                            <dx:LayoutItem ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer19" runat="server" SupportsDisabledAttribute="True">

                                                                        <dx:ASPxTreeList ID="treeList" runat="server" ClientInstanceName="treeList"
                                                                            AutoGenerateColumns="False" DataSourceID="SqlDataSource_ProjectRole" Height="400"
                                                                            Width="350" SettingsBehavior-AutoExpandAllNodes="true"
                                                                            KeyFieldName="ID"   SettingsLoadingPanel-Enabled="false"
                                                                            ParentFieldName="ParentID">
                                                                            <Columns>
                                                                                <dx:TreeListMemoColumn FieldName="Name" CellStyle-Wrap="True">
                                                                                </dx:TreeListMemoColumn>
                                                                            </Columns>
                                                                            <Settings ScrollableHeight="600" VerticalScrollBarMode="Visible" />

                                                                            <SettingsBehavior AutoExpandAllNodes="true" ExpandCollapseAction="NodeDblClick" ProcessSelectionChangedOnServer="True" />
                                                                            <SettingsSelection AllowSelectAll="true" Enabled="True" />

                                                                        </dx:ASPxTreeList>



                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>

                                                        </Items>
                                                    </dx:LayoutGroup>
                                                </Items>
                                            </dx:ASPxFormLayout>

                                        </dx:SplitterContentControl>
                                    </ContentCollection>
                                </dx:SplitterPane>
                            </Panes>
                        </dx:ASPxSplitter>

                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_User" runat="server" ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
    SelectCommand="select department,sAMAccountName,displayName,title from V_LWT_USER order by sAMAccountName"></asp:SqlDataSource>




<asp:SqlDataSource ID="SqlDataSource_ProjectRole" runat="server" ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
    SelectCommand="
    select *
    from
    (
        SELECT PortalId*-1 ID
        ,'Project :' + PortalName Name
        , null ParentID
        FROM PortalCfg_Globals  
        union all  
        SELECT 
        RoleID ID
        ,RoleName Name
        ,PortalID*-1 ParentID
        FROM Portal_Roles
    ) a
    order by a.ParentID,a.Name

    "></asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_UserData" runat="server" ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
    SelectCommand="
   select * from Portal_Users where UserID=@UserID
  ">
    <SelectParameters>
        <asp:SessionParameter Name="UserID" SessionField="UserID" />
    </SelectParameters>

</asp:SqlDataSource>
