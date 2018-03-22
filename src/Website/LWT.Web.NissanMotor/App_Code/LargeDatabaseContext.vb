Imports Microsoft.VisualBasic
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration


Public Class LargeDatabaseContext
    Inherits ContextBase
    Shared Sub New()
        Database.SetInitializer(Of LargeDatabaseContext)(Nothing)
    End Sub

    Public Sub New()
        MyBase.New("LWTReportsConnectionString")
    End Sub
    Public Property V_NissanMotors() As DbSet(Of V_NissanMotor)
    Public Property V_NissanMotorNMTs() As DbSet(Of V_NissanMotorNMT)
    Public Property V_NissanMotorNLTHs() As DbSet(Of V_NissanMotorNLTH)

    Public Property V_NLTH_All_Campaign_Dailys() As DbSet(Of V_NLTH_All_Campaign_Daily)
    Public Property V_NLTH_All_Campaign_Monthlys() As DbSet(Of V_NLTH_All_Campaign_Monthly)
    Public Property V_NLTH_All_Status_Dailys() As DbSet(Of V_NLTH_All_Status_Daily)
    Public Property V_NLTH_Free_NPP_Campaign_Monthlys() As DbSet(Of V_NLTH_Free_NPP_Campaign_Monthly)
    Public Property V_NLTH_All_Campaign_Daily_Excluded_Models() As DbSet(Of V_NLTH_All_Campaign_Daily_Excluded_Model)
    Public Property V_NLTH_GetTemp_By_NotificationDates() As DbSet(Of V_NLTH_GetTemp_By_NotificationDate)

    Public Property V_NMT_All_NPP_Campaign_Dailys() As DbSet(Of V_NMT_All_NPP_Campaign_Daily)
    Public Property V_NMT_All_NPP_Campaign_Monthlys() As DbSet(Of V_NMT_All_NPP_Campaign_Monthly)
    Public Property V_NMT_Free_NPP_Campaign_Dailys() As DbSet(Of V_NMT_Free_NPP_Campaign_Daily)
    Public Property V_NMT_Free_NPP_Campaign_Monthlys() As DbSet(Of V_NMT_Free_NPP_Campaign_Monthly)
    Public Property V_NMT_nonFree_NPP_Campaign_Dailys() As DbSet(Of V_NMT_nonFree_NPP_Campaign_Daily)
    Public Property V_NMT_Policy_Cancellation_Dailys() As DbSet(Of V_NMT_Policy_Cancellation_Daily)

    Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
        modelBuilder.Configurations.Add(New V_NissanMotor_Map("V_Nissan"))
        modelBuilder.Configurations.Add(New V_NissanMotorNMT_Map("V_NissanMotorNMT"))
        modelBuilder.Configurations.Add(New V_NissanMotorNLTH_Map("V_NissanMotorNLTH"))

        modelBuilder.Configurations.Add(New V_NLTH_All_Campaign_Daily_Map("V_NLTH_All_Campaign_Daily"))
        modelBuilder.Configurations.Add(New V_NLTH_All_Status_Daily_Map("V_NLTH_All_Status_Daily"))
        modelBuilder.Configurations.Add(New V_NLTH_All_Campaign_Monthly_Map("V_NLTH_All_Campaign_Monthly"))
        modelBuilder.Configurations.Add(New V_NLTH_Free_NPP_Campaign_Monthly_Map("V_NLTH_Free_NPP_Campaign_Monthly"))

        modelBuilder.Configurations.Add(New V_NLTH_All_Campaign_Daily_Excluded_Model_Map("V_NLTH_All_Campaign_Daily_Excluded_Model"))
        modelBuilder.Configurations.Add(New V_NLTH_GetTemp_By_NotificationDate_Map("V_NLTH_GetTemp_By_NotificationDate"))



        modelBuilder.Configurations.Add(New V_NMT_All_NPP_Campaign_Daily_Map("V_NMT_All_NPP_Campaign_Daily"))
        modelBuilder.Configurations.Add(New V_NMT_Free_NPP_Campaign_Daily_Map("V_NMT_Free_NPP_Campaign_Daily"))
        modelBuilder.Configurations.Add(New V_NMT_nonFree_NPP_Campaign_Daily_Map("V_NMT_nonFree_NPP_Campaign_Daily"))
        modelBuilder.Configurations.Add(New V_NMT_Policy_Cancellation_Daily_Map("V_NMT_Policy_Cancellation_Daily"))

        modelBuilder.Configurations.Add(New V_NMT_All_NPP_Campaign_Monthly_Map("V_NMT_All_NPP_Campaign_Monthly"))
        modelBuilder.Configurations.Add(New V_NMT_Free_NPP_Campaign_Monthly_Map("V_NMT_Free_NPP_Campaign_Monthly"))


    End Sub

End Class

'======================V_NissanMotorMap===============================
Partial Public Class V_NissanMotor
    <Global.System.Data.Linq.Mapping.ColumnAttribute(DbType:="Int NOT NULL", IsPrimaryKey:=True)> _
    Public Property AppID As Integer
    Public Property TempID As String
    Public Property CusName As String
    Public Property CusSurname As String
    Public Property CusIDCard As String
    Public Property CusAddress1 As String
    Public Property CusAddress2 As String
    Public Property CusMobile As String
    Public Property CusHomePhone As String
    Public Property CusOfficePhone As System.Nullable(Of Integer)
    Public Property CarEngineNo As String
    Public Property CarChassisNo As String
    Public Property CarPlantNo As String
    Public Property DateEffective As System.Nullable(Of Date)
    Public Property DateExpired As System.Nullable(Of Date)
    Public Property DateNotification As System.Nullable(Of Date)
    Public Property AppStatus As String
    Public Property AppIsActive As System.Nullable(Of Integer)
    Public Property ClientCode As String
    Public Property CmiBuying As System.Nullable(Of Integer)
    Public Property AppBeneficiary As String
    Public Property LeasingID As System.Nullable(Of Integer)
    Public Property ClosingDate As System.Nullable(Of Date)
    Public Property ClosingMonth As System.Nullable(Of Date)
    Public Property ClosingDay As System.Nullable(Of Date)
    Public Property AppStatusRecived As System.Nullable(Of Boolean)
    Public Property ProjectID As System.Nullable(Of Integer)
    Public Property NotifyName As String
    Public Property CusTitle As String
    Public Property CarGroupID As System.Nullable(Of Integer)
    Public Property CarGroupName As String
    Public Property CarBrandName As String
    Public Property CarModelName As String
    Public Property CarTypeName As String
    Public Property CarTypeNameFull As String
    Public Property CarModelCode As String
    Public Property CarGroupNameM2 As String
    Public Property DealerNameEn As String
    Public Property NLTDealerCode As String
    Public Property NLTArea As String
    Public Property InsurerNameTh As String
    Public Property InsurerCodeSibis As String
    Public Property InsurerNameEn As String
    Public Property InsurerCode As String
    Public Property LeasID As System.Nullable(Of Integer)
    Public Property LeasName As String
    Public Property LeasNameAbbr As String
    Public Property LeasNameShort As String
    Public Property ModelNameM2 As String
    'Public Property NLTBookingDate As System.Nullable(Of Date)
    Public Property SchemeCode As String
    Public Property VmiNetPremium As System.Nullable(Of Decimal)
    Public Property VmiStamp As System.Nullable(Of Decimal)
    Public Property VmiVat As System.Nullable(Of Decimal)
    Public Property VmiTotalPremium As System.Nullable(Of Decimal)
    Public Property CmiNetPremium As System.Nullable(Of Decimal)
    Public Property CmiStamp As System.Nullable(Of Decimal)
    Public Property CmiVat As System.Nullable(Of Decimal)
    Public Property CmiTotalPremium As System.Nullable(Of Decimal)
    Public Property ClassOfRiskVMI As String
    Public Property ClassOfRiskCMI As String
    Public Property ShowroomID As System.Nullable(Of Integer)
    Public Property ShowroomName As String
    Public Property DealerID As System.Nullable(Of Integer)
    Public Property ShorwoomNameEn As String
    Public Property CarTypeNameM2 As String
    Public Property TypeInsID As System.Nullable(Of Integer)
    Public Property TypeInsName As String
    Public Property TypeInsStatus As System.Nullable(Of Integer)
    Public Property TypeInsReportToNLTH As System.Nullable(Of Boolean)
    Public Property TypeInsIsFree As System.Nullable(Of Boolean)
    Public Property UserAddedTel As String
    Public Property UserAddedType As String
End Class
Public Class V_NissanMotor_Map
    Inherits EntityTypeConfiguration(Of V_NissanMotor)
    Public Sub New(_table As String)
        ' Primary Key
        Me.HasKey(Function(t) t.AppID)

        ' Properties
        'Me.Property(Function(t) t.FirstName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.LastName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.Phone).IsRequired().HasMaxLength(20)


        ' Table & Column Mappings
        'Me.ToTable("V_Nissan")
        Me.ToTable(_table)
        Me.Property(Function(t) t.AppID).HasColumnName("AppID")
        Me.Property(Function(t) t.TempID).HasColumnName("TempID")
        Me.Property(Function(t) t.CusName).HasColumnName("CusName")
        Me.Property(Function(t) t.CusSurname).HasColumnName("CusSurname")
        Me.Property(Function(t) t.CusIDCard).HasColumnName("CusIDCard")
        Me.Property(Function(t) t.CusAddress1).HasColumnName("CusAddress1")
        Me.Property(Function(t) t.CusAddress2).HasColumnName("CusAddress2")
        Me.Property(Function(t) t.CusMobile).HasColumnName("CusMobile")
        Me.Property(Function(t) t.CusHomePhone).HasColumnName("CusHomePhone")
        Me.Property(Function(t) t.CusOfficePhone).HasColumnName("CusOfficePhone")
        Me.Property(Function(t) t.CarEngineNo).HasColumnName("CarEngineNo")
        Me.Property(Function(t) t.CarChassisNo).HasColumnName("CarChassisNo")
        Me.Property(Function(t) t.CarPlantNo).HasColumnName("CarPlantNo")
        Me.Property(Function(t) t.DateEffective).HasColumnName("DateEffective")
        Me.Property(Function(t) t.DateExpired).HasColumnName("DateExpired")
        Me.Property(Function(t) t.DateNotification).HasColumnName("DateNotification")
        Me.Property(Function(t) t.AppStatus).HasColumnName("AppStatus")
        Me.Property(Function(t) t.AppIsActive).HasColumnName("AppIsActive")
        Me.Property(Function(t) t.ClientCode).HasColumnName("ClientCode")
        Me.Property(Function(t) t.CmiBuying).HasColumnName("CmiBuying")
        Me.Property(Function(t) t.AppBeneficiary).HasColumnName("AppBeneficiary")
        Me.Property(Function(t) t.LeasingID).HasColumnName("LeasingID")
        Me.Property(Function(t) t.ClosingDate).HasColumnName("ClosingDate")
        Me.Property(Function(t) t.ClosingMonth).HasColumnName("ClosingMonth")
        Me.Property(Function(t) t.ClosingDay).HasColumnName("ClosingDay")
        Me.Property(Function(t) t.AppStatusRecived).HasColumnName("AppStatusRecived")
        Me.Property(Function(t) t.ProjectID).HasColumnName("ProjectID")
        Me.Property(Function(t) t.NotifyName).HasColumnName("NotifyName")
        Me.Property(Function(t) t.CusTitle).HasColumnName("CusTitle")
        Me.Property(Function(t) t.CarGroupID).HasColumnName("CarGroupID")
        Me.Property(Function(t) t.CarGroupName).HasColumnName("CarGroupName")
        Me.Property(Function(t) t.CarBrandName).HasColumnName("CarBrandName")
        Me.Property(Function(t) t.CarModelName).HasColumnName("CarModelName")
        Me.Property(Function(t) t.CarTypeName).HasColumnName("CarTypeName")
        Me.Property(Function(t) t.CarTypeNameFull).HasColumnName("CarTypeNameFull")
        Me.Property(Function(t) t.CarModelCode).HasColumnName("CarModelCode")
        Me.Property(Function(t) t.CarGroupNameM2).HasColumnName("CarGroupNameM2")
        Me.Property(Function(t) t.DealerNameEn).HasColumnName("DealerNameEn")
        Me.Property(Function(t) t.NLTDealerCode).HasColumnName("NLTDealerCode")
        Me.Property(Function(t) t.NLTArea).HasColumnName("NLTArea")
        Me.Property(Function(t) t.InsurerNameTh).HasColumnName("InsurerNameTh")
        Me.Property(Function(t) t.InsurerCodeSibis).HasColumnName("InsurerCodeSibis")
        Me.Property(Function(t) t.InsurerNameEn).HasColumnName("InsurerNameEn")
        Me.Property(Function(t) t.InsurerCode).HasColumnName("InsurerCode")
        Me.Property(Function(t) t.LeasID).HasColumnName("LeasID")
        Me.Property(Function(t) t.LeasName).HasColumnName("LeasName")
        Me.Property(Function(t) t.LeasNameAbbr).HasColumnName("LeasNameAbbr")
        Me.Property(Function(t) t.LeasNameShort).HasColumnName("LeasNameShort")
        Me.Property(Function(t) t.ModelNameM2).HasColumnName("ModelNameM2")
        'Me.Property(Function(t) t.NLTBookingDate).HasColumnName("NLTBookingDate")
        Me.Property(Function(t) t.SchemeCode).HasColumnName("SchemeCode")
        Me.Property(Function(t) t.VmiNetPremium).HasColumnName("VmiNetPremium")
        Me.Property(Function(t) t.VmiStamp).HasColumnName("VmiStamp")
        Me.Property(Function(t) t.VmiVat).HasColumnName("VmiVat")
        Me.Property(Function(t) t.VmiTotalPremium).HasColumnName("VmiTotalPremium")
        Me.Property(Function(t) t.CmiNetPremium).HasColumnName("CmiNetPremium")
        Me.Property(Function(t) t.CmiStamp).HasColumnName("CmiStamp")
        Me.Property(Function(t) t.CmiVat).HasColumnName("CmiVat")
        Me.Property(Function(t) t.CmiTotalPremium).HasColumnName("CmiTotalPremium")
        Me.Property(Function(t) t.ClassOfRiskVMI).HasColumnName("ClassOfRiskVMI")
        Me.Property(Function(t) t.ClassOfRiskCMI).HasColumnName("ClassOfRiskCMI")
        Me.Property(Function(t) t.ShowroomID).HasColumnName("ShowroomID")
        Me.Property(Function(t) t.ShowroomName).HasColumnName("ShowroomName")
        Me.Property(Function(t) t.DealerID).HasColumnName("DealerID")
        Me.Property(Function(t) t.ShorwoomNameEn).HasColumnName("ShorwoomNameEn")
        Me.Property(Function(t) t.CarTypeNameM2).HasColumnName("CarTypeNameM2")
        Me.Property(Function(t) t.TypeInsID).HasColumnName("TypeInsID")
        Me.Property(Function(t) t.TypeInsName).HasColumnName("TypeInsName")
        Me.Property(Function(t) t.TypeInsStatus).HasColumnName("TypeInsStatus")
        Me.Property(Function(t) t.TypeInsReportToNLTH).HasColumnName("TypeInsReportToNLTH")
        Me.Property(Function(t) t.TypeInsIsFree).HasColumnName("TypeInsIsFree")
        Me.Property(Function(t) t.UserAddedTel).HasColumnName("UserAddedTel")
        Me.Property(Function(t) t.UserAddedType).HasColumnName("UserAddedType")

    End Sub
End Class

'======================V_NissanMotorNMTMap===============================
Partial Public Class V_NissanMotorNMT
    <Global.System.Data.Linq.Mapping.ColumnAttribute(DbType:="Int NOT NULL", IsPrimaryKey:=True)> _
    Public Property AppID As Integer
    Public Property TempID As String
    Public Property CusName As String
    Public Property CusSurname As String
    Public Property CusIDCard As String
    Public Property CusAddress1 As String
    Public Property CusAddress2 As String
    Public Property CusMobile As String
    Public Property CusHomePhone As String
    Public Property CusOfficePhone As System.Nullable(Of Integer)
    Public Property CarEngineNo As String
    Public Property CarChassisNo As String
    Public Property CarPlantNo As String
    Public Property DateEffective As System.Nullable(Of Date)
    Public Property DateExpired As System.Nullable(Of Date)
    Public Property DateNotification As System.Nullable(Of Date)
    Public Property AppStatus As String
    Public Property AppIsActive As System.Nullable(Of Integer)
    Public Property ClientCode As String
    Public Property CmiBuying As System.Nullable(Of Integer)
    Public Property AppBeneficiary As String
    Public Property LeasingID As System.Nullable(Of Integer)
    Public Property ClosingDate As System.Nullable(Of Date)
    Public Property ClosingMonth As System.Nullable(Of Date)
    Public Property ClosingDay As System.Nullable(Of Date)
    Public Property AppStatusRecived As System.Nullable(Of Boolean)
    Public Property ProjectID As System.Nullable(Of Integer)
    Public Property NotifyName As String
    Public Property CusTitle As String
    Public Property CarGroupID As System.Nullable(Of Integer)
    Public Property CarGroupName As String
    Public Property CarGroupName2 As String
    Public Property CarBrandName As String
    Public Property CarModelName As String
    Public Property CarTypeName As String
    Public Property CarTypeNameFull As String
    Public Property CarModelCode As String
    Public Property CarGroupNameM2 As String
    Public Property DealerNameEn As String
    Public Property NLTDealerCode As String
    Public Property NLTArea As String
    Public Property InsurerNameTh As String
    Public Property InsurerCodeSibis As String
    Public Property InsurerNameEn As String
    Public Property InsurerCode As String
    Public Property LeasID As System.Nullable(Of Integer)
    Public Property LeasName As String
    Public Property LeasNameAbbr As String
    Public Property LeasNameShort As String
    Public Property ModelNameM2 As String
    'Public Property NLTBookingDate As System.Nullable(Of Date)
    Public Property SchemeCode As String
    Public Property VmiNetPremium As System.Nullable(Of Decimal)
    Public Property VmiStamp As System.Nullable(Of Decimal)
    Public Property VmiVat As System.Nullable(Of Decimal)
    Public Property VmiTotalPremium As System.Nullable(Of Decimal)
    Public Property CmiNetPremium As System.Nullable(Of Decimal)
    Public Property CmiStamp As System.Nullable(Of Decimal)
    Public Property CmiVat As System.Nullable(Of Decimal)
    Public Property CmiTotalPremium As System.Nullable(Of Decimal)
    Public Property ClassOfRiskVMI As String
    Public Property ClassOfRiskCMI As String
    Public Property ShowroomID As System.Nullable(Of Integer)
    Public Property ShowroomName As String
    Public Property DealerID As System.Nullable(Of Integer)
    Public Property ShorwoomNameEn As String
    Public Property CarTypeNameM2 As String
    Public Property TypeInsID As System.Nullable(Of Integer)
    Public Property TypeInsName As String
    Public Property TypeInsStatus As System.Nullable(Of Integer)
    Public Property TypeInsReportToNLTH As System.Nullable(Of Boolean)
    Public Property TypeInsIsFree As System.Nullable(Of Boolean)
    Public Property UserAddedTel As String
    Public Property UserAddedType As String
