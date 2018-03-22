<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="StartUp.aspx.vb" Inherits="Portal.Web.SingleSignOn.StartUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_blank" />



    <style>
        /*@import "css/style.css";*/

        * {
            box-sizing: border-box;
        }

        html,
        body {
            margin: 0;
            padding: 0;
            font-family: "Open Sans";
            width: 100%;
            height: 100%;
        }

        h1,
        p {
            margin: 0;
        }

        ul {
            margin: 0;
        }

        a {
            color: #47d3ff;
        }

        .gradient-background {
            background: #00c6ff;
            background: -webkit-linear-gradient(top, #1e5799 0%, #00c6ff 25%, #26c6da 55%, #7AE3EF 100%);
            background: linear-gradient(to bottom, #1e5799 0%, #00c6ff 25%, #26c6da 55%, #7AE3EF 100%);
        }

        #screen,
        #wrapper,
        #touch-layer,
        #waves,
        #info-screen {
            position: relative;
            height: 100%;
            width: 100%;
        }

        #title {
            position: fixed;
            text-align: center;
            width: 100%;
            color: white;
            margin-top: 0.5em;
            z-index: 11;
        }

        #screen,
        #capture-screen,
        #ring-screen {
            position: relative;
        }

        /* Accuracy ring */
        #ring-screen {
            height: 100%;
            width: 100%;
            position: absolute;
            z-index: 3;
        }

        #ring {
            display: flex;
            flex-direction: row;
            align-items: center;
            justify-content: center;
            width: 150px;
            height: 150px;
            border: 2px solid white;
            border-radius: 100px;
        }

        #ring-active {
            border: 2px solid #64DD17;
            border-radius: 100px;
        }

        #ring-fill {
            width: 150px;
            height: 150px;
        }

        /* Capture Screen */
        #capture-screen {
            display: flex;
            flex-direction: column;
            background-color: #222;
            z-index: 12;
        }

        #poof-container {
            position: fixed;
            z-index: 10;
            height: 100%;
            width: 100%;
            top: 0;
            left: 0;
        }

        #poof {
            width: 100%;
            height: 20px;
            opacity: 0.8;
            background-image: url("img/pkmngo-poof.svg");
            background-repeat: no-repeat;
            background-position: center center;
            background-size: contain;
        }

        #capture-ball {
            position: relative;
            width: 200px;
            height: 200px;
            background-image: url("img/pkmngo-pokeball.png");
            background-repeat: no-repeat;
            background-position: center center;
            background-size: cover;
        }

        #capture-ball-button-container {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            z-index: 5;
        }

        #capture-ball-button {
            width: 30px;
            height: 30px;
            margin-top: -20px;
            border-radius: 20px;
            opacity: 0.8;
            background-color: red;
        }

        #target-container {
            position: relative;
        }

        #target {
            width: 140px;
            height: 140px;
            transform: rotate(-10deg);
            background-image: url("img/pkmngo-helix.png");
            background-repeat: no-repeat;
            background-position: center center;
            background-size: cover;
            z-index: 2;
        }

        #ball {
            position: fixed;
            height: 50px;
            width: 50px;
            border-radius: 25px;
            bottom: 10%;
            background-image: url("img/pkmngo-pokeball.png");
            background-repeat: no-repeat;
            background-position: center center;
            background-size: cover;
            z-index: 3;
        }

        #touch-layer {
            position: fixed;
            top: 0;
            left: 0;
            opacity: 0;
            z-index: 10;
        }

        #capture-status {
            position: absolute;
            font-size: 1.5em;
            top: 0;
            left: 0;
            width: 100%;
            text-align: center;
            padding: 1em 0.2em;
            color: #FFF;
        }

        #particles {
            position: fixed;
            width: 100%;
            height: 100%;
            z-index: 1;
        }

        .particle {
            position: fixed;
            width: 7px;
            height: 7px;
            background-color: #fff;
            border-radius: 5px;
            opacity: 0.7;
            z-index: 1;
        }

        #capture-confetti {
            position: fixed;
            width: 100%;
            height: 100%;
            z-index: 6;
        }

            #capture-confetti > div.particle {
                height: 10px;
                width: 10px;
                opacity: 0.9;
                background-color: #4aa6fb;
            }

        /*Info screen*/
        #info-button {
            position: fixed;
            top: 5px;
            right: 5px;
            z-index: 15;
            color: button;
            width: 30px;
            height: 30px;
            color: #FFF;
            border: 2px solid #FFF;
            border-radius: 15px;
            cursor: pointer;
        }

        #info-screen {
            z-index: 14;
        }

        #info-shade {
            background-color: #222;
            opacity: 0.89;
        }

        #info-text {
            display: flex;
            flex-direction: column;
            padding: 1em;
            font-size: 0.9em;
            color: white;
        }

        .flex-center, #screen,
        #capture-screen,
        #ring-screen, #poof-container, #capture-ball-button-container, #target-container, #info-button {
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .fixed-full-width, #capture-screen, #info-screen, #info-shade, #info-text {
            position: fixed;
            width: 100%;
            height: 100%;
            top: 0;
            left: 0;
        }

        .hidden {
            display: none !important;
        }
    </style>


    <script>
        function OnClosePopup() {
            window.parent.LetStart.Hide();
        }
        function OnStartup() {



            cbStartUp.PerformCallback('');



        }
    </script>
</head>
<body class="gradient-background">

    <form id="form1" runat="server">

        <p align="center" style="position: absolute; left: 25%; right: 25%; bottom: 25%;top:25%">
           <%-- Do you want to play the game?<br>

            <input type="Submit" name="StartQuiz" value="Yes" onclick="OnStartup(); return false;">

            <input type="Submit" name="LogOut" value="No" onclick="OnClosePopup(); return false;">--%>
          <a href="#" onclick="OnStartup(); return false;"><img src="images/cutting_red_ribbon.png" /></a>
        </p>

        <dx:ASPxCallback ID="cbStartUp" runat="server" ClientInstanceName="cbStartUp">
            <ClientSideEvents CallbackComplete="function(s,e){ 
                    document.location = e.result;

                    //alert(e.result);
                }" />
        </dx:ASPxCallback>

    </form>
</body>
</html>
