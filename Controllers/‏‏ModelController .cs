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
    public class ModulController : ControllerBase
    {
        private readonly IModulRepository _repository;
       
        private readonly IMapper _mapper;

        public ModulController(
            IModulRepository repository,
           
            IMapper mapper)
        {
            _repository = repository;
            
            _mapper = mapper;
        }
        [HttpGet("all")]

        public async Task<IActionResult> GetAll()
        {
            var modul = await _repository.GetAllAsync();
            return Ok(modul);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateModulVM vm)
        {
            if (vm == null) return BadRequest();

            var modul = _mapper.Map<ModulModel>(vm);
         
            var created = await _repository.CreateAsync(modul);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<ModulVM>(created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] CreateModulVM vm)
        {
            var existing = await _repository.GetWithRelationsAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(vm, existing);
            var updated = await _repository.UpdateAsync(existing);
            return Ok(_mapper.Map<ModulVM>(updated));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var modul = await _repository.GetWithRelationsAsync(id);
            if (modul == null) return NotFound();
            return Ok(_mapper.Map<ModulVM>(modul));
        }
    }

}
