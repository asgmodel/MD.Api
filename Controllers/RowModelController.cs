﻿//using Api.SM.Models;
//using Api.SM.Repository;
//using Api.SM.VM;
//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;

//namespace MD.Api.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class RowModelController : ControllerBase
//    {
//        private readonly IRowRepository _repository;
//        private readonly IMapper _mapper;

//        public RowModelController(IRowRepository repository, IMapper mapper)
//        {
//            _repository = repository;
//            _mapper = mapper;
//        }

//        // GET: /RowModel
//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var rowModels = await _repository.GetAllAsync();
//            return Ok(rowModels);
//        }

//        // GET: /RowModel/{id}
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetById(string id)
//        {
//            var rowModel = await _repository.GetByIdAsync(id);
//            if (rowModel == null)
//            {
//                return NotFound();
//            }
//            return Ok(rowModel);
//        }

//        // POST: /RowModel
//        [HttpPost("POSTRowModel")]
//        public async Task<IActionResult> Create([FromBody] RowVM rowModel)
//        {
//            if (rowModel == null)
//            {
//                return BadRequest("Row data is required.");
//            }

//            var row = _mapper.Map<RowModel>(rowModel);

//            row.Id = new IDgenerate().GenvetId("Row", '-', 10, 4, 3); // استخدم IDgenerate هنا كما هو في الموديل.

//            await _repository.CreateAsync(row);
//            return CreatedAtAction(nameof(GetById), new { id = row.Id }, row);
//        }

//        // PUT: /RowModel/{id}
//        //[HttpPut("{id}")]
//        //public async Task<IActionResult> Update(string id, [FromBody] UpdateRowVM rowModel)
//        //{
//        //    if (rowModel == null || id != rowModel.Id)
//        //    {
//        //        return BadRequest("Row data is incorrect.");
//        //    }

//        //    var existingRow = await _repository.GetByIdAsync(id);
//        //    if (existingRow == null)
//        //    {
//        //        return NotFound();
//        //    }

//        //    _mapper.Map(rowModel, existingRow);
//        //    await _repository.UpdateAsync(existingRow);

//        //    return NoContent();
//        //}

//        // DELETE: /RowModel/{id}
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(string id)
//        {
//            var existingRow = await _repository.GetByIdAsync(id);
//            if (existingRow == null)
//            {
//                return NotFound();
//            }

//            await _repository.DeleteAsync(id);
//            return NoContent();
//        }
//    }
//}

