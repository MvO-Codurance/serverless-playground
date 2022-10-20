using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace PrintHttpHeaders.Models;

public class HeadersOutputType
{
    [CosmosDBOutput("%DatabaseName%", "%CollectionName%", ConnectionStringSetting = "CosmosDbConnectionString")]
    public RequestResponseHeaders RequestResponseHeaders { get; set; }
    public HttpResponseData Response { get; set; }
}