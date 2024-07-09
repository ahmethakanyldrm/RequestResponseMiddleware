
namespace RequestResponseMiddleware.Library;
public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder AddRequestResponseMiddleware(this IApplicationBuilder appBuilder, Action<RequestResponseOptions> optionAction)
    {
        var option = new RequestResponseOptions();
        optionAction(option);

        if(option.RequestResponseHandler is null && option.LoggerFactory is null)
        {
            throw new ArgumentNullException($"{nameof(option.RequestResponseHandler)} and {nameof(option.LoggerFactory)}");
        }

        ILogWriter logWriter = option.LoggerFactory is null ?
            new NullLogWriter() :
            new LoggerFactoryLogWriter(option.LoggerFactory, option.LoggingOptions);

        if(option.RequestResponseHandler is not null)
        {
            appBuilder.UseMiddleware<HandlerRequestResponseLoggingMiddleware>(option.RequestResponseHandler, logWriter);
        }else
        {
            appBuilder.UseMiddleware<RequestResponseLoggingMiddleware>(logWriter);
        }
        return appBuilder;
    }
}
