var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.MapGet("/", async context =>
{
    // request headers
    await context.Response.WriteAsync("REQUEST HEADERS:\n");
    foreach (var requestHeader in context.Request.Headers)
    {
        await context.Response.WriteAsync($"{requestHeader.Key} = {requestHeader.Value}\n");
    }
    await context.Response.WriteAsync($"RemoteIpAddress = {context.Connection.RemoteIpAddress}");

    // response headers
    await context.Response.WriteAsync("\n\nRESPONSE HEADERS:\n");
    foreach (var responseHeader in context.Response.Headers)
    {
        await context.Response.WriteAsync($"{responseHeader.Key} = {responseHeader.Value}\n");
    }
});

app.Run();
