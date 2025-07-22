<<<<<<< Updated upstream
﻿using DuAnThucTap_BE01.Iterface;
using DuAnThucTap_BE01.Models;
using DuAnThucTapNhom3.Iterface;
using DuAnThucTapNhom3.Models;
using Microsoft.EntityFrameworkCore;
using System;


namespace DuAnThucTapNhom3.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly AppDbContext _context;
        public DepartmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task<Department> CreateAsync(Department department)
        {
            department.Createdat = DateTime.UtcNow;
            department.Updatedat = DateTime.UtcNow;
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<Department> UpdateAsync(int id, Department department)
        {
            var existing = await _context.Departments.FindAsync(id);
            if (existing == null) return null;

            existing.Departmentname = department.Departmentname;
            existing.Headteacherid = department.Headteacherid;
            existing.Updatedat = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return false;

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return true;
        }
    }

=======
﻿using DuAnThucTap.Data;
using DuAnThucTap.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DepartmentService : IDepartmentService
{
    private readonly ApplicationDbContext _context;

    public DepartmentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Department>> GetAllAsync()
    {
        return await _context.Departments.ToListAsync();
    }

    public async Task<Department?> GetByIdAsync(int id)
    {
        return await _context.Departments.FindAsync(id);
    }

    public async Task<Department> CreateAsync(Department department)
    {
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();
        return department;
    }

    public async Task<bool> UpdateAsync(int id, Department department)
    {
        if (id != department.Departmentid) return false;

        _context.Entry(department).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var dept = await _context.Departments.FindAsync(id);
        if (dept == null) return false;

        _context.Departments.Remove(dept);
        await _context.SaveChangesAsync();
        return true;
    }
>>>>>>> Stashed changes
}
