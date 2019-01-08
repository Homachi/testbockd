using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pmsample.Models
{

    public class Station
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Location { get; set; }

    }
}
