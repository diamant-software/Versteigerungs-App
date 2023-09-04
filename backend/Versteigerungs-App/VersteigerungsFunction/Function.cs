using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace VersteigerungsFunction
{
    public static class Function
    {
        [FunctionName("Function1")]
        public static void RunFunction1(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "function1")] HttpRequest req,
        ILogger log)
        {
            // Function 1 code here
            log.LogInformation("Function1 executed.");
        }

        [FunctionName("Function2")]
        public static void RunFunction2(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "function2")] HttpRequest req,
            ILogger log)
        {
            // Function 2 code here
            log.LogInformation("Function2 executed.");
        }

        [FunctionName("Function3")]
        public static void RunFunction3(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "function3")] HttpRequest req,
            ILogger log)
        {
            // Function 3 code here
            log.LogInformation("Function3 executed.");
        }
    }
}
