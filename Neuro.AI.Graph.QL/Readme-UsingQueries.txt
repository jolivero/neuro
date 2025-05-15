==============================================================================
README - Filtros , Ordenes y Paginación 
==============================================================================

public async Task<IQueryable<UserDto>> GetAllUsersAsync(UserRepository repository, int? filters)
{
	var users = await repository.GetAllUsersAsync(DateTime.Now.AddYears(-1), filters);

	return users.AsQueryable(); 
}

==============================================================================

Este proyecto soporta dos tipos de filtrado y paginación en la consulta:

1. Filtro Manual (.AddFilterSortingPagination())

2. Filtro Automático (HotChocolate con UsePaging, UseFiltering, UseSorting)


───────────────────────────────────────────────
1. FILTRO MANUAL
───────────────────────────────────────────────

📌 Ejemplo de consulta:

query {
  allUsers(
    filters: 1
    filter: [{ field: "email", value: "JOSE", condition: "contains" }]
    sort: "nickName"
    sortDir: "desc"
    page: 1
    pageSize: 10
  ) {
    items {
      email
      emailConfirmed
      nickName
      firstName
      personalId
    }
    totalCount
    page
    pageSize
    totalPages
  }
}

✔ Requiere:

- Clases auxiliares:
    - `FilterInput`: con propiedades `field`, `value`, `condition`.
    - `Paged<T>`: para retornar items, totalCount, page.

🛠️ Configuración:
- No necesita atributos `[UsePaging]`, `[UseFiltering]`, etc.

- Agregar su descripción personalizada
	descriptor.Field(t => t.GetAllUsersAsync(default!, default!))
				.AddFilterSortingPagination<UserDto>()
				.UseCustomQueryable<UserDto>()
				.Type<CustomType<UserDto>>();

- Registrar los CustomQueriesType en `Startup.cs`:
	builder.Services
		.AddGraphQLServer()
		.RegisterDbContextFactory<ApplicationDbContext>()
		.AddQueryType<CustomQueriesType>() <-----



───────────────────────────────────────────────
2. FILTRO AUTOMÁTICO (HotChocolate)
───────────────────────────────────────────────

📌 Ejemplo de consulta:

query {
  allUsers(
    filters: 2
    after: "OQ=="
    where: {
      emailConfirmed: {
        eq: false
      }
    }
  ) {
    pageInfo {
      startCursor
      endCursor
    }
    edges {
      node {
        email
        nickName
        emailConfirmed
      }
    }
  }
}

✔ Requiere:

- Decoradores en el resolver:
    - `[UsePaging]`
    - `[UseFiltering]`
    - `[UseSorting]`
- Configuración del `ObjectType` (opcional) para definir qué campos se pueden filtrar o ordenar.

🛠️ Configuración:
- Registrar los middlewares en `Startup.cs`:
  
builder.Services
    .AddGraphQLServer()
    .RegisterDbContextFactory<ApplicationDbContext>()
    .AddProjections()  <-----
    .AddFiltering() <-----
    .AddSorting() <-----




───────────────────────────────────────────────
POSTDATA 1 : Evitar conflictos con múltiples Queries
───────────────────────────────────────────────

⚠️ Si necesitas dividir tu lógica GraphQL en varias clases (por ejemplo, `CustomQueries` y `EntitiesQueries`), **no debes usar `.AddQueryType<>()` más de una vez**, ya que esto generará el siguiente error:

    System.ArgumentException: The root type `Queries` has already been registered.

✅ Para evitar este conflicto, registra un único tipo raíz con nombre `"Queries"` y luego extiende ese tipo usando `.AddTypeExtension<>()`:

    .AddTypeExtension<CustomQueries>()       // Extiende con tus resolvers personalizados
    .AddTypeExtension<EntitiesQueries>()     // Agrega más resolvers desde otra clase
    .AddQueryType<CustomQueriesType>()

🧱 Cada clase debe estar decorada con:

    [ExtendObjectType("Queries")]

Esto asegura que todos los métodos se agrupen correctamente bajo el mismo tipo raíz `Queries` y evita ambigüedad en la construcción del esquema.





───────────────────────────────────────────────
POSTDATA 2 : AGRUPAR CONSULTAS GRAPHQL EN CATEGORÍAS
───────────────────────────────────────────────

Si deseas organizar tus consultas GraphQL en secciones como `custom` y `entities`, puedes hacerlo utilizando una clase raíz (`Queries`) que agrupe diferentes clases de consultas.

✅ Esto permite realizar consultas como:

    query {
      custom {
        allUsers { ... }
      }
      entities {
        allEntities { ... }
      }
    }

───────────────────────────────────────────────
PASOS PARA CONFIGURAR CONSULTAS AGRUPADAS
───────────────────────────────────────────────

1. Define una clase raíz `Queries` que agrupe tus resolvers:

    public class Queries
    {
        public CustomQueries Custom => new();
        public EntitiesQueries Entities => new();
    }

2. Crea clases tipo `ObjectType<T>` para cada categoría (con descripción y nombre explícito) >>> DE SER NECESARIO:

	public class CustomQueriesType : ObjectType<CustomQueries>
	{
		protected override void Configure(IObjectTypeDescriptor<CustomQueries> descriptor)
		{
			descriptor.Field(t => t.GetAllUsersAsync(default!, default!))
				.AddFilterSortingPagination<UserDto>()
				.UseCustomQueryable<UserDto>()
				.Type<CustomType<UserDto>>();
		}
	}

