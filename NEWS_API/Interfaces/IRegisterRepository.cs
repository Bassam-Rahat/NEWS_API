namespace News_API.Interfaces
{
    using News_API.Models.Users;
    using News_API.Pagination;

    public interface IRegisterRepository
    {
        UserDataResponse Authenticate(UserDataRequest model);
        PaginationDTO<User> GetAll(int page);
        List<User> Get();
        User GetById(int id);
        string Add(User User);
        string Delete(int id);

    }
}
