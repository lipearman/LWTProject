<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxNoticeHeaderSetup_B0020.ascx.vb" Inherits="Modules_ucDevxNoticeHeaderSetup_B0020" %>
 <style>
    .x-grid-row-summary .x-grid-cell-inner {
        font-weight: bold;
        color: red;
    }
</style>
<f:Panel ID="MainRegionPanel" runat="server" BodyPadding="5px" ShowBorder="false"
    ShowHeader="false" Title="Projects" EnableCollapse="true">
    <Toolbars>
        <f:Toolbar ID="Toolbar9" runat="server">
            <Items>
                <f:Button ID="btnAdd" Text="Create Notification" runat="server" CssClass="inline" Icon="Add" ConfirmText="Create Notification?">
                </f:Button>
                <f:Button ID="btnDeleteAll" Text="Delete" runat="server" CssClass="inline" Icon="Delete" ConfirmText="Confirm Delete?">
                </f:Button>
                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                </f:ToolbarFill>
                <f:Button ID="btnRefresh" Text="รีเฟรชข้อมูล" runat="server" Icon="Reload">
                </f:Button>
            </Items>
        </f:Toolbar>
    </Toolbars>


    <Items>

        <f:Grid ID="Grid1" Title="Notification" ShowBorder="true" ShowHeader="true" SortField="CreationDate" SortDirection="DESC" Icon="Application" PageSize="20"
            AllowSorting="true" runat="server" AutoScroll="true" DataKeyNames="NoticeID" EmptyText="No Data" AllowPaging="true"
            EnableRowDoubleClickEvent="true"
            EnableCheckBoxSelect="true">
            <Columns>

                <%--                <f:LinkButtonField CommandName="btnAddData" Width="60px" Icon="Mail" ToolTip="Data" TextAlign="Center" HeaderText="data" />--%>
                <f:BoundField ColumnID="NoticeID" Width="120" SortField="NoticeID" DataField="NoticeID" HeaderText="NotificationID" TextAlign="Left" />
                <f:BoundField ColumnID="NoticeTitle" Width="200" SortField="NoticeTitle" DataField="NoticeTitle" HeaderText="Title" TextAlign="Left" />
                  <f:BoundField ColumnID="CreationBy" Width="160" SortField="CreationBy" DataField="CreationBy" HeaderText="CreateBy" TextAlign="Left" />

                <f:BoundField ColumnID="CreationDate" Width="160" SortField="CreationDate" DataField="CreationDate" HeaderText="CreationDate" TextAlign="Left" />
                <f:BoundField ColumnID="SendDate" Width="120" SortField="SendDate" DataField="SendDate" HeaderText="SendDate" TextAlign="Left" />
                <f:BoundField ColumnID="ReSendDate" Width="120" SortField="ReSendDate" DataField="ReSendDate" HeaderText="ReSendDate" TextAlign="Left" />




                <%--                <f:TemplateField ColumnID="expander" RenderAsRowExpander="true">
                    <ItemTemplate>
                        <div class="expander">
                            <p>
                                <strong>Fields：</strong><%# Eval("DataFields")%>
                            </p>
                        </div>
                    </ItemTemplate>
                </f:TemplateField>--%>
            </Columns>
        </f:Grid>


        <f:Panel ID="pnNotification" runat="server" ShowBorder="false" Layout="HBox" BoxConfigAlign="Stretch" Hidden="true"
            BoxConfigPosition="Start" BodyPadding="5px" BoxConfigChildMargin="0 5 0 0" Width="1000"
            ShowHeader="false">
            <Items>
                <f:Form runat="server" Icon="Application" ID="Form1" Width="900" ShowHeader="true" ShowBorder="true" BodyPadding="5" Title="Notification">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server" Position="Top">
                            <Items>
                                <f:HiddenField runat="server" ID="hdNotificationID"></f:HiddenField>
                                <f:Button ID="btnBack" Text="Back" runat="server" CssClass="inline" Icon="PageBack">
                                </f:Button>
                                <f:Button ID="btnSend" Text="Send" runat="server" ValidateForms="Form1" CssClass="inline" Icon="ApplicationGo" ConfirmText="Send Mail?">
                                </f:Button>
                                <f:Button ID="btnResend" Text="ReSend" runat="server" Hidden="true" ValidateForms="Form1" CssClass="inline" Icon="ArrowRedo" ConfirmText="ReSend Mail?">
                                </f:Button>



                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Rows>

                        <f:FormRow runat="server" ID="FormRow1">
                            <Items>
                                <f:Label runat="server" ID="lbNotificationTitle" Label="Title"></f:Label>
                                <f:Label runat="server" ID="lbCreationDate" Label="CreationDate"></f:Label>
                            </Items>
                        </f:FormRow>

                        <f:FormRow runat="server" ID="FormRow10">
                            <Items>
                                <f:Label runat="server" ID="lbSendDate" Label="SendDate"></f:Label>
                                <f:Label runat="server" ID="lbReSendDate" Label="ReSendDate"></f:Label>
                            </Items>
                        </f:FormRow>



                        <f:FormRow runat="server" ID="FormRow8">

                            <Items>
                                <f:TabStrip ID="TabStrip1" ShowBorder="true" TabPosition="Top"
                                    EnableTabCloseMenu="false" ActiveTabIndex="0"
                                    runat="server">
                                    <Tabs>

                                        <f:Tab ID="Tab1" Title="Data" CssClass="formitem"
                                            runat="server">
                                            <Items>


                                                <f:Grid ID="Grid3" Title="Agents" Icon="Application" EmptyText="No Data"
                                                    EnableRowDoubleClickEvent="true"
                                                    ShowBorder="true" ShowHeader="true"
                                                    runat="server"
                                                    SortField="AgentCode"
                                                    AllowSorting="true"
                                                    DataKeyNames="AgentCode,AgentName"
                                                    EnableCheckBoxSelect="true">

                                                    <Toolbars>
                                                        <f:Toolbar ID="Toolbar2" runat="server">
                                                            <Items>
                                                                <f:Button ID="btnUploadData" Text="Upload Data" runat="server" CssClass="inline" Icon="DiskUpload">
                                                                </f:Button>
                                                                <f:Button ID="btnDeleteData" Text="Delete" runat="server" CssClass="inline" Icon="Delete" ConfirmText="Confirm Delete?">
                                                                </f:Button>
                                                            </Items>
                                                        </f:Toolbar>
                                                    </Toolbars>

                                                    <Columns>
                                                        <f:RowNumberField HeaderText="No." Width="60" EnablePagingNumber="true" />
                                                        <f:BoundField SortField="AgentCode" ColumnID="AgentCode" DataField="AgentCode" HeaderText="Code" Width="80" TextAlign="Left" />
                                                        <f:BoundField SortField="AgentName" ColumnID="AgentName" DataField="AgentName" Width="280" HeaderText="AgentName" TextAlign="Left" />

                                                        <f:BoundField SortField="WH" DataFormatString="{0:F}" ColumnID="WH" DataField="WH" HeaderText="WH" Width="80" TextAlign="right" />


                                                        <f:BoundField SortField="Records" Width="100px" ColumnID="Records" DataField="Records" HeaderText="Records" TextAlign="center" />


                                                        <f:LinkButtonField CommandName="btnSend" Width="60px" ToolTip="Data" TextAlign="Center" HeaderText="Send" Icon="Mail" ConfirmText="Send Mail?" />


                                                        <f:TemplateField ColumnID="expander" RenderAsRowExpander="true">
                                                            <ItemTemplate>
                                                                <div class="expander">
                                                                    <p>
                                                                        <strong>Mailto：</strong><%# Eval("Mailto")%>
                                                                    </p>
                                                                </div>
                                                            </ItemTemplate>
                                                        </f:TemplateField>


                                                    </Columns>
                                                </f:Grid>


                                            </Items>
                                        </f:Tab>


                                        <f:Tab ID="Tab2" Title="Error Data" CssClass="formitem"
                                            runat="server">
                                            <Items>


                                                <f:Grid ID="Grid4" Title="Error Data" EnableCollapse="true" 
                                                    ShowBorder="true"  
                                                    runat="server"
                                                    DataKeyNames="NotificationID,AgentCode"
                                                    SummaryPosition="Flow">

 <Toolbars>
    <f:Toolbar ID="Toolbar4" runat="server">
        <Items>
 
            <f:Button ID="btnExportForm" Text="Export" runat="server" CssClass="inline" Icon="PageWhiteExcel"
                EnableAjax="false" DisableControlBeforePostBack="false">
            </f:Button>
                                               

        </Items>
    </f:Toolbar>
