namespace TeknoMES.Api.Services.Plc.ControlLogix;

public interface IMonitoredTag
{
    event EventHandler<EventArgs>? ValueChanged;
}
