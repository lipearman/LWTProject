Imports Microsoft.VisualBasic
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration


Public Class LargeDatabaseContext
    Inherits ContextBase
    Shared Sub New()
        Database.SetInitializer(Of LargeDatabaseContext)(Nothing)
    End Sub

    Public Sub New()
        MyBase.New("CPSConnectionString")
    End Sub
    Public Property V_MotorPremiumExts() As DbSet(Of V_MotorPremiumExt)
 

    Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
        modelBuilder.Configurations.Add(New V_MotorPremiumExt_Map("V_MotorPremiumExt"))
    
    End Sub

End Class

'======================V_MotorPremiumExt===============================
Partial Public Class V_MotorPremiumExt
    <Global.System.Data.Linq.Mapping.ColumnAttribute(DbType:="Int NOT NULL", IsPrimaryKey:=True)> _
    Public Property ID As Integer
    Public Property SourceFrom As String
    Public Property BrandName As String
    Public Property ModelName As String
    Public Property ModelYear As System.Nullable(Of Integer)
    Public Property Capacity As System.Nullable(Of Integer)
    Public Property InsurerName As String
    Public Property InsurerLogo As String
    Public Property InsurerSmallLogo As String
    Public Property PlanName As String
    Public Property Premium As System.Nullable(Of Decimal)
    Public Property OriginalPremium As System.Nullable(Of Decimal)
    Public Property DiscountValue As System.Nullable(Of Decimal)
    Public Property PlanCategory As String
    Public Property PromotionTitle As String
    Public Property UrlTitle As String
    Public Property [Class] As String
    Public Property SupplierName As String
    Public Property IsBroker As String
    Public Property CheckoutNotes As String
    Public Property OD As System.Nullable(Of Integer)
    Public Property ODDD As System.Nullable(Of Integer)
    Public Property Flood As System.Nullable(Of Integer)
    Public Property FIX As String
    Public Property TP As System.Nullable(Of Integer)
End Class
Public Class V_MotorPremiumExt_Map
    Inherits EntityTypeConfiguration(Of V_MotorPremiumExt)
    Public Sub New(_table As String)
        ' Primary Key
        Me.HasKey(Function(t) t.ID)

        ' Properties
        'Me.Property(Function(t) t.FirstName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.LastName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.Phone).IsRequired().HasMaxLength(20)


        ' Table & Column Mappings
        'Me.ToTable("V_Nissan")
        Me.ToTable(_table)
        Me.Property(Function(t) t.ID).HasColumnName("ID")
        Me.Property(Function(t) t.SourceFrom).HasColumnName("SourceFrom")
        Me.Property(Function(t) t.BrandName).HasColumnName("BrandName")
        Me.Property(Function(t) t.ModelName).HasColumnName("ModelName")
        Me.Property(Function(t) t.ModelYear).HasColumnName("ModelYear")
        Me.Property(Function(t) t.Capacity).HasColumnName("Capacity")
        Me.Property(Function(t) t.InsurerName).HasColumnName("InsurerName")
        Me.Property(Function(t) t.InsurerLogo).HasColumnName("InsurerLogo")
        Me.Property(Function(t) t.InsurerSmallLogo).HasColumnName("InsurerSmallLogo")
        Me.Property(Function(t) t.PlanName).HasColumnName("PlanName")
        Me.Property(Function(t) t.Premium).HasColumnName("Premium")
        Me.Property(Function(t) t.OriginalPremium).HasColumnName("OriginalPremium")
        Me.Property(Function(t) t.DiscountValue).HasColumnName("DiscountValue")
        Me.Property(Function(t) t.PlanCategory).HasColumnName("PlanCategory")
        Me.Property(Function(t) t.PromotionTitle).HasColumnName("PromotionTitle")
        Me.Property(Function(t) t.UrlTitle).HasColumnName("UrlTitle")
        Me.Property(Function(t) t.Class).HasColumnName("Class")
        Me.Property(Function(t) t.SupplierName).HasColumnName("SupplierName")
        Me.Property(Function(t) t.IsBroker).HasColumnName("IsBroker")
        Me.Property(Function(t) t.CheckoutNotes).HasColumnName("CheckoutNotes")
        Me.Property(Function(t) t.OD).HasColumnName("OD")
        Me.Property(Function(t) t.ODDD).HasColumnName("ODDD")
        Me.Property(Function(t) t.Flood).HasColumnName("Flood")
        Me.Property(Function(t) t.FIX).HasColumnName("FIX")
        Me.Property(Function(t) t.TP).HasColumnName("TP")


    End Sub
End Class
 

 