using System.ComponentModel.DataAnnotations;
using GraphQL.Attributes;

namespace Neuro.AI.Graph.QL.Extensions;

public class PagedParams
{
    [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than 0.")]
    public int Page { get; set; } = 1;

    [Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100.")]
    public int Size { get; set; } = 10;
}

public static class QueryableExtensions
{
	/// <summary>
	/// (Mode-Offset-Based) Aplica paginación manual a una colección en memoria. 
	/// </summary>
	/// <remarks>
	/// Alternativa a `[UseOffsetPaging]` de versiones anteriores de HotChocolate. <br/>
	/// No compatible con -> `[UsePaging]`, `[UseFiltering]` ni `[UseSorting]` ⚠️. <br/>
	/// Para integrar con GraphQL y usar paginación, filtrado y ordenación, usa <see cref="IQueryable{T}"/>.
	/// </remarks>
	/// <returns><see cref="Paged{T}"/> con los resultados paginados, de tipo <see cref="IEnumerable{T}"/></returns>
    public static Paged<T> ToPaged<T>(this IQueryable<T> source, PagedParams p)
    {
        var page = Math.Max(p.Page, 1);
        var size = Math.Max(p.Size, 1);

        var totalCount = source.Count();

        var items = source.Skip((page - 1) * size).Take(size);

        return new Paged<T>(items, totalCount, page, size);
    }
}


