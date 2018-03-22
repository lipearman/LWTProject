<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxNoticeHeaderSetup_B0027.ascx.vb" Inherits="Modules_ucDevxNoticeHeaderSetup_B0027" %>

<f:Panel ID="MainRegionPanel" runat="server" BodyPadding="5px" ShowBorder="false"
    ShowHeader="false" Title="Projects" EnableCollapse="true">
    <Toolbars>
        <f:Toolbar ID="Toolbar9" runat="server">
            <Items>



                <f:Button ID="btnImportForm" Text="Import" runat="server" CssClass="inline" Icon="Attach"
                    EnableAjax="false" DisableControlBeforePostBack="false">
                </f:Button>

                <f:Button ID="btnExportForm" Text="Export" runat="server" CssClass="inline" Icon="PageWhiteExcel"
                    EnableAjax="false" DisableControlBeforePostBack="false">
                </f:Button>

                <f:Button ID="btnSendMail" Text="ส่งแจ้งเมล์" ValidateForms="fform1" Type="Submit" Icon="Mail"
                    DisableControlBeforePostBack="false" ValidateTarget="Top" ConfirmText="Confirm to send mail" runat="server">
                </f:Button>

                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                </f:ToolbarFill>
                <f:Button ID="btnRefresh" Text="รีเฟรชข้อมูล" runat="server" Icon="Reload">
                </f:Button>
            </Items>
        </f:Toolbar>
    </Toolbars>


    <Items>

        <f:Grid ID="Grid1" Title="ข้อมูลเตรียมส่ง csv ไฟล์" ShowBorder="true" ShowHeader="true" SortField="InsuranceCode" Icon="Application"
            AllowSorting="true" runat="server" AutoScroll="true" DataKeyNames="ModelCode" EmptyText="No Data">
            <Toolbars>
                <f:Toolbar ID="Toolbar4" runat="server">
                    <Items>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Columns>

                <f:RowNumberField HeaderText="No." Width="50" />
                <f:BoundField ColumnID="InsuranceCode" Width="100" SortField="InsuranceCode" DataField="InsuranceCode" HeaderText="M2_Insurance Code" TextAlign="Left" />
                <f:BoundField ColumnID="Model_Code" Width="100" SortField="Model_Code" DataField="Model_Code" HeaderText="Model Code" TextAlign="Left" />
                <f:BoundField ColumnID="Memo" Width="100" SortField="Memo" DataField="Memo" HeaderText="Memo MONTH" TextAlign="Left" />
                <f:BoundField ColumnID="Invoice_Tax_No" Width="100" SortField="Invoice_Tax_No" DataField="Invoice_Tax_No" HeaderText="Invoice Tax NO" TextAlign="Left" />
                <f:BoundField ColumnID="Invoice_Tax_date" Width="100" SortField="Invoice_Tax_date" DataField="Invoice_Tax_date" HeaderText="Invoice Tax date" TextAlign="Left" />
                <f:BoundField ColumnID="Gross_Premium" Width="100" SortField="Gross_Premium" DataField="Gross_Premium" HeaderText="Gross Premium" TextAlign="Left" />
                <f:BoundField ColumnID="Stamp" Width="100" SortField="Stamp" DataField="Stamp" HeaderText="Stamp" TextAlign="Left" />
                <f:BoundField ColumnID="VAT" Width="100" SortField="VAT" DataField="VAT" HeaderText="VAT" TextAlign="Left" />
                <f:BoundField ColumnID="Discount" Width="100" SortField="Discount" DataField="Discount" HeaderText="discount" TextAlign="Left" />
                <f:BoundField ColumnID="CampaignPremium" Width="100" SortField="CampaignPremium" DataField="CampaignPremium" HeaderText="เบี้ยแคมเปญ" TextAlign="Left" />
                <f:BoundField ColumnID="Flag" Width="100" SortField="Flag" DataField="Flag" HeaderText="Flag" TextAlign="Left" />
            </Columns>
        </f:Grid>
    </Items>
</f:Panel>

<f:Window ID="WindowImportApplicationForm" runat="server" Title="Data" Hidden="true"
    IsModal="True" EnableClose="true" WindowPosition="Center" Width="400px" Target="Parent" Height="150px">
    <Items>
        <f:SimpleForm ID="SimpleForm5" runat="server" ShowBorder="false" BodyPadding="10px"
            ShowHeader="false">
            <Items>
                <%--                 <f:DropDownList Label="ประเภทการประกันภัย"  runat="server" ID="ddlTypeOfInsure" Width="500" Required="true" EnableSimulateTree="true" >
                </f:DropDownList>
                <f:TextArea runat="server" ID="txtData" Height="300px" Required="true">
                </f:TextArea>--%>


                <f:FileUpload runat="Server" ID="fuAttatch" LabelWidth="80" EmptyText="ไฟแนบ xls" Label="ไฟแนบ" Required="true"
                    ShowRedStar="true">
                </f:FileUpload>


                <f:Button ID="btnImportData" Text="Import" ValidateForms="SimpleForm5" Type="Submit" Icon="DiskUpload"
                    DisableControlBeforePostBack="false" ValidateTarget="Top" runat="server">
                </f:Button>
            </Items>
        </f:SimpleForm>
    </Items>
</f:Window>
