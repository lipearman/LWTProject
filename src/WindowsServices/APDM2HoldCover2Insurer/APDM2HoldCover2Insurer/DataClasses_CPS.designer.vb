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


<Global.System.Data.Linq.Mapping.DatabaseAttribute(Name:="CPS")>  _
Partial Public Class DataClasses_CPSDataContext
	Inherits System.Data.Linq.DataContext
	
	Private Shared mappingSource As System.Data.Linq.Mapping.MappingSource = New AttributeMappingSource()
	
  #Region "Extensibility Method Definitions"
  Partial Private Sub OnCreated()
  End Sub
  Partial Private Sub InserttblNoticeMailContact(instance As tblNoticeMailContact)
    End Sub
  Partial Private Sub UpdatetblNoticeMailContact(instance As tblNoticeMailContact)
    End Sub
  Partial Private Sub DeletetblNoticeMailContact(instance As tblNoticeMailContact)
    End Sub
  #End Region
	
	Public Sub New()
		MyBase.New(Global.APDM2HoldCover2Insurer.My.MySettings.Default.CPSConnectionString, mappingSource)
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
	
	Public ReadOnly Property tblNoticeMailContacts() As System.Data.Linq.Table(Of tblNoticeMailContact)
		Get
			Return Me.GetTable(Of tblNoticeMailContact)
		End Get
	End Property
End Class

