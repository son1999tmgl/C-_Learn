using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        public static List<HangHoa> hangHoas = new List<HangHoa>();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(hangHoas);
        }

        [HttpPost]
        public IActionResult Create(HangHoaVM hangHoaVM)
        {
            var hangHoa = new HangHoa
            {
                MaHangHoa = Guid.NewGuid(),
                TenHangHoa = hangHoaVM.TenHangHoa,
                DonGia = hangHoaVM.DonGia
            };
            hangHoas.Add(hangHoa);
            return Ok(new
            {
                Success = true,
                Data = hangHoa
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                //LINQ [Object] query
                var hangHoa = hangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (hangHoa == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(hangHoa);
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Edit(string id, HangHoa hangHoaEdit)
        {
            try
            {
                //LINQ [Object] query
                var hangHoa = hangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (hangHoa == null)
                {
                    return NotFound();
                }
                else
                {
                    if(id != hangHoa.MaHangHoa.ToString())
                    {
                        return BadRequest();
                    }
                    hangHoa.TenHangHoa = hangHoaEdit.TenHangHoa;
                    hangHoa.DonGia = hangHoaEdit.DonGia;
                    return Ok(hangHoaEdit);
                }
            }
            catch
            {
                return BadRequest();
            }
        }
        
        [HttpDelete("{id}")]
        public IActionResult Remove(string id)
        {
            try
            {
                //LINQ [Object] query
                var hangHoa = hangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (hangHoa == null)
                {
                    return NotFound();
                }
                else
                {
                    hangHoas.Remove(hangHoa);
                    return Ok();
                }
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
