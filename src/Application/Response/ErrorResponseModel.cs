using System.Collections.Generic;
using System.Text.Json;

namespace Application.Response
{
    public sealed class ErrorResponseModel : ResponseModel, IErrorResponseModel
    {
        public ICollection<KeyValuePair<string, string>> Errors { get; set; }

        public ErrorResponseModel()
        {
            Errors = new List<KeyValuePair<string, string>>();
        }

        public void AddError(string error)
        {
            AddError("error", error);
        }

        public void AddError(string header, string body)
        {
            Errors ??= new List<KeyValuePair<string, string>>();

            Errors.Add(new KeyValuePair<string, string>(header, body));

            Message ??= "An error occured while processing your request.";
        }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}