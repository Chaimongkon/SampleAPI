using Microsoft.AspNetCore.Mvc;
using SampleAPI.Models;
using SampleAPI.Service;
using System.Collections.Generic;


namespace SampleAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class SampleController : ControllerBase
    {
        private IProductService _productService;

        public SampleController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("Products")]
        public ActionResult GetAllProduct()
        {
            return Ok(_productService.GetAllProducts();

        }

        [HttpGet("Product/{id}")]
        public ActionResult GetProductById(int id)
        {
            return Ok(_productService.GetProduct(id));

        }

        [HttpPost("Products")]
        public ActionResult CreateProduct([FromBody] Product product)
        {
            try
            {
                _productService.CreateProduct(product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("Product/{id}")]
        public IActionResult UpdateProductById(int id, [FromBody] Product product)
        {
            try
            {
                _productService.UpdateProduct(id, product);

                return Ok(product);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("Product/{id}")]
        public IActionResult DeleteProductById(int id)
        {
            try
            {
                _productService.DeleteProduct(id);

                return Ok(id);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
