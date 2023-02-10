using News_API.Authorization;
using News_API.FilteringSorting;
using News_API.Interfaces;
using News_API.Models;
using News_API.Pagination;
using System.Security.Claims;

namespace News_API.Repository
{
    public class BookmarkRepository : IBookmarkRepository
    {
        PaginationResult _paginationResult = new PaginationResult();
        Filtering _filtering = new Filtering();
        Sorting<News> _sorting = new Sorting<News>();

        private readonly NewsApiContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IIdentityService _identityService;
        public BookmarkRepository(NewsApiContext context, IJwtUtils jwtutils, IIdentityService identityService)
        {
            _context = context;
            _jwtUtils = jwtutils;
            _identityService = identityService;
        }

        public PaginationDTO<News> Get(int page)
        {
            int userId = _identityService.GetUserId().Value;
            //var result = _context.Bookmarks.Include(e => e.UserBookmarkNewsNo).Include
            var bookmarkedIds = _context.BookMarks
            .Where(b => b.UserId == userId)
            .Select(b => b.NewsId)
            .ToList();

            var newsArticles = _context.News
            .Where(n => bookmarkedIds.Contains(n.Id))
            .ToList();

            var result = _paginationResult.GetPagination(page, newsArticles.AsQueryable());

            if (result is null)
            {
                return null;
            }
            return (result);
        }

        string IBookmarkRepository.Save(int id)
        {
            BookMark bookmark = new BookMark();

            int userId = _identityService.GetUserId().Value;

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

        string IBookmarkRepository.Delete(int id)
        {
            int userId = _identityService.GetUserId().Value;
            var user = _context.BookMarks.Where(x => x.UserId == userId && x.NewsId == id).FirstOrDefault();

            if (user is null)
            {
                return null;
            }

            _context.BookMarks.Remove(user);
            _context.SaveChanges();

            return ("Bookmark Deleted Successfully");
        }

        public List<News> GetAll()
        {
            int userId = _identityService.GetUserId().Value;

            var bookmarkedIds = _context.BookMarks
                       .Where(b => b.UserId == userId)
                       .Select(b => b.NewsId)
                       .ToList();

            var newsArticles = _context.News
            .Where(n => bookmarkedIds.Contains(n.Id))
            .ToList();

            if (bookmarkedIds is null || newsArticles is null)
            {
                return null;
            }
            return (newsArticles);
        }

        public PaginationDTO<News> GetFilterAndSorting(int page, string columnName, string find, string sortOrder)
        {
            int userId = _identityService.GetUserId().Value;

            var bookmarkedIds = _context.BookMarks
            .Where(b => b.UserId == userId)
            .Select(b => b.NewsId)
            .ToList();

            var newsArticles = _context.News
            .Where(n => bookmarkedIds.Contains(n.Id))
            .ToList();

            var filter = _filtering.GetFiltering<News>(columnName, find, newsArticles.AsQueryable());
            var sort = _sorting.GetSorting(sortOrder,columnName, filter.AsQueryable());
            var result = _paginationResult.GetPagination(page, filter.AsQueryable());
            _context.SaveChanges();
            return result;
        }
    }
}
