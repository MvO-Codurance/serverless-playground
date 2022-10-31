using System.Text.Json.Serialization;
using Microsoft.Azure.Functions.Worker.Http;

namespace PrintHttpHeaders.Models;

public class RequestResponseHeaders
{
    public List<KeyValuePair<string, IEnumerable<string>>>? RequestHeaders { get; set; }
    public List<KeyValuePair<string, IEnumerable<string>>>? ResponseHeaders { get; set; }

    [JsonConstructor]
    public RequestResponseHeaders()
    {
    }
    
    public RequestResponseHeaders(HttpHeadersCollection requestHeaders,HttpHeadersCollection responseHeaders)
    {
        RequestHeaders = requestHeaders.ToList();
        ResponseHeaders = responseHeaders.ToList();
    }
}