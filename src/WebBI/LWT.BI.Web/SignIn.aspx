<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SignIn.aspx.vb" Inherits="SignIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=sitename %> :: Lockton Wattana Insurance Brokers (Thailand) Ltd. </title>

    <link href="Policy/StyleSheet.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
       <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Modal="True">
        </dx:ASPxLoadingPanel>
        <div id="Header">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 150px;">
                        <div class="Logo"></div>
                    </td>

                    <td>
                        <div class="Line1"></div>
                        <div class="Line2"><%=sitename %></div>
                        <div class="Line3"></div>
                    </td>
                </tr>
            </table>
        </div>
        <hr />
        <div id="Content">
            <div style="text-align: center; width: 100%; margin: auto;">
               
                <table id="login" style="width: 1100px; background: none;" class="c">
                     <tr>
                        <td align="right">
                            <%--<asp:Button ID="Btn_Login" runat="server" CssClass="cssButton" Text="ตกลงและเข้าสู่ระบบ" SkinID="LoginButton" />--%>
 
 <dx:ASPxButton runat="server" ID="ASPxButton1" Image-Url="~/res/icon/flag_th.png" Enabled="false" Text="Thai" CssClass="TextBoxLogin">

 </dx:ASPxButton>
 <dx:ASPxButton runat="server" ID="cmdEnglish" AllowFocus="false" Image-Url="~/res/icon/flag_gb.png" Text="English" CssClass="TextBoxLogin">

 </dx:ASPxButton>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />

                             <center>
                            <table>
                                <tr>
                                    <td><span id="ctl00_ContentPlaceHolder1_Label1" style="display: inline-block; color: #0000CC; font-size: 14px; font-weight: bold; width: 82px;">User ID</span>
                                        <asp:TextBox ID="UserName" runat="server" name="login"></asp:TextBox></td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" ForeColor="Red" runat="server" Display="Dynamic" ControlToValidate="UserName"
                                            CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                                            ValidationGroup="LoginUserValidationGroup"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span id="ctl00_ContentPlaceHolder1_Label2" style="display: inline-block; color: #0000CC; font-size: 14px; font-weight: bold; width: 82px;">Password</span>
                                        <asp:TextBox ID="Password" runat="server" name="password" TextMode="Password"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" ForeColor="Red" runat="server" ControlToValidate="Password" Display="Dynamic"
                                            CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                                            ValidationGroup="LoginUserValidationGroup"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>

                                 </center>

                            <br /> 
                            <span class="textAlert">
                                <asp:Literal ID="FailureText" runat="server"></asp:Literal></span>


                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; font-size: 13px;">ข้าพเจ้าซึ่งเป็นผู้ใช้ระบบงานของบริษัท ล็อคตั้น วัฒนา อินชัวรันส์ โบรคเกอร์ส (ประเทศไทย) จำกัด ในฐานะผู้ที่ได้รับอำนาจ และ/หรือได้รับอนุญาตในการใช้รหัสผู้ใช้งาน (User ID) และ รหัสผ่าน (Password) เพื่อเข้าถึงข้อมูลต่าง ๆ ในระบบงานนี้ทราบดีว่ารหัสผู้ใช้งาน รหัสผ่าน และข้อมูลที่ได้รับอนุญาตให้เข้าถึงนั้นเป็นความลับเฉพาะองค์กร และ/หรือบุคคล จะเปิดเผยให้บุคคลอื่นที่ไม่เกี่ยวข้องหรือไม่ได้รับอนุญาตเข้าถึงข้อมูลเหล่านั้นไม่ได้
                <br />
                            <br />
                            ข้าพเจ้าทราบดีว่าสิทธิ์ในการใช้รหัสผู้ใช้งาน และรหัสผ่าน ยังคงอยู่ตราบเท่าที่ข้าพเจ้ายังเป็นพนักงานของบริษัทหรือร้านค้าที่ได้รับอนุญาตให้ใช้ระบบงานนี้เท่านั้น
                <br />
                            <br />
                            ข้าพเจ้าจะใช้เวลาอย่างเพียงพอในการตรวจสอบและบันทึกข้อมูลให้ถูกต้อง
                <br />
                            <br />
                            หากมีพนักงานของบริษัทหรือร้านค้าที่เป็นผู้ใช้ระบบงานลาออก หรือต้องการเปลี่ยนแปลงสิทธิ์ในการเข้าถึงข้อมูล บริษัทหรือร้านค้าที่ได้รับสิทธิ์ในการใช้ระบบงานจะต้องแจ้งให้ บริษัท ล็อคตั้น วัฒนาฯ ทราบเป็นอีเมล์หรือเอกสารแฟกซ์ เพื่อทำการยกเลิกสิทธิ์การใช้งาน หรือเปลี่ยนแปลงรหัสผ่าน รวมถึงเปลี่ยนแปลงสิทธิ์ในการเข้าถึงข้อมูลตามที่ร้องขอ ทั้งนี้เพื่อความปลอดภัยของข้อมูลสำหรับบริษัทหรือร้านค้าของท่านเอง

                    <br />
                            <br />


                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--<asp:Button ID="Btn_Login" runat="server" CssClass="cssButton" Text="ตกลงและเข้าสู่ระบบ" SkinID="LoginButton" />--%>

                            <asp:Button ID="LoginButton" runat="server" CssClass="ButtonLogin" CommandName="Login" Text="ตกลงและเข้าสู่ระบบ" ValidationGroup="LoginUserValidationGroup" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; font-size: 13px;">
                            <br />
                            ในปัจจุบันข้อมูลเป็นสิ่งสำคัญที่ทุกคนให้ความสนใจเป็นอย่างมาก บริษัท ล็อคตั้น วัฒนา อินชัวรันส์ โบรคเกอร์ส (ประเทศไทย) จำกัด มุ่งเน้นที่จะดูแลรักษาความปลอดภัยข้อมูลให้ดีที่สุด ให้แก่ลูกค้า คู่ค้าของล็อคตั้น วัฒนาฯ ดังนั้นจึงขอแนะนำเพิ่มเติมให้ผู้ใช้ระบบงานของล็อคตั้น วัฒนาฯ รับทราบและปฏิบัติตามดังต่อไปนี้

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; font-size: 13px;">
                            <nav><ul  style="list-style-type:decimal">                  
                        <li style="margin:5px;">หากท่านผู้ใช้ระบบงานไม่ได้ใช้หน้าจอคอมพิวเตอร์ทำงานเป็นเวลานาน ๆ ควรออกจากระบบ หรือปิดพักหน้าจอการทำงานชั่วคราว เพื่อป้องกันผู้ที่ไม่มีสิทธิ์แอบใช้ระบบงานหรือเข้าถึงข้อมูลของท่านโดยไม่ได้รับอนุญาต
                        </li>
                        <li style="margin:5px;">ท่านผู้ใช้ระบบงานควรจดจำรหัสผ่านส่วนตัวของท่าน และไม่บันทึกหรือเก็บรหัสผ่านส่วนตัวของท่านไว้ในที่ที่เปิดเผย
                        </li>
                        <li style="margin:5px;">ท่านควรติดตั้งโปรแกรมป้องกันไวรัสคอมพิวเตอร์บนเครื่องคอมพิวเตอร์ของผู้ใช้งานและพยายามปรับปรุงโปรแกรมป้องกันไวร้สให้มีความทันสมัยตลอดเวลา
                        </li>
                        <li style="margin:5px;">ล็อคตั้น วัฒนาฯ ไม่แนะนำให้ใช้ฟรีอินเตอร์เน็ตหรืออินเตอร์เน็ตสาธารณะ เช่น อินเตอร์เน็ตคาเฟ่ต่างๆ ทั้งนี้เนื่องจากปัจจุบันมีโปรแกรมแฮกเกอร์บางชนิดสามารถดักจับและขโมยข้อมูลได้ ท่านผู้ใช้ระบบงานควรหลีกเลี่ยงการใช้บริการในสถานที่ดังกล่าวเหล่านั้น
                        </li>
                        </ul>
                        </nav>
                        </td>
                    </tr>
                </table>

            </div>

            <br />
        </div>


        <hr />
        <div id="Footer">
            บริษัทล็อคตั้น วัฒนา อินชัวรันส์ โบรคเกอร์ส (ประเทศไทย) จำกัด    ชั้น 35 อาคารยูไนเต็ดเซ็นเตอร์ ถนนสีลม กรุงเทพมหานคร 10500 
    <br />
            โทรศัพท์ 0-2353-7000 ต่อ 4101-4107, เบอร์ตรง 0-2353-7600, โทรสาร 0-2353-7003-4
    <br />
            Lockton Wattana Insurance Brokers (Thailand) Ltd.
        </div>



        <dx:ASPxPopupControl ID="pcChagePassword"
            ClientInstanceName="pcChagePassword"
            HeaderText="Change Password"
            ShowFooter="false"
            ShowCloseButton="false"
            ShowCollapseButton="false"
            ShowMaximizeButton="false"
            ShowHeader="false"
            ShowShadow="false"
            ShowPinButton="false"
            ShowRefreshButton="false"
            ShowSizeGrip="False"
            runat="server"
            PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter"
            AllowDragging="false"
            Modal="True"
            EnableViewState="False">
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">

                    <dx:ASPxFormLayout ID="ASPxFormLayout1"
                        runat="server"
                        RequiredMarkDisplayMode="None"
                        Styles-LayoutGroupBox-Caption-CssClass="layoutGroupBoxCaption"
                        Width="100%">
                        <Items>

                            <dx:LayoutGroup Caption="Change Password" GroupBoxDecoration="HeadingLine" SettingsItemCaptions-HorizontalAlign="Right" ColCount="2">
                                <Items>
                                    <dx:LayoutItem ShowCaption="False">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxTextBox ID="passwordTextBox" CssClass="txtpassword" MaxLength="12"
                                                    NullText="New Password" runat="server" ToolTip="New Password"
                                                    ClientInstanceName="passwordTextBox" Password="true"
                                                    Width="170">
                                                    <ValidationSettings ErrorTextPosition="Bottom"
                                                        ErrorDisplayMode="Text" 
                                                        Display="Dynamic"
                                                        SetFocusOnError="true">
                                                        <RequiredField IsRequired="True" ErrorText="The value is required"  />
                                                        <RegularExpression ErrorText="Invalid Password policies, eg. P@ssword01" ValidationExpression="((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{8,20})$" />
                                                    </ValidationSettings>

                                                </dx:ASPxTextBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>

                                    <dx:LayoutItem ShowCaption="False" Caption="Confirm password" ColSpan="2">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxTextBox ID="confirmPasswordTextBox" MaxLength="12"
                                                    NullText="Confirm New Password"
                                                    runat="server" CssClass="txtpassword" ToolTip="Confirm New Password"
                                                    ClientInstanceName="confirmPasswordTextBox"
                                                    Password="true"
                                                    Width="170">
                                                    <ValidationSettings ErrorTextPosition="Bottom"
                                                        ErrorDisplayMode="Text"
                                                        Display="Dynamic"
                                                        SetFocusOnError="true">
                                                        <RequiredField IsRequired="True" ErrorText="The value is required" />
                                                    </ValidationSettings>
                                                    <ClientSideEvents Validation="function(s,e){
                                                             if (passwordTextBox.GetText() !== confirmPasswordTextBox.GetText())
                                                             {
                                                                e.isValid = false;
                                                                e.errorText = 'The password you entered do not match';
                                                             }
                               
                                                         }" />
                                                </dx:ASPxTextBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>


                                    <dx:LayoutItem ShowCaption="False" ColSpan="2">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxButton runat="server" ID="btnChangePassword" Text="Submit" Width="100px" CssClass="btnchangepwd" AutoPostBack="false">

                                                    <ClientSideEvents Click="function(s,e){
                                                           if(ASPxClientEdit.AreEditorsValid()) 
                                                           {
                                                                LoadingPanel.Show(); 
                                                                cbChangePwd.PerformCallback('');    
                                                           }
                                                                                                          
                                                       }" />

                                                </dx:ASPxButton>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>

                                </Items>
                            </dx:LayoutGroup>

                        </Items>
                    </dx:ASPxFormLayout>


                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>



        <dx:ASPxCallback runat="server" ID="cbChangePwd" ClientInstanceName="cbChangePwd">
            <ClientSideEvents
                CallbackError="function(s,e){LoadingPanel.Hide(); }"
                CallbackComplete="function(s,e){ 
                LoadingPanel.Hide();   
                if (e.result != 'success') {
                    alert(e.result);                                                
                }   
                else
                {

                    pcChagePassword.Hide();

                    alert('Your password has been changed successfully.');
                    
                }                                                                  
                e.processOnServer = false;
        }" />
        </dx:ASPxCallback>



    </form>
</body>
</html>
