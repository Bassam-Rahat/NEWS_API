﻿using Microsoft.EntityFrameworkCore;
using News_API.Interfaces;
using News_API.Models;
using News_API.Pagination;
using System.Data;

namespace News_API.Repository
{
    public class NewsRepository : INewsRepository
    {
        PaginationResult _paginationResult = new PaginationResult();
        private readonly NewsApiContext _context;
        public NewsRepository(NewsApiContext context)
        {
            _context = context;
        }

        public List<News> Add(News news)
        {
            _context.News.Add(news);
            _context.SaveChanges();
            return _context.News.ToList();
        }

        public string Delete(int id)
        {
            var _News = _context.News.FirstOrDefault(x => x.Id == id);
            //var _Bookmark = _context.BookMarks.FirstOrDefault(x => x.NewsId == id);

            if (_News == null)
            {
                return null;
            }

            _context.News.Remove(_News);
            //_context.BookMarks.Remove(_Bookmark);

            _context.SaveChanges();
            return "News Deleted Successfully";
        }

        public PaginationDTO<News> Get(int page)
        {
            var result = _paginationResult.GetPagination(page, _context.News.ToList());
            _context.SaveChanges();
            return result;
        }

        public News? GetById(int id)
        {
            var _News = _context.News.FirstOrDefault(x => x.Id == id);

            if (_News == null)
            {
                return null;
            }
            _context.SaveChanges();
            return _News;
        }

        public News? Update(int id, News updateRequest)
        {
            var _News = _context.News.FirstOrDefault(x => x.Id == id);

            if (_News == null)
            {
                return null;
            }

            _News.Title = updateRequest.Title;
            _News.Aurthor = updateRequest.Aurthor;
            _News.Content = updateRequest.Content;



            _context.SaveChanges();
            return _News;
        }

        //        string INewsRepository.DeleteBookmarks(int id, string email)
        //        {
        //            var user = _context.Bookmarks.Where(x => x.UserEmail == email && x.UserBookmarkNewsNo == id).FirstOrDefault();

        //            if (user is null)
        //            {
        //                return null;
        //            }

        //            _context.Bookmarks.Remove(user);
        //            _context.SaveChanges();

        //            return ("Bookmark Deleted Successfully");
        //        }

        //        List<News> INewsRepository.GetAllBookmarkNews(string email)
        //        {
        //            //var result = _context.Bookmarks.Include(e => e.UserBookmarkNewsNo).Include
        //            var bookmarkedIds = _context.Bookmarks
        //            .Where(b => b.UserEmail == email)
        //            .Select(b => b.UserBookmarkNewsNo)
        //            .ToList();

        //            var newsArticles = _context.News
        //            .Where(n => bookmarkedIds.Contains(n.Id))
        //            .ToList();

        //            if (bookmarkedIds is null)
        //            {
        //                return null;
        //            }
        //            return (newsArticles);
        //        }

        //        List<Bookmark> INewsRepository.SaveBookmarkNews(int id, string email)
        //        {
        //            Bookmark bookmark = new Bookmark();
        //            var news = _context.News.Where(x => x.Id == id);

        //            if (email is null && news is null)
        //                return null;

        //            bookmark.UserEmail = email;
        //            bookmark.UserBookmarkNewsNo = id;

        //            _context.Bookmarks.Add(bookmark);
        //            _context.SaveChanges();

        //            return (_context.Bookmarks.ToList());
        //        }
    }
    }