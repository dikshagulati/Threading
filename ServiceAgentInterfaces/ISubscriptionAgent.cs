using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WebApplication1.ServiceAgentInterfaces
{
    [ServiceContract]
    public partial interface ISubscriptionAgent
    {
        [OperationContract]
        [HttpGet("?subscriptionId={subscriptionId}")]
        List<Subscription> searchSubscriptions(string subscriptionId);


    }
}
