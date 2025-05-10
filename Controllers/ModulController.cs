
using Api.SM.Models;
using Api.SM.Repository;
using Api.SM.VM;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;

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
      
        [HttpGet("GetAllModul")]
        public async Task<ActionResult<IEnumerable<ModulVM>>> GetAllModul()
        {
            var modul = await _repository.GetAllAsync();
            var modulVM = _mapper.Map<IEnumerable<ModulVM>>(modul);
            return Ok(modulVM);
        }
        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] CreateModulVM vm)
        //{
        //    if (vm == null) return BadRequest();

        //    var modul = _mapper.Map<ModulModel>(vm);

        //    var created = await _repository.CreateAsync(modul);
        //    return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<ModulVM>(created));
        //}
        [HttpPost]
        public async Task<ActionResult<CreateModulVM>> CreateCard([FromBody] CreateModulVM creatVM)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var modul = _mapper.Map<ModulModel>(creatVM); // Map CreateCardVM -> CardModel
            await _repository.CreateAsync(modul);

            var modulVM = _mapper.Map<ModulVM>(modul); // Return the created card
            return Ok(modulVM);
            // return CreatedAtAction(nameof(GetById), new { id = modul.Id }, cardVM);
        }
        [HttpPost("CreateModul")]
        public async Task<ActionResult<ModulModel>> CreateModul([FromBody] CreateModulVM modulVM)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var modul = _mapper.Map<ModulModel>(modulVM);
            var createdmodul = await _repository.CreateAsync(modul);

            if (createdmodul == null)
            {


                var row = await _repository.GetByIdAsync(modulVM.RowId);
                if (row == null)
                    return BadRequest("الصف غير موجود.");

                if (row.Row.SchoolId != row.Row.SchoolId)
                    return BadRequest("الصف لا ينتمي إلى نفس المدرسة.");

                // return BadRequest("حدث خطأ أثناء إنشاء الطالب.");
            }
            var studentViewModel = _mapper.Map<ModulVM>(createdmodul);
            return Ok(studentViewModel);
        }
        [HttpPut("UpdateModul/{id}")]
        public async Task<ActionResult<CreateModulVM>> UpdateModul(string id, [FromBody] CreateModulVM vm)
        {
            if (!ModelState.IsValid || vm == null)
                return BadRequest("البيانات غير صالحة.");

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return NotFound("الطالب غير موجود.");


            var modul = _mapper.Map<ModulModel>(vm);
            modul.Id = id;

            var item = await _repository.UpdateAsync(existing);
            if (item != null)
            {
                var vmstu = _mapper.Map<CreateModulVM>(item);
                return Ok(vmstu);
            }
            return BadRequest("تم التحديث بنجاح.");
        }
        [HttpDelete("DeleteModul/{id}")]
        public async Task<IActionResult> DeleteModul(string id)
        {
            var card = await _repository.GetByIdAsync(id);
            if (card == null)
                return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }
        [HttpGet("GetByIdModul/{id}")]
        public async Task<IActionResult> GetByIdModul(string id)
        {
            var modul = await _repository.GetWithRelationsAsync(id);
            if (modul == null) return NotFound();
            return Ok(_mapper.Map<ModulVM>(modul));
        }
    }

}
