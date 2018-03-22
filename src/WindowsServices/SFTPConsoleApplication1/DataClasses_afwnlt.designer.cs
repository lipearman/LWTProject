﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SFTPConsoleApplication1
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="nltdb")]
	public partial class DataClasses_afwnltDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public DataClasses_afwnltDataContext() : 
				base(global::SFTPConsoleApplication1.Properties.Settings.Default.nltdbConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses_afwnltDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses_afwnltDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses_afwnltDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses_afwnltDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<vw_RetailSalesJP> vw_RetailSalesJPs
		{
			get
			{
				return this.GetTable<vw_RetailSalesJP>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.vw_RetailSalesJP")]
	public partial class vw_RetailSalesJP
	{
		
		private int _AID;
		
		private System.Nullable<System.DateTime> _ClosingDate;
		
		private string _TITLE;
		
		private string _FIRST_NAME;
		
		private string _LAST_NAME;
		
		private string _CUSTOMER_TYPE;
		
		private string _ID_CARD;
		
		private string _TAX_ID_NO;
		
		private string _MOBILE_NO;
		
		private string _HOME_NO;
		
		private string _OFFICE_NO;
		
		private string _Email;
		
		private string _CONTACT_PERSON;
		
		private string _CONTACT_ADDRESS;
		
		private string _POSTAL_ADDRESS;
		
		private string _VIN;
		
		private string _CLIENT_CODE;
		
		private string _CLOSING_DATE;
		
		private System.Nullable<System.DateTime> _AFWMakeDate;
		
		public vw_RetailSalesJP()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AID", DbType="Int NOT NULL")]
		public int AID
		{
			get
			{
				return this._AID;
			}
			set
			{
				if ((this._AID != value))
				{
					this._AID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ClosingDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> ClosingDate
		{
			get
			{
				return this._ClosingDate;
			}
			set
			{
				if ((this._ClosingDate != value))
				{
					this._ClosingDate = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TITLE", DbType="VarChar(32)")]
		public string TITLE
		{
			get
			{
				return this._TITLE;
			}
			set
			{
				if ((this._TITLE != value))
				{
					this._TITLE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FIRST_NAME", DbType="VarChar(50)")]
		public string FIRST_NAME
		{
			get
			{
				return this._FIRST_NAME;
			}
			set
			{
				if ((this._FIRST_NAME != value))
				{
					this._FIRST_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LAST_NAME", DbType="VarChar(50)")]
		public string LAST_NAME
		{
			get
			{
				return this._LAST_NAME;
			}
			set
			{
				if ((this._LAST_NAME != value))
				{
					this._LAST_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CUSTOMER_TYPE", DbType="VarChar(1) NOT NULL", CanBeNull=false)]
		public string CUSTOMER_TYPE
		{
			get
			{
				return this._CUSTOMER_TYPE;
			}
			set
			{
				if ((this._CUSTOMER_TYPE != value))
				{
					this._CUSTOMER_TYPE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID_CARD", DbType="VarChar(50)")]
		public string ID_CARD
		{
			get
			{
				return this._ID_CARD;
			}
			set
			{
				if ((this._ID_CARD != value))
				{
					this._ID_CARD = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TAX_ID_NO", DbType="VarChar(1) NOT NULL", CanBeNull=false)]
		public string TAX_ID_NO
		{
			get
			{
				return this._TAX_ID_NO;
			}
			set
			{
				if ((this._TAX_ID_NO != value))
				{
					this._TAX_ID_NO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MOBILE_NO", DbType="VarChar(50)")]
		public string MOBILE_NO
		{
			get
			{
				return this._MOBILE_NO;
			}
			set
			{
				if ((this._MOBILE_NO != value))
				{
					this._MOBILE_NO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HOME_NO", DbType="VarChar(50)")]
		public string HOME_NO
		{
			get
			{
				return this._HOME_NO;
			}
			set
			{
				if ((this._HOME_NO != value))
				{
					this._HOME_NO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OFFICE_NO", DbType="VarChar(50)")]
		public string OFFICE_NO
		{
			get
			{
				return this._OFFICE_NO;
			}
			set
			{
				if ((this._OFFICE_NO != value))
				{
					this._OFFICE_NO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Email", DbType="VarChar(1) NOT NULL", CanBeNull=false)]
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				if ((this._Email != value))
				{
					this._Email = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CONTACT_PERSON", DbType="VarChar(50)")]
		public string CONTACT_PERSON
		{
			get
			{
				return this._CONTACT_PERSON;
			}
			set
			{
				if ((this._CONTACT_PERSON != value))
				{
					this._CONTACT_PERSON = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CONTACT_ADDRESS", DbType="NVarChar(4000)")]
		public string CONTACT_ADDRESS
		{
			get
			{
				return this._CONTACT_ADDRESS;
			}
			set
			{
				if ((this._CONTACT_ADDRESS != value))
				{
					this._CONTACT_ADDRESS = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_POSTAL_ADDRESS", DbType="NVarChar(4000)")]
		public string POSTAL_ADDRESS
		{
			get
			{
				return this._POSTAL_ADDRESS;
			}
			set
			{
				if ((this._POSTAL_ADDRESS != value))
				{
					this._POSTAL_ADDRESS = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VIN", DbType="VarChar(50)")]
		public string VIN
		{
			get
			{
				return this._VIN;
			}
			set
			{
				if ((this._VIN != value))
				{
					this._VIN = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CLIENT_CODE", DbType="NVarChar(50)")]
		public string CLIENT_CODE
		{
			get
			{
				return this._CLIENT_CODE;
			}
			set
			{
				if ((this._CLIENT_CODE != value))
				{
					this._CLIENT_CODE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CLOSING_DATE", DbType="VarChar(10)")]
		public string CLOSING_DATE
		{
			get
			{
				return this._CLOSING_DATE;
			}
			set
			{
				if ((this._CLOSING_DATE != value))
				{
					this._CLOSING_DATE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AFWMakeDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> AFWMakeDate
		{
			get
			{
				return this._AFWMakeDate;
			}
			set
			{
				if ((this._AFWMakeDate != value))
				{
					this._AFWMakeDate = value;
				}
			}
		}
	}
}
#pragma warning restore 1591