<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Menu.aspx.vb" Inherits="Portal.Web.SingleSignOn.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
ol {
    -moz-column-count: 2;
    -moz-column-gap: 40px;
    -webkit-column-count: 1;
    -webkit-column-gap: 40px;
    column-count: 2;
    column-gap: 40px;
}
ol ul, ul ol, ul ul, ol ol {
  -webkit-margin-before: 0px;
  -webkit-margin-after: 0px;
  -webkit-column-count: 2;
}
    </style>

    <div class="menuInfo">




        <span class="failureNotification">
            <asp:Literal ID="FailureText" runat="server"></asp:Literal>
        </span>
        <fieldset class="register">
            <legend>System Information(<%=HttpContext.Current.User.Identity.Name%>)</legend>


<%--<p>
<b>You are browsing this site with:</b>
<%Response.Write(Request.ServerVariables("http_user_agent"))%>
</p>
<p>
<b>Your IP address is:</b>
<%Response.Write(Request.ServerVariables("remote_addr"))%>
</p>
<p>
<b>The DNS lookup of the IP address is:</b>
<%Response.Write(Request.ServerVariables("remote_host"))%>
</p>
<p>
<b>The method used to call the page:</b>
<%Response.Write(Request.ServerVariables("request_method"))%>
</p>
<p>
<b>The server's domain name:</b>
<%Response.Write(Request.ServerVariables("server_name"))%>
</p>
<p>
<b>The server's port:</b>
<%Response.Write(Request.ServerVariables("server_port"))%>
</p>
<p>
<b>The server's software:</b>
<%Response.Write(Request.ServerVariables("server_software"))%>
</p>--%>
            <div>
                <ol>
                    <asp:Repeater ID="outerRep" runat="server" OnItemDataBound="outerRep_ItemDataBound">
                        <ItemTemplate>
                            <li>
                                <asp:Label Font-Size="Large" Font-Bold="true" ID="lblCategoryName" runat="server"
                                    Text='<%# Eval("CategoryName") %>' />
                            </li>
                            <ul>
                                <asp:Repeater ID="innerRep" runat="server" OnItemCommand="innerRep_ItemCommand">
                                    <ItemTemplate>
                                        <li style="background-color: AliceBlue">
                                            <%--<asp:HyperLink ID="hlProductName"  runat="server" Text='<%# Eval("SubCategoryName")%>' NavigateUrl="" />--%>
                                            <asp:LinkButton runat="server" ID="lnkUrl" CommandName="cmdGotoURL" CommandArgument='<%# Eval("SubCategoryId")%>' Text='<%# Eval("SubCategoryName")%>'></asp:LinkButton>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </ItemTemplate>



                    </asp:Repeater>
                </ol>
            </div>


        </fieldset>

    </div>

    
     

</asp:Content>
