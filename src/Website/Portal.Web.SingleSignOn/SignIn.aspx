﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SignIn.aspx.vb" Inherits="Portal.Web.SingleSignOn.SignIn1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CodePen - Pokemon Go - ZingTouch x Anime.js </title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
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

    <%--    <script>
        window.console = window.console || function (t) { };
    </script>--%>
</head>
<body translate="no">
   
        <section id="wrapper">


            <div id="touch-layer"></div>
            <div id="particles"></div>
            <div id="ring-screen">
                <div id="ring">
                    <div id="ring-active">
                        <div id="ring-fill"></div>
                    </div>
                </div>
            </div>
            <div id="screen" class="gradient-background">
                <div id="target-container">
                    <div id="target"></div>
                </div>
                <div id="ball"></div>
                <div id="output"></div>
            </div>


            <div id="capture-screen" class="gradient-background hidden">
                <div id="capture-status" class="hidden">
                    You caught Omanyte!
                </div>
                <div id="poof-container" class="hidden">
                    <div id="poof"></div>
                </div>
                <div id="capture-confetti"></div>
                <div id="capture-ball"></div>
                <div id="capture-ball-button-container" class="hidden">
                    <div id="capture-ball-button"></div>
                </div>
            </div>


        </section>
        <script src="js/stopExecutionOnTimeout.js"></script>
        <script src='js/anime.min.js'></script>
        <script src='js/zingtouch.min.js'></script>

        <script>
            var Screen = {
                height: window.innerHeight,
                width: window.innerWidth
            };
            var MAX_VELOCITY = Screen.height * 0.009;
            var Resources = {
                pokeball: 'img/pkmngo-pokeball.png',
                pokeballActive: 'img/pkmngo-pokeballactive.png',
                pokeballClosed: 'img/pkmngo-pokeballclosed.png'
            };

            var Ball = {
                id: 'ball',
                size: 50,
                x: 0,
                y: 0,
                inMotion: false,
                moveBall: function(x, y) {
                    Ball.x = x;
                    Ball.y = y;
                    var BallElement = document.getElementById(Ball.id);
                    BallElement.style.top = Ball.y + 'px';
                    BallElement.style.left = Ball.x + 'px';
                },
                getElement() {
        return document.getElementById(Ball.id);
            },
            resetBall: function() {
                Ball.moveBall(Screen.width / 2 - (Ball.size / 2), Screen.height - (Ball.size + 10));
                var BallElement = document.getElementById(Ball.id);
                BallElement.style.transform = "";
                BallElement.style.width = BallElement.style.height = Ball.size + 'px';
                BallElement.style.backgroundImage = "url('" + Resources.pokeball + "')";
                Ball.inMotion = false;
            },
            savePosition: function() {
                var ballEle = document.getElementById('ball');
                var ballRect = ballEle.getBoundingClientRect();
                ballEle.style.transform = "";
                ballEle.style.top = ballRect.top + 'px';
                ballEle.style.left = ballRect.left + 'px';
                ballEle.style.height = ballEle.style.width = ballRect.width + 'px';
            }
            };

            //Initial Setup

            resetState();

            //Move omanyte
            anime({
                targets: ['#target'],
                rotate: 20,
                duration: 800,
                loop: true,
                easing: 'easeInOutQuad',
                direction: 'alternate'
            });

            window.onresize = function() {
                Screen.height = window.innerHeight;
                Screen.width = window.innerWidth;
                MAX_VELOCITY = Screen.height * 0.009;
                resetState();
            }


            /* Gesture Bindings */
            var touchElement = document.getElementById('touch-layer');
            var touchRegion = new ZingTouch.Region(touchElement);
            var CustomSwipe = new ZingTouch.Swipe({
                escapeVelocity: 0.1
            })

            var CustomPan = new ZingTouch.Pan();
            var endPan = CustomPan.end;
            CustomPan.end = function(inputs) {
                setTimeout(function() {
                    if (Ball.inMotion === false) {
                        Ball.resetBall();
                    }
                }, 100);
                return endPan.call(this, inputs);
            }

            touchRegion.bind(touchElement, CustomPan, function(e) {
                Ball.moveBall(e.detail.events[0].x - Ball.size / 2, e.detail.events[0].y - Ball.size / 2);
            });

            touchRegion.bind(touchElement, CustomSwipe, function(e) {
                Ball.inMotion = true;
                var screenEle = document.getElementById('screen');
                var screenPos = screenEle.getBoundingClientRect();
                var angle = e.detail.data[0].currentDirection;
                var rawVelocity = velocity = e.detail.data[0].velocity;
                velocity = (velocity > MAX_VELOCITY) ? MAX_VELOCITY : velocity;

                //Determine the final position.
                var scalePercent = Math.log(velocity + 1) / Math.log(MAX_VELOCITY + 1);
                var destinationY = (Screen.height - (Screen.height * scalePercent)) + screenPos.top;
                var movementY = destinationY - e.detail.events[0].y;

                //Determine how far it needs to travel from the current position to the destination.
                var translateYValue = -0.75 * Screen.height * scalePercent;
                var translateXValue = 1 * (90 - angle) * -(translateYValue / 100);

                anime.remove('#ring-fill');

                anime({
                    targets: ['#ball'],
                    translateX: {
                        duration: 300,
                        value: translateXValue,
                        easing: 'easeOutSine'
                    },
                    translateY: {
                        value: movementY * 1.25 + 'px',
                        duration: 300,
                        easing: 'easeOutSine'
                    },
                    scale: {
                        value: 1 - (0.5 * scalePercent),
                        easing: 'easeInSine',
                        duration: 300
                    },
                    complete: function() {
                        if (movementY < 0) {
                            throwBall(movementY, translateXValue, scalePercent);
                        } else {
                            setTimeout(resetState, 400);
                        }
                    }
                })
                //End
            });

            function throwBall(movementY,translateXValue, scalePercent){
                //Treat translations as fixed.
                Ball.savePosition();
                anime({
                    targets: ['#ball'],
                    translateY: {
                        value: movementY * -0.5 + 'px',
                        duration: 400,
                        easing: 'easeInOutSine'
                    },
                    translateX: {
                        value: -translateXValue * 0.25,
                        duration: 400,
                        easing: 'linear'
                    },
                    scale: {
                        value: 1 - (0.25 * scalePercent),
                        easing: 'easeInSine',
                        duration: 400
                    },
                    complete: determineThrowResult
                })
            }

            function determineThrowResult() {
                //Determine hit-region
                var targetCoords = getCenterCoords('target');
                var ballCoords = getCenterCoords('ball');

                //Determine if the ball is touching the target.
                var radius = document.getElementById('target').getBoundingClientRect().width / 2;
                if (ballCoords.x > targetCoords.x - radius &&
                    ballCoords.x < targetCoords.x + radius &&
                    ballCoords.y > targetCoords.y - radius &&
                    ballCoords.y < targetCoords.y + radius) {

                    Ball.savePosition();
                    var ballOrientation = (ballCoords.x < targetCoords.x) ? -1 : 1;
                    anime({
                        targets: ['#ball'],
                        translateY: {
                            value: -1.15 * radius,
                            duration: 200,
                            easing: 'linear'
                        },
                        translateX: {
                            value: 1.15 * radius * ballOrientation,
                            duration: 200,
                            easing: 'linear'
                        },
                        scaleX: {
                            value: ballOrientation,
                            duration: 200,
                        },
                        complete: function() {
                            var ball = Ball.getElement();
                            ball.style.backgroundImage = "url('" + Resources.pokeballActive + "')";
                            emitParticlesToPokeball();
                        }
                    });
                } else {
                    setTimeout(resetState, 400);
                }
            }


            function emitParticlesToPokeball() {
                var particles = [];
                var targetEle = getCenterCoords('target');
                var ballEle = Ball.getElement();
                var ballRect = ballEle.getBoundingClientRect();
                var particleLeft;
                var particleRight;
                var palette = [
                    '#E4D3A8',
                    '#6EB8C0',
                    '#FFF',
                    '#2196F3'
                ]
                var particleContainer = document.getElementById('particles');
                for (var i = 0; i < 50; i++) {
                    var particleEle = document.createElement('div');
                    particleEle.className = 'particle';
                    particleEle.setAttribute('id', 'particle-' + i);;
                    particleLeft = getRandNum(-60, 60) + targetEle.x;
                    particleEle.style.left = particleLeft + 'px';
                    particleRight = getRandNum(-60, 60) + targetEle.y;
                    particleEle.style.top = particleRight + 'px';
                    particleEle.style.backgroundColor = palette[getRandNum(0, palette.length)]
                    particleContainer.appendChild(particleEle);
                    anime({
                        targets: ['#particle-' + i],
                        translateX: {
                            value: ballRect.left - particleLeft,
                            delay: 100 + (i * 10)
                        },
                        translateY: {
                            value: ballRect.top + (Ball.size / 2) - particleRight,
                            delay: 100 + (i * 10),
                        },
                        opacity: {
                            value: 0,
                            delay: 100 + (i * 10),
                            duration: 800,
                            easing: 'easeInSine'
                        }
                    });
                    anime({
                        targets: ['#target'],
                        opacity: {
                            value: 0,
                            delay: 200,
                            easing: 'easeInSine'
                        }
                    });
                }
                setTimeout(function() {
                    var ball = Ball.getElement();
                    ball.style.backgroundImage = "url('" + Resources.pokeballClosed + "')";
                    document.getElementById('particles').innerHTML = "";
                    Ball.savePosition();

                    anime({
                        targets: ['#ball'],
                        translateY: {
                            value: "200px",
                            delay: 400,
                            duration: 400,
                            easing: 'linear'
                        },
                        complete: function() {
                            Ball.resetBall();
                        }
                    });
                    setTimeout(function() {
                        animateCaptureState();
                        resetState();
                    }, 750);

                }, 1000);
            }



            function animateCaptureState() {
                var ballContainer = document.getElementById('capture-screen');
                ballContainer.classList.toggle('hidden');

                var duration = 500;
                anime({
                    targets: ['#capture-ball'],
                    rotate: 40,
                    duration: duration,
                    easing: 'easeInOutBack',
                    loop: true,
                    direction: 'alternate'
                });

                var ringRect = (document.getElementById('ring-active')).getBoundingClientRect();
                var successRate = ((150 - ringRect.width) / 150) * 100;
                var seed = getRandNum(0, 100);
                setTimeout(function() {

                    anime.remove('#capture-ball');

                    if (seed < Math.floor(successRate)) {
                        var captureBall = document.getElementById('capture-ball');
                        var buttonContainer = document.getElementById('capture-ball-button-container');
                        buttonContainer.classList.toggle('hidden');

                        //Captured
                        var captureStatus = document.getElementById('capture-status');
                        captureStatus.classList.toggle('hidden');
                        captureStatus.innerHTML = "Gotcha!"
                        makeItRainConfetti();

                        anime({
                            targets: ['#capture-ball-button-container'],
                            opacity: {
                                value: 0,
                                duration: 800,
                                easing: 'easeInSine'
                            },
                            complete: function() {
                                setTimeout(function() {
                                    var ballContainer = document.getElementById('capture-screen');
                                    ballContainer.classList.toggle('hidden');
                                    var buttonContainer = document.getElementById('capture-ball-button-container');
                                    buttonContainer.classList.toggle('hidden');
                                    buttonContainer.style.opacity = "";
                                    document.getElementById('capture-status').classList.toggle('hidden');
                                }, 800);
                            }
                        });

                    } else {
                        var poofContainer = document.getElementById('poof-container');
                        poofContainer.classList.toggle('hidden');

                        var captureStatus = document.getElementById('capture-status');
                        captureStatus.innerHTML = "No Pokemon!"
                        captureStatus.classList.toggle('hidden');

                        anime({
                            targets: ['#poof'],
                            scale: {
                                value: 20,
                                delay: 400,
                                easing: 'linear',
                                duration: 600
                            },
                            complete: function() {
                                var ballContainer = document.getElementById('capture-screen');
                                ballContainer.classList.toggle('hidden');

                                var poofEle = document.getElementById('poof');
                                poofEle.style.transform = "";
                                var poofContainer = document.getElementById('poof-container');
                                poofContainer.classList.toggle('hidden');

                                var captureStatus = document.getElementById('capture-status');
                                captureStatus.classList.toggle('hidden');
                            }
                        })
                    }
                }, duration * 6);

            }


            function makeItRainConfetti() {
                for (var i = 0; i < 100; i++) {
                    var particleContainer = document.getElementById('capture-confetti');
                    var particleEle = document.createElement('div');
                    particleEle.className = 'particle';
                    particleEle.setAttribute('id', 'particle-' + i);
                    particleLeft = window.innerWidth / 2;
                    particleEle.style.left = particleLeft + 'px';
                    particleTop = window.innerHeight / 2;
                    particleEle.style.top = particleTop + 'px';
                    particleEle.style.backgroundColor = ((getRandNum(0, 2)) ? '#FFF' : '#4aa6fb')
                    particleContainer.appendChild(particleEle);
                    anime({
                        targets: ['#particle-' + i],
                        translateX: {
                            value: ((getRandNum(0, 2)) ? -1 : 1) * getRandNum(0, window.innerWidth / 2),
                            delay: 100
                        },
                        translateY: {
                            value: ((getRandNum(0, 2)) ? -1 : 1) * getRandNum(0, window.innerHeight / 2),
                            delay: 100,
                        },
                        opacity: {
                            value: 0,
                            duration: 800,
                            easing: 'easeInSine'
                        },
                        complete: function() {
                            document.getElementById('capture-confetti').innerHTML = "";
                        }
                    });
                }
            }

            function toggleInfoScreen() {
                var infoScreen = document.getElementById('info-screen');
                var infoButton = document.getElementById('info-button');
                infoScreen.classList.toggle('hidden');
                infoButton.innerHTML = (infoScreen.className === 'hidden') ? "?" : 'X';
            }

            /* * * * * * * * * * * * *
            * Universal Helpers
            * * * * * * * * * * * * */
            function resetState() {
                Ball.resetBall();
                document.getElementById('target').style.opacity = 1;
                //Adjust Ring
                var ring = document.getElementById('ring-fill');
                ring.style.height = "150px";
                ring.style.width = "150px";
                anime({
                    targets: ['#ring-fill'],
                    height: "5px",
                    width: "5px",
                    duration: 3000,
                    loop: true,
                    easing: 'linear'
                })
            }

            function getCenterCoords(elementId) {
                var rect = document.getElementById(elementId).getBoundingClientRect();
                return {
                    x: rect.left + rect.width / 2,
                    y: rect.top + rect.height / 2
                };
            }

            function getRandNum(min, max) {
                return Math.floor(Math.random() * (max - min)) + min;
            }
            //# sourceURL=pen.js
        </script>


        <%--        <script>
            if (document.location.search.match(/type=embed/gi)) {
                window.parent.postMessage("resize", "*");
            }
        </script>--%>
    

    <form id="form1" runat="server">
    </form>
</body>
</html>
