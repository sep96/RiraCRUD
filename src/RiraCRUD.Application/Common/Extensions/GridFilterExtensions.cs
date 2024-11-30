using RiraCRUD.Application.Common.DTOs.Base;
using RiraCRUD.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RiraCRUD.Application.Common.Extensions
{
    public static class GridFilterExtensions
    {
        /// <summary>
        /// Applies filtering, sorting, and pagination based on the GridFilterDto.
        /// </summary>
        public static IQueryable<T> ApplyGridFilter<T>(this IQueryable<T> query, GridFilterDto filter)
        {
            if (filter == null)
                return query;

            // Apply Filtering
            if (filter.Filters != null)
            {
                foreach (var filterItem in filter.Filters)
                {
                    query = query.ApplyCondition(filterItem.Key, filterItem.Value.Operator, filterItem.Value.Value);
                }
            }

            // Apply Sorting
            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                var isDescending = filter.SortDirection?.Equals("desc", StringComparison.OrdinalIgnoreCase) ?? false;
                query = query.OrderByProperty(filter.SortBy, isDescending);
            }

            // Apply Pagination
            if (int.TryParse(filter.PageIndex, out int pageIndex) && int.TryParse(filter.PageSize, out int pageSize))
            {
                query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }

            return query;
        }
        private static IQueryable<T> OrderByProperty<T>(this IQueryable<T> source, string propertyName, bool descending)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);

            var methodName = descending ? "OrderByDescending" : "OrderBy";
            var result = typeof(Queryable).GetMethods()
                .FirstOrDefault(method => method.Name == methodName && method.GetParameters().Length == 2)
                ?.MakeGenericMethod(typeof(T), property.Type)
                .Invoke(null, new object[] { source, lambda });

            return (IQueryable<T>)result!;
        }
        private static IQueryable<T> ApplyCondition<T>(this IQueryable<T> source, string propertyName, FilterOperator op, object value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var constant = Expression.Constant(value);
            Expression condition;

            switch (op)
            {
                case FilterOperator.Equal:
                    condition = Expression.Equal(property, constant);
                    break;
                case FilterOperator.NotEqual:
                    condition = Expression.NotEqual(property, constant);
                    break;
                case FilterOperator.GreaterThan:
                    condition = Expression.GreaterThan(property, constant);
                    break;
                case FilterOperator.LessThan:
                    condition = Expression.LessThan(property, constant);
                    break;
                case FilterOperator.Contains:
                    if (property.Type != typeof(string))
                        throw new InvalidOperationException($"Contains operation is only supported for strings.");
                    condition = Expression.Call(property, "Contains", null, constant);
                    break;
                default:
                    throw new InvalidOperationException($"Unsupported operator: {op}");
            }

            var lambda = Expression.Lambda<Func<T, bool>>(condition, parameter);
            return source.Where(lambda);
        }
    }
}
