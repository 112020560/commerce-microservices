using System;

namespace SharedKernel;

public class ResponseObject<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public DateTime Timestamp { get; set; }
    public string? Message { get; set; }
    public Exception? Exception { get; set; }
}

public class ResponseObject
{
    public bool IsSuccess { get; set; }
    public DateTime Timestamp { get; set; }
    public string? Message { get; set; }
    public Exception? Exception { get; set; }
}
