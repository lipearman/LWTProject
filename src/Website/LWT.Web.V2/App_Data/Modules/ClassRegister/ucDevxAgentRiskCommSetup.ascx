<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxAgentRiskCommSetup.ascx.vb" Inherits="Modules_ucDevxAgentRiskCommSetup" %>
<script type="text/javascript">
    var keyValue;
    function OnMoreInfo(element, key) {
        callbackPanel_MoreInfo.SetContentHtml("");
        popupMoreInfo.ShowAtElement(element);
        keyValue = key;
    }
    function popup_ShownCommOut(s, e) {
        callbackPanel_MoreInfo.PerformCallback(keyValue);
    }
</script>


<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Underwriter" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxButton ID="btnAddNewCommOut" runat="server" Text="เพิ่ม Comm Out" Image-IconID="actions_add_16x16">
                <ClientSideEvents Click="function(s, e) {           
                       popupAgentRiskComm.Show();   
                       callbackPanel_popupAgentRiskComm.PerformCallback('new')     
                       e.processOnServer = false; 
                      }" />
            </dx:ASPxButton>
            <br />
            <asp:SqlDataSource ID="SqlDataSource_AgentRiskComm" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
                SelectCommand="select * from [V_AgentRiskComm] "
                DeleteCommand="delete from [Register.AgentRiskComm]  where ID=@ID"
                UpdateCommand="update [Register.AgentRiskComm] 
                set   CommissionOut=@CommissionOut
                ,CommOutCal=@CommOutCal
                ,OROut=@OROut
                ,OROutCal=@OROutCal
                ,AdminOut1=@AdminOut1
                ,AdminOut1Cal=@AdminOut1Cal
                ,AdminOut2=@AdminOut2
                ,AdminOut2Cal=@AdminOut2Cal
                ,OROutCalFrom=@OROutCalFrom 
                ,IsActive=@IsActive
                ,ModifyBy=@UserName
                ,ModifyDate=getdate()
                where ID=@ID">

                <DeleteParameters>
                    <asp:Parameter Name="ID" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="ID" />
                    <asp:Parameter Name="CommissionOut" />
                    <asp:Parameter Name="CommOutCal" />
                    <asp:Parameter Name="OROut" />
                    <asp:Parameter Name="OROutCal" />
                    <asp:Parameter Name="AdminOut1" />
                    <asp:Parameter Name="AdminOut1Cal" />
                    <asp:Parameter Name="AdminOut2" />
                    <asp:Parameter Name="AdminOut2Cal" />
                    <asp:Parameter Name="OROutCalFrom" />
                    <asp:Parameter Name="IsActive" />
                    <asp:Parameter Name="UserName" />
                </UpdateParameters>
            </asp:SqlDataSource>


            <%--e.NewValues("CommissionOut") = CommissionOut.Value
