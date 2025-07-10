using DemoGRPCContracts.Protos;
using DemoGRPCProductServiceProxy.Services;

var builder = WebApplication.CreateBuilder(args);

// gRPC Client to talk to CoreGrpc
/*
builder.Services.AddGrpcClient<ProductGrpc.ProductGrpcClient>(o =>
{
    o.Address = new Uri("http://localhost:6001"); // <-- CoreGrpc server address
});
*/

builder.Services.AddGrpc();
builder.Services.AddScoped<ProductGrpcProxy>();

var app = builder.Build();

app.MapGrpcService<ProductGrpcProxy>();

app.MapGet("/", () => "This is the GRPC Gateway for ProductService.");

app.Run();