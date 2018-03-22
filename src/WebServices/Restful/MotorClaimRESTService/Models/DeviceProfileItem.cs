using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MotorClaimRESTService.Models
{
    public class DeviceProfileItem
    {
        //public string ID { get; set; }
        public string IDCard { get; set; }
        public string FullName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }

        public DateTime? Timestamp { get; set; }

        //AppInfo
        public string Version { get; set; }
        public string ShortVersion { get; set; }
        public string CurrentCulture { get; set; }
        public bool? IsBackgrounded { get; set; }

        //HardwareInfo
        public int? ScreenHeight { get; set; }
        public int? ScreenWidth { get; set; }
        public string DeviceId { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string OperatingSystem { get; set; }
        public string OperatingSystemVersion { get; set; }
        public bool? IsSimulator { get; set; }
        public bool? IsTablet { get; set; }

        //BatteryInfo
        public int? WhenBatteryPercentageChanged { get; set; }
        public int? WhenPowerStatusChanged { get; set; }
        // Unknown = 0,
        // Charging = 1,
        // Charged = 2,
        // NoBattery = 3,
        // Discharging = 4

        //INetworkInfo
        public int? InternetReachability { get; set; }
        // Unknown = 0,
        //NotReachable = 1,
        //Cellular = 2,
        //Wifi = 3,
        //Other = 4
        public string CellularNetworkCarrier { get; set; }
        public string IpAddress { get; set; }
        public string WifiSsid { get; set; }

    }
}