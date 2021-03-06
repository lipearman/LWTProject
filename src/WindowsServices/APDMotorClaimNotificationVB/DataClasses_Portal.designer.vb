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


<Global.System.Data.Linq.Mapping.DatabaseAttribute(Name:="Portal")>  _
Partial Public Class DataClasses_PortalDataContext
	Inherits System.Data.Linq.DataContext
	
	Private Shared mappingSource As System.Data.Linq.Mapping.MappingSource = New AttributeMappingSource()
	
  #Region "Extensibility Method Definitions"
  Partial Private Sub OnCreated()
  End Sub
  #End Region
	
	Public Sub New()
		MyBase.New(Global.APDMotorClaimNotificationVB.My.MySettings.Default.PortalConnectionString, mappingSource)
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
	
	Public ReadOnly Property v_ads_actives() As System.Data.Linq.Table(Of v_ads_active)
		Get
			Return Me.GetTable(Of v_ads_active)
		End Get
	End Property
End Class

<Global.System.Data.Linq.Mapping.TableAttribute(Name:="dbo.v_ads_active")>  _
Partial Public Class v_ads_active
	
	Private _objectGUID As System.Data.Linq.Binary
	
	Private _lastLogon As String
	
	Private _accountExpires As String
	
	Private _givenname As String
	
	Private _physicalDeliveryOfficeName As String
	
	Private _department As String
	
	Private _facsimileTelephoneNumber As String
	
	Private _mobile As String
	
	Private _ou As String
	
	Private _mail As String
	
	Private _sAMAccountName As String
	
	Private _telephoneNumber As String
	
	Private _displayName As String
	
	Private _title As String
	
	Private _company As String
	
	Private _pager As String
	
	Private _streetaddress As String
	
	Public Sub New()
		MyBase.New
	End Sub
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_objectGUID", DbType:="VarBinary(4000)", UpdateCheck:=UpdateCheck.Never)>  _
	Public Property objectGUID() As System.Data.Linq.Binary
		Get
			Return Me._objectGUID
		End Get
		Set
			If (Object.Equals(Me._objectGUID, value) = false) Then
				Me._objectGUID = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_lastLogon", DbType:="NVarChar(4000)")>  _
	Public Property lastLogon() As String
		Get
			Return Me._lastLogon
		End Get
		Set
			If (String.Equals(Me._lastLogon, value) = false) Then
				Me._lastLogon = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_accountExpires", DbType:="NVarChar(4000)")>  _
	Public Property accountExpires() As String
		Get
			Return Me._accountExpires
		End Get
		Set
			If (String.Equals(Me._accountExpires, value) = false) Then
				Me._accountExpires = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_givenname", DbType:="NVarChar(4000)")>  _
	Public Property givenname() As String
		Get
			Return Me._givenname
		End Get
		Set
			If (String.Equals(Me._givenname, value) = false) Then
				Me._givenname = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_physicalDeliveryOfficeName", DbType:="NVarChar(4000)")>  _
	Public Property physicalDeliveryOfficeName() As String
		Get
			Return Me._physicalDeliveryOfficeName
		End Get
		Set
			If (String.Equals(Me._physicalDeliveryOfficeName, value) = false) Then
				Me._physicalDeliveryOfficeName = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_department", DbType:="NVarChar(4000)")>  _
	Public Property department() As String
		Get
			Return Me._department
		End Get
		Set
			If (String.Equals(Me._department, value) = false) Then
				Me._department = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_facsimileTelephoneNumber", DbType:="NVarChar(4000)")>  _
	Public Property facsimileTelephoneNumber() As String
		Get
			Return Me._facsimileTelephoneNumber
		End Get
		Set
			If (String.Equals(Me._facsimileTelephoneNumber, value) = false) Then
				Me._facsimileTelephoneNumber = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_mobile", DbType:="NVarChar(4000)")>  _
	Public Property mobile() As String
		Get
			Return Me._mobile
		End Get
		Set
			If (String.Equals(Me._mobile, value) = false) Then
				Me._mobile = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ou", DbType:="NVarChar(4000)")>  _
	Public Property ou() As String
		Get
			Return Me._ou
		End Get
		Set
			If (String.Equals(Me._ou, value) = false) Then
				Me._ou = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_mail", DbType:="NVarChar(4000)")>  _
	Public Property mail() As String
		Get
			Return Me._mail
		End Get
		Set
			If (String.Equals(Me._mail, value) = false) Then
				Me._mail = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_sAMAccountName", DbType:="NVarChar(4000)")>  _
	Public Property sAMAccountName() As String
		Get
			Return Me._sAMAccountName
		End Get
		Set
			If (String.Equals(Me._sAMAccountName, value) = false) Then
				Me._sAMAccountName = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_telephoneNumber", DbType:="NVarChar(4000)")>  _
	Public Property telephoneNumber() As String
		Get
			Return Me._telephoneNumber
		End Get
		Set
			If (String.Equals(Me._telephoneNumber, value) = false) Then
				Me._telephoneNumber = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_displayName", DbType:="NVarChar(4000)")>  _
	Public Property displayName() As String
		Get
			Return Me._displayName
		End Get
		Set
			If (String.Equals(Me._displayName, value) = false) Then
				Me._displayName = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_title", DbType:="NVarChar(4000)")>  _
	Public Property title() As String
		Get
			Return Me._title
		End Get
		Set
			If (String.Equals(Me._title, value) = false) Then
				Me._title = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_company", DbType:="NVarChar(4000)")>  _
	Public Property company() As String
		Get
			Return Me._company
		End Get
		Set
			If (String.Equals(Me._company, value) = false) Then
				Me._company = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_pager", DbType:="NVarChar(4000)")>  _
	Public Property pager() As String
		Get
			Return Me._pager
		End Get
		Set
			If (String.Equals(Me._pager, value) = false) Then
				Me._pager = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_streetaddress", DbType:="NVarChar(4000)")>  _
	Public Property streetaddress() As String
		Get
			Return Me._streetaddress
		End Get
		Set
			If (String.Equals(Me._streetaddress, value) = false) Then
				Me._streetaddress = value
			End If
		End Set
	End Property
End Class
