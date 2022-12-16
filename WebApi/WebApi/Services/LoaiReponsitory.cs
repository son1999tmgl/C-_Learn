using Microsoft.EntityFrameworkCore;
using WebApi.Controllers.Data;
using WebApi.Controllers.Models;

namespace WebApi.Controllers.Services
{
    public class LoaiReponsitory : ILoaiReponsitory
    {
        private readonly DTMyDbContext _context;

        public LoaiReponsitory(DTMyDbContext context)
        {
            _context = context;
        }

        public ResultLoai Add(InputLoai model)
        {
            var _loai = new DTLoai
            {
                TenLoai = model.TenLoai.Trim(),
            };
            _context.Add(_loai);
            _context.SaveChanges();
            return new ResultLoai
            {
                TenLoai = _loai.TenLoai,
                MaLoai = _loai.MaLoai
            };

        }

        public void Delete(int id)
        {
            var loai = _context.Loais.SingleOrDefault(lo => lo.MaLoai == id);

            if(loai != null)
            {
                _context.Remove(loai);
                _context.SaveChanges();
            }
        }

        public List<ResultLoai> GetAll()
        {
            var loais = _context.Loais.Select(loai => new ResultLoai
            {
                MaLoai = loai.MaLoai,
                TenLoai = loai.TenLoai
            });
            return loais.ToList();
        }

        public ResultLoai GetById(int id)
        {
            var loai = _context.Loais.SingleOrDefault(lo => lo.MaLoai == id);
            if(loai != null)
            {
                return new ResultLoai
                {
                    MaLoai = loai.MaLoai,
                    TenLoai = loai.TenLoai
                };
            }
            return null;
        }

        public void Update(InputLoai model)
        {
            var loai = _context.Loais.SingleOrDefault(lo => lo.MaLoai == model.MaLoai);
        }
    }
}
