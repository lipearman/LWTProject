<%@ Page Language="VB" AutoEventWireup="false" CodeFile="B0021.aspx.vb" Inherits="Applications_Task_B0021" %>

<%@ Register Assembly="MultiTaskIndicator" Namespace="MultiTaskIndicator" TagPrefix="MTI" %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="Anthem" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
        window.onload = function () {
            document.getElementById('btnStart').click();
        }
    </script>
    <style>
        input {
          visibility: hidden;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <MTI:MultiTaskIndicator runat="server" ID="mtiTasks" ImageID="imgStatus" LabelID="lblStatus"
                ProcessingImageURL="images/ajaxloader.gif" TaskFinishedImageURL="images/checked.gif"
                Width="100%" AutoGenerateColumns="false" CancelButtonID="btnCancel">
                <HeaderStyle BackColor="navy" Font-Bold="true" ForeColor="white" HorizontalAlign="center" />
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50">
                        <ItemTemplate>
                            <asp:Image runat="server" ID="imgStatus" ImageUrl="images/arrow.gif" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Name" HeaderText="Dealer" ItemStyle-HorizontalAlign="left"   />

                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Status"  ItemStyle-Width="200" >
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblStatus" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </MTI:MultiTaskIndicator>
            <br />
            <br />
            <Anthem:Button runat="server" ID="btnStart" Text="Start!" />
            &nbsp;&nbsp;
           <Anthem:Button runat="server" ID="btnCancel" Visible="false" Text="Cancel" />
            &nbsp;&nbsp;
           <Anthem:Button runat="server" ID="btnReset"  Visible="false" Text="Reset" />


   
        </div>
    </form>
</body>


</html>
