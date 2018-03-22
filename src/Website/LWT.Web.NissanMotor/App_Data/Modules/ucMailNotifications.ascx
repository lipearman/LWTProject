<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucMailNotifications.ascx.vb" Inherits="Modules_ucMailNotifications" %>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Report" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="SqlDataSource_MailNotifications"
                KeyFieldName="ID" AutoGenerateColumns="False">
                <Columns>
                    <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowEditButton="true" />
                    <dx:GridViewDataTextColumn FieldName="Code">
                        <EditFormSettings Visible="True" VisibleIndex="0" />
                        <PropertiesTextEdit>
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Name">
                        <EditFormSettings Visible="True" VisibleIndex="1" />
                        <PropertiesTextEdit>
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataCheckColumn FieldName="IsActive">
                        <EditFormSettings Visible="True" VisibleIndex="2" />
                    </dx:GridViewDataCheckColumn>

                    <dx:GridViewDataTextColumn FieldName="MailSubject" Visible="false">
                        <EditFormSettings Visible="True" VisibleIndex="3" />
                        <PropertiesTextEdit>
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="MailFrom" Visible="false">
                        <EditFormSettings Visible="True" VisibleIndex="4" />
                        <PropertiesTextEdit>
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="MailTo" Visible="false">
                        <EditFormSettings Visible="True" VisibleIndex="5" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="MailCC" Visible="false">
                        <EditFormSettings Visible="True" VisibleIndex="6" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="MailBcc" Visible="false">
                        <EditFormSettings Visible="True" VisibleIndex="7" />
                    </dx:GridViewDataTextColumn>


                    <%--                     <dx:GridViewDataTextColumn FieldName="MailBody" Visible="False" VisibleIndex="8">
                                        <EditFormSettings Visible="True" />
                                        <EditItemTemplate>
                                            <dx:ASPxHtmlEditor ID="MailBody" runat="server" 
                                                Html='<%# Server.HtmlDecode(Eval("MailBody"))%>'>
                                            </dx:ASPxHtmlEditor>
                                        </EditItemTemplate>
                                    </dx:GridViewDataTextColumn>--%>

                    <dx:GridViewDataTextColumn FieldName="CreationDate">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ModifiedDate">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>

                </Columns>
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing EditFormColumnCount="1">
                </SettingsEditing>
                <SettingsCommandButton RenderMode="Button">
                    <UpdateButton Text="Save"></UpdateButton>
                </SettingsCommandButton>
                <Templates>
                    <EditForm>
                        <dx:ASPxGridViewTemplateReplacement ID="Editors" ReplacementType="EditFormEditors" runat="server"></dx:ASPxGridViewTemplateReplacement>
                        <br />
                        <dx:ASPxHtmlEditor ID="MailBody" runat="server" Html='<%# Server.HtmlDecode(Eval("MailBody"))%>'></dx:ASPxHtmlEditor>


                        <br />
                        <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                            runat="server"></dx:ASPxGridViewTemplateReplacement>

                        <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                            runat="server"></dx:ASPxGridViewTemplateReplacement>
                        <br />
                        <br />
                        <hr />
                    </EditForm>
                </Templates>
            </dx:ASPxGridView>





        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>



<asp:SqlDataSource ID="SqlDataSource_MailNotifications" runat="server" ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
    SelectCommand="select ID
                        ,Code
                        ,Name
                        ,MailFrom
                        ,MailTo
                        ,MailCC
                        ,MailBcc
                        ,MailSubject
                        ,isnull(MailBody,'') MailBody
                        ,IsActive
                        ,CreationDate
                        ,ModifiedDate 
                    from MailNotifications 
                    order by Code 
    
    "></asp:SqlDataSource>
