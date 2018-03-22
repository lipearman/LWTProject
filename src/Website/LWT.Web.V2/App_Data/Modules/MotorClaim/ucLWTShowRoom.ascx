<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucLWTShowRoom.ascx.vb" Inherits="Modules_ucLWTShowRoom" %>


<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true"  HeaderText=""  runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>




            <dx:ASPxGridView ID="gridData" ClientInstanceName="gridData" runat="server"
                DataSourceID="SqlDataSource_NoticeMailContact"
                SettingsBehavior-ColumnResizeMode="Control"
                Settings-HorizontalScrollBarMode="Visible"
                KeyFieldName="ID"
                AutoGenerateColumns="False"
                Width="100%">
                <SettingsPager Mode="ShowPager">
                    <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                </SettingsPager>
                <SettingsBehavior AllowEllipsisInText="true" />
                <SettingsSearchPanel CustomEditorID="tbToolbarSearch" />
                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
                <SettingsBehavior AllowFocusedRow="true" />
                <SettingsPopup>
                    <EditForm Modal="true" Width="600" Height="480" MinHeight="200" HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                </SettingsPopup>

                <Styles>
                    <Header Font-Bold="true" Font-Underline="true" HorizontalAlign="Center">
                    </Header>
                </Styles>

                <Toolbars>
                    <dx:GridViewToolbar ItemAlign="Left">
                        <Items>

                            <dx:GridViewToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButton ID="btnExportData"
                                        ClientInstanceName="btnExportData"
                                        runat="server"
                                        Image-IconID="export_export_16x16office2013"
                                        ToolTip="Export to Excel" Text="Export"
                                        Border-BorderWidth="0"
                                        OnClick="btnExportData_Click">
                                    </dx:ASPxButton>

                                </Template>
                            </dx:GridViewToolbarItem>


                            <dx:GridViewToolbarItem Command="Edit" BeginGroup="true" />
                            <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />

                            <dx:GridViewToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButtonEdit ID="tbToolbarSearch" runat="server" NullText="Search..." Height="100%">
                                        <Buttons>
                                            <dx:SpinButtonExtended Image-IconID="find_find_16x16gray" />
                                        </Buttons>
                                    </dx:ASPxButtonEdit>
                                </Template>
                            </dx:GridViewToolbarItem>

                        </Items>
                    </dx:GridViewToolbar>
                </Toolbars>

                <Columns>

                    <%--<dx:GridViewCommandColumn Width="50" CellStyle-Wrap="False" ShowNewButtonInHeader="true" ShowEditButton="true" />--%>
                    <dx:GridViewDataTextColumn FieldName="ShowRoomCode" Width="100" SortOrder="Ascending" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="DealerCode" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>

                    <%--<dx:GridViewDataTextColumn FieldName="ShowroomID" Settings-AllowHeaderFilter="True" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>--%>

                    <dx:GridViewDataComboBoxColumn FieldName="AgentCode" Caption="Dealer" PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" Width="200"
                        Settings-AllowHeaderFilter="True"
                        CellStyle-Wrap="False">
                        <PropertiesComboBox TextField="AgentName" ValueField="AgentCode" DataSourceID="SqlDataSource_Dealer" DropDownStyle="DropDown">
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataComboBoxColumn FieldName="ShowroomID" Caption="Showroom(M2)" PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" Width="300"
                        Settings-AllowHeaderFilter="True"
                        CellStyle-Wrap="False">
                        <PropertiesComboBox TextField="CompanyName" ValueField="ShowroomID" DataSourceID="SqlDataSource_ShowroomM2" DropDownStyle="DropDown">
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>



                    <dx:GridViewDataTextColumn FieldName="ShowRoomName" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ShowRoomNameEN" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="BranchCode" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="BranchName" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="BranchNameEN" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="PartsOrderCode" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Address" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Province" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Zipcode" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="TelCode" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="TelNo" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="FaxNo" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Email" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="AddressEN" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ProvinceEN" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Region" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="SalesArea" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Type" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="BP" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Longitude" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Latitude" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="SPDeptCode" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataColumn FieldName="IsActive" Settings-AllowHeaderFilter="True" Width="100"></dx:GridViewDataColumn>




                </Columns>



            </dx:ASPxGridView>

            <dx:ASPxGridViewExporter runat="server" ID="GridExporter" GridViewID="gridData">
                <Styles>
                    <Default Font-Names="Tahoma" Font-Size="Medium" Wrap="False"></Default>
                </Styles>
            </dx:ASPxGridViewExporter>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>



