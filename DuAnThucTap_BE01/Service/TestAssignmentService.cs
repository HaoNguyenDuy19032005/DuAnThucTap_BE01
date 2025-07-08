using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Interface; // Sửa "Iterface" thành "Interface" nếu đây là lỗi đánh máy
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Service
{
    public class TestAssignmentService : ITestAssignment
    {
        private static List<Testassignment> _testAssignments = new List<Testassignment>();
        private static int _nextAssignmentId = 1;

        public Task<IEnumerable<Testassignment>> GetAllAsync()
        {
            return Task.FromResult(_testAssignments.AsEnumerable());
        }

        public Task<Testassignment> GetByIdAsync(int id)
        {
            return Task.FromResult(_testAssignments.FirstOrDefault(ta => ta.Assignmentid == id));
        }

        public Task<Testassignment> CreateAsync(Testassignment testAssignment)
        {
            testAssignment.Assignmentid = _nextAssignmentId++;
            _testAssignments.Add(testAssignment);
            return Task.FromResult(testAssignment);
        }

        public Task<Testassignment> UpdateAsync(int id, Testassignment testAssignment)
        {
            var existing = _testAssignments.FirstOrDefault(ta => ta.Assignmentid == id);
            if (existing == null) return Task.FromResult((Testassignment)null);

            existing.Testid = testAssignment.Testid;
            existing.Classid = testAssignment.Classid;
            existing.Status = testAssignment.Status;

            return Task.FromResult(existing);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var testAssignment = _testAssignments.FirstOrDefault(ta => ta.Assignmentid == id);
            if (testAssignment == null) return Task.FromResult(false);

            _testAssignments.Remove(testAssignment);
            return Task.FromResult(true);
        }
    }
}