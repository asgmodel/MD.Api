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
}

public class CardRepository : Repository<CardModel>, ICardRepository
{
    private readonly IStudentRepository _studentRepository ;

    public CardRepository(DataContext context,  IStudentRepository studentRepository) : base(context)
    {
        _studentRepository = studentRepository;
    }

    public override async Task<CardModel?> CreateAsync(CardModel entity)
    {
        var card = await _studentRepository.GetByIdAsync(entity.StudentId);
        if (card == null || card.RowId == null || card.SchoolId == null )
            return null;
        
        return await base.CreateAsync(entity);
    }




}

