using HotChocolate.Types;
using GraphQL.Attributes;
using Data.Entities.Telered;
using Neuro.AI.Graph.QL.Queries;
using GraphQL.Middleware;
using Microsoft.EntityFrameworkCore;
using Neuro.AI.Graph.Repository;

public class CustomQueriesType : ObjectType<CustomQueries>
{
    protected override void Configure(IObjectTypeDescriptor<CustomQueries> descriptor)
    {
        descriptor.Field(t => t.GetCustomOrdersSessions(default!))
			.AddFilterSortingPagination<OrdersSessions>()
			.UseCustomQueryable<OrdersSessions>()
			.Type<CustomType<OrdersSessions>>();


	    descriptor.Field(t => t.GetAllUsersAsync(default!, default!))
			.AddFilterSortingPagination<UserDto>()
			.UseCustomQueryable<UserDto>()
			.Type<CustomType<UserDto>>();
    }
}
