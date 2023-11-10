using System.Linq.Expressions;

namespace API.Repository
{
    public static class RepositoryExtensions
    {
        public static IOrderedQueryable<TSource> Sort<TSource, TKey>(this IQueryable<TSource> source,
            Expression<Func<TSource, TKey>> keySelector, bool descending = false)
        {
            source = source ?? throw new ArgumentNullException(nameof(source));

            return descending 
                ? source.OrderByDescending(keySelector) 
                : source.OrderBy(keySelector);
        }

        public static IQueryable<TSource> Filter<TSource, TKey>(this IQueryable<TSource> source,
            TKey? filterBy, Expression<Func<TSource, bool>> predicate)
        {
            return filterBy != null ? source.Where(predicate) : source;
        }
    }
}