End Class
Public Class V_NissanMotorNMT_Map
    Inherits EntityTypeConfiguration(Of V_NissanMotorNMT)
    Public Sub New(_table As String)
        ' Primary Key
        Me.HasKey(Function(t) t.AppID)

        ' Properties
        'Me.Property(Function(t) t.FirstName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.LastName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.Phone).IsRequired().HasMaxLength(20)


        ' Table & Column Mappings
        'Me.ToTable("V_NissanMotorNMT")
        Me.ToTable(_table)
        Me.Property(Function(t) t.AppID).HasColumnName("AppID")
        Me.Property(Function(t) t.TempID).HasColumnName("TempID")
        Me.Property(Function(t) t.CusName).HasColumnName("CusName")
        Me.Property(Function(t) t.CusSurname).HasColumnName("CusSurname")
        Me.Property(Function(t) t.CusIDCard).HasColumnName("CusIDCard")
        Me.Property(Function(t) t.CusAddress1).HasColumnName("CusAddress1")
        Me.Property(Function(t) t.CusAddress2).HasColumnName("CusAddress2")
        Me.Property(Function(t) t.CusMobile).HasColumnName("CusMobile")
        Me.Property(Function(t) t.CusHomePhone).HasColumnName("CusHomePhone")
        Me.Property(Function(t) t.CusOfficePhone).HasColumnName("CusOfficePhone")
        Me.Property(Function(t) t.CarEngineNo).HasColumnName("CarEngineNo")
        Me.Property(Function(t) t.CarChassisNo).HasColumnName("CarChassisNo")
        Me.Property(Function(t) t.CarPlantNo).HasColumnName("CarPlantNo")
        Me.Property(Function(t) t.DateEffective).HasColumnName("DateEffective")
        Me.Property(Function(t) t.DateExpired).HasColumnName("DateExpired")
        Me.Property(Function(t) t.DateNotification).HasColumnName("DateNotification")
        Me.Property(Function(t) t.AppStatus).HasColumnName("AppStatus")
        Me.Property(Function(t) t.AppIsActive).HasColumnName("AppIsActive")
        Me.Property(Function(t) t.ClientCode).HasColumnName("ClientCode")
        Me.Property(Function(t) t.CmiBuying).HasColumnName("CmiBuying")
        Me.Property(Function(t) t.AppBeneficiary).HasColumnName("AppBeneficiary")
        Me.Property(Function(t) t.LeasingID).HasColumnName("LeasingID")
        Me.Property(Function(t) t.ClosingDate).HasColumnName("ClosingDate")
        Me.Property(Function(t) t.ClosingMonth).HasColumnName("ClosingMonth")
        Me.Property(Function(t) t.ClosingDay).HasColumnName("ClosingDay")
        Me.Property(Function(t) t.AppStatusRecived).HasColumnName("AppStatusRecived")
        Me.Property(Function(t) t.ProjectID).HasColumnName("ProjectID")
        Me.Property(Function(t) t.NotifyName).HasColumnName("NotifyName")
        Me.Property(Function(t) t.CusTitle).HasColumnName("CusTitle")
        Me.Property(Function(t) t.CarGroupID).HasColumnName("CarGroupID")
        Me.Property(Function(t) t.CarGroupName).HasColumnName("CarGroupName")
        Me.Property(Function(t) t.CarGroupName2).HasColumnName("CarGroupName2")
        Me.Property(Function(t) t.CarBrandName).HasColumnName("CarBrandName")
        Me.Property(Function(t) t.CarModelName).HasColumnName("CarModelName")
        Me.Property(Function(t) t.CarTypeName).HasColumnName("CarTypeName")
        Me.Property(Function(t) t.CarTypeNameFull).HasColumnName("CarTypeNameFull")
        Me.Property(Function(t) t.CarModelCode).HasColumnName("CarModelCode")
        Me.Property(Function(t) t.CarGroupNameM2).HasColumnName("CarGroupNameM2")
        Me.Property(Function(t) t.DealerNameEn).HasColumnName("DealerNameEn")
        Me.Property(Function(t) t.NLTDealerCode).HasColumnName("NLTDealerCode")
        Me.Property(Function(t) t.NLTArea).HasColumnName("NLTArea")
        Me.Property(Function(t) t.InsurerNameTh).HasColumnName("InsurerNameTh")
        Me.Property(Function(t) t.InsurerCodeSibis).HasColumnName("InsurerCodeSibis")
        Me.Property(Function(t) t.InsurerNameEn).HasColumnName("InsurerNameEn")
        Me.Property(Function(t) t.InsurerCode).HasColumnName("InsurerCode")
        Me.Property(Function(t) t.LeasID).HasColumnName("LeasID")
        Me.Property(Function(t) t.LeasName).HasColumnName("LeasName")
        Me.Property(Function(t) t.LeasNameAbbr).HasColumnName("LeasNameAbbr")
        Me.Property(Function(t) t.LeasNameShort).HasColumnName("LeasNameShort")
        Me.Property(Function(t) t.ModelNameM2).HasColumnName("ModelNameM2")
        'Me.Property(Function(t) t.NLTBookingDate).HasColumnName("NLTBookingDate")
        Me.Property(Function(t) t.SchemeCode).HasColumnName("SchemeCode")
        Me.Property(Function(t) t.VmiNetPremium).HasColumnName("VmiNetPremium")
        Me.Property(Function(t) t.VmiStamp).HasColumnName("VmiStamp")
        Me.Property(Function(t) t.VmiVat).HasColumnName("VmiVat")
        Me.Property(Function(t) t.VmiTotalPremium).HasColumnName("VmiTotalPremium")
        Me.Property(Function(t) t.CmiNetPremium).HasColumnName("CmiNetPremium")
        Me.Property(Function(t) t.CmiStamp).HasColumnName("CmiStamp")
        Me.Property(Function(t) t.CmiVat).HasColumnName("CmiVat")
        Me.Property(Function(t) t.CmiTotalPremium).HasColumnName("CmiTotalPremium")
        Me.Property(Function(t) t.ClassOfRiskVMI).HasColumnName("ClassOfRiskVMI")
        Me.Property(Function(t) t.ClassOfRiskCMI).HasColumnName("ClassOfRiskCMI")
        Me.Property(Function(t) t.ShowroomID).HasColumnName("ShowroomID")
        Me.Property(Function(t) t.ShowroomName).HasColumnName("ShowroomName")
        Me.Property(Function(t) t.DealerID).HasColumnName("DealerID")
        Me.Property(Function(t) t.ShorwoomNameEn).HasColumnName("ShorwoomNameEn")
        Me.Property(Function(t) t.CarTypeNameM2).HasColumnName("CarTypeNameM2")
        Me.Property(Function(t) t.TypeInsID).HasColumnName("TypeInsID")
        Me.Property(Function(t) t.TypeInsName).HasColumnName("TypeInsName")
        Me.Property(Function(t) t.TypeInsStatus).HasColumnName("TypeInsStatus")
        Me.Property(Function(t) t.TypeInsReportToNLTH).HasColumnName("TypeInsReportToNLTH")
        Me.Property(Function(t) t.TypeInsIsFree).HasColumnName("TypeInsIsFree")
        Me.Property(Function(t) t.UserAddedTel).HasColumnName("UserAddedTel")
        Me.Property(Function(t) t.UserAddedType).HasColumnName("UserAddedType")

    End Sub
End Class

'======================V_NissanMotorNLTHMap===============================
Partial Public Class V_NissanMotorNLTH
    <Global.System.Data.Linq.Mapping.ColumnAttribute(DbType:="Int NOT NULL", IsPrimaryKey:=True)> _
    Public Property AppID As Integer
    Public Property TempID As String
    Public Property CusName As String
    Public Property CusSurname As String
    Public Property CusIDCard As String
    Public Property CusAddress1 As String
    Public Property CusAddress2 As String
    Public Property CusMobile As String
    Public Property CusHomePhone As String
    Public Property CusOfficePhone As System.Nullable(Of Integer)
    Public Property CarEngineNo As String
    Public Property CarChassisNo As String
    Public Property CarPlantNo As String
    Public Property DateEffective As System.Nullable(Of Date)
    Public Property DateExpired As System.Nullable(Of Date)
    Public Property DateNotification As System.Nullable(Of Date)
    Public Property AppStatus As String
    Public Property AppIsActive As System.Nullable(Of Integer)
    Public Property ClientCode As String
    Public Property CmiBuying As System.Nullable(Of Integer)
    Public Property AppBeneficiary As String
    Public Property LeasingID As System.Nullable(Of Integer)
    Public Property ClosingDate As System.Nullable(Of Date)
    Public Property ClosingMonth As System.Nullable(Of Date)
    Public Property ClosingDay As System.Nullable(Of Date)
    Public Property AppStatusRecived As System.Nullable(Of Boolean)
    Public Property ProjectID As System.Nullable(Of Integer)
    Public Property NotifyName As String
    Public Property CusTitle As String
    Public Property CarGroupID As System.Nullable(Of Integer)
    Public Property CarGroupName As String
    Public Property CarBrandName As String
    Public Property CarModelName As String
    Public Property CarTypeName As String
    Public Property CarTypeNameFull As String
    Public Property CarModelCode As String
    Public Property CarGroupNameM2 As String
    Public Property DealerNameEn As String
    Public Property NLTDealerCode As String
    Public Property NLTArea As String
    Public Property InsurerNameTh As String
    Public Property InsurerCodeSibis As String
    Public Property InsurerNameEn As String
    Public Property InsurerCode As String
    Public Property LeasID As System.Nullable(Of Integer)
    Public Property LeasName As String
    Public Property LeasNameAbbr As String
    Public Property LeasNameShort As String
    Public Property ModelNameM2 As String
    'Public Property NLTBookingDate As System.Nullable(Of Date)
    Public Property SchemeCode As String
    Public Property VmiNetPremium As System.Nullable(Of Decimal)
    Public Property VmiStamp As System.Nullable(Of Decimal)
    Public Property VmiVat As System.Nullable(Of Decimal)
    Public Property VmiTotalPremium As System.Nullable(Of Decimal)
    Public Property CmiNetPremium As System.Nullable(Of Decimal)
    Public Property CmiStamp As System.Nullable(Of Decimal)
    Public Property CmiVat As System.Nullable(Of Decimal)
    Public Property CmiTotalPremium As System.Nullable(Of Decimal)
    Public Property ClassOfRiskVMI As String
    Public Property ClassOfRiskCMI As String
    Public Property ShowroomID As System.Nullable(Of Integer)
    Public Property ShowroomName As String
    Public Property DealerID As System.Nullable(Of Integer)
    Public Property ShorwoomNameEn As String
    Public Property CarTypeNameM2 As String
    Public Property TypeInsID As System.Nullable(Of Integer)
    Public Property TypeInsName As String
    Public Property TypeInsStatus As System.Nullable(Of Integer)
    Public Property TypeInsReportToNLTH As System.Nullable(Of Boolean)
    Public Property TypeInsIsFree As System.Nullable(Of Boolean)
    Public Property UserAddedTel As String
    Public Property UserAddedType As String
End Class
Public Class V_NissanMotorNLTH_Map
    Inherits EntityTypeConfiguration(Of V_NissanMotorNLTH)
    Public Sub New(_table As String)
        ' Primary Key
        Me.HasKey(Function(t) t.AppID)

        ' Properties
        'Me.Property(Function(t) t.FirstName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.LastName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.Phone).IsRequired().HasMaxLength(20)


        ' Table & Column Mappings
        'Me.ToTable("V_NissanMotorNLTH")
        Me.ToTable(_table)
        Me.Property(Function(t) t.AppID).HasColumnName("AppID")
        Me.Property(Function(t) t.TempID).HasColumnName("TempID")
        Me.Property(Function(t) t.CusName).HasColumnName("CusName")
        Me.Property(Function(t) t.CusSurname).HasColumnName("CusSurname")
        Me.Property(Function(t) t.CusIDCard).HasColumnName("CusIDCard")
        Me.Property(Function(t) t.CusAddress1).HasColumnName("CusAddress1")
        Me.Property(Function(t) t.CusAddress2).HasColumnName("CusAddress2")
        Me.Property(Function(t) t.CusMobile).HasColumnName("CusMobile")
        Me.Property(Function(t) t.CusHomePhone).HasColumnName("CusHomePhone")
        Me.Property(Function(t) t.CusOfficePhone).HasColumnName("CusOfficePhone")
        Me.Property(Function(t) t.CarEngineNo).HasColumnName("CarEngineNo")
        Me.Property(Function(t) t.CarChassisNo).HasColumnName("CarChassisNo")
        Me.Property(Function(t) t.CarPlantNo).HasColumnName("CarPlantNo")
        Me.Property(Function(t) t.DateEffective).HasColumnName("DateEffective")
        Me.Property(Function(t) t.DateExpired).HasColumnName("DateExpired")
        Me.Property(Function(t) t.DateNotification).HasColumnName("DateNotification")
        Me.Property(Function(t) t.AppStatus).HasColumnName("AppStatus")
        Me.Property(Function(t) t.AppIsActive).HasColumnName("AppIsActive")
        Me.Property(Function(t) t.ClientCode).HasColumnName("ClientCode")
        Me.Property(Function(t) t.CmiBuying).HasColumnName("CmiBuying")
        Me.Property(Function(t) t.AppBeneficiary).HasColumnName("AppBeneficiary")
        Me.Property(Function(t) t.LeasingID).HasColumnName("LeasingID")
        Me.Property(Function(t) t.ClosingDate).HasColumnName("ClosingDate")
        Me.Property(Function(t) t.ClosingMonth).HasColumnName("ClosingMonth")
        Me.Property(Function(t) t.ClosingDay).HasColumnName("ClosingDay")
        Me.Property(Function(t) t.AppStatusRecived).HasColumnName("AppStatusRecived")
        Me.Property(Function(t) t.ProjectID).HasColumnName("ProjectID")
        Me.Property(Function(t) t.NotifyName).HasColumnName("NotifyName")
        Me.Property(Function(t) t.CusTitle).HasColumnName("CusTitle")
        Me.Property(Function(t) t.CarGroupID).HasColumnName("CarGroupID")
        Me.Property(Function(t) t.CarGroupName).HasColumnName("CarGroupName")
        Me.Property(Function(t) t.CarBrandName).HasColumnName("CarBrandName")
        Me.Property(Function(t) t.CarModelName).HasColumnName("CarModelName")
        Me.Property(Function(t) t.CarTypeName).HasColumnName("CarTypeName")
        Me.Property(Function(t) t.CarTypeNameFull).HasColumnName("CarTypeNameFull")
        Me.Property(Function(t) t.CarModelCode).HasColumnName("CarModelCode")
        Me.Property(Function(t) t.CarGroupNameM2).HasColumnName("CarGroupNameM2")
        Me.Property(Function(t) t.DealerNameEn).HasColumnName("DealerNameEn")
        Me.Property(Function(t) t.NLTDealerCode).HasColumnName("NLTDealerCode")
        Me.Property(Function(t) t.NLTArea).HasColumnName("NLTArea")
        Me.Property(Function(t) t.InsurerNameTh).HasColumnName("InsurerNameTh")
        Me.Property(Function(t) t.InsurerCodeSibis).HasColumnName("InsurerCodeSibis")
        Me.Property(Function(t) t.InsurerNameEn).HasColumnName("InsurerNameEn")
        Me.Property(Function(t) t.InsurerCode).HasColumnName("InsurerCode")
        Me.Property(Function(t) t.LeasID).HasColumnName("LeasID")
        Me.Property(Function(t) t.LeasName).HasColumnName("LeasName")
        Me.Property(Function(t) t.LeasNameAbbr).HasColumnName("LeasNameAbbr")
        Me.Property(Function(t) t.LeasNameShort).HasColumnName("LeasNameShort")
        Me.Property(Function(t) t.ModelNameM2).HasColumnName("ModelNameM2")
        'Me.Property(Function(t) t.NLTBookingDate).HasColumnName("NLTBookingDate")
        Me.Property(Function(t) t.SchemeCode).HasColumnName("SchemeCode")
        Me.Property(Function(t) t.VmiNetPremium).HasColumnName("VmiNetPremium")
        Me.Property(Function(t) t.VmiStamp).HasColumnName("VmiStamp")
        Me.Property(Function(t) t.VmiVat).HasColumnName("VmiVat")
        Me.Property(Function(t) t.VmiTotalPremium).HasColumnName("VmiTotalPremium")
        Me.Property(Function(t) t.CmiNetPremium).HasColumnName("CmiNetPremium")
        Me.Property(Function(t) t.CmiStamp).HasColumnName("CmiStamp")
        Me.Property(Function(t) t.CmiVat).HasColumnName("CmiVat")
        Me.Property(Function(t) t.CmiTotalPremium).HasColumnName("CmiTotalPremium")
        Me.Property(Function(t) t.ClassOfRiskVMI).HasColumnName("ClassOfRiskVMI")
        Me.Property(Function(t) t.ClassOfRiskCMI).HasColumnName("ClassOfRiskCMI")
        Me.Property(Function(t) t.ShowroomID).HasColumnName("ShowroomID")
        Me.Property(Function(t) t.ShowroomName).HasColumnName("ShowroomName")
        Me.Property(Function(t) t.DealerID).HasColumnName("DealerID")
        Me.Property(Function(t) t.ShorwoomNameEn).HasColumnName("ShorwoomNameEn")
        Me.Property(Function(t) t.CarTypeNameM2).HasColumnName("CarTypeNameM2")
        Me.Property(Function(t) t.TypeInsID).HasColumnName("TypeInsID")
        Me.Property(Function(t) t.TypeInsName).HasColumnName("TypeInsName")
        Me.Property(Function(t) t.TypeInsStatus).HasColumnName("TypeInsStatus")
        Me.Property(Function(t) t.TypeInsReportToNLTH).HasColumnName("TypeInsReportToNLTH")
        Me.Property(Function(t) t.TypeInsIsFree).HasColumnName("TypeInsIsFree")
        Me.Property(Function(t) t.UserAddedTel).HasColumnName("UserAddedTel")
        Me.Property(Function(t) t.UserAddedType).HasColumnName("UserAddedType")

    End Sub
End Class



'======================NLTH===============================
'V_NLTH_All_Campaign_Daily
Partial Public Class V_NLTH_All_Campaign_Daily
    <Global.System.Data.Linq.Mapping.ColumnAttribute(DbType:="Int NOT NULL", IsPrimaryKey:=True)> _
    Public Property AppID As Integer
    Public Property TempID As String
    Public Property CusName As String
    Public Property CusSurname As String
    Public Property CusIDCard As String
    Public Property CusAddress1 As String
    Public Property CusAddress2 As String
    Public Property CusMobile As String
    Public Property CusHomePhone As String
    Public Property CusOfficePhone As System.Nullable(Of Integer)
    Public Property CarEngineNo As String
    Public Property CarChassisNo As String
    Public Property CarPlantNo As String
    Public Property DateEffective As System.Nullable(Of Date)
    Public Property DateExpired As System.Nullable(Of Date)
    Public Property DateNotification As System.Nullable(Of Date)
    Public Property AppStatus As String
    Public Property AppIsActive As System.Nullable(Of Integer)
    Public Property ClientCode As String
    Public Property CmiBuying As System.Nullable(Of Integer)
    Public Property AppBeneficiary As String
    Public Property LeasingID As System.Nullable(Of Integer)
    Public Property ClosingDate As System.Nullable(Of Date)
    Public Property ClosingMonth As System.Nullable(Of Date)
    Public Property ClosingDay As System.Nullable(Of Date)
    Public Property AppStatusRecived As System.Nullable(Of Boolean)
    Public Property ProjectID As System.Nullable(Of Integer)
    Public Property NotifyName As String
    Public Property CusTitle As String
    Public Property CarGroupID As System.Nullable(Of Integer)
    Public Property CarGroupName As String
    Public Property CarBrandName As String
    Public Property CarModelName As String
    Public Property CarTypeName As String
    Public Property CarTypeNameFull As String
    Public Property CarModelCode As String
    Public Property CarGroupNameM2 As String
    Public Property DealerNameEn As String
    Public Property NLTDealerCode As String
    Public Property NLTArea As String
    Public Property InsurerNameTh As String
    Public Property InsurerCodeSibis As String
    Public Property InsurerNameEn As String
    Public Property InsurerCode As String
    Public Property LeasID As System.Nullable(Of Integer)
    Public Property LeasName As String
    Public Property LeasNameAbbr As String
    Public Property LeasNameShort As String
    Public Property ModelNameM2 As String
    'Public Property NLTBookingDate As System.Nullable(Of Date)
    Public Property SchemeCode As String
    Public Property VmiNetPremium As System.Nullable(Of Decimal)
    Public Property VmiStamp As System.Nullable(Of Decimal)
    Public Property VmiVat As System.Nullable(Of Decimal)
    Public Property VmiTotalPremium As System.Nullable(Of Decimal)
    Public Property CmiNetPremium As System.Nullable(Of Decimal)
    Public Property CmiStamp As System.Nullable(Of Decimal)
    Public Property CmiVat As System.Nullable(Of Decimal)
    Public Property CmiTotalPremium As System.Nullable(Of Decimal)
    Public Property ClassOfRiskVMI As String
    Public Property ClassOfRiskCMI As String
    Public Property ShowroomID As System.Nullable(Of Integer)
    Public Property ShowroomName As String
    Public Property DealerID As System.Nullable(Of Integer)
    Public Property ShorwoomNameEn As String
    Public Property CarTypeNameM2 As String
    Public Property TypeInsID As System.Nullable(Of Integer)
    Public Property TypeInsName As String
    Public Property TypeInsStatus As System.Nullable(Of Integer)
    Public Property TypeInsReportToNLTH As System.Nullable(Of Boolean)
    Public Property TypeInsIsFree As System.Nullable(Of Boolean)
    Public Property UserAddedTel As String
    Public Property UserAddedType As String
