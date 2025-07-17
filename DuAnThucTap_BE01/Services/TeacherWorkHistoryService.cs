// Services/TeacherWorkHistoryService.cs
using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Dtos;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Services
{
    public class TeacherWorkHistoryService : ITeacherWorkHistoryService
    {
        private readonly ISCDbContext _context;
        public TeacherWorkHistoryService(ISCDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TeacherWorkHistoryDto>> GetAllAsync()
        {
            return await _context.Teacherworkhistories
                .Include(h => h.Teacher)
                .Include(h => h.Operationunit)
                .Include(h => h.Department)
                .Select(h => new TeacherWorkHistoryDto
                {
                    Workhistoryid = h.Workhistoryid,
                    Teacherid = h.Teacherid,
                    TeacherName = h.Teacher.Fullname,
                    Operationunitid = h.Operationunitid,
                    OperationUnitName = h.Operationunit.Organizationname, 
                    Departmentid = h.Departmentid,
                    DepartmentName = h.Department.Departmentname, 
                    Iscurrentschool = h.Iscurrentschool,
                    Positionheld = h.Positionheld,
                    Startdate = h.Startdate,
                    Enddate = h.Enddate
                }).ToListAsync();
        }

        public async Task<TeacherWorkHistoryDto?> GetByIdAsync(int id)
        {
            return await _context.Teacherworkhistories
                .Where(h => h.Workhistoryid == id)
                .Include(h => h.Teacher)
                .Include(h => h.Operationunit)
                .Include(h => h.Department)
                .Select(h => new TeacherWorkHistoryDto
                {
                    Workhistoryid = h.Workhistoryid,
                    Teacherid = h.Teacherid,
                    TeacherName = h.Teacher.Fullname,
                    Operationunitid = h.Operationunitid,
                    OperationUnitName = h.Operationunit.Organizationname,
                    Departmentid = h.Departmentid,
                    DepartmentName = h.Department.Departmentname,
                    Iscurrentschool = h.Iscurrentschool,
                    Positionheld = h.Positionheld,
                    Startdate = h.Startdate,
                    Enddate = h.Enddate
                }).FirstOrDefaultAsync();
        }

        public async Task<Teacherworkhistory> CreateAsync(Teacherworkhistory history)
        {
            _context.Teacherworkhistories.Add(history);
            await _context.SaveChangesAsync();
            return history;
        }

        public async Task<Teacherworkhistory?> UpdateAsync(int id, Teacherworkhistory updatedHistory)
        {
            var existing = await _context.Teacherworkhistories.FindAsync(id);
            if (existing == null) return null;

            existing.Teacherid = updatedHistory.Teacherid;
            existing.Operationunitid = updatedHistory.Operationunitid;
            existing.Departmentid = updatedHistory.Departmentid;
            existing.Iscurrentschool = updatedHistory.Iscurrentschool;
            existing.Positionheld = updatedHistory.Positionheld;
            existing.Startdate = updatedHistory.Startdate;
            existing.Enddate = updatedHistory.Enddate;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var history = await _context.Teacherworkhistories.FindAsync(id);
            if (history == null) return false;

            _context.Teacherworkhistories.Remove(history);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}