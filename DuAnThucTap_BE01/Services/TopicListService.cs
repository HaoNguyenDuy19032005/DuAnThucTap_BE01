using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Topiclist>> GetAllAsync()
        {
            return await _context.Topiclists.ToListAsync();
        }

        public async Task<Topiclist?> GetByIdAsync(int id)
        {
            return await _context.Topiclists.FindAsync(id);
        }

        public async Task<Topiclist> CreateAsync(Topiclist topicList)
        {
            if (string.IsNullOrWhiteSpace(topicList.Topicname))
                throw new ArgumentException("Topicname là bắt buộc.");

            _context.Topiclists.Add(topicList);
            await _context.SaveChangesAsync();
            return topicList;
        }

        public async Task<Topiclist?> UpdateAsync(int id, Topiclist updatedTopicList)
        {
            var existing = await _context.Topiclists.FindAsync(id);
            if (existing == null) return null;

            if (string.IsNullOrWhiteSpace(updatedTopicList.Topicname))
                throw new ArgumentException("Topicname là bắt buộc.");

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