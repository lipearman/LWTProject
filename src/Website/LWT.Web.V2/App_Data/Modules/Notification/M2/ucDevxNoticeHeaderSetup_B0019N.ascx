<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxNoticeHeaderSetup_B0019N.ascx.vb" Inherits="Modules_ucDevxNoticeHeaderSetup_B0019N" %>
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

        <f:Grid ID="Grid1" Title="Notification" ShowBorder="true" ShowHeader="true" SortField="CreationDate" SortDirection="DESC" Icon="Application" PageSize="20"
            AllowSorting="true" runat="server" AutoScroll="true" DataKeyNames="NoticeID" EmptyText="No Data" AllowPaging="true"
            
            EnableCheckBoxSelect="true">
            <Columns>
<f:LinkButtonField CommandName="btnUpload" Width="30" Icon="DiskUpload" ToolTip="Upload"  />
<f:LinkButtonField CommandName="btnExport" Width="30" Icon="PageWhiteExcel" ToolTip="Export Data" EnableAjax="false" EnablePostBack="true"  />
<f:LinkButtonField CommandName="btnSendMail" Width="30" ToolTip="Send Mail" TextAlign="Center"  Icon="Mail" ConfirmText="Send Mail?" />

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

                                 <f:Button ID="btnUploadData" Text="Upload Data" runat="server" CssClass="inline" Icon="DiskUpload">
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



                        <f:FormRow runat="server" ID="FormRow8" >

                            <Items>


                                                <f:Grid ID="Grid3" Title="Clients" Icon="Application" EmptyText="No Data"
                                                   
                                                    ShowBorder="true" ShowHeader="true" AutoScroll="true" 
                                                    height="400"
                                                    runat="server"
                                                    DataKeyNames="UWCode,UWName"
                                                    EnableCheckBoxSelect="true"
                                                    EnableRowDoubleClickEvent="true"
                                                    >

                                                    <Toolbars>
                                                        <f:Toolbar ID="Toolbar2" runat="server">
                                                            <Items>


                                                               

                                                                <f:Button ID="btnDeleteData" Text="Delete" runat="server" CssClass="inline" Icon="Delete" ConfirmText="Confirm Delete?"></f:Button>


  <f:Button ID="btnExport" Text="Export" runat="server" CssClass="inline" Icon="PageWhiteExcel"
                EnableAjax="false" DisableControlBeforePostBack="false">
            </f:Button>
                            


                                                                  <f:Button ID="btnSend" Text="Send Mail" runat="server" ValidateForms="Form1" CssClass="inline" Icon="Mail" ConfirmText="Send Mail?">
                                </f:Button>
                                                            </Items>
                                                        </f:Toolbar>
                                                    </Toolbars>

                                                    <Columns>



<%--
Code Client Code
f01	ผู้เอาประกันภัย
f02	New
f03	Policy No. หรือ เลขรับแจ้ง
f04	หมายเลขตัวถัง/ทะเบียนรถ
f05	SumInsure 
f06	VMI Premium 
f07	Beneficiary
f08	Insurer
f09	ShowRoom
f10	Type
f11	Amendment Details
f12	รายการข้อมูลเดิม
f13	รายการที่แก้ไขใหม่
--%>

<f:RowNumberField HeaderText="No" Width="60" EnablePagingNumber="true" />

<f:BoundField SortField="UWCode" ColumnID="UWCode" DataField="UWCode" HeaderText="Code" TextAlign="Left" />
<f:BoundField SortField="UWName" ColumnID="UWName" DataField="UWName" HeaderText="บริษัทประกันภัย" Width="300" TextAlign="Left" />
<f:BoundField SortField="Clients" ColumnID="Clients" DataField="Clients" HeaderText="จำนวน" TextAlign="Left" />

<f:LinkButtonField CommandName="btnSend" Width="60px" ToolTip="Data" TextAlign="Center" HeaderText="Send" Icon="Mail" ConfirmText="Send Mail?" />




