namespace News_API.FilteringSorting
{
    public class Sorting
    {
        public List<News> GetSorting(string sortOrder, IQueryable<News> _data)
        {
            IQueryable<News> result;
            switch (sortOrder)
            {
                case "asc":
                    result = _data.OrderBy(x => x.Title);
                    break;
                case "desc":
                    result = _data.OrderByDescending(x => x.Title);
                    break;
                default:
                    result = _data.OrderBy(x => x.Title);
                    break;
            }

            return result.ToList();
        }
    }
}
