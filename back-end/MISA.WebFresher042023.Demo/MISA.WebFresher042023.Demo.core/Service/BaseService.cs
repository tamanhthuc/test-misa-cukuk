using AutoMapper;
using MISA.WebFresher042023.Demo.Core.DTo;
using MISA.WebFresher042023.Demo.Core.Entity;
using MISA.WebFresher042023.Demo.Core.Interface;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using MISA.WebFresher042023.Demo.Core.MISAException;
using MISA.WebFresher042023.Demo.Core.Respond;
using MISA.WebFresher042023.Demo.MISAException;

namespace MISA.WebFresher042023.Demo.Core.Service
{
    public abstract class BaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto>
        : IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    {
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IMapper _mapper;

        protected BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Xoá 1 bản ghi
        /// </summary>
        /// <param name="id">id của bản ghi</param>
        /// <returns>1</returns>
        /// Created by: tmthuc (24/06/2023)

        public virtual async Task<int> DeleteAsync(Guid id)
        {
            var entity = await _baseRepository.GetAsync(id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            var result = await _baseRepository.DeleteAsync(id);

            return result;

        }
        /// <summary>
        /// Xoá nhiều bản ghi
        /// </summary>
        /// <param name="ids">Danh sách id của các bản ghi</param>
        /// <returns>1</returns>
        /// Created by: tmthuc (24/06/2023)
        public async Task<int> DeleteMultipelAsync(List<Guid> ids)
        {
            var result = await _baseRepository.DeleteMultipleAsync(ids);

            return result;
        }



        public async Task<List<TEntityDto>> GetAllAsync()
        {
            var entities = await _baseRepository.GetAllAsync();


            var entiyDtos =  _mapper.Map<List<TEntityDto>>(entities);

            return entiyDtos;
        }

        public virtual async Task<TEntityDto> GetAsync(Guid id)
        {
            var entity = await _baseRepository.GetAsync(id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            var entityDto = _mapper.Map<TEntityDto>(entity);

            return entityDto;
        }


        public virtual async Task<int?> InsertAsync(TEntityCreateDto entityCreateDto)
        {
            TEntity? entity = _mapper.Map<TEntity>(entityCreateDto);
            /// thuc hiẹn validate chung 
            ValidateData(entity);

            var result = await _baseRepository.InsertAsync(entity);

            return result;
        }

        protected void ValidateData(TEntity entity)
        {

        }
        public virtual async Task<int> UpdateAsync(Guid id, TEntityUpdateDto entityUpdateDto)
        {
      
            var entity = _mapper.Map<TEntity>(entityUpdateDto);


            var result = await _baseRepository.UpdateAsync(id, entity);

            return result;
        }
    }
}
