using Api.SM.Models;
using Api.SM.Repository;
using Api.SM.VM;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

//namespace Api.SM.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class CardModelController : ControllerBase
//    {
//        private readonly ICardRepository _repository;
//        private readonly IMapper _mapper;

//        public CardModelController(ICardRepository repository, IMapper mapper)
//        {
//            _repository = repository;
//            _mapper = mapper;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var cards = await _repository.GetAllCardsAsync();
//            var cardVMs = _mapper.Map<IEnumerable<CardVM>>(cards);
//            return Ok(cardVMs);
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetById(string id)
//        {
//            var card = await _repository.GetByIdAsync(id);
//            if (card == null)
//                return NotFound();

//            var cardVM = _mapper.Map<CardVM>(card);
//            return Ok(cardVM);
//        }
//        [HttpPost]
//        public async Task<IActionResult> Create([FromBody] CardVM vm)
//        {
//            if (vm == null)
//                return BadRequest();

//            var card = new CardModel
//            {
//                Id = new IDgenerate().GenvetId("Card", '-', 10, 4, 3),
//                Name = new NameModel
//                {
//                    Name = vm.Name.Name,   // ✅ صح
//                    Title = vm.Name.Title  // ✅ صح
//                },
//                Date = vm.Date,
//              //  SexType = vm.SexType
//            };

//            await _repository.CreateAsync(card);
//            return CreatedAtAction(nameof(GetById), new { id = card.Id }, card);
//        }

//        //[HttpPut("{id}")]
//        //public async Task<IActionResult> Update(string id, [FromBody] UpdateCardVM vm)
//        //{
//        //    if (vm == null || id != vm.Id)
//        //        return BadRequest();

//        //    var card = await _repository.GetByIdAsync(id);
//        //    if (card == null)
//        //        return NotFound();

//        //    card.Name.Name = vm.FullName.Name;    // ✅ صح
//        //    card.Name.Title = vm.FullName.Title;  // ✅ صح
//        //    card.Date = vm.Date;
//        //    card.SexType = vm.SexType;

//        //    await _repository.UpdateAsync(card);
//        //    return NoContent();
//        //}


//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(string id)
//        {
//            var card = await _repository.GetByIdAsync(id);
//            if (card == null)
//                return NotFound();

//            await _repository.DeleteAsync(id);
//            return NoContent();
//        }
//    }
//}
//using Api.SM.Models;
using Api.SM.Models;
using Api.SM.Repository;
using Api.SM.VM;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Api.SM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardModelController : ControllerBase
    {
        private readonly ICardRepository _repository;
        private readonly IMapper _mapper;

        public CardModelController(ICardRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // ✅ Get all cards
        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            var cards = await _repository.GetAllAsync();
            if (cards == null)
                return NotFound();

            var cardVMs = _mapper.Map<IEnumerable<CardVM>>(cards); // Correct Mapping
            return Ok(cardVMs);
        }
        [Authorize ]
        // ✅ Get card by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCardById(string id)
        {
            var card = await _repository.GetByIdAsync(id);
            if (card == null)
                return NotFound();

            var cardVM = _mapper.Map<CardVM>(card); // Correct Mapping
            return Ok(cardVM);
        }

        // ✅ Create new card
        //[HttpPost]
        //public async Task<IActionResult> CreateCard([FromBody] CardVM createCardVM)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var cardModel = _mapper.Map<CardModel>(createCardVM); // Map CreateCardVM -> CardModel
        //    await _repository.CreateAsync(cardModel);

        //    var cardVM = _mapper.Map<CardVM>(cardModel); // Return the created card
        //    return CreatedAtAction(nameof(GetCardById), new { id = cardModel.Id }, cardVM);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] CardVM vm)
        //{
        //    if (vm == null)
        //        return BadRequest();
        //    //var cardModel = _mapper.Map<CardModel>(vm); // Map CreateCardVM -> CardModel
        //    //var card = new CardModel
        //    //{
        //    //    Id = Guid.NewGuid().ToString(),
        //    //    Name = new NameModel
        //    //    {
        //    //        Name = vm.Name.Name,   // ✅ صح
        //    //        Title = vm.Name.Title  // ✅ صح
        //    //    },
        //    //    Date = vm.Date,
        //    //    SexType = (SexType?)vm.SexType
        //    //};

        //    await _repository.CreateAsync(card);
        //    return CreatedAtAction(nameof(GetCardById), new { id = card.Id }, card);
        //}

        // ✅ Update existing card
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateCard(string id, [FromBody] UpdateCardVM updateCardVM)
        //{
        //    if (id != updateCardVM.Id)
        //        return BadRequest("Card ID mismatch.");

        //    var existingCard = await _repository.GetByIdAsync(id);
        //    if (existingCard == null)
        //        return NotFound();

        //    var cardModel = _mapper.Map(updateCardVM, existingCard); // Update existing entity
        //    await _repository.UpdateAsync(cardModel);

        //    return NoContent();
        //}

        // ✅ Delete a card
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(string id)
        {
            var card = await _repository.GetByIdAsync(id);
            if (card == null)
                return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }

        // ✅ Get FullName of Name related to Card
        [HttpGet("{studentId}/name")]
        public async Task<IActionResult> GetCardFullName()
        {
            var names = await _repository.GetAllAsync();
            

            return Ok(names);
        }
    }
}
