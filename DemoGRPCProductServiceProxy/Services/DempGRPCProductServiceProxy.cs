namespace DemoGRPCProductServiceProxy.Services;

using DemoGRPCContracts.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;


public class ProductGrpcProxy : ProductProtoService.ProductProtoServiceBase
{
    private readonly ProductProtoService.ProductProtoServiceClient _coreClient;

    public ProductGrpcProxy(ProductProtoService.ProductProtoServiceClient coreClient)
    {
        _coreClient = coreClient;
    }

    public override Task<Product> GetProduct(GetProductRequest request, ServerCallContext context)
        => _coreClient.GetProductAsync(request).ResponseAsync;

    public override Task<ProductListResponse> ListProduct(Empty request, ServerCallContext context)
        => _coreClient.ListProductAsync(request).ResponseAsync;

    public override Task<Product> CreateProduct(CreateProductRequest request, ServerCallContext context)
        => _coreClient.CreateProductAsync(request).ResponseAsync;

    public override Task<Product> UpdateProduct(UpdateProductRequest request, ServerCallContext context)
        => _coreClient.UpdateProductAsync(request).ResponseAsync;

    public override async Task<Empty> DeleteProduct(DeleteProductRequest request, ServerCallContext context)
    {
        await _coreClient.DeleteProductAsync(request);
        return new Empty();
    }
}
