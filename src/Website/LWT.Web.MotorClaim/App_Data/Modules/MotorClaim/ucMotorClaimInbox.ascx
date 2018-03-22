<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucMotorClaimInbox.ascx.vb" Inherits="Modules_ucMotorClaimInbox" %>

<link type="text/css" rel="Stylesheet" href="Content/styles.css" />
<link type="text/css" rel="Stylesheet" href="Content/sprite.css" />

<script type="text/javascript" src="Scripts/jquery-1.4.4.js"></script>
 <script type="text/javascript">
     var keyValue;
     function OnMoreInfoClick(element, key) {
         callbackPanel_resultdetails.SetContentHtml("");
         popupResultDetails.ShowAtElement(element);
         keyValue = key;
     }
     function popup_Shown(s, e) {
         callbackPanel_resultdetails.PerformCallback(keyValue);
     }
    </script>

<script>
    var DemoState = 'MailList';



    function Adjust() {
        var bodySelector = LeftPanel.IsExpandable() ? "Portrait" : "Landscape";
        $('body').removeClass("Portrait").removeClass("Landscape").addClass(bodySelector);
        ChangeExpandButtonsVisibility(LeftPanel.IsExpandable(), LeftPanel.IsExpanded());
    }
    function ChangeExpandButtonsVisibility(expandable, expand) {
        ClientExpandPaneImage.SetVisible(expandable && !expand);
        ClientCollapsePaneImage.SetVisible(expandable && expand);
    }
    function ClientLayout_PaneResized() {
        var state = DemoState;
        if (!state) return;

        if (state == "MailList") {
            ClientMailGrid.SetHeight(0);
            ClientMailGrid.SetHeight(ASPxClientUtils.GetDocumentClientHeight() - TopPanel.GetHeight());
        }
        if (state == "MailForm" && window.ClientMailEditor) {
            ClientMailEditor.SetHeight(0);
            ClientMailEditor.SetHeight(ASPxClientUtils.GetDocumentClientHeight() - TopPanel.GetHeight() - $("#MailForm").get(0).offsetHeight);
        }
        if (state == "MailPreview") {
            ClientMailPreviewPanel.SetHeight(0);
            ClientMailPreviewPanel.SetHeight(ASPxClientUtils.GetDocumentClientHeight() - TopPanel.GetHeight());
        }

    }

    function ChangeLeftPaneExpandedState(expand) {
        if (expand)
            LeftPanel.Expand();
        else
            LeftPanel.Collapse();
    }
    function ChangeExpandButtonsVisibility(expandable, expand) {
        ClientExpandPaneImage.SetVisible(expandable && !expand);
        ClientCollapsePaneImage.SetVisible(expandable && expand);
    }

    function ClientCollapsePaneImage_Click(s, e) {
        ChangeLeftPaneExpandedState(false);
        ChangeExpandButtonsVisibility(true, false);
    }

    function ClientExpandPaneImage_Click(s, e) {
        ChangeLeftPaneExpandedState(true);
        ChangeExpandButtonsVisibility(true, true);
    }

    function ShowLoadingPanel(element) {
        this.loadingPanelTimer = window.setTimeout(function () {
            ClientLoadingPanel.ShowInElement(element);
        }, 500);
    }

    function HideLoadingPanel() {
        if (this.loadingPanelTimer > -1) {
            window.clearTimeout(this.loadingPanelTimer);
            this.loadingPanelTimer = -1;
        }
        ClientLoadingPanel.Hide();
    }



    //function ClientInfoMenu_ItemClick(s, e) {
    //    if(e.item.parent && e.item.parent.name == "theme") {
    //        ASPxClientUtils.SetCookie("MailDemoCurrentTheme", e.item.name || "");
    //        e.processOnServer = true;
    //    }
    //}





    function ClientActionMenu_ItemClick(s, e) {
        var command = e.item.name;
        //var state = DemoState;           
        switch (command) {
            case "new":
                DemoState = 'MailForm';
                ChangeDemoState("MailForm", "New");
                break;
            case "reply":
                //ChangeDemoState("MailForm", "Reply", state.Key);
                break;
            case "back":
                ClientMailGrid.SetVisible(true)
                DemoState = 'MailList';
                //ShowMenuItems('new');
                ChangeDemoState('MailList', 'new', '');
                break;
            case "delete":
                if (!window.confirm("Confirm Delete?"))
                    return;
                //var keys = [];
                //if (state.View == "MailList") {
                //    keys = ClientMailGrid.GetSelectedKeysOnPage();
                //} else if (state.View == "MailPreview") {
                //    keys = [state.Key];
                //    MailDemo.ChangeDemoState("MailList");
                //}
                //if (keys.length > 0) {
                //    MailDemo.DoCallback(ClientMailGrid, function () {
                //        ClientMailGrid.PerformCallback("Delete|" + keys.join("|"));
                //    });
                //    MailDemo.MarkMessagesAsRead(true, keys);
                //}
                break;
            case "send":
            case "save":
                //if (window.ClientToEditor && !ASPxClientEdit.ValidateEditorsInContainerById("MailForm"))
                //    return;
                //var args = command == "send" ? "SendMail" : "SaveMail";
                //if (state.Command === "EditDraft")
                //    args += "|" + state.Key;
                //MailDemo.ChangeDemoState("MailList");
                //MailDemo.DoCallback(ClientMailGrid, function () {
                //    ClientMailGrid.PerformCallback(args);
                //});


                break;
            case "read":
            case "unread":
                //var selectedKeys = ClientMailGrid.GetSelectedKeysOnPage();
                //if (selectedKeys.length == 0)
                //    return;
                //ClientMailGrid.UnselectAllRowsOnPage();
                //MailDemo.MarkMessagesAsRead(command == "read", selectedKeys);
                break;
        }


    }

    function ChangeDemoState(state, command, key) {
        //var state = DemoState;
        ClientMailGrid.SetVisible(false)
        ClientMailFormPanel.SetVisible(false);
        ClientMailPreviewPanel.SetVisible(false);
        ClientConsentFormPanel.SetVisible(false);


        ClientActionMenu.SetVisible(true);

        ClientActionMenu.GetItemByName("new").SetVisible(false);
        ClientActionMenu.GetItemByName("send").SetVisible(false);
        ClientActionMenu.GetItemByName("save").SetVisible(false);
        ClientActionMenu.GetItemByName("reply").SetVisible(false);
        ClientActionMenu.GetItemByName("back").SetVisible(false);
        ClientActionMenu.GetItemByName("delete").SetVisible(false);

        switch (state) {
            case "MailList":
                ClientLoadingPanel.Hide();
                ClientMailGrid.SetVisible(true)
                //ClientMailFormPanel.SetVisible(false);
                //ClientMailPreviewPanel.SetVisible(false);
                ClientActionMenu.GetItemByName("new").SetVisible(true);
                break;
            case "MailPreview":
                ClientLoadingPanel.Hide();
                ClientMailPreviewPanel.SetVisible(true);
                ClientActionMenu.GetItemByName("back").SetVisible(true);


                break;
            case "MailForm":
                ClientLoadingPanel.Hide();
                ClientMailFormPanel.SetVisible(true);
                ClientActionMenu.GetItemByName("back").SetVisible(true);



                break;
            case "MailConsentForm":
                ClientLoadingPanel.Hide();
                ClientConsentFormPanel.SetVisible(true);


                break;


            default:

                break;
        }
    }


    function ShowMailGrid() {
        MailDemo.HideLoadingPanel();
        MailDemo.HideMailPreview();
        MailDemo.HideMailForm();

        ClientMailGrid.SetVisible(true);
        MailDemo.ClientLayout_PaneResized();
    }

    function ShowMenuItems(command) {

        ClientActionMenu.SetVisible(true);
        switch (command) {
            case "new":

                ClientActionMenu.GetItemByName("new").SetVisible(true);
                ClientActionMenu.GetItemByName("send").SetVisible(false);
                ClientActionMenu.GetItemByName("save").SetVisible(false);
                ClientActionMenu.GetItemByName("reply").SetVisible(false);
                ClientActionMenu.GetItemByName("back").SetVisible(false);
                ClientActionMenu.GetItemByName("delete").SetVisible(false);
                break;

            case "preview":
                ClientActionMenu.GetItemByName("new").SetVisible(false);
                ClientActionMenu.GetItemByName("send").SetVisible(false);
                ClientActionMenu.GetItemByName("save").SetVisible(false);
                ClientActionMenu.GetItemByName("reply").SetVisible(false);
                ClientActionMenu.GetItemByName("back").SetVisible(true);
                ClientActionMenu.GetItemByName("delete").SetVisible(false);
                break;

            default:
                ClientActionMenu.GetItemByName("new").SetVisible(false);
                ClientActionMenu.GetItemByName("send").SetVisible(false);
                ClientActionMenu.GetItemByName("save").SetVisible(false);
                ClientActionMenu.GetItemByName("reply").SetVisible(false);
                ClientActionMenu.GetItemByName("back").SetVisible(true);
                ClientActionMenu.GetItemByName("delete").SetVisible(false);
                break;
        }


    }

