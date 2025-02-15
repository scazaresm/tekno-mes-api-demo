namespace TeknoMES.Api.Services.Exceptions;

public class UnsupportedTagTypeException : Exception
{
    public UnsupportedTagTypeException() { }

    public UnsupportedTagTypeException(string? message) : base(message) { }

    public UnsupportedTagTypeException(string? message, Exception innerException) : base(message, innerException) { }
}
