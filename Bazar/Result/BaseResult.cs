using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazar.Result
{
    public class BaseResult
    {
        [JsonIgnore]
        public bool ProccessOk { get; set; }
        [JsonIgnore]
        public string MsgError { get; set; }
        [JsonIgnore]
        public string MsgCatch { get; set; }
    }
}
