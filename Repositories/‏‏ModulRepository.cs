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
    private readonly IRowRepository _rowRepository;

    public ModulRepository(DataContext context, IRowRepository rowRepository) : base(context)
    {
        _rowRepository = rowRepository;
    }
    public override async Task<ModulModel?> CreateAsync(ModulModel entity)
    {
        if (string.IsNullOrWhiteSpace(entity.RowId))
            return null;

        var row = await _rowRepository.GetByIdAsync(entity.RowId);

        // تحقق أن الصف موجود وأن لديه SchoolId صالح
       // if (row == null || string.IsNullOrWhiteSpace(row.SchoolId))
            if (row == null )

                return null;

        return await base.CreateAsync(entity);
    }

    public async Task<ModulModel?> GetWithRelationsAsync(string id)
    {
        return await _dbSet
            .Include(m => m.Row)
            .Include(m => m.Teachers)
            .Include(m => m.Students)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
    //public override async Task<IEnumerable<ModulModel>?> GetAllAsync()
    //{
    //    return await _dbSet
    //        .Include(s => s.Row.Moduls) // تضمين الكيان المرتبط
    //        .ToListAsync();
    //}

    public override Task<ModulModel?> GetByIdAsync(string id)
    {


        return _dbSet.
               Where(x => x.Id == id).
               Include(p => p.Row).
               FirstOrDefaultAsync();
    }
}



