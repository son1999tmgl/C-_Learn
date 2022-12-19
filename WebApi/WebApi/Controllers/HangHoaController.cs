using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Data;
using WebApi.Controllers.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        //public static List<HangHoa> hangHoas = new List<HangHoa>();

        private readonly DTMyDbContext _context;
        private readonly IHangHoaReponsitory _hangHoaReponsitory;

        public HangHoaController(DTMyDbContext context, IHangHoaReponsitory hangHoaReponsitory)
        {
            _context = context;
            _hangHoaReponsitory = hangHoaReponsitory;
        }
        [HttpGet]
        public IActionResult GetAll(string? search, double? from, double? to, string? sortBy, int page=1, int perPage = 2)
        {
            return Ok(_hangHoaReponsitory.GetAll(search, page, from, to, sortBy));
        }

        [HttpPost]
        public IActionResult Create(InputHangHoa hangHoa)
        {
            /*try
            {*/
               var hh = _hangHoaReponsitory.Create(hangHoa);
                if(hh != null)
                {
                    return Ok(hh);
                }
                else
                {
                    return BadRequest();
                }
           /* }
            catch
            {
                return BadRequest("Có lỗi xảy ra");
            }*/
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                //LINQ [Object] query
                var hangHoa = _context.HangHoas.SingleOrDefault(hh => hh.MaHh == Guid.Parse(id));
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
        public IActionResult Edit(string id, InputHangHoa hangHoaEdit)
        {
            try
            {
                //LINQ [Object] query
                var hangHoa = _context.HangHoas.SingleOrDefault(hh => hh.MaHh == Guid.Parse(id));
                if (hangHoa == null)
                {
                    return NotFound();
                }
                else
                {
                    if (id != hangHoa.MaHh.ToString())
                    {
                        return BadRequest();
                    }
                    hangHoa.TenHh = hangHoaEdit.TenHangHoa;
                    hangHoa.DonGia = hangHoaEdit.DonGia;
                    hangHoa.MaLoai = hangHoaEdit.MaLoai;
                    _context.SaveChanges();
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
                var hangHoa = _context.HangHoas.SingleOrDefault(hh => hh.MaHh == Guid.Parse(id));
                if (hangHoa == null)
                {
                    return NotFound();
                }
                else
                {
                    _context.HangHoas.Remove(hangHoa);
                    _context.SaveChanges();
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
