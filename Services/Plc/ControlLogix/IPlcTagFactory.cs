using libplctag;

namespace TeknoMES.Api.Services.Plc.ControlLogix;

public interface IPlcTagFactory
{
    ITag Create(Type dataType, string gateway, string path, string tagName);
    IMonitoredTag CreateMonitored(Type dataType, string gateway, string path, string tagName, int intervalMilliseconds);
}
