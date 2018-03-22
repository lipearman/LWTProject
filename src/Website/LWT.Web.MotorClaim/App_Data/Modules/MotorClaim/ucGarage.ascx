<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucGarage.ascx.vb" Inherits="Modules_ucGarage" %>


<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Garage" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>
             
            <dx:ASPxGridView ID="gridData" 
                ClientInstanceName="gridData" 
                runat="server" 
                DataSourceID="SqlDataSource_gridData"
                KeyFieldName="GarageCode" 
                AutoGenerateColumns="False" 
                Width="100%" >
                 <Settings ShowGroupPanel="true" />
               <SettingsPager Mode="ShowPager" />

                <SettingsSearchPanel Visible="true" />

                <SettingsBehavior AllowDragDrop="True" 
                    AllowFocusedRow="True" 
                    EnableRowHotTrack="True"
                    ColumnResizeMode="Control" />


                <SettingsPager Mode="ShowPager" PageSize="15">
                    <PageSizeItemSettings Visible="false" Items="15, 30, 45" ShowAllItem="false" />
                </SettingsPager>


 

                <Columns>
 
                    <dx:GridViewDataColumn FieldName="GarageCode" Width="100" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
                    <dx:GridViewDataColumn FieldName="GarageName" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
                    <dx:GridViewDataColumn FieldName="Address" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
                    <dx:GridViewDataColumn FieldName="Province" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
                    <dx:GridViewDataColumn FieldName="GarageType" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
              
                 
                </Columns>
                 <GroupSummary>
                    <dx:ASPxSummaryItem FieldName="ShowRoomCode" SummaryType="Count"  />
                </GroupSummary>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter runat="server" ID="GridExporter" GridViewID="gridData" />

            <br />
            <dx:ASPxButton ID="btnExportData" runat="server" RenderMode="Button"
                Width="90px" Text="Export" AutoPostBack="false" CausesValidation="false">
                <Image IconID="export_exporttoxlsx_16x16"></Image>
            </dx:ASPxButton>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>


<asp:SqlDataSource ID="SqlDataSource_gridData" runat="server" ConnectionString="<%$ ConnectionStrings:MotorClaimConnectionString %>"
    
     SelectCommand="select 
        ID
        ,GarageCode
        ,GarageName
        ,Address
        ,Province
        ,GarageType   
     from V_Garage"
    
    >
    
</asp:SqlDataSource>
