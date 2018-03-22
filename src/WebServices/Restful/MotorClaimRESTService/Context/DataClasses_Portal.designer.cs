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

namespace MotorClaimRESTService.Context
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Portal")]
	public partial class DataClasses_PortalDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertMobile_Device_Profile(Mobile_Device_Profile instance);
    partial void UpdateMobile_Device_Profile(Mobile_Device_Profile instance);
    partial void DeleteMobile_Device_Profile(Mobile_Device_Profile instance);
    #endregion
		
		public DataClasses_PortalDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["PortalConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses_PortalDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses_PortalDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses_PortalDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses_PortalDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Mobile_Device_Profile> Mobile_Device_Profiles
		{
			get
			{
				return this.GetTable<Mobile_Device_Profile>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.[Mobile.Device.Profile]")]
	public partial class Mobile_Device_Profile : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _IDCard;
		
		private string _DeviceId;
		
		private string _FullName;
		
		private string _MobileNo;
		
		private string _Email;
		
		private System.Nullable<System.DateTime> _Timestamp;
		
		private string _Version;
		
		private string _ShortVersion;
		
		private string _CurrentCulture;
		
		private System.Nullable<bool> _IsBackgrounded;
		
		private System.Nullable<int> _ScreenHeight;
		
		private System.Nullable<int> _ScreenWidth;
		
		private string _Manufacturer;
		
		private string _Model;
		
		private string _OperatingSystem;
		
		private string _OperatingSystemVersion;
		
		private System.Nullable<bool> _IsSimulator;
		
		private System.Nullable<bool> _IsTablet;
		
		private System.Nullable<int> _WhenBatteryPercentageChanged;
		
		private System.Nullable<int> _WhenPowerStatusChanged;
		
		private System.Nullable<int> _InternetReachability;
		
		private string _CellularNetworkCarrier;
		
		private string _IpAddress;
		
		private string _WifiSsid;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDCardChanging(string value);
    partial void OnIDCardChanged();
    partial void OnDeviceIdChanging(string value);
    partial void OnDeviceIdChanged();
    partial void OnFullNameChanging(string value);
    partial void OnFullNameChanged();
    partial void OnMobileNoChanging(string value);
    partial void OnMobileNoChanged();
    partial void OnEmailChanging(string value);
    partial void OnEmailChanged();
    partial void OnTimestampChanging(System.Nullable<System.DateTime> value);
    partial void OnTimestampChanged();
    partial void OnVersionChanging(string value);
    partial void OnVersionChanged();
    partial void OnShortVersionChanging(string value);
    partial void OnShortVersionChanged();
    partial void OnCurrentCultureChanging(string value);
    partial void OnCurrentCultureChanged();
    partial void OnIsBackgroundedChanging(System.Nullable<bool> value);
    partial void OnIsBackgroundedChanged();
    partial void OnScreenHeightChanging(System.Nullable<int> value);
    partial void OnScreenHeightChanged();
    partial void OnScreenWidthChanging(System.Nullable<int> value);
    partial void OnScreenWidthChanged();
    partial void OnManufacturerChanging(string value);
    partial void OnManufacturerChanged();
    partial void OnModelChanging(string value);
    partial void OnModelChanged();
    partial void OnOperatingSystemChanging(string value);
    partial void OnOperatingSystemChanged();
    partial void OnOperatingSystemVersionChanging(string value);
    partial void OnOperatingSystemVersionChanged();
    partial void OnIsSimulatorChanging(System.Nullable<bool> value);
    partial void OnIsSimulatorChanged();
    partial void OnIsTabletChanging(System.Nullable<bool> value);
    partial void OnIsTabletChanged();
    partial void OnWhenBatteryPercentageChangedChanging(System.Nullable<int> value);
    partial void OnWhenBatteryPercentageChangedChanged();
    partial void OnWhenPowerStatusChangedChanging(System.Nullable<int> value);
    partial void OnWhenPowerStatusChangedChanged();
    partial void OnInternetReachabilityChanging(System.Nullable<int> value);
    partial void OnInternetReachabilityChanged();
    partial void OnCellularNetworkCarrierChanging(string value);
    partial void OnCellularNetworkCarrierChanged();
    partial void OnIpAddressChanging(string value);
    partial void OnIpAddressChanged();
    partial void OnWifiSsidChanging(string value);
    partial void OnWifiSsidChanged();
    #endregion
		
		public Mobile_Device_Profile()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IDCard", DbType="NVarChar(50) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string IDCard
		{
			get
			{
				return this._IDCard;
			}
			set
			{
				if ((this._IDCard != value))
				{
					this.OnIDCardChanging(value);
					this.SendPropertyChanging();
					this._IDCard = value;
					this.SendPropertyChanged("IDCard");
					this.OnIDCardChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DeviceId", DbType="NVarChar(150) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string DeviceId
		{
			get
			{
				return this._DeviceId;
			}
			set
			{
				if ((this._DeviceId != value))
				{
					this.OnDeviceIdChanging(value);
					this.SendPropertyChanging();
					this._DeviceId = value;
					this.SendPropertyChanged("DeviceId");
					this.OnDeviceIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FullName", DbType="NVarChar(255)")]
		public string FullName
		{
			get
			{
				return this._FullName;
			}
			set
			{
				if ((this._FullName != value))
				{
					this.OnFullNameChanging(value);
					this.SendPropertyChanging();
					this._FullName = value;
					this.SendPropertyChanged("FullName");
					this.OnFullNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MobileNo", DbType="NVarChar(50)")]
		public string MobileNo
		{
			get
			{
				return this._MobileNo;
			}
			set
			{
				if ((this._MobileNo != value))
				{
					this.OnMobileNoChanging(value);
					this.SendPropertyChanging();
					this._MobileNo = value;
					this.SendPropertyChanged("MobileNo");
					this.OnMobileNoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Email", DbType="NVarChar(150)")]
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
					this.OnEmailChanging(value);
					this.SendPropertyChanging();
					this._Email = value;
					this.SendPropertyChanged("Email");
					this.OnEmailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Timestamp", DbType="DateTime")]
		public System.Nullable<System.DateTime> Timestamp
		{
			get
			{
				return this._Timestamp;
			}
			set
			{
				if ((this._Timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._Timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Version", DbType="NVarChar(255)")]
		public string Version
		{
			get
			{
				return this._Version;
			}
			set
			{
				if ((this._Version != value))
				{
					this.OnVersionChanging(value);
					this.SendPropertyChanging();
					this._Version = value;
					this.SendPropertyChanged("Version");
					this.OnVersionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ShortVersion", DbType="NVarChar(255)")]
		public string ShortVersion
		{
			get
			{
				return this._ShortVersion;
			}
			set
			{
				if ((this._ShortVersion != value))
				{
					this.OnShortVersionChanging(value);
					this.SendPropertyChanging();
					this._ShortVersion = value;
					this.SendPropertyChanged("ShortVersion");
					this.OnShortVersionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CurrentCulture", DbType="NVarChar(255)")]
		public string CurrentCulture
		{
			get
			{
				return this._CurrentCulture;
			}
			set
			{
				if ((this._CurrentCulture != value))
				{
					this.OnCurrentCultureChanging(value);
					this.SendPropertyChanging();
					this._CurrentCulture = value;
					this.SendPropertyChanged("CurrentCulture");
					this.OnCurrentCultureChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsBackgrounded", DbType="Bit")]
		public System.Nullable<bool> IsBackgrounded
		{
			get
			{
				return this._IsBackgrounded;
			}
			set
			{
				if ((this._IsBackgrounded != value))
				{
					this.OnIsBackgroundedChanging(value);
					this.SendPropertyChanging();
					this._IsBackgrounded = value;
					this.SendPropertyChanged("IsBackgrounded");
					this.OnIsBackgroundedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ScreenHeight", DbType="Int")]
		public System.Nullable<int> ScreenHeight
		{
			get
			{
				return this._ScreenHeight;
			}
			set
			{
				if ((this._ScreenHeight != value))
				{
					this.OnScreenHeightChanging(value);
					this.SendPropertyChanging();
					this._ScreenHeight = value;
					this.SendPropertyChanged("ScreenHeight");
					this.OnScreenHeightChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ScreenWidth", DbType="Int")]
		public System.Nullable<int> ScreenWidth
		{
			get
			{
				return this._ScreenWidth;
			}
			set
			{
				if ((this._ScreenWidth != value))
				{
					this.OnScreenWidthChanging(value);
					this.SendPropertyChanging();
					this._ScreenWidth = value;
					this.SendPropertyChanged("ScreenWidth");
					this.OnScreenWidthChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Manufacturer", DbType="NVarChar(255)")]
		public string Manufacturer
		{
			get
			{
				return this._Manufacturer;
			}
			set
			{
				if ((this._Manufacturer != value))
				{
					this.OnManufacturerChanging(value);
					this.SendPropertyChanging();
					this._Manufacturer = value;
					this.SendPropertyChanged("Manufacturer");
					this.OnManufacturerChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Model", DbType="NVarChar(255)")]
		public string Model
		{
			get
			{
				return this._Model;
			}
			set
			{
				if ((this._Model != value))
				{
					this.OnModelChanging(value);
					this.SendPropertyChanging();
					this._Model = value;
					this.SendPropertyChanged("Model");
					this.OnModelChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OperatingSystem", DbType="NVarChar(255)")]
		public string OperatingSystem
		{
			get
			{
				return this._OperatingSystem;
			}
			set
			{
				if ((this._OperatingSystem != value))
				{
					this.OnOperatingSystemChanging(value);
					this.SendPropertyChanging();
					this._OperatingSystem = value;
					this.SendPropertyChanged("OperatingSystem");
					this.OnOperatingSystemChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OperatingSystemVersion", DbType="NVarChar(255)")]
		public string OperatingSystemVersion
		{
			get
			{
				return this._OperatingSystemVersion;
			}
			set
			{
				if ((this._OperatingSystemVersion != value))
				{
					this.OnOperatingSystemVersionChanging(value);
					this.SendPropertyChanging();
					this._OperatingSystemVersion = value;
					this.SendPropertyChanged("OperatingSystemVersion");
					this.OnOperatingSystemVersionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsSimulator", DbType="Bit")]
		public System.Nullable<bool> IsSimulator
		{
			get
			{
				return this._IsSimulator;
			}
			set
			{
				if ((this._IsSimulator != value))
				{
					this.OnIsSimulatorChanging(value);
					this.SendPropertyChanging();
					this._IsSimulator = value;
					this.SendPropertyChanged("IsSimulator");
					this.OnIsSimulatorChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsTablet", DbType="Bit")]
		public System.Nullable<bool> IsTablet
		{
			get
			{
				return this._IsTablet;
			}
			set
			{
				if ((this._IsTablet != value))
				{
					this.OnIsTabletChanging(value);
					this.SendPropertyChanging();
					this._IsTablet = value;
					this.SendPropertyChanged("IsTablet");
					this.OnIsTabletChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WhenBatteryPercentageChanged", DbType="Int")]
		public System.Nullable<int> WhenBatteryPercentageChanged
		{
			get
			{
				return this._WhenBatteryPercentageChanged;
			}
			set
			{
				if ((this._WhenBatteryPercentageChanged != value))
				{
					this.OnWhenBatteryPercentageChangedChanging(value);
					this.SendPropertyChanging();
					this._WhenBatteryPercentageChanged = value;
					this.SendPropertyChanged("WhenBatteryPercentageChanged");
					this.OnWhenBatteryPercentageChangedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WhenPowerStatusChanged", DbType="Int")]
		public System.Nullable<int> WhenPowerStatusChanged
		{
			get
			{
				return this._WhenPowerStatusChanged;
			}
			set
			{
				if ((this._WhenPowerStatusChanged != value))
				{
					this.OnWhenPowerStatusChangedChanging(value);
					this.SendPropertyChanging();
					this._WhenPowerStatusChanged = value;
					this.SendPropertyChanged("WhenPowerStatusChanged");
					this.OnWhenPowerStatusChangedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_InternetReachability", DbType="Int")]
		public System.Nullable<int> InternetReachability
		{
			get
			{
				return this._InternetReachability;
			}
			set
			{
				if ((this._InternetReachability != value))
				{
					this.OnInternetReachabilityChanging(value);
					this.SendPropertyChanging();
					this._InternetReachability = value;
					this.SendPropertyChanged("InternetReachability");
					this.OnInternetReachabilityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CellularNetworkCarrier", DbType="NVarChar(255)")]
		public string CellularNetworkCarrier
		{
			get
			{
				return this._CellularNetworkCarrier;
			}
			set
			{
				if ((this._CellularNetworkCarrier != value))
				{
					this.OnCellularNetworkCarrierChanging(value);
					this.SendPropertyChanging();
					this._CellularNetworkCarrier = value;
					this.SendPropertyChanged("CellularNetworkCarrier");
					this.OnCellularNetworkCarrierChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IpAddress", DbType="NVarChar(255)")]
		public string IpAddress
		{
			get
			{
				return this._IpAddress;
			}
			set
			{
				if ((this._IpAddress != value))
				{
					this.OnIpAddressChanging(value);
					this.SendPropertyChanging();
					this._IpAddress = value;
					this.SendPropertyChanged("IpAddress");
					this.OnIpAddressChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WifiSsid", DbType="NVarChar(255)")]
		public string WifiSsid
		{
			get
			{
				return this._WifiSsid;
			}
			set
			{
				if ((this._WifiSsid != value))
				{
					this.OnWifiSsidChanging(value);
					this.SendPropertyChanging();
					this._WifiSsid = value;
					this.SendPropertyChanged("WifiSsid");
					this.OnWifiSsidChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591