
using Api.SM.Data;
using Api.SM.Models;

namespace Api.SM.Repository;

public interface IStudentRepository : IRepsitory<StudentModel>
{
 
}
public class StudentRepository : Repository<StudentModel>, IStudentRepository
{
    private readonly INameRepository nameRepository;
    public StudentRepository(DataContext context,INameRepository nameReposi) : base(context)
    {
        nameRepository = nameReposi ;

       
    }

    public override Task CreateAsync(StudentModel entity)
    {
        nameRepository.GetByIdAsync(entity.card.Name.Name).Wait();

        return base.CreateAsync(entity);
    }

    
   

}