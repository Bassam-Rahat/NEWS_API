using News_API.Pagination;

namespace News_API.Interfaces
{
    public interface INewsRepository
    {
        PaginationDTO<News> Get(int page);
        News GetById(int id);
        List<News> Add(News news);
        News Update(int id, News updateRequest);
        string Delete(int id);
        //string DeleteBookmarks(int id, string email);
        //List<BookMark> SaveBookmarkNews(int id, string email);
        //List<News> GetAllBookmarkNews(string email);

    }
}