End Class
Public Class V_NLTH_All_Campaign_Daily_Map
    Inherits EntityTypeConfiguration(Of V_NLTH_All_Campaign_Daily)
    Public Sub New(_table As String)
        ' Primary Key
        Me.HasKey(Function(t) t.AppID)

        ' Properties
        'Me.Property(Function(t) t.FirstName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.LastName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.Phone).IsRequired().HasMaxLength(20)


        ' Table & Column Mappings
        'Me.ToTable("V_NLTH_All_Campaign_Daily")
        Me.ToTable(_table)
        Me.Property(Function(t) t.AppID).HasColumnName("AppID")
        Me.Property(Function(t) t.TempID).HasColumnName("TempID")
        Me.Property(Function(t) t.CusName).HasColumnName("CusName")
        Me.Property(Function(t) t.CusSurname).HasColumnName("CusSurname")
        Me.Property(Function(t) t.CusIDCard).HasColumnName("CusIDCard")
        Me.Property(Function(t) t.CusAddress1).HasColumnName("CusAddress1")
        Me.Property(Function(t) t.CusAddress2).HasColumnName("CusAddress2")
        Me.Property(Function(t) t.CusMobile).HasColumnName("CusMobile")
        Me.Property(Function(t) t.CusHomePhone).HasColumnName("CusHomePhone")
        Me.Property(Function(t) t.CusOfficePhone).HasColumnName("CusOfficePhone")
        Me.Property(Function(t) t.CarEngineNo).HasColumnName("CarEngineNo")
        Me.Property(Function(t) t.CarChassisNo).HasColumnName("CarChassisNo")
        Me.Property(Function(t) t.CarPlantNo).HasColumnName("CarPlantNo")
        Me.Property(Function(t) t.DateEffective).HasColumnName("DateEffective")
        Me.Property(Function(t) t.DateExpired).HasColumnName("DateExpired")
        Me.Property(Function(t) t.DateNotification).HasColumnName("DateNotification")
        Me.Property(Function(t) t.AppStatus).HasColumnName("AppStatus")
        Me.Property(Function(t) t.AppIsActive).HasColumnName("AppIsActive")
        Me.Property(Function(t) t.ClientCode).HasColumnName("ClientCode")
        Me.Property(Function(t) t.CmiBuying).HasColumnName("CmiBuying")
        Me.Property(Function(t) t.AppBeneficiary).HasColumnName("AppBeneficiary")
        Me.Property(Function(t) t.LeasingID).HasColumnName("LeasingID")
        Me.Property(Function(t) t.ClosingDate).HasColumnName("ClosingDate")
        Me.Property(Function(t) t.ClosingMonth).HasColumnName("ClosingMonth")
        Me.Property(Function(t) t.ClosingDay).HasColumnName("ClosingDay")
        Me.Property(Function(t) t.AppStatusRecived).HasColumnName("AppStatusRecived")
        Me.Property(Function(t) t.ProjectID).HasColumnName("ProjectID")
        Me.Property(Function(t) t.NotifyName).HasColumnName("NotifyName")
        Me.Property(Function(t) t.CusTitle).HasColumnName("CusTitle")
        Me.Property(Function(t) t.CarGroupID).HasColumnName("CarGroupID")
        Me.Property(Function(t) t.CarGroupName).HasColumnName("CarGroupName")
        Me.Property(Function(t) t.CarBrandName).HasColumnName("CarBrandName")
        Me.Property(Function(t) t.CarModelName).HasColumnName("CarModelName")
        Me.Property(Function(t) t.CarTypeName).HasColumnName("CarTypeName")
        Me.Property(Function(t) t.CarTypeNameFull).HasColumnName("CarTypeNameFull")
        Me.Property(Function(t) t.CarModelCode).HasColumnName("CarModelCode")
        Me.Property(Function(t) t.CarGroupNameM2).HasColumnName("CarGroupNameM2")
        Me.Property(Function(t) t.DealerNameEn).HasColumnName("DealerNameEn")
        Me.Property(Function(t) t.NLTDealerCode).HasColumnName("NLTDealerCode")
        Me.Property(Function(t) t.NLTArea).HasColumnName("NLTArea")
        Me.Property(Function(t) t.InsurerNameTh).HasColumnName("InsurerNameTh")
        Me.Property(Function(t) t.InsurerCodeSibis).HasColumnName("InsurerCodeSibis")
        Me.Property(Function(t) t.InsurerNameEn).HasColumnName("InsurerNameEn")
        Me.Property(Function(t) t.InsurerCode).HasColumnName("InsurerCode")
        Me.Property(Function(t) t.LeasID).HasColumnName("LeasID")
        Me.Property(Function(t) t.LeasName).HasColumnName("LeasName")
        Me.Property(Function(t) t.LeasNameAbbr).HasColumnName("LeasNameAbbr")
        Me.Property(Function(t) t.LeasNameShort).HasColumnName("LeasNameShort")
        Me.Property(Function(t) t.ModelNameM2).HasColumnName("ModelNameM2")
        'Me.Property(Function(t) t.NLTBookingDate).HasColumnName("NLTBookingDate")
        Me.Property(Function(t) t.SchemeCode).HasColumnName("SchemeCode")
        Me.Property(Function(t) t.VmiNetPremium).HasColumnName("VmiNetPremium")
        Me.Property(Function(t) t.VmiStamp).HasColumnName("VmiStamp")
        Me.Property(Function(t) t.VmiVat).HasColumnName("VmiVat")
        Me.Property(Function(t) t.VmiTotalPremium).HasColumnName("VmiTotalPremium")
        Me.Property(Function(t) t.CmiNetPremium).HasColumnName("CmiNetPremium")
        Me.Property(Function(t) t.CmiStamp).HasColumnName("CmiStamp")
        Me.Property(Function(t) t.CmiVat).HasColumnName("CmiVat")
        Me.Property(Function(t) t.CmiTotalPremium).HasColumnName("CmiTotalPremium")
        Me.Property(Function(t) t.ClassOfRiskVMI).HasColumnName("ClassOfRiskVMI")
        Me.Property(Function(t) t.ClassOfRiskCMI).HasColumnName("ClassOfRiskCMI")
        Me.Property(Function(t) t.ShowroomID).HasColumnName("ShowroomID")
        Me.Property(Function(t) t.ShowroomName).HasColumnName("ShowroomName")
        Me.Property(Function(t) t.DealerID).HasColumnName("DealerID")
        Me.Property(Function(t) t.ShorwoomNameEn).HasColumnName("ShorwoomNameEn")
        Me.Property(Function(t) t.CarTypeNameM2).HasColumnName("CarTypeNameM2")
        Me.Property(Function(t) t.TypeInsID).HasColumnName("TypeInsID")
        Me.Property(Function(t) t.TypeInsName).HasColumnName("TypeInsName")
        Me.Property(Function(t) t.TypeInsStatus).HasColumnName("TypeInsStatus")
        Me.Property(Function(t) t.TypeInsReportToNLTH).HasColumnName("TypeInsReportToNLTH")
        Me.Property(Function(t) t.TypeInsIsFree).HasColumnName("TypeInsIsFree")
        Me.Property(Function(t) t.UserAddedTel).HasColumnName("UserAddedTel")
        Me.Property(Function(t) t.UserAddedType).HasColumnName("UserAddedType")

    End Sub
End Class

'V_NLTH_All_Campaign_Monthly
Partial Public Class V_NLTH_All_Campaign_Monthly
    <Global.System.Data.Linq.Mapping.ColumnAttribute(DbType:="Int NOT NULL", IsPrimaryKey:=True)> _
    Public Property AppID As Integer
    Public Property TempID As String
    Public Property CusName As String
    Public Property CusSurname As String
    Public Property CusIDCard As String
    Public Property CusAddress1 As String
    Public Property CusAddress2 As String
    Public Property CusMobile As String
    Public Property CusHomePhone As String
    Public Property CusOfficePhone As System.Nullable(Of Integer)
    Public Property CarEngineNo As String
    Public Property CarChassisNo As String
    Public Property CarPlantNo As String
    Public Property DateEffective As System.Nullable(Of Date)
    Public Property DateExpired As System.Nullable(Of Date)
    Public Property DateNotification As System.Nullable(Of Date)
    Public Property AppStatus As String
    Public Property AppIsActive As System.Nullable(Of Integer)
    Public Property ClientCode As String
    Public Property CmiBuying As System.Nullable(Of Integer)
    Public Property AppBeneficiary As String
    Public Property LeasingID As System.Nullable(Of Integer)
    Public Property ClosingDate As System.Nullable(Of Date)
    Public Property ClosingMonth As System.Nullable(Of Date)
    Public Property ClosingDay As System.Nullable(Of Date)
    Public Property AppStatusRecived As System.Nullable(Of Boolean)
    Public Property ProjectID As System.Nullable(Of Integer)
    Public Property NotifyName As String
    Public Property CusTitle As String
    Public Property CarGroupID As System.Nullable(Of Integer)
    Public Property CarGroupName As String
    Public Property CarBrandName As String
    Public Property CarModelName As String
    Public Property CarTypeName As String
    Public Property CarTypeNameFull As String
    Public Property CarModelCode As String
    Public Property CarGroupNameM2 As String
    Public Property DealerNameEn As String
    Public Property NLTDealerCode As String
    Public Property NLTArea As String
    Public Property InsurerNameTh As String
    Public Property InsurerCodeSibis As String
    Public Property InsurerNameEn As String
    Public Property InsurerCode As String
    Public Property LeasID As System.Nullable(Of Integer)
    Public Property LeasName As String
    Public Property LeasNameAbbr As String
    Public Property LeasNameShort As String
    Public Property ModelNameM2 As String
    'Public Property NLTBookingDate As System.Nullable(Of Date)
    Public Property SchemeCode As String
    Public Property VmiNetPremium As System.Nullable(Of Decimal)
    Public Property VmiStamp As System.Nullable(Of Decimal)
    Public Property VmiVat As System.Nullable(Of Decimal)
    Public Property VmiTotalPremium As System.Nullable(Of Decimal)
    Public Property CmiNetPremium As System.Nullable(Of Decimal)
    Public Property CmiStamp As System.Nullable(Of Decimal)
    Public Property CmiVat As System.Nullable(Of Decimal)
    Public Property CmiTotalPremium As System.Nullable(Of Decimal)
    Public Property ClassOfRiskVMI As String
    Public Property ClassOfRiskCMI As String
    Public Property ShowroomID As System.Nullable(Of Integer)
    Public Property ShowroomName As String
    Public Property DealerID As System.Nullable(Of Integer)
    Public Property ShorwoomNameEn As String
    Public Property CarTypeNameM2 As String
    Public Property TypeInsID As System.Nullable(Of Integer)
    Public Property TypeInsName As String
    Public Property TypeInsStatus As System.Nullable(Of Integer)
    Public Property TypeInsReportToNLTH As System.Nullable(Of Boolean)
    Public Property TypeInsIsFree As System.Nullable(Of Boolean)
    Public Property UserAddedTel As String
    Public Property UserAddedType As String
End Class
Public Class V_NLTH_All_Campaign_Monthly_Map
    Inherits EntityTypeConfiguration(Of V_NLTH_All_Campaign_Monthly)
    Public Sub New(_table As String)
        ' Primary Key
        Me.HasKey(Function(t) t.AppID)

        ' Properties
        'Me.Property(Function(t) t.FirstName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.LastName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.Phone).IsRequired().HasMaxLength(20)


        ' Table & Column Mappings
        'Me.ToTable("V_NLTH_All_Campaign_Monthly")
        Me.ToTable(_table)
        Me.Property(Function(t) t.AppID).HasColumnName("AppID")
        Me.Property(Function(t) t.TempID).HasColumnName("TempID")
        Me.Property(Function(t) t.CusName).HasColumnName("CusName")
        Me.Property(Function(t) t.CusSurname).HasColumnName("CusSurname")
        Me.Property(Function(t) t.CusIDCard).HasColumnName("CusIDCard")
        Me.Property(Function(t) t.CusAddress1).HasColumnName("CusAddress1")
        Me.Property(Function(t) t.CusAddress2).HasColumnName("CusAddress2")
        Me.Property(Function(t) t.CusMobile).HasColumnName("CusMobile")
        Me.Property(Function(t) t.CusHomePhone).HasColumnName("CusHomePhone")
        Me.Property(Function(t) t.CusOfficePhone).HasColumnName("CusOfficePhone")
        Me.Property(Function(t) t.CarEngineNo).HasColumnName("CarEngineNo")
        Me.Property(Function(t) t.CarChassisNo).HasColumnName("CarChassisNo")
        Me.Property(Function(t) t.CarPlantNo).HasColumnName("CarPlantNo")
        Me.Property(Function(t) t.DateEffective).HasColumnName("DateEffective")
        Me.Property(Function(t) t.DateExpired).HasColumnName("DateExpired")
        Me.Property(Function(t) t.DateNotification).HasColumnName("DateNotification")
        Me.Property(Function(t) t.AppStatus).HasColumnName("AppStatus")
        Me.Property(Function(t) t.AppIsActive).HasColumnName("AppIsActive")
        Me.Property(Function(t) t.ClientCode).HasColumnName("ClientCode")
        Me.Property(Function(t) t.CmiBuying).HasColumnName("CmiBuying")
        Me.Property(Function(t) t.AppBeneficiary).HasColumnName("AppBeneficiary")
        Me.Property(Function(t) t.LeasingID).HasColumnName("LeasingID")
        Me.Property(Function(t) t.ClosingDate).HasColumnName("ClosingDate")
        Me.Property(Function(t) t.ClosingMonth).HasColumnName("ClosingMonth")
        Me.Property(Function(t) t.ClosingDay).HasColumnName("ClosingDay")
        Me.Property(Function(t) t.AppStatusRecived).HasColumnName("AppStatusRecived")
        Me.Property(Function(t) t.ProjectID).HasColumnName("ProjectID")
        Me.Property(Function(t) t.NotifyName).HasColumnName("NotifyName")
        Me.Property(Function(t) t.CusTitle).HasColumnName("CusTitle")
        Me.Property(Function(t) t.CarGroupID).HasColumnName("CarGroupID")
        Me.Property(Function(t) t.CarGroupName).HasColumnName("CarGroupName")
        Me.Property(Function(t) t.CarBrandName).HasColumnName("CarBrandName")
        Me.Property(Function(t) t.CarModelName).HasColumnName("CarModelName")
        Me.Property(Function(t) t.CarTypeName).HasColumnName("CarTypeName")
        Me.Property(Function(t) t.CarTypeNameFull).HasColumnName("CarTypeNameFull")
        Me.Property(Function(t) t.CarModelCode).HasColumnName("CarModelCode")
        Me.Property(Function(t) t.CarGroupNameM2).HasColumnName("CarGroupNameM2")
        Me.Property(Function(t) t.DealerNameEn).HasColumnName("DealerNameEn")
        Me.Property(Function(t) t.NLTDealerCode).HasColumnName("NLTDealerCode")
        Me.Property(Function(t) t.NLTArea).HasColumnName("NLTArea")
        Me.Property(Function(t) t.InsurerNameTh).HasColumnName("InsurerNameTh")
        Me.Property(Function(t) t.InsurerCodeSibis).HasColumnName("InsurerCodeSibis")
        Me.Property(Function(t) t.InsurerNameEn).HasColumnName("InsurerNameEn")
        Me.Property(Function(t) t.InsurerCode).HasColumnName("InsurerCode")
        Me.Property(Function(t) t.LeasID).HasColumnName("LeasID")
        Me.Property(Function(t) t.LeasName).HasColumnName("LeasName")
        Me.Property(Function(t) t.LeasNameAbbr).HasColumnName("LeasNameAbbr")
        Me.Property(Function(t) t.LeasNameShort).HasColumnName("LeasNameShort")
        Me.Property(Function(t) t.ModelNameM2).HasColumnName("ModelNameM2")
        'Me.Property(Function(t) t.NLTBookingDate).HasColumnName("NLTBookingDate")
        Me.Property(Function(t) t.SchemeCode).HasColumnName("SchemeCode")
        Me.Property(Function(t) t.VmiNetPremium).HasColumnName("VmiNetPremium")
        Me.Property(Function(t) t.VmiStamp).HasColumnName("VmiStamp")
        Me.Property(Function(t) t.VmiVat).HasColumnName("VmiVat")
        Me.Property(Function(t) t.VmiTotalPremium).HasColumnName("VmiTotalPremium")
        Me.Property(Function(t) t.CmiNetPremium).HasColumnName("CmiNetPremium")
        Me.Property(Function(t) t.CmiStamp).HasColumnName("CmiStamp")
        Me.Property(Function(t) t.CmiVat).HasColumnName("CmiVat")
        Me.Property(Function(t) t.CmiTotalPremium).HasColumnName("CmiTotalPremium")
        Me.Property(Function(t) t.ClassOfRiskVMI).HasColumnName("ClassOfRiskVMI")
        Me.Property(Function(t) t.ClassOfRiskCMI).HasColumnName("ClassOfRiskCMI")
        Me.Property(Function(t) t.ShowroomID).HasColumnName("ShowroomID")
        Me.Property(Function(t) t.ShowroomName).HasColumnName("ShowroomName")
        Me.Property(Function(t) t.DealerID).HasColumnName("DealerID")
        Me.Property(Function(t) t.ShorwoomNameEn).HasColumnName("ShorwoomNameEn")
        Me.Property(Function(t) t.CarTypeNameM2).HasColumnName("CarTypeNameM2")
        Me.Property(Function(t) t.TypeInsID).HasColumnName("TypeInsID")
        Me.Property(Function(t) t.TypeInsName).HasColumnName("TypeInsName")
        Me.Property(Function(t) t.TypeInsStatus).HasColumnName("TypeInsStatus")
        Me.Property(Function(t) t.TypeInsReportToNLTH).HasColumnName("TypeInsReportToNLTH")
        Me.Property(Function(t) t.TypeInsIsFree).HasColumnName("TypeInsIsFree")
        Me.Property(Function(t) t.UserAddedTel).HasColumnName("UserAddedTel")
        Me.Property(Function(t) t.UserAddedType).HasColumnName("UserAddedType")

    End Sub
End Class

'V_NLTH_All_Status_Daily
Partial Public Class V_NLTH_All_Status_Daily
    <Global.System.Data.Linq.Mapping.ColumnAttribute(DbType:="Int NOT NULL", IsPrimaryKey:=True)> _
    Public Property AppID As Integer
    Public Property TempID As String
    Public Property CusName As String
    Public Property CusSurname As String
    Public Property CusIDCard As String
    Public Property CusAddress1 As String
    Public Property CusAddress2 As String
    Public Property CusMobile As String
    Public Property CusHomePhone As String
    Public Property CusOfficePhone As System.Nullable(Of Integer)
    Public Property CarEngineNo As String
    Public Property CarChassisNo As String
    Public Property CarPlantNo As String
    Public Property DateEffective As System.Nullable(Of Date)
    Public Property DateExpired As System.Nullable(Of Date)
    Public Property DateNotification As System.Nullable(Of Date)
    Public Property AppStatus As String
    Public Property AppIsActive As System.Nullable(Of Integer)
    Public Property ClientCode As String
    Public Property CmiBuying As System.Nullable(Of Integer)
    Public Property AppBeneficiary As String
    Public Property LeasingID As System.Nullable(Of Integer)
    Public Property ClosingDate As System.Nullable(Of Date)
    Public Property ClosingMonth As System.Nullable(Of Date)
    Public Property ClosingDay As System.Nullable(Of Date)
    Public Property AppStatusRecived As System.Nullable(Of Boolean)
    Public Property ProjectID As System.Nullable(Of Integer)
    Public Property NotifyName As String
    Public Property CusTitle As String
    Public Property CarGroupID As System.Nullable(Of Integer)
    Public Property CarGroupName As String
    Public Property CarBrandName As String
    Public Property CarModelName As String
    Public Property CarTypeName As String
    Public Property CarTypeNameFull As String
    Public Property CarModelCode As String
    Public Property CarGroupNameM2 As String
    Public Property DealerNameEn As String
    Public Property NLTDealerCode As String
    Public Property NLTArea As String
    Public Property InsurerNameTh As String
    Public Property InsurerCodeSibis As String
    Public Property InsurerNameEn As String
    Public Property InsurerCode As String
    Public Property LeasID As System.Nullable(Of Integer)
    Public Property LeasName As String
    Public Property LeasNameAbbr As String
    Public Property LeasNameShort As String
    Public Property ModelNameM2 As String
    'Public Property NLTBookingDate As System.Nullable(Of Date)
    Public Property SchemeCode As String
    Public Property VmiNetPremium As System.Nullable(Of Decimal)
    Public Property VmiStamp As System.Nullable(Of Decimal)
    Public Property VmiVat As System.Nullable(Of Decimal)
    Public Property VmiTotalPremium As System.Nullable(Of Decimal)
    Public Property CmiNetPremium As System.Nullable(Of Decimal)
    Public Property CmiStamp As System.Nullable(Of Decimal)
    Public Property CmiVat As System.Nullable(Of Decimal)
    Public Property CmiTotalPremium As System.Nullable(Of Decimal)
    Public Property ClassOfRiskVMI As String
    Public Property ClassOfRiskCMI As String
    Public Property ShowroomID As System.Nullable(Of Integer)
    Public Property ShowroomName As String
    Public Property DealerID As System.Nullable(Of Integer)
    Public Property ShorwoomNameEn As String
    Public Property CarTypeNameM2 As String
    Public Property TypeInsID As System.Nullable(Of Integer)
    Public Property TypeInsName As String
    Public Property TypeInsStatus As System.Nullable(Of Integer)
    Public Property TypeInsReportToNLTH As System.Nullable(Of Boolean)
    Public Property TypeInsIsFree As System.Nullable(Of Boolean)
    Public Property UserAddedTel As String
    Public Property UserAddedType As String
