

namespace RequestResponseMiddleware.Library.Middlewares;
public abstract class BaseRequestResponseMiddleware
{
    private readonly RecyclableMemoryStreamManager recyclableMemoryStreamManager;
    private readonly RequestDelegate next;
    private readonly ILogWriter logWriter;

    public BaseRequestResponseMiddleware(RequestDelegate next, ILogWriter logWriter)

    {
        this.next = next;
        this.logWriter = logWriter;
        recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
    }
    protected async Task<RequestResponseContext> BaseMiddlewareInvoke(HttpContext context)
    {
        var requestBody = await GetRequestBody(context);
        var originalBodyStream = context.Response.Body;

        // No response
        await using var responseBody = recyclableMemoryStreamManager.GetStream();
        context.Response.Body = responseBody;

        var stopWatch = Stopwatch.StartNew();
        await next(context);
        stopWatch.Stop();

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        string responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin);

        // response


       var result = new RequestResponseContext(context)
        {
            ResponseCreationTime = TimeSpan.FromTicks(stopWatch.ElapsedTicks),
            RequestBody = requestBody,
            ResponseBody = responseBodyText

        };

        await logWriter.Write(result);
        return result;

    }

    private static string ReadStreamInChunks(Stream stream)
    {
        const int readChunkBufferLength = 4096;

        stream.Seek(0, SeekOrigin.Begin);

        using var textWriter = new StringWriter();
        using var reader = new StreamReader(stream, Encoding.UTF8);

        var readChunk = new char[readChunkBufferLength];
        int readChunkLength;

        do
        {
            readChunkLength = reader.ReadBlock(readChunk,
                                               0,
                                               readChunkBufferLength);
            textWriter.Write(readChunk, 0, readChunkLength);

        } while (readChunkLength > 0);

        return textWriter.ToString();
    }

    private async Task<string> GetRequestBody(HttpContext context)
    {
        context.Request.EnableBuffering();

        await using var requestStream = recyclableMemoryStreamManager.GetStream();
        await context.Request.Body.CopyToAsync(requestStream);

        string reqBody = ReadStreamInChunks(requestStream);

        context.Request.Body.Seek(0, SeekOrigin.Begin);

        return reqBody;
    }
}
