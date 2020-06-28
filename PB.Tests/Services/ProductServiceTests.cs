using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PB.Core.Dtos;
using PB.Core.Interfaces.Services;
using PB.Core.Services;
using PB.Data.Entities;
using PB.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PB.Tests.Services
{
    [TestClass]
    public class ProductServiceTests
    {
        private readonly Mock<IRepository<Product>> _productRepoMock;
        private readonly Mock<IValidationService<Product>> _productValidationServiceMock;
        private readonly IProductService _productServiceMock;
        private List<Product> _products;

        public ProductServiceTests()
        {
            _productRepoMock = new Mock<IRepository<Product>>();
            _productValidationServiceMock = new Mock<IValidationService<Product>>();
            _productServiceMock = new ProductService(_productRepoMock.Object, _productValidationServiceMock.Object);

            Init();
        }

        [TestMethod]
        public async Task GetProductByIdTest()
        {
            var result = await _productServiceMock.GetProductByIdAsync(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task SaveProduct()
        {
            var newProduct = new ProductDto
            {
                Name = "NewProduct",
                Description = "NewDescription"
            };

            var currentCount = _products.Count;

            await _productServiceMock.SaveProductAsync(newProduct);

            Assert.AreEqual(currentCount + 1, _products.Count);
        }

        private void Init()
        {
            _products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Test1",
                    Description = "Description"
                },
                new Product
                {
                    Id = 2,
                    Name = "Test2"
                }
            };

            _productRepoMock.SetupGet(c => c.Entities).Returns(_products.AsQueryable());
            _productRepoMock.Setup(c => c.GetByIdAsync(It.IsAny<long>())).ReturnsAsync((long c) => _products.Find(x => x.Id == c));
            _productRepoMock.Setup(c => c.InsertAsync(It.IsAny<Product>())).Callback<Product>(c => _products.Add(c)).ReturnsAsync((Product c) => c);
            _productValidationServiceMock.Setup(c => c.Validate(It.IsAny<Product>())).ReturnsAsync(true);

        }
    }
}
