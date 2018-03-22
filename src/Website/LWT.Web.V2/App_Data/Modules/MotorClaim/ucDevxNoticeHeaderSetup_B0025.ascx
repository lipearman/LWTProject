<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxNoticeHeaderSetup_B0025.ascx.vb" Inherits="Modules_ucDevxNoticeHeaderSetup_B0025" %>

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
                <f:Button ID="btnAdd" Text="Create Notification" runat="server" CssClass="inline" Icon="Add">
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
            EnableRowDoubleClickEvent="true">
            <Columns>
                <f:BoundField ColumnID="NoticeID" Width="100" SortField="NoticeID" DataField="NoticeID" HeaderText="NoticeID" TextAlign="Left" />

                <f:BoundField ColumnID="NoticeDate" Width="120" SortField="NoticeDate" DataField="NoticeDate" HeaderText="วันที่แจ้งเคลม" TextAlign="Left" />
                <f:BoundField ColumnID="DueDate" Width="120" SortField="DueDate" DataField="DueDate" HeaderText="ถึงวันที่" TextAlign="Left" />

                <f:BoundField ColumnID="NoticeTitle" Width="300" SortField="NoticeTitle" DataField="NoticeTitle" HeaderText="รายงาน" TextAlign="Left" />
                <f:BoundField ColumnID="CreationDate" Width="160" SortField="CreationDate" DataField="CreationDate" HeaderText="วันที่สร้าง" TextAlign="Left" />
                <f:BoundField ColumnID="CreationBy" Width="100" SortField="CreationBy" DataField="CreationBy" HeaderText="CreationBy" TextAlign="Left" />

                <f:BoundField ColumnID="SendDate" Width="120" SortField="SendDate" DataField="SendDate" HeaderText="วันที่ส่ง" TextAlign="Left" />
                <f:BoundField ColumnID="ReSendDate" Width="120" SortField="ReSendDate" DataField="ReSendDate" HeaderText="ส่งซ้ำ" TextAlign="Left" />

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
                                <%--                                <f:Button ID="btnAddFile" Text="Add File" runat="server" Icon="Attach">
                                </f:Button>--%>
                              <%--  <f:Button ID="btnSave" Text="Save" runat="server" ValidateForms="Form1" CssClass="inline" Icon="SystemSave">
                                </f:Button>
--%>

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
                        <f:FormRow runat="server" ID="FormRow2">
                            <Items>
                                 <f:Label runat="server" ID="lbNoticeDate" Label="วันที่เกิดเหตุ"></f:Label>
                                <f:Label runat="server" ID="lbDueDate" Label="ถึงวันที่"></f:Label> 
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


                                <f:Grid ID="Grid3" Title="ตัวแทน" Icon="Application" EmptyText="No Data"
                                    EnableRowDoubleClickEvent="true"
                                    ShowBorder="true" ShowHeader="true"
                                    runat="server"
                                    DataKeyNames="ShowRoomCode,ShowRoomName"
                                    EnableCheckBoxSelect="true">

                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar2" runat="server">
                                            <Items>
                                                 
                                                <f:Button ID="btnSend" Text="Send" runat="server" ValidateForms="Form1" CssClass="inline" Icon="ApplicationGo" ConfirmText="Send Mail?">
                                                </f:Button>
                                                <f:Button ID="btnResend" Text="ReSend" runat="server" Hidden="true" ValidateForms="Form1" CssClass="inline" Icon="ArrowRedo" ConfirmText="ReSend Mail?">
                                                </f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>

                                    <Columns>

                                        <f:RowNumberField HeaderText="No." Width="60" EnablePagingNumber="true" />
                                        <f:BoundField SortField="ShowRoomCode" ColumnID="ShowRoomCode" DataField="ShowRoomCode" HeaderText="ShowRoomCode" Width="80" TextAlign="Left" />
                                        <f:BoundField SortField="ShowRoomName" ColumnID="ShowRoomName" DataField="ShowRoomName" Width="280" HeaderText="ShowRoomName" TextAlign="Left" />
                                        <f:BoundField SortField="Records" Width="100px" ColumnID="Records" DataField="Records" HeaderText="Records" TextAlign="center" />
                                        <f:LinkButtonField CommandName="btnSend" Width="60px" ToolTip="Data" TextAlign="Center" HeaderText="Send" Icon="Mail" ConfirmText="Send Mail?" />


                                    </Columns>
                                </f:Grid>



                            </Items>
                        </f:FormRow>





                    </Rows>
                </f:Form>


            </Items>
        </f:Panel>




    </Items>
