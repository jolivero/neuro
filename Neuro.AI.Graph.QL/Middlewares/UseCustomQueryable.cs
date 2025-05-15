using GraphQL.Attributes;
using HotChocolate.Resolvers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GraphQL.Middleware
{
	public static class CustomQueryableMiddleware
	{
		public class FilterInput
		{
			public string Field { get; set; } = default!;
			public string Value { get; set; } = default!;
			public string Condition { get; set; } = "eq"; // eq, neq, gt, lt, contains.
		}

		private static Expression BuildCaseInsensitiveContains(Expression property, Expression value)
		{
			var toLowerMethod = typeof(string).GetMethod(nameof(string.ToLower), Type.EmptyTypes)!;
			var containsMethod = typeof(string).GetMethod(nameof(string.Contains), [typeof(string)])!;

			var propertyToLower = Expression.Call(property, toLowerMethod);
			var valueToLower = Expression.Call(value, toLowerMethod);

			return Expression.Call(propertyToLower, containsMethod, valueToLower);
		}

		public static IObjectFieldDescriptor UseCustomQueryable<T>(
			this IObjectFieldDescriptor descriptor,
			int defaultPageSize = 10,
			int maxPageSize = 100)
		{
			return descriptor.Use(
				// Middleware para aplicar filtros, ordenación y paginación
				next => async context =>
				{
					// Ejecutamos el resolver y obtenemos el IQueryable<T>
					await next(context);

					// Si no es un IQueryable<T>, no hacemos nada
					if (context.Result is not IQueryable<T> queryable) return;

					// Filtros
					var filters = context.ArgumentValue<List<FilterInput>>("filter");
					if (filters != null)
					{
						foreach (var filter in filters)
						{
							var prop = typeof(T)
								.GetProperties()
								.FirstOrDefault(p => string.Equals(p.Name, filter.Field, StringComparison.OrdinalIgnoreCase));

							if (prop != null)
							{
								var parameter = Expression.Parameter(typeof(T), "x");
								var propertyAccess = Expression.Property(parameter, prop);
								var value = Convert.ChangeType(filter.Value, prop.PropertyType);
								var constant = Expression.Constant(value);
								Expression body = filter.Condition?.ToLower() switch
								{
									"neq" => Expression.NotEqual(propertyAccess, constant),
									"gt" => Expression.GreaterThan(propertyAccess, constant),
									"lt" => Expression.LessThan(propertyAccess, constant),

									"contains" when prop.PropertyType == typeof(string)
										=> BuildCaseInsensitiveContains(propertyAccess, constant),
									_ => Expression.Equal(propertyAccess, constant)
								};

								var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
								queryable = queryable.Where(lambda);
							}
						}
					}


					// Ordenación
					var sortProperty = context.ArgumentValue<string>("sort");
					var sortDirection = context.ArgumentValue<string>("sortDir")?.ToLowerInvariant() ?? "asc";

					if (!string.IsNullOrWhiteSpace(sortProperty))
					{
						var propertyInfo = typeof(T)
							.GetProperties()
							.FirstOrDefault(p => string.Equals(p.Name, sortProperty, StringComparison.OrdinalIgnoreCase));

						if (propertyInfo is not null)
						{
							var parameter = Expression.Parameter(typeof(T), "x");
							var propertyAccess = Expression.Property(parameter, propertyInfo);
							var orderByLambda = Expression.Lambda(propertyAccess, parameter);

							var methodName = sortDirection == "desc" ? "OrderByDescending" : "OrderBy";

							var orderedQuery = Expression.Call(
								typeof(Queryable),
								methodName,
								[typeof(T), propertyInfo.PropertyType],
								queryable.Expression,
								Expression.Quote(orderByLambda));

							queryable = queryable.Provider.CreateQuery<T>(orderedQuery);
						}
					}


					// Paginación
					bool hasPage = context.Selection.Field.Arguments.Any(a => a.Name == "page");
					bool hasPageSize = context.Selection.Field.Arguments.Any(a => a.Name == "pageSize");

					int? pageArg = hasPage ? context.ArgumentValue<int?>("page") : null;
					int? pageSizeArg = hasPageSize ? context.ArgumentValue<int?>("pageSize") : null;

					int page = pageArg ?? 1;
					int pageSize = pageSizeArg ?? 0;
					int totalCount = queryable.Count();

					var items = (pageSize > 0)
						? queryable.Skip((page - 1) * pageSize).Take(pageSize).ToList()
						: queryable.ToList();

					int effectivePageSize = pageSize > 0 ? pageSize : totalCount;

					context.Result = new Paged<T>(items, totalCount, page, effectivePageSize);

				}
			);
		}
	}
}
