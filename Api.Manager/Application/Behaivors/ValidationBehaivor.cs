using FluentValidation;
using MediatR;

namespace Api.Manager.Application.Behaivors
{
    public class ValidationBehaivor<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaivor(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResult = await Task.WhenAll(_validators.Select(x => x.ValidateAsync(context, cancellationToken)));

                var failures = validationResult.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                {
                    throw new Domain.Exceptions.ValidationExceptions(failures);
                }
            }
            return await next();
        }
    }
}
