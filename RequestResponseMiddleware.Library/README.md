# RequestResponse Middleware

## Description

The RequestResponse Middleware is a built-in middleware for handling request-response cycles 
and logging within ASP.NET Core applications. It extends the functionality of `IApplicationBuilder` by integrating middleware that manages and logs request-response details in your web API projects. 
This middleware is highly configurable, allowing you to customize its behavior through various options.

## Dependencies

* [Microsoft.AspNetCore.Http](https://www.nuget.org/packages/Microsoft.AspNetCore.Http/)
* [Microsoft.AspNetCore.Http.Extensions](https://www.nuget.org/packages/Microsoft.AspNetCore.Http.Extensions/)
* [Microsoft.Extensions.Logging.Abstractions](https://www.nuget.org/packages/Microsoft.Extensions.Logging.Abstractions/)
* [Microsoft.IO.RecyclableMemoryStream](https://www.nuget.org/packages/Microsoft.IO.RecyclableMemoryStream/)

## Getting Started

This library help you to get details of request and response by adding a Middleware on your system. When the request end, it calls a handler.

## Usage 

Integrate the middleware into your application pipeline during

```csharp
app.AddRequestResponseMiddleware(option=> {
	option.UseHandler(async context => 
	{
	// when request and response are created and you handle
	var fullRequestUrl = context.Url;
	var requestBody = context.RequestBody;
	var requestLength = context.RequestLength;

	var responseBody = context.ResponseBody;
	var responseLength = context.ResponseLength;

	var responseTimeSpan = context.ResponseCreationTime;
	var responseFormattedTimeSpan = context.ResponseTime;

	});
});
```


### Installation

To install the RequestResponse Middleware, use the following .NET CLI command:

```bash
dotnet add package RequestResponseMiddleware
```

## Contributing
Contributions to the RequestResponse Middleware are welcome! 
If you have suggestions, improvements, or bug fixes, please fork the repository, make your changes, and submit a pull request.
For major changes, please open an issue first to discuss what you would like to change.

## Contact
If you have any questions or feedback, feel free to contact us at ahakan.yildirim336@gmail.com