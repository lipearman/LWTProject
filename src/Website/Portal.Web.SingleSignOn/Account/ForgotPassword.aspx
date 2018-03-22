<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ForgotPassword.aspx.vb" Inherits="Portal.Web.SingleSignOn.ForgotPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <h2>Forgotten Password
    </h2>
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
             </fieldset>
        <p class="submitButton">
            <asp:Button ID="SendPassword" runat="server" Text="Send Password"
                ValidationGroup="ChangeUserPasswordValidationGroup" />
        </p>
    </div>


</asp:Content>
