using Common.Enum;

namespace Common.Interface
{
    public interface IJSend<T>
    {
        StatusResponseEnum StatusAsEnum { get; set; }

        public string Status { get; }

        public T Data { get; set; }
    }
}
