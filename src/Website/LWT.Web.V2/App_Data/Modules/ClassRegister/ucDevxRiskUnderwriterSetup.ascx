<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxRiskUnderwriterSetup.ascx.vb" Inherits="Modules_ucDevxRiskUnderwriterSetup" %>


<script type="text/javascript">
    var keyValue;
    function OnMoreInfo(element, key) {
        callbackPanel_MoreInfo.SetContentHtml("");
        popupMoreInfo.ShowAtElement(element);
        keyValue = key;
    }
    function popup_ShownCommOut(s, e) {
        callbackPanel_MoreInfo.PerformCallback(keyValue);
    }
</script>


<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Underwriter" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxButton ID="btnAddNewCommIn" runat="server" Text="เพิ่ม CommIn" Image-IconID="actions_add_16x16">
                <ClientSideEvents Click="function(s, e) {           
                       popupRiskUnderwriter.Show();   
                       callbackPanel_popupRiskUnderwriter.PerformCallback('new')     
                       e.processOnServer = false; 
                      }" />
            </dx:ASPxButton>
            <br />
            <asp:SqlDataSource ID="SqlDataSource_RiskUwriter" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
                SelectCommand="select * from [V_RiskUwriter] "
                DeleteCommand="delete from [Register.RiskUwriter] where ID=@ID"
                UpdateCommand="update [Register.RiskUwriter] 
                set   Brokerage=@Brokerage
                ,BrokerageCal=@BrokerageCal
                ,ORCommissionPercent=@ORCommissionPercent
                ,ORInCal=@ORInCal
                ,AdminFeeIn1=@AdminFeeIn1
                ,AdminFeeIn1Cal=@AdminFeeIn1Cal
                ,AdminFeeIn2=@AdminFeeIn2
                ,AdminFeeIn2Cal=@AdminFeeIn2Cal
                ,ORCalFrom=@ORCalFrom
                ,OffsetORFlag=@OffsetORFlag
                ,OffsetAdm1Flag=@OffsetAdm1Flag
                ,OffsetAdm2Flag=@OffsetAdm2Flag
                ,Remark=@Remark
                ,IsActive=@IsActive
                ,ModifyBy=@UserName
                ,ModifyDate=getdate()
                where ID=@ID">
               
                <DeleteParameters>
                    <asp:Parameter Name="ID" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="ID" />
                    <asp:Parameter Name="Brokerage" />
                    <asp:Parameter Name="BrokerageCal" />
                    <asp:Parameter Name="ORCommissionPercent" />
                    <asp:Parameter Name="ORInCal" />
                    <asp:Parameter Name="AdminFeeIn1" />
                    <asp:Parameter Name="AdminFeeIn1Cal" />
                    <asp:Parameter Name="AdminFeeIn2" />
                    <asp:Parameter Name="AdminFeeIn2Cal" />
                    <asp:Parameter Name="ORCalFrom" />
                    <asp:Parameter Name="OffsetORFlag" />
                    <asp:Parameter Name="OffsetAdm1Flag" />
                    <asp:Parameter Name="OffsetAdm2Flag" />
                    <asp:Parameter Name="Remark" />
                    <asp:Parameter Name="IsActive" />
                    <asp:Parameter Name="UserName" />
                </UpdateParameters>
            </asp:SqlDataSource>


            <dx:ASPxGridView ID="gridRiskUwriter" ClientInstanceName="gridRiskUwriter" runat="server" DataSourceID="SqlDataSource_RiskUwriter"
                KeyFieldName="ID" AutoGenerateColumns="False" Width="100%" SettingsBehavior-AutoExpandAllGroups="true">
                <SettingsPager Mode="ShowPager">
                    <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                </SettingsPager>
                <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" />
                <SettingsSearchPanel Visible="true" />
                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
              <%--  <SettingsCommandButton>
                    <UpdateButton ButtonType="Button" Text="บันทึก"></UpdateButton>
                    <EditButton ButtonType="Button" Text="แก้ไข"></EditButton>
                    <DeleteButton ButtonType="Button" Text="ลบ"></DeleteButton>
                    <CancelButton ButtonType="Button" Text="ยกเลิก"></CancelButton>
                </SettingsCommandButton>--%>


                <SettingsText PopupEditFormCaption="แก้ไขรายการ" ConfirmDelete="ยืนยันการลบ" />
                <SettingsPopup>
                    <EditForm Modal="true" MinWidth="100" Width="400" HorizontalAlign="WindowCenter" VerticalAlign="Middle" />
                    <HeaderFilter Height="200" />
                </SettingsPopup>
                <Settings ShowHeaderFilterButton="true" ShowGroupPanel="true" />



                <Columns>

                    <dx:GridViewDataColumn Caption="" EditFormSettings-Visible="False" Width="50" CellStyle-HorizontalAlign="Center" EditCellStyle-VerticalAlign="Bottom">
                        <DataItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="RowEditable" runat="server" Text="แก้ไข" AutoPostBack="false">
                                            <ClientSideEvents Click="function (s, e) {  
                                                gridRiskUwriter.StartEditRow(s.cpVisibleIndex);
                                             }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>


                                        <dx:ASPxButton runat="server" AutoPostBack="false"
                                            RenderMode="Button"
                                            ID="RowDeleteable" 
                                            Text="ลบ">
                                            <ClientSideEvents Click="function(s) 
                                                {
                                                    if(confirm('Do you want to delete?'))
                                                    {  
                                                        gridRiskUwriter.DeleteRow(s.cpVisibleIndex);
                                                    }
                                                    e.processOnServer = false;
                                                }
                                            " />
                                        </dx:ASPxButton>

                                    </td>
                                </tr>

                            </table>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>

                    
                     <dx:GridViewDataColumn FieldName="status" Caption="Status" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                        <EditFormSettings Visible="False" />
                        <DataItemTemplate>
                            <img src='images/<%# Eval("status").ToString().Trim()%>.gif' />
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <%--<dx:GridViewDataColumn FieldName="Risk" CellStyle-Wrap="False"></dx:GridViewDataColumn>--%>
                    <dx:GridViewDataColumn FieldName="Risk" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center" CellStyle-Font-Bold="true">
                        <DataItemTemplate>
                            <a href="javascript:void(0);" onclick="OnMoreInfo(this, '<%# Eval("Risk").ToString().Trim()%>')"><%# Eval("Risk").ToString().Trim()%></a>
                        </DataItemTemplate>
                        <CellStyle Wrap="False"></CellStyle>
                    </dx:GridViewDataColumn>




                    <dx:GridViewDataColumn FieldName="Underwriter" Caption="UCode" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="AccountContact" Caption="Name" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                    <%--<dx:GridViewDataColumn FieldName="Name" CellStyle-Wrap="False"></dx:GridViewDataColumn>--%>




                    <dx:GridViewDataColumn FieldName="Brokerage_display" Caption="Brokerage" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ORCommissionPercent_display" Caption="SF" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="AdminFeeIn1_display" Caption="Admin1" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="AdminFeeIn2_display" Caption="Admin2" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                    <dx:GridViewDataColumn FieldName="OffsetORFlag" Caption="UpFront" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Remark" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                   
                    <dx:GridViewDataColumn FieldName="InsuranceLine" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>

                    <%--                    <dx:GridViewDataColumn FieldName="CommOut" Caption="Agent" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="newRequest" Caption="Request" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>--%>

                    <%--<dx:GridViewDataColumn FieldName="RiskShortDesc" CellStyle-Wrap="False"></dx:GridViewDataColumn>--%>
                    <%--                    <dx:GridViewDataColumn FieldName="Description" CellStyle-Wrap="False"></dx:GridViewDataColumn>--%>

                    <dx:GridViewDataColumn FieldName="CreationDate" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="CreationBy" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ApproveDate" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ApproveBy" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="IsActive" CellStyle-Wrap="False"></dx:GridViewDataColumn>


                    <dx:GridViewDataTextColumn FieldName="RequestDate" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataColumn FieldName="RequestBy" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>



                </Columns>

                <Templates>
                    <EditForm>

                        <dx:ASPxFormLayout ID="frmEditUnderwriter" ClientInstanceName="frmEditUnderwriter" runat="server" ColCount="2" RequiredMarkDisplayMode="None">
                            <Items>

                                <dx:LayoutItem Caption="Underwriter">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer
                                            ID="LayoutItemNestedControlContainer23"
                                            runat="server"
                                            SupportsDisabledAttribute="True">

                                            <dx:ASPxLabel runat="server" ID="Underwriter" Value='<%# Eval("Underwriter")%>'></dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>

                                <dx:LayoutItem Caption="Risk">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer
                                            ID="LayoutItemNestedControlContainer9"
                                            runat="server"
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel runat="server" ID="Risk" Value='<%# Eval("Risk")%>'></dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>

                                <dx:LayoutItem Caption="Brokerage" CaptionSettings-VerticalAlign="Middle">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">

                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxSpinEdit ID="Brokerage" runat="server" MaxLength="10" Width="100" ValidationSettings-ValidationGroup="frmEditUnderwriter"
                                                            MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" Value='<%# Eval("Brokerage")%>' NumberType="Float" DisplayFormatString="{0:n2}">
                                                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td>

                                                        <dx:ASPxRadioButtonList ID="BrokerageCal" runat="server" RepeatDirection="Horizontal" Value='<%# Eval("BrokerageCal")%>'>
                                                            <Items>
                                                                <dx:ListEditItem Text="%" Value="x" />
                                                                <dx:ListEditItem Text="บาท" Value="+" />
                                                            </Items>
                                                            <Border BorderStyle="None" />
                                                        </dx:ASPxRadioButtonList>
                                                    </td>

                                                </tr>
                                            </table>

                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>


                                <dx:LayoutItem Caption="SF" CaptionSettings-VerticalAlign="Middle">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">


                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxSpinEdit ID="OR" runat="server" MaxLength="10" Width="100" Value='<%# Eval("ORCommissionPercent")%>' ValidationSettings-ValidationGroup="frmEditUnderwriter"
                                                            MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false"  NumberType="Float" DisplayFormatString="{0:n2}">
                                                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td>

                                                        <dx:ASPxRadioButtonList ID="ORCal" runat="server" RepeatDirection="Horizontal" Value='<%# Eval("ORInCal")%>'>
                                                            <Items>
                                                                <dx:ListEditItem Text="%" Value="x" />
                                                                <dx:ListEditItem Text="บาท" Value="+" />
                                                            </Items>
                                                            <Border BorderStyle="None" />
                                                        </dx:ASPxRadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>


                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>

                                <dx:LayoutItem Caption="Admin1" CaptionSettings-VerticalAlign="Middle">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" SupportsDisabledAttribute="True">


                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxSpinEdit ID="Admin1" runat="server" MaxLength="10" Width="100" Value='<%# Eval("AdminFeeIn1")%>' ValidationSettings-ValidationGroup="frmEditUnderwriter"
                                                            MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" NumberType="Float" DisplayFormatString="{0:n2}">
                                                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td>

                                                        <dx:ASPxRadioButtonList ID="Admin1Cal" runat="server" RepeatDirection="Horizontal" Value='<%# Eval("AdminFeeIn1Cal")%>'>
                                                            <Items>
                                                                <dx:ListEditItem Text="%" Value="x" />
                                                                <dx:ListEditItem Text="บาท" Value="+" />
                                                            </Items>
                                                            <Border BorderStyle="None" />
                                                        </dx:ASPxRadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>


                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>


                                <dx:LayoutItem Caption="Admin2" CaptionSettings-VerticalAlign="Middle">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" SupportsDisabledAttribute="True">


                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxSpinEdit ID="Admin2" runat="server" MaxLength="10" Width="100" Value='<%# Eval("AdminFeeIn2")%>' ValidationSettings-ValidationGroup="frmEditUnderwriter"
                                                            MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false"  NumberType="Float" DisplayFormatString="{0:n2}">
                                                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td>

                                                        <dx:ASPxRadioButtonList ID="Admin2Cal" runat="server" RepeatDirection="Horizontal" Value='<%# Eval("AdminFeeIn2Cal")%>'>
                                                            <Items>
                                                                <dx:ListEditItem Text="%" Value="x" />
                                                                <dx:ListEditItem Text="บาท" Value="+" />
                                                            </Items>
                                                            <Border BorderStyle="None" />
                                                        </dx:ASPxRadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>

                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>


                                <dx:LayoutItem Caption="Upfont">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" SupportsDisabledAttribute="True">


                                            <dx:ASPxCheckBox runat="server" ID="OffsetORFlag" Value='<%# Eval("OffsetORFlag")%>'></dx:ASPxCheckBox>


                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>


                                <dx:LayoutItem Caption="CalFrom">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer21" runat="server" SupportsDisabledAttribute="True">

                                            <dx:ASPxComboBox ID="ORCalFrom" runat="server" Width="100" Value='<%# Eval("ORCalFrom")%>'>
                                                <Items>
                                                    <dx:ListEditItem Text="PREMIUM" Value="P" />
                                                    <dx:ListEditItem Text="BROKERAGE" Value="B" />
                                                </Items>
                                            </dx:ASPxComboBox>


                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>





                                <dx:LayoutItem Caption="Remark">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxMemo runat="server" ID="Remark" MaxLength="255" Value='<%# Eval("Remark")%>'></dx:ASPxMemo>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>

                                <dx:LayoutItem Caption="IsActive">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server" SupportsDisabledAttribute="True">

                                            <dx:ASPxCheckBox ID="IsActive" runat="server" Value='<%# Eval("IsActive")%>'>
                                            </dx:ASPxCheckBox>

                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>


                                <dx:LayoutItem ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">

                                            <table style="float: left">
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="b1" runat="server" UseSubmitBehavior="false" CausesValidation="true" AutoPostBack="false"
                                                            Text="Save">
                                                            <ClientSideEvents Click="function(s){
                                                                                if(ASPxClientEdit.ValidateGroup('frmEditUnderwriter')) 
                                                                                {
                                                                                        gridRiskUwriter.UpdateEdit();              
                                                                                }          
                                                                            }                                                                    
                                                                        " />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <dx:ASPxButton ID="b2" runat="server" UseSubmitBehavior="false" AutoPostBack="false"
                                                            Text="Cancel">
                                                            <ClientSideEvents Click="function(s){
                                                                        gridRiskUwriter.CancelEdit();
                                                                     }
                                                                        " />
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>

                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>


                            </Items>
                        </dx:ASPxFormLayout>






                    </EditForm>

                </Templates>
            </dx:ASPxGridView>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>



