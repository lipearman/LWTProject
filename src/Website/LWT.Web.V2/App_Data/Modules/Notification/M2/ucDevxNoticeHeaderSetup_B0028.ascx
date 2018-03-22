<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxNoticeHeaderSetup_B0028.ascx.vb" Inherits="Modules_ucDevxNoticeHeaderSetup_B0028" %>
 <%@ Register Assembly="PdfViewer" Namespace="PdfViewer" TagPrefix="cc1" %>
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
                <f:Button ID="btnExcelFormat" Text="Excel Format" runat="server" CssClass="inline" EnableAjax="false" Icon="PageWhiteExcel" >
                </f:Button>


                <f:Button ID="btnAdd" Text="Create Notification" runat="server" CssClass="inline" Icon="Add">
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
                <f:BoundField ColumnID="NoticeID" Width="100" SortField="NoticeID" DataField="NoticeID" HeaderText="NoticeID" TextAlign="Left" />


                <f:BoundField ColumnID="NoticeTitle" Width="200" SortField="NoticeTitle" DataField="NoticeTitle" HeaderText="รายงาน" TextAlign="Left" />
 

                <%--<f:BoundField ColumnID="NoticeDate" Width="120" SortField="NoticeDate" DataField="NoticeDate" HeaderText="วันที่แจ้ง" TextAlign="Left" />--%>
                <f:BoundField ColumnID="DueDate" Width="120" SortField="DueDate" DataField="DueDate" HeaderText="วันที่กำหนด" TextAlign="Left" />


                <f:BoundField ColumnID="CreationDate" Width="160" SortField="CreationDate" DataField="CreationDate" HeaderText="วันที่สร้าง" TextAlign="Left" />
                <f:BoundField ColumnID="CreateBy" Width="100" SortField="CreateBy" DataField="CreateBy" HeaderText="CreateBy" TextAlign="Left" />

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
                                <f:Button ID="btnSave" Text="Save" runat="server" ValidateForms="Form1" CssClass="inline" Icon="SystemSave">
                                </f:Button>


                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Rows>

                        <f:FormRow runat="server" ID="FormRow1">
                            <Items>
                                <f:Label runat="server" ID="lbNotificationTitle" Label="Title" Text="รายงานตรวจสอบการแจ้งประกันภัย"></f:Label>
                                <f:Label runat="server" ID="lbCreationDate" Label="CreationDate"></f:Label>
                            </Items>
                        </f:FormRow>

                        <f:FormRow runat="server" ID="FormRow2">
                            <Items>
                                <%--                                <f:Label runat="server" ID="lbNoticeDate" Label="วันที่แจ้ง"></f:Label>
                                <f:Label runat="server" ID="lbDueDate" Label="วันที่กำหนด"></f:Label>--%>
                                <%--<f:DatePicker runat="server" ID="dpNoticeDate" Required="true" Label="วันที่แจ้ง"></f:DatePicker>--%>
                                <f:TextBox runat="server" ID="tbxNoticeTitle" Label="ข้อความแจ้ง" Required="true"></f:TextBox>
                                <f:DatePicker runat="server" ID="dpDueDate" Required="true" Label="วันที่กำหนด"></f:DatePicker>

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

                                <f:Grid ID="Grid3" Title="Data" Icon="Application" EmptyText="No Data"
                                    EnableRowDoubleClickEvent="true"
                                    ShowBorder="true" ShowHeader="true"
                                    runat="server"
                                    DataKeyNames="AgentCode,AgentName"
                                    EnableCheckBoxSelect="true">

                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar2" runat="server">
                                            <Items>


                                                <f:Button ID="btnUploadData" Text="Upload Data" runat="server" CssClass="inline" Icon="DiskUpload">
                                                </f:Button>
                                                <f:Button ID="btnSend" Text="Send" runat="server" ValidateForms="Form1" CssClass="inline" Icon="ApplicationGo" ConfirmText="Send Mail?">
                                                </f:Button>
                                                <f:Button ID="btnResend" Text="ReSend" runat="server" Hidden="true" ValidateForms="Form1" CssClass="inline" Icon="ArrowRedo" ConfirmText="ReSend Mail?">
                                                </f:Button>

                                                <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                                </f:ToolbarFill>
                                                <f:Button ID="btnDeleteData" Text="Delete" runat="server" CssClass="inline" Icon="Delete" ConfirmText="Confirm Delete?"></f:Button>

                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>

                                    <Columns>

                                        <f:RowNumberField HeaderText="No." Width="60" EnablePagingNumber="true" />
                                        <f:BoundField SortField="AgentCode" ColumnID="AgentCode" DataField="AgentCode" HeaderText="Dealer Code" Width="80" TextAlign="Left" />
                                        <f:BoundField SortField="AgentName" ColumnID="AgentName" DataField="AgentName" Width="280" HeaderText="Dealer Name" TextAlign="Left" />
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

                <f:Label runat="server" ID="lbNewTitle" Label="Title" Text="รายงานตรวจสอบการแจ้งประกันภัย"></f:Label>
                
                <f:TextBox runat="server" ID="tbxNewNoticeTitle" Label="ข้อความแจ้ง" Required="true"></f:TextBox>
                <%--<f:DatePicker runat="server" ID="dpNewNoticeDate" Required="true" Label="วันที่แจ้งขาย"></f:DatePicker>--%>
                <f:DatePicker runat="server" ID="dpNewDueDate" Required="true" Label="วันที่กำหนด"></f:DatePicker>
                <%-- <f:FileUpload runat="Server" ID="fuAttatch" EmptyText="ไฟแนบบ pdf" Label="ไฟแนบ" Required="true"
                    ShowRedStar="true">
                </f:FileUpload>--%>
                <f:Button ID="btnSaveNew" Text="บันทึก" ValidateForms="SimpleForm1" Type="Submit" Icon="SystemSave"
                    DisableControlBeforePostBack="false" ValidateTarget="Top" runat="server">
                </f:Button>
            </Items>
        </f:SimpleForm>
    </Items>
