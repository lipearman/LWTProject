<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxAgentSetup.ascx.vb" Inherits="Modules_ucDevxAgentSetup" %>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Agent" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>
            <dx:ASPxButton ID="btnImportAgent" runat="server" Text="นำเข้าจาก SIBIS" Image-IconID="actions_add_16x16">
                <ClientSideEvents Click="function(s, e) {           
                       popUpAddAgent.Show();        
                       e.processOnServer = false; 
                      }" />
            </dx:ASPxButton>
            <dx:ASPxButton ID="btnExpandAll" runat="server" Text="Expand All">
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
            </dx:ASPxButton>

            <dx:ASPxGridView ID="gridAgent" ClientInstanceName="gridAgent" runat="server" DataSourceID="SqlDataSource_Agent"
                KeyFieldName="Agent" AutoGenerateColumns="False" Width="100%"  SettingsBehavior-AutoExpandAllGroups="true">
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

                <SettingsDetail ShowDetailRow="true" />
                <SettingsPopup>
                    <EditForm Modal="true" Width="400" HorizontalAlign="Center" VerticalAlign="Middle" />
                </SettingsPopup>
                <SettingsText PopupEditFormCaption="แก้ไขรายการ" ConfirmDelete="ยืนยันการลบ" />

                <Settings ShowGroupPanel="true" />
                
                <Columns>

                    <dx:GridViewCommandColumn Width="100" CellStyle-Wrap="False" ShowNewButtonInHeader="false" ShowEditButton="true" ShowDeleteButton="true" />
                    <dx:GridViewDataColumn FieldName="Agent" Caption="รหัสตัวแทน" CellStyle-Wrap="False" ReadOnly="true" Width="100" Settings-AutoFilterCondition="BeginsWith">
                        <EditFormSettings  VisibleIndex="0" />

                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Name" Caption="ชื่ออังกฤษ" ReadOnly="true"  CellStyle-Wrap="False">
                         <EditFormSettings  VisibleIndex="0" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataTextColumn FieldName="Addressee" Caption="ชื่อไทย" CellStyle-Wrap="False">
                        <PropertiesTextEdit>
                            <ValidationSettings Display="Dynamic">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                         <EditFormSettings  VisibleIndex="1" />
                    </dx:GridViewDataTextColumn>


                      <dx:GridViewDataTextColumn FieldName="Address1" Caption="ที่อยู่ 1" CellStyle-Wrap="False" Width="100" Visible="false">
                        <PropertiesTextEdit MaxLength="35">
                        </PropertiesTextEdit>
                        <EditFormSettings Visible="true" VisibleIndex="1" />
                    </dx:GridViewDataTextColumn>
                      <dx:GridViewDataTextColumn FieldName="Address2" Caption="ที่อยู่ 2" CellStyle-Wrap="False" Width="100" Visible="false">
                        <PropertiesTextEdit MaxLength="35">
                        </PropertiesTextEdit>
                        <EditFormSettings Visible="true" VisibleIndex="1" />
                    </dx:GridViewDataTextColumn>
                      <dx:GridViewDataTextColumn FieldName="Address3" Caption="ที่อยู่ 3" CellStyle-Wrap="False" Width="100" Visible="false">
                        <PropertiesTextEdit MaxLength="35">
                        </PropertiesTextEdit>
                        <EditFormSettings Visible="true" VisibleIndex="1" />
                    </dx:GridViewDataTextColumn>
                    
                     <dx:GridViewDataTextColumn FieldName="City" Caption="จังหวัด" CellStyle-Wrap="False" Width="100" Visible="false">
                        <PropertiesTextEdit MaxLength="35">
                        </PropertiesTextEdit>
                        <EditFormSettings Visible="true" VisibleIndex="1" />
                    </dx:GridViewDataTextColumn>



                     <dx:GridViewDataTextColumn FieldName="ContactPerson" Caption="ชื่อผู้ติดต่อ" CellStyle-Wrap="False" Width="100" Settings-AutoFilterCondition="BeginsWith">
                         <PropertiesTextEdit MaxLength="30">
                        </PropertiesTextEdit>
                        <EditFormSettings  VisibleIndex="1" />
                    </dx:GridViewDataTextColumn>

                       <dx:GridViewDataTextColumn FieldName="PhoneBusiness" Caption="เบอร์โทร"  Visible="false"  CellStyle-Wrap="False" Width="100" Settings-AutoFilterCondition="BeginsWith">
                         <PropertiesTextEdit MaxLength="30">
                        </PropertiesTextEdit>
                        <EditFormSettings  VisibleIndex="1"  Visible="True" />
                    </dx:GridViewDataTextColumn>


                      <dx:GridViewDataTextColumn FieldName="PhoneHome" Caption="แฟกซ์"  Visible="false" CellStyle-Wrap="False" Width="100" Settings-AutoFilterCondition="BeginsWith">
                         <PropertiesTextEdit MaxLength="15">
                        </PropertiesTextEdit>
                        <EditFormSettings  VisibleIndex="1"  Visible="True"/>
                    </dx:GridViewDataTextColumn>

                      <dx:GridViewDataTextColumn FieldName="Email" Caption="อีเมล์" Visible="false" CellStyle-Wrap="False"  Width="100" Settings-AutoFilterCondition="BeginsWith">
                         <PropertiesTextEdit MaxLength="128">
                        </PropertiesTextEdit>
                        <EditFormSettings  VisibleIndex="1" Visible="True" />
                    </dx:GridViewDataTextColumn>




                    <dx:GridViewDataComboBoxColumn FieldName="BPShop" Caption="อู่"  Visible="false"  >
                        <PropertiesComboBox>
                            <Items>
                                <dx:ListEditItem Text=" " Value=" " />
                                <dx:ListEditItem Text="BPShop" Value="BPShop" />
                                <dx:ListEditItem Text="Non-BPShop" Value="Non-BPShop" />
                            </Items>
                        </PropertiesComboBox>
                         <EditFormSettings  VisibleIndex="4"  Visible="true"  />
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataTextColumn FieldName="BillingName" Caption="ชื่อผู้วางบิล" CellStyle-Wrap="False" Width="100" Visible="false">
                        <PropertiesTextEdit MaxLength="250">
                        </PropertiesTextEdit>
                        <EditFormSettings Visible="true" VisibleIndex="2" />
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="BillingAddress" Caption="ที่อยู่ที่วางบิล" CellStyle-Wrap="False" Width="100" Visible="false">
                        <PropertiesTextEdit MaxLength="255">
                        </PropertiesTextEdit>
                        <EditFormSettings Visible="true" VisibleIndex="3" />
                    </dx:GridViewDataTextColumn>



                   

                     <dx:GridViewDataTextColumn FieldName="Occupation" Caption="กลุ่ม" CellStyle-Wrap="False" Width="100" Settings-AutoFilterCondition="BeginsWith">
                         <PropertiesTextEdit MaxLength="30">
                        </PropertiesTextEdit>
                        <EditFormSettings  VisibleIndex="0" />
                         <Settings AllowHeaderFilter="True" HeaderFilterMode="CheckedList" />
                    </dx:GridViewDataTextColumn>

                     

                    <dx:GridViewDataCheckColumn FieldName="IsActive" UnboundType="Boolean" Caption="สถานะ" CellStyle-Wrap="False" Width="100">
                         <EditFormSettings  VisibleIndex="16" />
                        <Settings AllowHeaderFilter="True"   />
                    </dx:GridViewDataCheckColumn>


                    <dx:GridViewDataColumn FieldName="InternetAddress" Caption="หมายเหตุ" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                        <Settings AllowHeaderFilter="True" HeaderFilterMode="CheckedList" />
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
                                <dx:TabPage Text="Agent Details" Visible="true">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl3" runat="server">
                                            <table cellpadding="2" cellspacing="2" style="border-collapse: collapse; width: 300px">
                                                <tr>
                                                    <td style="font-weight: bold">Agent: </td>
                                                    <td colspan="3"><%# Eval("Agent")%></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold">Name: </td>
                                                    <td colspan="3"><%# Eval("Name")%></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold">Addressee: </td>
                                                    <td colspan="3"><%# Eval("Addressee")%></td>
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

                                                    <td style="font-weight: bold">City: </td>
                                                    <td colspan="3"><%# Eval("City")%></td>

                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold">ContactPerson: </td>
                                                    <td colspan="3"><%# Eval("ContactPerson")%></td>
                                                </tr>


                                                <tr>
                                                    <td style="font-weight: bold">PhoneBusiness: </td>
                                                    <td colspan="3"><%# Eval("PhoneBusiness")%></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold">PhoneHome: </td>
                                                    <td colspan="3"><%# Eval("PhoneHome")%></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold">Email: </td>
                                                    <td colspan="3"><%# Eval("Email")%></td>
                                                </tr>

                                                <tr>
                                                    <td style="font-weight: bold">BPShop: </td>
                                                    <td colspan="3"><%# Eval("BPShop")%> </td>
                                                </tr>
                                                 <tr>
                                                    <td style="font-weight: bold">BillingName: </td>
                                                    <td colspan="3"><%# Eval("BillingName")%> </td>
                                                </tr>

                                                <tr>
                                                    <td style="font-weight: bold">BillingAddress: </td>
                                                    <td colspan="3"><%# Eval("BillingAddress")%></td>
                                                </tr>

                                                <tr>
                                                    <td style="font-weight: bold">Salutation: </td>
                                                    <td colspan="3"><%# Eval("Salutation")%></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold">Occupation: </td>
                                                    <td colspan="3"><%# Eval("Occupation")%></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold">Remark: </td>
                                                    <td colspan="3"><%# Eval("InternetAddress")%></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-weight: bold">IsActive: </td>
                                                    <td colspan="3"><%# Eval("IsActive")%></td>
                                                </tr>
                                            </table>

                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>


                             <%--   <dx:TabPage Text="Billing Details" Visible="true">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl1" runat="server">
                                            <table cellpadding="2" cellspacing="2" style="border-collapse: collapse; width: 300px">




                                               

                                            </table>

                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>--%>

                            </TabPages>
                        </dx:ASPxPageControl>
                    </DetailRow>
                </Templates>


            </dx:ASPxGridView>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>


