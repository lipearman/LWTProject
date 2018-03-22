Imports Portal.Components
Imports System.Data.SqlClient
Imports System.Data
Imports DevExpress.Web

Partial Class Modules_ucDevxRawDataSetup
    Inherits PortalModuleControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If (Not IsPostBack) Then

            Session("VIEW_ID") = Nothing

        End If



    End Sub



    Protected Sub callbackPanel_view_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel_view.Callback

        Dim _VIEW_ID = e.Parameter.ToString()
        Session("VIEW_ID") = _VIEW_ID
        Using dc As New DataClasses_LWTReportsExt()
            Dim _v = (From c In dc.tblReport_VIEWs Where c.VIEW_ID = _VIEW_ID).FirstOrDefault()
            Using objConn As New SqlConnection(_v.VIEW_CONNECTION)
                Dim daAuthors As New SqlDataAdapter(_v.VIEW_QUERY, objConn)
                Dim dsPubs As New DataSet("MAIN")
                daAuthors.FillSchema(dsPubs, SchemaType.Source, "VIEW")
                For Each item As System.Data.DataColumn In dsPubs.Tables("VIEW").Columns
                    ASPxTokenFields.Items.Add(item.Caption)
                Next
            End Using

        End Using

        gridFields.DataBind()
    End Sub



    Protected Sub cbAddFields_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbAddFields.Callback

        Try
            If Not String.IsNullOrEmpty(e.Parameter) Then
                Dim _VIEW_ID = Session("VIEW_ID")
                Dim _Fields = e.Parameter.Split(",")


                Using dc As New DataClasses_LWTReportsExt()
                    Dim itemList As New List(Of tblReport_VIEWs_Field)

                    For Each item As String In _Fields
                        Dim _Field As New tblReport_VIEWs_Field
                        With _Field
                            .FIELD_CAPTION = item
                            .FIELD_NAME = item
                            .VIEW_ID = Convert.ToInt32(_VIEW_ID)


                            .SummaryType = 0
                            .CellFormat_FormatType = 1
                            .CellFormat_FormatString = "{0:N2}"

                            .UnboundColumnType = 0
                            .UnboundExpressionMode = 0
                            .UnboundExpression = ""

                            .GroupInterval = 0
                        End With
                        itemList.Add(_Field)
                    Next

                    dc.tblReport_VIEWs_Fields.InsertAllOnSubmit(itemList)

                    dc.SubmitChanges()

                    e.Result = "Done"

                End Using
            End If


        Catch ex As Exception
            e.Result = ex.Message
        End Try

    End Sub

End Class
