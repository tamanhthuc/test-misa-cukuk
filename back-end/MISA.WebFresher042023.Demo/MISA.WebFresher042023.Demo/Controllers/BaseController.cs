
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Core.Entity;
using MISA.WebFresher042023.Demo.Core.Interface;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using MISA.WebFresher042023.Demo.Core.MISAException;
using MISA.WebFresher042023.Demo.Core.Service;

namespace MISA.WebFresher042023.Demo.Controllers
{
    [ApiController]
    public abstract class BaseController<TEntityDto, TEntityCreateDto, TEntityUpdateDto> : ControllerBase
    {
        protected readonly IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> _baseService;

        public BaseController(IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> baseService)
        {
            _baseService = baseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var entityDtos = await _baseService.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, entityDtos);
        }

        /// <summary>
        /// Hàm lấy đối tượng
        /// </summary>
        /// <param name="id">Định danh của đối tượng</param>
        /// <returns>Đối tượng</returns>
        /// Created by: tmthuc (19/06/2023)

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var entityDto = await _baseService.GetAsync(id);

            return StatusCode(StatusCodes.Status200OK, entityDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TEntityCreateDto entityCreateDto)
        {
            
            int? result = await _baseService.InsertAsync(entityCreateDto);

            return StatusCode(StatusCodes.Status201Created, result);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] TEntityUpdateDto entityUpdateDto)
        {

            var result = await _baseService.UpdateAsync(id, entityUpdateDto);

            return StatusCode(StatusCodes.Status204NoContent, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _baseService.DeleteAsync(id);
            return StatusCode(StatusCodes.Status200OK, result);

        }

        [HttpPost("mass-delete")]
        public async Task<IActionResult> DeleteMassAsync([FromBody] List<Guid> ids)
        {
            var result = await _baseService.DeleteMultipelAsync(ids);

            return StatusCode(StatusCodes.Status200OK, result);

        }


    }
}