using Api.SM.Models;
using Api.SM.Repository;
using Api.SM.VM;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MD.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RowModelController : ControllerBase
    {
        private readonly IRowRepository _repository;
        private readonly IMapper _mapper;

        public RowModelController(IRowRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: /RowModel
        //[HttpGet("GetAllRowModel")]

        //public async Task<IActionResult> GetAll()
        //{
        //    var rowModels = await _repository.GetAllAsync();
        //    return Ok(rowModels);
        //}
        [HttpGet("GetAllRowModel")]
        public async Task<ActionResult<IEnumerable<RowVM>>> GetAllRowModel()
        {
            var row = await _repository.GetAllAsync();
            var rowVM = _mapper.Map<IEnumerable<RowVM>>(row);
            return Ok(rowVM);
        }

        // GET: /RowModel/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var row = await _repository.GetByIdAsync(id);
            if (row == null)
                return NotFound("صف غير موجود.");

            var rowVM = _mapper.Map<RowVM>(row); // Correct Mapping
            return Ok(rowVM);
        }

        //// POST: /RowModel
        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] CreateRowVM rowModel)
        //{
        //    if (rowModel == null)
        //    {
        //        return BadRequest();
        //    }


        //    var row = _mapper.Map<RowModel>(rowModel);


        //    row.Id = Guid.NewGuid().ToString(); // استخدم IDgenerate هنا كما هو في الموديل.

        //    var rowVMs = await _repository.CreateAsync(row);
        //    if (rowVMs == null)
        //        return BadRequest();

        //    return CreatedAtAction(nameof(GetById), new { id = rowVMs.Id }, rowModel);
        //}
        [HttpPost("CreateRows")]
        public async Task<ActionResult<CreateRowVM>> CreateRows([FromBody] CreateRowVM rowModel)
        {
            if (rowModel == null)
            {
                return BadRequest();
            }

            // تحقق من وجود المدرسة قبل إنشاء الصف
            if (rowModel == null || string.IsNullOrWhiteSpace(rowModel.SchoolId))
            {
                return BadRequest("البيانات المدخلة غير صالحة.");
            }

            //var school = await _repository.GetByIdAsync(rowModel.SchoolId);
            //if (school == null)
            //{
            //    return BadRequest("المدرسة غير موجودة.");
            //}

            var row = _mapper.Map<RowModel>(rowModel);
            row.Id = Guid.NewGuid().ToString();


            var createdRow = await _repository.CreateAsync(row);
            if (createdRow == null)
            {
                return BadRequest("فشل في إنشاء الصف.");
            }
            var rowVM = _mapper.Map<RowVM>(row); // Return the created card


            return Ok(rowVM);
        
        }

        // PUT: /RowModel/{id}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(string id, [FromBody] UpdateRowVM rowModel)
        //{
        //    if (rowModel == null || id != rowModel.Id)
        //    {
        //        return BadRequest();
        //    }

        //    var existingRow = await _repository.GetByIdAsync(id);
        //    if (existingRow == null)
        //    {
        //        return NotFound();
        //    }

        //    _mapper.Map(rowModel, existingRow);
        //    await _repository.UpdateAsync(existingRow);

        //    return NoContent();
        //}
        //[HttpPut("UpdateRows/{id}")]
        //public async Task<ActionResult<CreateRowVM>> UpdateRows(string id, [FromBody] CreateRowVM vm)
        //{
        //    if (!ModelState.IsValid || vm == null)
        //        return BadRequest("البيانات غير صالحة.");

        //    var existing = await _repository.GetByIdAsync(id);
        //    if (existing == null)
        //        return NotFound("صف غير موجود.");


        //    var row = _mapper.Map<RowModel>(vm);
        //    row.Id = id;

        //    var item = await _repository.UpdateAsync(existing);
        //    if (item != null)
        //    {
        //        var vmteacher = _mapper.Map<CreateRowVM>(item);
        //        return Ok(vmteacher);
        //    }
        //    return BadRequest("تم التحديث بنجاح.");
        //}


        [HttpPut("UpdateRows/{id}")]
        public async Task<ActionResult<CreateRowVM>> UpdateRows(string id, [FromBody] CreateRowVM vm)
        {
            if (!ModelState.IsValid || vm == null)
                return BadRequest("البيانات غير صالحة.");

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return NotFound("صف غير موجود.");
            
            // تحديث الكائن الموجود مباشرةً بالقيم الجديدة
            _mapper.Map(vm, existing); // هذا يُحدث الكائن الموجود وليس إنشاء كائن جديد

            var item = await _repository.UpdateAsync(existing); // الآن التحديث فعلي
            if (item != null)
            {
                var vmteacher = _mapper.Map<CreateRowVM>(item);
                return Ok(vmteacher);
            }

            return BadRequest("فشل التحديث.");
        }

        // DELETE: /RowModel/{id}
        [HttpDelete("DeleteRows{id}")]
        public async Task<IActionResult> DeleteRows(string id)
        {



            var deleted =  await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound("صف غير موجود.");
            var Isdle = await _repository.DeleteAsync(id);
            if (!Isdle)
                return Ok("تم الحذف بنجاح.");
            return BadRequest("Nou Delete ");


            //var existingRow = await _repository.DeleteAsync(id);
            //if (existingRow == null)
            //{
            //    return NotFound();
            //}

            //await _repository.DeleteAsync(id);
            //return NoContent();
        }
        [HttpGet("SearchRows")]
        public async Task<IActionResult> SearchRows(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest("يرجى إدخال كلمة للبحث.");

            var row = await _repository.GetRowNameByIdAsync(keyword);

            if (row == null )
                return NotFound("لا توجد نتائج.");

            var rowtVMs = _mapper.Map<IEnumerable<RowVM>>(row);
            return Ok(rowtVMs);
        }

    }
}
