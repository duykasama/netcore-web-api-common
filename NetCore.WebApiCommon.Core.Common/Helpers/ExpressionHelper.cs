using System.Linq.Expressions;

namespace NetCore.Architecture.Core.Common.Helpers;

public static class ExpressionHelper
{
    public static Expression<Func<T, bool>> CombineExpressionsWithOr<T>(Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right)
    {
        var param = Expression.Parameter(typeof(T), typeof(T).Name);

        var combinedExpr = Expression.Or(
            Expression.Invoke(left, param),
            Expression.Invoke(right, param));

        return Expression.Lambda<Func<T, bool>>(combinedExpr, param);
    }
    
    public static Expression<Func<T, bool>> CombineExpressionsWithAnd<T>(Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right)
    {
        var param = Expression.Parameter(typeof(T), typeof(T).Name);

        var combinedExpr = Expression.And(
            Expression.Invoke(left, param),
            Expression.Invoke(right, param));

        return Expression.Lambda<Func<T, bool>>(combinedExpr, param);
    }
}