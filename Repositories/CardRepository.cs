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


public interface ICardRepository : IRepsitory<CardModel>
{
    Task CreateAsync(CardModel card);
    Task<IEnumerable<CardModel>> GetAllCardsAsync();
    Task<ICollection<string>> GetNameModulsAsync(string studentId);
}

public class CardRepository : Repository<CardModel>, ICardRepository
{
    private readonly INameRepository _nameRepository;
    private readonly DataContext _context; // أضفنا هذه لتعريف السياق إذا احتجناه مستقبلاً

    public CardRepository(DataContext context, INameRepository nameRepository) : base(context)
    {
        _context = context;
        _nameRepository = nameRepository;
    }
    public override async Task CreateAsync(CardModel entity)
    {
        // التحقق هل يوجد كارد بنفس الـ Id أو بنفس الاسم
        bool exists = await _dbSet.AnyAsync(c => c.Id == entity.Id || c.Name.Name == entity.Name.Name);

        if (exists)
        {
            throw new InvalidOperationException("A CardModel with the same ID or Name already exists.");
        }

        await base.CreateAsync(entity);
    }

    public async Task<IEnumerable<CardModel>> GetAllCardsAsync()
    {
        return await _dbSet
            .Include(c => c.Name) // إضافة Include اختياري لتحميل الأسماء مع الكروت لو احتجت
            .ToListAsync();
    }

    public async Task<ICollection<string>> GetNameModulsAsync(string studentId)
    {
        var card = await _dbSet
            .Include(c => c.Name)
            .FirstOrDefaultAsync(c => c.Id == studentId);

        if (card?.Name != null)
        {
            string fullName = $"{card.Name.Name} {card.Name.Title}";
            return new List<string> { fullName };
        }

        return new List<string>();
    }
}

