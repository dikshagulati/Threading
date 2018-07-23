using System.Runtime.Serialization;
namespace WebApplication1.DataModel
{
    [DataContract]
    public class Person
    {
        [DataMember]
        public string firstName { get; set; }
        [DataMember]
        public string lastName { get; set; }
    }
}
