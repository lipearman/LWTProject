<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxNoticeMailContactSetup_B0025.ascx.vb" Inherits="Modules_ucDevxNoticeMailContactSetup_B0025" %>


<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Mail Contract" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>


            <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server">
                <Items>
                    <dx:LayoutGroup Caption="Email">
                        <Items>
                            <dx:LayoutItem Caption="Subject">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox ID="MailSubject" Width="600" ValidationSettings-RequiredField-IsRequired="true"
                                            runat="server" />
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="From">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox ID="MailFrom" Width="600"
                                            runat="server" />
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <%--                                                <dx:LayoutItem Caption="To">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox ID="MailTo" Width="600"
                                                                runat="server" />
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>--%>
                            <dx:LayoutItem Caption="CC">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox ID="MailCC" Width="600"
                                            runat="server" />
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <%--                                                <dx:LayoutItem Caption="BCC">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox ID="MailBcc" Width="600"
                                                                runat="server" />
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>--%>
                            <dx:LayoutItem Caption="Body">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxHtmlEditor ID="MailBody" runat="server" ValidationSettings-RequiredField-IsRequired="true">
                                        </dx:ASPxHtmlEditor>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption=" ">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Save" AutoPostBack="false"
                                            ValidationContainerID="ASPxFormLayout1">
                                            <ClientSideEvents Click="function(s, e) {
                                                                    if(ASPxClientEdit.AreEditorsValid()) 
                                                                    {                                              
                                                                        LoadingPanel.Show();
                                                                        cbSaveEmail.PerformCallback('');
                                                                    }
                                                                    else
                                                                    {
                                                                        alert('กรุณากรอกข้อมูลให้ครบ');
                                                                    }
                                                                    e.processOnServer = false;
                                                                }" />


                                        </dx:ASPxButton>
                                        <dx:ASPxCallback runat="server" ID="cbSaveEmail" ClientInstanceName="cbSaveEmail">
                                            <ClientSideEvents
                                                CallbackError="function(s,e){LoadingPanel.Hide(); }"
                                                CallbackComplete="function(s,e){ 
                                                                     LoadingPanel.Hide();   
                                                                     if (e.result != 'success') {
                                                                        alert(e.result);                                                
                                                                     }                                                                     
                                                                     e.processOnServer = false;
                                                                }" />
                                        </dx:ASPxCallback>



                                        <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Test Send Mail" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s, e) {
                                                                    LoadingPanel.Show();
                                                                    cbTestSendEmail.PerformCallback('');
                                                                    e.processOnServer = false;
                                                                }" />


                                        </dx:ASPxButton>
                                        <dx:ASPxCallback runat="server" ID="cbTestSendEmail" ClientInstanceName="cbTestSendEmail">
                                            <ClientSideEvents
                                                CallbackError="function(s,e){LoadingPanel.Hide(); }"
                                                CallbackComplete="function(s,e){ 
                                                                     LoadingPanel.Hide();   
                                                                     if (e.result == 'success') {
                                                                        alert('Send');                                                
                                                                     }                                                                     
                                                                     e.processOnServer = false;
                                                                }" />
                                        </dx:ASPxCallback>


                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>



                        </Items>
                    </dx:LayoutGroup>
                </Items>
            </dx:ASPxFormLayout>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>


