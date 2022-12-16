using WebApi.Controllers.Models;

namespace WebApi.Controllers.Services
{
    public interface ILoaiReponsitory
    {
        List<ResultLoai> GetAll();

        ResultLoai GetById(int id);

        ResultLoai Add(InputLoai model);

        void Update(InputLoai model);

        void Delete(int id);
    }
}
