using Api.SM.Data;
using Api.SM.Models;
using Api.SM.Repository;
using Microsoft.EntityFrameworkCore;

public interface ITeacherRepository : IRepsitory<TeacherModel>
{
  //  Task<TeacherModel?> GetTeacherWithRelationsAsync(string id);
}
public class TeacherRepository : Repository<TeacherModel>, ITeacherRepository
{
    public TeacherRepository(DataContext context) : base(context) { }

   
   
}
//public async Task<TeacherModel?> GetTeacherWithRelationsAsync(string id)
//{
//    return await _dbSet
//        .Include(t => t.Name)
//        .Include(t => t.Rows)
//        .Include(t => t.Moduls)
//        .Include(t => t.Students)
//        .Include(t => t.SchoolModels)

//        .FirstOrDefaultAsync(t => t.Id == id);
//}