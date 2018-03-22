<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucEmployees.ascx.vb" Inherits="LWT.Portal.Web.ucEmployees" %>
 
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
                <f:DropDownList Label="EmpGroup" runat="server" ID="ddlEmpGroup">
                </f:DropDownList>
<%--                <f:TextBox ID="tbxKeyword" LabelWidth="0" runat="server" EmptyText="Keyword" ShowEmptyLabel="true">
                </f:TextBox>

                <f:Button ID="btnSearch" Text="ค้นหา" runat="server" CssClass="inline"  Icon="SystemSearch">
                </f:Button>--%>
  <f:TriggerBox ID="tbxKeyword"  TriggerIcon="Search" EmptyText="Keyword" ShowLabel="false" ShowEmptyLabel="true" Label="Search" runat="Server" Width="300" LabelWidth="80">
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
                    AllowSorting="true" runat="server" AutoScroll="true" DataKeyNames="EmployeeID" EmptyText="No Data" AllowPaging="true"
                    BoxFlex="1" EnableRowDoubleClickEvent="true"
                    Title="Users">
                    <Columns>

                        <f:RowNumberField HeaderText="No." Width="60" EnablePagingNumber="true" />
                        <f:BoundField ColumnID="FirstName" SortField="FirstName" Width="100" DataField="FirstName" HeaderText="FirstName" />
                        <f:BoundField ColumnID="LastName" SortField="LastName" Width="150" DataField="LastName" HeaderText="LastName" />
                        <f:BoundField ColumnID="Extension" Width="150" SortField="Extension" DataField="Extension" HeaderText="Extension" />
                        <f:BoundField ColumnID="Title" SortField="Title" Width="200" DataField="Title" HeaderText="Title" />

                        <f:BoundField ColumnID="HomePhone" Width="150" SortField="HomePhone" DataField="HomePhone" HeaderText="HomePhone" />

                        <f:BoundField ColumnID="Address" Width="200" SortField="Address" DataField="Address" HeaderText="Address" />
                    </Columns>
                </f:Grid>

                <f:SimpleForm ID="SimpleForm1" runat="server" Width="600" LabelAlign="Left" LabelWidth="80px" EnableCollapse="False"
                    Title="Employee :" BodyPadding="5px 10px" Margin="0" Icon="User" AutoScroll="true" Enabled="false" Hidden="true" >

                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server" Position="Top">
                            <Items>
                                <f:Button ID="btnBack" Text="Back" runat="server" CssClass="inline" Icon="PageBack">
                                </f:Button>
                                <f:Button ID="btnSaveEmployee" Text="Save" runat="server" CssClass="inline" Icon="SystemSave">
                                </f:Button>
                                <f:Button ID="btnUserProfile" Text="User Profile" runat="server" CssClass="inline" Icon="Information">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>


                    <Items>
                        <f:HiddenField runat="server" ID="hdEmployeeID"></f:HiddenField>
                         <f:HiddenField runat="server" ID="hdUserId"></f:HiddenField>

                        <f:GroupPanel runat="server" ID="GroupPanel1" Title="Employee Information" EnableCollapse="true">
                            <Items>
                                <f:SimpleForm ID="SimpleForm2" runat="server" ShowBorder="false" ShowHeader="false" LabelWidth="120">
                                    <Items>




                                        <f:Image ID="imgPhoto" CssClass="photo" ImageHeight="100" ImageUrl="~/res/images/blank.png" Label="Photo/Logo" ShowEmptyLabel="true" runat="server"></f:Image>


                                        <f:FileUpload runat="server" ID="filePhoto" ShowRedStar="false" ShowEmptyLabel="true"
                                            ButtonText="Image" ButtonOnly="true" Required="false" ButtonIcon="ImageAdd" AutoPostBack="true">
                                        </f:FileUpload>

                                        <f:DropDownList Label="EmpGroup" runat="server" ID="ddlEmpGroupID"></f:DropDownList>

                                        <f:TextBox ID="tbxEmail" Label="Email" runat="server"></f:TextBox>

                                        <f:DropDownList Label="TitleOfCourtesy" runat="server" EnableEdit="true" ID="ddlTitleOfCourtesy"></f:DropDownList>



                                        <f:TextBox ID="tbxFirstName" Label="FirstName" Required="true" runat="server"></f:TextBox>
                                        <f:TextBox ID="tbxLastName" Label="LastName" runat="server"></f:TextBox>
                                        <f:TextBox ID="tbxTitle" Label="Title" runat="server"></f:TextBox>
                                        <f:DatePicker runat="server" ID="dpBirthDate" Label="BirthDate"></f:DatePicker>
                                        <f:DatePicker runat="server" ID="dpHireDate" Label="HireDate"></f:DatePicker>
                                        <f:TextArea ID="tbxAddress" Label="Address" runat="server"></f:TextArea>



                                        <f:DropDownList Label="Region" runat="server" ID="ddlRegion" AutoPostBack="true" EnableEdit="true" AutoSelectFirstItem="true"></f:DropDownList>

                                        <f:DropDownList Label="Province" runat="server" ID="ddlProvince" AutoPostBack="true" EnableEdit="true" AutoSelectFirstItem="true"></f:DropDownList>

                                        <f:DropDownList Label="Amphur" runat="server" ID="ddlAmphur" AutoPostBack="true" EnableEdit="true" AutoSelectFirstItem="true"></f:DropDownList>


                                        <f:DropDownList Label="District" runat="server" ID="ddlLocation" AutoPostBack="true" EnableEdit="true" AutoSelectFirstItem="true"></f:DropDownList>

                                        <%--        <f:DropDownList Label="Zipcode" runat="server" ID="ddlZipcode"  AutoSelectFirstItem="true" ></f:DropDownList>--%>

                                        <%-- <f:Label runat="server" ID="lbZipCode" Label="Zipcode"></f:Label>--%>
                                        <f:TextBox ID="tbxZipcode" Label="Zipcode" runat="server"></f:TextBox>




                                        <%--                                        <f:TextBox ID="tbxPostalCode" Label="PostalCode" runat="server"></f:TextBox>
                                        <f:DropDownList Label="Country" runat="server" ID="ddlCountry"></f:DropDownList>--%>


                                        <f:TextBox ID="tbxHomePhone" Label="HomePhone" runat="server"></f:TextBox>
                                        <f:TextBox ID="tbxExtension" Label="Extension" runat="server"></f:TextBox>
                                        <f:TextArea ID="tbxNotes" Label="Notes" runat="server"></f:TextArea>


                                        <%--     <f:DropDownList Label="ReportsTo" runat="server" ID="ddlReportsTo"></f:DropDownList>--%>


                                        <%--
