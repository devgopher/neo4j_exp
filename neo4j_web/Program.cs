using Neo4j.Driver;

var builder = WebApplication.CreateBuilder(args);
// Register application setting using IOption provider mechanism
builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));

// Fetch settings object from configuration
var settings = new ApplicationSettings();
builder.Configuration.GetSection("ApplicationSettings").Bind(settings);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen();

builder.Services.AddLogging(cfg => cfg.AddConsole());
// This is to register your Neo4j Driver Object as a singleton
builder.Services.AddSingleton(GraphDatabase.Driver(settings.Neo4jConnection, AuthTokens.Basic(settings.Neo4jUser, settings.Neo4jPassword)));

// This is your Data Access Wrapper over Neo4j session, that is a helper class for executing parameterized Neo4j Cypher queries in Transactions
builder.Services.AddScoped<INeo4jDataAccess, Neo4JDataAccess>();

// This is the registration for your domain repository class
builder.Services.AddTransient<IPersonRepository, PersonRepository>();
builder.Services.AddTransient<IOccupationRepository, OccupationRepository>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.MapControllers();
app.UseCors();
app.Run();