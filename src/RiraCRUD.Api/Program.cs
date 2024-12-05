using RiraCRUD.Api.Interceptors;
using RiraCRUD.Api.Middleware;
using RiraCRUD.Api.Services;
using RiraCRUD.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCoreInfrastructure(builder.Configuration);
builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = builder.Environment.IsDevelopment();
});

builder.Services.AddSingleton<GlobalExceptionInterceptor>();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.MapGrpcService<PersonService>()
   .AddGrpcInterceptor<GlobalExceptionInterceptor>();

app.MapGet("/", () => "Use a gRPC client to communicate with this server.");

app.Run();