<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PDFViewer.aspx.vb" Inherits="Modules_PDFViewer" %>
<%@ Register Assembly="PdfViewer" Namespace="PdfViewer" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>PDF Control Test Page</title>
    <meta http-equiv="Content-Type" content="text/html; charset=windows-874">
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" style="font-family: Calibri" bgcolor="#cccccc">
    <form id="form1" runat="server">
    <div>

        <asp:HyperLink ID="HyperLink1" Target="_blank" runat="server"  Style="z-index: 101;left: 24px; position: absolute; top: 0">Open PDF into New Page</asp:HyperLink>
        <cc1:ShowPdf ID="ShowPdf1" runat="server" BorderStyle="Inset" BorderWidth="2px" 
            Height="600px" Style="z-index: 103; left: 0; position: absolute; top: 30px"
            Width="100%" />
    
    </div>
    </form>
</body>
</html>