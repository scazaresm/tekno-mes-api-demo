using libplctag;
using libplctag.DataTypes;

namespace TeknoMES.Api.Services.Plc.ControlLogix;

public class MonitoredTag<M, T> : Tag<M, T>, IMonitoredTag
    where M : IPlcMapper<T>, new()
{
    public event EventHandler<EventArgs>? ValueChanged;

    private int previousHash;

    public MonitoredTag()
    {
        ReadCompleted += NotifyValueChangedTag_ReadCompleted;
    }

    private void NotifyValueChangedTag_ReadCompleted(object? sender, TagEventArgs e)
    {
        var currentHash = Value!.GetHashCode();
        if (currentHash != previousHash)
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
        previousHash = currentHash;
    }
}
