//using System.Web.Http;
using System.Web.Http.Results;
using WebApi.Controllers.Data;
using WebApi.Controllers.Models;
using WebApi.Models;

namespace WebApi.Services
{
    public class HangHoaReponsitory : IHangHoaReponsitory
    {
        private readonly DTMyDbContext _context;

        public static int PER_PAGE { get; set; } = 2;

        public HangHoaReponsitory(DTMyDbContext context)
        {
            _context = context;
        }

        public ResultHangHoa Create(InputHangHoa hh)
        {
            try
            {
                var hangHoa = new DTHangHoa
                {
                    TenHh = hh.TenHangHoa,
                    DonGia = hh.DonGia,
                    MoTa = hh.Mota,
                    MaLoai = hh.MaLoai
                };
                _context.Add(hangHoa);
                _context.SaveChanges();
                return convertDTToResult(hangHoa);
            }
            catch
            {
                return null;
            }
        }

        public List<ResultHangHoa> GetAll(string search, int page, double? from, double? to, string? sortBy)
        {
            var allProducts = _context.HangHoas.AsQueryable();

            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allProducts = allProducts.Where(hh => hh.TenHh.Contains(search));
            }
            if (from.HasValue)
            {
                allProducts = allProducts.Where(hh => hh.DonGia >= from);
            }
            if (to.HasValue)
            {
                allProducts = allProducts.Where(hh => hh.DonGia <= to);
            }
            #endregion
            if (!string.IsNullOrEmpty(sortBy))
            {
                string[] sort = sortBy.Split(" ");
                if(sort.Length == 2)
                {
                    if(sort[0] == "DESC")
                    {
                        allProducts = allProducts.OrderByDescending(hh => hh.GetType().GetProperty(sort[0]));
                    }else if(sort[0] == "ASC")
                    {
                        allProducts = allProducts.OrderBy(hh => hh.GetType().GetProperty(sort[0]));
                        
                    }
                }
            
                
            }
            else
            { 
                allProducts = allProducts.OrderBy(hh => hh.TenHh);
            }

            #region paging
            //allProducts = allProducts.Skip((page-1)*PER_PAGE).Take(PER_PAGE);

            var result = PaginatedList<DTHangHoa>.Create(allProducts, page, PER_PAGE);
            #endregion


            return result.Select(hh => new ResultHangHoa
            {
                MaHangHoa = hh.MaHh,
                TenHangHoa = hh.TenHh,
                DonGia = hh.DonGia,
                TenLoai = hh.Loai?.TenLoai
            }).ToList();
        }


        private ResultHangHoa convertDTToResult(DTHangHoa dtHangHoa)
        {
            try
            { 
                var loai = _context.Loais.FirstOrDefault(lo => lo.MaLoai == dtHangHoa.MaLoai);
                if(loai != null)
                { 
                    return new ResultHangHoa
                    {
                        MaHangHoa = dtHangHoa.MaHh,
                        TenHangHoa = dtHangHoa.TenHh,
                        DonGia = dtHangHoa.DonGia,
                        Mota = dtHangHoa.MoTa,
                        MaLoai = dtHangHoa.MaLoai
                    };
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }


       
    }
}
