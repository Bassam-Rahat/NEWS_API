namespace News_API.Pagination
{
    public class PaginationDTO<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
