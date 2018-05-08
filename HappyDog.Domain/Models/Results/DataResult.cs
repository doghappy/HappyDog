namespace HappyDog.Domain.Models.Results
{
    public class DataResult<T>:BaseResult
    {
        public T Data { get; set; }
    }
}
