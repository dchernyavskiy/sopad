using System.Collections;
using Microsoft.AspNetCore.SignalR;

namespace LB4.Hubs;

public class Group
{
    public string Id { get; set; }
}

public class ChatHub : Hub
{
    private static List<Group> _connections = new();
    private ILogger<ChatHub> _logger;

    public ChatHub(ILogger<ChatHub> logger)
    {
        _logger = logger;
    }

    public async Task PingAsync()
    {
        await Clients.All.SendAsync("ping");
    }

    public async Task UpdateAsync(string secret)
    {
        var current = _connections.Find(x => x.Id == Context.ConnectionId);
        var others = _connections.Where(x => x != current);

        foreach (var other in others)
        {
            secret = await Clients.Client(other.Id)
                .InvokeCoreAsync<string>("updateSecret", new[] { secret }, CancellationToken.None);
        }

        await Clients.All.SendAsync("newSecret", new[] { secret }, CancellationToken.None);
    }

    public async Task SendAsync(Message message)
    {
        _logger.LogInformation($"current: {message.Name}, message: {message.Text}");
        await Clients.Others.SendAsync("receiveMessage", message);
    }

    public override Task OnConnectedAsync()
    {
        _connections.Add(new() { Id = Context.ConnectionId });
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _connections.Remove(_connections.First(x => x.Id == Context.ConnectionId));
        return base.OnDisconnectedAsync(exception);
    }
}

public class Message
{
    public string Text { get; set; }
    public string Name { get; set; }
}