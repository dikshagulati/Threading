using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using WebApplication1.DataModel;

namespace WebApplication1
{
    [DataContract]
    public class Subscription
    {
        [DataMember]
        public bool employee { get; set; }
        [DataMember]
        public DateTime expectedLastOrderCompleteDate { get; set; }
        [DataMember]
        public string externalId { get; set; }
        [DataMember]
        public string hotspotPassword { get; set; }
        [DataMember]
        public bool isSubscribedToAutoRefill { get; set; }
        [DataMember]
        public List<Product> products { get; set; }
        [DataMember]
        public long parentAccountNo { get; set; }
        [DataMember]
        public int parentBillFreq { get; set; }
        [DataMember]
        public string parentSegment { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public Party payer { get; set; }
        [DataMember]
        public bool paysForAccount { get; set; }
        [DataMember]
        public string rootLid { get; set; }
        [DataMember]
        public string subscriberStatus { get; set; }
        [DataMember]
        public string subscriberStatusDesc { get; set; }
        [DataMember]
        public string systemKey { get; set; }
        [DataMember]
        public Party user { get; set; }
        [DataMember]
        public Person person { get; set; }
        [DataMember]
        public bool foundInWholesale { get; set; }// Added for New FAS SMS tool
        [DataMember]
        public AdditionalParameter additionalParameter { get; set; }
        [DataMember]
        public List<Permissions> permissions { get; set; }
        [DataMember]
        public string columbusCustomerNumber { get; set; }

        #region Variables that are not coming from service
        public string portalsection { get; set; } // added to identify portal section
        public string madId { get; set; }
        public string zipCity { get; set; }
        public string fullAddress { get; set; }
        public bool IsEmergencyProcedure { get; set; }
        [DataMember]
        public string subscriptionId { get; set; }
        #endregion

    }
    [DataContract]
    public class AdditionalParameter
    {
        [DataMember]
        public string youseeSequence { get; set; }

        [DataMember]
        public long amsadrnr { get; set; }

        [DataMember]
        public long nodeId { get; set; }

        [DataMember]
        public long anelagId { get; set; }
    }
    [DataContract]
    public class Permissions
    {
        [DataMember]
        public string permissionItemId { get; set; }

        [DataMember]
        public string permissionParamValue { get; set; }
    }
}
