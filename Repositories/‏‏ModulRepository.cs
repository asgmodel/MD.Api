//using Api.SM.Data;
//using Api.SM.Models;
//using Api.SM.Repository;
//using Microsoft.EntityFrameworkCore;

//public interface ICardRepository : IRepsitory<CardModel>
//{
//    Task<IEnumerable<CardModel>> GetAllCardsAsync();
//    Task<ICollection<string>> GetNameModulsAsync(string studentId);
//}

//public class CardRepository : Repository<CardModel>, ICardRepository
//{
//    private readonly INameRepository _nameRepository;
//    public CardRepository(DataContext context, INameRepository nameRepository) : base(context) {

//        _nameRepository = nameRepository;


//    }

//    public async Task<IEnumerable<CardModel>> GetAllCardsAsync()
//    {
//        return await _dbSet.ToListAsync();
//    }
//    public async Task<ICollection<string>> GetNameModulsAsync(string studentId)
//    {
//        var card = await _dbSet
//            .Include(c => c.Name)
//            .FirstOrDefaultAsync(c => c.Id == studentId);

//        if (card?.Name != null)
//        {
//            // ✅ نكون FullName هنا
//            string fullName = $"{card.Name.Name} {card.Name.Title}";
//            return new List<string> { fullName };
//        }

//        return new List<string>();
//    }


//}
using Api.SM.Data;
using Api.SM.Models;
using Api.SM.Repository;
using Api.SM.VM;
using Microsoft.EntityFrameworkCore;

public interface IModulRepository : IRepsitory<ModulModel>
{
    Task<ModulModel?> GetWithRelationsAsync(string id);
}



public class ModulRepository : Repository<ModulModel>, IModulRepository
{
    public ModulRepository(DataContext context) : base(context) { }

    public async Task<ModulModel?> GetWithRelationsAsync(string id)
    {
        return await _dbSet
            .Include(m => m.Row)
            .Include(m => m.Teachers)
            .Include(m => m.Students)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
}



