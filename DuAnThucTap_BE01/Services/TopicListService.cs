using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Helpers;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Services
{
    public class TopicListService : ITopicListService
    {
        private readonly ISCDbContext _context;

        public TopicListService(ISCDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Topiclist>> GetAllAsync(string? searchTerm, int pageNumber, int pageSize)
        {
            // Bắt đầu với một IQueryable để xây dựng truy vấn
            var query = _context.Topiclists.AsQueryable();

            // 1. Áp dụng bộ lọc tìm kiếm (Filtering)
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
                query = query.Where(t =>
                    t.Topicname.ToLower().Contains(lowerCaseSearchTerm) ||
                    (t.Description != null && t.Description.ToLower().Contains(lowerCaseSearchTerm))
                );
            }

            // 2. Lấy tổng số lượng kết quả (trước khi phân trang)
            var totalCount = await query.CountAsync();

            // 3. Áp dụng phân trang (Paging)
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Trả về kết quả đã được đóng gói
            return new PagedResult<Topiclist>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<Topiclist?> GetByIdAsync(int id)
        {
            return await _context.Topiclists.FindAsync(id);
        }

        // --- Các phương thức Create, Update, Delete giữ nguyên logic cũ ---

        public async Task<Topiclist> CreateAsync(Topiclist topicList)
        {
            // Kiểm tra logic nghiệp vụ (ví dụ: tên chủ đề không được trùng)
            var existingTopic = await _context.Topiclists
                                        .FirstOrDefaultAsync(t => t.Topicname.ToLower() == topicList.Topicname.ToLower());
            if (existingTopic != null)
            {
                throw new ArgumentException($"Chủ đề với tên '{topicList.Topicname}' đã tồn tại.");
            }

            _context.Topiclists.Add(topicList);
            await _context.SaveChangesAsync();
            return topicList;
        }   

        public async Task<Topiclist?> UpdateAsync(int id, Topiclist updatedTopicList)
        {
            var existing = await _context.Topiclists.FindAsync(id);
            if (existing == null) return null;

            // Kiểm tra logic nghiệp vụ (ví dụ: tên chủ đề không được trùng với một chủ đề khác)
            var duplicateTopic = await _context.Topiclists
                .FirstOrDefaultAsync(t => t.Topicname.ToLower() == updatedTopicList.Topicname.ToLower() && t.Topicid != id);
            if (duplicateTopic != null)
            {
                throw new ArgumentException($"Chủ đề với tên '{updatedTopicList.Topicname}' đã tồn tại.");
            }

            existing.Topicname = updatedTopicList.Topicname;
            existing.Description = updatedTopicList.Description;
            existing.Teachingenddate = updatedTopicList.Teachingenddate;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var topicList = await _context.Topiclists.FindAsync(id);
            if (topicList == null) return false;

            _context.Topiclists.Remove(topicList);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
