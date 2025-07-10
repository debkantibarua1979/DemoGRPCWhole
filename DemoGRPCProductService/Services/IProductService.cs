using DemoGRPCProductService.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace DemoGRPCProductService.Services;

public interface IProductService
{
    Task<Product?> GetProduct(GetProductRequest request);
    Task<ProductListResponse> ListProduct(Empty request);
    Task<Product> CreateProduct(CreateProductRequest request);
    Task<Product> UpdateProduct(UpdateProductRequest request, ServerCallContext context);
    Task<bool> DeleteProduct(DeleteProductRequest request, ServerCallContext context);
}