using News_API.Interfaces;

namespace News_API.Repository
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        public int? GetUserId()
        {
            return GetUser()?.Id;
        }

        private User? GetUser()
        {
            return (User?)_httpContextAccessor.HttpContext?.Items.FirstOrDefault(item => item.Key.Equals("User")).Value;
        }
    }
}
