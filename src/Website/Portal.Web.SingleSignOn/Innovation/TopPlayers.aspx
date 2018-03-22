<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TopPlayers.aspx.vb" Inherits="Portal.Web.SingleSignOn.TopPlayers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <dx:ASPxGridView ID="grid"
            ClientInstanceName="grid"
            runat="server" Settings-ShowColumnHeaders="false"
            DataSourceID="SqlDataSource_UserRank"
            KeyFieldName="sAMAccountName"
            Width="700">


            <Columns>
                <dx:GridViewDataColumn Width="250" CellStyle-HorizontalAlign="Center">
                    <DataItemTemplate>
 <img src='http://lockthbnkedc01/sites/HRDweb/LWT%20Member%20Photos/<%# Eval("sAMAccountName")%>.jpg' />

                                    <br /><h1><%# Eval("Rank")%></h1>





                    </DataItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn CellStyle-VerticalAlign="Top">
                    <DataItemTemplate>
                        <b><%# Eval("sAMAccountName")%>  (<%# Eval("department")%>)</b><br />

                        <dx:ASPxVerticalGrid ID="ASPxVerticalGrid1" runat="server"
                            DataSourceID="SqlDataSource_UserPokemon"
                            SettingsPager-Mode="ShowAllRecords" 
                            KeyFieldName="Pokemon" Width="300" SettingsBehavior-AllowSort="false"
                            OnBeforePerformDataSelect="Grid_DataSelect">
                            <Rows>

                                <dx:VerticalGridDataRow VisibleIndex="0" RecordStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <center><img src='./img/<%# Eval("Pokemon")%>' width="50" /></center>
                                    </DataItemTemplate>
                                </dx:VerticalGridDataRow>

                                <dx:VerticalGridDataRow FieldName="PokemonCount" Caption="Count" RecordStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" VisibleIndex="1" />
                                <dx:VerticalGridDataRow FieldName="PokeHP" Visible="false" Caption="Score" RecordStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" VisibleIndex="2" />


                            </Rows>
                            <TotalSummary>
                                <dx:ASPxVerticalGridSummaryItem FieldName="PokemonCount" SummaryType="Count" />
                                <dx:ASPxVerticalGridSummaryItem FieldName="PokeHP" SummaryType="Sum" />
                            </TotalSummary>
                            <Settings RecordWidth="50" HeaderAreaWidth="0" ShowSummaryPanel="true"  />
                        </dx:ASPxVerticalGrid>
                    </DataItemTemplate>
                </dx:GridViewDataColumn>
            </Columns>
        </dx:ASPxGridView>



        <asp:SqlDataSource ID="SqlDataSource_UserRank" runat="server" ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
            SelectCommand="

SELECT  top 20  RANK() OVER (ORDER BY  sum(tblPokemon.PokeHP) DESC) AS Rank 
    ,v_ads_active.sAMAccountName
    ,v_ads_active.displayName
    ,v_ads_active.title
    ,v_ads_active.department
    ,v_ads_active.telephoneNumber
    ,tblInnovation.Name
    ,tblInnovation.StartTime
    ,tblInnovation.EndTime
    ,tblInnovation.IsStart
    ,count(tblInnovation_Result.Pokemon) as PokemonCount
    ,sum(tblPokemon.PokeHP) as PokeHP

  FROM v_ads_active
  left join dbo.tblInnovation_Result
  on tblInnovation_Result.AccountName collate Thai_CI_AS = v_ads_active.sAMAccountName
  
  left join dbo.tblInnovation 
  on tblInnovation_Result.InnovationID = tblInnovation.ID
  
  left join dbo.tblPokemon on  tblPokemon.Pokemon = tblInnovation_Result.Pokemon

  where 
  tblInnovation.ID = 1
  and tblInnovation_Result.ResultDate between tblInnovation.StartTime and tblInnovation.EndTime
  and tblInnovation.IsStart = 1 and tblInnovation_Result.Pokemon is not null
  and isnull(tblInnovation_Result.Result1,0) = 1

  group by 	v_ads_active.sAMAccountName
    ,v_ads_active.displayName
    ,v_ads_active.title
    ,v_ads_active.department
    ,v_ads_active.telephoneNumber
    ,tblInnovation.Name
    ,tblInnovation.StartTime
    ,tblInnovation.EndTime
    ,tblInnovation.IsStart    
    
order by sum(tblPokemon.PokeHP) Desc             
            
            
            "></asp:SqlDataSource>


        <asp:SqlDataSource ID="SqlDataSource_UserPokemon" runat="server" ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
            SelectCommand="
