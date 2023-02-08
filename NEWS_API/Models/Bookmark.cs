using System;
using System.Collections.Generic;

namespace News_API.Models;

public partial class BookMark
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int NewsId { get; set; }

    public DateTime CreationDate { get; set; }

    public virtual News News { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
