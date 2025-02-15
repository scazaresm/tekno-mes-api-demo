namespace TeknoMES.Api.Requests;

public record TagWriteRequest(string Gateway, string Path, string TagName, object Value);