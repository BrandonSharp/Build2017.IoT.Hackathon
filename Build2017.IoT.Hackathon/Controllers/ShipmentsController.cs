using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Build2017.IoT.Hackathon.Models;

namespace Build2017.IoT.Hackathon.Controllers
{
    [Route("api/[controller]")]
    public class ShipmentsController : Controller
    {
        static string iotHubConnectionstring = "HostName=bs-iot-hackathon.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=CruDa1E8fFYEuYFu2DHrzFW3rdA4IW8+jiRwD63vh6E=";
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=hackathon2017;AccountKey=5LktSt6p93x66DZQQm9yMi6CgZn/8uuI+X5Hk5biSa+sNqQfmHzQHH1drT0amWCAIsP+92yiR251GGa3FE7prQ==;EndpointSuffix=core.windows.net");
        public ShipmentsController() {

        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> GetShipmentIds() {
            return new string[] { "S001", "S002", "S003", "S004", "S005" };
        }

        [Route("devices")]
        [HttpGet]
        public IEnumerable<string> GetDeviceIds() {
            Microsoft.Azure.Devices.ServiceClient.CreateFromConnectionString(iotHubConnectionstring);
            return null;
        }

        [HttpPost]
        public async Task<bool> CreateShipment(ShipmentInformation shipInfo) {
            return true;
        }

        [Route("devices/{deviceId}")]
        [HttpGet]
        public IEnumerable<DeviceData> GetData(string deviceId) {
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("DeviceData");

            return null;
        }
    }
}
