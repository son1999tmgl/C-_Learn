using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Models;
using WebApi.Controllers.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaisController : ControllerBase
    {
        private readonly ILoaiReponsitory _loaiRepository;

        public LoaisController(ILoaiReponsitory loaiReponsitory)
        {
            _loaiRepository = loaiReponsitory;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var data = _loaiRepository.GetAll();
                if(data != null)
                {
                    return Ok(_loaiRepository.GetAll());
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError); 
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, InputLoai loai)
        {
            if(id != loai.MaLoai)
            {
                return BadRequest();
            }
            try
            {
                _loaiRepository.Update(loai);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                _loaiRepository.Delete(id);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(InputLoai loai)
        {
            try
            {
                return Ok(_loaiRepository.Add(loai));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
