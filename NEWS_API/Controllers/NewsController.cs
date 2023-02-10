using Microsoft.AspNetCore.Mvc;
using News_API.Interfaces;
using News_API.Authorization;
using News_API.Entities;
using News_API.Pagination;

namespace News_API.Controllers
{
    [Authorize]
    [ApiController] 
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {
        private INewsRepository NewsRepository;
        public NewsController(INewsRepository newsRepository)
        {
            NewsRepository = newsRepository;
        }

        [AllowAnonymous]
        [HttpGet("GetPaginatedNews")]
        public async Task<ActionResult<PaginationDTO<News>>> Get(int page)
        {
            var result = NewsRepository.Get(page);
            return Ok(result);

        }

        [AllowAnonymous]
        [HttpGet("GetFiltering&Sorting")]
        public async Task<ActionResult<PaginationDTO<News>>> GetFilterAndSorting(int page, string columnName, string find, string sortOrder)
        {
            var result = NewsRepository.GetFilterAndSorting(page, columnName, find, sortOrder);
            return Ok(result);

        }

        [AllowAnonymous]
        [HttpGet("GetAllNews")]
        public async Task<ActionResult<List<News>>> GetAll()
        {
            var result = NewsRepository.GetAll();

            if (result is null)
            {
                return NotFound("No News Found");
            }
            return Ok(result);

        }

        [Authorize(Role.Admin, Role.User)]
        [HttpGet("GetById")]
        public async Task<ActionResult<News>> GetById(int id)
        {
            var result = NewsRepository.GetById(id);

            if (result is null)
            {
                return NotFound("No News Found");
            }
            return Ok(result);
        }

        //        [HttpGet("SeeAllBookmarks")]
        //        public async Task<ActionResult<List<News>>> GetAllBookmarkNews()
        //        {
        //            var result = NewsRepository.GetAllBookmarkNews(GetCurrentUserEmail());

        //            if (result is null)
        //            {
        //                return NotFound("No News Found");
        //            }
        //            return Ok(result);
        //        }

        [Authorize(Role.Admin)]
        [HttpPost("PostNews")]
        public async Task<ActionResult<List<News>>> Add(News news)
        {
            var result = NewsRepository.Add(news);
            return Ok(result);
        }

        //        [HttpPost("SaveBookmarkNews")]
        //        public async Task<ActionResult<List<BookMark>>> SaveBookmarkNews(int id)
        //        {
        //            var result = NewsRepository.SaveBookmarkNews(id, GetCurrentUserEmail());

        //            if (result is null)
        //            {
        //                return NotFound("Enter the valid email or News ID");
        //            }
        //            return Ok(result);
        //        }

        [Authorize(Role.Admin)]
        [HttpPut("UpdateNews")]
        public async Task<ActionResult<News>> Update(int id, News updateRequest)
        {
            var result = NewsRepository.Update(id, updateRequest);

            if (result is null)
            {
                return NotFound("No News Found");
            }
            return Ok(result);
        }

        [Authorize(Role.Admin)]
        [HttpDelete("DeleteNews")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var result = NewsRepository.Delete(id);

            if (result is null)
            {
                return NotFound("No News Found");
            }
            return Ok(result);
        }

        //        [HttpDelete("DeleteBookmarks")]
        //        public async Task<ActionResult<string>> DeleteBookmarks(int id)
        //        {
        //            var result = NewsRepository.DeleteBookmarks(id, GetCurrentUserEmail());

        //            if (result is null)
        //            {
        //                return NotFound("No News with such ID Found");
        //            }
        //            return Ok(result);
        //        }


        //        private string GetCurrentUserEmail()
        //        {
        //            var email = string.Empty;
        //            if (HttpContext.User.Identity is ClaimsIdentity identity)
        //            {
        //                email = identity.FindFirst(ClaimTypes.Name).Value;
        //            }
        //            return email;
        //        }

    }
    }
