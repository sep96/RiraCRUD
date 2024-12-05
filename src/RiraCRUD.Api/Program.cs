using RiraCRUD.Api.Services;
using RiraCRUD.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCoreInfrastructure(builder.Configuration);
builder.Services.AddGrpc(); 

var app = builder.Build();

 
app.UseHttpsRedirection();
 

app.MapGrpcService<PersonService>();
app.MapGet("/", () => "Use a gRPC client to communicate with this server.");

app.Run();
