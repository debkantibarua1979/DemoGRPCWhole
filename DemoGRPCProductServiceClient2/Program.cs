using DemoGRPCProductServiceClient2.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;

namespace DemoGRPCProductServiceClient2;

class Program
{
    static async Task Main(string[] args)
    {
        AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

        // Connect to the gRPC server (adjust port as needed)
        using var channel = GrpcChannel.ForAddress("http://localhost:6001");
        var client = new ProductProtoService.ProductProtoServiceClient(channel);
        
        var created = await client.CreateProductAsync(new CreateProductRequest
        {
            Name = "Laptop",
            Description = "Powerful GRPC test laptop",
            Price = 999.99
        });
        
        
        Console.WriteLine($"Created: {created.Id} - {created.Name}");

        // Call ListProduct
        Console.WriteLine("Fetching product list...");
        var response = await client.ListProductAsync(new Empty());

        foreach (var product in response.Products)
        {
            Console.WriteLine($"> {product.Id}: {product.Name} - {product.Description} - ${product.Price}");
        }
    }
}