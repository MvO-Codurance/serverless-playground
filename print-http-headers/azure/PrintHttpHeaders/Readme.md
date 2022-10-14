# Print HTTP Headers in Azure Functions
This project shows how to run an ASP.NET Core Azure Functions application.

## Project Files
* `Program.cs` - entry point to the application that contains all of the top level statements initializing the ASP.NET Core application
* `Functions\Root.cs` - the "root" function (although Azure Functions does not allow functions to sit at the root route) that displays HTTP request/response headers
* `Functions\Calculator.cs` - the simple calculator functions to add/subtract/multiple/divide two integers
* `main.tf` - the main TerraForm file which contains statements to deploy the FunctionApp
* `variables.tf` - the TerraForm inputs definition file (change these to alter the names of the created Azure resources)
* `outputs.tf` - the TerraForm outputs definition file (defines what information is output to the console after deployment completes)

## Pre-requisites
Ensure you have the following:
* The [Terraform CLI](/tutorials/terraform/install-cli?in=terraform/aws-get-started) (1.3.2+) installed.
* [An Azure account](https://azure.microsoft.com/).
* The [Azure CLI (2.40+)](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli) installed. 

## To deploy the application
Open a **Powershell** prompt at the project folder, i.e. the folder than contains the `main.tf` file.

Log in to your Azure account:
```
az login
```
Use the browser that is opened to authenticate to your Azure account. Your `~/.azure` profile will be updated with the required authentication tokens. 
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
