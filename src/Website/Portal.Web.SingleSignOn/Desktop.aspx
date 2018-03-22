<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Desktop.aspx.vb" Inherits="Portal.Web.SingleSignOn.desktop" %>

<%@ Import Namespace="System.Security.Principal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-US" class="no-js">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" media="screen" href="res/css/style.css" />

    <script type="text/javascript" src="res/js/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="res/js/jquery-ui-1.8.17.custom.min.js"></script>
    <script type="text/javascript" src="res/js/modernizr.js"></script>
    <script type="text/javascript" src="res/js/fix-and-clock.js"></script>

    <script type="text/javascript">
         <!--
    function term_con_display() {
        popupTerms.Show();
    }


    function goto_url(id) {
        cbgoto_url.PerformCallback(id);
    }
    //-->
    </script>




</head>
<body>
    <form id="form1" runat="server">




        <!-- DESKTOP -->
        <header id="head" class="vis">
            <nav id="menu">
                <ul>
                    <li class="apple">
                        <a href="#">Menu</a>
                        <ul class="sublist">
                            <li>
                                <dx:ASPxHyperLink ID="ASPxHyperLink2" runat="server" Text="Innovation Game">
                                    <ClientSideEvents Click="function(s,e){
                                               
Pokemon.SetContentUrl('innovation/Pokemon.aspx');
Pokemon.Show();
                                            }" />
                                </dx:ASPxHyperLink>

                            </li>

                            <li>
                                <dx:ASPxHyperLink ID="ASPxHyperLink4" runat="server" Text="How to Play">
                                    <ClientSideEvents Click="function(s,e){
popupHow2Play.Show();
                                            }" />
                                </dx:ASPxHyperLink>

                            </li>

                            <li>

                                <dx:ASPxHyperLink ID="ASPxHyperLink3" runat="server" Text="Top Players">
                                    <ClientSideEvents Click="function(s,e){
                                            