Photo
PhotoPath
UserID
EmpGroupID
                                        --%>
                                    </Items>
                                </f:SimpleForm>
                            </Items>
                        </f:GroupPanel>

                    </Items>
                </f:SimpleForm>

            </Items>
        </f:Panel>
    </Items>
</f:Panel>


<f:Window ID="WindowUserProfile" runat="server" Title="UserProfile" Hidden="true" Target="Parent" EnableMaximize="true" 
    IsModal="True" EnableClose="true" WindowPosition="Center" Width="900px">
    <Items>



        <f:Panel ID="Panel1" runat="server" Layout="Column" BodyPadding="5px" EnableCollapse="true"
            ShowBorder="true" ShowHeader="false">

            <Toolbars>
                <f:Toolbar ID="Toolbar2" runat="server" Position="Bottom">
                    <Items>

                        <f:Button ID="btnApplyUser" Text="Apply" runat="server" CssClass="inline" Icon="SystemSave">
                        </f:Button>
                        <%--      <f:Button ID="btnDeletePage" Text="Delete" runat="server" Icon="ApplicationDelete" ConfirmText="Delete?">
                                </f:Button>--%>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>



                <f:Panel ID="Panel13" ColumnWidth="50%" CssClass="columnpanel" ShowBorder="false"
                    ShowHeader="false" runat="server">
                    <Items>
                        <f:Panel ID="Panel14" Height="400px" CssClass="rowpanel" runat="server" BodyPadding="5px"
                            ShowBorder="true" ShowHeader="false" >
                            <Items>
                                <f:SimpleForm ID="SimpleForm3" runat="server" ShowBorder="false" ShowHeader="true" Title="User Profile" LabelWidth="120" Height="400px" >
                                    <Items>
                                        <f:Image ID="Image1" CssClass="photo" ImageWidth="100" ImageUrl="~/res/images/blank.png" ShowEmptyLabel="true" runat="server"></f:Image>
                                        <f:Label runat="server" ID="lbUserName" Label="UserName"></f:Label>
                                        <f:CheckBox runat="server" ID="cbIsaApproved" Label="IsApproved"></f:CheckBox>
                                        <f:CheckBox runat="server" ID="cbIsLocked" Label="IsLocked"></f:CheckBox>
                                        <f:DatePicker runat="server" ID="dpExpiredDate" Label="ExpiredDate"></f:DatePicker>
                                        <f:TextArea ID="tbxComment" Label="Comment" runat="server" >
                                        </f:TextArea>
                                    </Items>
                                </f:SimpleForm>
                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel15" Height="100px" runat="server" BodyPadding="5px" ShowBorder="true"
                            ShowHeader="false">
                            <Items>
                                

                                <f:SimpleForm ID="SimpleForm4" runat="server" ShowBorder="false" ShowHeader="false" LabelWidth="120">
                                    <Items>
                                        <f:Label runat="server" ID="lbEmail" Label="Email"></f:Label>

                                        <f:Label runat="server" ID="lbPasswordQuestion" Label="Question"></f:Label>

                                        <f:Label runat="server" ID="lbPasswordAnswer" Label="Answer"></f:Label>
                                    </Items>
                                </f:SimpleForm>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:Panel>
                <f:Panel ID="Panel16" ColumnWidth="50%" ShowBorder="false" ShowHeader="false" runat="server">
                    <Items>
                        <f:Panel ID="Panel17" Height="400px" CssClass="rowpanel" runat="server" BodyPadding="5px" AutoScroll="true"
                            ShowBorder="true" ShowHeader="false">
                            <Items>
                               <%-- <f:DropDownList Label="Project" runat="server" ID="ddlProject" Width="400" AutoPostBack="true" AutoSelectFirstItem="true">
                                </f:DropDownList>

                                <f:CheckBoxList ID="rblUserRole" ColumnNumber="2" ColumnVertical="true" Width="400" ShowEmptyLabel="true" ShowLabel="false" AutoPostBack="true"
                                    runat="Server">
                                </f:CheckBoxList>--%>

                                
                                <f:Tree ID="trUserRoles" ShowBorder="false"  ShowHeader="true" Title="Projects and Roles"  runat="server"  ></f:Tree>




                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel18" Height="100px" runat="server" BodyPadding="5px" ShowBorder="true" AutoScroll="true"
                            ShowHeader="false">
                            <Items>
                                <f:SimpleForm ID="SimpleForm6" runat="server" ShowBorder="false" ShowHeader="false" LabelWidth="250">
                                    <Items>
                                        <f:Label runat="server" ID="lbCreationDate" Label="Creation Date"></f:Label>
                                        <f:Label runat="server" ID="lbLastLoginDate" Label="Last Login Date"></f:Label>
                                        <f:Label runat="server" ID="lbLastActivityDate" Label="Last Activity Date"></f:Label>
                                        <f:Label runat="server" ID="lbLastPasswordChangedDate" Label="Last Password Changed Date"></f:Label>
                                        <f:Label runat="server" ID="lbLastLockOutDate" Label="Last Lock Out Date"></f:Label>
                                        <f:Label runat="server" ID="lbFailedPasswordAttemptCount" Label="Failed Password Attempt Count"></f:Label>
                                        <f:Label runat="server" ID="lbFailedPasswordAttemptWindowStart" Label="Failed Password Attempt Window Date"></f:Label>
                                        <f:Label runat="server" ID="lbFailedPasswordAnswerAttemptCount" Label="Failed Password Answer Attempt Count"></f:Label>
                                        <f:Label runat="server" ID="lbFailedPasswordAnswerAttemptWindowStart" Label="Failed Password Answer Attempt Windows Start"></f:Label>
                                    </Items>
                                </f:SimpleForm>
                            </Items>
                        </f:Panel>


                    </Items>
                </f:Panel>



            </Items>
        </f:Panel>



    </Items>
</f:Window>
