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
        public IActionResult GetAll(string? search, double? from, double? to, string? sortBy)
        {
            return Ok(_hangHoaReponsitory.GetAll(search, from, to, sortBy));
        }

        [HttpPost]
        public IActionResult Create(InputHangHoa hangHoaVM)
        {
            try
            {
                if (hangHoaVM.MaLoai != null)
                {
                    var loai = _context.Loais.FirstOrDefault(l => l.MaLoai == hangHoaVM.MaLoai);
                    if (loai != null)
                    {
                        var hanghoa = new DTHangHoa
                        {
                            TenHh = hangHoaVM.TenHangHoa,
                            MaLoai = hangHoaVM.MaLoai,
                            DonGia = hangHoaVM.DonGia
                        };
                        _context.Add(hanghoa);
                        _context.SaveChanges();
                        return Ok(new ResultHangHoa
                        {
                            TenHangHoa = hanghoa.TenHh,
                            MaLoai = hanghoa.MaLoai,
                            DonGia = hanghoa.DonGia,
                            MaHangHoa = hanghoa.MaHh
                        });
                    }
                    else
                    {
                        return BadRequest("Mã loại không tồn tại.");
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest("Có lỗi xảy ra");
            }
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
