using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.GenericHandlers
{
    public class GenericCreateCommandHandler<TCommand, TEntity, TDto>
    : IRequestHandler<TCommand, TDto>
    where TCommand : IRequest<TDto>
    where TEntity : class
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public GenericCreateCommandHandler(IGenericRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TDto> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(request);
            var result = await _repository.CreateAsync(entity);
            return _mapper.Map<TDto>(result);
        }
    }
}
