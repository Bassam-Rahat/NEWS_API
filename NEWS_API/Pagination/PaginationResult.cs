namespace News_API.Pagination
{
    public class PaginationResult
    {
        public PaginationDTO<T> GetPagination<T>(int page, IQueryable<T> items)
        {
            var pageResults = 3f;
            var pageCount = Math.Ceiling(items.Count() / pageResults);

            items = items
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults);
            var findItems = items.ToList();
                //.ToList();

            var paginationResponse = new PaginationDTO<T>
            {
                Items = findItems,
                CurrentPage = page,
                Pages = (int)pageCount
            };

            return paginationResponse;
        }
    }
}
