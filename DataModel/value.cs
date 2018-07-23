using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebApplication1.DataModel
{
    [DataContract]
    public class SimpleResult<T>
    {
        [DataMember(IsRequired = true)]
        public T value { get; set; }
    }
}
