using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Iterface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Service
{
    public class TestAssignmentService : ITestAssignment
    {
        private static List<TestAssignment> _testAssignments = new List<TestAssignment>();
        private static int _nextAssignmentId = 1;

        public Task<IEnumerable<TestAssignment>> GetAllAsync()
        {
            return Task.FromResult(_testAssignments.AsEnumerable());
        }

        public Task<TestAssignment> GetByIdAsync(int id)
        {
            return Task.FromResult(_testAssignments.FirstOrDefault(ta => ta.AssignmentId == id));
        }

        public Task<TestAssignment> CreateAsync(TestAssignment testAssignment)
        {
            testAssignment.AssignmentId = _nextAssignmentId++;
            _testAssignments.Add(testAssignment);
            return Task.FromResult(testAssignment);
        }

        public Task<TestAssignment> UpdateAsync(int id, TestAssignment testAssignment)
        {
            var existing = _testAssignments.FirstOrDefault(ta => ta.AssignmentId == id);
            if (existing == null) return Task.FromResult((TestAssignment)null);

            existing.TestId = testAssignment.TestId;
            existing.ClassId = testAssignment.ClassId;
            existing.Status = testAssignment.Status;

            return Task.FromResult(existing);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var testAssignment = _testAssignments.FirstOrDefault(ta => ta.AssignmentId == id);
            if (testAssignment == null) return Task.FromResult(false);

            _testAssignments.Remove(testAssignment);
            return Task.FromResult(true);
        }
    }
}