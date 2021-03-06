﻿<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxMeasure.ascx.vb" Inherits="Modules_ucDevxMeasure" %>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="RawData" runat="server" Width="800">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxGridView ID="grid" 
                ClientInstanceName="grid" 
                runat="server" 
                DataSourceID="SqlDataSource_Data" 
                AutoGenerateColumns="False" 
                Width="100%"                
                 SettingsPager-Mode="ShowAllRecords"
                >
                <Settings ShowGroupPanel="True" />
                <Columns> 
                    <dx:GridViewDataTextColumn FieldName="CUBE_ID" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="DATABASE" Settings-AllowHeaderFilter="True" SettingsHeaderFilter-Mode="CheckedList" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="CUBE"  Settings-AllowHeaderFilter="True" SettingsHeaderFilter-Mode="CheckedList" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="BASE_CUBE_NAME"  Settings-AllowHeaderFilter="True" SettingsHeaderFilter-Mode="CheckedList" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>


                    <dx:GridViewDataTextColumn FieldName="FOLDER" Settings-AllowHeaderFilter="True" SettingsHeaderFilter-Mode="CheckedList" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>


                    <dx:GridViewDataTextColumn FieldName="ATTRIBUTE" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="FIELD"  Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="VISIBLE"   Settings-AllowGroup="False" Settings-AllowHeaderFilter="True" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="FormatType"  Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
      
                </Columns>
            </dx:ASPxGridView>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_Data" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select 
     tblCube.CUBE_ID
    ,tblCube.[DATABASE]
    ,tblCube.[CUBE]
    ,tblCube.[BASE_CUBE_NAME]
    ,tblMeasure.[FOLDER]
    ,tblMeasure.[ATTRIBUTE]
    ,tblMeasure.[FIELD]
    ,tblMeasure.[VISIBLE]
    ,tblMeasure.[FormatType] 
    from tblMeasure
    inner join tblCube on tblMeasure.CUBE_ID = tblCube.CUBE_ID
    " 
    
    
    >  
</asp:SqlDataSource>
