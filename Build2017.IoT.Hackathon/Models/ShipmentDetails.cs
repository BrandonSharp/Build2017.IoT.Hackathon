using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Build2017.IoT.Hackathon.Models
{
    [DataContract]
    public class ShipmentDetails
    {
        [DataMember]
        public bool WasDropped { get; set; }
        [DataMember]
        public bool WasTipped { get; set; }
    }
}
