using System;
using Newtonsoft.Json;

namespace AMTApi.Models
{
    public class ResultModel
    {
        public ResultModel(string msg, bool isError){
            Message = msg;
            IsError = isError;
        }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("isError")]
        public bool IsError { get; set; }
    }
}
