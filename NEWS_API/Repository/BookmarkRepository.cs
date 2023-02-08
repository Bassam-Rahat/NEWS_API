using News_API.Authorization;
using News_API.Interfaces;
using News_API.Models;
using News_API.Pagination;
using System.Security.Claims;

namespace News_API.Repository
{
    public class BookmarkRepository : IBookmarkRepository
    {
        PaginationResult _paginationResult = new PaginationResult();
        private readonly NewsApiContext _context;
        private IJwtUtils _jwtUtils;
        public BookmarkRepository(NewsApiContext context, IJwtUtils jwtutils)
        {
            _context = context;
            _jwtUtils = jwtutils;
        }

        public PaginationDTO<News> Get(int userId, int page)
        {
            //var result = _context.Bookmarks.Include(e => e.UserBookmarkNewsNo).Include
            var bookmarkedIds = _context.BookMarks
            .Where(b => b.UserId == userId)
            .Select(b => b.NewsId)
            .ToList();

            var newsArticles = _context.News
            .Where(n => bookmarkedIds.Contains(n.Id))
            .ToList();

            var result = _paginationResult.GetPagination(page, newsArticles);

            if (result is null)
            {
                return null;
            }
            return (result);
        }

        string IBookmarkRepository.Save(int id, int userId)
        {
            BookMark bookmark = new BookMark();

            var news = _context.News.Where(x => x.Id == id);
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);

            if (user is null && news is null)
                return null;


            bookmark.UserId = userId;
            bookmark.NewsId = id;
            bookmark.CreationDate = DateTime.Now;

            _context.BookMarks.Add(bookmark);
            _context.SaveChanges();

            return ("Bookmark Saved Successfully!");
        }

        string IBookmarkRepository.Delete(int id, int userId)
        {
            var user = _context.BookMarks.Where(x => x.UserId == userId && x.NewsId == id).FirstOrDefault();

            if (user is null)
            {
                return null;
            }

            _context.BookMarks.Remove(user);
            _context.SaveChanges();

            return ("Bookmark Deleted Successfully");
        }



    }
}
