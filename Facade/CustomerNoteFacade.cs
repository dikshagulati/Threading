using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.DataModel;

namespace WebApplication1.Facade
{
    public class CustomerNoteFacade
    {
        public CustomerNoteFacade()
        { }

        public async Task<CustomerNote> CustomerNoteFacade1()
        {
            using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://bc3-new.test.tdc.dk");
                    client.DefaultRequestHeaders.Add("x-tdc-user-roles", "CIP_PORTAL");
                    client.DefaultRequestHeaders.Add("x-tdc-username", "m32321");
                    client.DefaultRequestHeaders.Add("x-tdc-has-migrated-to-yspro", "true");
                    client.DefaultRequestHeaders.Add("SSOID", "m32321");

                    var customerNote = CreateNoteInstance();

                    var myContent = JsonConvert.SerializeObject(customerNote);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await client.PostAsync($"/bc/customer/note", byteContent);
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawWeather = JsonConvert.DeserializeObject<CustomerNote>(stringResult);

                    return rawWeather;

                }
            //}

            //catch (Exception ex)
            //{
            //    return partialView("Error");
            //}
        }


        public DataModel.CustomerNote CreateNoteInstance()
        {
            string[] additionalValues = null;
            var customerNote = new DataModel.CustomerNote
            {
                youseeNoteId = "0",
                sectionName = "Kundeoverblik",
                systemName = "CIP", //(TP, or CIP)
                created = "2018-07-05T02:40:27",
                note = "Testing purpose",
                status = "Active",
                userId = "M32321",
                userName = "Diksha Gulati",
                userInitials = "DG",

                entityId = null, //entityId must be guideSessionId because of other system using BC
                entityType = "GuideSelector",
                entityTitle = "",
                entityName = null,
                entityStep = null,

                address = "2600 glostrup",
                zip = "2600",
                city = "glostrup",
                customerName = "Morten juul fredbo poulsen",
                customerBan = "203621955",
                svarsted = "",
                contextId = "62525",
                departmentName = "ACCENTURE",

                lid = "87341127", //see see Product Backlog Item 1157:LID handling as part of starting a guide from the Guide and Article Selector
                additionalValues = string.IsNullOrEmpty("SysetemNote") ?
       new List<DataModel.AdditionalValue>()
       {
                    new DataModel.AdditionalValue()
                    {
                        key = "ActualLidId",
                        value = "87341127"
                    }
       } :
       new List<DataModel.AdditionalValue>()
       {
                    new DataModel.AdditionalValue()
                    {
                        key = "ActualLidId",
                        value = "87341127"
                    },
                    new DataModel.AdditionalValue()
                    {
                        key ="ActualLidId",
                        value = "87341127"
                    }
       }


            };

            customerNote.additionalValues = MapAdditionalValues(customerNote.additionalValues, additionalValues);
            return customerNote;
        }

        public static List<AdditionalValue> MapAdditionalValues(List<AdditionalValue> existingAdditionalValue, string[] newAdditionalValue)
        {
            if (newAdditionalValue != null && newAdditionalValue.Length > 0)
            {

                var systemAdditionalValue = existingAdditionalValue.Where(n => n.key == "ActualLidId").FirstOrDefault();
                var noterMessages = ConvertStringArrayToString(newAdditionalValue, systemAdditionalValue);
                var additionalValue = new AdditionalValue();

                if (systemAdditionalValue != null)
                {
                    systemAdditionalValue.value = noterMessages;
                }
                else
                {
                    additionalValue.key = "ActualLidId";
                    additionalValue.value = noterMessages;
                    existingAdditionalValue.Add(additionalValue);
                }

            }
            return existingAdditionalValue;
        }


        static string ConvertStringArrayToString(string[] array, AdditionalValue additionalValue = null)
        {
            StringBuilder noterMessage = new StringBuilder();
            if (additionalValue != null && !string.IsNullOrEmpty(additionalValue.value))
            {
                noterMessage.Append(additionalValue.value);
            }

            foreach (string value in array)
            {
                noterMessage.Append(value);
                noterMessage.Append("@@@");
            }
            //builder.Length -= 1;
            return noterMessage.ToString();
        }
    }
}