<asp:SqlDataSource ID="SqlDataSource_Agent" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select 
        rtrim(Agent) as Agent
        ,rtrim(Name) as Name
        ,rtrim(Address1) as Address1
        ,rtrim(Address2) as Address2
        ,rtrim(Address3) as Address3
        ,rtrim(PostCode) as PostCode
        ,rtrim(City) as City
        ,rtrim(PhoneBusiness) as PhoneBusiness
        ,rtrim(PhoneHome) as PhoneHome
        ,rtrim(ContactPerson) as ContactPerson
        ,rtrim(Addressee) as Addressee
        ,rtrim(Salutation) as Salutation
        ,rtrim(Occupation) as Occupation
        ,rtrim(EntryBy) as EntryBy
        ,rtrim(EntryDate) as EntryDate
        ,rtrim(IsSubAgent) as IsSubAgent
        ,rtrim(InternetAddress) as InternetAddress
        ,rtrim(CreationDate) as CreationDate
        ,rtrim(CreationBy) as CreationBy
        ,rtrim(RequestDate) as RequestDate
        ,rtrim(RequestBy) as RequestBy
        ,rtrim(ApproveDate) as ApproveDate
        ,rtrim(ApproveBy) as ApproveBy
        ,rtrim(UserName) as UserName
        ,rtrim(Password) as Password
        ,rtrim(Email) as Email
        ,IsActive
        ,rtrim(BPShop) as BPShop
        ,rtrim(BillingName) as BillingName
        ,rtrim(BillingAddress) as BillingAddress
     from [Register.Agent]"
    DeleteCommand="delete from [Register.Agent] where Agent=@Agent"
    UpdateCommand="update [Register.Agent] set  
     Addressee=@Addressee
    ,Address1=@Address1
    ,Address2=@Address2
    ,Address3=@Address3
    ,City=@City
    ,PhoneBusiness=@PhoneBusiness
    ,PhoneHome=@PhoneHome
    ,ContactPerson=@ContactPerson   
    ,Email=@Email
    ,IsActive=@IsActive
    ,BPShop=@BPShop
    ,BillingName=@BillingName
    ,BillingAddress=@BillingAddress
    ,Occupation=@Occupation
     where  Agent=@Agent ">
    <DeleteParameters>
        <asp:Parameter Name="Agent" Type="String" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="Agent" Type="String" />
        <asp:Parameter Name="Address1" Type="String" />
        <asp:Parameter Name="Address2" Type="String" />
        <asp:Parameter Name="Address3" Type="String" />
        <asp:Parameter Name="City" Type="String" />
        <asp:Parameter Name="PhoneBusiness" Type="String" />
        <asp:Parameter Name="PhoneHome" Type="String" />
        <asp:Parameter Name="ContactPerson" Type="String" />
        <asp:Parameter Name="Addressee" Type="String" />
        <asp:Parameter Name="Email" Type="String" />
        <asp:Parameter Name="IsActive" Type="Boolean" />
        <asp:Parameter Name="BPShop" Type="String" />
        <asp:Parameter Name="BillingName" Type="String" />
        <asp:Parameter Name="BillingAddress" Type="String" />
        <asp:Parameter Name="Occupation" Type="String" />
    </UpdateParameters>


