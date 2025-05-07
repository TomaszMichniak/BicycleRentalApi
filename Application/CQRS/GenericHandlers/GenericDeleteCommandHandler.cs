using MediatR;
using Domain.Interfaces;
namespace Application.CQRS.GenericHandlers
{
    public interface IHasId
    {
        Guid Id { get; }
    }
    public class GenericDeleteCommandHandler<TCommand, TEntity>
        : IRequestHandler<TCommand>
        where TCommand : IRequest, IHasId
        where TEntity : class
    {
        private readonly IGenericRepository<TEntity> _repository;

        public GenericDeleteCommandHandler(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task Handle(TCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new Exception("Entity not found");
            }

            await _repository.DeleteAsync(entity);
            return;
        }
    }

}