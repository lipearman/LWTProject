<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxMotorCampaignAssignment.ascx.vb" Inherits="Modules_ucDevxMotorCampaignAssignment" %>
<script>
    function OnFocusedCardChanged() {
        LoadingPanel.Show();
        cardView.GetCardValues(cardView.GetFocusedCardIndex(), 'CampaignID;CampaignName', OnGetCardValues);
    }
    function OnGetCardValues(values) {
        LoadingPanel.Show();
        cbCampaign.PerformCallback(values[0]);

    }



    //===========================
    function SetProject(result) {
        ProjectList.UnselectAll();

        var plist = result.split(';');
        var index;
        var indices = [];

        for (i = 0; i < ProjectList.items.length; i++) {
            for (index = 0; index < plist.length; index++) {
                if (ProjectList.items[i].value == plist[index]) {
                    indices.push(i);
                }
            }
        }

        ProjectList.SelectIndices(indices);
    }



</script>
<dx:ASPxCallback runat="server" ID="cbCampaign" ClientInstanceName="cbCampaign">
    <ClientSideEvents CallbackComplete="function(s,e){                                                                                     
                LoadingPanel.Hide(); 
                SetProject(e.result);
            }" />
</dx:ASPxCallback>



<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Motor Campaign Assignment" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>


            <dx:ASPxCallbackPanel ID="callbackPanel"
                ClientInstanceName="callbackPanel" runat="server"
                RenderMode="Table">

                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server">


                        <%--          <dx:ASPxCardView ID="cardView" ClientInstanceName="cardView" runat="server" DataSourceID="SqlDataSource_MotorCampaign"
                KeyFieldName="CampaignID" Width="100%">
                <ClientSideEvents FocusedCardChanged="OnFocusedCardChanged" />
                <Settings ShowHeaderFilterButton="true" />
                <SettingsBehavior AllowFocusedCard="true" />
                <SettingsPager>
                    <SettingsTableLayout ColumnCount="4" RowsPerPage="1" />
                </SettingsPager>
                <SettingsSearchPanel Visible="true" />
                <Settings ShowCustomizationPanel="true" />
                <SettingsText CommandCustomizationButton="เรียง/ค้นหา" CustomizationWindowCaption="เรียง/ค้นหา" />

                <Columns>

                    <dx:CardViewColumn FieldName="CampaignID" Visible="false" />
                    <dx:CardViewColumn FieldName="CampaignName" Caption="Name" />
                    <dx:CardViewColumn FieldName="CampaignCode" Caption="Code" />
                    <dx:CardViewColumn FieldName="CampaignDescription" Caption="Description" />
                    <dx:CardViewComboBoxColumn FieldName="RenewalYear" Caption="Year">
                        <PropertiesComboBox ValidationSettings-RequiredField-IsRequired="true">
                            <Items>
                                <dx:ListEditItem Text="ไม่ระบุ" Value="0" />
                                <dx:ListEditItem Text="ปีที่ 1" Value="1" />
                                <dx:ListEditItem Text="ปีที่ 2" Value="2" />
                                <dx:ListEditItem Text="ปีที่ 3" Value="3" />
                                <dx:ListEditItem Text="ปีที่ 4" Value="4" />
                                <dx:ListEditItem Text="ปีที่ 5" Value="5" />
                                <dx:ListEditItem Text="ปีที่ 6" Value="6" />
                                <dx:ListEditItem Text="ปีที่ 7" Value="7" />
                                <dx:ListEditItem Text="ปีที่ 8" Value="8" />
                                <dx:ListEditItem Text="ปีที่ 9" Value="9" />
                                <dx:ListEditItem Text="ปีที่ 10" Value="10" />
                            </Items>
                        </PropertiesComboBox>
                    </dx:CardViewComboBoxColumn>

                   <dx:CardViewComboBoxColumn FieldName="CampaignType">
                        <PropertiesComboBox ValidationSettings-RequiredField-IsRequired="true">
                            <Items>
                                <dx:ListEditItem Text="ภาคสมัครใจ + พรบ." Value="1" />
                                <dx:ListEditItem Text="ภาคสมัครใจ" Value="2" />
                            </Items>
                        </PropertiesComboBox>
                    </dx:CardViewComboBoxColumn>
                    <dx:CardViewComboBoxColumn FieldName="HasPremium">
                        <PropertiesComboBox ValidationSettings-RequiredField-IsRequired="true">
                            <Items>
                                <dx:ListEditItem Text="มีเบี้ย" Value="True" />
                                <dx:ListEditItem Text="ไม่มีเบี้ย" Value="False" />
                            </Items>

                        </PropertiesComboBox>
                    </dx:CardViewComboBoxColumn>



                    <dx:CardViewColumn FieldName="EffectiveDate" />
                    <dx:CardViewColumn FieldName="ExpiryDate" />
                </Columns>
                <CardLayoutProperties>
                    <Items>
                        <dx:CardViewColumnLayoutItem ColumnName="CampaignName" />
                        <dx:CardViewColumnLayoutItem ColumnName="CampaignCode" />
                        <dx:CardViewColumnLayoutItem ColumnName="CampaignDescription" Caption="Description" />
                        <dx:CardViewColumnLayoutItem ColumnName="RenewalYear" Caption="Year" />
                        <dx:CardViewColumnLayoutItem ColumnName="EffectiveDate" />
                        <dx:CardViewColumnLayoutItem ColumnName="ExpiryDate" />
                    </Items>
                </CardLayoutProperties>
            </dx:ASPxCardView>--%>

                        <dx:ASPxGridLookup ID="gridCampaign" ClientInstanceName="gridCampaign" runat="server" SelectionMode="Single"
                            DataSourceID="SqlDataSource_MotorCampaign" Width="300" Caption="Campaign"
                            KeyFieldName="CampaignID" TextFormatString="{1} - {2}">
                            <ClientSideEvents ValueChanged="function (s, e){
                                         cbCampaign.PerformCallback('');
                                         }" />
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                            <GridViewProperties>

                                <Settings ShowHeaderFilterButton="true" />
                                <SettingsSearchPanel Visible="true" />


                            </GridViewProperties>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="CampaignID" Visible="false" />
                                <dx:GridViewDataColumn FieldName="CampaignCode" Width="50" CellStyle-Wrap="False" />
                                <dx:GridViewDataColumn FieldName="CampaignName" CellStyle-Wrap="False" />

                                <dx:GridViewDataComboBoxColumn FieldName="RenewalYear" CellStyle-Wrap="False">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dx:ListEditItem Text="ไม่ระบุ" Value="0" />
                                            <dx:ListEditItem Text="ปีที่ 1" Value="1" />
                                            <dx:ListEditItem Text="ปีที่ 2" Value="2" />
                                            <dx:ListEditItem Text="ปีที่ 3" Value="3" />
                                            <dx:ListEditItem Text="ปีที่ 4" Value="4" />
                                            <dx:ListEditItem Text="ปีที่ 5" Value="5" />
                                            <dx:ListEditItem Text="ปีที่ 6" Value="6" />
                                            <dx:ListEditItem Text="ปีที่ 7" Value="7" />
                                            <dx:ListEditItem Text="ปีที่ 8" Value="8" />
                                            <dx:ListEditItem Text="ปีที่ 9" Value="9" />
                                            <dx:ListEditItem Text="ปีที่ 10" Value="10" />

                                        </Items>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>

                                <dx:GridViewDataColumn FieldName="EffectiveDate" CellStyle-Wrap="False" />
                                <dx:GridViewDataColumn FieldName="ExpiryDate" CellStyle-Wrap="False" />
                                <dx:GridViewDataColumn FieldName="Insurer" CellStyle-Wrap="False" />
                            </Columns>

                        </dx:ASPxGridLookup>


                        <asp:SqlDataSource ID="SqlDataSource_MotorCampaign" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
                            SelectCommand="select * 
                            , (select count(*) from (select distinct Uwriter from dbo.[tblCampaign.CommIn] where tblCampaign.CampaignID = [tblCampaign.CommIn].campaignID) a ) as Insurer
                            from tblCampaign where RiskGroupID=@RiskGroupID and IsActive=1 ">
                            <SelectParameters>
                                <asp:Parameter Name="RiskGroupID" />
                            </SelectParameters>
                        </asp:SqlDataSource>



                        <dx:ASPxFormLayout ID="frmdata" Width="100px" runat="server" ColCount="1" >
                            <Items>
                                <dx:LayoutGroup Caption="Projects">
                                    <Items>
                                        <dx:LayoutItem Caption="" ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer
                                                    ID="LayoutItemNestedControlContainer9"
                                                    runat="server"
                                                    SupportsDisabledAttribute="True">

                                                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Select All">
                                                        <ClientSideEvents Click="function(s, e) { ProjectList.SelectAll(); e.processOnServer = false;}" />

                                                    </dx:ASPxButton>
                                                    <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Unselect All">
                                                        <ClientSideEvents Click="function(s, e) { ProjectList.UnselectAll(); e.processOnServer = false;}" />
                                                    </dx:ASPxButton>


                                                    <dx:ASPxButton ID="btnSaveProject" runat="server" AutoPostBack="false"
                                                        Text="Save" Width="100">
                                                        <ClientSideEvents Click="function(s, e) {

                                                            if(ASPxClientEdit.AreEditorsValid()) {
                                                                LoadingPanel.Show();
                                                                cbSaveAddProject.PerformCallback(ProjectList.GetSelectedIndices());
                                                            }
                                                                e.processOnServer = false;
                                                }" />
                                                    </dx:ASPxButton>



                                                    <dx:ASPxCallback runat="server" ID="cbSaveAddProject" ClientInstanceName="cbSaveAddProject">
                                                        <ClientSideEvents CallbackComplete="function(s,e){ 
                                             LoadingPanel.Hide();   
                                             if (e.result != 'success') {
                                                alert(e.result);                                                
                                             }                                               
                                        }" />
                                                    </dx:ASPxCallback>



                                                    <dx:ASPxCheckBoxList ID="ProjectList" ClientInstanceName="ProjectList" runat="server" 
                                                        DataSourceID="SqlDataSource_Project"
                                                        CheckBoxStyle-Wrap="False" TextWrap="false"
                                                        ValueField="ProjectID" TextField="ProjectName" 
                                                        RepeatColumns="4" 
                                                        RepeatLayout="Table"
                                                        Caption="Select the Project ​​you've worked with">
                                                        <CaptionSettings Position="Top" />
                                                    </dx:ASPxCheckBoxList>






                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                    </Items>

                                </dx:LayoutGroup>

                            </Items>
                            <Styles LayoutItem-Caption-Font-Bold="true" />
                        </dx:ASPxFormLayout>



                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>



        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>



<%--<asp:SqlDataSource ID="SqlDataSource_MotorCampaign" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * from tblCampaign where RiskGroupID=@RiskGroupID ">
    <SelectParameters>
        <asp:Parameter Name="RiskGroupID" />
    </SelectParameters>
</asp:SqlDataSource>--%>


<asp:SqlDataSource ID="SqlDataSource_Project" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand=" SELECT * FROM V_UserProject where UserName=@UserName ">
    <SelectParameters>
        <asp:SessionParameter Name="UserName" SessionField="UserName" />
    </SelectParameters>
</asp:SqlDataSource>
