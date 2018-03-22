using System.Runtime.Serialization;
using System;
namespace MotorClaimRESTService.Models
{
   
    public class GarageItem
    {
        public int? ID { get; set; }
        public string GarageCode { get; set; }
        public string GarageName { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string ZipCode { get; set; }
        public string TelCode { get; set; }
        public string TelNo { get; set; }
        public string Mobile { get; set; }
        public string FaxNo { get; set; }
        public string Email { get; set; }
        public string Region { get; set; }
        public string SalesArea { get; set; }
        public int? GarageType { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; } 

        //public string ID { get; set; }
        //public string Name { get; set; }
        //public string Notes { get; set; }
        //public bool Done { get; set; }

    }
}