</script>


<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Inbox" runat="server"  Width="100%">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxSplitter runat="server" ID="ASPxSplitter2" >
                <Panes>
                    <dx:SplitterPane AutoHeight="True" Size="200" MinSize="110px">
                        <ContentCollection>
                            <dx:SplitterContentControl ID="SplitterContentControl4" runat="server"
                                SupportsDisabledAttribute="True">


                                <dx:ASPxTreeView runat="server" ID="MailTree" ClientInstanceName="ClientMailTree" AllowSelectNode="True" CssClass="MailTree">
                                    <Nodes>
                                        <dx:TreeViewNode Text="Claim Inbox" Name="ReserveInbox" Expanded="True" Image-SpriteProperties-CssClass="Sprite_SentItems">
                                            <Nodes>
                                                <dx:TreeViewNode Text="00 - Open" Name="ClaimOpen" Image-SpriteProperties-CssClass="Sprite_About" />
                                                <dx:TreeViewNode Text="01 - Reserve" Name="ClaimReserve" Image-SpriteProperties-CssClass="Sprite_About" />
                                                <dx:TreeViewNode Text="02 - Payment" Name="ClaimPayment" Image-SpriteProperties-CssClass="Sprite_About" />
                                                <dx:TreeViewNode Text="99 - Close" Name="ClaimClose" Image-SpriteProperties-CssClass="Sprite_About" />
                                                <dx:TreeViewNode Text="98 - ReOpen" Name="ClaimReOpen" Image-SpriteProperties-CssClass="Sprite_About" />
                                                <dx:TreeViewNode Text="Consent Form" Name="ClaimConsentForm" Image-SpriteProperties-CssClass="Sprite_Attachment" />
                                            </Nodes>
                                        </dx:TreeViewNode>
                                    </Nodes>
                                    <ClientSideEvents NodeClick="function(s,e){

                                   var command = s.GetSelectedNode().name;

                                   if(command == 'ClaimConsentForm')
                                    {
                                       DemoState = 'MailConsentForm';                          
                                       //ClientLoadingPanel.Show();                    
                                       ChangeDemoState('MailConsentForm', 'new', '');                                  
                                    }
                                else
                                    {
                                       DemoState = 'MailList';                          
                                       //ClientLoadingPanel.Show();                    
                                       ChangeDemoState('MailList', 'new', '');       
                                       ClientMailGrid.PerformCallback('FolderChanged'); 
                                    }                                                                                         
                                }" />
                                </dx:ASPxTreeView>

                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                    <dx:SplitterPane Size="100%" ScrollBars="Auto" >
                        <ContentCollection>
                            <dx:SplitterContentControl ID="SplitterContentControl5" runat="server"
                                SupportsDisabledAttribute="True">

                                <dx:ASPxPanel runat="server" ID="TopPanel" ClientInstanceName="TopPanel">
                                    <PanelCollection>
                                        <dx:PanelContent>




                                            <table>
                                                <tr>
                                                    <td class="Strut">
                                                        <div style="float: left">
                                                            <dx:ASPxImage ID="ExpandPaneImage" runat="server" Cursor="pointer" SpriteCssClass="Sprite_ExpandPane" ToolTip="Expand" AlternateText="Expand"
                                                                ClientInstanceName="ClientExpandPaneImage" ClientVisible="false">
                                                                <ClientSideEvents Click="ClientExpandPaneImage_Click" />
                                                            </dx:ASPxImage>
                                                        </div>
                                                        <div style="float: left">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:XmlDataSource ID="ActionMenuDataSource" runat="server" DataFile="~/App_Data/TopMenuActions.xml" />

                                                                        <dx:ASPxMenu ID="ActionMenu" runat="server" DataSourceID="ActionMenuDataSource" ClientVisible="false"
                                                                            ShowAsToolbar="true" ClientInstanceName="ClientActionMenu" CssClass="ActionMenu" SeparatorWidth="0"
                                                                            OnItemDataBound="ActionMenu_ItemDataBound">
                                                                            <ClientSideEvents ItemClick="ClientActionMenu_ItemClick" />
                                                                            <%-- <Border BorderWidth="0" />--%>
                                                                            <SubMenuStyle CssClass="SubMenu" />
                                                                        </dx:ASPxMenu>

                                                                    </td>
                                                                    <td>&nbsp; 
                                                                    </td>

                                                                    <td>
                                                                        <dx:ASPxButton ID="bnExportXLS" ClientInstanceName="bnExportXLS" runat="server" Image-IconID="export_export_32x32office2013" ToolTip="Export to Excel" Text="" Height="44"></dx:ASPxButton>


                                                                    </td>
                                                                </tr>
                                                            </table>


                                                        </div>
                                                        <div style="float: right">
                                                        </div>
                                                        <b class="clear"></b>
                                                    </td>
                                                    <td id="SearchBoxSpacer" class="Spacer" runat="server"><b></b></td>
                                                    <td></td>
                                                </tr>
                                            </table>


                                            <br />



                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxPanel>

                                <dx:ASPxGridView runat="server" ID="MailGrid" 
                                    SettingsLoadingPanel-Mode="ShowAsPopup" 
                                    Settings-VerticalScrollBarMode="Auto"
                                    Settings-HorizontalScrollBarMode="Auto"
                                    ClientInstanceName="ClientMailGrid" 
                                    Width="100%" 
                                    KeyFieldName="TRID"  
                                    Border-BorderWidth="0">

                                    <Columns>
  <dx:GridViewDataTextColumn FieldName="TRID" Visible="false" SortOrder="Descending"  CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />

                                        <dx:GridViewDataTextColumn FieldName="TRNo"  CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />

                                        <%--   <dx:GridViewDataTextColumn FieldName="ClaimStatus"  CellStyle-Wrap="False"  Settings-AllowFilterBySearchPanel="True" />--%>
                                        <%--<dx:TreeViewNode Text="00 - Open" Name="ClaimOpen" Image-SpriteProperties-CssClass="Sprite_About" />
