using News_API.Entities;
using System;
using System.Collections.Generic;

namespace News_API.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public Role Role { get; set; }

    public virtual ICollection<BookMark> BookMarks { get; } = new List<BookMark>();
}
