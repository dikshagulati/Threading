using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebApplication1.DataModel
{
    [Serializable]
    [DataContract]
    public class FASO
    {
        [DataMember(Name = "AfledtMark")]
        public string afledtMark { get; set; }

        [DataMember(Name = "BookingCalendar")]
        public string bookingCalendar { get; set; }

        [DataMember(Name = "BookingDate")]
        public DateTime bookingDate { get; set; }

        [DataMember(Name = "BookingTime")]
        public string bookingTime { get; set; }

        [DataMember(Name = "BookingWeekday")]
        public string bookingWeekday { get; set; }

        [DataMember(Name = "CancelAllowed")]
        public string cancelAllowed { get; set; }

        [DataMember(Name = "CancelCode")]
        public string cancelCode { get; set; }

        [DataMember(Name = "CustomerSegment")]
        public string customerSegment { get; set; }

        [DataMember(Name = "DIExpectedEndDate")]
        public string diExpectedEndDate { get; set; }

        [DataMember(Name = "DIFASOID")]
        public string difasoid { get; set; }

        [DataMember(Name = "DINextStatus")]
        public string diNextStatus { get; set; }

        [DataMember(Name = "FASOCondition")]
        public string fasoCondition { get; set; }

        [DataMember(Name = "FASOEjerArbjGrp")]
        public string fasoEjerArbjGrp { get; set; }

        [DataMember(Name = "FeedbackSMSContactNumber")]
        public string feedbackSMSContactNumber { get; set; }

        [DataMember(Name = "FejlStatus")]
        public string fejlStatus { get; set; }

        [DataMember(Name = "Modtagekode")]
        public string modtagekode { get; set; }

        [DataMember(Name = "MSOSContactNumber")]
        public string msosContactNumber { get; set; }

        [DataMember(Name = "OnsiteTime")]
        public DateTime onsiteTime { get; set; }

        [DataMember(Name = "OprettelsesTidpunkt")]
        public string oprettelsesTidpunkt { get; set; }

        [DataMember(Name = "OwnerFirst")]
        public string ownerFirst { get; set; }

        [DataMember(Name = "OwnerId")]
        public string ownerId { get; set; }

        [DataMember(Name = "OwnerLast")]
        public string ownerLast { get; set; }

        [DataMember(Name = "OwnerPhone")]
        public string ownerPhone { get; set; }

        [DataMember(Name = "rebookingAllowed")]
        public string rebookingAllowed { get; set; }

        [DataMember(Name = "RemoteTime")]
        public DateTime remoteTime { get; set; }

        [DataMember(Name = "Status")]
        public string status { get; set; }

        [DataMember(Name = "Technology")]
        public string technology { get; set; }

        [DataMember(Name = "X-Msg-Sms")]
        public string xMsgSms { get; set; }

        [DataMember(Name = "X-No-Close")]
        public string xNoClose { get; set; }

        [DataMember(Name = "X-Work-Complete")]
        public string xWorkComplete { get; set; }

        [DataMember(Name = "FASOID")]
        public string fasoid { get; set; }

        [DataMember(Name = "Type")]
        public string type { get; set; }

        [DataMember(Name = "FaultDescription")]
        public string faultDescription { get; set; }

        [DataMember(Name = "FaultCorrectionTime")]
        public string faultCorrectionTime { get; set; }

        [DataMember(Name = "SMSStatus")]
        public string smsStatus { get; set; }

        [DataMember(Name = "ToBePossible")]
        public string toBePossible { get; set; }

        // new changes add SLA_name and SLA_text for  Service option to know which is current service option seleted from multiple options. 
        [DataMember(Name = "SLA_name")]
        public string SLA_name { get; set; }

        [DataMember(Name = "SLA_text")]
        public string SLA_text { get; set; }

        [DataMember(Name = "noteHistory")]
        public string noteHistory { get; set; }

        [DataMember(Name = "waitingPosition")]
        public string waitingPosition { get; set; }

        [DataMember(Name = "waitingReason")]
        public string waitingReason { get; set; }


    }
}