<asp:SqlDataSource ID="SqlDataSource_Underwriter" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="
            SELECT  rtrim(Underwriter) as Underwriter 
            , case 
            when ltrim(rtrim(AccountContact)) = '' then 
	            case when [InsuranceLine] = 1 then  rtrim(Underwriter)  + ' - ' + rtrim(name) + ' (Life)'
					            when [InsuranceLine] = 2 then  rtrim(Underwriter)  + ' - ' + rtrim(name) + ' (Non Life)'
					            else ''
	            end	 
            else 
	            case when [InsuranceLine] = 1 then  rtrim(Underwriter)  + ' - ' + ltrim(rtrim(AccountContact))  + ' (Life)'
					            when [InsuranceLine] = 2 then rtrim(Underwriter)  + ' - ' +  ltrim(rtrim(AccountContact))  + ' (Non Life)'
					            else ''
	            end
            end as UWName  
            FROM [Register.Unwriter]  
            where IsActive = 1 and  InsuranceLine in(1,2)
            ORDER BY AccountContact ,UWName
    
    "></asp:SqlDataSource>

<dx:ASPxPopupControl ID="popupRiskUnderwriter"
    ClientInstanceName="popupRiskUnderwriter"
    runat="server" AllowDragging="true"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    ShowPageScrollbarWhenModal="true"
    Modal="true"
    CloseAction="CloseButton"
    HeaderText="Comm In">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            <dx:ASPxCallbackPanel ID="callbackPanel_popupRiskUnderwriter"
                ClientInstanceName="callbackPanel_popupRiskUnderwriter" runat="server"
                RenderMode="Table">
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server">

                        <dx:ASPxFormLayout ID="frmNewUnderwriter" runat="server" ColCount="2" RequiredMarkDisplayMode="None">
                            <Items>

                                <dx:LayoutItem Caption="Underwriter" ColSpan="2">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer
                                            ID="LayoutItemNestedControlContainer23"
                                            runat="server"
                                            SupportsDisabledAttribute="True">


                                            <dx:ASPxComboBox ID="Uwriter" ClientInstanceName="Uwriter" runat="server"
                                                DropDownStyle="DropDownList" Width="600"
                                                DataSourceID="SqlDataSource_Underwriter"
                                                TextField="UWName"
                                                ValueField="Underwriter">
                                                <ClientSideEvents SelectedIndexChanged="function (s, e){
                                                    callbackPanel_popupRiskUnderwriter.PerformCallback( 'select_uw|' + s.GetValue());
                                                }" />
                                                <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                    <RequiredField IsRequired="true" />
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>

                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>

                                <dx:LayoutItem Caption="Brokerage" CaptionSettings-VerticalAlign="Middle">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">

                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxSpinEdit ID="Brokerage" runat="server" MaxLength="10" Width="100"
                                                            MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td>

                                                        <dx:ASPxRadioButtonList ID="BrokerageCal" runat="server" RepeatDirection="Horizontal">
                                                            <Items>
                                                                <dx:ListEditItem Text="%" Value="x" Selected="true" />
                                                                <dx:ListEditItem Text="บาท" Value="+" />
                                                            </Items>
                                                            <Border BorderStyle="None" />
                                                        </dx:ASPxRadioButtonList>
                                                    </td>

                                                </tr>
                                            </table>

                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>


                                <dx:LayoutItem Caption="SF" CaptionSettings-VerticalAlign="Middle">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">


                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxSpinEdit ID="OR" runat="server" MaxLength="10" Width="100"
                                                            MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td>

                                                        <dx:ASPxRadioButtonList ID="ORCal" runat="server" RepeatDirection="Horizontal">
                                                            <Items>
                                                                <dx:ListEditItem Text="%" Value="x" Selected="true" />
                                                                <dx:ListEditItem Text="บาท" Value="+" />
                                                            </Items>
                                                            <Border BorderStyle="None" />
                                                        </dx:ASPxRadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>


                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>

                                <dx:LayoutItem Caption="Admin1" CaptionSettings-VerticalAlign="Middle">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" SupportsDisabledAttribute="True">


                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxSpinEdit ID="Admin1" runat="server" MaxLength="10" Width="100"
                                                            MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td>

                                                        <dx:ASPxRadioButtonList ID="Admin1Cal" runat="server" RepeatDirection="Horizontal">
                                                            <Items>
                                                                <dx:ListEditItem Text="%" Value="x" Selected="true" />
                                                                <dx:ListEditItem Text="บาท" Value="+" />
                                                            </Items>
                                                            <Border BorderStyle="None" />
                                                        </dx:ASPxRadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>


                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>

                                <%--                                <dx:LayoutItem Caption="Admin1 (Upfont)">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer22" runat="server" SupportsDisabledAttribute="True">


                                            <dx:ASPxCheckBox runat="server" ID="OffsetAdm1Flag" Checked="true"></dx:ASPxCheckBox>


                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>--%>

                                <dx:LayoutItem Caption="Admin2" CaptionSettings-VerticalAlign="Middle">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" SupportsDisabledAttribute="True">


                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxSpinEdit ID="Admin2" runat="server" MaxLength="10" Width="100"
                                                            MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td>

                                                        <dx:ASPxRadioButtonList ID="Admin2Cal" runat="server" RepeatDirection="Horizontal">
                                                            <Items>
                                                                <dx:ListEditItem Text="%" Value="x" Selected="true" />
                                                                <dx:ListEditItem Text="บาท" Value="+" />
                                                            </Items>
                                                            <Border BorderStyle="None" />
                                                        </dx:ASPxRadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>

                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>

                                <%--                                <dx:LayoutItem Caption="Admin2 (Upfont)">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer24" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxCheckBox runat="server" ID="OffsetAdm2Flag" Checked="true"></dx:ASPxCheckBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>--%>

                                <%-- <dx:LayoutItem Caption="EffectiveDate">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" SupportsDisabledAttribute="True">

                                            <dx:ASPxDateEdit ID="EffectiveDate" ClientInstanceName="EffectiveDate" runat="server">
                                                <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                    <RequiredField IsRequired="true" />
                                                </ValidationSettings>

                                            </dx:ASPxDateEdit>

                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>

                                <dx:LayoutItem Caption="ExpiryDate">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server" SupportsDisabledAttribute="True">

                                            <dx:ASPxDateEdit ID="ExpiryDate" ClientInstanceName="ExpiryDate" runat="server">
                                                <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>

                                            </dx:ASPxDateEdit>

                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>--%>

                                <dx:LayoutItem Caption="Upfont">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" SupportsDisabledAttribute="True">


                                            <dx:ASPxCheckBox runat="server" ID="OffsetORFlag" Checked="true"></dx:ASPxCheckBox>


                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>


                                <dx:LayoutItem Caption="CalFrom">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer21" runat="server" SupportsDisabledAttribute="True">

                                            <dx:ASPxComboBox ID="CalFrom" runat="server" Caption=" " Width="100">
                                                <Items>
                                                    <dx:ListEditItem Text="PREMIUM" Value="P" Selected="true" />
                                                    <dx:ListEditItem Text="BROKERAGE" Value="B" />
                                                </Items>
                                            </dx:ASPxComboBox>


                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>





                                <dx:LayoutItem Caption="Remark">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxMemo runat="server" ID="Remark" MaxLength="255"></dx:ASPxMemo>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>

                                <dx:LayoutItem Caption="IsActive">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server" SupportsDisabledAttribute="True">

                                            <dx:ASPxCheckBox ID="UWIsActive" runat="server">
                                            </dx:ASPxCheckBox>

                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>

                                <dx:LayoutItem Caption="Risk" ColSpan="2">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server" SupportsDisabledAttribute="True">


                                            <dx:ASPxGridView ID="gridCOR" ClientInstanceName="gridCOR" DataSourceID="SqlDataSource_COR"
                                                runat="server" Width="600"
                                                KeyFieldName="Risk">
                                                <SettingsBehavior EnableRowHotTrack="true" />
                                                <SettingsSearchPanel Visible="true" />
                                                <SettingsPager Mode="EndlessPaging" PageSize="20" />
                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Width="40" ShowClearFilterButton="true" SelectAllCheckboxMode="AllPages" />
                                                    <dx:GridViewDataTextColumn FieldName="Risk" Width="100" Caption="Risk" CellStyle-Wrap="False" Settings-AllowHeaderFilter="True" />
                                                    <dx:GridViewDataTextColumn FieldName="Description" Width="150" Caption="Description" CellStyle-Wrap="False" Settings-AllowHeaderFilter="True" />
                                                    <dx:GridViewDataTextColumn FieldName="InsuranceLine" Width="150" Caption="InsuranceLine" CellStyle-Wrap="False" Settings-AllowHeaderFilter="True" />
                                                </Columns>
                                            </dx:ASPxGridView>


                                            <asp:SqlDataSource ID="SqlDataSource_COR" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
                                                SelectCommand="select Risk,Description,
                                                 case when [InsuranceLine] = 1 then 'Life'
	                                                when [InsuranceLine] = 2 then 'Non Life'
	                                             else ''
                                                 end as [InsuranceLine] from [Register.ClassOfRisk] 
                                                where risk not in(select risk from V_RiskUwriter where Underwriter=@Underwriter)
                                                and InsuranceLine=(select InsuranceLine from [Register.Unwriter] where Underwriter=@Underwriter)
                                                 
                                                ">
                                                <SelectParameters>
                                                     
                                                    <asp:SessionParameter Name="Underwriter" SessionField="Underwriter" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>

                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>




                                <dx:LayoutItem Caption="">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server" SupportsDisabledAttribute="True">

                                            <dx:ASPxButton ID="btnAddInsurer" runat="server" ValidationContainerID="frmNewUnderwriter"
                                                Text="Save" Width="100">
                                                <ClientSideEvents Click="function(s, e) {
                                                    if(ASPxClientEdit.AreEditorsValid()) {
                                                        LoadingPanel.Show();
                                                        cbAddInsurer.PerformCallback('');
                                                        e.processOnServer = true; 
                                                    }
                                                    else
                                                    {
                                            
                                                    }

                                                    e.processOnServer = false;
                                                }" />
                                            </dx:ASPxButton>

                                            <dx:ASPxCallback runat="server" ID="cbAddInsurer" ClientInstanceName="cbAddInsurer">
                                                <ClientSideEvents CallbackComplete="function(s,e){                                                                                     
                                                    LoadingPanel.Hide(); 

                                                    if(e.result == 'success')
                                                    {
                                                        popupRiskUnderwriter.Hide();
                                                        gridRiskUwriter.ApplySearchPanelFilter(Uwriter.GetValue());
                                                    }                                                     
                                                    else
                                                    {
                                                        alert(e.result);
                                                    }
                                           
                                                }" />

                                            </dx:ASPxCallback>

                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>




                            </Items>
                        </dx:ASPxFormLayout>





                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>





