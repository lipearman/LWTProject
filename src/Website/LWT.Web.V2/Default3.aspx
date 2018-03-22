<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default3.aspx.vb" Inherits="Default3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

 <script type="text/javascript">
     function OnInit(s, e) {
         UpdateSelectedValuesListBox();
     }
     function OnRecordSelectionChanged(s, e) {
         UpdateSelectedValuesListBox();
     }
     function UpdateSelectedValuesListBox() {
         grid.GetSelectedFieldValues("IDCodeData", GetSelectedFieldValuesCallback);
     }
     function GetSelectedFieldValuesCallback(values) {
         selectionList.BeginUpdate();
         try {
             selectionList.ClearItems();
             for (var i = 0; i < values.length; i++) {
                 var item = values[i];
                 //var texts = [item[0], '$' + item[1]];
                 selectionList.AddItem(item);
             }
         } finally {
             selectionList.EndUpdate();
         }
         document.getElementById("selCount").innerHTML = grid.GetSelectedRecordCount();
     }
    </script>


  <div style="float: left; width: 60%">
        <div class="BottomPadding">
            Click checkbox to select item:
        </div>

            <dx:ASPxVerticalGrid ID="VerticalGrid" 
                SettingsBehavior-AllowSort="false" 
                runat="server"  KeyFieldName="IDCodeData" 
                DataSourceID="SqlDataSource_RENEWALL" 
                ClientInstanceName="grid"
                >
                
                 <ClientSideEvents SelectionChanged="OnRecordSelectionChanged" />
                
                <Rows>

                             
                  <dx:VerticalGridCommandRow    ShowSelectCheckbox="true" >
                        
                        
                    </dx:VerticalGridCommandRow>




                    <dx:VerticalGridTextRow FieldName="ข้อเสนอ" RecordStyle-HorizontalAlign="Center"  />
                    <dx:VerticalGridTextRow FieldName="บริษัทประกันภัย" RecordStyle-HorizontalAlign="Center" />
                    <dx:VerticalGridTextRow FieldName="การซ่อม" RecordStyle-HorizontalAlign="Center" />
                    <dx:VerticalGridTextRow FieldName="ประเภทความคุ้มครอง" RecordStyle-HorizontalAlign="Center" />
                    <dx:VerticalGridTextRow FieldName="อุปกรณ์เพิ่มเติมพิเศษ" RecordStyle-HorizontalAlign="Center" />
                    <dx:VerticalGridTextRow FieldName="การระบุผู้ขับขี่" RecordStyle-HorizontalAlign="Center" />
                    <dx:VerticalGridTextRow FieldName="ชื่อผู้ขับขี่ 1 / อายุ" RecordStyle-HorizontalAlign="Center" />
                    <dx:VerticalGridTextRow FieldName="ชื่อผู้ขับขี่ 2 / อายุ" RecordStyle-HorizontalAlign="Center" />

                    <dx:VerticalGridCategoryRow Caption="รายละเอียดความคุ้มครอง">
                        <Rows>
                            <dx:VerticalGridTextRow FieldName="ความรับผิดต่อบุคคลภายนอก" RecordStyle-HorizontalAlign="Right" />
                            <dx:VerticalGridTextRow FieldName="- บาดเจ็บหรือเสียชีวิต/คน" RecordStyle-HorizontalAlign="Right" />
                            <dx:VerticalGridTextRow FieldName="- บาดเจ็บหรือเสียชัวิต/ครั้ง" RecordStyle-HorizontalAlign="Right" />
                            <dx:VerticalGridTextRow FieldName="- ทรัพย์สิน" RecordStyle-HorizontalAlign="Right" />

                            <dx:VerticalGridTextRow FieldName="ความเสียหายต่อรถยนต์" RecordStyle-HorizontalAlign="Right" />
                            <dx:VerticalGridTextRow FieldName="- ความเสียหายต่อตัวรถ" RecordStyle-HorizontalAlign="Right" />
                            <dx:VerticalGridTextRow FieldName="- การสูญหาย" RecordStyle-HorizontalAlign="Right" />
                            <dx:VerticalGridTextRow FieldName="- ไฟใหม้" RecordStyle-HorizontalAlign="Right" />

                            <dx:VerticalGridTextRow FieldName="เอกสารแนบท้ายคุ้มครองเพิ่ม" RecordStyle-HorizontalAlign="Right" />
                            <dx:VerticalGridTextRow FieldName="- การประกันอุบัติเหตุส่วนบุคล (ผู้ขับขี่+ผู้โดยสาร)" RecordStyle-HorizontalAlign="Right" />
                            <dx:VerticalGridTextRow FieldName="- การประกันค่ารักษาพยาบาล (ผู้ขับขี่+ผู้โดยสาร)" RecordStyle-HorizontalAlign="Right" />
                            <dx:VerticalGridTextRow FieldName="- การประกันตัวผู้ขับขี่คดีอาญา" RecordStyle-HorizontalAlign="Right" />






                        </Rows>
                    </dx:VerticalGridCategoryRow>

                    <dx:VerticalGridCategoryRow Caption="ความเสียหายส่วนแรก">
                        <Rows>
                            <dx:VerticalGridTextRow FieldName="ความเสียหายส่วนแรก 1 (OD)" RecordStyle-HorizontalAlign="Center" />
                            <dx:VerticalGridTextRow FieldName="ความเสียหายส่วนแรก 2 (TPPD)" RecordStyle-HorizontalAlign="Center" />
                        </Rows>
                    </dx:VerticalGridCategoryRow>
                    <dx:VerticalGridCategoryRow Caption="ส่วนลด">
                        <Rows>
                            <dx:VerticalGridTextRow FieldName="ส่วนลดกลุ่ม (%)" RecordStyle-HorizontalAlign="Center" />
                            <dx:VerticalGridTextRow FieldName="ส่วนลดประวัติดี (%)" RecordStyle-HorizontalAlign="Center" />
                            <dx:VerticalGridTextRow FieldName="ส่วนลดอื่นๆ (%)" RecordStyle-HorizontalAlign="Center" />


                        </Rows>
                    </dx:VerticalGridCategoryRow>

                    <dx:VerticalGridCategoryRow Caption="ส่วนเพิ่ม">
                        <Rows>

                            <dx:VerticalGridTextRow FieldName="ส่วนเพิ่มประวัติดี (%)" RecordStyle-HorizontalAlign="Center" />
                            <dx:VerticalGridTextRow FieldName="ประวัติความเสียหาย" RecordStyle-HorizontalAlign="Center" />
                            <dx:VerticalGridTextRow FieldName="สรุปถึงวันที่" RecordStyle-HorizontalAlign="Center" />

                        </Rows>
                    </dx:VerticalGridCategoryRow>

                    <dx:VerticalGridCategoryRow Caption="เบี้ยประกัน">
                        <Rows>


                            <dx:VerticalGridTextRow FieldName="เบี้ยสุทธิ" RecordStyle-HorizontalAlign="Right" RecordStyle-Font-Bold="true" />
                            <dx:VerticalGridTextRow FieldName="แสตมป์" RecordStyle-HorizontalAlign="Right" />
                            <dx:VerticalGridTextRow FieldName="ภาษี" RecordStyle-HorizontalAlign="Right" />
                            <dx:VerticalGridTextRow FieldName="เบี้ยสุทธิรวมภาษี" RecordStyle-HorizontalAlign="Right" />
                            <dx:VerticalGridTextRow FieldName="ส่วนลดLWT" RecordStyle-HorizontalAlign="Right" RecordStyle-Font-Bold="true" />
                            <dx:VerticalGridTextRow FieldName="พรบ." RecordStyle-HorizontalAlign="Right" RecordStyle-Font-Bold="true" />
                            <dx:VerticalGridTextRow FieldName="ส่วนลด พรบ." RecordStyle-HorizontalAlign="Right" RecordStyle-Font-Bold="true" />
                            <dx:VerticalGridTextRow FieldName="เบี้ยรวม พรบ." RecordStyle-HorizontalAlign="Right" RecordStyle-Font-Bold="true" />
                            <dx:VerticalGridTextRow FieldName="เบี้ยสุทธิรวมภาษี(ส่วนลดLWT/พรบ.)" RecordStyle-HorizontalAlign="Right" RecordStyle-Font-Bold="true" />

                        </Rows>
                    </dx:VerticalGridCategoryRow>
                   



                      <dx:VerticalGridCommandRow ButtonRenderMode="Button"  >
                        <CustomButtons>
                            <dx:VerticalGridCommandRowCustomButton Text="Select">
                                
                            </dx:VerticalGridCommandRowCustomButton>
                        </CustomButtons>
                        
                    </dx:VerticalGridCommandRow>


                <%--     <dx:VerticalGridDataRow Caption=" " RecordStyle-HorizontalAlign="Center" >
                        <DataItemTemplate>

                            <dx:ASPxButton ID="ASPxButton1" runat="server"   AutoPostBack="false" Text="เลือก">
                                <ClientSideEvents Click="function(s,e){
                                     alert(e.visibleIndex);
                                    }" />
                            </dx:ASPxButton>
                        </DataItemTemplate>
                    </dx:VerticalGridDataRow>--%>


                </Rows>
                <Settings ShowCategoryIndents="false" HeaderAreaWidth="180" />


                <ClientSideEvents CustomButtonClick="function(s,e){
                       alert( e.visibleIndex );
                    
                    }"  />
            </dx:ASPxVerticalGrid>



      </div>




