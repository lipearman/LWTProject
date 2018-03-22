<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SignIn-Eng.aspx.vb" Inherits="SignIn_Eng" %>

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

                            <dx:ASPxButton runat="server" ID="cmdThai"  Visible="false" AllowFocus="false" Image-Url="~/res/icon/flag_th.png" Text="Thai" CssClass="TextBoxLogin">

 </dx:ASPxButton>
 <dx:ASPxButton runat="server" ID="cmdEnglish" Image-Url="~/res/icon/flag_gb.png" Visible="false" Text="English" Enabled="false" CssClass="TextBoxLogin">

 </dx:ASPxButton></td>
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
                        <td style="text-align: left; font-size: 13px;">I acknowledge and agree that by being granted access to Lockton Wattana’s system, my authorized login ID / password and any confidential information will not be disclosed or used by other unauthorized persons.
                <br />
                            <br />
                           I realize that my system access right is not valid once I am no longer working for the employer(s) who are authorized to use the system
                <br />
                            <br />
                            I will spend a sufficient time to check and verify for correctness of data that are input into the system
                <br />
                            <br />
                            In case of employee resignation or retirement, the access right must be changed or disabled by Lockton Wattana, it is a responsibility of the user’s employer to promptly inform Lockton Wattana. This is to ensure a good care of the data protection and security.

                    <br />
                            <br />


                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--<asp:Button ID="Btn_Login" runat="server" CssClass="cssButton" Text="ตกลงและเข้าสู่ระบบ" SkinID="LoginButton" />--%>

                            <asp:Button ID="LoginButton" runat="server" CssClass="ButtonLogin" CommandName="Login" Text="Accept  / Log In" ValidationGroup="LoginUserValidationGroup" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; font-size: 13px;">
                            <br />
                           Nowadays, everyone is interested in information. Lockton Wattana intends to do our best in taking good care of data safety and security for our customers, partners or contacts.  It is therefore, necessary to ensure that Lockton Wattana’s system users are aware and understand the IT security policy and will follow its security guidelines listed out below.

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; font-size: 13px;">
                            <nav><ul  style="list-style-type:decimal">                  
                        <li style="margin:5px;">If you leave your computer alone for a period of time, you should lock your screen in window to protect your computer from unauthorized access
                        </li>
                        <li style="margin:5px;">Please remember your password and do not keep your password in the public computer/devices or unsecure place.
                        </li>
                        <li style="margin:5px;">Please ensure that  anti-malware software is installed on your computer and with the latest updated version
                        </li>
                        <li style="margin:5px;">Lockton Wattana advises you to avoid using shared computers in cyber cafes or public areas as someone can use a software to try to hack onto your computer and steal your sensitive personal information 
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
            Lockton Wattana Insurance Brokers (Thailand) Ltd. 4th, 21st and 35th Floor, United Center Building,323 Silom Road, Khet Bangrak, Bangkok 10500
    <br />
            Telephone: +66 (0) 2 635 5000, FAX: +66 (0) 2 635 5111
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