TopPlayers.SetContentUrl('innovation/TopPlayers.aspx');
TopPlayers.Show();

                                            }" />
                                </dx:ASPxHyperLink>

                            </li>
                            <li>

                                <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text="Terms & Conditions">
                                    <ClientSideEvents Click="function(s,e){
                                                popupTerms.Show();
                                            }" />
                                </dx:ASPxHyperLink>

                            </li>
                            <li class="divider"></li>
                            <li><a href="SignOff.aspx">Log Off</a>


                            </li>

                        </ul>
                    </li>

                </ul>
            </nav>



            <nav id="menu-dx">
                <ul>
                    <li class="wireless">
                        <a href="#all">Wireless</a>
                    </li>
                    <li class="time">
                        <ul>
                            <li id="DateAbbr"></li>
                            <li class="hour"></li>
                            <li class="point">:</li>
                            <li class="mins"></li>
                        </ul>
                    </li>
                    <li class="username">
                        <a href="#all"><%=HttpContext.Current.User.Identity.Name.ToString() %></a>
                    </li>
                </ul>
            </nav>
        </header>

        <%--            <div id="spec" class="window spec windows-vis">
                <nav class="control-window">
                    <a href="#spec" class="close" data-rel="close">close</a>
                </nav>
                <h1 class="titleInside">Terms & Conditions</h1>
                <div class="container">
                    <div class="container-inside">
                        <div class="about-this">
                            <p>กรุณาอ่านข้อกำหนดและเงื่อนไขการใช้งานเว็บไซต์เหล่านี้ (“ข้อกำหนด”)</p>

                            <p style="text-align: left">เว็บไซต์นี้เป็นเจ้าของและดำเนินการโดยบริษัท บริษัท ล็อคตั้น วัฒนา อินชัวรันส์ โบรคเกอร์ส (ประเทศไทย) จำกัด  เมื่อท่านใช้งานเว็บไซต์นี้ หรือเว็บไซต์อื่นใดของเรา รวมทั้งไซต์ทางมือถือและหน้าเพจสื่อสังคมออนไลน์ (เรียกรวมว่า “เว็บไซต์”) ท่านตกลงที่จะปฏิบัติตามข้อกำหนดและเงื่อนไขการใช้งานเหล่านี้</p>

                            <p style="text-align: left">หากท่านไม่ตกลงยอมรับข้อกำหนดและเงื่อนไขการใช้งาน กรุณาอย่าใช้งานเว็บไซต์นี้</p>

                            <p style="text-align: left">เราอาจแก้ไขข้อกำหนดเป็นบางครั้ง ในกรณีที่เราเปลี่ยนแปลงแก้ไขข้อกำหนดเหล่านี้ เราจะแจ้งให้ท่านทราบโดยการลงข้อกำหนดที่ทบทวนแล้วบนเว็บไซต์ การแก้ไขเปลี่ยนแปลงใดๆ ในข้อกำหนดเหล่านี้จะมีผลในทันทีเมื่อมีการลงประกาศข้อกำหนดที่ทบทวนแล้วในเว็บไซต์ หากท่านไม่ตกลงยอมรับการเปลี่ยนแปลงแก้ไขในข้อกำหนดเหล่านี้ ท่านไม่ควรใช้งานเว็บไซต์นี้ต่อไปอีก การใช้งานเว็บไซต์ต่อไปหลังจากมีการเปลี่ยนแปลงเนื้อหาในข้อกำหนดจะถือว่าท่านยอมรับการแก้ไขเปลี่ยนแปลงนั้น กรุณากลับไปที่เว็บไซต์เป็นระยะเพื่อทบทวนข้อกำหนดฉบับปัจจุบัน</p>

                            <p style="text-align: left">โปรดทราบว่าการใช้งานเว็บไซต์ของท่านหรือเนื้อหาและคุณลักษณะเฉพาะของเว็บไซต์อาจอยู่ภายใต้บังคับของข้อกำหนดเพิ่มเติมที่อธิบายไว้ในเอกสารนี้ (“ข้อกำหนดเพิ่มเติม”) รวมถึงแต่ไม่จำกัดเพียง การชิงโชค การแข่งขัน และกิจกรรมพาณิชย์อิเล็กทรอนิกส์ (E-Commerce) เว้นแต่จะระบุไว้เป็นอย่างอื่น ในกรณีที่ข้อกำหนดใดๆ เหล่านี้ขัดกับข้อกำหนดเพิ่มเติม ให้ข้อกำหนดเหล่านี้มีผลใช้บังคับ เมื่อใช้งานเว็บไซต์หรือเนื้อหาและคุณลักษณะดังกล่าวแล้ว ถือว่าท่านตกลงยอมรับการมีผลผูกพันกับข้อกำหนดเพิ่มเติมเหล่านั้นด้วย</p>

                            <p style="text-align: left">เว็บไซต์นี้ได้รับการบังคับและดำเนินการตามกฎหมายของประเทศไทย ขณะที่ออกแบบมาเพื่อให้เทียบเคียงได้กับกฎหมายของประเทศต่างๆ ที่เราดำเนินงานอยู่ด้วย แม้ว่าข้อกำหนดเหล่านี้จะใช้บังคับควบคุมการใช้งานเว็บไซต์โดยท่าน ท่านอาจมีสิทธิได้รับการคุ้มครองตามสิทธิคุ้มครองผู้บริโภคบางประการภายใต้กฎหมายของเขตอำนาจศาลในท้องถิ่นของท่านด้วย หากท่านอาศัยอยู่นอกสหรัฐอเมริกา กฎหมายในประเทศที่พำนักอาศัยของท่านอาจนำมาบังคับใช้กับการทำธุรกรรมทางออนไลน์ใดๆ ที่ท่านร่วมทำกับเรา</p>
                        </div>
                    </div>
                </div>
            </div>--%>




        <!-- DOCK -->
        <dx:ASPxCallback ID="cbgoto_url" runat="server" ClientInstanceName="cbgoto_url">
            <ClientSideEvents CallbackComplete="function(s,e){
                      var result = e.result;

                      if(result.indexOf('http')==-1)
                      {
                        alert(result);
                      }
                      else
                      {
                        window.open(result);
                      }
                  }" />

        </dx:ASPxCallback>
        <div class="dock">

            <ul>
                <%=_dockmenu%>
                <%--<li id="finderr">
                    <a href="#warning" data-rel="showOp">
                        <em><span>Finder</span></em>
                        <img src="res/img/FinderIcon.png" alt="Finder" />
                    </a>
                </li>
                <li id="launchPad">
                    <a href="#warning" data-rel="showOp">
                        <em><span>Launchpad</span></em>
                        <img src="res/img/launchPad.png" alt="Launchpad" />
                    </a>
                </li>
                <li id="expose">
                    <a href="#warning" data-rel="showOp">
                        <em><span>Mission Control</span></em>
                        <img src="res/img/expose.png" alt="Mission Control" />
                    </a>
                </li>
                <li id="appStore">
                    <a href="#warning" data-rel="showOp">
                        <em><span>App Store</span></em>
                        <img src="res/img/appstore.png" alt="App Store" />
                    </a>
                </li>
                <li id="safari">
                    <a href="#warning" data-rel="showOp">
                        <em><span>Safari</span></em>
                        <img src="res/img/Safari.png" alt="Safari" />
                    </a>
                </li>
                <li id="iChat">
                    <a href="#warning" data-rel="showOp">
                        <em><span>iChat</span></em>
                        <img src="res/img/ichat.png" alt="iChat" />
                    </a>
                </li>
                <li id="facetime">
                    <a href="#warning" data-rel="showOp">
                        <em><span>FaceTime</span></em>
                        <img src="res/img/facetime.png" alt="Facetime" />
                    </a>
                </li>
                <li id="addressBook">
                    <a href="#warning" data-rel="showOp">
                        <em><span>Address Book</span></em>
                        <img src="res/img/address.png" alt="Address Book" />
                    </a>
                </li>
                <li id="preview">
                    <a href="#warning" data-rel="showOp">
                        <em><span>Preview</span></em>
                        <img src="res/img/preview.png" alt="Preview" />
                    </a>
                </li>
                <li id="iTunes">
                    <a href="#warning" data-rel="showOp">
                        <em><span>iTunes</span></em>
                        <img src="res/img/iTunes.png" alt="iTunes" />
                    </a>
                </li>
                <li id="preferences">
                    <a href="#warning" data-rel="showOp">
                        <em><span>System Preferences</span></em>
                        <img src="res/img/preferences.png" alt="System Preferences" />
                    </a>
                </li>
                <li id="Li1">
                    <a href="#trash" data-rel="showOpTrash">
                        <em><span>Trash</span></em>
                        <img src="res/img/trash.png" alt="Trash" />
                    </a>
                </li>--%>
            </ul>


        </div>



        <dx:ASPxPopupControl ID="popupTerms"
            ClientInstanceName="popupTerms"
            runat="server"
            Width="500"
            CloseAction="CloseButton"
            AutoUpdatePosition="true"
            PopupHorizontalAlign="WindowCenter"
            Top="40"
            AllowDragging="true"
            HeaderText="Terms & Conditions">
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">

                    <div class="container">
                        <div class="container-inside">
                            <div class="about-this">
                                <p>กรุณาอ่านข้อกำหนดและเงื่อนไขการใช้งานเว็บไซต์เหล่านี้ (“ข้อกำหนด”)</p>

                                <p style="text-align: left">เว็บไซต์นี้เป็นเจ้าของและดำเนินการโดยบริษัท บริษัท ล็อคตั้น วัฒนา อินชัวรันส์ โบรคเกอร์ส (ประเทศไทย) จำกัด  เมื่อท่านใช้งานเว็บไซต์นี้ หรือเว็บไซต์อื่นใดของเรา รวมทั้งไซต์ทางมือถือและหน้าเพจสื่อสังคมออนไลน์ (เรียกรวมว่า “เว็บไซต์”) ท่านตกลงที่จะปฏิบัติตามข้อกำหนดและเงื่อนไขการใช้งานเหล่านี้</p>

                                <p style="text-align: left">หากท่านไม่ตกลงยอมรับข้อกำหนดและเงื่อนไขการใช้งาน กรุณาอย่าใช้งานเว็บไซต์นี้</p>

                                <p style="text-align: left">เราอาจแก้ไขข้อกำหนดเป็นบางครั้ง ในกรณีที่เราเปลี่ยนแปลงแก้ไขข้อกำหนดเหล่านี้ เราจะแจ้งให้ท่านทราบโดยการลงข้อกำหนดที่ทบทวนแล้วบนเว็บไซต์ การแก้ไขเปลี่ยนแปลงใดๆ ในข้อกำหนดเหล่านี้จะมีผลในทันทีเมื่อมีการลงประกาศข้อกำหนดที่ทบทวนแล้วในเว็บไซต์ หากท่านไม่ตกลงยอมรับการเปลี่ยนแปลงแก้ไขในข้อกำหนดเหล่านี้ ท่านไม่ควรใช้งานเว็บไซต์นี้ต่อไปอีก การใช้งานเว็บไซต์ต่อไปหลังจากมีการเปลี่ยนแปลงเนื้อหาในข้อกำหนดจะถือว่าท่านยอมรับการแก้ไขเปลี่ยนแปลงนั้น กรุณากลับไปที่เว็บไซต์เป็นระยะเพื่อทบทวนข้อกำหนดฉบับปัจจุบัน</p>

                                <p style="text-align: left">โปรดทราบว่าการใช้งานเว็บไซต์ของท่านหรือเนื้อหาและคุณลักษณะเฉพาะของเว็บไซต์อาจอยู่ภายใต้บังคับของข้อกำหนดเพิ่มเติมที่อธิบายไว้ในเอกสารนี้ (“ข้อกำหนดเพิ่มเติม”) รวมถึงแต่ไม่จำกัดเพียง การชิงโชค การแข่งขัน และกิจกรรมพาณิชย์อิเล็กทรอนิกส์ (E-Commerce) เว้นแต่จะระบุไว้เป็นอย่างอื่น ในกรณีที่ข้อกำหนดใดๆ เหล่านี้ขัดกับข้อกำหนดเพิ่มเติม ให้ข้อกำหนดเหล่านี้มีผลใช้บังคับ เมื่อใช้งานเว็บไซต์หรือเนื้อหาและคุณลักษณะดังกล่าวแล้ว ถือว่าท่านตกลงยอมรับการมีผลผูกพันกับข้อกำหนดเพิ่มเติมเหล่านั้นด้วย</p>

                                <p style="text-align: left">เว็บไซต์นี้ได้รับการบังคับและดำเนินการตามกฎหมายของประเทศไทย ขณะที่ออกแบบมาเพื่อให้เทียบเคียงได้กับกฎหมายของประเทศต่างๆ ที่เราดำเนินงานอยู่ด้วย แม้ว่าข้อกำหนดเหล่านี้จะใช้บังคับควบคุมการใช้งานเว็บไซต์โดยท่าน ท่านอาจมีสิทธิได้รับการคุ้มครองตามสิทธิคุ้มครองผู้บริโภคบางประการภายใต้กฎหมายของเขตอำนาจศาลในท้องถิ่นของท่านด้วย หากท่านอาศัยอยู่นอกสหรัฐอเมริกา กฎหมายในประเทศที่พำนักอาศัยของท่านอาจนำมาบังคับใช้กับการทำธุรกรรมทางออนไลน์ใดๆ ที่ท่านร่วมทำกับเรา</p>
                            </div>
                        </div>
                    </div>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>


        <dx:ASPxPopupControl ID="Pokemon"
            runat="server"
            AllowDragging="True"
            AllowResize="True"
            CloseAction="CloseButton"
            ContentUrl="~/innovation/Pokemon.aspx"
            EnableViewState="False"
            PopupElementID="popupArea"
            PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter"
            ShowFooter="True"
            Width="400px"
            Height="600px"
            FooterText="Try to resize the control using the resize grip or the control's edges"
            HeaderText="LWT’s got talent (Innovation) "
            ClientInstanceName="Pokemon"
            EnableHierarchyRecreation="True">
        </dx:ASPxPopupControl>


        <dx:ASPxPopupControl ID="TopPlayers"
            runat="server"
            AllowDragging="True"
            AllowResize="True"
            CloseAction="CloseButton"
            ShowMaximizeButton="true"
            EnableViewState="False"
            PopupElementID="popupArea"
            PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter"
            ContentUrl="~/innovation/TopPlayers.aspx"
            ShowFooter="True"
            Width="800px"
            Height="600px"
            FooterText="Try to resize the control using the resize grip or the control's edges"
            HeaderText="Top Players"
            ClientInstanceName="TopPlayers"
            EnableHierarchyRecreation="True">
        </dx:ASPxPopupControl>


        <dx:ASPxPopupControl ID="LetStart"
            runat="server"
            AllowDragging="True"
            AllowResize="True"
            CloseAction="CloseButton"
            EnableViewState="False"
            PopupElementID="popupArea"
            PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter"
            ShowFooter="True"
            ShowMaximizeButton="true"
            Maximized="true"
            FooterText="Try to resize the control using the resize grip or the control's edges"
            HeaderText="LWT’s got talent (Innovation) "
            ContentUrl="~/innovation/Startup.aspx"
            ClientInstanceName="LetStart"
            EnableHierarchyRecreation="True">
        </dx:ASPxPopupControl>


        <dx:ASPxPopupControl ID="popupHow2Play" ClientInstanceName="popupHow2Play"
            runat="server"
            AllowDragging="True"
            AllowResize="True"
            ShowFooter="True"
            Width="600"
            Height="600"
            CloseAction="CloseButton"
            AutoUpdatePosition="true"
            PopupHorizontalAlign="WindowCenter"
            Top="40"
            ShowPageScrollbarWhenModal="true"
            ScrollBars="Vertical"
            FooterText="Try to resize the control using the resize grip or the control's edges"
            HeaderText="Innovation Game - How to Play">
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">

                    <div class="container">
                        <div class="container-inside">
                            <div class="about-this">
                                <h1>กติกา</h1>
                                <br />
                                <table>
                                    <tr>

                                        <td style="vertical-align: top; text-align: left">
                                            <p>
                                                - หลังจากที่เปิดงานแล้ว พนักงานจะมีเวลาเล่นได้จนถึงเวลา 12.00 น. ของวันที่ 2 กันยายน 2559<br />
                                                - หลังจากที่มีการโยนครั้งแรก ผู้เล่นจะมีเวลาในการเล่นเกมส์ 5 นาที โดยไม่จำกัดจำนวนครั้ง
                                                <br />
                                                - ผู้ที่ได้คะแนนสูงสุด 3 อันดับแรกจะได้รับของรางวัลตามที่ระบุไว้ใน Intranet Website<br />
                                            </p>
                                        </td>
                                    </tr>


                                </table>
                                <br />
                                <br />

                                <h1>วิธีการเล่น</h1>
                                <br />
                                <table>
                                    <tr>

                                        <td style="vertical-align: top; text-align: left">(เล่นเกมส์ ให้กดปุ่ม <b style="color: blue"><a href="javascript:void(0)" onclick="Pokemon.SetContentUrl('innovation/Pokemon.aspx');Pokemon.Show();">Play Game</a></b> ด้านล่าง 
                                              หรือ เลือกจากเมนู <b style="color: blue"><a href="javascript:void(0)" onclick="Pokemon.SetContentUrl('innovation/Pokemon.aspx');Pokemon.Show();">Innovation Game</a></b> ด้านบน)
                                            <br />
                                            <br />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="vertical-align: top; text-align: left">

                                            <b>1. คลิกที่ลูกบอลแล้วค้างไว้
                                                <br />
                                                2. ลากลูกบอลขึ้นแล้วปล่อย ให้ลูกบอลเข้าไปอยู่ในวงกลมสีเขียว </b>
                                            <center><img src="Innovation/img/how2play1.png" /></center>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td style="vertical-align: top; text-align: left">
                                            <b>3. รอผลจากลูกบอล</b>
                                            <center><img src="Innovation/img/how2play2.png" /></center>

                                        </td>
                                    </tr>
                                    <tr>

                                        <td style="vertical-align: top; text-align: left">
                                            <b>4. ถ้าจับได้จะแสดงผลดังภาพ </b>
                                            <center><img src="Innovation/img/how2play3.png" /></center>
                                    </tr>
                                </table>

                                <br />
                                <dx:ASPxButton runat="server" Image-IconID="arrows_play_32x32" Text="Play Game" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s,e){
                                            popupHow2Play.Hide();
                                            Pokemon.SetContentUrl('innovation/Pokemon.aspx');
                                            Pokemon.Show();
                                        }" />
                                </dx:ASPxButton>
                            </div>

                        </div>
                    </div>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>


    </form>

    <style>
        @keyframes notiny-animation-show-wtf {
            0% {
                opacity: 0;
                filter: blur(4px);
            }

            15% {
                opacity: 1;
            }

            50% {
            }

            90% {
                filter: blur(0px);
            }

            100% {
            }
        }



    </style>
    <link href="js/notiny/notiny.min.css" rel="stylesheet" />
    <script src="js/notiny/notiny.min.js"></script>
    <script>
        $(document).ready(function () {
          


            $.notiny({
                text: "<marquee scrolldelay='80' direction='RIGHT' >เมนูระบบ Single Sign On ให้เลือกจากไอคอนด้านล่างนี้ครับ!!!</marquee>"
                , position: 'fluid-top'
                , theme: "light" 
                , clickhide: true
                , autohide: false
                //, delay: 30000
                , image: './Theme/res/img/ichat.png'


                //// Image path (http/base64)
                //image: undefined,
                //// Position on screen, values: right-bottom, right-top, left-bottom, left-top, fluid-top, fluid-bottom
                //position: 'right-bottom',
                //// Theme
                //theme: 'dark',
                //// Template, these classes should ALWAYS be there
                //template: '',
                //// css width
                //width: '300',
                //// Text that will be displayed in notification
                //text: '',
                //// Display background or not, if false, background: transparent;
                //background: true,
                //// Hide automatically
                //autohide: true,
                //// Hide by click
                //clickhide: true,
                //// Autohide delay
                //delay: 3000,
                //// Enable animations
                //animate: true,
                //// Show animation string
                //animation_show: 'notiny-animation-show 0.4s forwards',
                //// Hide animation string
                //animation_hide: 'notiny-animation-hide 0.5s forwards'
            });


            $.notiny({
                text: "<marquee scrolldelay='80' direction='RIGHT' ><b style='color:blue'>Welcome to LWT 's get talent innovation !!!<b></marquee>"
                          , position: 'fluid-top'
                          , theme: "light"
                          , delay: 10000
                          , image: './Theme/res/img/ichat.png'
                          , clickhide: true
                          , autohide: true
            });


        });
</script>



</body>
</html>