<div style="float: right; width: 40%">
        <div class="BottomPadding">
            Selected values:
        </div>
        <dx:ASPxListBox ID="ASPxListBox1" ClientInstanceName="selectionList" runat="server" Width="200" Height="250px">
            <ClientSideEvents Init="OnInit" />
            <Columns>
                <dx:ListBoxColumn FieldName="IDCodeData" Width="100%" /> 
            </Columns>
        </dx:ASPxListBox>
        <div class="TopPadding">
            Selected count: <span id="selCount" style="font-weight: bold">0</span>
            <br />
            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="พิมพ์ในเสนอราคา"></dx:ASPxButton>
        </div>
    </div>










            <asp:SqlDataSource ID="SqlDataSource_RENEWALL" runat="server" ConnectionString="<%$ ConnectionStrings:TidmasterConnectionString %>"
                SelectCommand="
    select *
    from
    (
	    SELECT * FROM [LWTReports].[dbo].[V_M1_RENEWALL_OLD] where IDCodeData like 'RN1504030728%'  
	    union
	    SELECT * FROM [LWTReports].[dbo].[V_M1_RENEWALL_NEW] where IDCodeData like 'RN1504030728%'  
	    union
	    SELECT * FROM [LWTReports].[dbo].[V_M1_RENEWALL_NEW2] where IDCodeData like 'RN1504030728%'  

    ) a
    order by [ข้อเสนอ]
    "></asp:SqlDataSource>

        
    </form>
</body>
</html>
