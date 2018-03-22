<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucLWTConsentForm.ascx.vb" Inherits="Modules_ucLWTConsentForm" %>
<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true"  HeaderText=""  runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>


<dx:ASPxFileManager ID="ASPxFileManager1" runat="server" Width="100%" Height="400" Theme="Office2010Blue">


    <Settings AllowedFileExtensions=".jpg,.jpeg,.gif,.rtf,.txt,.png,.doc,.pdf,.xls,.xlsx" />


    <SettingsEditing AllowDownload="true" />

    <SettingsFileList View="Details">
    </SettingsFileList>


</dx:ASPxFileManager>

<script>
    function OnInit(s, e) {
        var elements = s.GetMainElement().getElementsByClassName("dxichCellSys");
        for (var i = 0; i < elements.length; i++) {
            elements[i].innerHTML = elements[i].innerHTML.replace(elements[i].innerText, "");
        }
    }
</script>

<dx:ASPxRadioButtonList ID="checkBoxList" runat="server" ReadOnly="true"  DataSourceID="SqlDataSource_uw"  TextWrap="false"
    RepeatColumns="3" 
    Width="550px"  ItemImage-Width="35"
    Border-BorderWidth="0" 
    ValueField="Unwriter" 
    ImageUrlField="url"  
    TextField="UWName">
    <ClientSideEvents Init="OnInit" />
</dx:ASPxRadioButtonList>

<%--<dx:ASPxDataView ID="ASPxDataView1" runat="server" DataSourceID="SqlDataSource_uw">
    <SettingsTableLayout RowsPerPage="1" />
    <ItemTemplate>
        <dx:ASPxImage ID="imgCover" runat="server" 
          ImageUrl='~/images/InsurerLogo/<%# Eval("Unwriter")%>.jpg' />
    </ItemTemplate>
</dx:ASPxDataView>--%>

<asp:SqlDataSource ID="SqlDataSource_uw" runat="server" ConnectionString="<%$ ConnectionStrings:MotorClaimConnectionString %>"
    SelectCommand="select rtrim(UWCode) as Unwriter, rtrim(UWCode) + ' - ' + rtrim(UWName) as UWName 
    ,'~/images/InsurerLogo/' +  rtrim(UWCode) + '.jpg' as url
    from Running
    where UWCode not in( 'dusit')
    
    "></asp:SqlDataSource>





            
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>