</f:Window>

<f:Window ID="WindowUploaddata" runat="server" Title="Data" Hidden="true"
    IsModal="True" EnableClose="true" WindowPosition="Center" Width="600px" Target="Parent" Height="400px">
    <Items>
        <f:SimpleForm ID="SimpleForm3" runat="server" ShowBorder="false" BodyPadding="10px"
            ShowHeader="false" LabelWidth="120px">
            <Items>
                <f:TextArea runat="server" ID="tbxdata" Height="300px" Required="true">
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

        <f:Grid ID="dgData" Title="Data" EnableCollapse="true" Width="800px" Height="350px"
            ShowBorder="true" ShowGridHeader="true"
            ShowHeader="false" runat="server"
            DataKeyNames="NotificationID,Code"
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
                <f:BoundField ColumnID="xf02" DataField="f02" HeaderText="Dealer Name" />
                <f:BoundField ColumnID="xf03" DataField="f03" HeaderText="Chassis No." />
                <f:BoundField ColumnID="xf04" DataField="f04" HeaderText="Engine No." />
                <f:BoundField ColumnID="xf05" DataField="f05" HeaderText="Customer's Thai Name" />
                <f:BoundField ColumnID="xf06" DataField="f06" HeaderText="Sold Date" /> 

            </Columns>
        </f:Grid>
        <%--AgentCode	Dealer Name	Chassis No.	Engine No.	Customer's Thai Name	Sold Date--%>

    </Items>
</f:Window>


<f:Window ID="Window1" runat="server" Title="Upload Module" Hidden="true" EnableIFrame="true" BodyPadding="5"
    IsModal="True" EnableClose="true" WindowPosition="Center" Width="570px" Height="320" Target="Parent">
</f:Window>
