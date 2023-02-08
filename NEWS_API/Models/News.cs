using System;
using System.Collections.Generic;

namespace News_API.Models;

public partial class News
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Aurthor { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public virtual ICollection<BookMark> BookMarks { get; } = new List<BookMark>();
}
