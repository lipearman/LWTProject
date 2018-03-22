<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default4.aspx.vb" Inherits="Default4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <dx:ASPxButton ID="btnDownloadFormat" runat="server" Text="Import Format" Image-IconID="export_exporttoxlsx_16x16" CausesValidation="false" AutoPostBack="false">
                <ClientSideEvents Click="function(s,e){  
                    //e.processOnServer = true;
















                    cbPreview.PerformCallback('xxxx');

                }" />

            </dx:ASPxButton>




<dx:ASPxCallback ID="cbPreview" runat="server" ClientInstanceName="cbPreview">
  
</dx:ASPxCallback>








        </div>
    </form>
</body>
</html>
