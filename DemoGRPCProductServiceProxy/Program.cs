using DemoGRPCPro

var builder = WebApplication.CreateBuilder(args);

// gRPC Client to talk to CoreGrpc
builder.Services.AddGrpcClient<ProductGrpc.ProductGrpcClient>(o =>
{
    o.Address = new Uri("http://localhost:6001"); // <-- CoreGrpc server address
});

// This Gateway exposes its own gRPC interface
builder.Services.AddGrpc();

// Register proxy service
builder.Services.AddScoped<ProductGrpcProxyService>();

var app = builder.Build();

app.MapGrpcService<ProductGrpcProxyService>();

app.MapGet("/", () => "This is the ProductService gRPC Gateway");

app.Run();