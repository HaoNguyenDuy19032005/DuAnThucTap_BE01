using DuAnThucTap.Data;
using DuAnThucTap.DTOs;
using DuAnThucTap.Model;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap.Services
{
    public class GradetypeService : IGradetypeService
    {
        private readonly ApplicationDbContext _context;

        public GradetypeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<Gradetype>> GetAllAsync(string? search, int pageIndex, int pageSize)
        {
            var query = _context.Gradetypes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.Gradetypename.Contains(search));
            }

            query = query.OrderByDescending(x => x.Gradetypeid);

            return await PaginatedList<Gradetype>.CreateAsync(query, pageIndex, pageSize);
        }



        public async Task<Gradetype?> GetByIdAsync(int id)
        {
            return await _context.Gradetypes.FindAsync(id);
        }

        public async Task<object> CreateAsync(GradetypeDto dto)
        {
            var entity = new Gradetype
            {
                Gradetypename = dto.Gradetypename!,
                Weightingfactor = dto.Weightingfactor!.Value,
                Mininstancessemester1 = dto.Mininstancessemester1!.Value,
                Mininstancessemester2 = dto.Mininstancessemester2!.Value
            };

            _context.Gradetypes.Add(entity);
            await _context.SaveChangesAsync();

            return new
            {
                success = true,
                message = "Thêm loại điểm thành công!",
                data = entity
            };
        }

        public async Task<object> UpdateAsync(int id, GradetypeDto dto)
        {
            var entity = await _context.Gradetypes.FindAsync(id);
            if (entity == null)
            {
                return new { success = false, message = "Không tìm thấy loại điểm cần sửa." };
            }

            entity.Gradetypename = dto.Gradetypename!;
            entity.Weightingfactor = dto.Weightingfactor!.Value;
            entity.Mininstancessemester1 = dto.Mininstancessemester1!.Value;
            entity.Mininstancessemester2 = dto.Mininstancessemester2!.Value;

            await _context.SaveChangesAsync();

            return new { success = true, message = "Cập nhật loại điểm thành công!" };
        }

        public async Task<object> DeleteAsync(int id)
        {
            var entity = await _context.Gradetypes.FindAsync(id);
            if (entity == null)
            {
                return new { success = false, message = "Không tìm thấy loại điểm để xoá." };
            }

            _context.Gradetypes.Remove(entity);
            await _context.SaveChangesAsync();

            return new { success = true, message = "Xoá loại điểm thành công!" };
        }
    }
}