<asp:SqlDataSource ID="SqlDataSource_NoticeMailContact" runat="server" ConnectionString="<%$ ConnectionStrings:MotorClaimConnectionString %>"
    SelectCommand="select * from tblShowRoom "
    InsertCommand="insert into tblShowRoom(
     ShowRoomCode,ShowroomID
    ,DealerCode
    ,BranchCode
    ,PartsOrderCode
    ,ShowRoomName
    ,BranchName
    ,Address
    ,Province
    ,Zipcode
    ,TelCode
    ,TelNo
    ,FaxNo
    ,Email
    ,ShowRoomNameEN
    ,BranchNameEN
    ,AddressEN
    ,ProvinceEN
    ,Region
    ,SalesArea
    ,Type
    ,BP
    ,Longitude
    ,Latitude
    ,SPDeptCode
    ,IsActive
    ,CreationDate
    ,CreationBy
    ) 

    values(
        @ShowRoomCode
        ,@ShowroomID
        ,@DealerCode
        ,@BranchCode
        ,@PartsOrderCode
        ,@ShowRoomName
        ,@BranchName
        ,@Address
        ,@Province
        ,@Zipcode
        ,@TelCode
        ,@TelNo
        ,@FaxNo
        ,@Email
        ,@ShowRoomNameEN
        ,@BranchNameEN
        ,@AddressEN
        ,@ProvinceEN
        ,@Region
        ,@SalesArea
        ,@Type
        ,@BP
        ,@Longitude
        ,@Latitude
        ,@SPDeptCode
        ,1
        ,getdate()
        ,@UserName
    )
    "
    UpdateCommand="UpDate tblShowRoom set 
     DealerCode = @DealerCode
    ,ShowRoomCode = @ShowRoomCode
    ,ShowroomID=@ShowroomID
    ,BranchCode = @BranchCode
    ,PartsOrderCode = @PartsOrderCode
    ,ShowRoomName = @ShowRoomName
    ,BranchName = @BranchName
    ,Address = @Address
    ,Province = @Province
    ,Zipcode = @Zipcode
    ,TelCode = @TelCode
    ,TelNo = @TelNo
    ,FaxNo = @FaxNo
    ,Email = @Email
    ,ShowRoomNameEN = @ShowRoomNameEN
    ,BranchNameEN = @BranchNameEN
    ,AddressEN = @AddressEN
    ,ProvinceEN = @ProvinceEN
    ,Region = @Region
    ,SalesArea = @SalesArea
    ,Type = @Type
    ,BP = @BP
    ,Longitude = @Longitude
    ,Latitude = @Latitude
    ,SPDeptCode = @SPDeptCode
    ,IsActive = @IsActive
    ,ModifyBy=@UserName
    ,ModifyDate=getdate()
    where ID = @ID ">


    <InsertParameters>
        <asp:Parameter Name="ShowRoomCode" />
        <asp:Parameter Name="ShowroomID" />
        <asp:Parameter Name="DealerCode" />
        <asp:Parameter Name="BranchCode" />
        <asp:Parameter Name="PartsOrderCode" />
        <asp:Parameter Name="ShowRoomName" />
        <asp:Parameter Name="BranchName" />
        <asp:Parameter Name="Address" />
        <asp:Parameter Name="Province" />
        <asp:Parameter Name="Zipcode" />
        <asp:Parameter Name="TelCode" />
        <asp:Parameter Name="TelNo" />
        <asp:Parameter Name="FaxNo" />
        <asp:Parameter Name="Email" />
        <asp:Parameter Name="ShowRoomNameEN" />
        <asp:Parameter Name="BranchNameEN" />
        <asp:Parameter Name="AddressEN" />
        <asp:Parameter Name="ProvinceEN" />
        <asp:Parameter Name="Region" />
        <asp:Parameter Name="SalesArea" />
        <asp:Parameter Name="Type" />
        <asp:Parameter Name="BP" />
        <asp:Parameter Name="Longitude" />
        <asp:Parameter Name="Latitude" />
        <asp:Parameter Name="SPDeptCode" />
        <asp:Parameter Name="IsActive" />
        <asp:Parameter Name="UserName" />
    </InsertParameters>

    <UpdateParameters>
        <asp:Parameter Name="ShowRoomCode" />
        <asp:Parameter Name="ShowroomID" />
        <asp:Parameter Name="DealerCode" />
        <asp:Parameter Name="BranchCode" />
        <asp:Parameter Name="PartsOrderCode" />
        <asp:Parameter Name="ShowRoomName" />
        <asp:Parameter Name="BranchName" />
        <asp:Parameter Name="Address" />
        <asp:Parameter Name="Province" />
        <asp:Parameter Name="Zipcode" />
        <asp:Parameter Name="TelCode" />
        <asp:Parameter Name="TelNo" />
        <asp:Parameter Name="FaxNo" />
        <asp:Parameter Name="Email" />
        <asp:Parameter Name="ShowRoomNameEN" />
        <asp:Parameter Name="BranchNameEN" />
        <asp:Parameter Name="AddressEN" />
        <asp:Parameter Name="ProvinceEN" />
        <asp:Parameter Name="Region" />
        <asp:Parameter Name="SalesArea" />
        <asp:Parameter Name="Type" />
        <asp:Parameter Name="BP" />
        <asp:Parameter Name="Longitude" />
        <asp:Parameter Name="Latitude" />
        <asp:Parameter Name="SPDeptCode" />
        <asp:Parameter Name="IsActive" />
        <asp:Parameter Name="UserName" />
        <asp:Parameter Name="ID" />
    </UpdateParameters>




</asp:SqlDataSource>



<asp:SqlDataSource ID="SqlDataSource_ShowroomM2" runat="server" ConnectionString="<%$ ConnectionStrings:nltdbConnectionString %>"
    SelectCommand="select ShowroomID,CompanyName + ' (' + case when IsActive = 1 then 'Active' else 'non-Active' end  + ') - ' + convert(varchar,ShowroomID)  as CompanyName from  Showroom  "></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_Dealer" runat="server" ConnectionString="<%$ ConnectionStrings:MotorClaimConnectionString %>"
    SelectCommand="select AgentCode, convert(varchar,DealerNameTH) + ' - ' + convert(varchar,AgentCode) as AgentName from  tblDealer order by DealerNameTH "></asp:SqlDataSource>

