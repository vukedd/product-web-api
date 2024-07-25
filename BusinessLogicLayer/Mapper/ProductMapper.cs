using BusinessLogicLayer.DTO;
using ProductWebAPI.DTO;
using ProductWebAPI.Models;

namespace ProductWebAPI.Mapper
{
    public static class ProductMapper
    {
        public static ProductDTO ToProductDTO(this Product productModel)
        {
            return new ProductDTO
            {
                Name = productModel.Name,
                Description = productModel.Description,
                Price = productModel.Price
            };
        }

        public static Product ToProductFromProductDTO(this ProductDTO productDTO)
        {
            return new Product
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price
            };
        }

        public static ProductPopularityDTO ToProductPopularityDTO(this Product product, int ConnectionCount, string OwnerUsername)
        {
            return new ProductPopularityDTO
            {
                Id = product.Id,
                ProductName = product.Name,
                NumberOfConnections = ConnectionCount,
                OwnerName = OwnerUsername.ToString()
            };
        }
    }
}
