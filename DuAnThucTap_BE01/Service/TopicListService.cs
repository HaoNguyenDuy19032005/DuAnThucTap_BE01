using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Data;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Service
{
    public class TopicListService : ITopicListService
    {
        private readonly ApplicationDbContext _context;

        public TopicListService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TopicList>> GetAllAsync()
        {
            return await _context.TopicLists.ToListAsync();
        }

        public async Task<TopicList?> GetByIdAsync(int id)
        {
            return await _context.TopicLists.FindAsync(id);
        }

        public async Task<TopicList> CreateAsync(TopicList topic)
        {
            _context.TopicLists.Add(topic);
            await _context.SaveChangesAsync();
            return topic;
        }

        public async Task<TopicList?> UpdateAsync(int id, TopicList topic)
        {
            var existing = await _context.TopicLists.FindAsync(id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(topic);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var topic = await _context.TopicLists.FindAsync(id);
            if (topic == null) return false;

            _context.TopicLists.Remove(topic);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
