using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GraphQL.Attributes
{
	public static class CustomQueriesAttribute
	{
		public static IObjectFieldDescriptor AddFilterSortingPagination<T>(this IObjectFieldDescriptor descriptor)
		{
			return descriptor
				.Argument("filter", a => a.Type<ListType<FilterInputType>>())
				.Argument("sort", a => a.Type<StringType>())
				.Argument("sortDir", a => a.Type<StringType>())
				.Argument("page", a => a.Type<IntType>())
				.Argument("pageSize", a => a.Type<IntType>());
		}
	}

	public class FilterInputType : InputObjectType
	{
		protected override void Configure(IInputObjectTypeDescriptor descriptor)
		{
			descriptor.Name("FilterInput");

			descriptor.Field("field")
				.Type<NonNullType<StringType>>()
				.Description("El campo que se va a filtrar");

			descriptor.Field("value")
				.Type<NonNullType<StringType>>()
				.Description("El valor que se va a comparar");

			descriptor.Field("condition")
				.Type<StringType>()
				.DefaultValue("eq")
				.Description("Condición: eq, neq, gt, lt, contains.");
		}
	}

	// El tipo de objeto para la paginación
	public class CustomType<T> : ObjectType<Paged<T>>
	{
		protected override void Configure(IObjectTypeDescriptor<Paged<T>> descriptor)
		{
			descriptor.Name($"Custom{typeof(T).Name}");
			descriptor.Field(x => x.Page).Type<NonNullType<IntType>>().Description("Página actual");
			descriptor.Field(x => x.PageSize).Type<NonNullType<IntType>>().Description("Tamaño de la página");
			descriptor.Field(x => x.TotalPages).Type<NonNullType<IntType>>().Description("Número total de páginas");
			descriptor.Field(x => x.TotalCount).Type<NonNullType<IntType>>().Description("Número total de elementos");
		}
	}

	// Record para la paginación
	public record Paged<T>(IEnumerable<T> Items, int TotalCount, int Page, int PageSize)
	{
		public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
	}
}