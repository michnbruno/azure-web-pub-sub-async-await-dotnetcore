using Azure.Messaging.WebPubSub;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PubSub.Test1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        //private readonly ILogger<ValuesController> _logger;
        //private readonly ITransientService _transientService1;
        //private readonly ITransientService _transientService2;
        //private readonly IScopedService _scopedService1;
        //private readonly IScopedService _scopedService2;
        //private readonly string _singletonService1;
        //private readonly ISingletonService _singletonService2;

        //public ValuesController(ILogger<ValuesController> logger,
        //ITransientService transientService1,
        //ITransientService transientService2,
        //IScopedService scopedService1,
        //IScopedService scopedService2,
        //ISingletonService singletonService1,
        //ISingletonService singletonService2)
        //{
        //    _logger = logger;
        //    _transientService1 = transientService1;
        //    _transientService2 = transientService2;
        //    _scopedService1 = scopedService1;
        //    _scopedService2 = scopedService2;
        //    _singletonService1 = singletonService1.GetOperationID();
        //    _singletonService2 = singletonService2;
        //}





        // GET: api/<ValuesController>
        [HttpGet]
        [Route("Get")]
        public IEnumerable<string> Get()
        {

            var webPubSubConnectionString = "Endpoint=https://pubsub-testmnb.webpubsub.azure.com;AccessKey=OhTvi1hB1NKftf9TtEgBlkC/+PXHu2HiTrmlkg3nTq4=;Version=1.0;";
            var pubSubHubName = "Hub"; // + Globals.id;
            var psClient = new WebPubSubServiceClient(
                    webPubSubConnectionString,
                    pubSubHubName);
            var clientConnectionInformation = psClient.GetClientAccessUri(); // await psClient.GetClientAccessUriAsync();

            var test = clientConnectionInformation.ToString();
            // var uri = pubSubClient.GetClientAccessUri().ToString();
            //   ViewBag.ClientConnection = test;
            var mess = "test 1111...." + pubSubHubName;
            psClient.SendToAll("mess 111");



            return new string[] { test, pubSubHubName };
        }

        //[HttpGet]
        //[Route("Get2")]
        //public IEnumerable<string> Get2()
        //{
        //    var webPubSubConnectionString = "Endpoint=https://pubsub-testmnb.webpubsub.azure.com;AccessKey=OhTvi1hB1NKftf9TtEgBlkC/+PXHu2HiTrmlkg3nTq4=;Version=1.0;";
        //    var pubSubHubName = "Hub" + Globals.id;
        //    var psClient = new WebPubSubServiceClient(
        //            webPubSubConnectionString,
        //            pubSubHubName);
        //    var clientConnectionInformation = psClient.GetClientAccessUri(); // await psClient.GetClientAccessUriAsync();

        //    var test = clientConnectionInformation.ToString();
        //    // var uri = pubSubClient.GetClientAccessUri().ToString();
        //    //  ViewBag.ClientConnection = test;
        //    var mess = "test 222...." + pubSubHubName;
        //    psClient.SendToAll("mess 22", pubSubHubName);



        //    return new string[] { test, pubSubHubName };
        //}

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