</asp:SqlDataSource>





<dx:ASPxPopupControl ID="popUpAddAgent"
    ClientInstanceName="popUpAddAgent"
    runat="server" Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="Agent"
    AllowDragging="true"
    EnableAnimation="true" CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="true" Width="100px">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">

            <dx:ASPxFormLayout ID="frmNewAgent" runat="server" AlignItemCaptionsInAllGroups="True" ColCount="1" RequiredMarkDisplayMode="None" ShowItemCaptionColon="False">
                <Items>

                    <dx:LayoutItem Caption="Agent">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer
                                ID="LayoutItemNestedControlContainer9"
                                runat="server"
                                SupportsDisabledAttribute="True">
                                <dx:ASPxGridLookup ID="GridAgentLookup" runat="server"
                                    SelectionMode="Single"
                                    DataSourceID="SqlDataSource_AgentLookup"
                                    ClientInstanceName="GridAgentLookup"
                                    KeyFieldName="Agent" Width="100"
                                    TextFormatString="{0}">
                                    <GridViewProperties>
                                        <SettingsBehavior EnableRowHotTrack="true" />
                                        <SettingsSearchPanel Visible="true" />
                                    </GridViewProperties>
                                    <ClearButton Visibility="True"></ClearButton>
                                    <ValidationSettings>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="Agent" Width="50" Settings-AutoFilterCondition="BeginsWith" />
                                        <dx:GridViewDataColumn FieldName="Name" CellStyle-Wrap="False" Settings-AutoFilterCondition="Contains" />
                                        <dx:GridViewDataColumn FieldName="Addressee" CellStyle-Wrap="False" Settings-AutoFilterCondition="Contains" />
                                        <dx:GridViewDataColumn FieldName="ContactPerson" CellStyle-Wrap="False" Settings-AutoFilterCondition="Contains" />
                                        <dx:GridViewDataColumn FieldName="Salutation" CellStyle-Wrap="False" Settings-AutoFilterCondition="Contains" />
                                        <dx:GridViewDataColumn FieldName="Occupation" CellStyle-Wrap="False" Settings-AutoFilterCondition="Contains" />
                                        <dx:GridViewDataColumn FieldName="InternetAddress" CellStyle-Wrap="False" Settings-AutoFilterCondition="Contains" />

                                    </Columns>
                                </dx:ASPxGridLookup>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>


                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxButton ID="btnSaveAgent" runat="server" ValidationContainerID="frmNewAgent"
                                    Text="Save" Width="100">
                                    <ClientSideEvents Click="function(s, e) {
                                        if(ASPxClientEdit.AreEditorsValid()) 
                                        {                                              
                                            LoadingPanel.Show();
                                            cbSaveAddAgent.PerformCallback('');
                                        }
                                        else
                                        {
                                            alert('กรุณากรอกข้อมูลให้ครบ');
                                        }
                                        e.processOnServer = false;
                                    }" />
                                </dx:ASPxButton>
                                <dx:ASPxCallback runat="server" ID="cbSaveAddAgent" ClientInstanceName="cbSaveAddAgent">
                                    <ClientSideEvents CallbackComplete="function(s,e){ 
                                             LoadingPanel.Hide();   
                                             if (e.result != 'success') {
                                                alert(e.result);                                                
                                             } 
                                             else
                                             {
                                                gridAgent.Refresh();
                                                popUpAddAgent.Hide();
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


<asp:SqlDataSource ID="SqlDataSource_AgentLookup" runat="server" ConnectionString="<%$ ConnectionStrings:SIBISDBConnectionString %>"
    SelectCommand="SELECT rtrim(Agent) as Agent, rtrim(Name) as Name , rtrim(Addressee) as Addressee,ContactPerson,Salutation,Occupation,InternetAddress FROM Agents Where Occupation not like 'LAPSE' order by Name"></asp:SqlDataSource>


