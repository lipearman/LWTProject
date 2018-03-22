<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxNoticeHeaderSetup_B0016R.ascx.vb" Inherits="Modules_ucDevxNoticeHeaderSetup_B0016R" %>
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

                <f:Button ID="btnAdd" Text="Create Notification" runat="server" CssClass="inline" Icon="Add" >
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
        <f:HiddenField runat="server" ID="hdNotificationID" ></f:HiddenField>
        <f:Grid ID="Grid1" Title="Notification" ShowBorder="true" ShowHeader="true" SortField="CreationDate" SortDirection="DESC" Icon="Application" PageSize="20"
            AllowSorting="true" runat="server" AutoScroll="true" DataKeyNames="NoticeID" EmptyText="No Data" AllowPaging="true"
            
            EnableCheckBoxSelect="true">
            <Columns>
<f:LinkButtonField CommandName="btnUpload" TextAlign="Center" Width="50"  Icon="DiskUpload" HeaderText="Upload" ToolTip="Upload"  />
<f:LinkButtonField CommandName="btnExport"  TextAlign="Center" Width="50" Icon="PageWhiteExcel" HeaderText="Export Endorse" ToolTip="Export Endorsement Data" EnableAjax="false" EnablePostBack="true"  />
<f:LinkButtonField CommandName="btnSendMail"  TextAlign="Center" Width="50" ToolTip="Send Mail Endorsement" HeaderText="Send Endorse"  Icon="Mail" ConfirmText="Endorsement: Send Mail?" />

<f:LinkButtonField CommandName="btnExport_Amend"  TextAlign="Center" Width="50" Icon="PageWhiteExcel" HeaderText="Export Amend" ToolTip="Export Amend Data" EnableAjax="false" EnablePostBack="true"  />
<f:LinkButtonField CommandName="btnSendMail_Amend"  TextAlign="Center" Width="50" ToolTip="Send Mail Amend" HeaderText="Send Amend"  Icon="Mail" ConfirmText="Amend: Send Mail?" />

<%--<f:BoundField ColumnID="NotificationID" Width="100" SortField="NotificationID" DataField="NotificationID" HeaderText="NotificationID" TextAlign="Left" />
<f:BoundField ColumnID="NotificationTitle" Width="300" SortField="NotificationTitle" DataField="NotificationTitle" HeaderText="รายงาน" TextAlign="Left" />
<f:BoundField ColumnID="NoticeDate" Width="120" SortField="NoticeDate" DataField="NoticeDate" HeaderText="วันที่แจ้ง" TextAlign="Left" />--%>
<f:TemplateField ColumnID="expander" HeaderText="รายการแจ้ง"  Width="350">
<ItemTemplate>
             
<%# Eval("NoticeTitle")%> (<%# Eval("NoticeDate", "{0:dd/MM/yyyy}")%>)
               
</ItemTemplate>
</f:TemplateField>


<f:BoundField ColumnID="CreationDate" Width="160" SortField="CreationDate" DataField="CreationDate" HeaderText="วันที่สร้าง" TextAlign="Left" />
<f:BoundField ColumnID="CreationBy" Width="100" SortField="CreationBy" DataField="CreationBy" HeaderText="ผู้สร้าง" TextAlign="Left" />
<f:BoundField ColumnID="SendDate" Width="120" SortField="SendDate" DataField="SendDate" HeaderText="วันที่ส่ง" TextAlign="Left" />
<f:BoundField ColumnID="ReSendDate" Width="120" SortField="ReSendDate" DataField="ReSendDate" HeaderText="ส่งล่าสุด" TextAlign="Left" />

            </Columns>
        </f:Grid>

 

    </Items>
</f:Panel>

<f:Window ID="WindowUploaddata" runat="server" Title="Data" Hidden="true"
    IsModal="True" EnableClose="true" WindowPosition="Center" Width="600px" Target="Parent" Height="330px">
    <Items>
        <f:SimpleForm ID="SimpleForm3" runat="server" ShowBorder="false" BodyPadding="10px"
            ShowHeader="false" LabelWidth="120px">
            <Items>
                <f:TextArea runat="server" ID="tbxdata" Height="250px" Required="true">
                </f:TextArea>
                <f:Button ID="btnUpload" Text="Upload" ValidateForms="SimpleForm3" Type="Submit" Icon="DiskUpload"
                    DisableControlBeforePostBack="true" EnableAjaxLoading="true" ValidateTarget="Top" runat="server" >
                </f:Button>
            </Items>
        </f:SimpleForm>
    </Items>
</f:Window>


<f:Window ID="WindowDataNew" runat="server" Title="New Notification" Hidden="true"
    IsModal="True" EnableClose="true" WindowPosition="Center" Width="450px" Target="Parent">
    <Items>
        <f:SimpleForm ID="SimpleForm1" runat="server" ShowBorder="false" BodyPadding="10px" MessageTarget="Qtip"
            ShowHeader="false" LabelWidth="80px">
            <Items>               
                <f:DatePicker runat="server" ID="dpNewNoticeDate" Required="true" Label="รายการวันที่"></f:DatePicker>                
                <f:Button ID="btnSaveNew" Text="บันทึก" ValidateForms="SimpleForm1" Type="Submit" Icon="SystemSave"
                    DisableControlBeforePostBack="false" ValidateTarget="Top" runat="server">
                </f:Button>
            </Items>
        </f:SimpleForm>
    </Items>
</f:Window>

 