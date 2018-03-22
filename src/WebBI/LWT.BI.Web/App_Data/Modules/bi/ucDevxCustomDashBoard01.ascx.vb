Imports Portal.Components
Imports System.Data.SqlClient
Imports System.Data
Imports DevExpress.Web

Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports Subgurim.Controles
Imports Subgurim.Controles.GoogleChartIconMaker
Imports System.Drawing
Imports Subgurim.Controles.Classes.UtilityLibrary.Options
Imports Subgurim.Controles.Classes.UtilityLibrary

Partial Class Modules_ucDevxCustomDashBoard01
    Inherits PortalModuleControl
    Public _province As String = ""
    'Public _province_regions As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(LWT.Website.webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")





        End If


        If Page.IsPostBack = False Then
            Session("Type_BP_Shop") = CmbType_BP_Shop.Value

            'Dim latlon = New GLatLng(10.2, 22)
            'GMap1.setCenter(latlon, 4)

            'Dim window = New GInfoWindow(latlon, "<b>infoWindow</b> example", True)
            'GMap1.Add(window)

            'GMap1.enableHookMouseWheelToZoom = True
            'GMap1.enableGKeyboardHandler = True


            'Dim latlng = New GLatLng(_item.Latitude, _item.Longitude)

            'GMap1.Version = "3"
            'GMap1.enableDragging = True
            'Dim latlng = New GLatLng(13.753558362796, 100.5029296875)
            ''Dim maptype = GMapType.GTypes.MapMaker_Normal
            'GMap1.setCenter(latlng)
            'GMap1.Language = "th"
            'GMap1.GZoom = 6
            'GMap1.enableHookMouseWheelToZoom = True
            ''GMap1.enableGKeyboardHandler = True


            ''Dim latlng As New GLatLng(46, 21)
            ''GMap1.setCenter(latlng, 4)
            'Dim puntos As New List(Of GLatLng)()
            'puntos.Add(New GLatLng(13.7648929, 100.3343991))
            'puntos.Add(New GLatLng(13.6670215, 100.4036861))
            'puntos.Add(New GLatLng(13.6988894, 100.4210604))
            'puntos.Add(New GLatLng(13.7733179, 100.4284178))
            'puntos.Add(New GLatLng(13.5995816, 100.4331316))
            'puntos.Add(New GLatLng(13.6955177, 100.4453018))
            'puntos.Add(New GLatLng(13.7207491, 100.4577015))
            'puntos.Add(New GLatLng(13.7207491, 100.4577015))
            'puntos.Add(New GLatLng(13.7604091, 100.471573))
            'puntos.Add(New GLatLng(13.7320727, 100.4814589))
            'puntos.Add(New GLatLng(13.7164911, 100.4874338))
            'puntos.Add(New GLatLng(13.7560243, 100.4986793))
            'puntos.Add(New GLatLng(13.7919952, 100.5004335))
            'puntos.Add(New GLatLng(13.6812411, 100.5037003))
            'puntos.Add(New GLatLng(13.7279708, 100.5087902))
            'puntos.Add(New GLatLng(13.7380091, 100.5098502))
            'puntos.Add(New GLatLng(13.7726943, 100.5099262))
            'puntos.Add(New GLatLng(13.6937454, 100.510007))
            'puntos.Add(New GLatLng(13.6191708, 100.5103542))
            'puntos.Add(New GLatLng(13.7514954, 100.5108479))
            'puntos.Add(New GLatLng(13.6954096, 100.5127381))
            'puntos.Add(New GLatLng(13.7171256, 100.5151327))
            'puntos.Add(New GLatLng(13.7262395, 100.5267991))
            'puntos.Add(New GLatLng(13.7516381, 100.5315687))
            'puntos.Add(New GLatLng(13.7568078, 100.5337793))
            'puntos.Add(New GLatLng(13.7401666, 100.5352367))
            'puntos.Add(New GLatLng(13.803181, 100.5393292))
            'puntos.Add(New GLatLng(13.7630761, 100.5433408))
            'puntos.Add(New GLatLng(13.7225158, 100.5540678))
            'puntos.Add(New GLatLng(13.8037421, 100.5546027))
            'puntos.Add(New GLatLng(13.7398583, 100.5594458))
            'puntos.Add(New GLatLng(13.7790391, 100.5739529))
            'puntos.Add(New GLatLng(13.8841935, 100.5774799))
            'puntos.Add(New GLatLng(13.8289265, 100.5943624))
            'puntos.Add(New GLatLng(13.7802488, 100.5999507))
            'puntos.Add(New GLatLng(13.9245384, 100.6038701))
            'puntos.Add(New GLatLng(13.6682906, 100.6048034))
            'puntos.Add(New GLatLng(13.7194781, 100.6129534))
            'puntos.Add(New GLatLng(13.6910654, 100.614025))
            'puntos.Add(New GLatLng(13.864387, 100.6146434))
            'puntos.Add(New GLatLng(13.693295, 100.645971))
            'puntos.Add(New GLatLng(13.7650282, 100.6473885))
            'puntos.Add(New GLatLng(13.8038957, 100.647812))
            'puntos.Add(New GLatLng(13.9213159, 100.683202))
            'puntos.Add(New GLatLng(13.793899, 100.692192))
            'puntos.Add(New GLatLng(13.7676357, 100.6928804))
            'puntos.Add(New GLatLng(13.8381942, 100.7295908))
            'puntos.Add(New GLatLng(13.8130488, 100.731339))
            'puntos.Add(New GLatLng(13.7277339, 100.7486314))
            'puntos.Add(New GLatLng(13.8559883, 100.8619689))



            'Dim poligono As New GPolygon(puntos, "557799", 3, 0.5, "237464", 0.5)
            'poligono.close()

            'Dim objJs = New StringBuilder()
            'objJs.Append("function addborder" & 0 & "()")
            'objJs.Append("{")
            'objJs.Append(poligono.ToString(GMap1.GMap_Id))
            'objJs.Replace("clickable:False", "clickable:false")
            ''  ' Replace incorrect False statement
            'objJs.Append("}")

            'GMap1.Add("addborder" & 0 & "();", True)
            'Dim objString = objJs.ToString()
            'Dim newstring = objString.Replace("[[", "[").Replace("]]", "]")
            'GMap1.Add(newstring)






            Using dc As New DataClasses_LWTReportsExt
                Dim _data = (From c In dc.V_CustomDashBoard01s Where c.Latitude IsNot Nothing Order By c.Aftersales_Opportunity Descending).ToList()
                Dim _id As Integer = 0

                Dim sb As New StringBuilder()

                For Each _item In _data
                    _id = _id + 1
                    sb.Append("{" & String.Format("coords: [{0}, {1}], name: '{2}.{3}',nonbpc:'{4}',nonbpa:'{5}',claimnotpaid:'{6}',aftersales:'{7}', status: 'nonbp'", _item.Latitude, _item.Longitude, _id, _item.AccidentPlace_F, _item.Non_BP_C, _item.Non_BP_A, _item.Claims_Incured_NotPaid, _item.Aftersales_Opportunity) & "},")


                    'Dim latlng = New GLatLng(_item.Latitude, _item.Longitude)

                    '        ''Dim oMarker As New GMarker(latlon)
                    '        ''GMap1.addGMarker(oMarker)


                    '        '_id = _id + 1

                    '        ' ''Dim icono = New GMarker(latlon)
                    '        'Dim Info = String.Format("ลำดับที่ : {0} <br> Non_BP_C : {1} <br> Non_BP_A : {2} <br>NotPaid : {3} <br>Aftersales : {4}", _id.ToString() & ". " & _item.AccidentPlace_F, _item.Non_BP_C, Convert.ToDecimal(_item.Non_BP_A).ToString("#,###,##0.00"), Convert.ToDecimal(_item.Claims_Incured_NotPaid).ToString("#,###,##0.00"), Convert.ToDecimal(_item.Aftersales_Opportunity).ToString("#,###,##0.00"))
                    '        ' ''Dim Window = New GInfoWindow(icono, Info, False, GListener.Event.mouseover)
                    '        ' ''GMap1.Add(Window)


                    '        'Dim icon2 As New GIcon()
                    '        'icon2.labeledMarkerIconOptions = New LabeledMarkerIconOptions(Color.Lime, Color.White, _id, Color.Red)
                    '        'Dim marker2 As New GMarker(latlon, icon2)
                    '        'Dim window2 As New GInfoWindow(marker2, Info)
                    '        'GMap1.Add(window2)


                    '        'Dim puntos As New List(Of GLatLng)()
                    '        'puntos.Add(latlon + New GLatLng(0, 8))
                    '        'puntos.Add(latlon + New GLatLng(-0.5, 4.2))
                    '        'puntos.Add(latlon)
                    '        'puntos.Add(latlon + New GLatLng(3.5, -4))
                    '        'puntos.Add(latlon + New GLatLng(4.79, +2.6))
                    '        'Dim poligono As New GPolygon(puntos, "557799", 3, 0.5, "237464", 0.5)
                    '        'poligono.close()
                    '        'GMap1.Add(poligono)








                    '        ''Dim icon As New GIcon()
                    '        ''icon.image = "http://gmaps-samples.googlecode.com/svn/trunk/markers/circular/greencirclemarker.png"
                    '        ''icon.iconSize = New GSize(32, 32)
                    '        ''icon.iconAnchor = New GPoint(16, 16)
                    '        ''icon.infoWindowAnchor = New GPoint(25, 7)


                    '        ''Dim iconOptions1 As New StyledIconOptions With { _
                    '        ''    .Text = "Hi", _
                    '        ''    .Color = Color.Blue, _
                    '        ''    .Fore = Color.Red, _
                    '        ''    .StarColor = Color.Green _
                    '        ''}

                    '        ''Dim icon1 As New StyledIcon(StyledIconType.Marker, iconOptions1)
                    '        ''Dim styledMarker1 As New StyledMarker(latlon, icon1)
                    '        'Dim iconOptions2 As New StyledIconOptions() With { _
                    '        '     .Text = "Hi, I'm a bubble!", _
                    '        '     .Color = Color.Orange, _
                    '        '     .Fore = Color.PaleGreen _
                    '        '}

                    '        'Dim icon2 As New StyledIcon(StyledIconType.Bubble, iconOptions2)

                    '        'Dim styledMarker2 As New StyledMarker(latlon, icon2)

                    '        ''Dim window1 As New GInfoWindow(styledMarker1, "You can user StyledMarker as any other marker!")
                    '        'Dim window2 As New GInfoWindow(styledMarker2, "You can user StyledMarker as any other marker!")

                    '        ''GMap1.Add(window1)
                    '        'GMap1.Add(window2)



















                    '        'Dim pinLetter = New PinLetter(_id, Color.Yellow, Color.Black)
                    '        ''Dim xPinLetter = New XPinLetter(PinShapes.pin_star, "B", Color.Blue, Color.White, Color.Chocolate)
                    '        ''Dim pinIcon = New PinIcon(PinIcons.home, Color.Cyan)
                    '        ''Dim xPinIcon = New XPinIcon(PinShapes.pin, PinIcons.home, Color.LightGreen, Color.BlueViolet)
                    '        ''Dim sPin = New SPin(0.5, -10, Color.Green, 8, PinFontStyle.normal, "C")

                    '        'GMap1.Add(New GMarker(latlon, New GMarkerOptions(New GIcon(pinLetter.ToString(), pinLetter.Shadow()))))
                    '        ''GMap1.Add(New GMarker(latlon + New GLatLng(2, 2), New GMarkerOptions(New GIcon(xPinLetter.ToString(), xPinLetter.Shadow()))))
                    '        ''GMap1.Add(New GMarker(latlon + New GLatLng(2, -2), New GMarkerOptions(New GIcon(pinIcon.ToString(), pinIcon.Shadow()))))
                    '        ''GMap1.Add(New GMarker(latlon + New GLatLng(-2, 2), New GMarkerOptions(New GIcon(xPinIcon.ToString(), xPinIcon.Shadow()))))
                    '        ''GMap1.Add(New GMarker(latlon + New GLatLng(-2, -2), New GMarkerOptions(New GIcon(sPin.ToString()))))









                    '        'Exit For
                    '        'Dim _GP As New GooglePoint()
                    '        '_GP.ID = System.Guid.NewGuid().ToString()
                    '        '_GP.Latitude = _item.Latitude
                    '        '_GP.Longitude = _item.Longitude

                    '        ''_GP.InfoHTML = String.Format("จังหวัด : {0} <br> Non_BP_C : {1} <br> Non_BP_A : {2} <br>NotPaid : {3} <br>Aftersales : {4}", _id.ToString() & ". " & _item.AccidentPlace_F, _item.Non_BP_C, Convert.ToDecimal(_item.Non_BP_A).ToString("#,###,##0.00"), Convert.ToDecimal(_item.Claims_Incured_NotPaid).ToString("#,###,##0.00"), Convert.ToDecimal(_item.Aftersales_Opportunity).ToString("#,###,##0.00"))

                    '        '_GP.InfoHTML = _GP.ID
                    '        '_GP.IconImage = "icons/RedCar.png"

                    '        ''_GP.ToolTip = String.Format("จังหวัด : {0} \n Non_BP_C : {1} \n Non_BP_A : {2} \n NotPaid : {3} \n Aftersales : {4}", _id.ToString() & ". " & _item.AccidentPlace_F, _item.Non_BP_C, Convert.ToDecimal(_item.Non_BP_A).ToString("#,###,##0.00"), Convert.ToDecimal(_item.Claims_Incured_NotPaid).ToString("#,###,##0.00"), Convert.ToDecimal(_item.Aftersales_Opportunity).ToString("#,###,##0.00"))

                    '        'GoogleMapForASPNet1.GoogleMapObject.Points.Add(_GP)
                Next


                _province = sb.ToString()
            End Using

        End If


    End Sub



    Protected Sub cbType_BP_Shop_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbType_BP_Shop.Callback

        Session("Type_BP_Shop") = e.Parameter

        e.Result = "success"
    End Sub
End Class
