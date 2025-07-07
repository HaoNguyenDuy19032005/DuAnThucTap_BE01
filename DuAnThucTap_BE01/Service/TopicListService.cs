using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Services
{
    public class TopicListService : ITopicListService
    {
        private readonly DemoBuoi2DbContext _context;

        public TopicListService(DemoBuoi2DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TopicListDto>> GetAllAsync()
        {
            return await _context.Topiclists
                .Select(t => new TopicListDto
                {
                    Topicid = t.Topicid,
                    Topicname = t.Topicname,
                    Description = t.Description,
                    Teachingenddate = t.Teachingenddate.HasValue ? t.Teachingenddate.Value.ToString("yyyy-MM-dd") : null
                })
                .ToListAsync();
        }

        public async Task<TopicListDto?> GetByIdAsync(Guid id)
        {
            return await _context.Topiclists
                .Where(t => t.Topicid == id)
                .Select(t => new TopicListDto
                {
                    Topicid = t.Topicid,
                    Topicname = t.Topicname,
                    Description = t.Description,
                    Teachingenddate = t.Teachingenddate.HasValue ? t.Teachingenddate.Value.ToString("yyyy-MM-dd") : null
                })
                .FirstOrDefaultAsync();
        }

        public async Task<TopicListDto> CreateAsync(TopicListDto topicListDto)
        {
            var topic = new Topiclist
            {
                Topicid = Guid.NewGuid(),
                Topicname = topicListDto.Topicname,
                Description = topicListDto.Description,
                Teachingenddate = topicListDto.Teachingenddate != null ? DateOnly.Parse(topicListDto.Teachingenddate) : null
            };

            _context.Topiclists.Add(topic);
            await _context.SaveChangesAsync();

            return topicListDto;
        }

        public async Task<TopicListDto?> UpdateAsync(Guid id, TopicListDto topicListDto)
        {
            var existing = await _context.Topiclists.FindAsync(id);
            if (existing == null) return null;

            existing.Topicname = topicListDto.Topicname;
            existing.Description = topicListDto.Description;
            existing.Teachingenddate = topicListDto.Teachingenddate != null ? DateOnly.Parse(topicListDto.Teachingenddate) : null;

            await _context.SaveChangesAsync();

            return new TopicListDto
            {
                Topicid = existing.Topicid,
                Topicname = existing.Topicname,
                Description = existing.Description,
                Teachingenddate = existing.Teachingenddate.HasValue ? existing.Teachingenddate.Value.ToString("yyyy-MM-dd") : null
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var topic = await _context.Topiclists.FindAsync(id);
            if (topic == null) return false;

            _context.Topiclists.Remove(topic);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}