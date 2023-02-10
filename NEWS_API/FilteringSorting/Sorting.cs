using System.Linq.Expressions;

namespace News_API.FilteringSorting
{
    public class Sorting<T>
    {

        public List<T> GetSorting(string sortOrder, string columnName, IQueryable<T> data)
        {
            IQueryable<T> result;
            var parameter = Expression.Parameter(typeof(T), "x");
            var sortExpression = Expression.Lambda<Func<T, object>>(Expression.Convert(Expression.Property(parameter, columnName), typeof(object)), parameter);
            switch (sortOrder)
            {
                case "asc":
                    result = data.OrderBy(sortExpression);
                    break;
                case "desc":
                    result = data.OrderByDescending(sortExpression);
                    break;
                default:
                    result = data.OrderBy(sortExpression);
                    break;
            }

            return result.ToList();
        }

        //public List<News> GetSorting(string sortOrder, IQueryable<News> _data)
        //{
        //    IQueryable<News> result;
        //    switch (sortOrder)
        //    {
        //        case "asc":
        //            result = _data.OrderBy(x => x.Title);
        //            break;
        //        case "desc":
        //            result = _data.OrderByDescending(x => x.Title);
        //            break;
        //        default:
        //            result = _data.OrderBy(x => x.Title);
        //            break;
        //    }

        //    return result.ToList();
        //}
    }
}
