using RedisExampleApp.API.Models;
using RedisExampleApp.Cache;
using StackExchange.Redis;
using System.Text.Json;

namespace RedisExampleApp.API.Repositories
{
    public class ProductRepositoryWithCacheDecorator : IProductRepository
    {
        //ProductRepository için Decorator Design Patern uygulandı.

        private const string productKey = "productCaches";
        private readonly IProductRepository _productRepository;
        private readonly RedisService _redisService;
        private readonly IDatabase _cacheRipository;

        public ProductRepositoryWithCacheDecorator(IProductRepository productRepository, RedisService redisService)
        {
            _productRepository = productRepository;
            _redisService = redisService;
            _cacheRipository = _redisService.GetDb(2);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            var newProduct = await _productRepository.CreateAsync(product);

            if (await _cacheRipository.KeyExistsAsync(productKey))
            {
                await _cacheRipository.HashSetAsync(productKey, product.Id, JsonSerializer.Serialize(newProduct));
            }

            return newProduct;
        }

        public async Task<List<Product>> GetAsync()
        {
            if (!await _cacheRipository.KeyExistsAsync(productKey))
            {
                return await LoadToCacheFromDbAsync();
            }

            var productList = new List<Product>();
            var cacheProduct = await _cacheRipository.HashGetAllAsync(productKey);

            foreach (var item in cacheProduct.ToList())
            {
                var product = JsonSerializer.Deserialize<Product>(item.Value);

                productList.Add(product);
            }

            return productList;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            if (await _cacheRipository.KeyExistsAsync(productKey))
            {
                var product = await _cacheRipository.HashGetAsync(productKey, id);

                return product.HasValue ? JsonSerializer.Deserialize<Product>(product) : null;

            }

            var productList = await LoadToCacheFromDbAsync();

            return productList.FirstOrDefault(x => x.Id == id);
        }
        private async Task<List<Product>> LoadToCacheFromDbAsync()
        {
            var productList = await _productRepository.GetAsync();

            productList.ForEach(product =>
            {
                _cacheRipository.HashSetAsync(productKey, product.Id, JsonSerializer.Serialize(product));
            });

            return productList;
        }
    }
}
