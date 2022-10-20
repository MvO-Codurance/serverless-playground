using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace PrintHttpHeaders.Models;

public class HeadersOutputType
{
    [CosmosDBOutput("%CosmosDatabaseName%", "%CosmosContainerName%", ConnectionStringSetting = "CosmosDbConnectionString")]
    public RequestResponseHeaders RequestResponseHeaders { get; }
    public HttpResponseData Response { get; }

    public HeadersOutputType(RequestResponseHeaders requestResponseHeaders, HttpResponseData response)
    {
        RequestResponseHeaders = requestResponseHeaders;
        Response = response;
    }
}