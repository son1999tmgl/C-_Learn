using WebApi.Controllers.Models;

namespace WebApi.Controllers.Services
{
    public class LoaiReponsitoryMemory : ILoaiReponsitory
    {
        static List<ResultLoai> loais = new List<ResultLoai>
        {
            new ResultLoai {MaLoai = 1, TenLoai = "Dien thoai"},
            new ResultLoai {MaLoai = 2, TenLoai = "tu lanh"},
            new ResultLoai {MaLoai = 3, TenLoai = "dieu hoa"},
            new ResultLoai {MaLoai = 4, TenLoai = "may giat"},
        };
        public ResultLoai Add(InputLoai model)
        {
            var _loai = new ResultLoai
            {
                MaLoai = loais.Max(lo => lo.MaLoai) + 1,
                TenLoai = model.TenLoai
            };
            
            loais.Add(_loai);
            return _loai;
            
        }

        public void Delete(int id)
        {
            var _loai = loais.SingleOrDefault(lo => lo.MaLoai == id);
            if (_loai != null)
            {
                loais.Remove(_loai);
            }
        }

        public List<ResultLoai> GetAll()
        {
            return loais;
        }

        public ResultLoai GetById(int id)
        {
            return loais.SingleOrDefault(lo => lo.MaLoai == id);
        }

        public void Update(InputLoai model)
        {
            var _loai = loais.SingleOrDefault(lo => lo.MaLoai == model.MaLoai);
            if(_loai != null)
            {
                _loai.TenLoai = model.TenLoai;
                _loai.MaLoai = model.MaLoai;
            }
        }
    }
}