End Class
Public Class V_NLTH_All_Status_Daily_Map
    Inherits EntityTypeConfiguration(Of V_NLTH_All_Status_Daily)
    Public Sub New(_table As String)
        ' Primary Key
        Me.HasKey(Function(t) t.AppID)

        ' Properties
        'Me.Property(Function(t) t.FirstName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.LastName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.Phone).IsRequired().HasMaxLength(20)


        ' Table & Column Mappings
        'Me.ToTable("V_NLTH_All_Status_Daily")
        Me.ToTable(_table)
        Me.Property(Function(t) t.AppID).HasColumnName("AppID")
        Me.Property(Function(t) t.TempID).HasColumnName("TempID")
        Me.Property(Function(t) t.CusName).HasColumnName("CusName")
        Me.Property(Function(t) t.CusSurname).HasColumnName("CusSurname")
        Me.Property(Function(t) t.CusIDCard).HasColumnName("CusIDCard")
        Me.Property(Function(t) t.CusAddress1).HasColumnName("CusAddress1")
        Me.Property(Function(t) t.CusAddress2).HasColumnName("CusAddress2")
        Me.Property(Function(t) t.CusMobile).HasColumnName("CusMobile")
        Me.Property(Function(t) t.CusHomePhone).HasColumnName("CusHomePhone")
        Me.Property(Function(t) t.CusOfficePhone).HasColumnName("CusOfficePhone")
        Me.Property(Function(t) t.CarEngineNo).HasColumnName("CarEngineNo")
        Me.Property(Function(t) t.CarChassisNo).HasColumnName("CarChassisNo")
        Me.Property(Function(t) t.CarPlantNo).HasColumnName("CarPlantNo")
        Me.Property(Function(t) t.DateEffective).HasColumnName("DateEffective")
        Me.Property(Function(t) t.DateExpired).HasColumnName("DateExpired")
        Me.Property(Function(t) t.DateNotification).HasColumnName("DateNotification")
        Me.Property(Function(t) t.AppStatus).HasColumnName("AppStatus")
        Me.Property(Function(t) t.AppIsActive).HasColumnName("AppIsActive")
        Me.Property(Function(t) t.ClientCode).HasColumnName("ClientCode")
        Me.Property(Function(t) t.CmiBuying).HasColumnName("CmiBuying")
        Me.Property(Function(t) t.AppBeneficiary).HasColumnName("AppBeneficiary")
        Me.Property(Function(t) t.LeasingID).HasColumnName("LeasingID")
        Me.Property(Function(t) t.ClosingDate).HasColumnName("ClosingDate")
        Me.Property(Function(t) t.ClosingMonth).HasColumnName("ClosingMonth")
        Me.Property(Function(t) t.ClosingDay).HasColumnName("ClosingDay")
        Me.Property(Function(t) t.AppStatusRecived).HasColumnName("AppStatusRecived")
        Me.Property(Function(t) t.ProjectID).HasColumnName("ProjectID")
        Me.Property(Function(t) t.NotifyName).HasColumnName("NotifyName")
        Me.Property(Function(t) t.CusTitle).HasColumnName("CusTitle")
        Me.Property(Function(t) t.CarGroupID).HasColumnName("CarGroupID")
        Me.Property(Function(t) t.CarGroupName).HasColumnName("CarGroupName")
        Me.Property(Function(t) t.CarBrandName).HasColumnName("CarBrandName")
        Me.Property(Function(t) t.CarModelName).HasColumnName("CarModelName")
        Me.Property(Function(t) t.CarTypeName).HasColumnName("CarTypeName")
        Me.Property(Function(t) t.CarTypeNameFull).HasColumnName("CarTypeNameFull")
        Me.Property(Function(t) t.CarModelCode).HasColumnName("CarModelCode")
        Me.Property(Function(t) t.CarGroupNameM2).HasColumnName("CarGroupNameM2")
        Me.Property(Function(t) t.DealerNameEn).HasColumnName("DealerNameEn")
        Me.Property(Function(t) t.NLTDealerCode).HasColumnName("NLTDealerCode")
        Me.Property(Function(t) t.NLTArea).HasColumnName("NLTArea")
        Me.Property(Function(t) t.InsurerNameTh).HasColumnName("InsurerNameTh")
        Me.Property(Function(t) t.InsurerCodeSibis).HasColumnName("InsurerCodeSibis")
        Me.Property(Function(t) t.InsurerNameEn).HasColumnName("InsurerNameEn")
        Me.Property(Function(t) t.InsurerCode).HasColumnName("InsurerCode")
        Me.Property(Function(t) t.LeasID).HasColumnName("LeasID")
        Me.Property(Function(t) t.LeasName).HasColumnName("LeasName")
        Me.Property(Function(t) t.LeasNameAbbr).HasColumnName("LeasNameAbbr")
        Me.Property(Function(t) t.LeasNameShort).HasColumnName("LeasNameShort")
        Me.Property(Function(t) t.ModelNameM2).HasColumnName("ModelNameM2")
        'Me.Property(Function(t) t.NLTBookingDate).HasColumnName("NLTBookingDate")
        Me.Property(Function(t) t.SchemeCode).HasColumnName("SchemeCode")
        Me.Property(Function(t) t.VmiNetPremium).HasColumnName("VmiNetPremium")
        Me.Property(Function(t) t.VmiStamp).HasColumnName("VmiStamp")
        Me.Property(Function(t) t.VmiVat).HasColumnName("VmiVat")
        Me.Property(Function(t) t.VmiTotalPremium).HasColumnName("VmiTotalPremium")
        Me.Property(Function(t) t.CmiNetPremium).HasColumnName("CmiNetPremium")
        Me.Property(Function(t) t.CmiStamp).HasColumnName("CmiStamp")
        Me.Property(Function(t) t.CmiVat).HasColumnName("CmiVat")
        Me.Property(Function(t) t.CmiTotalPremium).HasColumnName("CmiTotalPremium")
        Me.Property(Function(t) t.ClassOfRiskVMI).HasColumnName("ClassOfRiskVMI")
        Me.Property(Function(t) t.ClassOfRiskCMI).HasColumnName("ClassOfRiskCMI")
        Me.Property(Function(t) t.ShowroomID).HasColumnName("ShowroomID")
        Me.Property(Function(t) t.ShowroomName).HasColumnName("ShowroomName")
        Me.Property(Function(t) t.DealerID).HasColumnName("DealerID")
        Me.Property(Function(t) t.ShorwoomNameEn).HasColumnName("ShorwoomNameEn")
        Me.Property(Function(t) t.CarTypeNameM2).HasColumnName("CarTypeNameM2")
        Me.Property(Function(t) t.TypeInsID).HasColumnName("TypeInsID")
        Me.Property(Function(t) t.TypeInsName).HasColumnName("TypeInsName")
        Me.Property(Function(t) t.TypeInsStatus).HasColumnName("TypeInsStatus")
        Me.Property(Function(t) t.TypeInsReportToNLTH).HasColumnName("TypeInsReportToNLTH")
        Me.Property(Function(t) t.TypeInsIsFree).HasColumnName("TypeInsIsFree")
        Me.Property(Function(t) t.UserAddedTel).HasColumnName("UserAddedTel")
        Me.Property(Function(t) t.UserAddedType).HasColumnName("UserAddedType")

    End Sub
End Class

'V_NLTH_Free_NPP_Campaign_Monthly
Partial Public Class V_NLTH_Free_NPP_Campaign_Monthly
    <Global.System.Data.Linq.Mapping.ColumnAttribute(DbType:="Int NOT NULL", IsPrimaryKey:=True)> _
    Public Property AppID As Integer
    Public Property TempID As String
    Public Property CusName As String
    Public Property CusSurname As String
    Public Property CusIDCard As String
    Public Property CusAddress1 As String
    Public Property CusAddress2 As String
    Public Property CusMobile As String
    Public Property CusHomePhone As String
    Public Property CusOfficePhone As System.Nullable(Of Integer)
    Public Property CarEngineNo As String
    Public Property CarChassisNo As String
    Public Property CarPlantNo As String
    Public Property DateEffective As System.Nullable(Of Date)
    Public Property DateExpired As System.Nullable(Of Date)
    Public Property DateNotification As System.Nullable(Of Date)
    Public Property AppStatus As String
    Public Property AppIsActive As System.Nullable(Of Integer)
    Public Property ClientCode As String
    Public Property CmiBuying As System.Nullable(Of Integer)
    Public Property AppBeneficiary As String
    Public Property LeasingID As System.Nullable(Of Integer)
    Public Property ClosingDate As System.Nullable(Of Date)
    Public Property ClosingMonth As System.Nullable(Of Date)
    Public Property ClosingDay As System.Nullable(Of Date)
    Public Property AppStatusRecived As System.Nullable(Of Boolean)
    Public Property ProjectID As System.Nullable(Of Integer)
    Public Property NotifyName As String
    Public Property CusTitle As String
    Public Property CarGroupID As System.Nullable(Of Integer)
    Public Property CarGroupName As String
    Public Property CarBrandName As String
    Public Property CarModelName As String
    Public Property CarTypeName As String
    Public Property CarTypeNameFull As String
    Public Property CarModelCode As String
    Public Property CarGroupNameM2 As String
    Public Property DealerNameEn As String
    Public Property NLTDealerCode As String
    Public Property NLTArea As String
    Public Property InsurerNameTh As String
    Public Property InsurerCodeSibis As String
    Public Property InsurerNameEn As String
    Public Property InsurerCode As String
    Public Property LeasID As System.Nullable(Of Integer)
    Public Property LeasName As String
    Public Property LeasNameAbbr As String
    Public Property LeasNameShort As String
    Public Property ModelNameM2 As String
    'Public Property NLTBookingDate As System.Nullable(Of Date)
    Public Property SchemeCode As String
    Public Property VmiNetPremium As System.Nullable(Of Decimal)
    Public Property VmiStamp As System.Nullable(Of Decimal)
    Public Property VmiVat As System.Nullable(Of Decimal)
    Public Property VmiTotalPremium As System.Nullable(Of Decimal)
    Public Property CmiNetPremium As System.Nullable(Of Decimal)
    Public Property CmiStamp As System.Nullable(Of Decimal)
    Public Property CmiVat As System.Nullable(Of Decimal)
    Public Property CmiTotalPremium As System.Nullable(Of Decimal)
    Public Property ClassOfRiskVMI As String
    Public Property ClassOfRiskCMI As String
    Public Property ShowroomID As System.Nullable(Of Integer)
    Public Property ShowroomName As String
    Public Property DealerID As System.Nullable(Of Integer)
    Public Property ShorwoomNameEn As String
    Public Property CarTypeNameM2 As String
    Public Property TypeInsID As System.Nullable(Of Integer)
    Public Property TypeInsName As String
    Public Property TypeInsStatus As System.Nullable(Of Integer)
    Public Property TypeInsReportToNLTH As System.Nullable(Of Boolean)
    Public Property TypeInsIsFree As System.Nullable(Of Boolean)
    Public Property UserAddedTel As String
    Public Property UserAddedType As String
End Class
Public Class V_NLTH_Free_NPP_Campaign_Monthly_Map
    Inherits EntityTypeConfiguration(Of V_NLTH_Free_NPP_Campaign_Monthly)
    Public Sub New(_table As String)
        ' Primary Key
        Me.HasKey(Function(t) t.AppID)

        ' Properties
        'Me.Property(Function(t) t.FirstName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.LastName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.Phone).IsRequired().HasMaxLength(20)


        ' Table & Column Mappings
        'Me.ToTable("V_NLTH_All_Campaign_Monthly")
        Me.ToTable(_table)
        Me.Property(Function(t) t.AppID).HasColumnName("AppID")
        Me.Property(Function(t) t.TempID).HasColumnName("TempID")
        Me.Property(Function(t) t.CusName).HasColumnName("CusName")
        Me.Property(Function(t) t.CusSurname).HasColumnName("CusSurname")
        Me.Property(Function(t) t.CusIDCard).HasColumnName("CusIDCard")
        Me.Property(Function(t) t.CusAddress1).HasColumnName("CusAddress1")
        Me.Property(Function(t) t.CusAddress2).HasColumnName("CusAddress2")
        Me.Property(Function(t) t.CusMobile).HasColumnName("CusMobile")
        Me.Property(Function(t) t.CusHomePhone).HasColumnName("CusHomePhone")
        Me.Property(Function(t) t.CusOfficePhone).HasColumnName("CusOfficePhone")
        Me.Property(Function(t) t.CarEngineNo).HasColumnName("CarEngineNo")
        Me.Property(Function(t) t.CarChassisNo).HasColumnName("CarChassisNo")
        Me.Property(Function(t) t.CarPlantNo).HasColumnName("CarPlantNo")
        Me.Property(Function(t) t.DateEffective).HasColumnName("DateEffective")
        Me.Property(Function(t) t.DateExpired).HasColumnName("DateExpired")
        Me.Property(Function(t) t.DateNotification).HasColumnName("DateNotification")
        Me.Property(Function(t) t.AppStatus).HasColumnName("AppStatus")
        Me.Property(Function(t) t.AppIsActive).HasColumnName("AppIsActive")
        Me.Property(Function(t) t.ClientCode).HasColumnName("ClientCode")
        Me.Property(Function(t) t.CmiBuying).HasColumnName("CmiBuying")
        Me.Property(Function(t) t.AppBeneficiary).HasColumnName("AppBeneficiary")
        Me.Property(Function(t) t.LeasingID).HasColumnName("LeasingID")
        Me.Property(Function(t) t.ClosingDate).HasColumnName("ClosingDate")
        Me.Property(Function(t) t.ClosingMonth).HasColumnName("ClosingMonth")
        Me.Property(Function(t) t.ClosingDay).HasColumnName("ClosingDay")
        Me.Property(Function(t) t.AppStatusRecived).HasColumnName("AppStatusRecived")
        Me.Property(Function(t) t.ProjectID).HasColumnName("ProjectID")
        Me.Property(Function(t) t.NotifyName).HasColumnName("NotifyName")
        Me.Property(Function(t) t.CusTitle).HasColumnName("CusTitle")
        Me.Property(Function(t) t.CarGroupID).HasColumnName("CarGroupID")
        Me.Property(Function(t) t.CarGroupName).HasColumnName("CarGroupName")
        Me.Property(Function(t) t.CarBrandName).HasColumnName("CarBrandName")
        Me.Property(Function(t) t.CarModelName).HasColumnName("CarModelName")
        Me.Property(Function(t) t.CarTypeName).HasColumnName("CarTypeName")
        Me.Property(Function(t) t.CarTypeNameFull).HasColumnName("CarTypeNameFull")
        Me.Property(Function(t) t.CarModelCode).HasColumnName("CarModelCode")
        Me.Property(Function(t) t.CarGroupNameM2).HasColumnName("CarGroupNameM2")
        Me.Property(Function(t) t.DealerNameEn).HasColumnName("DealerNameEn")
        Me.Property(Function(t) t.NLTDealerCode).HasColumnName("NLTDealerCode")
        Me.Property(Function(t) t.NLTArea).HasColumnName("NLTArea")
        Me.Property(Function(t) t.InsurerNameTh).HasColumnName("InsurerNameTh")
        Me.Property(Function(t) t.InsurerCodeSibis).HasColumnName("InsurerCodeSibis")
        Me.Property(Function(t) t.InsurerNameEn).HasColumnName("InsurerNameEn")
        Me.Property(Function(t) t.InsurerCode).HasColumnName("InsurerCode")
        Me.Property(Function(t) t.LeasID).HasColumnName("LeasID")
        Me.Property(Function(t) t.LeasName).HasColumnName("LeasName")
        Me.Property(Function(t) t.LeasNameAbbr).HasColumnName("LeasNameAbbr")
        Me.Property(Function(t) t.LeasNameShort).HasColumnName("LeasNameShort")
        Me.Property(Function(t) t.ModelNameM2).HasColumnName("ModelNameM2")
        'Me.Property(Function(t) t.NLTBookingDate).HasColumnName("NLTBookingDate")
        Me.Property(Function(t) t.SchemeCode).HasColumnName("SchemeCode")
        Me.Property(Function(t) t.VmiNetPremium).HasColumnName("VmiNetPremium")
        Me.Property(Function(t) t.VmiStamp).HasColumnName("VmiStamp")
        Me.Property(Function(t) t.VmiVat).HasColumnName("VmiVat")
        Me.Property(Function(t) t.VmiTotalPremium).HasColumnName("VmiTotalPremium")
        Me.Property(Function(t) t.CmiNetPremium).HasColumnName("CmiNetPremium")
        Me.Property(Function(t) t.CmiStamp).HasColumnName("CmiStamp")
        Me.Property(Function(t) t.CmiVat).HasColumnName("CmiVat")
        Me.Property(Function(t) t.CmiTotalPremium).HasColumnName("CmiTotalPremium")
        Me.Property(Function(t) t.ClassOfRiskVMI).HasColumnName("ClassOfRiskVMI")
        Me.Property(Function(t) t.ClassOfRiskCMI).HasColumnName("ClassOfRiskCMI")
        Me.Property(Function(t) t.ShowroomID).HasColumnName("ShowroomID")
        Me.Property(Function(t) t.ShowroomName).HasColumnName("ShowroomName")
        Me.Property(Function(t) t.DealerID).HasColumnName("DealerID")
        Me.Property(Function(t) t.ShorwoomNameEn).HasColumnName("ShorwoomNameEn")
        Me.Property(Function(t) t.CarTypeNameM2).HasColumnName("CarTypeNameM2")
        Me.Property(Function(t) t.TypeInsID).HasColumnName("TypeInsID")
        Me.Property(Function(t) t.TypeInsName).HasColumnName("TypeInsName")
        Me.Property(Function(t) t.TypeInsStatus).HasColumnName("TypeInsStatus")
        Me.Property(Function(t) t.TypeInsReportToNLTH).HasColumnName("TypeInsReportToNLTH")
        Me.Property(Function(t) t.TypeInsIsFree).HasColumnName("TypeInsIsFree")
        Me.Property(Function(t) t.UserAddedTel).HasColumnName("UserAddedTel")
        Me.Property(Function(t) t.UserAddedType).HasColumnName("UserAddedType")

    End Sub
End Class