<dx:ASPxPopupControl ID="popupMoreInfo"
    ClientInstanceName="popupMoreInfo"
    runat="server" AllowDragging="true"
    PopupHorizontalAlign="LeftSides"
    PopupVerticalAlign="WindowCenter"
    ShowPageScrollbarWhenModal="true"
    Modal="true"
    CloseAction="CloseButton" ShowMaximizeButton="true" ScrollBars="Auto"
    HeaderText="Comm Out">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
            <dx:ASPxCallbackPanel ID="callbackPanel_MoreInfo" ClientInstanceName="callbackPanel_MoreInfo" runat="server"
                RenderMode="Table">
                <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide();}" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent2" runat="server">


                        <dx:ASPxFormLayout runat="server" ID="settingsFormLayout" AlignItemCaptionsInAllGroups="True" Width="100%" SettingsItemCaptions-HorizontalAlign="Right">
                            <Items>

                                <dx:LayoutGroup Caption="Class Of Risk" GroupBoxDecoration="HeadingLine" ColCount="2">
                                    <Items>
                                        <dx:LayoutItem Caption="Risk">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxLabel runat="server" ID="lbRisk" ClientInstanceName="lbRisk"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Description">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxLabel runat="server" ID="lbDescription"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Department">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxLabel runat="server" ID="lbDepartment"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem Caption="InsuranceLine">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="lbInsuranceLine"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="RiskShortDesc">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxLabel runat="server" ID="lbRiskShortDesc"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Status">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <%--<dx:ASPxLabel runat="server" ID="lbStatus"></dx:ASPxLabel>--%>
                                                    <dx:ASPxImage ID="lbStatus" Height="15" runat="server"></dx:ASPxImage>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Risk I">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="lbRiskGroupI"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Risk II">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="lbRiskGroupII"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Risk Government" ColSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="lbRiskGovernment"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>


                                                    <dx:ASPxButton ID="btnRequestCOR" ClientInstanceName="btnRequestCOR" runat="server" Text="Request - Class Of Risk">
                                                        <ClientSideEvents Click="function(s, e) {                                                                 
                                                                    LoadingPanel.Show();
                                                                    cbRequestCOR.PerformCallback(lbRisk.GetText());
                                                                    e.processOnServer = false;

                                                                    }
                                                                    " />
                                                    </dx:ASPxButton>

                                                    <dx:ASPxCallback runat="server" ID="cbRequestCOR" ClientInstanceName="cbRequestCOR">
                                                        <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide();}"
                                                            CallbackComplete="function(s,e){
                                                                    LoadingPanel.Hide(); 
                                                                    if(e.result == 'success')
                                                                    {
                                                                         //callbackPanel_MoreInfo.PerformCallback(lbRisk.GetText());
                                                                         //gridCOR.Refresh();
                                                                         popupMoreInfo.Hide();
                                                                         gridCOR.Refresh();
                                                                         gridRiskUwriter.Refresh();
                                                                    }                                                     
                                                                    else
                                                                    {
                                                                        alert(e.result);
                                                                    }
                                                            }" />
                                                    </dx:ASPxCallback>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
















                                    </Items>
                                </dx:LayoutGroup>


                                <dx:LayoutGroup Caption="Commission In" GroupBoxDecoration="HeadingLine" ColCount="3">
                                    <Items>


                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>


                                                    <dx:ASPxGridView ID="gridCommIn" ClientInstanceName="gridCommIn" runat="server" DataSourceID="SqlDataSource_CommIn"
                                                        KeyFieldName="ID" AutoGenerateColumns="False" Width="100%" SettingsBehavior-AutoExpandAllGroups="true">
                                                        <SettingsPager Mode="ShowPager">
                                                            <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                                                        </SettingsPager>
                                                        <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" />
                                                        <SettingsSearchPanel Visible="true" />
                                                        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
                                                       <%-- <SettingsCommandButton>
                                                            <UpdateButton ButtonType="Button" Text="บันทึก"></UpdateButton>
                                                            <EditButton ButtonType="Button" Text="แก้ไข"></EditButton>
                                                            <DeleteButton ButtonType="Button" Text="ลบ"></DeleteButton>
                                                            <CancelButton ButtonType="Button" Text="ยกเลิก"></CancelButton>
                                                        </SettingsCommandButton>--%>


                                                        <SettingsText PopupEditFormCaption="แก้ไขรายการ" ConfirmDelete="ยืนยันการลบ" />
                                                        <SettingsPopup>
                                                            <EditForm Modal="true" MinWidth="100" Width="400" HorizontalAlign="WindowCenter" VerticalAlign="Middle" />
                                                            <HeaderFilter Height="200" />
                                                        </SettingsPopup>
                                                        <Settings ShowHeaderFilterButton="true" ShowGroupPanel="true" />



                                                        <Columns>

                                                            <dx:GridViewDataColumn FieldName="Underwriter" Caption="UCode" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="AccountContact" Caption="Name" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                                                            <dx:GridViewDataColumn FieldName="Brokerage_display" Caption="Brokerage" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="ORCommissionPercent_display" Caption="SF" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="AdminFeeIn1_display" Caption="Admin1" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="AdminFeeIn2_display" Caption="Admin2" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                                                            <dx:GridViewDataColumn FieldName="OffsetORFlag" Caption="UpFront" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="Remark" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                                                          


                                                            <dx:GridViewDataColumn FieldName="CreationDate" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="CreationBy" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="ApproveDate" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="ApproveBy" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="IsActive" CellStyle-Wrap="False"></dx:GridViewDataColumn>


                                                            <dx:GridViewDataTextColumn FieldName="RequestDate" CellStyle-Wrap="False">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataTextColumn>

                                                            <dx:GridViewDataColumn FieldName="RequestBy" CellStyle-Wrap="False">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataColumn>

  <dx:GridViewDataColumn FieldName="status" Caption="Status" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                                <DataItemTemplate>
                                                                    <img src='images/<%# Eval("status").ToString().Trim()%>.gif' />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>


                                                        </Columns>

                                                    </dx:ASPxGridView>


                                                    <asp:SqlDataSource ID="SqlDataSource_CommIn" runat="server"
                                                        ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
                                                        SelectCommand="SELECT * from V_RiskUwriter where Risk=@Risk">
                                                        <SelectParameters>
                                                            <asp:SessionParameter Name="Risk" SessionField="Risk" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>



























                                                    <%--                                                    <dx:ASPxButton ID="btnRequestCommIn" runat="server" Text="Request - Commission In">
                                                        <ClientSideEvents Click="function(s, e) {                                                                 
                                                                    LoadingPanel.Show();
                                                                    cbRequestCommIn.PerformCallback(lbRisk.GetText());
                                                                    e.processOnServer = false;

                                                                    }
                                                                    " />

                                                    </dx:ASPxButton>



                                                    <dx:ASPxCallback runat="server" ID="cbRequestCommIn" ClientInstanceName="cbRequestCommIn">
                                                        <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide();}"
                                                            CallbackComplete="function(s,e){
                                                                    LoadingPanel.Hide(); 
                                                                    if(e.result == 'success')
                                                                    {
                                                                         callbackPanel_MoreInfo.PerformCallback(lbRisk.GetText());
                                                                         gridCOR.Refresh();
                                                                    }                                                     
                                                                    else
                                                                    {
                                                                        alert(e.result);
                                                                    }
                                                            }" />
                                                    </dx:ASPxCallback>--%>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>

                                <dx:LayoutGroup Caption="Commission Out" GroupBoxDecoration="HeadingLine" ColCount="2">
                                    <Items>
                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxGridView ID="gridCommOut" ClientInstanceName="gridCommOut" DataSourceID="SqlDataSource_CommOut"
                                                        runat="server"
                                                        KeyFieldName="ID">

                                                        <SettingsPager Mode="ShowPager">
                                                            <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                                                        </SettingsPager>
                                                        <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" />
                                                        <SettingsSearchPanel Visible="true" />
                                                        <Settings ShowHeaderFilterButton="true" />


                                                        <Columns>
                                                            <%--<dx:GridViewDataTextColumn FieldName="Risk" Caption="Risk" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />--%>
                                                            <dx:GridViewDataTextColumn FieldName="Agent" Caption="Code" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="Name" Caption="Name" CellStyle-Wrap="False" />
                                                            <dx:GridViewDataTextColumn FieldName="CommissionOut_Display" Caption="CommOut" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="OROut_Display" Caption="SF" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="AdminOut1_Display" Caption="Admin1" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="AdminOut2_Display" Caption="Admin2" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <%--            <dx:GridViewDataTextColumn FieldName="CreationDate"  CellStyle-Wrap="False"/>
            <dx:GridViewDataTextColumn FieldName="CreationBy"  CellStyle-Wrap="False"/>
                                                            --%>
                                                            <%--<dx:GridViewDataTextColumn FieldName="OROutCalFrom" Caption="OROutCalFrom"  CellStyle-Wrap="False"/>--%>


                                                            <dx:GridViewDataTextColumn FieldName="Remark" Caption="Remark" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />



                                                            <dx:GridViewDataTextColumn FieldName="AgentGroup" Caption="Group" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                           

                                                            <%-- <dx:GridViewDataColumn FieldName="HasOverComm" Caption="OverComm" CellStyle-Wrap="False"></dx:GridViewDataColumn>--%>

                                                            <dx:GridViewDataColumn FieldName="HasOverComm" Caption="OverComm" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                                <DataItemTemplate>
                                                                    <img src='images/alert_<%# Eval("HasOverComm").ToString().Trim()%>.gif' style="height: 15px;" />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>

                                                             <dx:GridViewDataColumn FieldName="IsActive" CellStyle-Wrap="False" />




                                                              <dx:GridViewDataTextColumn FieldName="RequestDate" CellStyle-Wrap="False">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataTextColumn>

                                                            <dx:GridViewDataColumn FieldName="RequestBy" CellStyle-Wrap="False">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataColumn>



                                                            <dx:GridViewDataColumn FieldName="status" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                                <DataItemTemplate>
                                                                    <img src='images/<%# Eval("status").ToString().Trim()%>.gif' />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>



                                                        </Columns>
                                                    </dx:ASPxGridView>

                                                    <asp:SqlDataSource ID="SqlDataSource_CommOut" runat="server"
                                                        ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
                                                        SelectCommand="SELECT * from V_AgentRiskComm where Risk=@Risk">
                                                        <SelectParameters>
                                                            <asp:SessionParameter Name="Risk" SessionField="Risk" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                                    <%-- <br />
                                                    <dx:ASPxButton ID="btnRequestCommOut" runat="server" Text="Request - Commission Out">
                                                        <ClientSideEvents Click="function(s, e) {                                                                 
                                                                    LoadingPanel.Show();
                                                                    cbRequestCommOut.PerformCallback(lbRisk.GetText());
                                                                    e.processOnServer = false;

                                                                    }
                                                                    " />

                                                    </dx:ASPxButton>

                                                    <dx:ASPxCallback runat="server" ID="cbRequestCommOut" ClientInstanceName="cbRequestCommOut">
                                                        <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide();}"
                                                            CallbackComplete="function(s,e){
                                                                    LoadingPanel.Hide(); 
                                                                    if(e.result == 'success')
                                                                    {
                                                                         callbackPanel_MoreInfo.PerformCallback(lbRisk.GetText());
                                                                         gridCOR.Refresh();
                                                                    }                                                     
                                                                    else
                                                                    {
                                                                        alert(e.result);
                                                                    }
                                                            }" />
                                                    </dx:ASPxCallback>--%>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>




                                    </Items>
                                </dx:LayoutGroup>

                            </Items>
                        </dx:ASPxFormLayout>





                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <ClientSideEvents Shown="popup_ShownCommOut" />
</dx:ASPxPopupControl>











