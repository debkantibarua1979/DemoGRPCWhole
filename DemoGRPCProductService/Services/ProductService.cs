using DemoGRPCProductService.Exceptions;
using DemoGRPCProductService.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace DemoGRPCProductService.Services;

public class ProductService : IProductService
{
    private readonly static List<Product> _products = [];

    public async Task<Product?> GetProduct(GetProductRequest request)
    {
        var product = _products.FirstOrDefault(p => p.Id == request.Id);
        if (product == null)
        {
            throw new NotFoundException("Product not found!");
        }
        else
        {
            return product;
        }
    }
    
    public async Task<ProductListResponse> ListProduct(Empty request)
    {
        var response = new ProductListResponse();
        response.Products.AddRange(_products);

        if (response.Products.Count > 0)
        {
            return response;
        }
        else
        {
            throw new NotFoundException("No products found!");
        }
    }
    
    public async Task<Product> CreateProduct(CreateProductRequest request)
    {
        var product = new Product
        {
            Id = _products.Count + 1,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price
        };

        _products.Add(product);
        return product;
    }

    public async Task<Product> UpdateProduct(UpdateProductRequest request, ServerCallContext context)
    {
        var product = _products.FirstOrDefault(p => p.Id == request.Id);

        if (product == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Product not found!"));
        }
        
        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;

        return product;
    }
    
    public async Task<bool> DeleteProduct(DeleteProductRequest request, ServerCallContext context)
    {
        var product = _products.FirstOrDefault(p => p.Id == request.Id);

        if (product == null)
        {
            throw new NotFoundException("Product not found!");
        }

        _products.Remove(product);
        return true;
    }
}