e.NewValues("CommOutCal") = CommOutCal.SelectedItem.Value
e.NewValues("OROut") = OROut.Value
e.NewValues("OROutCal") = OROutCal.SelectedItem.Value
e.NewValues("AdminOut1") = AdminOut1.Value
e.NewValues("AdminOut1Cal") = AdminOut1Cal.SelectedItem.Value
e.NewValues("AdminOut2") = AdminOut2.Value
e.NewValues("AdminOut2Cal") = AdminOut2Cal.SelectedItem.Value
e.NewValues("OROutCalFrom") = OROutCalFrom.SelectedItem.Value
e.NewValues("IsActive") = IsActive.Checked--%>

            <dx:ASPxGridView ID="gridAgentRiskComm" ClientInstanceName="gridAgentRiskComm" runat="server" DataSourceID="SqlDataSource_AgentRiskComm"
                KeyFieldName="ID" AutoGenerateColumns="False" Width="100%" SettingsBehavior-AutoExpandAllGroups="true">
                <SettingsPager Mode="ShowPager">
                    <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                </SettingsPager>
                <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" />
                <SettingsSearchPanel Visible="true" />
                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
               <%-- <SettingsCommandButton>
                    <UpdateButton ButtonType="Button" Text="บันทึก"></UpdateButton>
                    <EditButton ButtonType="Button" Text="แก้ไข"></EditButton>
                    <DeleteButton ButtonType="Button" Text="ลบ"></DeleteButton>
                    <CancelButton ButtonType="Button" Text="ยกเลิก"></CancelButton>
                </SettingsCommandButton>--%>


                <SettingsText PopupEditFormCaption="แก้ไขรายการ" ConfirmDelete="ยืนยันการลบ" />
                <SettingsPopup>
                    <EditForm Modal="true" MinWidth="100" Width="400" HorizontalAlign="WindowCenter" VerticalAlign="Middle" />
                    <HeaderFilter Height="200" />
                </SettingsPopup>
                <Settings ShowHeaderFilterButton="true" ShowGroupPanel="true" />



                <Columns>

                    <dx:GridViewDataColumn Caption="" EditFormSettings-Visible="False" Width="50" CellStyle-HorizontalAlign="Center" EditCellStyle-VerticalAlign="Bottom">
                        <DataItemTemplate>



                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="RowEditable" runat="server" Text="แก้ไข" AutoPostBack="false">
                                            <ClientSideEvents Click="function (s, e) {  
                                                gridAgentRiskComm.StartEditRow(s.cpVisibleIndex);
                                             }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>

                                        <dx:ASPxButton runat="server" AutoPostBack="false"
                                            RenderMode="Button"
                                            ID="RowDeleteable"
                                            Text="ลบ">
                                            <ClientSideEvents Click="function(s) 
                                                {
                                                    if(confirm('Do you want to delete?'))
                                                    {  
                                                        gridAgentRiskComm.DeleteRow(s.cpVisibleIndex);
                                                    }
                                                    e.processOnServer = false;
                                                }
                                            " />
                                        </dx:ASPxButton>

                                    </td>
                                </tr>

                            </table>



                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="status" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                        <EditFormSettings Visible="False" />
                        <DataItemTemplate>
                            <img src='images/<%# Eval("status").ToString().Trim()%>.gif' />
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>

                    <%-- <dx:GridViewDataColumn FieldName="Risk" CellStyle-Wrap="False"></dx:GridViewDataColumn>--%>

                    <dx:GridViewDataColumn FieldName="Risk" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center" CellStyle-Font-Bold="true">
                        <DataItemTemplate>
                            <a href="javascript:void(0);" onclick="OnMoreInfo(this, '<%# Eval("Risk").ToString().Trim()%>')"><%# Eval("Risk").ToString().Trim()%></a>
                        </DataItemTemplate>
                        <CellStyle Wrap="False"></CellStyle>
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataColumn FieldName="Agent" CellStyle-Wrap="False">
                        <Settings HeaderFilterMode="CheckedList" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Name" CellStyle-Wrap="False"></dx:GridViewDataColumn>


                    <dx:GridViewDataColumn FieldName="CommissionOut_Display" Caption="Comm" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="OROut_Display" Caption="SF" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="AdminOut1_Display" Caption="Admin1" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="AdminOut2_Display" Caption="Admin2" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                    <dx:GridViewDataColumn FieldName="AgentGroup" Caption="Group" CellStyle-Wrap="False"></dx:GridViewDataColumn>


                    <dx:GridViewDataColumn FieldName="Remark" CellStyle-Wrap="False"></dx:GridViewDataColumn>


                    <%--  <dx:GridViewDataColumn FieldName="HasOverComm"  Caption="OverComm" CellStyle-Wrap="False"></dx:GridViewDataColumn>--%>


                    <%--<dx:GridViewDataColumn FieldName="Underwriter" Caption="UWCode" CellStyle-Wrap="False"></dx:GridViewDataColumn>--%>
                    <%--<dx:GridViewDataColumn FieldName="Underwriter_Name" Caption="UWName" GroupIndex="0" CellStyle-Wrap="False"></dx:GridViewDataColumn>--%>
                    <%--<dx:GridViewDataColumn FieldName="CommIn_Display" Caption="CommIn" GroupIndex="1" CellStyle-Wrap="False"></dx:GridViewDataColumn>--%>

                    <dx:GridViewDataColumn FieldName="InsuranceLine" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                    <%--<dx:GridViewDataColumn FieldName="Description" CellStyle-Wrap="False"></dx:GridViewDataColumn>--%>



                    <dx:GridViewDataColumn FieldName="CreationDate" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="CreationBy" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ApproveDate" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ApproveBy" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>

                    <dx:GridViewDataColumn FieldName="HasOverComm" Caption="OverComm" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                        <EditFormSettings Visible="False" />
                        <DataItemTemplate>
                            <img src='images/alert_<%# Eval("HasOverComm").ToString().Trim()%>.gif' style="height: 15px;" />
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>


                    <dx:GridViewDataColumn FieldName="IsActive" CellStyle-Wrap="False"></dx:GridViewDataColumn>





                    <dx:GridViewDataTextColumn FieldName="RequestDate" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataColumn FieldName="RequestBy" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>


                </Columns>

                <Templates>

                    <EditForm>

                        <dx:ASPxFormLayout ID="frmEditAgentRisk" ClientInstanceName="frmEditAgentRisk" runat="server" ColCount="2" RequiredMarkDisplayMode="None">
                            <Items>

                                <dx:LayoutItem Caption="Agent">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer
                                            ID="LayoutItemNestedControlContainer23"
                                            runat="server"
                                            SupportsDisabledAttribute="True">

                                            <dx:ASPxLabel runat="server" ID="Agent" Value='<%# Eval("Agent")%>'></dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>

                                <dx:LayoutItem Caption="Risk">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer
                                            ID="LayoutItemNestedControlContainer9"
                                            runat="server"
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel runat="server" ID="Risk" Value='<%# Eval("Risk")%>'></dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>





                                <dx:LayoutItem Caption="CommissionOut" CaptionSettings-VerticalAlign="Middle">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">

                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxSpinEdit ID="CommissionOut" runat="server" MaxLength="10" Width="100" ValidationSettings-ValidationGroup="frmEditUnderwriter" DisplayFormatString="{0:n2}"
                                                            MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" Value='<%# Eval("CommissionOut")%>' NumberType="Float">
                                                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                                <RequiredField IsRequired="true" />
                                                                <RegularExpression ValidationExpression="" />
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td>

                                                        <dx:ASPxRadioButtonList ID="CommOutCal" runat="server" RepeatDirection="Horizontal" Value='<%# Eval("CommOutCal")%>'>
                                                            <Items>
                                                                <dx:ListEditItem Text="%" Value="x" />
                                                                <dx:ListEditItem Text="บาท" Value="+" />
                                                            </Items>
                                                            <Border BorderStyle="None" />
                                                        </dx:ASPxRadioButtonList>
                                                    </td>

                                                </tr>
                                            </table>

                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>


                                <dx:LayoutItem Caption="SF" CaptionSettings-VerticalAlign="Middle">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">


                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxSpinEdit ID="OROut" runat="server" MaxLength="10" Width="100" Value='<%# Eval("OROut")%>' ValidationSettings-ValidationGroup="frmEditUnderwriter" DisplayFormatString="{0:n2}"
                                                            MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" NumberType="Float">
                                                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td>

                                                        <dx:ASPxRadioButtonList ID="OROutCal" runat="server" RepeatDirection="Horizontal" Value='<%# Eval("OROutCal")%>'>
                                                            <Items>
                                                                <dx:ListEditItem Text="%" Value="x" />
                                                                <dx:ListEditItem Text="บาท" Value="+" />
                                                            </Items>
                                                            <Border BorderStyle="None" />
                                                        </dx:ASPxRadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>


                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>

                                <dx:LayoutItem Caption="Admin1" CaptionSettings-VerticalAlign="Middle">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" SupportsDisabledAttribute="True">


                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxSpinEdit ID="AdminOut1" runat="server" MaxLength="10" Width="100" Value='<%# Eval("AdminOut1")%>' ValidationSettings-ValidationGroup="frmEditUnderwriter" DisplayFormatString="{0:n2}"
                                                            MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" NumberType="Float">
                                                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td>

                                                        <dx:ASPxRadioButtonList ID="AdminOut1Cal" runat="server" RepeatDirection="Horizontal" Value='<%# Eval("AdminOut1Cal")%>'>
                                                            <Items>
                                                                <dx:ListEditItem Text="%" Value="x" />
                                                                <dx:ListEditItem Text="บาท" Value="+" />
                                                            </Items>
                                                            <Border BorderStyle="None" />
                                                        </dx:ASPxRadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>


                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>


                                <dx:LayoutItem Caption="Admin2" CaptionSettings-VerticalAlign="Middle">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" SupportsDisabledAttribute="True">


                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxSpinEdit ID="AdminOut2" runat="server" MaxLength="10" Width="100" Value='<%# Eval("AdminOut2")%>' ValidationSettings-ValidationGroup="frmEditUnderwriter" DisplayFormatString="{0:n2}"
                                                            MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" NumberType="Float">
                                                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td>

                                                        <dx:ASPxRadioButtonList ID="AdminOut2Cal" runat="server" RepeatDirection="Horizontal" Value='<%# Eval("AdminOut2Cal")%>'>
                                                            <Items>
                                                                <dx:ListEditItem Text="%" Value="x" />
                                                                <dx:ListEditItem Text="บาท" Value="+" />
                                                            </Items>
                                                            <Border BorderStyle="None" />
                                                        </dx:ASPxRadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>

                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>


                                <dx:LayoutItem Caption="CalFrom">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer21" runat="server" SupportsDisabledAttribute="True">

                                            <dx:ASPxComboBox ID="OROutCalFrom" runat="server" Width="100" Value='<%# Eval("OROutCalFrom")%>'>
                                                <Items>
                                                    <dx:ListEditItem Text="PREMIUM" Value="P" />
                                                    <dx:ListEditItem Text="BROKERAGE" Value="B" />
                                                </Items>
                                            </dx:ASPxComboBox>


                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>



                                <dx:LayoutItem Caption="IsActive">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server" SupportsDisabledAttribute="True">

                                            <dx:ASPxCheckBox ID="IsActive" runat="server" Value='<%# Eval("IsActive")%>'>
                                            </dx:ASPxCheckBox>

                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>


                                <dx:LayoutItem ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">

                                            <table style="float: left">
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="b1" runat="server" UseSubmitBehavior="false" CausesValidation="true" AutoPostBack="false"
                                                            Text="Save">
                                                            <ClientSideEvents Click="function(s){
                                                                                if(ASPxClientEdit.ValidateGroup('frmEditAgentRisk')) 
                                                                                {
                                                                                        gridAgentRiskComm.UpdateEdit();              
                                                                                }          
                                                                            }                                                                    
                                                                        " />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <dx:ASPxButton ID="b2" runat="server" UseSubmitBehavior="false" AutoPostBack="false"
                                                            Text="Cancel">
                                                            <ClientSideEvents Click="function(s){
                                                                        gridAgentRiskComm.CancelEdit();
                                                                     }
                                                                        " />
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>

                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>


                            </Items>
                        </dx:ASPxFormLayout>






                    </EditForm>

                </Templates>
            </dx:ASPxGridView>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>



