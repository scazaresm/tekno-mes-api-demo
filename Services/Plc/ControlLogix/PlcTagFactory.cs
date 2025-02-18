using libplctag;
using libplctag.DataTypes;
using libplctag.DataTypes.Simple;
using TeknoMES.Api.Services.Exceptions;

namespace TeknoMES.Api.Services.Plc.ControlLogix;

public class PlcTagFactory : IPlcTagFactory
{
    private readonly Dictionary<Type, Func<ITag>> _simpleTagTypes = new()
    {
        { typeof(string), () => GetTagString() },
        { typeof(bool), () => GetTagBool() }
    };

    private readonly Dictionary<Type, Func<IMonitoredTag>> _monitoredTagTypes = new()
    {
        { typeof(int), () => GetMonitoredTagDInt() }
    };


    public ITag Create(Type dataType, string gateway, string path, string tagName)
    {
        if (!_simpleTagTypes.TryGetValue(dataType, out var factory))
        {
            throw new UnsupportedTagTypeException(
                $"The {typeof(PlcTagFactory).Name} cannot create a tag of type {dataType.Name} because it is not supported.");
        }

        var tag = factory();
        tag.Gateway = gateway;
        tag.Path = path;
        tag.Name = tagName;

        return tag;
    }

    public IMonitoredTag CreateMonitored(Type dataType, string gateway, string path, string tagName, int intervalMilliseconds)
    {
        if (!_monitoredTagTypes.TryGetValue(dataType, out var factory))
        {
            throw new UnsupportedTagTypeException(
                $"The {typeof(PlcTagFactory).Name} cannot create a monitored tag of type {dataType.Name} because it is not supported.");
        }

        var monitoredTag = factory();

        if (monitoredTag is not ITag tag)
        {
            throw new InvalidOperationException($"Tag {tagName} is not of a valid tag type.");
        }

        tag.Gateway = gateway;
        tag.Path = path;
        tag.Name = tagName;
        tag.AutoSyncReadInterval = TimeSpan.FromMilliseconds(intervalMilliseconds);

        return (IMonitoredTag)tag;
    }


    private static TagString GetTagString() =>
        new()
        {
            Name = string.Empty,
            Gateway = string.Empty,
            Path = string.Empty,
            PlcType = PlcType.ControlLogix,
            Protocol = Protocol.ab_eip
        };

    private static TagBool GetTagBool() =>
        new()
        {
            Name = string.Empty,
            Gateway = string.Empty,
            Path = string.Empty,
            PlcType = PlcType.ControlLogix,
            Protocol = Protocol.ab_eip
        };

    private static MonitoredTag<DintPlcMapper, int> GetMonitoredTagDInt() =>
        new()
        {
            Name = string.Empty,
            Gateway = string.Empty,
            Path = string.Empty,
            PlcType = PlcType.ControlLogix,
            Protocol = Protocol.ab_eip
        };

}
