using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Build2017.IoT.Hackathon.Models
{
    public class DeviceData : TableEntity {
        public string deviceId { get; set; }
        public long messageId { get; set; }

        public long temperature { get; set; }
        public long MinTemp { get; set; }
        public long MaxTemp { get; set; }
        public long humidity { get; set; }
        public long MinHumid { get; set; }
        public long MaxHumid { get; set; }
        public bool Dropped { get; set; }
        public bool TempTripped { get; set; }
        public bool Tipped { get; set; }


    }
}