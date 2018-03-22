using System.Collections.Generic;
using System.Linq;
using MotorClaimRESTService.Models;

namespace MotorClaimRESTService.Services
{
    public interface IDeviceProfileRepository
    {
        bool DoesItemExist(string id);
        IEnumerable<DeviceProfileItem> Find(string id);
        void Insert(DeviceProfileItem item);
        //void Update(GarageItem item);
        //void Delete(string id);
    }
    public class DeviceProfileRepository : IDeviceProfileRepository
    {
        private List<DeviceProfileItem> _deviceptofile;

        public DeviceProfileRepository()
        {
            InitializeData();
        }

        public bool DoesItemExist(string id)
        {
            return _deviceptofile.Any(item => item.DeviceId == id);
        }

        public IEnumerable<DeviceProfileItem> Find(string id)
        {
            _deviceptofile = new List<DeviceProfileItem>();
            using (Context.DataClasses_PortalExt dc = new Context.DataClasses_PortalExt())
            {
                _deviceptofile = dc.Mobile_Device_Profiles.Where(i => i.DeviceId == id).Select(i => new DeviceProfileItem
                    {
                        IDCard = i.IDCard,
                        DeviceId = i.DeviceId,
                        FullName = i.FullName,
                        MobileNo = i.MobileNo,
                        Email = i.Email,
                        Timestamp = i.Timestamp,
                        Version = i.Version,
                        ShortVersion = i.ShortVersion,
                        CurrentCulture = i.CurrentCulture,
                        IsBackgrounded = i.IsBackgrounded,
                        ScreenHeight = i.ScreenHeight,
                        ScreenWidth = i.ScreenWidth,
                        Manufacturer = i.Manufacturer,
                        Model = i.Model,
                        OperatingSystem = i.OperatingSystem,
                        OperatingSystemVersion = i.OperatingSystemVersion,
                        IsSimulator = i.IsSimulator,
                        IsTablet = i.IsTablet,
                        WhenBatteryPercentageChanged = i.WhenBatteryPercentageChanged,
                        WhenPowerStatusChanged = i.WhenPowerStatusChanged,
                        InternetReachability = i.InternetReachability,
                        CellularNetworkCarrier = i.CellularNetworkCarrier,
                        IpAddress = i.IpAddress,
                        WifiSsid = i.WifiSsid,
                    }).ToList();
            }
            return _deviceptofile.Where(item => item.DeviceId == id).ToList();


        }

        public void Insert(DeviceProfileItem item)
        {
            _deviceptofile.Add(item);
            using (Context.DataClasses_PortalExt dc = new Context.DataClasses_PortalExt())
            {
                dc.Mobile_Device_Profiles.InsertOnSubmit(new Context.Mobile_Device_Profile
                {
                    IDCard = item.IDCard,
                    DeviceId = item.DeviceId,
                    FullName = item.FullName,
                    MobileNo = item.MobileNo,
                    Email = item.Email,
                    Timestamp = item.Timestamp,
                    Version = item.Version,
                    ShortVersion = item.ShortVersion,
                    CurrentCulture = item.CurrentCulture,
                    IsBackgrounded = item.IsBackgrounded,
                    ScreenHeight = item.ScreenHeight,
                    ScreenWidth = item.ScreenWidth,
                    Manufacturer = item.Manufacturer,
                    Model = item.Model,
                    OperatingSystem = item.OperatingSystem,
                    OperatingSystemVersion = item.OperatingSystemVersion,
                    IsSimulator = item.IsSimulator,
                    IsTablet = item.IsTablet,
                    WhenBatteryPercentageChanged = item.WhenBatteryPercentageChanged,
                    WhenPowerStatusChanged = item.WhenPowerStatusChanged,
                    InternetReachability = item.InternetReachability,
                    CellularNetworkCarrier = item.CellularNetworkCarrier,
                    IpAddress = item.IpAddress,
                    WifiSsid = item.WifiSsid,
                });
                dc.SubmitChanges();
            }
        }

        //public void Update(GarageItem item)
        //{
        //    var todoItem = this.Find(item.ID);
        //    var index = _garageList.IndexOf(todoItem);
        //    _garageList.RemoveAt(index);
        //    _garageList.Insert(index, item);
        //}

        //public void Delete(string id)
        //{
        //    _garageList.Remove(this.Find(id));
        //}

        #region Helpers

        private void InitializeData()
        {
            _deviceptofile = new List<DeviceProfileItem>();








        }

        #endregion
    }
}