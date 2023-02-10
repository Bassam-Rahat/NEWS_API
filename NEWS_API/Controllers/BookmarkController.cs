using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News_API.Authorization;
using News_API.Entities;
using News_API.Interfaces;
using News_API.Models;
using News_API.Pagination;
using News_API.Repository;
using System.Security.Claims;
using System.Security.Principal;

namespace News_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkRepository _bookmarkRepository;

        public BookmarkController(IBookmarkRepository bookmarkRepository)
        {
            _bookmarkRepository = bookmarkRepository;
        }

        [Authorize(Role.User)]
        [HttpPost("SaveBookmarkNews")]
        public async Task<ActionResult<string>> Save(int id)
        {
            var result = _bookmarkRepository.Save(id);

            if (result is null)
            {
                return NotFound("Enter the valid email or News ID");
            }
            return Ok(result);
        }

        [Authorize(Role.User)]
        [HttpGet("SeeAllPaginatedBookmarks")]
        public async Task<ActionResult<PaginationDTO<News>>> Get(int page)
        {
            var result = _bookmarkRepository.Get(page);

            if (result is null)
            {
                return NotFound("No News Found");
            }
            return Ok(result);
        }

        [Authorize(Role.User)]
        [HttpGet("GetFiltering&Sorting")]
        public async Task<ActionResult<PaginationDTO<News>>> GetFilterAndSorting(int page, string columnName, string find, string sortOrder)
        {
            var result = _bookmarkRepository.GetFilterAndSorting(page, columnName, find, sortOrder);
            return Ok(result);

        }

        [Authorize(Role.User)]
        [HttpGet("SeeAllBookmarks")]
        public async Task<ActionResult<List<News>>> GetAll()
        {
            var result = _bookmarkRepository.GetAll();

            if (result is null)
            {
                return NotFound("No News Found");
            }
            return Ok(result);
        }

        [Authorize(Role.User)]
        [HttpDelete("DeleteBookmarks")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var result = _bookmarkRepository.Delete(id);

            if (result is null)
            {
                return NotFound("No News with such ID Found");
            }
            return Ok(result);
        }

        //private int GetCurrentUserId()
        //{
        //    var id=string.Empty;
        //    if (HttpContext.User.Identity is ClaimsIdentity identity)
        //    {
        //        id = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    }
        //    return int.Parse(id);
        //}
    }
    }