<Global.System.Data.Linq.Mapping.TableAttribute(Name:="dbo.tblNoticeMailContact")>  _
Partial Public Class tblNoticeMailContact
	Implements System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	
	Private Shared emptyChangingEventArgs As PropertyChangingEventArgs = New PropertyChangingEventArgs(String.Empty)
	
	Private _ID As Integer
	
	Private _NoticeCode As String
	
	Private _Code As String
	
	Private _Name As String
	
	Private _MailTo As String
	
	Private _MailCC As String
	
	Private _AccountNo As String
	
	Private _ContactName As String
	
	Private _ContactAddress As String
	
	Private _ContactTel As String
	
	Private _ContactFax As String
	
	Private _IsActive As System.Nullable(Of Boolean)
	
	Private _CreationDate As System.Nullable(Of Date)
	
	Private _CreationBy As String
	
	Private _ModifyBy As String
	
	Private _ModifyDate As System.Nullable(Of Date)
	
	Private _M1Code As String
	
    #Region "Extensibility Method Definitions"
    Partial Private Sub OnLoaded()
    End Sub
    Partial Private Sub OnValidate(action As System.Data.Linq.ChangeAction)
    End Sub
    Partial Private Sub OnCreated()
    End Sub
    Partial Private Sub OnIDChanging(value As Integer)
    End Sub
    Partial Private Sub OnIDChanged()
    End Sub
    Partial Private Sub OnNoticeCodeChanging(value As String)
    End Sub
    Partial Private Sub OnNoticeCodeChanged()
    End Sub
    Partial Private Sub OnCodeChanging(value As String)
    End Sub
    Partial Private Sub OnCodeChanged()
    End Sub
    Partial Private Sub OnNameChanging(value As String)
    End Sub
    Partial Private Sub OnNameChanged()
    End Sub
    Partial Private Sub OnMailToChanging(value As String)
    End Sub
    Partial Private Sub OnMailToChanged()
    End Sub
    Partial Private Sub OnMailCCChanging(value As String)
    End Sub
    Partial Private Sub OnMailCCChanged()
    End Sub
    Partial Private Sub OnAccountNoChanging(value As String)
    End Sub
    Partial Private Sub OnAccountNoChanged()
    End Sub
    Partial Private Sub OnContactNameChanging(value As String)
    End Sub
    Partial Private Sub OnContactNameChanged()
    End Sub
    Partial Private Sub OnContactAddressChanging(value As String)
    End Sub
    Partial Private Sub OnContactAddressChanged()
    End Sub
    Partial Private Sub OnContactTelChanging(value As String)
    End Sub
    Partial Private Sub OnContactTelChanged()
    End Sub
    Partial Private Sub OnContactFaxChanging(value As String)
    End Sub
    Partial Private Sub OnContactFaxChanged()
    End Sub
    Partial Private Sub OnIsActiveChanging(value As System.Nullable(Of Boolean))
    End Sub
    Partial Private Sub OnIsActiveChanged()
    End Sub
    Partial Private Sub OnCreationDateChanging(value As System.Nullable(Of Date))
    End Sub
    Partial Private Sub OnCreationDateChanged()
    End Sub
    Partial Private Sub OnCreationByChanging(value As String)
    End Sub
    Partial Private Sub OnCreationByChanged()
    End Sub
    Partial Private Sub OnModifyByChanging(value As String)
    End Sub
    Partial Private Sub OnModifyByChanged()
    End Sub
    Partial Private Sub OnModifyDateChanging(value As System.Nullable(Of Date))
    End Sub
    Partial Private Sub OnModifyDateChanged()
    End Sub
    Partial Private Sub OnM1CodeChanging(value As String)
    End Sub
    Partial Private Sub OnM1CodeChanged()
    End Sub
    #End Region
	
	Public Sub New()
		MyBase.New
		OnCreated
	End Sub
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ID", AutoSync:=AutoSync.Always, DbType:="Int NOT NULL IDENTITY", IsDbGenerated:=true)>  _
	Public Property ID() As Integer
		Get
			Return Me._ID
		End Get
		Set
			If ((Me._ID = value)  _
						= false) Then
				Me.OnIDChanging(value)
				Me.SendPropertyChanging
				Me._ID = value
				Me.SendPropertyChanged("ID")
				Me.OnIDChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_NoticeCode", DbType:="VarChar(8) NOT NULL", CanBeNull:=false, IsPrimaryKey:=true)>  _
	Public Property NoticeCode() As String
		Get
			Return Me._NoticeCode
		End Get
		Set
			If (String.Equals(Me._NoticeCode, value) = false) Then
				Me.OnNoticeCodeChanging(value)
				Me.SendPropertyChanging
				Me._NoticeCode = value
				Me.SendPropertyChanged("NoticeCode")
				Me.OnNoticeCodeChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Code", DbType:="VarChar(8) NOT NULL", CanBeNull:=false, IsPrimaryKey:=true)>  _
	Public Property Code() As String
		Get
			Return Me._Code
		End Get
		Set
			If (String.Equals(Me._Code, value) = false) Then
				Me.OnCodeChanging(value)
				Me.SendPropertyChanging
				Me._Code = value
				Me.SendPropertyChanged("Code")
				Me.OnCodeChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Name", DbType:="VarChar(255)")>  _
	Public Property Name() As String
		Get
			Return Me._Name
		End Get
		Set
			If (String.Equals(Me._Name, value) = false) Then
				Me.OnNameChanging(value)
				Me.SendPropertyChanging
				Me._Name = value
				Me.SendPropertyChanged("Name")
				Me.OnNameChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_MailTo", DbType:="VarChar(MAX)")>  _
	Public Property MailTo() As String
		Get
			Return Me._MailTo
		End Get
		Set
			If (String.Equals(Me._MailTo, value) = false) Then
				Me.OnMailToChanging(value)
				Me.SendPropertyChanging
				Me._MailTo = value
				Me.SendPropertyChanged("MailTo")
				Me.OnMailToChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_MailCC", DbType:="VarChar(MAX)")>  _
	Public Property MailCC() As String
		Get
			Return Me._MailCC
		End Get
		Set
			If (String.Equals(Me._MailCC, value) = false) Then
				Me.OnMailCCChanging(value)
				Me.SendPropertyChanging
				Me._MailCC = value
				Me.SendPropertyChanged("MailCC")
				Me.OnMailCCChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_AccountNo", DbType:="VarChar(13)")>  _
	Public Property AccountNo() As String
		Get
			Return Me._AccountNo
		End Get
		Set
			If (String.Equals(Me._AccountNo, value) = false) Then
				Me.OnAccountNoChanging(value)
				Me.SendPropertyChanging
				Me._AccountNo = value
				Me.SendPropertyChanged("AccountNo")
				Me.OnAccountNoChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ContactName", DbType:="VarChar(255)")>  _
	Public Property ContactName() As String
		Get
			Return Me._ContactName
		End Get
		Set
			If (String.Equals(Me._ContactName, value) = false) Then
				Me.OnContactNameChanging(value)
				Me.SendPropertyChanging
				Me._ContactName = value
				Me.SendPropertyChanged("ContactName")
				Me.OnContactNameChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ContactAddress", DbType:="VarChar(255)")>  _
	Public Property ContactAddress() As String
		Get
			Return Me._ContactAddress
		End Get
		Set
			If (String.Equals(Me._ContactAddress, value) = false) Then
				Me.OnContactAddressChanging(value)
				Me.SendPropertyChanging
				Me._ContactAddress = value
				Me.SendPropertyChanged("ContactAddress")
				Me.OnContactAddressChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ContactTel", DbType:="VarChar(255)")>  _
	Public Property ContactTel() As String
		Get
			Return Me._ContactTel
		End Get
		Set
			If (String.Equals(Me._ContactTel, value) = false) Then
				Me.OnContactTelChanging(value)
				Me.SendPropertyChanging
				Me._ContactTel = value
				Me.SendPropertyChanged("ContactTel")
				Me.OnContactTelChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ContactFax", DbType:="VarChar(255)")>  _
	Public Property ContactFax() As String
		Get
			Return Me._ContactFax
		End Get
		Set
			If (String.Equals(Me._ContactFax, value) = false) Then
				Me.OnContactFaxChanging(value)
				Me.SendPropertyChanging
				Me._ContactFax = value
				Me.SendPropertyChanged("ContactFax")
				Me.OnContactFaxChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_IsActive", DbType:="Bit")>  _
	Public Property IsActive() As System.Nullable(Of Boolean)
		Get
			Return Me._IsActive
		End Get
		Set
			If (Me._IsActive.Equals(value) = false) Then
				Me.OnIsActiveChanging(value)
				Me.SendPropertyChanging
				Me._IsActive = value
				Me.SendPropertyChanged("IsActive")
				Me.OnIsActiveChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_CreationDate", DbType:="DateTime")>  _
	Public Property CreationDate() As System.Nullable(Of Date)
		Get
			Return Me._CreationDate
		End Get
		Set
			If (Me._CreationDate.Equals(value) = false) Then
				Me.OnCreationDateChanging(value)
				Me.SendPropertyChanging
				Me._CreationDate = value
				Me.SendPropertyChanged("CreationDate")
				Me.OnCreationDateChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_CreationBy", DbType:="VarChar(50)")>  _
	Public Property CreationBy() As String
		Get
			Return Me._CreationBy
		End Get
		Set
			If (String.Equals(Me._CreationBy, value) = false) Then
				Me.OnCreationByChanging(value)
				Me.SendPropertyChanging
				Me._CreationBy = value
				Me.SendPropertyChanged("CreationBy")
				Me.OnCreationByChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ModifyBy", DbType:="VarChar(50)")>  _
	Public Property ModifyBy() As String
		Get
			Return Me._ModifyBy
		End Get
		Set
			If (String.Equals(Me._ModifyBy, value) = false) Then
				Me.OnModifyByChanging(value)
				Me.SendPropertyChanging
				Me._ModifyBy = value
				Me.SendPropertyChanged("ModifyBy")
				Me.OnModifyByChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ModifyDate", DbType:="DateTime")>  _
	Public Property ModifyDate() As System.Nullable(Of Date)
		Get
			Return Me._ModifyDate
		End Get
		Set
			If (Me._ModifyDate.Equals(value) = false) Then
				Me.OnModifyDateChanging(value)
				Me.SendPropertyChanging
				Me._ModifyDate = value
				Me.SendPropertyChanged("ModifyDate")
				Me.OnModifyDateChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_M1Code", DbType:="VarChar(50)")>  _
	Public Property M1Code() As String
		Get
			Return Me._M1Code
		End Get
		Set
			If (String.Equals(Me._M1Code, value) = false) Then
				Me.OnM1CodeChanging(value)
				Me.SendPropertyChanging
				Me._M1Code = value
				Me.SendPropertyChanged("M1Code")
				Me.OnM1CodeChanged
			End If
		End Set
	End Property
	
	Public Event PropertyChanging As PropertyChangingEventHandler Implements System.ComponentModel.INotifyPropertyChanging.PropertyChanging
	
	Public Event PropertyChanged As PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
	
	Protected Overridable Sub SendPropertyChanging()
		If ((Me.PropertyChangingEvent Is Nothing)  _
					= false) Then
			RaiseEvent PropertyChanging(Me, emptyChangingEventArgs)
		End If
	End Sub
	
	Protected Overridable Sub SendPropertyChanged(ByVal propertyName As [String])
		If ((Me.PropertyChangedEvent Is Nothing)  _
					= false) Then
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End If
	End Sub
End Class
