<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Firework.aspx.vb" Inherits="Portal.Web.SingleSignOn.Firework" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.4.1.js"></script>
    <script src="Scripts/jquery.fireworks.js"></script>
    
    <script src="Scripts/audiojs/audio.min.js"></script>

    <script> 
        $(document).ready(function () {
            $('div.bg').fireworks();
        });

        audiojs.events.ready(function () {
            audiojs.createAll();
        });
    </script>


    <style>
        .audiojs { background: transparent; box-shadow: none; }
        .audiojs div { display: none; }
    </style>
</head>
<body style="background-color:black;" >
    <form id="form1" runat="server">
    <div class="bg" style="width: 100%; height: 100%; background: #000;">
            <div style="width: 300px;height:450px"></div>
    </div>
        <audio src="mp3/firework.mp3" autoplay></audio>



      <marquee scrolldelay="60" style="position:fixed; top:20%;width:100%" > <h1 style="color:orange;font-size:80px;text-shadow:inherit" >Welcome to the year of innovation - LWT's got talent.</h1></marquee>
    </form>
</body>
</html>
