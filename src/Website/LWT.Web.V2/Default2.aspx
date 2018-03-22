<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default2.aspx.vb" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  <link rel="stylesheet" media="all" href="js/jvectormap/jquery-jvectormap.css"/>

  <script src="js/jvectormap/assets/jquery-1.8.2.js"></script>
  <script src="js/jvectormap/jquery-jvectormap.js"></script>
  <script src="js/jvectormap/lib/jquery-mousewheel.js"></script>

  <script src="js/jvectormap/src/jvectormap.js"></script>

  <script src="js/jvectormap/src/abstract-element.js"></script>
  <script src="js/jvectormap/src/abstract-canvas-element.js"></script>
  <script src="js/jvectormap/src/abstract-shape-element.js"></script>

  <script src="js/jvectormap/src/svg-element.js"></script>
  <script src="js/jvectormap/src/svg-group-element.js"></script>
  <script src="js/jvectormap/src/svg-canvas-element.js"></script>
  <script src="js/jvectormap/src/svg-shape-element.js"></script>
  <script src="js/jvectormap/src/svg-path-element.js"></script>
  <script src="js/jvectormap/src/svg-circle-element.js"></script>
  <script src="js/jvectormap/src/svg-image-element.js"></script>
  <script src="js/jvectormap/src/svg-text-element.js"></script>

  <script src="js/jvectormap/src/vml-element.js"></script>
  <script src="js/jvectormap/src/vml-group-element.js"></script>
  <script src="js/jvectormap/src/vml-canvas-element.js"></script>
  <script src="js/jvectormap/src/vml-shape-element.js"></script>
  <script src="js/jvectormap/src/vml-path-element.js"></script>
  <script src="js/jvectormap/src/vml-circle-element.js"></script>
  <script src="js/jvectormap/src/vml-image-element.js"></script>

  <script src="js/jvectormap/src/map-object.js"></script>
  <script src="js/jvectormap/src/region.js"></script>
  <script src="js/jvectormap/src/marker.js"></script>

  <script src="js/jvectormap/src/vector-canvas.js"></script>
  <script src="js/jvectormap/src/simple-scale.js"></script>
  <script src="js/jvectormap/src/ordinal-scale.js"></script>
  <script src="js/jvectormap/src/numeric-scale.js"></script>
  <script src="js/jvectormap/src/color-scale.js"></script>
  <script src="js/jvectormap/src/legend.js"></script>
  <script src="js/jvectormap/src/data-series.js"></script>
  <script src="js/jvectormap/src/proj.js"></script>
  <script src="js/jvectormap/src/map.js"></script>


<script src="js/jvectormap/jquery-jvectormap-th-mill.js"></script>
<script>
    $(function () {
        $('#thailand-map').vectorMap({
            map: 'th_mill',

            markers: [
                  [16.01, 103.16 ],
                  [17.48, 101.72 ]
        ]
        });
    });
  </script>

</head>
<body>
    <form id="form1" runat="server">


<div id="thailand-map" style="width: 480px; height: 900px"></div>


    </form>
</body>
</html>
