using Azure.Messaging.WebPubSub;
using Microsoft.AspNetCore.Mvc;
using PubSub.Test1.Models;
using System.Diagnostics;


namespace PubSub.Test1.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {

            ViewBag.webPubSubConnectionString = _configuration.GetValue<string>("WebPubSubConnectionString");
            ViewBag.SwaggerUrl = _configuration.GetValue<string>("SwaggerUrl");

            //Random Name for Hub. This is part of ConnectionString and makes Connection Scope local to Browser/Session Scope
            var pubSubHubName = "Hub" + GetHubNameNew();
            ViewBag.Hub = pubSubHubName;

            TempData["Emp"] = pubSubHubName;

            var psClient = new WebPubSubServiceClient(
                    ViewBag.webPubSubConnectionString,
                    pubSubHubName);

            var clientConnectionInformation = psClient.GetClientAccessUri(); // await psClient.GetClientAccessUriAsync();

            ViewBag.ClientConnection = clientConnectionInformation.ToString();

            return View();
        }
        public IActionResult About()
        {
            return View();
        }


            [HttpGet]
        [Route("GetBreakfastSynchronous")]
        public IEnumerable<string> GetBreakfast1Synchronous()
        {
            var unboxedHubName = (string?)TempData["Emp"];

            //Reloaded into for subsequent Requests
            TempData["Emp"] = unboxedHubName;

            ViewBag.ConnectionString = _configuration.GetValue<string>("WebPubSubConnectionString");
            ViewBag.Hub = unboxedHubName;
            ViewBag.EmittedMessages = "start_";

            var pubSubClient = new WebPubSubServiceClient(
                    ViewBag.ConnectionString,
                    ViewBag.Hub);

            pubSubClient.SendToAll("Starting Stopwatch...." + unboxedHubName);
            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Start();

            BreakfastSynchronous.MakeSyncBreakfast(pubSubClient);

            stopWatch.Stop();

            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value. 
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 2);
            Console.WriteLine("RunTime " + elapsedTime);

            pubSubClient.SendToAll("RunTime " + elapsedTime);
            pubSubClient.SendToAll(" ");

            return new string[] { "value1", "value2" };

        }

        [HttpGet]
        [Route("GetBreakfastASynchronousAsync")]
        public async Task<IEnumerable<string>> GetBreakfast2ASynchronousAsync()
        {
            var unboxedHubName = (string?)TempData["Emp"];

            //Reloaded into for subsequent Requests
            TempData["Emp"] = unboxedHubName;

            ViewBag.ConnectionString = _configuration.GetValue<string>("WebPubSubConnectionString");
            ViewBag.Hub = unboxedHubName;
            ViewBag.EmittedMessages = "start_";

            var pubSubClient = new WebPubSubServiceClient(
                    ViewBag.ConnectionString,
                    ViewBag.Hub);

            pubSubClient.SendToAll("Starting Stopwatch...." + unboxedHubName);
            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Start();

            var ret = await BreakfastAsynchronous.MakeAsyncBreakfastAsync(pubSubClient);

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value. 
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 2);
            Console.WriteLine("RunTime " + elapsedTime);

            pubSubClient.SendToAll("RunTime " + elapsedTime);
            pubSubClient.SendToAll(" ");

            return new string[] { "value1", "value2" };

        }

            public IActionResult Privacy()
        {
            return View();
        }


        private string GetHubNameNew()
        {
            var g = Guid.NewGuid().ToString();
            //string example = g.ToString();

            // Get 6 characters from the right of the string
            string exampleRight = g.Substring(g.Length - 6, 6);

            var id = exampleRight;
            return id;

        }
    }


    internal class Bacon { }
    internal class Coffee { }
    internal class Egg { }
    internal class Juice { }
    internal class Toast { }

}