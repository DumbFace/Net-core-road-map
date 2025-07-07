using Common.Interface;
using Domain.Enum;
using System.Text.Json.Serialization;

namespace Common.Models.BaseModels
{
    public class ApiResponseModel<T> : IJSend<T>
    {
        [JsonIgnore]
        public StatusResponseEnum StatusAsEnum { get; set; } = StatusResponseEnum.Success;

        public string Status { get => StatusAsEnum.ToString(); }

        public T Data { get; set; }
    }
}
