using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleAPI.Models;
using SampleAPI.Service;


namespace SampleAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("Products")]

        public ActionResult GetAllProduct()
        {
            var GetAll = _productService.GetAllProducts();
            //return Ok(_productService.GetAllProducts());
            return new JsonResult(Ok(GetAll));
        }

        [HttpGet("Product/{id}")]
        public ActionResult GetProductById(int id)
        {
            var GetId = _productService.GetProduct(id);
            //return Ok(_productService.GetProduct(id));
            return new JsonResult(Ok(GetId));
        }

        [HttpPost("Products")]
        public ActionResult CreateProduct([FromBody] Product product)
        {
            try
            {
                _productService.CreateProduct(product);
                return new JsonResult(Ok(product));
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

                return new JsonResult(Ok(product));

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

                return new JsonResult(Ok(id)); ;

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
