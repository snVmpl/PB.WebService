using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PB.Core.Dtos;
using PB.Core.Interfaces.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PB.WebService.Controllers
{
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product by id</returns>
        /// <response code="200">Returns requested item</response>
        /// <response code="400">If the id is null or incorrect, or item not found</response>
        [HttpGet]
        [Route("product/{id}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProduct([Required] long id)
        {
            try
            {
                var result = await _productService.GetProductByIdAsync(id);
                ActualResult.Data = result;
            }
            catch (Exception ex)
            {
                SetExceptionResult(ex);
            }

            return Result();
        }

        /// <summary>
        /// Create product
        /// </summary>
        /// <param name="product">Product to create</param>
        /// <returns>A new created product</returns>
        /// <response code="201">Returns the new created product</response>
        /// <response code="400">If the item has errors</response>
        [HttpPost]
        [Route("products")]
        [Consumes("text/product")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
        [Authorize(Policy = "OddParity")]
        public async Task<IActionResult> CreateProduct([FromBody]ProductDto product)
        {
            try
            {
                var result = await _productService.SaveProductAsync(product);
                ActualResult.Data = result;
            }
            catch (Exception ex)
            {
                SetExceptionResult(ex);
            }
            return Result();
        }
    }
}