using Microsoft.AspNetCore.Http;

namespace BHEP.Contract.Abstractions.Shared;
public class Result<TData> : Result
{
    private readonly TData? _data;
    public Result(TData? data, bool isSuccess, string error, int statusCode = 200)
        : base(isSuccess, error, statusCode) =>
        _data = data;
    public TData data => IsSuccess
        ? _data!
        : string.IsNullOrEmpty(Message)
        ? throw new InvalidOperationException("The data of a failure result can not be accessed.")
        : throw new BadHttpRequestException(Message);

    public static implicit operator Result<TData>(TData? data) => Create(data);
}
