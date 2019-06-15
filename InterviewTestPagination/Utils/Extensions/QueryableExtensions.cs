using System.Linq;
using System.Linq.Expressions;

namespace InterviewTestPagination.Utils.Extensions {

    public static partial class QueryableExtensions {

        public static IOrderedQueryable<T> OrderByMember<T>(this IQueryable<T> source, string memberPath, bool reverse) {

            var parameter = Expression.Parameter(typeof(T), "item");

            var member = memberPath.Split('.')
                .Aggregate((Expression)parameter, Expression.PropertyOrField);

            var keySelector = Expression.Lambda(member, parameter);

            var methodCall = Expression.Call(
                typeof(Queryable), reverse ? "OrderByDescending" : "OrderBy",
                new[] { parameter.Type, member.Type },
                source.Expression, Expression.Quote(keySelector));

            return (IOrderedQueryable<T>)source.Provider.CreateQuery(methodCall);
        }
    }
}