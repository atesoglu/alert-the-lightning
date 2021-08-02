using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Infrastructure.Response
{
    public class ResponseModel : IResponseModel
    {
        public string CorrelationId { get; set; }
        public string Message { get; set; }

        public ICollection<KeyValuePair<string, string>> Params { get; set; }

        public ResponseModel()
        {
            CorrelationId = Guid.NewGuid().ToString("N");
            Params = new List<KeyValuePair<string, string>>();
        }


        public void AddParam(string key, string value)
        {
            Params ??= new List<KeyValuePair<string, string>>();

            Params.Add(new KeyValuePair<string, string>(key, value));
        }

        public override string ToString() => JsonSerializer.Serialize(this);
    }

    public sealed class ResponseModel<T> : ResponseModel, IResponseModel<T>
    {
        public T Data { get; set; }
        public int Total { get; set; }
        
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}