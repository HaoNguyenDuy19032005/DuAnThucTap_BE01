using DuAnThucTapNhom3.Data;
using DuAnThucTapNhom3.Iterface;
using DuAnThucTapNhom3.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom3.Service
{
    public class SemesterService : ISemesterService
    {
        private readonly AppDbContext _context;

        public SemesterService(AppDbContext context)
        {
            _context = context;
        }

        public List<Semester> GetAll()
        {
            return _context.Semesters.Include(s => s.Schoolyear).ToList();
        }

        public Semester? GetById(int id)
        {
            return _context.Semesters.Include(s => s.Schoolyear).FirstOrDefault(s => s.Semesterid == id);
        }

        public Semester Create(Semester model)
        {
            model.Createdat = DateTime.UtcNow;
            _context.Semesters.Add(model);
            _context.SaveChanges();
            return model;
        }

        public bool Update(int id, Semester model)
        {
            var existing = _context.Semesters.Find(id);
            if (existing == null)
                return false;

            existing.Semestername = model.Semestername;
            existing.Startdate = model.Startdate;
            existing.Enddate = model.Enddate;
            existing.Schoolyearid = model.Schoolyearid;
            existing.Updatedat = DateTime.UtcNow;

            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var existing = _context.Semesters.Find(id);
            if (existing == null)
                return false;

            _context.Semesters.Remove(existing);
            _context.SaveChanges();
            return true;
        }
    }
}