'V_NLTH_All_Campaign_Daily_Excluded_Model
Partial Public Class V_NLTH_All_Campaign_Daily_Excluded_Model
    <Global.System.Data.Linq.Mapping.ColumnAttribute(DbType:="Int NOT NULL", IsPrimaryKey:=True)> _
    Public Property AppID As Integer
    Public Property TempID As String
    Public Property CusName As String
    Public Property CusSurname As String
    Public Property CusIDCard As String
    Public Property CusAddress1 As String
    Public Property CusAddress2 As String
    Public Property CusMobile As String
    Public Property CusHomePhone As String
    Public Property CusOfficePhone As System.Nullable(Of Integer)
    Public Property CarEngineNo As String
    Public Property CarChassisNo As String
    Public Property CarPlantNo As String
    Public Property DateEffective As System.Nullable(Of Date)
    Public Property DateExpired As System.Nullable(Of Date)
    Public Property DateNotification As System.Nullable(Of Date)
    Public Property AppStatus As String
    Public Property AppIsActive As System.Nullable(Of Integer)
    Public Property ClientCode As String
    Public Property CmiBuying As System.Nullable(Of Integer)
    Public Property AppBeneficiary As String
    Public Property LeasingID As System.Nullable(Of Integer)
    Public Property ClosingDate As System.Nullable(Of Date)
    Public Property ClosingMonth As System.Nullable(Of Date)
    Public Property ClosingDay As System.Nullable(Of Date)
    Public Property AppStatusRecived As System.Nullable(Of Boolean)
    Public Property ProjectID As System.Nullable(Of Integer)
    Public Property NotifyName As String
    Public Property CusTitle As String
    Public Property CarGroupID As System.Nullable(Of Integer)
    Public Property CarGroupName As String
    Public Property CarBrandName As String
    Public Property CarModelName As String
    Public Property CarTypeName As String
    Public Property CarTypeNameFull As String
    Public Property CarModelCode As String
    Public Property CarGroupNameM2 As String
    Public Property DealerNameEn As String
    Public Property NLTDealerCode As String
    Public Property NLTArea As String
    Public Property InsurerNameTh As String
    Public Property InsurerCodeSibis As String
    Public Property InsurerNameEn As String
    Public Property InsurerCode As String
    Public Property LeasID As System.Nullable(Of Integer)
    Public Property LeasName As String
    Public Property LeasNameAbbr As String
    Public Property LeasNameShort As String
    Public Property ModelNameM2 As String
    'Public Property NLTBookingDate As System.Nullable(Of Date)
    Public Property SchemeCode As String
    Public Property VmiNetPremium As System.Nullable(Of Decimal)
    Public Property VmiStamp As System.Nullable(Of Decimal)
    Public Property VmiVat As System.Nullable(Of Decimal)
    Public Property VmiTotalPremium As System.Nullable(Of Decimal)
    Public Property CmiNetPremium As System.Nullable(Of Decimal)
    Public Property CmiStamp As System.Nullable(Of Decimal)
    Public Property CmiVat As System.Nullable(Of Decimal)
    Public Property CmiTotalPremium As System.Nullable(Of Decimal)
    Public Property ClassOfRiskVMI As String
    Public Property ClassOfRiskCMI As String
    Public Property ShowroomID As System.Nullable(Of Integer)
    Public Property ShowroomName As String
    Public Property DealerID As System.Nullable(Of Integer)
    Public Property ShorwoomNameEn As String
    Public Property CarTypeNameM2 As String
    Public Property TypeInsID As System.Nullable(Of Integer)
    Public Property TypeInsName As String
    Public Property TypeInsStatus As System.Nullable(Of Integer)
    Public Property TypeInsReportToNLTH As System.Nullable(Of Boolean)
    Public Property TypeInsIsFree As System.Nullable(Of Boolean)
    Public Property UserAddedTel As String
    Public Property UserAddedType As String
End Class
Public Class V_NLTH_All_Campaign_Daily_Excluded_Model_Map
    Inherits EntityTypeConfiguration(Of V_NLTH_All_Campaign_Daily_Excluded_Model)
    Public Sub New(_table As String)
        ' Primary Key
        Me.HasKey(Function(t) t.AppID)

        ' Properties
        'Me.Property(Function(t) t.FirstName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.LastName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.Phone).IsRequired().HasMaxLength(20)


        ' Table & Column Mappings
        'Me.ToTable("V_NLTH_All_Campaign_Monthly")
        Me.ToTable(_table)
        Me.Property(Function(t) t.AppID).HasColumnName("AppID")
        Me.Property(Function(t) t.TempID).HasColumnName("TempID")
        Me.Property(Function(t) t.CusName).HasColumnName("CusName")
        Me.Property(Function(t) t.CusSurname).HasColumnName("CusSurname")
        Me.Property(Function(t) t.CusIDCard).HasColumnName("CusIDCard")
        Me.Property(Function(t) t.CusAddress1).HasColumnName("CusAddress1")
        Me.Property(Function(t) t.CusAddress2).HasColumnName("CusAddress2")
        Me.Property(Function(t) t.CusMobile).HasColumnName("CusMobile")
        Me.Property(Function(t) t.CusHomePhone).HasColumnName("CusHomePhone")
        Me.Property(Function(t) t.CusOfficePhone).HasColumnName("CusOfficePhone")
        Me.Property(Function(t) t.CarEngineNo).HasColumnName("CarEngineNo")
        Me.Property(Function(t) t.CarChassisNo).HasColumnName("CarChassisNo")
        Me.Property(Function(t) t.CarPlantNo).HasColumnName("CarPlantNo")
        Me.Property(Function(t) t.DateEffective).HasColumnName("DateEffective")
        Me.Property(Function(t) t.DateExpired).HasColumnName("DateExpired")
        Me.Property(Function(t) t.DateNotification).HasColumnName("DateNotification")
        Me.Property(Function(t) t.AppStatus).HasColumnName("AppStatus")
        Me.Property(Function(t) t.AppIsActive).HasColumnName("AppIsActive")
        Me.Property(Function(t) t.ClientCode).HasColumnName("ClientCode")
        Me.Property(Function(t) t.CmiBuying).HasColumnName("CmiBuying")
        Me.Property(Function(t) t.AppBeneficiary).HasColumnName("AppBeneficiary")
        Me.Property(Function(t) t.LeasingID).HasColumnName("LeasingID")
        Me.Property(Function(t) t.ClosingDate).HasColumnName("ClosingDate")
        Me.Property(Function(t) t.ClosingMonth).HasColumnName("ClosingMonth")
        Me.Property(Function(t) t.ClosingDay).HasColumnName("ClosingDay")
        Me.Property(Function(t) t.AppStatusRecived).HasColumnName("AppStatusRecived")
        Me.Property(Function(t) t.ProjectID).HasColumnName("ProjectID")
        Me.Property(Function(t) t.NotifyName).HasColumnName("NotifyName")
        Me.Property(Function(t) t.CusTitle).HasColumnName("CusTitle")
        Me.Property(Function(t) t.CarGroupID).HasColumnName("CarGroupID")
        Me.Property(Function(t) t.CarGroupName).HasColumnName("CarGroupName")
        Me.Property(Function(t) t.CarBrandName).HasColumnName("CarBrandName")
        Me.Property(Function(t) t.CarModelName).HasColumnName("CarModelName")
        Me.Property(Function(t) t.CarTypeName).HasColumnName("CarTypeName")
        Me.Property(Function(t) t.CarTypeNameFull).HasColumnName("CarTypeNameFull")
        Me.Property(Function(t) t.CarModelCode).HasColumnName("CarModelCode")
        Me.Property(Function(t) t.CarGroupNameM2).HasColumnName("CarGroupNameM2")
        Me.Property(Function(t) t.DealerNameEn).HasColumnName("DealerNameEn")
        Me.Property(Function(t) t.NLTDealerCode).HasColumnName("NLTDealerCode")
        Me.Property(Function(t) t.NLTArea).HasColumnName("NLTArea")
        Me.Property(Function(t) t.InsurerNameTh).HasColumnName("InsurerNameTh")
        Me.Property(Function(t) t.InsurerCodeSibis).HasColumnName("InsurerCodeSibis")
        Me.Property(Function(t) t.InsurerNameEn).HasColumnName("InsurerNameEn")
        Me.Property(Function(t) t.InsurerCode).HasColumnName("InsurerCode")
        Me.Property(Function(t) t.LeasID).HasColumnName("LeasID")
        Me.Property(Function(t) t.LeasName).HasColumnName("LeasName")
        Me.Property(Function(t) t.LeasNameAbbr).HasColumnName("LeasNameAbbr")
        Me.Property(Function(t) t.LeasNameShort).HasColumnName("LeasNameShort")
        Me.Property(Function(t) t.ModelNameM2).HasColumnName("ModelNameM2")
        'Me.Property(Function(t) t.NLTBookingDate).HasColumnName("NLTBookingDate")
        Me.Property(Function(t) t.SchemeCode).HasColumnName("SchemeCode")
        Me.Property(Function(t) t.VmiNetPremium).HasColumnName("VmiNetPremium")
        Me.Property(Function(t) t.VmiStamp).HasColumnName("VmiStamp")
        Me.Property(Function(t) t.VmiVat).HasColumnName("VmiVat")
        Me.Property(Function(t) t.VmiTotalPremium).HasColumnName("VmiTotalPremium")
        Me.Property(Function(t) t.CmiNetPremium).HasColumnName("CmiNetPremium")
        Me.Property(Function(t) t.CmiStamp).HasColumnName("CmiStamp")
        Me.Property(Function(t) t.CmiVat).HasColumnName("CmiVat")
        Me.Property(Function(t) t.CmiTotalPremium).HasColumnName("CmiTotalPremium")
        Me.Property(Function(t) t.ClassOfRiskVMI).HasColumnName("ClassOfRiskVMI")
        Me.Property(Function(t) t.ClassOfRiskCMI).HasColumnName("ClassOfRiskCMI")
        Me.Property(Function(t) t.ShowroomID).HasColumnName("ShowroomID")
        Me.Property(Function(t) t.ShowroomName).HasColumnName("ShowroomName")
        Me.Property(Function(t) t.DealerID).HasColumnName("DealerID")
        Me.Property(Function(t) t.ShorwoomNameEn).HasColumnName("ShorwoomNameEn")
        Me.Property(Function(t) t.CarTypeNameM2).HasColumnName("CarTypeNameM2")
        Me.Property(Function(t) t.TypeInsID).HasColumnName("TypeInsID")
        Me.Property(Function(t) t.TypeInsName).HasColumnName("TypeInsName")
        Me.Property(Function(t) t.TypeInsStatus).HasColumnName("TypeInsStatus")
        Me.Property(Function(t) t.TypeInsReportToNLTH).HasColumnName("TypeInsReportToNLTH")
        Me.Property(Function(t) t.TypeInsIsFree).HasColumnName("TypeInsIsFree")
        Me.Property(Function(t) t.UserAddedTel).HasColumnName("UserAddedTel")
        Me.Property(Function(t) t.UserAddedType).HasColumnName("UserAddedType")

    End Sub
End Class



'V_NLTH_GetTemp_By_NotificationDate
Partial Public Class V_NLTH_GetTemp_By_NotificationDate
    <Global.System.Data.Linq.Mapping.ColumnAttribute(DbType:="Int NOT NULL", IsPrimaryKey:=True)> _
    Public Property FID As Integer
    Public Property FDate As System.Nullable(Of Date)
    Public Property Business As String
    Public Property Name As String
    Public Property SurName As String
    Public Property InsurerCode As String
    Public Property StartingDate As System.Nullable(Of Date)
    Public Property ClientCode As String
    Public Property VIN As String
    Public Property ShowRoomName As String
    Public Property leasingID As System.Nullable(Of Integer)
    Public Property LeasingName As String
    Public Property InsurerNameThai As String
    Public Property NotifigationDate As System.Nullable(Of Date)
    Public Property Title As String
    Public Property GroupName As String
    Public Property ModelName As String
    Public Property TypeName As String
    Public Property Benefit As String
    Public Property TempID As String
    Public Property ClosingDate As System.Nullable(Of Date)
    Public Property NetVoluntaryPremium As System.Nullable(Of Decimal)
    Public Property StampVoluntary As System.Nullable(Of Decimal)
    Public Property VATVoluntary As System.Nullable(Of Decimal)
    Public Property VoluntaryPremium As System.Nullable(Of Decimal)
    Public Property SchemeCode As String
    Public Property ClassOfRiskVMI As String
    Public Property ClassOfRiskCMI As String
    Public Property CMIBuyStatus As System.Nullable(Of Integer)
    Public Property CompulsoryPremium As System.Nullable(Of Decimal)
    Public Property StampCompulsory As System.Nullable(Of Decimal)
    Public Property VATCompulsory As System.Nullable(Of Decimal)
    Public Property NetCompulsoryPremium As System.Nullable(Of Decimal)
    Public Property AID As System.Nullable(Of Integer)
    Public Property IsActive As System.Nullable(Of Integer)
    Public Property NotifyName As String
    Public Property Status As String
End Class
Public Class V_NLTH_GetTemp_By_NotificationDate_Map
    Inherits EntityTypeConfiguration(Of V_NLTH_GetTemp_By_NotificationDate)
    Public Sub New(_table As String)
        ' Primary Key
        Me.HasKey(Function(t) t.FID)

        Me.ToTable(_table)
        Me.Property(Function(t) t.FID).HasColumnName("FID")
        Me.Property(Function(t) t.FDate).HasColumnName("FDate")
        Me.Property(Function(t) t.Business).HasColumnName("Business")
        Me.Property(Function(t) t.Name).HasColumnName("Name")
        Me.Property(Function(t) t.SurName).HasColumnName("SurName")
        Me.Property(Function(t) t.InsurerCode).HasColumnName("InsurerCode")
        Me.Property(Function(t) t.StartingDate).HasColumnName("StartingDate")
        Me.Property(Function(t) t.ClientCode).HasColumnName("ClientCode")
        Me.Property(Function(t) t.VIN).HasColumnName("VIN")
        Me.Property(Function(t) t.ShowRoomName).HasColumnName("ShowRoomName")
        Me.Property(Function(t) t.leasingID).HasColumnName("leasingID")
        Me.Property(Function(t) t.LeasingName).HasColumnName("LeasingName")
        Me.Property(Function(t) t.InsurerNameThai).HasColumnName("InsurerNameThai")
        Me.Property(Function(t) t.NotifigationDate).HasColumnName("NotifigationDate")
        Me.Property(Function(t) t.Title).HasColumnName("Title")
        Me.Property(Function(t) t.GroupName).HasColumnName("GroupName")
        Me.Property(Function(t) t.ModelName).HasColumnName("ModelName")
        Me.Property(Function(t) t.TypeName).HasColumnName("TypeName")
        Me.Property(Function(t) t.Benefit).HasColumnName("Benefit")
        Me.Property(Function(t) t.TempID).HasColumnName("TempID")
        Me.Property(Function(t) t.ClosingDate).HasColumnName("ClosingDate")
        Me.Property(Function(t) t.NetVoluntaryPremium).HasColumnName("NetVoluntaryPremium")
        Me.Property(Function(t) t.StampVoluntary).HasColumnName("StampVoluntary")
        Me.Property(Function(t) t.VATVoluntary).HasColumnName("VATVoluntary")
        Me.Property(Function(t) t.VoluntaryPremium).HasColumnName("VoluntaryPremium")
        Me.Property(Function(t) t.SchemeCode).HasColumnName("SchemeCode")
        Me.Property(Function(t) t.ClassOfRiskVMI).HasColumnName("ClassOfRiskVMI")
        Me.Property(Function(t) t.ClassOfRiskCMI).HasColumnName("ClassOfRiskCMI")
        Me.Property(Function(t) t.CMIBuyStatus).HasColumnName("CMIBuyStatus")
        Me.Property(Function(t) t.CompulsoryPremium).HasColumnName("CompulsoryPremium")
        Me.Property(Function(t) t.StampCompulsory).HasColumnName("StampCompulsory")
        Me.Property(Function(t) t.VATCompulsory).HasColumnName("VATCompulsory")
        Me.Property(Function(t) t.NetCompulsoryPremium).HasColumnName("NetCompulsoryPremium")
        Me.Property(Function(t) t.AID).HasColumnName("AID")
        Me.Property(Function(t) t.IsActive).HasColumnName("IsActive")
        Me.Property(Function(t) t.NotifyName).HasColumnName("NotifyName")
        Me.Property(Function(t) t.Status).HasColumnName("Status")


    End Sub
End Class



'======================NMT===============================
'V_NMT_All_NPP_Campaign_Daily
Partial Public Class V_NMT_All_NPP_Campaign_Daily
    <Global.System.Data.Linq.Mapping.ColumnAttribute(DbType:="Int NOT NULL", IsPrimaryKey:=True)> _
    Public Property AppID As Integer
    Public Property TempID As String
    Public Property CusName As String
    Public Property CusSurname As String
    Public Property CusIDCard As String
    Public Property CusAddress1 As String
    Public Property CusAddress2 As String
    Public Property CusMobile As String
    Public Property CusHomePhone As String
    Public Property CusOfficePhone As System.Nullable(Of Integer)
    Public Property CarEngineNo As String
    Public Property CarChassisNo As String
    Public Property CarPlantNo As String
    Public Property DateEffective As System.Nullable(Of Date)
    Public Property DateExpired As System.Nullable(Of Date)
    Public Property DateNotification As System.Nullable(Of Date)
    Public Property AppStatus As String
    Public Property AppIsActive As System.Nullable(Of Integer)
    Public Property ClientCode As String
    Public Property CmiBuying As System.Nullable(Of Integer)
    Public Property AppBeneficiary As String
    Public Property LeasingID As System.Nullable(Of Integer)
    Public Property ClosingDate As System.Nullable(Of Date)
    Public Property ClosingMonth As System.Nullable(Of Date)
    Public Property ClosingDay As System.Nullable(Of Date)
    Public Property AppStatusRecived As System.Nullable(Of Boolean)
    Public Property ProjectID As System.Nullable(Of Integer)
    Public Property NotifyName As String
    Public Property CusTitle As String
    Public Property CarGroupID As System.Nullable(Of Integer)
    Public Property CarGroupName As String
    Public Property CarGroupName2 As String
    Public Property CarBrandName As String
    Public Property CarModelName As String
    Public Property CarTypeName As String
    Public Property CarTypeNameFull As String
    Public Property CarModelCode As String
    Public Property CarGroupNameM2 As String
    Public Property DealerNameEn As String
    Public Property NLTDealerCode As String
    Public Property NLTArea As String
    Public Property InsurerNameTh As String
    Public Property InsurerCodeSibis As String
    Public Property InsurerNameEn As String
    Public Property InsurerCode As String
    Public Property LeasID As System.Nullable(Of Integer)
    Public Property LeasName As String
    Public Property LeasNameAbbr As String
    Public Property LeasNameShort As String
    Public Property ModelNameM2 As String
    'Public Property NLTBookingDate As System.Nullable(Of Date)
    Public Property SchemeCode As String
    Public Property VmiNetPremium As System.Nullable(Of Decimal)
    Public Property VmiStamp As System.Nullable(Of Decimal)
    Public Property VmiVat As System.Nullable(Of Decimal)
    Public Property VmiTotalPremium As System.Nullable(Of Decimal)
    Public Property CmiNetPremium As System.Nullable(Of Decimal)
    Public Property CmiStamp As System.Nullable(Of Decimal)
    Public Property CmiVat As System.Nullable(Of Decimal)
    Public Property CmiTotalPremium As System.Nullable(Of Decimal)
    Public Property ClassOfRiskVMI As String
    Public Property ClassOfRiskCMI As String
    Public Property ShowroomID As System.Nullable(Of Integer)
    Public Property ShowroomName As String
    Public Property DealerID As System.Nullable(Of Integer)
    Public Property ShorwoomNameEn As String
    Public Property CarTypeNameM2 As String
    Public Property TypeInsID As System.Nullable(Of Integer)
    Public Property TypeInsName As String
    Public Property TypeInsStatus As System.Nullable(Of Integer)
    Public Property TypeInsReportToNLTH As System.Nullable(Of Boolean)
    Public Property TypeInsIsFree As System.Nullable(Of Boolean)
    Public Property UserAddedTel As String
    Public Property UserAddedType As String
