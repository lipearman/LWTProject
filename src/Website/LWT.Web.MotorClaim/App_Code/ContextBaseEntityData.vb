Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports DevExpress.Internal

Public Class ContextBaseEntityData
    Inherits ContextBase
    Shared Sub New()
        Database.SetInitializer(Of ContextBaseEntityData)(Nothing)
    End Sub

    Public Sub New()
        MyBase.New("MotorClaimConnectionString")
    End Sub
    'Public Property V_NissanMotors() As DbSet(Of V_NissanMotor)
    'Public Property V_NissanMotorNMTs() As DbSet(Of V_NissanMotorNMT)
    'Public Property V_NissanMotorNLTHs() As DbSet(Of V_NissanMotorNLTH)

    'Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
    '    modelBuilder.Configurations.Add(New V_NissanMotorMap())
    '    modelBuilder.Configurations.Add(New V_NissanMotorNMTMap())
    '    modelBuilder.Configurations.Add(New V_NissanMotorNLTHMap())
    'End Sub
End Class
