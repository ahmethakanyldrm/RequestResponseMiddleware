using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseMiddleware.Library.Models;
public class RequestResponseOptions
{

    internal Func<RequestResponseContext, Task> RequestResponseHandler { get; set; }
    internal ILoggerFactory LoggerFactory;
    internal LoggingOptions LoggingOptions;
    public void UseHandler(Func<RequestResponseContext, Task> requestResponseHandler)
    {
        RequestResponseHandler = requestResponseHandler;
    }

    public void UseLogger(ILoggerFactory loggerFactory, Action<LoggingOptions> loggingAction)
    {
        LoggingOptions = new LoggingOptions();
        loggingAction(LoggingOptions);
        LoggerFactory = loggerFactory;
    }

}
