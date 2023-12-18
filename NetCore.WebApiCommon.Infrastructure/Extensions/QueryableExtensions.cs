using System.Linq.Expressions;

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
}