namespace News_API.Models.Users;

using System.ComponentModel.DataAnnotations;

public class UserDataRequest
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}