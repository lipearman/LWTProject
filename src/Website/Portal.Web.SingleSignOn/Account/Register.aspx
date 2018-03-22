<%@ Page Title="Register" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="Register.aspx.vb" Inherits="Portal.Web.SingleSignOn.Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>Create a New Account
    </h2>
    <p>
        Use the form below to create a new account.
    </p>
    <p>
        Passwords are required to be a minimum of 8 to 20 characters in length.
    </p>
    <span class="failureNotification">
        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
    </span>
    <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification"
        ValidationGroup="RegisterUserValidationGroup" />


    <div class="accountInfo">
        <fieldset class="register">
            <legend>Account Information</legend>
            <p>
                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>

                <span>
                    <asp:TextBox ID="UserName" runat="server" CssClass="textEntry" Width="120"></asp:TextBox>
                    <asp:Label ID="Label1" runat="server">@asia.lockton.com</asp:Label>
                    <asp:Button ID="btnCheckUser" runat="server"  Text="Check" />


                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                        CssClass="failureNotification" ErrorMessage="User Name is required." Display="Dynamic" ToolTip="User Name is required."
                        ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>





                </span>

            </p>
           <%-- <p>
                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                <asp:TextBox ID="Email" runat="server" CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                    CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required."
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                    ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                    ToolTip="Invalidates an e-mail address."
                    CssClass="failureNotification" ValidationGroup="RegisterUserValidationGroup" Display="Dynamic"
                    ControlToValidate="Email" ErrorMessage="Invalidates an e-mail address.">*
                </asp:RegularExpressionValidator>

            </p>--%>
            <p>
                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password" MaxLength="20"></asp:TextBox>

                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" Display="Dynamic"
                    CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                    ValidationExpression="((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{8,20})" ToolTip="Passwords are required to be a minimum of 8 to 20 characters string with at least one digit, one upper case letter, one lower case letter and one special symbol (@#$%)"
                    CssClass="failureNotification" ValidationGroup="RegisterUserValidationGroup" Display="Dynamic"
                    ControlToValidate="Password" ErrorMessage="Passwords are required to be a minimum of 8 to 20 characters string with at least one digit, one upper case letter, one lower case letter and one special symbol (@#$%)">*
                </asp:RegularExpressionValidator>
            </p>
            <p>
                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
                <asp:TextBox ID="ConfirmPassword" runat="server" CssClass="passwordEntry" TextMode="Password" MaxLength="20"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" CssClass="failureNotification"
                    Display="Dynamic" ErrorMessage="Confirm Password is required." ID="ConfirmPasswordRequired"
                    runat="server" ToolTip="Confirm Password is required." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>

                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                    ControlToValidate="ConfirmPassword" CssClass="failureNotification" Display="Dynamic"
                    ErrorMessage="The Password and Confirmation Password must match." ValidationGroup="RegisterUserValidationGroup">*</asp:CompareValidator>
            </p>
        </fieldset>
        <p class="submitButton">
            <asp:Button ID="CreateUserButton" runat="server" CommandName="MoveNext" Text="Create User"
                ValidationGroup="RegisterUserValidationGroup" />
        </p>
    </div>
</asp:Content>
