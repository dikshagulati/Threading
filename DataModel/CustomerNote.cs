using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebApplication1.DataModel
{
    [Serializable]
    [DataContract]
    public class CustomerNote
    {
        [DataMember]
        public long id { get; set; }

        [DataMember]
        public string svarsted { get; set; }

        [DataMember]
        public string contextId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public long tdcNoteId { get; set; }

        [DataMember]
        public string youseeNoteId { get; set; }

        [DataMember]
        public string lid { get; set; }

        [DataMember]
        public string created { get; set; }

        [DataMember]
        public string lastUpdated { get; set; }
        /// <summary>
        /// Size it limited to 16 bytes in BC
        /// </summary>
        [DataMember]
        public string sectionName { get; set; }

        [DataMember]
        public string systemName { get; set; }

        [DataMember]
        public string userId { get; set; }

        [DataMember]
        public string userName { get; set; }

        /// <summary>
        /// Size it limited to 8 bytes in BC -> We get error if more than 5 charactors
        /// </summary>
        [DataMember]
        public string userInitials { get; set; }

        /// <summary>
        /// Size it limited to 2048 bytes in BC
        /// </summary>
        [DataMember]
        public string note { get; set; }

        /// <summary>
        /// This is used to store SessionId or ArticleId
        /// </summary>
        [DataMember]
        public string entityId { get; set; }

        /// <summary>
        /// Guide/Article
        /// </summary>
        [DataMember]
        public string entityType { get; set; }

        /// <summary>
        /// The title of the guide/article
        /// </summary>
        [DataMember]
        public string entityTitle { get; set; }

        /// <summary>
        /// Step ID
        /// </summary>
        [DataMember]
        public string entityStep { get; set; }

        [DataMember]
        public string customerName { get; set; }

        /// <summary>
        /// Active, Parked, etc.
        /// </summary>
        [DataMember]
        public string status { get; set; }

        /// <summary>
        /// Used to save name of a guide. Note not the same as a title.
        /// </summary>
        [DataMember]
        public string entityName { get; set; }

        [DataMember]
        public string address { get; set; }
        [DataMember]
        public short callOriginKey { get; set; }
        [DataMember]
        public short callOriginSource { get; set; }
        [DataMember]
        public string city { get; set; }

        [DataMember]
        public string customerBan { get; set; }

        [DataMember]
        public string zip { get; set; }

        [DataMember]
        public List<AdditionalValue> additionalValues { get; set; }

        [DataMember(Name = "department")]
        public string departmentName { get; set; }
    }
}
