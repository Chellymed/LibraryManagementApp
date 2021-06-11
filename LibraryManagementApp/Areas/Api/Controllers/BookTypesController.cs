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
    public class BookTypesController : ControllerBase
    {
        private readonly IBookTypeService _BookTypeService;
        public BookTypesController(IBookTypeService BookTypeService)
        {
            _BookTypeService = BookTypeService;
        }

        ///<summary>
        ///Get all bookTypes
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                return Ok(await _BookTypeService.GetAll());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        ///<summary>
        ///Add a bookType
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] BookTypeModel model)
        {
            try
            {
                var createdModel = await _BookTypeService.Create(model);

                return CreatedAtRoute("GetBookType", new { id = createdModel.Id }, createdModel);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        ///<summary>
        ///Get bookType by Id 
        /// </summary>
        [HttpGet("{id}", Name = "GetBookType")]
        public async Task<IActionResult> GetAsync(string id)
        {
            try
            {
                var foundModel = await _BookTypeService.GetById(id);

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
        ///Edit bookType by Id
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(string id, [FromBody] BookTypeModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            try
            {
                await _BookTypeService.Update(model);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        ///<summary>
        ///Delete bookType by Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                var foundModel = await _BookTypeService.GetById(id);

                if (foundModel == null)
                {
                    return NotFound();
                }

                await _BookTypeService.Delete(foundModel);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}