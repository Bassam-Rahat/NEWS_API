namespace News_API.Pagination
{
    public class PaginationResult
    {
        public PaginationDTO<T> GetPagination<T>(int page, List<T> items)
        {
            var pageResults = 3f;
            var pageCount = Math.Ceiling(items.Count() / pageResults);

            var findNews = items
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToList();

            var paginationResponse = new PaginationDTO<T>
            {
                Items = findNews,
                CurrentPage = page,
                Pages = (int)pageCount
            };

            return paginationResponse;
        }
    }
}
