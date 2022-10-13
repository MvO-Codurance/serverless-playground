using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .Build();

await host.RunAsync();

public class Functions
{
    private readonly ILogger<Functions> _logger;

    public Functions(ILogger<Functions> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    [Function(nameof(Root))]
    public async Task<HttpResponseData> Root(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequestData request,
        FunctionContext executionContext)
    {
        var response = request.CreateResponse(HttpStatusCode.OK);
        var remoteIpAddress = "** REMOTE IP ADDRESS **";

        _logger.LogInformation("GET request for / from remote IP {RemoteIpAddress}", remoteIpAddress);
        
        // request headers
        await response.WriteStringAsync("REQUEST HEADERS:\n");
        foreach (var requestHeader in request.Headers)
        {
            await response.WriteStringAsync($"{requestHeader.Key} = {string.Join(", ", requestHeader.Value)}\n");
        }
        await response.WriteStringAsync($"RemoteIpAddress = {remoteIpAddress}");

        // response headers
        await response.WriteStringAsync("\n\nRESPONSE HEADERS:\n");
        foreach (var responseHeader in response.Headers)
        {
            await response.WriteStringAsync($"{responseHeader.Key} = {string.Join(", ", responseHeader.Value)}\n");
        }

        return response;
    }

}
