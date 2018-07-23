using System.Collections.Generic;
using System.Runtime.Serialization;

// ReSharper disable InconsistentNaming
namespace WebApplication1.DataModel
{
    [DataContract]
    public class Product
    {
        [DataMember]
        public string productName { get; set; }

        [DataMember]
        public string productCode { get; set; }

        [DataMember]
        public List<ProductPrices> prices { get; set; } 

        [DataMember]
        public List<ProductRelations> productRelations { get; set; }

        [DataMember]
        public List<ProductParameter> productParameters { get; set; }

        [DataMember]
        public bool hasService { get; set; }

    }
}