3. Registra los tipos en `Startup.cs`:

    builder.Services
        .AddGraphQLServer()
        .RegisterDbContextFactory<ApplicationDbContext>()
        .AddProjections()
        .AddFiltering()
        .AddSorting()
        .AddQueryType<Queries>()               // Raíz
        .AddType<CustomQueriesType>()          // Categoría custom
        .AddInMemorySubscriptions();

───────────────────────────────────────────────
IMPORTANTE: NO MEZCLAR CON EXTENSIONES
───────────────────────────────────────────────

⚠️ Si decides usar el enfoque agrupado (Queries → Custom / Entities), **no debes usar `[ExtendObjectType]` ni `.AddTypeExtension<>()`**.

Estas extensiones están pensadas para cuando todos los resolvers están directamente bajo `Query`, no agrupados.

───────────────────────────────────────────────
ESQUEMA ESPERADO
───────────────────────────────────────────────

type Query {
  custom: CustomQueries
  entities: EntitiesQueries
}

type CustomQueries {
  allUsers: [User]
}

type EntitiesQueries {
  allEntities: [Entity]
}










───────────────────────────────────────────────
POSTDATA 1: EXTENDER UNA RAÍZ ÚNICA DE CONSULTAS
───────────────────────────────────────────────

✅ Escenario: quieres mantener todos los resolvers directamente en `Queries`, pero dividir la lógica en múltiples clases (`CustomQueries`, `EntitiesQueries`, etc).

✅ Solución:

1. Crea clases de consulta con `[ExtendObjectType("Queries")]`:

    [ExtendObjectType("Queries")]
    public class CustomQueries
    {
        public IQueryable<User> GetAllUsers(...) => ...;
    }

    [ExtendObjectType("Queries")]
    public class EntitiesQueries
    {
        public IQueryable<Entity> GetAllEntities(...) => ...;
    }

2. En `Startup.cs`, registra una sola raíz `.AddQueryType(...)` y extiende:

	builder.Services
		.AddGraphQLServer()
		.RegisterDbContextFactory<ApplicationDbContext>()
		.AddProjections()
		.AddFiltering()
		.AddSorting()
		.AddTypeExtension<CustomQueries>()       // Extiende con tus resolvers personalizados
		.AddTypeExtension<EntitiesQueries>()     // Agrega más resolvers desde otra clase
		.AddQueryType<CustomQueriesType>()       // Agrega tipos personalizados de GraphQL al esquema.
		.AddInMemorySubscriptions();

🧱 Así todas las consultas quedan agrupadas directamente bajo `queries { allUsers ... allEntities ... }`.

───────────────────────────────────────────────
ESQUEMA RESULTANTE
───────────────────────────────────────────────
type Query {
  allUsers: [User]
  allEntities: [Entity]
  all more...
}

───────────────────────────────────────────────






───────────────────────────────────────────────
POSTDATA 2: AGRUPAR CONSULTAS EN CATEGORÍAS
───────────────────────────────────────────────

✅ Escenario: deseas organizar tus consultas en categorías lógicas como `custom`, `entities`, etc., para un esquema más limpio y jerárquico.

✅ Resultado esperado en GraphQL:

    query {
      custom {
        allUsers { ... }
      }
      entities {
        allEntities { ... }
      }
    }

───────────────────────────────────────────────
PASOS PARA CONFIGURARLO
───────────────────────────────────────────────

1. Crea una clase `Queries` como raíz de las categorías:

    public class Queries
    {
        public CustomQueries Custom => new();
        public EntitiesQueries Entities => new();
    }

2. (Opcional) Define tipos `ObjectType<T>` si necesitas descripciones o configuraciones adicionales:

    public class CustomQueriesType : ObjectType<CustomQueries>
    {
        protected override void Configure(IObjectTypeDescriptor<CustomQueries> descriptor)
        {
            descriptor.Description("Consultas personalizadas para usuarios");

            descriptor.Field(t => t.GetAllUsersAsync(...))
                .AddFilterSortingPagination<UserDto>()
                .UseCustomQueryable<UserDto>()
                .Type<CustomType<UserDto>>();
        }
    }

3. En `Startup.cs`, registra los tipos agrupados:

	builder.Services
		.AddGraphQLServer()
		.RegisterDbContextFactory<ApplicationDbContext>()
		.AddProjections()
		.AddFiltering()
		.AddSorting()
		.AddQueryType<Queries>()               // Raíz
		.AddType<CustomQueriesType>()          // Agrega tipos personalizados de GraphQL al esquema.
		.AddInMemorySubscriptions();

⚠️ IMPORTANTE: NO mezcles este enfoque con `[ExtendObjectType]` ni `.AddTypeExtension<>()`.
Las extensiones son exclusivas para cuando todos los resolvers cuelgan directamente de `Query`, no por categorías.

───────────────────────────────────────────────
ESQUEMA RESULTANTE
───────────────────────────────────────────────

type Query {
  custom: CustomQueries
  entities: EntitiesQueries
}

type CustomQueries {
  allUsers: [User]
}

type EntitiesQueries {
  allEntities: [Entity]
}
