namespace Template.Application.Helpers;

public class ServiceResultHelper<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }

    public static ServiceResultHelper<T> Ok(T data) =>
        new() { Success = true, Data = data };

    public static ServiceResultHelper<T> Fail(string? message = null) =>
        new() { Success = false, Message = message };
}