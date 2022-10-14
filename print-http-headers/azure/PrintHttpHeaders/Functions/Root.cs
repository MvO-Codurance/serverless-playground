using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace PrintHttpHeaders.Functions;

public class Root
{
    private readonly ILogger<Root> _logger;

    public Root(ILogger<Root> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    [Function(nameof(Root))]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequestData request)
    {
        /*
            HttpRequestData is not the real HttpRequest that the Functions runtime received.
            There is no HttpRequest.Connection.RemoteIpAddress property so we use the X-Forwarded-For header.
            This header seems to hold the client IP address plus a port number (something to do with the Functions 
            hosting/runtime) so we strip off the port number.
            All this works when in Azure, but not when running locally. 
        */
        request.Headers.TryGetValues("X-Forwarded-For", out var xForwardedForValues);
        var remoteIpAddress = (xForwardedForValues?.FirstOrDefault() ?? string.Empty).Split(":").FirstOrDefault();
        
        _logger.LogInformation("GET request for / from remote IP {RemoteIpAddress}", remoteIpAddress);
        
        var response = request.CreateResponse(HttpStatusCode.OK);
        
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