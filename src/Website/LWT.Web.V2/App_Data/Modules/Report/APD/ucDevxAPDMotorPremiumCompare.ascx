<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxAPDMotorPremiumCompare.ascx.vb" Inherits="Modules_ucDevxAPDMotorPremiumCompare" %>
<br />
<style>
    .headerfixed {
        position: fixed;
        /*width: 200px;*/
        /*left: 15px;*/
        top: 130px;
    }

    .dxcvTable_Office2010Blue {
        /*position: fixed;
        width: 840px;*/
        /*left: 15px;*/
        top: 150px;
    }
</style>

<div style="width: 250px; height: 400px; position: fixed; top:120PX">
    <dx:ASPxFormLayout runat="server" ID="exampleFormLayout" EnableViewState="false" EncodeHtml="false"
        Width="240">
        <Items>
            <dx:LayoutGroup Caption="กรองข้อมูล" SettingsItemHelpTexts-Position="Top" GroupBoxDecoration="Box">
                <Items>
                    <dx:LayoutItem Caption="บริษัทประกัน">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>

                                <dx:ASPxComboBox runat="server" DropDownStyle="DropDownList" ID="SearchInsurer" DataSourceID="SqlDataSource_insurer" Width="130" TextField="InsurerName" SelectedIndex="0">
                                </dx:ASPxComboBox>


                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="ประเภท">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>

                                <dx:ASPxComboBox runat="server" DropDownStyle="DropDownList" ID="ASPxComboBox6" Width="130" SelectedIndex="0">
                                    <Items>
                                        <%--<dx:ListEditItem Text="ไม่ระบุ" Value="0" />--%>
                                        <dx:ListEditItem Text="110 - รถยนต์นั่งส่วนบุคคล" Value="110" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="ชั้นประกัน">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>

                                <dx:ASPxComboBox runat="server" DropDownStyle="DropDownList" ID="ASPxComboBox1" Width="130" SelectedIndex="0">
                                    <Items>
                                        <%--<dx:ListEditItem Text="ไม่ระบุ" Value="0" />--%>
                                        <dx:ListEditItem Text="1" />
                                        <dx:ListEditItem Text="2+" />
                                        <dx:ListEditItem Text="3+" />
                                        <dx:ListEditItem Text="3" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="ปี">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>

                                <dx:ASPxComboBox runat="server" DropDownStyle="DropDownList" ID="ASPxComboBox2" DataSourceID="SqlDataSource_CarYear" TextField="ModelYear" Width="130" SelectedIndex="0">
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="ยี่ห้อ">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>

                                <dx:ASPxComboBox runat="server" DropDownStyle="DropDownList" ID="ASPxComboBox8" Width="130" SelectedIndex="0">
                                    <Items>
                                        <dx:ListEditItem Value="NISSAN" />
                                    </Items>

                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="รุ่น">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>

                                <dx:ASPxComboBox runat="server" DropDownStyle="DropDownList" ID="ASPxComboBox3" DataSourceID="SqlDataSource_Model" TextField="ModelName" Width="130" SelectedIndex="0">
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <%--  <dx:LayoutItem Caption="ขนาด">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox runat="server" DropDownStyle="DropDownList" ID="ASPxComboBox7" Width="130" SelectedIndex="0">
                                    <Items>
                                        <dx:ListEditItem Text="ไม่ระบุ" Value="0" />
                                        <dx:ListEditItem Text="<= 2000 cc" Value="1" />
                                        <dx:ListEditItem Text="> 2000 cc" Value="2" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>--%>
                    <dx:LayoutItem Caption="ทุน">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox runat="server" DropDownStyle="DropDownList" ID="ASPxComboBox4" DataSourceID="SqlDataSource_Suminsured" TextField="OD" Width="130" SelectedIndex="0">
                                    <Items>
                                    </Items>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="ซ่อม">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox runat="server" DropDownStyle="DropDownList" ID="ASPxComboBox5" DataSourceID="SqlDataSource_Garage" TextField="FIX" Width="130" SelectedIndex="0">
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="Deduct">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox runat="server" DropDownStyle="DropDownList" ID="ASPxComboBox7" DataSourceID="SqlDataSource_ODDD" TextField="ODDD" Width="130" SelectedIndex="0">
                                </dx:ASPxComboBox>

                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>




                    <dx:LayoutItem ShowCaption="false" RequiredMarkDisplayMode="Hidden" HorizontalAlign="Center" Width="100">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="submitButton" Text="ค้นหา" Width="100" />
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
</div>

