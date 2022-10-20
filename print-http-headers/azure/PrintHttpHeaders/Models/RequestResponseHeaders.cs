using Microsoft.Azure.Functions.Worker.Http;

namespace PrintHttpHeaders.Models;

public class RequestResponseHeaders
{
    public Guid Id => Guid.NewGuid();
    public HttpHeadersCollection RequestHeaders { get; }
    public HttpHeadersCollection ResponseHeaders { get; }

    public RequestResponseHeaders(HttpHeadersCollection requestHeaders, HttpHeadersCollection responseHeaders)
    {
        RequestHeaders = requestHeaders;
        ResponseHeaders = responseHeaders;
    }
}