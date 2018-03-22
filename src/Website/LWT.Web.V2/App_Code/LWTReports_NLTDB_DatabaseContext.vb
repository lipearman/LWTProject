Imports Microsoft.VisualBasic
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration

Public Class LWTReports_NLTDB_DatabaseContext
    Inherits ContextBase
    Shared Sub New()
        Database.SetInitializer(Of LWTReports_NLTDB_DatabaseContext)(Nothing)
    End Sub

    Public Sub New()
        MyBase.New("LWTReportsNLTDBConnectionString")
    End Sub


    Public Property V_TbClaimDataV2s() As DbSet(Of V_TbClaimDataV2Ext)


    Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
        modelBuilder.Configurations.Add(New V_TbClaimDataV2Ext_Map("V_TbClaimDataV2"))
    End Sub
End Class


'======================V_MotorPremiumExt===============================
Partial Public Class V_TbClaimDataV2Ext
    <Global.System.Data.Linq.Mapping.ColumnAttribute(DbType:="Int NOT NULL", IsPrimaryKey:=True)> _
    Public Property ID As Integer
    Public Property FullCustomerName As String
    Public Property ChassisNo As String
    Public Property PolicyNo As String
    Public Property Insurance_ClaimNo As String
    Public Property GarageProvinceTH As String
    Public Property GarageProvinceEN As String
    Public Property AccidentDate As System.Nullable(Of Date)
    Public Property Accident_Month As System.Nullable(Of Date)
    Public Property ClaimGroup As System.Nullable(Of Double)
    Public Property IndemnityGroup As System.Nullable(Of Double)
    Public Property No_of_Claim As System.Nullable(Of Double)
    Public Property ClaimCost As System.Nullable(Of Double)
    Public Property IndemnityType As String
    Public Property IndemnityType_E As String
    Public Property GarageName As String
    Public Property GarageType1 As String
    Public Property GarageType2 As String
    Public Property InsurerName As String
    Public Property InsurerCode As String
    Public Property CustType As String
    Public Property App_StartingDate As System.Nullable(Of Date)
    Public Property Starting_Month As System.Nullable(Of Date)
    Public Property Starting_Year As String
    Public Property App_EndDate As System.Nullable(Of Date)
    Public Property App_ShowroomName As String
    Public Property App_Beneficiary As String
    Public Property App_Beneficiary_Group As String
    Public Property App_CarGroup As String
    Public Property App_CarGroup_F As String
    Public Property App_CarRegistryYear As System.Nullable(Of Double)
    Public Property Claim_Paid_Month As System.Nullable(Of Double)
    Public Property Claim_Paid_Year As System.Nullable(Of Double)
    Public Property ClaimPaid As System.Nullable(Of Date)
    Public Property Accdent_Province As String
    Public Property CarAge As System.Nullable(Of Integer)
    Public Property GarageRegionTH As String
End Class
Public Class V_TbClaimDataV2Ext_Map
    Inherits EntityTypeConfiguration(Of V_TbClaimDataV2Ext)
    Public Sub New(_table As String)
        ' Primary Key
        Me.HasKey(Function(t) t.ID)

        Me.ToTable(_table)
        Me.Property(Function(t) t.ID).HasColumnName("ID")
        Me.Property(Function(t) t.FullCustomerName).HasColumnName("FullCustomerName")
        Me.Property(Function(t) t.ChassisNo).HasColumnName("ChassisNo")
        Me.Property(Function(t) t.PolicyNo).HasColumnName("PolicyNo")
        Me.Property(Function(t) t.Insurance_ClaimNo).HasColumnName("Insurance_ClaimNo")
        Me.Property(Function(t) t.GarageProvinceTH).HasColumnName("GarageProvinceTH")
        Me.Property(Function(t) t.GarageProvinceEN).HasColumnName("GarageProvinceEN")
        Me.Property(Function(t) t.AccidentDate).HasColumnName("AccidentDate")
        Me.Property(Function(t) t.Accident_Month).HasColumnName("Accident_Month")
        Me.Property(Function(t) t.ClaimGroup).HasColumnName("ClaimGroup")
        Me.Property(Function(t) t.IndemnityGroup).HasColumnName("IndemnityGroup")
        Me.Property(Function(t) t.No_of_Claim).HasColumnName("No_of_Claim")
        Me.Property(Function(t) t.ClaimCost).HasColumnName("ClaimCost")
        Me.Property(Function(t) t.IndemnityType).HasColumnName("IndemnityType")
        Me.Property(Function(t) t.IndemnityType_E).HasColumnName("IndemnityType_E")
        Me.Property(Function(t) t.GarageName).HasColumnName("GarageName")
        Me.Property(Function(t) t.GarageType1).HasColumnName("GarageType1")
        Me.Property(Function(t) t.GarageType2).HasColumnName("GarageType2")
        Me.Property(Function(t) t.InsurerName).HasColumnName("InsurerName")
        Me.Property(Function(t) t.InsurerCode).HasColumnName("InsurerCode")
        Me.Property(Function(t) t.CustType).HasColumnName("CustType")
        Me.Property(Function(t) t.App_StartingDate).HasColumnName("App_StartingDate")
        Me.Property(Function(t) t.Starting_Month).HasColumnName("Starting_Month")
        Me.Property(Function(t) t.Starting_Year).HasColumnName("Starting_Year")
        Me.Property(Function(t) t.App_EndDate).HasColumnName("App_EndDate")
        Me.Property(Function(t) t.App_ShowroomName).HasColumnName("App_ShowroomName")
        Me.Property(Function(t) t.App_Beneficiary).HasColumnName("App_Beneficiary")
        Me.Property(Function(t) t.App_Beneficiary_Group).HasColumnName("App_Beneficiary_Group")
        Me.Property(Function(t) t.App_CarGroup).HasColumnName("App_CarGroup")
        Me.Property(Function(t) t.App_CarGroup_F).HasColumnName("App_CarGroup_F")
        Me.Property(Function(t) t.App_CarRegistryYear).HasColumnName("App_CarRegistryYear")
        Me.Property(Function(t) t.Claim_Paid_Month).HasColumnName("Claim_Paid_Month")
        Me.Property(Function(t) t.Claim_Paid_Year).HasColumnName("Claim_Paid_Year")
        Me.Property(Function(t) t.ClaimPaid).HasColumnName("ClaimPaid")
        Me.Property(Function(t) t.Accdent_Province).HasColumnName("Accdent_Province")
        Me.Property(Function(t) t.CarAge).HasColumnName("CarAge")
        Me.Property(Function(t) t.GarageRegionTH).HasColumnName("GarageRegionTH")



    End Sub
End Class
