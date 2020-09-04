namespace API.Models
{
    public class BaseResponse<T>
    {
        public int StatusCode { get; set; }
        public string MessageError { get; set; }
        public T Result { get; set; }
    }
}