<dx:TreeViewNode Text="01 - Reserve" Name="ClaimReserve" Image-SpriteProperties-CssClass="Sprite_About" />
<dx:TreeViewNode Text="02 - Payment" Name="ClaimPayment" Image-SpriteProperties-CssClass="Sprite_About" />
<dx:TreeViewNode Text="99 - Close" Name="ClaimClose" Image-SpriteProperties-CssClass="Sprite_About" />
<dx:TreeViewNode Text="98 - ReOpen" Name="ClaimReOpen" Image-SpriteProperties-CssClass="Sprite_About" />
<dx:TreeViewNode Text="Consent Form" Name="ClaimConsentForm" Image-SpriteProperties-CssClass="Sprite_Attachment" />
                                        --%>


                                        <dx:GridViewDataComboBoxColumn FieldName="ClaimStatus" CellStyle-Wrap="False">
                                            <PropertiesComboBox>
                                                <Items>
                                                    <dx:ListEditItem Value="00" Text="00 - Open" />
                                                    <dx:ListEditItem Value="01" Text="01 - Reserve" />
                                                    <dx:ListEditItem Value="02" Text="02 - Payment" />
                                                    <dx:ListEditItem Value="99" Text="99 - Close" />
                                                    <dx:ListEditItem Value="98" Text="98 - ReOpen" />
                                                </Items>
                                            </PropertiesComboBox>

                                        </dx:GridViewDataComboBoxColumn>


                                        <dx:GridViewDataTextColumn FieldName="TempPolicy" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                                        <dx:GridViewDataTextColumn FieldName="RefNo" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                                        <dx:GridViewDataTextColumn FieldName="PolicyNo" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                                        <dx:GridViewDataTextColumn FieldName="ClaimNo" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />

                                        <dx:GridViewDataDateColumn FieldName="TransactionDate"  CellStyle-Wrap="False">
                                            <PropertiesDateEdit DisplayFormatString="g" />
                                        </dx:GridViewDataDateColumn>

                                        <dx:GridViewDataTextColumn FieldName="Status" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                                        <%--<dx:GridViewDataTextColumn FieldName="ResultMessage" CellStyle-Wrap="False" Settings-AllowEllipsisInText="True" />--%>



                                        <dx:GridViewDataTextColumn FieldName="InsuredName" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                                        <dx:GridViewDataTextColumn FieldName="CarLicense" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                                        <dx:GridViewDataTextColumn FieldName="ChassisNo" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                                        <dx:GridViewDataTextColumn FieldName="ShowRoomName" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                                        <dx:GridViewDataTextColumn FieldName="DriverName" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                                        <dx:GridViewDataTextColumn FieldName="AccidentDate" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                                        <dx:GridViewDataTextColumn FieldName="AccidentTime" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />

                                      
                                        <dx:GridViewDataTextColumn FieldName="AccidentPlace" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                                    </Columns>

                                    <SettingsPager Mode="ShowAllRecords" />

                                    <%--       <Settings ShowGroupPanel="false"
                            VerticalScrollBarMode="Auto"
                            VerticalScrollableHeight="0"
                            ShowGroupedColumns="false"
                            GridLines="Both" />--%>

                                    <SettingsSearchPanel Visible="true" />

                                    <SettingsBehavior AllowDragDrop="true"
                                        AutoExpandAllGroups="true"
                                        EnableRowHotTrack="True"
                                        ColumnResizeMode="Control" />


                                    <SettingsPager Mode="ShowPager" PageSize="15">
                                        <PageSizeItemSettings Visible="false" Items="15, 30, 45" ShowAllItem="false" />
                                    </SettingsPager>

                                    <Styles>
                                        <Row Cursor="pointer" />
                                    </Styles>

                                    <%--<ClientSideEvents EndCallback="function(s,e){ClientLoadingPanel.Hide();}" CallbackError="function(s,e){ClientLoadingPanel.Hide();}" />--%>

                                    <ClientSideEvents RowClick="function(s, e) {
                        var src = ASPxClientUtils.GetEventSource(e.htmlEvent);
                        if(src.tagName == 'TD' && src.className.indexOf('dxgvCommandColumn') != -1) // selection cell
                            return;
                        if(!s.IsDataRow(e.visibleIndex))
                            return;
                        var key = s.GetRowKey(e.visibleIndex);
                        DemoState = 'MailPreview';                          
                        ClientLoadingPanel.Show();                     
                        ChangeDemoState('MailPreview', 'preview', key);
                        ClientMailPreviewPanel.PerformCallback(key);
                    }     
                     " />
                                </dx:ASPxGridView>

                                <dx:ASPxGridViewExporter runat="server" ID="GridExporter" GridViewID="MailGrid">
                                    <Styles>
                                        <Default Font-Names="Tahoma" Font-Size="Medium" Wrap="False"></Default>
                                    </Styles>
                                </dx:ASPxGridViewExporter>

                                <dx:ASPxCallbackPanel ID="MailPreviewPanel" runat="server" RenderMode="Div" Height="400" CssClass="MailPreviewPanel" ClientInstanceName="ClientMailPreviewPanel" ClientVisible="false">
                                    <PanelCollection>
                                        <dx:PanelContent>
                                            <dx:ASPxFormLayout ID="formPreview" runat="server" Width="800" AlignItemCaptionsInAllGroups="True">
                                                <Items>
                                                    <dx:LayoutGroup Caption="Result" ColCount="2">
                                                        <Items>
                                                            <dx:LayoutItem Caption="TRNo" FieldName="TRNo">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer65" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel65"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="ConsentFormNo" FieldName="ConsentFormNo">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer50" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel50"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>

                                                            <dx:LayoutItem Caption="SubmitDate" FieldName="SubmitDate">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer66" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel66"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>


                                                            <dx:LayoutItem Caption="Status" FieldName="Status">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer67" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel67"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>

                                                            <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                                                            <dx:LayoutItem ShowCaption="False" ColSpan="2">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer64" runat="server" SupportsDisabledAttribute="True">



                                                                        <dx:ASPxGridView runat="server" ID="ASPxGridPreview_Result" SettingsBehavior-AllowSort="false" Width="100%" KeyFieldName="ID"
                                                                            Border-BorderWidth="0">
                                                                            <Columns>
                                                                                <dx:GridViewDataTextColumn FieldName="ResultNo" Caption="Reference No" Width="15%" />
                                                                                <dx:GridViewDataTextColumn FieldName="ResultCode" Caption="Error Field" Width="15%" />
                                                                                <dx:GridViewDataTextColumn FieldName="ResultMessage" Caption="ข้อความ" Width="80%" />
                                                                            </Columns>
                                                                            <SettingsPager Mode="ShowAllRecords" />

                                                                            <SettingsBehavior AllowDragDrop="false"
                                                                                AutoExpandAllGroups="true"
                                                                                EnableRowHotTrack="True"
                                                                                ColumnResizeMode="NextColumn" />




                                                                            <Styles>
                                                                                <Row Cursor="pointer" />
                                                                            </Styles>

                                                                        </dx:ASPxGridView>

                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>



















                                                        </Items>
                                                    </dx:LayoutGroup>
                                                    <%--<dx:EmptyLayoutItem />--%>
                                                    <dx:LayoutGroup Caption="Data" ColCount="1">
                                                        <Items>


                                                            <dx:LayoutItem Caption="ClaimStatus" FieldName="ClaimStatus">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">
                                                                        <%--<dx:ASPxLabel runat="server" ID="ASPxLabel1"></dx:ASPxLabel>--%>

                                                                        <dx:ASPxComboBox runat="server" DropDownStyle="DropDown"
                                                                            Border-BorderStyle="None"
                                                                            ReadOnly="true" Paddings-Padding="0"
                                                                            DropDownButton-Visible="false">
                                                                            <Items>

                                                                                <dx:ListEditItem Value="00" Text="00 - Open" />
                                                                                <dx:ListEditItem Value="01" Text="01 - Reserve" />
                                                                                <dx:ListEditItem Value="02" Text="02 - Payment" />
                                                                                <dx:ListEditItem Value="99" Text="99 - Close" />
                                                                                <dx:ListEditItem Value="98" Text="98 - ReOpen" />
                                                                            </Items>
                                                                        </dx:ASPxComboBox>

                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="TempPolicy" FieldName="TempPolicy">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel2"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="RefNo" FieldName="RefNo">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel3"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Version" FieldName="Version">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel4"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="PolicyNo" FieldName="PolicyNo">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel5"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="PolicyYear" FieldName="PolicyYear">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel6"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>

                                                            <dx:LayoutItem Caption="ClaimNo" FieldName="ClaimNo">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel8"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="TransactionDate" FieldName="TransactionDate">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel9"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Unwriter" FieldName="Unwriter">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel10"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="InsuredName" FieldName="InsuredName">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel11"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="EffectiveDate" FieldName="EffectiveDate">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel12"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="ExpiryDate" FieldName="ExpiryDate">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel13"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Beneficiary" FieldName="Beneficiary">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel14"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="CarBrand" FieldName="CarBrand">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel15"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="CarModel" FieldName="CarModel">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel16"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="CarLicense" FieldName="CarLicense">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer17" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel17"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="CarYear" FieldName="CarYear">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer18" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel18"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="ChassisNo" FieldName="ChassisNo">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer19" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel19"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="ShowRoomName" FieldName="ShowRoomName">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer20" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel20"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="ShowRoomCode" FieldName="ShowRoomCode">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer21" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel21"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="ClaimNoticeDate" FieldName="ClaimNoticeDate">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer22" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel22"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="ClaimNoticeTime" FieldName="ClaimNoticeTime">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel1"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="ClaimDetails" FieldName="ClaimDetails">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer23" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel23"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="ClaimType" FieldName="ClaimType">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer24" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel24"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="ClaimResult" FieldName="ClaimResult">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer25" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel25"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="ClaimDamageDetails" FieldName="ClaimDamageDetails">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer26" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel26"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>

                                                            <dx:LayoutItem Caption="CallCenter" FieldName="CallCenter">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer29" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel29"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="AccidentDate" FieldName="AccidentDate">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer30" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel30"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>

                                                            <dx:LayoutItem Caption="AccidentTime" FieldName="AccidentTime">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer27" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel7"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="AccidentPlace" FieldName="AccidentPlace">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer31" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel31"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="AccidentTumbon" FieldName="AccidentTumbon">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer32" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel32"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="AccidentAmphur" FieldName="AccidentAmphur">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer33" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel33"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="AccidentProvince" FieldName="AccidentProvince">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer34" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel34"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="AccidentZipcode" FieldName="AccidentZipcode">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer35" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel35"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="DriverName" FieldName="DriverName">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer36" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel36"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="DriverTel" FieldName="DriverTel">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer37" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel37"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="NoticeName" FieldName="NoticeName">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer38" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel38"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="NoticeTel" FieldName="NoticeTel">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer39" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel39"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="GarageType" FieldName="GarageType">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer40" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel40"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="GarageCode" FieldName="GarageCode">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer41" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel41"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="GarageName" FieldName="GarageName">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer42" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel42"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="GaragePlace" FieldName="GaragePlace">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer43" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel43"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="GarageTumbon" FieldName="GarageTumbon">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer44" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel44"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="GarageAmphur" FieldName="GarageAmphur">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer45" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel45"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="GarageProvince" FieldName="GarageProvince">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer46" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel46"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="GarageZipcode" FieldName="GarageZipcode">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer47" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel47"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="CarRepairDate" FieldName="CarRepairDate">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer48" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel48"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="CarReceiveDate" FieldName="CarReceiveDate">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer49" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel49"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>

                                                            <dx:LayoutItem Caption="PartsDealerName" FieldName="PartsDealerName">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer54" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel54"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>

                                                            <dx:LayoutItem Caption="PaymentDetails" FieldName="PaymentDetails">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer56" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel56"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>

                                                            <dx:LayoutItem Caption="Amount1" FieldName="Amount1">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer58" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel58"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Amount2" FieldName="Amount2">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer59" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel59"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Amount3" FieldName="Amount3">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer60" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel60"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Remark" FieldName="Remark">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer57" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel57"></dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>




                                                        </Items>
                                                    </dx:LayoutGroup>

                                                </Items>
                                            </dx:ASPxFormLayout>




                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxCallbackPanel>

                                <dx:ASPxCallbackPanel ID="MailFormPanel" runat="server" RenderMode="Div" Height="250" Width="600" ScrollBars="Auto" ClientVisible="false" ClientInstanceName="ClientMailFormPanel">
                                    <PanelCollection>
                                        <dx:PanelContent>


                                            <dx:ASPxFormLayout ID="frmImportConsentForm" ClientInstanceName="frmImportConsentForm" runat="server">

                                                <Items>
                                                    <dx:LayoutGroup Caption="Upload Claim Form" AlignItemCaptions="true" ColCount="2">
                                                        <Items>

                                                            <%--  <dx:LayoutItem Caption="Temp Policy">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem Caption="Ref No">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>--%>



                                                            <dx:LayoutItem Caption="กรุณาเลือกไฟล์ข้อมูล" ColSpan="2">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer>
                                                                        <dx:ASPxUploadControl ID="frmImport_UploadFile"
                                                                            ClientInstanceName="frmImport_UploadFile"
                                                                            runat="server"
                                                                            ShowUploadButton="false"
                                                                            ShowProgressPanel="true"
                                                                            Width="400">
                                                                            <ValidationSettings AllowedFileExtensions=".xlsx,.csv"></ValidationSettings>
                                                                            <ClientSideEvents FileUploadComplete=" function(s, e) {    
                                                                                 
                                                                                if (e.callbackData.indexOf('success') == -1 ){    
                                                                                   popupResult.Show();
                                                                                    ASPxGridViewResult.Refresh();
                                                                                    alert(e.callbackData);
                                                                                }
                                                                                else
                                                                                {
                                                                                    var node = ClientMailTree.GetNodeByText('Claim Inbox');
                                                                                   ClientMailTree.SetSelectedNode(node);
                                                                                   DemoState = 'MailList';                       
                                                                                   ChangeDemoState('MailList', 'new', '');       
                                                                                   ClientMailGrid.PerformCallback('FolderChanged'); 
                                                                                   alert(e.callbackData);
                                                                                }
                                                                        }" />
                                                                        </dx:ASPxUploadControl>

                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>

                                                            <dx:LayoutItem Caption="" ColSpan="2">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer>
                                                                        <dx:ASPxButton ID="btnDownloadFormat" runat="server" Text="Import Format" Image-IconID="export_exporttoxlsx_16x16" CausesValidation="false" AutoPostBack="false">
                                                                            <ClientSideEvents Click="function(s,e){  
                                                                                    e.processOnServer = true;
                                                                                }" />

                                                                        </dx:ASPxButton>

                                                                        <dx:ASPxButton runat="server" ID="frmImport_btnUPload"
                                                                            Text="Upload" AutoPostBack="false">
                                                                            <ClientSideEvents Click="function(s,e)
                                                                                  {
                                                 
                                                                                  if(ASPxClientEdit.AreEditorsValid()) 
                                                                                    {                                              
                                                                                        var txt = frmImport_UploadFile.GetText(); 
                                                           
                                                                                        if (txt == '') 
                                                                                        {                                                               
                                                                                           alert('Invalid File!!!');
                                                                                        }
                                                                                        else
                                                                                        {                                            
                                                                                           frmImport_UploadFile.Upload();
                                                                                        }                                                           
                                                                                    }
                                                                                    e.processOnServer = false;
                                                                           }
                                                                           " />
                                                                        </dx:ASPxButton>

                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:LayoutGroup>
                                                </Items>
                                            </dx:ASPxFormLayout>

                                        </dx:PanelContent>
                                    </PanelCollection>

                                </dx:ASPxCallbackPanel>

                                <dx:ASPxCallbackPanel ID="ConsentFormPanel" runat="server" RenderMode="Div" Height="100%" ClientVisible="false" ClientInstanceName="ClientConsentFormPanel">
                                    <PanelCollection>
                                        <dx:PanelContent>




                                            <dx:ASPxFileManager ID="ASPxFileManager1" runat="server" Width="900" Height="400">


                                                <Settings
                                                    AllowedFileExtensions=".jpg,.jpeg,.gif,.rtf,.txt,.png,.doc,.pdf,.xls,.xlsx" />


                                                <SettingsEditing AllowDownload="true" />




                                                <SettingsFileList View="Details">
                                                </SettingsFileList>


                                            </dx:ASPxFileManager>

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


