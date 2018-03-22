using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorClaimRESTService.Models
{
    public class PositionItem
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string DeviceId { get; set; }
        public DateTimeOffset StartTimestamp { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public double Accuracy { get; set; }
        public double AltitudeAccuracy { get; set; }
        public double Heading { get; set; }
        public double Speed { get; set; }
    }
}