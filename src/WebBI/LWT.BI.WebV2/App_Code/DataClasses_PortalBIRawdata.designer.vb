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


<Global.System.Data.Linq.Mapping.DatabaseAttribute(Name:="Portal.BI.RawData")>  _
Partial Public Class DataClasses_PortalBIRawdataDataContext
	Inherits System.Data.Linq.DataContext
	
	Private Shared mappingSource As System.Data.Linq.Mapping.MappingSource = New AttributeMappingSource()
	
  #Region "Extensibility Method Definitions"
  Partial Private Sub OnCreated()
  End Sub
  #End Region
	
	Public Sub New()
		MyBase.New(Global.System.Configuration.ConfigurationManager.ConnectionStrings("PortalBIRawDataConnection").ConnectionString, mappingSource)
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
	
	Public ReadOnly Property V_APDRenewal_RAWDATAs() As System.Data.Linq.Table(Of V_APDRenewal_RAWDATA)
		Get
			Return Me.GetTable(Of V_APDRenewal_RAWDATA)
		End Get
	End Property
End Class

<Global.System.Data.Linq.Mapping.TableAttribute(Name:="dbo.V_APDRenewal_RAWDATA")>  _
Partial Public Class V_APDRenewal_RAWDATA
	
	Private _ID As Integer
	
	Private _LOTNO As Integer
	
	Private _NO As Integer
	
	Private _ExpiredDate As System.Nullable(Of Date)
	
	Private _Officer As String
	
	Private _OfficerForSort As String
	
	Private _AE_Name As String
	
	Private _AE_Group_Running As System.Nullable(Of Integer)
	
	Private _AE_Group_Name_II As String
	
	Private _AES_Group_Running As System.Nullable(Of Integer)
	
	Private _AES_Group_Name As String
	
	Private _Business_Group As String
	
	Private _Business_GroupF As String
	
	Private _Class_Group As String
	
	Private _Division_Code As System.Nullable(Of Integer)
	
	Private _Type As String
	
	Private _Group As String
	
	Private _Code As String
	
	Private _Insured As String
	
	Private _Insurer As String
	
	Private _PolicyNo As String
	
	Private _SumInsured As System.Nullable(Of Double)
	
	Private _ExpiringPremium As System.Nullable(Of Double)
	
	Private _ReminderDate As String
	
	Private _ResponseDate As String
	
	Private _RenewalPremium As String
	
	Private _RemarkFromPolicy As String
	
	Private _RemarkFromAE As String
	
	Private _IsMatch As System.Nullable(Of Integer)
	
	Private _RenewFlag As String
	
	Private _TransType As String
	
	Private _RiskGroupI As String
	
	Private _Class_of_Risk__Sibis_ As String
	
	Private _New_PolicyNo As String
	
	Private _PeriodFrom As System.Nullable(Of Date)
	
	Private _PeriodTo As System.Nullable(Of Date)
	
	Private _DNDate As System.Nullable(Of Date)
	
	Private _InvoiceNo As System.Nullable(Of Integer)
	
	Private _Movement_Status As System.Nullable(Of Integer)
	
	Private _Premium As System.Nullable(Of Double)
	
	Private _StampDuty As System.Nullable(Of Double)
	
	Private _VAT As System.Nullable(Of Double)
	
	Private _TotalPremiumTHB As System.Nullable(Of Double)
	
	Private _TotalIncome As System.Nullable(Of Decimal)
	
	Private _NetIncome As System.Nullable(Of Decimal)
	
	Private _FiscalYear As System.Nullable(Of Integer)
	
	Private _Status As String
	
	Private _ClientCount As System.Nullable(Of Long)
	
	Public Sub New()
		MyBase.New
	End Sub
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ID", AutoSync:=AutoSync.Always, DbType:="Int NOT NULL IDENTITY", IsDbGenerated:=true)>  _
	Public Property ID() As Integer
		Get
			Return Me._ID
		End Get
		Set
			If ((Me._ID = value)  _
						= false) Then
				Me._ID = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_LOTNO", DbType:="Int NOT NULL")>  _
	Public Property LOTNO() As Integer
		Get
			Return Me._LOTNO
		End Get
		Set
			If ((Me._LOTNO = value)  _
						= false) Then
				Me._LOTNO = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_NO", DbType:="Int NOT NULL")>  _
	Public Property NO() As Integer
		Get
			Return Me._NO
		End Get
		Set
			If ((Me._NO = value)  _
						= false) Then
				Me._NO = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ExpiredDate", DbType:="DateTime")>  _
	Public Property ExpiredDate() As System.Nullable(Of Date)
		Get
			Return Me._ExpiredDate
		End Get
		Set
			If (Me._ExpiredDate.Equals(value) = false) Then
				Me._ExpiredDate = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Officer", DbType:="NVarChar(255)")>  _
	Public Property Officer() As String
		Get
			Return Me._Officer
		End Get
		Set
			If (String.Equals(Me._Officer, value) = false) Then
				Me._Officer = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_OfficerForSort", DbType:="NVarChar(255)")>  _
	Public Property OfficerForSort() As String
		Get
			Return Me._OfficerForSort
		End Get
		Set
			If (String.Equals(Me._OfficerForSort, value) = false) Then
				Me._OfficerForSort = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Name:="[AE Name]", Storage:="_AE_Name", DbType:="VarChar(50)")>  _
	Public Property AE_Name() As String
		Get
			Return Me._AE_Name
		End Get
		Set
			If (String.Equals(Me._AE_Name, value) = false) Then
				Me._AE_Name = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Name:="[AE Group Running]", Storage:="_AE_Group_Running", DbType:="Int")>  _
	Public Property AE_Group_Running() As System.Nullable(Of Integer)
		Get
			Return Me._AE_Group_Running
		End Get
		Set
			If (Me._AE_Group_Running.Equals(value) = false) Then
				Me._AE_Group_Running = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Name:="[AE Group Name II]", Storage:="_AE_Group_Name_II", DbType:="VarChar(50)")>  _
	Public Property AE_Group_Name_II() As String
		Get
			Return Me._AE_Group_Name_II
		End Get
		Set
			If (String.Equals(Me._AE_Group_Name_II, value) = false) Then
				Me._AE_Group_Name_II = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Name:="[AES Group Running]", Storage:="_AES_Group_Running", DbType:="Int")>  _
	Public Property AES_Group_Running() As System.Nullable(Of Integer)
		Get
			Return Me._AES_Group_Running
		End Get
		Set
			If (Me._AES_Group_Running.Equals(value) = false) Then
				Me._AES_Group_Running = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Name:="[AES Group Name]", Storage:="_AES_Group_Name", DbType:="VarChar(50)")>  _
	Public Property AES_Group_Name() As String
		Get
			Return Me._AES_Group_Name
		End Get
		Set
			If (String.Equals(Me._AES_Group_Name, value) = false) Then
				Me._AES_Group_Name = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Name:="[Business Group]", Storage:="_Business_Group", DbType:="VarChar(50)")>  _
	Public Property Business_Group() As String
		Get
			Return Me._Business_Group
		End Get
		Set
			If (String.Equals(Me._Business_Group, value) = false) Then
				Me._Business_Group = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Name:="[Business GroupF]", Storage:="_Business_GroupF", DbType:="VarChar(50)")>  _
	Public Property Business_GroupF() As String
		Get
			Return Me._Business_GroupF
		End Get
		Set
			If (String.Equals(Me._Business_GroupF, value) = false) Then
				Me._Business_GroupF = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Name:="[Class Group]", Storage:="_Class_Group", DbType:="VarChar(50)")>  _
	Public Property Class_Group() As String
		Get
			Return Me._Class_Group
		End Get
		Set
			If (String.Equals(Me._Class_Group, value) = false) Then
				Me._Class_Group = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Name:="[Division Code]", Storage:="_Division_Code", DbType:="Int")>  _
	Public Property Division_Code() As System.Nullable(Of Integer)
		Get
			Return Me._Division_Code
		End Get
		Set
			If (Me._Division_Code.Equals(value) = false) Then
				Me._Division_Code = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Type", DbType:="NVarChar(255)")>  _
	Public Property Type() As String
		Get
			Return Me._Type
		End Get
		Set
			If (String.Equals(Me._Type, value) = false) Then
				Me._Type = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Group", DbType:="NVarChar(255)")>  _
	Public Property [Group]() As String
		Get
			Return Me._Group
		End Get
		Set
			If (String.Equals(Me._Group, value) = false) Then
				Me._Group = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Code", DbType:="NVarChar(255)")>  _
	Public Property Code() As String
		Get
			Return Me._Code
		End Get
		Set
			If (String.Equals(Me._Code, value) = false) Then
				Me._Code = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Insured", DbType:="NVarChar(255)")>  _
	Public Property Insured() As String
		Get
			Return Me._Insured
		End Get
		Set
			If (String.Equals(Me._Insured, value) = false) Then
				Me._Insured = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Insurer", DbType:="NVarChar(255)")>  _
	Public Property Insurer() As String
		Get
			Return Me._Insurer
		End Get
		Set
			If (String.Equals(Me._Insurer, value) = false) Then
				Me._Insurer = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_PolicyNo", DbType:="NVarChar(255)")>  _
	Public Property PolicyNo() As String
		Get
			Return Me._PolicyNo
		End Get
		Set
			If (String.Equals(Me._PolicyNo, value) = false) Then
				Me._PolicyNo = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_SumInsured", DbType:="Float")>  _
	Public Property SumInsured() As System.Nullable(Of Double)
		Get
			Return Me._SumInsured
		End Get
		Set
			If (Me._SumInsured.Equals(value) = false) Then
				Me._SumInsured = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ExpiringPremium", DbType:="Float")>  _
	Public Property ExpiringPremium() As System.Nullable(Of Double)
		Get
			Return Me._ExpiringPremium
		End Get
		Set
			If (Me._ExpiringPremium.Equals(value) = false) Then
				Me._ExpiringPremium = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ReminderDate", DbType:="NVarChar(255)")>  _
	Public Property ReminderDate() As String
		Get
			Return Me._ReminderDate
		End Get
		Set
			If (String.Equals(Me._ReminderDate, value) = false) Then
				Me._ReminderDate = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ResponseDate", DbType:="NVarChar(255)")>  _
	Public Property ResponseDate() As String
		Get
			Return Me._ResponseDate
		End Get
		Set
			If (String.Equals(Me._ResponseDate, value) = false) Then
				Me._ResponseDate = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_RenewalPremium", DbType:="NVarChar(255)")>  _
	Public Property RenewalPremium() As String
		Get
			Return Me._RenewalPremium
		End Get
		Set
			If (String.Equals(Me._RenewalPremium, value) = false) Then
				Me._RenewalPremium = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_RemarkFromPolicy", DbType:="NText", UpdateCheck:=UpdateCheck.Never)>  _
	Public Property RemarkFromPolicy() As String
		Get
			Return Me._RemarkFromPolicy
		End Get
		Set
			If (String.Equals(Me._RemarkFromPolicy, value) = false) Then
				Me._RemarkFromPolicy = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_RemarkFromAE", DbType:="NText", UpdateCheck:=UpdateCheck.Never)>  _
	Public Property RemarkFromAE() As String
		Get
			Return Me._RemarkFromAE
		End Get
		Set
			If (String.Equals(Me._RemarkFromAE, value) = false) Then
				Me._RemarkFromAE = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_IsMatch", DbType:="Int")>  _
	Public Property IsMatch() As System.Nullable(Of Integer)
		Get
			Return Me._IsMatch
		End Get
		Set
			If (Me._IsMatch.Equals(value) = false) Then
				Me._IsMatch = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_RenewFlag", DbType:="VarChar(1)")>  _
	Public Property RenewFlag() As String
		Get
			Return Me._RenewFlag
		End Get
		Set
			If (String.Equals(Me._RenewFlag, value) = false) Then
				Me._RenewFlag = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_TransType", DbType:="VarChar(2)")>  _
	Public Property TransType() As String
		Get
			Return Me._TransType
		End Get
		Set
			If (String.Equals(Me._TransType, value) = false) Then
				Me._TransType = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_RiskGroupI", DbType:="VarChar(8)")>  _
	Public Property RiskGroupI() As String
		Get
			Return Me._RiskGroupI
		End Get
		Set
			If (String.Equals(Me._RiskGroupI, value) = false) Then
				Me._RiskGroupI = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Name:="[Class of Risk (Sibis)]", Storage:="_Class_of_Risk__Sibis_", DbType:="VarChar(8)")>  _
	Public Property Class_of_Risk__Sibis_() As String
		Get
			Return Me._Class_of_Risk__Sibis_
		End Get
		Set
			If (String.Equals(Me._Class_of_Risk__Sibis_, value) = false) Then
				Me._Class_of_Risk__Sibis_ = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Name:="[New PolicyNo]", Storage:="_New_PolicyNo", DbType:="VarChar(70)")>  _
	Public Property New_PolicyNo() As String
		Get
			Return Me._New_PolicyNo
		End Get
		Set
			If (String.Equals(Me._New_PolicyNo, value) = false) Then
				Me._New_PolicyNo = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_PeriodFrom", DbType:="DateTime")>  _
	Public Property PeriodFrom() As System.Nullable(Of Date)
		Get
			Return Me._PeriodFrom
		End Get
		Set
			If (Me._PeriodFrom.Equals(value) = false) Then
				Me._PeriodFrom = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_PeriodTo", DbType:="DateTime")>  _
	Public Property PeriodTo() As System.Nullable(Of Date)
		Get
			Return Me._PeriodTo
		End Get
		Set
			If (Me._PeriodTo.Equals(value) = false) Then
				Me._PeriodTo = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_DNDate", DbType:="DateTime")>  _
	Public Property DNDate() As System.Nullable(Of Date)
		Get
			Return Me._DNDate
		End Get
		Set
			If (Me._DNDate.Equals(value) = false) Then
				Me._DNDate = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_InvoiceNo", DbType:="Int")>  _
	Public Property InvoiceNo() As System.Nullable(Of Integer)
		Get
			Return Me._InvoiceNo
		End Get
		Set
			If (Me._InvoiceNo.Equals(value) = false) Then
				Me._InvoiceNo = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Name:="[Movement Status]", Storage:="_Movement_Status", DbType:="Int")>  _
	Public Property Movement_Status() As System.Nullable(Of Integer)
		Get
			Return Me._Movement_Status
		End Get
		Set
			If (Me._Movement_Status.Equals(value) = false) Then
				Me._Movement_Status = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Premium", DbType:="Float")>  _
	Public Property Premium() As System.Nullable(Of Double)
		Get
			Return Me._Premium
		End Get
		Set
			If (Me._Premium.Equals(value) = false) Then
				Me._Premium = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_StampDuty", DbType:="Float")>  _
	Public Property StampDuty() As System.Nullable(Of Double)
		Get
			Return Me._StampDuty
		End Get
		Set
			If (Me._StampDuty.Equals(value) = false) Then
				Me._StampDuty = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_VAT", DbType:="Float")>  _
	Public Property VAT() As System.Nullable(Of Double)
		Get
			Return Me._VAT
		End Get
		Set
			If (Me._VAT.Equals(value) = false) Then
				Me._VAT = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_TotalPremiumTHB", DbType:="Float")>  _
	Public Property TotalPremiumTHB() As System.Nullable(Of Double)
		Get
			Return Me._TotalPremiumTHB
		End Get
		Set
			If (Me._TotalPremiumTHB.Equals(value) = false) Then
				Me._TotalPremiumTHB = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_TotalIncome", DbType:="Decimal(21,2)")>  _
	Public Property TotalIncome() As System.Nullable(Of Decimal)
		Get
			Return Me._TotalIncome
		End Get
		Set
			If (Me._TotalIncome.Equals(value) = false) Then
				Me._TotalIncome = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_NetIncome", DbType:="Decimal(26,2)")>  _
	Public Property NetIncome() As System.Nullable(Of Decimal)
		Get
			Return Me._NetIncome
		End Get
		Set
			If (Me._NetIncome.Equals(value) = false) Then
				Me._NetIncome = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_FiscalYear", DbType:="Int")>  _
	Public Property FiscalYear() As System.Nullable(Of Integer)
		Get
			Return Me._FiscalYear
		End Get
		Set
			If (Me._FiscalYear.Equals(value) = false) Then
				Me._FiscalYear = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Status", DbType:="VarChar(15)")>  _
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
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ClientCount", DbType:="BigInt")>  _
	Public Property ClientCount() As System.Nullable(Of Long)
		Get
			Return Me._ClientCount
		End Get
		Set
			If (Me._ClientCount.Equals(value) = false) Then
				Me._ClientCount = value
			End If
		End Set
	End Property
End Class