<%--<asp:SqlDataSource ID="SqlDataSource_ClaimTransaction_Data" runat="server"
    ConnectionString="<%$ ConnectionStrings:MotorClaimConnectionString %>"
    SelectCommand="SELECT * FROM tblClaimTransaction_Data where Unwriter=@UWCode ">
    <SelectParameters>
        <asp:SessionParameter Name="UWCode" SessionField="UWCode" />
    </SelectParameters>
</asp:SqlDataSource>--%>





<dx:ASPxGlobalEvents runat="server" ID="GlobalEvents">
    <ClientSideEvents
        ControlsInitialized="function(s, e) {

                    //ASPxClientUtils.AttachEventToElement(window, 'resize', function () {
                    //    var bodySelector = LeftPanel.IsExpandable() ? 'Portrait' : 'Landscape';
                    //    $('body').removeClass('Portrait').removeClass('Landscape').addClass(bodySelector);
                    //    ChangeExpandButtonsVisibility(LeftPanel.IsExpandable(), LeftPanel.IsExpanded());
                    //});

                    //if(ASPxClientUtils.touchUI) {
                    //    ASPxClientUtils.AttachEventToElement(window, 'orientationchange', function () 
                    //    {
                    //        ASPxClientTouchUI.ensureOrientationChanged(Adjust());
                    //    }
                    //    , false);
                    //}

                    //Adjust();
                    ShowMenuItems('new');
                    ChangeDemoState(DemoState, 'new', '');
                    //ClientLayout_PaneResized();
                }"
        BrowserWindowResized="function(s,e){
                        //ClientLayout_PaneResized();                
                }" />


