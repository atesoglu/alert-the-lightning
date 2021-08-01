using System.Collections.Generic;

namespace Application.Response
{
    public interface IErrorResponseModel : IResponseModel
    {
        ICollection<KeyValuePair<string, string>> Errors { get; set; }

        void AddError(string error);
        void AddError(string header, string body);
    }
}