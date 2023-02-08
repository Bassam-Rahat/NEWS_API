namespace News_API.Models.Users;

using News_API.Entities;

public class UserDataResponse
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public string Token { get; set; }

    public UserDataResponse(User user, string token)
    {
        Id = user.Id;
        UserName = user.UserName;
        Email = user.Email;
        Password = user.Password;
        Role = user.Role;
        Token = token;
    }
}