</Toolbars>

                                                    <Columns>
                                                        <f:RowNumberField />
                                                        <f:BoundField ColumnID="errf03" DataField="f03" HeaderText="M2_SHOWROOM" />
                                                        <f:BoundField ColumnID="errf04" DataField="f04" HeaderText="M2_INSURER" />
                                                        <f:BoundField ColumnID="errf05" DataField="f05" HeaderText="M2_CHASSIS NO" />
                                                        <f:BoundField ColumnID="errf06" DataField="f06" HeaderText="M2_CLIENT Code" />
                                                        <f:BoundField ColumnID="errf07" DataField="f07" HeaderText="M2_Name Surname" />
                                                        <f:BoundField ColumnID="errf08" DataField="f08" HeaderText="Effective" />
                                                        <f:BoundField ColumnID="errf09" DataField="f09" HeaderText="NetPremium" TextAlign="Right" />
                                                        <f:BoundField ColumnID="errf10" DataField="f10" HeaderText="M2_VMIStamp" TextAlign="Right" />
                                                        <f:BoundField ColumnID="errf11" DataField="f11" HeaderText="WH" TextAlign="Right" />
                                                    </Columns>
                                                </f:Grid>


                                            </Items>
                                        </f:Tab>
                                    </Tabs>
                                </f:TabStrip>



                            </Items>
                        </f:FormRow>





                    </Rows>
                </f:Form>


            </Items>
        </f:Panel>




    </Items>
