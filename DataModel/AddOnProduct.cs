using System.Collections.Generic;
using System.Runtime.Serialization;

// ReSharper disable InconsistentNaming
namespace WebApplication1.DataModel
{
    [DataContract]
    public class AddOnProduct
    {
        [DataMember]
        public Product addOnProduct { get; set; }
    }
}
