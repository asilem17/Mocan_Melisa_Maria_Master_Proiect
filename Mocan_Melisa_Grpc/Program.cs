using Mocan_Melisa_Grpc.Services;
using Microsoft.EntityFrameworkCore;
using Mocan_Melisa_MariaMVC.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PetsAdoptionContext>(options =>

options.UseSqlServer(builder.Configuration.GetConnectionString("PetsAdoptionContext")));

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GrpcCRUDService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
