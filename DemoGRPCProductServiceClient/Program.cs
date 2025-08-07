using DemoGRPCContracts.Protos;
using Grpc.Net.Client;
using Google.Protobuf.WellKnownTypes;

Console.WriteLine("== GRPC Client Starting ==");

// Connect to GRPC Gateway (not the Core)
using var channel = GrpcChannel.ForAddress("http://localhost:6001");
var client = new ProductProtoService.ProductProtoServiceClient(channel);

// Create product
var created = await client.CreateProductAsync(new CreateProductRequest
{
    Name = "Laptop",
    Description = "Powerful GRPC test laptop",
    Price = 999.99
});
Console.WriteLine($"Created: {created.Id} - {created.Name}");

// List products
var list = await client.ListProductAsync(new Empty());
Console.WriteLine("Product List:");
foreach (var p in list.Products)
{
    Console.WriteLine($"- {p.Id}: {p.Name} (${p.Price})");
}