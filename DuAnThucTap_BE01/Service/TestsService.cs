using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Iterface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Service
{
    public class TestsService : ITests
    {
        private static List<Tests> _tests = new List<Tests>();
        private static int _nextId = 1;

        public Task<IEnumerable<Tests>> GetAllAsync()
        {
            return Task.FromResult(_tests.AsEnumerable());
        }

        public Task<Tests> GetByIdAsync(int id)
        {
            return Task.FromResult(_tests.FirstOrDefault(t => t.TestId == id));
        }

        public Task<Tests> CreateAsync(Tests test)
        {
            test.TestId = _nextId++;
            _tests.Add(test);
            return Task.FromResult(test);
        }

        public Task<Tests> UpdateAsync(int id, Tests test)
        {
            var existing = _tests.FirstOrDefault(t => t.TestId == id);
            if (existing == null) return Task.FromResult((Tests)null);

            existing.TeacherId = test.TeacherId;
            existing.Title = test.Title;
            existing.TestFormat = test.TestFormat;
            existing.DurationInMinutes = test.DurationInMinutes;
            existing.StartTime = test.StartTime;
            existing.EndTime = test.EndTime;
            existing.Description = test.Description;
            existing.Classification = test.Classification;
            existing.AttachmentUrl = test.AttachmentUrl;
            existing.RequireStudentAttachment = test.RequireStudentAttachment;

            return Task.FromResult(existing);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var test = _tests.FirstOrDefault(t => t.TestId == id);
            if (test == null) return Task.FromResult(false);

            _tests.Remove(test);
            return Task.FromResult(true);
        }
    }
}