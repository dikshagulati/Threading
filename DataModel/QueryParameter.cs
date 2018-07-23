using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebApplication1.DataModel
{
    [Serializable]
    [DataContract]
    public class QueryParameter
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string value { get; set; }
    }
}
