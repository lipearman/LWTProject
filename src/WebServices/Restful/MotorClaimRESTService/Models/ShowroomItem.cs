using System.Runtime.Serialization;
namespace MotorClaimRESTService.Models
{
    [DataContract]
    public class ShowroomItem
    {
        public string ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Notes { get; set; }
        [DataMember]
        public bool Done { get; set; }
    }
}