</dx:ASPxGlobalEvents>


<dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="ClientLoadingPanel" Modal="true" />
<dx:ASPxHiddenField ID="HiddenField" runat="server" ClientInstanceName="ClientHiddenField" />



<dx:ASPxPopupControl ID="popupResult" runat="server" ClientInstanceName="popupResult"
    Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="ผลการ upload"
    ScrollBars="Both"
    AllowDragging="true"
    EnableAnimation="true"
    CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="true"
    ShowPageScrollbarWhenModal="true"
    Width="900"
    Height="550">

    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">

            <dx:ASPxGridView runat="server" ID="ASPxGridViewResult" ClientInstanceName="ASPxGridViewResult"
                SettingsLoadingPanel-Mode="ShowAsPopup" SettingsBehavior-AutoExpandAllGroups="true"
                DataSourceID="ObjectDataSource1" Width="100%" KeyFieldName="RunNo"
                Settings-HorizontalScrollBarMode="Visible">

                <Columns>
                    <dx:GridViewDataTextColumn FieldName="Status" GroupIndex="0" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />

                    <dx:GridViewDataTextColumn FieldName="RunNo" Width="50"  CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    
                    <dx:GridViewDataTextColumn FieldName="RefNo" Width="150" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataColumn Caption="Result" >
                        <DataItemTemplate>
                            <a href="javascript:void(0);" onclick="OnMoreInfoClick(this, '<%# Container.KeyValue %>')">
                                More Info...</a>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>


                    <dx:GridViewDataComboBoxColumn FieldName="ClaimStatus" CellStyle-Wrap="False">
                        <PropertiesComboBox>
                            <Items>
                                <dx:ListEditItem Value="00" Text="00 - Open" />
                                <dx:ListEditItem Value="01" Text="01 - Reserve" />
                                <dx:ListEditItem Value="02" Text="02 - Payment" />
                                <dx:ListEditItem Value="99" Text="99 - Close" />
                                <dx:ListEditItem Value="98" Text="98 - ReOpen" />
                            </Items>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataTextColumn FieldName="TempPolicy" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    
                    <dx:GridViewDataTextColumn FieldName="Version" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="PolicyNo" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="PolicyYear" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="ClaimNo" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="TransactionDate" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="Unwriter" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="InsuredName" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="EffectiveDate" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="ExpiryDate" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="Beneficiary" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="CarBrand" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="CarModel" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="CarLicense" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="CarYear" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="ChassisNo" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="ShowRoomName" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="ShowRoomCode" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="ClaimNoticeDate" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="ClaimNoticeTime" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="ClaimDetails" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="ClaimType" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="ClaimResult" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="ClaimDamageDetails" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="CallCenter" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="AccidentDate" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="AccidentTime" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="AccidentPlace" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="AccidentTumbon" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="AccidentAmphur" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="AccidentProvince" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="AccidentZipcode" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="DriverName" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="DriverTel" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="NoticeName" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="NoticeTel" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="GarageType" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="GarageCode" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="GarageName" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="GaragePlace" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="GarageTumbon" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="GarageAmphur" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="GarageProvince" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="GarageZipcode" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="CarRepairDate" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="CarReceiveDate" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="ConsentFormNo" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="PartsDealerName" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="PaymentDetails" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="Amount1" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="Amount2" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="Amount3" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="Remark" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />

                </Columns>
                <GroupSummary>
                    <dx:ASPxSummaryItem FieldName="Status" SummaryType="Count" />
                </GroupSummary>

                <SettingsPager Mode="ShowPager" />

                <SettingsSearchPanel Visible="true" />

                <SettingsBehavior AllowDragDrop="True"
                    AutoExpandAllGroups="True" AllowFocusedRow="True" 
                    EnableRowHotTrack="True"
                    ColumnResizeMode="Control" />


                <SettingsPager Mode="ShowPager" PageSize="15">
                    <PageSizeItemSettings Visible="false" Items="15, 30, 45" ShowAllItem="false" />
                </SettingsPager>
 
            </dx:ASPxGridView>

        </dx:PopupControlContentControl>
    </ContentCollection>

</dx:ASPxPopupControl>


<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="MotorClaimUploadResult" TypeName="DataClasses_MotorClaimDataExt">
    <SelectParameters>
        <asp:SessionParameter Name="MCGUID" SessionField="MCGUID" />
        <asp:SessionParameter Name="MCTYPE" SessionField="MCTYPE" />
    </SelectParameters>


</asp:ObjectDataSource>


<dx:ASPxPopupControl ID="popupResultDetails" ClientInstanceName="popupResultDetails" runat="server" AllowDragging="true"
        PopupHorizontalAlign="OutsideRight" HeaderText="Result">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <dx:ASPxCallbackPanel ID="callbackPanel_resultdetails"
                     ClientInstanceName="callbackPanel_resultdetails" runat="server"
                    Width="320px" Height="100px" 
                    
                    RenderMode="Table">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <table class="InfoTable">
                                <tr>
                                    <td>
                                      
                                    </td>
                                    <td>
                                        <asp:Literal ID="litText" runat="server" Text=""></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <ClientSideEvents Shown="popup_Shown" />
    </dx:ASPxPopupControl>