</f:Panel>

<f:Window ID="WindowUploaddata" runat="server" Title="Data" Hidden="true"
    IsModal="True" EnableClose="true" WindowPosition="Center" Width="600px" Target="Parent" Height="600px">
    <Items>
        <f:SimpleForm ID="SimpleForm3" runat="server" ShowBorder="false" BodyPadding="10px"
            ShowHeader="false" LabelWidth="120px">
            <Items>
                <f:TextArea runat="server" ID="tbxdata" Height="500px" Required="true">
                </f:TextArea>
                <f:Button ID="btnUpload" Text="Upload" ValidateForms="SimpleForm3" Type="Submit" Icon="DiskUpload"
                    DisableControlBeforePostBack="false" ValidateTarget="Top" runat="server">
                </f:Button>
            </Items>
        </f:SimpleForm>
    </Items>
</f:Window>

<f:Window ID="WindowViewAgentData" runat="server" Title="Data" Hidden="true"
    IsModal="True" EnableClose="true" WindowPosition="Center" Target="Parent">
    <Items>
        <f:SimpleForm ID="SimpleForm1" runat="server" ShowBorder="false" BodyPadding="10px"
            ShowHeader="false" LabelWidth="120px">
            <Items>


                <f:Grid ID="Grid2" Title="" EnableCollapse="true" Width="800px" Height="350px"
                    ShowBorder="true" ShowGridHeader="false"
                    ShowHeader="false" runat="server"
                    DataKeyNames="NotificationID,AgentCode"
                    EnableSummary="true"
                    SummaryPosition="Flow">
                    <Columns>
                        <f:RowNumberField />
                        <f:BoundField ColumnID="f03" DataField="f03" HeaderText="M2_SHOWROOM" />
                        <f:BoundField ColumnID="f04" DataField="f04" HeaderText="M2_INSURER" />
                        <f:BoundField ColumnID="f05" DataField="f05" HeaderText="M2_CHASSIS NO" />
                        <f:BoundField ColumnID="f06" DataField="f06" HeaderText="M2_CLIENT Code" />
                        <f:BoundField ColumnID="f07" DataField="f07" HeaderText="M2_Name Surname" />
                        <f:BoundField ColumnID="f08" DataField="f08" HeaderText="Effective" />
                        <f:BoundField ColumnID="f09" DataField="f09" HeaderText="NetPremium" TextAlign="Right" />
                        <f:BoundField ColumnID="f10" DataField="f10" HeaderText="M2_VMIStamp" TextAlign="Right" />
                        <f:BoundField ColumnID="f11" DataField="f11" HeaderText="WH" TextAlign="Right" />
                    </Columns>
                </f:Grid>



            </Items>
        </f:SimpleForm>
    </Items>
</f:Window>


