using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using PrintHttpHeaders.Models;

namespace PrintHttpHeaders.Functions;

public class ChangeFeed
{
    private readonly ILogger<Root> _logger;

    public ChangeFeed(ILogger<Root> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    [Function(nameof(ChangeFeed))]
    public object Run(
        [CosmosDBTrigger("%CosmosDatabaseName%", "%CosmosContainerName%", ConnectionStringSetting = "CosmosDbConnectionString", 
            LeaseCollectionName = "leases", CreateLeaseCollectionIfNotExists = true)] IReadOnlyList<RequestResponseHeaders> inputs)
    {
        foreach (var input in inputs)
        {
            var xForwardedForHeader = input.RequestHeaders.FirstOrDefault(x => string.Equals(x.Key, "X-Forwarded-For", StringComparison.OrdinalIgnoreCase));
            var remoteIpAddress = (xForwardedForHeader.Value?.FirstOrDefault() ?? string.Empty).Split(":").FirstOrDefault();
            
            _logger.LogInformation("Change feed: inserted/updated entry for remote IP address {RemoteIpAddress}", remoteIpAddress);   
        }
        
        return null;
    }
}