</f:Panel>

<f:Window ID="WindowDataNew" runat="server" Title="New Notification" Hidden="true"
    IsModal="True" EnableClose="true" WindowPosition="Center" Width="450px" Target="Parent">
    <Items>
        <f:SimpleForm ID="SimpleForm1" runat="server" ShowBorder="false" BodyPadding="10px" MessageTarget="Qtip"
            ShowHeader="false" LabelWidth="120px">
            <Items>

                <f:Label runat="server" ID="lbNewTitle" Label="Title" Text="ข้อมูลตรวจสอบเคลม การเกิดเหตุรายสัปดาห์"></f:Label>
                <f:DatePicker runat="server" ID="dpNewNoticeDate" Required="true" Label="วันที่แจ้งเคลม"></f:DatePicker>
                <f:DatePicker runat="server" ID="dpNewDueDate" Required="true" Label="ถึงวันที่"></f:DatePicker>
                <f:Button ID="btnSaveNew" Text="บันทึก" ValidateForms="SimpleForm1" Type="Submit" Icon="SystemSave"
                    DisableControlBeforePostBack="false" ValidateTarget="Top" runat="server">
                </f:Button>
            </Items>
        </f:SimpleForm>
    </Items>
</f:Window>

 
<f:Window ID="WindowViewAgentData" runat="server" Title="Data" Hidden="true"
    IsModal="True" EnableClose="true" WindowPosition="Center" Target="Parent">
    <Items>

        <f:Grid ID="dgData" Title="Data" EnableCollapse="true" Width="800px" Height="350px"
            ShowBorder="true" ShowGridHeader="true"
            ShowHeader="false" runat="server"
            DataKeyNames="NoticeID,ShowRoomCode"
            EnableSummary="true"
            SummaryPosition="Flow">

            <Toolbars>
                <f:Toolbar ID="Toolbar3" runat="server">
                    <Items>
                    </Items>
                </f:Toolbar>
            </Toolbars>

            <Columns>
                <f:RowNumberField />
                <f:BoundField ColumnID="xf02" DataField="f02" HeaderText="ผู้เอาประกันภัย" />
                <f:BoundField ColumnID="xf03" DataField="f03" HeaderText="เลขตัวถัง" />
                <f:BoundField ColumnID="xf04" DataField="f04" HeaderText="เลขที่กรมธรรม์" />
                <f:BoundField ColumnID="xf05" DataField="f05" HeaderText="เลขที่เคลม" />
                <f:BoundField ColumnID="xf06" DataField="f06" HeaderText="วันที่เกิดเหตุ" />
                <f:BoundField ColumnID="xf07" DataField="f07" HeaderText="สถานที่เกิดเหตุ" />
                <f:BoundField ColumnID="xf08" DataField="f08" HeaderText="วันคุ้มครอง" />
                <f:BoundField ColumnID="xf09" DataField="f09" HeaderText="ผู้รับผลประโยชน์" />
                <f:BoundField ColumnID="xf10" DataField="f10" HeaderText="ประกันภัย" />
                <f:BoundField ColumnID="xf11" DataField="f11" HeaderText="ผู้แจ้งเคลม" />
                <f:BoundField ColumnID="xf12" DataField="f12" HeaderText="เบอร์โทรผู้แจ้งเคลม" />
            </Columns>
        </f:Grid>

    </Items>
</f:Window>


<f:Window ID="Window1" runat="server" Title="Upload Module" Hidden="true" EnableIFrame="true" BodyPadding="5"
    IsModal="True" EnableClose="true" WindowPosition="Center" Width="570px" Height="320" Target="Parent">
</f:Window>