<table>
    <tr>
        <td style="width: 250px"></td>
        <td>


            <br />
            <br />
            <br />

            <dx:ASPxCardView ID="CardView" runat="server"
                DataSourceID="DataSource_MotorPremiumCompare"
                KeyFieldName="ID"
                EnableCardsCache="false"
                Width="840">
                <Columns>
                    <dx:CardViewColumn FieldName="PlanName" />
                    <dx:CardViewColumn FieldName="Class" Settings-AllowHeaderFilter="True" />
                    <dx:CardViewTextColumn FieldName="OD" Settings-AllowHeaderFilter="True">
                        <PropertiesTextEdit DisplayFormatString="{0:N0}" />
                    </dx:CardViewTextColumn>
                    <dx:CardViewTextColumn FieldName="Flood">
                        <PropertiesTextEdit DisplayFormatString="{0:N0}" />
                    </dx:CardViewTextColumn>
                    <dx:CardViewTextColumn FieldName="ODDD">
                        <PropertiesTextEdit DisplayFormatString="{0:N0}" />
                    </dx:CardViewTextColumn>
                    <dx:CardViewColumn FieldName="FIX" />
                    <dx:CardViewTextColumn FieldName="TP">
                        <PropertiesTextEdit DisplayFormatString="{0:N0}" />
                    </dx:CardViewTextColumn>
                    <dx:CardViewTextColumn FieldName="Premium">
                        <PropertiesTextEdit DisplayFormatString="c" />
                    </dx:CardViewTextColumn>

                    <dx:CardViewImageColumn FieldName="InsurerSmallLogo" Settings-AllowSort="False">
                        <PropertiesImage ImageWidth="100" />
                    </dx:CardViewImageColumn>

                    <dx:CardViewTextColumn FieldName="OriginalPremium">
                        <PropertiesTextEdit DisplayFormatString="c" />
                    </dx:CardViewTextColumn>
                    <dx:CardViewTextColumn FieldName="DiscountValue">
                        <PropertiesTextEdit DisplayFormatString="c" />
                    </dx:CardViewTextColumn>
                    <dx:CardViewTextColumn FieldName="PromotionTitle">
                        <PropertiesTextEdit />
                    </dx:CardViewTextColumn>
                    <dx:CardViewTextColumn FieldName="CheckoutNotes">
                        <PropertiesTextEdit />
                    </dx:CardViewTextColumn>

                </Columns>


                <CardLayoutProperties ColCount="1" ShowItemCaptionColon="false">
                    <Items>
                        <dx:CardViewColumnLayoutItem ColumnName="InsurerSmallLogo" ShowCaption="False" VerticalAlign="Top" HorizontalAlign="Center" />
                        <dx:CardViewColumnLayoutItem ColumnName="PlanName" Caption="แผน:" ShowCaption="False" HorizontalAlign="Center">
                            <ParentContainerStyle Font-Bold="true" />
                        </dx:CardViewColumnLayoutItem>


                        <dx:CardViewColumnLayoutItem ColumnName="OriginalPremium" Caption="เบี้ยประกัน:" />
                        <dx:CardViewColumnLayoutItem ColumnName="DiscountValue" Caption="ส่วนลด:" />
                        <dx:CardViewColumnLayoutItem ColumnName="Premium" Caption="เบี้ยรวม:">
                            <ParentContainerStyle Font-Bold="true" />
                        </dx:CardViewColumnLayoutItem>


                        <dx:CardViewColumnLayoutItem ShowCaption="False" VerticalAlign="Top" HorizontalAlign="Center">
                            <ParentContainerStyle Font-Bold="true" />
                            <Template>
                                ความคุ้มครอง
                            </Template>
                        </dx:CardViewColumnLayoutItem>


                        <dx:CardViewColumnLayoutItem ColumnName="Class" Caption="ประกันชั้น:" />
                        <dx:CardViewColumnLayoutItem ColumnName="OD" Caption="ทุนประกัน:" />
                        <dx:CardViewColumnLayoutItem ColumnName="Flood" Caption="คุ้มครองน้ำท่วม:" />
                        <dx:CardViewColumnLayoutItem ColumnName="TP" Caption="ทรัพย์สินบุคคลภายนอก:" />

                        <dx:CardViewColumnLayoutItem ColumnName="ODDD" Caption="ค่าเสียหายส่วนแรก:" />
                        <dx:CardViewColumnLayoutItem ColumnName="FIX" Caption="ซ่อม:" />

                        <dx:CardViewColumnLayoutItem ShowCaption="False" VerticalAlign="Top" HorizontalAlign="Center">
                            <ParentContainerStyle Font-Bold="true" />
                            <Template>
                                โปรโมชั่น/ของแถม
                            </Template>
                        </dx:CardViewColumnLayoutItem>


                        <dx:CardViewColumnLayoutItem ShowCaption="False" ColumnName="PromotionTitle" Caption="โปรโมชั่น:" HorizontalAlign="Left">
                            <ParentContainerStyle Font-Bold="true" />
                        </dx:CardViewColumnLayoutItem>
                        <dx:CardViewColumnLayoutItem ShowCaption="False" ColumnName="CheckoutNotes" Caption="เมื่อชำระ online:" HorizontalAlign="Left">
                            <ParentContainerStyle Font-Bold="true" />
                        </dx:CardViewColumnLayoutItem>

                    </Items>
                </CardLayoutProperties>
                <SettingsPager Mode="EndlessPaging" SettingsTableLayout-ColumnCount="3" />
                <Styles>
                    <Card Width="450" />
                    <HeaderPanel CssClass="headerfixed"></HeaderPanel>
                </Styles>
                <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="false" ShowAllDataSourceColumns="false" MaxHierarchyDepth="1" />
                <Settings VerticalScrollableHeight="600" />
                <Settings ShowFilterBar="Visible" EnableFilterControlPopupMenuScrolling="true" />
                <Settings ShowHeaderPanel="true" LayoutMode="Table" />

            </dx:ASPxCardView>

        </td>

    </tr>
</table>

<dx:LinqServerModeDataSource ID="DataSource_MotorPremiumCompare" runat="server" ContextTypeName="LargeDatabaseContext" TableName="V_MotorPremiumExts" />




<asp:SqlDataSource ID="SqlDataSource_insurer" runat="server"
    ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select '-' as InsurerName union select InsurerName from V_MotorPremiumExt group by InsurerName order by InsurerName "></asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_CarYear" runat="server"
    ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select ModelYear from V_MotorPremiumExt group by ModelYear order by ModelYear Desc "></asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_Model" runat="server"
    ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select ModelName from V_MotorPremiumExt group by ModelName order by ModelName "></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_Suminsured" runat="server"
    ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select OD from V_MotorPremiumExt where OD <> 0 group by OD order by OD "></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_Garage" runat="server"
    ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select FIX from V_MotorPremiumExt group by FIX order by FIX "></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_ODDD" runat="server"
    ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select '-' as ODDD union all select ODDD from V_MotorPremiumExt where ODDD <> 0 group by ODDD order by ODDD "></asp:SqlDataSource>
