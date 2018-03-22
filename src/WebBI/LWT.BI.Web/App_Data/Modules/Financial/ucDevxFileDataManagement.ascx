<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxFileDataManagement.ascx.vb" Inherits="Modules_Financial_ucDevxFileDataManagement" %>
<script type="text/javascript">
    function OnExceptionOccurred(s, e) {
        e.handled = true;
        alert(e.message);
        window.location.reload();
    }
 

</script>





<%-- <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="false" 
     Image-IconID="actions_refresh_16x16office2013" 
     Text="Refresh">
    <ClientSideEvents Click="function(s,e){
         //Spreadsheet.SetFullscreenMode(true);
        Spreadsheet.Open(true);
    }" />
</dx:ASPxButton>--%>


<dx:ASPxSpreadsheet ID="Spreadsheet" ClientInstanceName="Spreadsheet"
    runat="server"   
    ShowFormulaBar="false"
    ShowSheetTabs="true"
    ActiveTabIndex="0" 
    Width="100%"  
    ShowConfirmOnLosingChanges="false">
    <ClientSideEvents   
        PopupMenuShowing="function(s,e){e.cancel = true;}"
        CallbackError="OnExceptionOccurred" />
    <RibbonTabs>
        <dx:RibbonTab Text="Menu">
            <Groups>
                <dx:SRFileCommonGroup>
                    <Items>
                        <dx:SRFileOpenCommand></dx:SRFileOpenCommand>
                        <dx:SRFullScreenCommand></dx:SRFullScreenCommand>


                        <dx:SRFilePrintCommand></dx:SRFilePrintCommand>
                        <dx:SRPageSetupMarginsCommand></dx:SRPageSetupMarginsCommand>
                        <dx:SRPageSetupOrientationCommand></dx:SRPageSetupOrientationCommand>
                        <dx:SRPageSetupPaperKindCommand></dx:SRPageSetupPaperKindCommand>




                        <dx:SRViewShowGridlinesCommand></dx:SRViewShowGridlinesCommand>
                        <dx:SRViewShowHeadingsCommand></dx:SRViewShowHeadingsCommand>
                    </Items>
                </dx:SRFileCommonGroup>
            </Groups>
        </dx:RibbonTab>
    </RibbonTabs>

</dx:ASPxSpreadsheet>