<%--<f:BoundField SortField="f09" ColumnID="f09" DataField="f09" HeaderText="UWCode" TextAlign="Left" />
<f:BoundField SortField="f10" ColumnID="f10" DataField="f10" HeaderText="ประกันภัย" TextAlign="Left" />


<f:BoundField SortField="Code" ColumnID="Code" DataField="Code" HeaderText="Client Code" Width="80" TextAlign="Left" />
<f:BoundField SortField="f01" ColumnID="f01" DataField="f01" HeaderText="ผู้เอาประกันภัย" TextAlign="Left" />
<f:BoundField SortField="f02" ColumnID="f02" DataField="f02" HeaderText="New" TextAlign="center" />
<f:BoundField SortField="f03" ColumnID="f03" DataField="f03" HeaderText="Policy No. หรือ เลขรับแจ้ง" TextAlign="Left" />
<f:BoundField SortField="f04" ColumnID="f04" DataField="f04" HeaderText="หมายเลขตัวถัง/ทะเบียนรถ" TextAlign="Left" />--%>
<%--<f:BoundField SortField="f05" ColumnID="f05" DataField="f05" HeaderText="SumInsure" TextAlign="Left" />--%>
<%--<f:BoundField SortField="f06" ColumnID="f06" DataField="f06" HeaderText="VMI Premium" TextAlign="Left" />--%>
<%--<f:BoundField SortField="f07" ColumnID="f07" DataField="f07" HeaderText="Beneficiary" TextAlign="Left" />--%>
<%--<f:BoundField SortField="f08" ColumnID="f08" DataField="f08" HeaderText="Insurer" TextAlign="Left" />
<f:BoundField SortField="f11" ColumnID="f11" DataField="f11" HeaderText="รายการแก้ไข" TextAlign="Left" />
<f:BoundField SortField="f12" ColumnID="f12" DataField="f12" HeaderText="รายการข้อมูลเดิม" TextAlign="Left" />
<f:BoundField SortField="f13" ColumnID="f13" DataField="f13" HeaderText="รายการที่แก้ไขใหม่" TextAlign="Left" />--%>

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




<f:Window ID="WindowViewData" runat="server" Title="Data" Hidden="true"
    IsModal="True" EnableClose="true" WindowPosition="Center" Target="Parent">
    <Items>

        <f:Grid ID="dgData" Title="Data" EnableCollapse="true" Width="800px" Height="350px"
            ShowBorder="true" ShowGridHeader="true"
            ShowHeader="false" runat="server"
            DataKeyNames="NoticeID,Code"
            EnableSummary="true"
            SummaryPosition="Flow">
            <Columns>
                <f:RowNumberField />
                <f:BoundField SortField="Code" ColumnID="Code" DataField="Code" HeaderText="Client Code" Width="80" TextAlign="Left" />
                <f:BoundField SortField="f01" ColumnID="f01" DataField="f01" HeaderText="ผู้เอาประกันภัย" TextAlign="Left" />
                <f:BoundField SortField="f02" ColumnID="f02" DataField="f02" HeaderText="New" TextAlign="center" />
                <f:BoundField SortField="f03" ColumnID="f03" DataField="f03" HeaderText="Policy No. หรือ เลขรับแจ้ง" TextAlign="Left" />
                <f:BoundField SortField="f04" ColumnID="f04" DataField="f04" HeaderText="หมายเลขตัวถัง/ทะเบียนรถ" TextAlign="Left" />
                <f:BoundField SortField="f11" ColumnID="f11" DataField="f11" HeaderText="รายการแก้ไข" TextAlign="Left" />
                <f:BoundField SortField="f12" ColumnID="f12" DataField="f12" HeaderText="รายการข้อมูลเดิม" TextAlign="Left" />
                <f:BoundField SortField="f13" ColumnID="f13" DataField="f13" HeaderText="รายการที่แก้ไขใหม่" TextAlign="Left" />

            </Columns>
        </f:Grid>

    </Items>
</f:Window>
