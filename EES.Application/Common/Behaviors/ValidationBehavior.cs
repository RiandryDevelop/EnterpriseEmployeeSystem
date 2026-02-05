using MediatR;
using FluentValidation;

namespace EES.Application.Common.Behaviors;

/// <summary>
/// A MediatR pipeline behavior that automatically validates requests using FluentValidation 
/// before they reach the command/query handlers.
/// This ensures a solid architecture by centralizing validation logic.
/// </summary>
/// <typeparam name="TRequest">The type of request being handled.</typeparam>
/// <typeparam name="TResponse">The type of response returned by the handler.</typeparam>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// Initializes a new instance of the ValidationBehavior with all registered validators for the request type.
    /// </summary>
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    /// <summary>
    /// Intercepts the request to perform asynchronous validation.
    /// </summary>
    /// <exception cref="ValidationException">Thrown when validation failures are detected.</exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            // Execute all registered validators for this request type in parallel
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            // Flatten results and collect all non-null validation errors
            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            // If any validation failures exist, throw a ValidationException 
            // to be caught by the ExceptionMiddleware 
            if (failures.Count != 0)
                throw new ValidationException(failures);
        }

        // Proceed to the next behavior in the pipeline or the final handler
        return await next();
    }
}