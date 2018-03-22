<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucSingleSignOn.ascx.vb" Inherits="LWT.Portal.Web.ucSingleSignOn" %>

<style>
    body.f-body {
        padding: 0;
    }

    .success {
        color: Green;
    }

    .error {
        color: Red;
    }

    .bold {
        font-weight: bold;
    }
</style>

<style type="text/css">
    .columnpanel {
        margin-right: 5px;
    }

    .rowpanel {
        margin-bottom: 5px;
    }
</style>

<f:Panel ID="MainRegionPanel" runat="server" BodyPadding="5px" ShowBorder="false"
    ShowHeader="false" Title="Projects" EnableCollapse="true">
    <Toolbars>
        <f:Toolbar ID="Toolbar9" runat="server">
            <Items>

                <%--  <f:TextBox ID="tbxKeyword" LabelWidth="0" runat="server" EmptyText="Keyword" ShowEmptyLabel="true">
                </f:TextBox>

                <f:Button ID="btnSearch" Text="ค้นหา" runat="server" CssClass="inline" Icon="SystemSearch">
                </f:Button>--%>
                <f:TriggerBox ID="tbxKeyword" TriggerIcon="Search" EmptyText="Keyword" ShowLabel="false" ShowEmptyLabel="true" Label="Search" runat="Server" Width="300" LabelWidth="80">
                </f:TriggerBox>
                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                </f:ToolbarFill>
                <f:Button ID="btnRefresh" Text="รีเฟรชข้อมูล" runat="server" Icon="Reload">
                </f:Button>
            </Items>
        </f:Toolbar>
    </Toolbars>
    <Items>

        <f:Panel ID="Panel2" runat="server" ShowBorder="false" Layout="HBox" BoxConfigAlign="Stretch"
            BoxConfigPosition="Start" BodyPadding="5px" BoxConfigChildMargin="0 5 0 0"
            ShowHeader="false">
            <Items>

                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" SortField="FirstName" Icon="User" Height="640"
                    AllowSorting="true" runat="server" AutoScroll="true" DataKeyNames="EmployeeID,UserID" EmptyText="No Data" AllowPaging="true"
                    BoxFlex="1" EnableRowDoubleClickEvent="true"
                    Title="Users">
                    <Columns>

                        <f:RowNumberField HeaderText="No." Width="60" EnablePagingNumber="true" />
                        <f:BoundField ColumnID="FirstName" SortField="FirstName" Width="100" DataField="FirstName" HeaderText="FirstName" />
                        <f:BoundField ColumnID="LastName" SortField="LastName" Width="150" DataField="LastName" HeaderText="LastName" />
                        <f:BoundField ColumnID="Extension" Width="200" SortField="Extension" DataField="Extension" HeaderText="Extension" />
                        <f:BoundField ColumnID="Title" SortField="Title" Width="300" DataField="Title" HeaderText="Title" />

                        <f:BoundField ColumnID="HomePhone" Width="150" SortField="HomePhone" DataField="HomePhone" HeaderText="HomePhone" />

                        <f:BoundField ColumnID="Address" Width="600" SortField="Address" DataField="Address" HeaderText="Address" />
                    </Columns>
                </f:Grid>

                <f:SimpleForm ID="SimpleForm1" runat="server" Width="400" LabelAlign="Left" LabelWidth="80px" EnableCollapse="False"
                    Title="Employee :" BodyPadding="5px 10px" Margin="0" Icon="User" AutoScroll="true" Enabled="false">

                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server" Position="Top">
                            <Items>

                                <f:DropDownList ShowLabel="false" AutoPostBack="false" EnableSimulateTree="true" Required="true" runat="server" ID="ddlProjectRole" Width="250"></f:DropDownList>

                                <f:Button ID="btnAddUser" Text="Add" runat="server" CssClass="inline" Icon="Add">
                                </f:Button>

                                <f:Button ID="btnDeleteUser" Text="Delete" runat="server" ConfirmText="Delete ?" CssClass="inline" Icon="ApplicationDelete">
                                </f:Button>

                            </Items>
                        </f:Toolbar>
                    </Toolbars>


                    <Items>
                        <f:HiddenField runat="server" ID="hdEmployeeID"></f:HiddenField>
                        <f:HiddenField runat="server" ID="hdUserId"></f:HiddenField>

                        <f:Tree ID="trUserRoles" ShowBorder="false" ShowHeader="false" runat="server" Expanded="true"></f:Tree>



                    </Items>
                </f:SimpleForm>

            </Items>
        </f:Panel>
    </Items>
</f:Panel>

<f:Window ID="WindowUser" runat="server" Title="User" Hidden="true" EnableMaximize="false"
    IsModal="True" EnableClose="true" WindowPosition="Center">
    <Items>
        <f:Grid ID="Grid3" ShowBorder="true" ShowHeader="false" Title="" Width="300px" runat="server" EnableCollapse="true" EnableRowDoubleClickEvent="true"
            DataKeyNames="UserName,Name" SortField="UserName" ShowGridHeader="false" EnableRowLines="false" EnableAlternateRowColor="false" Height="200px">
            <Columns>
                <f:BoundField ExpandUnusedSpace="true" DataField="UserName" DataFormatString="{0}" HeaderText="UserName" />
                <f:BoundField ExpandUnusedSpace="true" DataField="Name" DataFormatString="{0}" HeaderText="Name" />

            </Columns>
        </f:Grid>

    </Items>
</f:Window>
