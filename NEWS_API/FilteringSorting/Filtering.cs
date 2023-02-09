namespace News_API.FilteringSorting
{
    public class Filtering
    {
        public List<News> GetFiltering(string username, IQueryable<News> _data)
        {
            var result = _data.Where(x => x.Aurthor.ToLower().Contains(username.ToLower()));
            return result.ToList();
        }
    }
}
