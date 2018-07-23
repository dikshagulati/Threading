using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using WebApplication1.DataModel;

namespace WebApplication1.ServiceAgentInterfaces
{
    [ServiceContract]
    public partial interface IFasAgent
    {
        [OperationContract]
        [HttpGet("/fas/requeststatus?callback={callback}&requesttype={type}&daysback={daysBack}&queryby={queryBy}&queryvalue={val}&performlinecheck={performlinecheck}&checkval=foobar&checksum=5be1c53826f37d5912624fac6fadcb9a")]
        Stream requestStatusOnOpenClosedFault(string val, string queryBy, int daysBack, string type, string callback, bool performlinecheck);

    }
}
