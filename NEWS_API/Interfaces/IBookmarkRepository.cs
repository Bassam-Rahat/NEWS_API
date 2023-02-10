using News_API.Pagination;

namespace News_API.Interfaces
{
    public interface IBookmarkRepository
    {
        string Delete(int id);
        string Save(int id);
        PaginationDTO<News> Get(int page);
        List<News> GetAll();
        PaginationDTO<News> GetFilterAndSorting(int page, string columnName, string find, string sortOrder);
    }
}
