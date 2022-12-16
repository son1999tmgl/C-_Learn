using WebApi.Controllers.Data;
using WebApi.Controllers.Models;

namespace WebApi.Services
{
    public class HangHoaReponsitory : IHangHoaReponsitory
    {
        private readonly DTMyDbContext _context;

        public HangHoaReponsitory(DTMyDbContext context)
        {
            _context = context;
        }
        public List<ResultHangHoa> GetAll(string search, double? from, double? to, string? sortBy)
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
            #region sort


            #endregion

            var result = allProducts.Select(hh => new ResultHangHoa
            {
                MaHangHoa = hh.MaHh,
                TenHangHoa = hh.TenHh,
                DonGia = hh.DonGia,
                TenLoai = hh.Loai.TenLoai
            });
            return result.ToList();
        }
    }
}
