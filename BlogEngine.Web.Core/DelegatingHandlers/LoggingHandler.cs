namespace BlogEngine.Web.Core.DelegatingHandlers;

public sealed class LoggingHandler : DelegatingHandler
{
    private readonly ILogger<LoggingHandler> _logger;

    public LoggingHandler(ILogger<LoggingHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var requestGuid = Guid.NewGuid();

        _logger.LogInformation("Incoming request with id {requestId} incoming: {request}",
            requestGuid,
            JsonConvert.SerializeObject(request));

        var response = await base.SendAsync(request, cancellationToken);

        _logger.LogInformation("Response has been received from request id {requestId}: {response}",
            requestGuid,
            JsonConvert.SerializeObject(response));

        return response;
    }
}
