<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucM2ImportAppForm316.ascx.vb" Inherits="Modules_ucM2ImportAppForm316" %>


<f:Panel ID="MainRegionPanel" runat="server" BodyPadding="5px" ShowBorder="false"
    ShowHeader="false" Title="Projects" EnableCollapse="true">
    <Toolbars>
        <f:Toolbar ID="Toolbar9" runat="server">
            <Items>
<f:Button ID="btnExcelFormat" Text="Excel Format" runat="server" CssClass="inline" EnableAjax="false" Icon="PageWhiteExcel" >
                </f:Button>


  <f:Button ID="btnImportForm" Text="Import" runat="server" CssClass="inline" Icon="Attach"
                EnableAjax="false" DisableControlBeforePostBack="false">
            </f:Button>

            <f:Button ID="btnExportForm" Text="Export" runat="server" CssClass="inline" Icon="PageWhiteExcel"
                EnableAjax="false" DisableControlBeforePostBack="false">
            </f:Button>
                           

                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                </f:ToolbarFill>
                <f:Button ID="btnRefresh" Text="รีเฟรชข้อมูล" runat="server" Icon="Reload">
                </f:Button>
            </Items>
        </f:Toolbar>
    </Toolbars>


    <Items>

                <f:Grid ID="Grid1" Title="Thanachart New Application Form" ShowBorder="true" ShowHeader="true" SortField="RowNo" Icon="Application"
                    AllowSorting="true" runat="server" AutoScroll="true" DataKeyNames="ModelCode" EmptyText="No Data" >
 <Toolbars>
    <f:Toolbar ID="Toolbar4" runat="server">
        <Items>
                    

        </Items>
    </f:Toolbar>
</Toolbars>
                    <Columns>


