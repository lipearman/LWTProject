<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxAEAssignmentSetup.ascx.vb" Inherits="Modules_ucDevxAEAssignmentSetup" %>


<script>
 

    function SetAEUser(result) {
        AECodeList.UnselectAll();

        var AElist = result.split(';');
        var index;
        var indices = [];

        for (i = 0; i < AECodeList.items.length; i++) {
            for (index = 0; index < AElist.length; index++) {
                if (AECodeList.items[i].value == AElist[index]) {
                    indices.push(i);
                }
            }
        }

        AECodeList.SelectIndices(indices);
    }


    function OnFocusedCardChanged() {
        LoadingPanel.Show();
        cardView.GetCardValues(cardView.GetFocusedCardIndex(), 'sAMAccountName', OnGetCardValues);
    }
    function OnGetCardValues(values) {
       
        lbUserName.SetText(values)
        lbUserImage.SetImageUrl('http://lockthbnkedc01/sites/HRDweb/LWT%20Member%20Photos/' + values + '.jpg');
        cbOpenPopup.PerformCallback(values);
    }

</script>
<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="User" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>



            <dx:ASPxCardView ID="cardView" ClientInstanceName="cardView" runat="server" DataSourceID="SqlDataSource_User"
                Width="1000" KeyFieldName="sAMAccountName">
                <ClientSideEvents FocusedCardChanged="OnFocusedCardChanged" />
                <SettingsBehavior AllowFocusedCard="true" />
                <SettingsPager>
                    <SettingsTableLayout ColumnCount="4" RowsPerPage="1" />
                </SettingsPager>
                <SettingsSearchPanel Visible="true" />
                <%--<Settings ShowCustomizationPanel="false" />--%>

                <Columns>
                    <dx:CardViewColumn FieldName="sAMAccountName" />
                    <dx:CardViewColumn FieldName="displayName" />
                    <dx:CardViewColumn FieldName="department" />
                </Columns>

                <CardLayoutProperties ColCount="1" >
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




            <dx:ASPxFormLayout ID="frmdata" Width="100px" runat="server" ColCount="1">
                <Items>
                    <dx:LayoutGroup Caption="Details">
                        <Items>
                            <dx:LayoutItem Caption="" ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer
                                        ID="LayoutItemNestedControlContainer9"
                                        runat="server"
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxImage ID="lbUserImage" ClientInstanceName="lbUserImage" runat="server" Cursor="pointer"></dx:ASPxImage>
                                        <br />
                                        <dx:ASPxLabel ID="lbUserName" ClientInstanceName="lbUserName" runat="server" Text=""></dx:ASPxLabel>

                                        <hr />
                                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Select All">
                                            <ClientSideEvents Click="function(s, e) { AECodeList.SelectAll(); e.processOnServer = false;}" />

                                        </dx:ASPxButton>
                                        <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Unselect All">
                                            <ClientSideEvents Click="function(s, e) { AECodeList.UnselectAll(); e.processOnServer = false;}" />
                                        </dx:ASPxButton>

                                           <dx:ASPxButton ID="btnSaveAE" runat="server"
                                            Text="Save" Width="100">
                                            <ClientSideEvents Click="function(s, e) {
                                        LoadingPanel.Show();
                                        cbSaveAddAE.PerformCallback(lbUserName.GetText() +':'+ AECodeList.GetSelectedIndices());
                                        e.processOnServer = false;
                                    }" />
                                        </dx:ASPxButton>
                                        <dx:ASPxCallback runat="server" ID="cbSaveAddAE" ClientInstanceName="cbSaveAddAE">
                                            <ClientSideEvents CallbackComplete="function(s,e){ 
                                             LoadingPanel.Hide();   
                                             if (e.result != 'success') {
                                                alert(e.result);                                                
                                             } 
                                             else
                                             {
                                                gridUser.Refresh(); 
                                             }
                                        }" />
                                        </dx:ASPxCallback>



                                        <dx:ASPxCheckBoxList ID="AECodeList" ClientInstanceName="AECodeList" runat="server" DataSourceID="SqlDataSource_AE"
                                            CheckBoxStyle-Wrap="False" TextWrap="false"
                                            ValueField="AccountExecutive" TextField="Name" RepeatColumns="4" RepeatLayout="Table"
                                            Caption="Select the AECode ​​you've worked with">
                                            <CaptionSettings Position="Top" />
                                            <%--                               <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                             var selectedItemsCount = s.GetSelectedItems().length;
                                             cbAll.SetChecked(selectedItemsCount == s.GetItemCount());
                                        }" />--%>
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
</dx:ASPxRoundPanel>


<asp:SqlDataSource ID="SqlDataSource_User" runat="server" ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
    SelectCommand="select department,sAMAccountName,displayName,title from V_LWT_USER"></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_AEUser" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select a.UserName, rtrim(b.AccountExecutive) as AECode, rtrim(b.AccountExecutive) + ' - ' + rtrim(b.Name) as Name 
    from tblProjectUserAssignment a 
    inner join [Register.AccountExecutive] b on a.AECode = b.AccountExecutive   
    where a.UserName=@UserName ">
    <SelectParameters>
        <asp:SessionParameter Name="UserName" SessionField="AEUser" />
    </SelectParameters>
</asp:SqlDataSource>



<dx:ASPxCallback runat="server" ID="cbOpenPopup" ClientInstanceName="cbOpenPopup">
    <ClientSideEvents CallbackComplete="function(s,e){                                                                                     
                LoadingPanel.Hide(); 
                SetAEUser(e.result);    
            }" />
</dx:ASPxCallback>




<asp:SqlDataSource ID="SqlDataSource_AE" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select rtrim(AccountExecutive) as AccountExecutive, rtrim(AccountExecutive) + ' - ' + rtrim(Name) as Name from [dbo].[Register.AccountExecutive] order by AccountExecutive"></asp:SqlDataSource>



