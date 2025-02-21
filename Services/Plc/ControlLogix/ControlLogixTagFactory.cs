﻿using libplctag;
using libplctag.DataTypes.Simple;
using TeknoMES.Api.Services.Exceptions;

namespace TeknoMES.Api.Services.Plc.ControlLogix;

public class ControlLogixTagFactory
{
    private static readonly Dictionary<Type, Func<ITag>> _supportedTagTypes = new()
    {
        { typeof(string), () => GetTagString() },
        { typeof(bool), () =>GetTagBool() }
    };


    public static ITag CreateTag(Type dataType, string gateway, string path, string tagName)
    {
        if (!_supportedTagTypes.TryGetValue(dataType, out var factory))
        {
            throw new UnsupportedTagTypeException(
                $"The {typeof(ControlLogixTagFactory).Name} cannot create a tag of type {dataType.Name} because it is not supported.");
        }

        var tag = factory();
        tag.Gateway = gateway;
        tag.Path = path;
        tag.Name = tagName;

        return tag;
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
}
