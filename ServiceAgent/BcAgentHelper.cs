using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.ServiceModel;

using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

using WebApplication1.ServiceAgentInterfaces;

namespace WebApplication1.ServiceAgent
{
    public class BcAgentHelper
    {
        private readonly string _serviceUrlBase;
        public readonly bool _isActimoIntegrationAgent;

        private const string username = "m51073"; //"cip_test_001";
        private const string password = "Test1234";//"abc123";
        private const string ActimoApiKey = "9a77cbb3-dfc1-47a9-93af-8ac75587210d";


        public readonly Dictionary<Type, string> _serviceAreaMappings = new Dictionary<Type, string>
        {
            { typeof(ISubscriptionAgent), "subscription" },           
        };

        //ctor
        public BcAgentHelper(string serviceUrlBase)
        {
            if (serviceUrlBase == null) throw new ArgumentNullException("serviceUrlBase");
            _serviceUrlBase = serviceUrlBase;
        }

        //ctor
        public BcAgentHelper(string serviceUrlBase, bool isActimoIntegrationAgent)
        {
            if (serviceUrlBase == null) throw new ArgumentNullException("serviceUrlBase");
            _serviceUrlBase = serviceUrlBase;
            _isActimoIntegrationAgent = isActimoIntegrationAgent;
        }

        public T CreateChannel<T>(string url = null) where T : class
        {
            if (url == null)
            {
                url = System.IO.Path.Combine(_serviceUrlBase, GetServiceAreaName<T>());
            }

            var webHttpBinding = new MyWebHttpBinding();

           // var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress(new Uri(url));
            var channelFactory = new ChannelFactory<T>(webHttpBinding, endpoint);


            //var channelFactory = new  WebChannelFactory<T>(webHttpBinding, new Uri(url));
            foreach (OperationDescription op in channelFactory.Endpoint.Contract.Operations)
            {
                var dataContractBehavior = op.Behaviors.Find<DataContractSerializerOperationBehavior>();
                if (dataContractBehavior != null)
                {
                    dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                }
            }
            //set basic security
            if (url.StartsWith("https"))
            {
                webHttpBinding.Security.Mode = BasicHttpSecurityMode.Transport;
                ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) => true;  //Trust all certificates

                // trust sender
                //System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, cert, chain, errors) => cert.Subject.Contains("YourServerName"));

                // validate cert by calling a function
                //ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateRemoteCertificate);
            }
            else
            {
                webHttpBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            }
            webHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            webHttpBinding.AllowCookies = true;
            webHttpBinding.ReaderQuotas.MaxStringContentLength = 2147483647;
            webHttpBinding.ReaderQuotas.MaxArrayLength = 2147483647;
            webHttpBinding.ReaderQuotas.MaxBytesPerRead = 2147483647;
            webHttpBinding.ReaderQuotas.MaxNameTableCharCount = 2147483647;
            webHttpBinding.MaxReceivedMessageSize = 2147483647;
            webHttpBinding.MaxBufferPoolSize = 2147483647;
            webHttpBinding.MaxBufferSize = int.MaxValue;
            webHttpBinding.OpenTimeout = TimeSpan.FromSeconds(10);
            webHttpBinding.ReceiveTimeout = TimeSpan.FromSeconds(300);
            //Defect ID : 13847 - BC Timeout issue
            webHttpBinding.SendTimeout = TimeSpan.FromSeconds(300);
            //if (typeof (T) == typeof (IGuideAgent))
            //{
            //    webHttpBinding.ReceiveTimeout = TimeSpan.FromSeconds(300);
            //}
            //Defect ID : 13847 - BC Timeout issue

            ////use json by default
           // var webHttpBehaviour = new HttpHeaderEndPointBehavior<T>(); //.Find<WebHttpBehavior>();
            ////if (webHttpBehaviour == null)
           // channelFactory.Endpoint.EndpointBehaviors.Add(webHttpBehaviour); //( webHttpBehaviour = new WebHttpBehavior());
            //webHttpBehaviour.DefaultOutgoingRequestFormat =.Json;
            //webHttpBehaviour.DefaultOutgoingResponseFormat = WebMessageFormat.Json;

            //var httpHeaderEndPointBehavior = new HttpHeaderEndPointBehavior<T>();
            //httpHeaderEndPointBehavior.isActimoEndPoint = this._isActimoIntegrationAgent;

            //channelFactory.Endpoint.Behaviors.Add(httpHeaderEndPointBehavior);

            //set credentials.
            channelFactory.Credentials.UserName.UserName = username;
            channelFactory.Credentials.UserName.Password = password;

            return channelFactory.CreateChannel();
        }

        private string GetServiceAreaName<TAgent>()
        {
            string serviceArea;
            lock (_serviceAreaMappings)
            {
                if (!_serviceAreaMappings.TryGetValue(typeof(TAgent), out serviceArea))
                {
                    //apply namingconvention: IDeviceAgent returns device;
                    const string prefix = "I";
                    const string postfix = "Agent";
                    string agentName = typeof(TAgent).Name;
                    if (!agentName.StartsWith(prefix) || !agentName.EndsWith(postfix))
                    {
                        throw new InvalidOperationException("Type " + typeof(TAgent).FullName +
                                                            " is not registered in serviceAreaMappings and does not meet naming convention");
                    }
                    serviceArea = agentName
                        .Substring(prefix.Length, agentName.Length - prefix.Length - postfix.Length)
                        .ToLower();
                    _serviceAreaMappings.Add(typeof(TAgent), serviceArea);
                }
            }
            return serviceArea;
        }


