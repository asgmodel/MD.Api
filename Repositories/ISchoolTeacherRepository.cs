using Api.SM.Data;
using Api.SM.Models;
using Api.SM.Repository;
using Api.SM.VM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface ISchoolTeacherRepository : IRepsitory<SchoolTeacher>
{
    //Task<List<SchoolTeacher>> GetAllAsync();
    //Task<SchoolTeacher?> GetByIdAsync(string id);
    //Task CreateAsync(SchoolTeacher entity);
    //Task DeleteAsync(string id);
}


    public class SchoolTeacherRepository : Repository<SchoolTeacher>, ISchoolTeacherRepository
{

      
    public SchoolTeacherRepository(DataContext context) : base(context) { }
    public async Task<List<SchoolTeacher>> GetAllAsync()
    {
        return await _dbSet
            .Include(st => st.TeacherModel)
                .ThenInclude(t => t.Name) // إذا أردت اسم المعلم
            .Include(st => st.SchoolModel)
            .ToListAsync();
    }

    //public async Task<List<SchoolTeacher>> GetAllAsync()
    //{
    //    return await _dbSet
    //        .Include(st => st.SchoolModel)
    //        .Include(st => st.TeacherModel)
    //        .ToListAsync();
    //}

    public async Task<SchoolTeacher?> GetByIdAsync(string id)
    {
        return await _dbSet
            .Include(st => st.SchoolModel)
            .Include(st => st.TeacherModel)
            .FirstOrDefaultAsync(st => st.Id == id);
    }
   

    
}
//override 
//    public async Task CreateAsync(SchoolTeacher entity)
//    {
//        entity.Id = Guid.NewGuid().ToString();
//        await _context.SchoolTeachers.AddAsync(entity);
//        await _context.SaveChangesAsync();
//    }