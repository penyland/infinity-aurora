using Microsoft.AspNetCore.SignalR.Client;

namespace Infinity.Web.Features.FeedViewer;

public class FeedHubConnection : IAsyncDisposable
{
    private readonly HubConnection? hubConnection = null;

    public FeedHubConnection(IConfiguration configuration)
    {
        var feedHub = new Uri($"");

        hubConnection = new HubConnectionBuilder()
            .WithUrl(feedHub)
            .WithAutomaticReconnect()
            .Build();

        hubConnection.Closed += OnHubConnectionClosed;
        hubConnection.Reconnected += OnHubConnectionReconnected;
        hubConnection.Reconnecting += OnHubConnectionReconnecting;
    }

    /// <summary>
    /// Indicates the state of the <see cref="HubConnection"/> to the server.
    /// </summary>
    public HubConnectionState State =>
        hubConnection?.State ?? HubConnectionState.Disconnected;


    public async ValueTask DisposeAsync()
    {
        if (hubConnection is not null)
        {
            hubConnection.Closed -= OnHubConnectionClosed;
            hubConnection.Reconnected -= OnHubConnectionReconnected;
            hubConnection.Reconnecting -= OnHubConnectionReconnecting;

            await hubConnection.StopAsync();
            await hubConnection.DisposeAsync();
        }
    }

    private Task OnHubConnectionReconnecting(Exception? arg)
    {
        throw new NotImplementedException();
    }

    private Task OnHubConnectionReconnected(string? arg)
    {
        throw new NotImplementedException();
    }

    private Task OnHubConnectionClosed(Exception? arg)
    {
        throw new NotImplementedException();
    }
}
