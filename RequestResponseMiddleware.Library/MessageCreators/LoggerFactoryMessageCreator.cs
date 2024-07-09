namespace RequestResponseMiddleware.Library.MessageCreators;
internal class LoggerFactoryMessageCreator : BaseLogMessageCreator,ILogMessageCreator
{
    private readonly LoggingOptions loggingOptions;
   

    public LoggerFactoryMessageCreator(LoggingOptions loggingOptions)
    {
        this.loggingOptions = loggingOptions;
    }
    public string Create(RequestResponseContext context)
    {
        

        var stringBuilder = new StringBuilder();

        foreach (var field in loggingOptions.LoggingFields)
        {
            var value = GetValueByField(field, context);

            stringBuilder.AppendFormat("{0}: {1}\n", field, value);
        }

        return stringBuilder.ToString();
    }

   
}
