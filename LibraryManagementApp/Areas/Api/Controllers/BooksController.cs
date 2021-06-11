using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Web.Area.Api.Controllers
{
    [Area("api")]
    [Route("[Area]/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _BookService;
        public BooksController(IBookService BookService)
        {
            _BookService = BookService;
        }

        ///<summary>
        ///Get all books
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                return Ok(await _BookService.GetAll());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        ///<summary>
        ///Add a book
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] BookModel model)
        {
            try
            {
                var createdModel = await _BookService.Create(model); 

                return CreatedAtRoute("GetBook", new { id = createdModel.Id }, createdModel);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        ///<summary>
        ///Get book by Id
        /// </summary>
        [HttpGet("{id}", Name = "GetBook")]
        public async Task<IActionResult> GetAsync(string id)
        {
            try
            {
                var foundModel = await _BookService.GetById(id);

                if (foundModel == null)
                {
                    return NotFound();
                }

                return Ok(foundModel);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        ///<summary>
        ///Edit book by Id
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(string id, [FromBody] BookModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            try
            {
                await _BookService.Update(model);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        ///<summary>
        ///Delete book by Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                var foundModel = await _BookService.GetById(id);

                if (foundModel == null)
                {
                    return NotFound();
                }

                await _BookService.Delete(foundModel);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
