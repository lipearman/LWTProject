Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Data.Linq
Imports System.Linq
Imports System.Web
Imports System.Web.SessionState
Imports DevExpress.Data.Filtering

Imports MotorClaim


Public NotInheritable Class MotorClaimModel
    Private Shared currentDataProvider As MotorClaimDataProviderBase
    Private Sub New()
    End Sub
    Public Shared ReadOnly Property DataProvider() As MotorClaimDataProviderBase
        Get
            If currentDataProvider Is Nothing Then
                currentDataProvider = CreateDataProvider()
            End If
            Return currentDataProvider
        End Get
    End Property
    Private Shared Function CreateDataProvider() As MotorClaimDataProviderBase
        Return New MotorClaimDBDataProvider()
    End Function
End Class

Public MustInherit Class MotorClaimDataProviderBase
    Public MustOverride ReadOnly Property tblClaimTransaction_Data() As IEnumerable(Of tblClaimTransaction_Data)
    Public MustOverride ReadOnly Property tblClaimTransaction_Result() As IEnumerable(Of tblClaimTransaction_Result)
    Public MustOverride ReadOnly Property V_ClaimTransactionData() As IEnumerable(Of V_ClaimTransactionData)

    Public MustOverride ReadOnly Property Portal_User_Data() As IEnumerable(Of Portal_User)
End Class

Public Class MotorClaimDBDataProvider
    Inherits MotorClaimDataProviderBase

    Protected Function GetDataContext() As DataClasses_MotorClaimDataContext
        Return New DataClasses_MotorClaimDataExt()
    End Function


    Public Overrides ReadOnly Property tblClaimTransaction_Data() As IEnumerable(Of tblClaimTransaction_Data)
        Get
            Return GetDataContext().tblClaimTransaction_Datas
        End Get
    End Property
    Public Overrides ReadOnly Property tblClaimTransaction_Result() As IEnumerable(Of tblClaimTransaction_Result)
        Get
            Return GetDataContext().tblClaimTransaction_Results
        End Get
    End Property
    Public Overrides ReadOnly Property V_ClaimTransactionData() As IEnumerable(Of V_ClaimTransactionData)
        Get
            Return GetDataContext().V_ClaimTransactionDatas
        End Get
    End Property
    Public Overrides ReadOnly Property Portal_User_Data() As IEnumerable(Of Portal_User)
        Get
            Return GetDataContext().Portal_Users
        End Get
    End Property
End Class


 