# pdfless-dotnet
The Official Pdfless C#, .NetStandard, .NetCore API Library

# Introduction 
The Official Pdfless C# to consume Pdfless API. `https://www.pdfless.com/`
This library supports .NET Standard 2.1+, netcore 3.1+

# Prerequisites 
Pdfless account is required, [sign up for free](https://www.pdfless.com) for free to create up to 100 pdfs/month forever. 

# Installation

Using the [.NET Core command-line interface (CLI) tools][dotnet-core-cli-tools]:

```sh
dotnet add package Pdfless
```

Using the [NuGet Command Line Interface (CLI)][nuget-cli]:

```sh
nuget install Pdfless
```

Using the [Package Manager Console][package-manager-console]:

```powershell
Install-Package Pdfless
```

From within Visual Studio:

Open the Solution Explorer.
Right-click on a project within your solution.
Click on Manage NuGet Packages...
Click on the Browse tab and search for "Pdfless".
Click on the Pdfless package, select the appropriate version in the right-tab and click Install.

# Get started

## Configuration Appsetting
In your config file, add this config block :
```json
"Pdfless": {
    "ApiKey": "ak_xxxxxxxxx"
}
```

## Middleware
In your startup.cs, add Pdfless middleware.

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pdfless.Extensions;

namespace YourApp.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPdfless(Configuration);
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
```

### WebProxy
You can configure WebProxy :

```csharp
public void ConfigureServices(IServiceCollection services)
{
    var proxy = new WebProxy("http://proxy:1337");
    services.AddPdfless(Configuration, proxy);
    services.AddControllers();
}
```

# Usage

In your class, inject IPdflessClient through the constructor
```csharp
private readonly IPdflessClient _pdflessClient;

public MyConstructor(IPdflessClient pdflessClient)
{
    _pdflessClient = pdflessClient ?? throw new ArgumentNullException(nameof(pdflessClient));
}
```
Call `CreateAsync` function to generate PDF

```csharp
public async Task Myfunction()
{
    var payload = new DocumentContent
    {
        Company = "Synapsium", 
        AddressCompany = "10 boulevard de la République, 75001 Paris"
    };
    
    var result = await _pdflessClient.CreateAsync("xxxxxxx-xxx-xxxx-xxxx-xxxxxxxxxx", payload);
}
```

# Methods
```csharp
Task<Response<PdfResponse>> CreateAsync(string templateId, object payload);
```