<asp:SqlDataSource ID="SqlDataSource_Underwriter" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="SELECT  rtrim(Underwriter) as Underwriter ,rtrim(AccountContact) as Name 
                            ,ShortName FROM [Register.Unwriter]  
                            ORDER BY AccountContact "></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_Agent" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="SELECT rtrim(Agent) as Agent,rtrim(Name) as Name,rtrim(Salutation) as Salutation,rtrim(Occupation) as Occupation,rtrim(InternetAddress) as InternetAddress FROM [CPS].[dbo].[Register.Agent] where [Occupation] <> 'LAPSE' order by agent,name "></asp:SqlDataSource>

<dx:ASPxPopupControl ID="popupAgentRiskComm"
    ClientInstanceName="popupAgentRiskComm"
    runat="server" AllowDragging="true"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    ShowPageScrollbarWhenModal="true"
    Modal="true"
    CloseAction="CloseButton"
    HeaderText="Comm Out">
    <ClientSideEvents PopUp="function(s,e){ ASPxClientEdit.ClearEditorsInContainerById('frmNewAgentRisk') ;Agent.SetText('');gridCOR.Refresh()}" />
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            <dx:ASPxCallbackPanel ID="callbackPanel_popupAgentRiskComm"
                ClientInstanceName="callbackPanel_popupAgentRiskComm" runat="server"
                RenderMode="Table">
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server">

                        <dx:ASPxFormLayout ID="frmNewAgentRisk" runat="server" ColCount="2" RequiredMarkDisplayMode="None">
                            <Items>

                                <dx:LayoutItem Caption="Agent" ColSpan="2">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer
                                            ID="LayoutItemNestedControlContainer23"
                                            runat="server"
                                            SupportsDisabledAttribute="True">


                                            <dx:ASPxGridLookup ID="Agent" runat="server" SelectionMode="Single"
                                                DataSourceID="SqlDataSource_Agent" ClientInstanceName="Agent" Width="500"
                                                KeyFieldName="Agent" TextFormatString="{0} - {1}" MultiTextSeparator=",">
                                                <ClientSideEvents ValueChanged="function (s, e){
                                                    callbackPanel_popupAgentRiskComm.PerformCallback( 'select_ag|' + s.GetValue());
                                                }" />
                                                <ClearButton Visibility="false"></ClearButton>
                                                <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                    <RequiredField IsRequired="true" />
                                                </ValidationSettings>
                                                <GridViewProperties>
                                                    <Settings ShowHeaderFilterButton="true" />
                                                    <SettingsSearchPanel Visible="true" />

                                                </GridViewProperties>
                                                <Columns>
                                                    <dx:GridViewDataColumn FieldName="Agent" Width="50" CellStyle-Wrap="False" />
                                                    <dx:GridViewDataColumn FieldName="Name" CellStyle-Wrap="False" />
                                                    <dx:GridViewDataColumn FieldName="Salutation" CellStyle-Wrap="False" />

                                                    <dx:GridViewDataColumn FieldName="InternetAddress" CellStyle-Wrap="False" />
                                                    <dx:GridViewDataColumn FieldName="Occupation" CellStyle-Wrap="False" />

                                                </Columns>

                                            </dx:ASPxGridLookup>




                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>




                                <dx:LayoutItem Caption="Brokerage" CaptionSettings-VerticalAlign="Middle">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">

                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxSpinEdit ID="Brokerage" runat="server" MaxLength="10" Width="100"
                                                            MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" NumberType="Float" DisplayFormatString="{0:n2}">
                                                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td>

                                                        <dx:ASPxRadioButtonList ID="BrokerageCal" runat="server" RepeatDirection="Horizontal">
                                                            <Items>
                                                                <dx:ListEditItem Text="%" Value="x" Selected="true" />
                                                                <dx:ListEditItem Text="บาท" Value="+" />
                                                            </Items>
                                                            <Border BorderStyle="None" />
                                                        </dx:ASPxRadioButtonList>
                                                    </td>

                                                </tr>
                                            </table>

                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>


                                <dx:LayoutItem Caption="ORComm" CaptionSettings-VerticalAlign="Middle">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">


                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxSpinEdit ID="OR" runat="server" MaxLength="10" Width="100"
                                                            MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" NumberType="Float" DisplayFormatString="{0:n2}">
                                                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td>

                                                        <dx:ASPxRadioButtonList ID="ORCal" runat="server" RepeatDirection="Horizontal">
                                                            <Items>
                                                                <dx:ListEditItem Text="%" Value="x" Selected="true" />
                                                                <dx:ListEditItem Text="บาท" Value="+" />
                                                            </Items>
                                                            <Border BorderStyle="None" />
                                                        </dx:ASPxRadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>


                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>

                                <dx:LayoutItem Caption="Admin1" CaptionSettings-VerticalAlign="Middle">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" SupportsDisabledAttribute="True">


                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxSpinEdit ID="Admin1" runat="server" MaxLength="10" Width="100"
                                                            MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" NumberType="Float" DisplayFormatString="{0:n2}">
                                                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td>

                                                        <dx:ASPxRadioButtonList ID="Admin1Cal" runat="server" RepeatDirection="Horizontal">
                                                            <Items>
                                                                <dx:ListEditItem Text="%" Value="x" Selected="true" />
                                                                <dx:ListEditItem Text="บาท" Value="+" />
                                                            </Items>
                                                            <Border BorderStyle="None" />
                                                        </dx:ASPxRadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>


                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>


                                <dx:LayoutItem Caption="Admin2" CaptionSettings-VerticalAlign="Middle">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" SupportsDisabledAttribute="True">


                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxSpinEdit ID="Admin2" runat="server" MaxLength="10" Width="100"
                                                            MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" NumberType="Float" DisplayFormatString="{0:n2}">
                                                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td>

                                                        <dx:ASPxRadioButtonList ID="Admin2Cal" runat="server" RepeatDirection="Horizontal">
                                                            <Items>
                                                                <dx:ListEditItem Text="%" Value="x" Selected="true" />
                                                                <dx:ListEditItem Text="บาท" Value="+" />
                                                            </Items>
                                                            <Border BorderStyle="None" />
                                                        </dx:ASPxRadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>

                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>

                                <dx:LayoutItem Caption="Upfont">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" SupportsDisabledAttribute="True">


                                            <dx:ASPxCheckBox runat="server" ID="OffsetORFlag" Checked="true"></dx:ASPxCheckBox>


                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>


                                <dx:LayoutItem Caption="CalFrom">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer21" runat="server" SupportsDisabledAttribute="True">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxComboBox ID="CalFrom" runat="server" Caption=" " Width="100">
                                                            <Items>
                                                                <dx:ListEditItem Text="PREMIUM" Value="P" Selected="true" />
                                                                <dx:ListEditItem Text="BROKERAGE" Value="B" />
                                                            </Items>
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                    <td>&nbsp;IsActive :</td>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="IsActive" runat="server">
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                </tr>

                                            </table>





                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>


                                <%--                                 <dx:LayoutItem Caption="IsActive">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server" SupportsDisabledAttribute="True">

                                           
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>--%>



                                <dx:LayoutItem Caption="Risk" ColSpan="2">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server" SupportsDisabledAttribute="True">



                                            <dx:ASPxGridView ID="gridCOR" ClientInstanceName="gridCOR" DataSourceID="SqlDataSource_COR"
                                                runat="server" Width="600"
                                                KeyFieldName="Risk">
                                                <SettingsBehavior EnableRowHotTrack="true" />
                                                <SettingsSearchPanel Visible="true" />
                                                <SettingsPager Mode="EndlessPaging" PageSize="20" />
                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Width="40" ShowClearFilterButton="true" SelectAllCheckboxMode="AllPages" />
                                                    <dx:GridViewDataTextColumn FieldName="Risk" Width="100" Caption="Risk" CellStyle-Wrap="False" Settings-AllowHeaderFilter="True" />
                                                    <dx:GridViewDataTextColumn FieldName="Description" Width="150" Caption="Description" CellStyle-Wrap="False" Settings-AllowHeaderFilter="True" />
                                                    <dx:GridViewDataTextColumn FieldName="InsuranceLine" Width="150" Caption="InsuranceLine" CellStyle-Wrap="False" Settings-AllowHeaderFilter="True" />
                                                </Columns>
                                            </dx:ASPxGridView>

                                            <asp:SqlDataSource ID="SqlDataSource_COR" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
                                                SelectCommand="select Risk,Description,
                                                case when [InsuranceLine] = 1 then 'Life'
                                                when [InsuranceLine] = 2 then 'Non Life'
                                                else ''
                                                end as [InsuranceLine] from [Register.ClassOfRisk] 
                                                where risk not in(select risk from [Register.AgentRiskComm] where [Register.AgentRiskComm].risk=[Register.ClassOfRisk].risk and rtrim(Agent)=rtrim(@Agent)) 
                                                 
                                                ">

                                                <SelectParameters>
                                                    <asp:SessionParameter Name="Agent" SessionField="Agent" />
                                                </SelectParameters>


                                            </asp:SqlDataSource>

                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>




                                <dx:LayoutItem Caption="">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server" SupportsDisabledAttribute="True">

                                            <dx:ASPxButton ID="btnAddInsurer" runat="server" ValidationContainerID="frmNewUnderwriter"
                                                Text="Save" Width="100">
                                                <ClientSideEvents Click="function(s, e) {
                                                    if(ASPxClientEdit.AreEditorsValid()) {
                                                        LoadingPanel.Show();
                                                        cbAddAgentRisk.PerformCallback('');
                                                        e.processOnServer = true; 
                                                    }
                                                    else
                                                    {
                                            
                                                    }

                                                    e.processOnServer = false;
                                                }" />
                                            </dx:ASPxButton>

                                            <dx:ASPxCallback runat="server" ID="cbAddAgentRisk" ClientInstanceName="cbAddAgentRisk">
                                                <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide(); }" CallbackComplete="function(s,e){                                                                                     
                                                    LoadingPanel.Hide(); 

                                                    if(e.result == 'success')
                                                    {
                                                        popupAgentRiskComm.Hide();
                                                        gridAgentRiskComm.ApplySearchPanelFilter(Agent.GetValue());
                                                    }                                                     
                                                    else
                                                    {
                                                        alert(e.result);
                                                    }
                                           
                                                }" />

                                            </dx:ASPxCallback>

                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>



                            </Items>
                        </dx:ASPxFormLayout>





                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>






