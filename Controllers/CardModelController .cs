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
        [HttpGet("GetAllCards")]
        public async Task<ActionResult<IEnumerable<CardVM>>> GetAllCards()
        {
            var cards = await _repository.GetAllAsync();
            if (cards == null)
                return NotFound();

            var cardVMs = _mapper.Map<IEnumerable<CardVM>>(cards); // Correct Mapping
            return Ok(cardVMs);
        }
     
        // ✅ Get card by ID
        [HttpGet("GetCardById/{id}")]
        public async Task<IActionResult> GetCardById(string id)
        {
            var card = await _repository.GetByIdAsync(id);
            if (card == null)
                return NotFound();

            var cardVM = _mapper.Map<CardVM>(card); // Correct Mapping
            return Ok(cardVM);
        }
        [HttpPost("CreateCard")]
        public async Task<ActionResult<CreateCardVM>> CreateCard([FromBody] CreateCardVM cardVM)
        {

            if (cardVM == null)
                return BadRequest("Card data is required.");

            var card = _mapper.Map<CardModel>(cardVM);
            card.Id = Guid.NewGuid().ToString();
            var createdcard = await _repository.CreateAsync(card);
            if (createdcard == null)
                return BadRequest("Card data is required.");
            var cardtViewModel = _mapper.Map<CardVM>(createdcard);
            return Ok(cardtViewModel);
            //return CreatedAtAction(nameof(GetByIdSchool), new { id = school.Id }, school);
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
        [HttpPut("UpdateCard/{id}")]
        public async Task<ActionResult<CreateCardVM>> UpdateCard(string id, [FromBody] CreateCardVM vm)
        {
            if (!ModelState.IsValid || vm == null)
                return BadRequest("البيانات غير صالحة.");

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return NotFound("البطاقة غير موجود.");


            var card = _mapper.Map<CardModel>(vm);
                card.Id = id;
            var ns=   _mapper.Map(card, existing);
            var item = await _repository.UpdateAsync(ns);
            if (item != null)
            {
                var vmstu = _mapper.Map<CreateCardVM>(item);
                return Ok(vmstu);
            }
            return BadRequest("تم التحديث بنجاح.");
        }

        // ✅ Delete a card
        [HttpDelete("DeleteCard/{id}")]
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
