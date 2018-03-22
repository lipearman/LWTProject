<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucLWTDealer.ascx.vb" Inherits="Modules_ucLWTDealer" %>


<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true"  HeaderText=""  runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>




            <dx:ASPxGridView ID="gridData" ClientInstanceName="gridData" runat="server"
                DataSourceID="SqlDataSource_tblDealer"
                SettingsBehavior-ColumnResizeMode="Control"
                Settings-HorizontalScrollBarMode="Visible"
                KeyFieldName="AgentCode" 
  
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
                    <EditForm Modal="true" Width="600" MinHeight="200" HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
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
                    <dx:GridViewDataTextColumn FieldName="AgentCode" SortOrder="Ascending" Width="100" CellStyle-HorizontalAlign="Center" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="DealerNameEN" Width="200" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="DealerNameTH" Width="200" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Email" Width="300" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"></dx:GridViewDataTextColumn>
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



<asp:SqlDataSource ID="SqlDataSource_tblDealer" runat="server" ConnectionString="<%$ ConnectionStrings:MotorClaimConnectionString %>"
    SelectCommand="select * from tblDealer "
    InsertCommand="insert into tblDealer(
     AgentCode
    ,DealerNameEN
    ,DealerNameTH
    ,Email
    ) 

    values(
     @AgentCode
    ,@DealerNameEN
    ,@DealerNameTH
    ,@Email
    )
    "
    UpdateCommand="UpDate tblDealer
    SET DealerNameEN = @DealerNameEN
    , DealerNameTH = @DealerNameTH
    , Email = @Email
    where AgentCode = @AgentCode ">


    <InsertParameters>
        <asp:Parameter Name="AgentCode" />
        <asp:Parameter Name="DealerNameEN" />
        <asp:Parameter Name="DealerNameTH" />
        <asp:Parameter Name="Email" />
    </InsertParameters>

    <UpdateParameters>
        <asp:Parameter Name="AgentCode" />
        <asp:Parameter Name="DealerNameEN" />
        <asp:Parameter Name="DealerNameTH" />
        <asp:Parameter Name="Email" />
    </UpdateParameters>




</asp:SqlDataSource>
