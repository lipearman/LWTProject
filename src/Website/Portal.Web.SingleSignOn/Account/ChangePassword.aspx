<%@ Page Title="Change Password" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="ChangePassword.aspx.vb" Inherits="Portal.Web.SingleSignOn.ChangePassword" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>Change Password
    </h2>
    <p>
        Use the form below to change your password.
    </p>
    <p>
        New passwords are required to be a minimum of 8 to 20 characters in length.
    </p>

    <span class="failureNotification">
        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
    </span>
    <asp:ValidationSummary ID="ChangeUserPasswordValidationSummary" runat="server" CssClass="failureNotification"
        ValidationGroup="ChangeUserPasswordValidationGroup" />
    <div class="accountInfo">
        <fieldset class="changePassword">
            <legend>Account Information</legend>

            <p>
                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                <asp:TextBox ID="Email" runat="server" CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                    CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required."
                    ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                    ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                    ToolTip="Invalidates an e-mail address."
                    CssClass="failureNotification" ValidationGroup="ChangeUserPasswordValidationGroup" Display="Dynamic"
                    ControlToValidate="Email" ErrorMessage="Invalidates an e-mail address.">*
                </asp:RegularExpressionValidator>

            </p>


            <p>
                <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">Old Password:</asp:Label>
                <asp:TextBox ID="CurrentPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword"
                    CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Old Password is required."
                    ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">New Password:</asp:Label>
                <asp:TextBox ID="NewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword"
                    CssClass="failureNotification" ErrorMessage="New Password is required." ToolTip="New Password is required."
                    ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>



                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                    ValidationExpression="((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{8,20})$" ToolTip="NewPassword are required to be a minimum of 8 to 20 characters string with at least one digit, one upper case letter, one lower case letter and one special symbol (@#$%)"
                    CssClass="failureNotification" ValidationGroup="RegisterUserValidationGroup" Display="Dynamic"
                    ControlToValidate="NewPassword" ErrorMessage="NewPassword are required to be a minimum of 8 to 20 characters string with at least one digit, one upper case letter, one lower case letter and one special symbol (@#$%)">*
                </asp:RegularExpressionValidator>

            </p>
            <p>
                <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">Confirm New Password:</asp:Label>
                <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword"
                    CssClass="failureNotification" Display="Dynamic" ErrorMessage="Confirm New Password is required."
                    ToolTip="Confirm New Password is required." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword"
                    CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Confirm New Password must match the New Password entry."
                    ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:CompareValidator>
            </p>
        </fieldset>
        <p class="submitButton">
            <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
            <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword" Text="Change Password"
                ValidationGroup="ChangeUserPasswordValidationGroup" />
        </p>
    </div>

</asp:Content>
