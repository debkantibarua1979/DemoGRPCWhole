using DemoGRPCProductService.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace DemoGRPCProductService.Services;

public class ProductGrpcService: ProductProtoService.ProductProtoServiceBase
{
    private readonly IProductService _productService;

    public ProductGrpcService()
    {
        _productService = new ProductService(); // or use dependency injection
    }

    public override async Task<Product> GetProduct(GetProductRequest request, ServerCallContext context)
        => await _productService.GetProduct(request);

    public override async Task<ProductListResponse> ListProduct(Empty request, ServerCallContext context)
        => await _productService.ListProduct(request);

    public override async Task<Product> CreateProduct(CreateProductRequest request, ServerCallContext context)
        => await _productService.CreateProduct(request);

    public override async Task<Product> UpdateProduct(UpdateProductRequest request, ServerCallContext context)
        => await _productService.UpdateProduct(request, context);

    public override async Task<Empty> DeleteProduct(DeleteProductRequest request, ServerCallContext context)
    {
        await _productService.DeleteProduct(request, context);
        return new Empty();
    }
}