End Class
Public Class V_NMT_All_NPP_Campaign_Daily_Map
    Inherits EntityTypeConfiguration(Of V_NMT_All_NPP_Campaign_Daily)
    Public Sub New(_table As String)
        ' Primary Key
        Me.HasKey(Function(t) t.AppID)

        ' Properties
        'Me.Property(Function(t) t.FirstName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.LastName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.Phone).IsRequired().HasMaxLength(20)


        ' Table & Column Mappings
        'Me.ToTable("V_NMT_All_NPP_Campaign_Daily")
        Me.ToTable(_table)
        Me.Property(Function(t) t.AppID).HasColumnName("AppID")
        Me.Property(Function(t) t.TempID).HasColumnName("TempID")
        Me.Property(Function(t) t.CusName).HasColumnName("CusName")
        Me.Property(Function(t) t.CusSurname).HasColumnName("CusSurname")
        Me.Property(Function(t) t.CusIDCard).HasColumnName("CusIDCard")
        Me.Property(Function(t) t.CusAddress1).HasColumnName("CusAddress1")
        Me.Property(Function(t) t.CusAddress2).HasColumnName("CusAddress2")
        Me.Property(Function(t) t.CusMobile).HasColumnName("CusMobile")
        Me.Property(Function(t) t.CusHomePhone).HasColumnName("CusHomePhone")
        Me.Property(Function(t) t.CusOfficePhone).HasColumnName("CusOfficePhone")
        Me.Property(Function(t) t.CarEngineNo).HasColumnName("CarEngineNo")
        Me.Property(Function(t) t.CarChassisNo).HasColumnName("CarChassisNo")
        Me.Property(Function(t) t.CarPlantNo).HasColumnName("CarPlantNo")
        Me.Property(Function(t) t.DateEffective).HasColumnName("DateEffective")
        Me.Property(Function(t) t.DateExpired).HasColumnName("DateExpired")
        Me.Property(Function(t) t.DateNotification).HasColumnName("DateNotification")
        Me.Property(Function(t) t.AppStatus).HasColumnName("AppStatus")
        Me.Property(Function(t) t.AppIsActive).HasColumnName("AppIsActive")
        Me.Property(Function(t) t.ClientCode).HasColumnName("ClientCode")
        Me.Property(Function(t) t.CmiBuying).HasColumnName("CmiBuying")
        Me.Property(Function(t) t.AppBeneficiary).HasColumnName("AppBeneficiary")
        Me.Property(Function(t) t.LeasingID).HasColumnName("LeasingID")
        Me.Property(Function(t) t.ClosingDate).HasColumnName("ClosingDate")
        Me.Property(Function(t) t.ClosingMonth).HasColumnName("ClosingMonth")
        Me.Property(Function(t) t.ClosingDay).HasColumnName("ClosingDay")
        Me.Property(Function(t) t.AppStatusRecived).HasColumnName("AppStatusRecived")
        Me.Property(Function(t) t.ProjectID).HasColumnName("ProjectID")
        Me.Property(Function(t) t.NotifyName).HasColumnName("NotifyName")
        Me.Property(Function(t) t.CusTitle).HasColumnName("CusTitle")
        Me.Property(Function(t) t.CarGroupID).HasColumnName("CarGroupID")
        Me.Property(Function(t) t.CarGroupName).HasColumnName("CarGroupName")
        Me.Property(Function(t) t.CarGroupName2).HasColumnName("CarGroupName2")
        Me.Property(Function(t) t.CarBrandName).HasColumnName("CarBrandName")
        Me.Property(Function(t) t.CarModelName).HasColumnName("CarModelName")
        Me.Property(Function(t) t.CarTypeName).HasColumnName("CarTypeName")
        Me.Property(Function(t) t.CarTypeNameFull).HasColumnName("CarTypeNameFull")
        Me.Property(Function(t) t.CarModelCode).HasColumnName("CarModelCode")
        Me.Property(Function(t) t.CarGroupNameM2).HasColumnName("CarGroupNameM2")
        Me.Property(Function(t) t.DealerNameEn).HasColumnName("DealerNameEn")
        Me.Property(Function(t) t.NLTDealerCode).HasColumnName("NLTDealerCode")
        Me.Property(Function(t) t.NLTArea).HasColumnName("NLTArea")
        Me.Property(Function(t) t.InsurerNameTh).HasColumnName("InsurerNameTh")
        Me.Property(Function(t) t.InsurerCodeSibis).HasColumnName("InsurerCodeSibis")
        Me.Property(Function(t) t.InsurerNameEn).HasColumnName("InsurerNameEn")
        Me.Property(Function(t) t.InsurerCode).HasColumnName("InsurerCode")
        Me.Property(Function(t) t.LeasID).HasColumnName("LeasID")
        Me.Property(Function(t) t.LeasName).HasColumnName("LeasName")
        Me.Property(Function(t) t.LeasNameAbbr).HasColumnName("LeasNameAbbr")
        Me.Property(Function(t) t.LeasNameShort).HasColumnName("LeasNameShort")
        Me.Property(Function(t) t.ModelNameM2).HasColumnName("ModelNameM2")
        'Me.Property(Function(t) t.NLTBookingDate).HasColumnName("NLTBookingDate")
        Me.Property(Function(t) t.SchemeCode).HasColumnName("SchemeCode")
        Me.Property(Function(t) t.VmiNetPremium).HasColumnName("VmiNetPremium")
        Me.Property(Function(t) t.VmiStamp).HasColumnName("VmiStamp")
        Me.Property(Function(t) t.VmiVat).HasColumnName("VmiVat")
        Me.Property(Function(t) t.VmiTotalPremium).HasColumnName("VmiTotalPremium")
        Me.Property(Function(t) t.CmiNetPremium).HasColumnName("CmiNetPremium")
        Me.Property(Function(t) t.CmiStamp).HasColumnName("CmiStamp")
        Me.Property(Function(t) t.CmiVat).HasColumnName("CmiVat")
        Me.Property(Function(t) t.CmiTotalPremium).HasColumnName("CmiTotalPremium")
        Me.Property(Function(t) t.ClassOfRiskVMI).HasColumnName("ClassOfRiskVMI")
        Me.Property(Function(t) t.ClassOfRiskCMI).HasColumnName("ClassOfRiskCMI")
        Me.Property(Function(t) t.ShowroomID).HasColumnName("ShowroomID")
        Me.Property(Function(t) t.ShowroomName).HasColumnName("ShowroomName")
        Me.Property(Function(t) t.DealerID).HasColumnName("DealerID")
        Me.Property(Function(t) t.ShorwoomNameEn).HasColumnName("ShorwoomNameEn")
        Me.Property(Function(t) t.CarTypeNameM2).HasColumnName("CarTypeNameM2")
        Me.Property(Function(t) t.TypeInsID).HasColumnName("TypeInsID")
        Me.Property(Function(t) t.TypeInsName).HasColumnName("TypeInsName")
        Me.Property(Function(t) t.TypeInsStatus).HasColumnName("TypeInsStatus")
        Me.Property(Function(t) t.TypeInsReportToNLTH).HasColumnName("TypeInsReportToNLTH")
        Me.Property(Function(t) t.TypeInsIsFree).HasColumnName("TypeInsIsFree")
        Me.Property(Function(t) t.UserAddedTel).HasColumnName("UserAddedTel")
        Me.Property(Function(t) t.UserAddedType).HasColumnName("UserAddedType")

    End Sub
End Class

'V_NMT_All_NPP_Campaign_Monthly
Partial Public Class V_NMT_All_NPP_Campaign_Monthly
    <Global.System.Data.Linq.Mapping.ColumnAttribute(DbType:="Int NOT NULL", IsPrimaryKey:=True)> _
    Public Property AppID As Integer
    Public Property TempID As String
    Public Property CusName As String
    Public Property CusSurname As String
    Public Property CusIDCard As String
    Public Property CusAddress1 As String
    Public Property CusAddress2 As String
    Public Property CusMobile As String
    Public Property CusHomePhone As String
    Public Property CusOfficePhone As System.Nullable(Of Integer)
    Public Property CarEngineNo As String
    Public Property CarChassisNo As String
    Public Property CarPlantNo As String
    Public Property DateEffective As System.Nullable(Of Date)
    Public Property DateExpired As System.Nullable(Of Date)
    Public Property DateNotification As System.Nullable(Of Date)
    Public Property AppStatus As String
    Public Property AppIsActive As System.Nullable(Of Integer)
    Public Property ClientCode As String
    Public Property CmiBuying As System.Nullable(Of Integer)
    Public Property AppBeneficiary As String
    Public Property LeasingID As System.Nullable(Of Integer)
    Public Property ClosingDate As System.Nullable(Of Date)
    Public Property ClosingMonth As System.Nullable(Of Date)
    Public Property ClosingDay As System.Nullable(Of Date)
    Public Property AppStatusRecived As System.Nullable(Of Boolean)
    Public Property ProjectID As System.Nullable(Of Integer)
    Public Property NotifyName As String
    Public Property CusTitle As String
    Public Property CarGroupID As System.Nullable(Of Integer)
    Public Property CarGroupName As String
    Public Property CarGroupName2 As String
    Public Property CarBrandName As String
    Public Property CarModelName As String
    Public Property CarTypeName As String
    Public Property CarTypeNameFull As String
    Public Property CarModelCode As String
    Public Property CarGroupNameM2 As String
    Public Property DealerNameEn As String
    Public Property NLTDealerCode As String
    Public Property NLTArea As String
    Public Property InsurerNameTh As String
    Public Property InsurerCodeSibis As String
    Public Property InsurerNameEn As String
    Public Property InsurerCode As String
    Public Property LeasID As System.Nullable(Of Integer)
    Public Property LeasName As String
    Public Property LeasNameAbbr As String
    Public Property LeasNameShort As String
    Public Property ModelNameM2 As String
    'Public Property NLTBookingDate As System.Nullable(Of Date)
    Public Property SchemeCode As String
    Public Property VmiNetPremium As System.Nullable(Of Decimal)
    Public Property VmiStamp As System.Nullable(Of Decimal)
    Public Property VmiVat As System.Nullable(Of Decimal)
    Public Property VmiTotalPremium As System.Nullable(Of Decimal)
    Public Property CmiNetPremium As System.Nullable(Of Decimal)
    Public Property CmiStamp As System.Nullable(Of Decimal)
    Public Property CmiVat As System.Nullable(Of Decimal)
    Public Property CmiTotalPremium As System.Nullable(Of Decimal)
    Public Property ClassOfRiskVMI As String
    Public Property ClassOfRiskCMI As String
    Public Property ShowroomID As System.Nullable(Of Integer)
    Public Property ShowroomName As String
    Public Property DealerID As System.Nullable(Of Integer)
    Public Property ShorwoomNameEn As String
    Public Property CarTypeNameM2 As String
    Public Property TypeInsID As System.Nullable(Of Integer)
    Public Property TypeInsName As String
    Public Property TypeInsStatus As System.Nullable(Of Integer)
    Public Property TypeInsReportToNLTH As System.Nullable(Of Boolean)
    Public Property TypeInsIsFree As System.Nullable(Of Boolean)
    Public Property UserAddedTel As String
    Public Property UserAddedType As String
End Class
Public Class V_NMT_All_NPP_Campaign_Monthly_Map
    Inherits EntityTypeConfiguration(Of V_NMT_All_NPP_Campaign_Monthly)
    Public Sub New(_table As String)
        ' Primary Key
        Me.HasKey(Function(t) t.AppID)

        ' Properties
        'Me.Property(Function(t) t.FirstName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.LastName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.Phone).IsRequired().HasMaxLength(20)


        ' Table & Column Mappings
        'Me.ToTable("V_NMT_All_NPP_Campaign_Monthly")
        Me.ToTable(_table)
        Me.Property(Function(t) t.AppID).HasColumnName("AppID")
        Me.Property(Function(t) t.TempID).HasColumnName("TempID")
        Me.Property(Function(t) t.CusName).HasColumnName("CusName")
        Me.Property(Function(t) t.CusSurname).HasColumnName("CusSurname")
        Me.Property(Function(t) t.CusIDCard).HasColumnName("CusIDCard")
        Me.Property(Function(t) t.CusAddress1).HasColumnName("CusAddress1")
        Me.Property(Function(t) t.CusAddress2).HasColumnName("CusAddress2")
        Me.Property(Function(t) t.CusMobile).HasColumnName("CusMobile")
        Me.Property(Function(t) t.CusHomePhone).HasColumnName("CusHomePhone")
        Me.Property(Function(t) t.CusOfficePhone).HasColumnName("CusOfficePhone")
        Me.Property(Function(t) t.CarEngineNo).HasColumnName("CarEngineNo")
        Me.Property(Function(t) t.CarChassisNo).HasColumnName("CarChassisNo")
        Me.Property(Function(t) t.CarPlantNo).HasColumnName("CarPlantNo")
        Me.Property(Function(t) t.DateEffective).HasColumnName("DateEffective")
        Me.Property(Function(t) t.DateExpired).HasColumnName("DateExpired")
        Me.Property(Function(t) t.DateNotification).HasColumnName("DateNotification")
        Me.Property(Function(t) t.AppStatus).HasColumnName("AppStatus")
        Me.Property(Function(t) t.AppIsActive).HasColumnName("AppIsActive")
        Me.Property(Function(t) t.ClientCode).HasColumnName("ClientCode")
        Me.Property(Function(t) t.CmiBuying).HasColumnName("CmiBuying")
        Me.Property(Function(t) t.AppBeneficiary).HasColumnName("AppBeneficiary")
        Me.Property(Function(t) t.LeasingID).HasColumnName("LeasingID")
        Me.Property(Function(t) t.ClosingDate).HasColumnName("ClosingDate")
        Me.Property(Function(t) t.ClosingMonth).HasColumnName("ClosingMonth")
        Me.Property(Function(t) t.ClosingDay).HasColumnName("ClosingDay")
        Me.Property(Function(t) t.AppStatusRecived).HasColumnName("AppStatusRecived")
        Me.Property(Function(t) t.ProjectID).HasColumnName("ProjectID")
        Me.Property(Function(t) t.NotifyName).HasColumnName("NotifyName")
        Me.Property(Function(t) t.CusTitle).HasColumnName("CusTitle")
        Me.Property(Function(t) t.CarGroupID).HasColumnName("CarGroupID")
        Me.Property(Function(t) t.CarGroupName).HasColumnName("CarGroupName")
        Me.Property(Function(t) t.CarGroupName2).HasColumnName("CarGroupName2")
        Me.Property(Function(t) t.CarBrandName).HasColumnName("CarBrandName")
        Me.Property(Function(t) t.CarModelName).HasColumnName("CarModelName")
        Me.Property(Function(t) t.CarTypeName).HasColumnName("CarTypeName")
        Me.Property(Function(t) t.CarTypeNameFull).HasColumnName("CarTypeNameFull")
        Me.Property(Function(t) t.CarModelCode).HasColumnName("CarModelCode")
        Me.Property(Function(t) t.CarGroupNameM2).HasColumnName("CarGroupNameM2")
        Me.Property(Function(t) t.DealerNameEn).HasColumnName("DealerNameEn")
        Me.Property(Function(t) t.NLTDealerCode).HasColumnName("NLTDealerCode")
        Me.Property(Function(t) t.NLTArea).HasColumnName("NLTArea")
        Me.Property(Function(t) t.InsurerNameTh).HasColumnName("InsurerNameTh")
        Me.Property(Function(t) t.InsurerCodeSibis).HasColumnName("InsurerCodeSibis")
        Me.Property(Function(t) t.InsurerNameEn).HasColumnName("InsurerNameEn")
        Me.Property(Function(t) t.InsurerCode).HasColumnName("InsurerCode")
        Me.Property(Function(t) t.LeasID).HasColumnName("LeasID")
        Me.Property(Function(t) t.LeasName).HasColumnName("LeasName")
        Me.Property(Function(t) t.LeasNameAbbr).HasColumnName("LeasNameAbbr")
        Me.Property(Function(t) t.LeasNameShort).HasColumnName("LeasNameShort")
        Me.Property(Function(t) t.ModelNameM2).HasColumnName("ModelNameM2")
        'Me.Property(Function(t) t.NLTBookingDate).HasColumnName("NLTBookingDate")
        Me.Property(Function(t) t.SchemeCode).HasColumnName("SchemeCode")
        Me.Property(Function(t) t.VmiNetPremium).HasColumnName("VmiNetPremium")
        Me.Property(Function(t) t.VmiStamp).HasColumnName("VmiStamp")
        Me.Property(Function(t) t.VmiVat).HasColumnName("VmiVat")
        Me.Property(Function(t) t.VmiTotalPremium).HasColumnName("VmiTotalPremium")
        Me.Property(Function(t) t.CmiNetPremium).HasColumnName("CmiNetPremium")
        Me.Property(Function(t) t.CmiStamp).HasColumnName("CmiStamp")
        Me.Property(Function(t) t.CmiVat).HasColumnName("CmiVat")
        Me.Property(Function(t) t.CmiTotalPremium).HasColumnName("CmiTotalPremium")
        Me.Property(Function(t) t.ClassOfRiskVMI).HasColumnName("ClassOfRiskVMI")
        Me.Property(Function(t) t.ClassOfRiskCMI).HasColumnName("ClassOfRiskCMI")
        Me.Property(Function(t) t.ShowroomID).HasColumnName("ShowroomID")
        Me.Property(Function(t) t.ShowroomName).HasColumnName("ShowroomName")
        Me.Property(Function(t) t.DealerID).HasColumnName("DealerID")
        Me.Property(Function(t) t.ShorwoomNameEn).HasColumnName("ShorwoomNameEn")
        Me.Property(Function(t) t.CarTypeNameM2).HasColumnName("CarTypeNameM2")
        Me.Property(Function(t) t.TypeInsID).HasColumnName("TypeInsID")
        Me.Property(Function(t) t.TypeInsName).HasColumnName("TypeInsName")
        Me.Property(Function(t) t.TypeInsStatus).HasColumnName("TypeInsStatus")
        Me.Property(Function(t) t.TypeInsReportToNLTH).HasColumnName("TypeInsReportToNLTH")
        Me.Property(Function(t) t.TypeInsIsFree).HasColumnName("TypeInsIsFree")
        Me.Property(Function(t) t.UserAddedTel).HasColumnName("UserAddedTel")
        Me.Property(Function(t) t.UserAddedType).HasColumnName("UserAddedType")

    End Sub
End Class

'V_NMT_Free_NPP_Campaign_Daily
Partial Public Class V_NMT_Free_NPP_Campaign_Daily
    <Global.System.Data.Linq.Mapping.ColumnAttribute(DbType:="Int NOT NULL", IsPrimaryKey:=True)> _
    Public Property AppID As Integer
    Public Property TempID As String
    Public Property CusName As String
    Public Property CusSurname As String
    Public Property CusIDCard As String
    Public Property CusAddress1 As String
    Public Property CusAddress2 As String
    Public Property CusMobile As String
    Public Property CusHomePhone As String
    Public Property CusOfficePhone As System.Nullable(Of Integer)
    Public Property CarEngineNo As String
    Public Property CarChassisNo As String
    Public Property CarPlantNo As String
    Public Property DateEffective As System.Nullable(Of Date)
    Public Property DateExpired As System.Nullable(Of Date)
    Public Property DateNotification As System.Nullable(Of Date)
    Public Property AppStatus As String
    Public Property AppIsActive As System.Nullable(Of Integer)
    Public Property ClientCode As String
    Public Property CmiBuying As System.Nullable(Of Integer)
    Public Property AppBeneficiary As String
    Public Property LeasingID As System.Nullable(Of Integer)
    Public Property ClosingDate As System.Nullable(Of Date)
    Public Property ClosingMonth As System.Nullable(Of Date)
    Public Property ClosingDay As System.Nullable(Of Date)
    Public Property AppStatusRecived As System.Nullable(Of Boolean)
    Public Property ProjectID As System.Nullable(Of Integer)
    Public Property NotifyName As String
    Public Property CusTitle As String
    Public Property CarGroupID As System.Nullable(Of Integer)
    Public Property CarGroupName As String
    Public Property CarGroupName2 As String
    Public Property CarBrandName As String
    Public Property CarModelName As String
    Public Property CarTypeName As String
    Public Property CarTypeNameFull As String
    Public Property CarModelCode As String
    Public Property CarGroupNameM2 As String
    Public Property DealerNameEn As String
    Public Property NLTDealerCode As String
    Public Property NLTArea As String
    Public Property InsurerNameTh As String
    Public Property InsurerCodeSibis As String
    Public Property InsurerNameEn As String
    Public Property InsurerCode As String
    Public Property LeasID As System.Nullable(Of Integer)
    Public Property LeasName As String
    Public Property LeasNameAbbr As String
    Public Property LeasNameShort As String
    Public Property ModelNameM2 As String
    'Public Property NLTBookingDate As System.Nullable(Of Date)
    Public Property SchemeCode As String
    Public Property VmiNetPremium As System.Nullable(Of Decimal)
    Public Property VmiStamp As System.Nullable(Of Decimal)
    Public Property VmiVat As System.Nullable(Of Decimal)
    Public Property VmiTotalPremium As System.Nullable(Of Decimal)
    Public Property CmiNetPremium As System.Nullable(Of Decimal)
    Public Property CmiStamp As System.Nullable(Of Decimal)
    Public Property CmiVat As System.Nullable(Of Decimal)
    Public Property CmiTotalPremium As System.Nullable(Of Decimal)
    Public Property ClassOfRiskVMI As String
    Public Property ClassOfRiskCMI As String
    Public Property ShowroomID As System.Nullable(Of Integer)
    Public Property ShowroomName As String
    Public Property DealerID As System.Nullable(Of Integer)
    Public Property ShorwoomNameEn As String
    Public Property CarTypeNameM2 As String
    Public Property TypeInsID As System.Nullable(Of Integer)
    Public Property TypeInsName As String
    Public Property TypeInsStatus As System.Nullable(Of Integer)
    Public Property TypeInsReportToNLTH As System.Nullable(Of Boolean)
    Public Property TypeInsIsFree As System.Nullable(Of Boolean)
    Public Property UserAddedTel As String
    Public Property UserAddedType As String
