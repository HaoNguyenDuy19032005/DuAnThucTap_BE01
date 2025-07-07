using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Services
{
    public class TeachingAssignmentService : ITeachingAssignmentService
    {
        private readonly DemoBuoi2DbContext _context;

        public TeachingAssignmentService(DemoBuoi2DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TeachingAssignmentDto>> GetAllAsync()
        {
            return await _context.Teachingassignments
                .Select(ta => new TeachingAssignmentDto
                {
                    Assignmentid = ta.Assignmentid,
                    Teacherid = ta.Teacherid,
                    Subjectid = ta.Subjectid,
                    Classtypeid = ta.Classtypeid,
                    Topicid = ta.Topicid,
                    Schoolyearid = ta.Schoolyearid,
                    Teachingstartdate = ta.Teachingstartdate.HasValue ? ta.Teachingstartdate.Value.ToString("yyyy-MM-dd") : null,
                    Teachingenddate = ta.Teachingenddate.HasValue ? ta.Teachingenddate.Value.ToString("yyyy-MM-dd") : null,
                    Notes = ta.Notes
                })
                .ToListAsync();
        }

        public async Task<TeachingAssignmentDto?> GetByIdAsync(Guid id)
        {
            return await _context.Teachingassignments
                .Where(ta => ta.Assignmentid == id)
                .Select(ta => new TeachingAssignmentDto
                {
                    Assignmentid = ta.Assignmentid,
                    Teacherid = ta.Teacherid,
                    Subjectid = ta.Subjectid,
                    Classtypeid = ta.Classtypeid,
                    Topicid = ta.Topicid,
                    Schoolyearid = ta.Schoolyearid,
                    Teachingstartdate = ta.Teachingstartdate.HasValue ? ta.Teachingstartdate.Value.ToString("yyyy-MM-dd") : null,
                    Teachingenddate = ta.Teachingenddate.HasValue ? ta.Teachingenddate.Value.ToString("yyyy-MM-dd") : null,
                    Notes = ta.Notes
                })
                .FirstOrDefaultAsync();
        }

        public async Task<TeachingAssignmentDto> CreateAsync(TeachingAssignmentDto teachingAssignmentDto)
        {
            var teachingAssignment = new Teachingassignment
            {
                Assignmentid = Guid.NewGuid(),
                Teacherid = teachingAssignmentDto.Teacherid,
                Subjectid = teachingAssignmentDto.Subjectid,
                Classtypeid = teachingAssignmentDto.Classtypeid,
                Topicid = teachingAssignmentDto.Topicid,
                Schoolyearid = teachingAssignmentDto.Schoolyearid,
                Teachingstartdate = teachingAssignmentDto.Teachingstartdate != null ? DateOnly.Parse(teachingAssignmentDto.Teachingstartdate) : null,
                Teachingenddate = teachingAssignmentDto.Teachingenddate != null ? DateOnly.Parse(teachingAssignmentDto.Teachingenddate) : null,
                Notes = teachingAssignmentDto.Notes
            };

            _context.Teachingassignments.Add(teachingAssignment);
            await _context.SaveChangesAsync();

            return teachingAssignmentDto;
        }

        public async Task<TeachingAssignmentDto?> UpdateAsync(Guid id, TeachingAssignmentDto updatedTeachingAssignmentDto)
        {
            var existing = await _context.Teachingassignments.FindAsync(id);
            if (existing == null) return null;

            existing.Teacherid = updatedTeachingAssignmentDto.Teacherid;
            existing.Subjectid = updatedTeachingAssignmentDto.Subjectid;
            existing.Classtypeid = updatedTeachingAssignmentDto.Classtypeid;
            existing.Topicid = updatedTeachingAssignmentDto.Topicid;
            existing.Schoolyearid = updatedTeachingAssignmentDto.Schoolyearid;
            existing.Teachingstartdate = updatedTeachingAssignmentDto.Teachingstartdate != null ? DateOnly.Parse(updatedTeachingAssignmentDto.Teachingstartdate) : null;
            existing.Teachingenddate = updatedTeachingAssignmentDto.Teachingenddate != null ? DateOnly.Parse(updatedTeachingAssignmentDto.Teachingenddate) : null;
            existing.Notes = updatedTeachingAssignmentDto.Notes;

            await _context.SaveChangesAsync();

            return new TeachingAssignmentDto
            {
                Assignmentid = existing.Assignmentid,
                Teacherid = existing.Teacherid,
                Subjectid = existing.Subjectid,
                Classtypeid = existing.Classtypeid,
                Topicid = existing.Topicid,
                Schoolyearid = existing.Schoolyearid,
                Teachingstartdate = existing.Teachingstartdate.HasValue ? existing.Teachingstartdate.Value.ToString("yyyy-MM-dd") : null,
                Teachingenddate = existing.Teachingenddate.HasValue ? existing.Teachingenddate.Value.ToString("yyyy-MM-dd") : null,
                Notes = existing.Notes
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var teachingAssignment = await _context.Teachingassignments.FindAsync(id);
            if (teachingAssignment == null) return false;

            _context.Teachingassignments.Remove(teachingAssignment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}