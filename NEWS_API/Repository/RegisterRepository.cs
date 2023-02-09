

using Microsoft.Extensions.Options;
using News_API.Authorization;
using News_API.Helpers;
using News_API.Interfaces;
using News_API.Models;
using News_API.Models.Users;
using News_API.Pagination;
using Org.BouncyCastle.Crypto.Generators;
using System.Collections.Generic;

public class RegisterRepository : IRegisterRepository
{
    PaginationResult _paginationResult = new PaginationResult();

    private NewsApiContext _context;
    private IJwtUtils _jwtUtils;
    private readonly AppSettings _appSettings;

    public RegisterRepository(NewsApiContext context, IJwtUtils jwtutils,IOptions<AppSettings> appSettings)
    {
        _context = context;
        _jwtUtils = jwtutils;
        _appSettings = appSettings.Value;
    }
    public UserDataResponse Authenticate(UserDataRequest model)
    {
        var user = _context.Users.SingleOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);

        // validate
        if (user == null)
            throw new AppException("Username or password is incorrect");

        // authentication successful so generate jwt token
        var jwtToken = _jwtUtils.GenerateJwtToken(user);

        return new UserDataResponse(user, jwtToken);
    }

    public PaginationDTO<User> GetAll(int page)
    {
        var result = _paginationResult.GetPagination<User>(page, _context.Users.AsQueryable());
        return result;
    }
    public List<User> GetAll()
    {
        var result =  _context.Users.ToList();
        return result;
    }

    public User GetById(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }

    public string Add(User User)
    {
        _context.Users.Add(User);
        _context.SaveChanges();

        return ("Added Successfully!");
    }

    public string Delete(int id)
    {
        var user = _context.Users.FirstOrDefault(x => x.Id == id);
        var bookmarks = _context.BookMarks.Where(b => b.UserId == id);

        if (user == null)
        {
            return null;
        }

        _context.BookMarks.RemoveRange(bookmarks);
        _context.Users.Remove(user);

        _context.SaveChanges();
        return ("User Deleted Successfully!");
    }

    public List<User> Get()
    {
        if (_context.Users is null)
        {
            return null;
        }
        _context.SaveChanges();
        return (_context.Users.ToList());
    }
}