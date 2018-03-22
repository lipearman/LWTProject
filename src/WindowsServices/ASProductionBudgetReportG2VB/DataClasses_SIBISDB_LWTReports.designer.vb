﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Data.Linq
Imports System.Data.Linq.Mapping
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Reflection


<Global.System.Data.Linq.Mapping.DatabaseAttribute(Name:="LWTReports")>  _
Partial Public Class DataClasses_SIBISDB_LWTReportsDataContext
	Inherits System.Data.Linq.DataContext
	
	Private Shared mappingSource As System.Data.Linq.Mapping.MappingSource = New AttributeMappingSource()
	
  #Region "Extensibility Method Definitions"
  Partial Private Sub OnCreated()
  End Sub
  #End Region
	
	Public Sub New()
		MyBase.New(Global.ASProductionBudgetReportG2VB.My.MySettings.Default.LWTReportsConnectionString, mappingSource)
		OnCreated
	End Sub
	
	Public Sub New(ByVal connection As String)
		MyBase.New(connection, mappingSource)
		OnCreated
	End Sub
	
	Public Sub New(ByVal connection As System.Data.IDbConnection)
		MyBase.New(connection, mappingSource)
		OnCreated
	End Sub
	
	Public Sub New(ByVal connection As String, ByVal mappingSource As System.Data.Linq.Mapping.MappingSource)
		MyBase.New(connection, mappingSource)
		OnCreated
	End Sub
	
	Public Sub New(ByVal connection As System.Data.IDbConnection, ByVal mappingSource As System.Data.Linq.Mapping.MappingSource)
		MyBase.New(connection, mappingSource)
		OnCreated
	End Sub
	
	Public ReadOnly Property V_Production_APD_Budget_Monthly_Reports() As System.Data.Linq.Table(Of V_Production_APD_Budget_Monthly_Report)
		Get
			Return Me.GetTable(Of V_Production_APD_Budget_Monthly_Report)
		End Get
	End Property
End Class

<Global.System.Data.Linq.Mapping.TableAttribute(Name:="dbo.V_Production_APD_Budget_Monthly_Report")>  _
Partial Public Class V_Production_APD_Budget_Monthly_Report
	
	Private _AEGroupName As String
	
	Private _AESGroupName As String
	
	Private _AEName As String
	
	Private _Owner As String
	
	Private _NetIncome As System.Nullable(Of Double)
	
	Private _DNCount As System.Nullable(Of Integer)
	
	Private _CNCount As System.Nullable(Of Integer)
	
	Private _BudgetAmount As Double
	
	Private _DataDateFrom As System.Nullable(Of Date)
	
	Private _DataDateTo As System.Nullable(Of Date)
	
	Public Sub New()
		MyBase.New
	End Sub
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_AEGroupName", DbType:="VarChar(50)")>  _
	Public Property AEGroupName() As String
		Get
			Return Me._AEGroupName
		End Get
		Set
			If (String.Equals(Me._AEGroupName, value) = false) Then
				Me._AEGroupName = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_AESGroupName", DbType:="VarChar(50)")>  _
	Public Property AESGroupName() As String
		Get
			Return Me._AESGroupName
		End Get
		Set
			If (String.Equals(Me._AESGroupName, value) = false) Then
				Me._AESGroupName = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_AEName", DbType:="VarChar(60)")>  _
	Public Property AEName() As String
		Get
			Return Me._AEName
		End Get
		Set
			If (String.Equals(Me._AEName, value) = false) Then
				Me._AEName = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Owner", DbType:="VarChar(50)")>  _
	Public Property Owner() As String
		Get
			Return Me._Owner
		End Get
		Set
			If (String.Equals(Me._Owner, value) = false) Then
				Me._Owner = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_NetIncome", DbType:="Float")>  _
	Public Property NetIncome() As System.Nullable(Of Double)
		Get
			Return Me._NetIncome
		End Get
		Set
			If (Me._NetIncome.Equals(value) = false) Then
				Me._NetIncome = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_DNCount", DbType:="Int")>  _
	Public Property DNCount() As System.Nullable(Of Integer)
		Get
			Return Me._DNCount
		End Get
		Set
			If (Me._DNCount.Equals(value) = false) Then
				Me._DNCount = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_CNCount", DbType:="Int")>  _
	Public Property CNCount() As System.Nullable(Of Integer)
		Get
			Return Me._CNCount
		End Get
		Set
			If (Me._CNCount.Equals(value) = false) Then
				Me._CNCount = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_BudgetAmount", DbType:="Float NOT NULL")>  _
	Public Property BudgetAmount() As Double
		Get
			Return Me._BudgetAmount
		End Get
		Set
			If ((Me._BudgetAmount = value)  _
						= false) Then
				Me._BudgetAmount = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_DataDateFrom", DbType:="DateTime")>  _
	Public Property DataDateFrom() As System.Nullable(Of Date)
		Get
			Return Me._DataDateFrom
		End Get
		Set
			If (Me._DataDateFrom.Equals(value) = false) Then
				Me._DataDateFrom = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_DataDateTo", DbType:="DateTime")>  _
	Public Property DataDateTo() As System.Nullable(Of Date)
		Get
			Return Me._DataDateTo
		End Get
		Set
			If (Me._DataDateTo.Equals(value) = false) Then
				Me._DataDateTo = value
			End If
		End Set
	End Property
End Class
