using DuAnThucTapNhom3.Models;

namespace DuAnThucTapNhom3.Iterface
{
    public interface ISemesterService
    {
        List<Semester> GetAll();
        Semester? GetById(int id);
        Semester Create(Semester model);
        bool Update(int id, Semester model);
        bool Delete(int id);
    }
}
