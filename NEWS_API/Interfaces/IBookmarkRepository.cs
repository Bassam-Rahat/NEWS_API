using News_API.Pagination;

namespace News_API.Interfaces
{
    public interface IBookmarkRepository
    {
        string Delete(int id, int userId);
        string Save(int id, int userId);
        PaginationDTO<News> Get(int id, int page);
    }
}
