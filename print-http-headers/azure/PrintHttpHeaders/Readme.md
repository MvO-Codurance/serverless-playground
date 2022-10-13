# Print HTTP Headers in Azure Functions
<!--
This project shows how to run an ASP.NET Core Web API project as an AWS Lambda exposed through Amazon API Gateway. The NuGet package [Amazon.Lambda.AspNetCoreServer](https://www.nuget.org/packages/Amazon.Lambda.AspNetCoreServer) contains a Lambda function that is used to translate requests from API Gateway into the ASP.NET Core framework and then the responses from ASP.NET Core back to API Gateway.

For more information about how the Amazon.Lambda.AspNetCoreServer package works and how to extend its behavior view its [README](https://github.com/aws/aws-lambda-dotnet/blob/master/Libraries/src/Amazon.Lambda.AspNetCoreServer/README.md) file in GitHub.

## Executable Assembly

.NET Lambda projects that use C# top level statements like this project must be deployed as an executable assembly instead of a class library. To indicate to Lambda that the .NET function is an executable assembly the 
Lambda function handler value is set to the .NET Assembly name. This is different then deploying as a class library where the function handler string includes the assembly, type and method name.

To deploy as an executable assembly the Lambda runtime client must be started to listen for incoming events to process. For an ASP.NET Core application the Lambda runtime client is started by included the
`Amazon.Lambda.AspNetCoreServer.Hosting` NuGet package and calling `AddAWSLambdaHosting(LambdaEventSource.RestApi)` passing in the event source while configuring the services of the application. The
event source can be API Gateway REST API and HTTP API or Application Load Balancer.  
-->
## Project Files
<!--
* `Program.cs` - entry point to the application that contains all of the top level statements initializing the ASP.NET Core application.
The call to `AddAWSLambdaHosting` configures the application to work in Lambda when it detects Lambda is the executing environment. 
* `Controllers\CalculatorController.cs` - example Web API controller
* `main.tf` - the main TerraForm file which contains statements to deploy the Lambda function and API Gateway
* `variables.tf` - the TerraForm inputs definition file (change these to alter the names of the created AWS resources)
* `outputs.tf` - the TerraForm outputs definition file (defines what information is output to the console after deployment completes)
-->
## Pre-requisites

Ensure you have the following:
* The [Terraform CLI](/tutorials/terraform/install-cli?in=terraform/aws-get-started) (1.3.2+) installed.
* [An Azure account](https://azure.microsoft.com/).
* The [Azure CLI (2.40+)](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli) installed.
* 

## To deploy the application
Open a **Powershell** prompt at the project folder, i.e. the folder than contains the `main.tf` file.

Log in to your Azure account:
```
az login
```
Using the browser that is opened to authenticate. Your `~/.azure` profile will be updated with the required authentication tokens. 
Close the browser.

Build/publish the application:
```
./publish.ps1
```

Initialise TerraForm:
```
terraform init
```

View the deployment plan:
```
terraform plan
```

Deploy the application:
```
terraform apply
```
When prompted, enter `yes` to confirm the deployment.

Once complete, copy/paste the `function_app_url` output value from the console into your browser.

## To delete the application
```
terraform destroy
```
When prompted, enter `yes` to confirm the deletion.
