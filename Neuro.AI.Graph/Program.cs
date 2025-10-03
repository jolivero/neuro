using Keycloak.AuthServices.Authentication;
using Microsoft.EntityFrameworkCore;
using Neuro.AI.Graph.QL.Queries;
using Neuro.AI.Graph.Shield.Settings;
using Neuro.AI.Graph.Shield;
using TropigasMobile.Backend.Data;
using Neuro.AI.Graph.Repository;
using Neuro.AI.Graph.Shield.Solutions;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.QL.Mutations;
using Neuro.AI.Graph.Models.Manufacturing;
using Dapper;
using Azure.Storage.Blobs;
using Neuro.AI.Graph;

var builder = WebApplication.CreateBuilder(args);

var c = builder.Configuration;

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddPooledDbContextFactory<ApplicationDbContext>(options => options.UseSqlServer(c.GetConnectionString("Cnn_TropigasMobile")));
builder.Services.AddPooledDbContextFactory<ManufacturingDbContext>(options => options.UseSqlServer(c.GetConnectionString("Cnn_Manufacturing")));
builder.Services.AddSingleton(new BlobServiceClient(
	builder.Configuration["AzureConnections:AzureBlobStorage"]
));

builder.Services.AddKeycloakWebApiAuthentication(c.GetSection("KeycloakSettings"));
builder.Services.AddSingleton(c.ConfigureSection<KeycloakSettings>());
builder.Services.AddSingleton(c.ConfigureSection<AzureBlobsSettings>());

builder.Services.AddKeycloakLoginService();
builder.Services.AddBearerPropagationService();

builder.Services.AddPermissionPolicies();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Connectos
builder.Services.AddTransient<ManufacturingConnector>();

//Repositories
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<CompanyRepository>();
builder.Services.AddScoped<ProductionLineRepository>();
builder.Services.AddScoped<GroupRepository>();
builder.Services.AddScoped<StationRepository>();
builder.Services.AddScoped<MachineRepository>();
builder.Services.AddScoped<PartRepository>();
builder.Services.AddScoped<TurnRepository>();
builder.Services.AddScoped<DashboardRepository>();
builder.Services.AddScoped<MonthlyPlanningRepository>();
builder.Services.AddScoped<DailyPlanningRepository>();
builder.Services.AddScoped<DailyTaskRepository>();
builder.Services.AddScoped<ChangeRequestRepository>();
builder.Services.AddScoped<ProductionRecordRepository>();
builder.Services.AddScoped<StatusControlRepository>();
builder.Services.AddScoped<QualityRecordRepository>();
builder.Services.AddScoped<LogRepository>();

//Servicios
builder.Services.AddScoped<IAzureBlobStorageService, AzureBlobStorageService>();

//Dapper Handler
SqlMapper.AddTypeHandler(new TimeOnlyHandler());
SqlMapper.AddTypeHandler(new DateOnlyHandler());

builder.Services
	.AddGraphQLServer()
	.AddAuthorization()
	.RegisterDbContextFactory<ApplicationDbContext>()
	.RegisterDbContextFactory<ManufacturingDbContext>()
	.AddProjections()
	.AddFiltering()
	.AddSorting()
	.AddQueryType<Queries>()
	.AddMutationType<Mutations>()
	.AddType<CustomQueriesType>()
	.AddType<UploadType>()
	.AddInMemorySubscriptions();

builder.Services.AddCors(options =>
{
	options.AddPolicy("EnableCORS", build =>
	{
		build
			.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader();
	});
});

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
	//app.UseCors("EnableCORS");
}

app.UseCors("EnableCORS");

app.UseHttpsRedirection();
app.UseKeycloakLoginAPI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGraphQL();


app.Run();