End Class
Public Class V_NMT_Free_NPP_Campaign_Daily_Map
    Inherits EntityTypeConfiguration(Of V_NMT_Free_NPP_Campaign_Daily)
    Public Sub New(_table As String)
        ' Primary Key
        Me.HasKey(Function(t) t.AppID)

        ' Properties
        'Me.Property(Function(t) t.FirstName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.LastName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.Phone).IsRequired().HasMaxLength(20)


        ' Table & Column Mappings
        'Me.ToTable("V_NMT_Free_NPP_Campaign_Daily")
        Me.ToTable(_table)
        Me.Property(Function(t) t.AppID).HasColumnName("AppID")
        Me.Property(Function(t) t.TempID).HasColumnName("TempID")
        Me.Property(Function(t) t.CusName).HasColumnName("CusName")
        Me.Property(Function(t) t.CusSurname).HasColumnName("CusSurname")
        Me.Property(Function(t) t.CusIDCard).HasColumnName("CusIDCard")
        Me.Property(Function(t) t.CusAddress1).HasColumnName("CusAddress1")
        Me.Property(Function(t) t.CusAddress2).HasColumnName("CusAddress2")
        Me.Property(Function(t) t.CusMobile).HasColumnName("CusMobile")
        Me.Property(Function(t) t.CusHomePhone).HasColumnName("CusHomePhone")
        Me.Property(Function(t) t.CusOfficePhone).HasColumnName("CusOfficePhone")
        Me.Property(Function(t) t.CarEngineNo).HasColumnName("CarEngineNo")
        Me.Property(Function(t) t.CarChassisNo).HasColumnName("CarChassisNo")
        Me.Property(Function(t) t.CarPlantNo).HasColumnName("CarPlantNo")
        Me.Property(Function(t) t.DateEffective).HasColumnName("DateEffective")
        Me.Property(Function(t) t.DateExpired).HasColumnName("DateExpired")
        Me.Property(Function(t) t.DateNotification).HasColumnName("DateNotification")
        Me.Property(Function(t) t.AppStatus).HasColumnName("AppStatus")
        Me.Property(Function(t) t.AppIsActive).HasColumnName("AppIsActive")
        Me.Property(Function(t) t.ClientCode).HasColumnName("ClientCode")
        Me.Property(Function(t) t.CmiBuying).HasColumnName("CmiBuying")
        Me.Property(Function(t) t.AppBeneficiary).HasColumnName("AppBeneficiary")
        Me.Property(Function(t) t.LeasingID).HasColumnName("LeasingID")
        Me.Property(Function(t) t.ClosingDate).HasColumnName("ClosingDate")
        Me.Property(Function(t) t.ClosingMonth).HasColumnName("ClosingMonth")
        Me.Property(Function(t) t.ClosingDay).HasColumnName("ClosingDay")
        Me.Property(Function(t) t.AppStatusRecived).HasColumnName("AppStatusRecived")
        Me.Property(Function(t) t.ProjectID).HasColumnName("ProjectID")
        Me.Property(Function(t) t.NotifyName).HasColumnName("NotifyName")
        Me.Property(Function(t) t.CusTitle).HasColumnName("CusTitle")
        Me.Property(Function(t) t.CarGroupID).HasColumnName("CarGroupID")
        Me.Property(Function(t) t.CarGroupName).HasColumnName("CarGroupName")
        Me.Property(Function(t) t.CarGroupName2).HasColumnName("CarGroupName2")
        Me.Property(Function(t) t.CarBrandName).HasColumnName("CarBrandName")
        Me.Property(Function(t) t.CarModelName).HasColumnName("CarModelName")
        Me.Property(Function(t) t.CarTypeName).HasColumnName("CarTypeName")
        Me.Property(Function(t) t.CarTypeNameFull).HasColumnName("CarTypeNameFull")
        Me.Property(Function(t) t.CarModelCode).HasColumnName("CarModelCode")
        Me.Property(Function(t) t.CarGroupNameM2).HasColumnName("CarGroupNameM2")
        Me.Property(Function(t) t.DealerNameEn).HasColumnName("DealerNameEn")
        Me.Property(Function(t) t.NLTDealerCode).HasColumnName("NLTDealerCode")
        Me.Property(Function(t) t.NLTArea).HasColumnName("NLTArea")
        Me.Property(Function(t) t.InsurerNameTh).HasColumnName("InsurerNameTh")
        Me.Property(Function(t) t.InsurerCodeSibis).HasColumnName("InsurerCodeSibis")
        Me.Property(Function(t) t.InsurerNameEn).HasColumnName("InsurerNameEn")
        Me.Property(Function(t) t.InsurerCode).HasColumnName("InsurerCode")
        Me.Property(Function(t) t.LeasID).HasColumnName("LeasID")
        Me.Property(Function(t) t.LeasName).HasColumnName("LeasName")
        Me.Property(Function(t) t.LeasNameAbbr).HasColumnName("LeasNameAbbr")
        Me.Property(Function(t) t.LeasNameShort).HasColumnName("LeasNameShort")
        Me.Property(Function(t) t.ModelNameM2).HasColumnName("ModelNameM2")
        'Me.Property(Function(t) t.NLTBookingDate).HasColumnName("NLTBookingDate")
        Me.Property(Function(t) t.SchemeCode).HasColumnName("SchemeCode")
        Me.Property(Function(t) t.VmiNetPremium).HasColumnName("VmiNetPremium")
        Me.Property(Function(t) t.VmiStamp).HasColumnName("VmiStamp")
        Me.Property(Function(t) t.VmiVat).HasColumnName("VmiVat")
        Me.Property(Function(t) t.VmiTotalPremium).HasColumnName("VmiTotalPremium")
        Me.Property(Function(t) t.CmiNetPremium).HasColumnName("CmiNetPremium")
        Me.Property(Function(t) t.CmiStamp).HasColumnName("CmiStamp")
        Me.Property(Function(t) t.CmiVat).HasColumnName("CmiVat")
        Me.Property(Function(t) t.CmiTotalPremium).HasColumnName("CmiTotalPremium")
        Me.Property(Function(t) t.ClassOfRiskVMI).HasColumnName("ClassOfRiskVMI")
        Me.Property(Function(t) t.ClassOfRiskCMI).HasColumnName("ClassOfRiskCMI")
        Me.Property(Function(t) t.ShowroomID).HasColumnName("ShowroomID")
        Me.Property(Function(t) t.ShowroomName).HasColumnName("ShowroomName")
        Me.Property(Function(t) t.DealerID).HasColumnName("DealerID")
        Me.Property(Function(t) t.ShorwoomNameEn).HasColumnName("ShorwoomNameEn")
        Me.Property(Function(t) t.CarTypeNameM2).HasColumnName("CarTypeNameM2")
        Me.Property(Function(t) t.TypeInsID).HasColumnName("TypeInsID")
        Me.Property(Function(t) t.TypeInsName).HasColumnName("TypeInsName")
        Me.Property(Function(t) t.TypeInsStatus).HasColumnName("TypeInsStatus")
        Me.Property(Function(t) t.TypeInsReportToNLTH).HasColumnName("TypeInsReportToNLTH")
        Me.Property(Function(t) t.TypeInsIsFree).HasColumnName("TypeInsIsFree")
        Me.Property(Function(t) t.UserAddedTel).HasColumnName("UserAddedTel")
        Me.Property(Function(t) t.UserAddedType).HasColumnName("UserAddedType")

    End Sub
End Class

'V_NMT_Free_NPP_Campaign_Monthly
Partial Public Class V_NMT_Free_NPP_Campaign_Monthly
    <Global.System.Data.Linq.Mapping.ColumnAttribute(DbType:="Int NOT NULL", IsPrimaryKey:=True)> _
    Public Property AppID As Integer
    Public Property TempID As String
    Public Property CusName As String
    Public Property CusSurname As String
    Public Property CusIDCard As String
    Public Property CusAddress1 As String
    Public Property CusAddress2 As String
    Public Property CusMobile As String
    Public Property CusHomePhone As String
    Public Property CusOfficePhone As System.Nullable(Of Integer)
    Public Property CarEngineNo As String
    Public Property CarChassisNo As String
    Public Property CarPlantNo As String
    Public Property DateEffective As System.Nullable(Of Date)
    Public Property DateExpired As System.Nullable(Of Date)
    Public Property DateNotification As System.Nullable(Of Date)
    Public Property AppStatus As String
    Public Property AppIsActive As System.Nullable(Of Integer)
    Public Property ClientCode As String
    Public Property CmiBuying As System.Nullable(Of Integer)
    Public Property AppBeneficiary As String
    Public Property LeasingID As System.Nullable(Of Integer)
    Public Property ClosingDate As System.Nullable(Of Date)
    Public Property ClosingMonth As System.Nullable(Of Date)
    Public Property ClosingDay As System.Nullable(Of Date)
    Public Property AppStatusRecived As System.Nullable(Of Boolean)
    Public Property ProjectID As System.Nullable(Of Integer)
    Public Property NotifyName As String
    Public Property CusTitle As String
    Public Property CarGroupID As System.Nullable(Of Integer)
    Public Property CarGroupName As String
    Public Property CarGroupName2 As String
    Public Property CarBrandName As String
    Public Property CarModelName As String
    Public Property CarTypeName As String
    Public Property CarTypeNameFull As String
    Public Property CarModelCode As String
    Public Property CarGroupNameM2 As String
    Public Property DealerNameEn As String
    Public Property NLTDealerCode As String
    Public Property NLTArea As String
    Public Property InsurerNameTh As String
    Public Property InsurerCodeSibis As String
    Public Property InsurerNameEn As String
    Public Property InsurerCode As String
    Public Property LeasID As System.Nullable(Of Integer)
    Public Property LeasName As String
    Public Property LeasNameAbbr As String
    Public Property LeasNameShort As String
    Public Property ModelNameM2 As String
    'Public Property NLTBookingDate As System.Nullable(Of Date)
    Public Property SchemeCode As String
    Public Property VmiNetPremium As System.Nullable(Of Decimal)
    Public Property VmiStamp As System.Nullable(Of Decimal)
    Public Property VmiVat As System.Nullable(Of Decimal)
    Public Property VmiTotalPremium As System.Nullable(Of Decimal)
    Public Property CmiNetPremium As System.Nullable(Of Decimal)
    Public Property CmiStamp As System.Nullable(Of Decimal)
    Public Property CmiVat As System.Nullable(Of Decimal)
    Public Property CmiTotalPremium As System.Nullable(Of Decimal)
    Public Property ClassOfRiskVMI As String
    Public Property ClassOfRiskCMI As String
    Public Property ShowroomID As System.Nullable(Of Integer)
    Public Property ShowroomName As String
    Public Property DealerID As System.Nullable(Of Integer)
    Public Property ShorwoomNameEn As String
    Public Property CarTypeNameM2 As String
    Public Property TypeInsID As System.Nullable(Of Integer)
    Public Property TypeInsName As String
    Public Property TypeInsStatus As System.Nullable(Of Integer)
    Public Property TypeInsReportToNLTH As System.Nullable(Of Boolean)
    Public Property TypeInsIsFree As System.Nullable(Of Boolean)
    Public Property UserAddedTel As String
    Public Property UserAddedType As String
End Class
Public Class V_NMT_Free_NPP_Campaign_Monthly_Map
    Inherits EntityTypeConfiguration(Of V_NMT_Free_NPP_Campaign_Monthly)
    Public Sub New(_table As String)
        ' Primary Key
        Me.HasKey(Function(t) t.AppID)

        ' Properties
        'Me.Property(Function(t) t.FirstName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.LastName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.Phone).IsRequired().HasMaxLength(20)


        ' Table & Column Mappings
        'Me.ToTable("V_NMT_Free_NPP_Campaign_Monthly")
        Me.ToTable(_table)
        Me.Property(Function(t) t.AppID).HasColumnName("AppID")
        Me.Property(Function(t) t.TempID).HasColumnName("TempID")
        Me.Property(Function(t) t.CusName).HasColumnName("CusName")
        Me.Property(Function(t) t.CusSurname).HasColumnName("CusSurname")
        Me.Property(Function(t) t.CusIDCard).HasColumnName("CusIDCard")
        Me.Property(Function(t) t.CusAddress1).HasColumnName("CusAddress1")
        Me.Property(Function(t) t.CusAddress2).HasColumnName("CusAddress2")
        Me.Property(Function(t) t.CusMobile).HasColumnName("CusMobile")
        Me.Property(Function(t) t.CusHomePhone).HasColumnName("CusHomePhone")
        Me.Property(Function(t) t.CusOfficePhone).HasColumnName("CusOfficePhone")
        Me.Property(Function(t) t.CarEngineNo).HasColumnName("CarEngineNo")
        Me.Property(Function(t) t.CarChassisNo).HasColumnName("CarChassisNo")
        Me.Property(Function(t) t.CarPlantNo).HasColumnName("CarPlantNo")
        Me.Property(Function(t) t.DateEffective).HasColumnName("DateEffective")
        Me.Property(Function(t) t.DateExpired).HasColumnName("DateExpired")
        Me.Property(Function(t) t.DateNotification).HasColumnName("DateNotification")
        Me.Property(Function(t) t.AppStatus).HasColumnName("AppStatus")
        Me.Property(Function(t) t.AppIsActive).HasColumnName("AppIsActive")
        Me.Property(Function(t) t.ClientCode).HasColumnName("ClientCode")
        Me.Property(Function(t) t.CmiBuying).HasColumnName("CmiBuying")
        Me.Property(Function(t) t.AppBeneficiary).HasColumnName("AppBeneficiary")
        Me.Property(Function(t) t.LeasingID).HasColumnName("LeasingID")
        Me.Property(Function(t) t.ClosingDate).HasColumnName("ClosingDate")
        Me.Property(Function(t) t.ClosingMonth).HasColumnName("ClosingMonth")
        Me.Property(Function(t) t.ClosingDay).HasColumnName("ClosingDay")
        Me.Property(Function(t) t.AppStatusRecived).HasColumnName("AppStatusRecived")
        Me.Property(Function(t) t.ProjectID).HasColumnName("ProjectID")
        Me.Property(Function(t) t.NotifyName).HasColumnName("NotifyName")
        Me.Property(Function(t) t.CusTitle).HasColumnName("CusTitle")
        Me.Property(Function(t) t.CarGroupID).HasColumnName("CarGroupID")
        Me.Property(Function(t) t.CarGroupName).HasColumnName("CarGroupName")
        Me.Property(Function(t) t.CarGroupName2).HasColumnName("CarGroupName2")
        Me.Property(Function(t) t.CarBrandName).HasColumnName("CarBrandName")
        Me.Property(Function(t) t.CarModelName).HasColumnName("CarModelName")
        Me.Property(Function(t) t.CarTypeName).HasColumnName("CarTypeName")
        Me.Property(Function(t) t.CarTypeNameFull).HasColumnName("CarTypeNameFull")
        Me.Property(Function(t) t.CarModelCode).HasColumnName("CarModelCode")
        Me.Property(Function(t) t.CarGroupNameM2).HasColumnName("CarGroupNameM2")
        Me.Property(Function(t) t.DealerNameEn).HasColumnName("DealerNameEn")
        Me.Property(Function(t) t.NLTDealerCode).HasColumnName("NLTDealerCode")
        Me.Property(Function(t) t.NLTArea).HasColumnName("NLTArea")
        Me.Property(Function(t) t.InsurerNameTh).HasColumnName("InsurerNameTh")
        Me.Property(Function(t) t.InsurerCodeSibis).HasColumnName("InsurerCodeSibis")
        Me.Property(Function(t) t.InsurerNameEn).HasColumnName("InsurerNameEn")
        Me.Property(Function(t) t.InsurerCode).HasColumnName("InsurerCode")
        Me.Property(Function(t) t.LeasID).HasColumnName("LeasID")
        Me.Property(Function(t) t.LeasName).HasColumnName("LeasName")
        Me.Property(Function(t) t.LeasNameAbbr).HasColumnName("LeasNameAbbr")
        Me.Property(Function(t) t.LeasNameShort).HasColumnName("LeasNameShort")
        Me.Property(Function(t) t.ModelNameM2).HasColumnName("ModelNameM2")
        'Me.Property(Function(t) t.NLTBookingDate).HasColumnName("NLTBookingDate")
        Me.Property(Function(t) t.SchemeCode).HasColumnName("SchemeCode")
        Me.Property(Function(t) t.VmiNetPremium).HasColumnName("VmiNetPremium")
        Me.Property(Function(t) t.VmiStamp).HasColumnName("VmiStamp")
        Me.Property(Function(t) t.VmiVat).HasColumnName("VmiVat")
        Me.Property(Function(t) t.VmiTotalPremium).HasColumnName("VmiTotalPremium")
        Me.Property(Function(t) t.CmiNetPremium).HasColumnName("CmiNetPremium")
        Me.Property(Function(t) t.CmiStamp).HasColumnName("CmiStamp")
        Me.Property(Function(t) t.CmiVat).HasColumnName("CmiVat")
        Me.Property(Function(t) t.CmiTotalPremium).HasColumnName("CmiTotalPremium")
        Me.Property(Function(t) t.ClassOfRiskVMI).HasColumnName("ClassOfRiskVMI")
        Me.Property(Function(t) t.ClassOfRiskCMI).HasColumnName("ClassOfRiskCMI")
        Me.Property(Function(t) t.ShowroomID).HasColumnName("ShowroomID")
        Me.Property(Function(t) t.ShowroomName).HasColumnName("ShowroomName")
        Me.Property(Function(t) t.DealerID).HasColumnName("DealerID")
        Me.Property(Function(t) t.ShorwoomNameEn).HasColumnName("ShorwoomNameEn")
        Me.Property(Function(t) t.CarTypeNameM2).HasColumnName("CarTypeNameM2")
        Me.Property(Function(t) t.TypeInsID).HasColumnName("TypeInsID")
        Me.Property(Function(t) t.TypeInsName).HasColumnName("TypeInsName")
        Me.Property(Function(t) t.TypeInsStatus).HasColumnName("TypeInsStatus")
        Me.Property(Function(t) t.TypeInsReportToNLTH).HasColumnName("TypeInsReportToNLTH")
        Me.Property(Function(t) t.TypeInsIsFree).HasColumnName("TypeInsIsFree")
        Me.Property(Function(t) t.UserAddedTel).HasColumnName("UserAddedTel")
        Me.Property(Function(t) t.UserAddedType).HasColumnName("UserAddedType")

    End Sub
End Class