        class HttpHeaderEndPointBehavior<T> : IEndpointBehavior
        {
            public bool isActimoEndPoint = false;
            public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
            {
            }

            public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
            {
                var inspector = new ClientMessageInspector<T>();
                inspector.isActimoInspector = isActimoEndPoint;
                clientRuntime.ClientMessageInspectors.Add(inspector);
            }

            public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
            {
            }

            public void Validate(ServiceEndpoint endpoint)
            {
            }
        }

        class ClientMessageInspector<T> : IClientMessageInspector
        {
            public bool isActimoInspector = false;
            public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
            {
                //var bodyReader = reply.GetReaderAtBodyContents();
                //byte[] bodyBytes = bodyReader.ReadElementContentAsBase64();
                //string messageBody = Encoding.UTF8.GetString(bodyBytes); 
            }

            public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel)
            {

                HttpRequestMessageProperty httpRequestMessage;
                object httpRequestMessageObject;
                //var principal = (TpCIPPrincipal)Thread.CurrentPrincipal;

                //if (typeof(T) == typeof(IActimoIntegrationAgent) || this.isActimoInspector)
                //{
                //    if (request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out httpRequestMessageObject))
                //    {
                //        httpRequestMessage = httpRequestMessageObject as HttpRequestMessageProperty;
                //        if (string.IsNullOrEmpty(httpRequestMessage.Headers["api-key"]))
                //        {
                //            httpRequestMessage.Headers["api-key"] = UserToolBox.GetActimoKey();
                //        }
                //    }
                //}
                //else
                //{
                    var userName = "m52081";
                    userName = userName.Split('\\').Last();
                    if (request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out httpRequestMessageObject))
                    {
                        httpRequestMessage = httpRequestMessageObject as HttpRequestMessageProperty;
                        if (string.IsNullOrEmpty(httpRequestMessage.Headers["x-tdc-user-roles"]))
                        {
                            httpRequestMessage.Headers["x-tdc-user-roles"] = "CIP_PORTAL";// PortalToolBox principal.PortalId.ToUpper() == "CIP" ? "CIP_PORTAL" : "FTW_PORTAL";
                        }
                        if (string.IsNullOrEmpty(httpRequestMessage.Headers["x-tdc-username"]))
                        {
                            httpRequestMessage.Headers["x-tdc-username"] = userName;
                        }
                        if (string.IsNullOrEmpty(httpRequestMessage.Headers["x-tdc-has-migrated-to-yspro"]))
                        {
                            httpRequestMessage.Headers["x-tdc-has-migrated-to-yspro"] = "true";
                        }
                        if (string.IsNullOrEmpty(httpRequestMessage.Headers["SSOID"]))
                        {
                            httpRequestMessage.Headers["SSOID"] = userName;
                        }
                        // Project 17561:Add IP Address Information in Headers
                        //if (string.IsNullOrEmpty(httpRequestMessage.Headers["x-tdc-remote-addr"]) && typeof(T) == typeof(IPaymentAgent))
                        //{
                        //    httpRequestMessage.Headers["x-tdc-remote-addr"] = GetClientIP();
                        //}
                    }
                    else
                    {
                        httpRequestMessage = new HttpRequestMessageProperty();
                        httpRequestMessage.Headers.Add("x-tdc-user-roles", "CIP_PORTAL");
                        httpRequestMessage.Headers.Add("x-tdc-username", userName);
                        httpRequestMessage.Headers.Add("x-tdc-has-migrated-to-yspro", "true");
                        httpRequestMessage.Headers.Add("SSOID", userName);
                        // Project 17561:Add IP Address Information in Headers
                        //if (typeof(T) == typeof(IPaymentAgent))
                        //{
                        //    httpRequestMessage.Headers.Add("x-tdc-remote-addr", GetClientIP());
                        //}
                        request.Properties.Add(HttpRequestMessageProperty.Name, httpRequestMessage);
                    }
                //}
                return null;
            }

            private string GetClientIP()
            {
                try
                {
                    string Str = "";
                    Str = System.Net.Dns.GetHostName();
                    IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(Str);
                    IPAddress[] addr = ipEntry.AddressList;
                    return addr[addr.Length - 1].ToString();
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }

        }

        //class RawWebContentTypeMapper : System.ServiceModel //WebContentTypeMapper
        //{
        //    public override WebContentFormat GetMessageFormatForContentType(string contentType)
        //    {
        //        return WebContentFormat.Raw;
        //    }
        //}

        private class MyWebHttpBinding : BasicHttpBinding //WebHttpBinding
        {
            //public WebContentTypeMapper ContentTypeMapper { get; set; }

            public override BindingElementCollection CreateBindingElements()
            {
                var elements = base.CreateBindingElements();
                //if (ContentTypeMapper != null)
                //{
                //    var WebMessageEncodingBindingElement = elements.Find<WebMessageEncodingBindingElement>();
                //    WebMessageEncodingBindingElement.ContentTypeMapper = this.ContentTypeMapper;
                //}
                return elements;
            }
        }
    }
}
