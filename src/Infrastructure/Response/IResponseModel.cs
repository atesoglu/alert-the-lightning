using System.Collections.Generic;

namespace Infrastructure.Response
{
    public interface IResponseModel
    {
        string CorrelationId { get; }
        string Message { get; }

        ICollection<KeyValuePair<string, string>> Params { get; set; }

        void AddParam(string key, string value);
    }

    public interface IResponseModel<out T> : IResponseModel
    {
        T Data { get; }
        int Total { get; }
    }
}