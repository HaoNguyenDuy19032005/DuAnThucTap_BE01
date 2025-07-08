using DuAnThucTapNhom3.Data;
using DuAnThucTapNhom3.Iterface;
using DuAnThucTapNhom3.Models;

namespace DuAnThucTapNhom3.Service
{
    public class SchoolYearService : ISchoolYearService
    {
        private readonly AppDbContext _context;

        public SchoolYearService(AppDbContext context)
        {
            _context = context;
        }

        public List<Schoolyear> GetAll()
        {
            return _context.Schoolyears.ToList();
        }

        public Schoolyear GetById(int id)
        {
            return _context.Schoolyears.Find(id) ?? throw new Exception($"Schoolyear with ID {id} not found.");
        }

        public Schoolyear Create(CreateSchoolYearDto dto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                // Tạo mới năm học
                var model = new Schoolyear
                {
                    Schoolyearname = dto.Schoolyearname,
                    Startyear = dto.Startyear, // INT
                    Endyear = dto.Endyear,     // INT
                    Createdat = DateTime.UtcNow
                };

                _context.Schoolyears.Add(model);
                _context.SaveChanges();

                // Nếu chọn kế thừa học kỳ từ năm trước
                if (dto.IsInherit && dto.FromSchoolYearId.HasValue)
                {
                    var oldSemesters = _context.Semesters
                        .Where(s => s.Schoolyearid == dto.FromSchoolYearId.Value)
                        .ToList();

                    foreach (var old in oldSemesters)
                    {
                        // Giả sử Startdate và Enddate trong bảng Semesters là DateTime?
                        var newStartDate = old.Startdate.HasValue
                            ? DateTime.SpecifyKind(old.Startdate.Value, DateTimeKind.Utc)
                            : DateTime.UtcNow;

                        var newEndDate = old.Enddate.HasValue
                            ? DateTime.SpecifyKind(old.Enddate.Value, DateTimeKind.Utc)
                            : DateTime.UtcNow;

                        var newSemester = new Semester
                        {
                            Semestername = old.Semestername,
                            Startdate = newStartDate,
                            Enddate = newEndDate,
                            Createdat = DateTime.UtcNow,
                            Updatedat = DateTime.UtcNow,
                            Schoolyearid = model.Schoolyearid
                        };

                        _context.Semesters.Add(newSemester);
                    }

                    _context.SaveChanges();
                }

                transaction.Commit();
                return model;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception($"Save failed: {ex.InnerException?.Message ?? ex.Message}", ex);
            }
        }

        public bool Update(int id, Schoolyear model)
        {
            var existing = _context.Schoolyears.Find(id);
            if (existing == null) return false;

            existing.Schoolyearname = model.Schoolyearname;
            existing.Startyear = model.Startyear;  // INT
            existing.Endyear = model.Endyear;      // INT
            existing.Updatedat = DateTime.UtcNow;

            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var item = _context.Schoolyears.Find(id);
            if (item == null) return false;

            _context.Schoolyears.Remove(item);
            _context.SaveChanges();
            return true;
        }
    }
}
