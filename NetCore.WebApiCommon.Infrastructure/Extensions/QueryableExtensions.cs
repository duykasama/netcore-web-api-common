using System.Linq.Expressions;
using NetCore.WebApiCommon.Infrastructure.Exceptions;

namespace NetCore.WebApiCommon.Infrastructure.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> WhereEquals<T>(this IQueryable<T> query, string propertyName, dynamic value)
    {
        if (string.IsNullOrEmpty(propertyName))
        {
            return query;
        }
        
        var param = Expression.Parameter(typeof(T));
        
        MemberExpression property;
        dynamic correctTypeValue;
        try
        {
            property = Expression.Property(param, propertyName.Trim());
            var correctType = property.Type;
            correctTypeValue = Convert.ChangeType(value, correctType);
        }
        catch (Exception)
        {
            return query.Where(_ => false);
        }
        
        var body = Expression.Equal(
            property,
            Expression.Constant(correctTypeValue)
        );
        
        var call = Expression.Call(
            typeof(Queryable),
            "Where",
            new[] { typeof(T) },
            query.Expression,
            Expression.Lambda<Func<T, bool>>(body, param));
        
        return query.Provider.CreateQuery<T>(call);
    }

    public static IOrderedQueryable<T> SortBy<T>(this IQueryable<T> queryable, string propertyName, bool ascending = true)
    {
        var method = ascending ? "OrderBy" : "OrderByDescending";
        var resultExpression = BuildQueryableMethodCallExpression(typeof(T), propertyName, method, queryable.Expression);
        return (IOrderedQueryable<T>)queryable.Provider.CreateQuery<T>(resultExpression);
    }
    
    public static IOrderedQueryable<T> ThenSortBy<T>(this IOrderedQueryable<T> queryable, string propertyName, bool ascending = true)
    {
        var method = ascending ? "ThenBy" : "ThenByDescending";
        var resultExpression = BuildQueryableMethodCallExpression(typeof(T), propertyName, method, queryable.Expression);
        return (IOrderedQueryable<T>)queryable.Provider.CreateQuery<T>(resultExpression);
    }

    private static MethodCallExpression BuildQueryableMethodCallExpression(Type type, string propertyName, string methodName, Expression expression)
    {
        var property = type.GetProperties().SingleOrDefault(p => string.Equals(propertyName, p.Name, StringComparison.OrdinalIgnoreCase));
        if (property is null || property == default)
        {
            throw new PropertyNotFoundException();
        }
        var param = Expression.Parameter(type, nameof(type));
        var propertyAccess = Expression.MakeMemberAccess(param, property);
        var lambdaExpression = Expression.Lambda(propertyAccess, param);
        return Expression.Call(typeof(Queryable), methodName, new[] { type, property.PropertyType },
            expression, Expression.Quote(lambdaExpression));
    } 
}