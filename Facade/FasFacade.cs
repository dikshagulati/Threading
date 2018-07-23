using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.DataModel;
using WebApplication1.ServiceAgentInterfaces;

namespace WebApplication1.Facade
{
    public class FasFacade
    {
        private readonly IFasAgent _fasAgent;

        public FasFacade()
        { }


        public async Task<OpenClosedFaultsStatusReply> FasFacade1()//IServiceLocator serviceLocator
        {
            //customerFacade = new CustomerFacade(serviceLocator);
            //_subscriptionAgent = serviceLocator.GetService<ISubscriptionAgent>();

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://bc13-new.test.tdc.dk");
                    client.DefaultRequestHeaders.Add("x-tdc-user-roles", "CIP_PORTAL");
                    client.DefaultRequestHeaders.Add("x-tdc-username", "m32321");
                    client.DefaultRequestHeaders.Add("x-tdc-has-migrated-to-yspro", "true");
                    client.DefaultRequestHeaders.Add("SSOID", "m32321");

                    var response = await client.GetAsync($"/bc/fas/requeststatus?callback=callback&requesttype=OPEN&daysback=0&queryby=LID&queryvalue=em120603&performlinecheck=false&checkval=foobar&checksum=5be1c53826f37d5912624fac6fadcb9a");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var streamResult= ConvertStringToStream(stringResult);
                    var rawWeather = MapStream<OpenClosedFaultsStatusReply>(streamResult);
                   
                    return rawWeather;

                }
                catch (HttpRequestException httpRequestException)
                {
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public T MapStream<T>(Stream requestStream)
        {
            var strResponse = new StreamReader(requestStream).ReadToEnd();

            int jsonStart = strResponse.IndexOf('{');
            int jsonEnd = strResponse.LastIndexOf('}') + 1;
            var strJsonResponse = strResponse.Substring(jsonStart, jsonEnd - jsonStart);//.Replace("\\", ""); //remove callback({someJson});
            try
            {
                JObject jResponse = null;
                try
                {
                    jResponse = JsonConvert.DeserializeObject<JObject>(strJsonResponse);
                }
                catch
                {
                    strJsonResponse = strResponse.Substring(jsonStart, jsonEnd - jsonStart);
                    jResponse = JsonConvert.DeserializeObject<JObject>(strJsonResponse);
                }

                if (jResponse.Properties().Any(p => p.Name == "error") && (int)jResponse["error"]["code"] != 81520)
                {
                    var errorData = new BusinessCoreExceptionData
                    {
                        Code = (int)jResponse["error"]["code"],
                        Message = (string)jResponse["error"]["message"],
                        StackTrace = (string)jResponse["error"]["stack trace"]
                    };

                    throw new BusinessCoreException(typeof(IFasAgent), errorData.Message, errorData);
                }

                return jResponse.ToObject<T>();
            }
            catch (JsonReaderException ex)
            {
                throw new BusinessCoreException(typeof(IFasAgent), "Response returned from FAS is not valid JSON:\n" + strResponse, ex);
            }
        }

        private Stream ConvertStringToStream(string str)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

    }
}
