
using Api.SM.Data;
using Api.SM.Models;

namespace Api.SM.Repository;

public interface INameRepository:IRepsitory<NameModel>
{
 
}
public class NameRepository : Repository<NameModel>, INameRepository
{
    public NameRepository(DataContext context) : base(context)
    {
    }

   

}