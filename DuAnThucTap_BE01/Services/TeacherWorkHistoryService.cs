using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.DTO;
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
                    TeacherName = h.Teacher != null ? h.Teacher.Fullname : null, // Kiểm tra null an toàn
                    Operationunitid = h.Operationunitid,
                    OperationUnitName = h.Operationunit != null ? h.Operationunit.Organizationname : null, // Kiểm tra null an toàn
                    Departmentid = h.Departmentid,
                    DepartmentName = h.Department != null ? h.Department.Departmentname : null, // Kiểm tra null an toàn
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
                    TeacherName = h.Teacher != null ? h.Teacher.Fullname : null,
                    Operationunitid = h.Operationunitid,
                    OperationUnitName = h.Operationunit != null ? h.Operationunit.Organizationname : null,
                    Departmentid = h.Departmentid,
                    DepartmentName = h.Department != null ? h.Department.Departmentname : null,
                    Iscurrentschool = h.Iscurrentschool,
                    Positionheld = h.Positionheld,
                    Startdate = h.Startdate,
                    Enddate = h.Enddate
                }).FirstOrDefaultAsync();
        }

        // Cập nhật phương thức CreateAsync
        public async Task<Teacherworkhistory> CreateAsync(TeacherWorkHistoryRequestDto historyDto)
        {
            // Ánh xạ từ DTO sang Model Entity
            var history = new Teacherworkhistory
            {
                Teacherid = historyDto.Teacherid,
                Operationunitid = historyDto.Operationunitid,
                Departmentid = historyDto.Departmentid,
                Iscurrentschool = historyDto.Iscurrentschool,
                Positionheld = historyDto.Positionheld,
                Startdate = historyDto.Startdate,
                Enddate = historyDto.Enddate
            };

            _context.Teacherworkhistories.Add(history);
            await _context.SaveChangesAsync();
            return history;
        }

        // Cập nhật phương thức UpdateAsync
        public async Task<Teacherworkhistory?> UpdateAsync(int id, TeacherWorkHistoryRequestDto updatedHistoryDto)
        {
            var existing = await _context.Teacherworkhistories.FindAsync(id);
            if (existing == null) return null;

            // Ánh xạ các thuộc tính từ DTO sang entity hiện có
            existing.Teacherid = updatedHistoryDto.Teacherid;
            existing.Operationunitid = updatedHistoryDto.Operationunitid;
            existing.Departmentid = updatedHistoryDto.Departmentid;
            existing.Iscurrentschool = updatedHistoryDto.Iscurrentschool;
            existing.Positionheld = updatedHistoryDto.Positionheld;
            existing.Startdate = updatedHistoryDto.Startdate;
            existing.Enddate = updatedHistoryDto.Enddate;


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