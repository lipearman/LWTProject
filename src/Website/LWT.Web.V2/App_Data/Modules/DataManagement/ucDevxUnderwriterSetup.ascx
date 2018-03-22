<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxUnderwriterSetup.ascx.vb" Inherits="Modules_ucDevxUnderwriterSetup" %>
<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Underwriter" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>
            <dx:ASPxButton ID="btnImportUW" runat="server" Text="นำเข้าจาก SIBIS" Image-IconID="actions_add_16x16">
                <ClientSideEvents Click="function(s, e) {           
                       popUpAddUW.Show();        
                       e.processOnServer = false; 
                      }" />
            </dx:ASPxButton>
<%--            <dx:ASPxButton ID="btnExpandAll" runat="server" Text="Expand All">
                <ClientSideEvents Click="function(s, e) {           
                       LoadingPanel.Show();        
                       e.processOnServer = true; 
                      }" />
            </dx:ASPxButton>
            <dx:ASPxButton ID="btnCollapseAll" runat="server" Text="Collapse All">
                <ClientSideEvents Click="function(s, e) {           
                       LoadingPanel.Show();        
                       e.processOnServer = true; 
                      }" />
            </dx:ASPxButton>--%>

            <dx:ASPxGridView ID="gridUW" ClientInstanceName="gridUW" runat="server" DataSourceID="SqlDataSource_Unwriter"
                KeyFieldName="Underwriter" AutoGenerateColumns="False" Width="100%"  >
                <SettingsPager Mode="ShowPager">
                    <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                </SettingsPager>
                <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" AutoExpandAllGroups="true" />
                <SettingsSearchPanel Visible="true" />
                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />

              <%--  <SettingsCommandButton>
                    <UpdateButton ButtonType="Button" Text="บันทึก"></UpdateButton>
                    <EditButton ButtonType="Button" Text="แก้ไข"></EditButton>
                    <DeleteButton ButtonType="Button" Text="ลบ"></DeleteButton>
                    <CancelButton ButtonType="Button" Text="ยกเลิก"></CancelButton>
                </SettingsCommandButton>--%>
                <SettingsText PopupEditFormCaption="แก้ไขรายการ" ConfirmDelete="ยืนยันการลบ" />
                <SettingsDetail ShowDetailRow="true" />
                <SettingsPopup>
                    <EditForm Modal="true" Width="400" ShowHeader="true" HorizontalAlign="Center" VerticalAlign="Middle" />
                </SettingsPopup>
                  <Settings ShowHeaderFilterButton="true" ShowGroupPanel="true" />
                <Columns>

                    <dx:GridViewCommandColumn Width="100" CellStyle-Wrap="False" ShowNewButtonInHeader="false" ShowEditButton="true" ShowDeleteButton="true" />

                    <dx:GridViewDataColumn Caption="" EditFormSettings-Visible="False" Width="100" CellStyle-HorizontalAlign="Center" EditCellStyle-VerticalAlign="Bottom">
                        <DataItemTemplate>
                            <img src='images/InsurerLogo/<%# Eval("Underwriter").ToString().Trim() %>.jpg' width="30" />
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>


                    <dx:GridViewDataColumn FieldName="Underwriter" Caption="รหัส" CellStyle-Wrap="False" ReadOnly="true" Width="100" Settings-AutoFilterCondition="BeginsWith"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Name" Caption="ชื่ออังกฤษ" ReadOnly="true" CellStyle-Wrap="False">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataTextColumn FieldName="AccountContact" Caption="ชื่อไทย" CellStyle-Wrap="False">
                        <PropertiesTextEdit>
                            <ValidationSettings Display="Dynamic">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </PropertiesTextEdit>

                    </dx:GridViewDataTextColumn>

                      <dx:GridViewDataColumn FieldName="InsuranceLine" GroupIndex="0" ReadOnly="true" EditFormSettings-Visible="False" CellStyle-Wrap="False">
                    </dx:GridViewDataColumn>


                    <dx:GridViewDataTextColumn FieldName="ShortName" Caption="ชื่อย่อ" CellStyle-Wrap="False" Width="100">
                        <PropertiesTextEdit MaxLength="100">
                            <ValidationSettings Display="Dynamic">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </PropertiesTextEdit>

                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="InsurerCode" Caption="รหัสย่อ" CellStyle-Wrap="False" Width="100">
                        <PropertiesTextEdit MaxLength="2">
                            <ValidationSettings Display="Dynamic">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </PropertiesTextEdit>

                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataColumn FieldName="IsActive" Caption="สถานะ" CellStyle-Wrap="False" Width="100">
                    </dx:GridViewDataColumn>
 

                    <%--<dx:GridViewDataColumn FieldName="CrossReference" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="CreationBy" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="CreationDate" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="RequestBy" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="RequestDate" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ApproveBy" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ApproveDate" CellStyle-Wrap="False"></dx:GridViewDataColumn>--%>
                </Columns>

                <Templates>                    
                    <DetailRow>
                        <dx:ASPxPageControl runat="server" ID="pageControl" Width="400" EnableCallBacks="true">
                            <TabPages>


                                <dx:TabPage Text="English Information" Visible="true">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl1" runat="server">
                                            <table cellpadding="2" cellspacing="2" style="border-collapse: collapse; width: 100%">
                                                <tr>
                                                    <td style="font-weight: bold">Underwriter: </td>
                                                    <td colspan="3"><%# Eval("Underwriter")%></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold">CrossReference: </td>
                                                    <td colspan="3"><%# Eval("CrossReference")%></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold">InsurerCode: </td>
                                                    <td><%# Eval("InsurerCode")%></td>


                                                    <td style="font-weight: bold">ShortName: </td>
                                                    <td><%# Eval("ShortName")%></td>

                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold">Name: </td>
                                                    <td colspan="3"><%# Eval("Name")%></td>
                                                </tr>

                                                <tr>
                                                    <td style="font-weight: bold">Address: </td>
                                                    <td colspan="3"><%# Eval("Address1")%></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold"></td>
                                                    <td colspan="3"><%# Eval("Address2")%></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold"></td>
                                                    <td colspan="3"><%# Eval("Address3")%></td>
                                                </tr>

                                                <tr>
                                                    <td style="font-weight: bold">City:
                                                    </td>
                                                    <td>
                                                        <%# Eval("City")%>
                                                    </td>
                                                    <td style="font-weight: bold">PostCode:
                                                    </td>
                                                    <td>
                                                        <%# Eval("PostCode")%>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="font-weight: bold">Phone: </td>
                                                    <td colspan="3"><%# Eval("PhoneBusiness")%></td>
                                                </tr>

                                                <tr>
                                                    <td style="font-weight: bold">Home: </td>
                                                    <td colspan="3"><%# Eval("PhoneHome")%></td>
                                                </tr>

                                                <tr>
                                                    <td style="font-weight: bold">Fax: </td>
                                                    <td colspan="3"><%# Eval("Facsimile")%></td>
                                                </tr>
                                            </table>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>

                                <dx:TabPage Text="Thai Information" Visible="true">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl2" runat="server">
                                            <table cellpadding="2" cellspacing="2" style="border-collapse: collapse; width: 100%">
                                                <tr>
                                                    <td style="font-weight: bold">ชื่อ: </td>
                                                    <td colspan="3"><%# Eval("AccountContact")%></td>
                                                </tr>

                                                <tr>
                                                    <td style="font-weight: bold">ที่อยู่: </td>
                                                    <td colspan="3"><%# Eval("Addressee")%></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold"></td>
                                                    <td colspan="3"><%# Eval("Salutation")%></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold"></td>
                                                    <td colspan="3"><%# Eval("FinanceContact")%></td>
                                                </tr>

                                                <tr>
                                                    <td style="font-weight: bold">TAX ID: </td>
                                                    <td colspan="3"><%# Eval("PhoneFinance")%></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold">Claim Contact: </td>
                                                    <td><%# Eval("GeneralClaimContact")%></td>
                                                    <td style="font-weight: bold">Tel: </td>
                                                    <td><%# Eval("PhoneClaims")%></td>
                                                </tr>

                                                <tr>
                                                    <td style="font-weight: bold">Claim FAX:</td>
                                                    <td>
                                                        <%# Eval("FaxFinance")%>
                                                    </td>

                                                    <td style="font-weight: bold">Email:</td>
                                                    <td>
                                                        <%# Eval("Email")%>
                                                    </td>
                                                </tr>


                                            </table>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>


                                <dx:TabPage Text="Credit Term" Visible="true">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl3" runat="server">
                                            <table cellpadding="2" cellspacing="2" style="border-collapse: collapse; width: 100%">

                                                <tr>
                                                    <td style="font-weight: bold">Credir Term: </td>
                                                    <td><%# Eval("DaysCredit")%> Day(s)</td>
                                                    <td style="font-weight: bold">True Underwriter: </td>
                                                    <td><%# Eval("TrueUnderwriter")%></td>
                                                </tr>

                                                <tr>
                                                    <td style="font-weight: bold">Type Of Insurer:</td>
                                                    <td>
                                                        <%# Eval("Type")%>
                                                    </td>

                                                    <td style="font-weight: bold">Insurance Line:</td>
                                                    <td>
                                                        <%# Eval("InsuranceLine")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold">VAT Pay Type: </td>
                                                    <td colspan="3"><%# Eval("VATPayType")%></td>
                                                </tr>


                                            </table>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>



                            </TabPages>
                        </dx:ASPxPageControl>



                    </DetailRow>
                </Templates>


            </dx:ASPxGridView>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>


