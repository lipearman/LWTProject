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
Partial Public Class DataClasses_LWTReportsDataContext
	Inherits System.Data.Linq.DataContext
	
	Private Shared mappingSource As System.Data.Linq.Mapping.MappingSource = New AttributeMappingSource()
	
  #Region "Extensibility Method Definitions"
  Partial Private Sub OnCreated()
  End Sub
  #End Region
	
	Public Sub New()
		MyBase.New(Global.APDM2HoldCover2Insurer.My.MySettings.Default.LWTReportsConnectionString, mappingSource)
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
	
	Public ReadOnly Property Rawdata_M2_HoldCover_InsurerReport_Dailies() As System.Data.Linq.Table(Of Rawdata_M2_HoldCover_InsurerReport_Daily)
		Get
			Return Me.GetTable(Of Rawdata_M2_HoldCover_InsurerReport_Daily)
		End Get
	End Property
End Class

<Global.System.Data.Linq.Mapping.TableAttribute(Name:="dbo.Rawdata_M2_HoldCover_InsurerReport_Daily")>  _
Partial Public Class Rawdata_M2_HoldCover_InsurerReport_Daily
	
	Private _TempID As String
	
	Private _Name As String
	
	Private _SurName As String
	
	Private _Model As String
	
	Private _CompanyName As String
	
	Private _CampaignName As String
	
	Private _StartingDate As System.Nullable(Of Date)
	
	Private _VIN As String
	
	Private _InsurerNameThai As String
	
	Private _Status As String
	
	Private _InsurerCode As String
	
	Private _IsActive As System.Nullable(Of Integer)
	
	Private _GroupName As String
	
	Private _ModelName As String
	
	Private _NotifigationDate As System.Nullable(Of Date)
	
	Private _TypeName As String
	
	Private _Code1 As String
	
	Public Sub New()
		MyBase.New
	End Sub
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_TempID", DbType:="VarChar(50)")>  _
	Public Property TempID() As String
		Get
			Return Me._TempID
		End Get
		Set
			If (String.Equals(Me._TempID, value) = false) Then
				Me._TempID = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Name", DbType:="VarChar(120)")>  _
	Public Property Name() As String
		Get
			Return Me._Name
		End Get
		Set
			If (String.Equals(Me._Name, value) = false) Then
				Me._Name = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_SurName", DbType:="VarChar(120)")>  _
	Public Property SurName() As String
		Get
			Return Me._SurName
		End Get
		Set
			If (String.Equals(Me._SurName, value) = false) Then
				Me._SurName = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Model", DbType:="NVarChar(566)")>  _
	Public Property Model() As String
		Get
			Return Me._Model
		End Get
		Set
			If (String.Equals(Me._Model, value) = false) Then
				Me._Model = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_CompanyName", DbType:="VarChar(150)")>  _
	Public Property CompanyName() As String
		Get
			Return Me._CompanyName
		End Get
		Set
			If (String.Equals(Me._CompanyName, value) = false) Then
				Me._CompanyName = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_CampaignName", DbType:="NVarChar(100)")>  _
	Public Property CampaignName() As String
		Get
			Return Me._CampaignName
		End Get
		Set
			If (String.Equals(Me._CampaignName, value) = false) Then
				Me._CampaignName = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_StartingDate", DbType:="DateTime")>  _
	Public Property StartingDate() As System.Nullable(Of Date)
		Get
			Return Me._StartingDate
		End Get
		Set
			If (Me._StartingDate.Equals(value) = false) Then
				Me._StartingDate = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_VIN", DbType:="VarChar(50)")>  _
	Public Property VIN() As String
		Get
			Return Me._VIN
		End Get
		Set
			If (String.Equals(Me._VIN, value) = false) Then
				Me._VIN = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_InsurerNameThai", DbType:="NVarChar(255)")>  _
	Public Property InsurerNameThai() As String
		Get
			Return Me._InsurerNameThai
		End Get
		Set
			If (String.Equals(Me._InsurerNameThai, value) = false) Then
				Me._InsurerNameThai = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Status", DbType:="VarChar(2)")>  _
	Public Property Status() As String
		Get
			Return Me._Status
		End Get
		Set
			If (String.Equals(Me._Status, value) = false) Then
				Me._Status = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_InsurerCode", DbType:="VarChar(50)")>  _
	Public Property InsurerCode() As String
		Get
			Return Me._InsurerCode
		End Get
		Set
			If (String.Equals(Me._InsurerCode, value) = false) Then
				Me._InsurerCode = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_IsActive", DbType:="Int")>  _
	Public Property IsActive() As System.Nullable(Of Integer)
		Get
			Return Me._IsActive
		End Get
		Set
			If (Me._IsActive.Equals(value) = false) Then
				Me._IsActive = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_GroupName", DbType:="NVarChar(255)")>  _
	Public Property GroupName() As String
		Get
			Return Me._GroupName
		End Get
		Set
			If (String.Equals(Me._GroupName, value) = false) Then
				Me._GroupName = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ModelName", DbType:="NVarChar(255)")>  _
	Public Property ModelName() As String
		Get
			Return Me._ModelName
		End Get
		Set
			If (String.Equals(Me._ModelName, value) = false) Then
				Me._ModelName = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_NotifigationDate", DbType:="DateTime")>  _
	Public Property NotifigationDate() As System.Nullable(Of Date)
		Get
			Return Me._NotifigationDate
		End Get
		Set
			If (Me._NotifigationDate.Equals(value) = false) Then
				Me._NotifigationDate = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_TypeName", DbType:="NVarChar(50)")>  _
	Public Property TypeName() As String
		Get
			Return Me._TypeName
		End Get
		Set
			If (String.Equals(Me._TypeName, value) = false) Then
				Me._TypeName = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Code1", DbType:="VarChar(50)")>  _
	Public Property Code1() As String
		Get
			Return Me._Code1
		End Get
		Set
			If (String.Equals(Me._Code1, value) = false) Then
				Me._Code1 = value
			End If
		End Set
	End Property
End Class
