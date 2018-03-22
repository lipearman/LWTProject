<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxFileDataPreview.ascx.vb" Inherits="Modules_Financial_ucDevxFileDataPreview" %>
<script type="text/javascript">
    function OnExceptionOccurred(s, e) {
        e.handled = true;
        alert(e.message);
        window.location.reload();
    }

</script>

<dx:ASPxSpreadsheet ID="Spreadsheet" ClientInstanceName="Spreadsheet"
    runat="server"  
    ShowFormulaBar="false"
    ShowSheetTabs="true"
    ActiveTabIndex="0"
    Width="100%"
    ShowConfirmOnLosingChanges="false">
    <ClientSideEvents 
         CallbackError="OnExceptionOccurred" 
         PopupMenuShowing="function(s,e){e.cancel = true;}"
        />
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


        <%--         <dx:RibbonTab  Text="PAGE LAYOUT">
            <Groups  >
 
                <dx:SRPageSetupGroup>
                    <Items>
                        <dx:SRFilePrintCommand></dx:SRFilePrintCommand>
                        <dx:SRPageSetupMarginsCommand></dx:SRPageSetupMarginsCommand>
                        <dx:SRPageSetupOrientationCommand></dx:SRPageSetupOrientationCommand>
                        <dx:SRPageSetupPaperKindCommand></dx:SRPageSetupPaperKindCommand>


 

                    </Items>
                </dx:SRPageSetupGroup>
            </Groups>
        </dx:RibbonTab>--%>
    </RibbonTabs>
</dx:ASPxSpreadsheet>
