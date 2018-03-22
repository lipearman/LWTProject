<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucLWTGarage.ascx.vb" Inherits="Modules_ucLWTGarage" %>


<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true"  HeaderText=""  runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>


            <dx:ASPxGridView ID="gridData" ClientInstanceName="gridData" runat="server" DataSourceID="SqlDataSource_Garage"
                SettingsBehavior-ColumnResizeMode="Control" Settings-HorizontalScrollBarMode="Visible"
                KeyFieldName="ID" AutoGenerateColumns="False" Width="100%">
                <SettingsPager Mode="ShowPager">
                    <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                </SettingsPager>
                <SettingsBehavior AllowEllipsisInText="true" />
                <SettingsSearchPanel CustomEditorID="tbToolbarSearch" />
                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />

                <SettingsPopup>
                    <EditForm Modal="true" Width="600" Height="400" MinHeight="200" HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                </SettingsPopup>
                <SettingsBehavior AllowFocusedRow="true" />
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
                    <dx:GridViewDataTextColumn FieldName="GarageCode" Width="100" SortOrder="Ascending" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="GarageName" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="Address" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Province" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Zipcode" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="TelCode" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="TelNo" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="FaxNo" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Region" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="SalesArea" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="GarageType" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Longitude" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Latitude" Width="100" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>



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



<asp:SqlDataSource ID="SqlDataSource_Garage" runat="server" ConnectionString="<%$ ConnectionStrings:MotorClaimConnectionString %>"
    SelectCommand="select * from tblGarage "
    InsertCommand="insert into tblGarage(
     GarageCode
      ,GarageName
      ,Address
      ,Province
      ,Zipcode
      ,TelCode
      ,TelNo
      ,FaxNo
      ,Region
      ,SalesArea
      ,GarageType
      ,Longitude
      ,Latitude
    ) 

    values(
       @GarageCode
      ,@GarageName
      ,@Address
      ,@Province
      ,@Zipcode
      ,@TelCode
      ,@TelNo
      ,@FaxNo
      ,@Region
      ,@SalesArea
      ,@GarageType
      ,@Longitude
      ,@Latitude
    )
    "
    UpdateCommand="UpDate tblGarage
    SET GarageCode = @GarageCode
      ,GarageName = @GarageName
      ,Address = @Address
      ,Province = @Province
      ,Zipcode = @Zipcode
      ,TelCode = @TelCode
      ,TelNo = @TelNo
      ,FaxNo = @FaxNo
      ,Region = @Region
      ,SalesArea = @SalesArea
      ,GarageType = @GarageType
      ,Longitude = @Longitude
      ,Latitude = @Latitude
    where ID = @ID ">


    <InsertParameters>
        <asp:Parameter Name="GarageCode" />
        <asp:Parameter Name="GarageName" />
        <asp:Parameter Name="Address" />
        <asp:Parameter Name="Province" />
        <asp:Parameter Name="Zipcode" />
        <asp:Parameter Name="TelCode" />
        <asp:Parameter Name="TelNo" />
        <asp:Parameter Name="FaxNo" />
        <asp:Parameter Name="Region" />
        <asp:Parameter Name="SalesArea" />
        <asp:Parameter Name="GarageType" />
        <asp:Parameter Name="Longitude" />
        <asp:Parameter Name="Latitude" />
    </InsertParameters>

    <UpdateParameters>
        <asp:Parameter Name="ID" />
        <asp:Parameter Name="GarageCode" />
        <asp:Parameter Name="GarageName" />
        <asp:Parameter Name="Address" />
        <asp:Parameter Name="Province" />
        <asp:Parameter Name="Zipcode" />
        <asp:Parameter Name="TelCode" />
        <asp:Parameter Name="TelNo" />
        <asp:Parameter Name="FaxNo" />
        <asp:Parameter Name="Region" />
        <asp:Parameter Name="SalesArea" />
        <asp:Parameter Name="GarageType" />
        <asp:Parameter Name="Longitude" />
        <asp:Parameter Name="Latitude" />
    </UpdateParameters>




</asp:SqlDataSource>
