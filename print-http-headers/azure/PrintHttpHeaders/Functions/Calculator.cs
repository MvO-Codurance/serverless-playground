using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace PrintHttpHeaders.Functions;

public class Calculator
{
    private readonly ILogger<Calculator> _logger;

    public Calculator(ILogger<Calculator> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    [Function(nameof(Add))]
    public async Task<HttpResponseData> Add(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "calculator/add/{x:int}/{y:int}")] HttpRequestData request,
        int x,
        int y)
    {
        var result = x + y;
        return await BuildResponse(nameof(Add).ToLower(), request, x, y, result);
    }
    
    [Function(nameof(Subtract))]
    public async Task<HttpResponseData> Subtract(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "calculator/subtract/{x:int}/{y:int}")] HttpRequestData request,
        int x,
        int y)
    {
        var result = x - y;
        return await BuildResponse(nameof(Subtract).ToLower(), request, x, y, result);
    }
    
    [Function(nameof(Multiply))]
    public async Task<HttpResponseData> Multiply(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "calculator/multiply/{x:int}/{y:int}")] HttpRequestData request,
        int x,
        int y)
    {
        var result = x * y;
        return await BuildResponse(nameof(Multiply).ToLower(), request, x, y, result);
    }
    
    [Function(nameof(Divide))]
    public async Task<HttpResponseData> Divide(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "calculator/divide/{x:int}/{y:int}")] HttpRequestData request,
        int x,
        int y)
    {
        var result = x / y;
        return await BuildResponse(nameof(Divide).ToLower(), request, x, y, result);
    }

    private async Task<HttpResponseData> BuildResponse(string operation, HttpRequestData request, int x, int y, int result)
    {
        _logger.LogInformation("{X} {Operation} {Y} is {Result}", x, operation, y, result);

        var response = request.CreateResponse(HttpStatusCode.OK);
        await response.WriteStringAsync(result.ToString());

        return response;
    }
}