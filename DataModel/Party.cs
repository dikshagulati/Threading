using System.Runtime.Serialization;

namespace WebApplication1.DataModel
{
    [DataContract]
    public class Party
    {

        /*
            They are both Party objects, but both Person and Organization derive from Party. 
            Organization has a property called "name" and Person has "firstName" and "lastName"
         */
        [DataMember(EmitDefaultValue = false)]
        public string additionalAddress { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string apartmentNumber { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string attentionTo { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string city { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string co { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string country { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string customerNo { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string emailAddress { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string floor { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string floorside { get; set; }
        [DataMember(EmitDefaultValue = false)] // TODO: TASK 436 - added becasue we are getting it from BC
        public string firstName { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string houseLetter { get; set; }
        [DataMember(EmitDefaultValue = false)] // TODO: TASK 436 - added becasue we are getting it from BC
        public string lastName { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string linkItID { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string mobilePhoneNumber { get; set; } //althought its documented in D180, its always empty
        [DataMember(EmitDefaultValue = false)]
        public string nameAddressId { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string placeName { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string postbox { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string street { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string streetnumber { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string zipCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string name { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string customerHierarchyType { get; set; }
    }
}
