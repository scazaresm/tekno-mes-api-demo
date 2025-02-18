using libplctag;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace TeknoMES.Api.Services.Plc.ControlLogix;

public class TagMonitoringService(
    ILogger<TagMonitoringService> logger,
    IPlcTagFactory tagFactory,
    IHubContext<PlcHub> hubContext) : ITagMonitoringService
{
    private readonly ConcurrentDictionary<string, ITag> _monitoredTags = new();

    public async Task StartMonitoringAsync<T>(string gateway, string path, string tagName, int intervalMilliseconds)
    {
        var tagKey = GetTagKey(gateway, path, tagName);

        if (_monitoredTags.ContainsKey(tagKey))
        {
            logger.LogInformation("Tag {tagName} is already being monitored at {gateway} {path}.", tagName, gateway, path);
            return;
        }

        var monitoredTag = tagFactory.CreateMonitored(typeof(T), gateway, path, tagName, intervalMilliseconds);
        monitoredTag.ValueChanged += OnValueChanged;

        if (monitoredTag is not ITag tag)
        {
            throw new InvalidOperationException($"Tag {tagName} is not of a valid tag type.");
        }

        await tag.InitializeAsync();
        _monitoredTags.TryAdd(tagKey, tag);
        logger.LogInformation("Started monitoring commands at {gateway} {path}.", gateway, path);
    }

    public void StopMonitoring(string gateway, string path, string tagName)
    {
        var tagKey = GetTagKey(gateway, path, tagName);

        if (!_monitoredTags.TryGetValue(tagKey, out var tag))
        {
            logger.LogInformation("Tag {tagName} is not being monitored at {gateway} {path}.", tagName, gateway, path);
            return;
        }

        if (tag is not IMonitoredTag monitoredTag)
        {
            throw new InvalidOperationException($"Tag {tagName} is not of a valid tag type.");
        }

        monitoredTag.ValueChanged -= OnValueChanged;
        _monitoredTags.TryRemove(tagKey, out _);
        logger.LogInformation("Stopped monitoring tag {tagName} at {gateway} {path}.", tagName, gateway, path);
    }

    private void OnValueChanged(object? sender, EventArgs e)
    {
        if (sender is not ITag tag) return;

        hubContext.Clients.All.SendAsync("TagValueChanged", tag);

        logger.LogInformation("Tag {tagName} has changed it value at {gateway} {path}: {tagValue}", 
            tag.Name, tag.Gateway, tag.Path, tag.Value);
    }

    private static string GetTagKey(string gateway, string path, string commandTagName)
        => $"{gateway}-{path}-{commandTagName}";
}
