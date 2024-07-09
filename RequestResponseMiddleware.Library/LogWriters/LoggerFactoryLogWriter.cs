

namespace RequestResponseMiddleware.Library.LogWriters;
internal class LoggerFactoryLogWriter : ILogWriter
{
    private readonly ILogger logger;
    private readonly LoggingOptions loggingOptions;
    public ILogMessageCreator MessageCreator { get; }

    internal LoggerFactoryLogWriter(ILoggerFactory loggerFactory, LoggingOptions loggingOptions)
    {
        logger = loggerFactory.CreateLogger(loggingOptions.LoggerCategoryName);
        this.loggingOptions = loggingOptions;
        MessageCreator = new LoggerFactoryMessageCreator(loggingOptions);
       
    }


    public async Task Write(RequestResponseContext context)
    {
        var message = MessageCreator.Create(context);
        logger.Log(loggingOptions.LogLevel, message);

        await Task.CompletedTask;
    }
}
