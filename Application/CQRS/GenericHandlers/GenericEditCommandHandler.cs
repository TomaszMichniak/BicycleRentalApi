using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.GenericHandlers
{
    public class GenericEditCommandHandler<TCommand, TEntity, TDto>
          : IRequestHandler<TCommand, TDto>
          where TCommand : IRequest<TDto>, IHasId
          where TEntity : class
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<TEntity> _repository;

        public GenericEditCommandHandler(IMapper mapper, IGenericRepository<TEntity> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<TDto> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new Exception($"{typeof(TEntity).Name} not found");
            }
            _mapper.Map(request, entity);
            var updatedEntity = await _repository.UpdateAsync(entity);
            return _mapper.Map<TDto>(updatedEntity);
        }
    }
}
