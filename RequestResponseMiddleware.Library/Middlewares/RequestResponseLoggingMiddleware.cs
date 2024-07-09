namespace RequestResponseMiddleware.Library.Middlewares;
public class RequestResponseLoggingMiddleware : BaseRequestResponseMiddleware
{
   
    public RequestResponseLoggingMiddleware(RequestDelegate next, ILogWriter logWriter)
        : base(next, logWriter)
    {

        
    }
    public async Task Invoke(HttpContext context)
    {
       var requestResponseContext = await BaseMiddlewareInvoke(context);

      
    }
}
