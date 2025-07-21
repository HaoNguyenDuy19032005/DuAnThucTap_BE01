using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public class SchoolTransferHistoryService : ISchoolTransferHistoryService
    {
        private readonly AppDbContext _context;

        public SchoolTransferHistoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Schooltransferhistory>> GetAllAsync()
        {
            return await _context.Schooltransferhistories
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<(List<Schooltransferhistory> Transfers, int TotalCount)> GetPagedAsync(int page, int pageSize)
        {
            var query = _context.Schooltransferhistories.AsNoTracking();

            var totalCount = await query.CountAsync();

            var transfers = await query
                .OrderByDescending(t => t.Transferdate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (transfers, totalCount);
        }

        public async Task<Schooltransferhistory?> GetByIdAsync(int id)
        {
            return await _context.Schooltransferhistories.FindAsync(id);
        }

        public async Task<Schooltransferhistory> CreateAsync(Schooltransferhistory transfer)
        {
            _context.Schooltransferhistories.Add(transfer);
            await _context.SaveChangesAsync();
            return transfer;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var transfer = await _context.Schooltransferhistories.FindAsync(id);
            if (transfer == null) return false;

            _context.Schooltransferhistories.Remove(transfer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Schooltransferhistory?> UpdateAsync(int id, Schooltransferhistory transfer)
        {
            var existing = await _context.Schooltransferhistories.FindAsync(id);
            if (existing == null) return null;

            existing.Studentid = transfer.Studentid;
            existing.Fromschoolinfoid = transfer.Fromschoolinfoid;
            existing.Fromclassid = transfer.Fromclassid;
            existing.Toschoolinfoid = transfer.Toschoolinfoid;
            existing.Toclassid = transfer.Toclassid;
            existing.Semesterid = transfer.Semesterid;
            existing.Transferdate = transfer.Transferdate;
            existing.Reason = transfer.Reason;
            existing.Attachmenturl = transfer.Attachmenturl;
            existing.Transfertype = transfer.Transfertype;

            await _context.SaveChangesAsync();
            return existing;
        }
    }
}
