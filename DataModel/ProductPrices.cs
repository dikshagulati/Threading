using System.Runtime.Serialization;

namespace WebApplication1.DataModel
{
    [DataContract]
    public class ProductPrices
    {
        public enum EventCodeEnums { MTLY };

        [DataMember]
        public string eventCode { get; set; }

        [DataMember]
        public string priceType { get; set; }

        [DataMember]
        public decimal rate { get; set; }

        [DataMember]
        public string rateExclVat { get; set; }
    }
}