<dx:ASPxPopupControl ID="popupMoreInfo"
    ClientInstanceName="popupMoreInfo"
    runat="server" AllowDragging="true"
    PopupHorizontalAlign="LeftSides"
    PopupVerticalAlign="WindowCenter"
    ShowPageScrollbarWhenModal="true"
    Modal="true"
    CloseAction="CloseButton" ShowMaximizeButton="true" ScrollBars="Auto"
    HeaderText="Comm Out">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
            <dx:ASPxCallbackPanel ID="callbackPanel_MoreInfo" ClientInstanceName="callbackPanel_MoreInfo" runat="server"
                RenderMode="Table">
                <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide();}" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent2" runat="server">


                        <dx:ASPxFormLayout runat="server" ID="settingsFormLayout" AlignItemCaptionsInAllGroups="True" Width="100%" SettingsItemCaptions-HorizontalAlign="Right">
                            <Items>

                                <dx:LayoutGroup Caption="Class Of Risk" GroupBoxDecoration="HeadingLine" ColCount="2">
                                    <Items>
                                        <dx:LayoutItem Caption="Risk">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxLabel runat="server" ID="lbRisk" ClientInstanceName="lbRisk"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Description">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxLabel runat="server" ID="lbDescription"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Department">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxLabel runat="server" ID="lbDepartment"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem Caption="InsuranceLine">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="lbInsuranceLine"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="RiskShortDesc">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxLabel runat="server" ID="lbRiskShortDesc"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Status">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <%--<dx:ASPxLabel runat="server" ID="lbStatus"></dx:ASPxLabel>--%>
                                                    <dx:ASPxImage ID="lbStatus" Height="15" runat="server"></dx:ASPxImage>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Risk I">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="lbRiskGroupI"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Risk II">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="lbRiskGroupII"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Risk Government" ColSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="lbRiskGovernment"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>


                                                    <dx:ASPxButton ID="btnRequestCOR" ClientInstanceName="btnRequestCOR" runat="server" Text="Request - Class Of Risk">
                                                        <ClientSideEvents Click="function(s, e) {                                                                 
                                                                    LoadingPanel.Show();
                                                                    cbRequestCOR.PerformCallback(lbRisk.GetText());
                                                                    e.processOnServer = false;

                                                                    }
                                                                    " />
                                                    </dx:ASPxButton>

                                                    <dx:ASPxCallback runat="server" ID="cbRequestCOR" ClientInstanceName="cbRequestCOR">
                                                        <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide();}"
                                                            CallbackComplete="function(s,e){
                                                                    LoadingPanel.Hide(); 
                                                                    if(e.result == 'success')
                                                                    {
                                                                         //callbackPanel_MoreInfo.PerformCallback(lbRisk.GetText());
                                                                         //gridCOR.Refresh();
                                                                         popupMoreInfo.Hide();
                                                                         gridCOR.Refresh();
                                                                         gridAgentRiskComm.Refresh();
                                                                    }                                                     
                                                                    else
                                                                    {
                                                                        alert(e.result);
                                                                    }
                                                            }" />
                                                    </dx:ASPxCallback>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
















                                    </Items>
                                </dx:LayoutGroup>


                                <dx:LayoutGroup Caption="Commission In" GroupBoxDecoration="HeadingLine" ColCount="3">
                                    <Items>


                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>


                                                    <dx:ASPxGridView ID="gridCommIn" ClientInstanceName="gridCommIn" runat="server" DataSourceID="SqlDataSource_CommIn"
                                                        KeyFieldName="ID" AutoGenerateColumns="False" Width="100%" SettingsBehavior-AutoExpandAllGroups="true">
                                                        <SettingsPager Mode="ShowPager">
                                                            <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                                                        </SettingsPager>
                                                        <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" />
                                                        <SettingsSearchPanel Visible="true" />
                                                        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
                                                      <%--  <SettingsCommandButton>
                                                            <UpdateButton ButtonType="Button" Text="บันทึก"></UpdateButton>
                                                            <EditButton ButtonType="Button" Text="แก้ไข"></EditButton>
                                                            <DeleteButton ButtonType="Button" Text="ลบ"></DeleteButton>
                                                            <CancelButton ButtonType="Button" Text="ยกเลิก"></CancelButton>
                                                        </SettingsCommandButton>--%>


                                                        <SettingsText PopupEditFormCaption="แก้ไขรายการ" ConfirmDelete="ยืนยันการลบ" />
                                                        <SettingsPopup>
                                                            <EditForm Modal="true" MinWidth="100" Width="400" HorizontalAlign="WindowCenter" VerticalAlign="Middle" />
                                                            <HeaderFilter Height="200" />
                                                        </SettingsPopup>
                                                        <Settings ShowHeaderFilterButton="true" ShowGroupPanel="true" />



                                                        <Columns>

                                                            <dx:GridViewDataColumn FieldName="Underwriter" Caption="UCode" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="AccountContact" Caption="Name" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                                                            <dx:GridViewDataColumn FieldName="Brokerage_display" Caption="Brokerage" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="ORCommissionPercent_display" Caption="SF" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="AdminFeeIn1_display" Caption="Admin1" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="AdminFeeIn2_display" Caption="Admin2" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                                                            <dx:GridViewDataColumn FieldName="OffsetORFlag" Caption="UpFront" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="Remark" CellStyle-Wrap="False"></dx:GridViewDataColumn>




                                                            <dx:GridViewDataColumn FieldName="CreationDate" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="CreationBy" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="ApproveDate" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="ApproveBy" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="IsActive" CellStyle-Wrap="False"></dx:GridViewDataColumn>


                                                            <dx:GridViewDataTextColumn FieldName="RequestDate" CellStyle-Wrap="False">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataTextColumn>

                                                            <dx:GridViewDataColumn FieldName="RequestBy" CellStyle-Wrap="False">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataColumn>

                                                            <dx:GridViewDataColumn FieldName="status" Caption="Status" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                                <DataItemTemplate>
                                                                    <img src='images/<%# Eval("status").ToString().Trim()%>.gif' />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>


                                                        </Columns>

                                                    </dx:ASPxGridView>


                                                    <asp:SqlDataSource ID="SqlDataSource_CommIn" runat="server"
                                                        ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
                                                        SelectCommand="SELECT * from V_RiskUwriter where Risk=@Risk">
                                                        <SelectParameters>
                                                            <asp:SessionParameter Name="Risk" SessionField="Risk" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>



























                                                    <%--      <dx:ASPxButton ID="btnRequestCommIn" runat="server" Text="Request - Commission In">
                                                        <ClientSideEvents Click="function(s, e) {                                                                 
                                                                    LoadingPanel.Show();
                                                                    cbRequestCommIn.PerformCallback(lbRisk.GetText());
                                                                    e.processOnServer = false;

                                                                    }
                                                                    " />

                                                    </dx:ASPxButton>



                                                    <dx:ASPxCallback runat="server" ID="cbRequestCommIn" ClientInstanceName="cbRequestCommIn">
                                                        <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide();}"
                                                            CallbackComplete="function(s,e){
                                                                    LoadingPanel.Hide(); 
                                                                    if(e.result == 'success')
                                                                    {
                                                                         callbackPanel_MoreInfo.PerformCallback(lbRisk.GetText());
                                                                         gridCOR.Refresh();
                                                                    }                                                     
                                                                    else
                                                                    {
                                                                        alert(e.result);
                                                                    }
                                                            }" />
                                                    </dx:ASPxCallback>--%>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>

                                <dx:LayoutGroup Caption="Commission Out" GroupBoxDecoration="HeadingLine" ColCount="2">
                                    <Items>
                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxGridView ID="gridCommOut" ClientInstanceName="gridCommOut" DataSourceID="SqlDataSource_CommOut"
                                                        runat="server"
                                                        KeyFieldName="ID">

                                                        <SettingsPager Mode="ShowPager">
                                                            <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                                                        </SettingsPager>
                                                        <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" />
                                                        <SettingsSearchPanel Visible="true" />
                                                        <Settings ShowHeaderFilterButton="true" />


                                                        <Columns>
                                                            <%--<dx:GridViewDataTextColumn FieldName="Risk" Caption="Risk" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />--%>
                                                            <dx:GridViewDataTextColumn FieldName="Agent" Caption="Code" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="Name" Caption="Name" CellStyle-Wrap="False" />
                                                            <dx:GridViewDataTextColumn FieldName="CommissionOut_Display" Caption="CommOut" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="OROut_Display" Caption="SF" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="AdminOut1_Display" Caption="Admin1" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="AdminOut2_Display" Caption="Admin2" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <%--            <dx:GridViewDataTextColumn FieldName="CreationDate"  CellStyle-Wrap="False"/>
            <dx:GridViewDataTextColumn FieldName="CreationBy"  CellStyle-Wrap="False"/>
                                                            --%>
                                                            <%--<dx:GridViewDataTextColumn FieldName="OROutCalFrom" Caption="OROutCalFrom"  CellStyle-Wrap="False"/>--%>


                                                            <dx:GridViewDataTextColumn FieldName="Remark" Caption="Remark" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />



                                                            <dx:GridViewDataTextColumn FieldName="AgentGroup" Caption="Group" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />



                                                            <%-- <dx:GridViewDataColumn FieldName="HasOverComm" Caption="OverComm" CellStyle-Wrap="False"></dx:GridViewDataColumn>--%>

                                                            <dx:GridViewDataColumn FieldName="HasOverComm" Caption="OverComm" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                                <DataItemTemplate>
                                                                    <img src='images/alert_<%# Eval("HasOverComm").ToString().Trim()%>.gif' style="height: 15px;" />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>




                                                            <dx:GridViewDataColumn FieldName="IsActive" CellStyle-Wrap="False" />


                                                            <dx:GridViewDataTextColumn FieldName="RequestDate" CellStyle-Wrap="False">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataTextColumn>

                                                            <dx:GridViewDataColumn FieldName="RequestBy" CellStyle-Wrap="False">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataColumn>

                                                            <dx:GridViewDataColumn FieldName="status" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                                <DataItemTemplate>
                                                                    <img src='images/<%# Eval("status").ToString().Trim()%>.gif' />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>
                                                        </Columns>
                                                    </dx:ASPxGridView>

                                                    <%-- <br />
                                                    <dx:ASPxButton ID="btnRequestCommOut" runat="server" Text="Request - Commission Out">
                                                        <ClientSideEvents Click="function(s, e) {                                                                 
                                                                    LoadingPanel.Show();
                                                                    cbRequestCommOut.PerformCallback(lbRisk.GetText());
                                                                    e.processOnServer = false;

                                                                    }
                                                                    " />

                                                    </dx:ASPxButton>

                                                    <dx:ASPxCallback runat="server" ID="cbRequestCommOut" ClientInstanceName="cbRequestCommOut">
                                                        <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide();}"
                                                            CallbackComplete="function(s,e){
                                                                    LoadingPanel.Hide(); 
                                                                    if(e.result == 'success')
                                                                    {
                                                                         callbackPanel_MoreInfo.PerformCallback(lbRisk.GetText());
                                                                         gridCOR.Refresh();
                                                                    }                                                     
                                                                    else
                                                                    {
                                                                        alert(e.result);
                                                                    }
                                                            }" />
                                                    </dx:ASPxCallback>--%>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>




                                    </Items>
                                </dx:LayoutGroup>

                            </Items>
                        </dx:ASPxFormLayout>




                        <asp:SqlDataSource ID="SqlDataSource_CommOut" runat="server"
                            ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
                            SelectCommand="SELECT * from V_AgentRiskComm where Risk=@Risk">
                            <SelectParameters>
                                <asp:SessionParameter Name="Risk" SessionField="Risk" />
                            </SelectParameters>
                        </asp:SqlDataSource>

                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <ClientSideEvents Shown="popup_ShownCommOut" />
</dx:ASPxPopupControl>













