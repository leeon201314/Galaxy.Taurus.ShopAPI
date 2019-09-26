namespace Galaxy.Taurus.OrderingAPI.Entitys.DTO
{
    public class PageDataDTO<T>
    {
        public int Total { get; set; }

        public T Data { get; set; }
    }
}
