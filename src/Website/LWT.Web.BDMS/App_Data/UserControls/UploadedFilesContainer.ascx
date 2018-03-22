<%@ Control Language="vb" %>
<script runat="server">

	Public Property Width() As Integer
	Public Property Height() As Integer
	Public Property NameColumnWidth() As Integer
	Public Property SizeColumnWidth() As Integer
	Public Property HeaderText() As String
	Public Property UseExtendedPopup() As Boolean
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		FilesRoundPanel.Width = Width
		FilesRoundPanel.Height = Height
		FilesRoundPanel.HeaderText = HeaderText
	End Sub
	Protected Function GetOptionsString() As String
		Return "'" & GetStyleAttributeValue(NameColumnWidth) & "', '" & GetStyleAttributeValue(SizeColumnWidth) & "', " & UseExtendedPopup.ToString().ToLower()
	End Function
	Protected Function GetStyleAttributeValue(ByVal width As Integer) As String
		Return If(width > 0, String.Format("width: {0}px; max-width: {0}px", width), String.Empty)
	End Function
 </script>
<script type="text/javascript">
	DXUploadedFilesContainer.ApplySettings(<%=GetOptionsString()%>);
</script>
<dx:ASPxRoundPanel ID="FilesRoundPanel" ClientInstanceName="FilesRoundPanel" runat="server">
	<PanelCollection>
		<dx:PanelContent runat="server">
			<table id="uploadedFilesContainer" class="uploadedFilesContainer">
				<tbody></tbody>
			</table>
		</dx:PanelContent>
	</PanelCollection>
</dx:ASPxRoundPanel>