<f:BoundField ColumnID="RowNo" Width="100" SortField="RowNo" DataField="RowNo" HeaderText="RowNo" TextAlign="Left" />
<f:BoundField ColumnID="ReferenceNo" Width="100" SortField="ReferenceNo" DataField="ReferenceNo" HeaderText="ReferenceNo" TextAlign="Left" />
<f:BoundField ColumnID="Title" Width="100" SortField="Title" DataField="Title" HeaderText="Title" TextAlign="Left" />
<f:BoundField ColumnID="Name" Width="100" SortField="Name" DataField="Name" HeaderText="Name" TextAlign="Left" />
<f:BoundField ColumnID="SurName" Width="100" SortField="SurName" DataField="SurName" HeaderText="SurName" TextAlign="Left" />
<f:BoundField ColumnID="LoanContractNo" Width="100" SortField="LoanContractNo" DataField="LoanContractNo" HeaderText="LoanContractNo" TextAlign="Left" />
<f:BoundField ColumnID="Address1" Width="100" SortField="Address1" DataField="Address1" HeaderText="Address1" TextAlign="Left" />
<f:BoundField ColumnID="Address2" Width="100" SortField="Address2" DataField="Address2" HeaderText="Address2" TextAlign="Left" />
<f:BoundField ColumnID="Mobile" Width="100" SortField="Mobile" DataField="Mobile" HeaderText="Mobile" TextAlign="Left" />
<f:BoundField ColumnID="OfficePhone" Width="100" SortField="OfficePhone" DataField="OfficePhone" HeaderText="OfficePhone" TextAlign="Left" />
<f:BoundField ColumnID="HomePhone" Width="100" SortField="HomePhone" DataField="HomePhone" HeaderText="HomePhone" TextAlign="Left" />
<f:BoundField ColumnID="Insurer" Width="100" SortField="Insurer" DataField="Insurer" HeaderText="Insurer" TextAlign="Left" />
<f:BoundField ColumnID="TypeInsurance" Width="100" SortField="TypeInsurance" DataField="TypeInsurance" HeaderText="TypeInsurance" TextAlign="Left" />
<f:BoundField ColumnID="CarPrice" Width="100" SortField="CarPrice" DataField="CarPrice" HeaderText="CarPrice" TextAlign="Left" />
<f:BoundField ColumnID="Brand" Width="100" SortField="Brand" DataField="Brand" HeaderText="Brand" TextAlign="Left" />
<f:BoundField ColumnID="Model" Width="100" SortField="Model" DataField="Model" HeaderText="Model" TextAlign="Left" />
<f:BoundField ColumnID="ModelCode" Width="100" SortField="ModelCode" DataField="ModelCode" HeaderText="ModelCode" TextAlign="Left" />
<f:BoundField ColumnID="YearOfCar" Width="100" SortField="YearOfCar" DataField="YearOfCar" HeaderText="YearOfCar" TextAlign="Left" />
<f:BoundField ColumnID="CC" Width="100" SortField="CC" DataField="CC" HeaderText="CC" TextAlign="Left" />
<f:BoundField ColumnID="Seat" Width="100" SortField="Seat" DataField="Seat" HeaderText="Seat" TextAlign="Left" />
<f:BoundField ColumnID="Weight" Width="100" SortField="Weight" DataField="Weight" HeaderText="Weight" TextAlign="Left" />
<f:BoundField ColumnID="EngineNo" Width="100" SortField="EngineNo" DataField="EngineNo" HeaderText="EngineNo" TextAlign="Left" />
<f:BoundField ColumnID="ChassisNo" Width="100" SortField="ChassisNo" DataField="ChassisNo" HeaderText="ChassisNo" TextAlign="Left" />
<f:BoundField ColumnID="Colour" Width="100" SortField="Colour" DataField="Colour" HeaderText="Colour" TextAlign="Left" />
<f:BoundField ColumnID="LicenseNo" Width="100" SortField="LicenseNo" DataField="LicenseNo" HeaderText="LicenseNo" TextAlign="Left" />
<f:BoundField ColumnID="RegisterdName" Width="100" SortField="RegisterdName" DataField="RegisterdName" HeaderText="RegisterdName" TextAlign="Left" />
<f:BoundField ColumnID="UseOfCar" Width="100" SortField="UseOfCar" DataField="UseOfCar" HeaderText="UseOfCar" TextAlign="Left" />
<f:BoundField ColumnID="SumInsured" Width="100" SortField="SumInsured" DataField="SumInsured" HeaderText="SumInsured" TextAlign="Left" />
<f:BoundField ColumnID="StartingDate" Width="100" SortField="StartingDate" DataField="StartingDate" HeaderText="StartingDate" TextAlign="Left" />
<f:BoundField ColumnID="Showroom" Width="100" SortField="Showroom" DataField="Showroom" HeaderText="Showroom" TextAlign="Left" />
<f:BoundField ColumnID="ShowroomCode" Width="100" SortField="ShowroomCode" DataField="ShowroomCode" HeaderText="ShowroomCode" TextAlign="Left" />
<f:BoundField ColumnID="ShowroomContactName" Width="100" SortField="ShowroomContactName" DataField="ShowroomContactName" HeaderText="ShowroomContactName" TextAlign="Left" />
<f:BoundField ColumnID="ShowroomContactPhone" Width="100" SortField="ShowroomContactPhone" DataField="ShowroomContactPhone" HeaderText="ShowroomContactPhone" TextAlign="Left" />
<f:BoundField ColumnID="CMIStatus" Width="100" SortField="CMIStatus" DataField="CMIStatus" HeaderText="CMIStatus" TextAlign="Left" />
<f:BoundField ColumnID="Leasing" Width="100" SortField="Leasing" DataField="Leasing" HeaderText="Leasing" TextAlign="Left" />
<f:BoundField ColumnID="Beneficiary" Width="100" SortField="Beneficiary" DataField="Beneficiary" HeaderText="Beneficiary" TextAlign="Left" />
<f:BoundField ColumnID="Agent" Width="100" SortField="Agent" DataField="Agent" HeaderText="Agent" TextAlign="Left" />
<f:BoundField ColumnID="CoverageType" Width="100" SortField="CoverageType" DataField="CoverageType" HeaderText="CoverageType" TextAlign="Left" />
<f:BoundField ColumnID="BillingName1" Width="100" SortField="BillingName1" DataField="BillingName1" HeaderText="BillingName1" TextAlign="Left" />
<f:BoundField ColumnID="BillingAddress1" Width="100" SortField="BillingAddress1" DataField="BillingAddress1" HeaderText="BillingAddress1" TextAlign="Left" />
<f:BoundField ColumnID="BillingTotals1" Width="100" SortField="BillingTotals1" DataField="BillingTotals1" HeaderText="BillingTotals1" TextAlign="Left" />
<f:BoundField ColumnID="BillingName2" Width="100" SortField="BillingName2" DataField="BillingName2" HeaderText="BillingName2" TextAlign="Left" />
<f:BoundField ColumnID="BillingAddress2" Width="100" SortField="BillingAddress2" DataField="BillingAddress2" HeaderText="BillingAddress2" TextAlign="Left" />
<f:BoundField ColumnID="BillingTotals2" Width="100" SortField="BillingTotals2" DataField="BillingTotals2" HeaderText="BillingTotals2" TextAlign="Left" />
<f:BoundField ColumnID="TempID" Width="100" SortField="TempID" DataField="TempID" HeaderText="TempID" TextAlign="Left" />
<f:BoundField ColumnID="BatchNo" Width="100" SortField="BatchNo" DataField="BatchNo" HeaderText="BatchNo" TextAlign="Left" />
<f:BoundField ColumnID="IDCard" Width="100" SortField="IDCard" DataField="IDCard" HeaderText="IDCard" TextAlign="Left" />


                    </Columns>
                </f:Grid>
    </Items>
</f:Panel>

<f:Window ID="WindowImportApplicationForm" runat="server" Title="Data" Hidden="true"
    IsModal="True" EnableClose="true" WindowPosition="Center" Width="600px" Target="Parent" Height="150px">
    <Items>
        <f:SimpleForm ID="SimpleForm5" runat="server" ShowBorder="false" BodyPadding="10px"
            ShowHeader="false" LabelWidth="120px">
            <Items>
<%--                 <f:DropDownList Label="ประเภทการประกันภัย"  runat="server" ID="ddlTypeOfInsure" Width="500" Required="true" EnableSimulateTree="true" >
                </f:DropDownList>
                <f:TextArea runat="server" ID="txtData" Height="300px" Required="true">
                </f:TextArea>--%>


  <f:FileUpload runat="Server" ID="fuAttatch" EmptyText="ไฟแนบ xls" Label="ไฟแนบ" Required="true"
                    ShowRedStar="true">
                </f:FileUpload>


                <f:Button ID="btnImportData" Text="Import" ValidateForms="SimpleForm5" Type="Submit" Icon="DiskUpload"
                    DisableControlBeforePostBack="false" ValidateTarget="Top" runat="server">
                </f:Button>
            </Items>
        </f:SimpleForm>
    </Items>
</f:Window>