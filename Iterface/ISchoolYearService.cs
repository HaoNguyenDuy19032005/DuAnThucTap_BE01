using DuAnThucTapNhom3.Models;

namespace DuAnThucTapNhom3.Iterface
{
    public interface ISchoolYearService
    {
        List<Schoolyear> GetAll();
        Schoolyear GetById(int id);
        Schoolyear Create(CreateSchoolYearDto dto);
        bool Update(int id, Schoolyear model);
        bool Delete(int id);
    }
}