'V_NMT_nonFree_NPP_Campaign_Daily
Partial Public Class V_NMT_nonFree_NPP_Campaign_Daily
    <Global.System.Data.Linq.Mapping.ColumnAttribute(DbType:="Int NOT NULL", IsPrimaryKey:=True)> _
    Public Property AppID As Integer
    Public Property TempID As String
    Public Property CusName As String
    Public Property CusSurname As String
    Public Property CusIDCard As String
    Public Property CusAddress1 As String
    Public Property CusAddress2 As String
    Public Property CusMobile As String
    Public Property CusHomePhone As String
    Public Property CusOfficePhone As System.Nullable(Of Integer)
    Public Property CarEngineNo As String
    Public Property CarChassisNo As String
    Public Property CarPlantNo As String
    Public Property DateEffective As System.Nullable(Of Date)
    Public Property DateExpired As System.Nullable(Of Date)
    Public Property DateNotification As System.Nullable(Of Date)
    Public Property AppStatus As String
    Public Property AppIsActive As System.Nullable(Of Integer)
    Public Property ClientCode As String
    Public Property CmiBuying As System.Nullable(Of Integer)
    Public Property AppBeneficiary As String
    Public Property LeasingID As System.Nullable(Of Integer)
    Public Property ClosingDate As System.Nullable(Of Date)
    Public Property ClosingMonth As System.Nullable(Of Date)
    Public Property ClosingDay As System.Nullable(Of Date)
    Public Property AppStatusRecived As System.Nullable(Of Boolean)
    Public Property ProjectID As System.Nullable(Of Integer)
    Public Property NotifyName As String
    Public Property CusTitle As String
    Public Property CarGroupID As System.Nullable(Of Integer)
    Public Property CarGroupName As String
    Public Property CarGroupName2 As String
    Public Property CarBrandName As String
    Public Property CarModelName As String
    Public Property CarTypeName As String
    Public Property CarTypeNameFull As String
    Public Property CarModelCode As String
    Public Property CarGroupNameM2 As String
    Public Property DealerNameEn As String
    Public Property NLTDealerCode As String
    Public Property NLTArea As String
    Public Property InsurerNameTh As String
    Public Property InsurerCodeSibis As String
    Public Property InsurerNameEn As String
    Public Property InsurerCode As String
    Public Property LeasID As System.Nullable(Of Integer)
    Public Property LeasName As String
    Public Property LeasNameAbbr As String
    Public Property LeasNameShort As String
    Public Property ModelNameM2 As String
    'Public Property NLTBookingDate As System.Nullable(Of Date)
    Public Property SchemeCode As String
    Public Property VmiNetPremium As System.Nullable(Of Decimal)
    Public Property VmiStamp As System.Nullable(Of Decimal)
    Public Property VmiVat As System.Nullable(Of Decimal)
    Public Property VmiTotalPremium As System.Nullable(Of Decimal)
    Public Property CmiNetPremium As System.Nullable(Of Decimal)
    Public Property CmiStamp As System.Nullable(Of Decimal)
    Public Property CmiVat As System.Nullable(Of Decimal)
    Public Property CmiTotalPremium As System.Nullable(Of Decimal)
    Public Property ClassOfRiskVMI As String
    Public Property ClassOfRiskCMI As String
    Public Property ShowroomID As System.Nullable(Of Integer)
    Public Property ShowroomName As String
    Public Property DealerID As System.Nullable(Of Integer)
    Public Property ShorwoomNameEn As String
    Public Property CarTypeNameM2 As String
    Public Property TypeInsID As System.Nullable(Of Integer)
    Public Property TypeInsName As String
    Public Property TypeInsStatus As System.Nullable(Of Integer)
    Public Property TypeInsReportToNLTH As System.Nullable(Of Boolean)
    Public Property TypeInsIsFree As System.Nullable(Of Boolean)
    Public Property UserAddedTel As String
    Public Property UserAddedType As String
End Class
Public Class V_NMT_nonFree_NPP_Campaign_Daily_Map
    Inherits EntityTypeConfiguration(Of V_NMT_nonFree_NPP_Campaign_Daily)
    Public Sub New(_table As String)
        ' Primary Key
        Me.HasKey(Function(t) t.AppID)

        ' Properties
        'Me.Property(Function(t) t.FirstName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.LastName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.Phone).IsRequired().HasMaxLength(20)


        ' Table & Column Mappings
        'Me.ToTable("V_NMT_nonFree_NPP_Campaign_Daily")
        Me.ToTable(_table)
        Me.Property(Function(t) t.AppID).HasColumnName("AppID")
        Me.Property(Function(t) t.TempID).HasColumnName("TempID")
        Me.Property(Function(t) t.CusName).HasColumnName("CusName")
        Me.Property(Function(t) t.CusSurname).HasColumnName("CusSurname")
        Me.Property(Function(t) t.CusIDCard).HasColumnName("CusIDCard")
        Me.Property(Function(t) t.CusAddress1).HasColumnName("CusAddress1")
        Me.Property(Function(t) t.CusAddress2).HasColumnName("CusAddress2")
        Me.Property(Function(t) t.CusMobile).HasColumnName("CusMobile")
        Me.Property(Function(t) t.CusHomePhone).HasColumnName("CusHomePhone")
        Me.Property(Function(t) t.CusOfficePhone).HasColumnName("CusOfficePhone")
        Me.Property(Function(t) t.CarEngineNo).HasColumnName("CarEngineNo")
        Me.Property(Function(t) t.CarChassisNo).HasColumnName("CarChassisNo")
        Me.Property(Function(t) t.CarPlantNo).HasColumnName("CarPlantNo")
        Me.Property(Function(t) t.DateEffective).HasColumnName("DateEffective")
        Me.Property(Function(t) t.DateExpired).HasColumnName("DateExpired")
        Me.Property(Function(t) t.DateNotification).HasColumnName("DateNotification")
        Me.Property(Function(t) t.AppStatus).HasColumnName("AppStatus")
        Me.Property(Function(t) t.AppIsActive).HasColumnName("AppIsActive")
        Me.Property(Function(t) t.ClientCode).HasColumnName("ClientCode")
        Me.Property(Function(t) t.CmiBuying).HasColumnName("CmiBuying")
        Me.Property(Function(t) t.AppBeneficiary).HasColumnName("AppBeneficiary")
        Me.Property(Function(t) t.LeasingID).HasColumnName("LeasingID")
        Me.Property(Function(t) t.ClosingDate).HasColumnName("ClosingDate")
        Me.Property(Function(t) t.ClosingMonth).HasColumnName("ClosingMonth")
        Me.Property(Function(t) t.ClosingDay).HasColumnName("ClosingDay")
        Me.Property(Function(t) t.AppStatusRecived).HasColumnName("AppStatusRecived")
        Me.Property(Function(t) t.ProjectID).HasColumnName("ProjectID")
        Me.Property(Function(t) t.NotifyName).HasColumnName("NotifyName")
        Me.Property(Function(t) t.CusTitle).HasColumnName("CusTitle")
        Me.Property(Function(t) t.CarGroupID).HasColumnName("CarGroupID")
        Me.Property(Function(t) t.CarGroupName).HasColumnName("CarGroupName")
        Me.Property(Function(t) t.CarGroupName2).HasColumnName("CarGroupName2")
        Me.Property(Function(t) t.CarBrandName).HasColumnName("CarBrandName")
        Me.Property(Function(t) t.CarModelName).HasColumnName("CarModelName")
        Me.Property(Function(t) t.CarTypeName).HasColumnName("CarTypeName")
        Me.Property(Function(t) t.CarTypeNameFull).HasColumnName("CarTypeNameFull")
        Me.Property(Function(t) t.CarModelCode).HasColumnName("CarModelCode")
        Me.Property(Function(t) t.CarGroupNameM2).HasColumnName("CarGroupNameM2")
        Me.Property(Function(t) t.DealerNameEn).HasColumnName("DealerNameEn")
        Me.Property(Function(t) t.NLTDealerCode).HasColumnName("NLTDealerCode")
        Me.Property(Function(t) t.NLTArea).HasColumnName("NLTArea")
        Me.Property(Function(t) t.InsurerNameTh).HasColumnName("InsurerNameTh")
        Me.Property(Function(t) t.InsurerCodeSibis).HasColumnName("InsurerCodeSibis")
        Me.Property(Function(t) t.InsurerNameEn).HasColumnName("InsurerNameEn")
        Me.Property(Function(t) t.InsurerCode).HasColumnName("InsurerCode")
        Me.Property(Function(t) t.LeasID).HasColumnName("LeasID")
        Me.Property(Function(t) t.LeasName).HasColumnName("LeasName")
        Me.Property(Function(t) t.LeasNameAbbr).HasColumnName("LeasNameAbbr")
        Me.Property(Function(t) t.LeasNameShort).HasColumnName("LeasNameShort")
        Me.Property(Function(t) t.ModelNameM2).HasColumnName("ModelNameM2")
        'Me.Property(Function(t) t.NLTBookingDate).HasColumnName("NLTBookingDate")
        Me.Property(Function(t) t.SchemeCode).HasColumnName("SchemeCode")
        Me.Property(Function(t) t.VmiNetPremium).HasColumnName("VmiNetPremium")
        Me.Property(Function(t) t.VmiStamp).HasColumnName("VmiStamp")
        Me.Property(Function(t) t.VmiVat).HasColumnName("VmiVat")
        Me.Property(Function(t) t.VmiTotalPremium).HasColumnName("VmiTotalPremium")
        Me.Property(Function(t) t.CmiNetPremium).HasColumnName("CmiNetPremium")
        Me.Property(Function(t) t.CmiStamp).HasColumnName("CmiStamp")
        Me.Property(Function(t) t.CmiVat).HasColumnName("CmiVat")
        Me.Property(Function(t) t.CmiTotalPremium).HasColumnName("CmiTotalPremium")
        Me.Property(Function(t) t.ClassOfRiskVMI).HasColumnName("ClassOfRiskVMI")
        Me.Property(Function(t) t.ClassOfRiskCMI).HasColumnName("ClassOfRiskCMI")
        Me.Property(Function(t) t.ShowroomID).HasColumnName("ShowroomID")
        Me.Property(Function(t) t.ShowroomName).HasColumnName("ShowroomName")
        Me.Property(Function(t) t.DealerID).HasColumnName("DealerID")
        Me.Property(Function(t) t.ShorwoomNameEn).HasColumnName("ShorwoomNameEn")
        Me.Property(Function(t) t.CarTypeNameM2).HasColumnName("CarTypeNameM2")
        Me.Property(Function(t) t.TypeInsID).HasColumnName("TypeInsID")
        Me.Property(Function(t) t.TypeInsName).HasColumnName("TypeInsName")
        Me.Property(Function(t) t.TypeInsStatus).HasColumnName("TypeInsStatus")
        Me.Property(Function(t) t.TypeInsReportToNLTH).HasColumnName("TypeInsReportToNLTH")
        Me.Property(Function(t) t.TypeInsIsFree).HasColumnName("TypeInsIsFree")
        Me.Property(Function(t) t.UserAddedTel).HasColumnName("UserAddedTel")
        Me.Property(Function(t) t.UserAddedType).HasColumnName("UserAddedType")

    End Sub
End Class

'V_NMT_Policy_Cancellation_Daily
Partial Public Class V_NMT_Policy_Cancellation_Daily
    <Global.System.Data.Linq.Mapping.ColumnAttribute(DbType:="Int NOT NULL", IsPrimaryKey:=True)> _
    Public Property AppID As Integer
    Public Property TempID As String
    Public Property CusName As String
    Public Property CusSurname As String
    Public Property CusIDCard As String
    Public Property CusAddress1 As String
    Public Property CusAddress2 As String
    Public Property CusMobile As String
    Public Property CusHomePhone As String
    Public Property CusOfficePhone As System.Nullable(Of Integer)
    Public Property CarEngineNo As String
    Public Property CarChassisNo As String
    Public Property CarPlantNo As String
    Public Property DateEffective As System.Nullable(Of Date)
    Public Property DateExpired As System.Nullable(Of Date)
    Public Property DateNotification As System.Nullable(Of Date)
    Public Property AppStatus As String
    Public Property AppIsActive As System.Nullable(Of Integer)
    Public Property ClientCode As String
    Public Property CmiBuying As System.Nullable(Of Integer)
    Public Property AppBeneficiary As String
    Public Property LeasingID As System.Nullable(Of Integer)
    Public Property ClosingDate As System.Nullable(Of Date)
    Public Property ClosingMonth As System.Nullable(Of Date)
    Public Property ClosingDay As System.Nullable(Of Date)
    Public Property AppStatusRecived As System.Nullable(Of Boolean)
    Public Property ProjectID As System.Nullable(Of Integer)
    Public Property NotifyName As String
    Public Property CusTitle As String
    Public Property CarGroupID As System.Nullable(Of Integer)
    Public Property CarGroupName As String
    Public Property CarGroupName2 As String
    Public Property CarBrandName As String
    Public Property CarModelName As String
    Public Property CarTypeName As String
    Public Property CarTypeNameFull As String
    Public Property CarModelCode As String
    Public Property CarGroupNameM2 As String
    Public Property DealerNameEn As String
    Public Property NLTDealerCode As String
    Public Property NLTArea As String
    Public Property InsurerNameTh As String
    Public Property InsurerCodeSibis As String
    Public Property InsurerNameEn As String
    Public Property InsurerCode As String
    Public Property LeasID As System.Nullable(Of Integer)
    Public Property LeasName As String
    Public Property LeasNameAbbr As String
    Public Property LeasNameShort As String
    Public Property ModelNameM2 As String
    'Public Property NLTBookingDate As System.Nullable(Of Date)
    Public Property SchemeCode As String
    Public Property VmiNetPremium As System.Nullable(Of Decimal)
    Public Property VmiStamp As System.Nullable(Of Decimal)
    Public Property VmiVat As System.Nullable(Of Decimal)
    Public Property VmiTotalPremium As System.Nullable(Of Decimal)
    Public Property CmiNetPremium As System.Nullable(Of Decimal)
    Public Property CmiStamp As System.Nullable(Of Decimal)
    Public Property CmiVat As System.Nullable(Of Decimal)
    Public Property CmiTotalPremium As System.Nullable(Of Decimal)
    Public Property ClassOfRiskVMI As String
    Public Property ClassOfRiskCMI As String
    Public Property ShowroomID As System.Nullable(Of Integer)
    Public Property ShowroomName As String
    Public Property DealerID As System.Nullable(Of Integer)
    Public Property ShorwoomNameEn As String
    Public Property CarTypeNameM2 As String
    Public Property TypeInsID As System.Nullable(Of Integer)
    Public Property TypeInsName As String
    Public Property TypeInsStatus As System.Nullable(Of Integer)
    Public Property TypeInsReportToNLTH As System.Nullable(Of Boolean)
    Public Property TypeInsIsFree As System.Nullable(Of Boolean)
    Public Property UserAddedTel As String
    Public Property UserAddedType As String
End Class
Public Class V_NMT_Policy_Cancellation_Daily_Map
    Inherits EntityTypeConfiguration(Of V_NMT_Policy_Cancellation_Daily)
    Public Sub New(_table As String)
        ' Primary Key
        Me.HasKey(Function(t) t.AppID)

        ' Properties
        'Me.Property(Function(t) t.FirstName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.LastName).IsRequired().HasMaxLength(32)
        'Me.Property(Function(t) t.Phone).IsRequired().HasMaxLength(20)


        ' Table & Column Mappings
        'Me.ToTable("V_NMT_Policy_Cancellation_Daily")
        Me.ToTable(_table)
        Me.Property(Function(t) t.AppID).HasColumnName("AppID")
        Me.Property(Function(t) t.TempID).HasColumnName("TempID")
        Me.Property(Function(t) t.CusName).HasColumnName("CusName")
        Me.Property(Function(t) t.CusSurname).HasColumnName("CusSurname")
        Me.Property(Function(t) t.CusIDCard).HasColumnName("CusIDCard")
        Me.Property(Function(t) t.CusAddress1).HasColumnName("CusAddress1")
        Me.Property(Function(t) t.CusAddress2).HasColumnName("CusAddress2")
        Me.Property(Function(t) t.CusMobile).HasColumnName("CusMobile")
        Me.Property(Function(t) t.CusHomePhone).HasColumnName("CusHomePhone")
        Me.Property(Function(t) t.CusOfficePhone).HasColumnName("CusOfficePhone")
        Me.Property(Function(t) t.CarEngineNo).HasColumnName("CarEngineNo")
        Me.Property(Function(t) t.CarChassisNo).HasColumnName("CarChassisNo")
        Me.Property(Function(t) t.CarPlantNo).HasColumnName("CarPlantNo")
        Me.Property(Function(t) t.DateEffective).HasColumnName("DateEffective")
        Me.Property(Function(t) t.DateExpired).HasColumnName("DateExpired")
        Me.Property(Function(t) t.DateNotification).HasColumnName("DateNotification")
        Me.Property(Function(t) t.AppStatus).HasColumnName("AppStatus")
        Me.Property(Function(t) t.AppIsActive).HasColumnName("AppIsActive")
        Me.Property(Function(t) t.ClientCode).HasColumnName("ClientCode")
        Me.Property(Function(t) t.CmiBuying).HasColumnName("CmiBuying")
        Me.Property(Function(t) t.AppBeneficiary).HasColumnName("AppBeneficiary")
        Me.Property(Function(t) t.LeasingID).HasColumnName("LeasingID")
        Me.Property(Function(t) t.ClosingDate).HasColumnName("ClosingDate")
        Me.Property(Function(t) t.ClosingMonth).HasColumnName("ClosingMonth")
        Me.Property(Function(t) t.ClosingDay).HasColumnName("ClosingDay")
        Me.Property(Function(t) t.AppStatusRecived).HasColumnName("AppStatusRecived")
        Me.Property(Function(t) t.ProjectID).HasColumnName("ProjectID")
        Me.Property(Function(t) t.NotifyName).HasColumnName("NotifyName")
        Me.Property(Function(t) t.CusTitle).HasColumnName("CusTitle")
        Me.Property(Function(t) t.CarGroupID).HasColumnName("CarGroupID")
        Me.Property(Function(t) t.CarGroupName).HasColumnName("CarGroupName")
        Me.Property(Function(t) t.CarGroupName2).HasColumnName("CarGroupName2")
        Me.Property(Function(t) t.CarBrandName).HasColumnName("CarBrandName")
        Me.Property(Function(t) t.CarModelName).HasColumnName("CarModelName")
        Me.Property(Function(t) t.CarTypeName).HasColumnName("CarTypeName")
        Me.Property(Function(t) t.CarTypeNameFull).HasColumnName("CarTypeNameFull")
        Me.Property(Function(t) t.CarModelCode).HasColumnName("CarModelCode")
        Me.Property(Function(t) t.CarGroupNameM2).HasColumnName("CarGroupNameM2")
        Me.Property(Function(t) t.DealerNameEn).HasColumnName("DealerNameEn")
        Me.Property(Function(t) t.NLTDealerCode).HasColumnName("NLTDealerCode")
        Me.Property(Function(t) t.NLTArea).HasColumnName("NLTArea")
        Me.Property(Function(t) t.InsurerNameTh).HasColumnName("InsurerNameTh")
        Me.Property(Function(t) t.InsurerCodeSibis).HasColumnName("InsurerCodeSibis")
        Me.Property(Function(t) t.InsurerNameEn).HasColumnName("InsurerNameEn")
        Me.Property(Function(t) t.InsurerCode).HasColumnName("InsurerCode")
        Me.Property(Function(t) t.LeasID).HasColumnName("LeasID")
        Me.Property(Function(t) t.LeasName).HasColumnName("LeasName")
        Me.Property(Function(t) t.LeasNameAbbr).HasColumnName("LeasNameAbbr")
        Me.Property(Function(t) t.LeasNameShort).HasColumnName("LeasNameShort")
        Me.Property(Function(t) t.ModelNameM2).HasColumnName("ModelNameM2")
        'Me.Property(Function(t) t.NLTBookingDate).HasColumnName("NLTBookingDate")
        Me.Property(Function(t) t.SchemeCode).HasColumnName("SchemeCode")
        Me.Property(Function(t) t.VmiNetPremium).HasColumnName("VmiNetPremium")
        Me.Property(Function(t) t.VmiStamp).HasColumnName("VmiStamp")
        Me.Property(Function(t) t.VmiVat).HasColumnName("VmiVat")
        Me.Property(Function(t) t.VmiTotalPremium).HasColumnName("VmiTotalPremium")
        Me.Property(Function(t) t.CmiNetPremium).HasColumnName("CmiNetPremium")
        Me.Property(Function(t) t.CmiStamp).HasColumnName("CmiStamp")
        Me.Property(Function(t) t.CmiVat).HasColumnName("CmiVat")
        Me.Property(Function(t) t.CmiTotalPremium).HasColumnName("CmiTotalPremium")
        Me.Property(Function(t) t.ClassOfRiskVMI).HasColumnName("ClassOfRiskVMI")
        Me.Property(Function(t) t.ClassOfRiskCMI).HasColumnName("ClassOfRiskCMI")
        Me.Property(Function(t) t.ShowroomID).HasColumnName("ShowroomID")
        Me.Property(Function(t) t.ShowroomName).HasColumnName("ShowroomName")
        Me.Property(Function(t) t.DealerID).HasColumnName("DealerID")
        Me.Property(Function(t) t.ShorwoomNameEn).HasColumnName("ShorwoomNameEn")
        Me.Property(Function(t) t.CarTypeNameM2).HasColumnName("CarTypeNameM2")
        Me.Property(Function(t) t.TypeInsID).HasColumnName("TypeInsID")
        Me.Property(Function(t) t.TypeInsName).HasColumnName("TypeInsName")
        Me.Property(Function(t) t.TypeInsStatus).HasColumnName("TypeInsStatus")
        Me.Property(Function(t) t.TypeInsReportToNLTH).HasColumnName("TypeInsReportToNLTH")
        Me.Property(Function(t) t.TypeInsIsFree).HasColumnName("TypeInsIsFree")
        Me.Property(Function(t) t.UserAddedTel).HasColumnName("UserAddedTel")
        Me.Property(Function(t) t.UserAddedType).HasColumnName("UserAddedType")

    End Sub
End Class