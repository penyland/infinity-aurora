namespace Infinity.Web.Features.FeedViewer;

public record Message
{
    public string MessageId { get; init; }

    public string ContentType { get; init; }

    public string CorrelationId { get; init; }

    public string Body { get; init; }

    public string EventType { get; init; }

    public DateTimeOffset CreatedAt { get; init; }

    public DateTimeOffset ReceivedAt { get; init; }
}
