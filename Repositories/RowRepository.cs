using Api.SM.Data;
using Api.SM.Models;

namespace Api.SM.Repository
{
    // تعريف الواجهة الخاصة بـ RowRepository
    public interface IRowRepository : IRepsitory<RowModel>
    {
        Task AddStudent(string id, StudentModel student);
    }

    // تعريف الصف الخاص بـ RowRepository
    public class RowRepository : Repository<RowModel>, IRowRepository
    {
        // بناء الـ RowRepository بحيث يستقبل الـ DataContext
        public RowRepository(DataContext context) : base(context)
        {
        }
        public override Task CreateAsync(RowModel entity)
        {
            return base.CreateAsync(entity);
        }
        public async Task AddStudent(string id, StudentModel student)
        {
            var row = _dbSet.FirstOrDefault(r => r.Id == id);
            if (row != null)
            {
                student.Row = row;
                student.RowId = row.Id;
                row.Students.Add(student);
            }
            else
            {
                throw new Exception("Row not found");
            }
        }
        // يمكنك إضافة وظائف إضافية هنا حسب الحاجة
    }
}
