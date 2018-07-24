using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.DataModel;
using WebApplication1.Facade;
using WebApplication1.Models;
using WebApplication1.ServiceLocator;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ILogger _logger;
        public HomeController(ILogger logger){
            _logger=logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }


        public IActionResult Error()
        {
            // Gets the status code from the exception or web server.
            var statusCode = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error is HttpException httpEx ?
                httpEx.StatusCode : (HttpStatusCode)Response.StatusCode;

            // For API errors, responds with just the status code (no page).
            if (HttpContext.Features.Get<IHttpRequestFeature>().RawTarget.StartsWith("/api/", StringComparison.Ordinal))
                return StatusCode((int)statusCode);

            var status = StatusCode((int)statusCode).StatusCode;
            // Creates a view model for a user-friendly error page.
            string text = null;
            switch (statusCode)
            {
                case HttpStatusCode.NotFound: text = "Page not found."; break;
                    // Add more as desired.
            }
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorText = statusCode.ToString(), Status = status.ToString() });
        }
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        public PartialViewResult Test()
        {
           
            _logger.LogInformation("\n#### Using AppLogger Warning####\n");
                ViewData["Message"] = "Test Ajax";
                ServiceLocatorTool serviceLocator = new ServiceLocatorTool();
                SubscriptionFacade facade = new SubscriptionFacade();

                var subs = facade.SubscriptionFacade1();
                var res = PartialView(new HomePageViewModel { Test1 = "Acc No.:-" + subs.Result[0].parentAccountNo.ToString(), Test2 = "Test 2" });
                return res;
           
            //catch (Exception ex)
            //{
            //    var result = PartialView(new ErrorViewModel { RequestId = "RequestId" + ex.StackTrace.ToString()});
            //    return result;
            //}

        }

        public string TestData()
        {
            ViewData["Message"] = "Test Ajax";

            return JsonConvert.SerializeObject(new HomePageViewModel { Test1 = HttpContext.Request.Host.ToString() + HttpContext.Request.Path, Test2 = "Test 2" });
        }


        public async Task<OpenClosedFaultsStatusReply> notifyUser(string fasoId, string message)
        {
            ServiceLocatorTool serviceLocator = new ServiceLocatorTool();
            // SubscriptionFacade facade = new SubscriptionFacade(serviceLocator.ObjServiceLocator);
            FasFacade facade = new FasFacade();
            try
            {
                var obj = await facade.FasFacade1();
                return obj;

            }
            catch
            {
                return null;
            }
        }

        public async Task<CustomerNote> saveNote(string fasoId, string message)
        {
            ServiceLocatorTool serviceLocator = new ServiceLocatorTool();
            // SubscriptionFacade facade = new SubscriptionFacade(serviceLocator.ObjServiceLocator);
            CustomerNoteFacade facade = new CustomerNoteFacade();
            try
            {
                var obj = await facade.CustomerNoteFacade1();
                return obj;

            }
            catch
            {
                return null;
            }
        }

    }
}
