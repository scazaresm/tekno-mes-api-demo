namespace TeknoMES.Api.Requests;

public sealed record TagReadRequest(string Gateway, string Path, string TagName);
