using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.DTO;
using Nhom2ThucTap.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nhom2ThucTap.Service
{
    public class TestQuestionService : ITestQuestionService
    {
        private readonly AppDbContext _context;

        public TestQuestionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TestQuestionItem>> GetAllAsync()
        {
            return await _context.TestQuestionItems.ToListAsync();
        }

        public async Task<TestQuestionItem?> GetByIdAsync(int id)
        {
            return await _context.TestQuestionItems.FindAsync(id);
        }

        public async Task<TestQuestionItem> CreateAsync(TestQuestionItemDto dto)
        {
            var question = new TestQuestionItem
            {
                TestId = dto.TestId,
                DisplayOrder = dto.DisplayOrder,
                Content = dto.Content ?? "",
                OptionA = dto.OptionA,
                OptionB = dto.OptionB,
                OptionC = dto.OptionC,
                OptionD = dto.OptionD,
                CreatedAt = DateTime.UtcNow
            };

            _context.TestQuestionItems.Add(question);
            await _context.SaveChangesAsync();
            return question;
        }

        public async Task<TestQuestionItem?> UpdateAsync(int id, TestQuestionItemDto dto)
        {
            var question = await _context.TestQuestionItems.FindAsync(id);
            if (question == null) return null;

            question.TestId = dto.TestId;
            question.DisplayOrder = dto.DisplayOrder;
            question.Content = dto.Content ?? "";
            question.OptionA = dto.OptionA;
            question.OptionB = dto.OptionB;
            question.OptionC = dto.OptionC;
            question.OptionD = dto.OptionD;
            question.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return question;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var question = await _context.TestQuestionItems.FindAsync(id);
            if (question == null) return false;

            _context.TestQuestionItems.Remove(question);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
