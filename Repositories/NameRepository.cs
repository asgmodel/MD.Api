
using Api.SM.Data;
using Api.SM.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.SM.Repository;

public interface INameRepository:IRepsitory<NameModel>
{
    Task<IEnumerable<NameModel>> GetAllNamesAsync();
}
public class NameRepository : Repository<NameModel>, INameRepository
{
    public NameRepository(DataContext context) : base(context)
    {
    }
    public async Task<IEnumerable<NameModel>> GetAllNamesAsync()
    {
        return await _dbSet.ToListAsync();
    }
    public override Task CreateAsync(NameModel entity)
    {
        return base.CreateAsync(entity);
    }

}