SELECT  
     v_ads_active.sAMAccountName
    ,v_ads_active.displayName
    ,v_ads_active.title
    ,v_ads_active.department
    ,v_ads_active.telephoneNumber
    ,tblInnovation.Name
    ,tblInnovation.StartTime
    ,tblInnovation.EndTime
    ,tblInnovation.IsStart
    ,tblInnovation_Result.Pokemon
    ,count(tblInnovation_Result.Pokemon) as PokemonCount
    ,sum(tblPokemon.PokeHP) as PokeHP

  FROM v_ads_active
  left join dbo.tblInnovation_Result
  on tblInnovation_Result.AccountName collate Thai_CI_AS = v_ads_active.sAMAccountName
  
  left join dbo.tblInnovation 
  on tblInnovation_Result.InnovationID = tblInnovation.ID
  
  left join dbo.tblPokemon on  tblPokemon.Pokemon = tblInnovation_Result.Pokemon

  where 
  tblInnovation.ID = 1
  and tblInnovation_Result.ResultDate between tblInnovation.StartTime and tblInnovation.EndTime
  and tblInnovation.IsStart = 1
  and sAMAccountName=@sAMAccountName
  and tblInnovation_Result.Pokemon is not null
  and isnull(tblInnovation_Result.Result1,0) = 1
  group by 	v_ads_active.sAMAccountName
    ,v_ads_active.displayName
    ,v_ads_active.title
    ,v_ads_active.department
    ,v_ads_active.telephoneNumber
    ,tblInnovation.Name
    ,tblInnovation.StartTime
    ,tblInnovation.EndTime
    ,tblInnovation.IsStart    
    ,tblInnovation_Result.Pokemon
    
order by sum(tblPokemon.PokeHP) Desc             
            ">

            <SelectParameters>
                <asp:SessionParameter Name="sAMAccountName" SessionField="sAMAccountName" />
            </SelectParameters>

        </asp:SqlDataSource>



        <%--         <dx:ASPxCardView ID="cardView" 
             ClientInstanceName="cardView" 
             runat="server" 
             DataSourceID="SqlDataSource_User"  
             KeyFieldName="sAMAccountName"> 


                <SettingsBehavior AllowFocusedCard="true" />
                <SettingsPager Visible="false">
                    <SettingsTableLayout ColumnCount="2" RowsPerPage="5" />
                </SettingsPager> 

                <Columns>
                    <dx:CardViewColumn FieldName="sAMAccountName" />
                    <dx:CardViewColumn FieldName="displayName" />
                    <dx:CardViewColumn FieldName="department" />
                </Columns>

                <CardLayoutProperties ColCount="1">
                    <Items>
                        <dx:CardViewColumnLayoutItem Caption="" ColSpan="1" HorizontalAlign="Center">
                            <Template>
                                <img src='http://lockthbnkedc01/sites/HRDweb/LWT%20Member%20Photos/<%# Eval("sAMAccountName")%>.jpg' />
                            </Template>
                        </dx:CardViewColumnLayoutItem>
                        <dx:CardViewColumnLayoutItem ShowCaption="False" HorizontalAlign="Center">
                            <Template>
                                <center>
                                  Win:  <b><%# Eval("Result1")%></b> <br /> 
                                  Loss:  <b><%# Eval("Result2")%></b> <br /> 
                                  Department:  <%# Eval("department")%> <br /> 
                                  Position:  (<%# Eval("title")%>)

                                </center>
                            </Template>
                        </dx:CardViewColumnLayoutItem>
                    </Items>
                </CardLayoutProperties>
            </dx:ASPxCardView>



        <asp:SqlDataSource ID="SqlDataSource_User" runat="server" ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
    SelectCommand="
        SELECT  
	         v_ads_active.sAMAccountName
	        ,v_ads_active.displayName
	        ,v_ads_active.title
	        ,v_ads_active.department
	        ,v_ads_active.telephoneNumber
	        ,tblInnovation.Name
	        ,tblInnovation.StartTime
	        ,tblInnovation.EndTime
	        ,tblInnovation.IsStart
            ,sum(ISNULL(Result1,0)) as Result1
            ,sum(ISNULL(Result2,0)) as Result2

          FROM v_ads_active
          inner join dbo.tblInnovation_Result
          on replace(tblInnovation_Result.AccountName,'asia\','') collate Thai_CI_AS = v_ads_active.sAMAccountName
          inner join dbo.tblInnovation 
          on tblInnovation_Result.InnovationID = tblInnovation.ID
  
          where 
          tblInnovation.ID = 1
          and tblInnovation_Result.ResultDate between tblInnovation.StartTime and tblInnovation.EndTime
          and tblInnovation.IsStart = 1
  
          group by 	v_ads_active.sAMAccountName
	        ,v_ads_active.displayName
	        ,v_ads_active.title
	        ,v_ads_active.department
	        ,v_ads_active.telephoneNumber
	        ,tblInnovation.Name
	        ,tblInnovation.StartTime
	        ,tblInnovation.EndTime
	        ,tblInnovation.IsStart    
            
            
       order by Result1 Desc              
            
            
            "></asp:SqlDataSource>--%>
    </form>
</body>
</html>
