using MIS.Application._ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MIS.Application._Helpers
{
    public class FilterKeyPair
    {
        public string Field { get; set; }
        public string Value { get; set; }
    }

    public class QueryHelper
    {
        public static string GetFilterValue(List<FilterKeyPair> filters, string fieldName)
        {
            return filters.FirstOrDefault(p => p.Field.ToUpper() == fieldName.ToUpper())?.Value;
        }

        public static string GetFilterValue(List<FieldValuePair> filters, string fieldName)
        {
            if (filters is null)
            {
                return null;
            }

            return filters.FirstOrDefault(p => p.Field.ToUpper() == fieldName.ToUpper())?.Value;
        }

        public static IOrderedQueryable<T> Ordering<T>(IQueryable<T> source, string propertyName, bool descending, bool anotherLevel)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), string.Empty); // I don't care about some naming
            MemberExpression property = Expression.PropertyOrField(param, propertyName);
            LambdaExpression sort = Expression.Lambda(property, param);
            MethodCallExpression call = Expression.Call(
                typeof(Queryable),
                (!anotherLevel ? "OrderBy" : "ThenBy") + (descending ? "Descending" : string.Empty),
                new[] { typeof(T), property.Type },
                source.Expression,
                Expression.Quote(sort));

            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        }
    }
}
