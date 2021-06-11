using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Api.Controllers
{
    [Area("api")]
    [Route("[Area]/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _AuthorService;
        public AuthorsController(IAuthorService AuthorService)
        {
            _AuthorService = AuthorService;
        }

        ///<summary>
        ///Get all authors
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                return Ok(await _AuthorService.GetAll());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        ///<summary>
        ///Add an author
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AuthorModel model)
        {
            try
            {
                var createdModel = await _AuthorService.Create(model);

                return CreatedAtRoute("GetAuthor", new { id = createdModel.Id }, createdModel);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        ///<summary>
        ///Get author by Id
        /// </summary>
        [HttpGet("{id}", Name = "GetAuthor")]
        public async Task<IActionResult> GetAsync(string id)
        {
            try
            {
                var foundModel = await _AuthorService.GetById(id);

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
        ///Edit author by Id
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(string id, [FromBody] AuthorModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            try
            {
                await _AuthorService.Update(model);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        ///<summary>
        ///Delete author by Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                var foundModel = await _AuthorService.GetById(id);

                if (foundModel == null)
                {
                    return NotFound();
                }

                await _AuthorService.Delete(foundModel);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}