<asp:SqlDataSource ID="SqlDataSource_Unwriter" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * from [V_Register_Unwriter]"
    DeleteCommand="delete from [Register.Unwriter] where Underwriter=@Underwriter"
    UpdateCommand="update [Register.Unwriter] set IsActive=@IsActive,AccountContact=@AccountContact,ShortName=@ShortName,InsurerCode=@InsurerCode where Underwriter=@Underwriter ">
    <DeleteParameters>
        <asp:Parameter Name="Underwriter" Type="String" />
    </DeleteParameters>
    <UpdateParameters>

        <asp:Parameter Name="Underwriter" Type="String" />
        <asp:Parameter Name="AccountContact" Type="String" />
        <asp:Parameter Name="ShortName" Type="String" />
        <asp:Parameter Name="InsurerCode" Type="String" />
        <asp:Parameter Name="IsActive" Type="Boolean" />

    </UpdateParameters>


</asp:SqlDataSource>









<dx:ASPxPopupControl ID="popUpAddUW"
    ClientInstanceName="popUpAddUW"
    runat="server" Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="บริษัทประกันภัย"
    AllowDragging="true"
    EnableAnimation="true" CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="true" Width="100px">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">

            <dx:ASPxFormLayout ID="frmNewUW" runat="server" AlignItemCaptionsInAllGroups="True" ColCount="1" RequiredMarkDisplayMode="None" ShowItemCaptionColon="False">
                <Items>

                    <dx:LayoutItem Caption="บริษัทประกันภัย">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer
                                ID="LayoutItemNestedControlContainer9"
                                runat="server"
                                SupportsDisabledAttribute="True">
                                <dx:ASPxGridLookup ID="GridUWLookup" runat="server"
                                    SelectionMode="Single"
                                    DataSourceID="SqlDataSource_UWLookup"
                                    ClientInstanceName="GridUWLookup"
                                    KeyFieldName="Underwriter" Width="100"
                                    TextFormatString="{1}">
                                    <GridViewProperties>
                                        <SettingsBehavior EnableRowHotTrack="true" />
                                        <SettingsSearchPanel Visible="true" />
                                    </GridViewProperties>
                                    <ClearButton Visibility="True"></ClearButton>
                                    <ValidationSettings>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <Columns>

                                        <dx:GridViewDataColumn Caption="" Width="100" CellStyle-HorizontalAlign="Center" EditCellStyle-VerticalAlign="Bottom">
                                            <DataItemTemplate>
                                                <img src='images/InsurerLogo/<%# Eval("Underwriter").ToString().Trim() %>.jpg' width="30" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Underwriter" Width="50" Settings-AutoFilterCondition="BeginsWith" />
                                        <dx:GridViewDataColumn FieldName="Name" CellStyle-Wrap="False" Settings-AutoFilterCondition="Contains" />
                                        <dx:GridViewDataColumn FieldName="AccountContact" CellStyle-Wrap="False" Settings-AutoFilterCondition="Contains" />
                                    </Columns>
                                </dx:ASPxGridLookup>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>


                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxButton ID="btnSaveUW" runat="server" ValidationContainerID="frmNewUW"
                                    Text="บันทึก" Width="100">
                                    <ClientSideEvents Click="function(s, e) {
                                        if(ASPxClientEdit.AreEditorsValid()) 
                                        {                                              
                                            LoadingPanel.Show();
                                            cbSaveAddUW.PerformCallback('');
                                        }
                                        else
                                        {
                                            alert('กรุณากรอกข้อมูลให้ครบ');
                                        }
                                        e.processOnServer = false;
                                    }" />
                                </dx:ASPxButton>
                                <dx:ASPxCallback runat="server" ID="cbSaveAddUW" ClientInstanceName="cbSaveAddUW">
                                    <ClientSideEvents CallbackComplete="function(s,e){ 
                                             LoadingPanel.Hide();   
                                             if (e.result != 'success') {
                                                alert(e.result);                                                
                                             } 
                                             else
                                             {
                                                gridUW.Refresh();
                                                popUpAddUW.Hide();
                                             }
                                        }" />
                                </dx:ASPxCallback>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                </Items>
            </dx:ASPxFormLayout>



        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>


<asp:SqlDataSource ID="SqlDataSource_UWLookup" runat="server" ConnectionString="<%$ ConnectionStrings:SIBISDBConnectionString %>"
    SelectCommand="SELECT rtrim(Underwriter) as Underwriter, rtrim(Name) as Name , rtrim(AccountContact) as AccountContact FROM Underwriter  order by Name"></asp:SqlDataSource>
